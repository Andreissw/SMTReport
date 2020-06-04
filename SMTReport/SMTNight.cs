using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMTReport;
using Microsoft.VisualBasic;
using System.IO;
using System.Text.RegularExpressions;

namespace SMTReport
{
    public partial class SMTNight : Form
    {
        public SMTNight()
        {
            InitializeComponent();
        }

      
        string StartdateOmron;
        string EnddateOmron;
        string sttime0, sttime1, sttime2, sttime3, sttime4,  sttime8, st6,st7,st8, st9;
        double TH, TA, bot, top, GSC593TOP, GSC593bot;
             

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            yearLB.Text = DateTime.Now.ToString("dd.MM.yyyy");
            
            HrLB.Text = DateTime.Now.ToString("HH:mm:ss");
        }
       
        void очистка()
        { 
        All.RowCount = 1;
            ModelLabel1.Refresh();
            ModelLabel2.Refresh();
            ModelLabel3.Refresh();
            ModelLabel4.Refresh();
            ModelLabel5.Refresh();
            ModelLabel6.Refresh();
            ModelLabel7.Refresh();
        }

    

        public static int count = 0;
        private void ГлавныйТаймер_Tick(object sender, EventArgs e)
        {
           
           
          
                this.Activate();
                if (label28.Text == "Status OK")
                {
                if (All.Rows.Count != 0)
                    {

                
                        ГлавныйТаймер.Interval = 500000;
                        DirectoryInfo di = new DirectoryInfo(@"C:\Скриншот");
                        FileInfo[] fi = di.GetFiles();
                        foreach (FileInfo f in fi)
                        {
                            f.Delete();
                        }
                        count = 7;
                        ScreenShot.Enabled = true;
                    }
                    else
                    {
                        this.Close();
                     }
                 }
                else
                {
                    this.Close();
                }


            
        }

        private void ScreenShot_Tick(object sender, EventArgs e)
        {
            count++;
            ClassDay.Screens(this.Width - 10, this.Height - 10, "" + count.ToString() + ".jpg");
            //Форма1.Enabled = true;
            ScreenShot.Enabled = false;
            Отправка.Enabled = true;

        }

        private void Отправка_Tick(object sender, EventArgs e)
        {

            ClassNight.SendEmail(@"C:\Скриншот\" + count.ToString() + ".jpg", "SMT-Ночная карта");
            Отправка.Enabled = false;
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                label28.Text = "Status OK";
                label28.BackColor = Color.Gainsboro;
            }
            else
            {
                StatusLabel();
            }
        }

        private void StatusLabel()
        {
            label28.Text = "Status NOK";
            label28.BackColor = Color.Red;
        }

