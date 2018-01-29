﻿using System;
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
            this.connection = new SqlConnection(
                connectionString: @"Data Source=BENOIT\SQLEXPRESS;Initial Catalog=tpcontact;
                            Integrated Security=True;Connect Timeout=5;"
            );
        }

        public void Create(Contact contact)
        {
            this.connection.Open();

            string email = contact.Email != null ? "'" + contact.Email + "'" : "NULL";
            string phone = contact.Phone != null ? "'" + contact.Phone + "'" : "NULL";

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Contact(firstname, lastname, email, phone) " +
                "VALUES('" + contact.Firstname + "', '" + contact.Lastname + "', " + email + ", " + phone + ");"
            ;

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
                "WHERE id = " + contact.Id +
            ";";

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

        public Contact FindOneById(int contact_id)
        {
            this.connection.Open();
            Contact contact = null;

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT id, firstname, lastname, email, phone FROM " + TABLE_NAME + " WHERE id = " + contact_id + ";";

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                long id = reader.GetInt64(0);
                string firstname = reader.GetString(1);
                string lastname = reader.GetString(2);
                string email = !reader.IsDBNull(3) ? reader.GetString(3) : "NULL";
                string phone = !reader.IsDBNull(4) ? reader.GetString(4) : "NULL";

                contact = new Contact(id, firstname, lastname, email, phone);
            }

            this.connection.Close();

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
    }
}
