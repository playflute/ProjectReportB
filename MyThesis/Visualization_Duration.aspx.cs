using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;


namespace MyThesis
{
    public partial class Visualization_Duration : System.Web.UI.Page
    {
        protected XmlDocument xDoc;
        protected void Page_Load(object sender, EventArgs e)
        {
            //read the XML file,keep it in a protected var
           xDoc = new XmlDocument();
            xDoc.Load(@"C:\Users\Zhigang Zhang\Documents\visual studio 2010\Projects\ExtractDataFromExcel\MyThesis\XML\duration_numberOfActivity.xml");

           


        }
    }
}