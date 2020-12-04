using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day_4
{
    class Program
    {
        static class Globals
        {
            public static string[] requiredfields;
             //BirthYear ,(Issue Year),(Expiration Year),(Height),(Hair Color),(Eye Color),(Passport ID
             public static string[] optionalfields;
             //(Country ID)
        }
        static void Main(string[] args)
        {
            Globals.requiredfields = new string[]{"byr","iyr","eyr","hgt","hcl","ecl","pid"};
            Globals.optionalfields = new string[]{"cid"};

            string input = File.ReadAllText("input.txt");
            string[] passports = input.Split(new string[] { "\r\n\r\n" },StringSplitOptions.RemoveEmptyEntries);
            
            int validpassports = 0;
            foreach (string s in passports)
            {
                Console.WriteLine(s);
                Console.WriteLine();
                if (ValidatePassport(s,Globals.requiredfields))
                {
                    validpassports++;
                }

            }
            Console.WriteLine("Number of valid passports is: " + validpassports);
        }
        static bool ValidatePassport (string passport, string[] requiredfields)
        {
            foreach (string field in requiredfields)
            {
                if (! passport.Contains(field)) //loop through all. if it doesn't contain the current required field -> false
                {
                    return false;
                }
                if (field =="byr")
                {
                    int index = passport.IndexOf(field);
                    if (! (passport[index+8] == ' ' || passport[index+8].ToString() == Environment.NewLine))
                    {
                        return false; //field was malformed
                    }
                    
                    int year = int.Parse(passport.Substring(index+4,index+8));
                    if (!(year >= 1920 && year <= 2002))
                    {
                        return false; //not in valid date range
                    }
                }

                if (field =="iyr")
                {
                    int index = passport.IndexOf(field);
                    if (! (passport[index+8] == ' ' || passport[index+8].ToString() == Environment.NewLine))
                    {
                        return false; //field was malformed
                    }
                    
                    int year = int.Parse(passport.Substring(index+4,index+8));
                    if (!(year >= 2010 && year <= 2020))
                    {
                        return false; //not in valid date range
                    }
                }

                if (field =="eyr")
                {
                    int index = passport.IndexOf(field);
                    if (! (passport[index+8] == ' ' || passport[index+8].ToString() == Environment.NewLine))
                    {
                        return false; //field was malformed
                    }
                    
                    int year = int.Parse(passport.Substring(index+4,index+8));
                    if (!(year >= 2020 && year <= 2030))
                    {
                        return false; //not in valid date range
                    }
                }
                
                if (field =="hgt")
                {
                    int index = passport.IndexOf(field);
                    if (! (passport[index+8] == ' ' || passport[index+8].ToString() == Environment.NewLine))
                    {
                        return false; //field was malformed
                    }
                    
                    int year = int.Parse(passport.Substring(index+4,index+8));
                    if (!(year >= 2010 && year <= 2020))
                    {
                        return false; //not in valid date range
                    }
                }


            }   // looped through all, none were missing, therefore valid!
            return true;
        }
    }
}
