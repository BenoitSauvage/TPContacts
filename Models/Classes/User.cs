using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Classes {
    class User {

        public static long? current_user_id = null;

        public long?    Id        { get; private set; }
        public string   Login     { get; private set; }
        public int      Password  { get; private set; }

        public long?    Contact_Id       { get; private set; }
        public long[]   Contacts_Ids { get; private set; }

        public User(string login, string password) {
            this.Login      = login;
            this.Password   = Encrypt(password);
        }

        private int Encrypt(string password) {
            return password.GetHashCode();
        }

        private bool CheckPassword(string password) {
            return Encrypt(password) == Password;
        }

        public void Connect(string password) {
            if (CheckPassword(password)) {
                current_user_id = Id;
            } else { 
                // TO DO
                // THROW EXCEPTIONS
                }
        }

        public void Disconnect() {
            current_user_id = null;
        }



    }
}
