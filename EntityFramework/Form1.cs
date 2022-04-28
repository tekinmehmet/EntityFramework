using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFramework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();
        private void btnDersListesi_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.TBLDERSLER.ToList();
        }

        private void btnOgrenciListele_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = db.TBLOGRENCI.ToList();
            var query = (from x in db.TBLOGRENCI
                                        select new
                                        {
                                            x.ID,
                                            x.AD,
                                            x.SOYAD,
                                            x.FOTOGRAF

                                        }).ToList();
            dataGridView1.DataSource = query;
        }

        private void btnNotListele_Click(object sender, EventArgs e)
        {
            var query = from item in db.TBLNOTLAR
                        select new
                        {
                            item.NOTID,
                            item.TBLOGRENCI.AD,
                            item.DERS,
                            item.SINAV1,
                            item.SINAV2,
                            item.SINAV3,
                            item.ORTALAMA,
                            item.DURUM,
                        };
            dataGridView1.DataSource = query.ToList();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            TBLOGRENCI t = new TBLOGRENCI();
            t.AD = txtAd.Text;
            t.SOYAD = txtSoyad.Text;
            t.FOTOGRAF = txtFoto.Text;
            db.TBLOGRENCI.Add(t);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Eklendi");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            TBLOGRENCI t = new TBLOGRENCI();
            int id = int.Parse(txtOgrenciId.Text);
            var x = db.TBLOGRENCI.Find(id);
            db.TBLOGRENCI.Remove(x);
            db.SaveChanges();
            MessageBox.Show("silindi");
            

        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtOgrenciId.Text);
            var x = db.TBLOGRENCI.Find(id);
            x.AD = txtAd.Text;
            x.SOYAD = txtSoyad.Text;
            x.FOTOGRAF = txtFoto.Text;
            db.SaveChanges();
            MessageBox.Show("Öğrenci Bilgileri Başarıyla güncellendi.");

        }

        private void btnProcedure_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.NOTLAR();//prosedürü çağırdık.
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = db.TBLOGRENCI.Where(x => x.AD ==txtAd.Text & x.SOYAD==txtSoyad.Text).ToList();///bulma işlemi 
            
        }

        private void txtAd_TextChanged(object sender, EventArgs e)
        {
            string aranan = txtAd.Text;
            var degerler = from x in db.TBLOGRENCI
                           where x.AD.Contains(aranan)
                           select x;
            dataGridView1.DataSource = degerler.ToList();
        }

        private void BTNLINQENT_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked==true)
            {
                //asc-ascending
                List<TBLOGRENCI> liste1 = db.TBLOGRENCI.OrderBy(p => p.AD).ToList();
                dataGridView1.DataSource = liste1;
            }
            if (radioButton2.Checked==true)
            {
                //desc-descending
                List<TBLOGRENCI> liste2 = db.TBLOGRENCI.OrderByDescending(p => p.AD).ToList();
                dataGridView1.DataSource = liste2;
            }
            if (radioButton3.Checked==true)
            {
                //ilk 3 kayıt
                List<TBLOGRENCI> liste3 = db.TBLOGRENCI.OrderBy(p => p.ID).Take(3).ToList();
                dataGridView1.DataSource = liste3;
            }
            if (radioButton4.Checked==true)
            {
                //son 3 kayıt
                List<TBLOGRENCI> liste4 = db.TBLOGRENCI.OrderByDescending(p => p.ID).Take(3).ToList();
                dataGridView1.DataSource = liste4;
            }
            if (radioButton5.Checked==true)
            {
                //ıd=5 olan getir
                List<TBLOGRENCI> liste5 = db.TBLOGRENCI.Where(x => x.ID == 5).ToList();
                dataGridView1.DataSource = liste5;
            }
            if (radioButton6.Checked==true)
            {
                //adı a ile başlayan
                List<TBLOGRENCI> liste6 = db.TBLOGRENCI.Where(x => x.AD.StartsWith("a")).ToList();
                dataGridView1.DataSource = liste6;
            }
            if (radioButton7.Checked == true)
            {
                //adı t ile biren
                List<TBLOGRENCI> liste7 = db.TBLOGRENCI.Where(x => x.AD.EndsWith("t")).ToList();
                dataGridView1.DataSource = liste7;
            }
            if (radioButton8.Checked==true)
            {
                //öğrenci tablosunda değer var mı
                string mesaj;
                bool deger = db.TBLOGRENCI.Any();
                MessageBox.Show(deger==true ? mesaj="Öğrenci tablosunda değer var" : mesaj="öğrenci tablosunda değer Yok","Öğrenci Tablosunda Değer Var mı?",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            if (radioButton9.Checked==true)
            {
                //toplam öğrenci sayısı
                int sayi = db.TBLOGRENCI.Count();
                MessageBox.Show("Toplam "+sayi+" öğrenci kayıtlıdır.","Toplam Öğrenci Sayısı",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            if (radioButton10.Checked==true)
            {
                //Sınav 1 Toplam Puan
                int toplam = Convert.ToInt32(db.TBLNOTLAR.Sum(p => p.SINAV1));
                MessageBox.Show("1.sınav toplam puan " + toplam, "Saçma Sapan 1. sınav notları toplamı", MessageBoxButtons.OK, MessageBoxIcon.Information);
 
            }
            if (radioButton11.Checked==true)
            {
                //sınav 1 ortalaması
                //int toplam = Convert.ToInt32(db.TBLNOTLAR.Sum(p => p.SINAV1));
                //int sayi = db.TBLOGRENCI.Count();
                //int ort = toplam / sayi;
                //MessageBox.Show("1.sınav ortalama puan " + ort, "1. sınav notları ortalamsı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                int ort = Convert.ToInt32(db.TBLNOTLAR.Average(p => p.SINAV1));
                MessageBox.Show("1.sınav ortalama puan " + ort, "1. sınav notları ortalamsı", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            if (radioButton12.Checked==true)
            {
                //1.sınav ortalaması genel ortalamadan büyük olan değerler
                int ort = Convert.ToInt32(db.TBLNOTLAR.Average(x => x.SINAV1));
                var degerler = db.NOTLAR().Where(x => x.SINAV1 > ort);
                dataGridView1.DataSource = degerler.ToList();
            }
            if (radioButton13.Checked==true)
            {
                //En Yüksek Sınav1
                int maxSınav1 = Convert.ToInt32(db.TBLNOTLAR.Max(x => x.SINAV1));
                int minSınav1 = Convert.ToInt32(db.TBLNOTLAR.Min(x => x.SINAV1));
                MessageBox.Show("En yüksek 1.sınav notu " + maxSınav1+ "\n En düşük 1.sınav notu " + minSınav1, "1. sınav notu en yüksek ve en düşük", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            if (radioButton14.Checked==true)
            {
                //En Yüksek ve En Düşük Sınav1 Öğrencisi
                var enyuksek = db.NOTLAR().Where(x => x.SINAV1 == (db.NOTLAR().Max(y => y.SINAV1)) || x.SINAV1 == (db.NOTLAR().Min(y => y.SINAV1))).ToList();

                var endusuk = db.NOTLAR().Where(x => x.SINAV1 == (db.NOTLAR().Min(y => y.SINAV1))).ToList();
                dataGridView1.DataSource = enyuksek;

                MessageBox.Show("En Yüksek Sınav Notunun Sahibi :"+enyuksek.First().ÖĞRENCİ+" \n En Düşük Notun Sahibi : "+endusuk.First().ÖĞRENCİ);

            }

        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            //var sorgu = from x in db.TBLNOTLAR
            //            join y in db.TBLOGRENCI
            //            on x.OGR equals y.ID
            //            select new
            //            {
            //                Ad = y.AD,
            //                Soyad = y.SOYAD,
            //                ÖğrenciAdSoyad =y.AD+" "+y.SOYAD,
            //                Sınav1 = x.SINAV1,
            //                Sınav2 = x.SINAV2,
            //                Sınav3 = x.SINAV3,
            //                Ortalama=x.ORTALAMA
            //            };
            var sorgu = from x in db.TBLNOTLAR
                        join y in db.TBLOGRENCI
                        on x.OGR equals y.ID
                        join z in db.TBLDERSLER
                        on x.DERS equals z.DERSID
                        select new
                        {
                            Ad = y.AD,
                            Soyad = y.SOYAD,
                            ÖğrenciAdSoyad = y.AD + " " + y.SOYAD,
                            Sınav1 = x.SINAV1,
                            Sınav2 = x.SINAV2,
                            Sınav3 = x.SINAV3,
                            Ders = z.DERSAD,
                            Ortalama = x.ORTALAMA
                        };
            dataGridView1.DataSource = sorgu.ToList();
        }
    }
}
