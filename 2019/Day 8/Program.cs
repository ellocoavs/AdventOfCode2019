using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day_8
{
    class Program
    {
        static class Globals
        {
            public static int width = 25;
            public static int height = 6;
        }
        static void Main(string[] args)
        {
            string input = File.ReadAllText("input.txt");
            int width = Globals.width;
            int height = Globals.height;

            List<string> layers = new List<string>(); //split input into a list of layers for further analysis.
            for (int i=0;i<input.Length;i+=width*height)
            {
                layers.Add(input.Substring(i,(width*height)));
            }

            int index = FindIndexOfLayerWithLeastZeroes(layers);

            string layer = layers[index];
            int answer = CalculateString(layer);

            Console.WriteLine("Answer is: " +answer);

        }
        public static int FindIndexOfLayerWithLeastZeroes (List<string> layers)
        {
            int result = 0;
            int leastamountofzeroes=Globals.width*Globals.height;
            for (int i=0;i<layers.Count;i++)
            {
                int count = layers[i].Count(f => f == '0');
                if (count < leastamountofzeroes)
                {
                    leastamountofzeroes = count;
                    //Console.WriteLine(layers[i]);
                    Console.WriteLine("Amount of zeroes found: " + count);
                    result = i;
                }
            }
            Console.WriteLine("Least amount of zeroes was: "+result);
            return result;
        }
        public static int CalculateString (string layer)
        {
            int result = 0;
            int countOnes = layer.Count(f => f =='1');
            int countTwos = layer.Count(f => f =='2');
            Console.WriteLine("ones found: " + countOnes + " twos found: " + countTwos);
            result = countOnes * countTwos;
            return result;
        }
    }
}
