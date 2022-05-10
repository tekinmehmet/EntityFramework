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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                var sorgu = db.TBLNOTLAR.Where(x => x.SINAV1 < 50).ToList();
                dataGridView1.DataSource = sorgu;
            }
            if (radioButton2.Checked == true)
            {
                var sorgu = db.TBLOGRENCI.Where(x => x.AD == "Ali");
                dataGridView1.DataSource = sorgu.ToList();
            }
            if (radioButton3.Checked == true)
            {
                var sorgu = db.TBLOGRENCI.Where(x => x.AD == textBox1.Text || x.SOYAD == textBox1.Text);
                dataGridView1.DataSource = sorgu.ToList();
            }
            if (radioButton4.Checked == true)
            {
                //var sorgu = db.TBLOGRENCI.Select(x => x);//tüm tabloyu listeler
                //var sorgu = db.TBLOGRENCI.Select(x => x.AD);//tablodaki adın uzunluklarını getirir.
                var sorgu = db.TBLOGRENCI.Select(x => new { Soyad = x.SOYAD });
                dataGridView1.DataSource = sorgu.ToList();
            }
            if (radioButton5.Checked == true)
            {
                var sorgu = db.TBLOGRENCI.Select(x => new { Ad = x.AD.ToUpper(), Soyad = x.SOYAD.ToLower() });
                dataGridView1.DataSource = sorgu.ToList();
            }
            if (radioButton6.Checked == true)
            {
                //var sorgu = db.TBLOGRENCI.Select(x => new { Ad = x.AD.ToLower(), Soyad = x.SOYAD.ToUpper() }).Where(x => x.Ad.Contains("me"));//adında me olanlar
                var sorgu = db.TBLOGRENCI.Select(x => new { Ad = x.AD.ToLower(), Soyad = x.SOYAD.ToUpper() }).Where(x => x.Ad=="Mehmet");
                dataGridView1.DataSource = sorgu.ToList();
            }
            if (radioButton7.Checked == true)
            {
                var sorgu = db.TBLNOTLAR.Select(x =>
                 new
                 {
                     OgrenciAd=x.OGR,
                     Ortalama=x.ORTALAMA,
                     Durumu=x.DURUM==true ? "Geçti" : "Kaldı"
                     
                 });
                dataGridView1.DataSource = sorgu.ToList();
            }
            if (radioButton8.Checked==true)
            {
                var sorgu = db.TBLNOTLAR.SelectMany(x => db.TBLOGRENCI.Where(y => y.ID == x.OGR),(x,y) => new
                { 
                    y.AD,
                    x.ORTALAMA,
                    Durum=x.DURUM==true ? "Geçti" : "Kaldı"
                });

                dataGridView1.DataSource = sorgu.ToList();

            }
            if (radioButton9.Checked==true)
            {
                var sorgu = db.TBLOGRENCI.OrderBy(x => x.ID).Take(3);
                dataGridView1.DataSource = sorgu.ToList();
            }
            if (radioButton10.Checked == true)
            {
                var sorgu = db.TBLOGRENCI.OrderByDescending(x => x.ID).Take(3);
                dataGridView1.DataSource = sorgu.ToList();
            }
            if (radioButton11.Checked == true)
            {
                var sorgu = db.TBLOGRENCI.OrderBy(x => x.AD);
                dataGridView1.DataSource = sorgu.ToList();
            }
            if (radioButton12.Checked == true)
            {
                var sorgu = db.TBLOGRENCI.OrderBy(x => x.ID).Skip(5);
                dataGridView1.DataSource = sorgu.ToList();
            }
            label2.Text = db.TBLURUNLER.Count().ToString();//toplam ürün sayısı
            label2.Text = db.TBLURUNLER.Sum(x=>x.STOK).ToString();//toplam stok sayısı
            label2.Text = db.TBLURUNLER.Where(x=>x.AD=="Buzdolabı").Sum(x=>x.STOK).ToString();//toplam buzdolabı stok sayısı
            label2.Text = db.TBLURUNLER.Count(x => x.AD == "Buzdolabı").ToString();//toplam buzdolabı sayısı
            label2.Text = db.TBLURUNLER.Average(x=>x.FIYAT).ToString();//ortalama ürün fiyatı
            label2.Text = db.TBLURUNLER.Where(x => x.AD == "Buzdolabı").Average(x=>x.FIYAT).ToString();//ortalama buzdolabı ürün fiyatı
            label2.Text = (from x in db.TBLURUNLER
                           orderby x.STOK descending
                           select x.AD).First();//en çok stoğu olan değer gelecek.
        }
    }
}
