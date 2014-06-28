<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Single_Model.aspx.cs" Inherits="WebApplication1.Single_Model" %>
<%@ Register  TagPrefix="usercontrol" TagName="MainNav" Src="~/UserControl/MainNav.ascx" %>
<%@ Register  TagPrefix="usercontrol" TagName="subnav" Src="~/UserControl/subNav.ascx" %>
<%@ Register  TagPrefix="usercontrol" TagName="top" Src="~/UserControl/top.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
<title>水利数值模拟云服务平台</title>
<link href="css/styles.css" rel="stylesheet" type="text/css" /> 
    <link href="JS/plugins/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript" src="Scripts/jquery-1.8.0.js"></script>
    <script type="text/javascript" src="Scripts/Jquery.Query.js"></script>
    <script src="Scripts/jquery.cookie.js" type="text/javascript"></script>
    <script src="js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/plugins/ui/jquery.collapsible.min.js"></script>
    <script type="text/javascript" src="JS/plugins/uploadify/jquery.uploadify.js"></script>

<script type="text/javascript">
    $(document).ready(function () {
        //=====Start Collapsible elements management =====//

        $('.exp').collapsible({
            defaultOpen: 'current',
            cookieName: 'navAct',
            cssOpen: 'subOpened',
            cssClose: 'subClosed',
            speed: 200
        });

        $('.opened').collapsible({
            defaultOpen: 'opened,toggleOpened',
            cssOpen: 'inactive',
            cssClose: 'normal',
            speed: 200
        });

        $('.closed').collapsible({
            defaultOpen: '',
            cssOpen: 'inactive',
            cssClose: 'normal',
            speed: 200
        });
        //=====End Collapsible elements management =====//

        $(".subNav").find("a").click(function () {
            var temp = $(this).attr('value');
            $('#contentiframe').attr("src", temp.toString());
            $(".subNav").find("a")._removeClass("this");
            $(".subNav").find("li")._removeClass("this");
            $(this).toggleClass("this");
        });
//        $(".bLightBlue").click(function () {
//            $("#ModelNameForCal").html(
//            "已预备计算</br><span style=\" font-size:14px;color:red;\">" + $(this).attr("id") + "</span>"
//            );
//        });

        ////修改最后完成后的Done
//        function IsCalAvali() {
//            var str = $("#strongCanshupeizhi").html() + $("#strongBianjiekongzhi").html() + $("#strongChushitiaojian").html() + $("#strongShuchukongzhi").html();
//            if (str.toString().length == 16) {
//                $("#strongZaixianjisuan").html("Done");
//            }
//        }

//        $('#zaixianjisuan').click(function () {

//            $.ajax({
//                url: 'runModel.aspx',
//                type: 'POST',
//                data: { Name: "keyun" },
//                dataType: 'html',
//                timeout: 1000,
//                error: function () {
//                    //信息框例一
//                    layer.alert('出现错误，请重试', 5);
//                },
//                beforeSend: function () {
//                    layer.load(3); //5秒后关闭
//                },
//                success: function (result) {
//                    $.layer({
//                        type: 2,
//                        title: false,
//                        iframe: { src: 'LatestResultShow.aspx?ResultLogsID=' + result },
//                        area: ['1000px', '500px'],
//                        success: function () {
//                            layer.shift('bottom', 400)
//                        }
//                    });
//                }
//            });
//        });

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
            <!-- Sidebar subnav -->
         <usercontrol:subnav runat="server"  ID="subNav" />                                       
       </div> 
       <div class="clear"></div>
   </div>
</div>
<!-- Sidebar ends -->    
	
    
<!-- Content begins -->
<div id="content">
<iframe id="contentiframe" src="Single_Model_Content.aspx" width="100%" height="1100"></iframe>
</div>
<!-- Content ends -->    

</body>
</html>
