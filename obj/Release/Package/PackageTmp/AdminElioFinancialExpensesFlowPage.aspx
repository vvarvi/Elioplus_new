<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="AdminElioFinancialExpensesFlowPage.aspx.cs" Inherits="WdS.ElioPlus.AdminElioFinancialExpensesFlowPage" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Modals/AddToTeamForm.ascx" TagName="UcAddToTeam" TagPrefix="controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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

        function CloseInvitationPopUp() {
            var x = $('#InvitationModal').modal('hide');
            console.log(x);
        }

        function BtnCancelMessage_OnClientClick(sender, e) {
            var x = $('#InvitationModal').modal('hide');
        }

    </script>
</telerik:RadScriptBlock>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>

    <telerik:RadAjaxPanel ID="RapPage" ClientEvents-OnRequestStart="RapPage_OnRequestStart" ClientEvents-OnResponseEnd="RapPage_OnResponseEnd" runat="server" RestoreOriginalRenderDelegate="false">
        <!-- BEGIN PAGE BAR -->
        <div class="page-bar">
            <ul class="page-breadcrumb">
                <li>
                    <span><asp:Label ID="LblDashboard" runat="server" /></span>
                    <i class="fa fa-circle"></i>
                </li>
                <li>
                    <span><asp:Label ID="LblDashPage" runat="server" /></span>
                </li>
            </ul>
            <div class="page-toolbar" id="divPgToolbar" runat="server">
                <div class="clearfix">
                    <asp:Label ID="LblPricingPlan" runat="server" />
                    <span style="margin-left:10px;">
                        <a id="aBtnGoPremium" runat="server" href="#PaymentModal" role="button" data-toggle="modal" class="btn btn-circle green-light btn-md" visible="false">
                            <asp:Label ID="LblBtnGoPremium" runat="server" />
                        </a>
                        <a id="aBtnGoFull" runat="server" class="btn btn-circle green-light btn-md" visible="false">
                            <asp:Label ID="LblGoFull" runat="server" />
                        </a>
                    </span>
                    <br /><asp:Label ID="LblRenewalHead" runat="server" Visible="false" /><asp:Label ID="LblRenewal" Visible="false" runat="server" />
                </div>
            </div>
        </div>    
        <!-- END PAGE BAR -->
        <!-- BEGIN PAGE TITLE-->
        <h3 class="page-title"><asp:Label ID="LblElioplusDashboard" runat="server" />
            <small><asp:Label ID="LblDashSubTitle" runat="server" /></small>
        </h3>
        <!-- END PAGE TITLE-->    
    
        <div class="row">
            <div class="col-md-12">
                <div style="padding: 10px;" class="form-group">
                </div>               
                <div class="portlet-body">
                    
                    <asp:Panel ID="PnlSearch" style="border-radius:5px; background-color:#F0F3CD;margin-bottom:10px; padding:30px 10px 30px 10px; margin:auto; position:relative;" runat="server">
                        <table>
                            <tr>
                                <td style="width:120px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblAdminId" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblAdminIdValue" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:120px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblAdminName" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:350px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblAdminNameValue" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <div style="margin-bottom:10px;"></div>
                        <table>
                            <tr>
                                <td style="width:120px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblId" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblIdValue" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:120px;">
                                    <div style="padding:5px;">
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:120px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblExpensesAmount" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <asp:TextBox ID="TbxExpensesAmount" Width="120" runat="server" /> $
                                    </div>
                                </td>
                                <td style="width:140px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblExpensesReason" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <asp:TextBox ID="TbxExpensesReason" Width="120" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblOrganization" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:170px;">
                                    <div style="padding:5px;">
                                        <asp:TextBox ID="TbxOrganization" Width="120" runat="server" />                                  
                                    </div>
                                </td>
                                <td style="width:60px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblUserId" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:80px;">
                                    <div style="padding:5px;">
                                        <asp:TextBox ID="TbxUserId" Width="50" runat="server" />                                  
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:120px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblExpensesVat" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <asp:TextBox ID="TbxExpensesVat" Width="120" runat="server" /> %
                                    </div>
                                </td>
                                <td style="width:120px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblExpensesVatAmount" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <asp:TextBox ID="TbxExpensesVatAmount" ReadOnly="true" Width="120" runat="server" /> $
                                    </div>
                                </td>                                     
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblExpensesWithNoVat" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:170px;">
                                    <div style="padding:5px;">
                                        <asp:TextBox ID="TbxExpensesWithNoVat" ReadOnly="true" Width="120" runat="server" /> $                                    
                                        <asp:ImageButton ID="ImgBtnCalculate" ImageUrl="~/images/icons/small/calculator.png" OnClick="ImgBtnCalculate_OnClick" runat="server" />                                   
                                    </div>                                    
                                </td>                                        
                            </tr>
                            <tr>
                                <td style="width:120px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblDateIn" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <telerik:RadDatePicker ID="RdpDateIn" Width="120" runat="server" />
                                    </div>
                                </td>
                                <td style="width:120px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LbllastUpdated" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <telerik:RadDatePicker ID="RdpLastUpdated" Width="120" runat="server" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:120px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblIsPublic" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <telerik:RadComboBox ID="RcbxIsPublic" Width="120" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Selected="true" Value="1" Text="Yes" runat="server" />
                                                <telerik:RadComboBoxItem Value="0" Text="No" runat="server" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>
                                </td>
                                <td style="width:120px;">
                                    <div style="padding:5px;">                                        
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">                                        
                                    </div>
                                </td>
                            </tr>
                        </table>

                        <table>
                            <tr>
                                <td style="width:120px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblComments" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td>
                                    <div style="padding:5px;">
                                        <asp:TextBox ID="TbxComments" Width="710" Rows="10" TextMode="MultiLine" Height="150" runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <div style="margin-bottom:10px;"></div>
                        <table>
                            <tr>
                                <td style="width:125px;">
                                </td>
                                <td>
                                    <telerik:RadButton ID="RbtnSave" Width="150" OnClick="RbtnSave_OnClick" style="text-align:center;" runat="server">
                                        <ContentTemplate>
                                            <span>
                                                <asp:Label ID="LblSaveText" runat="server" />
                                            </span>
                                        </ContentTemplate>
                                    </telerik:RadButton>
                                    <telerik:RadButton ID="RbtnReset" Width="150" OnClick="RbtnReset_OnClick" style="text-align:center;" runat="server">
                                        <ContentTemplate>
                                            <span>
                                                <asp:Label ID="LblResetText" runat="server" />
                                            </span>
                                        </ContentTemplate>
                                    </telerik:RadButton>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:125px; height:50px;">
                                </td>
                                <td>
                                    <controls:MessageControl ID="UcMessage" Visible="false" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <div class="row"></div>   
                        <table>
                            <tr>
                                <td style="width:120px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblCurrency" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <telerik:RadComboBox AutoPostBack="true" ID="RcbxCurrency" OnTextChanged="RcbxCurrency_OnTextChanged" Width="50" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Value="0" Text="$" runat="server" />
                                                <telerik:RadComboBoxItem Value="1" Text="€" runat="server" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>
                                </td>
                                <td style="width:120px;">
                                    <div style="padding:5px;">
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:110px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblTotalIncomes" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <asp:TextBox ID="TbxTotalIncome" Width="120" ReadOnly="true" runat="server" /> <asp:Label ID="LblTotalIncomeCurrency" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:80px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblTotalIncomeVat" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <asp:TextBox ID="TbxTotalIncomeVat" Width="120" ReadOnly="true" runat="server" /> <asp:Label ID="LblTotalIncomeVatCurrency" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblTotalIncomeWithNoVat" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <asp:TextBox ID="TbxTotalIncomeWithNoVat" Width="120" ReadOnly="true" runat="server" /> <asp:Label ID="LblTotalIncomeWithNoVatCurrency" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:110px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblTotalExpenses" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <asp:TextBox ID="TbxTotalExpenses" Width="120" ReadOnly="true" runat="server" /> <asp:Label ID="LblTotalExpensesCurrency" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:80px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblTotalExpensesVat" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <asp:TextBox ID="TbxTotalExpensesVat" Width="120" ReadOnly="true" runat="server" /> <asp:Label ID="LblTotalExpensesVatCurrency" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblTotalExpensesWithNoVat" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <asp:TextBox ID="TbxTotalExpensesWithNoVat" Width="120" ReadOnly="true" runat="server" /> <asp:Label ID="LblTotalExpensesWithNoVatCurrency" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <div style="margin-top:5px; border-bottom:1px solid #454545;"></div>
                        <table>
                            <tr>
                                <td style="width:120px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblFinalAmount" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <asp:TextBox ID="TbxFinalAmount" Width="120" ReadOnly="true" runat="server" /> <asp:Label ID="LblFinalAmountCurrency" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:120px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblFinalVat" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <asp:TextBox ID="TbxFinalVat" Width="120" ReadOnly="true" runat="server" /> <asp:Label ID="LblFinalVatCurrency" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <asp:Label ID="LblFinalClearAmount" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                                <td style="width:150px;">
                                    <div style="padding:5px;">
                                        <asp:TextBox ID="TbxFinalClearAmount" Width="120" ReadOnly="true" runat="server" /> <asp:Label ID="LblFinalClearCurrency" CssClass="label_title" runat="server" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    
                    <controls:messagecontrol id="UcMessageAlert" visible="false" runat="server" />

                    <div class="form-group">
                        <telerik:RadGrid ID="RdgResults" BorderStyle="None" AllowPaging="true" PageSize="10" OnPageIndexChanged="RdgResults_PageIndexChanged" OnItemDataBound="RdgResults_OnItemDataBound" OnNeedDataSource="RdgResults_OnNeedDataSource" AutoGenerateColumns="false" runat="server" PagerStyle-Position="Bottom">
                            <MasterTableView>
                                <NoRecordsTemplate>
                                    <div class="emptyGridHolder">
                                        <asp:Literal ID="LtlNoDataFound" runat="server" />
                                    </div>
                                </NoRecordsTemplate>
                                <Columns>                            
                                    <telerik:GridBoundColumn DataField="id" UniqueName="id" />
                                    <telerik:GridBoundColumn DataField="admin_id" UniqueName="admin_id" />
                                    <telerik:GridBoundColumn DataField="admin_name" UniqueName="admin_name" />                                    
                                    <telerik:GridBoundColumn DataField="expenses_reason" UniqueName="expenses_reason" />
                                    <telerik:GridBoundColumn DataField="user_id" UniqueName="user_id" />
                                    <telerik:GridBoundColumn DataField="organization" UniqueName="organization" />
                                    <telerik:GridBoundColumn DataField="datein" UniqueName="datein" />
                                    <telerik:GridBoundColumn DataField="last_updated" UniqueName="last_updated" />
                                    <telerik:GridBoundColumn DataField="last_edit_user_id" UniqueName="last_edit_user_id" />
                                    <telerik:GridBoundColumn DataField="vat" UniqueName="vat" />
                                    <telerik:GridBoundColumn DataField="expenses_amount" UniqueName="expenses_amount" />
                                    <telerik:GridBoundColumn DataField="vat_amount" UniqueName="vat_amount" />
                                    <telerik:GridBoundColumn DataField="expenses_amount_with_no_vat" UniqueName="expenses_amount_with_no_vat" />                                
                                    <telerik:GridBoundColumn DataField="comments" UniqueName="comments" />
                                    <telerik:GridBoundColumn Display="false" DataField="is_public" UniqueName="is_public" />                                    
                                    <telerik:GridTemplateColumn DataField="actions" UniqueName="actions">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgBtnEdit" OnClick="ImgBtnEdit_OnClick" ImageUrl="~/images/edit.png" runat="server" /> 
                                            <asp:ImageButton ID="ImgBtnDelete" OnClick="ImgBtnDelete_OnClick" ImageUrl="~/images/icons/small/delete.png" runat="server" />                                                       
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>                                             
                    </div>      
                </div>           
            </div>        
        </div>
       
        <div id="loader" style="display:none;">
            <div id="loadermsg">
            </div>
        </div>
    </telerik:RadAjaxPanel>
</asp:Content>
