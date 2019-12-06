using System;
using System.IO;
using System.Collections.Generic;

namespace Day_6
{
    class Program
    {
        static void Main(string[] args)
        {
            var orbits = File.ReadAllLines("input.txt");
            int orbitCounter = 0;
            List<string> leftsides = new List<string>();
            List<string> rightsides = new List<string>();


            foreach (string orbitline in orbits)
            {
                var rightside = orbitline.Substring(orbitline.LastIndexOf(')') + 1);
                var leftside = orbitline.Substring(0, orbitline.LastIndexOf(')'));
                leftsides.Add(leftside);
                rightsides.Add(rightside);
            }

            for (int i =0; i < rightsides.Count; i++)
            {
                orbitCounter += FindOrbits(rightsides[i],leftsides,rightsides);
            }

            Console.WriteLine("Total number of orbits is: " + orbitCounter);
        }

        static int FindOrbits(string rightside, List<string> leftsides,List<string> rightsides)
        {
            if (rightside != "COM")
                {
                    return 1 + FindOrbits(leftsides[rightsides.IndexOf(rightside)],leftsides,rightsides);
                }
            else return 0;
        }
    }
}
