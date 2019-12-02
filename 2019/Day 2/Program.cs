using System;
using System.IO;

namespace Day_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = File.ReadAllText("input.txt");
            string[] split = text.Split(",");
            int[] opcodes= new int[1000] ;
            int counter = 0;
            foreach (string x in split)
            {
                opcodes[counter] =Int32.Parse(x);
                counter++;
            }
            //int[] opcodes = {1,1,1,4,99,5,6,0,99}; //TESTING INPUTS
            for (int position = 0; ; position += 4)  // removed middle condition opcodes[position] != 99 cause not needed
            {
                Console.WriteLine("Opcode at position 0 is: " + opcodes[0]);
                Console.WriteLine("Processing opcodes: " + opcodes[position] + "," + opcodes[position+1]+ "," + opcodes[position+2] + "," + opcodes[position+3]);
                Console.WriteLine("Operands at: " + opcodes[position+1] + "," + opcodes[position+2] + " are: "+  opcodes[opcodes[position+1]] + "," + opcodes[opcodes[position+2]] );
                
                if (opcodes[position] == 1)
                {
                    opcodes[opcodes[position+3]] = opcodes[opcodes[position+1]] + opcodes[opcodes[position+2]];
                    Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                }
                else if (opcodes[position] == 2)
                {
                    opcodes[opcodes[position+3]] = opcodes[opcodes[position+1]] * opcodes[opcodes[position+2]];
                    Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                }
                else if (opcodes[position] == 99)
                {
                    Console.WriteLine("Code 99 encountered, stopping after seeing opcode 99 at: " + position);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid opcode detected: " + opcodes[position]);
                }
            }
            Console.WriteLine("Ended with value: " + opcodes[0] + " at position 0");
        }
    }
}
