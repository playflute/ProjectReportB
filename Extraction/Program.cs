using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using Common;
using System.Threading;
using System.IO;
using System.Xml;
namespace Extraction
{

    class Program
    {
        private double totalEventNumber = 0;
        private DataSet[] ds = null;
        private DateTime today = DateTime.Today;//get today's datetime
        private Dictionary<string, Int32> Activity_Dic = new Dictionary<string, int>();
        //*******************
        public void StartTimer(int dueTime)
        {
            Timer t = new Timer(new TimerCallback(TimerProc));
            t.Change(0, dueTime);//the first parameter is the delay,second is the timer interval
           
        }
        private void TimerProc(object state)
        {
            // The state object is the Timer object.

            //Timer t = (Timer)state;
            //t.Dispose();
            this.Clean_DB_Table();//clean the stale data
            Activity_Dic.Clear();//clear the classification dictionary
            this.ds=this.getDataSet();//read the excel and get new data set
            this.GenerateXML();
            this.classify();//classify the activity update the dictionary and write into the database again
            this.GenerateXML_ActivityNumber();//show activity number by category
            this.Show_TotalActivityNumber();//show total activity number
            Console.WriteLine("Last week event number is :" + this.Last_Week_Event_Number());
            Console.WriteLine("Last two week event number is :" + this.Last_TwoWeek_Event_Number());
            Console.WriteLine("Last month event number is: " + this.Last_Month_Event_Number());
            Console.WriteLine("Update once at "+DateTime.Now.ToString());
            Console.WriteLine("*************************The timer callback executes.*******************");
        }
        //*************************
        static void Main(string[] args)
        {
            Program p = new Program();
            p.StartTimer(1800000);//update the database every half an hour,that is 1800s=1800,000ms
            
            Console.ReadLine();
        }
        /// <summary>
        /// classify the activity update the dictionary and write into the database again
        /// </summary>
        public void classify()
        {
            
            int counter = 0;
            foreach (DataRow row in ds[0].Tables[0].Rows)
            {
                counter++;


                Console.Write(counter + " ");
                for (int i = 0; i < 3; i++)
                {
                    Console.Write(row[i].ToString() + "  ");
                }
                Console.WriteLine();
                Duration du = new Duration()
                {
                    TimeStamp = Convert.ToDateTime(row[0].ToString()),
                    Activity_id = row[1].ToString(),
                    Activity_duration = row[2].ToString(),

                };
                if (Activity_Dic.ContainsKey(du.Activity_id))
                {
                    Activity_Dic[du.Activity_id]++;
                }
                else
                {
                    Activity_Dic.Add(du.Activity_id, 1);

                }


                using (Week8Container w8c = new Week8Container())
                {
                    w8c.DurationSet.AddObject(du);

                    w8c.SaveChanges();

                }
                




            }
            Console.Write("☆☆☆☆☆☆☆First Table:DurationSet Import into database done☆☆☆☆☆☆☆");

            foreach (DataRow row in ds[1].Tables[0].Rows)
            {
                Embedded em = new Embedded()
                {
                    TimeStamp = Convert.ToDateTime(row[0].ToString()),
                    Item1 = row[1].ToString(),
                    Item2 = row[2].ToString()

                };

                using (Week8Container w8c = new Week8Container())
                {
                   
                    w8c.EmbeddedSet.AddObject(em);
                    w8c.SaveChanges();

                }
 
            }
            Console.Write("☆☆☆☆☆☆☆Second Table:EmbeddedSet Import into database done☆☆☆☆☆☆☆");
            foreach (DataRow row in ds[2].Tables[0].Rows)////////////////
            {
                Triplets tr = new Triplets()
                {
                    TimeStamp = Convert.ToDateTime(row[0].ToString()),
                    Subject = row[1].ToString(),
                    Verb = row[2].ToString(),
                    Object=row[3].ToString()

                };
                using (Week8Container w8c = new Week8Container())
                {
                    w8c.TripletsSet.AddObject(tr);
                    w8c.SaveChanges();

                }

            }
            Console.Write("☆☆☆☆☆☆☆Third Table:TripletsSet Import into database done☆☆☆☆☆☆☆");
            using (Week8Container w8c = new Week8Container())
            {
                totalEventNumber = w8c.DurationSet.Count();
            }
 
        }
        /// <summary>
        /// show activity number by category
        /// </summary>
        public void GenerateXML_ActivityNumber()
        {
            //先判断xml文件是否存在，如果存在则删除
            //
            if (File.Exists(@"C:\Users\Zhigang Zhang\Documents\visual studio 2010\Projects\ExtractDataFromExcel\Extraction\XML\duration_numberOfActivity.xml"))
            {
                File.Delete(@"C:\Users\Zhigang Zhang\Documents\visual studio 2010\Projects\ExtractDataFromExcel\Extraction\XML\duration_numberOfActivity.xml");
            }
            
            XmlDocument doc = new XmlDocument();
            XmlElement root=doc.CreateElement("Activities");
            doc.AppendChild(root);
            foreach (var item in Activity_Dic)
            {
                XmlElement group = doc.CreateElement("Group");
                XmlElement name = doc.CreateElement("activity_name");
                name.InnerText = item.Key;
                XmlElement number = doc.CreateElement("activity_number");
                number.InnerText = item.Value.ToString();
                group.AppendChild(name);
                group.AppendChild(number);
                root.AppendChild(group);

                Console.WriteLine("Activity name:{0} and it appeared {1} times,and probability is {2}%", item.Key, item.Value,(item.Value/totalEventNumber)*100);
            }
            //persist into XML data fomat,in order to let to web application to analyze
            
             doc.Save(@"C:\Users\Zhigang Zhang\Documents\visual studio 2010\Projects\ExtractDataFromExcel\Extraction\XML\duration_numberOfActivity.xml"); 
 
        }
        /// <summary>
        /// show total activity number
        /// </summary>
        public void Show_TotalActivityNumber()
        {

            using (Week8Container container = new Week8Container())
            {
                Console.WriteLine("Total event Number is" + container.DurationSet.Count());
                
                
            }
          
 
        }
        public int Last_Week_Event_Number()
        {
            int Last_Week_Event_Number = 0;
            ds = this.getDataSet();
            DateTime FactorDate=new DateTime();
            foreach (DataRow row in ds[0].Tables[0].Rows)
            {
                FactorDate=Convert.ToDateTime(row[0].ToString());

                if((DateTime.Compare(FactorDate,ImprotantDateTime.GetLastOneWeekMondayStartTime())>0)&&(DateTime.Compare(FactorDate,ImprotantDateTime.GetLastOneWeekSundayEndTime())<0))
                {
                    Last_Week_Event_Number++;
                }

            }
            return Last_Week_Event_Number;

        }
        public int Last_TwoWeek_Event_Number()
        {
            int Last_Two_Week_Event_Number = 0;
            ds = this.getDataSet();
            DateTime FactorDate = new DateTime();
            foreach (DataRow row in ds[0].Tables[0].Rows)
            {
                FactorDate = Convert.ToDateTime(row[0].ToString());

                if ((DateTime.Compare(FactorDate, ImprotantDateTime.GetLastTwoWeekMondayStartTime()) > 0) && (DateTime.Compare(FactorDate, ImprotantDateTime.GetLastTwoWeekSundayEndTime()) < 0))
                {
                    Last_Two_Week_Event_Number++;
                }

            }
            return Last_Two_Week_Event_Number;

        }

