using System;
using System.IO;

namespace Day_5
{
    class Program
    {
        static class Globals
        {
            public static int input = 1;
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

            int result = 0 ;
            result = Compute(opcodes);   
        }
            
            
            
           
        
        static int Compute (int[] opcodes) 
        {
            for (int position = 0; ; position +=2)  //edit to do +1 and do an extra +3 if needed
            {
                int actualOpcode = opcodes[position] % 100; //last two digits
                bool isPosMode1 = (opcodes[position] / 100) % 10 == 0; // digit before last two
                bool isPosMode2 = (opcodes[position] / 1000) % 10 == 0; //digit before last three
                bool isPosMode3 = (opcodes[position] / 10000) % 10 == 0; // digit before last four

                Console.WriteLine("Opcode at position 0 is: " + actualOpcode);
                Console.WriteLine("IsPositionalMode values are " + isPosMode1+isPosMode2+isPosMode3);
                
                //Console.WriteLine("Processing opcodes: " + opcodes[position] + "," + opcodes[position+1]+ "," + opcodes[position+2] + "," + opcodes[position+3]);
                //Console.WriteLine("Operands at: " + opcodes[position+1] + "," + opcodes[position+2] + " are: "+  opcodes[opcodes[position+1]] + "," + opcodes[opcodes[position+2]] );
              

                if (actualOpcode == 1) //ADD
                {
                    //OLD WAY opcodes[opcodes[position+3]] = opcodes[opcodes[position+1]] + opcodes[opcodes[position+2]];
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : opcodes[position+2];
                    opcodes[opcodes[position+3]] = parameter1 + parameter2;
                    position += 2;
                }
                else if (actualOpcode == 2) //Multiply
                {
                    //opcodes[opcodes[position+3]] = opcodes[opcodes[position+1]] * opcodes[opcodes[position+2]];
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                    var parameter1 = isPosMode1 ? opcodes[opcodes[position+1]] : opcodes[position+1];
                    var parameter2 = isPosMode2 ? opcodes[opcodes[position+2]] : opcodes[position+2];
                    opcodes[opcodes[position+3]] = parameter1 * parameter2;
                    position += 2;
                }
                else if (actualOpcode == 3) //only one parameter, no +3 needed. grab input from user and store at parameter
                {
                    opcodes[opcodes[position+1]] = Globals.input;
                    Console.WriteLine("Storing value: " + Globals.input + " at position: " +  opcodes[position+1]);
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                }
                else if (actualOpcode == 4) //print what's stored at parameter
                {
                    Console.WriteLine(opcodes[opcodes[position+1]]);
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
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
