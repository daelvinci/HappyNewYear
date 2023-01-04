using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal interface IDepartment
    {
        Employee[] Employees { get; }
        void AddEmployee(Employee newEmployee);
        void GetEmployeesByDate(DateTime startDate, DateTime endDate);
        bool RemoveEmployee(string noStr);
        bool SearchByName(string search);
    }
}
