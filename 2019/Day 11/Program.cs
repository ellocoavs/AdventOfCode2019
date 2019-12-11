using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Day_1
{
    class Program
    {
        public static class Globals
        {
            public static Int64 input = 2; // 1 is test mode, 2 is boost mode
            public static Int64 relativeBase = 0;

            public static int[,] panels = new int[1000,1000];
            public enum direction
            {
                up,
                down,
                left,
                right
            }
            public static (int,int) currentposition = (500,500);
            public static List<int> outputs;
        }
        static void Main(string[] args)
        {
            var text = File.ReadAllText("input.txt");
            string[] split = text.Split(",");
            Int64[] opcodes= new Int64[50000000];
            Int64 counter = 0;
            
            foreach (string x in split)
            {
                opcodes[counter] =Int64.Parse(x);
                counter++;
            }
            //Array.Copy(original,opcodes,1000000);

                        
            Task task0 = new Task( () => Compute(opcodes));
            task0.Start();


        }
        
        static int RobotMoves()
        {
            int step = 0;
            while (true)
            {
                int NumberOfInstructions = Globals.outputs.Capacity;
                if (NumberOfInstructions > 0)
                {
                    // If instruction = 99 STOP WORKING


                    //process input
                    if (step == 0)
                    {
                        //PAINT
                        step++;
                    }
                    else if (step == 1)
                    {
                        // TURN AND MOVE
                        step =0;
                    }
                    
                }
                else 
                {   
                    //WAIT FOR INPUT
                    Thread.Sleep(5);
                }
            }
        }
        static int GetInput()
        {
            int input = Globals.panels[Globals.currentposition.Item1,Globals.currentposition.Item2];
            return input;
        }
            
        static void DoOutput(long parameter) 
        {
            if (parameter <= 1 | parameter >= 0  | parameter == 99)
            {
                Globals.outputs.Add((int)parameter);
            }
            else
            {
                Console.WriteLine("Invalid output attempted: " + parameter);
            }
            
        }
        
        static Int64 Compute (Int64[] opcodes) 
        {
            for (Int64 position = 0; ; )
            {
                Int64 actualOpcode = opcodes[position] % 100; //last two digits
                bool isPosMode1 = (opcodes[position] / 100) % 10 == 0; // digit before last two
                bool isPosMode2 = (opcodes[position] / 1000) % 10 == 0; //digit before last three
                bool isPosMode3 = (opcodes[position] / 10000) % 10 == 0; // digit before last four
                bool isRelMode1 = (opcodes[position] / 100) % 10 == 2; // digit before last two
                bool isRelMode2 = (opcodes[position] / 1000) % 10 == 2; //digit before last three
                bool isRelMode3 = (opcodes[position] / 10000) % 10 == 2; // digit before last four
                //Console.WriteLine("Opcode at position 0 is: " + actualOpcode);
                //Console.WriteLine("IsPositionalMode values are " + isPosMode1+isPosMode2+isPosMode3);
                
                //Console.WriteLine("Processing opcodes: " + opcodes[position] + "," + opcodes[position+1]+ "," + opcodes[position+2] + "," + opcodes[position+3]);
                //Console.WriteLine("Operands at: " + opcodes[position+1] + "," + opcodes[position+2] + " are: "+  opcodes[opcodes[position+1]] + "," + opcodes[opcodes[position+2]] );
              

                if (actualOpcode == 1) //ADD 3 params
                {
                    //OLD WAY opcodes[opcodes[position+3]] = opcodes[opcodes[position+1]] + opcodes[opcodes[position+2]];
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : isRelMode2 ? opcodes[opcodes[position+2]+Globals.relativeBase] : opcodes[position+2];
                    //opcodes[opcodes[position+3]] = parameter1 + parameter2;
                    if (isPosMode3)
                    {
                        opcodes[opcodes[position+3]] = parameter1 + parameter2;
                    }
                    if (isRelMode3)
                    {
                        opcodes[opcodes[position+3]+Globals.relativeBase] = parameter1 + parameter2;
                    }
                    else //direct mode
                    {
                        opcodes[position+3] = parameter1 + parameter2;
                    }
                    position += 4; //+4 because 1 op + 3 params
                }
                else if (actualOpcode == 2) //Multiply 3 params
                {
                    //opcodes[opcodes[position+3]] = opcodes[opcodes[position+1]] * opcodes[opcodes[position+2]];
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : isRelMode2 ? opcodes[opcodes[position+2]+Globals.relativeBase] : opcodes[position+2];
                    //opcodes[opcodes[position+3]] = parameter1 * parameter2;
                    if (isPosMode3)
                    {
                        opcodes[opcodes[position+3]] = parameter1 * parameter2;
                    }
                    else if (isRelMode3)
                    {
                        opcodes[opcodes[position+3]+Globals.relativeBase] = parameter1 * parameter2;
                    }
                    else //direct mode
                    {
                        opcodes[position+3] = parameter1 * parameter2;
                    }
                    position += 4;
                }
                else if (actualOpcode == 3) //only one parameter. grab input from user and store at parameter
                {
                    //CHANGE TO GRAB INPUT FROM camera, make method GetInput (save GetInput result in local var then use.)
                    Thread.Sleep(5);
                    int input =  GetInput();
                    if (isPosMode1)
                    {
                        opcodes[opcodes[position+1]] = input;
                    }
                    else if (isRelMode1)
                    {
                        opcodes[opcodes[position+1]+Globals.relativeBase] = input;
                    }
                    else //direct mode
                    {
                        opcodes[position+1] = input;
                    }
                    //opcodes[opcodes[position+1]] = Globals.input;
                    //Console.WriteLine("Storing value: " + Globals.input + " at position: " +  opcodes[position+1]);
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    position += 2;
                }
                else if (actualOpcode == 4) //print what's stored at parameter1
                {
                    //output to queue and PAUSE for a bit. use Thread.Sleep(5); outputprocessor runs parallel.
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    Console.WriteLine(parameter1);
                    Console.WriteLine("Result stored at position: " + position + " is: " + parameter1);
                    DoOutput(parameter1);
                    Thread.Sleep(5);
                    position += 2;
                }
                else if (actualOpcode == 5) //jump-if-true, 2 params
                {
                    //Console.WriteLine(opcodes[opcodes[position+1]]);
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    //if param 1 true go to param 2 else position += 2;
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : isRelMode2 ? opcodes[opcodes[position+2]+Globals.relativeBase] : opcodes[position+2];
                    if (parameter1 > 0){position=parameter2;} else {position += 3;}
                }
                else if (actualOpcode == 6) //jump-if-false 2 params
                {
                    //Console.WriteLine(opcodes[opcodes[position+1]]);
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    // if param 1 fals go to param 2 else position += 2;
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : isRelMode2 ? opcodes[opcodes[position+2]+Globals.relativeBase] : opcodes[position+2];
                    if (parameter1 == 0){position=parameter2;} else {position += 3;}
                }
                else if (actualOpcode == 7) //less than, 3 params
                {
                    //Console.WriteLine(opcodes[opcodes[position+1]]);
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    // if param 1less than param2 -> 1 else -> 0
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : isRelMode2 ? opcodes[opcodes[position+2]+Globals.relativeBase] : opcodes[position+2];
                    //opcodes[opcodes[position+3]] = parameter1 < parameter2 ? 1  : 0;
                    if (isPosMode3)
                    {
                        opcodes[opcodes[position+3]] = parameter1 < parameter2 ? 1  : 0;
                    }
                    else if (isRelMode3)
                    {
                        opcodes[opcodes[position+3]+Globals.relativeBase] = parameter1 < parameter2 ? 1  : 0;
                    }
                    else //direct mode
                    {
                        opcodes[position+3] = parameter1 < parameter2 ? 1  : 0;
                    }
                    position += 4;
                }
                else if (actualOpcode == 8) //equals, 3 params
                {
                    //Console.WriteLine(opcodes[opcodes[position+1]]);
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    // if param1 equals param2 -> 1 else 0
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : isRelMode2 ? opcodes[opcodes[position+2]+Globals.relativeBase] : opcodes[position+2];
                    //opcodes[opcodes[position+3]] = parameter1 == parameter2 ? 1 : 0;

                    if (isPosMode3)
                    {
                        opcodes[opcodes[position+3]] = parameter1 == parameter2 ? 1  : 0;
                    }
                    else if (isRelMode3)
                    {
                        opcodes[opcodes[position+3]+Globals.relativeBase] = parameter1 == parameter2 ? 1  : 0;
                    }
                    else //direct mode
                    {
                        opcodes[position+3] = parameter1 == parameter2 ? 1  : 0;
                    }
                    position += 4;
                }
                else if (actualOpcode == 9) //adjust relative base, 1 params
                {
                    //Console.WriteLine(opcodes[opcodes[position+1]]);
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    // if param1 equals param2 -> 1 else 0
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : isRelMode1 ? opcodes[opcodes[position+1]+Globals.relativeBase] : opcodes[position+1];
                    Globals.relativeBase += parameter1;
                    position += 2;
                }
                else if (actualOpcode == 99)
                {
                    Console.WriteLine("Code 99 encountered, stopping after seeing opcode 99 at: " + position);
                    DoOutput(99);
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
