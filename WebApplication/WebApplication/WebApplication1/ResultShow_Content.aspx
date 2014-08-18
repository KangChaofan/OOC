<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResultShow_Content.aspx.cs" Inherits="WebApplication1.ResultShow_Content" %>
<%@ Register TagPrefix="usercontrol" TagName="Breadcrumbs" Src="~/UserControl/Breadcrumbs.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="css/styles.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="js/plugins/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/plugins/ui/jquery.collapsible.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("td input").click(function () {
<<<<<<< HEAD
                window.location.replace("/statistics.aspx?Type=" + $(this).attr("title") + "&TaskID=" + $(this).attr("id"));
=======
                window.location.replace("/statistics.aspx?Type=" + $(this).attr("name") + "&ResultLogsID="+$(this).attr("id"));
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
            });
        });        
    </script>
    <title></title>
</head>
<body>
    <div id="content" style="padding-top: 0px;">
        <usercontrol:Breadcrumbs runat="server" ID="Breadcrumbs" style="width: 90%;" />
        <!-- Breadcrumbs line -->
        <!-- Main content -->
        <div class="wrapper">
            <!-- Standard table -->
            <div class="widget">
                <div class="whead">
                    <h6>
                        黄河下游一维非恒定水沙数学模型可以较完备满足不同服务需求，解决河道洪水演进预报与枯水条件下水量调度等。大洪水时模型可以进行洪水预报作业，预测河道洪水演进过程，为防洪减灾提供决策支持；小流量时可以为黄河水量精细调度提供技术支持；考虑泥沙、河床冲淤演变模块可为河道淤积、河床演变等预测提供依据</h6>
                    <div class="clear">
                    </div>
                </div>
                <table cellpadding="0" cellspacing="0" width="100%" class="tDefault">
                    <thead>
                        <tr>
                            <td>
                                名称
                            </td>
                            <td>
<<<<<<< HEAD
                                开始时间
                            </td>
                            <td>
                                结束时间
=======
                                模型
                            </td>
                            <td>
                                时间
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
                            </td>
                            <td>
                                查看
                            </td>
                            <td>
                                编辑重算
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Literal ID="LitResultLogs" runat="server"></asp:Literal>
                    </tbody>
                </table>
            </div>
            <div class="divider">
                <span></span>
            </div>
        </div>
        <!-- Main content ends -->
    </div>
</body>
</html>
