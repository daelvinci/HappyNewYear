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
        static Employee()
        {
            _counter++;
            _no = _counter;
        }

        public int No => _no;
        private static int _no;

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
                string[] split = fullName.Split(' ');


                if (split.Length != 2)
                {
                    try
                    {
                        throw new Exception("Fullname'i yalniz 2 sozden ibaret ola biler ve Sozler arasinde 1 bosluq qoyun");
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }

                else
                {
                    if (!char.IsUpper(split[0][0]) || !char.IsUpper(split[1][0]))
                    {
                        try
                        {
                            char.ToUpper(split[0][0]);
                            char.ToUpper(split[0][0]);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Yalniz Herfler daxil oluna biler ");
                            return false;
                        }
                    }

                    for (int i = 0; i < split[i].Length; i++)
                    {
                        for (int j = 1; j < split[i].Length; j++)
                        {
                            if (!char.IsDigit(split[i][j]) || !char.IsSymbol(split[i][j]))
                            {
                                if (!char.IsUpper(split[i][j]))
                                    return true;
                                else
                                {
                                    try
                                    {
                                        throw new Exception("Bas herfden sonrakilar kicik herf olmalidir! ");
                                    }
                                    catch (Exception)
                                    {
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                try
                                {
                                    throw new Exception("Yalniz herfler daxil oluna biler");
                                }
                                catch (Exception)
                                {
                                    return false;
                                }
                            }

                        }
                    }
                    return true;

                }
               
            }
            else
            {

                try
                {
                    throw new Exception("Herf daxil edin");
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}

//Her employe elave eliyende yoxla gor salary limit ve employee limiti asir mi
//GetEmployeByDate()de 2ci parametr optionaldi, default now'du
//Add employeda (start date) olsun 
////employe yarananda ozu nomre versin.

//Employee class
// -No
// - FullName
// - Salary
// - StartDate

//IDepartment
// - Employees
// - AddEmployee()
// - RemoveEmployee()
// - Search()

//Department class
// -Name
// - EmployeeLimit
// - SalaryLimit
// - Employees
// - AddEmployee()
// - RemoveEmployee(no)
// - GetEmployeesByDates(fromDate, toDate)
// - FindEmployeeByNo(no)
// - UpdateSalary(no, newSalary)
// - Search()


//EmployeeFullName yalnız hərflərdən
//ibarət olmalıdır və Böyük hərflə başlamalıdır
//Salary dəyəri 250-dən aşağı ola bilməz
//AddEmployee metodu employees array-e employee obyekti
//əlavə etmək üçündür.

