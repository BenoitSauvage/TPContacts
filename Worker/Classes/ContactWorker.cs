﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using DAL.DAO;
using Models.Classes;

namespace Worker.Classes
{
    public class ContactWorker
    {
        const string FIRSTNAME = "Firstname";
        const string LASTNAME = "Lastname";

        const int INDEX_FIRSTNAME = 0;
        const int INDEX_LASTNAME = 1;
        const int INDEX_EMAIL = 2;
        const int INDEX_PHONE = 3;

        static List<Contact> contacts = new List<Contact>();

        public static List<Contact> GetAll()
        {
            contacts = new DAOContact().FindAll();
            return contacts;
        }

        public static List<Contact> GetAllForUser(long currentUserId)
        {
            contacts = new DAOContact().FindAllForUser(currentUserId);
            return contacts;
        }

        public static List<string> ValidateContact(Dictionary<string, string> contact)
        {
            List<string> errors = new List<string>();

            contact.TryGetValue("firstname", out string firstname);
            contact.TryGetValue("lastname", out string lastname);
            contact.TryGetValue("email", out string email);
            contact.TryGetValue("phone", out string phone);

            if (firstname == null || firstname == "")
                errors.Add("Le champ 'firstname' est obligatoire !");

            if (lastname == null || lastname == "")
                errors.Add("Le champ 'lastname' est obligatoire !");

            if (phone != null && phone != "")
            {
                Regex phone_regex = new Regex(@"^([0-9]{3})[-.]?([0-9]{3})[-.]?([0-9]{4})$");
                if (!phone_regex.IsMatch(phone))
                    errors.Add("Le format du telephone n'est pas sous la forme XXX-XXX-XXXX");
            }

            if (email != null && email != "")
            {
                Regex email_regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                if (!email_regex.IsMatch(email))
                    errors.Add("L'email a un format invalide");
            }

            return errors;
        }

        public static void AddContact(Dictionary<string, string> c, long current_user)
        {
            DAOContact dao = new DAOContact();

            c.TryGetValue("firstname", out string firstname);
            c.TryGetValue("lastname", out string lastname);
            c.TryGetValue("email", out string email);
            c.TryGetValue("phone", out string phone);

            phone = phone == "" ? null : phone;
            email = email == "" ? null : email;

            Contact contact = new Contact(firstname, lastname, email, phone);

            dao.Create(contact, current_user);
        }

        public static void UpdateContact(Dictionary<string, string> c)
        {
            DAOContact dao = new DAOContact();

            c.TryGetValue("id", out string id);
            c.TryGetValue("firstname", out string firstname);
            c.TryGetValue("lastname", out string lastname);
            c.TryGetValue("email", out string email);
            c.TryGetValue("phone", out string phone);

            phone = phone == "" ? null : phone;
            email = email == "" ? null : email;

            Contact contact = new Contact(long.Parse(id),  firstname, lastname, email, phone);

            dao.Update(contact);
        }

        public static void RemoveContact(Dictionary<string, string> c)
        {
            DAOContact dao = new DAOContact();

            c.TryGetValue("id", out string id);
            c.TryGetValue("firstname", out string firstname);
            c.TryGetValue("lastname", out string lastname);
            c.TryGetValue("email", out string email);
            c.TryGetValue("phone", out string phone);

            phone = phone == "" ? null : phone;
            email = email == "" ? null : email;

            Contact contact = new Contact(long.Parse(id), firstname, lastname, email, phone);

            dao.Remove(contact);
        }

        public static List<Contact> Sort(string column)
        {
            if (column == FIRSTNAME)
                contacts.Sort((a, b) => string.Compare(a.Firstname, b.Firstname));

            if (column == LASTNAME)
                contacts.Sort((a, b) => string.Compare(a.Lastname, b.Lastname));

            return contacts;
        }

        public static List<Contact> Search(Dictionary<int, string> search, long user_id)
        {
            DAOContact dao = new DAOContact(user_id);

            search.TryGetValue(INDEX_FIRSTNAME, out string firstname);
            search.TryGetValue(INDEX_LASTNAME, out string lastname);
            search.TryGetValue(INDEX_EMAIL, out string email);
            search.TryGetValue(INDEX_PHONE, out string phone);

            return dao.FindByMultiCriteria(firstname, lastname, email, phone);
        }
    }
}
