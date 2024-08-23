<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateNewInvitationToPartnersMessage.ascx.cs" Inherits="WdS.ElioPlus.Controls.Collaboration.CreateNewInvitationToPartnersMessage" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<telerik:RadAjaxPanel ID="RapPage" runat="server" RestoreOriginalRenderDelegate="false">
    <div id="PartnersInvitationModal" class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title" id="exampleModalLabel">
                    <asp:Label ID="LblMessageHeader" Text="Invitation to your existing partners" runat="server" />
                </h4>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <i aria-hidden="true" class="ki ki-close"></i>
                </button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <asp:Panel ID="PnlMsg" runat="server" Style="width: 100%;">
                        <div id="divCustomInvitation" visible="false" runat="server">
                            <div class="form-group">
                                <asp:HiddenField ID="HdnInvitationId" Value="0" runat="server" />
                                <asp:TextBox ID="TbxSubject" CssClass="form-control" BorderStyle="None" placeholder="Subject" data-placement="top" runat="server" />
                            </div>
                            <div class="form-group">
                                <asp:TextBox ID="TbxMsg" Rows="5" CssClass="form-control" placeholder="Message" data-placement="top" TextMode="MultiLine" runat="server" />
                            </div>
                        </div>
                        <div id="divPartnersEmail" runat="server">
                            <div class="col-lg-12 col-md-12">
                                <div class="input-group">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text">
                                            <i class="flaticon2-email"></i>
                                        </span>
                                    </div>
                                    <asp:TextBox ID="TbxPartnerEmailAddress" CssClass="form-control" Width="550" MaxLength="100" placeholder="Partner's email address (comma delimiter if many)" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group" style="padding: 10px;">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="divFileUpload" class="fileinput fileinput-new" style="width: 72%;" data-provides="fileinput" runat="server">
                                        <div class="input-group input-large" style="float: left;">
                                            <span>
                                                <input type="file" name="uploadFile" id="inputFile" data-buttonname="btn-black btn-outline"
                                                    data-iconname="ion-image mr-5"
                                                    class="filestyle" accept=".csv, .xls, .xlsx"
                                                    runat="server" />
                                            </span>
                                        </div>
                                        <div style="float: right;">
                                            <asp:Button ID="BtnImportCsv" Text="Import csv" OnClick="BtnImportCsv_Click" CssClass="btn btn-light-warning" runat="server" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="BtnImportCsv" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>

                        <div id="divInvitations" visible="false" class="form-group" style="overflow-y: scroll; height: 220px;" runat="server">
                            <telerik:RadGrid ID="RdgMyInvitations" Visible="false" Style="margin: auto; position: relative;" AllowPaging="true" AllowSorting="false" HeaderStyle-BackColor="#39b4ef" PagerStyle-Position="Bottom" HeaderStyle-ForeColor="#ffffff" PageSize="10" Width="100%" CssClass="rgdd" OnNeedDataSource="RdgMyInvitations_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                <MasterTableView>
                                    <NoRecordsTemplate>
                                        <div class="emptyGridHolder">
                                            <asp:Literal ID="LtlNoDataFound" runat="server" />
                                        </div>
                                    </NoRecordsTemplate>
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderStyle-Width="20" Display="false" DataField="id" UniqueName="id" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="20" Display="false" DataField="user_id" UniqueName="user_id" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="120" HeaderText="Subject" DataField="inv_subject" UniqueName="inv_subject" />
                                        <telerik:GridBoundColumn HeaderStyle-Width="300" HeaderText="Message" DataField="inv_content" UniqueName="inv_content" />
                                        <telerik:GridTemplateColumn HeaderStyle-Width="10" HeaderText="Actions">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgBtnLoad" ImageUrl="~/Images/icons/small/load_to_edit.png" runat="server" />
                                                <asp:ImageButton ID="ImgBtnDelete" OnClick="ImgBtnDelete_OnClick" ImageUrl="~/images/icons/small/delete.png" runat="server" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>

                        <controls:MessageControl ID="UcMessageAlert" runat="server" />
                    </asp:Panel>
                </div>
                <div class="row" style="margin-top: 25px;">
                    <div id="divGeneralSuccess" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-success fade show" role="alert">
                        <div class="alert-icon">
                            <i class="flaticon-warning"></i>
                        </div>
                        <div class="alert-text">
                            <strong>
                                <asp:Label ID="LblGeneralSuccess" Text="Done!" runat="server" />
                            </strong>
                            <asp:Label ID="LblSuccess" runat="server" />
                        </div>
                        <div class="alert-close">
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">
                                    <i class="ki ki-close"></i>
                                </span>
                            </button>
                        </div>
                    </div>

                    <div id="divGeneralFailure" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-danger fade show" role="alert">
                        <div class="alert-icon">
                            <i class="flaticon-questions-circular-button"></i>
                        </div>
                        <div class="alert-text">
                            <strong>
                                <asp:Label ID="LblGeneralFailure" Text="Error!" runat="server" />
                            </strong>
                            <asp:Label ID="LblFailure" runat="server" />
                        </div>
                        <div class="alert-close">
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">
                                    <i class="ki ki-close"></i>
                                </span>
                            </button>
                        </div>
                    </div>

                    <div id="divGeneralInfo" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-primary fade show" role="alert">
                        <div class="alert-icon">
                            <i class="flaticon-warning"></i>
                        </div>
                        <div class="alert-text">
                            <strong>
                                <asp:Label ID="LblGeneralInfo" Text="Info!" runat="server" />
                            </strong>
                            <asp:Label ID="LblInfo" runat="server" />
                        </div>
                        <div class="alert-close">
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">
                                    <i class="ki ki-close"></i>
                                </span>
                            </button>
                        </div>
                    </div>

                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="BtnCancelMessage" data-dismiss="modal" OnClick="BtnCancelMessage_OnClick" Text="Cancel" CssClass="btn btn-danger" runat="server" />
                <asp:Button ID="BtnClear" OnClick="BtnClear_OnClick" Text="Clear" CssClass="btn btn-light-primary" runat="server" />
                <asp:Button ID="BtnLoadDefaultTemplate" OnClick="BtnLoadDefaultTemplate_OnClick" Visible="false" Text="Create custom invitation" CssClass="btn btn-light-primary font-weight-bold" runat="server" />
                <asp:Button ID="BtnProccedMessage" OnClick="BtnProccedMessage_OnClick" Text="Send Invitation" CssClass="btn btn-primary" runat="server" />
            </div>
        </div>
    </div>
</telerik:RadAjaxPanel>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">

        function ClosePartnersInvitationPopUp() {
            $('#PartnersInvitationModal').modal('hide');
        }
        function OpenPartnersInvitationPopUp() {
            $('#PartnersInvitationModal').modal('show');
        }

        function disable() {
            var x = document.getElementById('<%= BtnProccedMessage.ClientID %>').disabled = true;

            console.log(x);
        }

        function BtnProccedMessage_OnClientClick(sender, e) {
            var y = $("#BtnProccedMessage.ClientID").click();
            var x = document.getElementById('<%= BtnProccedMessage.ClientID %>').disabled = true;

            console.log(y);
        }

    </script>
    <script src="/assets/global/plugins/ckeditor/ckeditor.js" type="text/javascript"></script>

</telerik:RadScriptBlock>
