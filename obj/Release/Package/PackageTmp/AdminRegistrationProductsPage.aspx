<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="AdminRegistrationProductsPage.aspx.cs" Inherits="WdS.ElioPlus.AdminRegistrationProductsPage" %>

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

            function OpenConfDeleteUserPopUp() {
                $('#divConfirmDeletion').modal('show');
            }

            function CloseConfDeleteUserPopUp() {
                $('#divConfirmDeletion').modal('hide');
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
                            <div style="margin-top: 50px;">
                                <h2>
                                    <asp:Label ID="Label5" runat="server" />
                                </h2>
                                <asp:Panel ID="PnlElioRegistrationProductsGrid" Style="margin-top: 20px; text-align: left; padding: 10px; margin: auto;" runat="server">
                                    <div style="width: 100%; display: inline-block;">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-2"></div>
                                                <div class="col-md-8">
                                                    <table class="">
                                                        <tr class="step2-row">
                                                            <td style="padding: 10px; padding-top: 20px;">
                                                                <div>
                                                                    <asp:Label ID="LblProductId" runat="server" Text="ID" />
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div style="padding-left: 15px;">
                                                                    <asp:TextBox ID="TbxProductId" Enabled="false" MaxLength="10" Width="50" runat="server" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr class="step2-row">
                                                            <td style="padding: 10px; padding-top: 20px;">
                                                                <div>
                                                                    <asp:Label ID="LblProductDescription" runat="server" Text="Product description" />
                                                                </div>
                                                            </td>
                                                            <td class="step2-td2">
                                                                <div style="padding-left: 15px;">
                                                                    <asp:TextBox ID="TbxProductDescription" Width="300" runat="server" />
                                                                    <div style="color: #fd5840; padding: 5px;">
                                                                        <asp:Label ID="LblProductDescriptionError" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr class="step2-row">
                                                            <td style="padding: 10px; padding-top: 20px;">
                                                                <div>
                                                                    <asp:Label ID="LblIsPublic" runat="server" Text="Is public" />
                                                                </div>
                                                            </td>
                                                            <td class="step2-td2">
                                                                <div style="padding-left: 15px;">
                                                                    <asp:TextBox ID="TbxIsPublic" TextMode="Number" Text="1" MaxLength="5" Width="50" runat="server" />
                                                                    <div style="color: #fd5840; padding: 5px;">
                                                                        <asp:Label ID="LblIsPublicError" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="col-md-2"></div>
                                            </div>
                                        </div>
                                    </div>

                                    <div style="margin-top: 10px; display: inline-block;"></div>
                                    <controls:MessageControl ID="UcMessage" Visible="false" runat="server" />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-2"></div>
                                            <div class="col-md-8">
                                                <table class="">
                                                    <tr>
                                                        <td style="width: 153px; padding: 10px;"></td>
                                                        <td>
                                                            <div style="padding: 10px;">
                                                                <telerik:RadButton ID="RbtnSave" OnClick="RbtnSave_Click" CssClass="btn blue" Width="135" Style="text-align: center;" runat="server">
                                                                    <ContentTemplate>
                                                                        <span>
                                                                            <asp:Label ID="LblSaveText" Text="Save" runat="server" />
                                                                        </span>
                                                                    </ContentTemplate>
                                                                </telerik:RadButton>
                                                                <telerik:RadButton ID="RbtnClear" OnClick="RbtnClear_Click" CssClass="btn red-sunglo" Width="135" Style="text-align: center;" runat="server">
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
                                            </div>
                                            <div class="col-md-2"></div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div style="margin-bottom: 20px;"></div>
                                <div id="divData" runat="server">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-2">
                                            </div>
                                            <div class="col-md-8">
                                                <div class="col-lg-4" style="width: 410px; padding: 0px;">
                                                    Search By Description:                                                    
                                                    <asp:TextBox ID="TbxDescriptionSearch" runat="server" />
                                                    <asp:Button ID="BtnSearch" Text="Search" OnClick="BtnSearch_Click" CssClass="btn blue" runat="server" />
                                                </div>
                                                <div style="float: right; padding-bottom: 15px;">
                                                    <div class="col-lg-1" style="width: 58px; padding: 0px; padding-top: 10px;">
                                                        Sort By:
                                                    </div>
                                                    <div class="col-lg-8">
                                                        <asp:DropDownList ID="DrpSortList" Width="155" AutoPostBack="true" OnSelectedIndexChanged="DrpSortList_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                            <asp:ListItem Value="0" Text="Default Sort" />
                                                            <asp:ListItem Value="1" Text="A-Z" />
                                                            <asp:ListItem Value="2" Text="Z-A" />
                                                            <asp:ListItem Value="3" Text="Oldest to Newest" />
                                                            <asp:ListItem Value="4" Text="Newest to Oldest" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-2"></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-2"></div>
                                            <div class="col-md-8">
                                                <telerik:RadGrid ID="RdgRegProducts" Style="margin: auto; position: relative;" AllowPaging="true" AllowSorting="true" HeaderStyle-BackColor="#2b3643" PagerStyle-Position="TopAndBottom" HeaderStyle-ForeColor="#ffffff" PageSize="50" Width="100%" CssClass="rgdd"
                                                    OnNeedDataSource="RdgRegProducts_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                                    <MasterTableView Width="100%" Name="Parent" AllowMultiColumnSorting="true">
                                                        <NoRecordsTemplate>
                                                            <div class="emptyGridHolder">
                                                                <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                            </div>
                                                        </NoRecordsTemplate>
                                                        <Columns>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="80" HeaderText="ID" DataField="id" UniqueName="id" />
                                                            <telerik:GridTemplateColumn HeaderStyle-Width="200" HeaderText="Description" DataField="description" UniqueName="description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblDescription" Text='<%# DataBinder.Eval(Container.DataItem, "description")%>' runat="server" />
                                                                    <asp:TextBox ID="TbxDescription" Visible="false" Width="180" Text='<%# DataBinder.Eval(Container.DataItem, "description")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderStyle-Width="100" HeaderText="Is Public" DataField="is_public" UniqueName="is_public">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblIsPublic" Text='<%# DataBinder.Eval(Container.DataItem, "is_public")%>' runat="server" />
                                                                    <asp:TextBox ID="TbxIsPublic" Visible="false" Width="50" Text='<%# DataBinder.Eval(Container.DataItem, "is_public")%>' runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="150" HeaderText="Date" DataField="sysdate" UniqueName="sysdate" />
                                                            <telerik:GridTemplateColumn HeaderStyle-Width="80" HeaderText="Actions">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImgBtnEditProduct" OnClick="ImgBtnEditProduct_Click" ImageUrl="~/Images/edit.png" runat="server" />
                                                                    <asp:ImageButton ID="ImgBtnCancelProduct" OnClick="ImgBtnCancelProduct_Click" Visible="false" ImageUrl="~/Images/cancel.png" runat="server" />
                                                                    <asp:ImageButton ID="ImgBtnUpdateProduct" OnClick="ImgBtnUpdateProduct_Click" Visible="false" ImageUrl="~/Images/save.png" runat="server" />
                                                                    <asp:ImageButton ID="ImgBtnDeleteProduct" OnClick="ImgBtnDeleteProduct_Click" ToolTip="Delete product" ImageUrl="~/images/icons/small/delete.png" runat="server" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </div>
                                            <div class="col-md-2"></div>
                                        </div>
                                    </div>
                                </div>
                                <controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />
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
                                <asp:Label ID="LblConfTitle" Text="Delete Product Confirmation" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="LblConfMsg" Text="Are you sure to delete this product? It is going to delete from user too. Procced?" CssClass="control-label" runat="server" />
                                        <asp:HiddenField ID="HdnId" Value="0" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="BtnBack" Text="Cancel" OnClick="BtnBack_Click" CssClass="btn dark btn-outline" runat="server" />
                            <asp:Button ID="BtnDeleteConfirm" Text="Delete" OnClick="BtnDeleteConfirm_Click" CssClass="btn red-sunglo" runat="server" />
                            <asp:Button ID="BtnProceedMerge" Visible="false" Text="Merge" OnClick="BtnProceedMerge_Click" CssClass="btn red-sunglo" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
