using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Database.Sqlite;
using Android.Database;

namespace Task_Manager.Helper
{
    public class DbHelper : SQLiteOpenHelper
    {
        private static String DB_NAME = "EDMTDev";
        private static int DB_VER = 1;
        public static String DB_TABLE = "Task";
        public static String DB_COLUMN = "TaskName";
        public DbHelper (Context context):base(context,DB_NAME, null, DB_VER) { }
        public override void OnCreate(SQLiteDatabase db)
        {
            string query = $"CREATE TABLE {DbHelper.DB_TABLE } (ID INTEGER PRIMARY KEY AUTOINCREMENT,{DbHelper.DB_COLUMN} TEXT NOT NULL);";
            db.ExecSQL(query);
        }
        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            string query = $"DELET TABLE IF EXISTS {DB_TABLE}";
            db.ExecSQL(query);
            OnCreate(db);
        }

        public void InsertNewTask(String task)
        {
            SQLiteDatabase db = this.WritableDatabase;
            ContentValues values = new ContentValues();
            values.Put(DB_COLUMN, task);
            db.InsertWithOnConflict(DB_TABLE, null, values, Android.Database.Sqlite.Conflict.Replace);
            db.Close();

        }

        public void deletTask(String task)
        {
            SQLiteDatabase db = this.WritableDatabase;
            db.Delete(DB_TABLE, DB_COLUMN + "= ? ", new String[] { task });
            db.Close();
        }

        public List<string> getTaskList()
        {
            List<string> taskList = new List<string>();
            SQLiteDatabase db = this.ReadableDatabase;
            ICursor cursor = db.Query(DB_TABLE,new string[] { DB_COLUMN},null,null,null,null,null);
            while (cursor.MoveToNext())
            {
                int index = cursor.GetColumnIndex(DB_COLUMN);
                taskList.Add(cursor.GetString(index));
            }
            return taskList;
        }
    }
}