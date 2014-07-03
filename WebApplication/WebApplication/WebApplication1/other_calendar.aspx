﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="other_calendar.aspx.cs" Inherits="WebApplication1.other_calendar" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
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

</head>

<body>

<!-- Style switcher -->
<div class="switcher">
    <a href="#" title="" class="pat1"><img src="images/switcher/2.png" alt="" /></a>
    <a href="#" title="" class="pat2"><img src="images/switcher/3.png" alt="" /></a>
    <a href="#" title="" class="pat3"><img src="images/switcher/4.png" alt="" /></a>
    <a href="#" title="" class="pat4"><img src="images/switcher/5.png" alt="" /></a>
    <a href="#" title="" class="pat5"><img src="images/switcher/6.png" alt="" /></a>
    <a href="#" title="" class="pat6"><img src="images/switcher/7.png" alt="" /></a>
    <a href="#" title="" class="pat7"><img src="images/switcher/8.png" alt="" /></a>
    <a href="#" title="" class="pat8"><img src="images/switcher/9.png" alt="" /></a>
    <a href="#" title="" class="pat10"><img src="images/switcher/10.png" alt="" /></a>
    <a href="#" title="" class="pat9"><img src="images/switcher/1.png" alt="" /></a>
</div>

<!-- Top line begins -->
<div id="top">
	<div class="wrapper">
    	<a href="index.html" title="" class="logo"><img src="images/logo.png" alt="" /></a>
        
        <!-- Right top nav -->
        <div class="topNav">
            <ul class="userNav">
                <li><a title="" class="search"></a></li>
                <li><a href="#" title="" class="screen"></a></li>
                <li><a href="#" title="" class="settings"></a></li>
                <li><a href="#" title="" class="logout"></a></li>
                <li class="showTabletP"><a href="#" title="" class="sidebar"></a></li>
            </ul>
            <a title="" class="iButton"></a>
            <a title="" class="iTop"></a>
            <div class="topSearch">
            	<div class="topDropArrow"></div>
                <form action="">
                    <input type="text" placeholder="search..." name="topSearch" />
                    <input type="submit" value="" />
                </form>
            </div>
        </div>
        
        <!-- Responsive nav -->
        <ul class="altMenu">
            <li><a href="index.html" title="">Dashboard</a></li>
            <li><a href="ui.html" title="" class="exp">UI elements</a>
                <ul>
                    <li><a href="ui.html">General elements</a></li>
                    <li><a href="ui_icons.html">Icons</a></li>
                    <li><a href="ui_buttons.html">Button sets</a></li>
                    <li><a href="ui_grid.html">Grid</a></li>
                    <li><a href="ui_custom.html">Custom elements</a></li>
                    <li><a href="ui_experimental.html">Experimental</a></li>
                </ul>
            </li>
            <li><a href="forms.html" title="" class="exp">Forms stuff</a>
                <ul>
                    <li><a href="forms.html">Inputs &amp; elements</a></li>
                    <li><a href="form_validation.html">Validation</a></li>
                    <li><a href="form_editor.html">File uploads &amp; editor</a></li>
                    <li><a href="form_wizards.html">Form wizards</a></li>
                </ul>
            </li>
            <li><a href="messages.html" title="">Messages</a></li>
            <li><a href="statistics.html" title="">Statistics</a></li>
            <li><a href="tables.html" title="" class="exp">Tables</a>
                <ul>
                    <li><a href="tables.html">Standard tables</a></li>
                    <li><a href="tables_dynamic.html">Dynamic tables</a></li>
                    <li><a href="tables_control.html">Tables with control</a></li>
                    <li><a href="tables_sortable.html">Sortable &amp; resizable</a></li>
                </ul>
            </li>
            <li><a href="other_calendar.html" title="" class="exp" id="current">Other pages</a>
                <ul>
                    <li><a href="other_calendar.html" class="active">Calendar</a></li>
                    <li><a href="other_gallery.html">Images gallery</a></li>
                    <li><a href="other_file_manager.html">File manager</a></li>
                    <li><a href="other_404.html">Sample error page</a></li>
                    <li><a href="other_typography.html">Typography</a></li>
                </ul>
            </li>
        </ul>
        
        <div class="clear"></div>
    </div>
