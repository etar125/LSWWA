using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lswwa
{
    class Globl
    {
        public static int Version = 0; // Основная версия
        public static int SubVersion = 1; // Дополнительная версия
        public static int Build = 106231925; // Версия сборки: ДЕНЬ-МЕСЯЦ-ГОД-час-время~

        public static string RemoveByLast(string text, char symbol)
        {
            if (text.Contains(symbol))
            {
                int kk = text.LastIndexOf(symbol);
                return text.Substring(0, kk).Trim();
            }
            else
                return text;
        }

        public static string[] SplitByFirst(string text, char sym)
        {
            string first = "";
            string second = "";
            if (text.Contains(sym))
            {
                int m = 0;
                for (int a = 0; a < text.Length; a++)
                {
                    if (text[a] == sym)
                    {
                        m = a;
                        break;
                    }
                }
                first = text.Substring(0, m);
                second = text.Substring(m + 1);
            }
            else
                return new string[] { text, "" };
            return new string[] { first, second };
        }

        public static ConsoleColor ParseBy(string color)
        {
            string clr = color.ToLower();
            if (clr == "white")
                return ConsoleColor.White;
            else if (clr == "black")
                return ConsoleColor.Black;
            else if (clr == "red")
                return ConsoleColor.Red;
            else if (clr == "green")
                return ConsoleColor.Green;
            else if (clr == "blue")
                return ConsoleColor.Blue;
            else if (clr == "yellow")
                return ConsoleColor.Yellow;
            else if (clr == "cyan")
                return ConsoleColor.Cyan;
            else if (clr == "gray")
                return ConsoleColor.Gray;
            else if (clr == "magenta")
                return ConsoleColor.Magenta;
            else if (clr == "darkblue")
                return ConsoleColor.DarkBlue;
            else if (clr == "darkcyan")
                return ConsoleColor.DarkCyan;
            else if (clr == "darkgray")
                return ConsoleColor.DarkGray;
            else if (clr == "darkgreen")
                return ConsoleColor.DarkGreen;
            else if (clr == "darkmagenta")
                return ConsoleColor.DarkMagenta;
            else if (clr == "darkred")
                return ConsoleColor.DarkRed;
            else if (clr == "darkyellow")
                return ConsoleColor.DarkYellow;
            return ConsoleColor.White;
        }

        public static ConsoleColor ParseBy(int color)
        {
            return (ConsoleColor)color;
        }
    }
}
