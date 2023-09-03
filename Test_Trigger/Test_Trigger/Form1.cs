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

namespace Test_Trigger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-S866UD2;Initial Catalog=Test;Integrated Security=True");

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBLKITAPLAR", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void sayac()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM TBLSAYAC", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblKitap.Text = dr[0].ToString();
            }
            baglanti.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            sayac();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO TBLKITAPLAR (AD,YAZAR,SAYFA,YAYINEVI,TUR) VALUES (@P1,@P2,@P3,@P4,@P5)", baglanti);
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtYazar.Text);
            komut.Parameters.AddWithValue("@P3", TxtSayfa.Text);
            komut.Parameters.AddWithValue("@P4", TxtYayinEvi.Text);
            komut.Parameters.AddWithValue("@P5", TxtTur.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Eklendi!");
            listele();
            sayac();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            TxtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtYazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            TxtSayfa.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtYayinEvi.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtTur.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("DELETE FROM TBLKITAPLAR WHERE ID=@P1",baglanti);
            komut.Parameters.AddWithValue("@P1",TxtId.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Silindi!");
            listele();
            sayac();
        }
    }
}
