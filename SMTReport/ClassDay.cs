using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuleConnect;
using System.Windows.Forms;
using System.Drawing;

namespace SMTReport
{
    class ClassDay
    {
        static string SQL;


        static public void Топ_Omron(DataGridView TOP, string startdate, string endDate , string время_начало, string Время_конец, string модель)
        {
            SQL = @"Use Orbo00_0000 SELECT  TOP(3) OrboCdbRecipe.dbo.TblComponent.componentName, COUNT(*) as Qty FROM Orbo00_0000.dbo.TblComponent P
             LEFT JOIN OrboCdbRecipe.dbo.TblComponent  ON ProductID=OrboCdbRecipe.dbo.TblComponent.recipeId   Left join [dbo].[TblPanel] as pn on p.PanelID = pn.PanelID
              WHERE CompID=OrboCdbRecipe.dbo.TblComponent.componentTan and P.Classification<>0 and p.TestDateTime  between ('" + startdate + " " + время_начало + "') and ('" + endDate + " " + Время_конец + "') "+
            " and ProductName = '"+ модель + "'     GROUP BY OrboCdbRecipe.dbo.TblComponent.componentName ORDER BY Qty Desc";
            Class1.loadgridOmron(TOP, SQL);
        }


        static public void ТопОшибокSMT(DataGridView TOP,string startdate, string время_начало, string Время_конец, string модель, string линия)
        {
            SQL = @"SELECT DISTINCT(four.CIR_NAME),   COUNT(four.CIR_NAME) FROM PRISM.INSP_RESULT_SUMMARY_INFO one  LEFT JOIN COMP_RESULT_INFO two ON one.INSP_ID = two.INSP_ID
              LEFT JOIN COMP_INFO three ON one.PG_ITEM_ID = three.PG_ITEM_ID AND two.COMP_ID = three.COMP_ID  
                LEFT JOIN USR_INSP_RESULT_NAME six ON one.INSP_RESULT_CODE = six.USR_INSP_RESULT_CODE 
                LEFT JOIN CIR_INFO four ON three.CIR_ID = four.CIR_ID AND  three.PG_ITEM_ID = four.PG_ITEM_ID
                Left JOIN PG_INFO mod ON one.PG_ITEM_ID = mod.PG_ITEM_ID
              WHERE INSP_END_DATE BETWEEN TO_DATE(CONCAT('" + startdate + "','" + время_начало + "'),'DD.MM.YY HH24:MI:SS') AND TO_DATE(CONCAT('" + startdate + "','" + Время_конец + "'),'DD.MM.YY HH24:MI:SS')" +
              " AND one.INSP_RESULT_CODE<>0 AND one.VC_LAST_RESULT_CODE<>0 " +
              " AND two.INSP_RESULT_CODE<>0 AND two.VC_LAST_RESULT_CODE<>0 AND six.LANG_ID = 3 AND MOD.PG_NAME = '" + модель + "' AND one.SYS_MACHINE_NAME ='" + линия + "'  " +
              "  GROUP BY four.CIR_NAME  " +
              "   ORDER BY COUNT(four.CIR_NAME) DESC ";
            Class1.LoadGridOra(TOP, SQL);
        }

        static public void ТопОшибокSMTOmron(DataGridView TOP, string startdate, string время_начало, string Время_конец, string модель)
        {
            SQL = @" USE Orbo00_0000 SELECT  Distinct(cp.ReferenceDesignator), count(cp.ReferenceDesignator) as количество  FROM [Orbo00_0000].[dbo].[TblPanel] as pn
              left join [dbo].[TblComponent] as cp on pn.PanelID = cp.PanelID  where pn.TestDateTime between ('" + startdate + " "+ время_начало + "') and ('" + startdate + " " + Время_конец + "') " +
              "and FailedBoards = 1 and ClassifiedDefects != 0 and ProductName = '"+ модель + "' group by cp.ReferenceDesignator  order by count(cp.ReferenceDesignator) desc ";
            Class1.loadgridOmron(TOP, SQL);
        }


