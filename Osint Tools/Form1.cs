using System;
using System.Threading;
using System.Windows.Forms;
using EbubekirBastamatxtokuma;
using OpenQA.Selenium.Chrome;

namespace Osint_Tools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        string dgr = "";
        Thread th; ChromeDriver drv; OpenFileDialog op;
        string[] urller = { "https://twitter.com/", "https://www.facebook.com/" , "https://www.instagram.com/", "https://github.com/" };
        private void button2_Click(object sender, EventArgs e)
        {
            op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                th = new Thread(verioku); th.Start();
            }
        }
        private void verioku()
        {
            BekraTxtOkuma.Txtİmport(op.FileName, listBox1, false);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            th = new Thread(tara); th.Start();
        }
        private void tara()
        {

            drv = new ChromeDriver();
            for (int i = 0; i < urller.Length; i++)
            {
                dgr = urller[i].ToString();
                for (int j = 0; j < listBox1.Items.Count; j++)
                {
                    if (dgr == "https://twitter.com/")
                    {
                        urlac(urller[i].ToString() + listBox1.Items[j].ToString());
                        Thread.Sleep(2000);
                        if (drv.PageSource.IndexOf("Böyle bir hesap yok") != -1)
                        {
                            listBox2.Items.Add("Bu twitter Kullanıcı Adı : " + listBox1.Items[j].ToString() + " Mevcut Değil..");
                        }
                        else if (drv.PageSource.IndexOf("Hesap askıya alındı") != -1)
                        {
                            listBox2.Items.Add("Bu twitter Kullanıcı Adı : " + listBox1.Items[j].ToString() + " Askıya Alındı..");
                        }
                        else
                        {
                            listBox2.Items.Add("Bu twitter Kullanıcı Adı : " + listBox1.Items[j].ToString() + " Mevcut");
                        }
                    }
                    else if (dgr == "https://www.facebook.com/")
                    {
                        urlac(urller[i].ToString() + listBox1.Items[j].ToString());
                        if (drv.PageSource.IndexOf("Bu sayfaya ulaşılamıyor") != -1)
                        {
                            listBox2.Items.Add("Bu facebook Kullanıcı Adı : " + listBox1.Items[j].ToString() + " Mevcut Değil..");
                        }
                        else
                        {
                            listBox2.Items.Add("Bu facebook Kullanıcı Adı : " + listBox1.Items[j].ToString() + " Mevcut");
                        }
                    }
                    else if (dgr == "https://www.instagram.com/")
                    {
                        urlac(urller[i].ToString() + listBox1.Items[j].ToString());
                        if (drv.PageSource.IndexOf("Üzgünüz, bu sayfaya ulaşılamıyor.") != -1)
                        {
                            listBox2.Items.Add("Bu instagram Kullanıcı Adı : " + listBox1.Items[j].ToString() + " Mevcut Değil..");
                        }
                        else
                        {
                            listBox2.Items.Add("Bu instagram Kullanıcı Adı : " + listBox1.Items[j].ToString() + " Mevcut");
                        }
                    }
                    else if (dgr == "https://github.com/")
                    {
                        urlac(urller[i].ToString() + listBox1.Items[j].ToString());
                        if (drv.PageSource.IndexOf("Find code, projects, and people on GitHub:") != -1)
                        {
                            listBox2.Items.Add("Bu github Kullanıcı Adı : " + listBox1.Items[j].ToString() + " Mevcut Değil..");
                        }
                        else
                        {
                            listBox2.Items.Add("Bu github Kullanıcı Adı : " + listBox1.Items[j].ToString() + " Mevcut");
                        }
                    }
                }
                
            }

        }

        private void urlac(string url)
        {
            drv.Navigate().GoToUrl(url);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BastamaTextSave.txt_save.txtlistsave(listBox2, "Veriler Akatarıldı....", Application.StartupPath + "\\", "osint");
        }
    }
}
