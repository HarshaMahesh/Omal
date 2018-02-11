using System;
using System.IO;
using Omal.iOS.Persistence;
using Omal.Persistence;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqLiteDb))]
namespace Omal.iOS.Persistence
{
    public class SqLiteDb : ISQLiteDb
    {
        public SqLiteDb()
        {
        }

        public SQLiteAsyncConnection GetConnection()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");
            if (!System.IO.Directory.Exists(libFolder)) System.IO.Directory.CreateDirectory(libFolder);
            var path = Path.Combine(libFolder, "MySQLiteDb.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}