        static public void ТопОшибокSMTНОЧЬ(DataGridView TOP, string startdate, string startdatet ,string время_начало, string Время_конец, string модель, string линия)
        {
            SQL = @"SELECT DISTINCT(four.CIR_NAME),   COUNT(four.CIR_NAME) FROM PRISM.INSP_RESULT_SUMMARY_INFO one  LEFT JOIN COMP_RESULT_INFO two ON one.INSP_ID = two.INSP_ID
              LEFT JOIN COMP_INFO three ON one.PG_ITEM_ID = three.PG_ITEM_ID AND two.COMP_ID = three.COMP_ID  
                LEFT JOIN USR_INSP_RESULT_NAME six ON one.INSP_RESULT_CODE = six.USR_INSP_RESULT_CODE 
                LEFT JOIN CIR_INFO four ON three.CIR_ID = four.CIR_ID AND  three.PG_ITEM_ID = four.PG_ITEM_ID
                Left JOIN PG_INFO mod ON one.PG_ITEM_ID = mod.PG_ITEM_ID
              WHERE INSP_END_DATE BETWEEN TO_DATE(CONCAT('" + startdate + "','" + время_начало + "'),'DD.MM.YY HH24:MI:SS') AND TO_DATE(CONCAT('" + startdatet + "','" + Время_конец + "'),'DD.MM.YY HH24:MI:SS')" +
              " AND one.INSP_RESULT_CODE<>0 AND one.VC_LAST_RESULT_CODE<>0 " +
              " AND two.INSP_RESULT_CODE<>0 AND two.VC_LAST_RESULT_CODE<>0 AND six.LANG_ID = 3 AND MOD.PG_NAME = '" + модель + "' AND one.SYS_MACHINE_NAME ='" + линия + "'  " +
              "  GROUP BY four.CIR_NAME  " +
              "   ORDER BY COUNT(four.CIR_NAME) DESC ";
            Class1.LoadGridOra(TOP, SQL);
        }

        static public void ТопОшибокSMTcount(Label count, string startdate,  string время_начало, string Время_конец)
        {
            SQL = @"Select count(*) FROM PRISM.INSP_RESULT_SUMMARY_INFO one  LEFT JOIN COMP_RESULT_INFO two ON one.INSP_ID = two.INSP_ID
              LEFT JOIN COMP_INFO three ON one.PG_ITEM_ID = three.PG_ITEM_ID AND two.COMP_ID = three.COMP_ID  LEFT JOIN CIR_INFO four ON three.CIR_ID = four.CIR_ID AND  three.PG_ITEM_ID = four.PG_ITEM_ID
              WHERE INSP_END_DATE BETWEEN TO_DATE(CONCAT('" + startdate + "','" + время_начало + "'),'DD.MM.YY HH24:MI:SS') AND TO_DATE(CONCAT('" + startdate + "','" + Время_конец + "'),'DD.MM.YY HH24:MI:SS') AND one.INSP_RESULT_CODE<>0 AND one.VC_LAST_RESULT_CODE<>0  AND two.INSP_RESULT_CODE<>0 AND two.VC_LAST_RESULT_CODE<>0 ";
            count.Text = Class1.SelectStringOraInteger(SQL).ToString();
        }

