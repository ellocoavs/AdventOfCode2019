using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            int validpasswords = 0;
            foreach (string line in input)
            {
                Console.WriteLine(line);

                string[] parts = line.Split(":");
                char letter = parts[0].Last();
                string rule = parts[0].Remove(parts[0].Length - 2);
                string[] numbers = rule.Split("-");
                int min = Int32.Parse(numbers[0]);
                int max = Int32.Parse(numbers[1]);
                string password =parts[1].Trim();
                
                int count = password.Count(f => f == letter);
                Console.WriteLine(count);
                if (count >= min && count <=max)
                {
                    validpasswords++;
                }
            }
            Console.WriteLine("Number of valid passwords is: " + validpasswords);
        }
    }
}
