<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="WdS.ElioPlus.AdminPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="DashboardHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Our platform allows IT vendors, resellers and developers to connect based on their interests and industry information." />
    <meta name="keywords" content="the best SaaS vendors, channel partnerships, IT channel community" />
</asp:Content>

<asp:Content ID="DashboardMain" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
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

            function CloseAddConnectionsPopUp() {
                $('#MdAddConnections').modal('hide');
            }

            function OpenConfDeleteUserPopUp() {
                $('#divConfirmDeletion').modal('show');
            }

            function CloseConfDeleteUserPopUp() {
                $('#divConfirmDeletion').modal('hide');
            }

            function OpenSearchConnectionPartnerModal() {
                $('#SearchConnectionPartnerModal').modal('show');
            }

            function CloseSearchConnectionPartnerModal() {
                $('#SearchConnectionPartnerModal').modal('hide');
            }

        </script>
    </telerik:RadScriptBlock>
    <telerik:RadAjaxPanel ID="RapPage" ClientEvents-OnRequestStart="RapPage_OnRequestStart" ClientEvents-OnResponseEnd="RapPage_OnResponseEnd" runat="server" RestoreOriginalRenderDelegate="false">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
                <div class="body_wrap">
                    <!-- header -->
                    <div class="header header_thin">
                    </div>
                    <div>
                        <div style="width: 50%; float: left; padding-top: 10px; padding-left: 50px">
                            <asp:Label ID="Label1" runat="server" ForeColor="White" />
                        </div>
                    </div>
                    <!--/ header -->
                    <!-- middle -->
                    <div id="middle">
                        <div class="clearfix">
                            <div style="margin-top: 50px; text-align: center;">
                                <h2>
                                    <asp:Label ID="Label5" runat="server" /></h2>
                                <asp:Panel ID="PnlElioCompaniesGrid" Style="margin-top: 20px; text-align: left; padding: 10px; margin: auto;" runat="server">
                                    <div style="margin-bottom: 50px; margin-top: 30px; position: relative;">
                                        <div class="admin-box-left">
                                            <div class="dash-header-css" style="text-align: center; background-color: #fd5840; border-bottom: 1px solid #fff; color: #fff; font-weight: bold; font-size: 20px;">
                                                <asp:Label ID="LblVendors" runat="server" />
                                            </div>
                                            <div class="txt-color" style="background-color: #f98d7d; color: #fff; border-bottom-right-radius: 5px; border-bottom-left-radius: 5px;">
                                                <div class="row" style="padding: 10px; font-size: 40px; text-align: center;">
                                                    <asp:Label ID="LblTotalVendors" CssClass="bold" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div style="margin-bottom: 20px;"></div>
                                        <div class="admin-box-left">
                                            <div class="dash-header-css" style="text-align: center; background-color: #94d7c1; border-bottom: 1px solid #fff; color: #fff; font-weight: bold; font-size: 20px;">
                                                <asp:Label ID="LblDevelopers" runat="server" />
                                            </div>
                                            <div class="txt-color" style="background-color: #bae9da; color: #fff; border-bottom-right-radius: 5px; border-bottom-left-radius: 5px;">
                                                <div class="row" style="padding: 10px; font-size: 40px; text-align: center;">
                                                    <asp:Label ID="LblTotalDevelopers" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div style="margin-bottom: 20px;"></div>
                                        <div class="admin-box-left">
                                            <div class="dash-header-css" style="text-align: center; background: #0266a3; color: #fff; font-weight: bold; border-bottom: 1px solid #fff; font-size: 20px;">
                                                <asp:Label ID="LblTResellers" runat="server" />
                                            </div>
                                            <div class="txt-color" style="background-color: #49a4db; color: #fff; border-bottom-right-radius: 5px; border-bottom-left-radius: 5px;">
                                                <div class="row" style="padding: 10px; font-size: 40px; text-align: center;">
                                                    <asp:Label ID="LblTotalResellers" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div style="margin-bottom: 20px;"></div>
                                        <div class="admin-box-left">
                                            <div class="dash-header-css" style="text-align: center; background: #0266a3; color: #fff; font-weight: bold; border-bottom: 1px solid #fff; font-size: 20px;">
                                                <asp:Label ID="LblTThirdPartyResellers" runat="server" />
                                            </div>
                                            <div class="txt-color" style="background-color: #49a4db; color: #fff; border-bottom-right-radius: 5px; border-bottom-left-radius: 5px;">
                                                <div class="row" style="padding: 10px; font-size: 40px; text-align: center;">
                                                    <asp:Label ID="LblTotalThirdPartyResellers" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div style="margin-bottom: 20px;"></div>
                                        <div class="admin-box-left">
                                            <div class="dash-header-css" style="text-align: center; background-color: #ecba22; color: #fff; font-weight: bold; border-bottom: 1px solid #fff; font-size: 20px;">
                                                <asp:Label ID="LblRegistered" runat="server" />
                                            </div>
                                            <div class="txt-color" style="background-color: #efd278; color: #fff; border-bottom-right-radius: 5px; border-bottom-left-radius: 5px;">
                                                <div class="row" style="padding: 10px; font-size: 40px; text-align: center;">
                                                    <asp:Label ID="LblTotalRegistered" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div style="margin-bottom: 20px;"></div>
                                        <div class="admin-box-left">
                                            <div class="dash-header-css" style="text-align: center; background-color: #3dc98c; color: #fff; font-weight: bold; border-bottom: 1px solid #fff; font-size: 20px;">
                                                <asp:Label ID="LblNotRegistered" runat="server" />
                                            </div>
                                            <div class="txt-color" style="background-color: #49e5a1; color: #fff; border-bottom-right-radius: 5px; border-bottom-left-radius: 5px;">
                                                <div class="row" style="padding: 10px; font-size: 40px; text-align: center;">
                                                    <asp:Label ID="LblTotalNotRegistered" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:Panel ID="PnlSearch" Style="border-radius: 5px; background-color: #F0F3CD; margin-bottom: 10px; padding: 30px 10px 30px 10px; margin: auto; position: relative;" runat="server">

                                        <table>
                                            <tr>
                                                <td style="width: 70px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="Label6" CssClass="label_title" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadComboBox ID="RcbxCategory" Width="300" runat="server" />
                                                    </div>
                                                </td>
                                                <td style="width: 270px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="LblStatus" CssClass="label_title" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadComboBox ID="RcbxStatus" Width="300" runat="server" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="LblIsPublic" CssClass="label_title" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadComboBox ID="RcbxIsPublic" Width="300" runat="server" />
                                                    </div>
                                                </td>
                                                <td style="width: 150px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="Label8" CssClass="label_title" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadComboBox ID="RcbxVendorsCompanyName" Width="300" Height="210" runat="server" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 70px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="Label2" CssClass="label_title" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadComboBox ID="RcbxBillingType" Width="300" runat="server" />
                                                    </div>
                                                </td>
                                                <td style="width: 150px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="LblResellersCompanyName" CssClass="label_title" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadComboBox ID="RcbxResellersCompanyName" Width="300" runat="server" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 130px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="Label3" CssClass="label_title" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadTextBox ID="RtbxUserId" MaxLength="30" Width="300" runat="server" />
                                                    </div>
                                                </td>
                                                <td style="width: 70px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="LblThirdPartyCompanyName" CssClass="label_title" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadComboBox ID="RcbxThirdPartyCompanyName" Width="300" runat="server" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 130px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="Label10" CssClass="label_title" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadTextBox ID="RtbxCompanyEmail" Width="300" runat="server" />
                                                    </div>
                                                </td>
                                                <td style="width: 70px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="Label9" CssClass="label_title" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadTextBox ID="RtbxCompanyName" MaxLength="30" Width="300" runat="server" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 130px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="LblAddRole" CssClass="label_title" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadTextBox ID="RtbxUserToAssignRole" onkeypress="return isNumberOnly(event);" MaxLength="10" Width="80" runat="server" />
                                                        <div style="vertical-align: middle; float: right; margin-top: 10px;">
                                                            <asp:ImageButton ID="ImgGetUserRoles" OnClick="ImgGetUserRoles_OnClick" ToolTip="Load user roles" ImageUrl="~/Images/icons/small/usr_roles.png" runat="server" />
                                                            <asp:ImageButton ID="ImgBtnSave" ToolTip="Save selected roles to user" OnClick="ImgBtnSave_OnClick" ImageUrl="~/Images/icons/small/usr_role_add.png" runat="server" />
                                                        </div>
                                                    </div>
                                                </td>
                                                <td style="width: 130px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="Label4" CssClass="label_title" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px; width: 145px;">
                                                        <telerik:RadTextBox ID="RtbxStripeCustomerId" Width="300" runat="server" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 130px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="LblRoles" CssClass="label_title" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <asp:CheckBoxList ID="CbxRoles" runat="server" />
                                                    </div>
                                                </td>
                                                <td style="width: 70px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="Label7" CssClass="label_title" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadComboBox ID="RcbxApplicationType" Width="300" runat="server" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 130px;">
                                                    <div style="padding: 5px;">
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                    </div>
                                                </td>
                                                <td style="width: 70px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="Label16" CssClass="label_title" Text="Reg Date From" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadDatePicker ID="RdpRegDateFrom" Width="300" runat="server" />
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 130px;">
                                                    <div style="padding: 5px;">
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                    </div>
                                                </td>
                                                <td style="width: 70px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="Label18" CssClass="label_title" Text="Reg Date To" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadDatePicker ID="RdpRegDateTo" Width="300" runat="server" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="margin-top: 10px;"></div>
                                        <table>
                                            <tr>
                                                <td style="width: 130px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="LblPacketStatusUserId" CssClass="label_title" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadTextBox ID="RtbxPacketStatusUserId" onkeypress="return isNumberOnly(event);" MaxLength="10" Width="70" runat="server" />
                                                    </div>
                                                </td>
                                                <td style="width: 70px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="LblPacketStatusConnections" CssClass="label_title" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadTextBox ID="RtbxPacketStatusConnections" onkeypress="return isNumberOnly(event);" MaxLength="10" Width="50" runat="server" />
                                                    </div>
                                                </td>
                                                <td style="width: 70px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="LblStartingDate" CssClass="label_title" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadDatePicker ID="RdpStartingDate" Width="120" runat="server" />
                                                    </div>
                                                </td>
                                                <td style="width: 70px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="LblExpirationDate" CssClass="label_title" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadDatePicker ID="RdpExpirationDate" Width="120" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadButton ID="RbtnAddConnections" Width="150" Style="text-align: center;" OnClick="RbtnAddConnections_OnClick" runat="server">
                                                            <ContentTemplate>
                                                                <span>
                                                                    <asp:Label ID="LblAddConnectionsText" Text="Add" runat="server" />
                                                                </span>
                                                            </ContentTemplate>
                                                        </telerik:RadButton>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="margin-top: 10px;"></div>
                                        <table>
                                            <tr>
                                                <td style="width: 130px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="Label15" Text="From ID" CssClass="label_title" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadTextBox ID="RtbxFromUserID" onkeypress="return isNumberOnly(event);" MaxLength="10" Width="100" runat="server" />
                                                    </div>
                                                </td>
                                                <td style="width: 70px;">
                                                    <div style="padding: 5px;">
                                                        <asp:Label ID="Label17" Text="To ID" CssClass="label_title" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadTextBox ID="RtbxToUserID" onkeypress="return isNumberOnly(event);" MaxLength="10" Width="100" runat="server" />
                                                    </div>
                                                </td>
                                                <td style="width: 70px;">
                                                    <div style="padding: 5px;">
                                                        <telerik:RadButton ID="RtbxUpdateUsers" Style="text-align: center;" OnClick="RtbxUpdateUsers_Click" runat="server">
                                                            <ContentTemplate>
                                                                <span>
                                                                    <asp:Label ID="LblUpdateUsersText" Text="Update Address/City" runat="server" />
                                                                </span>
                                                            </ContentTemplate>
                                                        </telerik:RadButton>
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                        <telerik:RadButton ID="RtbxClearUsersAddressCity" Style="text-align: center;" OnClick="RtbxClearUsersAddressCity_Click" runat="server">
                                                            <ContentTemplate>
                                                                <span>
                                                                    <asp:Label ID="LblClearUsersAddressCityText" Text="Clear" runat="server" />
                                                                </span>
                                                            </ContentTemplate>
                                                        </telerik:RadButton>
                                                    </div>
                                                </td>
                                                <td style="width: 70px;">
                                                    <div style="padding: 5px;">
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                    </div>
                                                </td>
                                                <td>
                                                    <div style="padding: 5px;">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="margin-top: 20px;"></div>
                                        <div style="margin-bottom: 10px;"></div>
                                        <table>
                                            <tr>
                                                <td style="width: 135px;"></td>
                                                <td>
                                                    <telerik:RadButton ID="RbtnSearch" Width="135" Style="text-align: center;" OnClick="RbtnSearch_OnClick" runat="server">
                                                        <ContentTemplate>
                                                            <span>
                                                                <asp:Label ID="LblSearchText" runat="server" />
                                                            </span>
                                                        </ContentTemplate>
                                                    </telerik:RadButton>
                                                    <telerik:RadButton ID="RbtnReset" Width="135" Style="text-align: center;" OnClick="RbtnReset_OnClick" runat="server">
                                                        <ContentTemplate>
                                                            <span>
                                                                <asp:Label ID="LblResetText" runat="server" />
                                                            </span>
                                                        </ContentTemplate>
                                                    </telerik:RadButton>
                                                    <telerik:RadButton ID="RbtnGetClearbitData" Style="text-align: center;" OnClick="RbtnGetClearbitData_OnClick" runat="server">
                                                        <ContentTemplate>
                                                            <span>
                                                                <asp:Label ID="LblGetClearbitDataText" runat="server" />
                                                            </span>
                                                        </ContentTemplate>
                                                    </telerik:RadButton>
                                                </td>
                                            </tr>
                                        </table>
                                        <controls:MessageControl ID="UcMessageControlCriteria" Visible="false" runat="server" />
                                        <div class="row"></div>
                                    </asp:Panel>
                                    <div style="display: inline-block; overflow-x: scroll; width: 100%;">
                                        <telerik:RadGrid ID="RdgElioUsers" Style="margin: auto; position: relative;" AllowPaging="true" AllowSorting="true" HeaderStyle-BackColor="#39b4ef" PagerStyle-Position="TopAndBottom" HeaderStyle-ForeColor="#ffffff" PageSize="50" Width="100%" CssClass="rgdd" OnDetailTableDataBind="RdgElioUsers_DetailTableDataBind" OnItemDataBound="RdgElioUsers_OnItemDataBound" OnNeedDataSource="RdgElioUsers_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                            <MasterTableView Width="100%" DataKeyNames="id" Name="Parent" AllowMultiColumnSorting="true">
                                                <NoRecordsTemplate>
                                                    <div class="emptyGridHolder">
                                                        <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                    </div>
                                                </NoRecordsTemplate>
                                                <DetailTables>
                                                    <telerik:GridTableView PageSize="100" DataKeyNames="id" Name="CompanyItems" Width="100%">
                                                        <Columns>
                                                            <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                            <telerik:GridBoundColumn HeaderText="Id" HeaderStyle-Width="20" DataField="connection_id" UniqueName="connection_id" />
                                                            <telerik:GridTemplateColumn HeaderText="Select" HeaderStyle-Width="30" DataField="select" UniqueName="select">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CbxSelectUser" AutoPostBack="true" OnCheckedChanged="CbxSelectUser_OnCheckedChanged" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="80" HeaderText="Company Name" DataField="company_name" UniqueName="company_name" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="80" HeaderText="Application Type" DataField="user_application_type" UniqueName="user_application_type" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="50" HeaderText="Country" DataField="country" UniqueName="country" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="80" HeaderText="Email" DataField="email" UniqueName="email" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="50" HeaderText="Period From" DataField="current_period_start" UniqueName="current_period_start" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="50" HeaderText="Period End" DataField="current_period_end" UniqueName="current_period_end" />
                                                            <telerik:GridTemplateColumn HeaderText="Status" HeaderStyle-Width="20" DataField="status" UniqueName="status">
                                                                <ItemTemplate>
                                                                    <asp:Image ID="ImgStatus" AlternateText="status" Visible="false" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </telerik:GridTableView>
                                                </DetailTables>
                                                <Columns>
                                                    <telerik:GridBoundColumn HeaderStyle-Width="20" HeaderText="Id" DataField="id" UniqueName="id" />
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="200" DataField="company_name" UniqueName="company_name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblName" runat="server" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="50" DataField="billing_type" UniqueName="billing_type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblBillingType" runat="server" />                                                           
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn HeaderStyle-Width="50" DataField="country" UniqueName="country" />
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="50" HeaderText="State" DataField="state" UniqueName="state">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblState" runat="server" />
                                                            <telerik:RadTextBox ID="RtbxState" Visible="false" runat="server" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="50" HeaderText="City" DataField="city" UniqueName="city">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblCity" runat="server" />
                                                            <telerik:RadTextBox ID="RtbxCity" Visible="false" runat="server" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="50" DataField="stripe_customer_id" UniqueName="stripe_customer_id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblStripeCustomerId" runat="server" />
                                                            <telerik:RadTextBox ID="RtbxStripeCustomerId" Visible="false" runat="server" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="30" DataField="company_type" UniqueName="company_type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblCategory" runat="server" />
                                                            <telerik:RadComboBox ID="RcbxCategory" Visible="false" Width="120" runat="server" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="30" DataField="email" UniqueName="email">
                                                        <ItemTemplate>
                                                            <telerik:RadComboBox ID="RcbxEmail" Width="200" runat="server" />
                                                            <asp:Label ID="LblEmail" runat="server" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="30" DataField="is_public" UniqueName="is_public">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblPublic" runat="server" />
                                                            <telerik:RadComboBox ID="RcbxPublic" Visible="false" Width="60" runat="server">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Value="1" Text="Yes" runat="server" />
                                                                    <telerik:RadComboBoxItem Value="0" Text="No" runat="server" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="10" DataField="account_status" UniqueName="account_status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblStatus" runat="server" />
                                                            <telerik:RadComboBox ID="RcbxStatus" Visible="false" Width="140" runat="server">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Value="0" Text="NotCompleted" runat="server" />
                                                                    <telerik:RadComboBoxItem Value="1" Text="Completed" runat="server" />
                                                                    <telerik:RadComboBoxItem Value="2" Text="Deleted" runat="server" />
                                                                    <telerik:RadComboBoxItem Value="3" Text="Blocked" runat="server" />
                                                                    <telerik:RadComboBoxItem Value="4" Text="Active" runat="server" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn HeaderStyle-Width="50" DataField="available_connections_count" UniqueName="available_connections_count" />
                                                    <telerik:GridBoundColumn HeaderStyle-Width="150" DataField="last_login" UniqueName="last_login" />
                                                    <telerik:GridBoundColumn HeaderStyle-Width="5" DataField="login_times" UniqueName="login_times" />
                                                    <telerik:GridBoundColumn DataField="sysdate" UniqueName="sysdate" />
                                                    <telerik:GridTemplateColumn HeaderStyle-Width="60" DataField="actions" UniqueName="actions">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImgBtnEditCompany" OnClick="ImgBtnEditCompany_OnClick" ImageUrl="~/Images/edit.png" runat="server" />
                                                            <asp:ImageButton ID="ImgBtnCancel" Visible="false" OnClick="ImgBtnCancel_OnClick" ImageUrl="~/Images/cancel.png" runat="server" />
                                                            <asp:ImageButton ID="ImgBtnSaveChanges" Visible="false" OnClick="ImgBtnSaveChanges_OnClick" ImageUrl="~/Images/save.png" runat="server" />
                                                            <asp:ImageButton ID="ImgBtnLoginAsCompany" OnClick="ImgBtnLoginAsCompany_OnClick" ImageUrl="~/Images/admin.png" runat="server" />
                                                            <asp:ImageButton ID="ImgBtnPreviewCompany" OnClick="ImgBtnPreviewCompany_OnClick" ImageUrl="~/Images/customer.png" runat="server" />
                                                            <asp:ImageButton ID="ImgBtnSendEmail" OnClick="ImgBtnSendEmail_OnClick" runat="server" />
                                                            <asp:ImageButton ID="ImgBtnGetClearBitData" Width="20" Height="20" OnClick="ImgBtnGetClearBitData_OnClick" runat="server" />
                                                            <asp:ImageButton ID="ImgBtnDeleteUser" ToolTip="Delete user" ImageUrl="~/images/icons/small/delete.png" OnClick="ImgBtnDeleteUser_Click" runat="server" />
                                                            <asp:Image ID="ImgInfo" Visible="true" Style="cursor: pointer;" AlternateText="info" ImageUrl="~/images/icons/small/info.png" Width="20" Height="20" runat="server" />
                                                            <telerik:RadToolTip ID="RttImgInfo" Visible="true" TargetControlID="ImgInfo" Width="500" Title="Company Description" AutoCloseDelay="10000" HideEvent="ManualClose" Position="TopRight" Animation="Fade" runat="server" />
                                                            <a id="aSearchConnectionPartner" href="#SearchConnectionPartnerModal" role="button" data-toggle="modal" runat="server">
                                                                <asp:Image ID="ImgSearchConnectionPartner" AlternateText="search connection partner" ToolTip="Search connection" ImageUrl="~/images/preview.png" runat="server" />
                                                            </a>
                                                            <asp:ImageButton ID="ImgBtnUpdateSubscription" ToolTip="Update user subscription" ImageUrl="~/images/icons/small/add_task_1.png" OnClick="ImgBtnUpdateSubscription_Click" runat="server" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                    <div style="min-height: 50px; text-align: center; margin-top: 20px;">
                                        <controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />
                                        <controls:MessageControl ID="UcStripeMessageAlert" Visible="false" runat="server" />
                                        <controls:MessageControl ID="UcMessageAlertWarning" Visible="false" runat="server" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <!--/ middle -->
                </div>
                <div id="loader" style="display: none; vertical-align: middle; position: fixed; left: 48%; top: 42%; background-color: #ffffff; padding-top: 20px; padding-bottom: 20px; padding-left: 40px; padding-right: 40px; border: 1px solid #0d1f39; border-radius: 5px 5px 5px 5px; -moz-box-shadow: 1px 1px 10px 2px #aaa; -webkit-box-shadow: 1px 1px 10px 2px #aaa; box-shadow: 1px 1px 10px 2px #aaa; z-index: 10000;">
                    <div id="loadermsg" style="background-color: #ffffff; padding: 10px; border-radius: 5px; background-image: url(../Images/loading.gif); background-repeat: no-repeat; background-position: center center;">
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </telerik:RadAjaxPanel>

    <div id="divConfirmDeletion" class="modal fade" tabindex="-1" data-width="300">
        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                            <h4 class="modal-title">
                                <asp:Label ID="LblConfTitle" Text="Delete User Confirmation" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="LblConfMsg" CssClass="control-label" runat="server" />
                                        <asp:HiddenField ID="HdnUserId" Value="0" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="BtnBack" Text="Back" OnClick="BtnBack_Click" CssClass="btn dark btn-outline" runat="server" />
                            <asp:Button ID="BtnDeleteConfirm" OnClick="BtnDeleteConfirm_Click" CssClass="btn red-sunglo" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div id="SearchConnectionPartnerModal" class="modal fade" tabindex="-1">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">X</button>
                            <h4 class="modal-title">
                                <asp:Label ID="Label11" Text="Search Partner with User Criteria" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <table>
                                <tr>
                                    <td style="width: 105px;">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:Label ID="LblConnectionUserId" Text="Partner ID" CssClass="control-label" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="row" style="margin-bottom: -15px;">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <telerik:RadTextBox ID="TbxConnectionUserId" Width="380" runat="server" />
                                                    <asp:Label ID="Label12" Text="more than one ID with comma(,) seperated" Style="color: #000000; font-style: italic; font-size: 12px;" CssClass="control-label" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <a id="aBtnAddById" runat="server" onserverclick="aBtnAddById_ServerClick" title="Attach user connections by ids" class="btn btn-icon-only blue">
                                                    <i class="fa fa-plus"></i>
                                                </a>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="row" style="margin-top: 10px; margin-bottom: -15px;">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:Label ID="LblConnectionCompanyName" Text="Partner Name" CssClass="control-label" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="row" style="margin-top: 10px; margin-bottom: -15px;">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <telerik:RadTextBox ID="TbxConnectionCompanyName" Width="380" runat="server" />
                                                    <asp:Label ID="Label13" Text="more than one Name with comma(,) seperated" Style="color: #000000; font-style: italic; font-size: 12px;" CssClass="control-label" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <a id="aBtnAddByCompanyName" runat="server" title="Attach user connections by company names" onserverclick="aBtnAddByCompanyName_ServerClick" class="btn btn-icon-only blue">
                                                    <i class="fa fa-plus"></i>
                                                </a>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="row" style="margin-top: 10px; margin-bottom: -15px;">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:Label ID="LblConnectionCompanyEmail" Text="Partner Email" CssClass="control-label" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="row" style="margin-top: 10px; margin-bottom: -15px;">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <telerik:RadTextBox ID="TbxConnectionCompanyEmail" Width="380" runat="server" />
                                                    <asp:Label ID="Label14" Text="more than one Email with comma(,) seperated" Style="color: #000000; font-style: italic; font-size: 12px;" CssClass="control-label" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <a id="aBtnAddByCompanyEmail" runat="server" title="Attach user connections by emails" onserverclick="aBtnAddByCompanyEmail_ServerClick" class="btn btn-icon-only blue">
                                                    <i class="fa fa-plus"></i>
                                                </a>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <div class="row">
                                <controls:MessageControl ID="UcMessageControlAddConnections" Visible="false" runat="server" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="BtnCloseSearchConnectionPartnerModal" Text="Close" OnClick="BtnCloseSearchConnectionPartnerModal_Click" CssClass="btn btn-light-primary" runat="server" />
                            <asp:Button ID="BtnClear" Text="Clear" OnClick="BtnClear_Click" CssClass="btn btn-primary" runat="server" />
                            <asp:Button ID="BtnSearchConnectionPartner" Text="Done" OnClick="BtnSearchConnectionPartner_Click" CssClass="btn btn-danger" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