        static public void Screens(int width, int height, string screen)
        {
            Bitmap BM = new Bitmap(width, height);

            Size size = new Size(width, height);
            Point point = new Point(10, 5);
            Point point2 = new Point(10, 5);
            Graphics GH = Graphics.FromImage(BM as Image);

            //GH.CopyFromScreen(0, 0, 0, 0, FORM.Size);
            GH.CopyFromScreen(point, point2, BM.Size);
            //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            //pictureBox1.Image = BM;
            BM.Save(@"C:\Скриншот\" + screen + "");
        }
        public static int k = 0;
        static public void SpisokModels(DataGridView DGOmron, DataGridView DGOracle , DataGridView ALL, string startdate, string STomrDate, string endomrDate)
        {
            
            SQL = @"use [Orbo00_0000]   select distinct (ProductName),   substring(ProductName,PATINDEX('%top%', ProductName),3),    substring(ProductName,PATINDEX('%bot%', ProductName),3)
                    FROM [Orbo00_0000].[dbo].[TblPanel]
                     where TestDateTime between  ('" + STomrDate + "') and ('" + endomrDate + "') ";
            Class1.loadgridOmron(DGOmron, SQL);

            SQL = @"SELECT DISTINCT(MOD.pg_name), substr(L.USR_MACHINE_NAME,1,13) ,SUBSTR(L.USR_MACHINE_NAME,19,1) as Line  FROM PRISM.INSP_RESULT_SUMMARY_INFO 
                    INNER JOIN SEG_RESULT_INFO sri ON INSP_RESULT_SUMMARY_INFO.INSP_ID = sri.INSP_ID AND INSP_RESULT_SUMMARY_INFO.SYS_MACHINE_NAME = sri.SYS_MACHINE_NAME
                    INNER JOIN PG_INFO mod ON INSP_RESULT_SUMMARY_INFO.PG_ITEM_ID = mod.PG_ITEM_ID INNER JOIN CONNECTION_MACHINE_INFO L ON INSP_RESULT_SUMMARY_INFO.SYS_MACHINE_NAME = l.SYS_MACHINE_NAME
                   WHERE INSP_END_DATE   BETWEEN TO_DATE(CONCAT('" + startdate + "',' 06:00:00'),'DD.MM.YY HH24:MI:SS') AND TO_DATE(CONCAT('" + startdate + "',' 21:59:00'),'DD.MM.YY HH24:MI:SS') order by Line";
            Class1.LoadGridOra(DGOracle, SQL);

            //ALL.Rows.Add(25);
            int L = 0;
            for (int i = 0; i < DGOmron.Rows.Count - 1; i++)
            {
                ALL.Rows[i].Cells[0].Value = DGOmron.Rows[i].Cells[0].Value;
                ALL.Rows[i].Cells[1].Value = "3";
                ALL.Rows[i].Cells[2].Value = "3";
                ALL.Rows[i].Cells[3].Value = "Omron";
                L++;
            }

            for (int i = 0; i < DGOracle.Rows.Count - 1; i++)
            {
                ALL.Rows[L].Cells[0].Value = DGOracle.Rows[i].Cells[0].Value;
                ALL.Rows[L].Cells[1].Value = DGOracle.Rows[i].Cells[2].Value;
                ALL.Rows[L].Cells[2].Value = DGOracle.Rows[i].Cells[1].Value;
                ALL.Rows[L].Cells[3].Value = "Oracle";
                L++;
            }

                      

        }
        static public void SPModelLine(Label line, Label model,DataGridView dg, int i)
        {
            try
            {
                model.Text = dg.Rows[i].Cells[0].Value.ToString();
                line.Text = dg.Rows[i].Cells[1].Value.ToString();
            }
            catch (Exception)
            {

                model.Text = "-";
                line.Text = "-";
            }
           
         }

        static public void SPModelLines(DataGridView DG, Label Line, Label status, int i)
        {
            if (DG.Rows[i].Cells[2].Value == null)  { Line.Text = "-"; status.Text = "-"; }
            else   { Line.Text  = DG.Rows[i].Cells[2].Value.ToString();  status.Text = DG.Rows[i].Cells[3].Value.ToString(); }

            
        }


        static public void vypOmronALL(string ndate, string mdate, string model,Label LB)
        {
            object k;
            SQL = @" Select count(ReferenceBarCode) From[Orbo00_0000].[dbo].[TblPanel]  where TestDateTime between('"+ ndate + "') and ('" + mdate + "') And ProductName = '" + model + "'";
            k = Class1.SelectStringIntOmron(SQL);
            LB.Text = k.ToString();
        }

        static public void vypOtkazALL(string ndate, string mdate, string model, Label LB)
        {
            object k;
            SQL = @"SELECT count (ReferenceBarCode)  FROM [Orbo00_0000].[dbo].[TblPanel]   where TestDateTime between  ('" + ndate + "') and ('" + mdate + "') and ProductName = '" + model + "'  and FailedBoards = 1  and ClassifiedDefects != 0";
            k = Class1.SelectStringIntOmron(SQL);
            LB.Text = k.ToString();
        }


        static public void vypyskOmron(string modelname, string STomrDate, string endomrDate, Label LB)// Выпуск omron
        {
            object k; 
            SQL = @"Select  count(ReferenceBarCode) As Выпуск
                    From [Orbo00_0000].[dbo].[TblPanel]
                    where TestDateTime between  cast(CONVERT(varchar(10), getdate(), 120) as smalldatetime)  And CURRENT_TIMESTAMP
                    and Convert(varchar(8), TestDateTime, 108) between ('" + STomrDate + "') and ('" + endomrDate + "')   And ProductName = '" + modelname + "'";
            k = Class1.SelectStringIntOmron(SQL);
            LB.Text = k.ToString();
        }





        static public void OtkazOmron(string modelname, string STomrDate, string endomrDate, Label LB) //Отказы Omron
        {
            object k;
            SQL = @"SELECT count (ReferenceBarCode) as Выпуск FROM [Orbo00_0000].[dbo].[TblPanel]  where TestDateTime between  cast(CONVERT(varchar(10), getdate(), 120) as smalldatetime)  And CURRENT_TIMESTAMP
                  and convert(varchar(8), TestDateTime, 108 ) between ('" + STomrDate + "') and ('" + endomrDate + "')  and ProductName = '" + modelname + "'  and FailedBoards = 1  and ClassifiedDefects != 0";
            k = Class1.SelectStringIntOmron(SQL);
            LB.Text = k.ToString();
        }

        static public void vypyskOracle(string modelname, string STomrDate, string endomrDate, string startdate, Label LB, string line) //Отказы Omron
        {
            object k;
            SQL = @"SELECT COUNT(sri.INSP_ID) FROM PRISM.INSP_RESULT_SUMMARY_INFO   INNER JOIN SEG_RESULT_INFO sri ON INSP_RESULT_SUMMARY_INFO.INSP_ID = sri.INSP_ID AND INSP_RESULT_SUMMARY_INFO.SYS_MACHINE_NAME = sri.SYS_MACHINE_NAME 
              INNER JOIN PG_INFO mod ON INSP_RESULT_SUMMARY_INFO.PG_ITEM_ID = mod.PG_ITEM_ID WHERE INSP_END_DATE 
              BETWEEN TO_DATE(CONCAT('" + startdate + "','" + STomrDate + "'),'DD.MM.YY HH24:MI:SS') AND TO_DATE(CONCAT('" + startdate + "','" + endomrDate + "'),'DD.MM.YY HH24:MI:SS') AND PRISM.INSP_RESULT_SUMMARY_INFO.SYS_MACHINE_NAME= '" + line + "' AND MOD.PG_NAME = '" + modelname + "'";
            k = Class1.SelectStringOraInteger(SQL);
            LB.Text = k.ToString();
        }

        static public void OtkazOracle(string modelname, string STomrDate, string endomrDate, string startdate, Label LB, string line) //Отказы Omron
        {
            object k;
            SQL = @"SELECT COUNT(*)    FROM PRISM.INSP_RESULT_SUMMARY_INFO  
            INNER JOIN PG_INFO mod ON INSP_RESULT_SUMMARY_INFO.PG_ITEM_ID = mod.PG_ITEM_ID   
            WHERE INSP_END_DATE BETWEEN TO_DATE(CONCAT('" + startdate + "','" + STomrDate + "'),'DD.MM.YY HH24:MI:SS')" +
            " AND TO_DATE(CONCAT('" + startdate + "','" + endomrDate + "'),'DD.MM.YY HH24:MI:SS')  AND SYS_MACHINE_NAME= '" + line + "'     AND MOD.PG_NAME = '" + modelname + "' AND INSP_RESULT_CODE<>0 AND VC_LAST_RESULT_CODE<>0";
            k = Class1.SelectStringOraInteger(SQL);
            LB.Text = k.ToString();
        }



        public static void FPY(Label vyp, Label otk, Label fpys)
        {
            try
            {
                if (vyp.Text == "0") { fpys.Text = "0"; }

                else
                { 
                    Single k;
                //k = Convert.ToDouble(100 - (Convert.ToInt32(otk.Text) / (Convert.ToInt32(vyp.Text) + Convert.ToInt32(otk.Text))) * 100);
                k = 100 - (Convert.ToSingle(otk.Text) / (Convert.ToSingle(vyp.Text) + Convert.ToSingle(otk.Text)) * 100);
                    fpys.Text = k.ToString("#.#");
                }
            }
            catch (Exception)
            {

                fpys.Text = "0";
            }

        }

        public static void FPYColor(Label fpys, double cell, string check)
        {
            if (check == "-")
            {
                fpys.BackColor = Color.Transparent;
            }
            else
            {
                if (fpys.Text == "0")
                {

                }
                else if (Convert.ToSingle(fpys.Text) <= cell)
                {
                    fpys.BackColor = Color.Tomato;
                }
                else if (Convert.ToSingle(fpys.Text) > cell) { fpys.BackColor = Color.LightGreen;  }
            }

        }







    }
}
