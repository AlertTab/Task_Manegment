using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Task_Manager.Helper;
using System.Collections.Generic;

namespace Task_Manager
{
    [Activity(Label = "Task_Manager", MainLauncher = true, Icon = "@drawable/icon",Theme = "@style/Theme.AppCompat.Light")]
    public class MainActivity : AppCompatActivity
    {
        EditText taskEditText;
        DbHelper dbHelper;
        CustomAdapter adapter;
        ListView lstTask;
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_iteam,menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_add:
                    taskEditText = new EditText(this);
                    Android.Support.V7.App.AlertDialog dialog = new Android.Support.V7.App.AlertDialog.Builder(this)
                    .SetTitle("Add New Task")
                    .SetMessage("What do you want to do next?")
                    .SetView(taskEditText)
                    .SetPositiveButton("Add", OkAction)
                    .SetNegativeButton("Cansel", CancelAction)
                    .Create();
                    dialog.Show();
                    return true;

            }

            return base.OnOptionsItemSelected(item);
        }

        private void CancelAction(object sender, DialogClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OkAction(object sender, DialogClickEventArgs e)
        {
            string task = taskEditText.Text;
            dbHelper.InsertNewTask(task);
            LoadTaskList();
        }

        public void LoadTaskList()
        {
            List<string> taskList= dbHelper.getTaskList();
            adapter = new CustomAdapter(this,taskList,dbHelper);
            lstTask.Adapter = adapter;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);           
            SetContentView(Resource.Layout.Main);

            dbHelper = new DbHelper(this);
            lstTask = FindViewById<ListView>(Resource.Id.lstTask);

            LoadTaskList();
        }
    }
}
