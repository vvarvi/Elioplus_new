<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubIndustries.ascx.cs" Inherits="WdS.ElioPlus.Controls.Dashboard.SubIndustries" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Panel ID="PnlContent" runat="server" style="margin-bottom:50px; margin-left:50px; min-height:550px;">
    <h2><asp:Label ID="Label2" Text="Company Sub Categories Information" runat="server" /></h2>
    <div style="margin-top:20px;"></div>
    <div class="nav nav-tabs" style="float:left;">        
        <telerik:RadTabStrip AutoPostBack="false" ID="RtbSubInfo" Orientation="VerticalLeft" runat="server" MultiPageID="MultiPageRtbSubInfo">
            <Tabs>
                <telerik:RadTab Value="1" Text="Sales & Marketing" PageViewID="PgVSalesMarketing" Selected="true" runat="server" />
                <telerik:RadTab Value="2" Text="Customer Management" PageViewID="PgVCustomerManagement" runat="server" />
                <telerik:RadTab Value="3" Text="Project Management" PageViewID="PgVProjectManagement " runat="server" />
                <telerik:RadTab Value="4" Text="Operations & Workflow" PageViewID="PgVOperationsWorkflow" runat="server" />
                <telerik:RadTab Value="5" Text="Tracking & Measurement" PageViewID="PgVTrackingMeasurement" runat="server" />
                <telerik:RadTab Value="6" Text="Accounting & Financials" PageViewID="PgVAccountingFinancials" runat="server" />
                <telerik:RadTab Value="7" Text="HR" PageViewID="PgVHR" runat="server" />
                <telerik:RadTab Value="8" Text="Web Mobile Software Development" PageViewID="PgVWebMobileSoftwareDevelopment" runat="server" />
                <telerik:RadTab Value="9" Text="IT & Infrastructure" PageViewID="PgVITInfrastructure" runat="server" />
                <telerik:RadTab Value="10" Text="Business Utilities" PageViewID="PgVBusinessUtilities" runat="server" />
                <telerik:RadTab Value="11" Text="Security & Backup" PageViewID="PgVSecurityBackup" runat="server" />
                <telerik:RadTab Value="12" Text="Design & Multimedia" PageViewID="PgVDesignMultimedia" runat="server" />
                <telerik:RadTab Value="13" Text="Miscellaneous" PageViewID="PgVMiscellaneous" runat="server" />
            </Tabs>
        </telerik:RadTabStrip>
        <div style="">
            <table>
                <tr>
                    <td style="padding:10px 0 10px 0; width:90px;">
                        <div>
                            <telerik:RadButton ID="RbtnUpdate" OnClick="RbtnUpdate_OnClick" style="width:265px;" runat="server">
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
        </div>
    </div>
    <div style="display:inherit;">
        <telerik:RadMultiPage ID="MultiPageRtbSubInfo" style="width:900px;margin-left:300px; min-height:600px;" runat="server">

            <telerik:RadPageView ID="PgVSalesMarketing" Selected="true" runat="server">
                <asp:Panel ID="PnlSalesMarketing" CssClass="multy-panel-css" runat="server">
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
                </asp:Panel>
            </telerik:RadPageView>

            <telerik:RadPageView ID="PgVCustomerManagement" runat="server">
                <asp:Panel ID="Panel1" CssClass="multy-panel-css" runat="server">
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
                </asp:Panel>
            </telerik:RadPageView>

            <telerik:RadPageView ID="PgVProjectManagement" runat="server">
                <asp:Panel ID="Panel2" CssClass="multy-panel-css" runat="server">
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
                </asp:Panel>
            </telerik:RadPageView>

            <telerik:RadPageView ID="PgVOperationsWorkflow" runat="server">
                <asp:Panel ID="Panel3" CssClass="multy-panel-css" runat="server">
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
                </asp:Panel>
            </telerik:RadPageView>

            <telerik:RadPageView ID="PgVTrackingMeasurement" runat="server">
                <asp:Panel ID="Panel4" CssClass="multy-panel-css" runat="server">
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
                </asp:Panel>
            </telerik:RadPageView>

            <telerik:RadPageView ID="PgVAccountingFinancials" runat="server">
                <asp:Panel ID="Panel5" CssClass="multy-panel-css" runat="server">
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
                </asp:Panel>
            </telerik:RadPageView>

            <telerik:RadPageView ID="PgVHR" runat="server">
                <asp:Panel ID="Panel6" CssClass="multy-panel-css" runat="server">
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
                </asp:Panel>
            </telerik:RadPageView>

            <telerik:RadPageView ID="PgVWebMobileSoftwareDevelopment" runat="server">
                <asp:Panel ID="Panel7" CssClass="multy-panel-css" runat="server">
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
                </asp:Panel>
            </telerik:RadPageView>

            <telerik:RadPageView ID="PgVITInfrastructure" runat="server">
                <asp:Panel ID="Panel8" CssClass="multy-panel-css" runat="server">
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
                </asp:Panel>
            </telerik:RadPageView>

            <telerik:RadPageView ID="PgVBusinessUtilities" runat="server">
                <asp:Panel ID="Panel9" CssClass="multy-panel-css" runat="server">
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
                </asp:Panel>
            </telerik:RadPageView>

            <telerik:RadPageView ID="PgVSecurityBackup" runat="server">
                <asp:Panel ID="Panel10" CssClass="multy-panel-css" runat="server">
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
                </asp:Panel>
            </telerik:RadPageView>

            <telerik:RadPageView ID="PgVDesignMultimedia" runat="server">
                <asp:Panel ID="Panel11" CssClass="multy-panel-css" runat="server">
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
            </telerik:RadPageView>

            <telerik:RadPageView ID="PgVMiscellaneous" runat="server">
                <asp:Panel ID="PnlMiscellaneous" CssClass="multy-panel-css" runat="server">
                    <table>
                        <tr>
                            <td>
                                <div class="row"></div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="dash-bar-row-sub">   
                                    <asp:CheckBoxList ID="cbSub37" TextAlign="left" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                            <td>
                                <div class="dash-bar-row-sub">    
                                    <asp:CheckBoxList ID="cbSub38" TextAlign="left" runat="server">
                                    </asp:CheckBoxList> 
                                </div>
                            </td>
                            <td>
                                <div class="dash-bar-row-sub"> 
                                    
                                </div>
                            </td>
                        </tr> 
                    </table>
                </asp:Panel>
            </telerik:RadPageView>

            <telerik:RadPageView ID="PgVUnifiedCommunications" runat="server">
                <asp:Panel ID="Panel12" CssClass="multy-panel-css" runat="server">
                    <table>
                        <tr>
                            <td>
                                <div class="row"></div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="dash-bar-row-sub">   
                                    <asp:CheckBoxList ID="cbSub39" TextAlign="left" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                            <td>
                                <div class="dash-bar-row-sub">    
                                    <asp:CheckBoxList ID="cbSub40" TextAlign="left" runat="server">
                                    </asp:CheckBoxList> 
                                </div>
                            </td>
                            <td>
                                <div class="dash-bar-row-sub"> 
                                    <asp:CheckBoxList ID="cbSub41" TextAlign="left" runat="server">
                                    </asp:CheckBoxList> 
                                </div>
                            </td>
                        </tr> 
                    </table>
                </asp:Panel>
            </telerik:RadPageView>
            <telerik:RadPageView ID="PgVCadPlm" runat="server">
                <asp:Panel ID="Panel13" CssClass="multy-panel-css" runat="server">
                    <table>
                        <tr>
                            <td>
                                <div class="row"></div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="dash-bar-row-sub">   
                                    <asp:CheckBoxList ID="cbSub42" TextAlign="left" runat="server">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                            <td>
                                <div class="dash-bar-row-sub">    
                                    <asp:CheckBoxList ID="cbSub43" TextAlign="left" runat="server">
                                    </asp:CheckBoxList> 
                                </div>
                            </td>
                            <td>
                                <div class="dash-bar-row-sub"> 
                                    
                                </div>
                            </td>
                            <td>
                                <div class="dash-bar-row-sub"> 
                                    <asp:CheckBoxList ID="cbSub44" TextAlign="left" runat="server">
                                    </asp:CheckBoxList> 
                                </div>
                            </td>
                        </tr> 
                    </table>
                </asp:Panel>
            </telerik:RadPageView>
        </telerik:RadMultiPage>        
    </div>
    <div style="margin-top:20px;">
        <controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />
    </div>
</asp:Panel>