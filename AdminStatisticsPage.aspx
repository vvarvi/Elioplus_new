<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="AdminStatisticsPage.aspx.cs" Inherits="WdS.ElioPlus.AdminStatisticsPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="DashboardHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Our platform allows IT vendors, resellers and developers to connect based on their interests and industry information."/>
    <meta name="keywords" content="the best SaaS vendors, channel partnerships, IT channel community"/>
</asp:Content>

<asp:Content ID="DashboardMain" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <link rel="stylesheet" type="text/css" href="style.css" />
        <script type="text/javascript">

            function RapPage_OnRequestStart(sender, args) {
                $('#loader').show();
            }

            function endsWith(s) {
                return this.length >= s.length && this.substr(this.length - s.length) == s;
            }

            function RapPage_OnResponseEnd(sender, args) {
                $('#loader').hide();
            }

            function isNumberOnly(evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;

                return true;
            }
        </script>
    </telerik:RadScriptBlock>

    <telerik:RadAjaxPanel ID="RapPage" ClientEvents-OnRequestStart="RapPage_OnRequestStart" ClientEvents-OnResponseEnd="RapPage_OnResponseEnd" runat="server" RestoreOriginalRenderDelegate="false">
        <div class="body_wrap">
            <!-- header -->
            
            <!--/ header -->
            <!-- middle -->
            <div id="middle">
                <div class="clearfix">
                    <div style="margin-top:0px; text-align:center;"> 
                        <h2><asp:Label ID="Label5" runat="server" /></h2>
                        <asp:Panel ID="PnlElioCompaniesGrid" style="margin-top:0px; text-align:left; padding:10px;margin:auto;" runat="server">
                           
                            <asp:Panel ID="PnlSearch" style="border-radius:5px; background-color:#F0F3CD;margin-bottom:10px; padding:30px 10px 30px 10px; margin:auto; position:relative;" runat="server">
                                <table>
                                    <tr>
                                        <td style="width:70px;">
                                            <div style="padding:5px;">
                                                <asp:Label ID="Label6" CssClass="label_title" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px;">
                                                <telerik:RadComboBox ID="RcbxCategory" Width="300" runat="server" />
                                            </div>
                                        </td>
                                        <td style="width:130px;">
                                            <div style="padding:5px;">
                                                <asp:Label ID="LblStatus" CssClass="label_title" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px;">
                                                <telerik:RadComboBox ID="RcbxStatus" Width="300" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:70px;">
                                            <div style="padding:5px;">
                                                <asp:Label ID="LblIsPublic" CssClass="label_title" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px;">
                                                <telerik:RadComboBox ID="RcbxIsPublic" Width="300" runat="server" />
                                            </div>
                                        </td>                                    
                                        <td style="width:130px;">
                                            <div style="padding:5px;">
                                                <asp:Label ID="Label8" CssClass="label_title" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px;">
                                                <telerik:RadComboBox ID="RcbxName" Width="300" Height="210" runat="server" />
                                            </div>
                                        </td>                                        
                                    </tr>
                                    <tr>
                                        <td style="width:70px;">
                                            <div style="padding:5px;">
                                                <asp:Label ID="Label2" CssClass="label_title" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px;">
                                                <telerik:RadComboBox ID="RcbxBillingType" Width="300" runat="server" />
                                            </div>
                                        </td>
                                        <td style="width:70px;">
                                            <div style="padding:5px;">
                                                <asp:Label ID="Label7" CssClass="label_title" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px;">
                                                <telerik:RadComboBox ID="RcbxApplicationType" Width="300" runat="server" />
                                            </div>
                                        </td>                                      
                                    </tr>
                                    <tr>
                                        <td style="width:130px;">
                                            <div style="padding:5px;">
                                                <asp:Label ID="Label3" CssClass="label_title" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px;">
                                                <telerik:RadTextBox ID="RtbxUserId" onkeypress="return isNumberOnly(event);" MaxLength="10" Width="300" runat="server" />
                                            </div>
                                        </td>
                                        <td style="width:70px;">
                                            <div style="padding:5px;">
                                                <asp:Label ID="Label4" CssClass="label_title" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px;">
                                                <telerik:RadTextBox ID="RtbxStripeCustomerId" Width="300" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:70px;">
                                            <div style="padding:5px;">
                                                <asp:Label ID="Label9" CssClass="label_title" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px;">
                                                <telerik:RadTextBox ID="RtbxEmail" MaxLength="100" Width="300" runat="server" />
                                            </div>
                                        </td>
                                        <td style="width:130px;">
                                            <div style="padding:5px;">
                                                <asp:Label ID="Label10" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px;">
                                                <telerik:RadComboBox ID="RcbxCountries" Width="300" Height="210" runat="server" />
                                            </div>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td style="width:70px;">
                                            <div style="padding:5px;">
                                                
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px;">
                                                
                                            </div>
                                        </td>
                                        <td style="width:130px;">
                                            <div style="padding:5px;">
                                                
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px;">
                                                
                                            </div>
                                        </td>
                                        
                                    </tr>
                                </table>                                
                                <table>
                                    <tr>
                                        <td style="width:135px;">
                                        </td>
                                        <td>
                                            <telerik:RadButton ID="RbtnUpSearch" Width="135" style="text-align:center;" OnClick="RbtnSearch_OnClick" runat="server">
                                                <ContentTemplate>
                                                    <span>
                                                        <asp:Label ID="LblUpSearchText" runat="server" />
                                                    </span>
                                                </ContentTemplate>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="RbtnUpReset" Width="135" style="text-align:center;" OnClick="RbtnReset_OnClick" runat="server">
                                                <ContentTemplate>
                                                    <span>
                                                        <asp:Label ID="LblUpResetText" runat="server" />
                                                    </span>
                                                </ContentTemplate>
                                            </telerik:RadButton>
                                        </td>
                                    </tr>
                                </table>
                                <div style="margin-bottom:10px;"></div>
                                <controls:MessageControl ID="UcMessageControlCriteria" Visible="false" runat="server" />                           
                                <div class="row"></div>                                
                            </asp:Panel>
                            <div style="width:100%; display:inline-block;">
                                <asp:Panel ID="PnlSubIndustry" runat="server">   
                                    <telerik:RadPanelBar ID="RpbRegistrationSteps" ExpandMode="SingleExpandedItem" Width="100%" runat="server">
                                        <Items>
                                            <telerik:RadPanelItem Expanded="True" Text="Sales & Marketing" runat="server">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server">
                                                        <ItemTemplate>
                                                            <div style="display: inline-block; width:100%;">
                                                                <div class="dash-bar-row-5" style="float:left;">   
                                                                    <asp:CheckBoxList ID="cbSub1" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                                <div class="dash-bar-row-5" style="float:right;">    
                                                                    <asp:CheckBoxList ID="cbSub2" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                                <div class="dash-bar-row-5"> 
                                                                    <asp:CheckBoxList ID="cbSub3" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem Text="Customer Management" runat="server">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server">
                                                        <ItemTemplate>
                                                            <div style="display: inline-block; width:100%;">
                                                                <div class="dash-bar-row-6" style="float:left;">   
                                                                    <asp:CheckBoxList ID="cbSub4" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                                <div class="dash-bar-row-6" style="float:right;">    
                                                                    <asp:CheckBoxList ID="cbSub5" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                                <div class="dash-bar-row-6"> 
                                                                    <asp:CheckBoxList ID="cbSub6" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>                                        
                                            <telerik:RadPanelItem Text="Project Management" runat="server">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server">
                                                        <ItemTemplate>
                                                            <div style="display: inline-block; width:100%;">
                                                                <div class="dash-bar-row-7" style="float:left;">   
                                                                    <asp:CheckBoxList ID="cbSub7" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                                <div class="dash-bar-row-7" style="float:right;">    
                                                                    <asp:CheckBoxList ID="cbSub8" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                                <div class="dash-bar-row-7"> 
                                                                    <asp:CheckBoxList ID="cbSub9" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem Text="Operations & Workflow" runat="server">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server">
                                                        <ItemTemplate>
                                                            <div style="display: inline-block; width:100%;">
                                                                <div class="dash-bar-row-8" style="float:left;">   
                                                                    <asp:CheckBoxList ID="cbSub10" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                                <div class="dash-bar-row-8" style="float:right;">    
                                                                    <asp:CheckBoxList ID="cbSub11" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                                <div class="dash-bar-row-8"> 
                                                                    <asp:CheckBoxList ID="cbSub12" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem Text="Tracking & Measurement" runat="server">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server">
                                                        <ItemTemplate>
                                                            <div style="display: inline-block; width:100%;">
                                                                <div class="dash-bar-row-9" style="float:left;">   
                                                                    <asp:CheckBoxList ID="cbSub13" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                                <div class="dash-bar-row-9" style="float:right;">    
                                                                    <asp:CheckBoxList ID="cbSub14" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                                <div class="dash-bar-row-9"> 
                                                                    <asp:CheckBoxList ID="cbSub15" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                            </div>  
                                                        </ItemTemplate>
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem Text="Accounting & Financials" runat="server">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server">
                                                        <ItemTemplate>
                                                            <div style="display: inline-block; width:100%;">
                                                                <div class="dash-bar-row-10" style="float:left;">   
                                                                    <asp:CheckBoxList ID="cbSub16" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                                <div class="dash-bar-row-10" style="float:right;">    
                                                                    <asp:CheckBoxList ID="cbSub17" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                                <div class="dash-bar-row-10"> 
                                                                    <asp:CheckBoxList ID="cbSub18" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem Text="HR" runat="server">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server">
                                                        <ItemTemplate>
                                                            <div style="display: inline-block; width:100%;">
                                                                <div class="dash-bar-row-11" style="float:left;">   
                                                                    <asp:CheckBoxList ID="cbSub19" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                                <div class="dash-bar-row-11" style="float:right;">    
                                                                    <asp:CheckBoxList ID="cbSub20" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                                <div class="dash-bar-row-11"> 
                                                                    <asp:CheckBoxList ID="cbSub21" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem Text="Web Mobile Software Development" runat="server">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server">
                                                        <ItemTemplate>
                                                            <div style="display: inline-block; width:100%;">
                                                                <div class="dash-bar-row-12" style="float:left;">   
                                                                    <asp:CheckBoxList ID="cbSub22" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                                <div class="dash-bar-row-12" style="float:right;">    
                                                                    <asp:CheckBoxList ID="cbSub23" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                                <div class="dash-bar-row-12"> 
                                                                    <asp:CheckBoxList ID="cbSub24" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem Text="IT & Infrastructure" runat="server">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server">
                                                        <ItemTemplate>
                                                            <div style="display: inline-block; width:100%;">
                                                                <div class="dash-bar-row-13" style="float:left;">   
                                                                    <asp:CheckBoxList ID="cbSub25" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                                <div class="dash-bar-row-13" style="float:right;">    
                                                                    <asp:CheckBoxList ID="cbSub26" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                                <div class="dash-bar-row-13"> 
                                                                    <asp:CheckBoxList ID="cbSub27" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem Text="Business Utilities" runat="server">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server">
                                                        <ItemTemplate>
                                                            <div style="display: inline-block; width:100%;">
                                                                <div class="dash-bar-row-14" style="float:left;">   
                                                                    <asp:CheckBoxList ID="cbSub28" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                                <div class="dash-bar-row-14" style="float:right;">    
                                                                    <asp:CheckBoxList ID="cbSub29" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                                <div class="dash-bar-row-14"> 
                                                                    <asp:CheckBoxList ID="cbSub30" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem Text="Data Security & GRC" runat="server">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server">
                                                        <ItemTemplate>
                                                            <div style="display: inline-block; width:100%;">
                                                                <div class="dash-bar-row-15" style="float:left;">   
                                                                    <asp:CheckBoxList ID="cbSub31" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                                <div class="dash-bar-row-15" style="float:right;">    
                                                                    <asp:CheckBoxList ID="cbSub32" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                                <div class="dash-bar-row-15"> 
                                                                    <asp:CheckBoxList ID="cbSub33" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem Text="Design & Multimedia" runat="server">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server">
                                                        <ItemTemplate>
                                                            <div style="display: inline-block; width:100%;">
                                                                <div class="dash-bar-row-16" style="float:left;">   
                                                                    <asp:CheckBoxList ID="cbSub34" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                                <div class="dash-bar-row-16" style="float:right;">    
                                                                    <asp:CheckBoxList ID="cbSub35" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                                <div class="dash-bar-row-16"> 
                                                                    <asp:CheckBoxList ID="cbSub36" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem Text="Miscellaneous" runat="server">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server">
                                                        <ItemTemplate>
                                                            <div style="display: inline-block; width:100%;">
                                                                <div class="dash-bar-row-3" style="float:left;">   
                                                                    <asp:CheckBoxList ID="cbSub37" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                                <div class="dash-bar-row-3" style="float:right;">    
                                                                    <asp:CheckBoxList ID="cbSub38" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                                <div class="dash-bar-row-3">                                                             
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem Text="Unified Communications" runat="server">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server">
                                                        <ItemTemplate>
                                                            <div style="display: inline-block; width:100%;">
                                                                <div class="dash-bar-row-16" style="float:left;">   
                                                                    <asp:CheckBoxList ID="cbSub39" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                                <div class="dash-bar-row-16" style="float:right;">    
                                                                    <asp:CheckBoxList ID="cbSub40" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                                <div class="dash-bar-row-16"> 
                                                                    <asp:CheckBoxList ID="cbSub41" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                            <telerik:RadPanelItem Text="CAD & PLM" runat="server">
                                                <Items>
                                                    <telerik:RadPanelItem runat="server">
                                                        <ItemTemplate>
                                                            <div style="display: inline-block; width:100%;">
                                                                <div class="dash-bar-row-16" style="float:left;">   
                                                                    <asp:CheckBoxList ID="cbSub42" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList>
                                                                </div>
                                                                <div class="dash-bar-row-16" style="float:right;">    
                                                                    <asp:CheckBoxList ID="cbSub43" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                                <div class="dash-bar-row-16"> 
                                                                    <asp:CheckBoxList ID="cbSub44" TextAlign="left" runat="server">                            
                                                                    </asp:CheckBoxList> 
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:RadPanelItem>
                                                </Items>
                                            </telerik:RadPanelItem>
                                        </Items>
                                    </telerik:RadPanelBar>
                                    <div class="row row-fix">
                                        <asp:Label ID="LblSubIndustryError" CssClass="pers-info-row-error" runat="server" />
                                    </div>
                                </asp:Panel>
                            </div>
                            <div style="margin-bottom:10px;"></div>   
                            <asp:Panel ID="Panel1" style="border-radius:5px; background-color:#F0F3CD;margin-bottom:10px; padding:30px 10px 30px 10px; margin:auto; position:relative;" runat="server">                             
                                <table>
                                    <tr>
                                        <td style="width:135px;">
                                        </td>
                                        <td>
                                            <telerik:RadButton ID="RbtnSearch" Width="135" style="text-align:center;" OnClick="RbtnSearch_OnClick" runat="server">
                                                <ContentTemplate>
                                                    <span>
                                                        <asp:Label ID="LblSearchText" runat="server" />
                                                    </span>
                                                </ContentTemplate>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="RbtnReset" Width="135" style="text-align:center;" OnClick="RbtnReset_OnClick" runat="server">
                                                <ContentTemplate>
                                                    <span>
                                                        <asp:Label ID="LblResetText" runat="server" />
                                                    </span>
                                                </ContentTemplate>
                                            </telerik:RadButton>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <div style="margin-bottom:10px;"></div>  
                            <telerik:RadGrid ID="RdgElioUsers" style="margin:auto; position:relative;" AllowPaging="true" AllowSorting="false" HeaderStyle-BackColor="#39b4ef" PagerStyle-Position="TopAndBottom" HeaderStyle-ForeColor="#ffffff" PageSize="10" Width="100%" CssClass="rgdd" OnDetailTableDataBind="RdgElioUsers_DetailTableDataBind" OnPreRender="RdgElioUsers_PreRender" OnItemDataBound="RdgElioUsers_OnItemDataBound" OnNeedDataSource="RdgElioUsers_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                <MasterTableView Width="100%" DataKeyNames="id" Name="Parent" AllowMultiColumnSorting="true">
                                    <NoRecordsTemplate>
                                        <div class="emptyGridHolder">
                                            <asp:Literal ID="LtlNoDataFound" runat="server" />
                                        </div>
                                    </NoRecordsTemplate>
                                    <DetailTables>
                                        <telerik:GridTableView DataKeyNames="id" Name="CompanyItems" Width="100%">
                                            <Columns>
                                                <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                <telerik:GridBoundColumn HeaderText="Id" HeaderStyle-Width="30" DataField="connection_id" UniqueName="connection_id" />                                               
                                                <telerik:GridBoundColumn HeaderStyle-Width="120" HeaderText="Company Name" DataField="company_name" UniqueName="company_name" />                                                
                                                <telerik:GridBoundColumn HeaderStyle-Width="100" HeaderText="Country" DataField="country" UniqueName="country" />
                                                <telerik:GridBoundColumn HeaderStyle-Width="50" HeaderText="Email" DataField="email" UniqueName="email" />
                                                <telerik:GridBoundColumn Display="false" HeaderStyle-Width="20" HeaderText="Sysdate" DataField="sysdate" UniqueName="sysdate" />
                                                <telerik:GridBoundColumn Display="false" HeaderStyle-Width="20" HeaderText="Last Updated" DataField="last_updated" UniqueName="last_updated" />
                                                <telerik:GridBoundColumn HeaderStyle-Width="100" HeaderText="Period From" DataField="current_period_start" UniqueName="current_period_start" />
                                                <telerik:GridBoundColumn HeaderStyle-Width="100" HeaderText="Period End" DataField="current_period_end" UniqueName="current_period_end" />
                                                <telerik:GridTemplateColumn HeaderText="Status" HeaderStyle-Width="50" DataField="status" UniqueName="status">
                                                    <ItemTemplate>
                                                        <asp:Image ID="ImgStatus" Visible="false" runat="server" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderStyle-Width="80" DataField="actions" UniqueName="actions">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImgBtnNestedLoginAsCompany" OnClick="ImgBtnNestedLoginAsCompany" ImageUrl="~/Images/admin.png" runat="server" />  
                                                        <asp:ImageButton ID="ImgBtnNestedPreviewCompany" OnClick="ImgBtnNestedPreviewCompany_OnClick" ImageUrl="~/Images/customer.png" runat="server" />  
                                                        <a id="aNestedShowVerticals" onserverclick="ImgNestedShowVerticals_OnClick" title="Show company verticals" runat="server">
                                                            <i class="icon-arrow-right font-lg font-blue"></i>
                                                        </a>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderStyle-Width="20" HeaderText="Id" DataField="id" UniqueName="id" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="200" DataField="company_name" UniqueName="company_name" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="50" DataField="billing_type" UniqueName="billing_type" />                                            
                                        <telerik:GridBoundColumn HeaderStyle-Width="50" DataField="stripe_customer_id" UniqueName="stripe_customer_id" />                                            
                                        <telerik:GridBoundColumn HeaderStyle-Width="30" DataField="company_type" UniqueName="company_type" />
                                        <telerik:GridTemplateColumn HeaderStyle-Width="50" DataField="email" UniqueName="email">
                                            <ItemTemplate>
                                                <telerik:RadComboBox ID="RcbxEmail" Width="200" runat="server" />
                                                <asp:Label ID="LblEmail" runat="server" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="10" DataField="is_public" UniqueName="is_public" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="10" DataField="account_status" UniqueName="account_status" />                                        
                                        <telerik:GridBoundColumn HeaderStyle-Width="80" DataField="country" UniqueName="country" />                                        
                                        <telerik:GridTemplateColumn HeaderStyle-Width="80" DataField="actions" UniqueName="actions">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgBtnLoginAsCompany" OnClick="ImgBtnLoginAsCompany_OnClick" ImageUrl="~/Images/admin.png" runat="server" />  
                                                <asp:ImageButton ID="ImgBtnPreviewCompany" OnClick="ImgBtnPreviewCompany_OnClick" ImageUrl="~/Images/customer.png" runat="server" />
                                                <a id="aShowVerticals" onserverclick="ImgShowVerticals_OnClick" title="Show company verticals" runat="server">
                                                    <i class="icon-arrow-right font-lg font-blue"></i>
                                                </a>
                                                <asp:Image ID="ImgInfo" Visible="true" style="cursor:pointer;" ImageUrl="~/images/icons/small/info.png" Width="20" Height="20" runat="server" />
                                                <telerik:RadToolTip ID="RttImgInfo" Visible="true" TargetControlID="ImgInfo" Width="500" Title="Company Description" AutoCloseDelay="10000" HideEvent="ManualClose" Position="TopRight" Animation="Fade" runat="server" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />
                            <controls:MessageControl ID="UcStripeMessageAlert" Visible="false" runat="server" />
                        </asp:Panel>
                    </div>         
                </div>                
            </div>
            <!--/ middle -->
        </div>        
        <div id="loader" style="display:none;vertical-align:middle;
            position:fixed;
            left:48%;
            top:42%;
            background-color: #ffffff;    
            padding-top:20px;
            padding-bottom:20px;
            padding-left: 40px;
            padding-right:40px;
            border:1px solid #0d1f39;
            border-radius: 5px 5px 5px 5px;
            -moz-box-shadow: 1px 1px 10px 2px #aaa;
            -webkit-box-shadow: 1px 1px 10px 2px #aaa;
            box-shadow: 1px 1px 10px 2px #aaa;
            z-index:10000;">
            <div id="loadermsg" style="background-color:#ffffff;padding:10px;border-radius:5px;background-image:url(../Images/loading.gif);background-repeat:no-repeat;background-position:center center;">
            </div>
        </div>
    </telerik:RadAjaxPanel>

    <div id="Verticals" class="modal fade" tabindex="-1" data-width="500">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                            <h4 class="modal-title"><asp:Label ID="LblVerticalsTitle" Text="Company Verticals" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">                                   
                                    <div class="form-group">
                                        <asp:Label ID="LblVerticals" CssClass="control-label" runat="server" />
                                        <asp:TextBox ID="TbxVerticals" MaxLength="3995" ReadOnly="true" TextMode="MultiLine" Rows="10" CssClass="form-control" data-placement="top" runat="server" />
                                    </div>
                                </div>                            
                            </div>                    
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn dark btn-outline">Close</button>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        function ClosePopUp() {
            $('#Verticals').modal('hide');
        }
    </script>
    <script type="text/javascript">
        function OpenPopUp() {
            $('#Verticals').modal('show');
        }
    </script>
</asp:Content>
