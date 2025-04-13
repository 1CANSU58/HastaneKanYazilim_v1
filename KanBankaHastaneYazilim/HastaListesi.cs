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
    public partial class HastaListesi: Form
    {
        public HastaListesi()
        {
            InitializeComponent();
            uyeler();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-6KIJ0IH\SQLEXPRESS;Initial Catalog=KanBankasiDB;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True");

        private void uyeler()
        {
            baglanti.Open();
            string query = "select * from HastaTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, baglanti);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            HastaDGV.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void reset()
        {
            HAdSoyadTb.Text = "";
            HYasTb.Text = "";
            HTelefonTb.Text = "";
            HCinsCb.SelectedIndex = -1;
            HKGrupCb.SelectedIndex = -1;
            HAdresTb.Text = "";
            key = 0;
        }

        int key = 0;

        private void HastaDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            HAdSoyadTb.Text = HastaDGV.SelectedRows[0].Cells[1].Value.ToString();
            HYasTb.Text = HastaDGV.SelectedRows[0].Cells[2].Value.ToString();
            HTelefonTb.Text = HastaDGV.SelectedRows[0].Cells[3].Value.ToString();
            HCinsCb.Text = HastaDGV.SelectedRows[0].Cells[4].Value.ToString();
            HKGrupCb.Text = HastaDGV.SelectedRows[0].Cells[5].Value.ToString();
            HAdresTb.Text = HastaDGV.SelectedRows[0].Cells[6].Value.ToString();

            if(HAdSoyadTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(HastaDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            if(key == 0)
            {
                MessageBox.Show("Silinecek Hastayı Seçiniz");
            }
            else
            {
                try
                {
                    string query = "delete from HastaTbl where HNum = "+key+";";
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand(query, baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Hasta Başarılı Bir Şekilde Silindi");
                    baglanti.Close();
                    reset();
                    uyeler();
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

        private void label4_Click(object sender, EventArgs e)
        {
            Hasta ha = new Hasta();
            ha.Show();
            this.Hide();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (HAdSoyadTb.Text == "" || HYasTb.Text == "" || HTelefonTb.Text == "" || HCinsCb.SelectedIndex == -1 || HKGrupCb.SelectedIndex == -1 || HAdresTb.Text == "")
                {
                MessageBox.Show("Eksik Bilgi");
            }
            else
            {
                try
                {
                    string query = "update HastaTbl set HAdSoyad = '"+HAdSoyadTb.Text+"',HYas = "+HYasTb.Text+",HTelefon = '"+HTelefonTb.Text+"',HCinsiyet = '"+HCinsCb.SelectedItem.ToString()+"',HKGrup = '"+HKGrupCb.SelectedItem.ToString()+"',HAdres = '" +HAdresTb.Text+"' where HNum = "+key+";";
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand(query, baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Hasta Başarılı Bir Şekilde Güncellendi");
                    baglanti.Close();
                    reset();
                    uyeler();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Hata Mesajı : " + Ex.Message + " dır.");
                }
            }
        }
    }
}
