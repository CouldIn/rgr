using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;

namespace RGR_PROECTIROVANIE
{
    public partial class Form1 : Form
    {
        public string ASCII;
        public string filepath;

        public Form1(string[] args)
        {
            InitializeComponent();
            if (args.Length != 0)
            {
                OpenImage(args[0]);
            }
        }

        public void ButtonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filepath = ofd.FileName;
                OpenImage(ofd.FileName);
            }
        }

        public void OpenImage(string FileName)
        {
            try
            {
                pictureBox1.Image = new Bitmap(FileName);

                Size resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;

                int height = pictureBox1.Image.Size.Height;

                if (height < resolution.Height - 100)
                {
                    this.Height = pictureBox1.Image.Size.Height;
                    this.Width = pictureBox1.Image.Size.Width + 120;
                }
                else
                {
                    double k = Convert.ToDouble(resolution.Height - 100) / Convert.ToDouble(height);
                    this.Height = (int)(height * k);
                    this.Width = (int)(pictureBox1.Image.Size.Width * k) + 120;
                }

            }
            catch
            {
                MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ButtonConvert_Click(object sender, EventArgs e)
        {            
            if (pictureBox1.Image != null)
            {
                Bitmap input = new Bitmap(pictureBox1.Image);
                input = Jackalisate(input);                
                Bitmap grayBm = RGBtoGray(input);
                ASCII = imageToASCII(grayBm);
                showResult(ASCII, grayBm.Height);
            }
        }

        public Bitmap RGBtoGray(Bitmap input)
        {    
            Bitmap output = new Bitmap(input.Width, input.Height);
            for (int j = 0; j < input.Height; j++)
            {
                for (int i = 0; i < input.Width; i++)
                {
                    UInt32 pixel = (UInt32)(input.GetPixel(i, j).ToArgb());
                    float R = (float)((pixel & 0x00FF0000) >> 16);
                    float G = (float)((pixel & 0x0000FF00) >> 8);
                    float B = (float)(pixel & 0x000000FF);
                    R = G = B = (R + G + B) / 3.0f;
                    UInt32 newPixel = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);
                    output.SetPixel(i, j, Color.FromArgb((int)newPixel));
                }
            }
            return output;
        }

        public Bitmap Jackalisate(Image input)
        {
            Bitmap bm = new Bitmap(input);
            int curWigth = bm.Width;
            int curHeight = bm.Height;

            Size resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
            double k = Convert.ToDouble(resolution.Height) / Convert.ToDouble(bm.Height) / 2;

            int newWidth = (int)(bm.Width * k);
            int newHeight = resolution.Height / 2 / 2;

            Bitmap b = new Bitmap(newWidth, newHeight);
            Graphics g = Graphics.FromImage((Image)b);


            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(input, 0, 0, newWidth, newHeight);
            g.Dispose();

            return b;
        }

        public string imageToASCII(Bitmap bm)
        {            
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bm.Height; i++)
            {
                for (int j = 0; j < bm.Width; j++)
                {
                    int gettedPixel = bm.GetPixel(j, i).ToArgb();
                    float RGB = (float)(gettedPixel & 0x000000FF);
                    sb.Append(getASCII((int)RGB));
                }
                sb.Append("\r\n");
            }
            return sb.ToString();
        }

        public char getASCII(int RGB)
        {
            string ASCII = "#@W&HOL?I/=;:-.";
            char output = ASCII[RGB/18];
            return output;
        }

        public void SaveAsTxt(string FileName)
        {
            string s = FileName;
            s = s.Remove(s.Length - 3);
            s += "txt";
            MessageBox.Show(s);
            try
            {
                StreamWriter sr = new StreamWriter(FileName);
                sr.Write(ASCII);
                MessageBox.Show("Сохранено");
                sr.Close();
            }
            catch // в случае ошибки выводим MessageBox
            {
                MessageBox.Show("Невозможно сохранить текстовое изображение", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void showResult(string result, double height)
        {
            Form2 f = new Form2(result, height);
            f.Show();
        }
    }
}
