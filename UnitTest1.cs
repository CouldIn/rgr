using Microsoft.VisualStudio.TestTools.UnitTesting;
using RGR_PROECTIROVANIE;
using System.Drawing;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRGBtoGray()
        {
            Form1 f = new Form1(new string[] { "C:\\Users\\CouldIn\\Desktop\\Отчет\\Без названия.jpg" });
            Bitmap input = new Bitmap(f.pictureBox1.Image);
            Bitmap gray = f.RGBtoGray(input);
            for (int j = 0; j < input.Height; j++)
            {
                for (int i = 0; i < input.Width; i++)
                {
                    int pixel = (int)(gray.GetPixel(i, j).ToArgb());
                    float R = (float)((pixel & 0x00FF0000) >> 16);
                    float G = (float)((pixel & 0x0000FF00) >> 8);
                    float B = (float)(pixel & 0x000000FF);
                    Assert.AreEqual(R, G, B);
                }
            }
        }

        [TestMethod]
        public void TestGetASCII()
        {
            Form1 f = new Form1(new string[] { "C:\\Users\\CouldIn\\Desktop\\Отчет\\Без названия.jpg" });
            int[] test = { 17, 18, 36, 54, 72, 90, 108, 126, 144, 162, 180, 198, 216, 234, 252 };
            string ASCII = "#@W&HOL?I/=;:-.";
            for (int i = 0; i < 14; i++)
            {
                Assert.AreEqual(ASCII[i], f.getASCII(test[i]));
            }
        }

        [TestMethod]
        public void TestOpen()
        {
            Form1 f = new Form1(new string[] { "C:\\Users\\CouldIn\\Desktop\\Отчет\\Без названия.jpg" });
            f.ButtonOpen_Click(null, null);
            Assert.AreNotEqual(f.filepath, null);
        }

        [TestMethod]
        public void TestConvert()
        {
            Form1 f = new Form1(new string[] { "C:\\Users\\CouldIn\\Desktop\\Отчет\\Без названия.jpg" });
            f.ButtonConvert_Click(null, null);
            Assert.AreNotEqual(f.ASCII, null);
        }

        [TestMethod]
        public void TestJacalisate()
        {
            Form1 f = new Form1(new string[] { "C:\\Users\\CouldIn\\Desktop\\Отчет\\Без названия.jpg" });
            Bitmap bm = f.Jackalisate(f.pictureBox1.Image);
            Assert.AreNotEqual(bm, null);
        }
    }
}