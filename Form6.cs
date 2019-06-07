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

namespace Электронный_архив
{
    public partial class Form6 : Form
    {
        public static string filename_new;
        public static string filename_old;
        public static string folder; //путь для файла

        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //ок
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Не введен шифр проекта!");
            }
            else
            {
                folder = ".\\Электронный каталог\\" + textBox1.Text + "\\"; //путь для файла
                if (!Directory.Exists(folder))
                {
                    MessageBox.Show("Такого проекта не существует!");
                }
                else
                {
                    listBox1.Enabled = true;
                    listBox1.Items.Clear();
                    DirectoryInfo dir = new DirectoryInfo(folder);
                    // Для извлечения имени файла используется цикл foreach и свойство files.name
                    foreach (FileInfo files in dir.GetFiles())
                    {
                        listBox1.Items.Add(Path.GetFileNameWithoutExtension(files.Name));
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) //обзор
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            filename_new = openFileDialog1.FileName;
            //выводим полный путь выбранного файла
            label7.Text = filename_new;
            button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e) //внесение изменений
        {
            try
            {
                string name = filename_old;
                int number_update = Convert.ToInt16(name.Substring(name.Length - 1)) + 1;
                name = name.Substring(0, name.Length - 1);

                string f_name = name + number_update + Path.GetExtension(filename_new);

                File.Copy(filename_new, Path.Combine(folder, f_name));
                try
                {
                    using (StreamWriter sw = new StreamWriter("log.txt", true)) //запись в журнал
                    {
                        DateTime curDate = DateTime.Now;
                        sw.WriteLine("Изменение в " + filename_old);
                        sw.WriteLine("Новая версия: " + number_update);
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка записи в журнал");
                    return;
                }
                this.Close();
            }
            catch
            {
                MessageBox.Show("Error");
                this.Close();
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e) //отмена
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char)e.KeyChar == (Char)Keys.Back) return; //разрешен ввод BackSpace
            if (char.IsDigit(e.KeyChar) || char.IsLetter(e.KeyChar) || e.KeyChar == 45) return; //разрешен ввод цифр, букв и дефиса
            e.Handled = true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            filename_old = listBox1.SelectedItem.ToString();
            button2.Enabled = true;
        }
    }
}
