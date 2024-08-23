<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_RdgFollowing.ascx.cs" Inherits="WdS.ElioPlus.Controls.Community.Grids.uc_RdgFollowing" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<telerik:RadGrid ID="RdgFollowing" AllowPaging="true" Width="950" PageSize="10" Skin="MetroTouch" CssClass="rgdd" OnPageIndexChanged="RdgFollowing_PageIndexChanged" OnItemCreated="RdgFollowing_ItemCreated" OnItemDataBound="RdgFollowing_OnItemDataBound" OnNeedDataSource="RdgFollowing_OnNeedDataSource" AutoGenerateColumns="false" runat="server" PagerStyle-Position="Bottom">
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
            <telerik:GridTemplateColumn>
                <ItemTemplate>
                    <div class="">                                               
                        <div style="width:15px; padding:5px; text-align:center; margin-top:0px;float:left;">                    	                            
                            <asp:Image ID="ImgFPersonal" runat="server" /> 
                        </div>
                        <div style="padding:10px; margin-left:20px;">
                            <h2>
                                <asp:Label ID="LblFUser" CssClass="bold" runat="server" />
                            </h2>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblFUserJobText" CssClass="bold" runat="server" />                                                                
                                    </td>
                                    <td>
                                        <asp:Label ID="LblFUserJob" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LblFSummaryText" CssClass="bold" runat="server" />
                                    </td>
                                    <td>
                                        <div style="width:740px;">
                                            <asp:Label ID="LblFSummary" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ItemTemplate>
            </telerik:GridTemplateColumn>
        </Columns>
    </MasterTableView>
</telerik:RadGrid>
<controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />