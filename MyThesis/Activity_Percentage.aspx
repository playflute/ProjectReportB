<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Activity_Percentage.aspx.cs" Inherits="MyThesis.Activity_Percentage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="System.Xml" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Relative Percentage</title>
        <style type="text/css">
        #dashboard_div
        {
            height:500px;
            width:500px;
            }
    </style>
    <script src="Javascript/google_js_api.js" type="text/javascript"></script>
        <script type="text/javascript">

            // Load the Visualization API and the controls package.
            google.load('visualization', '1.0', { 'packages': ['controls'] });

            // Set a callback to run when the Google Visualization API is loaded.
            google.setOnLoadCallback(drawDashboard);

            // Callback that creates and populates a data table,
            // instantiates a dashboard, a range slider and a pie chart,
            // passes in the data and draws it.
            function drawDashboard() {

                // Create our data table.
                var data = google.visualization.arrayToDataTable([
          ['ActivityName', 'Total Number'],
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
        


                // Create a dashboard.
                var dashboard = new google.visualization.Dashboard(
            document.getElementById('dashboard_div'));

                // Create a range slider, passing some options
                var donutRangeSlider = new google.visualization.ControlWrapper({
                    'controlType': 'NumberRangeFilter',
                    'containerId': 'filter_div',
                    'options': {
                        'filterColumnLabel': 'Total Number',

                    }
                });

                // Create a pie chart, passing some options
                var pieChart = new google.visualization.ChartWrapper({
                    'chartType': 'PieChart',
                    'containerId': 'chart_div',
                    'options': {
                        'width': 600,
                        'height': 600,
                        'pieSliceText': 'value',
                        'legend': 'right',
                        fontSize: 6
                    }
                });

                // Establish dependencies, declaring that 'filter' drives 'pieChart',
                // so that the pie chart will only display entries that are let through
                // given the chosen slider range.
                dashboard.bind(donutRangeSlider, pieChart);

                // Draw the dashboard.
                dashboard.draw(data);
            }
    </script>
</head>
<body>
    <!--Div that will hold the dashboard-->
    <div id="dashboard_div">
      <!--Divs that will hold each control and chart-->
      <div id="filter_div"></div>
      <div id="chart_div"></div>
    </div>
</body>
</html>
