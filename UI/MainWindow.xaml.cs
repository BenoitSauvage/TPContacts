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
        const string FIRSTNAME = "Firstname";
        const string LASTNAME = "Lastname";

        DAOContact daoContact;
        string sort = null;

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
                Header = FIRSTNAME,
                DisplayMemberBinding = new Binding(FIRSTNAME)
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = LASTNAME,
                DisplayMemberBinding = new Binding(LASTNAME)
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
                    this.FormContactErrorStack.Children.Add(text);
                }
            }
            else
            {
                if (this.input_id.Text == "")
                {
                    ContactWorker.AddContact(contact);
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.input_search.Text == "")
                this.ShowList();

            else
            {
                this.Contacts_List.Items.Clear();

                // Populate list
                foreach (Contact contact in ContactWorker.Filter(this.input_search.Text))
                    this.Contacts_List.Items.Add(contact);
            }
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
            this.input_search.Visibility = Visibility.Hidden;
            this.search_text.Visibility = Visibility.Hidden;
        }

        private void ShowList()
        {
            this.Contacts_List.Visibility = Visibility.Visible;
            this.BtnDisplayFormContact.Visibility = Visibility.Visible;
            this.input_search.Visibility = Visibility.Visible;
            this.search_text.Visibility = Visibility.Visible;

            this.input_search.Clear();
            this.Contacts_List.Items.Clear();

            // Populate list
            foreach (Contact contact in ContactWorker.GetAll())
                this.Contacts_List.Items.Add(contact);
        }

        private Dictionary<string, string> GenerateContact()
        {
            Dictionary<string, string> contact = new Dictionary<string, string>();

            contact.Add("id", this.input_id.Text);
            contact.Add("firstname", this.input_firstname.Text);
            contact.Add("lastname", this.input_lastname.Text);
            contact.Add("email", this.input_email.Text);
            contact.Add("phone", this.input_phone.Text);

            return contact;
        }
    }
}
