﻿using System;
using System.Collections.Generic;
using DAL.DAO;
using Models.Classes;

namespace DAL
{
    class Program
    {
        static void Main(string[] args)
        {
            DAOContact dao = new DAOContact();
            List<Contact> contacts = new List<Contact>();
            List<Contact> dbContacts = new List<Contact>();

            contacts.Add(new Contact("Benoit", "Sauvage", "benoit.sauvage@example.com", "+1 (514) 111-2222"));
            contacts.Add(new Contact("Bob", "Durand", null, "+1 (514) 111-3333"));
            contacts.Add(new Contact("Pierre", "Martin", "pierre.martin@example.com"));

            // PRINT
            Console.WriteLine("=== BEFORE CREATE ===");
            Console.WriteLine();
            foreach (Contact contact in contacts)
                contact.Print();

            // CREATE
            foreach (Contact contact in contacts)
                dao.Create(contact);

            // FIND ALL
            foreach (Contact contact in dao.FindAll())
                dbContacts.Add(contact);


            // PRINT
            Console.WriteLine("=== DB BEFORE UPDATE ===");
            Console.WriteLine();
            foreach (Contact contact in dbContacts)
                contact.Print();

            // UPDATE
            Contact contact1 = dbContacts[0];
            contact1.Firstname = "Bob";
            contact1.Email = "bob.sauvage@example.com";

            dao.Update(contact1);

            // FIND ONE BY ID
            Contact contact3 = dao.FindOneById(3);

            // UPDATE
            contact3.Phone = "+1 (514) 111-4444";
            dbContacts[2] = contact3;

            dao.Update(contact3);

            // FIN ALL
            dbContacts = dao.FindAll();

            // PRINT
            Console.WriteLine("=== DB AFTER UPDATE ===");
            Console.WriteLine();
            foreach (Contact contact in dbContacts)
                contact.Print();

            Console.Read();
        }
    }
}
