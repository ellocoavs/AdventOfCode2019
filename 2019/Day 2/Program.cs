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
            int[] original = new int[100000];
            int[] opcodes= new int[100000];
            int counter = 0;
            
            
            foreach (string x in split)
            {
                original[counter] =Int32.Parse(x);
                counter++;
            }
            Array.Copy(original,opcodes,100000);

            int noun = 0;
            int verb = 0;
            int result = 0 ;
            while (noun < 100){
                while (verb < 100){
                    Console.WriteLine("Computing with verb, noun as: "+noun+","+verb);
                    result = Compute(opcodes,noun,verb);
                    if (result == 19690720)
                    {
                        Console.WriteLine("Input was " + (noun*100+verb));
                        break;
                    }
                    else Array.Copy(original,opcodes,100000); verb++; //restart with original array and try again
                }
                if (result == 19690720){
                    Console.WriteLine("Input was " + (noun*100+verb));
                    break;
                }
                
                else verb =0;Array.Copy(original,opcodes,100000); noun++;//restart with original array and try again
            }
            
            
            
           
            
        }
        static int Compute (int[] opcodes, int noun, int verb) 
        {
            opcodes[1]= noun;
            opcodes[2]= verb;
            for (int position = 0; ; position += 4)  
            {
                //Console.WriteLine("Opcode at position 0 is: " + opcodes[0]);
                //Console.WriteLine("Processing opcodes: " + opcodes[position] + "," + opcodes[position+1]+ "," + opcodes[position+2] + "," + opcodes[position+3]);
                //Console.WriteLine("Operands at: " + opcodes[position+1] + "," + opcodes[position+2] + " are: "+  opcodes[opcodes[position+1]] + "," + opcodes[opcodes[position+2]] );
                
                if (opcodes[position] == 1)
                {
                    opcodes[opcodes[position+3]] = opcodes[opcodes[position+1]] + opcodes[opcodes[position+2]];
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
                }
                else if (opcodes[position] == 2)
                {
                    opcodes[opcodes[position+3]] = opcodes[opcodes[position+1]] * opcodes[opcodes[position+2]];
                    //Console.WriteLine("Result stored at position: " + position + " is: " + opcodes[position+3]);
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
            return opcodes[0];
        }
    }
}