</div>
<!-- Top line ends -->


<!-- Sidebar begins -->
<div id="sidebar">

	<!-- Main nav -->
 <div class="mainNav">
        <div class="user">
            <a title="" class="leftUserDrop"><img src="images/user.png" alt="" /><span></span></a><span>黄科院</span>
            <ul class="leftUser">
                <li><a href="#" title="" class="sProfile">My profile</a></li>
                <li><a href="#" title="" class="sMessages">Messages</a></li>
                <li><a href="#" title="" class="sSettings">Settings</a></li>
                <li><a href="#" title="" class="sLogout">Logout</a></li>
            </ul>
        </div>
        
        <!-- Responsive nav -->
        <div class="altNav">
            <div class="userSearch">
                <form action="">
                    <input type="text" placeholder="search..." name="userSearch" />
                    <input type="submit" value="" />
                </form>
            </div>
            
            <!-- User nav -->
            <ul class="userNav">
                <li><a href="#" title="" class="profile"></a></li>
                <li><a href="#" title="" class="messages"></a></li>
                <li><a href="#" title="" class="settings"></a></li>
                <li><a href="#" title="" class="logout"></a></li>
            </ul>
        </div>
        
        <!-- Main nav -->
        <ul class="nav">
            <li><a href="index.aspx" title=""><span>模型介绍</span></a></li>
            <li><a href="ui.aspx" title=""><span>单模型库</span></a>
                <ul>
                    <li><a href="ui.aspx" title="">一维水沙</a></li>
                    <li><a href="ui_icons.aspx" title="">二维水沙</a></li>
                    <li><a href="ui_buttons.aspx" title="">水库调度</a></li>
                    <li><a href="ui_grid.aspx" title="">区域产流</a></li>
                    <li><a href="ui_custom.aspx" title="">区域汇流</a></li>
                </ul>
            </li>
            <li><a href="forms.aspx" title=""><span>模型组合</span></a>
                <ul>
                    <li><a href="forms.aspx" title=""><span class="icol-list"></span>Inputs &amp; elements</a></li>
                    <li><a href="form_validation.html" title=""><span class="icol-alert"></span>Validation</a></li>
                    <li><a href="form_editor.html" title=""><span class="icol-pencil"></span>File uploader &amp; WYSIWYG</a></li>
                    <li><a href="form_wizards.html" title=""><span class="icol-signpost"></span>Form wizards</a></li>
                </ul>
            </li>
            <li><a href="messages.aspx" title=""><span>模型发布</span></a></li>
            <li><a href="statistics.aspx" title=""><span>计算结果显示</span></a></li>
            <li><a href="tables.aspx" title=""><span>模型库管理</span></a>
                <ul>
                    <li><a href="tables.aspx" title=""><span class="icol-frames"></span>Standard tables</a></li>
                    <li><a href="tables_dynamic.aspx" title=""><span class="icol-refresh"></span>Dynamic table</a></li>
                    <li><a href="tables_control.aspx" title=""><span class="icol-bullseye"></span>Tables with control</a></li>
                    <li><a href="tables_sortable.aspx" title=""><span class="icol-transfer"></span>Sortable and resizable</a></li>
                </ul>
            </li>
            <li><a href="other_calendar.aspx" title="">用户答疑</span></a>
                <ul>
                    <li><a href="other_calendar.aspx" title=""><span class="icol-dcalendar"></span>Calendar</a></li>
                    <li><a href="other_gallery.aspx" title=""><span class="icol-images2"></span>Images gallery</a></li>
                    <li><a href="other_file_manager.aspx" title=""><span class="icol-files"></span>File manager</a></li>
                    <li><a href="#" title="" class="exp"><span class="icol-alert"></span>Error pages <span class="dataNumRed">6</span></a>
                        <ul>
                            <li><a href="other_403.html" title="">403 error</a></li>
                            <li><a href="other_404.html" title="">404 error</a></li>
                            <li><a href="other_405.html" title="">405 error</a></li>
                            <li><a href="other_500.html" title="">500 error</a></li>
                            <li><a href="other_503.html" title="">503 error</a></li>
                            <li><a href="other_offline.html" title="">Website is offline error</a></li>
                        </ul>
                    </li>
                    <li><a href="other_typography.html" title=""><span class="icol-create"></span>Typography</a></li>
                    <li><a href="other_invoice.html" title=""><span class="icol-money2"></span>Invoice template</a></li>
                </ul>
            </li>
        </ul>
    </div>
    
    <!-- Secondary nav -->
    <div class="secNav">
        <div class="secWrapper">
            <div class="secTop">
                <div class="balance">
                    <div class="balInfo">Balance:<span>Apr 21 2012</span></div>
                    <div class="balAmount"><span class="balBars"><!--5,10,15,20,18,16,14,20,15,16,12,10--></span><span>$58,990</span></div>
                </div>
                <a href="#" class="triangle-red"></a>
            </div>
            
            <!-- Tabs container -->
            <div id="tab-container" class="tab-container">
                <ul class="iconsLine ic2 etabs">
                    <li><a href="#general" title=""><span class="icos-fullscreen"></span></a></li>
                    <li><a href="#alt1" title=""><span class="icos-user"></span></a></li>
                </ul>
                
                <div class="divider"><span></span></div>
                
                <div id="general">
                    <ul class="subNav">
                        <li><a href="other_calendar.html" title="" class="this"><span class="icos-dcalendar"></span>Calendar</a></li>
                        <li><a href="other_gallery.html" title=""><span class="icos-images2"></span>Images gallery</a></li>
                        <li><a href="other_file_manager.html" title=""><span class="icos-files"></span>File manager</a></li>
                        <li><a href="#" title="" class="exp"><span class="icos-alert"></span>Error pages <span class="dataNumRed">6</span></a>
                            <ul>
                                <li><a href="other_403.html" title="">403 error</a></li>
                                <li><a href="other_404.html" title="">404 error</a></li>
                                <li><a href="other_405.html" title="">405 error</a></li>
                                <li><a href="other_500.html" title="">500 error</a></li>
                                <li><a href="other_503.html" title="">503 error</a></li>
                                <li><a href="other_offline.html" title="">Website is offline error</a></li>
                            </ul>
                        </li>
                        <li><a href="other_typography.html" title=""><span class="icos-create"></span>Typography</a></li>
                        <li><a href="other_invoice.html" title=""><span class="icos-money2"></span>Invoice template</a></li>
                    </ul>
                </div>
                
                <div id="alt1">
                    <div class="sidePad">
                        <a href="#" title="" class="sideB bBlue">Add new session</a>
                        <a href="#" title="" class="sideB bRed mt10">Add new session</a>
                        <a href="#" title="" class="sideB bGreen mt10">Add new session</a>
                        <a href="#" title="" class="sideB bGreyish mt10">Add new session</a>
                        <a href="#" title="" class="sideB bBrown mt10">Add new session</a>
                    </div>
                </div>
            </div>
            
            <div class="divider"><span></span></div>
        
        	<!-- Sidebar buttons -->
            <div class="fluid sideWidget">
                <div class="grid6"><input type="submit" class="buttonS bRed" value="Cancel" /></div>
                <div class="grid6"><input type="submit" class="buttonS bGreen" value="Submit" /></div>
            </div>
            
            <div class="divider"><span></span></div>
            
            <!-- Sidebar datepicker -->
            <div class="sideWidget">
                <div class="inlinedate"></div>
            </div>
        
            <div class="divider"><span></span></div>
        </div> 
        <div class="clear"></div>
   </div>
