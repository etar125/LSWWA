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
            switch(func)
            {
                case "print":
                    if (Program.vars.ContainsKey(arg)) {
                        Console.Write(Program.vars[arg]);
                    }
                    else {
                        throw new Exception("Not found variable " + arg);
                    }
                    break;
                case "pause":
                    Console.ReadKey(true);
                    break;
                case "getline":
                    string text = Console.ReadLine();
                    if (Program.vars.ContainsKey(arg))
                        Program.vars[arg] = text;
                    else
                        Program.vars.Add(arg, text);
                    break;
                case "getkey":
                    string key = Console.ReadKey().Key.ToString();
                    if (Program.vars.ContainsKey(arg))
                        Program.vars[arg] = key;
                    else
                        Program.vars.Add(arg, key);
                    break;
                case "sgetkey":
                    string skey = Console.ReadKey().Key.ToString();
                    if (Program.vars.ContainsKey(arg))
                        Program.vars[arg] = skey;
                    else
                        Program.vars.Add(arg, skey);
                    break;
                case "goto":
                    int kk = Program.Search(0, arg + ":");
                    if (kk != -1)
                        Program.index = kk;
                    else
                        throw new Exception("Not found label " + arg);
                    break;
                case "set":
                    string[] splt = arg.Split(' ');
                    if (Program.vars.ContainsKey(splt[0]))
                        Program.vars[splt[0]] = Globl.ConvertS(splt[1]);
                    else
                        throw new Exception("Not found variable " + splt[0]);
                    break;
                case "if":
                    string[] spl = arg.Split(' ');
                    byte kn = byte.Parse(spl[0]);
                    string one = "";
                    string two = "";
                    string oper = spl[2];
                    if (Program.vars.ContainsKey(spl[1])) one = Program.vars[spl[1]];
                    else throw new Exception("Not found variable " + spl[1]);
                    if (Program.vars.ContainsKey(spl[3])) two = Program.vars[spl[3]];
                    else throw new Exception("Not found variable " + spl[3]);
                    switch(oper)
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
                    break;

                case "include":
                    if (File.Exists(arg + ".dll"))
                    {
                        Assembly a = Assembly.Load(arg); // Загружаем библиотеку
                        Object o = a.CreateInstance("Program"); // Получаем классы
                        Type t = a.GetType("testdll.Program"); // Получаем класс
                        MethodInfo mi = t.GetMethod("Do"); // Получаем метод
                        libs.Add(arg, new object[] { o, mi });
                    }
                    else
                        throw new Exception("Not found file " + arg);
                    break;

                default:
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
                    break;
            }
        }
    }
}
