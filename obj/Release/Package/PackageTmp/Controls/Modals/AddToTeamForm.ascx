<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddToTeamForm.ascx.cs" Inherits="WdS.ElioPlus.Controls.Modals.AddToTeamForm" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">

        function CloseInvitationPopUp() {
            var x = $('#InvitationModal').modal('hide');
            console.log(x);
        }

    </script>
</telerik:RadScriptBlock>

<div id="InvitationModal" class="modal-dialog modal-dialog-centered" role="document" style="width: 430px;">
    <div class="modal-content">
        <div class="modal-header">
            <h4 class="modal-title">
                <asp:Label ID="LblAddMessageHeader" Text="Invite new user" runat="server" />
            </h4>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <i aria-hidden="true" class="ki ki-close"></i>
            </button>
        </div>
        <div class="modal-body">
            <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text">
                                <i class="flaticon2-email"></i>
                            </span>
                        </div>
                        <asp:TextBox ID="RtbxAddTeamEmail" CssClass="form-control" MaxLength="100" placeholder="Enter email" runat="server" />
                    </div>
                </div>
            </div>
            <div class="row" style="margin-top: 20px;">
                <div class="col-lg-6">
                    <div class="form-group">
                        <label>
                            <asp:Label ID="LblTitleRole" Text="Select Role" runat="server" /></label>
                        <div class="checkbox-list">
                            <div id="div1" runat="server">
                                <label class="checkbox">
                                    <input id="CbxR1" runat="server" type="checkbox">
                                    <asp:Label ID="LblCbx1" Style="display: contents;" runat="server" />
                                    <span></span>
                                </label>
                            </div>
                            <div id="div2" runat="server">
                                <label class="checkbox">
                                    <input id="CbxR2" runat="server" type="checkbox">
                                    <asp:Label ID="LblCbx2" Style="display: contents;" runat="server" />
                                    <span></span>
                                </label>
                            </div>
                            <div id="div3" runat="server">
                                <label class="checkbox">
                                    <input id="CbxR3" runat="server" type="checkbox">
                                    <asp:Label ID="LblCbx3" Style="display: contents;" runat="server" />
                                    <span></span>
                                </label>
                            </div>
                            <div id="div4" runat="server">
                                <label class="checkbox">
                                    <input id="CbxR4" runat="server" type="checkbox">
                                    <asp:Label ID="LblCbx4" Style="display: contents;" runat="server" />
                                    <span></span>
                                </label>
                            </div>
                            <div id="div5" runat="server">
                                <label class="checkbox">
                                    <input id="CbxR5" runat="server" type="checkbox">
                                    <asp:Label ID="LblCbx5" Style="display: contents;" runat="server" />
                                    <span></span>
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="form-group">
                        <label></label>
                        <div class="checkbox-list">
                            <div id="div6" runat="server">
                                <label class="checkbox">
                                    <input id="CbxR6" runat="server" type="checkbox">
                                    <asp:Label ID="LblCbx6" Style="display: contents;" runat="server" />
                                    <span></span>
                                </label>
                            </div>
                            <div id="div7" runat="server">
                                <label class="checkbox">
                                    <input id="CbxR7" runat="server" type="checkbox">
                                    <asp:Label ID="LblCbx7" Style="display: contents;" runat="server" />
                                    <span></span>
                                </label>
                            </div>
                            <div id="div8" runat="server">
                                <label class="checkbox">
                                    <input id="CbxR8" runat="server" type="checkbox">
                                    <asp:Label ID="LblCbx8" Style="display: contents;" runat="server" />
                                    <span></span>
                                </label>
                            </div>
                            <div id="div9" runat="server">
                                <label class="checkbox">
                                    <input id="CbxR9" runat="server" type="checkbox">
                                    <asp:Label ID="LblCbx9" Style="display: contents;" runat="server" />
                                    <span></span>
                                </label>
                            </div>
                            <div id="div10" runat="server">
                                <label class="checkbox">
                                    <input id="CbxR10" runat="server" type="checkbox">
                                    <asp:Label ID="LblCbx10" Style="display: contents;" runat="server" />
                                    <span></span>
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div id="divAddGeneralSuccess" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-success fade show" role="alert">
                    <div class="alert-icon">
                        <i class="flaticon-warning"></i>
                    </div>
                    <div class="alert-text">
                        <strong>
                            <asp:Label ID="LblAddGeneralSuccess" Text="" runat="server" />
                        </strong>
                        <asp:Label ID="LblAddSuccess" runat="server" />
                    </div>
                    <div class="alert-close">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">
                                <i class="ki ki-close"></i>
                            </span>
                        </button>
                    </div>
                </div>
                <div id="divAddGeneralFailure" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-danger fade show" role="alert">
                    <div class="alert-icon">
                        <i class="flaticon-questions-circular-button"></i>
                    </div>
                    <div class="alert-text">
                        <strong>
                            <asp:Label ID="LblAddGeneralFailure" Text="Error! " runat="server" />
                        </strong>
                        <asp:Label ID="LblAddFailure" runat="server" />
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
            <asp:Button ID="BtnAddCancelMessage" Text="Cancel" data-dismiss="modal" CssClass="btn btn-danger" runat="server" />
            <asp:Button ID="BtnAdd" OnClick="BtnAdd_OnClick" Text="Invite" CssClass="btn btn-primary" runat="server" />
        </div>
    </div>
</div>
