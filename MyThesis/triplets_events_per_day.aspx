<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="triplets_events_per_day.aspx.cs" Inherits="MyThesis.triplets_events_per_day" %>

<!DOCTYPE html>

<html>
  <head>
      <title></title>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
      <script src="Javascript/jquery-2.1.3.min.js"></script>
      <script src="Javascript/jsrender.min.js"></script>
      <script src="Javascript/bootstrap.min.js"></script>
      <script src="Javascript/bootstrap-select.min.js"></script>
      <link href="CSS/bootstrap.min.css" rel="stylesheet" />
      <link href="CSS/bootstrap-theme.min.css" rel="stylesheet" />
      <link href="CSS/bootstrap-select.min.css" rel="stylesheet" />


      
      <script id="dataitemTempl" type="text/x-jsrender">[new Date({{:date}}),{{:number}}],</script>
                  <script type="text/javascript">
                      $(function () {
                          //////////333
                          //google.load("visualization", "1.1", { packages: ["calendar"] });
                          google.setOnLoadCallback(function () {
                              $(function () {
                                  // init my stuff
                                  $.ajax({
                                      type: "POST",
                                      url: "triplets.ashx",
                                      dataType: "JSON",
                                      data: "type=list_event_number_per_day",
                                      success: function (msg) {
                                          var myTemplate = $.templates("#dataitemTempl");
                                          var day_and_number_array = myTemplate.render(msg).toString();
                                          var day_and_number_array_removed_comma = day_and_number_array.substring(0, day_and_number_array.length - 1);

                                          google.setOnLoadCallback(drawChart);
                                          function drawChart() {
                                              var dataTable = new google.visualization.DataTable();
                                              dataTable.addColumn({ type: 'date', id: 'Date' });
                                              dataTable.addColumn({ type: 'number', id: 'EventNumber' });
                                              //dataTable.addRows([
                                              //   [new Date(2012, 3, 13), 37032],
                                              //   [new Date(2012, 3, 14), 38024],
                                              //   [new Date(2012, 3, 15), 38024]    
                                              //]);
                                              dataTable.addRows(eval("[" + day_and_number_array_removed_comma + "]"));

                                              var chart = new google.visualization.Calendar(document.getElementById('calendar_basic'));

                                              var options = {
                                                  title: "Event occurrence number",
                                                  height: 350,
                                                  noDataPattern: {
                                                      backgroundColor: '#76a7fa',
                                                      color: '#000000'
                                                  }
                                              };

                                              chart.draw(dataTable, options);
                                          }
                                          drawChart();



                                      }
                                  });
                              });
                          });

                      });
      </script>

      
      <script type="text/javascript" src='https://www.google.com/jsapi?autoload={"modules":[{"name":"visualization","version":"1.1","packages":["calendar"]}]}'>
      </script>
             <script id="dataitemTemp2" type="text/x-jsrender">[new Date({{:date}}),{{:number}}],</script>
      <script type="text/javascript">

          $(function () {

              $.ajax({
                  type: "GET",
                  url: "triplets.ashx",
                  data: "type=list_all_events",
                  dataType: "JSON",
                  success: function (object) {
                      $("#eventsList").append("<option value='show_all_event'>*********All events**************</option>");

                      for (var i = 0; i < object.length; i++) {
                          $("#eventsList").append("<option value ='" + object[i] + "'>" + object[i] + "</option>");
                      }
                      $('.selectpicker').selectpicker("refresh");

                  }
              });

              $("#eventsList").change(function () {                      
                        
                            
                              $.ajax({
                                  type: "POST",
                                  url: "triplets.ashx",
                                  dataType: "JSON",
                                  data: "type=list_single_event&" + "eventname=" + $("#eventsList").val(),
                                  success: function (msg2) {
                                      var myTemplate2 = $.templates("#dataitemTemp2");
                                      var day_and_number_array2 = myTemplate2.render(msg2).toString();
                                      var day_and_number_array_removed_comma2 = day_and_number_array2.substring(0, day_and_number_array2.length - 1);

                                      //google.setOnLoadCallback(drawChart);
                                      function drawChart2() {
                                          var dataTable2 = new google.visualization.DataTable();
                                          dataTable2.addColumn({ type: 'date', id: 'Date' });
                                          dataTable2.addColumn({ type: 'number', id: 'EventNumber' });

                                          dataTable2.addRows(eval("[" + day_and_number_array_removed_comma2 + "]"));

                                          var chart2 = new google.visualization.Calendar(document.getElementById('calendar_basic'));

                                          var options2 = {
                                              title: "Event occurrence number",
                                              height: 350,
                                              noDataPattern: {
                                                  backgroundColor: '#76a7fa',
                                                  color: '#FF0000'
                                              }
                                          };

                                          chart2.draw(dataTable2, options2);
                                      }
                                      drawChart2();



                                  }
                              });
                          
                      



              });

              ws = new WebSocket("ws://localhost:8015/Action/ChatHandler.ashx");
              ws.onmessage = function (evt) {
                  if (evt.data == "U") {
                      /////////////#####################################
                      $.ajax({
                          type: "POST",
                          url: "triplets.ashx",
                          dataType: "JSON",
                          data: "type=list_single_event&" + "eventname=" + $("#eventsList").val(),
                          success: function (msg2) {
                              var myTemplate2 = $.templates("#dataitemTemp2");
                              var day_and_number_array2 = myTemplate2.render(msg2).toString();
                              var day_and_number_array_removed_comma2 = day_and_number_array2.substring(0, day_and_number_array2.length - 1);

                              //google.setOnLoadCallback(drawChart);
                              function drawChart2() {
                                  var dataTable2 = new google.visualization.DataTable();
                                  dataTable2.addColumn({ type: 'date', id: 'Date' });
                                  dataTable2.addColumn({ type: 'number', id: 'EventNumber' });

                                  dataTable2.addRows(eval("[" + day_and_number_array_removed_comma2 + "]"));

                                  var chart2 = new google.visualization.Calendar(document.getElementById('calendar_basic'));

                                  var options2 = {
                                      title: "Event occurrence number",
                                      height: 350,
                                      noDataPattern: {
                                          backgroundColor: '#76a7fa',
                                          color: '#FF0000'
                                      }
                                  };

                                  chart2.draw(dataTable2, options2);
                              }
                              drawChart2();



                          }
                      });
                      /////////////#####################################
                  }
              };
          })
          ///////////////////接收websocket并且重绘


      </script>
  </head>
  <body>
    <div id="calendar_basic" style="width: 1000px; height: 350px;"></div>
      <select id="eventsList" class="selectpicker" data-live-search="true" data-style="btn-primary" data-width="auto">
          
      </select>  

  </body>
</html>
