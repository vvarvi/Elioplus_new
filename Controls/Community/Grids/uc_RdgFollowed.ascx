<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_RdgFollowed.ascx.cs" Inherits="WdS.ElioPlus.Controls.Community.Grids.uc_RdgFollowed" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<telerik:RadGrid ID="RdgFollowed" AllowPaging="true" Width="950" PageSize="10" Skin="MetroTouch" CssClass="rgdd" OnPageIndexChanged="RdgFollowed_PageIndexChanged" OnItemCreated="RdgFollowed_ItemCreated" OnItemDataBound="RdgFollowed_OnItemDataBound" OnNeedDataSource="RdgFollowed_OnNeedDataSource" AutoGenerateColumns="false" runat="server" PagerStyle-Position="Bottom">
    <MasterTableView>
        <NoRecordsTemplate>
            <div class="emptyGridHolder">
                <asp:Literal ID="LtlNoDataFound" runat="server" />
            </div>
        </NoRecordsTemplate>
        <Columns>
            <telerik:GridBoundColumn DataField="id" Display="false" UniqueName="id" />
            <telerik:GridBoundColumn DataField="community_status" Display="false" UniqueName="community_status" />
            <telerik:GridBoundColumn DataField="last_name" Display="false" UniqueName="last_name" />
            <telerik:GridBoundColumn DataField="first_name" Display="false" UniqueName="first_name" />
            <telerik:GridBoundColumn DataField="personal_image" Display="false" UniqueName="personal_image" />
            <telerik:GridBoundColumn DataField="username" Display="false" UniqueName="username" />
            <telerik:GridBoundColumn DataField="position" Display="false" UniqueName="position" />
            <telerik:GridBoundColumn DataField="community_summary_text" Display="false" UniqueName="community_summary_text" />
            <telerik:GridBoundColumn DataField="linkedin" Display="false" UniqueName="linkedin" />
            <telerik:GridBoundColumn DataField="twitter" Display="false" UniqueName="twitter" />
            <telerik:GridTemplateColumn>
                <ItemTemplate>
                    <div class="">
                        <div style="width:30px; height:30px; padding:5px; text-align:center; margin-top:0px;float:left;">
                            <a id="aFPersonal" runat="server">                   	                            
                                <asp:Image ID="ImgFPersonal" width="30" height="30" runat="server" /> 
                            </a>
                        </div>
                        <div style="padding:1px; margin-left:60px; font-size:14px;">
                            <h2>
                                <a id="aFUser" runat="server">
                                    <asp:Label ID="LblFUser" runat="server" />
                                </a>
                                <%--<asp:LinkButton ID="LblFUser" OnClick="LblFUser_OnClick" CssClass="bold" runat="server" />--%>
                                <asp:ImageButton ID="ImgBtnUserDetails" OnClick="ImgBtnUserDetails_OnClick" ImageUrl="/Images/icons/u_info.png" runat="server" />
                            </h2>                            
                            <div>
                                <telerik:RadPanelBar ID="RpbUserDetails" CssClass="hidden" style="background-color:#f0f3f5; border-color:#fff;" Width="100%" runat="server">
	                                <Items>
		                                <telerik:RadPanelItem style="border-width:0px; border-style:none;border-color:#fff;">
			                                <Items>
				                                <telerik:RadPanelItem style="border-width:0px; border-style:none;border-color:#fff;">
					                                <ContentTemplate>
						                                <table>
                                                            <tr>
                                                                <td>
                                                                    <div style="width:120px;">
                                                                        <asp:Label ID="LblFUserJobText" CssClass="bold" runat="server" />                                                                
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LblFUserJob" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div style="width:120px;">
                                                                        <asp:Label ID="LblSocialText" CssClass="bold" runat="server" />                                                                
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div class="">
                                                                        <asp:HyperLink id="LinkedinLink" class="user-profile-twitter-link grey" target="_blank" runat="server" visible="false">
                                                                            <asp:Image ID="Image2" ImageUrl="/images/icons/small/in.png" runat="server" />                            
                                                                        </asp:HyperLink>
                                                                        <asp:HyperLink id="TwitterLink" class="user-profile-linkedin-link grey" target="_blank" runat="server" visible="false">
                                                                            <asp:Image ID="Image3" runat="server" ImageUrl="/images/icons/small/tw.png" />
                                                                        </asp:HyperLink>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div style="width:120px;">
                                                                        <asp:Label ID="LblFSummaryText" CssClass="bold" runat="server" />
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div style="width:100%">
                                                                        <asp:Label ID="LblFSummary" runat="server" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
					                                </ContentTemplate>
				                                </telerik:RadPanelItem>
			                                </Items>
		                                </telerik:RadPanelItem>
	                                </Items>
                                </telerik:RadPanelBar>
                            </div>                            
                        </div>                                                
                    </div>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
<controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />