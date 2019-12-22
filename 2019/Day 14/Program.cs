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
                var rightsideofrule = rule.Substring(rule.LastIndexOf("=>") + 2);
                outputchems.Add(Splitter(rightsideofrule));
                
                var leftsideofrule = rule.Substring(0, rule.LastIndexOf("=>"));
                leftsideofrule.Trim();
                string[] inters = leftsideofrule.Split(',');
                (int,string)[] lefts = new (int,string)[100];
                for (int i =0; i<inters.Length; i++)
                {
                    lefts[i] = Splitter(inters[i]);
                }
                inputchems.Add(lefts);
            }

            //some checks
            bool check = (outputchems.Count) == inputchems.Count;
            Console.WriteLine("Number of inputs matches number of outputs is: " +check);
            for (int i=0; i<outputchems.Count; i++)
            {
                Console.WriteLine("input: "+string.Join("",inputchems[i]) + " => outputs: " + outputchems[i] );    
            }

            string fuel = "FUEL";
            string ore = "ORE";

            
        }
        static (int,string) Splitter (string input)
        {
            var trimmedInput =input.Trim();
            string[] intermediate = trimmedInput.Split();
            int amount =  int.Parse(intermediate[0]);
            string name = intermediate[1];
            return (amount,name);
        }
        static List<(int,string)> FuelToOre (int count,string chemical)
        {
            return new List<(int,string)>();//create rulepoin class with a count and name?
        }
    }
}
