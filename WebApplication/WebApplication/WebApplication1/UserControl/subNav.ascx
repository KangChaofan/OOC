<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="subNav.ascx.cs" Inherits="WebApplication1.UserControl.subNav" %>

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
<div class="divider"><span></span></div>   
<asp:Literal ID="LitModelType" runat="server">
</asp:Literal>
  <div class="divider"><span></span></div> 