        public int Last_Month_Event_Number()
        {
            int Last_Month_Event_Number = 0;
            ds = this.getDataSet();
            DateTime FactorDate = new DateTime();
            foreach (DataRow row in ds[0].Tables[0].Rows)
            {
                FactorDate = Convert.ToDateTime(row[0].ToString());

                if ((DateTime.Compare(FactorDate, ImprotantDateTime.GetLastMonthStartTime()) > 0) && (DateTime.Compare(FactorDate, ImprotantDateTime.GetLastMonthEndTime()) < 0))
                {
                    Last_Month_Event_Number++;
                }

            }
            return Last_Month_Event_Number;


        }
        /// <summary>
        /// read the excel and get new data set
        /// </summary>
        /// <returns></returns>
        private DataSet[] getDataSet()
        {

            string relative_Path_Duration = @"..\..\duration.xlsx";
            string relative_Path_Embedded = @"..\..\embedded.xlsx";
            string relative_Path_Triplets = @"..\..\triplets.xlsx";
            //string absolute_FilePath = @"c:\users\zhigang zhang\documents\visual studio 2010\Projects\ExtractDataFromExcel\Extraction\duration.xlsx";
           

            String strConnDuration = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data source=" + relative_Path_Duration + ";" + "Extended Properties=Excel 8.0";
            String strConnEmbedded = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data source=" + relative_Path_Embedded + ";" + "Extended Properties=Excel 8.0";
            String strConnTrplets = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data source=" + relative_Path_Triplets + ";" + "Extended Properties=Excel 8.0";
           
            OleDbConnection connDuration = new OleDbConnection(strConnDuration);
            OleDbConnection connEmbedded = new OleDbConnection(strConnEmbedded);
            OleDbConnection connTrplets = new OleDbConnection(strConnTrplets);

            OleDbDataAdapter adapterDuration = new OleDbDataAdapter("SELECT * FROM [Form Responses$]", strConnDuration);
            OleDbDataAdapter adapterEmbedded = new OleDbDataAdapter("SELECT * FROM [Form Responses$]", strConnEmbedded);
            OleDbDataAdapter adapterTrplets = new OleDbDataAdapter("SELECT * FROM [Form Responses$]", strConnTrplets);

            DataSet[] myDataSet = new DataSet[3];
            for (int i = 0; i < myDataSet.Length; i++)
            {
                myDataSet[i] = new DataSet();
            }
                try
                {

                    adapterDuration.Fill(myDataSet[0]);
                    adapterEmbedded.Fill(myDataSet[1]);
                    adapterTrplets.Fill(myDataSet[2]);

                }

                catch (Exception ex)
                {

                    throw new Exception("The sheet name of the Excel file is incorrect," + ex.Message);

                }

            return myDataSet;
 
        }

