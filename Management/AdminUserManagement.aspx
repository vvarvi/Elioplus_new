<%@ Page Title="" Language="C#" MasterPageFile="~/Management/DashboardAdminManagementMaster.Master" AutoEventWireup="true" CodeBehind="AdminUserManagement.aspx.cs" Inherits="WdS.ElioPlus.Management.AdminUserManagement" %>

<%@ Register Assembly="Libero.FusionCharts" Namespace="Libero.FusionCharts.Control" TagPrefix="fcl" %>
<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="DashboardHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Our platform allows IT vendors, resellers and developers to connect based on their interests and industry information."/>
    <meta name="keywords" content="the best SaaS vendors, channel partnerships, IT channel community"/>
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
                                                                     
                    <div style="margin-top:50px; text-align:center;"> 
                        <h2><asp:Label ID="Label5" runat="server" /></h2>
                        <asp:Panel ID="PnlElioCompaniesGrid" Visible="false" style="margin-top:20px; text-align:left; padding:10px;margin:auto;" runat="server">
                            
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
                                                <asp:Label ID="Label9" CssClass="label_title" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px;">
                                                <telerik:RadTextBox ID="RtbxCompanyName" MaxLength="30" Width="300" runat="server" />
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
                                                <telerik:RadTextBox ID="RtbxUserId" MaxLength="30" Width="300" runat="server" />
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
                                                <asp:Label ID="LblAddRole" CssClass="label_title" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px;">
                                                <telerik:RadTextBox ID="RtbxUserToAssignRole" onkeypress="return isNumberOnly(event);" MaxLength="10" Width="80" runat="server" />
                                                <div style="vertical-align:middle; float:right; margin-top:10px;">
                                                     <asp:ImageButton ID="ImgGetUserRoles" OnClick="ImgGetUserRoles_OnClick" ToolTip="Load user roles" ImageUrl="~/Images/icons/small/usr_roles.png" runat="server" />
                                                    <asp:ImageButton ID="ImgBtnSave" ToolTip="Save selected roles to user" OnClick="ImgBtnSave_OnClick" ImageUrl="~/Images/icons/small/usr_role_add.png" runat="server" />                                               
                                                </div>
                                            </div>
                                        </td>
                                        <td style="width:130px;">
                                            <div style="padding:5px;">
                                                <asp:Label ID="Label4" CssClass="label_title" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px; width:145px;">
                                                <telerik:RadTextBox ID="RtbxStripeCustomerId" Width="300" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:130px;">
                                            <div style="padding:5px;">
                                                <asp:Label ID="LblRoles" CssClass="label_title" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px;">
                                                <asp:CheckBoxList ID="CbxRoles" runat="server" />
                                            </div>
                                        </td>
                                        <td style="width:130px;">
                                            <div style="padding:5px;">
                                                <asp:Label ID="Label10" CssClass="label_title" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px;">
                                                <telerik:RadTextBox ID="RtbxCompanyEmail" Width="300" runat="server" />
                                            </div>
                                        </td>
                                        
                                    </tr>
                                </table>
                                <div style="margin-top:10px;"></div>
                                <table>
                                    <tr>
                                        <td style="width:130px;">
                                            <div style="padding:5px;">
                                                <asp:Label ID="LblPacketStatusUserId" CssClass="label_title" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px;">
                                                <telerik:RadTextBox ID="RtbxPacketStatusUserId" onkeypress="return isNumberOnly(event);" MaxLength="10" Width="70" runat="server" />
                                            </div>
                                        </td>
                                        <td style="width:70px;">
                                            <div style="padding:5px;">
                                                <asp:Label ID="LblPacketStatusConnections" CssClass="label_title" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px;">
                                                <telerik:RadTextBox ID="RtbxPacketStatusConnections" onkeypress="return isNumberOnly(event);" MaxLength="10" Width="50" runat="server" />
                                            </div>
                                        </td>
                                        <td style="width:70px;">
                                            <div style="padding:5px;">
                                                <asp:Label ID="LblStartingDate" CssClass="label_title" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px;">
                                                <telerik:RadDatePicker ID="RdpStartingDate" Width="120" runat="server" />
                                            </div>
                                        </td>
                                        <td style="width:70px;">
                                            <div style="padding:5px;">
                                                <asp:Label ID="LblExpirationDate" CssClass="label_title" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px;">
                                                <telerik:RadDatePicker ID="RdpExpirationDate" Width="120" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div style="padding:5px;">
                                                <telerik:RadButton ID="RbtnAddConnections" Width="150" style="text-align:center;" OnClick="RbtnAddConnections_OnClick" runat="server">
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
                                <div style="margin-top:20px;"></div>
                                <div style="margin-bottom:10px;"></div>
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
                                <controls:MessageControl ID="UcMessageControlCriteria" Visible="false" runat="server" />                           
                                <div class="row"></div>                                
                            </asp:Panel>
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
                                                <telerik:GridBoundColumn HeaderText="Id" HeaderStyle-Width="20" DataField="connection_id" UniqueName="connection_id" />
                                                <telerik:GridTemplateColumn HeaderText="Select" HeaderStyle-Width="30" DataField="select" UniqueName="select">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CbxSelectUser" AutoPostBack="true" OnCheckedChanged="CbxSelectUser_OnCheckedChanged" runat="server" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn HeaderStyle-Width="80" HeaderText="Company Name" DataField="company_name" UniqueName="company_name" />
                                                <telerik:GridBoundColumn HeaderStyle-Width="80" HeaderText="Application Type" DataField="user_application_type" UniqueName="user_application_type" />
                                                <telerik:GridBoundColumn HeaderStyle-Width="20" HeaderText="Phone" DataField="phone" UniqueName="phone" />
                                                <telerik:GridBoundColumn HeaderStyle-Width="50" HeaderText="Address" DataField="address" UniqueName="address" />
                                                <telerik:GridBoundColumn HeaderStyle-Width="50" HeaderText="Country" Display="false" DataField="country" UniqueName="country" />
                                                <telerik:GridBoundColumn HeaderStyle-Width="80" HeaderText="Email" DataField="email" UniqueName="email" />
                                                <telerik:GridBoundColumn HeaderStyle-Width="50" HeaderText="Website" DataField="website" UniqueName="website" />
                                                <telerik:GridBoundColumn Display="false" HeaderStyle-Width="20" HeaderText="Sysdate" DataField="sysdate" UniqueName="sysdate" />
                                                <telerik:GridBoundColumn Display="false" HeaderStyle-Width="20" HeaderText="Last Updated" DataField="last_updated" UniqueName="last_updated" />
                                                <telerik:GridBoundColumn HeaderStyle-Width="50" HeaderText="Period From" DataField="current_period_start" UniqueName="current_period_start" />
                                                <telerik:GridBoundColumn HeaderStyle-Width="50" HeaderText="Period End" DataField="current_period_end" UniqueName="current_period_end" />
                                                <telerik:GridTemplateColumn HeaderText="Status" HeaderStyle-Width="20" DataField="status" UniqueName="status">
                                                    <ItemTemplate>
                                                        <asp:Image ID="ImgStatus" Visible="false" runat="server" />
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
                                                <telerik:RadComboBox ID="RcbxBillingType" Visible="false" runat="server">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Value="1" Text="Freemium" />
                                                        <telerik:RadComboBoxItem Value="2" Text="Premium" />
                                                    </Items>
                                                </telerik:RadComboBox>
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
                                                <telerik:RadComboBox ID="RcbxEmail" Width="280" runat="server" />
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
                                        <telerik:GridTemplateColumn HeaderStyle-Width="5" DataField="features_no" UniqueName="features_no">
                                            <ItemTemplate>
                                                <asp:Label ID="LblFeature" runat="server" />
                                                <telerik:RadTextBox ID="RtbxFeature" Visible="false" onkeypress="return isNumberOnly(event);" Width="30" runat="server" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="50" DataField="available_connections_count" UniqueName="available_connections_count" />                                            
                                        <telerik:GridBoundColumn HeaderStyle-Width="150" DataField="last_login" UniqueName="last_login" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="5" DataField="login_times" UniqueName="login_times" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="5" DataField="count" UniqueName="count" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="5" DataField="first_send" UniqueName="first_send" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="5" DataField="last_send" UniqueName="last_send" />
                                        <telerik:GridTemplateColumn HeaderStyle-Width="60" DataField="actions" UniqueName="actions">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgBtnEditCompany" OnClick="ImgBtnEditCompany_OnClick" ImageUrl="~/Images/edit.png" runat="server" />
                                                <asp:ImageButton ID="ImgBtnCancel" Visible="false" OnClick="ImgBtnCancel_OnClick" ImageUrl="~/Images/cancel.png" runat="server" />
                                                <asp:ImageButton ID="ImgBtnSaveChanges" Visible="false" OnClick="ImgBtnSaveChanges_OnClick" ImageUrl="~/Images/save.png" runat="server" />
                                                <asp:ImageButton ID="ImgBtnLoginAsCompany" OnClick="ImgBtnLoginAsCompany_OnClick" ImageUrl="~/Images/admin.png" runat="server" />  
                                                <asp:ImageButton ID="ImgBtnPreviewCompany" OnClick="ImgBtnPreviewCompany_OnClick" ImageUrl="~/Images/customer.png" runat="server" />  
                                                <asp:ImageButton ID="ImgBtnSendEmail" OnClick="ImgBtnSendEmail_OnClick" runat="server" />
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
</asp:Content>
