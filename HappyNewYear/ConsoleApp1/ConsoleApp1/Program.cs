using System;
using System.Xml.Linq;

namespace ConsoleApp1
{
    internal class Program
    {

        static void Main(string[] args)
        {
            string startDateStr;
            string endDateStr;
            DateTime startDate;
            DateTime endDate;
            string numStr;
            Department balvini = new Department();


            do
            {

                Console.WriteLine(" 1. Isci elave et");
                Console.WriteLine(" 2. Iscinin maasini deyisdir");
                Console.WriteLine(" 3. Tarixe gore iscilere bax");
                Console.WriteLine(" 4. Nomreye gore iscilere bax");
                Console.WriteLine(" 5. Butun iscilere bax");
                Console.WriteLine(" 6. nomresine gore iscini sil");
                Console.WriteLine(" 7. Adina gore axtaris et");
                Console.WriteLine("0. Menyudan cix");
                numStr = Console.ReadLine();
                string noStr;
                string salaryStr;

                switch (numStr)
                {
                    case "1":
                        balvini.AddEmployee(balvini.CreateEmployee());
                        break;

                    case "2":
                        do
                        {

                            Console.WriteLine("Iscinin nomresini daxil edin");
                            noStr = Console.ReadLine();

                            Console.WriteLine("Yeni maasi daxil edin");
                            salaryStr = Console.ReadLine();
                            balvini.UptadeSalary(noStr, salaryStr);


                        } while (!balvini.UptadeSalary(noStr, salaryStr));
                        break;

                    case "3":

                        do
                        {
                            do
                            {

                                Console.WriteLine("Format ay/gun/il seklinde olmalidir ve reqem daxil edin");
                                Console.WriteLine("Employee'nin baslama tarixini yazin");
                                startDateStr = Console.ReadLine();
                                Console.WriteLine("Son tarixini yazin");
                                endDateStr = Console.ReadLine();

                            } while (!DateTime.TryParse(startDateStr, out startDate) || !DateTime.TryParse(endDateStr, out endDate));

                            balvini.GetEmployeesByDate()

                        } while (true);





                }


            } while (numStr != "0");


        }
    }
}//Her employe elave eliyende yoxla gor salary limit ve employee limiti asir mi
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

//Console.WriteLine("Hello World!");
//Department balvini = new Department();

//balvini.AddEmployee(balvini.CreateEmployee());