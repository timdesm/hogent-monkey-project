using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace HoGent_Monkey_Project
{
    class Wood
    {
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public List<Tree> Trees = new List<Tree>();

        public List<Monkey> Monkeys = new List<Monkey>();
        public Bitmap map { get; set; }

        private int Scale = 2;
    
        public Wood(int ID, int X, int Y, int tAmount, int mAmount)
        {
            this.ID = ID;
            this.X = X;
            this.Y = Y;
            this.map = new Bitmap(X * this.Scale, Y * this.Scale);

            Random r = new Random();
            for (int i = 0; i < tAmount; i++)
            {
                Tree tree = new Tree(this.Trees.Count, r.Next(0, this.X), r.Next(0, this.Y));
                while(this.Trees.Contains(tree))
                    tree = new Tree(this.Trees.Count, r.Next(0, this.X), r.Next(0, this.Y));
                this.Trees.Add(tree);
                this.drawTree(tree);
            }

            for(int i = 0; i < mAmount; i++)
            {
                Tree start = this.Trees[r.Next(this.Trees.Count)];
                while (this.Monkeys.Any(x => x.Start == start))
                    start = this.Trees[r.Next(this.Trees.Count)];
                Monkey monkey = new Monkey(this.Monkeys.Count, Program.monkeyNames[this.Monkeys.Count], start);
                this.Monkeys.Add(monkey);
                drawStartTree(monkey);
            }
        }

        private void drawTree(Tree tree)
        {
            Graphics g = Graphics.FromImage(this.map);
            Pen p = new Pen(Color.Green, 1);
            g.DrawEllipse(p, tree.X * this.Scale, tree.Y * this.Scale, 2f * this.Scale, 2f * this.Scale);
        }

        private void drawStartTree(Monkey monkey)
        {
            Graphics g = Graphics.FromImage(this.map);
            Brush b = new SolidBrush(Program.monkeyColors[monkey.ID]);
            g.FillEllipse(b, monkey.Start.X * this.Scale, monkey.Start.Y * this.Scale, 2f * this.Scale, 2f * this.Scale);
        }

        public void saveToImage(String folder)
        {
            this.map.Save(folder + "/" + this.ID + "_wood.jpg", ImageFormat.Jpeg);
        }
    }
}
