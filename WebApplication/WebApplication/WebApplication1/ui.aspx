<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ui.aspx.cs" Inherits="WebApplication1.ui" %>
<%@ Register  TagPrefix="usercontrol" TagName="MainNav" Src="~/UserControl/MainNav.ascx" %>
<%@ Register  TagPrefix="usercontrol" TagName="Breadcrumbs" Src="~/UserControl/Breadcrumbs.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>水利数值模拟云服务平台</title>
<link href="css/styles.css" rel="stylesheet" type="text/css" />
<script src="Scripts/jquery-1.8.0.min.js" type="text/javascript"></script>
<script src="js/plugins/layer/layer.min.js" type="text/javascript"></script>
<script src="Scripts/jquery.cookie.js" type="text/javascript"></script>
<script type="text/javascript" src="js/plugins/ui/jquery.collapsible.min.js"></script>

<script type="text/javascript" >
    $(document).ready(function () {
        $('#hhsxmx').click(function () {
            $.ajax({
                url: 'runModel.aspx',

                type: 'POST',

                data: { Name: "keyun" },

                dataType: 'html',

                timeout: 1000,

                error: function () { },

                success: function (result) { }

            });
            alert('黄河数学模型开始运行.......');
        });
    });
</script>


</head>

<body>



<!-- Content begins -->    
<div id="content" style="padding-top:0px;">
  
    <!-- Breadcrumbs line -->
        <usercontrol:Breadcrumbs runat="server" ID="Breadcrumbs" style=" width:90%;"/>
    
    <!-- Main content -->
    <div class="wrapper">    	    	
        <!-- Buttons with font icons -->
                    <div class="widget">
                    <div class="whead">
                    <h6>
                    一维数学模型是发展最早，也是最简单的数学模型，在理论上和实践上都比较成熟，国内外使用都很普遍。在洪水演进模拟过程中，一维模型计算速度快，计算范围大，可以在宏观上描述洪水运动，在洪水演进模拟中应用较多。
                    </h6>
                    <div class="clear"></div></div>
               
                <div class="whead"><h6>基本信息</h6><div class="clear"></div></div>
                <div class="formRow">
                    <div class="grid3"><label>中文名称：</label></div>
                    <div class="grid9"><div class="uSlider"><%= modelOne.name%>></div></div><div class="clear"></div>
                </div>
                
                <div class="formRow">
                    <div class="grid3"><label>英文缩写：</label></div>
                    <div class="grid9">
                        <div class="sliderSpecs">
                            <label><%= modelOne.eName%></label>
                            <div class="clear"></div>
                        </div>
                        <div class="uRange"></div>
                    </div>
                    <div class="clear"></div>
                </div>		
                
                <div class="formRow">
                    <div class="grid3"><label>基本描述：</label></div>
                    <div class="grid9">
                        <div class="sliderSpecs">
                            <label><%=modelOne.@abstract %></label>
                            <div class="clear"></div>
                        </div>
                        <div class="uMin"></div>
                    </div>
                    <div class="clear"></div>
                </div>	
                
                <div class="formRow">
                    <div class="grid3"><label>适用流域：</label></div>
                    <div class="grid9">
                        <div class="sliderSpecs">
                            <label><%=modelOne.riverBasin %></label>
                            <div class="clear"></div>
                        </div>
                        <div class="uMax"></div>
                    </div>
                    <div class="clear"></div>
                </div>
                
            </div>
            <div class="divider"><span></span></div>

                                <div class="widget">                   
               
                <div class="whead"><h6>水流模块</h6><div class="clear"></div></div>
                <div class="formRow">
                    <div class="grid3"><label>计算方法：</label></div>
                    <div class="grid9"><div class="uSlider"></div></div><div class="clear"></div>
                </div>
                
                <div class="formRow">
                    <div class="grid3"><label>求解格式：</label></div>
                    <div class="grid9">
                        <div class="sliderSpecs">
                            <label></label>
                            <div class="clear"></div>
                        </div>
                        <div class="uRange"></div>
                    </div>
                    <div class="clear"></div>
                </div>		
                
                <div class="formRow">
                    <div class="grid3"><label>汇流计算：</label></div>
                    <div class="grid9">
                        <div class="sliderSpecs">
                            <label></label>
                            <div class="clear"></div>
                        </div>
                    </div>
                    <div class="clear"></div>
                </div>	
                
                <div class="formRow">
                    <div class="grid3"><label>上摊模拟：</label></div>
                    <div class="grid9">
                        <div class="sliderSpecs">
                            <label for="maxRangeAmount"></label>
                            <div class="clear"></div>
                        </div>
                        <div class="uMax"></div>
                    </div>
                    <div class="clear"></div>
                </div>
                
            </div>
                        <div class="divider"><span></span></div>
                        
                                <div class="widget">                   
               
                <div class="whead"><h6>泥沙模块</h6><div class="clear"></div></div>
                <div class="formRow">
                    <div class="grid3"><label>挟沙力公式：</label></div>
                    <div class="grid9"><div class="uSlider"></div></div><div class="clear"></div>
                </div>
                
                <div class="formRow">
                    <div class="grid3"><label>动库阻力：</label></div>
                    <div class="grid9">
                        <div class="sliderSpecs">
                            <label></label>
                            <div class="clear"></div>
                        </div>
                        <div class="uRange"></div>
                    </div>
                    <div class="clear"></div>
                </div>		
                
                <div class="formRow">
                    <div class="grid3"><label>沉速公式：</label></div>
                    <div class="grid9">
                        <div class="sliderSpecs">
                            <label></label>
                            <div class="clear"></div>
                        </div>
                    </div>
                    <div class="clear"></div>
                </div>	
                
                <div class="formRow">
                    <div class="grid3"><label>紊动模式：</label></div>
                    <div class="grid9">
                        <div class="sliderSpecs">
                            <label for="maxRangeAmount"></label>
                            <div class="clear"></div>
                        </div>
                        <div class="uMax"></div>
                    </div>
                    <div class="clear"></div>
                </div>
                
            </div>
    </div>
    <!-- Main content ends -->
    
    </div>
<!-- Content ends -->
    

</body>
</html>
