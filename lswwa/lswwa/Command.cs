using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Runtime;

namespace lswwa
{
    class Command
    {
        public static Dictionary<string, object[]> libs = new Dictionary<string, object[]> {};
        public static Dictionary<string, object[]> slibs = new Dictionary<string, object[]> { };

        public static void Do(string func, string arg, int indx)
        {
            if (func == "print")
                Console.Write(Globl.ConvertS(arg));
            else if (func.StartsWith("$"))
            {
                func = func.Remove(0, 1);
                if (Program.funcs.ContainsKey(func))
                {
                    List<string> cod = Program.funcs[func];
                    string ag = cod[0];
                    cod.RemoveAt(0);
                    if (Program.arrs.ContainsKey(arg))
                    {
                        if (Program.arrs.ContainsKey(ag))
                            Program.arrs[ag] = Program.arrs[arg];
                        else
                            Program.arrs.Add(ag, Program.arrs[arg]);
                        int index;
                        bool intry = false;
                        for (index = 0; index < cod.Count; index++)
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
                                        int kk = Program.Search(index, "end");
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
                                    Program.ex = e;
                                    int kk = Program.Search(index, "catch");
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
                        throw new Exception("Not found list " + arg);
                }
                else
                    throw new Exception("Not found method " + func);
            }
            else if (func.StartsWith("#"))
            {
                func = func.Remove(0, 1);
                if (Program.fancs.ContainsKey(func))
                {
                    List<string> cod = Program.fancs[func];
                    int index;
                    bool intry = false;
                    for (index = 0; index < cod.Count; index++)
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
                                    int kk = Program.Search(index, "end");
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
                                Program.ex = e;
                                int kk = Program.Search(index, "catch");
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
                    throw new Exception("Not found method " + func);
            }
            else if (func.StartsWith("%"))
            {
                func = func.Remove(0, 1);
                if (Program.fincs.ContainsKey(func))
                {
                    List<string> cod = Program.fincs[func];
                    string ag = cod[0];
                    cod.RemoveAt(0);
                    if (Program.vars.ContainsKey(arg))
                    {
                        if (Program.vars.ContainsKey(ag))
                            Program.vars[ag] = Program.vars[arg];
                        else
                            Program.vars.Add(ag, Program.vars[arg]);
                        int index;
                        bool intry = false;
                        for (index = 0; index < cod.Count; index++)
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
                                        int kk = Program.Search(index, "end");
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
                                    Program.ex = e;
                                    int kk = Program.Search(index, "catch");
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
                        throw new Exception("Not found variable " + arg);
                }
                else
                    throw new Exception("Not found method " + func);
            }
            else if (func == "println")
                Console.WriteLine(Globl.ConvertS(arg));
            else if (func == "clearlists")
                Program.arrs = new Dictionary<string, List<string>> { };
            else if (func == "clearvars")
                Program.vars = new Dictionary<string, string> { };
            else if (func == "change")
            {
                Program.fil = arg;
                Program.cheel = true;
            }
            else if (func == "title")
                Console.Title = Globl.ConvertS(arg);
            else if (func == "pause")
                Console.ReadKey(true);
            else if (func == "clear")
                Console.Clear();
            else if (func == "exit")
                Environment.Exit(0);
            else if (func == "foreground")
                Console.ForegroundColor = Globl.ParseBy(arg);
            else if (func == "background")
                Console.BackgroundColor = Globl.ParseBy(arg);
            else if (func == "getline")
            {
                string text = Console.ReadLine();
                if (Program.vars.ContainsKey(arg))
                    Program.vars[arg] = text;
                else
                    Program.vars.Add(arg, text);
            }
            else if (func == "getpos")
            {
                if (Program.arrs.ContainsKey(arg))
                    Program.arrs[arg] = new List<string> { Console.WindowLeft.ToString(), Console.WindowTop.ToString() };
                else
                    Program.arrs.Add(arg, new List<string> { Console.WindowLeft.ToString(), Console.WindowTop.ToString() });
            }
            else if (func == "getsize")
            {
                if (Program.arrs.ContainsKey(arg))
                    Program.arrs[arg] = new List<string> { Console.WindowWidth.ToString(), Console.WindowHeight.ToString() };
                else
                    Program.arrs.Add(arg, new List<string> { Console.WindowWidth.ToString(), Console.WindowHeight.ToString() });
            }
            else if (func == "setpos")
            {
                if (Program.arrs.ContainsKey(arg))
                    Console.SetWindowPosition(int.Parse(Program.arrs[arg][0]), int.Parse(Program.arrs[arg][1]));
                else
                    throw new Exception("Not found array " + arg);
            }
            else if (func == "setsize")
            {
                if (Program.arrs.ContainsKey(arg))
                    Console.SetWindowSize(int.Parse(Program.arrs[arg][0]), int.Parse(Program.arrs[arg][1]));
                else
                    throw new Exception("Not found array " + arg);
            }
            else if (func == "getcpos")
            {
                if (Program.arrs.ContainsKey(arg))
                    Program.arrs[arg] = new List<string> { Console.CursorLeft.ToString(), Console.CursorTop.ToString() };
                else
                    Program.arrs.Add(arg, new List<string> { Console.CursorLeft.ToString(), Console.CursorTop.ToString() });
            }
            else if (func == "setcpos")
            {
                if (Program.arrs.ContainsKey(arg))
                    Console.SetCursorPosition(int.Parse(Program.arrs[arg][0]), int.Parse(Program.arrs[arg][1]));
                else
                    throw new Exception("Not found array " + arg);
            }
            else if (func == "wait")
                System.Threading.Thread.Sleep(int.Parse(arg));
            else if (func == "eget")
            {
                if (Program.vars.ContainsKey(arg))
                    Program.vars[arg] = Program.ex.Message;
                else
                    throw new Exception("Not found variable " + arg);
            }
            else if (func == "getkey")
            {
                string key = Console.ReadKey().Key.ToString();
                if (Program.vars.ContainsKey(arg))
                    Program.vars[arg] = key;
                else
                    Program.vars.Add(arg, key);
            }
            else if (func == "sgetkey")
            {
                string skey = Console.ReadKey().Key.ToString();
                if (Program.vars.ContainsKey(arg))
                    Program.vars[arg] = skey;
                else
                    Program.vars.Add(arg, skey);
            }
            else if (func == "goto")
            {
                int kk = Program.Search(0, ":" + arg);
                if (kk != -1)
                    indx = kk;
                else
                    throw new Exception("Not found label " + arg);
            }
            else if (func == "set")
            {
                string[] splt = Globl.SplitByFirst(arg, ' ');
                if (Program.vars.ContainsKey(splt[0]))
                    Program.vars[splt[0]] = Globl.ConvertS(splt[1]);
                else
                    Program.vars.Add(splt[0], Globl.ConvertS(splt[1]));
            }
            else if (func == "add")
            {
                string[] splt = Globl.SplitByFirst(arg, ' ');
                if (Program.arrs.ContainsKey(splt[0]))
                    Program.arrs[splt[0]].Add(Globl.ConvertS(splt[1]));
                else
                    Program.arrs.Add(splt[0], new List<string> { Globl.ConvertS(splt[1]) });
            }
            else if (func == "rem")
            {
                string[] splt = Globl.SplitByFirst(arg, ' ');
                if (Program.arrs.ContainsKey(splt[0]))
                    Program.arrs[splt[0]].RemoveAt(int.Parse(Globl.ConvertS(splt[1])));
                else
                    throw new Exception("Not found array " + splt[0]);
            }
            else if (func == "clr")
            {
                if (Program.arrs.ContainsKey(arg))
                    Program.arrs[arg].Clear();
                else
                    throw new Exception("Not found array " + arg);
            }
            else if (func == "edit")
            {
                string[] splt = Globl.SplitByFirst(arg, ' ');
                string arr = splt[0];
                string[] splt2 = Globl.SplitByFirst(splt[1], ' ');
                int index = int.Parse(Globl.ConvertS(splt2[0]));
                string var = splt2[1];
                if (Program.arrs.ContainsKey(arr))
                    Program.arrs[arr][index] = Globl.ConvertS(arg);
                else
                    throw new Exception("Not found array " + arr);
            }
            else if (func == "alen")
            {
                string[] splt = Globl.SplitByFirst(arg, ' ');
                string arr = splt[0];
                string var = splt[1];
                if (Program.arrs.ContainsKey(arr))
                {
                    if (Program.vars.ContainsKey(var))
                        Program.vars[var] = "" + Program.arrs[arr].Count;
                    else
                        throw new Exception("Not found variable " + var);
                }
                else
                    throw new Exception("Not found array " + arr);
            }
            else if (func == "len")
            {
                string[] splt = Globl.SplitByFirst(arg, ' ');
                string var1 = Globl.ConvertS(splt[0]);
                string var2 = splt[1];
                if (Program.vars.ContainsKey(var2))
                    Program.vars[var2] = "" + Program.vars[var1].Length;
                else
                    throw new Exception("Not found variable " + var2);
            }
            else if (func == "remc")
            {
                string[] splt = Globl.SplitByFirst(arg, ' ');
                string var = splt[0];
                string[] splt2 = Globl.SplitByFirst(splt[1], ' ');
                int index = int.Parse(Globl.ConvertS(splt2[0]));
                int len = int.Parse(Globl.ConvertS(splt2[1]));
                if (Program.vars.ContainsKey(var))
                    Program.vars[var] = Program.vars[var].Remove(index, len);
                else
                    throw new Exception("Not found variable " + var);
            }
            else if (func == "subs")
            {
                string[] splt = Globl.SplitByFirst(arg, ' ');
                string var = splt[0];
                string[] splt2 = Globl.SplitByFirst(splt[1], ' ');
                int index = int.Parse(Globl.ConvertS(splt2[0]));
                int len = int.Parse(Globl.ConvertS(splt2[1]));
                if (Program.vars.ContainsKey(var))
                    Program.vars[var] = Program.vars[var].Substring(index, len);
                else
                    throw new Exception("Not found variable " + var);
            }
            else if (func == "get")
            {
                string[] splt = Globl.SplitByFirst(arg, ' ');
                string arr = splt[0];
                string[] splt2 = Globl.SplitByFirst(splt[1], ' ');
                int index = int.Parse(Globl.ConvertS(splt2[0]));
                string var = splt2[1];
                if (Program.arrs.ContainsKey(arr))
                {
                    if (Program.vars.ContainsKey(var))
                        Program.vars[var] = Program.arrs[arr][index];
                    else
                        Program.vars.Add(var, Program.arrs[arr][index]);
                }
                else
                    throw new Exception("Not found array " + arr);
            }
            else if (func == "if")
            {
                string[] spl = arg.Split(' ');
                byte kn = byte.Parse(spl[0]);
                string one = Globl.ConvertS(spl[1]);
                string two = Globl.ConvertS(spl[3]);
                string oper = spl[2];
                switch (oper)
                {
                    case "==":
                        if (one != two)
                        {
                            int k1 = Program.Search(indx, "endif " + kn);
                            if (k1 != -1)
                                indx = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case "!=":
                        if (one == two)
                        {
                            int k1 = Program.Search(indx, "endif " + kn);
                            if (k1 != -1)
                                indx = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case "?=":
                        if (!one.Contains(two))
                        {
                            int k1 = Program.Search(indx, "endif " + kn);
                            if (k1 != -1)
                                indx = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case "?!":
                        if (one.Contains(two))
                        {
                            int k1 = Program.Search(indx, "endif " + kn);
                            if (k1 != -1)
                                indx = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case "(=":
                        if (!one.StartsWith(two))
                        {
                            int k1 = Program.Search(indx, "endif " + kn);
                            if (k1 != -1)
                                indx = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case "(!":
                        if (one.StartsWith(two))
                        {
                            int k1 = Program.Search(indx, "endif " + kn);
                            if (k1 != -1)
                                indx = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case ")=":
                        if (!one.EndsWith(two))
                        {
                            int k1 = Program.Search(indx, "endif " + kn);
                            if (k1 != -1)
                                indx = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case ")!":
                        if (one.EndsWith(two))
                        {
                            int k1 = Program.Search(indx, "endif " + kn);
                            if (k1 != -1)
                                indx = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case "<=":
                        double onea = double.Parse(one);
                        double twoa = double.Parse(two);
                        if (onea <= twoa)
                        {
                            int k1 = Program.Search(indx, "endif " + kn);
                            if (k1 != -1)
                                indx = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case ">=":
                        double onea2 = double.Parse(one);
                        double twoa2 = double.Parse(two);
                        if (onea2 >= twoa2)
                        {
                            int k1 = Program.Search(indx, "endif " + kn);
                            if (k1 != -1)
                                indx = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case "<":
                        double onea3 = double.Parse(one);
                        double twoa3 = double.Parse(two);
                        if (onea3 < twoa3)
                        {
                            int k1 = Program.Search(indx, "endif " + kn);
                            if (k1 != -1)
                                indx = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case ">":
                        double onea4 = double.Parse(one);
                        double twoa4 = double.Parse(two);
                        if (onea4 > twoa4)
                        {
                            int k1 = Program.Search(indx, "endif " + kn);
                            if (k1 != -1)
                                indx = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                }
            }

            else if (func == "include")
            {
                string[] sl = Globl.SplitByFirst(arg, ' ');
                string dll = sl[0];
                string name = sl[1];
                if (File.Exists(dll + ".dll"))
                {
                    Assembly a = Assembly.Load(dll); // Загружаем библиотеку
                    Object o = a.CreateInstance("Program"); // Получаем классы
                    Type t = a.GetType(name + ".Program"); // Получаем класс
                    MethodInfo mi = t.GetMethod("Do"); // Получаем метод
                    libs.Add(name, new object[] { o, mi });
                }
                else
                    throw new Exception("Not found file " + dll + ".dll");
            }
            else if (func == "sinclude")
            {
                string[] sl = Globl.SplitByFirst(arg, ' ');
                string dll = sl[0];
                string name = sl[1];
                if (File.Exists(dll + ".lib"))
                {
                    Dictionary<string, List<string>> funcs = new Dictionary<string, List<string>> { };
                    Dictionary<string, List<string>> fancs = new Dictionary<string, List<string>> { };
                    Dictionary<string, List<string>> fincs = new Dictionary<string, List<string>> { };
                    List<string> code = new List<string> { };
                    List<string> cod = new List<string> { };
                    foreach (string s in File.ReadAllLines(dll + ".lib"))
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
                                        doom = true;
                                        break;
                                    }
                                    else if (coda[a] != String.Empty)
                                    {
                                        if (coda[a].StartsWith("$"))
                                        {
                                            List<string> mcode = new List<string> { };
                                            string[] sla = Globl.SplitByFirst(code[a], ' ');
                                            string nam = sla[0].Remove(0, 1);
                                            string ar = sla[1];
                                            mcode.Add(ar);
                                            int kk = Program.ISearch(coda, a + 1, "$end");
                                            if (kk != -1)
                                            {
                                                for (int k = a + 1; k < kk; k++)
                                                    if (!coda[k].StartsWith(";"))
                                                        mcode.Add(Globl.RemoveByLast(coda[k].Trim(), ';'));
                                                funcs.Add(nam, mcode);
                                                a = kk;
                                            }
                                            else
                                                throw new Exception("Not found " + nam + " end in library " + dll);
                                        }
                                        else if (coda[a].StartsWith("#"))
                                        {
                                            List<string> mcode = new List<string> { };
                                            string nam = coda[a].Remove(0, 1);
                                            int kk = Program.ISearch(coda, a + 1, "#end");
                                            if (kk != -1)
                                            {
                                                for (int k = a + 1; k < kk; k++)
                                                    if (!coda[k].StartsWith(";"))
                                                        mcode.Add(Globl.RemoveByLast(coda[k].Trim(), ';'));
                                                fancs.Add(nam, mcode);
                                                a = kk;
                                            }
                                            else
                                                throw new Exception("Not found " + name + " end in library " + dll);
                                        }
                                        else if (coda[a].StartsWith("%"))
                                        {
                                            List<string> mcode = new List<string> { };
                                            string[] sla = Globl.SplitByFirst(code[a], ' ');
                                            string nam = sla[0].Remove(0, 1);
                                            string ar = sla[1];
                                            mcode.Add(ar);
                                            int kk = Program.ISearch(coda, a + 1, "%end");
                                            if (kk != -1)
                                            {
                                                for (int k = a + 1; k < kk; k++)
                                                    if (!coda[k].StartsWith(";"))
                                                        mcode.Add(Globl.RemoveByLast(coda[k].Trim(), ';'));
                                                fincs.Add(nam, mcode);
                                                a = kk;
                                            }
                                            else
                                                throw new Exception("Not found " + name + " end in library " + dll);
                                        }
                                    }
                                }
                                catch (Exception e) { Console.WriteLine("Line::" + a + "\nText::" + coda[a] + "\nError::" + e.Message); Console.ReadKey(true); Environment.Exit(0); }
                            }
                        }
                        else if (code[i] == "begin")
                        {
                            break;
                        }
                        else
                        {
                            try
                            {
                                if (code[i] != String.Empty)
                                {
                                    string[] sla = Globl.SplitByFirst(code[i], ' ');
                                    string nam = sla[0];
                                    string[] sl2 = Globl.SplitByFirst(sla[1], ' ');
                                    string type = sl2[0];
                                    string text = sl2[1];
                                    if (type == "var")
                                        Program.vars.Add(nam, text);
                                    else if (type == "list")
                                        Program.arrs.Add(nam, new List<string> { });
                                    else
                                        throw new Exception("Unkown type " + type);
                                }
                            }
                            catch (Exception e) { Console.WriteLine("Line::" + i + "\nText::" + code[i] + "\nError::" + e.Message); Console.ReadKey(true); Environment.Exit(0); }
                        }
                    }
                    slibs.Add(name, new object[] { fancs, fincs, funcs });
                }
                else
                    throw new Exception("Not found file " + dll + ".lib");
            }

            else
            {
                if (!func.Contains("."))
                    throw new Exception("Not found function " + func);
                else
                {
                    string[] ll = Globl.SplitByFirst(func, '.');
                    if (slibs.ContainsKey(ll[0]))
                    {
                        Dictionary<string, List<string>> funcs = slibs[ll[0]][2] as Dictionary<string, List<string>>;
                        Dictionary<string, List<string>> fancs = slibs[ll[0]][0] as Dictionary<string, List<string>>;
                        Dictionary<string, List<string>> fincs = slibs[ll[0]][1] as Dictionary<string, List<string>>;
                        if (fancs.ContainsKey(ll[1]))
                        {
                            List<string> cod = fancs[ll[1]];
                            int index;
                            bool intry = false;
                            for (index = 0; index < cod.Count; index++)
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
                                            int kk = Program.Search(index, "end");
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
                                        Program.ex = e;
                                        int kk = Program.Search(index, "catch");
                                        if (kk != -1)
                                        {
                                            index = kk;
                                            intry = false;
                                        }
                                    }
                                }
                            }
                        }
                        else if (fincs.ContainsKey(ll[1]))
                        {
                            List<string> cod = fincs[ll[1]];
                            string ag = cod[0];
                            cod.RemoveAt(0);
                            if (Program.vars.ContainsKey(arg))
                            {
                                if (Program.vars.ContainsKey(ag))
                                    Program.vars[ag] = Program.vars[arg];
                                else
                                    Program.vars.Add(ag, Program.vars[arg]);
                                int index;
                                bool intry = false;
                                for (index = 0; index < cod.Count; index++)
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
                                                int kk = Program.Search(index, "end");
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
                                            Program.ex = e;
                                            int kk = Program.Search(index, "catch");
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
                                throw new Exception("Not found variable " + arg);
                        }
                        else if (funcs.ContainsKey(ll[1]))
                        {
                            List<string> cod = funcs[ll[1]];
                            string ag = cod[0];
                            cod.RemoveAt(0);
                            if (Program.arrs.ContainsKey(arg))
                            {
                                if (Program.arrs.ContainsKey(ag))
                                    Program.arrs[ag] = Program.arrs[arg];
                                else
                                    Program.arrs.Add(ag, Program.arrs[arg]);
                                int index;
                                bool intry = false;
                                for (index = 0; index < cod.Count; index++)
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
                                                int kk = Program.Search(index, "end");
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
                                            Program.ex = e;
                                            int kk = Program.Search(index, "catch");
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
                                throw new Exception("Not found array " + arg);
                        }
                    }
                    else if (libs.ContainsKey(ll[0]))
                    {
                        object[] objs = libs[ll[0]];
                        object[] args =
                        {
                                    ll[1],
                                    arg,
                                    new object[]
                                    {
                                        Program.fil,
                                        Program.cod,
                                        Program.vars,
                                        Program.arrs,
                                        indx,
                                        Globl.Version + "." + Globl.SubVersion + "." + Globl.Build
                                    }
                                };
                        Object o = objs[0];
                        MethodInfo mi = (MethodInfo)objs[1];
                        mi.Invoke(o, args);
                    }
                    else
                        throw new Exception("Not found library " + ll[0]);
                }
            }
        }
    }
}