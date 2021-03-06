<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LatestResultShow.aspx.cs" Inherits="WebApplication1.LatestResultShow" %>
<%@ Register  TagPrefix="usercontrol" TagName="MainNav" Src="~/UserControl/MainNav.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>水利数值模拟云服务平台</title>
<link href="css/styles.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7/jquery.min.js"></script>
<script type="text/javascript" src="js/plugins/forms/ui.spinner.js"></script>
<script type="text/javascript" src="js/plugins/forms/jquery.mousewheel.js"></script>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"></script>
<script type="text/javascript" src="js/plugins/charts/excanvas.min.js"></script>
<script type="text/javascript" src="js/plugins/charts/jquery.flot.js"></script>
<script type="text/javascript" src="js/plugins/charts/jquery.flot.orderBars.js"></script>
<script type="text/javascript" src="js/plugins/charts/jquery.flot.pie.js"></script>
<script type="text/javascript" src="js/plugins/charts/jquery.flot.resize.js"></script>
<script type="text/javascript" src="js/plugins/charts/jquery.sparkline.min.js"></script>

<script type="text/javascript" src="js/plugins/tables/jquery.dataTables.js"></script>
<script type="text/javascript" src="js/plugins/tables/jquery.sortable.js"></script>
<script type="text/javascript" src="js/plugins/tables/jquery.resizable.js"></script>
<script src="Scripts/jquery.cookie.js" type="text/javascript"></script>
<script type="text/javascript" src="js/plugins/forms/autogrowtextarea.js"></script>
<script type="text/javascript" src="js/plugins/forms/jquery.uniform.js"></script>
<script type="text/javascript" src="js/plugins/forms/jquery.inputlimiter.min.js"></script>
<script type="text/javascript" src="js/plugins/forms/jquery.tagsinput.min.js"></script>
<script type="text/javascript" src="js/plugins/forms/jquery.maskedinput.min.js"></script>
<script type="text/javascript" src="js/plugins/forms/jquery.autotab.js"></script>
<script type="text/javascript" src="js/plugins/forms/jquery.chosen.min.js"></script>
<script type="text/javascript" src="js/plugins/forms/jquery.dualListBox.js"></script>
<script type="text/javascript" src="js/plugins/forms/jquery.cleditor.js"></script>
<script type="text/javascript" src="js/plugins/forms/jquery.ibutton.js"></script>
<script type="text/javascript" src="js/plugins/forms/jquery.validationEngine-en.js"></script>
<script type="text/javascript" src="js/plugins/forms/jquery.validationEngine.js"></script>

<script type="text/javascript" src="js/plugins/uploader/plupload.js"></script>
<script type="text/javascript" src="js/plugins/uploader/plupload.html4.js"></script>
<script type="text/javascript" src="js/plugins/uploader/plupload.html5.js"></script>
<script type="text/javascript" src="js/plugins/uploader/jquery.plupload.queue.js"></script>

<script type="text/javascript" src="js/plugins/wizards/jquery.form.wizard.js"></script>
<script type="text/javascript" src="js/plugins/wizards/jquery.validate.js"></script>
<script type="text/javascript" src="js/plugins/wizards/jquery.form.js"></script>

<script type="text/javascript" src="js/plugins/ui/jquery.collapsible.min.js"></script>
<script type="text/javascript" src="js/plugins/ui/jquery.breadcrumbs.js"></script>
<script type="text/javascript" src="js/plugins/ui/jquery.tipsy.js"></script>
<script type="text/javascript" src="js/plugins/ui/jquery.progress.js"></script>
<script type="text/javascript" src="js/plugins/ui/jquery.timeentry.min.js"></script>
<script type="text/javascript" src="js/plugins/ui/jquery.colorpicker.js"></script>
<script type="text/javascript" src="js/plugins/ui/jquery.jgrowl.js"></script>
<script type="text/javascript" src="js/plugins/ui/jquery.fancybox.js"></script>
<script type="text/javascript" src="js/plugins/ui/jquery.fileTree.js"></script>
<script type="text/javascript" src="js/plugins/ui/jquery.sourcerer.js"></script>

