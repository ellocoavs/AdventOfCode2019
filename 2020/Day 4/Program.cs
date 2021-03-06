﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
                    Console.WriteLine("Invalid passport: " + passport );
                    Console.WriteLine("Doesn't contain required field: " + field);
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
                        Console.WriteLine("Trying to parse year from: " + passport.Substring(index+length-4, 4));
                        if (length <= 8)
                        {
                            int year1 = int.Parse( passport.Substring(index+length-4, 4));
                            if (!(year1 >= 1920 && year1 <= 2002))
                            {
                                Console.WriteLine("Invalid passport: " + passport );
                                Console.WriteLine("Problem in required field: " + field);
                                return false; //not in valid date range
                            }
                            else 
                            {
                                validFields++;
                            }
                        }
                        else
                        {
                            if (! (passport[index+8] == ' ' || passport[index+8].ToString() == Environment.NewLine))
                    {
                        Console.WriteLine("Index of space after field is: "+ spaceindex);
                        Console.WriteLine("Index+8 is" + passport[index+8]);
                        Console.WriteLine("Field was malformed!");
                        Console.WriteLine("Invalid passport: " + passport );
                        Console.WriteLine("Problem in required field: " + field);
                        return false; //field was malformed
                    }
                    
                    Console.WriteLine("Checking year value correctness");
                    int year2 = int.Parse(passport.Substring(index+4,4)); //second argument =4 because years are 4 characters
                    Console.WriteLine("Year is:" + year2);
                    if (!(year2 >= 1920 && year2 <= 2002))
                    {
                        Console.WriteLine("Invalid passport: " + passport );
                        Console.WriteLine("Problem in required field: " + field);
                        return false; //not in valid date range
                    }
                    else 
                    {
                        validFields++;
                    }
                        }
                    }
                    else if (! (passport[index+8] == ' ' || passport[index+8].ToString() == Environment.NewLine))
                    {
                        Console.WriteLine("Index of space after field is: "+ spaceindex);
                        Console.WriteLine("Index+8 is" + passport[index+8]);
                        Console.WriteLine("Field was malformed!");
                        Console.WriteLine("Invalid passport: " + passport );
                        Console.WriteLine("Problem in required field: " + field);
                        return false; //field was malformed
                    }
                    
                    Console.WriteLine("Checking year value correctness");
                    int year = int.Parse(passport.Substring(index+4,4)); //second argument =4 because years are 4 characters
                    Console.WriteLine("Year is:" + year);
                    if (!(year >= 1920 && year <= 2002))
                    {
                        Console.WriteLine("Invalid passport: " + passport );
                        Console.WriteLine("Problem in required field: " + field);
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
                    Console.WriteLine("Checking byr index + 8 chars being a space. Index is:" + index);
                    int spaceindex = passport.IndexOf(" ",index);
                    if (spaceindex == -1)
                    {
                        //last field of passport, no space afterwards, so check for stuffs in safe way
                        int length = passport.Substring(index).Length;
                        Console.WriteLine("There are " + length + " characters till end of passport");
                        Console.WriteLine("Trying to parse year from: " + passport.Substring(index+length-4, 4));
                        if (length <= 8)
                        {
                            int year1 = int.Parse( passport.Substring(index+length-4, 4));
                            if (!(year1 >= 2010 && year1 <= 2020))
                            {
                                Console.WriteLine("Invalid passport: " + passport );
                                Console.WriteLine("Problem in required field: " + field);
                                return false; //not in valid date range
                            }
                            else 
                            {
                                validFields++;
                            }
                        }
                        else
                        {
                            if (! (passport[index+8] == ' ' || passport[index+8].ToString() == Environment.NewLine))
                    {
                        Console.WriteLine("Index of space after field is: "+ spaceindex);
                        Console.WriteLine("Index+8 is" + passport[index+8]);
                        Console.WriteLine("Field was malformed!");
                        Console.WriteLine("Invalid passport: " + passport );
                        Console.WriteLine("Problem in required field: " + field);
                        return false; //field was malformed
                    }
                    
                    Console.WriteLine("Checking year value correctness");
                    int year2 = int.Parse(passport.Substring(index+4,4)); //second argument =4 because years are 4 characters
                    Console.WriteLine("Year is:" + year2);
                    if (!(year2 >= 2010 && year2 <= 2020))
                    {
                        Console.WriteLine("Invalid passport: " + passport );
                        Console.WriteLine("Problem in required field: " + field);
                        return false; //not in valid date range
                    }
                    else 
                    {
                        validFields++;
                    }
                        }
                    }
                    else if (! (passport[index+8] == ' ' || passport[index+8].ToString() == Environment.NewLine))
                    {
                        Console.WriteLine("Index of space after field is: "+ spaceindex);
                        Console.WriteLine("Index+8 is" + passport[index+8]);
                        Console.WriteLine("Field was malformed!");
                        Console.WriteLine("Invalid passport: " + passport );
                        Console.WriteLine("Problem in required field: " + field);
                        return false; //field was malformed
                    }
                    
                    Console.WriteLine("Checking year value correctness");
                    int year = int.Parse(passport.Substring(index+4,4)); //second argument =4 because years are 4 characters
                    Console.WriteLine("Year is:" + year);
                    if (!(year >= 2010 && year <= 2020))
                    {
                        Console.WriteLine("Invalid passport: " + passport );
                        Console.WriteLine("Problem in required field: " + field);
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
                    Console.WriteLine("Checking byr index + 8 chars being a space. Index is:" + index);
                    int spaceindex = passport.IndexOf(" ",index);
                    if (spaceindex == -1)
                    {
                        //last field of passport, no space afterwards, so check for stuffs in safe way
                        int length = passport.Substring(index).Length;
                        Console.WriteLine("There are " + length + " characters till end of passport");
                        Console.WriteLine("Trying to parse year from: " + passport.Substring(index+length-4, 4));
                        if (length <= 8)
                        {
                            int year1 = int.Parse( passport.Substring(index+length-4, 4));
                            if (!(year1 >= 2020 && year1 <= 2030))
                            {
                                Console.WriteLine("Invalid passport: " + passport );
                                Console.WriteLine("Problem in required field: " + field);
                                return false; //not in valid date range
                            }
                            else 
                            {
                                validFields++;
                            }
                        }
                        else
                        {
                            if (! (passport[index+8] == ' ' || passport[index+8] == '\r'))
                            {
                                Console.WriteLine("Index of space after field is: "+ spaceindex);
                                Console.WriteLine("Index+8 is" + passport[index+8]);
                                Console.WriteLine("Field was malformed!");
                                Console.WriteLine("Invalid passport: " + passport );
                                Console.WriteLine("Problem in required field: " + field);
                                return false; //field was malformed
                            }
                    
                            Console.WriteLine("Checking year value correctness");
                            int year2 = int.Parse(passport.Substring(index+4,4)); //second argument =4 because years are 4 characters
                            Console.WriteLine("Year is:" + year2);
                            if (!(year2 >= 2020 && year2 <= 2030))
                            {
                                Console.WriteLine("Invalid passport: " + passport );
                                Console.WriteLine("Problem in required field: " + field);
                                return false; //not in valid date range
                            }
                            else 
                            {
                                validFields++;
                            }
                        }
                    }
                    else if (! (passport[index+8] == ' ' || passport[index+8] == '\r'))
                    {
                        Console.WriteLine("Index of space after field is: "+ spaceindex);
                        Console.WriteLine("Index+8 is" + passport[index+8]);
                        Console.WriteLine("Unicode value is: " + (int)passport[index+8]);
                        Console.WriteLine("Field was malformed!");
                        Console.WriteLine("Invalid passport: " + passport );
                        Console.WriteLine("Problem in required field: " + field);
                        return false; //field was malformed
                    }
                    
                    Console.WriteLine("Checking year value correctness");
                    int year = int.Parse(passport.Substring(index+4,4)); //second argument =4 because years are 4 characters
                    Console.WriteLine("Year is:" + year);
                    if (!(year >= 2020 && year <= 2030))
                    {
                        Console.WriteLine("Invalid passport: " + passport );
                        Console.WriteLine("Problem in required field: " + field);
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
                    Console.WriteLine("Index of field is: " + index);
                    int spaceindex = passport.IndexOf(" ",index);
                    Console.WriteLine("Are we at the last field of the passport, then -1 : " + spaceindex);
                    if (spaceindex == -1)
                    {
                        //last field of passport, no space afterwards, so check for stuffs in safe way
                        int length = passport.Substring(index).Length;
                        Console.WriteLine("There are " + length + " characters till end of passport");
                        Console.WriteLine(passport.Substring(index+length-2, 2) + " found");
                        if ( passport.Substring(index+length-2, 2) == "cm")
                        {
                            Console.WriteLine("CM detected, checking values");
                            if (passport.Substring(index+length-5, 3).All(char.IsDigit)) //three digits?
                            {
                                Console.WriteLine("Valid substring detected, parsing value");
                                int height = int.Parse(passport.Substring(index+length-5, 3));
                                Console.WriteLine ("Height is: " + height);
                                if (!(height >= 150 && height <= 193))
                                {
                                    Console.WriteLine("Invalid passport: " + passport );
                                    Console.WriteLine("Problem in required field: " + field);
                                    return false;
                                }
                                else 
                                {
                                    validFields++;
                                }
                            }
                        }
                        else if ( passport.Substring(index+length-2, 2) == "in")
                        {
                             Console.WriteLine("INCH detected, checking values");
                            if (passport.Substring(index+length-5, 3).All(char.IsDigit)) //three digits?
                            {
                                int height = int.Parse(passport.Substring(index+length-5, 3));
                                if (!(height >= 59 && height <= 76))
                                {
                                    Console.WriteLine("Invalid passport: " + passport );
                                    Console.WriteLine("Problem in required field: " + field);
                                    return false;
                                }
                                else 
                                {
                                    validFields++;
                                }
                            }
                        }
                    }
                    else if ( passport.Substring(spaceindex-2, 2) == "cm")
                    {
                        //check for 3 numbers between 150 and 193
                        if (passport.Substring(spaceindex-5, 3).All(char.IsDigit)) //three digits?
                        {
                            int height = int.Parse(passport.Substring(spaceindex-5, 3));
                            if (!(height >= 150 && height <= 193))
                            {
                                Console.WriteLine("Invalid passport: " + passport );
                                Console.WriteLine("Problem in required field: " + field);
                                return false;
                            }
                            else 
                            {
                                validFields++;
                            }
                        }
                    }
                    else if ( passport.Substring(spaceindex-2, 2) == "in")
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
                        Console.WriteLine("Invalid passport: " + passport );
                        Console.WriteLine("Problem in required field: " + field);
                        return false; //field was malformed
                    }
                    
                }
                if (field =="hcl")
                {
                    int index = passport.IndexOf(field);
                    Console.WriteLine("Index of field is: " + index);
                    int spaceindex = passport.IndexOf(" ",index);
                    Console.WriteLine("Are we at the last field of the passport, then -1 : " + spaceindex);
                    if (spaceindex == -1)
                    {
                        int length = passport.Substring(index).Length;
                        Console.WriteLine("There are " + length + " characters till end of passport");
                        Console.WriteLine(passport.Substring(index, 1) + " found");
                        if (passport.Substring(index, 1) == "#"  ) //starts with hashtag
                        {
                            if (Regex.IsMatch(passport.Substring(index+1,6), @"^[a-f0-9]+$"))
                            {
                                validFields++;
                            }
                        }
                    }
                    else if ( passport.Substring(spaceindex-7, 1) == "#")
                    {   
                        Console.WriteLine("Found a #: " +passport.Substring(spaceindex-7, 1));
                        if (Regex.IsMatch(passport.Substring(spaceindex-6,6), @"^[a-f0-9]+$"))
                        {
                            validFields++;
                        }

                    }
                    else 
                    {
                        Console.WriteLine("Invalid passport: " + passport );
                        Console.WriteLine("Problem in required field: " + field);
                        return false;
                    }
                    
                }
                if (field =="ecl")
                {
                    string[] eyecolours = {"amb","blu","brn","gry","grn","hzl","oth"};
                    int index = passport.IndexOf(field);
                    Console.WriteLine("Index of field is: " + index);
                    int spaceindex = passport.IndexOf(" ",index);
                    Console.WriteLine("Are we at the last field of the passport, then -1 : " + spaceindex);
                    if (spaceindex == -1)
                    {
                        int length = passport.Substring(index).Length;
                        Console.WriteLine("There are " + length + " characters till end of passport");
                        Console.WriteLine(passport.Substring(index+4, 1) + " found");
                        if (eyecolours.Any(s => s.Equals(passport.Substring(index+4, 3)))) //if string matches one of the valid eyecolors
                        {
                            validFields++;
                        }
                    }
                    else if (eyecolours.Any(s => s.Equals(passport.Substring(index+4, 3)))) //if string matches one of the valid eyecolors
                    {
                        validFields++;
                    }
                    else
                    {
                        Console.WriteLine("Invalid passport: " + passport );
                        Console.WriteLine("Problem in required field: " + field);
                        return false;
                    }
                }
                if (field =="pid")
                {
                    int index = passport.IndexOf(field);
                    Console.WriteLine("Index of field is: " + index);
                    int spaceindex = passport.IndexOf(" ",index);
                    Console.WriteLine("Are we at the last field of the passport, then -1 : " + spaceindex);
                    if (spaceindex == -1)
                    {
                        int length = passport.Substring(index).Length;
                        Console.WriteLine("There are " + length + " characters till end of passport");
                        Console.WriteLine(passport.Substring(index+4, 9) + " found");
                        if (passport.Substring(index+length-9, 9).All(char.IsDigit))
                        {
                            validFields++;
                        }
                    }
                    else if (passport.Substring(spaceindex-9, 9).All(char.IsDigit))
                    {
                        validFields++;
                    }
                    else
                    {
                        Console.WriteLine("Invalid passport: " + passport );
                        Console.WriteLine("Problem in required field: " + field);
                        return false;
                    }
                }

            }   
            if (validFields >= requiredValidFields)
            {   // looped through all, none were missing, therefore valid!
                Console.WriteLine("Valid passport: " + passport );
                return true;
            }
            else
            {
                Console.WriteLine("Invalid passport due to missing field: " + passport );
                return false; //some field was missing?
            }
        }
    }
}
