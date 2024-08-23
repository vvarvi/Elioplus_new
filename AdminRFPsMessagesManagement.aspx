<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="AdminRFPsMessagesManagement.aspx.cs" Inherits="WdS.ElioPlus.Management.AdminRFPsMessagesManagement" %>

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
        </script>

    </telerik:RadScriptBlock>

    <telerik:RadAjaxPanel ID="RapPage" ClientEvents-OnRequestStart="RapPage_OnRequestStart" ClientEvents-OnResponseEnd="RapPage_OnResponseEnd" runat="server" RestoreOriginalRenderDelegate="false">
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

                            <asp:Panel ID="PnlSearch" Style="border-radius: 5px; background-color: #F0F3CD; margin-bottom: 10px; padding: 30px 10px 30px 10px; margin: auto; position: relative;" runat="server">

                                <table>
                                    <tr>
                                        <td style="width: 70px;">
                                            <div style="padding: 5px;">
                                                <asp:Label ID="Label9" CssClass="label_title" runat="server" />

                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding: 5px;">
                                                <asp:TextBox ID="RtbxCompanyName" MaxLength="30" Width="300" runat="server" />
                                            </div>
                                        </td>
                                        <td style="width: 70px;">
                                            <div style="padding: 5px;">
                                                <asp:Label ID="Label10" CssClass="label_title" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding: 5px;">
                                                <asp:TextBox ID="RtbxCompanyEmail" Width="300" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 130px;">
                                            <div style="padding: 5px;">
                                                <asp:Label ID="Label2" CssClass="label_title" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding: 5px;">
                                                <telerik:RadComboBox ID="RcbxIsPublic" Width="300" runat="server" />
                                            </div>
                                        </td>
                                        <td style="width: 130px;">
                                            <div style="padding: 5px;">
                                                <asp:Label ID="Label4" CssClass="label_title" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding: 5px;">
                                                <telerik:RadComboBox ID="RcbxApproved" Width="300" runat="server">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Select request status" Value="-2" />
                                                        <telerik:RadComboBoxItem Text="Approved" Value="1" />
                                                        <telerik:RadComboBoxItem Text="Not Approved" Value="0" />
                                                        <telerik:RadComboBoxItem Text="Rejected" Value="-1" />
                                                    </Items>
                                                </telerik:RadComboBox>
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
                                        </td>
                                    </tr>
                                </table>
                                <div class="row"></div>
                            </asp:Panel>
                            <div class="row" style="width: 100%;">
                                <controls:MessageControl ID="UcMessageControlCriteria" Visible="false" runat="server" />
                            </div>
                            <div class="col-md-12" style="margin-top: 50px;">
                                <div class="tabbable-line tabbable-full-width">
                                    <ul class="nav nav-tabs">
                                        <li class="active">
                                            <a href="#tab_1_1" data-toggle="tab">
                                                <asp:Label ID="LblPendingRequests" Text="Pending RFQs requests" runat="server" /></a>
                                        </li>
                                        <li>
                                            <a href="#tab_1_2" data-toggle="tab">
                                                <asp:Label ID="LblApprovedRequests" Text="Approved RFQs requests" runat="server" /></a>
                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <!--tab_1_1-->
                                        <div class="tab-pane active" id="tab_1_1">
                                            <div class="col-md-12">
                                                <telerik:RadGrid ID="RdgElioUsersDemoRequests" Style="margin: auto; position: relative;" AllowPaging="true" AllowSorting="false" HeaderStyle-BackColor="#39b4ef" PagerStyle-Position="TopAndBottom" HeaderStyle-ForeColor="#ffffff" PageSize="10" Width="100%" CssClass="rgdd" OnNeedDataSource="RdgElioUsersDemoRequests_OnNeedDataSource" OnItemDataBound="RdgElioUsersDemoRequests_OnItemDataBound" AutoGenerateColumns="false" runat="server">
                                                    <MasterTableView Width="100%" DataKeyNames="id" Name="Parent" AllowMultiColumnSorting="true">
                                                        <NoRecordsTemplate>
                                                            <div class="emptyGridHolder">
                                                                <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                            </div>
                                                        </NoRecordsTemplate>
                                                        <Columns>
                                                            <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="id" UniqueName="id" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="120" HeaderText="Company name" DataField="lead_company_name" UniqueName="lead_company_name" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="140" HeaderText="Company email" DataField="lead_company_email" UniqueName="lead_company_email" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="140" HeaderText="Company country" DataField="lead_country" UniqueName="lead_country" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="80" HeaderText="Product" DataField="product" UniqueName="product" />
                                                            <telerik:GridBoundColumn Display="false" HeaderText="First name" DataField="lead_first_name" UniqueName="lead_first_name" />
                                                            <telerik:GridBoundColumn Display="false" HeaderText="Last name" DataField="lead_last_name" UniqueName="lead_last_name" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="20" HeaderText="Company size" DataField="lead_company_size" UniqueName="lead_company_size" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="120" HeaderText="Apply for Company" DataField="rfp_company_name" UniqueName="rfp_company_name" />
                                                            <telerik:GridBoundColumn Display="false" DataField="message" UniqueName="message" />
                                                            <telerik:GridBoundColumn Display="false" HeaderStyle-Width="100" HeaderText="Date created" DataField="sysdate" UniqueName="sysdate" />
                                                            <telerik:GridBoundColumn Display="false" HeaderStyle-Width="20" HeaderText="Is approved" DataField="is_approved" UniqueName="is_approved" />
                                                            <telerik:GridTemplateColumn HeaderStyle-Width="120" HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <a id="aBtnApprove" onserverclick="aBtnApprove_ServerClick" title="approve request/send notification" class="btn btn-circle btn-sm purple" runat="server">
                                                                        <asp:Label ID="LblBtnSendEmail" runat="server" />
                                                                        <i class="fa fa-edit"></i>
                                                                    </a>
                                                                    <a id="aBtnReject" onserverclick="BtnReject_ServerClick" title="reject request" class="btn btn-circle btn-sm red" runat="server">
                                                                        <asp:Label ID="LblBtnReject" runat="server" />
                                                                        <i class="fa fa-remove"></i>
                                                                    </a>
                                                                    <a id="aSuccess" visible="false" title="approved" class="btn btn-circle btn-sm green" runat="server">
                                                                        <i class="fa fa-check"></i>
                                                                    </a>
                                                                    <asp:Image ID="ImgInfo" Visible="true" Style="cursor: pointer;" AlternateText="info" ImageUrl="~/images/icons/small/info.png" Width="20" Height="20" runat="server" />
                                                                    <telerik:RadToolTip ID="RttImgInfo" Visible="true" TargetControlID="ImgInfo" Width="400" Title="RFP user Message" AutoCloseDelay="10000" HideEvent="ManualClose" Position="TopRight" Animation="Fade" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                                <controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />
                                            </div>
                                        </div>
                                        <!--tab_1_2-->
                                        <div class="tab-pane" id="tab_1_2">
                                            <div class="col-md-12">
                                                <telerik:RadGrid ID="RdgElioUsersApprovedDemoRequests" Style="margin: auto; position: relative;" AllowPaging="true" AllowSorting="false" HeaderStyle-BackColor="#39b4ef" PagerStyle-Position="TopAndBottom" HeaderStyle-ForeColor="#ffffff" PageSize="10" Width="100%" CssClass="rgdd" OnNeedDataSource="RdgElioUsersApprovedDemoRequests_OnNeedDataSource" OnItemDataBound="RdgElioUsersApprovedDemoRequests_OnItemDataBound" AutoGenerateColumns="false" runat="server">
                                                    <MasterTableView Width="100%" DataKeyNames="id" Name="Parent" AllowMultiColumnSorting="true">
                                                        <NoRecordsTemplate>
                                                            <div class="emptyGridHolder">
                                                                <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                            </div>
                                                        </NoRecordsTemplate>
                                                        <Columns>
                                                            <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="id" UniqueName="id" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="120" HeaderText="Company name" DataField="lead_company_name" UniqueName="lead_company_name" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="140" HeaderText="Company email" DataField="lead_company_email" UniqueName="lead_company_email" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="140" HeaderText="Company country" DataField="lead_country" UniqueName="lead_country" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="80" HeaderText="Product" DataField="product" UniqueName="product" />
                                                            <telerik:GridBoundColumn Display="false" HeaderText="First name" DataField="lead_first_name" UniqueName="lead_first_name" />
                                                            <telerik:GridBoundColumn Display="false" HeaderText="Last name" DataField="lead_last_name" UniqueName="lead_last_name" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="20" HeaderText="Company size" DataField="lead_company_size" UniqueName="lead_company_size" />
                                                            <telerik:GridBoundColumn HeaderStyle-Width="120" HeaderText="Apply for Company" DataField="rfp_company_name" UniqueName="rfp_company_name" />
                                                            <telerik:GridBoundColumn Display="false" DataField="message" UniqueName="message" />                                                            
                                                            <telerik:GridBoundColumn Display="false" HeaderText="Date created" DataField="sysdate" UniqueName="sysdate" />
                                                            <telerik:GridBoundColumn Display="false" HeaderText="Is approved" DataField="is_approved" UniqueName="is_approved" />
                                                            <telerik:GridBoundColumn Display="false" DataField="is_confirmed" UniqueName="is_confirmed" />
                                                            <telerik:GridTemplateColumn HeaderStyle-Width="120" HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <a id="aSuccess" title="approved and confirmed from Clearbit" class="btn btn-circle btn-sm green" runat="server">
                                                                        <i class="fa fa-check"></i>
                                                                    </a>
                                                                    <a id="aBtnConfirm" onserverclick="aBtnConfirm_ServerClick" title="Confirm data from Clearbit" visible="false" class="btn btn-circle btn-sm purple" runat="server">
                                                                        <i class="fa fa-edit"></i>
                                                                    </a>
                                                                    <asp:Image ID="ImgInfo" Visible="true" Style="cursor: pointer;" AlternateText="info" ImageUrl="~/images/icons/small/info.png" Width="20" Height="20" runat="server" />
                                                                    <telerik:RadToolTip ID="RttImgInfo" Visible="true" TargetControlID="ImgInfo" Width="400" Title="RFP user Message" AutoCloseDelay="10000" HideEvent="ManualClose" Position="TopRight" Animation="Fade" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                                <controls:MessageControl ID="UcMessageAlertApproved" Visible="false" runat="server" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <controls:MessageControl ID="UcStripeMessageAlert" Visible="false" runat="server" />
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
    </telerik:RadAjaxPanel>
</asp:Content>
