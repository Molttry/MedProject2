using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace MedCenterProgram
{
    public class Menu
    {
        Nurse_Admin nurse_admin = new Nurse_Admin();
        Doctor doctor = new Doctor();
        List<Nurse_Admin> stufflist = new List<Nurse_Admin>();
        List<Doctor> doctorlist = new List<Doctor>();
        private void SaveDoctors()
        {
            using (var save = new FileStream(@"B:\VisualRepos\doctorlist.xml", FileMode.Create))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Doctor>));
                xml.Serialize(save, doctorlist);
            }

        }
        private void SaveStuff()
        {
            using (var save = new FileStream(@"B:\VisualRepos\baseofworkers.xml", FileMode.Create))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Nurse_Admin>));
                xml.Serialize(save, stufflist);
            }
        }
        public List<Doctor> DeserializationDoctor()
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<Doctor>));
            using (FileStream load = File.Open(@"B:\VisualRepos\doctorlist.xml", FileMode.Open))
            {
                doctorlist = (List<Doctor>)xml.Deserialize(load);
            }
            return doctorlist;
        }
        public List<Nurse_Admin> DeserializationStuff()
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<Nurse_Admin>));
            using (FileStream load = File.Open(@"B:\VisualRepos\baseofworkers.xml", FileMode.Open))
            {
                stufflist = (List<Nurse_Admin>)xml.Deserialize(load);
            }
            return stufflist;
        }
        public void AddAccount()
        {
            Console.WriteLine("Creating ne profile");
            Console.WriteLine("Choose who you want to create");
            Console.WriteLine("(A) admin, (D) doctor, (N) nurse");
            char input = Console.ReadKey(true).KeyChar;
            string type = "";
            if (Char.ToUpper(input) == 'D')
            {
                type = "doctor";
                Console.WriteLine("Write name:");
                string firstName = Console.ReadLine();
                Console.WriteLine("Write surname:");
                string lastName = Console.ReadLine();
                Console.WriteLine("Write username:");
                string username = Console.ReadLine();
                Console.WriteLine("Write password:");
                string password = Console.ReadLine();
                Console.WriteLine("Write type (urologist, cardiologist,neurologist,laryngologist):");
                string proffesion = Console.ReadLine();
                Console.WriteLine("Write pesel");
                string pesel = Console.ReadLine();
                Console.WriteLine("write pwz");
                string pwz = Console.ReadLine();
                Console.WriteLine("Account of doctor " + firstName + " created");
                doctorlist.Add(new Doctor() { firstName = firstName, lastName = lastName, type = type, pesel = pesel, password = password, username = username, duty = null, pwz = pwz, profession = proffesion });
                SaveDoctors();
            }

            if (Char.ToUpper(input) == 'A' || Char.ToUpper(input) == 'N')
            {
                if (Char.ToUpper(input) == 'A') type = "admin";
                if (Char.ToUpper(input) == 'N') type = "nurse";

                Console.WriteLine("Write name:");
                string firstName = Console.ReadLine();
                Console.WriteLine("Write surname:");
                string lastName = Console.ReadLine();
                Console.WriteLine("Write username:");
                string username = Console.ReadLine();
                Console.WriteLine("Write password:");
                string password = Console.ReadLine();
                Console.WriteLine("Write pesel");
                string pesel = Console.ReadLine();
                Console.WriteLine("Account of stuff member " + firstName + " created");
                stufflist.Add(new Nurse_Admin() { firstName = firstName, lastName = lastName, username = username, password = password, pesel = pesel, type = type, duty = null });
                SaveStuff();
            }
        }
        public void ShowList()
        {
            DeserializationDoctor();
            for (int i = 0; i < doctorlist.Count; i++)
            {
                Console.Write($"{doctorlist[i].firstName} {doctorlist[i].lastName} {doctorlist[i].type} {doctorlist[i].profession} duties: ");
                foreach (var date in doctorlist[i].duty)
                {
                    Console.Write($"{date.ToShortDateString()} ");
                }
                Console.WriteLine();
            }
            DeserializationStuff();
            for (int i = 0; i < stufflist.Count; i++)
            {
                Console.WriteLine($"{stufflist[i].firstName} {stufflist[i].lastName} {stufflist[i].type} duties: ");
                foreach (var date in stufflist[i].duty)
                {
                    Console.Write($"{date} ");
                }
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey(true);
        }
        public void AddDuty()
        {
            ShowList();
            Console.WriteLine("Enter first and last name to add duty");
            string input = Console.ReadLine();
            string[] parts = input.Split(" ");
            int DoctorId = 0;
            bool days = false, profession = false, number = false, name = false;
            for (int i = 0; i < doctorlist.Count; i++)
            {
                if (parts[0] == doctorlist[i].firstName && parts[1] == doctorlist[i].lastName)
                {
                    DoctorId = i;
                    name = true;
                }
            }
            if (name == true)
            {
                do
                {
                    Console.WriteLine("Enter date in format yyyy-mm-dd");
                    string date = Console.ReadLine();
                    var convertedDate = DateTime.Parse(date);

                    for (int i = 0; i < doctorlist.Count; i++)
                    {
                        foreach (DateTime duty in doctorlist[i].duty)
                        {
                            if (convertedDate != duty || (convertedDate == duty && doctorlist[DoctorId].profession != doctorlist[i].profession))
                            {
                                profession = true;
                            }
                        }
                    }
                    if (doctorlist[DoctorId].duty.Count() == 0) days = true;
                    foreach (var duty in doctorlist[DoctorId].duty)
                    {
                        if (convertedDate == duty || convertedDate == duty.AddDays(-1) || convertedDate == duty.AddDays(1))
                        {
                            days = false;
                            break;
                        }
                        else days =true;

                    }
                    if (doctorlist[DoctorId].duty.Count() < 10)
                    {
                        number = true;
                    }
                    if (profession == true && number == true && days == true)
                    {
                        doctorlist[DoctorId].duty.Add(convertedDate);
                        SaveDoctors();
                        Console.WriteLine($"If you want to exit you can press 'E', otherwise keep adding duty to {doctorlist[DoctorId].firstName} {doctorlist[DoctorId].lastName}");
                    }
                    else
                    {
                        Console.WriteLine("Couldn't book the date. Try one more time.");
                        Console.ReadKey();
                        break;
                    }
                } while (Console.ReadKey(true).Key != ConsoleKey.E);
            }
            else
            {
                Console.WriteLine("User doesn't exist");
  
            }

        } 
    }
}
