using System;
using System.Collections.Generic;
using System.Linq;

namespace Day_12
{
    class Program
    {
        public static class Globals
        {
            //x,y,z coords
            public static List<(int,int,int)> planets = new List<(int, int, int)>();
            public static List<(int,int,int)> initialplanets;
            public static List<(int,int,int)> velocities = new List<(int, int, int)>();
        }
        static void Main(string[] args)
        {
            long NumberOfSteps = 4686774924;
            InitPlanets();

            Console.WriteLine("Position and speed at start");
            PrintCurrentPositionAndSpeed(0);
            //do a loop where you repeat x number of cycles and call DoCycle();
            for (long i=0; i< NumberOfSteps; i++)
            {
                //Console.WriteLine("Cycle number: "+i);
                
                DoCycle();
                PrintCurrentPositionAndSpeed(i);
                //Console.WriteLine("");
            }

            //then after loop print final positions and speeds
            Console.WriteLine("Final positions and speeds after "+NumberOfSteps+" steps");
            PrintCurrentPositionAndSpeed(NumberOfSteps);
          
            //and do the final calculation of the situation
            int TotalEnergy = CalculateEnergy();
            Console.WriteLine("Final energy level is: "+ TotalEnergy);
        }

        static int CalculateEnergy()
        {
            List<int> energies = new List<int>(); //creating list of energies for each planet
            for (int i=0; i<Globals.planets.Count;i++)
            {
                int pot = Math.Abs(Globals.planets[i].Item1) + Math.Abs(Globals.planets[i].Item2) + Math.Abs(Globals.planets[i].Item3);
                int kin = Math.Abs(Globals.velocities[i].Item1) + Math.Abs(Globals.velocities[i].Item2) + Math.Abs(Globals.velocities[i].Item3);
                energies.Add(pot*kin); //total moon energy = potential energy * kinetic energy
            }

            int sum = energies.Take(Globals.planets.Count).Sum(); // Now sum all total moon energies for total system energy
            return sum;
        }
        static void PrintCurrentPositionAndSpeed(long cycle)
        {
            if ( Globals.planets[0] == Globals.initialplanets[0] &&
                Globals.planets[1] == Globals.initialplanets[1] &&
                Globals.planets[2] == Globals.initialplanets[2] &&
                Globals.planets[3] == Globals.initialplanets[3])  
            {
                    Console.WriteLine("During cycle " + cycle + " there was a return to starting position");
                    for (int i=0; i<Globals.planets.Count;i++)
                        {
                            Console.WriteLine("Position of planet " + i + " is: " + Globals.planets[i] + " and velocities are" + Globals.velocities[i]);
                        }                            
            }

            if ( Globals.velocities[0] == (0,0,0) &&
                Globals.velocities[1] == (0,0,0) &&
                Globals.velocities[2] == (0,0,0) &&
                Globals.velocities[3] == (0,0,0))  
            {
                //standstill (cycle +1 )x2
                    Console.WriteLine("During cycle " + cycle + " there was a return to return to standstill");
                    for (int i=0; i<Globals.planets.Count;i++)
                        {
                            Console.WriteLine("Position of planet " + i + " is: " + Globals.planets[i] + " and velocities are" + Globals.velocities[i]);
                        }                            
            }
        }
        static void InitPlanets() //the puzzle input
        {
            //actuall input
            // Globals.planets.Add((-10,-13,7));
            // Globals.planets.Add((1,2,1));
            // Globals.planets.Add((-15, -3, 13));
            // Globals.planets.Add((3,7,-4));

            //test input
            // Globals.planets.Add((-1,0,2));
            // Globals.planets.Add((2,-10,-7));
            // Globals.planets.Add((4,-8,8));
            // Globals.planets.Add((3,5,-1));

            //test input 2
            Globals.planets.Add((-8,-10,0));
            Globals.planets.Add((5,5,10));
            Globals.planets.Add((2,-7,3));
            Globals.planets.Add((9,-8,-3));

            Globals.initialplanets = Globals.planets.ToList();
            //initial speed =0
            Globals.velocities.Add((0,0,0));
            Globals.velocities.Add((0,0,0));
            Globals.velocities.Add((0,0,0));
            Globals.velocities.Add((0,0,0));
        }

        static void DoCycle()
        {   //first update velocities then apply to do a full cycle
            UpdateVelocities();
            ApplyVelocities();
        }

        static void UpdateVelocities()
        {
            //change velocities
            for (int i = 0;i < Globals.planets.Count; i++)
            {
                foreach ( (int,int,int) otherplanet in Globals.planets) //for each pair of planets
                {
                    if (Globals.planets[i] != otherplanet) //don't compare to self
                    {
                        int deltavx = 0;  //start with no change
                        int deltavy = 0;
                        int deltavz = 0;

                        if (Globals.planets[i].Item1 > otherplanet.Item1){deltavx = -1;} //if this bigger than other, we become smaller
                        else if (Globals.planets[i].Item1 < otherplanet.Item1){deltavx = 1;} //else reverse, otherwise =0

                        if (Globals.planets[i].Item2 > otherplanet.Item2){deltavy = -1;}
                        else if (Globals.planets[i].Item2 < otherplanet.Item2){deltavy = 1;} 

                        if (Globals.planets[i].Item3 > otherplanet.Item3){deltavz = -1;}
                        else if (Globals.planets[i].Item3 < otherplanet.Item3){deltavz = 1;} 

                        //now add the deltas to the current velocities to calculate new velocities
                        deltavx += Globals.velocities[i].Item1; 
                        deltavy += Globals.velocities[i].Item2; 
                        deltavz += Globals.velocities[i].Item3; 
                        Globals.velocities[i] = (deltavx,deltavy,deltavz);    
                    }
                }
            }
        }

        static void ApplyVelocities()
        {
            //apply velocities to positions
            for (int i = 0;i < Globals.planets.Count; i++)
            {
                int newX = 0;
                int newY = 0;
                int newZ = 0;
                // add velocities of planet i to position of planet i 
                newX = Globals.planets[i].Item1 + Globals.velocities[i].Item1;
                newY = Globals.planets[i].Item2 + Globals.velocities[i].Item2;
                newZ = Globals.planets[i].Item3 + Globals.velocities[i].Item3;
                
                Globals.planets[i] = (newX,newY,newZ);
            }
        }
    }
}
