using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace KanBankaSistem
{
    public partial class Donor: Form
    {
        public Donor()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-6KIJ0IH\SQLEXPRESS;Initial Catalog=KanBankasiDB;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True");
        //;Trust Server Certificate=True

        private void reset()
        {
            DAdSoyadTb.Text = "";
            DYasTb.Text = "";
            DCinsCb.SelectedIndex = -1;
            DTelefonTb.Text = "";
            DAdresTb.Text = "";
            DKGrupCb.SelectedIndex = -1;
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if(DAdSoyadTb.Text == "" || DYasTb.Text == "" || DCinsCb.SelectedIndex == -1 || DTelefonTb.Text == "" || DKGrupCb.SelectedIndex == -1 || DAdresTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi, Tüm Alanaların Doldurulması Zorunludur.");
            }
            else
            {
                try
                {
                    string query = "insert into DonorTbl values ('"+DAdSoyadTb.Text+"',"+DYasTb.Text+",'"+DCinsCb.SelectedItem.ToString()+"','"+DTelefonTb.Text+"','"+DAdresTb.Text+"','"+DKGrupCb.SelectedItem.ToString()+"')";
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand(query, baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Donor Başarılı Bir Şekilde Kayıt Edildi");
                    baglanti.Close();
                    reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Hata Mesajı : " + Ex.Message + " dır.");
                }
            }
        }
    }
}
