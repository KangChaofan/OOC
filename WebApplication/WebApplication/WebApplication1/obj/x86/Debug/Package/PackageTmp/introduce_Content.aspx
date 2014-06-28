<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="introduce_Content.aspx.cs" Inherits="WebApplication1.introduce_Content" %>
<%@ Register  TagPrefix="usercontrol" TagName="Breadcrumbs" Src="~/UserControl/Breadcrumbs.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<link href="css/styles.css" rel="stylesheet" type="text/css" />
<script src="Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
<script src="js/plugins/layer/layer.min.js" type="text/javascript"></script>
<script type="text/javascript" src="js/plugins/ui/jquery.collapsible.min.js"></script>
    <title></title>
    <script type="text/javascript">
        $(document).ready(function () {
            $("td input").click(function () {
                window.location.replace("/ui.aspx?ModelID=" + $(this).attr("id") + "&Type=" + $(this).attr("name"));
            });
        });
</script>
</head>
<body>
    
<!-- Content begins -->
<div id="content"  style="padding-top:0px;">
    <!-- Breadcrumbs line -->
    <usercontrol:Breadcrumbs runat="server" ID="Breadcrumbs" style=" width:90%;"/>
    <!-- Main content -->
    <div class="wrapper" style=" width:90%;">
		<!-- Standard table -->
        <div class="widget">
            <div class="whead"><h6>
            一维数学模型是发展最早，也是最简单的数学模型，在理论上和实践上都比较成熟，国内外使用都很普遍。在洪水演进模拟过程中，一维模型计算速度快，计算范围大，可以在宏观上描述洪水运动，在洪水演进模拟中应用较多。
            </h6>
            </div>
            
            <table cellpadding="0" cellspacing="0" width="100%" class="tDefault">
                <thead>
                    <tr>
                        <td>中文名称</td>
                        <td>英文缩写</td>
                        <td>基本描述</td>
                        <td>适用流域</td>
                        <td></td>
                    </tr>
                </thead>
                <tbody>
                    <asp:Literal runat="server" ID="forModelList"></asp:Literal>
                </tbody>
            </table>
        </div>

  </div>
  <!-- Main content ends -->
  </div>
<!-- Content ends -->    
</body>
</html>
