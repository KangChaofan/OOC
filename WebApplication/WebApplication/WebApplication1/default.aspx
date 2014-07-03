<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WebApplication1._default" %>
<%@ Register  TagPrefix="usercontrol" TagName="subnav" Src="~/UserControl/subNav.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Uploadify</title>

    <link href="JS/plugins/uploadify/uploadify.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript"
     src="Scripts/jquery-1.4.1.js"></script>

    <script type="text/javascript"
     src="JS/jquery.uploadify-v2.1.0/swfobject.js"></script>

    <script type="text/javascript"  src="JS/plugins/uploadify/jquery.uploadify.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#uploadify").uploadify({
                'height': '30',
                'swf': 'JS/plugins/uploadify/uploadify.swf',
                'uploader': 'WebForm1.aspx',
                'folder': 'UploadFile',
                'width': '120',
                'buttonText': '选择文件',
                'debug': true,
                'auto': true,
                //允许上传的文件后缀
                'fileTypeExts': '*.OMI',
                //上传文件的大小限制
                'fileSizeLimit': '3MB',
                //上传到服务器，服务器返回相应信息到data里
                'onUploadSuccess': function (file, data, response) {
                    alert(data);
                }
            });
        });
    </script>

</head>
<body>
    <div id="fileQueue"></div>
    <input type="file" name="uploadify" id="uploadify" />
    <p>
      <a href="javascript:$('#uploadify').uploadify('upload','*')">上传</a>| 
      <a href="javascript:$('#uploadify').uploadifyClearQueue()">取消上传</a>
    </p>
          <usercontrol:subnav runat="server"  ID="subNav" />    
</body>
</html>