using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Globalization;

namespace lswwa
{
    class Program
    {
        public static string fil = "";
        public static List<string> code = new List<string> { };
        public static List<string> cod = new List<string> { };
        public static Dictionary<string, string> vars = new Dictionary<string, string> { };
        public static Dictionary<string, List<string>> arrs = new Dictionary<string, List<string>>
        {
            { "lswwaver", new List<string> { Globl.Version.ToString(), Globl.SubVersion.ToString(), Globl.Build.ToString() } }
        };
        public static Dictionary<string, List<string>> funcs = new Dictionary<string, List<string>> { };
        public static Dictionary<string, List<string>> fancs = new Dictionary<string, List<string>> { };
        public static Dictionary<string, List<string>> fincs = new Dictionary<string, List<string>> { };
        public static Exception ex = new Exception("no error");
        public static bool intry = false;
        public static int index = 0;
        public static bool cheel = false;
        public static bool dor = true;

        public static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                foreach (string s in args)
                    fil = s;
                while (dor)
                {
                    oth();
                    cheel = false;
                }
            }
            else
            {
                Console.WriteLine("LSWWA v" + Globl.Version + "." + Globl.SubVersion + "." + Globl.Build);
                Console.WriteLine("Made by Etar125 on C#");
                Console.ReadKey(true);
            }
        }

        public static void oth()
        {
            if (File.Exists(fil))
            {
                code = new List<string> { };
                cod = new List<string> { };
                foreach (string s in File.ReadAllLines(fil))
                    code.Add(s);
                bool doom = false;
                for (int i = 0; i < code.Count && !doom; i++)
                {
                    if (code[i] == "methods")
                    {
                        List<string> coda = code;
                        for (int a = i + 1; a < coda.Count; a++)
                        {
                            try
                            {
                                coda[a] = coda[a].Trim();
                                if (coda[a] == "begin")
                                {
                                    index = a + 1;
                                    doom = true;
                                    break;
                                }
                                else if (coda[a] != String.Empty)
                                {
                                    if (coda[a].StartsWith("$"))
                                    {
                                        List<string> mcode = new List<string> { };
                                        string[] sl = Globl.SplitByFirst(code[a], ' ');
                                        string name = sl[0].Remove(0, 1);
                                        string arg = sl[1];
                                        mcode.Add(arg);
                                        int kk = ISearch(coda, a + 1, "$end");
                                        if (kk != -1)
                                        {
                                            for (int k = a + 1; k < kk; k++)
                                                if (!coda[k].StartsWith(";"))
                                                    mcode.Add(Globl.RemoveByLast(coda[k].Trim(), ';'));
                                            funcs.Add(name, mcode);
                                            a = kk;
                                        }
                                        else
                                            throw new Exception("Not found " + name + " end");
                                    }
                                    else if (coda[a].StartsWith("#"))
                                    {
                                        List<string> mcode = new List<string> { };
                                        string name = coda[a].Remove(0, 1);
                                        int kk = ISearch(coda, a + 1, "#end");
                                        if (kk != -1)
                                        {
                                            for (int k = a + 1; k < kk; k++)
                                                if (!coda[k].StartsWith(";"))
                                                    mcode.Add(Globl.RemoveByLast(coda[k].Trim(), ';'));
                                            fancs.Add(name, mcode);
                                            a = kk;
                                        }
                                        else
                                            throw new Exception("Not found " + name + " end");
                                    }
                                    else if (coda[a].StartsWith("%"))
                                    {
                                        List<string> mcode = new List<string> { };
                                        string[] sl = Globl.SplitByFirst(code[a], ' ');
                                        string name = sl[0].Remove(0, 1);
                                        string arg = sl[1];
                                        mcode.Add(arg);
                                        int kk = ISearch(coda, a + 1, "%end");
                                        if (kk != -1)
                                        {
                                            for (int k = a + 1; k < kk; k++)
                                                if (!coda[k].StartsWith(";"))
                                                    mcode.Add(Globl.RemoveByLast(coda[k].Trim(), ';'));
                                            fincs.Add(name, mcode);
                                            a = kk;
                                        }
                                        else
                                            throw new Exception("Not found " + name + " end");
                                    }
                                }
                            }
                            catch (Exception e) { Console.WriteLine("Line::" + a + "\nText::" + coda[a] + "\nError::" + e.Message); Console.ReadKey(true); Environment.Exit(0); }
                        }
                    }
                    else if (code[i] == "begin")
                    {
                        index = i + 1;
                        break;
                    }
                    else
                    {
                        try
                        {
                            if (code[i] != String.Empty)
                            {
                                string[] sl = Globl.SplitByFirst(code[i], ' ');
                                string name = sl[0];
                                string[] sl2 = Globl.SplitByFirst(sl[1], ' ');
                                string type = sl2[0];
                                string text = sl2[1];
                                if (type == "var")
                                    vars.Add(name, text);
                                else if (type == "list")
                                    arrs.Add(name, new List<string> { });
                                else
                                    throw new Exception("Unkown type " + type);
                            }
                        }
                        catch (Exception e) { Console.WriteLine("Line::" + i + "\nText::" + code[i] + "\nError::" + e.Message); Console.ReadKey(true); Environment.Exit(0); }
                    }
                }
                for (int i = index; i < code.Count; i++)
                    cod.Add(code[i]);
                for (int i = 0; i < cod.Count; i++)
                    cod[i] = cod[i].Trim();
                for (int i = 0; i < cod.Count; i++)
                {
                    if (cod[i].StartsWith(";"))
                        cod.RemoveAt(i);
                    else
                        cod[i] = Globl.RemoveByLast(cod[i], ';');
                }

                for (index = 0; index < cod.Count && !cheel; index++)
                {
                    try
                    {
                        if (!cod[index].StartsWith("endif ") && cod[index] != "end" && !cod[index].StartsWith(":") && cod[index] != "catch")
                        {
                            if (cod[index] == "try")
                                intry = true;
                            else if (cod[index] == "catch" && intry)
                            {
                                intry = false;
                                int kk = Search(index, "end");
                                if (kk != -1)
                                {
                                    index = kk;
                                }
                            }
                            else if (cod[index].StartsWith("@"))
                            {
                                try
                                {
                                    string[] sl = Globl.SplitByFirst(cod[index].Remove(0, 1), ' ');
                                    Command.Do(sl[0], sl[1], index);
                                }
                                catch { }
                            }
                            else if (cod[index].StartsWith("!"))
                            {
                                try
                                {
                                    string[] sl = Globl.SplitByFirst(cod[index].Remove(0, 1), ' ');
                                    Command.Do(sl[0], sl[1], index);
                                }
                                catch (Exception e) { File.WriteAllText("exception", "Line::" + index + "\nText::" + cod[index] + "\nError::" + e.Message); }
                            }
                            else
                            {
                                string[] sl = Globl.SplitByFirst(cod[index], ' ');
                                Command.Do(sl[0], sl[1], index);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (!intry)
                        {
                            Console.WriteLine("Line::" + index + "\nText::" + cod[index] + "\nError::" + e.Message); Console.ReadKey(true); Environment.Exit(0);
                        }
                        else
                        {
                            ex = e;
                            int kk = Search(index, "catch");
                            if (kk != -1)
                            {
                                index = kk;
                                intry = false;
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"Not found file \"{fil}\"");
            }
            if (!cheel) dor = false;
        }

        public static int Search(int startIndex, string text)
        {
            for (int ia = startIndex; ia < cod.Count; ia++)
            {
                if (cod[ia].Trim() == text)
                {
                    return ia;
                }
            }
            return -1;
        }
        public static int ISearch(List<string> where, int startIndex, string text)
        {
            for (int ia = startIndex; ia < where.Count; ia++)
            {
                if (where[ia].Trim() == text)
                {
                    return ia;
                }
            }
            return -1;
        }
    }
}
