using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var text = File.ReadAllLines("input.txt");
            
            string[] wire1 = text[0].Split(",");
            string[] wire2 = text[1].Split(",");

            byte[,] grid = new byte[50000,50000];

            //start coordinates 500,500?
            int startx =25000;
            int starty = 25000;

            //TEST DATA
            // byte[,]grid = new byte[1000,1000];
            // int startx= 500;
            // int starty = 500;
            // string[] wire1 = "R75,D30,R83,U83,L12,D49,R71,U7,L72".Split(",");
            // string[] wire2 = "U62,R66,U55,R34,D71,R55,D58,R83".Split(",");
            
            
            
            //keep track of current position separately, reset after finishing a wire
            int currentx = startx;
            int currenty = starty;

            grid[startx,starty] = 2; //both wires start and therefore touch the starting position

            //process wire1
            Console.WriteLine("Processing Wire 1");
            foreach (string path in wire1)
            {
                Process(ref currentx,ref currenty,path,grid);
            }

            for (int col =0; col < grid.GetLength(0); col ++)
            {
                for (int row =0; row < grid.GetLength(1); row ++)
                {
                    if (grid[col,row] > 1)
                    {
                        //Console.WriteLine("Grid square value at: " + col + "," + row + " is: " + grid[col,row]);
                        grid[col,row] =1;
                        
                    }
                }
            }

            //process wire2
            //Reset to starting position
            currentx = startx;
            currenty = starty;
            Console.WriteLine("Processing Wire 2");
            foreach (string path in wire2)
            {
                Process(ref currentx, ref currenty,path,grid);
            }

            //Calculate and save distances into list
            List<int> distances = new List<int>();
            for (int col =0; col < grid.GetLength(0); col ++)
            {
                for (int row =0; row < grid.GetLength(1); row ++)
                {
                    if (grid[col,row] > 1)
                    {
                        //Console.WriteLine("Grid square value at: " + col + "," + row + " is: " + grid[col,row]);
                        int distance =CalculateManhattanDistance(col,row,startx,starty);
                        if (distance > 0){
                            Console.WriteLine("Distance: " + distance + " coordinates: " + col + "," + row);
                            distances.Add(distance);
                        }
                        
                    }
                }
            }

            int smallestDistance = distances.Min(c => c);
            Console.WriteLine("Smallest distance found is: " + smallestDistance);
            //distances.ForEach(Console.WriteLine);

        }
        static void Process(ref int currentx, ref int currenty, string path, byte[,] grid)
        {
            Console.WriteLine("Currently processing path: " + path + " starting from: " + currentx + "," + currenty);
            if (path[0] == 'U'){
                int delta = Int32.Parse(path.Substring(1));
                while (delta>0)
                {
                    Console.WriteLine("Adding 1 to coordinates: " + (currentx+1) + "," + currenty);
                    grid[(currentx+1),currenty]++;
                    delta--;
                    currentx++;
                }
            }
            else if (path[0] == 'D'){
                int delta = Int32.Parse(path.Substring(1));
                while (delta>0)
                {
                    Console.WriteLine("Adding 1 to coordinates: " + (currentx-1) + "," + currenty);
                    grid[(currentx-1),currenty]++;
                    delta--;
                    currentx--;
                }
            }
            else if (path[0] == 'L'){
                int delta = Int32.Parse(path.Substring(1));
                while (delta>0)
                {
                    Console.WriteLine("Adding 1 to coordinates: " + currentx + "," + (currenty-1));
                    grid[currentx,(currenty-1)]++;
                    delta--;
                    currenty--;
                }
            }
            else if (path[0] == 'R'){
                int delta = Int32.Parse(path.Substring(1));
                while (delta>0)
                {
                    Console.WriteLine("Adding 1 to coordinates: " + currentx + "," + (currenty+1));
                    grid[currentx,(currenty+1)]++;
                    delta--;
                    currenty++;
                }
            }
            else {Console.WriteLine("Invalid direction detected: " + path + " direction letter was: " + path[0]);}
        }
        static int CalculateManhattanDistance(int startx, int starty, int currentx, int currenty)
        {
            int distance =0;
            distance = Math.Abs(startx - currentx) + Math.Abs(starty-currenty);
            return distance;
        }
    }
}