</div>
<!-- Sidebar ends -->
   
    
<!-- Content begins -->
<div id="content">
    <div class="contentTop">
        <span class="pageTitle"><span class="icon-calendar"></span>Calendar</span>
        <ul class="quickStats">
            <li>
                <a href="" class="blueImg"><img src="images/icons/quickstats/plus.png" alt="" /></a>
                <div class="floatR"><strong class="blue">5489</strong><span>visits</span></div>
            </li>
            <li>
                <a href="" class="redImg"><img src="images/icons/quickstats/user.png" alt="" /></a>
                <div class="floatR"><strong class="blue">4658</strong><span>users</span></div>
            </li>
            <li>
                <a href="" class="greenImg"><img src="images/icons/quickstats/money.png" alt="" /></a>
                <div class="floatR"><strong class="blue">1289</strong><span>orders</span></div>
            </li>
        </ul>
        <div class="clear"></div>
    </div>
    
    <!-- Breadcrumbs line -->
    <div class="breadLine">
        <div class="bc">
            <ul id="breadcrumbs" class="breadcrumbs">
                <li><a href="index.html">Dashboard</a></li>
                <li><a href="other_calendar.html">Other pages</a>
                    <ul>
                        <li><a href="other_gallery.html" title="">Images gallery</a></li>
                        <li><a href="other_file_manager.html" title="">File manager</a></li>
                        <li><a href="other_404.html" title="">Error page sample</a></li>
                        <li><a href="other_typography.html" title="">Typography</a></li>
                        <li><a href="other_invoice.html" title="">Invoice template</a></li>
                    </ul>
                </li>
                <li class="current"><a href="other_calendar.html" title="">Calendar</a></li>
            </ul>
        </div>
        
        <div class="breadLinks">
            <ul>
                <li><a href="#" title=""><i class="icos-list"></i><span>Orders</span> <strong>(+58)</strong></a></li>
                <li><a href="#" title=""><i class="icos-check"></i><span>Tasks</span> <strong>(+12)</strong></a></li>
                <li class="has">
                    <a title="">
                        <i class="icos-money3"></i>
                        <span>Invoices</span>
                        <span><img src="images/elements/control/hasddArrow.png" alt="" /></span>
                    </a>
                    <ul>
                        <li><a href="#" title=""><span class="icos-add"></span>New invoice</a></li>
                        <li><a href="#" title=""><span class="icos-archive"></span>History</a></li>
                        <li><a href="#" title=""><span class="icos-printer"></span>Print invoices</a></li>
                    </ul>
                </li>
            </ul>
             <div class="clear"></div>
        </div>
    </div>
    
    <!-- Main content -->
    <div class="wrapper">
        <ul class="middleNavR">
            <li><a href="#" title="Add an article" class="tipN"><img src="images/icons/middlenav/create.png" alt="" /></a></li>
            <li><a href="#" title="Upload files" class="tipN"><img src="images/icons/middlenav/upload.png" alt="" /></a></li>
            <li><a href="#" title="Add something" class="tipN"><img src="images/icons/middlenav/add.png" alt="" /></a></li>
            <li><a href="#" title="Messages" class="tipN"><img src="images/icons/middlenav/dialogs.png" alt="" /></a><strong>8</strong></li>
            <li><a href="#" title="Check statistics" class="tipN"><img src="images/icons/middlenav/stats.png" alt="" /></a></li>
        </ul>
    
        <!-- Calendar -->
        <div class="widget">
            <div class="whead"><h6>Calendar</h6><div class="clear"></div></div>
            <div id="calendar"></div>
        </div>
    </div>
    <!-- Main content ends -->
    
</div>
<!-- Content ends -->  
             

</body>
</html>