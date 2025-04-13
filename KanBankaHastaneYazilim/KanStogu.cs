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
    public partial class KanStogu: Form
    {
        public KanStogu()
        {
            InitializeComponent();
            kanstok();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-6KIJ0IH\SQLEXPRESS;Initial Catalog=KanBankasiDB;Integrated Security=True;Pooling=False;Encrypt=True;TrustServerCertificate=True");

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

    }
}
