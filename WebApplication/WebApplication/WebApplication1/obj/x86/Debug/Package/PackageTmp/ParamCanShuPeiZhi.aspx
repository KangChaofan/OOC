<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParamCanShuPeiZhi.aspx.cs" Inherits="WebApplication1.ParamCanShuPeiZhi" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
<title>水利数值模拟云服务平台</title>
<link href="css/styles.css" rel="stylesheet" type="text/css" />
    <link href="JS/plugins/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
<script src="Scripts/jquery-1.8.0.js" type="text/javascript"></script>
    <script type="text/javascript" src="Scripts/Jquery.Query.js"></script>
    <script type="text/javascript" src="JS/plugins/uploadify/jquery.uploadify.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var ModelIDParam = $.query.get('ModelID');
            //获取sessionStart
            $.ajax({
                url: 'ForSession.aspx?ContentName=ShuiLiuShiJianBuChang',
                type: 'POST',
                dataType: 'html',
                timeout: 10000,
                error: function () {
                     $("#tijiaoInfo").html("提交出现错误1！");
                },
                success: function (result) {
                    if (result == "SetOK") {
                        result = "";
                    }
                    else {
                        $("#BuChang").val(result);
                    }
                }
            });
            $.ajax({
                url: 'ForSession.aspx?ContentName=CanShuPeiZhiWenJian',
                type: 'POST',
                dataType: 'html',
                timeout: 10000,
                error: function () {
                    $("#tijiaoInfo").html("提交出现错误2！");
                },
                success: function (result) {
                    if (result == "SetOK") {
                        result = "";
                    }
                    else {
                        $("#uploadifyNote").html(result);
                    }
                }
            });
            //--------------------------//获取Session_End-------------------------------------------------------------


            //提交按钮Ajax方式传递数据
            $("#tijiaobtn").click(function () {
                if ($("#tijiaoInfo").html() != "已提交！") {
                    var BuChang = $("#BuChang").val();
                    var uploadifyNote = $("#uploadifyNote").html();
                    var IsAllOK = 0;
                    $.ajax({
                        url: 'ForSession.aspx?ContentName=ShuiLiuShiJianBuChang&Value=' + BuChang,
                        type: 'POST',
                        dataType: 'html',
                        timeout: 10000,
                        error: function () {
                            $("#tijiaoInfo").html("提交出现错误！");
                        },
                        success: function (result) {
                            if (result == "SetOK") {
                                IsAllOK++;          
                            }
                        }
                    });
                    $.ajax({
                        url: 'ForSession.aspx?ContentName=CanShuPeiZhiWenJian&Value=' + uploadifyNote,
                        type: 'POST',
                        dataType: 'html',
                        timeout: 10000,
                        error: function () {
   
                            $("#tijiaoInfo").html("提交出现错误！");
                        },
                        success: function (result) {
                            if (result == "SetOK") {
                                $("#tijiaoInfo").html("已提交！");
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
                $("#BuChang").val(null);
                $("#uploadifyNote").html(null);
            });


            $("#uploadify").uploadify({
                'height': '30',
                'swf': 'JS/plugins/uploadify/uploadify.swf',
                'uploader': 'WebForm1.aspx?Method=canshupeizhi&ModelID=' + ModelIDParam,
                'folder': 'UploadFile',
                'width': '120',
                'buttonText': '选择文件',
                'debug': false,
                'auto': true,
                //允许上传的文件后缀
                'fileTypeExts': '*.*',
                //上传文件的大小限制
                'fileSizeLimit': '10MB',
                //上传到服务器，服务器返回相应信息到data里
                'onUploadSuccess': function (file, data, response) {
                    $("#uploadifyNote").html(data);
                }
            });
        });
    </script>
</head>

<body  style="min-height: 50%;">
    <div class="wrapper">
        <form action="" class="main">
            <fieldset>
                <div class="widget fluid">
                    <div class="whead"><h6>参数配置</h6><div class="clear"></div></div>
                    <div class="formRow">
                        <div class="grid3"><label>水流时间步长:</label></div>
                        <div class="grid9"><input type="text" name="regular" id="BuChang" style="width:17%;" />（秒）</div>
                        <div class="clear"></div>
                    </div>

                    <div class="formRow">
                        <div class="grid3"><label>参数配置文件上传：</label></div>
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
                </div>
            </fieldset>
        </form>
    </div>
</body>
</html>
