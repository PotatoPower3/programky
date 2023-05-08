using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace krizovatka_schopp
{
    public partial class Krizovatka : Form
    {
        int[] tab = new int[] { 50, 150, 50, 50, 50, 150, 50, 50, 50, 150, 50, 50, 50 };
        int count =0;
        int end = 50;
        bool noc = false;
        bool norm = true;
        int stat = 1;

        
        public Krizovatka()
        {
            InitializeComponent();
            timer.Enabled = true;
        }
        
        private int allblack()
        {
            all_off();
            hlroc1.BackColor = Color.Black;
            hlroc2.BackColor = Color.Black;
            hllec1.BackColor = Color.Black;
            hllec2.BackColor = Color.Black;
            chhlc1.BackColor = Color.Black;
            chhlc2.BackColor = Color.Black;
            chhlc3.BackColor = Color.Black;
            chhlc4.BackColor = Color.Black;
            veroc1.BackColor = Color.Black;
            veroc2.BackColor = Color.Black;
            chvec1.BackColor = Color.Black;
            chvec2.BackColor = Color.Black;
            chvec3.BackColor = Color.Black;
            chvec4.BackColor = Color.Black;
            return 0;
        }
        private int all_off()
        {
            hlroz1.BackColor = Color.Black;
            hlroz2.BackColor = Color.Black;
            hlroo1.BackColor = Color.Black;
            hlroo2.BackColor = Color.Black;
            hlroc1.BackColor = Color.Red;
            hlroc2.BackColor = Color.Red;

            hllez1.BackColor = Color.Black;
            hllez2.BackColor = Color.Black;
            hlleo1.BackColor = Color.Black;
            hlleo2.BackColor = Color.Black;
            hllec1.BackColor = Color.Red;
            hllec2.BackColor = Color.Red;

            chhlc1.BackColor = Color.Red;
            chhlc2.BackColor = Color.Red;
            chhlc3.BackColor = Color.Red;
            chhlc4.BackColor = Color.Red;
            chhlz1.BackColor = Color.Black;
            chhlz2.BackColor = Color.Black;
            chhlz3.BackColor = Color.Black;
            chhlz4.BackColor = Color.Black;

            veroz1.BackColor = Color.Black;
            veroz2.BackColor = Color.Black;
            veroo1.BackColor = Color.Black;
            veroo2.BackColor = Color.Black;
            veroc1.BackColor = Color.Red;
            veroc2.BackColor = Color.Red;
            veprz1.BackColor = Color.Black;
            veprz2.BackColor = Color.Black;

            chvec1.BackColor = Color.Red;
            chvec2.BackColor = Color.Red;
            chvec3.BackColor = Color.Red;
            chvec4.BackColor = Color.Red;
            chvez1.BackColor = Color.Black;
            chvez2.BackColor = Color.Black;
            chvez3.BackColor = Color.Black;
            chvez4.BackColor = Color.Black;
            return 0;
        }
        private int hlready()
        {   //hlpro+hlprc
            hlroo1.BackColor = Color.Yellow;
            hlroo2.BackColor = Color.Yellow;
            return 0;
        }
        private int hlgo()
        {   //hlprz+hlchz
            hlroz1.BackColor = Color.LimeGreen;
            hlroz2.BackColor = Color.LimeGreen;
            hlroc1.BackColor = Color.Black;
            hlroc2.BackColor = Color.Black;
            chhlz1.BackColor = Color.LimeGreen;
            chhlz2.BackColor = Color.LimeGreen;
            chhlz3.BackColor = Color.LimeGreen;
            chhlz4.BackColor = Color.LimeGreen;
            chhlc1.BackColor = Color.Black;
            chhlc2.BackColor = Color.Black;
            chhlc3.BackColor = Color.Black;
            chhlc4.BackColor = Color.Black;
            return 0;
        }
        private int hlstop()
        {   //hlpro
            hlroc1.BackColor = Color.Black;
            hlroc2.BackColor = Color.Black;
            hlroo1.BackColor = Color.Yellow;
            hlroo2.BackColor = Color.Yellow;
            return 0;
        }
        private int hlleready()
        {   //hllec+hlleo
            hlleo1.BackColor = Color.Yellow;
            hlleo2.BackColor = Color.Yellow;
            return 0;
        }
        private int hllego()
        {   //hllez
            hllez1.BackColor = Color.LimeGreen;
            hllez2.BackColor = Color.LimeGreen;
            hllec1.BackColor = Color.Black;
            hllec2.BackColor = Color.Black;
            return 0;
        }
        private int hllestop()
        {   //hlleo+veprc
            hllec1.BackColor = Color.Black;
            hllec2.BackColor = Color.Black;
            hlleo1.BackColor = Color.Yellow;
            hlleo2.BackColor = Color.Yellow;
            return 0;
        }
        private int veready()
        {   //veroc+veroo
            veroo1.BackColor = Color.Yellow;
            veroo2.BackColor = Color.Yellow;
            return 0;
        }
        private int vego()
        {   //veroz+vechz
            veroc1.BackColor = Color.Black;
            veroc2.BackColor = Color.Black;
            veroz1.BackColor = Color.LimeGreen;
            veroz2.BackColor = Color.LimeGreen;
            chvec1.BackColor = Color.Black;
            chvec2.BackColor = Color.Black;
            chvec3.BackColor = Color.Black;
            chvec4.BackColor = Color.Black;
            chvez1.BackColor = Color.LimeGreen;
            chvez2.BackColor = Color.LimeGreen;
            chvez3.BackColor = Color.LimeGreen;
            chvez4.BackColor = Color.LimeGreen;
            return 0;
        }
        private int vestop()
        {   //veroo+vechc
            veroo1.BackColor = Color.Yellow;
            veroo2.BackColor = Color.Yellow;
            veroc1.BackColor = Color.Black;
            veroc2.BackColor = Color.Black;
            return 0;
        }
        private int hlvebl()
        {
            hllec1.BackColor = Color.Black;
            hllec2.BackColor = Color.Black;
            return 0;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            count++;
            switch (stat)
            {
                case 0:
                if (count == 50)
                {   //hlpro+hlprc
                    all_off();
                    hlready();
                }
                else if (count == 100)
                {   //hlprz+hlchz
                    all_off();
                    hlgo();
                }
                else if (count == 250)
                {   //hlpro
                    all_off();
                    hlstop();
                }
                else if (count == 300)
                {   //hllec+hlleo
                    all_off();
                    hlleready();
                }
                else if (count == 350)
                {   //hllez+veprz
                    all_off();
                    hllego();
                    veprz1.BackColor = Color.LimeGreen;
                    veprz2.BackColor = Color.LimeGreen;
                }
                else if (count == 500)
                {   //hlleo+veprc
                    all_off();
                    hllestop();
                }
                else if (count == 550)
                {   //hllec
                    all_off();
                }
                else if (count == 600)
                {   //veroc+veroo
                    all_off();
                    veready();
                }
                else if (count == 650)
                {   //veroz+vechz
                    all_off();
                    vego();
                }
                else if (count == 800)
                {   //veroo+vechc
                    all_off();
                    vestop();
                }
                else if (count == 850)
                {   //veroc
                    all_off();
                    count = 0;
                }
                break;

                case 1:
                    if (count == 50)
                    {   //hlpro+hlprc
                        all_off();
                        hlvebl();
                        hlready();
                    }
                    else if (count == 100)
                    {   //hlprz+hlchz
                        all_off();
                        hlvebl();
                        hlgo();
                    }
                    else if (count == 250)
                    {   //hlpro
                        all_off();
                        hlvebl();
                        hlstop();
                    }
                    else if (count == 300)
                    {   //hlprc
                        all_off();
                        hlvebl();
                    }
                    else if (count == 350)
                    {   //veroc+veroo
                        all_off();
                        hlvebl();
                        veready();
                    }
                    else if (count == 400)
                    {   //veroz+vechz
                        all_off();
                        hlvebl();
                        vego();
                    }
                    else if (count == 550)
                    {   //veroo+vechc
                        all_off();
                        hlvebl();
                        vestop();
                    }
                    else if (count == 600)
                    {   //veroc
                        all_off();
                        hlvebl();
                        count = 0;
                    }
                    break;
                case 2:
                    if (count == 50)
                    {   //all black
                        allblack();                        
                    }
                    else if (count == 100)
                    {   //all orange
                        hlroo1.BackColor = Color.Yellow;
                        hlroo2.BackColor = Color.Yellow;
                        hlleo1.BackColor = Color.Yellow;
                        hlleo2.BackColor = Color.Yellow;
                        veroo1.BackColor = Color.Yellow;
                        veroo2.BackColor = Color.Yellow;
                        count = 0;
                    }
                    break;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            all_off();
            count = 0;
        }
    }
}
