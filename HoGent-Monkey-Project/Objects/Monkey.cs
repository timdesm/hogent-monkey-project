using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoGent_Monkey_Project
{
    class Monkey
    {
        public int ID { get; set; }
        public String Name { get; set; }
        
        public Tree Start { get; set; }

        public List<Tree> Path = new List<Tree>();

        public Boolean Done { get; set; } 

        public Monkey(int ID, String Name, Tree Start)
        {
            this.ID = ID;
            this.Name = Name;
            this.Start = Start;
            this.Path.Add(Start);
            this.Done = false;
        }

        public Tree getNextMove(Wood wood)
        {
            Tree currentTree = this.Path.Last();
            double borderDistance = currentTree.getBorderDistance(wood);

            Dictionary<Tree, double> calulateList = new Dictionary<Tree, double>();
            foreach(Tree tree in wood.Trees)
            {
                if(!this.Path.Contains(tree))
                    calulateList.Add(tree, tree.getTreeDistance(currentTree));
            }

            var closest = calulateList.OrderBy(x => x.Value).First();
            if (borderDistance > closest.Value)
                return closest.Key;
            this.Done = true;
            return null;
        }
    }
}
