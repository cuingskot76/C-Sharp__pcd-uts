using System;
using System.Drawing;
using System.Windows.Forms;

namespace klasifikasikematangan
{
    public partial class Form1 : Form
    {
        Bitmap bm;
        Bitmap bm1;

        public Form1()
        {

            InitializeComponent();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "bm";
            ofd.Filter = "image files|*.png*; *.jpg*; *.bmp*; *.jpeg*;";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
                Bitmap bm = new Bitmap(pictureBox1.Image);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);

            pictureBox1.MouseMove += new MouseEventHandler(pictureBox1_MouseMove);

            pictureBox1.MouseEnter += new EventHandler(pictureBox1_MouseEnter);
            this.Controls.Add(pictureBox1);
        }
        int crpX, crpY, rectW, rectH;
        public Pen crpPen = new Pen(Color.White);
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                crpPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                crpX = e.X;
                crpY = e.Y;
            }
        }
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Cross;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                pictureBox1.Refresh();
                rectW = e.X - crpX;
                rectH = e.Y - crpY;
                Graphics g = pictureBox1.CreateGraphics();
                g.DrawRectangle(crpPen, crpX, crpY, rectW, rectH);
                g.Dispose();
            }

        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label10.Text = "Dimensi : " + rectH + "," + rectW;
            bm1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bm1, pictureBox1.ClientRectangle);

            Bitmap crpImg = new Bitmap(rectW, rectH);
            for (int x = 0; x < rectW; x++)
            {
                for (int y = 0; y < rectH; y++)
                {
                    Color pxlclr = bm1.GetPixel(crpX + x, crpY + y);
                    crpImg.SetPixel(x, y, pxlclr);
                }
            }
            pictureBox2.Image = (Image)crpImg;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bm = (Bitmap)pictureBox2.Image;

            double jRed = 0, jGreen = 0, jBlue = 0;
            double Red = 0, Green = 0, Blue = 0;

            double jumlahPiksel = bm.Height * bm.Height;
            for (int x = 0; x < bm.Width - 1; x++)
            {
                for (int y = 0; y < bm.Height - 1; y++)
                {
                    Color c = bm.GetPixel(x, y);
                    Red = c.R;
                    Green = c.G;
                    Blue = c.B;

                    jRed = jRed + Red;
                    jGreen = jGreen + Green;
                    jBlue = jBlue + Blue;
                }
            }
            double hasilRed = jRed / jumlahPiksel;
            double hasilGreen = jGreen / jumlahPiksel;
            double hasilBlue = jBlue / jumlahPiksel;

            double bulatRed = Math.Round(hasilRed, 3);
            double bulatGreen = Math.Round(hasilGreen, 3);
            double bulatBlue = Math.Round(hasilBlue, 3);

            textBox1.Text = ("" + bulatRed);
            textBox2.Text = ("" + bulatGreen);
            textBox3.Text = ("" + bulatBlue);

            double hasilY = 0.2999 * hasilRed + 0.578 * hasilGreen + 0.114 * hasilBlue;
            double hasilCb = -0.16874 * hasilRed - 0.33126 * hasilGreen + 0.5 * hasilBlue;
            double hasilCr = 0.5 * hasilRed - 0.41869 * hasilGreen - 0.08131 * hasilBlue;

            double bulatY = Math.Round(hasilY, 3);
            double bulatCb = Math.Round(hasilCb, 3);
            double bulatCr = Math.Round(hasilCr, 3);

            textBox4.Text = ("" + bulatY);
            textBox5.Text = ("" + bulatCb);
            textBox6.Text = ("" + bulatCr);

            if (hasilCb >= -58 && hasilCb <= -35 && hasilCr >= 11 && hasilCr <= 35)
            {
                textBox7.Text = ("Matang");
            }
            else if (hasilCb >= -61 && hasilCb <= -31 && hasilCr >= -3 && hasilCr <= 4)
            {
                textBox7.Text = ("Setengah Matng");
            }
            else if (hasilCb >= -29 && hasilCb <= -10 && hasilCr >= -17 && hasilCr <= -10)
            {
                textBox7.Text = ("Mentah");
            }
            else
            {
                textBox7.Text = ("Tidak tahu Bang");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

    }
}
