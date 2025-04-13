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
    public partial class Hasta: Form
    {
        public Hasta()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-6KIJ0IH\SQLEXPRESS;Initial Catalog=KanBankasiDB;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True");

        private void reset()
        {
            HAdSoyadTb.Text = "";
            HYasTb.Text = "";
            HTelefonTb.Text = "";
            HCinsCb.SelectedIndex = -1;
            HKGrupCb.SelectedIndex = -1;
            HAdresTb.Text = "";
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (HAdSoyadTb.Text == "" || HYasTb.Text == "" || HTelefonTb.Text == "" || HCinsCb.SelectedIndex == -1 || HKGrupCb.SelectedIndex == -1 || HAdresTb.Text == "")
            {
                MessageBox.Show("Eksik Bilgi, Tüm Alanaların Doldurulması Zorunludur.");
            }
            else
            {
                try
                {
                    string query = "insert into HastaTbl values ('" + HAdSoyadTb.Text + "'," + HYasTb.Text + ",'" + HTelefonTb.Text + "','" + HCinsCb.SelectedItem.ToString() + "','" + HKGrupCb.SelectedItem.ToString() + "','" + HAdresTb.Text + "')";
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand(query, baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Hasta Başarılı Bir Şekilde Kayıt Edildi");
                    baglanti.Close();
                    reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Hata Mesajı : " + Ex.Message + " dır.");
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            HastaListesi HL = new HastaListesi();
            HL.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            KanTransferi kt = new KanTransferi();
            kt.Show();
            this.Hide();
        }
    }
}