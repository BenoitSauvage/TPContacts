﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections.Generic;
using System.Windows.Media;

using DAL.DAO;
using Models.Classes;
using Worker.Classes;

namespace UI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string FIRSTNAME = "Firstname";
        const string LASTNAME = "Lastname";
        const string EMAIL = "Email";
        const string PHONE = "Phone";

        DAOContact daoContact;
        long currentUser;

        public MainWindow(long currentUser)
        {
            InitializeComponent();
            this.daoContact = new DAOContact();
            this.currentUser = currentUser;

            this.HideForm();

            // Add columns
            var gridView = new GridView();
            this.Contacts_List.View = gridView;
            this.Contacts_List.FontSize = 20;
            var header_style = new Style();

            gridView.Columns.Add(new GridViewColumn
            {
                Header = FIRSTNAME,
                Width = 150,
                DisplayMemberBinding = new Binding(FIRSTNAME)
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = LASTNAME,
                Width = 150,
                DisplayMemberBinding = new Binding(LASTNAME)
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = EMAIL,
                Width = 300,
                DisplayMemberBinding = new Binding(EMAIL)
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = PHONE,
                Width = 180,
                DisplayMemberBinding = new Binding(PHONE)
            });

            this.ShowList();
        }

        private void BtnAddContact_Click(object sender, RoutedEventArgs e)
        {
            this.HideList();
            this.ShowForm();

            this.BtnContactValidate.Content = "Ajouter";
        }

        private void BtnContactValidate_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> contact = this.GenerateContact();

            List<string> errors = ContactWorker.ValidateContact(contact);
            this.FormContactErrorStack.Children.Clear();

            if (errors.Count > 0)
            {
                this.FormContactErrorStack.Visibility = Visibility.Visible;
                foreach(string error in errors)
                {
                    TextBlock text = new TextBlock();
                    text.Text = error;
                    text.TextAlignment = TextAlignment.Center;
                    text.Foreground = Brushes.Red;
                    text.FontWeight = FontWeights.Bold;
                    text.FontSize = 20;
                    this.FormContactErrorStack.Children.Add(text);
                }
            }
            else
            {
                if (this.input_id.Text == "")
                {
                    ContactWorker.AddContact(contact, this.currentUser);
                }
                else
                {
                    ContactWorker.UpdateContact(contact);
                }
               
                this.HideForm();
                this.ShowList();
            }           
        }

        private void Contacts_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Contact contact = (Contact)e.AddedItems[0];

                this.HideList();
                this.ShowForm();
                
                this.input_id.Text = contact.Id.ToString();
                this.input_firstname.Text = contact.Firstname;
                this.input_lastname.Text = contact.Lastname;
                this.input_email.Text = contact.Email == "NULL" ? "" : contact.Email;
                this.input_phone.Text = contact.Phone == "NULL" ? "" : contact.Phone;

                this.BtnContactValidate.Content = "Modifier";
            }
            catch (Exception exception)
            {
                // DO NOTHING
            }

        }

        private void BackToList_Click(object sender, RoutedEventArgs e)
        {
            this.HideForm();
            this.ShowList();
        }

        private void BtnDeleteFormContact_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> contact = this.GenerateContact();

            ContactWorker.RemoveContact(contact);

            this.HideForm();
            this.ShowList();
        }

        private void GridViewSort(object sender, RoutedEventArgs e)
        {
            var header = e.OriginalSource as GridViewColumnHeader;

            this.Contacts_List.Items.Clear();

            // Populate list
            foreach (Contact contact in ContactWorker.Sort(header.Content.ToString()))
                this.Contacts_List.Items.Add(contact);
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Contacts_List.Items.Clear();

            Dictionary<int, string> search = new Dictionary<int, string>
            {
                { 0, this.search_firstname.Text },
                { 1, this.search_lastname.Text },
                { 2, this.search_email.Text },
                { 3, this.search_phone.Text }
            };

            foreach (Contact contact in ContactWorker.Search(search, this.currentUser))
                this.Contacts_List.Items.Add(contact);
        }

        private void HideForm()
        {
            this.BackToList.Visibility = Visibility.Hidden;

            this.FormContactTextStack.Visibility = Visibility.Hidden;
            this.FormContactInputStack.Visibility = Visibility.Hidden;
            this.BtnContactValidate.Visibility = Visibility.Hidden;
            this.BtnDeleteFormContact.Visibility = Visibility.Hidden;
            this.FormContactErrorStack.Visibility = Visibility.Hidden;
        }

        private void ShowForm()
        {
            this.BackToList.Visibility = Visibility.Visible;

            this.FormContactTextStack.Visibility = Visibility.Visible;
            this.FormContactInputStack.Visibility = Visibility.Visible;
            this.BtnContactValidate.Visibility = Visibility.Visible;
            this.BtnDeleteFormContact.Visibility = Visibility.Visible;
            this.FormContactErrorStack.Visibility = Visibility.Visible;

            this.input_id.Clear();
            this.input_firstname.Clear();
            this.input_lastname.Clear();
            this.input_email.Clear();
            this.input_phone.Clear();
        }

        private void HideList()
        {
            this.Contacts_List.Visibility = Visibility.Hidden;
            this.BtnDisplayFormContact.Visibility = Visibility.Hidden;
            this.Search_Bar.Visibility = Visibility.Hidden;
        }

        private void ShowList()
        {
            this.Contacts_List.Visibility = Visibility.Visible;
            this.BtnDisplayFormContact.Visibility = Visibility.Visible;
            this.Search_Bar.Visibility = Visibility.Visible;

            this.search_firstname.Clear();
            this.search_lastname.Clear();
            this.search_email.Clear();
            this.search_phone.Clear();

            this.Contacts_List.Items.Clear();

            // Populate list
            foreach (Contact contact in ContactWorker.GetAllForUser(this.currentUser))
                this.Contacts_List.Items.Add(contact);
        }

        private Dictionary<string, string> GenerateContact()
        {
            Dictionary<string, string> contact = new Dictionary<string, string>
            {
                { "id", this.input_id.Text },
                { "firstname", this.input_firstname.Text },
                { "lastname", this.input_lastname.Text },
                { "email", this.input_email.Text },
                { "phone", this.input_phone.Text }
            };

            return contact;
        }
    }
}
