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

        public static void Do(string func, string arg)
        {
            if (func == "print")
            {
                if (Program.vars.ContainsKey(arg))
                    Console.Write(Globl.ConvertS(arg));
                else
                    throw new Exception("Not found variable " + arg);
            }
            else if (func == "println")
            {
                if (Program.vars.ContainsKey(arg))
                    Console.WriteLine(Globl.ConvertS(arg));
                else
                    throw new Exception("Not found variable " + arg);
            }
            else if (func == "title")
            {
                if (Program.vars.ContainsKey(arg))
                    Console.Title = Globl.ConvertS(arg);
                else
                    throw new Exception("Not found variable " + arg);
            }
            else if (func == "pause")
                Console.ReadKey(true);
            else if (func == "clear")
                Console.ReadKey(true);
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
                int kk = Program.Search(0, arg + ":");
                if (kk != -1)
                    Program.index = kk;
                else
                    throw new Exception("Not found label " + arg);
            }
            else if (func == "set")
            {
                string[] splt = arg.Split(' ');
                if (Program.vars.ContainsKey(splt[0]))
                    Program.vars[splt[0]] = Globl.ConvertS(splt[1]);
                else
                    throw new Exception("Not found variable " + splt[0]);
            }
            else if (func == "add")
            {
                string[] splt = Globl.SplitByFirst(arg, ' ');
                if (Program.arrs.ContainsKey(splt[0]))
                    Program.arrs[splt[0]].Add(Globl.ConvertS(splt[1]));
                else
                    throw new Exception("Not found array " + splt[0]);
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
                if (Program.vars.ContainsKey(splt[0]))
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
                if (Program.vars.ContainsKey(splt[0]))
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
                if (Program.arrs.ContainsKey(splt[0]))
                {
                    if (Program.vars.ContainsKey(var))
                        Program.vars[var] = Program.arrs[arr][index];
                    else
                        throw new Exception("Not found variable " + var);
                }
                else
                    throw new Exception("Not found array " + splt[0]);
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
                            int k1 = Program.Search(Program.index, "endif " + kn);
                            if (k1 != -1)
                                Program.index = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case "!=":
                        if (one == two)
                        {
                            int k1 = Program.Search(Program.index, "endif " + kn);
                            if (k1 != -1)
                                Program.index = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case "?=":
                        if (!one.Contains(two))
                        {
                            int k1 = Program.Search(Program.index, "endif " + kn);
                            if (k1 != -1)
                                Program.index = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case "?!":
                        if (one.Contains(two))
                        {
                            int k1 = Program.Search(Program.index, "endif " + kn);
                            if (k1 != -1)
                                Program.index = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case "(=":
                        if (!one.StartsWith(two))
                        {
                            int k1 = Program.Search(Program.index, "endif " + kn);
                            if (k1 != -1)
                                Program.index = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case "(!":
                        if (one.StartsWith(two))
                        {
                            int k1 = Program.Search(Program.index, "endif " + kn);
                            if (k1 != -1)
                                Program.index = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case ")=":
                        if (!one.EndsWith(two))
                        {
                            int k1 = Program.Search(Program.index, "endif " + kn);
                            if (k1 != -1)
                                Program.index = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case ")!":
                        if (one.EndsWith(two))
                        {
                            int k1 = Program.Search(Program.index, "endif " + kn);
                            if (k1 != -1)
                                Program.index = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case "<=":
                        double onea = double.Parse(one);
                        double twoa = double.Parse(two);
                        if (onea <= twoa)
                        {
                            int k1 = Program.Search(Program.index, "endif " + kn);
                            if (k1 != -1)
                                Program.index = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case ">=":
                        double onea2 = double.Parse(one);
                        double twoa2 = double.Parse(two);
                        if (onea2 >= twoa2)
                        {
                            int k1 = Program.Search(Program.index, "endif " + kn);
                            if (k1 != -1)
                                Program.index = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case "<":
                        double onea3 = double.Parse(one);
                        double twoa3 = double.Parse(two);
                        if (onea3 < twoa3)
                        {
                            int k1 = Program.Search(Program.index, "endif " + kn);
                            if (k1 != -1)
                                Program.index = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                    case ">":
                        double onea4 = double.Parse(one);
                        double twoa4 = double.Parse(two);
                        if (onea4 > twoa4)
                        {
                            int k1 = Program.Search(Program.index, "endif " + kn);
                            if (k1 != -1)
                                Program.index = k1;
                            else
                                throw new Exception("Not found endif " + kn);
                        }
                        break;
                }
            }

                else if (func == "include")
                {
                    if (File.Exists(arg + ".dll"))
                    {
                        Assembly a = Assembly.Load(arg); // Загружаем библиотеку
                        Object o = a.CreateInstance("Program"); // Получаем классы
                        Type t = a.GetType(arg + ".Program"); // Получаем класс
                        MethodInfo mi = t.GetMethod("Do"); // Получаем метод
                        libs.Add(arg, new object[] { o, mi });
                    }
                    else
                        throw new Exception("Not found file " + arg);
                }

                else
                {
                    if (!func.Contains("."))
                        throw new Exception("Not found function " + func);
                    else
                    {
                        string[] ll = Globl.SplitByFirst(func, '.');
                        if (libs.ContainsKey(ll[0]))
                        {
                            object[] objs = libs[ll[0]];
                            object[] args =
                            {
                                    ll[1],
                                    arg,
                                    Program.fil,
                                    Program.cod,
                                    Program.vars,
                                    Program.index,
                                    Globl.Version + "." + Globl.SubVersion + "." + Globl.Build
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