<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApplication1.index" %>
<%@ Register  TagPrefix="usercontrol" TagName="MainNav" Src="~/UserControl/MainNav.ascx" %>
<%@ Register  TagPrefix="usercontrol" TagName="top" Src="~/UserControl/top.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
<title>水利数值模拟云服务平台</title>
<link href="/css/styles.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7/jquery.min.js"></script>
<script src="Scripts/jquery.cookie.js" type="text/javascript"></script>
<script type="text/javascript" src="js/plugins/forms/ui.spinner.js"></script>
<script type="text/javascript" src="js/plugins/forms/jquery.mousewheel.js"></script>
 <script src="Scripts/jquery.cookie.js" type="text/javascript"></script>
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
<script src="Scripts/jquery.cookie.js" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function () {

        var account = $.cookie('DigitalBasinUserName');
        if (typeof (account) == "undefined" || account == null) {
            $("#UserAndPwd").show();
            $("#Logindiv").show();
            $("#userNameForLogin").html("");
        }
        else {
            $("#userNameForLogin").html("<span>欢迎您：" + $.cookie('DigitalBasinUserName') + "</span>")
            $("#UserAndPwd").hide();
            $("#Logindiv").hide();
        }

        //提交按钮Ajax方式传递数据
        $("#LogOutBtn").click(function () {
            $("#UserAndPwd").show();
            $("#Logindiv").show();
            $("#userNameForLogin").html("");
            $.cookie('DigitalBasinUserName', null);
        });
        $("#LoginBtn").click(function () {
            var UserName = $("#UserNameInput").val();
            var Password = $("#PasswordInput").val();
            $.ajax({
                url: 'webForm1.aspx?Method=Login&UserName=' + UserName + '&Password=' + Password,
                type: 'POST',
                data: { Name: "keyun" },
                dataType: 'html',
                timeout: 1000,
                error: function () {
                    $("#LoginInfo").html("连接出现错误请重试！");
                },
                success: function (result) {
                    if (result == "OK") {
                        $("#UserAndPwd").hide();
                        $("#Logindiv").hide();
                        $("#userNameForLogin").html("<span>欢迎您：" + $.cookie('DigitalBasinUserName') + "</span>")
                    }
                    else {
                        $("#LoginInfo").html("用户名或密码错误！");
                    }
                }
            });

        });

        function current() {
            var d = new Date(), str = '';
            //str += d.getFullYear() + '/'; //获取当前年份 
            str += d.getMonth() + 1 + '月'; //获取当前月份（0——11） 
            str += d.getDate() + '日  ';
            str += d.getHours() + ':';
            str += d.getMinutes() + ':';
            str += d.getSeconds();
            return str;
        }
        setInterval(function () { $("#nowTime").html(current) }, 1000);
    });

</script> 
</head>

<body>

<!-- Style switcher -->
<!-- Top line begins -->
<usercontrol:top runat="server" ID="topForAll" />
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
        <div class="balInfo"><span id="nowTime"></span></div>
        <div class="balUserName" id="userNameForLogin"></div>

  
        <div class="balAmount"><span>积分:58,990</span></div>
    </div>
    <a href="#" class="triangle-red"></a>
</div>
<div class="divider"><span></span></div>   
    <!-- Sidebar buttons -->
      <div class="formRow" style=" text-align:center;" id="UserAndPwd">      
      <span class="grid2"><input type="text" name="g8" value="用户名" style=" width:80%; margin-bottom:5px;"  id="UserNameInput"></span><div class="clear"></div>
      <span class="grid2"><input type="password" name="g8" value="密码" style=" width:80%; margin-bottom:5px;" id="PasswordInput" ></span><div class="clear"></div>
      <span class="grid2" id="LoginInfo"></span><div class="clear"></div>
      </div>
<div class="fluid sideWidget">
    <div class="grid6" id="LogOutdiv"><input type="button" class="buttonS bRed" value="注销"  id="LogOutBtn"/></div>
    <div class="grid6" id="Logindiv"><input type="button" class="buttonS bGreen" value="登陆"  id="LoginBtn"/></div>
</div>
            
            <!-- Tabs container -->
                <div id="general" style=" margin-top:10px;">
                    <ul class="subNav">
                        <li><a href="#" title=""><span class="icos-list"></span>工作日志</a></li>
                        <li><a href="#" title=""><span class="icos-list"></span>信息交流</a></li>
                        <li><a href="#" title=""><span class="icos-list"></span>文件管理</a></li>
                        <li><a href="#" title="" class="exp"><span class="icos-list"></span>资源管理 <span class="dataNumRed">6</span></a>
                                  </li>
                        <li><a href="#" title=""><span class="icos-list"></span>任务管理</a></li>
                        <li><a href="#" title=""><span class="icos-list"></span>数据管理</a></li>
                    </ul>
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
        <span class="pageTitle"><span class="icon-calendar"></span>Welcome to DigitalBasin</span>
        <ul class="quickStats">
            <li>
                <a href="" class="blueImg"><img src="images/icons/quickstats/plus.png" alt="" /></a>
                <div class="floatR"><strong class="blue">5489</strong><span>visits</span></div>
            </li>
            <li>
                <a href="" class="redImg"><img src="images/icons/quickstats/plus.png" alt="" /></a>
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
                <li><a href="index.aspx">首页</a></li>
                <li><a href="#">工作日志</a>
            
                </li>
               
            </ul>
        </div>
        
        <div class="breadLinks">
            <ul>
                <li><a href="#" title=""><i class="icos-list"></i><span>Orders</span> <strong>(+58)</strong></a></li>
                <li><a href="#" title=""><i class="icos-list"></i><span>Tasks</span> <strong>(+12)</strong></a></li>
                <li class="has">
                    <a title="">
                        <i class="icos-list"></i>
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
            <div class="whead"><h6>工作日志</h6><div class="clear"></div></div>
            <div id="calendar"></div>
        </div>
    </div>
    <!-- Main content ends -->
    
</div>
<!-- Content ends -->  
             

</body>
</html>
