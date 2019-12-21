using System;
using System.IO;
using System.Collections.Generic;

namespace Day_14
{
    class Program
    {
        static void Main(string[] args)
        {
            var rules = File.ReadAllLines("input.txt");
            
            List<(int,string)[]> inputchems = new List<(int,string)[]>();
            List<(int,string)> outputchems = new List<(int,string)>();

            foreach (string rule in rules)
            {
                var rightsideofrule = rule.Substring(rule.LastIndexOf("=>") + 1);
                Console.WriteLine(rightsideofrule);
                var leftsideofrule = rule.Substring(0, rule.LastIndexOf("=>"));
                Console.WriteLine(leftsideofrule);
                
                // inputchems.Add(leftsideofrule);
                // outputchems.Add(rightsideofrule);
            }

            string fuel = "FUEL";
            string ore = "ORE";

            
        }

        static (int,string)[] FuelToOre (int count,string chemical)
        {
            return (0,"");//create rulepoin class with a count and name?
        }
    }
}
