<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="forms.aspx.cs" Inherits="WebApplication1.forms" %>
<%@ Register  TagPrefix="usercontrol" TagName="MainNav" Src="~/UserControl/MainNav.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
<title>水利数值模拟云服务平台</title>
<link href="css/styles.css" rel="stylesheet" type="text/css" />
    <link href="JS/plugins/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript"  src="Scripts/jquery-1.4.1.js"></script>
    <script type="text/javascript"  src="JS/plugins/uploadify/jquery.uploadify.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //提交按钮Ajax方式传递数据
            $("#tijiaobtn").click(function () {
                if ($("#tijiaoInfo").html() == "") {
                    var ModelName = $("#ModelName").val();
                    var SelectModelType = $("#SelectModelType").val();
                    var ModelDllName = $("#ModelDllName").val();
                    var ResultStartRow = $("#ResultStartRow").val();
                    var uploadifyNote = $("#uploadifyNote").html();                    
                    $.ajax({
                        url: 'webForm1.aspx?Method=ModelInfo&ModelName=' + ModelName + '&SelectModelType=' + SelectModelType + '&ModelDllName=' + ModelDllName + '&ResultStartRow=' + ResultStartRow + '&uploadifyNote=' + uploadifyNote,
                        type: 'POST',
                        data: { Name: "keyun" },
                        dataType: 'html',
                        timeout: 1000,
                        error: function () {
                            $("#tijiaoInfo").html("提交出现错误！");
                        },
                        success: function (result) {
                            if (result == "OK") {
                                $("#tijiaoInfo").html("提交成功！");
                            }
                        }
                    });
                }
                else {
                    $("#tijiaoInfo").html("已提交成功，无需再次提交！");
                }
            });

            //重置清空列表
            $("#tijiaoClear").click(function () {
                $("#tijiaoInfo").html(null);
                $("#ModelName").val(null);
                $("#SelectModelType").get(0).selectedIndex = 0
                $("#ModelDllName").val(null);
                $("#ResultStartRow").val(null);
                $("#uploadifyNote").html(null);
            });

            $("#uploadify").uploadify({
                'height': '30',
                'swf': 'JS/plugins/uploadify/uploadify.swf',
                'uploader': 'WebForm1.aspx?Method=saveOMI',
                'folder': 'UploadFile',
                'width': '120',
                'buttonText': '选择文件',
                'debug': false,
                'auto': true,
                //允许上传的文件后缀
                'fileTypeExts': '*.OMI',
                //上传文件的大小限制
                'fileSizeLimit': '3MB',
                //上传到服务器，服务器返回相应信息到data里
                'onUploadSuccess': function (file, data, response) {
                    $("#uploadifyNote").html(data);
                }
            });
        });
    </script>
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
            <li><a href="forms.aspx" title="" class="exp" id="current">Forms stuff</a>
                <ul>
                    <li><a href="forms.aspx" class="active">新增模型类型</a></li>
                    <li><a href="form_validation.aspx">Validation</a></li>
                    <li><a href="form_editor.aspx">File uploads &amp; editor</a></li>
                    <li><a href="form_wizards.aspx">Form wizards</a></li>
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
                <usercontrol:MainNav runat="server" ID="MainNav" />
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
                    <li><a href="#general" title=""><span class="icos-fullscreen"></span></a></li>
                    <li><a href="#alt1" title=""><span class="icos-user"></span></a></li>
                    <li><a href="#alt2" title=""><span class="icos-archive"></span></a></li>
                </ul>
                
                <div class="divider"><span></span></div>
                
                <div id="general">
                    <ul class="subNav">
                        <li><a href="forms.aspx" title="" class="this"><span class="icos-list"></span>新增运算模型</a></li>
                        <li><a href="form_validation.html" title="" ><span class="icos-alert"></span>Validation</a></li>
                        <li><a href="form_editor.html" title=""><span class="icos-pencil"></span>File uploader &amp; WYSIWYG</a></li>
                        <li><a href="form_wizards.html" title=""><span class="icos-signpost"></span>Form wizards</a></li>
                    </ul>
                </div>
                
                <div id="alt1">
                    <div class="widget">
                        <div class="whead">
                            <h6><span class="icon-tree-view"></span>Simple jQuery file tree</h6>
                            <div class="clear"></div>
                        </div>
                        <div class="filetree"></div>
                    </div>
                    
                    <div class="divider"><span></span></div>
                
                    <div class="sidePad">
                        <a href="#" title="" class="sideB bBlue">Add new session</a>
                        <a href="#" title="" class="sideB bRed mt10">Add new session</a>
                        <a href="#" title="" class="sideB bGreen mt10">Add new session</a>
                        <a href="#" title="" class="sideB bBrown mt10">Add new session</a>
                        <a href="#" title="" class="sideB bGreyish mt10">Add new session</a>
                    </div>
                </div>
                
                <div id="alt2">
                    <div class="sideWidget">
                        <div class="inlinedate"></div>
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
        
        	<!-- Sidebar form -->
            <div class="sideWidget">
                <div class="formRow">
                    <label>XXXX:</label>
                    <input type="text" name="regular" placeholder="Your name" />
                </div>
                <div class="formRow">
                   <label>XXXX:</label>
                    <input type="password" name="regular" placeholder="Your password" /> 
                </div>
                <div class="formRow">
                    <label>Single file uploader:</label>
                    <input type="file" class="fileInput" id="fileInputS" />
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
                    <input type="checkbox" id="check2S" name="chbox1" checked="checked" class="check" />
                    <label for="check2S"  class="nopadding">Checkbox checked</label>
                    <div class="clear"></div>
                </div>
                <div class="formRow">
                    <input type="radio" id="radio1S" name="question1" checked="checked" />
                    <label for="radio1S"  class="nopadding">Usual radio button</label>
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
        
            <div class="divider"><span></span></div>
            
       </div> 
       <div class="clear"></div>
   </div>
