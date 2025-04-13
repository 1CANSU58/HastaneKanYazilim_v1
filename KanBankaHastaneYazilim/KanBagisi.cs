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
    public partial class KanBagisi: Form
    {
        public KanBagisi()
        {
            InitializeComponent();
            uyeler();
            kanstok();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-6KIJ0IH\SQLEXPRESS;Initial Catalog=KanBankasiDB;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True");

        private void uyeler()
        {
            baglanti.Open();
            string query = "select * from DonorTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, baglanti);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            KBagisiDGV.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void kanstok()
        {
            baglanti.Open();
            string query = "select * from KanTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, baglanti);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            KStoguDGV.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        int eskistok;

        private void stok(string kgrup)
        {
            baglanti.Open();
            string query = "select * from KanTbl where KGrup = '"+kgrup+"'";
            SqlCommand komut = new SqlCommand(query, baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(komut);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                eskistok = Convert.ToInt32(dr["KStok"].ToString());
            }
            baglanti.Close();
        }


        private void reset()
        {
            DAdSoyadTb.Text = "";
            DKGrubuTb.Text = "";
        }

        private void KBagisiDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DAdSoyadTb.Text = KBagisiDGV.SelectedRows[0].Cells[1].Value.ToString();
            DKGrubuTb.Text = KBagisiDGV.SelectedRows[0].Cells[6].Value.ToString();
            stok(DKGrubuTb.Text);
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if(DAdSoyadTb.Text == "")
            {
                MessageBox.Show("Bir Donor Seçiniz");
            }
            else
            {
                try
                {
                    int stok = eskistok + 1;
                    string query = "update KanTbl set KStok = '" + stok + "' where KGrup = '" + DKGrubuTb.Text + "';";
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand(query, baglanti);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Kan Bağışı Başarılı Bir Şekilde Kayıt Edildi");
                    baglanti.Close();
                    reset();
                    kanstok();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Hata Mesajı : " + Ex.Message + " dır.");
                }
            }
        }
    }
}
