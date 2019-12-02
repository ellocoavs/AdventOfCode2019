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

            for (int position = 0; opcodes[position] != 99; position += 4)
            {
                if (opcodes[position] == 1)
                {

                }
                if (opcodes[position] == 2)
                {

                }
                if (opcodes[position] == 99)
                {

                }
                else
                {
                    Console.WriteLine("Invalid opcode detected: " + opcodes[position]);
                }
            }

        }
    }
}
