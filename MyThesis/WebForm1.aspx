<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="MyThesis.WebForm1" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script src="Javascript/jquery-2.1.3.min.js" type="text/javascript"></script>
    <script src="Javascript/google_js_api.js" type="text/javascript"></script>
    <script type="text/javascript">
        google.load('visualization', '1', { packages: ['controls'] });
        google.setOnLoadCallback(createTable);

        function createTable() {
            // Create the dataset (DataTable)  
            var myData = new google.visualization.DataTable();
            myData.addColumn('datetime','Date');
            myData.addColumn('number', 'Activity=0');
            myData.addColumn('number', 'Activity=1');
            myData.addColumn('number', 'Activity=-1');
            
            //myData.addColumn('number', 'Hours Worked');
            myData.addRows([
    [new Date(2013, 7, 24, 8, 30, 25), 1, 0, 0],
    [new Date(2013, 7, 24, 8, 30, 29), 0,1,0],
    [new Date(2013, 7, 24, 8, 31, 25), 0,0,1],
    [new Date(2013, 7, 25, 8, 3, 25), 0,2,8]
   
  ]);

            // Create a dashboard.  
            var dash_container = document.getElementById('dashboard_div'),
    myDashboard = new google.visualization.Dashboard(dash_container);

            // Create a date range slider  
            var myDateSlider = new google.visualization.ControlWrapper({
                'controlType': 'ChartRangeFilter',
                'containerId': 'control_div',
                'options': {
                    'filterColumnLabel': 'Date',

                    //"minValue": 10,
                    //"maxValue": 80
                }
            });

            // Table visualization  
            //var myTable = new google.visualization.ChartWrapper({
            //    'chartType': 'Table',
            //    'containerId': 'table_div'
            //});

            //// Bind myTable to the dashboard, and to the controls  
            //// this will make sure our table is update when our date changes  
            //myDashboard.bind(myDateSlider, myTable);

            // Line chart visualization  
            var myBar = new google.visualization.ChartWrapper({
                'chartType': 'BarChart',
                'containerId': 'bar_div',
               
                'options': {
                    isStacked:true
                    //axes: {
                    //    x: {
                    //        0: { side: 'top', label: 'Percentage' } // Top x-axis.
                    //    }
                    //},
                    //'chartArea': { 'height': '80%', 'width': '90%' },
                    //'chxt':'r'

                }
  
            });


            // Bind myLine to the dashboard, and to the controls  
            // this will make sure our line chart is update when our date changes  
            myDashboard.bind(myDateSlider, myBar);

            google.visualization.events.addListener(myDateSlider, 'statechange', function (e) {
                if (e.inProgress == false) {

                    ////////%%%%%%%%%%%%%%%%%%
                    // Load the Visualization API and the piechart package.
                    google.load('visualization', '1.0', { 'packages': ['corechart'] });

                    function drawChart() {

                        // Create the data table.
                        var data = new google.visualization.DataTable();
                        data.addColumn('string', 'Activity_name');
                        data.addColumn('number', 'Number');
                        data.addRows([
                          ['Activity=1', 3],
                          ['Activity=0', 1],
                          ['Activity=-1', 1]
                        ]);

                        // Set chart options
                        var options = {
                            'title': 'Activity numbers during this period',
                            'width': 400,
                            'height': 300
                        };

                        // Instantiate and draw our chart, passing in some options.
                        var chart = new google.visualization.BarChart(document.getElementById('fuckbar_div'));
                        chart.draw(data, options);
                    }
                    ////////////////%%%%%%%%%%%%%%%%%%EEEEEEE
                    drawChart();
                }
            });



            myDashboard.draw(myData);
   
        }
  
</script>  
  
    <script type="text/javascript">
        $(function () {
            $.ajax({
                type: "GET",
                url: "GetEventNumbers_TimeWindow.ashx",
                data:"type=list_event",
                dataType: "JSON",
                success: function (object) {
                    for (var i = 0; i < object.length; i++)
                    {
                        $("#eventsList").append("<option value ='" + object[i] + "'>" + object[i] + "</option>");
                    }
                    
                }
            });
        });
    </script>
</head>
<body>
<div id="dashboard_div">  
  <div id="control_div"><!-- Controls renders here --></div>  
  <div id="bar_div"><!-- Line chart renders here --></div>  
  <div id="table_div"><!-- Table renders here --></div>  
</div>  

    <div id="fuckbar_div"></div>
<select id="eventsList">  </select>  

</body>
</html>