        private void SMTNight_Load(object sender, EventArgs e)
        {
            ОбновлениеОтчета();
            if (Start_Form.timesOK == true)
            {
                ГлавныйТаймер.Enabled = true;
            }

        }
        public static DateTime now = DateTime.Now;
        private void ОбновлениеОтчета(int k = 0)
        {
            try
            {
                DateTime now = DateTime.Now;
                очистка();
                All.Rows.Add(25);

                GSC593TOP = 94.00;
                GSC593bot = 95.80;
                TH = 98.50;
                TA = 98.50;
                bot = 98.00;
                top = 95.80;

                #region Время переменные
                sttime0 = "00:00";
                sttime1 = "02:00";
                sttime2 = "04:00";
                sttime3 = "06:00";
                sttime4 = "08:00";
                sttime8 = "22:00";
                st6 = "18:00";
                st7 = "20:00";
                st8 = "22:00";
                st9 = "23:59";



                #endregion

                //DateTime.Now = SMTDay.now.AddDays(k);
                StartdateOmron = now.ToString("yyyy-MM-dd");
                EnddateOmron = now.AddDays(1).ToString("yyyy-MM-dd");
                yearLB.Text = now.ToString("dd.MM.yyyy");
                HrLB.Text = now.ToString("HH:mm:ss");


                //if (Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss")) <= Convert.ToDateTime(now.ToString("09:50:00")))
                //{
                //    StatusLabel();
                //}

                //if (Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss")) >= Convert.ToDateTime(now.ToString("08:03:00")))
                //{
                //    StatusLabel();
                //}

                ClassNight.SpisokModels(DG, DGOracle, All, now.ToString("yyyy-MM-dd"), now.AddDays(-1).ToString("dd.MM.yyyy"), now.AddDays(1).ToString("yyyy-MM-dd"), now.ToString("dd.MM.yyyy"));


                #region ЦИКЛ который раставляет МОДЕЛЬ по порядку 
                ClassDay.SPModelLine(LineLabel1, ModelLabel1, All, 0);
                ClassDay.SPModelLine(LineLabel2, ModelLabel2, All, 1);
                ClassDay.SPModelLine(LineLabel3, ModelLabel3, All, 2);
                ClassDay.SPModelLine(LineLabel4, ModelLabel4, All, 3);
                ClassDay.SPModelLine(LineLabel5, ModelLabel5, All, 4);
                ClassDay.SPModelLine(LineLabel6, ModelLabel6, All, 5);
                ClassDay.SPModelLine(LineLabel7, ModelLabel7, All, 6);
                #endregion



                #region ЦИКЛ который раставляет ЛИНИЮ по порядку 
                ClassDay.SPModelLines(All, LabelLine1, Status1, 0);
                ClassDay.SPModelLines(All, LabelLine2, Status2, 1);
                ClassDay.SPModelLines(All, LabelLine3, Status3, 2);
                ClassDay.SPModelLines(All, LabelLine4, Status4, 3);
                ClassDay.SPModelLines(All, LabelLine5, Status5, 4);
                ClassDay.SPModelLines(All, LabelLine6, Status6, 5);
                ClassDay.SPModelLines(All, LabelLine7, Status7, 6);
                #endregion

                if (Status1.Text == "-")
                    GroupAll.Visible = false;
                else if (Status1.Text == "Omron") { Vyp1Omron(); Otkaz1Omron(); FPY1(); ClassDay.Топ_Omron(TopLine1, now.AddDays(-1).ToString("yyyy-MM-dd"), now.ToString("yyyy-MM-dd"), "20:00:00", "08:00:00", ModelLabel1.Text); }
                else if (Status1.Text == "Oracle") { Vypysk1Oracle(); Otkaz1Oracle(); FPY1(); ClassDay.ТопОшибокSMTНОЧЬ(TopLine1, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.AddDays(0).ToString("dd.MM.yyyy"), "18:00:00", "06:00:00", ModelLabel1.Text, LabelLine1.Text); }

                if (Status2.Text == "-")
                    SMTGroup2.Visible = false;
                else if (Status2.Text == "Omron") { Vyp2Omron(); Otkaz2Omron(); FPY2(); ClassDay.Топ_Omron(TopLine2, now.AddDays(-1).ToString("yyyy-MM-dd"), now.ToString("yyyy-MM-dd"), "20:00:00", "08:00:00", ModelLabel2.Text); }
                else if (Status2.Text == "Oracle") { Vypysk2Oracle(); Otkaz2Oracle(); FPY2(); ClassDay.ТопОшибокSMTНОЧЬ(TopLine2, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.AddDays(0).ToString("dd.MM.yyyy"), "18:00:00", "06:00:00", ModelLabel2.Text, LabelLine2.Text); }

                if (Status3.Text == "-")
                    SMTGroup3.Visible = false;
                else if (Status3.Text == "Omron") { Vyp3Omron(); Otkaz3Omron(); FPY3(); ClassDay.Топ_Omron(TopLine3, now.AddDays(-1).ToString("yyyy-MM-dd"), now.ToString("yyyy-MM-dd"), "20:00:00", "08:00:00", ModelLabel3.Text); }
                else if (Status3.Text == "Oracle") { Vypysk3Oracle(); Otkaz3Oracle(); FPY3(); ClassDay.ТопОшибокSMTНОЧЬ(TopLine3, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.AddDays(0).ToString("dd.MM.yyyy"), "18:00:00", "06:00:00", ModelLabel3.Text, LabelLine3.Text); }

                if (Status4.Text == "-")
                    SMTGroup4.Visible = false;
                else if (Status4.Text == "Omron") { Vyp4Omron(); Otkaz4Omron(); FPY4(); ClassDay.Топ_Omron(TopLine4, now.AddDays(-1).ToString("yyyy-MM-dd"), now.ToString("yyyy-MM-dd"), "20:00:00", "08:00:00", ModelLabel4.Text); }
                else if (Status4.Text == "Oracle") { Vypysk4Oracle(); Otkaz4Oracle(); FPY4(); ClassDay.ТопОшибокSMTНОЧЬ(TopLine4, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.AddDays(0).ToString("dd.MM.yyyy"), "18:00:00", "06:00:00", ModelLabel4.Text, LabelLine4.Text); }

                if (Status5.Text == "-")
                    SMTGroup5.Visible = false;
                else if (Status5.Text == "Omron") { Vyp5Omron(); Otkaz5Omron(); FPY5(); ClassDay.Топ_Omron(TopLine5, now.AddDays(-1).ToString("yyyy-MM-dd"), now.ToString("yyyy-MM-dd"), "20:00:00", "08:00:00", ModelLabel5.Text); }
                else if (Status5.Text == "Oracle") { Vypysk5Oracle(); Otkaz5Oracle(); FPY5(); ClassDay.ТопОшибокSMTНОЧЬ(TopLine5, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.AddDays(0).ToString("dd.MM.yyyy"), "18:00:00", "06:00:00", ModelLabel5.Text, LabelLine5.Text); }

                if (Status6.Text == "-")
                    SMTGroup6.Visible = false;
                else if (Status6.Text == "Omron") { Vyp6Omron(); Otkaz6Omron(); FPY6(); ClassDay.Топ_Omron(TopLine6, now.AddDays(-1).ToString("yyyy-MM-dd"), now.ToString("yyyy-MM-dd"), "20:00:00", "08:00:00", ModelLabel6.Text); }
                else if (Status6.Text == "Oracle") { Vypysk6Oracle(); Otkaz6Oracle(); FPY6(); ClassDay.ТопОшибокSMTНОЧЬ(TopLine6, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.AddDays(0).ToString("dd.MM.yyyy"), "18:00:00", "06:00:00", ModelLabel6.Text, LabelLine6.Text); }

                if (Status7.Text == "-")
                    SMTGroup7.Visible = false;
                else if (Status7.Text == "Omron") { Vyp7Omron(); Otkaz7Omron(); FPY7(); ClassDay.Топ_Omron(TopLine7, now.AddDays(-1).ToString("yyyy-MM-dd"), now.ToString("yyyy-MM-dd"), "20:00:00", "08:00:00", ModelLabel7.Text); }
                else if (Status7.Text == "Oracle") { Vypysk7Oracle(); Otkaz7Oracle(); FPY7(); ClassDay.ТопОшибокSMTНОЧЬ(TopLine7, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.AddDays(0).ToString("dd.MM.yyyy"), "18:00:00", "06:00:00", ModelLabel7.Text, LabelLine7.Text); }

                cells(Cell1, ModelLabel1.Text);
                cells(Cel2, ModelLabel2.Text);
                cells(Cel3, ModelLabel3.Text);
                cells(Cel4, ModelLabel4.Text);
                cells(Cel5, ModelLabel5.Text);
                cells(Cel6, ModelLabel6.Text);
                cells(Cel7, ModelLabel7.Text);

                //MessageBox.Show(Convert.ToString(Convert.ToDouble(Cell1.Text)));
                FPYColor1();
                FPYColor2();
                FPYColor3();
                FPYColor4();
                FPYColor5();
                FPYColor6();
                FPYColor7();

                //ClassDay.ТопОшибокSMTНочь(TopLine1, now.ToString("dd.MM.yyyy"), "18:00:00", "06:00;00", ModelLabel1.Text, LabelLine1.Text);
               
                                    
            }
            catch (Exception)
            {

                StatusLabel();
            }
        }

