using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Day_7
{
    class Program
    {
        static class Globals
        {
            public static List<string> phasepermutations = new List<string>();
            public static Tuple<int,bool>[] outputs = new Tuple<int,bool>[5];
            public static int result =0;
            public static int input = 0;
        }
        static void Main(string[] args)
        {
            //var text = File.ReadAllText("testinput2.txt");
            var text = File.ReadAllText("input.txt");
            string[] split = text.Split(",");
            int[] original = new int[100000];
            int[] opcodes0= new int[100000];
            int[] opcodes1= new int[100000];
            int[] opcodes2= new int[100000];
            int[] opcodes3= new int[100000];
            int[] opcodes4= new int[100000];
            int counter = 0;
            
            foreach (string x in split)
            {
                original[counter] =Int32.Parse(x);
                counter++;
            }
        

            string phases = "98765"; //day 7 part 2 phase options
            char[] arr = phases.ToCharArray();
            GetPer(arr); //put all permutation in that global list

            
            List<int> finalsignals = new List<int>(); //all final signals from the 5 compute rounds stored here, find the max value here

            foreach (string perm in Globals.phasepermutations){
                for (int i = 0; i<Globals.outputs.Length; i++) {
                Globals.outputs[i]= new Tuple<int,bool>(0,false);
                }
                Array.Copy(original,opcodes0,100000);
                Array.Copy(original,opcodes1,100000);
                Array.Copy(original,opcodes2,100000);
                Array.Copy(original,opcodes3,100000);
                Array.Copy(original,opcodes4,100000);
                char[] currentperm = perm.ToCharArray(); 
                //char[] currentperm = phases.ToCharArray(); to test specific permutation
                int[] intperm = Array.ConvertAll(currentperm, c => (int)Char.GetNumericValue(c));
                //Console.WriteLine("Starting iteration with phasepermutation: " + currentperm.ToString());
                Task task0 = new Task( () => ComputeAsync(opcodes0,intperm[0],0));
                task0.Start();
                Task task1 = new Task( () => ComputeAsync(opcodes1,intperm[1],1));
                task1.Start();
                Task task2 = new Task( () => ComputeAsync(opcodes2,intperm[2],2));
                task2.Start();
                Task task3 = new Task( () => ComputeAsync(opcodes3,intperm[3],3));
                task3.Start();
                Task task4 = new Task( () => ComputeAsync(opcodes4,intperm[4],4));
                task4.Start();


                Task.WaitAll(task0,task1,task2,task3,task4);

                Globals.result= Globals.outputs[0].Item1;
                Console.WriteLine("Saving thrust signal result to finals: " + Globals.result);
                finalsignals.Add(Globals.result);
            }
            Task.WaitAll();
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
    
            
           
        
        static async void ComputeAsync (int[] opcodes, int phase, int amplifier) 
        {
            int inputshandled;
            inputshandled = 0;
            int position;
            position =0;
            for (position = 0; ; )
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
                    //change to grab input from globals, and to wait if the input if not there (0,false)
                    //note: first grab the phaseinput, then always wait for the input from previous amp.
                    if (inputshandled == 0)  //phase setting, leave as is.
                    {
                        //Console.WriteLine("Processing phase input for amp " + amplifier);
                        opcodes[opcodes[position+1]] = phase;
                        inputshandled++;
                    }
                    else if (inputshandled == 1 && amplifier==0 ){
                        //Console.WriteLine("Processing input 0 for amp " + amplifier);
                        opcodes[opcodes[position+1]] = Globals.input;
                        inputshandled++;
                    }
                    
                    else  //in this case grab from globals
                    {
                        while (Globals.outputs[amplifier].Item2 == false)
                        {
                            //Console.WriteLine("Waiting for input in amp: " + amplifier);
                            //Console.WriteLine((Globals.outputs[amplifier].Item1).ToString() + (Globals.outputs[amplifier].Item2));
                            //await Task.Delay(100);
                            Thread.Sleep(5);
                        }
                        //Console.WriteLine("Processing input in amp: " + amplifier);
                        opcodes[opcodes[position+1]] = Globals.outputs[amplifier].Item1;
                        inputshandled++;
                        Globals.outputs[amplifier]= new Tuple<int,bool>(0,false);
                    }

                    //Console.WriteLine("Storing value: " + Globals.input + " at position: " +  opcodes[position+1]);
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    position += 2;
                }
                else if (actualOpcode == 4) //store in globals value,true for next amp
                {
                    //output = input for amp+1 unless amp 4 -> then 0
                    Globals.outputs[(amplifier+1)%5]= new Tuple<int,bool>(opcodes[opcodes[position+1]],true);
                   // if (amplifier==4){
                     //   Console.WriteLine("Value outputted: " + opcodes[opcodes[position+1]]);
                    //}
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    position += 2;
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
                    //Console.WriteLine("Code 99 encountered, stopping after seeing opcode 99 at: " + position);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid opcode detected: " + opcodes[position]);
                    break;
                }

            }
            

        }
    }
}
