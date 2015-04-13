<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivityOverTime.aspx.cs" Inherits="MyThesis.ActivityOverTime" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Activity Over Time Chart</title>
    <script src="Javascript/google_js_api.js" type="text/javascript"></script>
        <script type='text/javascript'>
            google.load('visualization', '1', { 'packages': ['annotatedtimeline'] });
            google.setOnLoadCallback(drawChart);
            function drawChart() {
                var data = new google.visualization.DataTable();


                data.addColumn('datetime', 'DateTime');
                data.addColumn('number', '次数');
                data.addColumn('string', '事件');



  
                
                data.addRows([
          [new Date(2008, 1, 1, 5, 30, 12, 0), 30, '0'],
          [new Date(2008, 1, 2, 5, 30, 12, 0), 50, '1'],
          [new Date(2008, 1, 3, 5, 30, 12, 0), 10, '-1']
//          [new Date(2008, 1, 3, 5, 30, 12, 0), undefined, undefined, 5, '公交'],
//          [new Date(2008, 1, 4, 5, 30, 12, 0), undefined, undefined, 300, '飞机'],
//          [new Date(2008, 1, 5, 5, 30, 12, 0), 150, '汤圆', undefined, undefined],
//          [new Date(2008, 1, 6, 5, 30, 12, 0), 27, '面包', undefined, undefined]
        ]);

                var chart = new google.visualization.AnnotatedTimeLine(document.getElementById('chart_div'));
                google.visualization.events.addListener(chart, 'rangechange', rangechange_handler);

                function rangechange_handler(e) {
                    console.log('You changed the range to ', e['start'], ' and ', e['end']);
//                    alert("your start time" + e['start'].toLocaleString());
//                    alert("your end time" + e['end'].toLocaleString());
                }

 
                chart.draw(data, { displayAnnotations: true });
            }
    </script>
</head>
<body>
<div id='chart_div' style='width: 700px; height: 240px;'></div>

</body>
</html>
