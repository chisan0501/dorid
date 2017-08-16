using Android.App;
using Android.Widget;
using Android.OS;
using MySql.Data.MySqlClient;
using System;

namespace WLookup
{
    [Activity(Label = "WLookup", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //link all controls from front end
            TextView Title = FindViewById<TextView>(Resource.Id.textView1);
            EditText SearchBar = FindViewById<EditText>(Resource.Id.editText1);
            Button SearchBtn = FindViewById<Button>(Resource.Id.button1);


            SearchBtn.Click += (object sender, EventArgs e) =>
            {
                // Translate user's alphanumeric phone number to numeric
                var input = SearchBar.Text;
                if (String.IsNullOrWhiteSpace(input))
                {
                    Toast.MakeText(this, "Search Field Can't Be Empty.", ToastLength.Short).Show();

                }
                else
                {
                    Search(input);
                }
            };
        }


            public string Search(string input) {

            var result = "";

            try
            {
                new I18N.West.CP1250();
                MySqlConnection conn;
                string connsqlstring = "server=MYSQL5013.SmarterASP.NET;user id=a094d4_icdb;password=icdb123!;database=db_a094d4_icdb;persist security info=True;pooling=true;";
                conn = new MySqlConnection(connsqlstring);

                string cmdText = "select * from retail_history where receipt = '"+input+"' ";
                using (MySqlCommand cmd = new MySqlCommand(cmdText, conn))
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader(); //execure the reader

                    while (reader.Read())
                    {
                        result = (reader["ictag"].ToString());


                    }
                    conn.Close();
                }

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
    }
    
}

