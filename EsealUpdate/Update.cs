using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace EsealUpdate
{
    [Activity(Label = "Update")]
    public class Update : Activity
    {
        Button btnSubmit;
        Button btnAd;
        EditText imei;
        EditText txv;
        EditText Shipment;
        EditText No_mobil;
        EditText Nama_Sopir;
        EditText No_DO;
        EditText No_Job;
        EditText No_Aju;
        EditText PickUpDari ;
        EditText No_Container ;


        protected override void OnCreate(Bundle savedInstanceState)
        {


            Shipment = (EditText)FindViewById(Resource.Id.ShipmentID);
            No_mobil = (EditText)FindViewById(Resource.Id.txt_NoMobil);
            Nama_Sopir = (EditText)FindViewById(Resource.Id.txt_NamaSopir);
            No_DO = (EditText)FindViewById(Resource.Id.txt_NoDO);
            No_Job = (EditText)FindViewById(Resource.Id.txt_NoJob);
            No_Aju = (EditText)FindViewById(Resource.Id.txt_NoAju);
            PickUpDari = (EditText)FindViewById(Resource.Id.txt_PickUpDari);
            No_Container = (EditText)FindViewById(Resource.Id.txt_noContainer);
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.Update);
            
            btnSubmit = FindViewById<Button>(Resource.Id.Btn_Submit);
            btnSubmit.Click += BtnSubmit_Click;
            btnAd = FindViewById<Button>(Resource.Id.Btn_Add);
            btnAd.Click += BtnAdd_Click;
            EditText AutoCompleteTxt = (EditText)FindViewById(Resource.Id.AutoCompleteInput);
            AutoCompleteTxt.RequestFocus();
            if (AutoCompleteTxt.HasFocus==true)
            {
                startActiv();
            }
            
        }

        

        private void startActiv ()
        {
            

            SetDisable();

            TextView tgl_kirim = (TextView)FindViewById(Resource.Id.tgl_date);
            tgl_kirim.Text = DateTime.Today.ToShortDateString();
            
            System.Data.DataSet ds = new System.Data.DataSet();
            com.rahmat_faisal.www.Service ws = new com.rahmat_faisal.www.Service();
            ds = ws.G_eseal();

            List<String> listEseal;
            listEseal = new List<string>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listEseal.Add(ds.Tables[0].Rows[i]["NoEseal"].ToString().Substring(15, 3));
            }
            

            //string[] geseal = Resources.GetStringArray(Resource.Array.Geseal_array);
            var adapter = new ArrayAdapter<String>(this, Resource.Layout.list_item);
            ArrayAdapter autoCompleteAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, listEseal);

            var autocompleteTextView = FindViewById<AutoCompleteTextView>(Resource.Id.AutoCompleteInput);
            autocompleteTextView.Adapter = autoCompleteAdapter;

            autocompleteTextView.ItemClick += AutocompleteTextView_ItemClick;
            /*AlertDialog.Builder dlg = new AlertDialog.Builder(this);
            AlertDialog alert = dlg.Create();
            alert.SetTitle("masukan No Eseal dahulu");
            alert.Show();*/

        }
        private void SetDisable()
        {
            Shipment = (EditText)FindViewById(Resource.Id.ShipmentID);
            No_mobil = (EditText)FindViewById(Resource.Id.txt_NoMobil);
            Nama_Sopir = (EditText)FindViewById(Resource.Id.txt_NamaSopir);
            No_DO = (EditText)FindViewById(Resource.Id.txt_NoDO);
            No_Job = (EditText)FindViewById(Resource.Id.txt_NoJob);
            No_Aju = (EditText)FindViewById(Resource.Id.txt_NoAju);
            PickUpDari = (EditText)FindViewById(Resource.Id.txt_PickUpDari);
            No_Container = (EditText)FindViewById(Resource.Id.txt_noContainer);
            btnSubmit = FindViewById<Button>(Resource.Id.Btn_Submit);
            btnAd = FindViewById<Button>(Resource.Id.Btn_Add);
            Shipment.Enabled = false;
            No_mobil.Enabled = false;
            Nama_Sopir.Enabled = false;
            No_DO.Enabled = false;
            No_Job.Enabled = false;
            No_Aju.Enabled = false;
            PickUpDari.Enabled = false;
            No_Container.Enabled = false;
            btnSubmit.Enabled = false;
            btnAd.Enabled = false;

        }
        private void AutocompleteTextView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            
                com.rahmat_faisal.www.Service ws = new com.rahmat_faisal.www.Service();
                AutoCompleteTextView txv = FindViewById<AutoCompleteTextView>(Resource.Id.AutoCompleteInput);
                EditText tximei = FindViewById<EditText>(Resource.Id.etIMEI);
                tximei.Text = ws.G_IMEI("ESEAL_AG_BC_PLB" + txv.Text.Trim());
                SetEnable();
           

        }

        private void SetEnable()
            {
            Shipment = (EditText)FindViewById(Resource.Id.ShipmentID);
            No_mobil = (EditText)FindViewById(Resource.Id.txt_NoMobil);
            Nama_Sopir = (EditText)FindViewById(Resource.Id.txt_NamaSopir);
            No_DO = (EditText)FindViewById(Resource.Id.txt_NoDO);
            No_Job = (EditText)FindViewById(Resource.Id.txt_NoJob);
            No_Aju = (EditText)FindViewById(Resource.Id.txt_NoAju);
            PickUpDari = (EditText)FindViewById(Resource.Id.txt_PickUpDari);
            No_Container = (EditText)FindViewById(Resource.Id.txt_noContainer);
            btnSubmit = FindViewById<Button>(Resource.Id.Btn_Submit);
            btnAd = FindViewById<Button>(Resource.Id.Btn_Add);
            Shipment.Enabled = true;
                No_mobil.Enabled = true;
                Nama_Sopir.Enabled = true;
                No_DO.Enabled = true;
                No_Job.Enabled = true;
                No_Aju.Enabled = true;
                PickUpDari.Enabled = true;
                No_Container.Enabled = true;
                btnSubmit.Enabled = true;
                btnAd.Enabled = true;
            
            }

        private void BtnSubmit_Click(object sender, System.EventArgs e)
        {
            try
            {
                CekValid();
            }
            catch (Exception ex)
            {
                AlertDialog.Builder dlg = new AlertDialog.Builder(this);
                AlertDialog alert = dlg.Create();
                alert.SetTitle("Submit Gagal");
                alert.SetMessage("data Tidak lengkap"+ex.Message.ToString());
                alert.Show();

            }
        }
            private void SendData()
        {

            try
            {
                btnSubmit = FindViewById<Button>(Resource.Id.Btn_Submit);
                txv = FindViewById<AutoCompleteTextView>(Resource.Id.AutoCompleteInput);
                Shipment = (EditText)FindViewById(Resource.Id.ShipmentID);
                No_mobil = (EditText)FindViewById(Resource.Id.txt_NoMobil);
                Nama_Sopir = (EditText)FindViewById(Resource.Id.txt_NamaSopir);
                No_DO = (EditText)FindViewById(Resource.Id.txt_NoDO);
                No_Job = (EditText)FindViewById(Resource.Id.txt_NoJob);
                No_Aju = (EditText)FindViewById(Resource.Id.txt_NoAju);
                PickUpDari = (EditText)FindViewById(Resource.Id.txt_PickUpDari);
                No_Container = (EditText)FindViewById(Resource.Id.txt_noContainer);
                imei = (EditText)FindViewById(Resource.Id.etIMEI);




                //throw new NotImplementedException();
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("http://server.m2fleet.co.id/webservicescript/16/WS02?user=wstester&pass=wstester&id={0}&Shipment1={1}&Vehicle={2}&Driver={3}&DONumber={4}&JobNumber={5}&AjuNumber={6}&PickUpFrom={7}&Container={8}&Delivery={9}",
                  imei.Text,
                  Shipment.Text,
                  No_mobil.Text,
                  Nama_Sopir.Text,
                  No_DO.Text,
                  No_Job.Text,
                  No_Aju.Text,
                  PickUpDari.Text,
                  No_Container.Text,
                  DateTime.Now);
                HttpWebRequest request = WebRequest.Create(sb.ToString()) as HttpWebRequest;
                //optional
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream stream = response.GetResponseStream();


                SetDisable();
                btnAd = FindViewById<Button>(Resource.Id.Btn_Add);
                btnAd.Enabled = true;
                AlertDialog.Builder dlg = new AlertDialog.Builder(this);
                AlertDialog alert = dlg.Create();
                alert.SetTitle("Submit Sukses");
                alert.Show();
            }
            catch (Exception ex)
            {
                AlertDialog.Builder dlg = new AlertDialog.Builder(this);
                AlertDialog alert = dlg.Create();
                alert.SetTitle("Submit Gagal");
                alert.SetMessage(ex.Message.ToString());
                alert.Show();
            }
        }

        private void CekValid()
        {
            btnSubmit = FindViewById<Button>(Resource.Id.Btn_Submit);
            txv = FindViewById<AutoCompleteTextView>(Resource.Id.AutoCompleteInput);
            Shipment = (EditText)FindViewById(Resource.Id.ShipmentID);
            No_mobil = (EditText)FindViewById(Resource.Id.txt_NoMobil);
            Nama_Sopir = (EditText)FindViewById(Resource.Id.txt_NamaSopir);
            No_DO = (EditText)FindViewById(Resource.Id.txt_NoDO);
            No_Job = (EditText)FindViewById(Resource.Id.txt_NoJob);
            No_Aju = (EditText)FindViewById(Resource.Id.txt_NoAju);
            PickUpDari = (EditText)FindViewById(Resource.Id.txt_PickUpDari);
            No_Container = (EditText)FindViewById(Resource.Id.txt_noContainer);
            imei = (EditText)FindViewById(Resource.Id.etIMEI);
            
            try
            {
                if (string.IsNullOrWhiteSpace(imei.Text) ||
                    string.IsNullOrWhiteSpace(txv.Text) ||
                    string.IsNullOrWhiteSpace(No_Aju.Text) ||
                    string.IsNullOrWhiteSpace(No_Container.Text) ||
                    string.IsNullOrWhiteSpace(Nama_Sopir.Text)||
                    string.IsNullOrWhiteSpace(PickUpDari.Text)
                    )
                    
                {
                   
                    AlertDialog.Builder dlg = new AlertDialog.Builder(this);
                    AlertDialog alert = dlg.Create();
                    alert.SetTitle("Allert !");
                    alert.SetMessage("Data Belum Lengkap");
                    alert.Show();
                  
                }
                else
                {
                    SendData();

                }
            }
           catch (Exception ex)
            {
                AlertDialog.Builder dlg = new AlertDialog.Builder(this);
                AlertDialog alert = dlg.Create();
                alert.SetTitle("Submit Gagal");
                alert.SetMessage(ex.Message.ToString());
                alert.Show();


            }

        }

        
        private void BtnAdd_Click(object sender, System.EventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            imei = (EditText)FindViewById(Resource.Id.etIMEI);
            txv = (EditText)FindViewById(Resource.Id.AutoCompleteInput);

            Shipment = (EditText)FindViewById(Resource.Id.ShipmentID);
            No_mobil = (EditText)FindViewById(Resource.Id.txt_NoMobil);
            Nama_Sopir = (EditText)FindViewById(Resource.Id.txt_NamaSopir);
            No_DO = (EditText)FindViewById(Resource.Id.txt_NoDO);
            No_Job = (EditText)FindViewById(Resource.Id.txt_NoJob);
            No_Aju = (EditText)FindViewById(Resource.Id.txt_NoAju);
            PickUpDari = (EditText)FindViewById(Resource.Id.txt_PickUpDari);
            No_Container = (EditText)FindViewById(Resource.Id.txt_noContainer);
            
            imei.Text = "";
            txv.Text = "";
            Shipment.Text = "";
            No_mobil.Text = "";
            Nama_Sopir.Text = "";
            No_DO.Text = "";
            No_Job.Text = "";
            No_Aju.Text = "";
            PickUpDari.Text = "";
            No_Container.Text = "";
        }
    }
}