using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HoGent_Monkey_Project.Objects
{
    class Log
    {
        private String Path { get; set; }
        private String File { get; set; }
        private List<String> Lines = new List<string>();
        private int Current { get; set; }
        private Boolean Run { get; set; }

        public Log(String Path, String File)
        {
            this.Path = Path;
            this.File = File;
            this.Current = 0;
            this.Run = true;
            Task.Run(() => this.loggingThread());
        }

        public void addLine(String line)
        {
            this.Lines.Add(line);
        }

        public void stopLogging()
        {
            this.Run = false;
        }

        private void loggingThread()
        {
            while(this.Run || this.Lines.Count > this.Current)
            {
                if (this.Lines.Count > this.Current)
                {
                    for (int i = this.Current; i < this.Lines.Count; i++)
                    {
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(this.Path + @"\" + this.File, true))
                        {
                            file.WriteLine(this.Lines[i]);
                        }

                        this.Current = i +1;
                    }
                }
            }
        } 
    }
}
