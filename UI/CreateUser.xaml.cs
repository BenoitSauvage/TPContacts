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
using System.Windows.Shapes;
using Worker.Classes;

namespace UI {
    /// <summary>
    /// Interaction logic for CreateUser.xaml
    /// </summary>
    public partial class CreateUser : Window {
        UserWorker userManager;

        public CreateUser() {
            InitializeComponent();
            userManager = new UserWorker();
        }

        private void Button_Create_User(object sender, RoutedEventArgs e) {

            if (txtBox_login.Text.Length == 0)
                MessageBox.Show("Error : login can't be empty");

            else if (txtBox_password.Text.Length == 0)
                MessageBox.Show("Error : password can't be empty");

            else if (txtBox_firstname.Text.Length == 0)
                MessageBox.Show("Error : firstname can't be empty");

            else if (txtBox_lastname.Text.Length == 0)
                MessageBox.Show("Error : lastname can't be empty");

            else if (userManager.CheckExistLogin(txtBox_login.Text))
                MessageBox.Show("Error : this login is already used");

            else {
                CreateNewUser();
                MessageBox.Show("New User Created");
                Sign_in win = new Sign_in();
                win.Show();
                this.Close();
            }

        }

        private void CreateNewUser() {
            string login = txtBox_login.Text;
            string password = txtBox_password.Text;
            string firstname = txtBox_firstname.Text;
            string lastname = txtBox_lastname.Text;
            string email = txtBox_email.Text;
            string phone = txtBox_phone.Text;

            userManager.CreateUser(login, password, firstname, lastname, email, phone);
        }

        private void Button_Quit_Create_User(object sender, RoutedEventArgs e) {
            Sign_in win = new Sign_in();
            win.Show();
            this.Close();
        }

    }
}
