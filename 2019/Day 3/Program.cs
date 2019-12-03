using System;
using System.IO;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var text = File.ReadAllLines("input.txt");
            
            string[] split = text[0].Split(",");
            int[] wire1= new int[1000] ;
            int counter = 0;
            foreach (string x in split)
            {
                wire1[counter] =Int32.Parse(x);
                counter++;
            }

            split = text[1].Split(",");
            int[] wire2= new int[1000] ;
            counter = 0;
            foreach (string x in split)
            {
                wire2[counter] =Int32.Parse(x);
                counter++;
            }

            int[,] grid = new int[1000,1000];
            //start coordinates 500,500?
            

        }
    }
}
