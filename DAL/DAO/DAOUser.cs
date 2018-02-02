using System;
using System.Collections.Generic;
using Models.Classes;
using System.Data.SqlClient;

namespace DAL.DAO {
    class DAOUser {
        private SqlConnection connection;
        const string TABLE_NAME = "User";
        const string TABLE_BOOKING = "Contact_Book";

        public DAOUser() {
            this.connection = new SqlConnection(connectionString: ConnectData.connectionString);
        }

        public void Create(User user, Contact contact) {
            this.connection.Open();

            string email = contact.Email != null ? "'" + contact.Email + "'" : "NULL";
            string phone = contact.Phone != null ? "'" + contact.Phone + "'" : "NULL";

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Contact(firstname, lastname, email, phone) " +
                " OUTPUT INSERTED.ID VALUES('" + contact.Firstname + "', '" + contact.Lastname + "', " + email + ", " + phone + ");";

            long contact_id = (long)command.ExecuteScalar();
            
            string  login       = user.Login;
            int     password    = user.Password;

            command = connection.CreateCommand();
            command.CommandText = "INSERT INTO \"User\"(login, password, contact_id) " +
                "VALUES('" + user.Login + "', '" + user.Password + "', " + contact_id + ");";

            SqlDataReader reader = command.ExecuteReader();

            this.connection.Close();

        }

        public void Update(User user) {
            this.connection.Open();

            string login    = user.Login != null ? "'" + user.Login + "'" : "NULL";
            string password = "'" + user.Password + "'";

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE \"" + TABLE_NAME + "\" SET " +
            "login = '" + user.Login + "', " +
            "password = '" + user.Password + "', " + 
            "WHERE id = " + user.Id + ";";

            SqlDataReader reader = command.ExecuteReader();

            this.connection.Close();
        }

        public void Remove(long user_id) {

            User user = FindOneById(user_id);

            this.connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM \"" + TABLE_NAME + "\" " +
                "WHERE id = " + user_id +
            ";";

            SqlDataReader reader = command.ExecuteReader();

            //command = connection.CreateCommand();

            //command.CommandText = "DELETE FROM Contact " +
            //    "WHERE id = " + user_id + ";";
            //reader = command.ExecuteReader();

            this.connection.Close();
        }

        public List<User> FindAll() {
            this.connection.Open();
            List<User> user = new List<User>();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM \"" + TABLE_NAME + "\";";

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) {
                long id = reader.GetInt64(0);
                string login = reader.GetString(1);
                int password = reader.GetInt32(2);
                long contact_id = reader.GetInt64(3);

                user.Add(new User(id, login, password, contact_id));
            }

            this.connection.Close();

            return user;

        }

        public User FindOneById(long? user_id) {
            User user = null;

            if (user_id != null) {

                this.connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT id, login, password, contact_id FROM \"" + TABLE_NAME + "\" WHERE id = " + user_id + ";";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) {
                    long id = reader.GetInt64(0);
                    string login = reader.GetString(1);
                    int password = reader.GetInt32(2);
                    long contact_id = reader.GetInt64(3);

                    user = new User(id, login, password, contact_id);
                }

                this.connection.Close();
            } else {
                Console.WriteLine(" ERROR  : CONTACT-ID IS NULL ");
                //TO DO THROW EXCEPTION
            }


            return user;
        }


        public void AddContactRelation(long user_id, long contact_id) {
            this.connection.Open();

            SqlCommand command = connection.CreateCommand();
                

            command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Contact_Book(user_id, contact_id) " +
                "VALUES('" + user_id + "', '" + contact_id + ");";

            SqlDataReader reader = command.ExecuteReader();

            this.connection.Close();

        }


        public void DeleteContactRelation(long user_id, long contact_id) {
            SqlCommand command = connection.CreateCommand();
            command = connection.CreateCommand();
            command.CommandText = "DELETE FROM \"" + TABLE_BOOKING  +
            "\" WHERE user_id = " + user_id + " AND contact_id = " + contact_id + ";";

            SqlDataReader reader = command.ExecuteReader();

            this.connection.Close();

        }


        //Renvoie un booleen qui indique si une tel connection exist deja ou non
        public bool DetectExistingRelation(long user_id, long contact_id) {
            bool retour = false;

            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM \"" + TABLE_BOOKING + "\" WHERE user_id LIKE " + user_id + " AND contact_id LIKE " + contact_id;
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) {
                retour = true;
            }

            connection.Close();

            return retour;
        }

        public List<Contact> GetAllConnectionForUser(long user_id) {
            List<Contact> contacts = new List<Contact>();
            List<long> contact_ids = new List<long>();
            DAOContact cDAO = new DAOContact();

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT contact_id FROM \"" + TABLE_BOOKING + "\" WHERE user_id = " + user_id +";";
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) {
                contact_ids.Add(reader.GetInt64(0));
            }

            connection.Close();

            return cDAO.FindByIds(contact_ids);
        }

        public List<User> GetAllUsers() {
            this.connection.Open();
            List<User> users = new List<User>();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM \"" + TABLE_NAME + "\";";

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) {
                long id = reader.GetInt64(0);
                string login = reader.GetString(1);
                int password= reader.GetInt32(2);
                long contact_id = reader.GetInt64(3);

                users.Add(new User(id, login, password, contact_id));
            }

            this.connection.Close();

            return users;
        }

    }
}
