<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_RdgPosts.ascx.cs" Inherits="WdS.ElioPlus.Controls.Community.Grids.uc_RdgPosts" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<telerik:RadGrid ID="RdgPostsResults" AllowPaging="true" PageSize="10" Skin="MetroTouch" CssClass="RgPosts" OnPageIndexChanged="RdgPostsResults_PageIndexChanged" OnItemCreated="RdgPostsResults_ItemCreated" OnItemDataBound="RdgPostsResults_OnItemDataBound" OnNeedDataSource="RdgPostsResults_OnNeedDataSource" AutoGenerateColumns="false" runat="server" PagerStyle-Position="Bottom">
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
            <telerik:GridBoundColumn DataField="sysdate" Display="false" UniqueName="sysdate" />
            <telerik:GridBoundColumn DataField="must_read" Display="false" UniqueName="must_read" />
            <telerik:GridBoundColumn DataField="total_votes" Display="false" UniqueName="total_votes" />
            <telerik:GridBoundColumn DataField="total_comments" Display="false" UniqueName="total_comments" />
            <telerik:GridBoundColumn DataField="username" Display="false" UniqueName="username" />
            <telerik:GridBoundColumn DataField="last_name" Display="false" UniqueName="last_name" />
            <telerik:GridBoundColumn DataField="first_name" Display="false" UniqueName="first_name" />
            <telerik:GridBoundColumn DataField="personal_image" Display="false" UniqueName="personal_image" />
            <telerik:GridBoundColumn DataField="position" Display="false" UniqueName="position" />
            <telerik:GridBoundColumn DataField="community_summary_text" Display="false" UniqueName="community_summary_text" />
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
                        <div style="padding-left:50px;">
                            <div class="" style="font-size:16px; width:850px; display:inline-block;">
                                <span>
                                    <div style="float:left;">
                                        <a id="aUrlTopic" target="_blank" runat="server">                                 
                                        <asp:Label ID="LblaUrlTopic" CssClass="bold" style="text-decoration:underline;" runat="server" />
                                        </a>
                                        <a id="aTitle" runat="server">
                                            <asp:Label ID="LblTitle" CssClass="bold" style="text-decoration:underline;" runat="server" />
                                        </a>
                                        <%--<asp:LinkButton ID="LnkBtnTitle" CssClass="bold" OnClick="LnkBtnComments_OnClick" style="text-decoration:underline;" runat="server" />--%>
                                        <asp:ImageButton ID="ImgBtnSetNotPublic" Visible="false" OnClick="ImgBtnSetNotPublic_OnClick" ImageUrl="/images/delete.png" runat="server" />
                                        <asp:ImageButton ID="ImgBtnMustRead" Visible="false" OnClick="ImgBtnMustRead_OnClick" runat="server" />
                                        <asp:label ID="LblUrlTopic" style="color:#b6b8b9; margin-left:30px;" runat="server" Visible="false" />
                                    </div>                                    
                                </span>                                                                
                            </div>                                  
                            <div style="font-size:14px;">
                                <span>
                                    <Asp:label ID="LblLastUpdate" runat="server" />
                                    <Asp:label ID="LblUser"  runat="server" />
                                    <a id="aUsername" runat="server">
                                        <Asp:Label ID="LblUsername" style="color:#39b4ef;" runat="server" />
                                    </a>
                                </span>
                            </div>                        
                            <div style="background-image:url(/images/icons/chat/Chat1.png); color:#fff; margin-right:5px; height:25px; width:30px; float:left; text-align:center;">
                                <div style="margin-top:0px; font-size:13px;color:#ffffff;">
                                    <span>
                                        <Asp:label ID="LblComments" runat="server" />
                                    </span>
                                </div>
                            </div>                                            
                            <div style="font-size:14px; margin-top:10px;">
                                <a id="aComments" runat="server">
                                    <asp:Label ID="LblaComments" style="color:#39b4ef;font-style:italic; text-decoration:none;" runat="server" />
                                </a>
                                <Asp:label ID="LblSlash" Text=" | " runat="server" />                               
                                <asp:LinkButton ID="LnkBtnSummary" OnClick="LnkBtnSummary_OnClick" style="color:#39b4ef;font-style:italic; text-decoration:none;" runat="server" />
                            </div>
                        </div>
                        <div>
                            <telerik:RadPanelBar ID="RpbPost" CssClass="hidden" style="background-color:#f0f3f5; border-color:#fff; margin-left:50px;" Width="850px" runat="server">
	                            <Items>
		                            <telerik:RadPanelItem style="border-width:0px; border-style:none;border-color:#fff;">
			                            <Items>
				                            <telerik:RadPanelItem style="border-width:0px; border-style:none;border-color:#fff;">
					                            <ContentTemplate>
						                            <asp:Panel ID="PnlPostContent" style="margin-top:10px; text-align:justify; padding:10px;" runat="server">
						                                <asp:Label ID="LblPostContent" runat="server" />
						                            </asp:Panel>
					                            </ContentTemplate>
				                            </telerik:RadPanelItem>
			                            </Items>
		                            </telerik:RadPanelItem>
	                            </Items>
                            </telerik:RadPanelBar>
                        </div>
                    </div>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
<controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />