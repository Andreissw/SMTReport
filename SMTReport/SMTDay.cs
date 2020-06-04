using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace SMTReport
{
    public partial class SMTDay : Form
    {
        public SMTDay()
        {
            InitializeComponent();
        }

     
        //public static DateTime now = DateTime.Now.AddDays(-6);
        string StartdateOmron;
        string EnddateOmron;
        string sttime0, sttime1, sttime2, sttime3, sttime4, sttime5, sttime6, sttime7, sttime8;
        double TH, TA, bot, top, GSC593TOP, GSC593bot, B621Top,B621Bot;
        
        string endtime1, endtime2, endtime3, endtime4, endtime5, endtime6, endtime7, endtime8;

        private void timer1_Tick(object sender, EventArgs e)
        {
            yearLB.Text = DateTime.Now.ToString("dd.MM.yyyy");
            HrLB.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void BtOK_Click(object sender, EventArgs e)
        {
            
        }

        bool bolean = false;
        private void SMTDay_Load(object sender, EventArgs e)
        {
          
            
            ОбновлениеЭкрана();
            if (Start_Form.timesOK == true)
            {
                ГлавныйТаймер.Enabled = true;
            }
           
        }

                  

        bool timesOK = false;
      

        public static int count = 10;
        private void ГлавныйТаймер_Tick(object sender, EventArgs e)
        {
            
                 //this.Select();
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
            ClassDay.Screens(this.Width - 10, this.Height -10, "" + count.ToString() + ".jpg");
            ScreenShot.Enabled = false;
            Отправка.Enabled = true;
        }

        private void Отправка_Tick(object sender, EventArgs e)
        {
            ClassNight.SendEmail(@"C:\Скриншот\" + count.ToString() + ".jpg", "SMT-Дневная-Карта");
            Отправка.Enabled = false;
            this.Close();
        }

        void очистка()
        {
            All.RowCount = 1;
            ModelLabel1.Text = "-";
            ModelLabel2.Text = "-";
            ModelLabel3.Text = "-";
            ModelLabel4.Text = "-";
            ModelLabel5.Text = "-";
            ModelLabel6.Text = "-";
            ModelLabel7.Text = "-";
            LineLabel1.Text = "-";
            LineLabel2.Text = "-";
            LineLabel3.Text = "-";
            LineLabel4.Text = "-";
            LineLabel5.Text = "-";
            LineLabel6.Text = "-";
            LineLabel7.Text = "-";
        }

        private void ОбновлениеЭкрана(int k = 0)
        {
            try
            {
            DateTime now = DateTime.Now.AddDays(0);
            очистка();
            All.Rows.Add(25);

                GSC593TOP = 95.80;
                GSC593bot = 98.00;

                B621Top = 93.20;
                B621Bot = 97.00;

                TA = 98.00;

                bot = 98.00;
                top = 95.80;

                #region Время переменные
                sttime0 = "06:00";
                sttime1 = "08:00";
                sttime2 = "10:00";
                sttime3 = "12:00";
                sttime4 = "14:00";
                sttime5 = "16:00";
                sttime6 = "18:00";
                sttime7 = "20:00";
                sttime8 = "22:00";

                endtime1 = "10:00";
                endtime2 = "12:00";
                endtime3 = "14:00";
                endtime4 = "16:00";
                endtime5 = "18:00";
                endtime6 = "20:00";
                endtime7 = "22:00";
                endtime8 = "23:59";
            #endregion


            //DateTime.Now = now.AddDays(k);
            StartdateOmron = now.ToString("yyyy-MM-dd");


            EnddateOmron = now.AddDays(1).ToString("yyyy-MM-dd");
                yearLB.Text = now.ToString("dd.MM.yyyy");
                HrLB.Text = now.ToString("HH:mm:ss");

                //if (Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss")) <= Convert.ToDateTime(now.ToString("09:00:00")))
                //{
                //    StatusLabel();
                //}
                //else if (Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss")) >= Convert.ToDateTime(now.ToString("22:00:00")))
                //{
                //    StatusLabel();
                //}

                ClassDay.SpisokModels(DG, DGOracle, All, now.ToString("dd.MM.yyyy"), StartdateOmron, EnddateOmron);


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
                else if (Status1.Text == "Omron") { Vyp1Omron(); Otkaz1Omron(); FPY1(); ClassDay.Топ_Omron(TopLine1, now.ToString("yyyy-MM-dd"), now.ToString("yyyy-MM-dd"), "08:00:00", "20:00:00", ModelLabel1.Text); }
                else if (Status1.Text == "Oracle") { Vypysk1Oracle(); Otkaz1Oracle(); FPY1(); ClassDay.ТопОшибокSMT(TopLine1, now.ToString("dd.MM.yyyy"), "06:00:00", "21:59:59", ModelLabel1.Text, LabelLine1.Text); }

                if (Status2.Text == "-")
                    SMTGroup2.Visible = false;
                else if (Status2.Text == "Omron") { Vyp2Omron(); Otkaz2Omron(); FPY2(); ClassDay.Топ_Omron(TopLine2, now.ToString("yyyy-MM-dd"), now.ToString("yyyy-MM-dd"), "08:00:00", "20:00:00", ModelLabel2.Text); }
                else if (Status2.Text == "Oracle") { Vypysk2Oracle(); Otkaz2Oracle(); FPY2(); ClassDay.ТопОшибокSMT(TopLine2, now.ToString("dd.MM.yyyy"), "06:00:00", "21:59:59", ModelLabel2.Text, LabelLine2.Text); }

                if (Status3.Text == "-")
                    SMTGroup3.Visible = false;
                else if (Status3.Text == "Omron") { Vyp3Omron(); Otkaz3Omron(); FPY3(); ClassDay.Топ_Omron(TopLine3, now.ToString("yyyy-MM-dd"), now.ToString("yyyy-MM-dd") , "08:00:00", "20:00:00", ModelLabel3.Text); }
                else if (Status3.Text == "Oracle") { Vypysk3Oracle(); Otkaz3Oracle(); FPY3(); ClassDay.ТопОшибокSMT(TopLine3, now.ToString("dd.MM.yyyy"), "06:00:00", "21:59:59", ModelLabel3.Text, LabelLine3.Text); }

                if (Status4.Text == "-")
                    SMTGroup4.Visible = false;
                else if (Status4.Text == "Omron") { Vyp4Omron(); Otkaz4Omron(); FPY4(); ClassDay.Топ_Omron(TopLine4, now.ToString("yyyy-MM-dd"), now.ToString("yyyy-MM-dd"), "08:00:00", "20:00:00", ModelLabel4.Text); }
                else if (Status4.Text == "Oracle") { Vypysk4Oracle(); Otkaz4Oracle(); FPY4(); ClassDay.ТопОшибокSMT(TopLine4, now.ToString("dd.MM.yyyy"), "06:00:00", "21:59:59", ModelLabel4.Text, LabelLine4.Text); }

                if (Status5.Text == "-")
                    SMTGroup5.Visible = false;
                else if (Status5.Text == "Omron") { Vyp5Omron(); Otkaz5Omron(); FPY5(); ClassDay.Топ_Omron(TopLine5, now.ToString("yyyy-MM-dd"), now.ToString("yyyy-MM-dd"), "08:00:00", "20:00:00", ModelLabel5.Text); }
                else if (Status5.Text == "Oracle") { Vypysk5Oracle(); Otkaz5Oracle(); FPY5(); ClassDay.ТопОшибокSMT(TopLine5, now.ToString("dd.MM.yyyy"), "06:00:00", "21:59:59", ModelLabel5.Text, LabelLine5.Text); }

                if (Status6.Text == "-")
                    SMTGroup6.Visible = false;
                else if (Status6.Text == "Omron") { Vyp6Omron(); Otkaz6Omron(); FPY6(); ClassDay.Топ_Omron(TopLine6, now.ToString("yyyy-MM-dd"), now.ToString("yyyy-MM-dd"), "08:00:00", "20:00:00", ModelLabel6.Text); }
                else if (Status6.Text == "Oracle") { Vypysk6Oracle(); Otkaz6Oracle(); FPY6(); ClassDay.ТопОшибокSMT(TopLine6, now.ToString("dd.MM.yyyy"), "06:00:00", "21:59:59", ModelLabel6.Text, LabelLine6.Text); }

                if (Status7.Text == "-")
                    SMTGroup7.Visible = false;
                else if (Status7.Text == "Omron") { Vyp7Omron(); Otkaz7Omron(); FPY7(); ClassDay.Топ_Omron(TopLine7, now.ToString("yyyy-MM-dd"), now.ToString("yyyy-MM-dd"),"08:00:00", "20:00:00", ModelLabel7.Text); }
                else if (Status7.Text == "Oracle") { Vypysk7Oracle(); Otkaz7Oracle(); FPY7(); ClassDay.ТопОшибокSMT(TopLine7, now.ToString("dd.MM.yyyy"), "06:00:00", "21:59:59", ModelLabel7.Text, LabelLine7.Text); }

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
                
                //ClassDay.ТопОшибокSMTOmron(TopLine1, now.ToString("yyyy-MM-dd"), "08:00:00", "23:59:59", ModelLabel1.Text);
              
                //ClassDay.Топ_Omron(TopLine1, now.ToString("yyyy-MM-dd"), "08:00:00", "20:00:00",ModelLabel1.Text);

                

            }
            catch (Exception e)
            {
                //StatusLabel();
                MessageBox.Show(e.Message);
            }
        }

        private void StatusLabel()
        {
            label28.Text = "Status NOK";
            label28.BackColor = Color.Red;
        }

        private void FPYColor1()
        {
            ClassDay.FPYColor(FPY110, Convert.ToDouble(Cell1.Text), ModelLabel1.Text);
            ClassDay.FPYColor(FPY112, Convert.ToDouble(Cell1.Text), ModelLabel1.Text);
            ClassDay.FPYColor(FPY114, Convert.ToDouble(Cell1.Text), ModelLabel1.Text);
            ClassDay.FPYColor(FPY116, Convert.ToDouble(Cell1.Text), ModelLabel1.Text);
            ClassDay.FPYColor(FPY118, Convert.ToDouble(Cell1.Text), ModelLabel1.Text);
            ClassDay.FPYColor(FPY120, Convert.ToDouble(Cell1.Text), ModelLabel1.Text);
     
            ClassDay.FPYColor(FPY1All, Convert.ToDouble(Cell1.Text), ModelLabel1.Text);
        }
        private void FPYColor2()
        {
            ClassDay.FPYColor(FPY210, Convert.ToDouble(Cel2.Text), ModelLabel2.Text);
            ClassDay.FPYColor(FPY212, Convert.ToDouble(Cel2.Text), ModelLabel2.Text);
            ClassDay.FPYColor(FPY214, Convert.ToDouble(Cel2.Text), ModelLabel2.Text);
            ClassDay.FPYColor(FPY216, Convert.ToDouble(Cel2.Text), ModelLabel2.Text);
            ClassDay.FPYColor(FPY218, Convert.ToDouble(Cel2.Text), ModelLabel2.Text);
            ClassDay.FPYColor(FPY220, Convert.ToDouble(Cel2.Text), ModelLabel2.Text);
         
            ClassDay.FPYColor(FPY2All, Convert.ToDouble(Cel2.Text), ModelLabel2.Text);
        }
        private void FPYColor3()
        {
            ClassDay.FPYColor(FPY310, Convert.ToDouble(Cel3.Text), ModelLabel3.Text);
            ClassDay.FPYColor(FPY312, Convert.ToDouble(Cel3.Text), ModelLabel3.Text);
            ClassDay.FPYColor(FPY314, Convert.ToDouble(Cel3.Text), ModelLabel3.Text);
            ClassDay.FPYColor(FPY316, Convert.ToDouble(Cel3.Text), ModelLabel3.Text);
            ClassDay.FPYColor(FPY318, Convert.ToDouble(Cel3.Text), ModelLabel3.Text);
            ClassDay.FPYColor(FPY320, Convert.ToDouble(Cel3.Text), ModelLabel3.Text);
       
            ClassDay.FPYColor(FPY3All, Convert.ToDouble(Cel3.Text), ModelLabel3.Text);
        }
        private void FPYColor4()
        {
            ClassDay.FPYColor(FPY410, Convert.ToDouble(Cel4.Text), ModelLabel4.Text);
            ClassDay.FPYColor(FPY412, Convert.ToDouble(Cel4.Text), ModelLabel4.Text);
            ClassDay.FPYColor(FPY414, Convert.ToDouble(Cel4.Text), ModelLabel4.Text);
            ClassDay.FPYColor(FPY416, Convert.ToDouble(Cel4.Text), ModelLabel4.Text);
            ClassDay.FPYColor(FPY418, Convert.ToDouble(Cel4.Text), ModelLabel4.Text);
            ClassDay.FPYColor(FPY420, Convert.ToDouble(Cel4.Text), ModelLabel4.Text);
            
            ClassDay.FPYColor(FPY4All, Convert.ToDouble(Cel4.Text), ModelLabel4.Text);
        }
        private void FPYColor5()
        {
            ClassDay.FPYColor(FPY510, Convert.ToDouble(Cel5.Text), ModelLabel5.Text);
            ClassDay.FPYColor(FPY512, Convert.ToDouble(Cel5.Text), ModelLabel5.Text);
            ClassDay.FPYColor(FPY514, Convert.ToDouble(Cel5.Text), ModelLabel5.Text);
            ClassDay.FPYColor(FPY516, Convert.ToDouble(Cel5.Text), ModelLabel5.Text);
            ClassDay.FPYColor(FPY518, Convert.ToDouble(Cel5.Text), ModelLabel5.Text);
            ClassDay.FPYColor(FPY520, Convert.ToDouble(Cel5.Text), ModelLabel5.Text);
        
            ClassDay.FPYColor(FPY5All, Convert.ToDouble(Cel5.Text), ModelLabel5.Text);
        }
        private void FPYColor6()
        {
            ClassDay.FPYColor(FPY610, Convert.ToDouble(Cel6.Text), ModelLabel6.Text);
            ClassDay.FPYColor(FPY612, Convert.ToDouble(Cel6.Text), ModelLabel6.Text);
            ClassDay.FPYColor(FPY614, Convert.ToDouble(Cel6.Text), ModelLabel6.Text);
            ClassDay.FPYColor(FPY616, Convert.ToDouble(Cel6.Text), ModelLabel6.Text);
            ClassDay.FPYColor(FPY618, Convert.ToDouble(Cel6.Text), ModelLabel6.Text);
            ClassDay.FPYColor(FPY620, Convert.ToDouble(Cel6.Text), ModelLabel6.Text);
       
            ClassDay.FPYColor(FPY6All, Convert.ToDouble(Cel6.Text), ModelLabel6.Text);
        }
        private void FPYColor7()
        {
            ClassDay.FPYColor(FPY710, Convert.ToDouble(Cel7.Text), ModelLabel7.Text);
            ClassDay.FPYColor(FPY712, Convert.ToDouble(Cel7.Text), ModelLabel7.Text);
            ClassDay.FPYColor(FPY714, Convert.ToDouble(Cel7.Text), ModelLabel7.Text);
            ClassDay.FPYColor(FPY716, Convert.ToDouble(Cel7.Text), ModelLabel7.Text);
            ClassDay.FPYColor(FPY718, Convert.ToDouble(Cel7.Text), ModelLabel7.Text);
            ClassDay.FPYColor(FPY720, Convert.ToDouble(Cel7.Text), ModelLabel7.Text);
          
            ClassDay.FPYColor(FPY7All, Convert.ToDouble(Cel7.Text), ModelLabel7.Text);
        }


        private void cells(Label cel, string model) //цель 
        {
            if (model == "-") { }

            else if (model.Contains("561"))
                cel.Text = Convert.ToString(TA);

            else if (model.Contains("562"))
                cel.Text = Convert.ToString(TA);


            else if (model.Contains("621"))
            {
                if (model.Contains("top"))
                {
                    cel.Text = Convert.ToString(B621Top);

                }
                else if (model.Contains("bot"))
                {
                    cel.Text = Convert.ToString(B621Bot);
                }
            }

            else
            {
                if (model.Contains("top"))
                {
                    cel.Text = Convert.ToString(GSC593TOP);

                }
                else if (model.Contains("bot"))
                {
                    cel.Text = Convert.ToString(GSC593TOP);
                }
                else
                {
                    cel.Text = "96";
                }
            }
        }

         

        void FPY1()
        {
            ClassDay.FPY(Vyp110, Otkaz110, FPY110);
            ClassDay.FPY(Vyp112, Otkaz112, FPY112);
            ClassDay.FPY(Vyp114, Otkaz114, FPY114);
            ClassDay.FPY(Vyp116, Otkaz116, FPY116);
            ClassDay.FPY(Vyp118, Otkaz118, FPY118);
            ClassDay.FPY(Vyp120, Otkaz120, FPY120);
         
            ClassDay.FPY(Vyp1All, Otkaz1All, FPY1All);
        }
        void FPY2()
        {
            ClassDay.FPY(Vyp210, Otkaz210, FPY210);
            ClassDay.FPY(Vyp212, Otkaz212, FPY212);
            ClassDay.FPY(Vyp214, Otkaz214, FPY214);
            ClassDay.FPY(Vyp216, Otkaz216, FPY216);
            ClassDay.FPY(Vyp218, Otkaz218, FPY218);
            ClassDay.FPY(Vyp220, Otkaz220, FPY220);
         
            ClassDay.FPY(Vyp2All, Otkaz2All, FPY2All);
        }
        void FPY3()
        {
            ClassDay.FPY(Vyp310, Otkaz310, FPY310);
            ClassDay.FPY(Vyp312, Otkaz312, FPY312);
            ClassDay.FPY(Vyp314, Otkaz314, FPY314);
            ClassDay.FPY(Vyp316, Otkaz316, FPY316);
            ClassDay.FPY(Vyp318, Otkaz318, FPY318);
            ClassDay.FPY(Vyp320, Otkaz320, FPY320);
     
            ClassDay.FPY(Vyp3All, Otkaz3All, FPY3All);
        }
        void FPY4()
        {
            ClassDay.FPY(Vyp410, Otkaz410, FPY410);
            ClassDay.FPY(Vyp412, Otkaz412, FPY412);
            ClassDay.FPY(Vyp414, Otkaz414, FPY414);
            ClassDay.FPY(Vyp416, Otkaz416, FPY416);
            ClassDay.FPY(Vyp418, Otkaz418, FPY418);
            ClassDay.FPY(Vyp420, Otkaz420, FPY420);
        
            ClassDay.FPY(Vyp4All, Otkaz4All, FPY4All);
        }
        void FPY5()
        {
            ClassDay.FPY(Vyp510, Otkaz510, FPY510);
            ClassDay.FPY(Vyp512, Otkaz512, FPY512);
            ClassDay.FPY(Vyp514, Otkaz514, FPY514);
            ClassDay.FPY(Vyp516, Otkaz516, FPY516);
            ClassDay.FPY(Vyp518, Otkaz518, FPY518);
            ClassDay.FPY(Vyp520, Otkaz520, FPY520);
           
            ClassDay.FPY(Vyp5All, Otkaz5All, FPY5All);
        }
        void FPY6()
        {
            ClassDay.FPY(Vyp610, Otkaz610, FPY610);
            ClassDay.FPY(Vyp612, Otkaz612, FPY612);
            ClassDay.FPY(Vyp614, Otkaz614, FPY614);
            ClassDay.FPY(Vyp616, Otkaz616, FPY616);
            ClassDay.FPY(Vyp618, Otkaz618, FPY618);
            ClassDay.FPY(Vyp620, Otkaz620, FPY620);
    
            ClassDay.FPY(Vyp6All, Otkaz6All, FPY6All);
        }
        void FPY7()
        {
            ClassDay.FPY(Vyp710, Otkaz710, FPY710);
            ClassDay.FPY(Vyp712, Otkaz712, FPY712);
            ClassDay.FPY(Vyp714, Otkaz714, FPY714);
            ClassDay.FPY(Vyp716, Otkaz716, FPY716);
            ClassDay.FPY(Vyp718, Otkaz718, FPY718);
            ClassDay.FPY(Vyp720, Otkaz720, FPY720);
         
            ClassDay.FPY(Vyp7All, Otkaz7All, FPY7All);
        }

        int день = 0;

        void Vypysk1Oracle()
        {
            ClassDay.vypyskOracle(ModelLabel1.Text, sttime0, sttime1, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp110, LabelLine1.Text);
            ClassDay.vypyskOracle(ModelLabel1.Text, sttime1, sttime2, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp112, LabelLine1.Text);
            ClassDay.vypyskOracle(ModelLabel1.Text, sttime2, sttime3, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp114, LabelLine1.Text);
            ClassDay.vypyskOracle(ModelLabel1.Text, sttime3, sttime4, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp116, LabelLine1.Text);
            ClassDay.vypyskOracle(ModelLabel1.Text, sttime4, sttime5, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp118, LabelLine1.Text);
            ClassDay.vypyskOracle(ModelLabel1.Text, sttime5, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp120, LabelLine1.Text);
       
            ClassDay.vypyskOracle(ModelLabel1.Text, sttime0, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp1All, LabelLine1.Text);
        }
        void Vypysk2Oracle()
        {
            ClassDay.vypyskOracle(ModelLabel2.Text, sttime0, sttime1, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp210, LabelLine2.Text);
            ClassDay.vypyskOracle(ModelLabel2.Text, sttime1, sttime2, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp212, LabelLine2.Text);
            ClassDay.vypyskOracle(ModelLabel2.Text, sttime2, sttime3, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp214, LabelLine2.Text);
            ClassDay.vypyskOracle(ModelLabel2.Text, sttime3, sttime4, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp216, LabelLine2.Text);
            ClassDay.vypyskOracle(ModelLabel2.Text, sttime4, sttime5, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp218, LabelLine2.Text);
            ClassDay.vypyskOracle(ModelLabel2.Text, sttime5, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp220, LabelLine2.Text);
          
            ClassDay.vypyskOracle(ModelLabel2.Text, sttime0, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp2All, LabelLine2.Text);
        }
        void Vypysk3Oracle()
        {
            ClassDay.vypyskOracle(ModelLabel3.Text, sttime0, sttime1, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp310, LabelLine3.Text);
            ClassDay.vypyskOracle(ModelLabel3.Text, sttime1, sttime2, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp312, LabelLine3.Text);
            ClassDay.vypyskOracle(ModelLabel3.Text, sttime2, sttime3, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp314, LabelLine3.Text);
            ClassDay.vypyskOracle(ModelLabel3.Text, sttime3, sttime4, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp316, LabelLine3.Text);
            ClassDay.vypyskOracle(ModelLabel3.Text, sttime4, sttime5, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp318, LabelLine3.Text);
            ClassDay.vypyskOracle(ModelLabel3.Text, sttime5, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp320, LabelLine3.Text);
           
            ClassDay.vypyskOracle(ModelLabel3.Text, sttime0, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp3All, LabelLine3.Text);
        }
        void Vypysk4Oracle()
        {
            ClassDay.vypyskOracle(ModelLabel4.Text, sttime0, sttime1, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp410, LabelLine4.Text);
            ClassDay.vypyskOracle(ModelLabel4.Text, sttime1, sttime2, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp412, LabelLine4.Text);
            ClassDay.vypyskOracle(ModelLabel4.Text, sttime2, sttime3, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp414, LabelLine4.Text);
            ClassDay.vypyskOracle(ModelLabel4.Text, sttime3, sttime4, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp416, LabelLine4.Text);
            ClassDay.vypyskOracle(ModelLabel4.Text, sttime4, sttime5, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp418, LabelLine4.Text);
            ClassDay.vypyskOracle(ModelLabel4.Text, sttime5, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp420, LabelLine4.Text);
        
            ClassDay.vypyskOracle(ModelLabel4.Text, sttime0, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp4All, LabelLine4.Text);
        }
        void Vypysk5Oracle()
        {
            ClassDay.vypyskOracle(ModelLabel5.Text, sttime0, sttime1, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp510, LabelLine5.Text);
            ClassDay.vypyskOracle(ModelLabel5.Text, sttime1, sttime2, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp512, LabelLine5.Text);
            ClassDay.vypyskOracle(ModelLabel5.Text, sttime2, sttime3, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp514, LabelLine5.Text);
            ClassDay.vypyskOracle(ModelLabel5.Text, sttime3, sttime4, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp516, LabelLine5.Text);
            ClassDay.vypyskOracle(ModelLabel5.Text, sttime4, sttime5, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp518, LabelLine5.Text);
            ClassDay.vypyskOracle(ModelLabel5.Text, sttime5, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp520, LabelLine5.Text);
         
            ClassDay.vypyskOracle(ModelLabel5.Text, sttime0, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp5All, LabelLine5.Text);
        }
        void Vypysk6Oracle()
        {
            ClassDay.vypyskOracle(ModelLabel6.Text, sttime0, sttime1, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp610, LabelLine6.Text);
            ClassDay.vypyskOracle(ModelLabel6.Text, sttime1, sttime2, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp612, LabelLine6.Text);
            ClassDay.vypyskOracle(ModelLabel6.Text, sttime2, sttime3, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp614, LabelLine6.Text);
            ClassDay.vypyskOracle(ModelLabel6.Text, sttime3, sttime4, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp616, LabelLine6.Text);
            ClassDay.vypyskOracle(ModelLabel6.Text, sttime4, sttime5, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp618, LabelLine6.Text);
            ClassDay.vypyskOracle(ModelLabel6.Text, sttime5, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp620, LabelLine6.Text);
     
            ClassDay.vypyskOracle(ModelLabel6.Text, sttime0, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp6All, LabelLine6.Text);
        }
        void Vypysk7Oracle()
        {
            ClassDay.vypyskOracle(ModelLabel7.Text, sttime0, sttime1, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp710, LabelLine7.Text);
            ClassDay.vypyskOracle(ModelLabel7.Text, sttime1, sttime2, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp712, LabelLine7.Text);
            ClassDay.vypyskOracle(ModelLabel7.Text, sttime2, sttime3, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp714, LabelLine7.Text);
            ClassDay.vypyskOracle(ModelLabel7.Text, sttime3, sttime4, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp716, LabelLine7.Text);
            ClassDay.vypyskOracle(ModelLabel7.Text, sttime4, sttime5, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp718, LabelLine7.Text);
            ClassDay.vypyskOracle(ModelLabel7.Text, sttime5, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp720, LabelLine7.Text);
        
            ClassDay.vypyskOracle(ModelLabel7.Text, sttime0, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Vyp7All, LabelLine7.Text);
        }

        void Otkaz1Oracle()
        {
            ClassDay.OtkazOracle(ModelLabel1.Text, sttime0, sttime1, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz110, LabelLine1.Text);
            ClassDay.OtkazOracle(ModelLabel1.Text, sttime1, sttime2, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz112, LabelLine1.Text);
            ClassDay.OtkazOracle(ModelLabel1.Text, sttime2, sttime3, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz114, LabelLine1.Text);
            ClassDay.OtkazOracle(ModelLabel1.Text, sttime3, sttime4, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz116, LabelLine1.Text);
            ClassDay.OtkazOracle(ModelLabel1.Text, sttime4, sttime5, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz118, LabelLine1.Text);
            ClassDay.OtkazOracle(ModelLabel1.Text, sttime5, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz120, LabelLine1.Text);

            ClassDay.OtkazOracle(ModelLabel1.Text, sttime0, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz1All, LabelLine1.Text);
        }
        void Otkaz2Oracle()
        {
            ClassDay.OtkazOracle(ModelLabel2.Text, sttime0, sttime1, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz210, LabelLine2.Text);
            ClassDay.OtkazOracle(ModelLabel2.Text, sttime1, sttime2, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz212, LabelLine2.Text);
            ClassDay.OtkazOracle(ModelLabel2.Text, sttime2, sttime3, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz214, LabelLine2.Text);
            ClassDay.OtkazOracle(ModelLabel2.Text, sttime3, sttime4, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz216, LabelLine2.Text);
            ClassDay.OtkazOracle(ModelLabel2.Text, sttime4, sttime5, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz218, LabelLine2.Text);
            ClassDay.OtkazOracle(ModelLabel2.Text, sttime5, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz220, LabelLine2.Text);
        
            ClassDay.OtkazOracle(ModelLabel2.Text, sttime0, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz2All,LabelLine2.Text);
        }
        void Otkaz3Oracle()
        {
            ClassDay.OtkazOracle(ModelLabel3.Text, sttime0, sttime1, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz310, LabelLine3.Text);
            ClassDay.OtkazOracle(ModelLabel3.Text, sttime1, sttime2, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz312, LabelLine3.Text);
            ClassDay.OtkazOracle(ModelLabel3.Text, sttime2, sttime3, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz314, LabelLine3.Text);
            ClassDay.OtkazOracle(ModelLabel3.Text, sttime3, sttime4, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz316, LabelLine3.Text);
            ClassDay.OtkazOracle(ModelLabel3.Text, sttime4, sttime5, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz318, LabelLine3.Text);
            ClassDay.OtkazOracle(ModelLabel3.Text, sttime5, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz320, LabelLine3.Text);
        
            ClassDay.OtkazOracle(ModelLabel3.Text, sttime0, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz3All, LabelLine3.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
                if (checkBox1.Checked == true)
            {
                StatusOKLabel();
            }
            else
                {
                    StatusLabel();
                }
           
        }

        private void StatusOKLabel()
        {
            label28.Text = "Status OK";
            label28.BackColor = Color.Gainsboro;
        }

        private void GroupTop_Enter(object sender, EventArgs e)
        {

        }

        void Otkaz4Oracle()
        {
            ClassDay.OtkazOracle(ModelLabel4.Text, sttime0, sttime1, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz410, LabelLine4.Text);
            ClassDay.OtkazOracle(ModelLabel4.Text, sttime1, sttime2, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz412, LabelLine4.Text);
            ClassDay.OtkazOracle(ModelLabel4.Text, sttime2, sttime3, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz414, LabelLine4.Text);
            ClassDay.OtkazOracle(ModelLabel4.Text, sttime3, sttime4, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz416, LabelLine4.Text);
            ClassDay.OtkazOracle(ModelLabel4.Text, sttime4, sttime5, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz418, LabelLine4.Text);
            ClassDay.OtkazOracle(ModelLabel4.Text, sttime5, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz420, LabelLine4.Text);
        
            ClassDay.OtkazOracle(ModelLabel4.Text, sttime0, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz4All, LabelLine4.Text);
        }
        void Otkaz5Oracle()
        {
            ClassDay.OtkazOracle(ModelLabel5.Text, sttime0, sttime1, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"),  Otkaz510, LabelLine5.Text);
            ClassDay.OtkazOracle(ModelLabel5.Text, sttime1, sttime2, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"),  Otkaz512, LabelLine5.Text);
            ClassDay.OtkazOracle(ModelLabel5.Text, sttime2, sttime3, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"),  Otkaz514, LabelLine5.Text);
            ClassDay.OtkazOracle(ModelLabel5.Text, sttime3, sttime4, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"),  Otkaz516, LabelLine5.Text);
            ClassDay.OtkazOracle(ModelLabel5.Text, sttime4, sttime5, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"),  Otkaz518, LabelLine5.Text);
            ClassDay.OtkazOracle(ModelLabel5.Text, sttime5, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"),  Otkaz520, LabelLine5.Text);

            ClassDay.OtkazOracle(ModelLabel5.Text, sttime0, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"),  Otkaz5All, LabelLine5.Text);
        }
        void Otkaz6Oracle()
        {
            ClassDay.OtkazOracle(ModelLabel6.Text, sttime0, sttime1, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz610, LabelLine6.Text);
            ClassDay.OtkazOracle(ModelLabel6.Text, sttime1, sttime2, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz612, LabelLine6.Text);
            ClassDay.OtkazOracle(ModelLabel6.Text, sttime2, sttime3, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz614, LabelLine6.Text);
            ClassDay.OtkazOracle(ModelLabel6.Text, sttime3, sttime4, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz616, LabelLine6.Text);
            ClassDay.OtkazOracle(ModelLabel6.Text, sttime4, sttime5, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz618, LabelLine6.Text);
            ClassDay.OtkazOracle(ModelLabel6.Text, sttime5, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz620, LabelLine6.Text);
 
            ClassDay.OtkazOracle(ModelLabel6.Text, sttime0, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz6All, LabelLine6.Text);
        }
        void Otkaz7Oracle()
        {
            ClassDay.OtkazOracle(ModelLabel7.Text, sttime0, sttime1, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz710, LabelLine7.Text);
            ClassDay.OtkazOracle(ModelLabel7.Text, sttime1, sttime2, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz712, LabelLine7.Text);
            ClassDay.OtkazOracle(ModelLabel7.Text, sttime2, sttime3, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz714, LabelLine7.Text);
            ClassDay.OtkazOracle(ModelLabel7.Text, sttime3, sttime4, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz716, LabelLine7.Text);
            ClassDay.OtkazOracle(ModelLabel7.Text, sttime4, sttime5, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz718, LabelLine7.Text);
            ClassDay.OtkazOracle(ModelLabel7.Text, sttime5, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz720, LabelLine7.Text);

            ClassDay.OtkazOracle(ModelLabel7.Text, sttime0, sttime6, DateTime.Now.AddDays(день).ToString("dd.MM.yyyy"), Otkaz7All, LabelLine7.Text);
        }
      
        void Otkaz1Omron()
        {
            #region
            ClassDay.OtkazOmron(ModelLabel1.Text, sttime1, endtime1, Otkaz110);
            ClassDay.OtkazOmron(ModelLabel1.Text, sttime2, endtime2, Otkaz112);
            ClassDay.OtkazOmron(ModelLabel1.Text, sttime3, endtime3, Otkaz114);
            ClassDay.OtkazOmron(ModelLabel1.Text, sttime4, endtime4, Otkaz116);
            ClassDay.OtkazOmron(ModelLabel1.Text, sttime5, endtime5, Otkaz118);
            ClassDay.OtkazOmron(ModelLabel1.Text, sttime6, endtime6, Otkaz120);
        
            ClassDay.OtkazOmron(ModelLabel1.Text, sttime1, endtime6, Otkaz1All);
            #endregion
        }
        void Otkaz2Omron()
        {
            #region
            ClassDay.OtkazOmron(ModelLabel2.Text, sttime1, endtime1, Otkaz210);
            ClassDay.OtkazOmron(ModelLabel2.Text, sttime2, endtime2, Otkaz212);
            ClassDay.OtkazOmron(ModelLabel2.Text, sttime3, endtime3, Otkaz214);
            ClassDay.OtkazOmron(ModelLabel2.Text, sttime4, endtime4, Otkaz216);
            ClassDay.OtkazOmron(ModelLabel2.Text, sttime5, endtime5, Otkaz218);
            ClassDay.OtkazOmron(ModelLabel2.Text, sttime6, endtime6, Otkaz220);
            
            ClassDay.OtkazOmron(ModelLabel2.Text, sttime1, endtime6, Otkaz2All);
            #endregion
        }
        void Otkaz3Omron()
        {
            #region
            ClassDay.OtkazOmron(ModelLabel3.Text, sttime1, endtime1, Otkaz310);
            ClassDay.OtkazOmron(ModelLabel3.Text, sttime2, endtime2, Otkaz312);
            ClassDay.OtkazOmron(ModelLabel3.Text, sttime3, endtime3, Otkaz314);
            ClassDay.OtkazOmron(ModelLabel3.Text, sttime4, endtime4, Otkaz316);
            ClassDay.OtkazOmron(ModelLabel3.Text, sttime5, endtime5, Otkaz318);
            ClassDay.OtkazOmron(ModelLabel3.Text, sttime6, endtime6, Otkaz320);
          
            ClassDay.OtkazOmron(ModelLabel3.Text, sttime1, endtime6, Otkaz3All);
            #endregion
        }
        void Otkaz4Omron()
        {
            #region
            ClassDay.OtkazOmron(ModelLabel4.Text, sttime1, endtime1, Otkaz410);
            ClassDay.OtkazOmron(ModelLabel4.Text, sttime2, endtime2, Otkaz412);
            ClassDay.OtkazOmron(ModelLabel4.Text, sttime3, endtime3, Otkaz414);
            ClassDay.OtkazOmron(ModelLabel4.Text, sttime4, endtime4, Otkaz416);
            ClassDay.OtkazOmron(ModelLabel4.Text, sttime5, endtime5, Otkaz418);
            ClassDay.OtkazOmron(ModelLabel4.Text, sttime6, endtime6, Otkaz420);
         
            ClassDay.OtkazOmron(ModelLabel4.Text, sttime1, endtime6, Otkaz4All);
            #endregion
        }
        void Otkaz5Omron()
        {
            #region
            ClassDay.OtkazOmron(ModelLabel5.Text, sttime1, endtime1, Otkaz510);
            ClassDay.OtkazOmron(ModelLabel5.Text, sttime2, endtime2, Otkaz512);
            ClassDay.OtkazOmron(ModelLabel5.Text, sttime3, endtime3, Otkaz514);
            ClassDay.OtkazOmron(ModelLabel5.Text, sttime4, endtime4, Otkaz516);
            ClassDay.OtkazOmron(ModelLabel5.Text, sttime5, endtime5, Otkaz518);
            ClassDay.OtkazOmron(ModelLabel5.Text, sttime6, endtime6, Otkaz520);
          
            ClassDay.OtkazOmron(ModelLabel5.Text, sttime1, endtime6, Otkaz5All);
            #endregion
        }
        void Otkaz6Omron()
        {
            #region
            ClassDay.OtkazOmron(ModelLabel6.Text, sttime1, endtime1, Otkaz610);
            ClassDay.OtkazOmron(ModelLabel6.Text, sttime2, endtime2, Otkaz612);
            ClassDay.OtkazOmron(ModelLabel6.Text, sttime3, endtime3, Otkaz614);
            ClassDay.OtkazOmron(ModelLabel6.Text, sttime4, endtime4, Otkaz616);
            ClassDay.OtkazOmron(ModelLabel6.Text, sttime5, endtime5, Otkaz618);
            ClassDay.OtkazOmron(ModelLabel6.Text, sttime6, endtime6, Otkaz620);
         
            ClassDay.OtkazOmron(ModelLabel6.Text, sttime1, endtime6, Otkaz6All);
            #endregion
        }
        void Otkaz7Omron()
        {
            #region
            ClassDay.OtkazOmron(ModelLabel7.Text, sttime1, endtime1, Otkaz710);
            ClassDay.OtkazOmron(ModelLabel7.Text, sttime2, endtime2, Otkaz712);
            ClassDay.OtkazOmron(ModelLabel7.Text, sttime3, endtime3, Otkaz714);
            ClassDay.OtkazOmron(ModelLabel7.Text, sttime4, endtime4, Otkaz716);
            ClassDay.OtkazOmron(ModelLabel7.Text, sttime5, endtime5, Otkaz718);
            ClassDay.OtkazOmron(ModelLabel7.Text, sttime6, endtime6, Otkaz720);
           
            ClassDay.OtkazOmron(ModelLabel7.Text, sttime1, endtime6, Otkaz7All);
            #endregion
        }

        void Vyp1Omron()
        {
            #region Запрос на выпуск с базы OMRON
            ClassDay.vypyskOmron(ModelLabel1.Text, sttime1, endtime1, Vyp110);
            ClassDay.vypyskOmron(ModelLabel1.Text, sttime2, endtime2, Vyp112);
            ClassDay.vypyskOmron(ModelLabel1.Text, sttime3, endtime3, Vyp114);
            ClassDay.vypyskOmron(ModelLabel1.Text, sttime4, endtime4, Vyp116);
            ClassDay.vypyskOmron(ModelLabel1.Text, sttime5, endtime5, Vyp118);
            ClassDay.vypyskOmron(ModelLabel1.Text, sttime6, endtime6, Vyp120);
  
            ClassDay.vypyskOmron(ModelLabel1.Text, sttime1, endtime6, Vyp1All);
            #endregion
        }
        void Vyp2Omron()
        {
            #region Запрос на выпуск с базы OMRON
            ClassDay.vypyskOmron(ModelLabel2.Text, sttime1, endtime1, Vyp210);
            ClassDay.vypyskOmron(ModelLabel2.Text, sttime2, endtime2, Vyp212);
            ClassDay.vypyskOmron(ModelLabel2.Text, sttime3, endtime3, Vyp214);
            ClassDay.vypyskOmron(ModelLabel2.Text, sttime4, endtime4, Vyp216);
            ClassDay.vypyskOmron(ModelLabel2.Text, sttime5, endtime5, Vyp218);
            ClassDay.vypyskOmron(ModelLabel2.Text, sttime6, endtime6, Vyp220);
     
            ClassDay.vypyskOmron(ModelLabel2.Text, sttime1, endtime6, Vyp2All);
            #endregion
        }
        void Vyp3Omron()
        {
            #region Запрос на выпуск с базы OMRON
            ClassDay.vypyskOmron(ModelLabel3.Text, sttime1, endtime1, Vyp310);
            ClassDay.vypyskOmron(ModelLabel3.Text, sttime2, endtime2, Vyp312);
            ClassDay.vypyskOmron(ModelLabel3.Text, sttime3, endtime3, Vyp314);
            ClassDay.vypyskOmron(ModelLabel3.Text, sttime4, endtime4, Vyp316);
            ClassDay.vypyskOmron(ModelLabel3.Text, sttime5, endtime5, Vyp318);
            ClassDay.vypyskOmron(ModelLabel3.Text, sttime6, endtime6, Vyp320);
       
            ClassDay.vypyskOmron(ModelLabel3.Text, sttime1, endtime6, Vyp3All);
            #endregion
        }
        void Vyp4Omron()
        {
            #region Запрос на выпуск с базы OMRON
            ClassDay.vypyskOmron(ModelLabel4.Text, sttime1, endtime1, Vyp410);
            ClassDay.vypyskOmron(ModelLabel4.Text, sttime2, endtime2, Vyp412);
            ClassDay.vypyskOmron(ModelLabel4.Text, sttime3, endtime3, Vyp414);
            ClassDay.vypyskOmron(ModelLabel4.Text, sttime4, endtime4, Vyp416);
            ClassDay.vypyskOmron(ModelLabel4.Text, sttime5, endtime5, Vyp418);
            ClassDay.vypyskOmron(ModelLabel4.Text, sttime6, endtime6, Vyp420);
    
            ClassDay.vypyskOmron(ModelLabel4.Text, sttime1, endtime6, Vyp4All);
            #endregion
        }
        void Vyp5Omron()
        {
            #region Запрос на выпуск с базы OMRON
            ClassDay.vypyskOmron(ModelLabel5.Text, sttime1, endtime1, Vyp510);
            ClassDay.vypyskOmron(ModelLabel5.Text, sttime2, endtime2, Vyp512);
            ClassDay.vypyskOmron(ModelLabel5.Text, sttime3, endtime3, Vyp514);
            ClassDay.vypyskOmron(ModelLabel5.Text, sttime4, endtime4, Vyp516);
            ClassDay.vypyskOmron(ModelLabel5.Text, sttime5, endtime5, Vyp518);
            ClassDay.vypyskOmron(ModelLabel5.Text, sttime6, endtime6, Vyp520);

            ClassDay.vypyskOmron(ModelLabel5.Text, sttime1, endtime6, Vyp5All);
            #endregion
        }
        void Vyp6Omron()
        {
            #region Запрос на выпуск с базы OMRON
            ClassDay.vypyskOmron(ModelLabel6.Text, sttime1, endtime1, Vyp610);
            ClassDay.vypyskOmron(ModelLabel6.Text, sttime2, endtime2, Vyp612);
            ClassDay.vypyskOmron(ModelLabel6.Text, sttime3, endtime3, Vyp614);
            ClassDay.vypyskOmron(ModelLabel6.Text, sttime4, endtime4, Vyp616);
            ClassDay.vypyskOmron(ModelLabel6.Text, sttime5, endtime5, Vyp618);
            ClassDay.vypyskOmron(ModelLabel6.Text, sttime6, endtime6, Vyp620);

            ClassDay.vypyskOmron(ModelLabel6.Text, sttime1, endtime6, Vyp6All);
            #endregion
        }
        void Vyp7Omron()
        {
            #region Запрос на выпуск с базы OMRON
            ClassDay.vypyskOmron(ModelLabel7.Text, sttime1, endtime1, Vyp710);
            ClassDay.vypyskOmron(ModelLabel7.Text, sttime2, endtime2, Vyp712);
            ClassDay.vypyskOmron(ModelLabel7.Text, sttime3, endtime3, Vyp714);
            ClassDay.vypyskOmron(ModelLabel7.Text, sttime4, endtime4, Vyp716);
            ClassDay.vypyskOmron(ModelLabel7.Text, sttime5, endtime5, Vyp718);
            ClassDay.vypyskOmron(ModelLabel7.Text, sttime6, endtime6, Vyp720);

            ClassDay.vypyskOmron(ModelLabel7.Text, sttime1, endtime6, Vyp7All);
            #endregion
        }
        
    }
}
