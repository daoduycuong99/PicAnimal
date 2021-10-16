using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using xNet;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace Animals
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string Get(HttpRequest http, string url = "")
        {
            http = new HttpRequest();
            var html = http.Get(url).ToString();
            return html;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = "https://wallpaperscraft.com";
            string url = "https://wallpaperscraft.com/catalog/animals";
            HttpRequest http = new HttpRequest();
            var html = Get(http, url);
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);
            var list = document.DocumentNode.SelectNodes("//a[@class='wallpapers__link']").ToList();
            try {
                for (int i = 0; i < list.Count; i++)
                {
                    var href = list[i].Attributes["href"].Value;
                    var href1 = str + href;

                    HttpRequest http1 = new HttpRequest();
                    var html1 = Get(http1, href1);
                    HtmlDocument document1 = new HtmlDocument();
                    document1.LoadHtml(html1);
                    var list1 = document1.DocumentNode.SelectNodes("//section[2]/div[2]/div/ul/li/a").ToList();
                    for (int j = 0; j < list1.Count; j++)
                    {
                        var href2 = list1[j].Attributes["href"].Value;
                        var link = str + href2;

                        HttpRequest http2 = new HttpRequest();
                        var html2 = Get(http2, link);
                        HtmlDocument document2 = new HtmlDocument();
                        document2.LoadHtml(html2);
                        var list2 = document2.DocumentNode.SelectNodes("//a[@class='gui-button gui-button_full-height']").ToList();
                        for (int u = 0; u < list2.Count; u++)
                        {

                            var linkimage = list2[u].Attributes["href"].Value;
                            if (!string.IsNullOrEmpty(linkimage))
                            {
                                richTextBox1.AppendText(linkimage + "\n");
                            }


                           

                        }
                    }
                }
            } catch {
                
            }
           
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] link = richTextBox1.Lines;
            for(int  i =0; i<link.Length; i++)
            using (WebClient client = new WebClient())
            {
                string namefile = i + ".png";
                if(link[i] != null)
                    {
                        client.DownloadFile(link[i], @"C:\image\" + namefile);
                    }
                
                
            }
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            pictureBox1.ImageLocation = e.LinkText;
        }
    }
}
