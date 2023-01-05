using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;

namespace ConsoleApp1
{
    internal class Employee
    {
        private static int _counter;
        public Employee()
        {
            _counter++;
            _no = _counter;
        }

        public int No => _no;
        private int _no;

        public string FullName
        {
            get => _fullName;
            set
            {
                if (CheckFullName(value))
                    _fullName = value;
            }
        }

        private string _fullName;

        public DateTime Startdate;

        private double _salary;
        public double Salary
        {
            get => _salary;
            set
            {
                _salary = value;
            }
        }

        public static bool CheckFullName(string fullName)
        {
            if (!string.IsNullOrWhiteSpace(fullName))
            {

                fullName = fullName.Trim();
                string[] splitted = fullName.Split(' ');


                if (splitted.Length != 2)
                {
                    try
                    {
                        throw new Exception(" Fullname'i yalniz 2 sozden ibaret ola biler ve Sozler arasinde 1 bosluq qoyun");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"\n{e.Message}\n");
                        return false;
                    }
                }

                else
                {
                    if (splitted[0].Length > 1 && splitted[1].Length > 1)
                    {
                        if (!char.IsUpper(splitted[0][0]) || !char.IsUpper(splitted[1][0]))
                        {
                            try
                            {
                                char.ToUpper(splitted[0][0]);
                                char.ToUpper(splitted[1][0]);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("\n Yalniz herfler daxil oluna biler\n");
                                return false;
                            }
                        }

                        for (int i = 0; i < splitted.Length; i++)
                        {
                            if (HasSymbolAndHasDigitAndHasUpper(splitted[i]))
                            {
                                try
                                {
                                    throw new Exception(" Yalniz herfler daxil oluna biler");
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine($"\n{e.Message}\n");
                                    return false;
                                }
                            }
                        }
                        return true;
                    }
                    else
                    {
                        try
                        {
                            throw new Exception(" Ad ve soyadin uzunlugu min. 2 olmalidi!");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"\n{e.Message}\n");
                            return false;
                        }
                    }
                }

            }
            else
            {

                try
                {
                    throw new Exception(" Herf daxil edin");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\n{e.Message}\n");
                    return false;
                }
            }
        }
        public static bool HasSymbolAndHasDigitAndHasUpper(string input)
        {
            for (int i = 1; i < input.Length; i++)
            {
                if (char.IsSymbol(input[i]) && char.IsDigit(input[i]) && char.IsUpper(input[i]))
                    return true;
            }
            return false;
        }
    }
}