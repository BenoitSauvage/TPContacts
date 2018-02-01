using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Classes {
    public class User {

        public static long? current_user_id = null;

        public long? Id { get; private set; }
        public string Login { get; private set; }
        public int Password { get; private set; }

        public long? Contact_Id { get; private set; }
        public List<long> Contacts_Ids { get; private set; }

        public User(string login, string password) {
            this.Login = login;
            this.Password = Encrypt(password);
        }

        public User(long id, string login, int password, long contact_id) {
            Id = id;
            Login = login;
            Password = password;
            Contact_Id = contact_id;
        }

        private int Encrypt(string password) {
            return password.GetHashCode();
        }

        private bool CheckPassword(string password) {
            return Encrypt(password) == Password;
        }

        public void AddContact(long contact_id) {
            Contacts_Ids.Add(contact_id);
        }

        public void Print() {
            Console.WriteLine();
            Console.WriteLine("Mon Login : {0}", this.Login);
            Console.WriteLine("Id : {0}", this.Id);
            Console.WriteLine("encrypted password : {0}", this.Password);



        }

        public byte Connect(string login, string password) {
            //Code d'érreurs
            // 0 = login && Password incorect
            // 1 = password incorect
            // 2 = login incorect
            // 3 = Connection réussit

            byte retour = 0;

            if (login == Login)
                retour += 1;
            if (CheckPassword(password))
                retour += 2;
            if (retour == 3)
                current_user_id = Id;

            return retour;
        }

        public void Disconnect() {
            current_user_id = null;
        }



    }
}