        private void Clean_DB_Table()
        {
            //清空DurationSet表
            using (Week8Container container = new Week8Container())
            {
                var result=from needTODelete in container.DurationSet select needTODelete;
                if (result.Count() > 0)
                {
                    foreach (var item in result)
                    {
                        container.DurationSet.DeleteObject(item);
                    }
                }
                container.SaveChanges();
               
            }
            Console.WriteLine("Database Table DurationSet is cleaned");
            //清空EmbeddedSet表
            using (Week8Container container = new Week8Container())
            {
                var result = from needTODelete in container.EmbeddedSet select needTODelete;
                if (result.Count() > 0)
                {
                    foreach (var item in result)
                    {
                        container.EmbeddedSet.DeleteObject(item);
                    }
                }
                container.SaveChanges();

            }
            Console.WriteLine("Database Table EmbeddedSet is cleaned");
            //清空TripletsSet表
            using (Week8Container container = new Week8Container())
            {
                var result = from needTODelete in container.TripletsSet select needTODelete;
                if (result.Count() > 0)
                {
                    foreach (var item in result)
                    {
                        container.TripletsSet.DeleteObject(item);
                    }
                }
                container.SaveChanges();

            }
            Console.WriteLine("Database Table TripletsSet is cleaned");
            

        }
        private void GenerateXML()
        {
            
            string xmlDoc = this.ds[0].GetXml();
            StreamWriter sw = new StreamWriter(@"C:\Users\Zhigang Zhang\Documents\visual studio 2010\Projects\ExtractDataFromExcel\Extraction\XML\duration.xml",false);
            sw.Write(xmlDoc);
            sw.Flush();
            sw.Close();
        }
        //public void GenerateXML_DurationDayByDay
        //{
        //                //先判断xml文件是否存在，如果存在则删除
        //    //

        //                if (File.Exist(@"C:\Users\Zhigang Zhang\Documents\visual studio 2010\Projects\ExtractDataFromExcel\Extraction\XML\duration_ActivityNumberDaybyDay.xml"))
        //{
        //        File.Delete(@"C:\Users\Zhigang Zhang\Documents\visual studio 2010\Projects\ExtractDataFromExcel\Extraction\XML\duration_ActivityNumberDaybyDay.xml");
        //}
        //    XmlDocument doc = new XmlDocument();
        //    XmlElement root=doc.CreateElement("ActivityNumberPair");
        //    doc.AppendChild(root);
        //    //我需要有一个字典，键是日期，值是计数
        //    //遍历duration XML文件
        //    //如果是新日期，则加入字典,计数加一
        //    //如果是已经存在的日期，计数加一


        //    foreach (var item in Activity_Dic)
        //    {

        //    }
        //    //persist into XML data fomat,in order to let to web application to analyze
            
        //     doc.Save(@"C:\Users\Zhigang Zhang\Documents\visual studio 2010\Projects\ExtractDataFromExcel\Extraction\XML\duration_numberOfActivity.xml"); 
 
        //}
        
    }
}