        private void FPYColor1()
        {
            ClassDay.FPYColor(FPY122, Convert.ToDouble(Cell1.Text), ModelLabel1.Text);
            ClassDay.FPYColor(FPY100, Convert.ToDouble(Cell1.Text), ModelLabel1.Text);
            ClassDay.FPYColor(FPY110, Convert.ToDouble(Cell1.Text), ModelLabel1.Text);
            ClassDay.FPYColor(FPY112, Convert.ToDouble(Cell1.Text), ModelLabel1.Text);
            ClassDay.FPYColor(FPY114, Convert.ToDouble(Cell1.Text), ModelLabel1.Text);
            ClassDay.FPYColor(FPY116, Convert.ToDouble(Cell1.Text), ModelLabel1.Text);
            ClassDay.FPYColor(FPY1All, Convert.ToDouble(Cell1.Text), ModelLabel1.Text);
        }
        private void FPYColor2()
        {
            ClassDay.FPYColor(FPY222, Convert.ToDouble(Cel2.Text), ModelLabel2.Text);
            ClassDay.FPYColor(FPY200, Convert.ToDouble(Cel2.Text), ModelLabel2.Text);
            ClassDay.FPYColor(FPY210, Convert.ToDouble(Cel2.Text), ModelLabel2.Text);
            ClassDay.FPYColor(FPY212, Convert.ToDouble(Cel2.Text), ModelLabel2.Text);
            ClassDay.FPYColor(FPY214, Convert.ToDouble(Cel2.Text), ModelLabel2.Text);
            ClassDay.FPYColor(FPY216, Convert.ToDouble(Cel2.Text), ModelLabel2.Text);
            ClassDay.FPYColor(FPY2All, Convert.ToDouble(Cel2.Text), ModelLabel2.Text);
        }
        private void FPYColor3()
        {
            ClassDay.FPYColor(FPY322, Convert.ToDouble(Cel3.Text), ModelLabel3.Text);
            ClassDay.FPYColor(FPY300, Convert.ToDouble(Cel3.Text), ModelLabel3.Text);
            ClassDay.FPYColor(FPY310, Convert.ToDouble(Cel3.Text), ModelLabel3.Text);
            ClassDay.FPYColor(FPY312, Convert.ToDouble(Cel3.Text), ModelLabel3.Text);
            ClassDay.FPYColor(FPY314, Convert.ToDouble(Cel3.Text), ModelLabel3.Text);
            ClassDay.FPYColor(FPY316, Convert.ToDouble(Cel3.Text), ModelLabel3.Text);
            ClassDay.FPYColor(FPY3All, Convert.ToDouble(Cel3.Text), ModelLabel3.Text);
        }
        private void FPYColor4()
        {
            ClassDay.FPYColor(FPY422, Convert.ToDouble(Cel4.Text), ModelLabel4.Text);
            ClassDay.FPYColor(FPY400, Convert.ToDouble(Cel4.Text), ModelLabel4.Text);
            ClassDay.FPYColor(FPY410, Convert.ToDouble(Cel4.Text), ModelLabel4.Text);
            ClassDay.FPYColor(FPY412, Convert.ToDouble(Cel4.Text), ModelLabel4.Text);
            ClassDay.FPYColor(FPY414, Convert.ToDouble(Cel4.Text), ModelLabel4.Text);
            ClassDay.FPYColor(FPY416, Convert.ToDouble(Cel4.Text), ModelLabel4.Text);
            ClassDay.FPYColor(FPY4All, Convert.ToDouble(Cel4.Text), ModelLabel4.Text);
        }
        private void FPYColor5()
        {
            ClassDay.FPYColor(FPY522, Convert.ToDouble(Cel5.Text), ModelLabel5.Text);
            ClassDay.FPYColor(FPY500, Convert.ToDouble(Cel5.Text), ModelLabel5.Text);
            ClassDay.FPYColor(FPY510, Convert.ToDouble(Cel5.Text), ModelLabel5.Text);
            ClassDay.FPYColor(FPY512, Convert.ToDouble(Cel5.Text), ModelLabel5.Text);
            ClassDay.FPYColor(FPY514, Convert.ToDouble(Cel5.Text), ModelLabel5.Text);
            ClassDay.FPYColor(FPY516, Convert.ToDouble(Cel5.Text), ModelLabel5.Text);
            ClassDay.FPYColor(FPY5All, Convert.ToDouble(Cel5.Text), ModelLabel5.Text);
        }
        private void FPYColor6()
        {
            ClassDay.FPYColor(FPY622, Convert.ToDouble(Cel6.Text), ModelLabel6.Text);
            ClassDay.FPYColor(FPY600, Convert.ToDouble(Cel6.Text), ModelLabel6.Text);
            ClassDay.FPYColor(FPY610, Convert.ToDouble(Cel6.Text), ModelLabel6.Text);
            ClassDay.FPYColor(FPY612, Convert.ToDouble(Cel6.Text), ModelLabel6.Text);
            ClassDay.FPYColor(FPY614, Convert.ToDouble(Cel6.Text), ModelLabel6.Text);
            ClassDay.FPYColor(FPY616, Convert.ToDouble(Cel6.Text), ModelLabel6.Text);
            ClassDay.FPYColor(FPY6All, Convert.ToDouble(Cel6.Text), ModelLabel6.Text);
        }
        private void FPYColor7()
        {
            ClassDay.FPYColor(FPY722, Convert.ToDouble(Cel7.Text), ModelLabel7.Text);
            ClassDay.FPYColor(FPY700, Convert.ToDouble(Cel7.Text), ModelLabel7.Text);
            ClassDay.FPYColor(FPY710, Convert.ToDouble(Cel7.Text), ModelLabel7.Text);
            ClassDay.FPYColor(FPY712, Convert.ToDouble(Cel7.Text), ModelLabel7.Text);
            ClassDay.FPYColor(FPY714, Convert.ToDouble(Cel7.Text), ModelLabel7.Text);
            ClassDay.FPYColor(FPY716, Convert.ToDouble(Cel7.Text), ModelLabel7.Text);
            ClassDay.FPYColor(FPY7All, Convert.ToDouble(Cel7.Text), ModelLabel7.Text);
        }


        private void cells(Label cel, string model)
        {
            if (model == "-")
            {

            }
            else
                if (model.Substring(0, 3) == "Bar")
                {
                    cel.Text = Convert.ToString(TA);
                }
                else if (model.Substring(0, 3) == "Bar")
                {
                    cel.Text = Convert.ToString(TH);
                }
                else if (model.Substring(model.Length - 3, 3) == "bot")
                    cel.Text = Convert.ToString(bot);
                else if (model.Contains("593"))
                {

                    if (model.Contains("top"))
                    {
                        cel.Text = Convert.ToString(GSC593TOP);
                    
                    }
                    else if (model.Contains("bot"))
                    {
                         cel.Text = Convert.ToString(GSC593bot);
                    }
                 
                 }
                else if (model.Contains("621"))
                {

                    if (model.Contains("top"))
                    {
                        cel.Text = Convert.ToString(GSC593TOP);

                    }
                    else if (model.Contains("bot"))
                    {
                        cel.Text = Convert.ToString(GSC593bot);
                    }

            }
                    else
                    cel.Text = Convert.ToString(top);
           
        }

