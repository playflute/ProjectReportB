<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Visualization_Duration.aspx.cs" Inherits="MyThesis.Visualization_Duration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="System.Xml" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <style type="text/css">
    div
    {
        height:500px;
        width:500px;
        }
    </style>
    <script src="Javascript/google_js_api.js" type="text/javascript"></script>
    <script type="text/javascript">
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            var data = google.visualization.arrayToDataTable([
          ['Activity', 'Number of Times'],

          <%
            XmlNodeList nodes = xDoc.SelectNodes("Activities/Group");
            int TotalNodesNumber = nodes.Count;
            for (int i = 0; i < TotalNodesNumber;i++ )
            {
                XmlNode xNode = nodes[i];
                    
                Response.Write("['");
                Response.Write(xNode.SelectSingleNode("activity_name").InnerText);
                Response.Write("',");
                
                Response.Write(xNode.SelectSingleNode("activity_number").InnerText);
                if (i == TotalNodesNumber - 1)
                {
                    Response.Write("]");
                }
                else
                {
                    Response.Write("],");
                }
                
            }
           %>
        ]);

            var options = {
                title: 'statistics of the activity number',
                vAxis: { title: 'Activity', titleTextStyle: { color: 'red'} },
                backgroundColor:'yellow',
                fontSize:5
            };

            var chart = new google.visualization.BarChart(document.getElementById('chart_div'));

            chart.draw(data, options);
        }
    </script>
</head>
<body>
<div id='chart_div' style='width: 1000px; height: 900px;'></div>
</body>
</html>
