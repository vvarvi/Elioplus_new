<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_RdgUserComments.ascx.cs" Inherits="WdS.ElioPlus.Controls.Community.Grids.uc_RdgUserComments" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<telerik:RadGrid ID="RdgPostComments" AllowPaging="true" Width="100%" PageSize="10" Skin="MetroTouch" CssClass="rgdd" OnPageIndexChanged="RdgPostComments_PageIndexChanged" OnItemCreated="RdgPostComments_ItemCreated" OnItemDataBound="RdgPostComments_OnItemDataBound" OnNeedDataSource="RdgPostComments_OnNeedDataSource" AutoGenerateColumns="false" runat="server" PagerStyle-Position="Bottom">
    <MasterTableView>
        <NoRecordsTemplate>
            <div class="emptyGridHolder">
                <asp:Literal ID="LtlNoDataFound" runat="server" />
            </div>
        </NoRecordsTemplate>
        <Columns>
            <telerik:GridBoundColumn Display="false" DataField="post_id" UniqueName="post_id" />
            <telerik:GridBoundColumn Display="false" DataField="comment_id" UniqueName="comment_id" />
            <telerik:GridBoundColumn Display="false" DataField="creator_user_id" UniqueName="creator_user_id" />
            <telerik:GridBoundColumn Display="false" DataField="topic" UniqueName="topic" />
            <telerik:GridBoundColumn Display="false" DataField="topic_url" UniqueName="topic_url" />
            <telerik:GridBoundColumn DataField="personal_image" UniqueName="personal_image" />
            <telerik:GridBoundColumn Display="false" DataField="total_votes" UniqueName="total_votes" />
            <telerik:GridTemplateColumn>
                <ItemTemplate>
                    <div>
                        <div style="padding-top:5px; float:right;">
                    	    <asp:ImageButton ID="ImgBtnLogo" Width="30" Height="30" OnClick="ImgBtnUser_OnClick" runat="server" />
                        </div>
                        <div style="width:15px; text-align:center; float:left; font-size:16px; padding-top:5px;">
                    	    <asp:ImageButton ID="ImgBtnUpVote" OnClick="ImgBtnUpVote_OnClick" runat="server" />                            
                            <asp:label ID="LblVotes" runat="server" />                            
                        </div>
                        <div style="float:left; padding-left:15px;">
                            <asp:Image ID="ImgSeePost" ImageUrl="/images/icons/small/up_1.png" runat="server" />
                        </div>
                        <div class="" style="font-size:16px; width:750px; display:inline-block;">
                            <span>
                                <div style="float:left;">
                                    <asp:HyperLink ID="HpLnkUrlTopic" Target="_blank" CssClass="bold" style="text-decoration:underline;" runat="server" />
                                    <asp:LinkButton ID="LnkBtnTitle" CssClass="bold" style="text-decoration:underline;" runat="server" />                                   
                                    <asp:label ID="LblUrlTopic" style="color:#b6b8b9; margin-left:30px;" runat="server" Visible="false" />
                                </div>
                            </span>
                        </div>
                        <div style="font-size:14px; margin-left:20px;">
                            <span>
                                <Asp:label ID="LblSysdate" runat="server" />
                            </span>
                        </div>
                        <div class="" style="font-size:14px; display:inline-block;">
                            <span>
                                <div style="float:left;width:100%;text-align:justify; display:inline-block;float:left;">
                                    <Asp:label ID="LblComment" runat="server" />
                                </div>
                            </span>
                        </div>
                    </div>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
<controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />