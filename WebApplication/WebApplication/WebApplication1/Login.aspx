<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>
<%@ Register  TagPrefix="usercontrol" TagName="top" Src="~/UserControl/top.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>水利数值模拟云服务平台</title>
    <link href="css/styles.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.cookie.js" type="text/javascript"></script>
    <style type="text/css" media="all">
        html, body {
            height: 100%;
            margin: 0;
            padding: 0;
            background: url('images/backgrounds/body.jpg') repeat scroll 0% 0% transparent;
        }

        #top {
            position: relative;
            float: right;
            width: 100%;
            text-align: center;
        }

        #floater {
            height: 50%;
            margin-bottom: -150px;
            position: relative;
        }

        #content {
            clear: both;
            height: 300px;
            position: relative;
            margin: 0 auto;
            width: 70%;
            padding: 20px;
        }

        .after {
            text-align: right;
            padding-right: 20px;
            font-size: 0.75em;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            //
            var account = $.cookie('DigitalBasinUserName');

            //$("#PasswordInput").keydown(function (e) {
            //    if (e.keyCode == 13) {
            //        var UserName = $("#UserNameInput").val();
            //        var Password = $("#PasswordInput").val();
            //        $.ajax({
            //            url: 'webForm1.aspx?Method=Login&UserName=' + UserName + '&Password=' + Password,
            //            type: 'POST',
            //            data: { Name: "keyun" },
            //            dataType: 'html',
            //            timeout: 1000,
            //            error: function () {
            //                $("#LoginInfo").html("连接出现错误请重试！");
            //            },
            //            success: function (result) {
            //                if (result == "OK") {
            //                    window.location.replace("index.aspx")
            //                }
            //                else {
            //                    $("#LoginInfo").html("用户名或密码错误！");
            //                }
            //            }
            //        });
            //        //ajax结束
            //    }
            //    //if end

            //});
            //
            document.onkeydown = function (e) {
                var ev = document.all ? window.event : e;
                if (ev.keyCode == 13) {
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
                                window.location.replace("introduce.aspx")
                            }
                            else {
                                $("#LoginInfo").html("用户名或密码错误！");
                            }
                        }
                    });
                    //ajax结束                 
                }
            }
        });
        
       
         
             </script>
</head>

<body>

    <!-- Top line begins -->
    <usercontrol:top runat="server" ID="topForAll" />
    <!-- Top line ends -->

    <div id="floater">
    </div>

    <div id="content">

        <h2>水利数值模拟云服务平台登录</h2>
        <p>

            <div class="formRow" style="text-align: center;" id="UserAndPwd">
                <span class="grid2">
                    <input type="text" name="g8"  placeholder="用户名" style="width: 30%; margin-bottom: 10px;" id="UserNameInput" /></span><div class="clear"></div>
                <span class="grid2">
                    <input type="password" name="g8" placeholder="密码" style="width: 30%; margin-bottom: 10px;" id="PasswordInput" />

                </span>
                <div class="clear"></div>
                <span class="grid2" id="LoginInfo"></span>
            </div>

        </p>
        <p style="color: #858585; font-size: 0.80em;">
            请按照规则输入正确的用户名以及密码 点击<a style="font-size:11px;" href="#">[回车]</a>键登录
        </p>
    </div>

    <p class="after">水利数值模拟云服务平台提供模型云计算服务</p>
    <p class="after">By <a href="#">prewin</a> for <a href="#">水利数值模拟云服务平台</a></p>

</body>

</html>

