<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompanyCategoriesData.ascx.cs" Inherits="WdS.ElioPlus.Controls.Dashboard.CompanyCategoriesData" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<h2><asp:Label ID="Label2" runat="server" /></h2>
<div style="margin-top:20px;"></div>
<asp:Panel ID="PnlIndustry" runat="server">
    <div class="reg-header">
        <asp:Label ID="Label37" runat="server" />
    </div>
    <table>
        <tr>
            <td>
                <div class="row"></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="dash-bar-row">   
                    <asp:CheckBoxList ID="cb1" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList>
                </div>
            </td>
            <td>
                <div class="dash-bar-row">    
                    <asp:CheckBoxList ID="cb2" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
            <td>
                <div class="dash-bar-row"> 
                    <asp:CheckBoxList ID="cb3" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
        </tr>           
    </table>    
</asp:Panel>
<asp:Panel ID="PnlSubIndustry" Visible="false" runat="server">
    <div class="reg-header">
        <asp:Label ID="LabelSub7" runat="server" Text="Sales & Marketing" />
    </div>
    <table>
        <tr>
            <td>
                <div class="row"></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="dash-bar-row-sub">   
                    <asp:CheckBoxList ID="cbSub1" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList>
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub">    
                    <asp:CheckBoxList ID="cbSub2" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub"> 
                    <asp:CheckBoxList ID="cbSub3" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
        </tr> 
    </table>
    <div class="reg-header">
        <asp:Label ID="Label15" runat="server" Text="Customer Management" />
    </div>
    <table>
        <tr>
            <td>
                <div class="row"></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="dash-bar-row-sub">   
                    <asp:CheckBoxList ID="cbSub4" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList>
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub">    
                    <asp:CheckBoxList ID="cbSub5" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub"> 
                    <asp:CheckBoxList ID="cbSub6" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
        </tr>
    </table>
    <div class="reg-header">
        <asp:Label ID="Label17" runat="server" Text="Project Management" />
    </div>
    <table>
        <tr>
            <td>
                <div class="row"></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="dash-bar-row-sub">   
                    <asp:CheckBoxList ID="cbSub7" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList>
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub">    
                    <asp:CheckBoxList ID="cbSub8" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub"> 
                    <asp:CheckBoxList ID="cbSub9" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
        </tr>
    </table>
    <div class="reg-header">
        <asp:Label ID="Label18" runat="server" Text="Operations & Workflow" />
    </div>
    <table>
        <tr>
            <td>
                <div class="row"></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="dash-bar-row-sub">   
                    <asp:CheckBoxList ID="cbSub10" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList>
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub">    
                    <asp:CheckBoxList ID="cbSub11" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub"> 
                    <asp:CheckBoxList ID="cbSub12" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
        </tr>
    </table>
    <div class="reg-header">
        <asp:Label ID="Label20" runat="server" Text="Tracking & Measurement" />
    </div>
    <table>
        <tr>
            <td>
                <div class="row"></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="dash-bar-row-sub">   
                    <asp:CheckBoxList ID="cbSub13" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList>
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub">    
                    <asp:CheckBoxList ID="cbSub14" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub"> 
                    <asp:CheckBoxList ID="cbSub15" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
        </tr>
    </table>
    <div class="reg-header">
        <asp:Label ID="Label21" runat="server" Text="Accounting & Financials" />
    </div>
    <table>
        <tr>
            <td>
                <div class="row"></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="dash-bar-row-sub">   
                    <asp:CheckBoxList ID="cbSub16" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList>
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub">    
                    <asp:CheckBoxList ID="cbSub17" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub"> 
                    <asp:CheckBoxList ID="cbSub18" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
        </tr>
    </table>
    <div class="reg-header">
        <asp:Label ID="Label22" runat="server" Text="HR" />
    </div>
    <table>
        <tr>
            <td>
                <div class="row"></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="dash-bar-row-sub">   
                    <asp:CheckBoxList ID="cbSub19" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList>
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub">    
                    <asp:CheckBoxList ID="cbSub20" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub"> 
                    <asp:CheckBoxList ID="cbSub21" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
        </tr>
    </table>
    <div class="reg-header">
        <asp:Label ID="Label23" runat="server" Text="Web Mobile Software Development" />
    </div>
    <table>
        <tr>
            <td>
                <div class="row"></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="dash-bar-row-sub">   
                    <asp:CheckBoxList ID="cbSub22" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList>
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub">    
                    <asp:CheckBoxList ID="cbSub23" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub"> 
                    <asp:CheckBoxList ID="cbSub24" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
        </tr>
    </table>
    <div class="reg-header">
        <asp:Label ID="Label24" runat="server" Text="IT & Infrastructure" />
    </div>
    <table>
        <tr>
            <td>
                <div class="row"></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="dash-bar-row-sub">   
                    <asp:CheckBoxList ID="cbSub25" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList>
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub">    
                    <asp:CheckBoxList ID="cbSub26" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub"> 
                    <asp:CheckBoxList ID="cbSub27" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
        </tr>
    </table>
    <div class="reg-header">
        <asp:Label ID="Label3" runat="server" Text="Business Utilities" />
    </div>
    <table>
        <tr>
            <td>
                <div class="row"></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="dash-bar-row-sub">   
                    <asp:CheckBoxList ID="cbSub28" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList>
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub">    
                    <asp:CheckBoxList ID="cbSub29" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub"> 
                    <asp:CheckBoxList ID="cbSub30" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
        </tr>
    </table>
    <div class="reg-header">
        <asp:Label ID="Label27" runat="server" Text="Security & Backup" />
    </div>
    <table>
        <tr>
            <td>
                <div class="row"></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="dash-bar-row-sub">   
                    <asp:CheckBoxList ID="cbSub31" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList>
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub">    
                    <asp:CheckBoxList ID="cbSub32" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub"> 
                    <asp:CheckBoxList ID="cbSub33" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
        </tr>
    </table>
    <div class="reg-header">
        <asp:Label ID="Label4" runat="server" Text="Design & Multimedia" />
    </div>
    <table>
        <tr>
            <td>
                <div class="row"></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="dash-bar-row-sub">   
                    <asp:CheckBoxList ID="cbSub34" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList>
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub">    
                    <asp:CheckBoxList ID="cbSub35" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
            <td>
                <div class="dash-bar-row-sub"> 
                    <asp:CheckBoxList ID="cbSub36" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
        </tr>
    </table>   
