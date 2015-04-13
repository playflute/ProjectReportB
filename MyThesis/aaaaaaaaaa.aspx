<html>
  <head>
    <!--Load the AJAX API-->
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">

        // Load the Visualization API and the piechart package.
        google.load('visualization', '1.0', { 'packages': ['corechart'] });

        // Set a callback to run when the Google Visualization API is loaded.
        google.setOnLoadCallback(drawChart);

        // Callback that creates and populates a data table,
        // instantiates the pie chart, passes in the data and
        // draws it.
        function drawChart() {

            // Create the data table.
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'N');
            data.addColumn('number', 'Value');
            data.addRow(['V', 200]);
            // Set chart options
            var options = {
                'title': 'How Much Pizza I Ate Last Night',
                'width': 400,
                'height': 300,
                animation: {
                    duration: 1000,
                    easing: 'out',
                }

            };

            // Instantiate and draw our chart, passing in some options.
            var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
            var button = document.getElementById('btn');

            function drawChart() {
                // Disabling the button while the chart is drawing.
                button.disabled = true;
                google.visualization.events.addListener(chart, 'ready',
                    function () {
                        button.disabled = false;
                    });
                chart.draw(data, options);
            }

            button.onclick = function () {
                var newValue = 1000 - data.getValue(0, 1);
                data.addRow(['N', 300]);
                drawChart();
            }
            drawChart();
        }
    </script>
  </head>

  <body>
    <!--Div that will hold the pie chart-->
    <div id="chart_div"></div>
      <input type="button" id="btn" i value="点击我显示动画效果 " />
  </body>
</html>