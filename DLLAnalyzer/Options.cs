using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLLAnalyzer
{
    public class Options
    {
        public string File { get; set; }
        public string ClassRegexp { get; set; } = ".*";
        public string MethodRegexp { get; set; } = ".*";
        /// <summary>
        /// c - for classes
        /// m - for methods 
        /// f - for full
        /// </summary>
        public eDepth Depth {get;set;} = eDepth.Full;
        
        public enum eDepth
        {
            Full, Class, Method
        }

        public void Parse(string[] args)
        {
            for(int i=0; i < args.Length; i++)
            {
                var arg = args[i];
                if(arg == "-cr")
                {
                    this.ClassRegexp = args[i+1];
                    i++;
                    continue;
                }
                if(arg == "-mr")
                {
                    this.MethodRegexp = args[i + 1];
                    i++;
                    continue;
                }
                if(arg == "-d")
                {
                    if (args[i+1] == "method")
                    {
                        this.Depth = eDepth.Method;
                        i++;
                        continue;
                    }
                    if (args[i + 1] == "class")
                    {
                        this.Depth = eDepth.Class;
                        i++;
                        continue;
                    }
                    if (args[i + 1] == "full")
                    {
                        this.Depth = eDepth.Full;
                        i++;
                        continue;
                    }
                }
                if(i == args.Length-1)
                {
                    this.File = args[i];
                }
            }
        }
    }
}
