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
            
            
            List<string> leftsides = new List<string>();
            List<string> rightsides = new List<string>();
            
            string startPlanet = "YOU";
            List<string> startToCOM = new List<string>();
            string endPlanet = "SAN";
            List<string> endToCOM = new List<string>();
            string centerplanet = "COM";
            string commonAncestor = "";
            int startToAncestor = 0;
            int endToAncestor = 0;

            foreach (string orbitline in orbits)
            {
                var rightside = orbitline.Substring(orbitline.LastIndexOf(')') + 1);
                var leftside = orbitline.Substring(0, orbitline.LastIndexOf(')'));
                leftsides.Add(leftside);
                rightsides.Add(rightside);
            }

            pathToCom(startPlanet,leftsides,rightsides,startToCOM);
            Console.WriteLine(startToCOM.Count);

            pathToCom(endPlanet,leftsides,rightsides,endToCOM);
            Console.WriteLine(endToCOM.Count);

            commonAncestor = findFirstCommon(startToCOM, endToCOM);
            if (commonAncestor != null)
            {
                Console.WriteLine(commonAncestor);
            }
            startToAncestor = startToCOM.IndexOf(commonAncestor);
            endToAncestor = endToCOM.IndexOf(commonAncestor);
            
            Console.WriteLine("First index: " + startToAncestor);
            Console.WriteLine("Second index: " + endToAncestor);
            Console.WriteLine("So result should be: " + (startToAncestor+endToAncestor));
            // for (int i =0; i < rightsides.Count; i++)
            // {
            //     orbitCounter += FindOrbits(rightsides[i],leftsides,rightsides);
            // }

            // Console.WriteLine("Total number of orbits is: " + orbitCounter);
        }

        static void pathToCom (string startfrom,List<string> leftsides,List<string> rightsides, List<string> path)
        {
            if (startfrom != "COM")
            {
                string parent = leftsides[rightsides.IndexOf(startfrom)];
                path.Add(parent);
                pathToCom(parent, leftsides,rightsides,path);
            }
        }
        static string findFirstCommon (List<string> start, List<string> end)
        {
            foreach (string step in start)
            {
                if (end.Contains(step))
                {
                    //return common ancestor
                    return step;
                }
            }
            return null;
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
