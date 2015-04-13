<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivityNumberSelectable.aspx.cs" Inherits="MyThesis.ActivityNumberSelectable" %>






<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
            myData.addColumn('date', 'Date');
            myData.addColumn('number', 'Hours Worked');
            myData.addRows([
    [new Date(2014, 6, 12), 9],
    [new Date(2014, 6, 13), 8],
    [new Date(2014, 6, 14), 10],
    [new Date(2014, 6, 15), 8],
    [new Date(2014, 6, 16), 0]
  ]);

            // Create a dashboard.  
            var dash_container = document.getElementById('dashboard_div'),
    myDashboard = new google.visualization.Dashboard(dash_container);

            // Create a date range slider  
            var myDateSlider = new google.visualization.ControlWrapper({
                'controlType': 'ChartRangeFilter',
                'containerId': 'control_div',
                'options': {
                    'filterColumnLabel': 'Date'
                }
            });

            // Table visualization  
            var myTable = new google.visualization.ChartWrapper({
                'chartType': 'Table',
                'containerId': 'table_div'
            });

            // Bind myTable to the dashboard, and to the controls  
            // this will make sure our table is update when our date changes  
            myDashboard.bind(myDateSlider, myTable);

            // Line chart visualization  
            var myBar = new google.visualization.ChartWrapper({
                'chartType': 'BarChart',
                'containerId': 'bar_div'
            });


            // Bind myLine to the dashboard, and to the controls  
            // this will make sure our line chart is update when our date changes  
            myDashboard.bind(myDateSlider, myBar);
            google.visualization.events.addListener(myDateSlider, 'statechange', function () {
                var state = myDateSlider.getState();
                alert(state.range.start.toLocaleString()); //得到了过滤器的起始时间☆☆☆☆☆☆☆☆☆☆☆☆☆☆
                
                //console.log(state);



            });

            myDashboard.draw(myData);
        }
  
</script>  
  
</head>
<body>
<div id="dashboard_div">  
  <div id="control_div"><!-- Controls renders here --></div>  
  <div id="bar_div"><!-- Line chart renders here --></div>  
  <div id="table_div"><!-- Table renders here --></div>  
</div>  
<select id="eventsList">  
  <option value ="1">Volvo</option>  
  <option value ="2">Saab</option>  
  <option value="3">Opel</option>  
  <option value="4">Audi</option>  
</select>  

</body>
</html>
