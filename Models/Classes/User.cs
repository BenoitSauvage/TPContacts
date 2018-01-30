using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Classes {
    class User {

        public static bool IsConnect        = false;
        public static long? current_user_id = null;

        public long?    Id        { get; private set; }
        public string   Login     { get; private set; }
        public int      Password  { get; private set; }
        public string   Email     { get; private set; }

        public long[] ContactId { get; private set; }

        public User(string login, string password, string email) {
            this.Login      = login;
            this.Password   = Encrypt(password);
            this.Email      = email;
        }

        private int Encrypt(string password) {
            return password.GetHashCode();
        }


    }
}
