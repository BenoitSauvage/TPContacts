# TPContacts

1 - Le script pour créer la base de données est situé dans "DAL/scripts/create_database.sql" 
2 - Pour fonctionner, créer un fichier "DAL/DAO/ConnectData.cs" avec le code suivant :

```csharp
using System;

namespace DAL.DAO
{
    class ConnectData
    {
        public static string connectionString = @"Data Source = BENOIT\SQLEXPRESS; Initial Catalog = tpcontact; Integrated Security = True; Connect Timeout = 5;";
    }
}
```

3 - Modifier la chaine de connection pour correspondre à votre besoin
