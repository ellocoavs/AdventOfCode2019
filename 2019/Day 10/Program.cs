using System;
using System.IO;
using System.Collections.Generic;

namespace Day_10
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            int width = input[0].Length;
            int height = input.Length;
            Console.WriteLine(width + "," + height);
            List<(int,int)> astroCoords = new List<(int, int)>(); //coordinates of all known astroids x,y
            int maxSeen = 0;
            for (int i=0; i<input.Length;i++)
            {
                for (int j=0;j<input[i].Length;j++)
                {
                    if (input[i][j]=='#')
                    {
                        astroCoords.Add((i,j));
                    }
                }
            }
            foreach ((int,int) coord in astroCoords)
            {
                int seen = 0;
                for (int i=0;i<width;i++)
                {
                    for (int j=0;j<height;j++)
                    {
                        if (coord != (i,j)) //if not itself
                        {
                            //if there isn't an astroid with the same ANGLE (atan2)! and shorter distance, +1 seen.

                        }
                    }
                }
                if (seen > maxSeen){maxSeen=seen;} //if bigger than max, save as new max
            }

            
        }
  
    }
}
