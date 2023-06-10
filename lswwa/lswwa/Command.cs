using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lswwa
{
    class Command
    {
        public static void Do(string func, string arg)
        {
            #region Console
            if (func == "print") {
                if (Program.vars.ContainsKey(arg)) {
                    Console.Write(Program.vars[arg]);
                }
            }
            else if (func == "pause")
                Console.ReadKey();
            #endregion
        }
    }
}
