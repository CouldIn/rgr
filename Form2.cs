using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RGR_PROECTIROVANIE
{
    public partial class Form2 : Form
    {
        string ASCII;
        public Form2(string result, double imageHeight)
        {
            ASCII = result;
            InitializeComponent();
            textBox1.Font = new Font(FontFamily.GenericMonospace, 3, FontStyle.Regular);
            textBox1.Text = result;
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if(e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(ASCII);
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                button1_Click(null, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Сохранить как...";
            sfd.OverwritePrompt = true; // показывать ли "Перезаписать файл" если пользователь указывает имя файла, который уже существует
            sfd.CheckPathExists = true; // отображает ли диалоговое окно предупреждение, если пользователь указывает путь, который не существует
                                        // фильтр форматов файлов
            sfd.Filter = "Text Files(*.TXT)|*.txt|All files (*.*)|*.*";
            sfd.ShowHelp = true; // отображается ли кнопка Справка в диалоговом окне
                                 // если в диалоге была нажата кнопка ОК
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamWriter sr = new StreamWriter(sfd.FileName);
                    sr.Write(ASCII);
                    MessageBox.Show("Сохранено");
                    sr.Close();
                }
                catch // в случае ошибки выводим MessageBox
                {
                    MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(ASCII);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
