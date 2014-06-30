<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="subNav.ascx.cs" Inherits="WebApplication1.UserControl.subNav" %>

<script type="text/javascript">
    $(document).ready(function () {

        var account = $.cookie('DigitalBasinUserName');
        if (typeof (account) == "undefined" || account == null) {      
            $("#userNameForLogin").html("");
        }
        else {
            $("#userNameForLogin").html("<span>欢迎您：" + $.cookie('DigitalBasinUserName') + "</span>")
            $("#UserAndPwd").hide();   
        }
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

  
        <div class="balAmount"><span style="font-size:12px;">积分:58,990</span></div>
    </div>
    <a href="#" class="triangle-red"></a>
</div>
 
<asp:Literal ID="LitModelType" runat="server">
</asp:Literal>
  <div class="divider"><span></span></div> 
