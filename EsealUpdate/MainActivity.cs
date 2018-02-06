using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android;
using Android.Content.PM;
using System;
using SQLite.Net;


namespace EsealUpdate
{
    [Activity(Label = "EsealUpdater v1.0.1", Icon = "@drawable/Agility" ,MainLauncher = true)]
    public class MainActivity : Activity
    {
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);

            Button BtnLogin = FindViewById<Button>(Resource.Id.BtnLogin);
            BtnLogin.Click += BtnLogin_Click;
        }
        
        
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                com.rahmat_faisal.www.Service ws = new com.rahmat_faisal.www.Service();
            string hasil;

            EditText txtUserID = FindViewById<EditText>(Resource.Id.userName);
            EditText txtPwd = FindViewById<EditText>(Resource.Id.password);
            hasil = ws.CheckEmployeeID(txtUserID.Text.ToString(), txtPwd.Text.ToString());
                if (hasil.ToLower() != "xxxxxxxxxx")
                {
                    //StartActivity(typeof(ListPickList));
                    Intent intent = new Intent(this, typeof(Update));
                    intent.PutExtra("NIK", txtUserID.Text.ToString());
                    intent.PutExtra("Nama", hasil);
                    StartActivity(intent);
                }
                else
                {
                    Notification.Builder builder = new Notification.Builder(this)
                                .SetContentTitle("Login Gagal")
                                .SetContentText("NIK atau Password salah")
                                .SetDefaults(NotificationDefaults.Sound);


                    // Build the notification:
                    Notification notification = builder.Build();

                    // Get the notification manager:
                    NotificationManager notificationManager =
                        GetSystemService(Context.NotificationService) as NotificationManager;

                    // Publish the notification:
                    const int notificationId = 0;
                    notificationManager.Notify(notificationId, notification);

                    AlertDialog.Builder dlg = new AlertDialog.Builder(this);
                    AlertDialog alert = dlg.Create();
                    alert.SetTitle("Login Gagal");
                    alert.SetMessage("NIK atau Password salah");
                    alert.Show();
                }
            }

            catch (Exception ex)
            {
                AlertDialog.Builder dlg = new AlertDialog.Builder(this);
                AlertDialog alert = dlg.Create();
                alert.SetTitle("Please Relogin   ");
                alert.SetMessage("User ID + Password must be Entry, " +
                    "Please Cek Your internet Connection or send your problem to Kaizencldd@agility.com  =problem>>" + ex.Message.ToString());
                alert.Show();
            }
        }

        }
    }


