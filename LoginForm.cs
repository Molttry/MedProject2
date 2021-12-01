using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MedCenterProgram
{
    class LoginForm
    {
        Nurse_Admin nurse_admin = new Nurse_Admin();
        Doctor doctor = new Doctor();
        Menu functions = new Menu();
        List<Nurse_Admin> stufflistlogin = new List<Nurse_Admin>();
        List<Doctor> doctorlistlogin = new List<Doctor>();
        string type;
        public void Welcoming()
        {
            Console.WriteLine("             Welcome to the MedSystem            ");
            Console.WriteLine("Write your username:");
            string login = Console.ReadLine();
            Console.WriteLine("Write your password:");
            string password = Console.ReadLine();
            Console.WriteLine("   Choose your catrgory(Doctor(D),Nurse(N),Admin(A)      ");
            char convertedInput = Console.ReadKey(true).KeyChar;

            switch ((Char.ToUpper(convertedInput)))
            {
                case 'D':
                    type = "Doctor";
                    checkLoginData(login, password, type);
                    break;
                case 'A':
                    type = "Admin";
                    checkLoginData(login, password, type);
                    break;
                case 'N':
                    type = "Nurse";
                    checkLoginData(login, password, type);
                    break;
                default:
                    Console.WriteLine("Try one more time");
                    Environment.Exit(0);
                    break;
            }


        }
        private void checkLoginData(string login, string password, string type)
        {
            int i = 0;
            if (type == "Doctor")
            {
                doctorlistlogin = functions.DeserializationDoctor();
                for (int j=0; j< doctorlistlogin.Count; j++)
                {
                    if (doctorlistlogin[j].username == login) i++;
                    if (doctorlistlogin[j].password == password) i++;
                    i++;
                }
            }else 
            {
                stufflistlogin = functions.DeserializationStuff();
                for (int j = 0; j < stufflistlogin.Count; j++)
                {
                   if (stufflistlogin[j].username == login) i++ ;
                   if (stufflistlogin[j].password == password) i++;
                }
            }
            if (i >= 2)
            {
                Console.WriteLine("Login Seccessfull!");
                Console.ReadKey(true);
                menu(type);
            } 
            if (i < 2 )
            {
                Console.WriteLine("Login Failed");
                Console.ReadKey();
                Environment.Exit(0);
            }

        }
        private void menu(string Choice)
        {
            string Input;
            Console.Clear();
            Console.WriteLine("             Good day!            ");
            Console.WriteLine("Choose the option that you want by entering number next to it");
            Console.WriteLine(type);
            if (Choice == "Doctor" || Choice == "Nurse")
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("1. See list of staff");
                    Console.WriteLine("2. Exit");
                    Input = Console.ReadLine();
                    switch (Input)
                    {
                        case "1":
                            functions.ShowList();
                            break;
                        case "2":
                            Environment.Exit(0);
                            break;

                    }
                } 
            }
            else
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("1. See list of staff");
                    Console.WriteLine("2. Set a duty");
                    Console.WriteLine("3. Create Account");
                    Console.WriteLine("4. Delete Account");
                    Console.WriteLine("5. Exit");
                    Input = Console.ReadLine();
                    switch (Input)
                    {
                        case "1":
                            functions.ShowList();
                            break;
                        case "2":
                            functions.AddDuty();
                            break;
                        case "3":
                            functions.AddAccount();
                            break;
                        case "4":
                            functions.ShowList();
                            break;
                        case "5":
                            Environment.Exit(0);
                            break;

                    }
                } 
            }
        }
    }


}
