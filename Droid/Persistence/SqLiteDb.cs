﻿using System;
using System.IO;
using Omal.Droid.Persistence;
using Omal.Persistence;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqLiteDb))]
namespace Omal.Droid.Persistence
{
    public class SqLiteDb:ISQLiteDb
    {
        public SqLiteDb()
        {
        }

        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySQLiteDb.db3");
            return new SQLiteAsyncConnection(path);
        }

        public long GetDBSize()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySQLiteDb.db3");
            var fileInfo = new FileInfo(path);
            if (!fileInfo.Exists)
                return 0;
            else
                return fileInfo.Length/1000;
        }
    }
}









