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
                Console.WriteLine("\n 1. Isci elave et\n");
                Console.WriteLine(" 2. Iscinin maasini deyisdir\n");
                Console.WriteLine(" 3. Iscilere tarixe gore bax\n");
                Console.WriteLine(" 4. Iscilere nomreye gore bax\n");
                Console.WriteLine(" 5. Iscilerin hamisina bax\n");
                Console.WriteLine(" 6. Iscini nomresine gore sil\n");
                Console.WriteLine(" 7. Iscinin adina gore axtaris et\n");
                Console.WriteLine(" 0. Menyudan cix\n");
                numStr = Console.ReadLine();
                string noStr;
                int no;
                string salaryStr;
                double newSalary;

                
                switch (numStr)
                {
                    case "1":
                        balvini.AddEmployee(balvini.CreateEmployee());
                        break;

                    case "2":
                        do
                        {
                            do
                            {

                                Console.Write("\n Iscinin nomresini daxil edin (Yalniz reqem daxil edin): ");
                                noStr = Console.ReadLine();

                            } while (!int.TryParse(noStr, out no));

                            do
                            {
                                Console.Write("\n Yeni maasi daxil edin (Yalniz reqem daxil edin): ");
                                salaryStr = Console.ReadLine();

                            } while (!double.TryParse(salaryStr,out newSalary));

                        } while (!balvini.UptadeSalary(no, newSalary));

                        break;

                    case "3":
                        do
                        {

                            Console.Write("\n Employee'nin baslama tarixini yazin! ");
                            Console.Write("\n Format ay/gun/il seklinde olmalidir ve yalniz reqem daxil edin: ");
                            startDateStr = Console.ReadLine();
                            Console.Write("\n Son tarixini yazin: ");
                            endDateStr = Console.ReadLine();

                        } while (!DateTime.TryParse(startDateStr, out startDate) || !DateTime.TryParse(endDateStr, out endDate));

                        balvini.GetEmployeesByDate(startDate, endDate);
                        break;

                    case "4":
                        string search;
                        do
                        {
                            Console.Write("\n Axtaris etmek istediyiniz deyeri daxil edin: ");
                            search = Console.ReadLine();

                        } while (!balvini.GetEmployeeByNo(search));

                        break;

                    case "5":
                        balvini.ShowAllEmployees();
                        break;


                    case "6":
                        string employeeNoStr;
                        do
                        {
                            Console.Write("\n Silmek istediyiniz employeenin nomresini daxil edin: ");
                            employeeNoStr = Console.ReadLine();

                        } while (!balvini.RemoveEmployee(employeeNoStr));
                      
                        break;

                    case "7":
                        string employeeName;
                        do
                        {
                            Console.Write("\n Axtarmaq istediyiniz employeenin adini daxil edin: ");
                            employeeName = Console.ReadLine();

                        } while (!balvini.GetEmployeesByName(employeeName));
                      
                        break;
                }

            } while (numStr != "0");
        }
    }
}