﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day_7
{
    class Program
    {
        static class Globals
        {
            public static List<string> phasepermutations = new List<string>();
        }
        static void Main(string[] args)
        {
            var text = File.ReadAllText("input.txt");
            string[] split = text.Split(",");
            int[] original = new int[100000];
            int[] opcodes= new int[100000];
            int counter = 0;
            
            foreach (string x in split)
            {
                original[counter] =Int32.Parse(x);
                counter++;
            }
            Array.Copy(original,opcodes,100000);

            string phases = "01234";
            char[] arr = phases.ToCharArray();
            GetPer(arr); //put all permutation in that global list

            List<int> finalsignals = new List<int>(); //all final signals from the 5 compute rounds stored here, find the max value here

            foreach (string perm in Globals.phasepermutations){
                char[] currentperm = perm.ToCharArray();
                int[] intperm = Array.ConvertAll(currentperm, c => (int)Char.GetNumericValue(c));
                int result =0;
                result = Compute(opcodes,intperm[0],0);
                result = Compute(opcodes,intperm[1],result);
                result = Compute(opcodes,intperm[2],result);
                result = Compute(opcodes,intperm[3],result);
                result = Compute(opcodes,intperm[4],result);
                finalsignals.Add(result);
            }

            int maxSignal = finalsignals.Max(t => t);
            Console.WriteLine("Maximum thrust signal reached was: " + maxSignal);
        }
            
        private static void Swap(ref char a, ref char b)
        {
            if (a == b) return;

            var temp = a;
            a = b;
            b = temp;
        }

        public static void GetPer(char[] list)
        {
            int x = list.Length - 1;
            GetPer(list, 0, x);
        }

        private static void GetPer(char[] list, int k, int m)
        {
            if (k == m)
            {
                //Console.Write(list);
                string perm = new string(list);
                Globals.phasepermutations.Add(perm);
            }
            else
                for (int i = k; i <= m; i++)
                {
                    Swap(ref list[k], ref list[i]);
                    GetPer(list, k + 1, m);
                    Swap(ref list[k], ref list[i]);
                }
        }
    
            
           
        
        static int Compute (int[] opcodes, int phase, int inputsignal) 
        {
            int inputshandled = 0;
            for (int position = 0; ; )
            {
                int actualOpcode = opcodes[position] % 100; //last two digits
                bool isPosMode1 = (opcodes[position] / 100) % 10 == 0; // digit before last two
                bool isPosMode2 = (opcodes[position] / 1000) % 10 == 0; //digit before last three
                bool isPosMode3 = (opcodes[position] / 10000) % 10 == 0; // digit before last four

                //Console.WriteLine("Opcode at position 0 is: " + actualOpcode);
                //Console.WriteLine("IsPositionalMode values are " + isPosMode1+isPosMode2+isPosMode3);
                
                //Console.WriteLine("Processing opcodes: " + opcodes[position] + "," + opcodes[position+1]+ "," + opcodes[position+2] + "," + opcodes[position+3]);
                //Console.WriteLine("Operands at: " + opcodes[position+1] + "," + opcodes[position+2] + " are: "+  opcodes[opcodes[position+1]] + "," + opcodes[opcodes[position+2]] );
              

                if (actualOpcode == 1) //ADD 3 params
                {
                    //OLD WAY opcodes[opcodes[position+3]] = opcodes[opcodes[position+1]] + opcodes[opcodes[position+2]];
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : opcodes[position+2];
                    opcodes[opcodes[position+3]] = parameter1 + parameter2;
                    position += 4; //+4 because 1 op + 3 params
                }
                else if (actualOpcode == 2) //Multiply 3 params
                {
                    //opcodes[opcodes[position+3]] = opcodes[opcodes[position+1]] * opcodes[opcodes[position+2]];
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : opcodes[position+2];
                    opcodes[opcodes[position+3]] = parameter1 * parameter2;
                    position += 4;
                }
                else if (actualOpcode == 3) //only one parameter. grab input from user and store at parameter
                {
                    if (inputshandled == 0)
                    {
                        opcodes[opcodes[position+1]] = phase;
                        inputshandled++;
                    }
                    else if (inputshandled == 1)
                    {
                        opcodes[opcodes[position+1]] = inputsignal;
                        inputshandled++;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input request, only two allowed at position: " + position);
                    }
                    //Console.WriteLine("Storing value: " + Globals.input + " at position: " +  opcodes[position+1]);
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    position += 2;
                }
                else if (actualOpcode == 4) //print what's stored at parameter1
                {
                    //Console.WriteLine(opcodes[opcodes[position+1]]);
                    return opcodes[opcodes[position+1]];
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    //position += 2;
                }
                else if (actualOpcode == 5) //jump-if-true, 2 params
                {
                    //Console.WriteLine(opcodes[opcodes[position+1]]);
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    //if param 1 true go to param 2 else position += 2;
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : opcodes[position+2];
                    if (parameter1 > 0){position=parameter2;} else {position += 3;}
                }
                else if (actualOpcode == 6) //jump-if-false 2 params
                {
                    //Console.WriteLine(opcodes[opcodes[position+1]]);
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    // if param 1 fals go to param 2 else position += 2;
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : opcodes[position+2];
                    if (parameter1 == 0){position=parameter2;} else {position += 3;}
                }
                else if (actualOpcode == 7) //less than, 3 params
                {
                    //Console.WriteLine(opcodes[opcodes[position+1]]);
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    // if param 1less than param2 -> 1 else -> 0
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : opcodes[position+2];
                    opcodes[opcodes[position+3]] = parameter1 < parameter2 ? 1  : 0;
                    position += 4;
                }
                else if (actualOpcode == 8) //equals, 3 params
                {
                    //Console.WriteLine(opcodes[opcodes[position+1]]);
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    // if param1 equals param2 -> 1 else 0
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : opcodes[position+2];
                    opcodes[opcodes[position+3]] = parameter1 == parameter2 ? 1 : 0;
                    position += 4;
                }
                
                else if (actualOpcode == 99)
                {
                    Console.WriteLine("Code 99 encountered, stopping after seeing opcode 99 at: " + position);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid opcode detected: " + opcodes[position]);
                }

            }
            return opcodes[0];

        }
    }
}
