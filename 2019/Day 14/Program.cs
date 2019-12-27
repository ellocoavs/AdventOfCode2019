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
            //var rules = File.ReadAllLines("testinput4.txt");
            List<List<(long,string)>> inputchems = new List<List<(long,string)>>();
            List<(long,string)> outputchems = new List<(long,string)>();
            List<List<(long,string)>> sortedinputchems = new List<List<(long,string)>>();
            List<(long,string)> sortedoutputchems = new List<(long,string)>();
            List<string> visited = new List<string>();
            List<(long,string)> queue = new List<(long,string)>();
            List<(long,string)> leftovers = new List<(long,string)>();

            foreach (string rule in rules)
            {
                var rightsideofrule = rule.Substring(rule.LastIndexOf("=>") + 2);
                outputchems.Add(Splitter(rightsideofrule));
                
                var leftsideofrule = rule.Substring(0, rule.LastIndexOf("=>"));
                leftsideofrule.Trim();
                string[] inters = leftsideofrule.Split(',');
                List<(long,string)> lefts = new List<(long,string)>();
                for (int i =0; i<inters.Length; i++)
                {
                    lefts.Add(Splitter(inters[i]));
                }
                inputchems.Add(lefts);
            }

            TopoSort((1,"FUEL"),outputchems,inputchems,sortedoutputchems,sortedinputchems);
            //some checks
            bool check = (outputchems.Count) == inputchems.Count;
            Console.WriteLine("Number of inputs matches number of outputs is: " +check);
            for (int i=0; i<outputchems.Count; i++)
            {
                Console.WriteLine("input: "+string.Join("",inputchems[i]) + " => outputs: " + outputchems[i] );    
                Console.WriteLine("Sorted input: "+string.Join("",sortedinputchems[i]) + " => outputs: " + sortedoutputchems[i] );    
            }


            string fuel = "FUEL";
            string ore = "ORE";

            //int answer =CalculateOreFromFuel((1,fuel),outputchems,inputchems);
            queue.Add((1,fuel));
            long answer = CalcOreBFS(sortedoutputchems,sortedinputchems,visited, queue,leftovers);
            Console.WriteLine("Result number of ore needed is: " + answer);
            
        }
        static (long,string) Splitter (string input)
        {
            var trimmedInput =input.Trim();
            string[] intermediate = trimmedInput.Split();
            int amount =  int.Parse(intermediate[0]);
            string name = intermediate[1];
            return (amount,name);
        }


        static void TopoSort ((long,string) startpoint,List<(long,string)> outputchems, List<List<(long,string)>> inputchems,List<(long,string)> sortedoutputchems, List<List<(long,string)>> sortedinputchems)
        {
            var ruleIndex =  outputchems.FindIndex(a => a.Item2.Equals(startpoint.Item2)); //find child items index in rule list
            //add in order of finding to sorted lists
            // if ruleindex then were at an ORE and we can skip?
            if (ruleIndex != -1)
            {
                sortedinputchems.Add(inputchems[ruleIndex]);
                sortedoutputchems.Add(outputchems[ruleIndex]);
            
                foreach ((long,string)newchem in inputchems[ruleIndex]) //for each child item
                {
                    int foundinqueueindex = sortedoutputchems.FindIndex(a => a.Item2.Equals(newchem.Item2)); //find if child is queueud
                    //if not in queue yet add as new with calculated amount
                    if (foundinqueueindex== -1)
                    {
                        TopoSort(newchem,outputchems,inputchems,sortedoutputchems,sortedinputchems);
                    }
                }
            }

        }
        static long CalcOreBFS(List<(long,string)> outputchems, List<List<(long,string)>> inputchems, List<string> visited, List<(long,string)> queue, List<(long,string)> leftovers )
        {
            long OreCounter = 0;
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
                    ruleIndex =  outputchems.FindIndex(a => a.Item2.Equals(item.Item2)); //find child items index in rule list
                    int leftoverindex = leftovers.FindIndex(a => a.Item2.Equals(item.Item2));
                    bool leftoversproduced = false;
                    foreach ((long,string)newchem in inputchems[ruleIndex]) //for each child item
                    {
                        long numbertouse=0;
                        //check if we have leftovers, reduce by that amount?
                        
                        if (leftoverindex > -1) //we found leftovers!
                        {
                            //calculate amount of item we need, reduce by leftovers, remove from leftovers
                            Console.WriteLine("We found some leftovers to reuse for: " + leftovers[leftoverindex].Item2 + ", amount: " +leftovers[leftoverindex].Item1);
                            numbertouse =(long)(Math.Ceiling((decimal)item.Item1/(outputchems[ruleIndex].Item1))) * newchem.Item1 - leftovers[leftoverindex].Item1;
                            leftovers.RemoveAt(leftoverindex);
                            leftoverindex =-1;
                        }
                        else
                        {
                        //calculate amount of item we need
                        numbertouse = (long)(Math.Ceiling((decimal)item.Item1/(outputchems[ruleIndex].Item1))) * newchem.Item1;
                        }

                        //compute lefover from needed!
                        // TODO figure out how to use leftovers?
                        long leftovernewchem = ((numbertouse / newchem.Item1) * outputchems[ruleIndex].Item1)  - item.Item1;
                        if (leftovernewchem > 0 && leftoversproduced == false)
                        {
                            Console.WriteLine("We are going to have some leftovers from this reaction: " +leftovernewchem +" , "+ item.Item2);
                            leftovers.Add((leftovernewchem,item.Item2));
                            leftoversproduced = true;
                        }
                        
                        int foundinqueueindex = queue.FindIndex(a => a.Item2.Equals(newchem.Item2)); //find if child is queueud
                        //also find if child is in leftovers
                        int newchemleftoversindex = leftovers.FindIndex( b => b.Item2.Equals(newchem.Item2));
                        if (newchemleftoversindex > -1)
                        {
                            Console.WriteLine("Leftovers found to use up for child! " + newchem.Item2 + " in the amount of " + leftovers[newchemleftoversindex].Item1 + ", reducing required amount by this.");
                            numbertouse -=leftovers[newchemleftoversindex].Item1;
                            leftovers.RemoveAt(newchemleftoversindex);
                        }
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