<script type="text/javascript" src="js/plugins/others/jquery.fullcalendar.js"></script>
<script type="text/javascript" src="js/plugins/others/jquery.elfinder.js"></script>

<script type="text/javascript" src="js/plugins/ui/jquery.easytabs.min.js"></script>
<script type="text/javascript" src="js/files/bootstrap.js"></script>
<script type="text/javascript" src="js/files/functions.js"></script>
<script type="text/javascript" src="js/charts/chart.js"></script>
<script type="text/javascript" src="js/charts/bar.js"></script>
<script type="text/javascript" src="js/charts/hBar.js"></script>
<script type="text/javascript" src="js/charts/updating.js"></script>
<script type="text/javascript" src="js/charts/pie.js"></script>

<script type="text/javascript" src="js/charts/bar_side.js"></script>

<script type="text/javascript">
    $(function () {
        var sin = [];
        var ChartName = "Y-水位 X-流量";
        var strVal = $("#litForScript").val();
        eval(strVal);
        var valScript = $("#litForScript")
        var plot = $.plot($(".chart"),
           [{ data: sin, label: ChartName}], {
               series: {
                   lines: { show: true },
                   points: { show: true }
               },
               grid: { hoverable: true, clickable: true },
               yaxis: { min: 0, max: 150 },
               xaxis: { min: 0, max: 300 }
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
</script>
</head>

<body>


<input id="litForScript" runat="server" type="hidden" />
<input id="YMax" runat="server" type="hidden" />
<input id="XMax" runat="server" type="hidden" />

<!-- Sidebar begins -->
<div id="sidebar" style=" visibility:hidden;" >
    <!-- Secondary nav -->
    <div class="secNav">
        <div class="secWrapper">
            
            <!-- Sidebar chart -->
            <div class="sideChart">
                <div class="chartS"></div>
            </div>
            
            <div class="divider"><span></span></div>
            
            <!-- Sidebar chart -->
            <div class="sideChart">
                <div class="barsS" id="placeholder1_hS"></div>
            </div>
            
            <div class="divider"><span></span></div>
            
            <!-- Sidebar chart -->
            <div class="sideChart">
                <div class="barsS" id="placeholder1S"></div>
            </div>
            
            <div class="divider"><span></span></div>
            
       </div> 
       <div class="clear"></div>
   </div>
</div>
<!-- Sidebar ends -->
    
    
<!-- Content begins -->
<div id="content"  style=" margin:0px;">
   
    <!-- Main content -->
    <div class="wrapper">    
        <div class="widget chartWrapper" style=" margin-top:0px;">
        <div id="showresult">
        </div>
        <div class="whead"><h6><%=ModelResultLogs.CalTime%></h6><div class="clear"></div></div>
            <div class="whead"><h6>结果显示</h6><div class="clear"></div></div>
            <div class="body">
            <div class="chart"></div></div>
        </div>
    
        <div class="fluid">
        
            <!-- Bars chart -->
            <div class="widget grid6 chartWrapper">
                <div class="whead"><h6>Vertican bars</h6><div class="clear"></div></div>
                <div class="body"><div class="bars" id="placeholder1"></div></div>
            </div>
            
            <!-- Bars chart -->
            <div class="widget grid6 chartWrapper">
                <div class="whead"><h6>Horizontal bars</h6><div class="clear"></div></div>
                <div class="body"><div class="bars" id="placeholder1_h"></div></div>
            </div>
        
        </div>
    
        <div class="fluid">
        
            <!-- Donut -->
            <div class="widget grid4 chartWrapper">
                <div class="whead"><h6>Donut chart</h6><div class="clear"></div></div>
                <div class="body"><div class="pie" id="donut"></div></div>
            </div>
            
            <!-- Auto updating chart -->
            <div class="widget grid8 chartWrapper">
                <div class="whead"><h6>Auto updating chart</h6><div class="clear"></div></div>
                <div class="body"><div class="updating"></div></div>
            </div>
            <div class="clear"></div>
            
        </div>
    </div>
    <!-- Main content ends -->
    
</div>
 <!-- Content ends -->       
        

</body>
</html>
