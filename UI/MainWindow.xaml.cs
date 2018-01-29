using System;
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
        DAOContact daoContact;

        public MainWindow()
        {
            InitializeComponent();
            this.daoContact = new DAOContact();

            this.HideForm();

            // Add columns
            var gridView = new GridView();
            this.Contacts_List.View = gridView;

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Firstname",
                DisplayMemberBinding = new Binding("Firstname")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Lastname",
                DisplayMemberBinding = new Binding("Lastname")
            });

            this.ShowList();
        }

        private void BtnAddContact_Click(object sender, RoutedEventArgs e)
        {
            this.HideList();
            this.ShowForm();
        }

        private void AddContactBtnValidate_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> contact = new Dictionary<string, string>();

            contact.Add("firstname", this.input_firstname.Text);
            contact.Add("lastname", this.input_lastname.Text);
            contact.Add("email", this.input_email.Text);
            contact.Add("phone", this.input_phone.Text);

            List<string> errors = ContactWorker.ValidateContact(contact);
            this.AddContactErrorStack.Children.Clear();

            if (errors.Count > 0)
            {
                this.AddContactErrorStack.Visibility = Visibility.Visible;
                foreach(string error in errors)
                {
                    TextBlock text = new TextBlock();
                    text.Text = error;
                    text.TextAlignment = TextAlignment.Center;
                    text.Foreground = Brushes.Red;
                    text.FontWeight = FontWeights.Bold;
                    this.AddContactErrorStack.Children.Add(text);
                }
            }
            else
            {
                ContactWorker.AddContact(contact);
                this.HideForm();
                this.ShowList();
            }           
        }

        private void HideForm()
        {
            this.AddContactTextStack.Visibility = Visibility.Hidden;
            this.AddContactInputStack.Visibility = Visibility.Hidden;
            this.AddContactBtnValidate.Visibility = Visibility.Hidden;
            this.AddContactErrorStack.Visibility = Visibility.Hidden;
        }

        private void ShowForm()
        {
            this.AddContactTextStack.Visibility = Visibility.Visible;
            this.AddContactInputStack.Visibility = Visibility.Visible;
            this.AddContactBtnValidate.Visibility = Visibility.Visible;
            this.AddContactErrorStack.Visibility = Visibility.Visible;

            this.input_firstname.Clear();
            this.input_lastname.Clear();
            this.input_email.Clear();
            this.input_phone.Clear();
        }

        private void HideList()
        {
            this.Contacts_List.Visibility = Visibility.Hidden;
            this.BtnAddContact.Visibility = Visibility.Hidden;
        }

        private void ShowList()
        {
            this.Contacts_List.Visibility = Visibility.Visible;
            this.BtnAddContact.Visibility = Visibility.Visible;

            this.Contacts_List.Items.Clear();

            // Populate list
            foreach (Contact contact in this.daoContact.FindAll())
                this.Contacts_List.Items.Add(contact);
        }
    }
}
