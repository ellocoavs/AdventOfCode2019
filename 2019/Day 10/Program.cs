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
            //Console.WriteLine(width + "," + height);
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
                foreach ((int,int) othercoord in astroCoords)
                {
                    if (coord != othercoord) //if not itself
                    {
                        //if there isn't an astroid with the same ANGLE (atan2)! and shorter distance, +1 seen.
                        double angle = Math.Atan2(coord.Item1-othercoord.Item1,coord.Item2-othercoord.Item2);
                        double distance = GetDistance(coord.Item1,coord.Item2, othercoord.Item1,othercoord.Item2);
                        bool sameanglecloser = FindSameAngleCloser( angle,  distance, coord, astroCoords);
                        if (!sameanglecloser)
                        {
                            seen++;
                        }
                    }
                }
                
                    
                if (seen > maxSeen)
                {
                    maxSeen=seen;
                    Console.WriteLine(coord.Item1+","+coord.Item2);
                } //if bigger than max, save as new max
            }
            Console.WriteLine(maxSeen);
            
        }
        private static double GetDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        public static bool FindSameAngleCloser(double angle, double distance, (int,int) coord,List<(int,int)> astroCoords)
        {
            bool found = false;
            foreach ((int,int)other in astroCoords)
            {
                double otherDist = GetDistance(coord.Item1,coord.Item2,other.Item1,other.Item2);
                double otherAngle = Math.Atan2(coord.Item1-other.Item1,coord.Item2-other.Item2);
                if (otherAngle == angle && otherDist < distance && coord != other)
                {
                    found=true;
                }
            }
            return found;
        }
    }
}
