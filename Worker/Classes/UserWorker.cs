using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Classes;

namespace Worker.Classes {
    public class UserWorker {
        DAL.DAO.DAOUser userManager;

        public UserWorker() {
            userManager = new DAL.DAO.DAOUser();
        }

        public long GetCurrentUser() {
            return (long)User.current_user_id;
        }

        public string GetLoginById(long id) {
            User user = userManager.FindOneById(id);

            return user.Login;
        }

        public bool CheckExistLogin(string login) {
            bool res = false;

            List<User> users = userManager.FindAll();

            for (int i=0; i<users.Count && !res; i++) {
                if(users[i].Login == login) {
                    res = true;
                }

            }


            return res;
        }

        public void CreateUser(string login, string password, string firstname, string lastname,string email, string phone) {

            User user = new User(login, password);
            Contact contact = new Contact(firstname, lastname, email, phone);

            userManager.Create(user, contact);

        }


        public byte CheckConnect(string login, string password) {
            byte res = 0;

            List<User> users = userManager.FindAll();

            foreach(User user in users) {
                switch (user.Connect(login, password)) {
                    case 3:
                        res = 2;
                        break;
                    case 1:
                        res = 1;
                        break;
                }
            }    

            return res;
        }

    }
}
