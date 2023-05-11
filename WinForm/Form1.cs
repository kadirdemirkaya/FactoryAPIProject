using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            // Set the file dialog to filter for graphics files.
            // Dosya iletişim kutusunu grafik dosyalarını filtrelemek için ayarlayın.
            this.openFileDialog.Filter =
                //"Images (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;.PNG|" +
                "All files (*.*)|*.*";

            // Allow the user to select multiple images.
            // Kullanıcının birden çok görüntü seçmesine izin verin.
            this.openFileDialog.Multiselect = true;
            this.openFileDialog.Title = "Yüklenecek dosyalara göz atın.";
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            //Çoklu ve Tekli Gönderim Resim Yükleme
            //Multiple and Single Send Image Upload
            DialogResult dr = this.openFileDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                try
                {
                    HttpClient httpClient = new HttpClient();
                    foreach (String file in openFileDialog.FileNames)
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        byte[] fileContents = File.ReadAllBytes(fileInfo.FullName);
                        MultipartFormDataContent multiPartContent = new MultipartFormDataContent();
                        ByteArrayContent byteArrayContent = new ByteArrayContent(fileContents);
                        byteArrayContent.Headers.Add("Content-Type", "application/octet-stream");
                        multiPartContent.Add(byteArrayContent, "\"files\"", string.Format("\"{0}\"", fileInfo.Name));
                        string imgExtension = fileInfo.Extension;
                        HttpResponseMessage response = await httpClient.PostAsync(txtWebAPIBaseAddress.Text, multiPartContent);
                        string data = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Resim gönderildi!");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Resim gönderilemedi!");
                }
            }
        }
    }
}
