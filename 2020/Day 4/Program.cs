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
            Console.WriteLine("Now validating: " + passport);
            int validFields = 0;
            int requiredValidFields = requiredfields.Count();

            foreach (string field in requiredfields)
            {
                Console.WriteLine("Now checking field: " + field);    
                if (! passport.Contains(field)) //loop through all. if it doesn't contain the current required field -> false
                {
                    return false;
                }
                if (field =="byr")
                {
                    int index = passport.IndexOf(field);
                    Console.WriteLine("Checking byr index + 8 chars being a space. Index is:" + index);
                    int spaceindex = passport.IndexOf(" ",index);
                    if (spaceindex == -1)
                    {
                        //last field of passport, no space afterwards, so check for stuffs in safe way
                        int length = passport.Substring(index).Length;
                        Console.WriteLine("There are " + length + " characters till end of passport");

                    }
                    else if (! (passport[index+8] == ' ' || passport[index+8].ToString() == Environment.NewLine))
                    {
                        Console.WriteLine("Index of space after field is: "+ spaceindex);
                        Console.WriteLine("Index+8 is" + passport[index+8]);
                        Console.WriteLine("Field was malformed!");
                        return false; //field was malformed
                    }
                    
                    Console.WriteLine("Checking year value correctness");
                    int year = int.Parse(passport.Substring(index+4,4)); //second argument =4 because years are 4 characters
                    Console.WriteLine("Year is:" + year);
                    if (!(year >= 1920 && year <= 2002))
                    {
                        return false; //not in valid date range
                    }
                    else 
                    {
                        validFields++;
                    }
                }

                if (field =="iyr")
                {
                    int index = passport.IndexOf(field);
                    Console.WriteLine("Checking iyr index + 8 chars being a space. Index is:" + index);
                    int spaceindex = passport.IndexOf(" ",index);
                    if (spaceindex == -1)
                    {
                        //last field of passport, no space afterwards, so check for stuffs in safe way
                        int length = passport.Substring(index).Length;
                        Console.WriteLine("There are " + length + " characters till end of passport");

                    }
                    else if (! (passport[index+8] == ' ' || passport[index+8].ToString() == Environment.NewLine))
                    {
                        Console.WriteLine("Index of space after field is: "+ spaceindex);
                        Console.WriteLine("Index+8 is" + passport[index+8]);
                        Console.WriteLine("Field was malformed!");
                        return false; //field was malformed
                    }
                    
                    Console.WriteLine("Checking year value correctness");
                    int year = int.Parse(passport.Substring(index+4,4)); //second argument =4 because years are 4 characters
                    Console.WriteLine("Year is:" + year);
                    if (!(year >= 2010 && year <= 2020))
                    {
                        return false; //not in valid date range
                    }
                    else 
                    {
                        validFields++;
                    }
                }

                if (field =="eyr")
                {
                    int index = passport.IndexOf(field);
                    Console.WriteLine("Checking eyr index + 8 chars being a space. Index is:" + index);
                    int spaceindex = passport.IndexOf(" ",index);
                    if (spaceindex == -1)
                    {
                        //last field of passport, no space afterwards, so check for stuffs in safe way
                        int length = passport.Substring(index).Length;
                        Console.WriteLine("There are " + length + " characters till end of passport");

                    }
                    else if (! (passport[index+8] == ' ' || passport[index+8].ToString() == Environment.NewLine))
                    {
                        Console.WriteLine("Index of space after field is: "+ spaceindex);
                        Console.WriteLine("Index+8 is" + passport[index+8]);
                        Console.WriteLine("Field was malformed!");
                        return false; //field was malformed
                    }
                    
                    Console.WriteLine("Checking year value correctness");
                    int year = int.Parse(passport.Substring(index+4,4)); //second argument =4 because years are 4 characters
                    Console.WriteLine("Year is:" + year);
                    if (!(year >= 2020 && year <= 2030))
                    {
                        return false; //not in valid date range
                    }
                    else 
                    {
                        validFields++;
                    }
                }
                
                if (field =="hgt")
                {
                    int index = passport.IndexOf(field);
                    int spaceindex = passport.IndexOf(" ",index);
                    if (spaceindex == -1)
                    {
                        //last field of passport, no space afterwards, so check for stuffs in safe way
                        int length = passport.Substring(index).Length;
                        Console.WriteLine("There are " + length + " characters till end of passport");

                    }
                    if ( passport.Substring(spaceindex-2, 2) == "cm")
                    {
                        //check for 3 numbers between 150 and 193
                        if (passport.Substring(spaceindex-5, 3).All(char.IsDigit)) //three digits?
                        {
                            int height = int.Parse(passport.Substring(spaceindex-5, 3));
                            if (!(height >= 150 && height <= 193))
                            {
                                return false;
                            }
                            else 
                            {
                                validFields++;
                            }
                        }
                    }
                    if ( passport.Substring(spaceindex-2, 2) == "in")
                    {
                        //check for 2 numbers between 59 and 76
                        if (passport.Substring(spaceindex-4, 2).All(char.IsDigit)) //two digits?
                        {
                            int height = int.Parse(passport.Substring(spaceindex-4, 2));
                            if (!(height >= 59 && height <= 76))
                            {
                                return false;
                            }
                            else 
                            {
                                validFields++;
                            }
                        }
                    }
                    else 
                    {
                    return false; //field was malformed
                    }
                    
                }


            }   
            if (validFields >= requiredValidFields)
            {   // looped through all, none were missing, therefore valid!
                return true;
            }
            else
            {
                return false; //some field was missing?
            }
        }
    }
}
