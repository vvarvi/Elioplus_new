<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="AdminAddThirdPartyUsersPage.aspx.cs" Inherits="WdS.ElioPlus.AdminAddThirdPartyUsersPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="DashEditHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="DashEditMain" ContentPlaceHolderID="MainContent" runat="server">

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
        <!-- BEGIN PAGE BAR -->
        <div class="page-bar">
            <ul class="page-breadcrumb">
                <li><span>
                    <asp:Label ID="LblDashboard" runat="server" /></span> <i class="fa fa-circle"></i>
                </li>
                <li><span>
                    <asp:Label ID="LblDashPage" runat="server" /></span> </li>
            </ul>
        </div>
        <!-- END PAGE BAR -->
        <!-- BEGIN PAGE TITLE-->
        <h3 class="page-title">
            <asp:Label ID="LblElioplusDashboard" runat="server" />
            <small>
                <asp:Label ID="LblDashSubTitle" runat="server" /></small>
        </h3>
        <!-- END PAGE TITLE-->
        <div id="middle">
            <div class="clearfix">
                <div style="margin-top: 0px; text-align: center;">
                    <h2><asp:Label ID="Label9" Text="Insert Third Party Users" runat="server" /></h2>
                    <asp:Panel ID="PnlThirdparty" Style="margin-top: 20px; text-align: left; padding: 10px;margin: auto; display:inline-block;" runat="server">
                        <h2><asp:Label ID="Label8" runat="server" /></h2>
                        <div style="float:left; width:100%; display:inline-block;">
                            <table class="qsf-fb">
                                <tr class="step2-row">
                                    <td style="width: 195px;padding-top:20px;">
                                        <div>
                                            <asp:Label ID="LblUserId" runat="server" Text="ID" />
                                        </div>
                                    </td>
                                    <td>
                                        <div style="">
                                            <asp:TextBox ID="RtbxUserId" CssClass="form-control" Enabled="false" Width="300" runat="server" />
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblUserIdError" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="step2-row">
                                    <td style="width: 195px; padding-top:20px;">
                                        <div>
                                            <asp:Label ID="LblUsername" runat="server" Text="Username" />
                                        </div>
                                    </td>
                                    <td>
                                        <div style="">
                                            <asp:TextBox ID="RtbxUsername" CssClass="form-control" Width="300" runat="server" />
                                            <div style="color:#fd5840;">
                                                <asp:Label ID="LblUsernameError" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div style="margin-top: 20px;">
                                            <asp:ImageButton ID="LnkBtnGenerateUsername" OnClick="LnkBtnGenerateUsername_OnClick" ImageUrl="~/images/settings.png" runat="server" />
                                            <telerik:RadToolTip ID="RttGenerateUsername" TargetControlID="LnkBtnGenerateUsername" Position="TopRight" Animation="Fade" Text="Generate Username & Password" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                                <tr class="step2-row">
                                    <td style="width: 195px;padding-top:20px;">
                                        <div>
                                            <asp:Label ID="LblPassword" runat="server" Text="Password" />
                                        </div>
                                    </td>
                                    <td>
                                        <div style="">
                                            <asp:TextBox ID="RtbxPassword" CssClass="form-control" Width="300" runat="server" />
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblPasswordError" runat="server" />
                                            </div>                                            
                                        </div>
                                    </td>
                                    <td>
                                        <div style="margin-top: 20px;">
                                            <asp:ImageButton ID="LnkBtnGeneratePassword" Visible="false" OnClick="LnkBtnGeneratePassword_OnClick" ImageUrl="~/images/settings.png" runat="server" />
                                            <telerik:RadToolTip ID="RttGeneratePassword" Visible="false" TargetControlID="LnkBtnGeneratePassword" Position="TopRight" Animation="Fade" Text="Generate Password" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                                <tr class="step2-row">
                                    <td style="width: 195px;padding-top:20px;">
                                        <div>
                                            <asp:Label ID="LblType" runat="server" Text="Company Type" />
                                        </div>
                                    </td>
                                    <td>
                                        <div style="padding-top:10px;">
                                            <asp:DropDownList ID="RcbxCategory" Width="300" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="2" Text="Channel Partners" Selected="True" />
                                            </asp:DropDownList>
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblTypeError" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="step2-row">
                                    <td style="width: 195px; padding-top:20px;">
                                        <div>
                                            <asp:Label ID="Label3" runat="server" Text="Company Name" />
                                        </div>
                                    </td>
                                    <td>
                                        <div style="">
                                            <asp:TextBox ID="RtbxCompanyName" CssClass="form-control" MaxLength="100" Width="300" runat="server" />
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblCompanyNameError" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="step2-row">
                                    <td style="width: 195px;padding-top:20px;">
                                        <div>
                                            <asp:Label ID="Label2" runat="server" Text="Company Email" />
                                        </div>
                                    </td>
                                    <td class="step2-td2">
                                        <div style="">
                                            <asp:TextBox ID="RtbxEmail" CssClass="form-control" MaxLength="100" Width="300" runat="server" />
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblEmailError" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="step2-row">
                                    <td style="width: 195px;padding-top:20px;">
                                        <div>
                                            <asp:Label ID="Label4" runat="server" Text="Website" />
                                        </div>
                                    </td>
                                    <td class="step2-td2">
                                        <div style="">
                                            <asp:TextBox ID="RtbxWebSite" CssClass="form-control" EmptyMessage="http:// or https://" MaxLength="100" Width="300" runat="server" />
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblWebSiteError" runat="server" />
                                            </div>
                                            <div style="color:#57C373; padding:5px;">
                                                <asp:Label ID="LblWebSitePass" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div style="margin-top: 20px;">
                                            <asp:ImageButton ID="ImgBtnCheck" OnClick="ImgBtnCheck_OnClick" ImageUrl="~/images/icons/small/check_www_3.png" runat="server" />
                                            <telerik:RadToolTip ID="RttImgBtnCheckMessage" TargetControlID="ImgBtnCheck" Position="TopRight" Animation="Fade" Text="Check if domain is already registered" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                                <tr class="step2-row">
                                    <td style="width: 195px;padding-top:20px;">
                                        <div>
                                            <asp:Label ID="Label5" runat="server" Text="Country" />
                                        </div>
                                    </td>
                                    <td class="step2-td2">
                                        <div style="">
                                            <telerik:RadComboBox AutoPostBack="true" OnTextChanged="RcbxCountries_OnTextChanged" ID="RcbxCountries" Width="295" runat="server" />
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblCountryError" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="step2-row">
                                    <td style="width: 195px; padding-top:20px;">
                                        <div>
                                            <asp:Label ID="Label6" runat="server" Text="Address" />
                                        </div>
                                    </td>
                                    <td class="step2-td2">
                                        <div style="">
                                            <asp:TextBox ID="RtbxAddress" CssClass="form-control" MaxLength="350" Width="300" runat="server" />
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblAddressError" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="step2-row">
                                    <td style="width: 195px;padding-top:20px;">
                                        <div>
                                            <asp:Label ID="Label1" runat="server" Text="City" />
                                        </div>
                                    </td>
                                    <td class="step2-td2">
                                        <div style="">
                                            <asp:TextBox ID="RtbxCity" CssClass="form-control" MaxLength="100" Width="300" runat="server" />
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="Label10" runat="server" />
                                            </div>
                                            <div style="color:#57C373; padding:5px;">
                                                <asp:Label ID="LblCityError" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div style="margin-top: 20px;">
                                            <asp:ImageButton ID="ImgBtnGetCity" OnClick="ImgBtnGetCity_Click" ImageUrl="~/images/icons/small/move_right_3.png" runat="server" />
                                            <telerik:RadToolTip ID="RttGetCity" TargetControlID="ImgBtnGetCity" Position="TopRight" Animation="Fade" Text="Get city from address" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                                <tr class="step2-row">
                                    <td style="width: 195px;padding-top:20px;">
                                        <div>
                                            <asp:Label ID="LblState" runat="server" Text="State" />
                                        </div>
                                    </td>
                                    <td class="step2-td2">
                                        <div style="">
                                            <asp:TextBox ID="RtbxState" CssClass="form-control" MaxLength="100" Width="300" runat="server" />
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblStateError" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="step2-row">
                                    <td style="width: 195px;padding-top:20px;">
                                        <div>
                                            <asp:Label ID="Label7" runat="server" Text="Phone" />
                                        </div>
                                    </td>
                                    <td class="step2-td2">
                                        <div style="">
                                            <asp:TextBox ID="RtbxPhoneNumberPrefix" Width="50" ReadOnly="true" runat="server" />
                                            <asp:TextBox ID="RtbxPhone" onkeypress="return isNumberOnly(event);" MaxLength="15" Width="245" runat="server" />
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblPhoneError" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="step2-row">
                                    <td style="width: 195px;padding-top:20px;">
                                        <div>
                                            <asp:Label ID="LblLinkedin" runat="server" Text="Linkedin Url" />
                                        </div>
                                    </td>
                                    <td class="step2-td2">
                                        <div style="">
                                            <asp:TextBox ID="RtbxLinkedin" CssClass="form-control" MaxLength="100" Width="300" runat="server" />
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblLinkedinError" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="step2-row">
                                    <td style="width: 195px;">
                                        <div>
                                            <asp:Label ID="LblManagedServiceProvider" runat="server" Text="MSP" />
                                        </div>
                                    </td>
                                    <td class="step2-td2">
                                        <div style="float:left; margin-left:15px;">
                                            <asp:CheckBox ID="CbxManagedServiceProvider" runat="server" />
                                            <asp:Label ID="LblManagedServiceProviderTxt" runat="server" Text="Yes" />
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblManagedServiceProviderError" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="step2-row">
                                    <td style="width: 195px;">
                                        <div>
                                            <asp:Label ID="LblProducts" runat="server" Text="Products" />
                                        </div>
                                    </td>
                                    <td class="step2-td2">
                                        <div style="">
                                            <telerik:RadAutoCompleteBox RenderMode="Lightweight" runat="server" ID="RcbxProducts" Delimiter=" "
                                                Width="260" height="26" DataSourceID="AllProducts" AllowCustomEntry="true" DataTextField="description" DataValueField="id"
                                                Filter="StartsWith" TextSettings-SelectionMode="Single" InputType="Text" DropDownHeight="400"
                                                DropDownWidth="400" EmptyMessage="Start typing the name of the products you use">
                                                <DropDownItemTemplate>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="left" style="width: 400px; padding-left: 10px;">
                                                                <%# DataBinder.Eval(Container.DataItem, "description")%>                                                               
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </DropDownItemTemplate>
                                            </telerik:RadAutoCompleteBox>
                                            <asp:SqlDataSource ID="AllProducts" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                ProviderName="System.Data.SqlClient" SelectCommand="SELECT [id], [description] FROM [Elio_registration_products] WHERE [is_public] = 1 order by description">
                                            </asp:SqlDataSource>
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblProductsError" runat="server" />
                                            </div>
                                            <div class="col-md-6" style="padding-top:5px;">
                                                <strong>
                                                    <asp:TextBox ID="LblIntegrationProducts" CssClass="form-control" Width="260" TextMode="MultiLine" Rows="5" runat="server" />
                                                </strong>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="col-md-1" style="padding-top:5px;">
                                            <asp:ImageButton ID="ImgBtnAdd" OnClick="ImgBtnAdd_Click" runat="server" ImageUrl="~/images/icons/add_btn_1.png" />
                                        </div>
                                        <div class="col-md-1" style="padding-top:5px;">
                                            <asp:ImageButton Visible="false" ID="ImgBtnRemove" OnClick="ImgBtnRemove_Click" runat="server" ImageUrl="~/images/icons/small/error_1.png" />
                                        </div>
                                    </td>
                                </tr>
                            </table>                            
                        </div>
                        <div style="float:left; width:100%; display:inline-block;">
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
                                                            <div class="dash-bar-row-3" style="float:left;">   
                                                                <asp:CheckBoxList ID="cbSub39" TextAlign="left" runat="server">                            
                                                                </asp:CheckBoxList>
                                                            </div>
                                                            <div class="dash-bar-row-3" style="float:right;">    
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
                                                            <div class="dash-bar-row-3" style="float:left;">   
                                                                <asp:CheckBoxList ID="cbSub42" TextAlign="left" runat="server">                            
                                                                </asp:CheckBoxList>
                                                            </div>
                                                            <div class="dash-bar-row-3" style="float:right;">    
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
                                        <telerik:RadPanelItem Text="Hardware" runat="server">
                                            <Items>
                                                <telerik:RadPanelItem runat="server">
                                                    <ItemTemplate>
                                                        <div style="display: inline-block; width:100%;">
                                                            <div class="dash-bar-row-3" style="float:left;">   
                                                                <asp:CheckBoxList ID="cbSub45" TextAlign="left" runat="server">                            
                                                                </asp:CheckBoxList>
                                                            </div>
                                                            <div class="dash-bar-row-3" style="float:right;">    
                                                                <asp:CheckBoxList ID="cbSub46" TextAlign="left" runat="server">                            
                                                                </asp:CheckBoxList> 
                                                            </div>
                                                            <div class="dash-bar-row-16"> 
                                                                <asp:CheckBoxList ID="cbSub47" TextAlign="left" runat="server">                            
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
                        <div style="margin-top:10px; display:inline-block;"></div>
                        <controls:Messagecontrol id="UcMessage" visible="false" runat="server" />
                        <table class="qsf-fb">
                            <tr>
                                <td style="width: 150px;padding-top:20px;">
                                </td>
                                <td>
                                    <div style="">
                                        <asp:Button ID="RbtnSave" OnClick="RbtnSave_OnClick" Text="Save" CssClass="btn btn-primary" runat="server" /> 
                                        <asp:Button ID="RbtnClear" OnClick="RbtnClear_OnClick" Text="Clear" CssClass="btn btn-primary" runat="server" />
                                        <asp:Button ID="RbtnCancelEdit" OnClick="RbtnCancelEdit_OnClick" Text="Cancel" CssClass="btn btn-primary" Visible="false" runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="margin-bottom:20px;"></div>                    
                    <controls:messagecontrol id="UcMessageAlert" visible="false" runat="server" />
                    <controls:messagecontrol id="UcClearbitMessageAlert" visible="false" runat="server" />
                </div>
            </div>
        </div>
        <div id="loader" style="display:none;">
            <div id="loadermsg">
            </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
