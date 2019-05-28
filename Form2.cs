using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Электронный_архив
{
    public partial class Form2 : Form
    {
        /* Переменные, которые будут хранить на протяжение работы программы логин и пароль. */
        public string id = string.Empty;
        public string password = string.Empty;
        private Users user = new Users(); // Экземпляр класса пользователей.

        public Form2()
        {
            InitializeComponent();
            LoadUsers(); // Метод десериализует класс.
        }

        private void LoadUsers()
        {
            try
            {
                FileStream fs = new FileStream("Users.dat", FileMode.Open);

                BinaryFormatter formatter = new BinaryFormatter();

                user = (Users)formatter.Deserialize(fs);

                fs.Close();
            }
            catch { return; }
        }

        private void EnterToForm()
        {
            for (int i = 0; i < user.Logins.Count; i++) // Ищем пользователя и проверяем правильность пароля.
            {
                if (user.Logins[i] == textBox1.Text && user.Passwords[i] == textBox2.Text)
                {
                    id = user.Logins[i];
                    password = user.Passwords[i];

                    Form4 form4 = new Form4();
                    form4.Show();
                    this.Close();

                    MessageBox.Show("Вы вошли в систему!");                 
                }
                else if (user.Logins[i] == textBox1.Text && textBox2.Text != user.Passwords[i])
                {
                    id = user.Logins[i];

                    MessageBox.Show("Неверный пароль!");
                }
            }

            if (id == "") { MessageBox.Show("Пользователь " + textBox1.Text + " не найден!"); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 127)
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char)e.KeyChar == (Char)Keys.Back) return;
            if (char.IsDigit(e.KeyChar) || char.IsLetter(e.KeyChar)) return;
            e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EnterToForm();
        }
    }
}
