<%@ Control Language="C#" CodeBehind="LogonTemplateContent.ascx.cs" ClassName="LogonTemplateContent" Inherits="QAMS.Web.LogonTemplateContent" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v20.2, Version=20.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.ExpressApp.Web.Templates.ActionContainers"
    TagPrefix="xaf" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v20.2, Version=20.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.ExpressApp.Web.Templates.Controls"
    TagPrefix="xaf" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v20.2, Version=20.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.ExpressApp.Web.Controls"
    TagPrefix="xaf" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v20.2, Version=20.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.ExpressApp.Web.Templates"
    TagPrefix="xaf" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v20.2, Version=20.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<div class="LogonTemplate">
    <xaf:XafUpdatePanel ID="UPPopupWindowControl" runat="server">
        <xaf:XafPopupWindowControl runat="server" ID="PopupWindowControl" />
    </xaf:XafUpdatePanel>
    <%--    <xaf:XafUpdatePanel ID="UPHeader" runat="server">
        <div class="white borderBottom width100" id="headerTableDiv">
            <div class="paddings sizeLimit" style="margin: auto">
                <table id="headerTable" class="headerTable xafAlignCenter white width100 sizeLimit" style="height: 2px;">
                    <tbody>
                        <tr>
                            <td>
                                <asp:HyperLink runat="server" ID="LogoLink">
                                    <xaf:ThemedImageControl ID="TIC" ImageName="Logo" BorderWidth="0px" runat="server" />
                                </asp:HyperLink>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </xaf:XafUpdatePanel>--%>

    <div style="top: 5%; width: 100%; position: absolute;">
        <%--class="LogonMainTable LogonContentWidth"--%>
        <%--float: left;--%> 
        <table  style="width: 100%;height:90%;">
            <tr>
                <td>
                    <xaf:XafUpdatePanel ID="UPEI" runat="server">
                        <xaf:ErrorInfoControl ID="ErrorInfo" Style="margin:5% 10% 5% 10%" runat="server" />
                    </xaf:XafUpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <%--class="LogonContent LogonContentWidth"--%>
                    <%--style="float: left"--%>
                    <table  style="background-color:white;margin-right:20%; margin-left:20%;margin-top:5%;float:none;">
                        <tr>
                            <%--class="LogonContentCell"--%>
                            <td style="width: 50%;height:100%; padding-left:2em;>
                                <xaf:XafUpdatePanel ID="UPVSC" runat="server">
                                    <xaf:ViewSiteControl ID="viewSiteControl" runat="server" />
                                </xaf:XafUpdatePanel>
                                <%--CssClass="right"--%>
                                <xaf:XafUpdatePanel ID="UPPopupActions" runat="server" >
                                    <xaf:ActionContainerHolder ID="PopupActions" runat="server" Orientation="Horizontal" ContainerStyle="Buttons">
                                        <Menu Width="100%" ItemAutoWidth="False" />
                                        <ActionContainers>
                                            <xaf:WebActionContainer ContainerId="PopupActions" />
                                        </ActionContainers>
                                    </xaf:ActionContainerHolder>
                                </xaf:XafUpdatePanel>
                            </td>
                             <%--background: url('Images/qa-automation-testing.png') no-repeat center center fixed; -webkit-background-size: inherit; -moz-background-size: inherit; -o-background-size: inherit; background-size: inherit;--%>
                            <td style="width: 50%;height:10%;margin-right:20%; margin-left:20%;margin-top:30%;margin-bottom:30%;float:none;">
                                <img src="Images/LoginRight.jpg" style="width:100% ; height:10%; resize:both" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>

</div>
<script type="text/javascript">
    (function () {
        var mainWindow = xaf.Utils.GetMainWindow();
        mainWindow.pageLoaded = false;
        $(window).on("load", function () {
            var mainWindow = xaf.Utils.GetMainWindow();
            mainWindow.pageLoaded = true;
            PageLoaded();
        });
    })();
</script>
