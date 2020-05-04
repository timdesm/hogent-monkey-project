using System;
using System.Collections.Generic;
using System.Text;

namespace HoGent_Monkey_Project
{
    class Monkey
    {
        public int ID { get; set; }
        public String Name { get; set; }
        
        public Tree Start { get; set; }

        public List<Tree> Path = new List<Tree>();

        public Monkey(int ID, String name, Tree Start)
        {
            this.ID = ID;
            this.Name = Name;
            this.Start = Start;
        }

        public void makeMove()
        {

        }
    }
}
