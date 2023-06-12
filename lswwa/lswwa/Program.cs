﻿using System;
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
        public static Dictionary<string, List<string>> arrs = new Dictionary<string, List<string>> { };
        public static int index = 0;

        public static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                foreach (string s in args)
                    fil = s;
                if (File.Exists(fil))
                {
                    foreach (string s in File.ReadAllLines(fil))
                        code.Add(s);
                    for(int i = 0; i <= code.Count; i++)
                    {
                        if(code[i] == "begin")
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
                    for(int i = 0; i < cod.Count; i++)
                    {
                        if (cod[i].StartsWith(";"))
                            cod.RemoveAt(i);
                        else
                            cod[i] = Globl.RemoveByLast(cod[i], ';');
                    }

                    for(index = 0; index < cod.Count; index++)
                    {
                        try
                        {
                            if (!cod[index].StartsWith("endif "))
                            {
                                if (cod[index].StartsWith("@"))
                                {
                                    try
                                    {
                                        string[] sl = Globl.SplitByFirst(cod[index].Remove(0, 1), ' ');
                                        Command.Do(sl[0], sl[1]);
                                    }
                                    catch { }
                                }
                                else if (cod[index].StartsWith("#"))
                                {
                                    try
                                    {
                                        string[] sl = Globl.SplitByFirst(cod[index].Remove(0, 1), ' ');
                                        Command.Do(sl[0], sl[1]);
                                    }
                                    catch (Exception e) { File.WriteAllText("exception", "Line::" + index + "\nText::" + cod[index] + "\nError::" + e.Message); }
                                }
                                else
                                {
                                    string[] sl = Globl.SplitByFirst(cod[index], ' ');
                                    Command.Do(sl[0], sl[1]);
                                }
                            }
                        }
                        catch (Exception e) { Console.WriteLine("Line::" + index + "\nText::" + cod[index] + "\nError::" + e.Message); Console.ReadKey(true); Environment.Exit(0); }
                        //Console.WriteLine(sl[0] + "|" + sl[1]);
                    }
                }
                else
                {
                    Console.WriteLine($"Not found file \"{fil}\"");
                }
            }
            else
            {
                Console.WriteLine("LSWWA v" + Globl.Version + "." + Globl.SubVersion + "." + Globl.Build);
                Console.WriteLine("Made by Etar125 on C#");
                Console.ReadKey(true);
            }
        }

        public static int Search(int startIndex, string text)
        {
            for (int ia = startIndex; ia < cod.Count; ia++)
            {
                if (cod[ia] == text)
                {
                    return ia;
                }
            }
            return -1;
        }
    }
}
