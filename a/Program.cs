using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace a
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("Activities");
            doc.AppendChild(root);
            XmlElement preview = doc.CreateElement("activity_name");
            preview.InnerText = "nihao";
            root.AppendChild(preview);
            doc.Save(@"C:\Users\Zhigang Zhang\Documents\visual studio 2010\Projects\ExtractDataFromExcel\a\XML\duration_numberOfActivity.xml"); 
        }
    }
}
