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
    public partial class Form8 : Form
    {
        public int printed = 0;
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("log.txt", Encoding.UTF8);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                listBox1.Items.Add(line);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                //Вызываем встроенный метод для начала печати
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            var g = e.Graphics;
            // max высота текста
            int max_height = e.PageBounds.Height;
            int height = 0; // Отпечатано по высоте
            int temp; // Размер одной строки

            for (; printed < listBox1.Items.Count; ++printed)
            {
                temp = TextRenderer.MeasureText((string)listBox1.Items[printed], this.Font).Height;

                if (height + temp + 5 > max_height)
                    break;

                g.DrawString(
                    (string)listBox1.Items[printed],
                    this.Font,
                    Brushes.Black,
                    new Rectangle(
                        e.PageBounds.X,
                        height,
                        e.PageBounds.Width,
                        temp
                    )
                );

                height += temp + 5;
            }

            e.HasMorePages = printed != listBox1.Items.Count;
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (printed != 0)
                e.Cancel = true;
        }

        private void printDocument1_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printed = 0;
        }
    }
}
