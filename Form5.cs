using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Электронный_архив
{
    public partial class Form5 : Form
    {
        public static string filename;
        public static string type_of_document;

        public Form5()
        {
            InitializeComponent();
            openFileDialog1.Filter = "All files(*.*)|*.*";
        }

        private void button1_Click(object sender, EventArgs e) //обзор
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            filename = openFileDialog1.FileName;
            //выводим полный путь выбранного файла
            label7.Text = filename;
            // читаем файл в строку
            //string fileText = File.ReadAllText(filename);
        }

        private void button2_Click(object sender, EventArgs e) //внесение документа
        {
            if (filename == null)
            {
                MessageBox.Show("Не выбран файл!");
            }
            else
            {
                if ((textBox1.Text == "") || (textBox2.Text == "") || (textBox3.Text == "") || (textBox4.Text == "") || (type_of_document == ""))
                {
                    MessageBox.Show("Заполнены не все поля!");
                }
                else
                {
                    try
                    {
                        string folder = ".\\Электронный каталог\\" + textBox1.Text + "\\"; //путь для файла
                        if (!Directory.Exists(folder))
                            Directory.CreateDirectory(folder); //если папки с введенным шифром не существует - создаем
                        string[] filePaths = Directory.GetFiles(folder);
                        int fileCount = filePaths.Length + 1; //порядковый номер файла
                        string f_name = textBox1.Text + " " + type_of_document + " " + fileCount + " " + " 0" + Path.GetExtension(filename); //имя файла

                        File.Copy(filename, Path.Combine(folder, f_name));
                        try
                        {
                            using (StreamWriter sw = new StreamWriter("log.txt", true)) //запись в журнал
                            {
                                DateTime curDate = DateTime.Now;
                                sw.WriteLine(textBox1.Text + " " + type_of_document + " " + fileCount + " " + textBox2.Text + "   " + textBox3.Text + "   " + curDate + "  " + textBox4.Text + "  " +
                                    filename + "   version 0");
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
                        MessageBox.Show("Ошибка");
                        this.Close();
                        return;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) //отмена
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) //шифр проекта
        {
            if ((char)e.KeyChar == (Char)Keys.Back) return; //разрешен ввод BackSpace
            if (char.IsDigit(e.KeyChar) || char.IsLetter(e.KeyChar) || e.KeyChar == 45) return; //разрешен ввод цифр, букв и дефиса
            e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e) //Заказчик
        {
            if ((char)e.KeyChar == (Char)Keys.Back) return; //разрешен ввод BackSpace
            if (char.IsDigit(e.KeyChar) || char.IsLetter(e.KeyChar) || e.KeyChar == 45 || e.KeyChar == 46) return; //разрешен ввод цифр, букв, точки и дефиса
            e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e) //руководитель проекта
        {
            if ((char)e.KeyChar == (Char)Keys.Back) return; //разрешен ввод BackSpace
            if (char.IsLetter(e.KeyChar) || e.KeyChar == 45 || e.KeyChar == 46) return; //разрешен ввод букв, точки и дефиса
            e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e) //децимальный номер
        {
            if ((char)e.KeyChar == (Char)Keys.Back) return; //разрешен ввод BackSpace
            if (char.IsDigit(e.KeyChar) || char.IsLetter(e.KeyChar) || e.KeyChar == 45 || e.KeyChar == 46) return; //разрешен ввод цифр, букв, точки и дефиса
            e.Handled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //выбор вида документа
        {
            type_of_document = comboBox1.SelectedItem.ToString();
        }
    }
}
