using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KanBankaSistem
{
    public partial class KanTransferi: Form
    {
        public KanTransferi()
        {
            InitializeComponent();
            fillhastacb();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-6KIJ0IH\SQLEXPRESS;Initial Catalog=KanBankasiDB;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True");

        private void fillhastacb()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select HNum from HastaTbl", baglanti);
            SqlDataReader rdr;
            rdr = komut.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("HNum", typeof(int));
            dt.Load(rdr);
            HastaIDCb.ValueMember = "HNum";
            HastaIDCb.DataSource = dt;
            baglanti.Close();
        }

        private void verial()
        {
            baglanti.Open();
            string query = "select * from HastaTbl where HNum = " + HastaIDCb.SelectedValue.ToString() + "";
            SqlCommand komut = new SqlCommand(query, baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(komut);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                HAdSoyadTb.Text = dr["HAdSoyad"].ToString();
                HKGrupTb.Text = dr["HKGrup"].ToString();
            }
            baglanti.Close();
        }

        int stokk = 0;

        private void stok(string kgrup)
        {
            baglanti.Open();
            string query = "select * from KanTbl where KGrup = '" + kgrup + "'";
            SqlCommand komut = new SqlCommand(query, baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(komut);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                stokk = Convert.ToInt32(dr["KStok"].ToString());
            }
            baglanti.Close();
        }

        private void HastaIDCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            verial();
            stok(HKGrupTb.Text);
            if(stokk > 0)
            {
                TransferBtn.Visible = true;
                UygunLbl.Text = "Stok Uygun";
                UygunLbl.Visible = true;
            }
            else
            {
                UygunLbl.Text = "Stok Uygun Değil";
                UygunLbl.Visible = true; 
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Hasta ha = new Hasta();
            ha.Show();
            this.Hide();
        }
    }
}
