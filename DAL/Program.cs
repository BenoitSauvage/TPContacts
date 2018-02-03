using System;
using System.Collections.Generic;
using DAL.DAO;
using Models.Classes;

namespace DAL
{
    class Program
    {

        static void Main(string[] args) {

            //TestContactQuerys();

            //AddRandomContacts();


            Console.WriteLine("");
            Console.WriteLine("LES CONTACTS");
            Console.WriteLine("");
            CheckCurentContacts();            

            //AddRandomUsers();

            Console.WriteLine("");
            Console.WriteLine("LES UTILISATEURS");
            Console.WriteLine("");
            
            CheckUsers();


        }


        public static void AddRandomUsers() {
            DAOUser dao = new DAOUser();

            User alex = new User("Artur", "Excamlott");
            Contact alex_C = new Contact("Alexandre", "Astier", "AAstier@kaamelott.con");

            User percy = new User("Percy", "culdchouette");
            Contact percy_C = new Contact("Perceval", "de Galle", "provencallegaulois@kaamelott.con");

            User karadoc = new User("Karadoc", "saucissonfinesherbes");
            Contact karadoc_C = new Contact("Karadoc", "de Vanne", "tartinerillette@kaamelott.con");

            

            dao.Create(alex, alex_C);
            dao.Create(percy, percy_C);
            dao.Create(karadoc, karadoc_C);




        }

        public static void CheckUsers() {

            DAOUser daoUser = new DAOUser();
            List<User> users= daoUser.FindAll();

            DAOContact daoContact = new DAOContact();

            foreach (User c in users) {
                c.Print();
                daoContact.FindOneById(c.Contact_Id).Print();
            }


        }

        public static void CheckCurentContacts() {
            DAOContact dao = new DAOContact();
            List<Contact> contacts = dao.FindAll();

            foreach (Contact c in contacts) {
                c.Print();
            }
        }

        public static void AddRandomContacts() {
            DAOContact dao = new DAOContact();
            List<Contact> contacts = new List<Contact>();
            List<Contact> dbContacts = new List<Contact>();

            contacts.Add(new Contact("Benoit", "Sauvage", "benoit.sauvage@example.com", "+1 (514) 111-2222"));
            contacts.Add(new Contact("Bob", "Durand", null, "+1 (514) 111-3333"));
            contacts.Add(new Contact("James", "Bob", "JamyBob@exemp.com", "+1 (254) 220-2659"));
            contacts.Add(new Contact("Pierre", "Martin", "pierre.martin@example.com"));


            // CREATE
            foreach (Contact contact in contacts)
                dao.Create(contact);

            // FIND ALL
            foreach (Contact contact in dao.FindAll())
                dbContacts.Add(contact);

            // PRINT
            Console.WriteLine("=== ACTUAL DB ===");
            Console.WriteLine();
            foreach (Contact contact in dbContacts)
                contact.Print();

        }

        public static void TestContactQuerys()
        {
            DAOContact dao = new DAOContact();


            List<Contact> contacts = new List<Contact>();
            List<Contact> dbContacts = new List<Contact>();

            contacts.Add(new Contact("Benoit", "Sauvage", "benoit.sauvage@example.com", "+1 (514) 111-2222"));
            contacts.Add(new Contact("Bob", "Durand", null, "+1 (514) 111-3333"));
            contacts.Add(new Contact("James", "Bob", "JamyBob@exemp.com", "+1 (254) 220-2659"));
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
            Contact contact3 = dao.FindOneById(dbContacts[2].Id);

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


            //SEARCH BY NAME
            Console.WriteLine("=== TEST SEARCH BY NAME (\"Bob\") ===");
            Console.WriteLine();
            List<Contact> contact4 = dao.FindByName("Bob");

            foreach (Contact contact in contact4)
                contact.Print();

            //SEARCH BY EMAIL
            Console.WriteLine("=== TEST SEARCH BY EMAIL (\"Jam\") ===");
            Console.WriteLine();
            List<Contact> contact5 = dao.FindByEmail("Jam");

            foreach (Contact contact in contact5)
                contact.Print();

            //SEARCH BY PHONE
            Console.WriteLine("=== TEST SEARCH BY PHONE (\"22\") ===");
            Console.WriteLine();
            List<Contact> contact6 = dao.FindByPhone("22");

            foreach (Contact contact in contact6)
                contact.Print();

            //REMOVE ALL (Juste pour pas avoir une DB de 8 bornes qui se repette a chaque fois que l'on lance un test)
            foreach (Contact contact in dao.FindAll())
                dao.Remove(contact);

            Console.Read();
        }
    }
}

            Console.WriteLine();
            List<Contact> contact4 = dao.FindByName("Bob");
