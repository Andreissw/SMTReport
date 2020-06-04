using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMTReport;
using AutoFasYeld;
using ESDReport;

namespace SMTReport
{
    public partial class Start_Form : Form
    {
        public Start_Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // SMT НОЧЬ
        {
            SMTNight night = new SMTNight();
            night.Show();
            //Hide();
        }

        private void DayBT_Click(object sender, EventArgs e) // SMT ДЕНЬ
        {
            SMTDay day = new SMTDay();
            day.Show();
            //Hide();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            start = "11:30";
            end = "15:00";
            ESDR ESD = new ESDR();
            ESD.Show();
        }

        private void button1_Click_1(object sender, EventArgs e) //Закрытие программы
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e) //FAS ОТЧЁТ
        {
            AutoFasYeld.Programm FAS = new AutoFasYeld.Programm();
            FAS.Show();
        }

        void локация()
        {
            var point = new Point(0);
            var size = new Size(1257,861);
            this.Location = Point.Add(point, size);
        }

        bool bolean = false;
        private void Start_Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.F4)
            {
                if (ГлавныйТаймер.Enabled == false)
                {
                    this.BackColor = Color.Beige;
                    Status.Visible = true;
                    локация();
                    bolean = true;
                    ГлавныйТаймер.Enabled = true;

                }
                else
                {
                    Status.Visible = false;
                    this.BackColor = Color.FloralWhite;
                    локация();
                    bolean = false;
                    ГлавныйТаймер.Enabled = false;

                }
            }
        }


        public static bool timesOK;
        public static string start,end;

        void times()
        {
            timesOK = false;
              
            //if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("08:02:00")) //КАРТА НА НОЧЬ
            //{
            //    var smtNight = new SMTNight();
            //    timesOK = true;
            //    smtNight.Show();
            //}

             if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("09:58:00")) // КАРТА FAS
            {

                var FAS = new Programm();
                timesOK = true;
                FAS.Show();

            }

            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("09:55:00")) // КАРТА ДЕНЬ
            {
                var smtday = new SMTDay();
                timesOK = true;
                smtday.Show();

            }

            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("11:58:00")) // КАРТА FAS
            {
                var FAS = new Programm();
                timesOK = true;
                FAS.Show();

            }

            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("11:55:00")) // КАРТА ДЕНЬ
            {
                var smtday = new SMTDay();
                timesOK = true;
                smtday.Show();

            }

            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("13:58:00")) // КАРТА FAS
            {
                var FAS = new Programm();
                timesOK = true;
                FAS.Show();

            }

            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("13:55:00")) // КАРТА ДЕНЬ
            {
                var smtday = new SMTDay();
                timesOK = true;
                smtday.Show();

            }

            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("15:58:00")) // КАРТА FAS
            {
                var FAS = new Programm();
                timesOK = true;
                FAS.Show();

            }

            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("15:55:00")) // КАРТА ДЕНЬ
            {
                var smtday = new SMTDay();
                timesOK = true;
                smtday.Show();

            }

            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("17:58:00")) // КАРТА FAS
            {
                var FAS = new Programm();
                timesOK = true;
                FAS.Show();

            }

            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("17:55:00")) // КАРТА ДЕНЬ
            {
                var smtday = new SMTDay();
                timesOK = true;
                smtday.Show();

            }

            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("19:58:00")) // КАРТА FAS
            {
                var FAS = new Programm();
                timesOK = true;
                FAS.Show();

            }

            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("19:55:00")) // КАРТА ДЕНЬ
            {
                var smtday = new SMTDay();
                timesOK = true;
                smtday.Show();

            }

            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("09:01:00")) // ESD
            {
                start = "07:00";
                end = "09:00";
                ESDR ESD = new ESDR();
                timesOK = true;
                ESD.Show();
                
            }

            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("15:01:00")) // ESD
            {
                start = "11:30";
                end = "14:59";
                ESDR ESD = new ESDR();
                timesOK = true;
                ESD.Show();

            }

            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("08:00:00")) // ESD
            {
                start = "00:00";
                end = "07:00";
                ESDR ESD = new ESDR();
                timesOK = true;
                ESD.Show();

            }


        }

        void ТаймерСубботаВоскресенье()
        {
            timesOK = false;

            //if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("08:02:00")) //КАРТА НА НОЧЬ
            //{
            //    var smtNight = new SMTNight();
            //    timesOK = true;
            //    smtNight.Show();
            //}

            if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("09:58:00")) // КАРТА FAS
            {

                var FAS = new Programm();
                timesOK = true;
                FAS.Show();

            }

            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("09:55:00")) // КАРТА ДЕНЬ
            {
                var smtday = new SMTDay();
                timesOK = true;
                smtday.Show();

            }


            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("11:58:00")) // КАРТА FAS
            {

                var FAS = new Programm();
                timesOK = true;
                FAS.Show();

            }


            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("11:55:00")) // КАРТА ДЕНЬ
            {
                var smtday = new SMTDay();
                timesOK = true;
                smtday.Show();

            }

            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("13:58:00")) // КАРТА FAS
            {

                var FAS = new Programm();
                timesOK = true;
                FAS.Show();

            }

            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("13:55:00")) // КАРТА ДЕНЬ
            {
                var smtday = new SMTDay();
                timesOK = true;
                smtday.Show();

            }

            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("15:58:00")) // КАРТА FAS
            {

                var FAS = new Programm();
                timesOK = true;
                FAS.Show();

            }



            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("15:55:00")) // КАРТА ДЕНЬ
            {
                var smtday = new SMTDay();
                timesOK = true;
                smtday.Show();

            }


            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("17:58:00")) // КАРТА FAS
            {

                var FAS = new Programm();
                timesOK = true;
                FAS.Show();

            }

            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("17:55:00")) // КАРТА ДЕНЬ
            {
                var smtday = new SMTDay();
                timesOK = true;
                smtday.Show();

            }

            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("19:58:00")) // КАРТА FAS
            {

                var FAS = new Programm();
                timesOK = true;
                FAS.Show();

            }


            else if (DateTime.Now.ToString("HH:mm:ss") == DateTime.Now.ToString("19:55:00")) // КАРТА ДЕНЬ
            {
                var smtday = new SMTDay();
                timesOK = true;
                smtday.Show();

            }

           


        }

        private void ГлавныйТаймер_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.ToString("ddddd") == "суббота" || DateTime.Now.ToString("ddddd") == "воскресенье")
            {
                ТаймерСубботаВоскресенье();
            }
            else
            {
                times();
            }

        }

        private void Start_Form_Load(object sender, EventArgs e)
        {
            Status.Visible = false;
            
        }

       
    }
}
