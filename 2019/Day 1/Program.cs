using System;
using System.IO;

namespace Day_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            int[] parts= new int[100] ;
            int counter = 0;
            foreach (string x in lines)
            {
                parts[counter] =Int32.Parse(x);
                counter++;
            }
            double RunningTotal = 0 ;
            
            Console.WriteLine("Starting total = " + RunningTotal);
            foreach (int part in parts)
                {
                    Console.WriteLine("Part weight is:" + part);
                    double partfuel =CalculateTotalFuelForPart(part);
                    Console.WriteLine("PartFuel is: " + partfuel);
                    RunningTotal += partfuel;
                    Console.WriteLine("Running total is: " + RunningTotal);
                }
            Console.WriteLine("End total = " + RunningTotal);
        }
        static double CalculateTotalFuelForPart (double PartWeight)
        {
            int count = 0;
            double Remaining = Math.Floor(PartWeight / 3) - 2;
            double TotalFuel = Remaining;
            while (Remaining >= 0 && count < 30)
                {
                    double Increment = Math.Floor(Remaining / 3) - 2;
                    if (Increment <= 0){break;}
                    TotalFuel += Increment;
                    Remaining = Increment;
                    count++;
                    Console.WriteLine("Fuel added amount is: " + Increment);
                    Console.WriteLine("Remaining Fuel amount is: " + Remaining);
                }
            return TotalFuel;
        }
    }
}
