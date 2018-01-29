using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Classes {
    class User {

        public bool IsConnect { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }

        public long[] ContactId { get; private set; }


    }
}
