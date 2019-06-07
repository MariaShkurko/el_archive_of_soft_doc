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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(".\\Электронный каталог"))
                Directory.CreateDirectory(".\\Электронный каталог"); //если корневой папки не существует - создаем
            if (!File.Exists("log.txt"))
            {
                using (StreamWriter sw = File.CreateText("log.txt")) //если журнала нет - создаем
                {
                    DateTime curDate = DateTime.Now;
                    sw.WriteLine("Журнал Электронного архива");
                    sw.WriteLine("Создан: " + curDate);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "Admin") && (textBox2.Text == "12345"))
            {
                Form4 form4 = new Form4();
                form4.Show();
                this.Hide();

                using (StreamWriter sw = new StreamWriter("log.txt", true)) //запись в журнал
                {
                    DateTime curDate = DateTime.Now;
                    sw.WriteLine("-------------------------------------------------------");
                    sw.WriteLine("Вход: " + textBox1.Text + " " + curDate);
                    sw.WriteLine("-------------------------------------------------------");
                }
            }
            else MessageBox.Show("Неверный пароль или логин");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char)e.KeyChar == (Char)Keys.Back) return;
            if (char.IsDigit(e.KeyChar) || char.IsLetter(e.KeyChar)) return;
            e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char)e.KeyChar == (Char)Keys.Back) return;
            if (char.IsDigit(e.KeyChar) || char.IsLetter(e.KeyChar)) return;
            e.Handled = true;
        }
    }
}
