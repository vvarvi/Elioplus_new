<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewInfoLogFiles.ascx.cs" Inherits="WdS.ElioPlus.Controls.Dashboard.Admin.ViewInfoLogFiles" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:UpdatePanel runat="server" ID="UpdatePanel2">
    <ContentTemplate>
        <asp:Panel ID="PnlViewLogs" runat="server">
            <%--<h2><asp:Label ID="LblViewLogFiles" Text="Monthly Users Registrations" runat="server" /></h2>   --%>
            <telerik:RadGrid ID="RdgViewLogs" AllowPaging="true" PageSize="10" CssClass="rgdd" HeaderStyle-BackColor="#39b4ef" PagerStyle-Position="TopAndBottom" HeaderStyle-ForeColor="#ffffff" OnNeedDataSource="ViewLogs_OnNeedDataSource" OnItemCreated="ViewLogs_ItemCreated" OnItemDataBound="ViewLogs_OnItemDataBound" AutoGenerateColumns="false" runat="server">
                <MasterTableView>
                    <NoRecordsTemplate>
                        <div class="emptyGridHolder">
                            <asp:Literal ID="LtlNoDataFound" runat="server" />
                        </div>
                    </NoRecordsTemplate>
                    <Columns> 
                        <telerik:GridBoundColumn HeaderStyle-Width="20" Display="false" DataField="id" UniqueName="id" />
                        <telerik:GridBoundColumn HeaderStyle-Width="50" DataField="directory" UniqueName="directory" />
                        <telerik:GridBoundColumn Display="false" HeaderStyle-Width="70" DataField="fileName" UniqueName="fileName" /> 
                        <telerik:GridTemplateColumn HeaderStyle-Width="50" DataField="fileName_Temp" UniqueName="fileName_Temp" >
                            <ItemTemplate>
                                <a id="aViewLogFilesText" runat="server">
                                    <asp:Label ID="LblViewLogFiles" runat="server" />
                                </a>
                                <asp:HyperLink ID="HpLnkPreViewLogFiles" Visible="false" Target="_blank" style="text-decoration:underline;" runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>            
                        <telerik:GridBoundColumn HeaderStyle-Width="80" DataField="fileSize" UniqueName="fileSize" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100" DataField="date" UniqueName="date" />
                        <telerik:GridTemplateColumn HeaderStyle-Width="80" DataField="actions" UniqueName="actions">
                            <ItemTemplate>
                                <a id="aViewLogFiles" runat="server">
                                    <asp:Image ID="ImgViewLogFiles" ImageUrl="~/Images/preview.png" runat="server" />
                                </a>
                                <asp:HyperLink ID="HpLnkBtnPreViewLogFiles" Visible="false" ImageUrl="~/Images/preview.png" Target="_blank" runat="server" />
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