using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using DAL.DAO;
using Models.Classes;

namespace UI
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DAOContact daoContact = new DAOContact();

            // Add columns
            var gridView = new GridView();
            this.Contacts_List.View = gridView;

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Id",
                DisplayMemberBinding = new Binding("Id")
            });
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
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Email",
                DisplayMemberBinding = new Binding("Email")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Phone",
                DisplayMemberBinding = new Binding("Phone")
            });

            // Populate list
            foreach (Contact contact in daoContact.FindAll())
                this.Contacts_List.Items.Add(contact);
        }
    }
}
