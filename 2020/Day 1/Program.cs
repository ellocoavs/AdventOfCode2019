using System;
using System.IO;

namespace Day_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            int[] numbers = new int[200];
            int counter =0;
            foreach (var x in lines)
            {
                numbers[counter] = Int32.Parse(x);
                counter++;
            }
            Console.WriteLine("Total numbers parsed: " + counter);
            Console.WriteLine("The following two numbers add up to 2020:");
            bool found = false;
            foreach (int x in numbers){
                foreach (int y in numbers){
                    foreach (int z in numbers){
                        if (x+y+z == 2020)
                        {
                            Console.WriteLine(x);
                            Console.WriteLine(y);
                            Console.WriteLine(z);
                            Console.WriteLine("Multiplied value: " + (x*y*z) );
                            found = true;
                        }
                    if (found==true) {break;}
                    }
                if (found==true) {break;}
                }
            if (found==true) {break;}
            }
        }
    }
}
