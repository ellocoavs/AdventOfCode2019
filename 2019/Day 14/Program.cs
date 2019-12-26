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
            var rules = File.ReadAllLines("testinput3.txt");
            List<List<(int,string)>> inputchems = new List<List<(int,string)>>();
            List<(int,string)> outputchems = new List<(int,string)>();
            List<string> visited = new List<string>();
            List<(int,string)> queue = new List<(int,string)>();


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
            // bool check = (outputchems.Count) == inputchems.Count;
            // Console.WriteLine("Number of inputs matches number of outputs is: " +check);
            // for (int i=0; i<outputchems.Count; i++)
            // {
            //     Console.WriteLine("input: "+string.Join("",inputchems[i]) + " => outputs: " + outputchems[i] );    
            // }


            string fuel = "FUEL";
            string ore = "ORE";

            //int answer =CalculateOreFromFuel((1,fuel),outputchems,inputchems);
            queue.Add((1,fuel));
            int answer = CalcOreBFS(outputchems,inputchems,visited, queue);
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

        static int CalcOreBFS(List<(int,string)> outputchems, List<List<(int,string)>> inputchems, List<string> visited, List<(int,string)> queue)
        {
            int OreCounter = 0;
            while (queue.Count > 0)
            {
                Console.WriteLine("Top item in queue: " + queue[0]);
                
                //use a list as a queue, we need to be able to edit items inthe queue so no real queue datatype
                var item = queue[0];
                queue.RemoveAt(0);
                
                if(item.Item2 == "ORE") //if we ended up at ore we can count numbers of ore...
                {
                    Console.WriteLine("Ore found, adding "+ item.Item1 + " ore to total orecount of: " + OreCounter);
                    OreCounter += item.Item1;
                }
                else
                {
                    //add children with right amount in queue, do look in queue to see if we have some identical stuffs queued?
                    int ruleIndex = 0; 
                    ruleIndex =  outputchems.FindIndex(a => a.Item2.Contains(item.Item2)); //find child items index in rule list
                    foreach ((int,string)newchem in inputchems[ruleIndex]) //for each child item
                    {
                        //calculate amount of item we need
                        int numbertouse = (int)(Math.Ceiling((double)item.Item1/(outputchems[ruleIndex].Item1))) * newchem.Item1;
                        
                        int foundinqueueindex = queue.FindIndex(a => a.Item2.Equals(newchem.Item2)); //find if child is queueud
                        //if not in queue yet add as new with calculated amount
                        if (foundinqueueindex== -1)
                        {
                            Console.WriteLine("Not found, adding to queue: "+numbertouse +" of item " +newchem.Item2);
                            queue.Add((numbertouse,newchem.Item2));
                        }
                        //if in queue, add calculated amount to existing
                        else
                        {
                            Console.WriteLine("Found at: "+ foundinqueueindex + " Adding amount to currently queued chem: "+numbertouse + " " + newchem.Item2);
                            queue[foundinqueueindex] = (numbertouse+queue[foundinqueueindex].Item1, newchem.Item2);
                        }
                        //result += CalculateOreFromFuel((numbertouse,newchem.Item2),outputchems,inputchems);
                    }

                }
            }
            return OreCounter;
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
