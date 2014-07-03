<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParamGuoChengKongZhi.aspx.cs"
    Inherits="WebApplication1.ParamGuoChengKongZhi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <title>水利数值模拟云服务平台</title>
    <link href="css/styles.css" rel="stylesheet" type="text/css" />
    <link href="JS/plugins/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/jquery-1.4.1.js"></script>
    <script type="text/javascript" src="Scripts/Jquery.Query.js"></script>
    <script type="text/javascript" src="JS/plugins/uploadify/jquery.uploadify.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var ModelIDParam = $.query.get('ModelID');

            $.ajax({
                url: 'ForSession.aspx?ContentName=HeDaoYinShuiLiuLiang',
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
                        $("#uploadifyNote").html(result);
                    }
                }
            });

            $.ajax({
                url: 'ForSession.aspx?ContentName=DongPingHuFenShui',
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
                        $("#uploadifyNoteDongpinghufenshui").html(result);
                    }
                }
            });



            $.ajax({
                url: 'ForSession.aspx?ContentName=Tandixushui',
                type: 'POST',
                dataType: 'html',
                timeout: 10000,
                error: function () {
                    $("#tijiaoInfo").html("提交出现错误3！");
                },
                success: function (result) {
                    if (result == "SetOK") {
                        result = "";
                    }
                    else {
                        $("#uploadifyNoteTandixushui").html(result);
                    }
                }
            });


            $.ajax({
                url: 'ForSession.aspx?ContentName=Yiluoheruhuang',
                type: 'POST',
                dataType: 'html',
                timeout: 10000,
                error: function () {
                    $("#tijiaoInfo").html("提交出现错误4！");
                },
                success: function (result) {
                    if (result == "SetOK") {
                        result = "";
                    }
                    else {
                        $("#uploadifyNoteYiluoheruhuang").html(result);
                    }
                }
            });




            $.ajax({
                url: 'ForSession.aspx?ContentName=Qinheruhuang',
                type: 'POST',
                dataType: 'html',
                timeout: 10000,
                error: function () {
                    $("#tijiaoInfo").html("提交出现错误5！");
                },
                success: function (result) {
                    if (result == "SetOK") {
                        result = "";
                    }
                    else {
                        $("#uploadifyNoteQinheruhuang").html(result);
                    }
                }
            });








            //提交按钮Ajax方式传递数据
            $("#tijiaobtn").click(function () {
                var uploadifyNote = $("#uploadifyNote").html();
                var uploadifyNoteDongpinghufenshui = $("#uploadifyNoteDongpinghufenshui").html();
                var uploadifyNoteTandixushui = $("#uploadifyNoteTandixushui").html();
                var uploadifyNoteYiluoheruhuang = $("#uploadifyNoteYiluoheruhuang").html();
                var uploadifyNoteQinheruhuang = $("#uploadifyNoteQinheruhuang").html();
                var ModelName = $("#ModelName").val();
                var IsAllOK = 0;
                if ($("#tijiaoInfo").html() != "已提交！") {
                         $.ajax({
                        url: 'ForSession.aspx?ContentName=HeDaoYinShuiLiuLiang&Value=' + uploadifyNote,
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
                        url: 'ForSession.aspx?ContentName=DongPingHuFenShui&Value=' + uploadifyNoteDongpinghufenshui,
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
                        url: 'ForSession.aspx?ContentName=Tandixushui&Value=' + uploadifyNoteTandixushui,
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
                        url: 'ForSession.aspx?ContentName=Yiluoheruhuang&Value=' + uploadifyNoteYiluoheruhuang,
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
                        url: 'ForSession.aspx?ContentName=Qinheruhuang&Value=' + uploadifyNoteQinheruhuang,
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
                }
                else {
                    $("#tijiaoInfo").html("已提交成功，无需再次提交！");
                }
                //----------------------
           });
          

            //重置清空列表
            $("#tijiaoClear").click(function () {
                $("#tijiaoInfo").html(null);
                $("#uploadifyNote").html(null);
                $("#uploadifyNoteDongpinghufenshui").html(null);
                $("#uploadifyNoteTandixushui").html(null);
                $("#uploadifyNoteYiluoheruhuang").html(null);
                $("#uploadifyNoteQinheruhuang").html(null);

            });

            //$("input[name^='uploadify']").uploadify({
            $("#uploadify").uploadify({
                'height': '30',
                'swf': 'JS/plugins/uploadify/uploadify.swf',
                'uploader': 'WebForm1.aspx?Method=guochengkongzhi&ModelID=' + ModelIDParam,
                'folder': 'UploadFile',
                'width': '120',
                'buttonText': '选择文件',
                'debug': false,
                'auto': true,
                //允许上传的文件后缀
                'fileTypeExts': '*.*',
                //上传文件的大小限制
                'fileSizeLimit': '100MB',
                //上传到服务器，服务器返回相应信息到data里
                'onUploadSuccess': function (file, data, response) {
                    $("#uploadifyNote").html(data);
                }
            });
            $("#uploadifyDongpinghufenshui").uploadify({
                'height': '30',
                'swf': 'JS/plugins/uploadify/uploadify.swf',
                'uploader': 'WebForm1.aspx?Method=guochengkongzhi&ModelID=' + ModelIDParam,
                'folder': 'UploadFile',
                'width': '120',
                'buttonText': '选择文件',
                'debug': false,
                'auto': true,
                //允许上传的文件后缀
                'fileTypeExts': '*.*',
                //上传文件的大小限制
                'fileSizeLimit': '100MB',
                //上传到服务器，服务器返回相应信息到data里
                'onUploadSuccess': function (file, data, response) {
                    $("#uploadifyNoteDongpinghufenshui").html(data);
                }
            });
            $("#uploadifyTandixushui").uploadify({
                'height': '30',
                'swf': 'JS/plugins/uploadify/uploadify.swf',
                'uploader': 'WebForm1.aspx?Method=guochengkongzhi&ModelID=' + ModelIDParam,
                'folder': 'UploadFile',
                'width': '120',
                'buttonText': '选择文件',
                'debug': false,
                'auto': true,
                //允许上传的文件后缀
                'fileTypeExts': '*.*',
                //上传文件的大小限制
                'fileSizeLimit': '100MB',
                //上传到服务器，服务器返回相应信息到data里
                'onUploadSuccess': function (file, data, response) {
                    $("#uploadifyNoteTandixushui").html(data);
                }
            });
            $("#uploadifyYiluoheruhuang").uploadify({
                'height': '30',
                'swf': 'JS/plugins/uploadify/uploadify.swf',
                'uploader': 'WebForm1.aspx?Method=guochengkongzhi&ModelID=' + ModelIDParam,
                'folder': 'UploadFile',
                'width': '120',
                'buttonText': '选择文件',
                'debug': false,
                'auto': true,
                //允许上传的文件后缀
                'fileTypeExts': '*.*',
                //上传文件的大小限制
                'fileSizeLimit': '100MB',
                //上传到服务器，服务器返回相应信息到data里
                'onUploadSuccess': function (file, data, response) {
                    $("#uploadifyNoteYiluoheruhuang").html(data);
                }
            });
            $("#uploadifyQinheruhuang").uploadify({
                'height': '30',
                'swf': 'JS/plugins/uploadify/uploadify.swf',
                'uploader': 'WebForm1.aspx?Method=guochengkongzhi&ModelID=' + ModelIDParam,
                'folder': 'UploadFile',
                'width': '120',
                'buttonText': '选择文件',
                'debug': false,
                'auto': true,
                //允许上传的文件后缀
                'fileTypeExts': '*.*',
                //上传文件的大小限制
                'fileSizeLimit': '100MB',
                //上传到服务器，服务器返回相应信息到data里
                'onUploadSuccess': function (file, data, response) {
                    $("#uploadifyNoteQinheruhuang").html(data);
                }
            });
        });
    </script>
