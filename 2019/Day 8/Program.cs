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
            // public static int width = 2;
            // public static int height = 2;
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

            //int index = FindIndexOfLayerWithLeastZeroes(layers);

            //string layer = layers[index];
            //int answer = CalculateString(layer);

            //Console.WriteLine("Answer is: " +answer);
            int[,] image = new int[width,height];
            for (int i =0; i<Globals.width;i++)
            {
                for (int j =0; j<Globals.height;j++)
                {
                    image[i,j] = 2;
                }
            }
            PrintImage(image);
            //fill in values with FindFirstNonTransparent
            for (int i =0; i<Globals.width;i++)
            {
                for (int j =0; j<Globals.height;j++)
                {
                    if (image[i,j] ==2)
                    {
                        Console.WriteLine("Processing coords" + i +","+j);
                        image[i,j] = FindFirstNonTransparent(layers,i,j);
                    }
                    
                }
            }
            //print
            PrintImage(image);
        }
        public static int FindFirstNonTransparent (List<string> layers,int x, int y){
            
            bool nontransparentFound = false;
            int size = Globals.width*Globals.height;
            int numberoflayers = layers.Count;
            while (!nontransparentFound)
            {
                for(int i=0;i<numberoflayers;i++)
                {
                    string currentlayer =layers[i];
                    int index = x + (y * Globals.width); //converting coordinates to indexposition
                    //Console.WriteLine("Processing layer number" + i + " which looks like:" + currentlayer);
                    //onsole.WriteLine("Processed character should be: " + currentlayer[index]);
                    if(currentlayer[index] == '1' | currentlayer[index] == '0')
                    {
                        
                        char result = currentlayer[index];
                        int actualresult = (int)char.GetNumericValue(result);
                        //Console.WriteLine("Returning colorvalue: " + actualresult);
                        return actualresult;
                    }
                    
                }
            }
            Console.WriteLine("Trying to return invalid color");
            return 2;
        }
        public static void PrintImage (int[,] image)
        {
           // Console.BackgroundColor = ConsoleColor.Blue;
            for (int j =0; j<Globals.height;j++)
            {
                for (int i =0; i<Globals.width;i++) //inner iteration = width so that we do entire horizontal lines first.
                {
                    if (image[i,j] == 0){
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    if (image[i,j] == 1){
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write("{0} ", image[i, j]);
                }
                Console.Write(Environment.NewLine);
            }       
            Console.ResetColor();
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
