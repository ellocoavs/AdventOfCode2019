using System;
using System.Collections.Generic;
using System.Linq;


namespace Day_4
{
    class Program
    {
        static void Main(string[] args)
        {
            int lownumber = 137683;
            int highnumber = 596253;
            List<int>validnumbers = new List<int>();

            int test1 = 112233;
            int test2 = 123444;
            int test3 = 111122;
            // loop through all numbers in range with both checks, adding to a list if is passed both tests
            for (int i = lownumber; i <= highnumber; i++)
            {
                if (HasDouble(i) && !HasDecrease(i))  //if checks out
                {
                    //add to list of valids
                    validnumbers.Add(i);
                }
            }
            Console.WriteLine("Total number of valid nums: " + (validnumbers.Count));

            //Test
            // Console.WriteLine("String: " + test1 + " hasdouble result: " + HasDouble(test1));
            // Console.WriteLine("String: " + test2 + " hasdouble result: " + HasDouble(test2));
            // Console.WriteLine("String: " + test3 + " hasdouble result: " + HasDouble(test3));

            // Console.WriteLine("String: " + test1 + " HasDecrease result: " + HasDecrease(test1));
            // Console.WriteLine("String: " + test2 + " HasDecrease result: " + HasDecrease(test2));
            // Console.WriteLine("String: " + test3 + " HasDecrease result: " + HasDecrease(test3));
        }

        static Boolean HasDouble(int x)
        {
            bool doublefound = false;
            string number = x.ToString();
            for ( int i = 1 ; i < number.Length && !doublefound ; ++i )
            {
                doublefound = (number[i] == number[i-1] && !OccursMoreThanTwice(number,i));
            }
            return doublefound;
        }

        static Boolean HasDecrease(int x)
        {
            bool decreasefound = false;
            string number = x.ToString();
            for ( int i = 1 ; i < number.Length && !decreasefound ; ++i )
            {
                decreasefound = number[i] < number[i-1] ;
            }
            return decreasefound;
        }

        static Boolean OccursMoreThanTwice (string number, int i)
        {
            bool MoreThanTwice = false;
            MoreThanTwice = number.Count(c => c == number[i]) > 2;
            return MoreThanTwice;
        }
    }
}
