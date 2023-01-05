using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    internal class Department : IDepartment
    {
        public string Name;
        public int EmployeeLimit = 2;
        public double SalaryLimit = 2000;
        private Employee[] _employees = { };
        public Employee[] Employees => _employees;

        public void AddEmployee(Employee newEmployee)
        {
            if (CheckEmployeeLimit())
            {
                Array.Resize(ref _employees, _employees.Length + 1);
                _employees[_employees.Length - 1] = newEmployee;
            }
            else
            {
                try
                {
                    throw new Exception(" Isci limiti dolub! Isci elave etmek mumkun deyil");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\n{e.Message}\n");
                }
            }
        }

        public Employee CreateEmployee()
        {
            string fullName;
            string salaryStr;
            double salary;
            string startDateStr;
            DateTime startDate;

            do
            {
                Console.Write("\n Employee'nin adini daxil edin: ");
                fullName = Console.ReadLine();

            } while (!Employee.CheckFullName(fullName));

            do
            {
                Console.WriteLine($"\n Employee'nin maasini daxil edin. Maas max {SalaryLimit - CalculateCurrentAllSalary()} Teyin oluna biler!");
                Console.Write(" Yalniz reqemler: ");
                salaryStr = Console.ReadLine();

            } while (!double.TryParse(salaryStr, out salary) || !CheckSettingSalary(salary));

            do
            {
                Console.WriteLine("\n Employee'nin baslama tarixini yazin!");
                Console.Write(" Format ay/gun/il saat:deqiqe:saniye seklinde olmalidir ve reqem daxil edin: ");
                startDateStr = Console.ReadLine();

            } while (!DateTime.TryParse(startDateStr, out startDate));

            Employee newEmployee = new Employee
            {
                FullName = fullName,
                Salary = salary,
                Startdate = startDate
            };

            return newEmployee;
        }

        public bool RemoveEmployee(string noStr)
        {
            bool hasFound = false;

            if (int.TryParse(noStr, out int no))
            {
                if (_employees.Length > 1)
                {
                    if (_employees.Length == no)
                    {
                        Array.Resize(ref _employees, _employees.Length - 1);
                        return true;
                    }

                    for (int i = no - 1; i < _employees.Length - 1; i++)
                    {
                        _employees[i] = _employees[i + 1];
                        hasFound = true;
                    }

                    if (hasFound)
                    {
                        Array.Resize(ref _employees, _employees.Length - 1);
                        return true;
                    }
                    else
                    {
                        try
                        {
                            throw new Exception(" Bu nomrede employee yoxdur");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"\n{e.Message}\n");
                            return false;
                        }
                    }
                }

                else if (_employees.Length == 1)
                {
                    if (no > 1 && no < 0)
                    {
                        try
                        {
                            throw new Exception(" Sirketde 1 eded isci var");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"\n{e.Message}\n");
                            return false;
                        }
                    }
                    else
                    {
                        Array.Resize(ref _employees, _employees.Length - 1);
                        return true;
                    }
                }
                try
                { 
                    throw new Exception(" Sirkette isci yoxdur");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\n{e.Message}\n");
                    return false;
                }
            }
            else
            {
                try
                {
                    throw new Exception(" Yalniz reqem daxil edin");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\n{e.Message}\n");
                    return false;
                }
            }
        }

        public bool GetEmployeesByName(string search)
        {
            search = search.ToLower();

            if (!string.IsNullOrWhiteSpace(search))
            {
                for (int i = 0; i < search.Length; i++)
                {
                    if (char.IsDigit(search[i]) || char.IsSymbol(search[i]))
                    {
                        try
                        {
                            throw new Exception(" Bas herfden sonrakilar kicik herf olmalidir! ");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"\n{e.Message}\n");
                            return false;
                        }
                    }
                }

                for (int i = 0; i < _employees.Length; i++)
                {
                    if (_employees[i].FullName.ToLower().Contains(search))
                    {
                        ShowInfo(_employees[i]);
                        return true;
                    }
                }
                return true;
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

        public bool GetEmployeeByNo(string noStr)
        {
            if (int.TryParse(noStr, out int no))
            {
                for (int i = 0; i < _employees.Length; i++)
                {
                    if (_employees[i].No == no)
                    {
                        ShowInfo(_employees[i]);
                        return true;
                    }
                }
                try
                {
                    throw new Exception(" Bu nomrede employee yoxdur");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\n{e.Message}\n");
                    return false;
                }
            }
            else
            {
                try
                {
                    throw new Exception(" Yalniz reqem daxil edin");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\n{e.Message}\n");
                    return false;
                }
            }
        }

        public void GetEmployeesByDate(DateTime startDate, DateTime endDate)//default " =DateTime.Now " qoyanda error cixir mellim hell ede bilmedim
        {
            bool hasFound = false;

            for (int i = 0; i < _employees.Length; i++)
            {
                if (_employees[i].Startdate >= startDate && _employees[i].Startdate <= endDate)
                {
                    hasFound = true;
                    ShowInfo(_employees[i]);
                }

                if (!hasFound)
                {
                    try
                    {
                        throw new Exception(" Axtardiginiz tarixde istifadeci yoxdur");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"\n{e.Message}\n");
                    }
                }
            }
        }

        public void ShowInfo(Employee employee)
        {
            Console.WriteLine($"\n Fullname: {employee.FullName} - Maasi: {employee.Salary} - Nomresi: {employee.No} - Baslama tarixi: {employee.Startdate}\n");
        }

        public bool UptadeSalary(int no, double newSalary)
        {
            Employee wantedEmp = new Employee();

            do
            {
                for (int i = 0; i < _employees.Length; i++)
                {
                    if (_employees[i].No == no)
                    {
                        wantedEmp = _employees[i];
                        break;
                    }
                }

                if (wantedEmp == null)
                {
                    try
                    {
                        throw new Exception(" Bu nomrede employee yoxdur");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"\n{e.Message}\n");
                        return false;
                    }
                }
            } while (wantedEmp == null);

            if (CheckSalaryLimit(newSalary, wantedEmp.Salary))
                wantedEmp.Salary = newSalary;

            return true;
        }

        public bool CheckSalaryLimit(double newSalary, double employeeSalary)
        {

            if (!(newSalary >= 250))
            {
                try
                {
                    throw new Exception(" Maas 250 den yuxari olmalidir");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\n{e.Message}\n");
                    return false;
                }
            }

            else
            {
                if ((SalaryLimit - CalculateCurrentAllSalary()) >= (newSalary - employeeSalary))
                    return true;

                else
                {
                    try
                    {
                        throw new Exception($" Maas max {SalaryLimit - CalculateCurrentAllSalary()} Teyin oluna biler");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"\n{e.Message}\n");
                        return false;
                    }
                }
            }
        }

        public bool CheckSettingSalary(double salary)
        {
            if (SalaryLimit - CalculateCurrentAllSalary() >= salary)
            {
                if (salary >= 250)
                    return true;

                else
                {

                    try
                    {
                        throw new Exception(" Maas 250 den az ola bilmez!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"\n{e.Message}\n");
                        return false;
                    }
                }
            }
            else
            {

                try
                {
                    throw new Exception(" Maas limiti asir");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\n{e.Message}\n");
                    return false;
                }
            }
        }

        public double CalculateCurrentAllSalary()
        {
            double allSalary = 0;

            for (int i = 0; i < _employees.Length; i++)
            {
                allSalary += _employees[i].Salary;
            }
            return allSalary;
        }

        public bool CheckEmployeeLimit()
        {
            if (_employees.Length < EmployeeLimit)
                return true;
            else
                try
                {
                    throw new Exception(" Isci limiti dolmusdur!");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\n{e.Message}\n");
                    return false;
                }
        }

        public void ShowAllEmployees()
        {
            for (int i = 0; i < _employees.Length; i++)
            {
                ShowInfo(_employees[i]);
            }
        }
    }
}