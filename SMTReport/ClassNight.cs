using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuleConnect;
using System.Windows.Forms;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace SMTReport
{
    class ClassNight
    {
        static string SQL;


        static public void ТопОшибокSMT(DataGridView TOP, string startdate, string startdate2, string время_начало, string Время_конец)
        {
            SQL = @"SELECT DISTINCT(four.CIR_NAME), COUNT(four.CIR_NAME) FROM PRISM.INSP_RESULT_SUMMARY_INFO one  LEFT JOIN COMP_RESULT_INFO two ON one.INSP_ID = two.INSP_ID
              LEFT JOIN COMP_INFO three ON one.PG_ITEM_ID = three.PG_ITEM_ID AND two.COMP_ID = three.COMP_ID  LEFT JOIN CIR_INFO four ON three.CIR_ID = four.CIR_ID AND  three.PG_ITEM_ID = four.PG_ITEM_ID
              WHERE INSP_END_DATE BETWEEN TO_DATE(CONCAT('" + startdate + "','" + время_начало + "'),'DD.MM.YY HH24:MI:SS') AND TO_DATE(CONCAT('" + startdate2 + "','" + Время_конец + "'),'DD.MM.YY HH24:MI:SS') AND one.INSP_RESULT_CODE<>0 AND one.VC_LAST_RESULT_CODE<>0  AND two.INSP_RESULT_CODE<>0 AND two.VC_LAST_RESULT_CODE<>0    GROUP BY four.CIR_NAME    ORDER BY COUNT(four.CIR_NAME) DESC ";
            Class1.LoadGridOra(TOP, SQL);
        }

        static public void ТопОшибокSMTcount(Label count, string startdate, string startdate2, string время_начало, string Время_конец)
        {
            SQL = @"Select count(*) FROM PRISM.INSP_RESULT_SUMMARY_INFO one  LEFT JOIN COMP_RESULT_INFO two ON one.INSP_ID = two.INSP_ID
              LEFT JOIN COMP_INFO three ON one.PG_ITEM_ID = three.PG_ITEM_ID AND two.COMP_ID = three.COMP_ID  LEFT JOIN CIR_INFO four ON three.CIR_ID = four.CIR_ID AND  three.PG_ITEM_ID = four.PG_ITEM_ID
              WHERE INSP_END_DATE BETWEEN TO_DATE(CONCAT('" + startdate + "','" + время_начало + "'),'DD.MM.YY HH24:MI:SS') AND TO_DATE(CONCAT('" + startdate2 + "','" + Время_конец + "'),'DD.MM.YY HH24:MI:SS') AND one.INSP_RESULT_CODE<>0 AND one.VC_LAST_RESULT_CODE<>0  AND two.INSP_RESULT_CODE<>0 AND two.VC_LAST_RESULT_CODE<>0 ";
            count.Text = Class1.SelectStringOraInteger(SQL).ToString();
        }

        static public AlternateView getEmbeddedImage(string filePath, string content)

        {

            LinkedResource res = new LinkedResource(filePath);
            res.ContentId = content;
            string htmlBody = @"<img src='cid:" + res.ContentId + @"'/>";
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(res);
           
            



            return alternateView;

        }

        static public void SendEmail(string image, string card)
        {
          
                //mail.AlternateViews.Add(getEmbeddedImage("С:\путь\к\скриншоту.jpg"));

                MailAddress fromMailAdress = new MailAddress("controlerotk@dtvs.ru", "КонтроллерОТК");
            //MailAddress fromMailAdress = new MailAddress("a.volodin@dtvs.ru", "КонтроллерОТК");
            //MailAddress to = new MailAddress("a.volodin@dtvs.ru", "ME");
            MailAddress to = new MailAddress("a.volodin@dtvs.ru", "ME");

            AlternateView html_view = AlternateView.CreateAlternateViewFromString(fromMailAdress.Address, null, "text/html");

                using (MailMessage MailMessage = new MailMessage(fromMailAdress, to))
                using (SmtpClient SmtpClient = new SmtpClient("mail.technopolis.gs", 25))
            {
                MailMessage.CC.Add("Овчинников Дмитрий Игоревич < ovchinnikov@dtvs.ru >");
                MailMessage.CC.Add("Парфенов Евгений Александрович <parfenov@dtvs.ru>");
                MailMessage.CC.Add("Мастер SMT <mastersmt@dtvs.ru>");
                MailMessage.CC.Add("Канаев Виталий Владимирович <kanaev@dtvs.ru>");
                
                MailMessage.CC.Add("Гусаров Валерий Вячеславович <gusarov@dtvs.ru>");
                MailMessage.CC.Add("Каспирович Дмитрий Иванович <kaspirovich@dtvs.ru>");
                MailMessage.CC.Add("Фомичев Дмитрий Валерьевич <d.fomichev@dtvs.ru>");
         
                MailMessage.CC.Add("Костина Ксения Викторовна <kostina@dtvs.ru>");
                MailMessage.CC.Add("Ганжур Артем Юрьевич <a.ganzhur@dtvs.ru>");
                MailMessage.CC.Add("Слабицкая Татьяна Михайловна <slabitskaya@dtvs.ru>");
                MailMessage.CC.Add("Лишик Станислав Александрович <lishik@dtvs.ru>");
                MailMessage.CC.Add("Мелехин Константин Данилович <melekhin@dtvs.ru>");
                MailMessage.CC.Add("Контролер ОТК <controlerotk@dtvs.ru>");
                MailMessage.CC.Add("Лобанов Олег Юрьевич <lobanov@dtvs.ru>");
                MailMessage.CC.Add("Сузи Дмитрий Игоревич <d.suzi@dtvs.ru>");
                MailMessage.CC.Add("Фролов Дмитрий Андреевич <d.frolov@dtvs.ru>");
                MailMessage.CC.Add("volodin1971@rambler.ru");

                MailMessage.Subject = card;

                    MailMessage.AlternateViews.Add(getEmbeddedImage(image, "1"));

                    //SmtpClient.EnableSsl = true;
                    //SmtpClient.Host = "mail.technopolis.gs";
                    //SmtpClient.Port = 587;
                    //SmtpClient.EnableSsl = false;
                    SmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    SmtpClient.UseDefaultCredentials = false;
                SmtpClient.Credentials = new NetworkCredential("controlerotk@dtvs.ru", "qwertyuio");
                SmtpClient.Send(MailMessage);
                }
                          
        }

        public static int k = 0;
        static public void SpisokModels(DataGridView DGOmron, DataGridView DGOracle, DataGridView ALL, string startdate, string startdate1, string startdate2, string startdate4)
        {

            SQL = @"use [Orbo00_0000]   select distinct (ProductName),   substring(ProductName,PATINDEX('%top%', ProductName),3),    substring(ProductName,PATINDEX('%bot%', ProductName),3)
                    FROM [Orbo00_0000].[dbo].[TblPanel]
                where   TestDateTime  between  ('" + startdate + "') and ('" + startdate2 + "')  and convert(varchar(8), TestDateTime, 108 ) between ('00:00:00') and ('08:00:00')";
            Class1.loadgridOmron(DGOmron, SQL);

            SQL = @"SELECT DISTINCT(MOD.pg_name), substr(L.USR_MACHINE_NAME,1,13) ,SUBSTR(L.USR_MACHINE_NAME,19,1) as Line  FROM PRISM.INSP_RESULT_SUMMARY_INFO 
                    INNER JOIN SEG_RESULT_INFO sri ON INSP_RESULT_SUMMARY_INFO.INSP_ID = sri.INSP_ID AND INSP_RESULT_SUMMARY_INFO.SYS_MACHINE_NAME = sri.SYS_MACHINE_NAME
                    INNER JOIN PG_INFO mod ON INSP_RESULT_SUMMARY_INFO.PG_ITEM_ID = mod.PG_ITEM_ID INNER JOIN CONNECTION_MACHINE_INFO L ON INSP_RESULT_SUMMARY_INFO.SYS_MACHINE_NAME = l.SYS_MACHINE_NAME
                   WHERE INSP_END_DATE   BETWEEN TO_DATE(CONCAT('" + startdate1 + "','22:00:00'),'DD.MM.YY HH24:MI:SS') AND TO_DATE(CONCAT('" + startdate4 + "','08:00:00'),'DD.MM.YY HH24:MI:SS') order by Line";
            Class1.LoadGridOra(DGOracle, SQL);
            int L = 0;
            //ALL.Rows.Add(25);
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

        static public void vypyskOracle(string modelname, string STomrDate, string endomrDate, string startdate, string startdate1, Label LB, string line) //Отказы Omron
        {
            object k;
            SQL = @"SELECT COUNT(sri.INSP_ID) FROM PRISM.INSP_RESULT_SUMMARY_INFO   INNER JOIN SEG_RESULT_INFO sri ON INSP_RESULT_SUMMARY_INFO.INSP_ID = sri.INSP_ID AND INSP_RESULT_SUMMARY_INFO.SYS_MACHINE_NAME = sri.SYS_MACHINE_NAME 
              INNER JOIN PG_INFO mod ON INSP_RESULT_SUMMARY_INFO.PG_ITEM_ID = mod.PG_ITEM_ID WHERE INSP_END_DATE 
              BETWEEN TO_DATE(CONCAT('" + startdate + "','" + STomrDate + "'),'DD.MM.YY HH24:MI:SS') AND TO_DATE(CONCAT('" + startdate1 + "','" + endomrDate + "'),'DD.MM.YY HH24:MI:SS') AND PRISM.INSP_RESULT_SUMMARY_INFO.SYS_MACHINE_NAME= '" + line + "' AND MOD.PG_NAME = '" + modelname + "'";
            k = Class1.SelectStringOraInteger(SQL);
            LB.Text = k.ToString();
        }

        static public void OtkazOracle(string modelname, string STomrDate, string endomrDate, string startdate, string startdate1, Label LB, string line) //Отказы Omron
        {
            object k;
            SQL = @"SELECT COUNT(*)    FROM PRISM.INSP_RESULT_SUMMARY_INFO  
            INNER JOIN PG_INFO mod ON INSP_RESULT_SUMMARY_INFO.PG_ITEM_ID = mod.PG_ITEM_ID   
            WHERE INSP_END_DATE BETWEEN TO_DATE(CONCAT('" + startdate + "','" + STomrDate + "'),'DD.MM.YY HH24:MI:SS')" +
            " AND TO_DATE(CONCAT('" + startdate1 + "','" + endomrDate + "'),'DD.MM.YY HH24:MI:SS')  AND SYS_MACHINE_NAME= '" + line + "'     AND MOD.PG_NAME = '" + modelname + "' AND INSP_RESULT_CODE<>0 AND VC_LAST_RESULT_CODE<>0";
            k = Class1.SelectStringOraInteger(SQL);
            LB.Text = k.ToString();
        }
    }
}