</asp:Panel>  
<%--<asp:Panel ID="PnlSubIndustry" runat="server">
    <div class="reg-header">
        <asp:Label ID="Label3" runat="server" />
    </div>
    <table>
        <tr>
            <td>
                <div class="row"></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="dash-bar-row">   
                    <asp:CheckBoxList ID="cbSub1" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList>
                </div>
            </td>
            <td>
                <div class="dash-bar-row">    
                    <asp:CheckBoxList ID="cbSub2" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
            <td>
                <div class="dash-bar-row"> 
                    <asp:CheckBoxList ID="cbSub3" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
            <td>
                <div class="dash-bar-row">   
                    <asp:CheckBoxList ID="cbSub4" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList>
                </div>
            </td>
            <td>
                <div class="dash-bar-row">    
                    <asp:CheckBoxList ID="cbSub5" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
        </tr>           
    </table>
</asp:Panel>--%>
<asp:Panel ID="PnlPartner" runat="server">
    <div class="reg-header">
        <asp:Label ID="Label1" runat="server" />
    </div>
    <table>
        <tr>
            <td>
                <div class="row"></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="dash-bar-row">   
                    <asp:CheckBoxList ID="cb4" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
            <td>
                <div class="dash-bar-row">
                    <asp:CheckBoxList ID="cb5" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList>
                </div>
            </td>
            <td>
                <div class="dash-bar-row">
                    <asp:CheckBoxList ID="cb6" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList>
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="PnlMarket" runat="server">
    <div class="reg-header">
        <asp:Label ID="Label26" runat="server" />
    </div>
    <table>
        <tr>
            <td>
                <div class="row"></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="dash-bar-row">    
                    <asp:CheckBoxList ID="cb7" TextAlign="left" runat="server" />
                </div>
            </td>
            <td>
                <div class="dash-bar-row" style="width:300px;">   
                    <asp:CheckBoxList ID="cb8" TextAlign="left" runat="server"/>
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="PnlApi" runat="server">
    <div class="reg-header">
        <asp:Label ID="Label29" runat="server" />
    </div>
    <table>
        <tr>
            <td>
                <div class="row"></div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="dash-bar-row">  
                    <asp:CheckBoxList ID="cb9" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList>
                </div>
            </td>
            <td>
                <div class="dash-bar-row">   
                    <asp:CheckBoxList ID="cb10" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList> 
                </div>
            </td>
            <td>
                <div class="dash-bar-row">   
                    <asp:CheckBoxList ID="cb11" TextAlign="left" runat="server">                            
                    </asp:CheckBoxList>
                </div>
            </td>
        </tr>                               
    </table>
</asp:Panel>
<table>
    <tr>
        <td style="padding: 10px; width:90px;">
            <div>
                <telerik:RadButton ID="RbtnUpdate" OnClick="RbtnUpdate_OnClick" runat="server">
                    <ContentTemplate>
                        <span>
                            <asp:Label ID="LblUpdateText" runat="server" />
                        </span>
                    </ContentTemplate>
                </telerik:RadButton>                                        
            </div>
        </td>
    </tr>
</table>
<controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />