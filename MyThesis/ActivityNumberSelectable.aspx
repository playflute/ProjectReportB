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
            myData.addColumn('datetime','Date');
            myData.addColumn('number', 'Activity=0');
            myData.addColumn('number', 'Activity=1');
            myData.addColumn('number', 'Activity=-1');
            
            //myData.addColumn('number', 'Hours Worked');
            myData.addRows([
    [new Date(<%=start_str.ToString()%>), 0, 0, 0],
    //[new Date(2013, 7, 24, 8, 30, 25), 8],
    //[new Date(2013, 7, 24, 8, 30, 25), 10],
    //[new Date(2013, 7, 24, 8, 30, 25), 8],
    [new Date(<%=end_str.ToString()%>), 1, 2, 3]
  ]);

            // Create a dashboard.  
            var dash_container = document.getElementById('dashboard_div'),
    myDashboard = new google.visualization.Dashboard(dash_container);

            // Create a date range slider  ,换成全局了，我把var删掉了
             myDateSlider = new google.visualization.ControlWrapper({
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
                'containerId': 'bar_div'
               
                //'options': {
                //    //axes: {
                //    //    x: {
                //    //        0: { side: 'top', label: 'Percentage' } // Top x-axis.
                //    //    }
                //    //},
                //    //'chartArea': { 'height': '80%', 'width': '90%' }
                //    

                //}
  
            });


            // Bind myLine to the dashboard, and to the controls  
            // this will make sure our line chart is update when our date changes  
            myDashboard.bind(myDateSlider, myBar);
            google.visualization.events.addListener(myDateSlider, 'statechange', function (e) {
                if (e.inProgress == false)
                {
                    var state = myDateSlider.getState();
                    //parse start time into standard format
                    var start_datetime = state.range.start;
                    var start_year = start_datetime.getFullYear();
                    var start_month = start_datetime.getMonth() + 1 < 10 ? "0" + (start_datetime.getMonth() + 1) : start_datetime.getMonth() + 1;
                    var start_date = start_datetime.getDate() < 10 ? "0" + start_datetime.getDate() : start_datetime.getDate();
                    var start_hour = start_datetime.getHours() < 10 ? "0" + start_datetime.getHours() : start_datetime.getHours();
                    var start_minute = start_datetime.getMinutes() < 10 ? "0" + start_datetime.getMinutes() : start_datetime.getMinutes();
                    var start_second = start_datetime.getSeconds() < 10 ? "0" + start_datetime.getSeconds() : start_datetime.getSeconds();
                    var standard_start_time = start_year + "-" + start_month + "-" + start_date + " " + start_hour + ":" + start_minute + ":" + start_second;
                    /////////////
                    //parse end time into standard format
                    var end_datetime = state.range.end;
                    var end_year = end_datetime.getFullYear();
                    var end_month = end_datetime.getMonth() + 1 < 10 ? "0" + (end_datetime.getMonth() + 1) : end_datetime.getMonth() + 1;
                    var end_date = end_datetime.getDate() < 10 ? "0" + end_datetime.getDate() : end_datetime.getDate();
                    var end_hour = end_datetime.getHours() < 10 ? "0" + end_datetime.getHours() : end_datetime.getHours();
                    var end_minute = end_datetime.getMinutes() < 10 ? "0" + end_datetime.getMinutes() : end_datetime.getMinutes();
                    var end_second = end_datetime.getSeconds() < 10 ? "0" + end_datetime.getSeconds() : end_datetime.getSeconds();
                    var standard_end_time = end_year + "-" + end_month + "-" + end_date + " " + end_hour + ":" + end_minute + ":" + end_second;
                    /////////////
                    //alert(state.range.start.toLocaleString()); //得到了过滤器的起始时间☆☆☆☆☆☆☆☆☆☆☆☆☆☆
                    var eventName = $("#eventsList").val();
                    $.ajax({
                        type: "POST",
                        url: "GetEventNumbers_TimeWindow.ashx",
                        dataType: "json",
                        data: "startTime=" + standard_start_time + "&endTime=" + standard_end_time + "&type=query&eventName=" + eventName,
                        success: function (msg) {

                            google.load('visualization', '1.0', { 'packages': ['corechart'] });

                            function drawChart() {

                                // Create the data table.
                                var data = new google.visualization.DataTable();
                                data.addColumn('string', 'Activity_name');
                                data.addColumn('number', 'Number');
                                data.addRows([
                                  ['Activity=1', parseInt(msg["1"])],
                                  ['Activity=0', parseInt(msg["0"])],
                                  ['Activity=-1', parseInt(msg["-1"])]
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

                            drawChart();



                        }
                    });



                }




            });


            myDashboard.draw(myData);
            //myDateSlider.draw(myData);
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

    <script type="text/javascript">
        google.load('visualization', '1', { 'packages': ['corechart'] });
        $(function () {
            /////初始加载页面时画的barchart Start
            //////////////////△△△△△△△△△△△△△△△△△△△△△△
           
           
            $.ajax({
                type: "GET",
                url: "GetEventNumbers_TimeWindow.ashx",
                dataType: "json",
                data: "type=query",
                success: function (msg) {

                    
                    //google.setOnLoadCallback(drawChart_initial);
                    function drawChart_initial() {

                        // Create the data table.
                        var data = new google.visualization.DataTable();
                        data.addColumn('string', 'Activity_name');
                        data.addColumn('number', 'Number');
                        data.addRows([
                          ['Activity=1', parseInt(msg["1"])],
                          ['Activity=0', parseInt(msg["0"])],
                          ['Activity=-1', parseInt(msg["-1"])]
                        ]);


                        // Set chart options
                        var options = {
                            'title': 'Activity numbers during this period',
                            'width': 400,
                            'height': 300
                        };

                        // Instantiate and draw our chart, passing in some options.
                         chart = new google.visualization.BarChart(document.getElementById('fuckbar_div'));
                        chart.draw(data, options);
                    }

                    drawChart_initial();



                }
            });

            //////////////////△△△△△△△△△△△△△△△△△△△△△△
            /////初始加载页面时画的barchart End

            //when change the event name
            $("#eventsList").change(function () {
                
                //////////////////△△△△△△△△△△△△△△△△△△△△△△
                var state = myDateSlider.getState();
     
                //parse start time into standard format
                if (state.range == undefined) {
                    //代表range还没有值，应使用初始值
                    //alert("state.range是undefined的");
                    var start_datetime = new Date(<%=start_str.ToString()%>);
                    var start_year = start_datetime.getFullYear();
                    var start_month = start_datetime.getMonth() + 1 < 10 ? "0" + (start_datetime.getMonth() + 1) : start_datetime.getMonth() + 1;
                    var start_date = start_datetime.getDate() < 10 ? "0" + start_datetime.getDate() : start_datetime.getDate();
                    var start_hour = start_datetime.getHours() < 10 ? "0" + start_datetime.getHours() : start_datetime.getHours();
                    var start_minute = start_datetime.getMinutes() < 10 ? "0" + start_datetime.getMinutes() : start_datetime.getMinutes();
                    var start_second = start_datetime.getSeconds() < 10 ? "0" + start_datetime.getSeconds() : start_datetime.getSeconds();
                    var standard_start_time = start_year + "-" + start_month + "-" + start_date + " " + start_hour + ":" + start_minute + ":" + start_second;
                    /////////////
                    //parse end time into standard format
                    var end_datetime = new Date(<%=end_str.ToString()%>);
                    var end_year = end_datetime.getFullYear();
                    var end_month = end_datetime.getMonth() + 1 < 10 ? "0" + (end_datetime.getMonth() + 1) : end_datetime.getMonth() + 1;
                    var end_date = end_datetime.getDate() < 10 ? "0" + end_datetime.getDate() : end_datetime.getDate();
                    var end_hour = end_datetime.getHours() < 10 ? "0" + end_datetime.getHours() : end_datetime.getHours();
                    var end_minute = end_datetime.getMinutes() < 10 ? "0" + end_datetime.getMinutes() : end_datetime.getMinutes();
                    var end_second = end_datetime.getSeconds() < 10 ? "0" + end_datetime.getSeconds() : end_datetime.getSeconds();
                    var standard_end_time = end_year + "-" + end_month + "-" + end_date + " " + end_hour + ":" + end_minute + ":" + end_second;
                }
                else {
                    var start_datetime = state.range.start;
                    var start_year = start_datetime.getFullYear();
                    var start_month = start_datetime.getMonth() + 1 < 10 ? "0" + (start_datetime.getMonth() + 1) : start_datetime.getMonth() + 1;
                    var start_date = start_datetime.getDate() < 10 ? "0" + start_datetime.getDate() : start_datetime.getDate();
                    var start_hour = start_datetime.getHours() < 10 ? "0" + start_datetime.getHours() : start_datetime.getHours();
                    var start_minute = start_datetime.getMinutes() < 10 ? "0" + start_datetime.getMinutes() : start_datetime.getMinutes();
                    var start_second = start_datetime.getSeconds() < 10 ? "0" + start_datetime.getSeconds() : start_datetime.getSeconds();
                    var standard_start_time = start_year + "-" + start_month + "-" + start_date + " " + start_hour + ":" + start_minute + ":" + start_second;
                    /////////////
                    //parse end time into standard format
                    var end_datetime = state.range.end;
                    var end_year = end_datetime.getFullYear();
                    var end_month = end_datetime.getMonth() + 1 < 10 ? "0" + (end_datetime.getMonth() + 1) : end_datetime.getMonth() + 1;
                    var end_date = end_datetime.getDate() < 10 ? "0" + end_datetime.getDate() : end_datetime.getDate();
                    var end_hour = end_datetime.getHours() < 10 ? "0" + end_datetime.getHours() : end_datetime.getHours();
                    var end_minute = end_datetime.getMinutes() < 10 ? "0" + end_datetime.getMinutes() : end_datetime.getMinutes();
                    var end_second = end_datetime.getSeconds() < 10 ? "0" + end_datetime.getSeconds() : end_datetime.getSeconds();
                    var standard_end_time = end_year + "-" + end_month + "-" + end_date + " " + end_hour + ":" + end_minute + ":" + end_second;

                }
               
 
                /////////////
                //alert(state.range.start.toLocaleString()); //得到了过滤器的起始时间☆☆☆☆☆☆☆☆☆☆☆☆☆☆

                var eventName = $("#eventsList").val();
                //google.load('visualization', '1.0', { 'packages': ['corechart'] });
                $.ajax({
                    type: "POST",
                    url: "GetEventNumbers_TimeWindow.ashx",
                    dataType: "json",
                    data: "startTime=" + standard_start_time + "&endTime=" + standard_end_time + "&type=query&eventName=" + eventName,
                    success: function (msg) {

                        
                        //google.setOnLoadCallback(drawChart2);
                        function drawChart_activity_number_bar() {

                            // Create the data table.
                            var data = new google.visualization.DataTable();
                            data.addColumn('string', 'Activity_name');
                            data.addColumn('number', 'Number');
                            data.addRows([
                              ['Activity=1', parseInt(msg["1"])],
                              ['Activity=0', parseInt(msg["0"])],
                              ['Activity=-1', parseInt(msg["-1"])]
                            ]);


                            // Set chart options
                            var options = {
                                'title': 'Activity numbers during this period',
                                'width': 400,
                                'height': 300,
                                animation: {
                                    duration: 1000,
                                    easing: 'out',
                                }
                            };

                            // Instantiate and draw our chart, passing in some options.
                            var chart = new google.visualization.BarChart(document.getElementById('fuckbar_div'));
                            chart.draw(data, options);
                        }

                        drawChart_activity_number_bar();



                    }
                });

                //////////////////△△△△△△△△△△△△△△△△△△△△△△

            });

        });

    </script>
</head>
<body>
<div id="dashboard_div">  
  <div id="control_div"><!-- Controls renders here --></div>  
  <div id="bar_div" style="display:none"><!-- Line chart renders here --></div>  
  <div id="table_div"><!-- Table renders here --></div>  
</div>  

        <div id="fuckbar_div"></div>
<select id="eventsList">  </select>  

</body>
</html>
