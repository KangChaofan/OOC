﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="messages.aspx.cs" Inherits="WebApplication1.messages" %>

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
    	<a href="index.aspx" title="" class="logo"><img src="images/logo.png" alt="" /></a>
        
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
            <li><a href="forms.aspx" title="" class="exp">Forms stuff</a>
                <ul>
                    <li><a href="forms.aspx">新增运算模型</a></li>
                    <li><a href="form_validation.html">Validation</a></li>
                    <li><a href="form_editor.html">File uploads &amp; editor</a></li>
                    <li><a href="form_wizards.html">Form wizards</a></li>
                </ul>
            </li>
            <li><a href="messages.html" title="" class="active">Messages</a></li>
            <li><a href="statistics.html" title="">Statistics</a></li>
            <li><a href="tables.html" title="" class="exp">Tables</a>
                <ul>
                    <li><a href="tables.html">Standard tables</a></li>
                    <li><a href="tables_dynamic.html">Dynamic tables</a></li>
                    <li><a href="tables_control.html">Tables with control</a></li>
                    <li><a href="tables_sortable.html">Sortable &amp; resizable</a></li>
                </ul>
            </li>
            <li><a href="other_calendar.html" title="" class="exp">Other pages</a>
                <ul>
                    <li><a href="other_calendar.html">Calendar</a></li>
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
                <ul class="iconsLine ic3 etabs">
                    <li><a href="#general" title=""><span class="icos-user"></span></a></li>
                    <li><a href="#stuff" title=""><span class="icos-list"></span></a></li>
                    <li><a href="#allusers" title=""><span class="icos-users"></span></a></li>
                </ul>
                
                <div class="divider"><span></span></div>
                
                <div id="general">
                    <ul class="userList">
                        <li>
                            <a href="#" title="">
                                <img src="images/live/face1.png" alt="" />
                                <span class="contactName">
                                    <strong>Eugene Kopyov <span>(5)</span></strong>
                                    <i>web &amp; ui designer</i>
                                </span>
                                <span class="status_away"></span>
                                <span class="clear"></span>
                            </a>
                        </li>
                        <li>
                            <a href="#" title="">
                                <img src="images/live/face2.png" alt="" />
                                <span class="contactName">
                                    <strong>Lucy Wilkinson <span>(12)</span></strong>
                                    <i>Team leader</i>
                                </span>
                                <span class="status_off"></span>
                                <span class="clear"></span>
                            </a>
                        </li>
                        <li>
                            <a href="#" title="">
                                <img src="images/live/face3.png" alt="" />
                                <span class="contactName">
                                    <strong>John Dow</strong>
                                    <i>PHP developer</i>
                                </span>
                                <span class="status_available"></span>
                                <span class="clear"></span>
                            </a>
                        </li>
                        <li class="this">
                            <a href="#" title="">
                                <img src="images/live/face4.png" alt="" />
                                <span class="contactName">
                                    <strong>The Incredible</strong>
                                    <i>web &amp; ui designer</i>
                                </span>
                                <span class="status_available"></span>
                                <span class="clear"></span>
                            </a>
                        </li>
                        <li>
                            <a href="#" title="">
                                <img src="images/live/face5.png" alt="" />
                                <span class="contactName">
                                    <strong>The wazzup guy</strong>
                                    <i>web &amp; ui designer</i>
                                </span>
                                <span class="status_available"></span>
                                <span class="clear"></span>
                            </a>
                        </li>
                        <li>
                            <a href="#" title="">
                                <img src="images/live/face6.png" alt="" />
                                <span class="contactName">
                                    <strong>Viktor Fedorovich</strong>
                                    <i>web &amp; ui designer</i>
                                </span>
                                <span class="status_available"></span>
                                <span class="clear"></span>
                            </a>
                        </li>
                    </ul>
                    <div class="clear"></div>
                </div>
                
                <div id="stuff">
                    <div class="sidePad">
                        <a href="#" title="" class="sideB bBlue">Add new session</a>
                        <a href="#" title="" class="sideB bRed mt10">Add new session</a>
                        <a href="#" title="" class="sideB bGreen mt10">Add new session</a>
                        <a href="#" title="" class="sideB bBrown mt10">Add new session</a>
                        <a href="#" title="" class="sideB bGreyish mt10">Add new session</a>
                    </div>
                    
                    <div class="divider"><span></span></div>
                
                	<!-- Sidebar forms -->
                    <div class="sideWidget">
                        <div class="formRow">
                            <label>Usual input field:</label>
                            <input type="text" name="regular" placeholder="Your name" />
                        </div>
                        <div class="formRow">
                           <label>Usual password field:</label>
                            <input type="password" name="regular" placeholder="Your password" /> 
                        </div>
                        <div class="formRow">
                            <label>Single file uploader:</label>
                            <input type="file" class="fileInput" id="fileInput" />
                        </div>
                        <div class="formRow">
                            <label>Dropdown menu:</label>
                            <select name="select2" >
                                <option value="opt1">Usual select box</option>
                                <option value="opt2">Option 2</option>
                                <option value="opt3">Option 3</option>
                                <option value="opt4">Option 4</option>
                                <option value="opt5">Option 5</option>
                                <option value="opt6">Option 6</option>
                                <option value="opt7">Option 7</option>
                                <option value="opt8">Option 8</option>
                            </select>
                        </div>
                        
                        <div class="formRow searchDrop">
                            <label>Dropdown with search:</label>
                            <select data-placeholder="Choose a Country..." class="select" tabindex="2">
                                <option value=""></option> 
                                <option value="Cambodia">Cambodia</option> 
                                <option value="Cameroon">Cameroon</option> 
                                <option value="Canada">Canada</option> 
                                <option value="Cape Verde">Cape Verde</option> 
                                <option value="Cayman Islands">Cayman Islands</option> 
                                <option value="Central African Republic">Central African Republic</option> 
                                <option value="Chad">Chad</option> 
                                <option value="Chile">Chile</option> 
                                <option value="China">China</option> 
                                <option value="Christmas Island">Christmas Island</option> 
                                <option value="Cocos (Keeling) Islands">Cocos (Keeling) Islands</option> 
                                <option value="Colombia">Colombia</option> 
                                <option value="Comoros">Comoros</option> 
                                <option value="Congo">Congo</option> 
                                <option value="Congo, The Democratic Republic of The">Congo, The Democratic Republic of The</option> 
                                <option value="Cook Islands">Cook Islands</option> 
                                <option value="Costa Rica">Costa Rica</option> 
                                <option value="Cote D'ivoire">Cote D'ivoire</option> 
                                <option value="Croatia">Croatia</option> 
                                <option value="Cuba">Cuba</option> 
                                <option value="Cyprus">Cyprus</option> 
                                <option value="Czech Republic">Czech Republic</option> 
                                <option value="Denmark">Denmark</option> 
                                <option value="Djibouti">Djibouti</option> 
                                <option value="Dominica">Dominica</option> 
                                <option value="Dominican Republic">Dominican Republic</option> 
                                <option value="Ecuador">Ecuador</option> 
                                <option value="Egypt">Egypt</option> 
                                <option value="El Salvador">El Salvador</option> 
                                <option value="Equatorial Guinea">Equatorial Guinea</option> 
                                <option value="Eritrea">Eritrea</option> 
                                <option value="Estonia">Estonia</option> 
                                <option value="Ethiopia">Ethiopia</option> 
                                <option value="Falkland Islands (Malvinas)">Falkland Islands (Malvinas)</option> 
                                <option value="Faroe Islands">Faroe Islands</option> 
                                <option value="Fiji">Fiji</option> 
                                <option value="Finland">Finland</option> 
                                <option value="France">France</option> 
                                <option value="French Guiana">French Guiana</option> 
                                <option value="French Polynesia">French Polynesia</option> 
                                <option value="French Southern Territories">French Southern Territories</option> 
                                <option value="Gabon">Gabon</option> 
                                <option value="Gambia">Gambia</option> 
                                <option value="Georgia">Georgia</option> 
                                <option value="Germany">Germany</option> 
                                <option value="Ghana">Ghana</option> 
                                <option value="Gibraltar">Gibraltar</option> 
                                <option value="Greece">Greece</option> 
                            </select>
                        </div>
                    
                        <div class="formRow">
                            <input type="checkbox" id="check2" name="chbox1" checked="checked" class="check" />
                            <label for="check2"  class="nopadding">Checkbox checked</label>
                            <div class="clear"></div>
                        </div>
                        <div class="formRow">
                            <input type="radio" id="radio1" name="question1" checked="checked" />
                            <label for="radio1"  class="nopadding">Usual radio button</label>
                            <div class="clear"></div>
                        </div>
                        <div class="formRow">
                            <label>Usual textarea:</label>
                            <textarea rows="8" cols="" name="textarea" placeholder="Your message"></textarea>
                        </div>
                        <div class="formRow">
                            <input type="submit" class="buttonS bLightBlue" value="Submit button" />
                        </div>
                    </div>
                </div>
                
                <div id="allusers">
                    <ul class="userList">
                        <li>
                            <a href="#" title="">
                                <img src="images/live/face1.png" alt="" />
                                <span class="contactName">
                                    <strong>Eugene Kopyov <span>(5)</span></strong>
                                    <i>web &amp; ui designer</i>
                                </span>
                                <span class="status_away"></span>
                                <span class="clear"></span>
                            </a>
                        </li>
                        <li>
                            <a href="#" title="">
                                <img src="images/live/face2.png" alt="" />
                                <span class="contactName">
                                    <strong>Lucy Wilkinson <span>(12)</span></strong>
                                    <i>Team leader</i>
                                </span>
                                <span class="status_off"></span>
                                <span class="clear"></span>
                            </a>
                        </li>
                        <li>
                            <a href="#" title="">
                                <img src="images/live/face3.png" alt="" />
                                <span class="contactName">
                                    <strong>John Dow</strong>
                                    <i>PHP developer</i>
                                </span>
                                <span class="status_available"></span>
                                <span class="clear"></span>
                            </a>
                        </li>
                        <li>
                            <a href="#" title="">
                                <img src="images/live/face4.png" alt="" />
                                <span class="contactName">
                                    <strong>The Incredible</strong>
                                    <i>web &amp; ui designer</i>
                                </span>
                                <span class="status_available"></span>
                                <span class="clear"></span>
                            </a>
                        </li>
                        <li>
                            <a href="#" title="">
                                <img src="images/live/face5.png" alt="" />
                                <span class="contactName">
                                    <strong>The wazzup guy</strong>
                                    <i>web &amp; ui designer</i>
                                </span>
                                <span class="status_available"></span>
                                <span class="clear"></span>
                            </a>
                        </li>
                        <li>
                            <a href="#" title="">
                                <img src="images/live/face6.png" alt="" />
                                <span class="contactName">
                                    <strong>Viktor Fedorovich</strong>
                                    <i>web &amp; ui designer</i>
                                </span>
                                <span class="status_available"></span>
                                <span class="clear"></span>
                            </a>
                        </li>
                        <li>
                            <a href="#" title="">
                                <img src="images/live/face1.png" alt="" />
                                <span class="contactName">
                                    <strong>Eugene Kopyov <span>(5)</span></strong>
                                    <i>web &amp; ui designer</i>
                                </span>
                                <span class="status_away"></span>
                                <span class="clear"></span>
                            </a>
                        </li>
                        <li>
                            <a href="#" title="">
                                <img src="images/live/face2.png" alt="" />
                                <span class="contactName">
                                    <strong>Lucy Wilkinson <span>(12)</span></strong>
                                    <i>Team leader</i>
                                </span>
                                <span class="status_off"></span>
                                <span class="clear"></span>
                            </a>
                        </li>
                        <li>
                            <a href="#" title="">
                                <img src="images/live/face3.png" alt="" />
                                <span class="contactName">
                                    <strong>John Dow</strong>
                                    <i>PHP developer</i>
                                </span>
                                <span class="status_available"></span>
                                <span class="clear"></span>
                            </a>
                        </li>
                        <li class="this">
                            <a href="#" title="">
                                <img src="images/live/face4.png" alt="" />
                                <span class="contactName">
                                    <strong>The Incredible</strong>
                                    <i>web &amp; ui designer</i>
                                </span>
                                <span class="status_available"></span>
                                <span class="clear"></span>
                            </a>
                        </li>
                        <li>
                            <a href="#" title="">
                                <img src="images/live/face5.png" alt="" />
                                <span class="contactName">
                                    <strong>The wazzup guy</strong>
                                    <i>web &amp; ui designer</i>
                                </span>
                                <span class="status_available"></span>
                                <span class="clear"></span>
                            </a>
                        </li>
                        <li>
                            <a href="#" title="">
                                <img src="images/live/face6.png" alt="" />
                                <span class="contactName">
                                    <strong>Viktor Fedorovich</strong>
                                    <i>web &amp; ui designer</i>
                                </span>
                                <span class="status_available"></span>
                                <span class="clear"></span>
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
            
             <div class="divider"><span></span></div>
            
            <!-- Sidebar datepicker -->
            <div class="sideWidget">
                <div class="inlinedate"></div>
            </div>
            
            <div class="divider"><span></span></div>
            
            <!-- Sidebar tags list -->
            <div class="formRow">
                <input type="text" id="tags" name="tags" class="tags" value="these,are,sample,tags" />
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
        <span class="pageTitle"><span class="icon-chat-2"></span>Messages layout</span>
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
                <li class="current"><a href="messages.html" title="">Messages</a></li>
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
            <li><a href="#" title="添加模型" class="tipN" id="AddModel"><img src="images/icons/middlenav/add.png" alt="" /></a></li>
            <li><a href="#" title="添加模型" class="tipN"><img src="images/icons/middlenav/create.png" alt="" /></a></li>
            <li><a href="#" title="Upload files" class="tipN"><img src="images/icons/middlenav/upload.png" alt="" /></a></li>        
            <li><a href="#" title="Messages" class="tipN"><img src="images/icons/middlenav/dialogs.png" alt="" /></a><strong>8</strong></li>
            <li><a href="#" title="Check statistics" class="tipN"><img src="images/icons/middlenav/stats.png" alt="" /></a></li>
        </ul>
    
    	<!-- Messages #1 -->
        <div class="widget">
            <div class="whead">
                <h6>Messages layout #1</h6>
                <div class="on_off">
                    <span class="icon-reload-CW"></span>
                    <input type="checkbox" id="check1" checked="checked" name="chbox" />
                </div>            
                <div class="clear"></div>
            </div>
            
            <ul class="messagesOne">
                <li class="by_user">
                    <a href="#" title=""><img src="images/live/face1.png" alt="" /></a>
                    <div class="messageArea">
                        <span class="aro"></span>
                        <div class="infoRow">
                            <span class="name"><strong>John</strong> says:</span>
                            <span class="time">3 hours ago</span>
                            <div class="clear"></div>
                        </div>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vel est enim, vel eleifend felis. Ut volutpat, leo eget euismod scelerisque, eros purus lacinia velit, nec rhoncus mi dui eleifend orci. 
                        Phasellus ut sem urna, id congue libero. Nulla eget arcu vel massa suscipit ultricies ac id velit
                    </div>
                    <div class="clear"></div>
                </li>
            
                <li class="divider"><span></span></li>
            
                <li class="by_me">
                    <a href="#" title=""><img src="images/live/face2.png" alt="" /></a>
                    <div class="messageArea">
                        <span class="aro"></span>
                        <div class="infoRow">
                            <span class="name"><strong>Eugene</strong> says:</span>
                            <span class="time">3 hours ago</span>
                            <div class="clear"></div>
                        </div>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vel est enim, vel eleifend felis. Ut volutpat, leo eget euismod scelerisque, eros purus lacinia velit, nec rhoncus mi dui eleifend orci. 
                        Phasellus ut sem urna, id congue libero. Nulla eget arcu vel massa suscipit ultricies ac id velit
                    </div>
                    <div class="clear"></div>
                </li>
            
                <li class="by_me">
                    <a href="#" title=""><img src="images/live/face2.png" alt="" /></a>
                    <div class="messageArea">
                        <span class="aro"></span>
                        <div class="infoRow">
                            <span class="name"><strong>Eugene</strong> says:</span>
                            <span class="time">3 hours ago</span>
                            <div class="clear"></div>
                        </div>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vel est enim, vel eleifend felis. Ut volutpat, leo eget euismod scelerisque, eros purus lacinia velit, nec rhoncus mi dui eleifend orci. 
                        Phasellus ut sem urna, id congue libero. Nulla eget arcu vel massa suscipit ultricies ac id velit
                    </div>
                    <div class="clear"></div>
                </li>
                
                <li class="divider"><span></span></li>
            
                <li class="by_user">
                    <a href="#" title=""><img src="images/live/face1.png" alt="" /></a>
                    <div class="messageArea">
                        <span class="aro"></span>
                        <div class="infoRow">
                            <span class="name"><strong>John</strong> says:</span>
                            <span class="time">3 hours ago</span>
                            <div class="clear"></div>
                        </div>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vel est enim, vel eleifend felis. Ut volutpat, leo eget euismod scelerisque, eros purus lacinia velit, nec rhoncus mi dui eleifend orci. 
                        Phasellus ut sem urna, id congue libero. Nulla eget arcu vel massa suscipit ultricies ac id velit
                    </div>
                    <div class="clear"></div>
                </li>
                
                <li class="divider"><span></span></li>
            
                <li class="by_me">
                    <a href="#" title=""><img src="images/live/face2.png" alt="" /></a>
                    <div class="messageArea">
                        <span class="aro"></span>
                        <div class="infoRow">
                            <span class="name"><strong>Eugene</strong> says:</span>
                            <span class="time">3 hours ago</span>
                            <div class="clear"></div>
                        </div>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vel est enim, vel eleifend felis. Ut volutpat, leo eget euismod scelerisque, eros purus lacinia velit, nec rhoncus mi dui eleifend orci. 
                        Phasellus ut sem urna, id congue libero. Nulla eget arcu vel massa suscipit ultricies ac id velit
                    </div>
                    <div class="clear"></div>
                </li>
            </ul>
        </div>
        
        <!-- Enter message field -->
        <div class="enterMessage">
            <input type="text" name="enterMessage" placeholder="Enter your message..." />
            <div class="sendBtn">
                <a href="#" title="" class="attachPhoto"></a>
                <a href="#" title="" class="attachLink"></a>
                <input type="submit" name="sendMessage" class="buttonS bLightBlue" value="Send" />
            </div>
        </div>
         <div class="enterMessage" id="NewModel">
            <input type="text" name="enterMessage" placeholder="添加模型名称" />
            <input type="text" name="enterMessage" placeholder="选择模型类型" />
            <div class="sendBtn">
                <input type="submit" name="sendMessage" class="buttonS bLightBlue" value="添加" />
            </div>
        </div>
        <div class="divider"><span></span></div>
            
            
        <!-- Messages #2 -->
        <div class="widget">
            <div class="whead">
                <h6>Messages layout #2</h6>
                <div class="on_off">
                    <span class="icon-reload-CW"></span>
                    <input type="checkbox" name="chbox" />
                </div>            
                <div class="clear"></div>
            </div>
            
            <ul class="messagesTwo">
                <li class="by_user">
                    <a href="#" title=""><img src="images/live/face1.png" alt="" /></a>
                    <div class="messageArea">
                        <div class="infoRow">
                            <span class="name"><strong>Jonathan</strong> says:</span>
                            <span class="time">3 hours ago</span>
                            <div class="clear"></div>
                        </div>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vel est enim, vel eleifend felis. Ut volutpat, leo eget euismod scelerisque, eros purus lacinia velit, nec rhoncus mi dui eleifend orci. 
                        Phasellus ut sem urna, id congue libero. Nulla eget arcu vel massa suscipit ultricies ac id velit
                    </div>
                    <div class="clear"></div>
                </li>
            
                <li class="by_me">
                    <a href="#" title=""><img src="images/live/face2.png" alt="" /></a>
                    <div class="messageArea">
                        <div class="infoRow">
                            <span class="name"><strong>Eugene</strong> says:</span>
                            <span class="time">3 hours ago</span>
                            <div class="clear"></div>
                        </div>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vel est enim, vel eleifend felis. Ut volutpat, leo eget euismod scelerisque, eros purus lacinia velit, nec rhoncus mi dui eleifend orci. 
                        Phasellus ut sem urna, id congue libero. Nulla eget arcu vel massa suscipit ultricies ac id velit
                    </div>
                    <div class="clear"></div>
                </li>
            
                <li class="by_me">
                    <a href="#" title=""><img src="images/live/face2.png" alt="" /></a>
                    <div class="messageArea">
                        <div class="infoRow">
                            <span class="name"><strong>Eugene</strong> says:</span>
                            <span class="time">3 hours ago</span>
                            <div class="clear"></div>
                        </div>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vel est enim, vel eleifend felis. Ut volutpat, leo eget euismod scelerisque, eros purus lacinia velit, nec rhoncus mi dui eleifend orci. 
                        Phasellus ut sem urna, id congue libero. Nulla eget arcu vel massa suscipit ultricies ac id velit
                    </div>
                    <div class="clear"></div>
                </li>
            
                <li class="by_user">
                    <a href="#" title=""><img src="images/live/face1.png" alt="" /></a>
                    <div class="messageArea">
                        <div class="infoRow">
                            <span class="name"><strong>Jonathan</strong> says:</span>
                            <span class="time">3 hours ago</span>
                            <div class="clear"></div>
                        </div>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vel est enim, vel eleifend felis. Ut volutpat, leo eget euismod scelerisque, eros purus lacinia velit, nec rhoncus mi dui eleifend orci. 
                        Phasellus ut sem urna, id congue libero. Nulla eget arcu vel massa suscipit ultricies ac id velit
                    </div>
                    <div class="clear"></div>
                </li>
            
                <li class="by_me">
                    <a href="#" title=""><img src="images/live/face2.png" alt="" /></a>
                    <div class="messageArea">
                        <div class="infoRow">
                            <span class="name"><strong>Eugene</strong> says:</span>
                            <span class="time">3 hours ago</span>
                            <div class="clear"></div>
                        </div>
                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam vel est enim, vel eleifend felis. Ut volutpat, leo eget euismod scelerisque, eros purus lacinia velit, nec rhoncus mi dui eleifend orci. 
                        Phasellus ut sem urna, id congue libero. Nulla eget arcu vel massa suscipit ultricies ac id velit
                    </div>
                    <div class="clear"></div>
                </li>
            </ul>
        </div>
        
        <!-- Enter messages field -->
        <div class="enterMessage">
            <input type="text" name="enterMessage" placeholder="Enter your message..." />
            <div class="sendBtn">
                <a href="#" title="" class="attachPhoto"></a>
                <a href="#" title="" class="attachLink"></a>
                <input type="submit" name="sendMessage" class="buttonS bLightBlue" value="Send" />
            </div>
        </div>
    </div>
    <!-- Main content ends -->

</div>
<!-- Content ends -->


</body>
</html>
