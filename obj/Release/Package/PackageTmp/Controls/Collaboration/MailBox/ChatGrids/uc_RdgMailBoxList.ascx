<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_RdgMailBoxList.ascx.cs" Inherits="WdS.ElioPlus.Controls.Collaboration.MailBox.ChatGrids.uc_RdgMailBoxList" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:UpdatePanel runat="server" ID="UpdatePanelChat" ChildrenAsTriggers="true" UpdateMode="Conditional">
    <ContentTemplate>
        <triggers>
            <asp:AsyncPostBackTrigger ControlId="Timer1" />
        </triggers>
        <asp:Timer ID="Timer1" Interval="7000" OnTick="Timer_OnTick" runat="server" />
        <div id="divNoCollaborationPartners" style="width: 100%; display: inline-block; text-align: center;" visible="false" runat="server">
            <div class="widget-heading">
                <i class="fa fa-file-text-o"></i>
                <asp:Label ID="Label1" Text="You have no Collaboration Partners yet. Click the button below to Invite and start a discussion" runat="server" />
            </div>
            <div class="col-md-12" style="margin-top:20px;">
                <a id="aInvitationToPartners" runat="server" class="btn btn-light-primary" style="padding: 20px; width: 250px;">
                    <i class="ion-plus-round mr-5"></i>
                    <asp:Label ID="LblInvitationToPartners" Text="Invite Partners" runat="server" />
                </a>
            </div>
        </div>

        <div id="divSimpleMailBox" visible="false" runat="server">
            <telerik:RadGrid ID="RdgMailBox" Visible="true" ShowHeader="false" AllowPaging="false" Skin="MetroTouch" CssClass="RgPosts" OnPageIndexChanged="RdgMailBox_PageIndexChanged" OnItemCreated="RdgMailBox_ItemCreated" OnItemDataBound="RdgMailBox_OnItemDataBound" OnNeedDataSource="RdgMailBox_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                <MasterTableView>
                    <NoRecordsTemplate>
                        <div class="emptyGridHolder">
                            <asp:Literal ID="LtlNoDataFound" runat="server" />
                        </div>
                    </NoRecordsTemplate>
                    <Columns>
                        <telerik:GridBoundColumn DataField="id" Display="false" UniqueName="id" />
                        <telerik:GridBoundColumn DataField="mailbox_id" Display="false" UniqueName="mailbox_id" />
                        <telerik:GridBoundColumn DataField="user_id" Display="false" UniqueName="user_id" />
                        <telerik:GridBoundColumn DataField="message_content" Display="false" UniqueName="message_content" />
                        <telerik:GridBoundColumn DataField="date_created" Display="false" UniqueName="date_created" />
                        <telerik:GridBoundColumn DataField="date_send" Display="false" UniqueName="date_send" />
                        <telerik:GridBoundColumn DataField="date_received" Display="false" UniqueName="date_received" />
                        <telerik:GridBoundColumn DataField="is_new" Display="false" UniqueName="is_new" />
                        <telerik:GridBoundColumn DataField="is_viewed" Display="false" UniqueName="is_viewed" />
                        <telerik:GridBoundColumn DataField="date_viewed" Display="false" UniqueName="date_viewed" />
                        <telerik:GridBoundColumn DataField="total_reply_comments" Display="false" UniqueName="total_reply_comments" />
                        <telerik:GridBoundColumn DataField="username" Display="false" UniqueName="username" />
                        <telerik:GridBoundColumn DataField="company_logo" Display="false" UniqueName="company_logo" />
                        <telerik:GridTemplateColumn>
                            <ItemTemplate>
                                <!--begin::Message In-->
                                <div id="divMessage" runat="server" class="d-flex flex-column mb-5 align-items-start">
                                    <div class="d-flex align-items-center">
                                        <div class="symbol symbol-circle symbol-35 mr-3">
                                            <a id="aCompanyLogo" runat="server">
                                                <asp:Image ID="ImgLogo" ImageUrl="/assets/media/users/300_12.jpg" Width="50" Height="50" runat="server" />
                                            </a>
                                        </div>
                                        <div>
                                            <a id="aCompanyName" runat="server" class="text-dark-75 text-hover-primary font-weight-bold font-size-h6">
                                                <asp:Label ID="LblCompanyName" Text="Nich Larson" runat="server" />
                                            </a>
                                            <span class="text-muted font-size-sm">
                                                <asp:Label ID="LblTimeAgo" runat="server" />
                                                <asp:Label ID="LblLastUpdate" runat="server" />
                                                <asp:Label ID="LblMsgStatus" runat="server" />
                                                <asp:PlaceHolder ID="PhPartnersLogo" runat="server" />
                                            </span>
                                        </div>
                                    </div>
                                    <div id="divContentMessage" runat="server" class="mt-2 rounded p-5 bg-light-success text-dark-50 font-weight-bold font-size-lg text-left max-w-400px">
                                        <asp:Label ID="LblMailBoxContent" Text="How likely are you to recommend our company to your friends and family?" runat="server" />
                                        <asp:Panel ID="Panel1" Style="float: right; margin-left: 20px;" runat="server">
                                            <a id="aImgBtnPreviewLogFiles" runat="server" class="d-flex align-items-center text-muted text-hover-primary py-1">
                                                <span class="flaticon2-clip-symbol text-warning icon-1x mr-2"></span>
                                                <asp:Label ID="HpLnkPreViewLogFiles" runat="server" />                                                
                                            </a>
                                            <%--<asp:ImageButton ID="ImgBtnPreviewLogFiles" OnClick="ImgBtnPreviewLogFiles_OnClick" ImageUrl="~/images/icons/small/attachment.png" Visible="false" runat="server" />
                                            <asp:HyperLink ID="HpLnkPreViewLogFiles" Target="_blank" Style="text-decoration: underline;" runat="server" />--%>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <!--end::Message In-->
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>

        <div id="divGroupMailBox" visible="false" runat="server">
            <telerik:RadGrid ID="RdgGroupMailBox" Visible="false" ShowHeader="false" AllowPaging="false" Skin="MetroTouch" CssClass="RgPosts" OnPageIndexChanged="RdgGroupMailBox_PageIndexChanged" OnItemCreated="RdgGroupMailBox_ItemCreated" OnItemDataBound="RdgGroupMailBox_OnItemDataBound" OnNeedDataSource="RdgGroupMailBox_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                <MasterTableView>
                    <NoRecordsTemplate>
                        <div class="emptyGridHolder">
                            <asp:Literal ID="LtlNoDataFound" runat="server" />
                        </div>
                    </NoRecordsTemplate>
                    <Columns>
                        <telerik:GridBoundColumn DataField="id" Display="false" UniqueName="id" />
                        <telerik:GridBoundColumn DataField="user_id" Display="false" UniqueName="user_id" />
                        <telerik:GridBoundColumn DataField="message_content" Display="false" UniqueName="message_content" />
                        <telerik:GridBoundColumn DataField="date_created" Display="false" UniqueName="date_created" />
                        <telerik:GridBoundColumn DataField="date_send" Display="false" UniqueName="date_send" />
                        <telerik:GridBoundColumn DataField="date_received" Display="false" UniqueName="date_received" />
                        <telerik:GridBoundColumn DataField="date_viewed" Display="false" UniqueName="date_viewed" />
                        <telerik:GridBoundColumn DataField="total_reply_comments" Display="false" UniqueName="total_reply_comments" />
                        <telerik:GridBoundColumn DataField="is_public" Display="false" UniqueName="is_public" />
                        <telerik:GridTemplateColumn>
                            <ItemTemplate>
                                <!--begin::Message In-->
                                <div id="divMessage" runat="server" class="d-flex flex-column mb-5 align-items-start">
                                    <div class="d-flex align-items-center">
                                        <div class="symbol symbol-circle symbol-35 mr-3">
                                            <a id="aCompanyLogo" runat="server">
                                                <asp:Image ID="ImgLogo" ImageUrl="/assets/media/users/300_12.jpg" Width="50" Height="50" runat="server" />
                                            </a>
                                        </div>
                                        <div>
                                            <a id="aCompanyName" runat="server" class="text-dark-75 text-hover-primary font-weight-bold font-size-h6">
                                                <asp:Label ID="LblCompanyName" Text="Nich Larson" runat="server" />
                                            </a>
                                            <span class="text-muted font-size-sm">
                                                <asp:Label ID="LblTimeAgo" runat="server" />
                                                <asp:Label ID="LblLastUpdate" runat="server" />
                                                <asp:Label ID="LblMsgStatus" runat="server" />
                                                <asp:PlaceHolder ID="PhPartnersLogo" runat="server" />
                                            </span>
                                        </div>
                                    </div>
                                    <div id="divContentMessage" runat="server" class="mt-2 rounded p-5 bg-light-success text-dark-50 font-weight-bold font-size-lg text-left max-w-400px">
                                        <asp:Label ID="LblMailBoxContent" Text="How likely are you to recommend our company to your friends and family?" runat="server" />
                                        <asp:Panel ID="Panel1" Style="float: right; margin-left: 20px;" runat="server">
                                            <a id="aImgBtnPreviewLogFiles" runat="server" class="d-flex align-items-center text-muted text-hover-primary py-1">
                                                <span class="flaticon2-clip-symbol text-warning icon-1x mr-2"></span>
                                                <asp:Label ID="HpLnkPreViewLogFiles" runat="server" />                                                
                                            </a>
                                            <%--<asp:ImageButton ID="ImgBtnPreviewLogFiles" OnClick="ImgBtnPreviewLogFiles_OnClick" ImageUrl="~/images/icons/small/attachment.png" Visible="false" runat="server" />
                                            <asp:HyperLink ID="HpLnkPreViewLogFiles" Target="_blank" Style="text-decoration: underline;" runat="server" />--%>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <!--end::Message In-->
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
        <controls:MessageControl ID="UcMailBoxListMessageAlert" Visible="false" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>

<style>
    .RadGrid_MetroTouch .rgAltRow {
        background-color: transparent !important;
    }

    .RadGrid_MetroTouch .rgRow {
        background-color: transparent !important;
    }
</style>

