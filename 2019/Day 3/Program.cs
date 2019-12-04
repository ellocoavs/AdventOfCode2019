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

            int gridsize = 30000;
            byte[,] grid = new byte[gridsize,gridsize];

            //start coordinates 500,500?
            int startx = gridsize/3;
            int starty = gridsize/3;

            int[,] steps1 = new int[gridsize,gridsize];
            int[,] steps2 = new int[gridsize,gridsize];
            
            // //TEST DATA
            // string[] wire1 = "R75,D30,R83,U83,L12,D49,R71,U7,L72".Split(",");
            // string[] wire2 = "U62,R66,U55,R34,D71,R55,D58,R83".Split(",");
            
            
            
            //keep track of current position separately, reset after finishing a wire
            int currentx = startx;
            int currenty = starty;

            grid[startx,starty] = (2); //both wires start and therefore touch the starting position
            int stepping = 1;
            //process wire1
            Console.WriteLine("Processing Wire 1");
            foreach (string path in wire1)
            {
                Process(ref currentx,ref currenty,path,grid, steps1, ref stepping);
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
            stepping = 1;
            Console.WriteLine("Processing Wire 2");
            foreach (string path in wire2)
            {
                Process(ref currentx, ref currenty,path,grid, steps2, ref stepping);
            }

            //Calculate and save distances into list
            List<int> distances = new List<int>();
            List<int> totalsteps = new List<int>();

            for (int col =0; col < grid.GetLength(0) -1; col ++)
            {
                for (int row =0; row < grid.GetLength(1) -1; row ++)
                {
                    if (grid[col,row] == 2)
                    {
                        //Console.WriteLine("Grid square value at: " + col + "," + row + " is: " + grid[col,row]);
                        int distance =CalculateManhattanDistance(col,row,startx,starty);
                        int steps = steps1[col,row]+steps2[col,row];
                        if (distance > 0){
                            Console.WriteLine("Distance: " + distance + " coordinates: " + col + "," + row);
                            Console.WriteLine("Steps: " + steps + " coordinates: " + col + "," + row);
                            
                            distances.Add(distance);
                            totalsteps.Add(steps);
                        }
                        
                    }
                }
            }

            int smallestDistance = distances.Min(c => c);
            Console.WriteLine("Smallest distance found is: " + smallestDistance);

            int leaststeps = totalsteps.Min(c => c);
            Console.WriteLine("Smallest number of steps found is: " + leaststeps);
            // Console.WriteLine(steps1[startx,starty]);
            // Console.WriteLine(steps1[startx+1,starty]);
            // Console.WriteLine(steps1[startx,starty+1]);
            // Console.WriteLine(steps1[startx-1,starty]);
            // Console.WriteLine(steps1[startx,starty-1]);
            // Console.WriteLine(steps2[startx,starty]);
            // Console.WriteLine(steps2[startx+1,starty]);
            // Console.WriteLine(steps2[startx,starty+1]);
            // Console.WriteLine(steps2[startx-1,starty]);
            // Console.WriteLine(steps2[startx,starty-1]);
            
            totalsteps.ForEach(Console.WriteLine);
            //distances.ForEach(Console.WriteLine);

        }
        static void Process(ref int currentx, ref int currenty, string path, byte[,] grid, int[,] steps, ref int stepping)
        {
            Console.WriteLine("Currently processing path: " + path + " starting from: " + currentx + "," + currenty);
            if (path[0] == 'U'){
                int delta = Int32.Parse(path.Substring(1));
                while (delta>0)
                {
                    //Console.WriteLine("Adding 1 to coordinates: " + (currentx+1) + "," + currenty);
                    grid[(currentx+1),currenty]++;
                    delta--;
                    
                    if (steps[currentx+1,currenty] == 0)
                    {
                        steps[currentx+1,currenty] = stepping;
                        stepping ++;
                    }

                    currentx++;
                }
            }
            else if (path[0] == 'D'){
                int delta = Int32.Parse(path.Substring(1));
                while (delta>0)
                {
                    //Console.WriteLine("Adding 1 to coordinates: " + (currentx-1) + "," + currenty);
                    grid[(currentx-1),currenty]++;
                    delta--;
                    
                    if (steps[currentx-1,currenty] == 0)
                    {
                        steps[currentx-1,currenty] =stepping;
                        stepping++;
                    }
                    
                    currentx--;
                }
            }
            else if (path[0] == 'L'){
                int delta = Int32.Parse(path.Substring(1));
                while (delta>0)
                {
                    //Console.WriteLine("Adding 1 to coordinates: " + currentx + "," + (currenty-1));
                    grid[currentx,(currenty-1)]++;
                    delta--;
                    
                    if (steps[currentx,currenty-1] == 0)
                    {
                        steps[currentx,currenty-1] = stepping;
                        stepping++;
                    }
                    
                    currenty--;
                }
            }
            else if (path[0] == 'R'){
                int delta = Int32.Parse(path.Substring(1));
                while (delta>0)
                {
                    //Console.WriteLine("Adding 1 to coordinates: " + currentx + "," + (currenty+1));
                    grid[currentx,(currenty+1)]++;
                    delta--;
                    
                    if (steps[currentx,currenty+1] == 0)
                    {
                        steps[currentx,currenty+1] = stepping;
                        stepping++;
                    }
                    
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
