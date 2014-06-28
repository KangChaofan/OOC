<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Single_Model_Content.aspx.cs" Inherits="WebApplication1.Single_Model_Content" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register  TagPrefix="usercontrol" TagName="Breadcrumbs" Src="~/UserControl/Breadcrumbs.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>水利数值模拟云服务平台</title>
<link href="css/styles.css" rel="stylesheet" type="text/css" />
<script src="Scripts/jquery-1.8.0.js" type="text/javascript"></script>
<script src="js/plugins/layer/layer.min.js" type="text/javascript"></script>
<script type="text/javascript" src="js/plugins/ui/jquery.collapsible.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        //初始化时隐藏配置按钮，等待选择模型后显示
        $(".middleNavS").hide();
        $(".bLightBlue").click(function () {
            $("#ModelNameForCal").html(
            "已预备计算</br><span id=\"" + $(this).attr("id") + "\" style=\" font-size:14px;color:red;\">" + $(this).attr("name") + "</span>");
            //选择模型后显示配置按钮
            $(".middleNavS").show();
        });

        ////修改最后完成后的Done
        function IsCalAvali() {
            var str = $("#strongCanshupeizhi").html() + $("#strongBianjiekongzhi").html() + $("#strongChushitiaojian").html() + $("#strongShuchukongzhi").html();
            if (str.toString().length == 16) {
                $("#strongZaixianjisuan").html("Done");
            }
        }

        //计算配置-------------Start------------------------------------------------------------------
        $("#canshupeizhi").click(function () {
            $.layer({
                type: 2,
                title: ['参数配置', true],
                iframe: { src: 'ParamCanShuPeiZhi.aspx?ModelID=' + $("#ModelNameForCal span").attr("id") },
                area: ['1000px', '500px'],
                offset: ['100px', ''],
                success: function () {
                    layer.shift('bottom', 200)
                },
                close: function (index) {
                    $("#strongCanshupeizhi").html("Done");
                    IsCalAvali();
                    layer.close(index);
                }
            });

        });
        $("#bianjiekongzhi").click(function () {
            $.layer({
                type: 2,
                title: ['边界控制', true],
                iframe: { src: 'ParamBianJieKongZhi.aspx?ModelID=' + $("#ModelNameForCal span").attr("id") },
                area: ['1000px', '500px'],
                success: function () {
                    layer.shift('bottom', 200)
                },
                close: function (index) {
                    $("#strongBianjiekongzhi").html("Done");
                    IsCalAvali();
                    layer.close(index);
                }
            });
        });
        $("#chushitiaojian").click(function () {
            $.layer({
                type: 2,
                title: ['初始条件', true],
                iframe: { src: 'ParamChuShiTiaoJian.aspx?ModelID=' + $("#ModelNameForCal span").attr("id") },
                area: ['1000px', '500px'],
                success: function () {
                    layer.shift('bottom', 400)
                },
                close: function (index) {
                    //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#ModelName', index).val(), 3, 1);                    
                    $("#strongChushitiaojian").html("Done");
                    IsCalAvali();
                    layer.close(index);
                }
            });
        });



        $("#guochengkongzhi").click(function () {
            $.layer({
                type: 2,
                title: ['过程控制', true],
                iframe: { src: 'ParamGuoChengKongZhi.aspx?ModelID=' + $("#ModelNameForCal span").attr("id") },
                area: ['1000px', '650px'],
                success: function () {
                    layer.shift('bottom', 200)
                },
                close: function (index) {
                    //layer.msg('您获得了子窗口标记：' + layer.getChildFrame('#ModelName', index).val(), 3, 1);                    
                    $("#strongGuochengkongzhi").html("Done");
                    IsCalAvali();
                    layer.close(index);
                }
            });
        });

        $('#zaixianjisuan').click(function () {
            $.ajax({
                url: 'runModel.aspx?ModelID=' + $("#ModelNameForCal span").attr("id"),
                type: 'POST',
                data: { Name: "keyun2" },
                dataType: 'html',
                timeout: 100000,
                error: function () {
                    alert("kaishi");
                },
                beforeSend: function () {
                    layer.load(20); //5秒后关闭
                },
                success: function (result) {
                    $.layer({
                        type: 2,
                        title: false,
                        iframe: { src: 'LatestResultShow.aspx?ResultLogsID=' + result },
                        area: ['1000px', '650px'],
                        success: function () {
                            layer.shift('bottom', 200)
                        }
                    });
                }
            });
        });
        //计算配置-------------End------------------------------------------------------------------
    });
</script>
</head>

<body>

    
<!-- Content begins -->   
    <!-- Breadcrumbs line -->
        <usercontrol:Breadcrumbs runat="server" ID="Breadcrumbs" />
    <!-- Main content -->
    <div class="wrapper">
        <ul class="middleNavR">
            <li><a href="#" title="Add an article" class="tipN"><img src="images/icons/middlenav/create.png" alt="" /></a></li>
            <li><a href="#" title="Upload files" class="tipN"><img src="images/icons/middlenav/upload.png" alt="" /></a></li>
            <li><a href="#" title="Add something" class="tipN"><img src="images/icons/middlenav/add.png" alt="" /></a></li>
            <li><a href="#" title="Messages" class="tipN"><img src="images/icons/middlenav/dialogs.png" alt="" /></a><strong>8</strong></li>
            <li><a href="#" title="Check statistics" class="tipN"><img src="images/icons/middlenav/stats.png" alt="" /></a></li>
        </ul>
    
		<!-- Standard table -->
        <div class="widget">
            <div class="whead"><h6>一维数学模型是发展最早，也是最实用的数学模型，在理论上和实践上都比较成熟，国内外使用都很普遍。在洪水演进模拟过程中，一维模型计算速度快，计算范围大，可以在宏观上描述洪水运动，在洪水演进模拟中应用较多。</h6><div class="clear"></div></div>
            
            <table cellpadding="0" cellspacing="0" width="100%" class="tDefault">
                <thead>
                    <tr>
                        <td>中文名称</td>
                        <td>英文缩写</td>
                        <td>模型分类</td>
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
        <ul class="middleNavR">
            <li><a id="ModelNameForCal" class="tipN"><img src="images/icons/middlenav/create.png" alt=""></a></li>
        </ul>
    <ul class="middleNavS">
            <li><a title="Step 1" class="tipN" id="canshupeizhi">Step 1</br>参数配置</a><strong id="strongCanshupeizhi"></strong></li>
            <li><a title="Step 2" class="tipN" id="bianjiekongzhi">Step 2</br>边界控制</a><strong id="strongBianjiekongzhi"></strong></li>
            <li><a title="Step 3" class="tipN"id="chushitiaojian">Step 3</br>初始条件</a><strong id="strongChushitiaojian"></strong></li>
            <li><a title="Step 4" class="tipN" id="guochengkongzhi">Step 4</br>过程控制</a><strong id="strongGuochengkongzhi"></strong></li>
            <li><a title="Step 5" class="tipN" id="zaixianjisuan">Step 5</br>在线计算</a><strong id="strongZaixianjisuan"></strong></li>
        </ul>    
        <div class="divider"><span></span></div>


  </div>
  <!-- Main content ends -->
    
<!-- Content ends -->    
   
        

</body>
</html>