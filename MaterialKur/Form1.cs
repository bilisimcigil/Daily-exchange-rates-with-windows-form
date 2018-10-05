using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Xml;
namespace MaterialKur
{
    public partial class Form1 : MaterialSkin.Controls.MaterialForm
    {
        MaterialSkinManager skinManager;
        public Form1()
        {
            InitializeComponent();
            skinManager =MaterialSkinManager.Instance;
            SkinManager.AddFormToManage(this);
            SkinManager.Theme = MaterialSkinManager.Themes.DARK;
            SkinManager.ColorScheme = new ColorScheme(Primary.Green600, Primary.Green900,Primary.Green900 ,Accent.LightBlue200, TextShade.WHITE);
        }
       
        public DataTable source()
        {
            DataTable dt = new DataTable();
            // DataTable nesnemizi yaratıyoruz
            DataRow dr;
            // DataTable ın satırlarını tanımlıyoruz.
            dt.Columns.Add(new DataColumn("Adı", typeof(string)));
            dt.Columns.Add(new DataColumn("Kod", typeof(string)));
            dt.Columns.Add(new DataColumn("Döviz alış", typeof(string)));
            dt.Columns.Add(new DataColumn("Döviz satış", typeof(string)));
            dt.Columns.Add(new DataColumn("Efektif alış", typeof(string)));
            dt.Columns.Add(new DataColumn("Efektif Satış", typeof(string)));
            // DataTableımıza 6 sütün ekliyoruz ve değişken tiplerini tanımlıyoruz.
            XmlTextReader rdr = new XmlTextReader("http://www.tcmb.gov.tr/kurlar/today.xml");
            // XmlTextReader nesnesini yaratıyoruz ve parametre olarak xml dokümanın urlsini veriyoruz
            // XmlTextReader urlsi belirtilen xml dokümanlarına hızlı ve forward-only giriş imkanı sağlar.
            XmlDocument myxml = new XmlDocument();
            // XmlDocument nesnesini yaratıyoruz.
            myxml.Load(rdr);
            // Load metodu ile xml yüklüyoruz
            XmlNode tarih = myxml.SelectSingleNode("/Tarih_Date/@Tarih");
            XmlNodeList mylist = myxml.SelectNodes("/Tarih_Date/Currency");
            XmlNodeList adi = myxml.SelectNodes("/Tarih_Date/Currency/Isim");
            XmlNodeList kod = myxml.SelectNodes("/Tarih_Date/Currency/@Kod");
            XmlNodeList doviz_alis = myxml.SelectNodes("/Tarih_Date/Currency/ForexBuying");
            XmlNodeList doviz_satis = myxml.SelectNodes("/Tarih_Date/Currency/ForexSelling");
            XmlNodeList efektif_alis = myxml.SelectNodes("/Tarih_Date/Currency/BanknoteBuying");
            XmlNodeList efektif_satis = myxml.SelectNodes("/Tarih_Date/Currency/BanknoteSelling");
            // XmlNodeList cinsinden her bir nodu, SelectSingleNode metoduna nodların xpathini parametre olarak
            // göndererek tanımlıyoruz.
            materialLabel1.Text = tarih.InnerText.ToString() + " tarihli Merkez Bankası Kur Bilgileri";
            // datagridimin captionu ayarlıyoruz.
            int x = 19;
            /*  Burada xmlde bahsettiğim - bence-  mantık hatasından dolayı x gibi bir değişken tanımladım.
            bu x =19  DataTable a sadece 19 satır eklenmesini sağlıyor. çünkü xml dökümanında 19. node dan sonra
            güncel kur bilgileri değil Euro dönüşüm kurları var ve bu node dan sonra yapı ilk 18 node ile tutmuyor
            Bence ayrı bir xml dökümanda tutulması gerekirdi. 
            */
            for (int i = 0; i < x; i++)
            {
                dr = dt.NewRow();
                dr[0] = adi.Item(i).InnerText.ToString(); // i. adi nodunun içeriği
                                                          // Adı isimli DataColumn un satırlarını  /Tarih_Date/Currency/Isim node ları ile dolduruyoruz.
                dr[1] = kod.Item(i).InnerText.ToString();
                // Kod satırları
                dr[2] = doviz_alis.Item(i).InnerText.ToString();
                // Döviz Alış
                dr[3] = doviz_satis.Item(i).InnerText.ToString();
                // Döviz  Satış
                dr[4] = efektif_alis.Item(i).InnerText.ToString();
                // Efektif Alış
                dr[5] = efektif_satis.Item(i).InnerText.ToString();
                // Efektif Satış.
                dt.Rows.Add(dr);
            }
            // DataTable ımızın satırlarını 18 satır olacak şekilde dolduruyoruz
            // gerçi yine web mastırın insafı devreye giriyor:).
            // yukarıda bahsettiğim sorun.
            return dt;
            // DataTable ı döndürüyoruz.
        }
        public List<sourceiki> 
        {
               listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
 
            listView1.Columns.Add("ADI", 100);
            listView1.Columns.Add("SOYADI", 70);
            listView1.Columns.Add("TELEFONU", 70);
        }
    private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource= source();
            
        }
    }
}
