using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            int xpos = 0;
            int ypos = 0;
            int width = input[0].Length;   //31 characters per line -> ergo mod 31?
            int depth = input.Length;
            int treecount = 0;
            
            // rule def
            int xstepsize =3;
            int ystepsize =1;

            while (ypos < depth)
            {
                //do stuff
                Console.WriteLine("Checking positions: " + xpos + " " + ypos);
                if (input[ypos][xpos] == '#')
                {
                    treecount++; //tree found
                }
                
                xpos = (xpos+xstepsize) % width;
                ypos = ypos+ystepsize;
            }

            Console.WriteLine("Number of crossed trees is: " + treecount);
        }
    }
}
