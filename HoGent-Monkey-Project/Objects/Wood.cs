using HoGent_Monkey_Project.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HoGent_Monkey_Project
{
    class Wood
    {
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public List<Tree> Trees = new List<Tree>();

        public List<Monkey> Monkeys = new List<Monkey>();

        private Log Log { get; set; }
        private List<String> dataLog = new List<String>();

        public Bitmap map { get; set; }
        private Graphics Graphics { get; set; }
        
        private int Scale = 2;
        
    
        public Wood(int ID, int X, int Y, int tAmount, int mAmount)
        {
            this.ID = ID;
            this.X = X;
            this.Y = Y;
            this.map = new Bitmap(X * this.Scale, Y * this.Scale);
            this.Graphics = Graphics.FromImage(this.map);
            this.Log = new Log(Program.ExportPath, DateTime.Now.ToString("yyyy-MM-dd") + "_" + this.ID + "_log.txt");

            Program.printHeader();
            Console.WriteLine("Start logging wood " + ID);
            Console.WriteLine("--------------------");
            this.log("Started generating map...");

            Random r = new Random();
            for (int i = 0; i < tAmount; i++)
            {
                Tree tree = new Tree(this.Trees.Count, r.Next(0, this.X), r.Next(0, this.Y));
                while(this.Trees.Contains(tree))
                    tree = new Tree(this.Trees.Count, r.Next(0, this.X), r.Next(0, this.Y));
                this.Trees.Add(tree);
                this.drawTree(tree);
            }

            this.log("Map generated with " + tAmount + " trees");

            Task.Run(() => this.saveTreesThread());

            this.log("Start generating monkeys...");

            for (int i = 0; i < mAmount; i++)
            {
                Tree start = this.Trees[r.Next(this.Trees.Count)];
                while (this.Monkeys.Any(x => x.Start == start))
                    start = this.Trees[r.Next(this.Trees.Count)];
                Monkey monkey = new Monkey(this.Monkeys.Count, Program.monkeyNames[this.Monkeys.Count], start);
                this.Monkeys.Add(monkey);
                drawStartTree(monkey);
                this.log($"[ID: {monkey.ID}] New monkey {monkey.Name}, {Program.monkeyColors[monkey.ID].ToString()}");
            }

            this.log("Monkey generation done, generated " + mAmount + " monkeys");
            Thread.Sleep(500);
            this.moveMonkeys();

            while(this.Monkeys.Any(x => x.Done == false)) {}
            Thread.Sleep(5);
            foreach (Monkey monkey in this.Monkeys)
                Task.Run(() => this.saveMonkeyThread(monkey));
            Task.Run(() => this.saveDataLogThread());
            this.log("Start drawing paths...");
            this.generateImagePath();
            this.log("Paths drawed on map");
            this.saveToImage(Program.ExportPath);
            this.Log.stopLogging();

            Thread.Sleep(500);
            Console.WriteLine(" ");
            Console.Write("Press ENTER to continue...");
            Console.ReadLine();
        }

        public void saveToImage(String folder)
        {
            String fileName = DateTime.Now.ToString("yyyy-MM-dd") + "_" + this.ID + "_map.jpg";
            this.map.Save(folder + "/" + fileName, ImageFormat.Jpeg);
            this.log("Saved image as " + fileName);
        }

        private void moveMonkeys()
        {
            this.log("Start moving monkeys...");
            foreach (Monkey monkey in this.Monkeys)
            {
                Task thread = Task.Run(() => moveMonkeyThread(monkey));
            }
        }

        private void moveMonkeyThread(Monkey monkey)
        {
            Boolean run = true;
            while(run)
            {
                Tree move = monkey.getNextMove(this);
                if(move != null)
                {
                    // this.drawPath(monkey, move);
                    monkey.Path.Add(move);
                    this.log($"[ID: {monkey.ID}] {monkey.Name} moved to {move.X},{move.Y}");
                    this.dataLog.Add($"{monkey.ID};{monkey.Name} is now in tree {move.ID} at location ({move.X},{move.Y})");
                    continue;
                }
                run = false;
                this.log($"[ID: {monkey.ID}] {monkey.Name} reached the border");
            }
        }

        private void saveTreesThread()
        {
            this.log($"Saving trees to database...");
            DatabaseManager.logTrees(this.ID, this.Trees);
            this.log($"All trees saved to database");
        }

        private void saveMonkeyThread(Monkey monkey)
        {
            this.log($"Saving monkey [ID: {monkey.ID}] to database...");
            DatabaseManager.logMonkeys(this.ID, monkey);
            this.log($"Monkey [ID: {monkey.ID}] saved to database");
        }

        private void saveDataLogThread()
        {
            this.log($"Saving data log to database...");
            DatabaseManager.log(this.ID, this.dataLog);
            this.log($"Data log saved to database");
        }

        private void drawTree(Tree tree)
        {
            Graphics g = this.Graphics;
            Pen p = new Pen(Color.Green, 1);
            g.DrawEllipse(p, tree.X * this.Scale -2, tree.Y * this.Scale - 2, 2f * this.Scale, 2f * this.Scale);
        }

        private void drawStartTree(Monkey monkey)
        {
            Graphics g = this.Graphics;
            Brush b = new SolidBrush(Program.monkeyColors[monkey.ID]);
            g.FillEllipse(b, monkey.Start.X * this.Scale - 2, monkey.Start.Y * this.Scale - 2, 2f * this.Scale, 2f * this.Scale);
        }

        private void drawPath(Color color, Tree from, Tree to)
        {
            Graphics g = this.Graphics;
            Pen p = new Pen(color, 1);
            g.DrawLine(p, from.X * this.Scale, from.Y * this.Scale, to.X * this.Scale, to.Y * this.Scale);
        }

        private void generateImagePath()
        {
            foreach(Monkey monkey in this.Monkeys)
            {
                for (int i = 0; i < monkey.Path.Count - 1; i++)
                {
                    this.drawPath(Program.monkeyColors[monkey.ID], monkey.Path[i], monkey.Path[i + 1]);
                }
            }
        }

        private void log(String log)
        {
            String time = DateTime.Now.ToString("HH:mm:ss");
            String line = $"[{time}] " + log;
            this.Log.addLine(line);
            Console.WriteLine(line);
        }
    }
}
