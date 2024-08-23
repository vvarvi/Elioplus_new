<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_RdgComments.ascx.cs" Inherits="WdS.ElioPlus.Controls.Community.Grids.uc_RdgComments" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<telerik:RadGrid ID="RdgPostComments" AllowPaging="true" Width="950" PageSize="10" Skin="MetroTouch" CssClass="rgdd" OnPageIndexChanged="RdgPostComments_PageIndexChanged" OnItemCreated="RdgPostComments_ItemCreated" OnItemDataBound="RdgPostComments_OnItemDataBound" OnNeedDataSource="RdgPostComments_OnNeedDataSource" AutoGenerateColumns="false" runat="server" PagerStyle-Position="Bottom">
    <MasterTableView>
        <NoRecordsTemplate>
            <div class="emptyGridHolder">
                <asp:Literal ID="LtlNoDataFound" runat="server" />
            </div>
        </NoRecordsTemplate>
        <Columns>
            <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
            <telerik:GridBoundColumn Display="false" DataField="user_id" UniqueName="user_id" />
            <telerik:GridBoundColumn Display="false" DataField="comment" UniqueName="comment" />
            <telerik:GridBoundColumn Display="false" DataField="sysdate" UniqueName="sysdate" />
            <telerik:GridBoundColumn Display="false" DataField="username" UniqueName="username" />
            <telerik:GridBoundColumn Display="false" DataField="last_name" UniqueName="last_name" />
            <telerik:GridBoundColumn Display="false" DataField="first_name" UniqueName="first_name" />
            <telerik:GridBoundColumn Display="false" DataField="personal_image" UniqueName="personal_image" />
            <telerik:GridBoundColumn Display="false" DataField="reply_to_comment_id" UniqueName="reply_to_comment_id" />
            <telerik:GridBoundColumn Display="false" DataField="depth" UniqueName="depth" />
            <telerik:GridTemplateColumn>
                <ItemTemplate>
                    <asp:Panel ID="PnlComment" CssClass="test-class" runat="server">
                        <div class="">
                            <div style=" margin-top:0px;float:right;">
                    	        <asp:ImageButton ID="ImgBtnLogo" OnClick="ImgBtnUser_OnClick" Width="25" Height="25" runat="server" />
                            </div>
                            <div style="font-size:15px; font-weight:bold;">
                                <span>
                                    <Asp:LinkButton ID="LknBtnUsername" style="color:#34a4d8;" OnClick="LknBtnUsername_OnClick" runat="server" />
                                    <Asp:label ID="LblSysdate" runat="server" />
                                    <asp:ImageButton ID="ImgBtnSetNotPublic" Visible="false" OnClick="ImgBtnSetNotPublic_OnClick" ImageUrl="~/images/delete.png" runat="server" />
                                    <asp:ImageButton ID="ImgBtnReply" OnClick="ImgBtnReply_OnClick" ImageUrl="~/images/icons/small/reply.png" runat="server" />
                                </span>
                            </div>    
                            <div style="padding:5px 0 0 5px;">
                                <div class="" style="font-size:14px; display:inline-block;">
                                    <span>
                                        <div style="float:left;width:100%;text-align:justify; display:inline-block;float:left;">
                                            <Asp:label ID="LblComment" runat="server" />
                                        </div>
                                    </span>
                                </div>
                            </div>
                            <asp:Panel ID="PnlReply" Visible="false" style="padding:5px 0 0 5px; display:inline-block; margin-bottom:50px; float:left;" runat="server">
                                <div style="padding:0px;">
                                    <div class="" style="font-size:14px; display:inline-block;">
                                        <span>
                                            <div style="float:left;width:100%;text-align:justify;">
                                                <telerik:RadTextBox ID="RtbxNewComment" runat="server" EmptyMessage="Add your comment..." MaxLength="2000" TextMode="MultiLine" Rows="5" Width="550" Height="150px" />                                              
                                                <div>
                                                    <asp:Label ID="LblNewCommentError" CssClass="error" runat="server" />
                                                </div>
                                            </div>
                                        </span>
                                    </div>
                                </div>
                                <div style="float:right;display:inline-block; position:absolute;">
                                    <telerik:RadButton ID="RbtnCancel" OnClick="RbtnCancel_OnClick" CssClass="btncommunity" runat="server">
                                        <ContentTemplate>
                                            <span>
                                                <asp:Label ID="LblCancelText" runat="server" />
                                            </span>
                                        </ContentTemplate>
                                    </telerik:RadButton>   
                                    <telerik:RadButton ID="RbtnAddComment" OnClick="RbtnAddComment_OnClick" CssClass="btncommunity" runat="server">
                                        <ContentTemplate>
                                            <span>
                                                <asp:Label ID="LblAddCommentText" runat="server" />
                                            </span>
                                        </ContentTemplate>
                                    </telerik:RadButton>
                                </div>
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
<controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />