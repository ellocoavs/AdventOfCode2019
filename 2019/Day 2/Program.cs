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
            int[] parts= new int[1000] ;
            int counter = 0;
            foreach (string x in split)
            {
                parts[counter] =Int32.Parse(x);
                counter++;
            }
                    
        }
    }
}
