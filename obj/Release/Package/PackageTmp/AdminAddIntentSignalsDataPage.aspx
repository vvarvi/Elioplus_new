<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="AdminAddIntentSignalsDataPage.aspx.cs" Inherits="WdS.ElioPlus.AdminAddIntentSignalsDataPage" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="DashEditHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="DashEditMain" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <link rel="stylesheet" type="text/css" href="style.css" />
        <script type="text/javascript">

            function isNumberOnly(evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;

                return true;
            }
        </script>
    </telerik:RadScriptBlock>

    <telerik:RadAjaxPanel ID="RapPage" runat="server" RestoreOriginalRenderDelegate="false">
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
                    <h2>
                        <asp:Label ID="Label9" Text="Insert Intent Signals Data" runat="server" /></h2>
                    <asp:Panel ID="PnlAddIntentSignalsData" Style="margin-top: 20px; text-align: left; padding: 10px; margin: auto; display: inline-block;" runat="server">
                        <h2>
                            <asp:Label ID="Label8" runat="server" /></h2>
                        <div style="float: left; width: 100%; display: inline-block;">
                            <table class="">
                                <tr class="step2-row">
                                    <td style="padding: 10px;">
                                        <div>
                                            <asp:Label ID="LblType" runat="server" Text="Type" />
                                        </div>
                                    </td>
                                    <td>
                                        <div style="padding-left: 10px;">
                                            <asp:DropDownList ID="RcbxType" Width="300" Height="42" runat="server">
                                                <asp:ListItem Value="0" Text="Select One" Selected="True" />
                                                <asp:ListItem Value="1" Text="Quote" />
                                                <asp:ListItem Value="2" Text="Intent Data" />
                                            </asp:DropDownList>
                                            <div style="color: #fd5840; padding: 5px;">
                                                <asp:Label ID="LblTypeError" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="step2-row">
                                    <td style="padding: 10px;">
                                        <div>
                                            <asp:Label ID="Label5" runat="server" Text="Country" />
                                        </div>
                                    </td>
                                    <td class="step2-td2">
                                        <div style="padding-left: 10px;">
                                            <asp:DropDownList ID="RcbxCountries" Width="300" Height="42" runat="server" />
                                            <div style="color: #fd5840; padding: 5px;">
                                                <asp:Label ID="LblCountryError" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="step2-row">
                                    <td style="padding: 10px; padding-top: 20px;">
                                        <div>
                                            <asp:Label ID="Label3" runat="server" Text="City" />
                                        </div>
                                    </td>
                                    <td>
                                        <div style="padding-left: 15px;">
                                            <asp:TextBox ID="TbxCity" MaxLength="100" Width="300" runat="server" />
                                            <div style="color: #fd5840; padding: 5px;">
                                                <asp:Label ID="LblCityError" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="step2-row">
                                    <td style="padding: 10px; padding-top: 20px;">
                                        <div>
                                            <asp:Label ID="LblProducts" runat="server" Text="Products" />
                                        </div>
                                    </td>
                                    <td class="step2-td2">
                                        <div style="padding-left: 15px;">
                                            <asp:TextBox ID="TbxProducts" Width="300" runat="server" />
                                            <div style="color: #fd5840; padding: 5px;">
                                                <asp:Label ID="LblProductsError" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr class="step2-row">
                                    <td style="padding-top: 20px;">
                                        <div>
                                            <asp:Label ID="Label2" runat="server" Text="NUmber of Users" />
                                        </div>
                                    </td>
                                    <td class="step2-td2">
                                        <div style="padding: 10px;">
                                            <asp:TextBox ID="TbxUsersCount" TextMode="Number" MaxLength="100" Width="300" Height="42" runat="server" />
                                            <div style="color: #fd5840; padding: 5px;">
                                                <asp:Label ID="LblUsersCountError" runat="server" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div style="margin-top: 10px; display: inline-block;"></div>
                        <controls:MessageControl ID="UcMessage" Visible="false" runat="server" />
                        <table class="">
                            <tr>
                                <td style="width: 120px; padding: 10px;"></td>
                                <td>
                                    <div style="padding: 10px;">
                                        <telerik:RadButton ID="RbtnSave" OnClick="RbtnSave_OnClick" Width="135" Style="text-align: center;" runat="server">
                                            <ContentTemplate>
                                                <span>
                                                    <asp:Label ID="LblSaveText" Text="Save" runat="server" />
                                                </span>
                                            </ContentTemplate>
                                        </telerik:RadButton>
                                        <telerik:RadButton ID="RbtnClear" OnClick="RbtnClear_OnClick" Width="135" Style="text-align: center;" runat="server">
                                            <ContentTemplate>
                                                <span>
                                                    <asp:Label ID="LblClearText" Text="Clear" runat="server" />
                                                </span>
                                            </ContentTemplate>
                                        </telerik:RadButton>
                                        <telerik:RadButton ID="RbtnCancelEdit" Visible="false" Width="135" Style="text-align: center;" runat="server">
                                            <ContentTemplate>
                                                <span>
                                                    <asp:Label ID="LblClearText" Text="Cancel Edit" runat="server" />
                                                </span>
                                            </ContentTemplate>
                                        </telerik:RadButton>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div style="margin-bottom: 20px;"></div>
                    <div id="divData" runat="server">
                        <table class="table domain-table">
                            <thead>
                                <tr class="rt-light-gray">
                                    <th class="text-323639 rt-strong f-size-18">Type</th>
                                    <th class="text-323639 rt-strong f-size-18">Country</th>
                                    <th class="text-323639 rt-strong f-size-18">City</th>
                                    <th class="text-323639 rt-strong f-size-18">Product/Technology</th>
                                    <th class="text-323639 rt-strong f-size-18">Number of users</th>
                                </tr>
                            </thead>
                            <asp:Repeater ID="RdgIntentData" OnLoad="RdgIntentData_OnNeedDataSource" runat="server">
                                <ItemTemplate>
                                    <tbody>
                                        <tr>
                                            <td class="f-size-18 f-size-md-18 rt-semiblod text-eb7"><%# DataBinder.Eval(Container.DataItem, "type")%></td>
                                            <td class="f-size-18 f-size-md-18 rt-semiblod text-605"><%# DataBinder.Eval(Container.DataItem, "country")%></td>
                                            <td class="f-size-18 f-size-md-18 rt-semiblod text-338"><%# DataBinder.Eval(Container.DataItem, "city")%></td>
                                            <td class="f-size-18 f-size-md-18 rt-semiblod primary-color"><%# DataBinder.Eval(Container.DataItem, "product")%></td>
                                            <td class="text-center"><a href="#" class="rt-btn rt-gradient2 rt-sm4 pill"><%# DataBinder.Eval(Container.DataItem, "users_count")%></a></td>
                                        </tr>
                                    </tbody>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />
                </div>
            </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
