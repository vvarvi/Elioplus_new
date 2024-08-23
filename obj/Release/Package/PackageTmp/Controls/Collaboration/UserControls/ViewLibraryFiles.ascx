<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewLibraryFiles.ascx.cs" Inherits="WdS.ElioPlus.Controls.Collaboration.UserControls.ViewLibraryFiles" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:UpdatePanel runat="server" ID="UpdatePanel7">
        <ContentTemplate>
            <asp:Panel ID="PnlViewLogs" runat="server">
                <div style="padding:5px;">
                    <h4><asp:Label ID="LblViewSelectedCategoryFiles" runat="server" /></h4>
                </div>
                <telerik:RadGrid ID="RdgViewLogs" AllowPaging="true" PageSize="10" CssClass="rgdd" HeaderStyle-BackColor="#39b4ef" PagerStyle-Position="TopAndBottom" HeaderStyle-ForeColor="#ffffff" OnNeedDataSource="ViewLogs_OnNeedDataSource" OnItemCreated="ViewLogs_ItemCreated" OnItemDataBound="ViewLogs_OnItemDataBound" AutoGenerateColumns="false" runat="server">
                    <MasterTableView>
                        <NoRecordsTemplate>
                            <div class="emptyGridHolder">
                                <asp:Literal ID="LtlNoDataFound" runat="server" />
                            </div>
                        </NoRecordsTemplate>
                        <Columns> 
                            <telerik:GridBoundColumn HeaderStyle-Width="20" Display="false" DataField="id" UniqueName="id" />
                            <telerik:GridBoundColumn Display="false" DataField="user_id" UniqueName="user_id" />
                            <telerik:GridBoundColumn Display="false" DataField="uploaded_by_user_id" UniqueName="uploaded_by_user_id" />
                            <telerik:GridBoundColumn HeaderStyle-Width="20" Display="false" DataField="user_guid" UniqueName="user_guid" />
                            <telerik:GridBoundColumn HeaderStyle-Width="50" Display="false" DataField="file_path" UniqueName="file_path" />
                            <telerik:GridBoundColumn HeaderStyle-Width="100" Display="false" DataField="selected_category" UniqueName="selected_category" />
                            <telerik:GridBoundColumn HeaderStyle-Width="100" Display="false" DataField="company_name" UniqueName="company_name" />
                            <telerik:GridBoundColumn HeaderStyle-Width="100" DataField="file_type" UniqueName="file_type" />
                            <telerik:GridBoundColumn Display="false" HeaderStyle-Width="70" DataField="fileName" UniqueName="fileName" /> 
                            <telerik:GridTemplateColumn HeaderStyle-Width="50" DataField="fileName_Temp" UniqueName="fileName_Temp" >
                                <ItemTemplate>
                                    <asp:HyperLink ID="HpLnkPreViewLogFiles" Target="_blank" style="text-decoration:underline;" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderStyle-Width="50" DataField="from_to" UniqueName="from_to" >
                                <ItemTemplate>
                                    <asp:Image ID="ImgFromTo" AlternateText="send" ImageUrl="~/images/icons/small/info.png" style="cursor:pointer;" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderStyle-Width="80" Display="false" DataField="fileSize" UniqueName="fileSize" />
                            <telerik:GridBoundColumn HeaderStyle-Width="100" DataField="date" UniqueName="date" />
                            <telerik:GridBoundColumn Display="false" DataField="is_new" UniqueName="is_new" />
                            <telerik:GridBoundColumn Display="false" DataField="collaboration_group_id" UniqueName="collaboration_group_id" />
                            <telerik:GridTemplateColumn HeaderStyle-Width="80" DataField="actions" UniqueName="actions">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgBtnPreViewLogFiles" OnClick="ImgBtnPreViewLogFiles_OnClick" ImageUrl="~/Images/preview.png" ToolTip="Set as not new" runat="server" />
                                    <asp:ImageButton ID="ImgBtnDeleteLogFiles" OnClick="ImgBtnDeleteLogFiles_OnClick" ToolTip="Delete log file" ImageUrl="~/Images/icons/small/delete.png" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </asp:Panel>
            <controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server"/>
        </ContentTemplate>
</asp:UpdatePanel>
