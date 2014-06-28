<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Breadcrumbs.ascx.cs" Inherits="WebApplication1.UserControl.Breadcrumbs" %>
     
       <div class="contentTop">
        <span class="pageTitle"><img src="../images/elements/loaders/6s.gif" /> <span></span>  <%=Names[0]%></span>
        <ul class="quickStats">
            <li>
                <a href="" class="blueImg"><img src="images/icons/quickstats/plus.png" alt="" /></a>
                <div class="floatR"><strong class="blue">5489</strong><span>visits</span></div>
            </li>
            <li>
                <a href="" class="redImg"><img src="images/icons/quickstats/plus.png" alt="" /></a>
                <div class="floatR"><strong class="blue">4658</strong><span>users</span></div>
            </li>
            <li>
                <a href="" class="greenImg"><img src="images/icons/quickstats/money.png" alt="" /></a>
                <div class="floatR"><strong class="blue">1289</strong><span>orders</span></div>
            </li>
        </ul>
        <div class="clear"></div>
    </div>

    <div class="breadLine">
        <div class="bc">
            <ul id="breadcrumbs" class="breadcrumbs">
                <li><a href="#" id="bread1"><%=Names[0] %></a></li>
                <%if (Names[1].Length > 1 && Names[2].Length > 1)
                  {
                      Response.Write("<li><a href=\"#\" id=\"bread2\">" + Names[1] + "</a></li>");
                      Response.Write("<li class=\"current\"><a href=\"#\" title=\"\">" + Names[2] + "</a></li>");
                  }
                  %>
                  <%if (Names[2].Length < 1 && Names[1].Length > 1)
                    {
                        Response.Write("<li class=\"current\"><a href=\"#\" id=\"bread2\">" + Names[1] + "</a></li>");
                    }
                  %>
              
            </ul>
        </div>
        
        <div class="breadLinks">
            <ul>
                <li><a href="#" title=""><i class="icos-list"></i><span>Orders</span> <strong>(+58)</strong></a></li>
                <li><a href="#" title=""><i class="icos-list"></i><span>Tasks</span> <strong>(+12)</strong></a></li>
                <li class="has">
                    <a title="">
                        <i class="icos-list"></i>
                        <span>Invoices</span>
                        <span><img src="images/elements/control/hasddArrow.png" alt="" /></span>
                    </a>
                    <ul>
                        <li><a href="#" title=""><span class="icos-add"></span>New invoice</a></li>
                        <li><a href="#" title=""><span class="icos-archive"></span>History</a></li>
                        <li><a href="#" title=""><span class="icos-printer"></span>Print invoices</a></li>
                    </ul>
                </li>
            </ul>
             <div class="clear"></div>
        </div>
    </div>
    