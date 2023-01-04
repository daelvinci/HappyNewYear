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
        public int EmployeeLimit;
        public double SalaryLimit;
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
                    throw new Exception("Isci limiti dolub! Isci elave etmek mumkun deyil");
                }
                catch (Exception)
                {
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
                Console.Write("Employee'nin adini daxil edin: ");
                fullName = Console.ReadLine();

            } while (!Employee.CheckFullName(fullName));

            do
            {
                Console.Write("Employee'nin maasini daxil edin (Yalniz reqemler ola biler!) : ");
                salaryStr = Console.ReadLine();

            } while (!double.TryParse(salaryStr, out salary) || !CheckSalary(salary));


            do
            {

                Console.WriteLine("Format ay/gun/il seklinde olmalidir ve reqem daxil edin");
                Console.WriteLine("Employee'nin baslama tarixini yazin");
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
            int no = 0;
            bool hasFound = false;
            if (CheckNo(noStr, no))
            {
                for (int i = no; i < _employees.Length - 1; i++)
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
                        throw new Exception("Bu nomrede employee yoxdur");
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
                    throw new Exception("Yalniz reqem daxil edin");
                }
                catch (Exception)
                {
                    return false;
                }
            }




        }

        public bool SearchByName(string search)
        {
            if (!string.IsNullOrWhiteSpace(search))
            {
                for (int i = 0; i < search.Length; i++)
                {

                    if (char.IsDigit(search[i]) || char.IsSymbol(search[i]))
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
                    throw new Exception("Herf daxil edin");
                }
                catch (Exception)
                {
                    return false;
                }
            }

        }
        public void FindEmployeeByNo(string noStr)
        {
            int no = 0;

            if (CheckNo(noStr, no))
            {
                for (int i = 0; i < _employees.Length; i++)
                {
                    if (_employees[i].No == no)
                    {
                        ShowInfo(_employees[i]);
                    }

                }
                try
                {
                    throw new Exception("Bu nomrede employee yoxdur");
                }
                catch (Exception)
                {
                }
            }


        }

        public void GetEmployeesByDate(DateTime startDate, DateTime endDate)//default now olmalidi 2ci parametrde
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
                        throw new Exception("Bu tarixde employee yoxdur");
                    }
                    catch (Exception)
                    {

                    }

                }
            }
        }
        public void ShowInfo(Employee employee)
        {
            Console.WriteLine($"Fullname: {employee.FullName} - Maasi: {employee.Salary} - Nomresi: {employee.No} - Baslama tarixi: {employee.Startdate}");
        }


        public bool UptadeSalary(string noStr, string salaryStr)
        {
            int no = 0;
            double newSalary = 0;
            Employee wantedEmp = new Employee();

            if (CheckNo(noStr, no) && CheckNo(salaryStr, newSalary))
            {

                do
                {
                    for (int i = 0; i < _employees.Length; i++)
                    {
                        if (_employees[i].No == no)
                        {
                            wantedEmp = _employees[i];

                        }
                    }
                    if (wantedEmp == null)
                    {
                        try
                        {
                            throw new Exception("Bu nomrede employee yoxdur");
                        }
                        catch (Exception)
                        {
                            return false;
                        };
                    }

                } while (!CheckSalary(wantedEmp.Salary));

                wantedEmp.Salary = newSalary;
                return true;
            }
            else
            {

                try
                {
                    throw new Exception("Yalniz reqem daxil edin");
                }
                catch (Exception)
                {
                    return false;
                };
            }

        }


        public bool CheckSalary(double newSalary)
        {
            if (newSalary! >= 250)
            {
                try
                {
                    throw new Exception("Maas 250 den yuxari olmalidir");
                }
                catch (Exception)
                {
                    return false;
                }
            }

            double salaryLimit = 0;

            salaryLimit += newSalary + CalculateCurrentAllSalary();

            if (salaryLimit <= SalaryLimit)
                return true;
            else
            {
                try
                {
                    throw new Exception($"Maas max {SalaryLimit - CalculateCurrentAllSalary()} Teyin oluna biler");
                }
                catch (Exception)
                {
                    return false;
                }
            }

        }

        public double CalculateCurrentAllSalary()
        {
            double salaryLimit = 0;

            for (int i = 0; i < Employees.Length; i++)
            {
                salaryLimit += Employees[i].Salary;
            }
            return salaryLimit;
        }

        public bool CheckNo(string noStr, int no)
        {
            if (int.TryParse(noStr,out no))
            {
                return true;
            }

            else
            {

                try
                {
                    throw new Exception("Yalniz reqem daxil edin");
                }
                catch (Exception)
                {
                    return false;
                };
            }
        }
        public bool CheckNo(string salaryStr, double salary)
        {
            if (double.TryParse(salaryStr, out salary))
            {
                return true;
            }

            else
            {

                try
                {
                    throw new Exception("Yalniz reqem daxil edin");
                }
                catch (Exception)
                {
                    return false;
                };
            }
        }
        public bool CheckEmployeeLimit()
        {
            if (_employees.Length < EmployeeLimit)
                return true;
            else
                return false;
        }

    }
}//Her employe elave eliyende yoxla gor salary limit ve employee limiti asir mi

//EmployeeFullName yalnız hərflərdən ibarət olmalıdır və Böyük hərflə başlamalıdır, ozun kapitilayz eliye bilersen bas herflerini
//Salary dəyəri 250-dən aşağı ola bilməz

//1.Do while
//2. try catch
//3. menyu duzeltmek



//Employee class
// -No employe yarananda ozu nomre versin.
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
// - AddEmployee() Add employeda (start date) olsun 
// - RemoveEmployee(no)
// - GetEmployeesByDates(fromDate, toDate) GetEmployeByDate()de 2ci parametr optionaldi, default now'du
// - FindEmployeeByNo(no)
// - UpdateSalary(no, newSalary)
// - Search()