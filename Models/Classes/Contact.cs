using System;

namespace Models.Classes
{
    public class Contact
    {
        public readonly long? Id;
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Contact(long id, string firstname, string lastname, string email = null, string phone = null) 
        {
            this.Id = id;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Email = email;
            this.Phone = phone;
        }

        public Contact(string firstname, string lastname, string email = null, string phone = null)
        {
            this.Id = null;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Email = email;
            this.Phone = phone;
        }

        public void Print()
        {
            Console.WriteLine("ID : {0}", this.Id);
            Console.WriteLine("FIRSTNAME : {0}", this.Firstname);
            Console.WriteLine("LASTNAME : {0}", this.Lastname);
            Console.WriteLine("EMAIL : {0}", this.Email);
            Console.WriteLine("PHONE : {0}", this.Phone);

            Console.WriteLine();
        }
    }
}
