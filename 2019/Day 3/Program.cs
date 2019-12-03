using System;
using System.IO;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var text = File.ReadAllLines("input.txt");
            
            string[] wire1 = text[0].Split(",");
            string[] wire2 = text[1].Split(",");

            int[,] grid = new int[1000,1000];

            //start coordinates 500,500?
            int startx =500;
            int starty = 500;

            //keep track of current position separately, reset after finishing a wire
            int currentx = startx;
            int currenty = starty;

            //process wire1
            foreach (string path in wire1)
            {
                Process(currentx,currenty,path,grid);
            }

            //process wire2
            //Reset to starting position
            currentx = startx;
            currenty = starty;

        }
        static void Process(int currentx, int currenty, string path, int[,] grid)
        {
            if (path[0] == 'U'){

            }
            if (path[0] == 'D'){
                
            }
            if (path[0] == 'L'){
                
            }
            if (path[0] == 'R'){
                
            }
            else {Console.WriteLine("Invalid direction detected: " + path);}
        }
    }
}
