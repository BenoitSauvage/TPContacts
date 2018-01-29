using System;
using System.Collections.Generic;
using Models.Classes;
using System.Data.SqlClient;

namespace DAL.DAO
{
    public class DAOContact
    {
        private SqlConnection connection;
        const string TABLE_NAME = "Contact";

        public DAOContact()
        {
            this.connection = new SqlConnection( connectionString: ConnectData.connectionString );
        }

        public void Create(Contact contact)
        {
            this.connection.Open();

            string email = contact.Email != null ? "'" + contact.Email + "'" : "NULL";
            string phone = contact.Phone != null ? "'" + contact.Phone + "'" : "NULL";

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Contact(firstname, lastname, email, phone) " +
                "VALUES('" + contact.Firstname + "', '" + contact.Lastname + "', " + email + ", " + phone + ");";

            SqlDataReader reader = command.ExecuteReader();

            this.connection.Close();
        }

        public void Update(Contact contact)
        {
            this.connection.Open();

            string email = contact.Email != null ? "'" + contact.Email + "'" : "NULL";
            string phone = contact.Phone != null ? "'" + contact.Phone + "'" : "NULL";

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE " + TABLE_NAME + " SET " +
                "firstname = '" + contact.Firstname + "', " +
                "lastname = '" + contact.Lastname + "', " +
                "email = " + email + ", " +
                "phone = " + phone + " " +
                "WHERE id = " + contact.Id + ";";

            SqlDataReader reader = command.ExecuteReader();

            this.connection.Close();
        }

        public void Remove(Contact contact)
        {
            this.connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM " + TABLE_NAME + " " +
                "WHERE id = " + contact.Id +
            ";";

            SqlDataReader reader = command.ExecuteReader();

            this.connection.Close();
        }

        public void Remove(long contact_id) {
            this.connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM " + TABLE_NAME + " " +
                "WHERE id = " + contact_id +
            ";";

            SqlDataReader reader = command.ExecuteReader();

            this.connection.Close();
        }

        public Contact FindOneById(long? contact_id)
        {
            Contact contact = null;

            if (contact_id != null) {
                this.connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT id, firstname, lastname, email, phone FROM " + TABLE_NAME + " WHERE id = " + contact_id + ";";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) {
                    long id = reader.GetInt64(0);
                    string firstname = reader.GetString(1);
                    string lastname = reader.GetString(2);
                    string email = !reader.IsDBNull(3) ? reader.GetString(3) : "NULL";
                    string phone = !reader.IsDBNull(4) ? reader.GetString(4) : "NULL";

                    contact = new Contact(id, firstname, lastname, email, phone);
                }

                this.connection.Close();
            }
            else {
                Console.WriteLine(" ERROR  : CONTACT-ID IS NULL ");
                //TO DO THROW EXCEPTION
            }


            return contact;
        }

        public List<Contact> FindAll()
        {
            this.connection.Open();
            List<Contact> contacts = new List<Contact>();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM " + TABLE_NAME + ";";

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                long id = reader.GetInt64(0);
                string firstname = reader.GetString(1);
                string lastname = reader.GetString(2);
                string email = !reader.IsDBNull(3) ? reader.GetString(3) : "NULL";
                string phone = !reader.IsDBNull(4) ? reader.GetString(4) : "NULL";

                contacts.Add(new Contact(id, firstname, lastname, email, phone));
            }

            this.connection.Close();

            return contacts;
        }

        public List<Contact> FindByName(string name) 
            {
            List<Contact> contacts = new List<Contact>();
            this.connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM " + TABLE_NAME + " WHERE firstname LIKE '%"+name+"%' OR lastname LIKE '%"+name+"%';";

            SqlDataReader reader = command.ExecuteReader();


            while (reader.Read()) {
                long id = reader.GetInt64(0);
                string firstname = reader.GetString(1);
                string lastname = reader.GetString(2);
                string email = !reader.IsDBNull(3) ? reader.GetString(3) : "NULL";
                string phone = !reader.IsDBNull(4) ? reader.GetString(4) : "NULL";

                contacts.Add(new Contact(id, firstname, lastname, email, phone));
            }

            this.connection.Close();


            return contacts;
        }

        public List<Contact> FindByEmail(string email) {
            List<Contact> contacts = new List<Contact>();
            this.connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM " + TABLE_NAME + " WHERE email LIKE '%" + email + "%';";

            SqlDataReader reader = command.ExecuteReader();


            while (reader.Read()) {
                long id = reader.GetInt64(0);
                string firstname = reader.GetString(1);
                string lastname = reader.GetString(2);
                string mail = !reader.IsDBNull(3) ? reader.GetString(3) : "NULL";
                string phone = !reader.IsDBNull(4) ? reader.GetString(4) : "NULL";

                contacts.Add(new Contact(id, firstname, lastname, mail, phone));
            }

            this.connection.Close();


            return contacts;
        }

    }
}
