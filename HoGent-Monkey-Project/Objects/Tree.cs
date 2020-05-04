using System;
using System.Collections.Generic;
using System.Text;

namespace HoGent_Monkey_Project
{
    class Tree
    {
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Tree(int ID, int X, int Y)
        {
            this.ID = ID;
            this.X = X;
            this.Y = Y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Tree))
                return false;
            return (this.X == ((Tree)obj).X) && (this.Y == ((Tree)obj).Y);
        }
    }
}