        void FPY1()
        {
            ClassDay.FPY(Vyp122, Otkaz122, FPY122);
            ClassDay.FPY(Vyp100, Otkaz100, FPY100);
            ClassDay.FPY(Vyp110, Otkaz110, FPY110);
            ClassDay.FPY(Vyp112, Otkaz112, FPY112);
            ClassDay.FPY(Vyp114, Otkaz114, FPY114);
            ClassDay.FPY(Vyp116, Otkaz116, FPY116);
            ClassDay.FPY(Vyp1All, Otkaz1All, FPY1All);
        }
        void FPY2()
        {
            ClassDay.FPY(Vyp222, Otkaz222, FPY222);
            ClassDay.FPY(Vyp200, Otkaz200, FPY200);
            ClassDay.FPY(Vyp210, Otkaz210, FPY210);
            ClassDay.FPY(Vyp212, Otkaz212, FPY212);
            ClassDay.FPY(Vyp214, Otkaz214, FPY214);
            ClassDay.FPY(Vyp216, Otkaz216, FPY216);
            ClassDay.FPY(Vyp2All, Otkaz2All, FPY2All);
        }
        void FPY3()
        {
            ClassDay.FPY(Vyp322, Otkaz322, FPY322);
            ClassDay.FPY(Vyp300, Otkaz300, FPY300);
            ClassDay.FPY(Vyp310, Otkaz310, FPY310);
            ClassDay.FPY(Vyp312, Otkaz312, FPY312);
            ClassDay.FPY(Vyp314, Otkaz314, FPY314);
            ClassDay.FPY(Vyp316, Otkaz316, FPY316);
            ClassDay.FPY(Vyp3All, Otkaz3All, FPY3All);
        }
        void FPY4()
        {
            ClassDay.FPY(Vyp422, Otkaz422, FPY422);
            ClassDay.FPY(Vyp400, Otkaz400, FPY400);
            ClassDay.FPY(Vyp322, Otkaz322, FPY322);
            ClassDay.FPY(Vyp300, Otkaz300, FPY300);
            ClassDay.FPY(Vyp410, Otkaz410, FPY410);
            ClassDay.FPY(Vyp412, Otkaz412, FPY412);
            ClassDay.FPY(Vyp414, Otkaz414, FPY414);
            ClassDay.FPY(Vyp416, Otkaz416, FPY416);
            ClassDay.FPY(Vyp4All, Otkaz4All, FPY4All);
        }
        void FPY5()
        {
            ClassDay.FPY(Vyp522, Otkaz522, FPY522);
            ClassDay.FPY(Vyp500, Otkaz500, FPY500);
            ClassDay.FPY(Vyp510, Otkaz510, FPY510);
            ClassDay.FPY(Vyp512, Otkaz512, FPY512);
            ClassDay.FPY(Vyp514, Otkaz514, FPY514);
            ClassDay.FPY(Vyp516, Otkaz516, FPY516);
            ClassDay.FPY(Vyp5All, Otkaz5All, FPY5All);
        }
        void FPY6()
        {
            ClassDay.FPY(Vyp622, Otkaz622, FPY622);
            ClassDay.FPY(Vyp600, Otkaz600, FPY600);
            ClassDay.FPY(Vyp610, Otkaz610, FPY610);
            ClassDay.FPY(Vyp612, Otkaz612, FPY612);
            ClassDay.FPY(Vyp614, Otkaz614, FPY614);
            ClassDay.FPY(Vyp616, Otkaz616, FPY616);
            ClassDay.FPY(Vyp6All, Otkaz6All, FPY6All);
        }
        void FPY7()
        {
            ClassDay.FPY(Vyp722, Otkaz722, FPY722);
            ClassDay.FPY(Vyp700, Otkaz700, FPY700);
            ClassDay.FPY(Vyp710, Otkaz710, FPY710);
            ClassDay.FPY(Vyp712, Otkaz712, FPY712);
            ClassDay.FPY(Vyp714, Otkaz714, FPY714);
            ClassDay.FPY(Vyp716, Otkaz716, FPY716);
            ClassDay.FPY(Vyp7All, Otkaz7All, FPY7All);
        }

        int день = -1;

        void Vypysk1Oracle()

