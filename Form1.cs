using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebtsToMp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "https://static.wouzee.com/nas01/video/133c95e/8ae41002e3313b6e0bbc642177c5d2d2_nhd.mp4/seg-1-v1-a1.ts";
            GetUrlData(this.textBox1.Text);
        }

        private void GetUrlData(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            var dataWithAllDatasMerged = new System.IO.MemoryStream();
            string baseurl = url;

            var status = ((System.Net.HttpWebResponse)response).StatusDescription;

            int chunk = 1;

            while (true)
            {

                url = baseurl.Substring(0, baseurl.IndexOf("seg-"))+ "seg-" + chunk.ToString() + "-v1-a1.ts";

                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "GET";

                // if the URI doesn't exist, an exception will be thrown here...
                using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
                {
                    using (Stream responseStream = httpResponse.GetResponseStream())
                    {
                        using (FileStream localFileStream =
                            new FileStream(Path.Combine(@"C:\\tmp\\", @"alvaro.mp4"), FileMode.Append))
                        {
                            var buffer = new byte[4096];
                            long totalBytesRead = 0;
                            int bytesRead;

                            while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                totalBytesRead += bytesRead;
                                localFileStream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }

                chunk += 1;

            }
            //while (status == "OK")
            //{
            //    Stream data;
            //    data = response.GetResponseStream();
            //    response.GetResponseStream().CopyTo(dataWithAllDatasMerged);

            //    chunk += 1;
            //    url = baseurl.Substring(0, baseurl.IndexOf("seg-")) + chunk.ToString() + "-v1-a1.ts";

            //    request = WebRequest.Create(url);

            //    try
            //    {
            //        response = request.GetResponse();
            //    }
            //    catch (Exception)
            //    {
            //        if (((System.Net.HttpWebResponse)response).StatusDescription != "OK")
            //            return;
            //    }


            //}
        }
    }
}
