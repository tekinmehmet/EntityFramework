using EntityFrameworkSonKisim.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkSonKisim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbOgrenciNotEntities db = new DbOgrenciNotEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            //var degerler = db.tblogrencıler.orderby(x => x.ogrsehır).groupby(x => x.ogrsehır).select(z =>
            //new
            //{
            //    şehir = z.key,
            //    toplam = z.count()
            //});
            //   var degerler = db.TBLOGRENCILER.OrderBy(x => x.OGRSEHIR).GroupBy(x => x.OGRSEHIR).Select(z =>
            //new
            //{
            //    Şehir = z.Key,
            //    Toplam = z.Count()
            //}).OrderByDescending(m=>m.Toplam);//dersek de en çok üyesi olandan en aaz üyesi olana göre gruplar
            //   dataGridView1.DataSource = degerler.ToList();
            //label1.Text = db.TBLNOTLAR.Max(x => x.SINAV1).ToString();//en yüksek sınav1
            //label1.Text = db.TBLNOTLAR.Max(x => x.ORTALAMA).ToString();//en yüksek ortalama
            label1.Text = db.TBLNOTLAR.Min(x => x.ORTALAMA).ToString();//en düşük ortalama
            label1.Text = db.TBLNOTLAR.Where(y => y.ORTALAMA < 50).Max(x => x.ORTALAMA).ToString();//kalanlar arasında en yüksek ortalamaya sahip çocuk
            //var degerler = db.TBLNOTLAR.Where(x=>x.ORTALAMA<50);
            //dataGridView1.DataSource = degerler.ToList();
        }
    }
}