</div>
<!-- Sidebar ends -->


<!-- Content begins -->
<div id="content">
    <div class="contentTop">
        <span class="pageTitle"><span class="icon-link"></span>General form elements</span>
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
                <li><a href="forms.html">Forms stuff</a>
                    <ul>
                        <li><a href="form_validation.html" title="">Validation</a></li>
                        <li><a href="form_editor.html" title="">File uploader &amp; WYSIWYG</a></li>
                        <li><a href="form_wizards.html" title="">Form wizards</a></li>
                    </ul>
                </li>
                <li class="current"><a href="forms.html" title="">Inputs &amp; elements</a></li>
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
        <form action="" class="main">
            <fieldset>
                <div class="widget fluid">
                    <div class="whead"><h6>新增运算模型</h6><div class="clear"></div></div>
                    <div class="formRow">
                        <div class="grid3"><label>模型名称:</label></div>
                        <div class="grid9"><input type="text" name="regular" id="ModelName" /></div>
                        <div class="clear"></div>
                    </div>
                    <div class="formRow">
                        <div class="grid3"><label>模型类型:</label></div>
                        <div class="grid9">
                         <select name="select2"  id="SelectModelType">
                                <option value="opt1">尚未选择</option>
                                <asp:Literal ID="modelTypeListOption" runat="server"></asp:Literal>                                 
                            </select>
                        </div>
                        <div class="clear"></div>
                    </div>
                    <div class="formRow">
                        <div class="grid3"><label>模型DLL名称：</label></div>
                        <div class="grid9"><input type="text" name="regular" id="ModelDllName" /></div>
                        <div class="clear"></div>
                    </div>
            
                    <div class="formRow">
                        <div class="grid3"><label>数据起始行：</label></div>
                        <div class="grid9"><input type="text" name="regular"  id="ResultStartRow"/></div>
                        <div class="clear"></div>
                    </div>
                    <div class="formRow">
                        <div class="grid3"><label>OMI文件上传：</label></div>
                        <div class="grid9">     <div id="fileQueue"></div>
    <input type="file" name="uploadify" id="uploadify" />
    <span class="note" id="uploadifyNote"></span></div>
                        <div class="clear"></div>
                    </div>
                    <div class="formRow">
                        <div class="grid3"><label></label></div>
                        <div class="grid9"><input type="button" class="buttonS bLightBlue" id="tijiaobtn" value="提交" />   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <input type="button" class="buttonS bRed" value="重置" id="tijiaoClear" />
                         <span class="note" id="tijiaoInfo"></span>
                        </div>
                        <div class="clear"></div>
                    </div>
                    <div class="formRow">
                        <div class="grid3"><label>Predefined value:</label></div>
                        <div class="grid9"><input type="text" name="regular" value="http://" /></div>
                        <div class="clear"></div>
                    </div>
                    <div class="formRow">
                        <div class="grid3"><label>Field with tooltip:</label></div>
                        <div class="grid9"><input type="text" name="regular" title="Tooltip in different directions" class="tipS" /></div>
                        <div class="clear"></div>
                    </div>
                    <div class="formRow">
                        <div class="grid3"><label>Max 10 characters:</label></div>
                        <div class="grid9"><input type="text" name="regular" maxlength="10" placeholder="Max 10 characters" /></div>
                        <div class="clear"></div>
                    </div>
                    <div class="formRow">
                        <div class="grid3"><label><span class="icos-user"></span>With icons:</label></div>
                        <div class="grid9">
                            <input type="text" name="regular" />
                            <img src="images/icons/usual/icon-download.png" alt="" class="fieldIcon" />
                        </div>
                        <div class="clear"></div>
                    </div>
                    <div class="formRow">
                        <div class="grid3"><label for="labelfor">Clickable label:</label></div>
                        <div class="grid9"><input type="text" name="labelfor" id="labelfor" /></div>
                        <div class="clear"></div>
                    </div>
                    <div class="formRow">
                        <div class="grid3"><label>Textarea:</label></div>
                        <div class="grid9"><textarea rows="8" cols="" name="textarea"></textarea> </div>
                        <div class="clear"></div>
                    </div>
                    <div class="formRow">
                        <div class="grid3"><label>Elastic textarea:</label></div>
                        <div class="grid9"><textarea rows="8" cols="" name="textarea" class="auto"></textarea></div>
                        <div class="clear"></div>
                    </div>
                    <div class="formRow">
                        <div class="grid3"><label>With character counter:</label></div>
                        <div class="grid9">
                            <textarea rows="8" cols="" name="textarea" class="auto lim"></textarea>
                            <span class="note" id="limitingtext">Field limited to 100 characters.</span>
                        </div>
                        <div class="clear"></div>
                    </div>
                    <div class="formRow">
                        <div class="grid3"><label>Tags:</label></div>
                        <div class="grid9"><input type="text" id="tags" name="tags" class="tags" value="these,are,sample,tags" /></div>
                        <div class="clear"></div>
                    </div>
                </div>
            </fieldset>                        

        </form>
    </div>
</div>
<!-- Content ends -->  
        

</body>
</html>
