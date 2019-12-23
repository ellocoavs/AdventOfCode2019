using System;
using System.IO;
using System.Collections.Generic;

namespace Day_14
{
    class Program
    {
        static void Main(string[] args)
        {
            //var rules = File.ReadAllLines("input.txt");
            var rules = File.ReadAllLines("testinput1.txt");
            List<List<(int,string)>> inputchems = new List<List<(int,string)>>();
            List<(int,string)> outputchems = new List<(int,string)>();

            foreach (string rule in rules)
            {
                var rightsideofrule = rule.Substring(rule.LastIndexOf("=>") + 2);
                outputchems.Add(Splitter(rightsideofrule));
                
                var leftsideofrule = rule.Substring(0, rule.LastIndexOf("=>"));
                leftsideofrule.Trim();
                string[] inters = leftsideofrule.Split(',');
                List<(int,string)> lefts = new List<(int,string)>();
                for (int i =0; i<inters.Length; i++)
                {
                    lefts.Add(Splitter(inters[i]));
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

            int answer =CalculateOreFromFuel((1,fuel),outputchems,inputchems);

            Console.WriteLine("Result number of ore needed is: " + answer);
            
        }
        static (int,string) Splitter (string input)
        {
            var trimmedInput =input.Trim();
            string[] intermediate = trimmedInput.Split();
            int amount =  int.Parse(intermediate[0]);
            string name = intermediate[1];
            return (amount,name);
        }

        static int CalculateOreFromFuel ((int,string) currentchem, List<(int,string)> outputchems, List<List<(int,string)>> inputchems)
        {

            int currentnumber = currentchem.Item1;
            string currentname = currentchem.Item2;
            Console.WriteLine("Processing: "+currentnumber+" of "+currentname);
            if (currentname =="ORE")
            {
                return currentnumber;
            }
            else
            {
                int ruleIndex = 0; 
                ruleIndex =  outputchems.FindIndex(a => a.Item2.Contains(currentname));
                int result = 0;
                foreach ((int,string)newchem in inputchems[ruleIndex])
                {
                    //wrong calculations
                    //int numbertouse = (currentnumber/newchem.Item1) *outputchems[ruleIndex].Item1;
                    //(newchem.Item1*currentnumber) / (currentnumber/outputchems[ruleIndex].Item1);

                    int numbertouse = (currentnumber/(outputchems[ruleIndex].Item1)) * newchem.Item1;
                    result += CalculateOreFromFuel((numbertouse,newchem.Item2),outputchems,inputchems);
                }
                return result;
            }

        }


        static List<(int,string)> FuelToOre (int count,string chemical)
        {
            return new List<(int,string)>();
        }
    }
}