</head>
<body>
    <div class="wrapper">
        <form action="" class="main">
        <fieldset>
            <div class="widget fluid">
                <div class="whead">
                    <h6>
                        过程控制</h6>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <div class="grid3">
                        <label>
                            河道引水流量 / 损失流量文件：</label></div>
                    <div class="grid9">
                        <div id="fileQueue">
                        </div>
                        <input type="file" name="uploadify" id="uploadify" />
                        <span class="note" id="uploadifyNote"></span>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <div class="grid3">
                        <label>
                            东平湖分水：</label></div>
                    <div class="grid9">
                        <div id="Div1">
                        </div>
                        <input type="file" name="uploadify" id="uploadifyDongpinghufenshui" />
                        <span class="note" id="uploadifyNoteDongpinghufenshui"></span>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <div class="grid3">
                        <label>
                            滩地蓄水：</label></div>
                    <div class="grid9">
                        <div id="Div2">
                        </div>
                        <input type="file" name="uploadify" id="uploadifyTandixushui" />
                        <span class="note" id="uploadifyNoteTandixushui"></span>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <div class="grid3">
                        <label>
                            伊洛河入黄过程：</label></div>
                    <div class="grid9">
                        <div id="Div3">
                        </div>
                        <input type="file" name="uploadify" id="uploadifyYiluoheruhuang" />
                        <span class="note" id="uploadifyNoteYiluoheruhuang"></span>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <div class="grid3">
                        <label>
                            沁河入黄过程：</label></div>
                    <div class="grid9">
                        <div id="Div4">
                        </div>
                        <input type="file" name="uploadify" id="uploadifyQinheruhuang" />
                        <span class="note" id="uploadifyNoteQinheruhuang"></span>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="formRow">
                    <div class="grid3">
                        <label>
                        </label>
                    </div>
                    <div class="grid9">
                        <input type="button" class="buttonS bLightBlue" id="tijiaobtn" value="提交" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <input type="button" class="buttonS bRed" value="重置" id="tijiaoClear" />
                        <span class="note" id="tijiaoInfo"></span>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </fieldset>
        </form>
    </div>
</body>
</html>
