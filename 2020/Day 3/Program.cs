using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day_3
{
    class Program
    {
          static class Globals
        {
            public static int width; // 1 is test mode, 2 is boost mode
            public static int depth;
            
        }
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            int xpos = 0;
            int ypos = 0;
            Globals.width = input[0].Length;   //31 characters per line -> ergo mod 31?
            Globals.depth = input.Length;
            int treecount = 0;
            
            // rule def
            int xstepsize =3;
            int ystepsize =1;
            
            Traverse(1,1,input);
            Traverse(3,1,input);
            Traverse(5,1,input);
            Traverse(7,1,input);
            Traverse(1,2,input);
/*
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
            }*/
            //Console.WriteLine("Number of crossed trees is: " + treecount);
        }


        static void Traverse (int xrule,int yrule, string[] input)
        {
            int xpos = 0;
            int ypos = 0;
            int treecount = 0;
            while (ypos < Globals.depth)
            {
                //do stuff
                //Console.WriteLine("Checking positions: " + xpos + " " + ypos);
                if (input[ypos][xpos] == '#')
                {
                    treecount++; //tree found
                }
                
                xpos = (xpos+xrule) % Globals.width;
                ypos = ypos+yrule;
            }
            Console.WriteLine("Number of crossed trees is: " + treecount);
        }
    }
}