        {
            ClassDay.vypyskOracle(ModelLabel1.Text, st6, st7, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp122, LabelLine1.Text);
            ClassDay.vypyskOracle(ModelLabel1.Text, st7, st8, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp100, LabelLine1.Text);
            ClassNight.vypyskOracle(ModelLabel1.Text, sttime8, sttime0, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Vyp110, LabelLine1.Text);
            ClassDay.vypyskOracle(ModelLabel1.Text, sttime0, sttime1, DateTime.Now.ToString("dd.MM.yyyy"), Vyp112, LabelLine1.Text);
            ClassDay.vypyskOracle(ModelLabel1.Text, sttime1, sttime2, DateTime.Now.ToString("dd.MM.yyyy"), Vyp114, LabelLine1.Text);
            ClassDay.vypyskOracle(ModelLabel1.Text, sttime2, sttime3, DateTime.Now.ToString("dd.MM.yyyy"), Vyp116, LabelLine1.Text);
            ClassNight.vypyskOracle(ModelLabel1.Text, st6, sttime3, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"),Vyp1All, LabelLine1.Text);
        }
        void Vypysk2Oracle()
        {
            ClassDay.vypyskOracle(ModelLabel2.Text, st6, st7, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp222, LabelLine2.Text);
            ClassDay.vypyskOracle(ModelLabel2.Text, st7, st8, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp200, LabelLine2.Text);
            ClassNight.vypyskOracle(ModelLabel2.Text, sttime8, sttime0, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Vyp210, LabelLine2.Text);
            ClassDay.vypyskOracle(ModelLabel2.Text, sttime0, sttime1, DateTime.Now.ToString("dd.MM.yyyy"), Vyp212, LabelLine2.Text);
            ClassDay.vypyskOracle(ModelLabel2.Text, sttime1, sttime2, DateTime.Now.ToString("dd.MM.yyyy"), Vyp214, LabelLine2.Text);
            ClassDay.vypyskOracle(ModelLabel2.Text, sttime2, sttime3, DateTime.Now.ToString("dd.MM.yyyy"), Vyp216, LabelLine2.Text);
            ClassNight.vypyskOracle(ModelLabel2.Text, st6, sttime3, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Vyp2All, LabelLine2.Text);
        }
        void Vypysk3Oracle()
        {
            ClassDay.vypyskOracle(ModelLabel3.Text, st6, st7, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp322, LabelLine3.Text);
            ClassDay.vypyskOracle(ModelLabel3.Text, st7, st8, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp300, LabelLine3.Text);
            ClassNight.vypyskOracle(ModelLabel3.Text, sttime8, sttime0, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Vyp310, LabelLine3.Text);
            ClassDay.vypyskOracle(ModelLabel3.Text, sttime0, sttime1, DateTime.Now.ToString("dd.MM.yyyy"), Vyp312, LabelLine3.Text);
            ClassDay.vypyskOracle(ModelLabel3.Text, sttime1, sttime2, DateTime.Now.ToString("dd.MM.yyyy"), Vyp314, LabelLine3.Text);
            ClassDay.vypyskOracle(ModelLabel3.Text, sttime2, sttime3, DateTime.Now.ToString("dd.MM.yyyy"), Vyp316, LabelLine3.Text);
            ClassNight.vypyskOracle(ModelLabel3.Text, st6, sttime3, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Vyp3All, LabelLine3.Text);
        }
        void Vypysk4Oracle()
        {
            ClassDay.vypyskOracle(ModelLabel4.Text, st6, st7, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp422, LabelLine4.Text);
            ClassDay.vypyskOracle(ModelLabel4.Text, st7, st8, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp400, LabelLine4.Text);
            ClassNight.vypyskOracle(ModelLabel4.Text, sttime8, sttime0, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Vyp410, LabelLine4.Text);
            ClassDay.vypyskOracle(ModelLabel4.Text, sttime0, sttime1, DateTime.Now.ToString("dd.MM.yyyy"), Vyp412, LabelLine4.Text);
            ClassDay.vypyskOracle(ModelLabel4.Text, sttime1, sttime2, DateTime.Now.ToString("dd.MM.yyyy"), Vyp414, LabelLine4.Text);
            ClassDay.vypyskOracle(ModelLabel4.Text, sttime2, sttime3, DateTime.Now.ToString("dd.MM.yyyy"), Vyp416, LabelLine4.Text);
            ClassNight.vypyskOracle(ModelLabel4.Text, st6, sttime3, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Vyp4All, LabelLine4.Text);
        }
        void Vypysk5Oracle()
        {
            ClassDay.vypyskOracle(ModelLabel5.Text, st6, st7, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp522, LabelLine5.Text);
            ClassDay.vypyskOracle(ModelLabel5.Text, st7, st8, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp500, LabelLine5.Text);
            ClassNight.vypyskOracle(ModelLabel5.Text, sttime8, sttime0, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Vyp510, LabelLine5.Text);
            ClassDay.vypyskOracle(ModelLabel5.Text, sttime0, sttime1, DateTime.Now.ToString("dd.MM.yyyy"), Vyp512, LabelLine5.Text);
            ClassDay.vypyskOracle(ModelLabel5.Text, sttime1, sttime2, DateTime.Now.ToString("dd.MM.yyyy"), Vyp514, LabelLine5.Text);
            ClassDay.vypyskOracle(ModelLabel5.Text, sttime2, sttime3, DateTime.Now.ToString("dd.MM.yyyy"), Vyp516, LabelLine5.Text);
            ClassNight.vypyskOracle(ModelLabel5.Text, st6, sttime3, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Vyp5All, LabelLine5.Text);
        }
        void Vypysk6Oracle()
        {
            ClassDay.vypyskOracle(ModelLabel6.Text, st6, st7, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp622, LabelLine6.Text);
            ClassDay.vypyskOracle(ModelLabel6.Text, st7, st8, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp600, LabelLine6.Text);
            ClassNight.vypyskOracle(ModelLabel6.Text, sttime8, sttime0, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Vyp610, LabelLine6.Text);
            ClassDay.vypyskOracle(ModelLabel6.Text, sttime0, sttime1, DateTime.Now.ToString("dd.MM.yyyy"), Vyp612, LabelLine6.Text);
            ClassDay.vypyskOracle(ModelLabel6.Text, sttime1, sttime2, DateTime.Now.ToString("dd.MM.yyyy"), Vyp614, LabelLine6.Text);
            ClassDay.vypyskOracle(ModelLabel6.Text, sttime2, sttime3, DateTime.Now.ToString("dd.MM.yyyy"), Vyp616, LabelLine6.Text);
            ClassNight.vypyskOracle(ModelLabel6.Text, st6, sttime3, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Vyp6All, LabelLine6.Text);
        }
        void Vypysk7Oracle()
        {
            ClassDay.vypyskOracle(ModelLabel7.Text, st6, st7, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp722, LabelLine7.Text);
            ClassDay.vypyskOracle(ModelLabel7.Text, st7, st8, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp700, LabelLine7.Text);
            ClassNight.vypyskOracle(ModelLabel7.Text, sttime8, sttime0, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Vyp710, LabelLine7.Text);
            ClassDay.vypyskOracle(ModelLabel7.Text, sttime0, sttime1, DateTime.Now.ToString("dd.MM.yyyy"), Vyp712, LabelLine7.Text);
            ClassDay.vypyskOracle(ModelLabel7.Text, sttime1, sttime2, DateTime.Now.ToString("dd.MM.yyyy"), Vyp714, LabelLine7.Text);
            ClassDay.vypyskOracle(ModelLabel7.Text, sttime2, sttime3, DateTime.Now.ToString("dd.MM.yyyy"), Vyp716, LabelLine7.Text);
            ClassNight.vypyskOracle(ModelLabel7.Text, st6, sttime3, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Vyp7All, LabelLine7.Text);
        }

        void Otkaz1Oracle()
        {
            ClassDay.OtkazOracle(ModelLabel1.Text, st6, st7, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz122, LabelLine1.Text);
            ClassDay.OtkazOracle(ModelLabel1.Text, st7, st8, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz100, LabelLine1.Text);
            ClassNight.OtkazOracle(ModelLabel1.Text, sttime8, sttime0, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Otkaz110, LabelLine1.Text);
            ClassDay.OtkazOracle(ModelLabel1.Text, sttime0, sttime1, DateTime.Now.ToString("dd.MM.yyyy"), Otkaz112, LabelLine1.Text);
            ClassDay.OtkazOracle(ModelLabel1.Text, sttime1, sttime2, DateTime.Now.ToString("dd.MM.yyyy"), Otkaz114, LabelLine1.Text);
            ClassDay.OtkazOracle(ModelLabel1.Text, sttime2, sttime3, DateTime.Now.ToString("dd.MM.yyyy"), Otkaz116, LabelLine1.Text);
            ClassNight.OtkazOracle(ModelLabel1.Text, st6, sttime3, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Otkaz1All, LabelLine1.Text);
        }
        void Otkaz2Oracle()
        {
            ClassDay.OtkazOracle(ModelLabel2.Text, st6, st7, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz222, LabelLine2.Text);
            ClassDay.OtkazOracle(ModelLabel2.Text, st7, st8, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz200, LabelLine2.Text);
            ClassNight.OtkazOracle(ModelLabel2.Text, sttime8, sttime0, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Otkaz210, LabelLine2.Text);
            ClassDay.OtkazOracle(ModelLabel2.Text, sttime0, sttime1, DateTime.Now.ToString("dd.MM.yyyy"), Otkaz212, LabelLine2.Text);
            ClassDay.OtkazOracle(ModelLabel2.Text, sttime1, sttime2, DateTime.Now.ToString("dd.MM.yyyy"), Otkaz214, LabelLine2.Text);
            ClassDay.OtkazOracle(ModelLabel2.Text, sttime2, sttime3, DateTime.Now.ToString("dd.MM.yyyy"), Otkaz216, LabelLine2.Text);
            ClassNight.OtkazOracle(ModelLabel2.Text, st6, sttime3, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Otkaz2All, LabelLine2.Text);
        }
        void Otkaz3Oracle()
        {
            ClassDay.OtkazOracle(ModelLabel3.Text, st6, st7, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz322, LabelLine3.Text);
            ClassDay.OtkazOracle(ModelLabel3.Text, st7, st8, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz300, LabelLine3.Text);
            ClassNight.OtkazOracle(ModelLabel3.Text, sttime8, sttime0, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Otkaz310, LabelLine3.Text);
            ClassDay.OtkazOracle(ModelLabel3.Text, sttime0, sttime1, DateTime.Now.ToString("dd.MM.yyyy"), Otkaz312, LabelLine3.Text);
            ClassDay.OtkazOracle(ModelLabel3.Text, sttime1, sttime2, DateTime.Now.ToString("dd.MM.yyyy"), Otkaz314, LabelLine3.Text);
            ClassDay.OtkazOracle(ModelLabel3.Text, sttime2, sttime3, DateTime.Now.ToString("dd.MM.yyyy"), Otkaz316, LabelLine3.Text);
            ClassNight.OtkazOracle(ModelLabel3.Text, st6, sttime3, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Otkaz3All, LabelLine3.Text);
        }

     

        void Otkaz4Oracle()
        {
            ClassDay.OtkazOracle(ModelLabel4.Text, st6, st7, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz422, LabelLine4.Text);
            ClassDay.OtkazOracle(ModelLabel4.Text, st7, st8, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz400, LabelLine4.Text);
            ClassNight.OtkazOracle(ModelLabel4.Text, sttime8, sttime0, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Otkaz410, LabelLine4.Text);
            ClassDay.OtkazOracle(ModelLabel4.Text, sttime0, sttime1, DateTime.Now.ToString("dd.MM.yyyy"), Otkaz412, LabelLine4.Text);
            ClassDay.OtkazOracle(ModelLabel4.Text, sttime1, sttime2, DateTime.Now.ToString("dd.MM.yyyy"), Otkaz414, LabelLine4.Text);
            ClassDay.OtkazOracle(ModelLabel4.Text, sttime2, sttime3, DateTime.Now.ToString("dd.MM.yyyy"), Otkaz416, LabelLine4.Text);
            ClassNight.OtkazOracle(ModelLabel4.Text, st6, sttime3, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Otkaz4All, LabelLine4.Text);
        }
        void Otkaz5Oracle()
        {
            ClassDay.OtkazOracle(ModelLabel5.Text, st6, st7, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz522, LabelLine5.Text);
            ClassDay.OtkazOracle(ModelLabel5.Text, st7, st8, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz500, LabelLine5.Text);
            ClassNight.OtkazOracle(ModelLabel5.Text, sttime8, sttime0, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Otkaz510, LabelLine5.Text);
            ClassDay.OtkazOracle(ModelLabel5.Text, sttime0, sttime1, DateTime.Now.ToString("dd.MM.yyyy"), Otkaz512, LabelLine5.Text);
            ClassDay.OtkazOracle(ModelLabel5.Text, sttime1, sttime2, DateTime.Now.ToString("dd.MM.yyyy"), Otkaz514, LabelLine5.Text);
            ClassDay.OtkazOracle(ModelLabel5.Text, sttime2, sttime3, DateTime.Now.ToString("dd.MM.yyyy"), Otkaz516, LabelLine5.Text);
            ClassNight.OtkazOracle(ModelLabel5.Text, st6, sttime3, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Otkaz5All, LabelLine5.Text);
        }
        void Otkaz6Oracle()
        {
            ClassDay.OtkazOracle(ModelLabel6.Text, st6, st7, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz622, LabelLine6.Text);
            ClassDay.OtkazOracle(ModelLabel6.Text, st7, st8, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz600, LabelLine6.Text);
            ClassNight.OtkazOracle(ModelLabel6.Text, sttime8, sttime0, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Otkaz610, LabelLine6.Text);
            ClassDay.OtkazOracle(ModelLabel6.Text, sttime0, sttime1, DateTime.Now.ToString("dd.MM.yyyy"), Otkaz612, LabelLine6.Text);
            ClassDay.OtkazOracle(ModelLabel6.Text, sttime1, sttime2, DateTime.Now.ToString("dd.MM.yyyy"), Otkaz614, LabelLine6.Text);
            ClassDay.OtkazOracle(ModelLabel6.Text, sttime2, sttime3, DateTime.Now.ToString("dd.MM.yyyy"), Otkaz616, LabelLine6.Text);
            ClassNight.OtkazOracle(ModelLabel6.Text, st6, sttime3, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Otkaz6All, LabelLine6.Text);
        }
        void Otkaz7Oracle()
        {
            ClassDay.OtkazOracle(ModelLabel7.Text, st6, st7, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz722, LabelLine7.Text);
            ClassDay.OtkazOracle(ModelLabel7.Text, st7, st8, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz700, LabelLine7.Text);
            ClassNight.OtkazOracle(ModelLabel7.Text, sttime8, sttime0, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Otkaz710, LabelLine7.Text);
            ClassDay.OtkazOracle(ModelLabel7.Text, sttime0, sttime1, DateTime.Now.ToString("dd.MM.yyyy"), Otkaz712, LabelLine7.Text);
            ClassDay.OtkazOracle(ModelLabel7.Text, sttime1, sttime2, DateTime.Now.ToString("dd.MM.yyyy"), Otkaz714, LabelLine7.Text);
            ClassDay.OtkazOracle(ModelLabel7.Text, sttime2, sttime3, DateTime.Now.ToString("dd.MM.yyyy"), Otkaz716, LabelLine7.Text);
            ClassNight.OtkazOracle(ModelLabel7.Text, st6, sttime3, DateTime.Now.AddDays(-1).ToString("dd.MM.yyyy"), DateTime.Now.ToString("dd.MM.yyyy"), Otkaz7All, LabelLine7.Text);
        }

        void Otkaz1Omron()
        {
            #region
            ClassDay.vypOtkazALL(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 20:00:00", DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 22:00:00", ModelLabel1.Text, Otkaz122);
            ClassDay.vypOtkazALL(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 22:00:00", DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59", ModelLabel1.Text, Otkaz100);
            //ClassDay.OtkazOmron(ModelLabel1.Text, st7, st8, Otkaz122);
            //ClassDay.OtkazOmron(ModelLabel1.Text, st8, st9, Otkaz100);
            ClassDay.OtkazOmron(ModelLabel1.Text, sttime0, sttime1, Otkaz110);
            ClassDay.OtkazOmron(ModelLabel1.Text, sttime1, sttime2, Otkaz112);
            ClassDay.OtkazOmron(ModelLabel1.Text, sttime2, sttime3, Otkaz114);
            ClassDay.OtkazOmron(ModelLabel1.Text, sttime3, sttime4, Otkaz116);
            //ClassDay.OtkazOmron(ModelLabel1.Text, sttime0, sttime4, Otkaz1All);
            ClassDay.vypOtkazALL(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 20:00:00", DateTime.Now.AddDays(0).ToString("yyyy-MM-dd") + " 08:00:00", ModelLabel1.Text, Otkaz1All);

            #endregion
        }
        void Otkaz2Omron()
        {
            #region
            ClassDay.vypOtkazALL(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 20:00:00", DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 22:00:00", ModelLabel2.Text, Otkaz222);
            ClassDay.vypOtkazALL(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 22:00:00", DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59", ModelLabel2.Text, Otkaz200);
            //ClassDay.OtkazOmron(ModelLabel2.Text, st7, st8, Otkaz222);
            //ClassDay.OtkazOmron(ModelLabel2.Text, st8, st9, Otkaz200);
            ClassDay.OtkazOmron(ModelLabel2.Text, sttime0, sttime1, Otkaz210);
            ClassDay.OtkazOmron(ModelLabel2.Text, sttime1, sttime2, Otkaz212);
            ClassDay.OtkazOmron(ModelLabel2.Text, sttime2, sttime3, Otkaz214);
            ClassDay.OtkazOmron(ModelLabel2.Text, sttime3, sttime4, Otkaz216);
            //ClassDay.OtkazOmron(ModelLabel2.Text, sttime0, sttime4, Otkaz2All);
            ClassDay.vypOtkazALL(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 20:00:00", DateTime.Now.AddDays(0).ToString("yyyy-MM-dd") + " 08:00:00", ModelLabel2.Text, Otkaz2All);
            #endregion
        }
        void Otkaz3Omron()
        {
            #region
            ClassDay.OtkazOmron(ModelLabel2.Text, st7, st8, Otkaz222);
            ClassDay.OtkazOmron(ModelLabel2.Text, st8, st9, Otkaz200);
            ClassDay.OtkazOmron(ModelLabel3.Text, sttime0, sttime1, Otkaz310);
            ClassDay.OtkazOmron(ModelLabel3.Text, sttime1, sttime2, Otkaz312);
            ClassDay.OtkazOmron(ModelLabel3.Text, sttime2, sttime3, Otkaz314);
            ClassDay.OtkazOmron(ModelLabel3.Text, sttime3, sttime4, Otkaz316);
            //ClassDay.OtkazOmron(ModelLabel3.Text, sttime0, sttime0, Otkaz3All);
            ClassDay.vypOtkazALL(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 20:00:00", DateTime.Now.AddDays(0).ToString("yyyy-MM-dd") + " 08:00:00", ModelLabel3.Text, Otkaz3All);
            
            #endregion
        }
        void Otkaz4Omron()
        {
            #region
            ClassDay.OtkazOmron(ModelLabel4.Text, st7, st8, Otkaz422);
            ClassDay.OtkazOmron(ModelLabel4.Text, st8, st9, Otkaz400);
            ClassDay.OtkazOmron(ModelLabel4.Text, sttime0, sttime1, Otkaz410);
            ClassDay.OtkazOmron(ModelLabel4.Text, sttime1, sttime2, Otkaz412);
            ClassDay.OtkazOmron(ModelLabel4.Text, sttime2, sttime3, Otkaz414);
            ClassDay.OtkazOmron(ModelLabel4.Text, sttime3, sttime4, Otkaz416);
            //ClassDay.OtkazOmron(ModelLabel4.Text, sttime0, sttime0, Otkaz4All);
            ClassDay.vypOtkazALL(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 20:00:00", DateTime.Now.AddDays(0).ToString("yyyy-MM-dd") + " 08:00:00", ModelLabel4.Text, Otkaz4All);
            #endregion
        }
        void Otkaz5Omron()
        {
            #region
            ClassDay.OtkazOmron(ModelLabel5.Text, st7, st8, Otkaz522);
            ClassDay.OtkazOmron(ModelLabel5.Text, st8, st9, Otkaz500);
            ClassDay.OtkazOmron(ModelLabel5.Text, sttime0, sttime1, Otkaz510);
            ClassDay.OtkazOmron(ModelLabel5.Text, sttime1, sttime2, Otkaz512);
            ClassDay.OtkazOmron(ModelLabel5.Text, sttime2, sttime3, Otkaz514);
            ClassDay.OtkazOmron(ModelLabel5.Text, sttime3, sttime4, Otkaz516);
            //ClassDay.OtkazOmron(ModelLabel5.Text, sttime0, sttime0, Otkaz5All);
            ClassDay.vypOtkazALL(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 20:00:00", DateTime.Now.AddDays(0).ToString("yyyy-MM-dd") + " 08:00:00", ModelLabel5.Text, Otkaz5All);
            #endregion
        }
        void Otkaz6Omron()
        {
            #region
            ClassDay.OtkazOmron(ModelLabel6.Text,  st7, st8, Otkaz622);
            ClassDay.OtkazOmron(ModelLabel6.Text, st8, st9, Otkaz600);
            ClassDay.OtkazOmron(ModelLabel6.Text, sttime0, sttime1, Otkaz610);
            ClassDay.OtkazOmron(ModelLabel6.Text, sttime1, sttime2, Otkaz612);
            ClassDay.OtkazOmron(ModelLabel6.Text, sttime2, sttime3, Otkaz614);
            ClassDay.OtkazOmron(ModelLabel6.Text, sttime3, sttime4, Otkaz616);
            ClassDay.OtkazOmron(ModelLabel6.Text, sttime0, sttime0, Otkaz6All);
            ClassDay.vypOtkazALL(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 20:00:00", DateTime.Now.AddDays(0).ToString("yyyy-MM-dd") + " 08:00:00", ModelLabel6.Text, Otkaz6All);
            #endregion
        }
        void Otkaz7Omron()
        {
            #region
            ClassDay.OtkazOmron(ModelLabel7.Text, st7, st8, Otkaz722);
            ClassDay.OtkazOmron(ModelLabel7.Text, st8, st9, Otkaz700);
            ClassDay.OtkazOmron(ModelLabel7.Text, sttime0, sttime1, Otkaz710);
            ClassDay.OtkazOmron(ModelLabel7.Text, sttime1, sttime2, Otkaz712);
            ClassDay.OtkazOmron(ModelLabel7.Text, sttime2, sttime3, Otkaz714);
            ClassDay.OtkazOmron(ModelLabel7.Text, sttime3, sttime4, Otkaz716);
            //ClassDay.OtkazOmron(ModelLabel7.Text, sttime0, sttime0, Otkaz7All);
            ClassDay.vypOtkazALL(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 20:00:00", DateTime.Now.AddDays(0).ToString("yyyy-MM-dd") + " 08:00:00", ModelLabel7.Text, Otkaz7All);
            #endregion
        }

        void Vyp1Omron()
        {
            #region Запрос на выпуск с базы OMRON
            ClassDay.vypOmronALL(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 20:00:00", DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 22:00:00", ModelLabel1.Text, Vyp122);
            ClassDay.vypOmronALL(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 22:00:00", DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59", ModelLabel1.Text, Vyp100);
            //ClassDay.vypyskOmron(ModelLabel1.Text, st7, st8, Vyp122);
            //ClassDay.vypyskOmron(ModelLabel1.Text, st8, st9, Vyp100);
            ClassDay.vypyskOmron(ModelLabel1.Text, sttime0, sttime1, Vyp110);
            ClassDay.vypyskOmron(ModelLabel1.Text, sttime1, sttime2, Vyp112);
            ClassDay.vypyskOmron(ModelLabel1.Text, sttime2, sttime3, Vyp114);
            ClassDay.vypyskOmron(ModelLabel1.Text, sttime3, sttime4, Vyp116);
            //ClassDay.vypyskOmron(ModelLabel1.Text, sttime0, sttime4, Vyp1All);
            ClassDay.vypOmronALL(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") +  " 20:00:00", DateTime.Now.AddDays(0).ToString("yyyy-MM-dd") + " 08:00:00", ModelLabel1.Text, Vyp1All);

            #endregion
        }
        void Vyp2Omron()
        {
            #region Запрос на выпуск с базы OMRON
            ClassDay.vypyskOmron(ModelLabel2.Text, st7, st8, Vyp222);
            ClassDay.vypyskOmron(ModelLabel2.Text, st8, st9, Vyp200);
            ClassDay.vypyskOmron(ModelLabel2.Text, sttime0, sttime1, Vyp210);
            ClassDay.vypyskOmron(ModelLabel2.Text, sttime1, sttime2, Vyp212);
            ClassDay.vypyskOmron(ModelLabel2.Text, sttime2, sttime3, Vyp214);
            ClassDay.vypyskOmron(ModelLabel2.Text, sttime3, sttime4, Vyp216);
            //ClassDay.vypyskOmron(ModelLabel2.Text, sttime0, sttime4, Vyp2All);
            ClassDay.vypOmronALL(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 20:00:00", DateTime.Now.AddDays(0).ToString("yyyy-MM-dd") + " 08:00:00", ModelLabel2.Text, Vyp2All);
            #endregion
        }
        void Vyp3Omron()
        {
            #region Запрос на выпуск с базы OMRON
            ClassDay.vypyskOmron(ModelLabel3.Text, st7, st8, Vyp322);
            ClassDay.vypyskOmron(ModelLabel3.Text, st8, st9, Vyp300);
            ClassDay.vypyskOmron(ModelLabel3.Text, sttime0, sttime1, Vyp310);
            ClassDay.vypyskOmron(ModelLabel3.Text, sttime1, sttime2, Vyp312);
            ClassDay.vypyskOmron(ModelLabel3.Text, sttime2, sttime3, Vyp314);
            ClassDay.vypyskOmron(ModelLabel3.Text, sttime3, sttime4, Vyp316);
            //ClassDay.vypyskOmron(ModelLabel3.Text, sttime0, sttime4, Vyp3All);
            ClassDay.vypOmronALL(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 20:00:00", DateTime.Now.AddDays(0).ToString("yyyy-MM-dd") + " 08:00:00", ModelLabel3.Text, Vyp3All);

            #endregion
        }
        void Vyp4Omron()
        {
            #region Запрос на выпуск с базы OMRON
            ClassDay.vypyskOmron(ModelLabel4.Text, st7, st8, Vyp422);
            ClassDay.vypyskOmron(ModelLabel4.Text, st8, st9, Vyp400);
            ClassDay.vypyskOmron(ModelLabel4.Text, sttime0, sttime1, Vyp410);
            ClassDay.vypyskOmron(ModelLabel4.Text, sttime1, sttime2, Vyp412);
            ClassDay.vypyskOmron(ModelLabel4.Text, sttime2, sttime3, Vyp414);
            ClassDay.vypyskOmron(ModelLabel4.Text, sttime3, sttime4, Vyp416);
            ClassDay.vypyskOmron(ModelLabel4.Text, sttime0, sttime4, Vyp4All);
            ClassDay.vypOmronALL(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 20:00:00", DateTime.Now.AddDays(0).ToString("yyyy-MM-dd") + " 08:00:00", ModelLabel4.Text, Vyp4All);
            #endregion
        }
        void Vyp5Omron()
        {
            #region Запрос на выпуск с базы OMRON
            ClassDay.vypyskOmron(ModelLabel5.Text,  st7, st8, Vyp522);
            ClassDay.vypyskOmron(ModelLabel5.Text, st8, st9, Vyp500);
            ClassDay.vypyskOmron(ModelLabel5.Text, sttime0, sttime1, Vyp510);
            ClassDay.vypyskOmron(ModelLabel5.Text, sttime1, sttime2, Vyp512);
            ClassDay.vypyskOmron(ModelLabel5.Text, sttime2, sttime3, Vyp514);
            ClassDay.vypyskOmron(ModelLabel5.Text, sttime3, sttime4, Vyp516);
            //ClassDay.vypyskOmron(ModelLabel5.Text, sttime0, sttime4, Vyp5All);
            ClassDay.vypOmronALL(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 20:00:00", DateTime.Now.AddDays(0).ToString("yyyy-MM-dd") + " 08:00:00", ModelLabel5.Text, Vyp5All);
            #endregion
        }
        void Vyp6Omron()
        {
            #region Запрос на выпуск с базы OMRON
            ClassDay.vypyskOmron(ModelLabel6.Text,  st7, st8, Vyp622);
            ClassDay.vypyskOmron(ModelLabel6.Text, st8, st9, Vyp600);
            ClassDay.vypyskOmron(ModelLabel6.Text, sttime0, sttime1, Vyp610);
            ClassDay.vypyskOmron(ModelLabel6.Text, sttime1, sttime2, Vyp612);
            ClassDay.vypyskOmron(ModelLabel6.Text, sttime2, sttime3, Vyp614);
            ClassDay.vypyskOmron(ModelLabel6.Text, sttime3, sttime4, Vyp616);
            //ClassDay.vypyskOmron(ModelLabel6.Text, sttime0, sttime4, Vyp6All);
            ClassDay.vypOmronALL(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 20:00:00", DateTime.Now.AddDays(0).ToString("yyyy-MM-dd") + " 08:00:00", ModelLabel6.Text, Vyp6All);

            #endregion
        }
        void Vyp7Omron()
        {
            #region Запрос на выпуск с базы OMRON
            ClassDay.vypyskOmron(ModelLabel7.Text, st7, st8, Vyp722);
            ClassDay.vypyskOmron(ModelLabel7.Text, st8, st9, Vyp700);
            ClassDay.vypyskOmron(ModelLabel7.Text, sttime0, sttime1, Vyp710);
            ClassDay.vypyskOmron(ModelLabel7.Text, sttime1, sttime2, Vyp712);
            ClassDay.vypyskOmron(ModelLabel7.Text, sttime2, sttime3, Vyp714);
            ClassDay.vypyskOmron(ModelLabel7.Text, sttime3, sttime4, Vyp716);
            ClassDay.vypyskOmron(ModelLabel7.Text, sttime0, sttime4, Vyp7All);
            ClassDay.vypOmronALL(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 20:00:00", DateTime.Now.AddDays(0).ToString("yyyy-MM-dd") + " 08:00:00", ModelLabel7.Text, Vyp7All);
            #endregion
        }

    }
}
