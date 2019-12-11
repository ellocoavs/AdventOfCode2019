using System;
using System.IO;
using System.Collections.Generic;


namespace Day_10
{
    class Program
    {
        static void Main(string[] args)
        {
            // var input = File.ReadAllLines("input.txt");
            // (int,int)station = (22,17);

            var input = File.ReadAllLines("testinput4.txt");
            (int,int)station = (11,13);
            
            
            int width = input[0].Length;
            int height = input.Length;
            
            //Console.WriteLine(width + "," + height);
            List<(int,int)> astroCoords = new List<(int, int)>(); //coordinates of all known astroids x,y
            List<(int,int,double)> seenFromStation = new List<(int,int,double)>(); //list of all astroids visible from station with angle from station 
            int maxSeen = 0;
            for (int i=0; i<input.Length;i++)
            {
                for (int j=0;j<input[i].Length;j++)
                {
                    if (input[i][j]=='#')
                    {
                        astroCoords.Add((j,i));
                    }
                }
            }

            foreach ((int,int) coord in astroCoords)
            {
                if(coord != station)
                {
                    double angle = Math.Atan2(station.Item2-coord.Item2,station.Item1-coord.Item1) ;
                    double distance = GetDistance(coord.Item1,coord.Item2, station.Item1,station.Item2);
                    bool sameanglecloser = FindSameAngleCloser( angle,  distance, station, astroCoords);
                    if (!sameanglecloser)
                    {
                        seenFromStation.Add((coord.Item1,coord.Item2,angle));
                    }
                }
            }
            seenFromStation.Sort((x, y) => (y.Item3).CompareTo(x.Item3));
            foreach ((int,int,double) seenAstro in seenFromStation)
            {
                Console.WriteLine(seenAstro);
            }
            Console.WriteLine(seenFromStation.Count);
            Console.WriteLine("");
            Console.WriteLine("Station at: " + station);
            Console.WriteLine("");

            Console.WriteLine("Checking two coords");
            (int,int) coordtest = (11,11);
            Console.WriteLine(coordtest);
            double angletest =  Math.Atan2(station.Item2-coordtest.Item2,station.Item1-coordtest.Item1);
            double distancetest = GetDistance(coordtest.Item1,coordtest.Item2, station.Item1,station.Item2);
            bool closertest = FindSameAngleCloser(angletest,distancetest,station,astroCoords);
            Console.WriteLine( closertest);
            Console.WriteLine( angletest);
            Console.WriteLine( distancetest);
            
            Console.WriteLine("");

            (int,int) coordtest2 = (11,12);
            Console.WriteLine(coordtest2);
            double angletest2 =  Math.Atan2(station.Item2-coordtest2.Item2,station.Item1-coordtest2.Item1);
            double distancetest2 = GetDistance(coordtest2.Item1,coordtest2.Item2, station.Item1,station.Item2);
            bool closertest2 = FindSameAngleCloser(angletest,distancetest2,station,astroCoords);
            Console.WriteLine( closertest2);
            Console.WriteLine( angletest2);
            Console.WriteLine( distancetest2);
            //rechtomhoog zou pi/2 moeten zijn.. niet 0...

            // foreach ((int,int) coord in astroCoords)
            // {
            //     int seen = 0;
            //     foreach ((int,int) othercoord in astroCoords)
            //     {
            //         if (coord != othercoord) //if not itself
            //         {
            //             //if there isn't an astroid with the same ANGLE (atan2)! and shorter distance, +1 seen.
            //             double angle = Math.Atan2(othercoord.Item1-coord.Item1,othercoord.Item2-coord.Item2);
            //             double distance = GetDistance(coord.Item1,coord.Item2, othercoord.Item1,othercoord.Item2);
            //             bool sameanglecloser = FindSameAngleCloser( angle,  distance, othercoord, astroCoords);
            //             if (!sameanglecloser)
            //             {
            //                 seen++;
            //             }
            //         }
            //     }
                
                    
            //     if (seen >= maxSeen)
            //     {
            //         maxSeen=seen;
            //         Console.WriteLine(coord.Item1+","+coord.Item2);
            //         Console.WriteLine(seen);
            //     } //if bigger than max, save as new max
            // }
            // Console.WriteLine(maxSeen);
            
        }
        private static double GetDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2)));
        }

        public static bool FindSameAngleCloser(double angle, double distance, (int,int) station,List<(int,int)> astroCoords)
        {
            bool found = false;
            foreach ((int,int)other in astroCoords)
            {
                double otherDist = GetDistance(station.Item1,station.Item2,other.Item1,other.Item2);
                double otherAngle = Math.Atan2(station.Item2-other.Item2,station.Item1-other.Item1);
                if (otherAngle == angle && otherDist < distance && station != other)
                {
                    found=true;
                }
            }
            return found;
        }
    }
}
