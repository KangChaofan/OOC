$(function () {
    var sin = [];
    for (var i = 0; i < 2000; i += 100) {
        sin.push([i, i + 200]);
    }
    var ChartName = "H-shuiwei Q-liuliang";
    var plot = $.plot($(".chart"),
           [{ data: sin, label: ChartName}], {
               series: {
                   lines: { show: true },
                   points: { show: true }
               },
               grid: { hoverable: true, clickable: true },
               yaxis: { min: 1990, max: 20000 },
               xaxis: { min: 1990, max: 20000 }
           });


    function showTooltip(x, y, contents) {
        $('<div id="tooltip" class="tooltip">' + contents + '</div>').css({
            position: 'absolute',
            display: 'none',
            top: y + 5,
            left: x + 5,
            'z-index': '9999',
            'color': '#fff',
            'font-size': '11px',
            opacity: 0.8
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $(".chart").bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));
        if ($(".chart").length > 0) {
            if (item) {
                if (previousPoint != item.dataIndex) {
                    previousPoint = item.dataIndex;
                    $("#tooltip").remove();
                    var x = item.datapoint[0].toFixed(2),
                        y = item.datapoint[1].toFixed(2);
                    showTooltip(item.pageX, item.pageY,
                                item.series.label + " of " + x + " = " + y);
                }
            }
            else {
                $("#tooltip").remove();
                previousPoint = null;
            }
        }
    });

    $(".chart").bind("plotclick", function (event, pos, item) {
        if (item) {
            $("#clickdata").text("You clicked point " + item.dataIndex + " in " + item.series.label + ".");
            plot.highlight(item.series, item.datapoint);
        }
    });
});


