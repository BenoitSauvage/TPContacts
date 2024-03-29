﻿using System;
using System.Collections.Generic;
using Models.Classes;
using System.Data.SqlClient;

namespace DAL.DAO {
    public class DAOContact {
        private SqlConnection connection;
        const string TABLE_NAME = "Contact";
        long currentUser = 0;

        public DAOContact()
        {
            this.connection = new SqlConnection(connectionString: ConnectData.connectionString);
        }

        public DAOContact(long currentUser) {
            this.currentUser = currentUser;
            this.connection = new SqlConnection(connectionString: ConnectData.connectionString);
        }

        public void Create(Contact contact, long current_user) {
            this.connection.Open();

            string email = contact.Email != null ? "'" + contact.Email + "'" : "NULL";
            string phone = contact.Phone != null ? "'" + contact.Phone + "'" : "NULL";

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Contact(firstname, lastname, email, phone) OUTPUT Inserted.id " +
                "VALUES('" + contact.Firstname + "', '" + contact.Lastname + "', " + email + ", " + phone + ");";

            int new_contact = int.Parse(command.ExecuteScalar().ToString());

            command.CommandText = "INSERT INTO Contact_Book VALUES(" + current_user + ", " + new_contact + ");";
            SqlDataReader reader = command.ExecuteReader();

            this.connection.Close();
        }

        public void Update(Contact contact) {
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

        public void Remove(Contact contact) {
            this.connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Contact_Book " +
                "WHERE contact_id = " + contact.Id +
            ";";

            SqlDataReader reader = command.ExecuteReader();
            reader.Close();

            command.CommandText = "DELETE FROM " + TABLE_NAME + " " +
                "WHERE id = " + contact.Id +
            ";";

            reader = command.ExecuteReader();

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

        public Contact FindOneById(long? contact_id) {
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
            } else {
                Console.WriteLine(" ERROR  : CONTACT-ID IS NULL ");
                //TO DO THROW EXCEPTION
            }


            return contact;
        }

        public List<Contact> FindByIds(List<long> contact_id) {
            List<Contact> contact = new List<Contact>();


            SqlCommand command = connection.CreateCommand();
            string commandText = "SELECT id, firstname, lastname, email, phone FROM " + TABLE_NAME + " WHERE id = ";

            for (int i = 0; i< contact_id.Count; i++) {

                if (i != contact_id.Count - 1)
                    commandText += contact_id[i] + " OR ";
                else
                    commandText += contact_id[i] + ";";

            }

            this.connection.Open();
            command.CommandText = commandText;
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) {
                long id = reader.GetInt64(0);
                string firstname = reader.GetString(1);
                string lastname = reader.GetString(2);
                string email = !reader.IsDBNull(3) ? reader.GetString(3) : "NULL";
                string phone = !reader.IsDBNull(4) ? reader.GetString(4) : "NULL";

                contact.Add(new Contact(id, firstname, lastname, email, phone));
            }

            this.connection.Close();



            return contact;
        }

        public List<Contact> FindAll() {
            this.connection.Open();
            List<Contact> contacts = new List<Contact>();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM " + TABLE_NAME + ";";

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

        public List<Contact> FindAllForUser(long user_id)
        {
            this.connection.Open();
            List<Contact> contacts = new List<Contact>();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM " + TABLE_NAME + " c JOIN Contact_Book cb ON c.id = cb.contact_id " + 
                "WHERE cb.user_id = " + user_id + ";";

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

        public List<Contact> FindByName(string name) {
            List<Contact> contacts = new List<Contact>();
            this.connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM " + TABLE_NAME + " WHERE firstname LIKE '%" + name + "%';";

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

        public List<Contact> FindByLastname(string name)
        {
            List<Contact> contacts = new List<Contact>();
            this.connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM " + TABLE_NAME + " WHERE lastname LIKE '%" + name + "%';";

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

        public List<Contact> FindByPhone(string phone)
        {
            List<Contact> contacts = new List<Contact>();
            this.connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM " + TABLE_NAME + " WHERE phone LIKE '%" + phone + "%';";

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) {
                long id = reader.GetInt64(0);
                string firstname = reader.GetString(1);
                string lastname = reader.GetString(2);
                string mail = !reader.IsDBNull(3) ? reader.GetString(3) : "NULL";
                string cel = !reader.IsDBNull(4) ? reader.GetString(4) : "NULL";

                contacts.Add(new Contact(id, firstname, lastname, mail, cel));
            }

            this.connection.Close();

            return contacts;
        }

        public List<Contact> FindByMultiCriteria(string firstname, string lastname, string email, string phone)
        {
            List<Contact> contacts = new List<Contact>();
            this.connection.Open();

            string SQLQuery= "SELECT * FROM " + TABLE_NAME + " c JOIN Contact_Book cb ON c.id = cb.contact_id " +
                "WHERE cb.user_id = " + this.currentUser + " AND 1=1";

            if (firstname != "")
                SQLQuery += " AND firstname LIKE '%" + firstname + "%'";

            if (lastname != "")
                SQLQuery += " AND lastname LIKE '%" + lastname + "%'";

            if (email != "")
                SQLQuery += " AND email LIKE '%" + email + "%'";

            if (phone != "")
                SQLQuery += " AND phone LIKE '%" + phone + "%'";

            SQLQuery += ";";

            SqlCommand command = connection.CreateCommand();
            command.CommandText = SQLQuery;
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                long id = reader.GetInt64(0);
                string first = reader.GetString(1);
                string last = reader.GetString(2);
                string mail = !reader.IsDBNull(3) ? reader.GetString(3) : "NULL";
                string cel = !reader.IsDBNull(4) ? reader.GetString(4) : "NULL";

                contacts.Add(new Contact(id, first, last, mail, cel));
            }

            this.connection.Close();

            return contacts;
        }
    }
}