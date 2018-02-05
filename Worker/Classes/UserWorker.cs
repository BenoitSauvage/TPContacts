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

        public byte CheckConnect(string login, string password) {
            byte res = 0;
            bool stopLoop = false;

            List<User> users = userManager.FindAll();

            for (int i = 0; !stopLoop && i < users.Count; i++) {
                switch (users[i].Connect(login, password)) {
                    case 3:
                        stopLoop = true ;
                        res = 4;
                        break;
                    case 1:
                        stopLoop = true;
                        res = 1;
                        break;

                }

            }    

            return res;
        }

    }
}
