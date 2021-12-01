using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace MedCenterProgram
{
     abstract public class Users 
    {
        public string firstName;
        public string lastName;
        public string type;
        public string pesel;
        public string username;
        public string password;
        public List<DateTime> duty = new List<DateTime>();
    }
    public class Nurse_Admin : Users
    {
       
    }
    public class Doctor : Users
    {
        public string profession;
        public string pwz;
    }

}