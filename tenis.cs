using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tenis
{
    public partial class Form1 : Form
    {
        int startx = 466;  //pocatecni pozice micku
        int starty = 284;
        int krokx = 6;    //krok micku
        int kroky = 6;
        int startp = 254; //pocatecni pozice palek
        int c1 = 10;       // krok palek
        int c2 = 10;
        int body1 = 0;
        int body2 = 0;
        int auto = 5;   //krok automaticke palky
        int rndx;       //nahodny smer micku
        int rndy;
        int odpal = 0;  //pocet odpalu
        int casp = 17;  //interal casovace 2
         public Form1()
         {
             InitializeComponent();
            radioButton2.Checked = (true);
            player1.Top = startp;
            player2.Top = startp;
        }

        public int rand()   //generovani nahodne smeru micku
        {
            do
            {
                Random rnd = new Random();
                rndx = rnd.Next(-1, 2);
            } while (rndx == 0);
            do
            {
                Random rnd = new Random();
                rndy = rnd.Next(-1, 2);
            } while (rndy == 0);
            krokx = krokx * rndx;
            kroky = kroky * rndy;
            return 0;
        }
        public int res()    //reset hry
        {
            body1 = 0;
            body2 = 0;
            set.Visible = (true);
            set.Enabled = (true);
            groupBox1.Enabled = (true);
            groupBox2.Enabled = (true);
            label3.Text = ("0");
            label4.Text = ("0");
            nuluj();
            return 0;
        }
        public int nuluj()  //reset kola
        {
            timer1.Enabled = (false);
            timer2.Enabled = (false);
            casp = 17;
            timer2.Interval = casp;
            ball.Left = startx;
            ball.Top = starty;
            player1.Top = startp;
            player2.Top = startp;
            krokx = 6;
            kroky = 6;
            rand();
            if (body1 > 4)
            {
                label5.Text = string.Format("Hráč 1 vyhrál {0}:{1}", body1, body2);
                label5.Visible = (true);
                res();
            }
            else if (body2 > 4)
            {
                label5.Text = string.Format("Hráč 2 vyhrál {1}:{0}", body1, body2);
                label5.Visible = (true);
                res();
            }
            return 0;
        }
         private void Form1_Load(object sender, EventArgs e)
         {
            timer1.Enabled = (false);
            timer2.Enabled = (false);
            // this.Focus();
        }
         private void Timer1_Tick(object sender, EventArgs e)
         {      //pohyb micku
            Invalidate();
            ball.Left += krokx;
            ball.Top += kroky;
            if (ball.Bounds.IntersectsWith(BorderBot.Bounds))
            { kroky = kroky * (-1); }
            else if (ball.Bounds.IntersectsWith(BorderTop.Bounds))
            { kroky = kroky * (-1); }
            else if (ball.Bounds.IntersectsWith(player1.Bounds))
            {
                krokx = krokx * (-1)+1; 
                ball.Left = 53;
                odpal += 1;
            }
            else if (ball.Bounds.IntersectsWith(player2.Bounds))
            {
                krokx = krokx * (-1)-1;
                ball.Left = 903;
                odpal += 1;
            }
            else if (ball.Bounds.IntersectsWith(BorderRight.Bounds))
            {
                body1 += 1;
                label3.Text = string.Format("{0}", body1);
                nuluj();
            }
            else if (ball.Bounds.IntersectsWith(BorderLeft.Bounds))
            {
                body2 += 1;
                label4.Text = string.Format("{0}", body2);
                nuluj();
            }
            if (odpal == 3) //zpomaleni automaticke palky
            {
                odpal = 0;
                casp = casp + 8;
                timer2.Stop();
                timer2.Interval = casp;
                timer2.Start();
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {       //pohyb automaticke palky
            if (!radioButton2.Checked)
            {
                if (radioButton3.Checked)
                {
                    if (kroky > 0 && !player1.Bounds.IntersectsWith(BorderTop.Bounds))
                        player1.Top += auto;
                    else if (kroky < 0 && !player1.Bounds.IntersectsWith(BorderBot.Bounds))
                        player1.Top -= auto;
                }
                else if (radioButton4.Checked)
                {
                    if (kroky > 0 && !player2.Bounds.IntersectsWith(BorderBot.Bounds))
                        player2.Top += auto;
                    else if (kroky < 0 && !player2.Bounds.IntersectsWith(BorderTop.Bounds))
                        player2.Top -= auto;
                }
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {       
            if (e.KeyCode == Keys.P)  //pauza
            {
                set.Visible = (true);
                set.Enabled = (true);
                groupBox1.Enabled = (false);
                groupBox2.Enabled = (false);
                timer1.Enabled = false;
                timer2.Enabled = false;
            }   //ovladani palek
            else if (e.KeyCode == Keys.Down && !player2.Bounds.IntersectsWith(BorderBot.Bounds) && !radioButton4.Checked)
            { player2.Top += c2; }
            else if (e.KeyCode == Keys.Up && !player2.Bounds.IntersectsWith(BorderTop.Bounds) && !radioButton4.Checked)
            { player2.Top -= c2; }
            else if (e.KeyCode == Keys.W && !player1.Bounds.IntersectsWith(BorderTop.Bounds) && !radioButton3.Checked)
            { player1.Top -= c1; }
            else if (e.KeyCode == Keys.S && !player1.Bounds.IntersectsWith(BorderBot.Bounds) && !radioButton3.Checked)
            { player1.Top += c1; }
            else if (e.KeyCode == Keys.Space)
            {       //start kola
                timer1.Enabled = true;
                timer2.Enabled = true;
                set.Visible = (false);
                set.Enabled = (false);
                label5.Visible = (false);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {       //resetovaci tlacitko
            res();
            label5.Visible = (false);
        }
        private void button2_Click(object sender, EventArgs e)
        {       //startovaci tlacitko
            if (radioButton2.Checked || radioButton3.Checked || radioButton4.Checked)
            {
                set.Visible = (false);
                set.Enabled = (false);
                timer1.Enabled = (true);
                timer2.Enabled = (true);
                label5.Visible = (false);
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {   //volba poctu hracu
            if (radioButton2.Checked)
            {
                groupBox2.Enabled = (false);
            }
            else if (radioButton1.Checked)
            {
                groupBox2.Enabled = (true);
            }
        }
        private void cit1_ValueChanged(object sender, EventArgs e)
        {       //ovladani citliosti hrace 1
            c1 = cit1.Value;
        }
        private void cit2_ValueChanged(object sender, EventArgs e)
        {       //ovladani citliosti hrace 2
            c2 = cit2.Value;
        }
    }
}
