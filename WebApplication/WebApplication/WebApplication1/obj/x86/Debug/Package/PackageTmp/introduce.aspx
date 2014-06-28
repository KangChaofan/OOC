<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="introduce.aspx.cs" Inherits="WebApplication1.introduce" %>
<%@ Register  TagPrefix="usercontrol" TagName="MainNav" Src="~/UserControl/MainNav.ascx" %>
<%@ Register  TagPrefix="usercontrol" TagName="subnav" Src="~/UserControl/subNav.ascx" %>
<%@ Register  TagPrefix="usercontrol" TagName="Breadcrumbs" Src="~/UserControl/Breadcrumbs.ascx" %>
<%@ Register  TagPrefix="usercontrol" TagName="top" Src="~/UserControl/top.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
<title>水利数值模拟云服务平台--模型介绍</title>
<link href="css/styles.css" rel="stylesheet" type="text/css" />
<script src="Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
<script src="Scripts/jquery.cookie.js" type="text/javascript"></script>
<script src="js/plugins/layer/layer.min.js" type="text/javascript"></script>
<script type="text/javascript" src="js/plugins/ui/jquery.collapsible.min.js"></script>

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

        //        alert($(".subNav").find("a").html());
        //  alert($(".subNav>div:a").val());
        //$(".subNav>li>a:first").click(function () {
        //alert($(".subNav>li>a:first").val());
        // });
        $(".subNav").find("a").click(function () {
            var temp = $(this).attr('value');

            $('#contentiframe').attr("src", temp.toString());

            $(".subNav").find("a")._removeClass("this");
            $(".subNav").find("li")._removeClass("this");
            $(this).toggleClass("this");
        });

    });
</script>
</head>

<body>



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
<!-- Content begins -->
<div id="content" style=" margin-left:0px;">
<iframe id="contentiframe" src="introduce_Content.aspx" width="100%" height="100%" scrolling="auto"></iframe>
</div>
<!-- Content ends -->    
   
<!-- Content ends -->    
   
        

</body>
</html>
