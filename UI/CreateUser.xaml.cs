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
using Models.Classes;

namespace UI {
    /// <summary>
    /// Interaction logic for CreateUser.xaml
    /// </summary>
    public partial class CreateUser : Window {
        DAL.DAO.DAOUser userMannager;

        public CreateUser() {
            InitializeComponent();
            userMannager = new DAL.DAO.DAOUser();
        }

        private void Button_Create_User(object sender, RoutedEventArgs e) {


        }

        private void Button_Quit_Create_User(object sender, RoutedEventArgs e) {


        }

    }
}
