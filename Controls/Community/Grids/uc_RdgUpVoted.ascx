<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_RdgUpVoted.ascx.cs" Inherits="WdS.ElioPlus.Controls.Community.Grids.uc_RdgUpVoted" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<telerik:RadGrid ID="RdgUpVoted" AllowPaging="true" Width="950" PageSize="10" Skin="MetroTouch" CssClass="rgdd" OnPageIndexChanged="RdgUpVoted_PageIndexChanged" OnItemCreated="RdgUpVoted_ItemCreated" OnItemDataBound="RdgUpVoted_OnItemDataBound" OnNeedDataSource="RdgUpVoted_OnNeedDataSource" AutoGenerateColumns="false" runat="server" PagerStyle-Position="Bottom">
    <MasterTableView>
        <NoRecordsTemplate>
            <div class="emptyGridHolder">
                <asp:Literal ID="LtlNoDataFound" runat="server" />
            </div>
        </NoRecordsTemplate>
        <Columns>
            <telerik:GridBoundColumn DataField="id" Display="false" UniqueName="id" />
            <telerik:GridBoundColumn DataField="creator_user_id" Display="false" UniqueName="creator_user_id" />
            <telerik:GridBoundColumn DataField="topic" Display="false" UniqueName="topic" />
            <telerik:GridBoundColumn DataField="topic_url" Display="false" UniqueName="topic_url" />
            <telerik:GridBoundColumn DataField="post" Display="false" UniqueName="post" />
            <telerik:GridBoundColumn DataField="last_update" Display="false" UniqueName="last_update" />
            <telerik:GridBoundColumn DataField="total_votes" Display="false" UniqueName="total_votes" />
            <telerik:GridBoundColumn DataField="total_comments" Display="false" UniqueName="total_comments" />
            <telerik:GridTemplateColumn>
                <ItemTemplate>
                    <div class="">                                               
                        <div style="width:15px; padding:5px; text-align:center; margin-top:0px;float:left;">
                    	    <asp:ImageButton ID="ImgBtnUpVote" OnClick="ImgBtnUpVote_OnClick" runat="server" />
                            <Asp:label ID="LblVotes" runat="server" />
                        </div>
                        <div style="height:130px; margin-left:40px; padding:5px;">
                            <div class="" style="font-size:18px; width:750px; display:inline-block;">
                                <span>
                                    <div style="float:left;width:520px;">
                                        <asp:Image ID="ImgSeePost" ImageUrl="~/images/icons/small/up_1.png" runat="server" />
                                        <Asp:LinkButton ID="LnkBtnTopic" CssClass="bold" style="text-decoration:underline;" OnClick="LnkBtnTopic_OnClick" runat="server" />
                                        <asp:ImageButton ID="ImgBtnSetNotPublic" Visible="false" OnClick="ImgBtnSetNotPublic_OnClick" ImageUrl="~/images/delete.png" runat="server" />
                                    </div>
                                    <div style="float:right;">
                                        <asp:HyperLink ID="HpLnkUrlTopic" style="color:#b6b8b9;" Target="_blank" runat="server" />
                                    </div>
                                </span>                                                                
                            </div>
                            <div>
                                <telerik:RadPanelBar ID="RpbPost" CssClass="hidden" style="background-color:#f0f3f5; border-color:#fff;" Width="100%" runat="server">
	                                <Items>
		                                <telerik:RadPanelItem style="border-width:0px; border-style:none;border-color:#fff;">
			                                <Items>
				                                <telerik:RadPanelItem style="border-width:0px; border-style:none;border-color:#fff;">
					                                <ContentTemplate>
						                                <asp:Panel ID="PnlUpVotedPostContent" style="margin-top:10px; padding:10px;" runat="server">
						                                    <asp:Label ID="LblUpVotedPostContent" runat="server" />
						                                </asp:Panel>
					                                </ContentTemplate>
				                                </telerik:RadPanelItem>
			                                </Items>
		                                </telerik:RadPanelItem>
	                                </Items>
                                </telerik:RadPanelBar>
                            </div>
                            <div style="font-size:15px;">
                                <span>
                                    <Asp:label ID="LblLastUpdate" runat="server" />
                                </span>
                            </div>                                                    
                            <div style="background-image:url(images/icons/chat/cht_3.png); color:#fff; margin-right:5px; height:50px; width:50px; float:left; text-align:center;">
                                <div style="margin-top:3px; color:#fff;">
                                    <span>
                                        <Asp:label ID="LblComments" runat="server" />                                                                        
                                    </span>
                                </div>
                            </div>                                            
                            <div style="margin-top:20px; color:#fff;">
                                <asp:LinkButton ID="LnkBtnComments" OnClick="LnkBtnComments_OnClick" style="color:#34a4d8;font-style:italic; text-decoration:none;" runat="server" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
<controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />