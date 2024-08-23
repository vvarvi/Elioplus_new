<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Statistics.ascx.cs" Inherits="WdS.ElioPlus.Controls.Dashboard.Charts.Statistics" %>

<asp:Panel ID="PnlRegistrations" style="padding:10px;" runat="server">
    <h2><asp:Label ID="LblRegistrations" Text="Monthly Total Registrations" runat="server" /></h2>
    <telerik:RadGrid ID="RdgRegistrations" AllowPaging="true" PageSize="7" CssClass="rgdd" HeaderStyle-BackColor="#39b4ef" PagerStyle-Position="Bottom" HeaderStyle-ForeColor="#ffffff" OnNeedDataSource="RdgRegistrations_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
        <MasterTableView>
            <NoRecordsTemplate>
                <div class="emptyGridHolder">
                    <asp:Literal ID="LtlNoDataFound" runat="server" />
                </div>
            </NoRecordsTemplate>                                       
            <Columns> 
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="80" DataField="year" UniqueName="year" />               
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_1" UniqueName="month_1" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_2" UniqueName="month_2" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_3" UniqueName="month_3" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_4" UniqueName="month_4" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_5" UniqueName="month_5" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_6" UniqueName="month_6" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_7" UniqueName="month_7" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_8" UniqueName="month_8" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_9" UniqueName="month_9" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_10" UniqueName="month_10" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_11" UniqueName="month_11" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_12" UniqueName="month_12" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="sum" UniqueName="sum" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="avg" UniqueName="avg" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Panel>

<asp:Panel ID="PnlThirdRegistrations" style="padding:10px;" runat="server">
    <h2><asp:Label ID="LblThirdRegistrations" Text="Monthly Total Third Party Registrations (Completed)" runat="server" /></h2>
    <telerik:RadGrid ID="RdgThirdRegistrations" AllowPaging="true" PageSize="7" CssClass="rgdd" HeaderStyle-BackColor="#39b4ef" PagerStyle-Position="Bottom" HeaderStyle-ForeColor="#ffffff" OnNeedDataSource="RdgThirdRegistrations_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
        <MasterTableView>
            <NoRecordsTemplate>
                <div class="emptyGridHolder">
                    <asp:Literal ID="LtlNoDataFound" runat="server" />
                </div>
            </NoRecordsTemplate>                                       
            <Columns> 
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="80" DataField="year" UniqueName="year" />               
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_1" UniqueName="month_1" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_2" UniqueName="month_2" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_3" UniqueName="month_3" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_4" UniqueName="month_4" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_5" UniqueName="month_5" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_6" UniqueName="month_6" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_7" UniqueName="month_7" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_8" UniqueName="month_8" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_9" UniqueName="month_9" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_10" UniqueName="month_10" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_11" UniqueName="month_11" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_12" UniqueName="month_12" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="sum" UniqueName="sum" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="avg" UniqueName="avg" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Panel>

<asp:Panel ID="PnlRegisteredUsers" style="padding:10px; margin-top:20px;" runat="server">
    <h2><asp:Label ID="Header1" Text="Monthly Full Registrations" runat="server" /></h2>
    <telerik:RadGrid ID="RdgRegisteredUsers" AllowPaging="true" PageSize="7" CssClass="rgdd" HeaderStyle-BackColor="#39b4ef" PagerStyle-Position="Bottom" HeaderStyle-ForeColor="#ffffff" OnNeedDataSource="RdgRegisteredUsers_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
        <MasterTableView>
            <NoRecordsTemplate>
                <div class="emptyGridHolder">
                    <asp:Literal ID="LtlNoDataFound" runat="server" />
                </div>
            </NoRecordsTemplate>                                       
            <Columns> 
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="80" DataField="year" UniqueName="year" />               
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_1" UniqueName="month_1" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_2" UniqueName="month_2" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_3" UniqueName="month_3" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_4" UniqueName="month_4" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_5" UniqueName="month_5" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_6" UniqueName="month_6" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_7" UniqueName="month_7" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_8" UniqueName="month_8" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_9" UniqueName="month_9" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_10" UniqueName="month_10" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_11" UniqueName="month_11" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_12" UniqueName="month_12" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="sum" UniqueName="sum" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="avg" UniqueName="avg" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Panel>
<asp:Panel ID="PnlNotRegisteredUsers" style="padding:10px; margin-top:20px;" runat="server">
    <h2><asp:Label ID="Header2" Text="Monthly Not Full Registrations" runat="server" /></h2>
    <telerik:RadGrid ID="RdgNotRegisteredUsers" AllowPaging="true" PageSize="7" CssClass="rgdd" HeaderStyle-BackColor="#39b4ef" PagerStyle-Position="Bottom" HeaderStyle-ForeColor="#ffffff" OnNeedDataSource="RdgNotRegisteredUsers_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
        <MasterTableView>
            <NoRecordsTemplate>
                <div class="emptyGridHolder">
                    <asp:Literal ID="LtlNoDataFound" runat="server" />
                </div>
            </NoRecordsTemplate>                                       
            <Columns>                
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="80" DataField="year" UniqueName="year" />               
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_1" UniqueName="month_1" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_2" UniqueName="month_2" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_3" UniqueName="month_3" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_4" UniqueName="month_4" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_5" UniqueName="month_5" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_6" UniqueName="month_6" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_7" UniqueName="month_7" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_8" UniqueName="month_8" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_9" UniqueName="month_9" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_10" UniqueName="month_10" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_11" UniqueName="month_11" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_12" UniqueName="month_12" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="sum" UniqueName="sum" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="avg" UniqueName="avg" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Panel>
<asp:Panel ID="PnlRegVsAll" style="padding:10px; margin-top:20px;" runat="server">
    <h2><asp:Label ID="Header3" Text="Monthly Full Registrations vs Total Registrations (%)" runat="server" /></h2>
    <telerik:RadGrid ID="RdgRegVsAll" AllowPaging="true" PageSize="7" CssClass="rgdd" HeaderStyle-BackColor="#39b4ef" PagerStyle-Position="Bottom" HeaderStyle-ForeColor="#ffffff" OnNeedDataSource="RdgRegVsAll_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
        <MasterTableView>
            <NoRecordsTemplate>
                <div class="emptyGridHolder">
                    <asp:Literal ID="LtlNoDataFound" runat="server" />
                </div>
            </NoRecordsTemplate>                                       
            <Columns>                
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="year" UniqueName="year" />               
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_1" UniqueName="month_1" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_2" UniqueName="month_2" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_3" UniqueName="month_3" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_4" UniqueName="month_4" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_5" UniqueName="month_5" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_6" UniqueName="month_6" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_7" UniqueName="month_7" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_8" UniqueName="month_8" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_9" UniqueName="month_9" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_10" UniqueName="month_10" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_11" UniqueName="month_11" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_12" UniqueName="month_12" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="sum" UniqueName="sum" />
                <%--<telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="avg" UniqueName="avg" />--%>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Panel>
<asp:Panel ID="PnlNotRegVsAll" style="padding:10px; margin-top:20px;" runat="server">
    <h2><asp:Label ID="Header4" Text="Monthly Not Full Registrations vs Total Registrations (%)" runat="server" /></h2>
    <telerik:RadGrid ID="RdgNotRegVsAll" AllowPaging="true" PageSize="7" CssClass="rgdd" HeaderStyle-BackColor="#39b4ef" PagerStyle-Position="Bottom" HeaderStyle-ForeColor="#ffffff" OnNeedDataSource="RdgNotRegVsAll_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
        <MasterTableView>
            <NoRecordsTemplate>
                <div class="emptyGridHolder">
                    <asp:Literal ID="LtlNoDataFound" runat="server" />
                </div>
            </NoRecordsTemplate>                                       
            <Columns>                
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="year" UniqueName="year" />               
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_1" UniqueName="month_1" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_2" UniqueName="month_2" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_3" UniqueName="month_3" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_4" UniqueName="month_4" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_5" UniqueName="month_5" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_6" UniqueName="month_6" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_7" UniqueName="month_7" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_8" UniqueName="month_8" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_9" UniqueName="month_9" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_10" UniqueName="month_10" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_11" UniqueName="month_11" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="month_12" UniqueName="month_12" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="sum" UniqueName="sum" />
                <%--<telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="30" DataField="avg" UniqueName="avg" />--%>
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Panel>
<asp:Panel ID="PnlAllTypes" Visible="false" style="padding:10px; margin-top:20px;" runat="server">
    <h2><asp:Label ID="Header5" Text="Monthly Not Full Registrations" runat="server" /></h2>
    <telerik:RadGrid ID="RdgAllTypes" AllowPaging="true" PageSize="7" CssClass="rgdd" HeaderStyle-BackColor="#39b4ef" PagerStyle-Position="Bottom" HeaderStyle-ForeColor="#ffffff" OnNeedDataSource="RdgAllTypes_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
        <MasterTableView>
            <NoRecordsTemplate>
                <div class="emptyGridHolder">
                    <asp:Literal ID="LtlNoDataFound" runat="server" />
                </div>
            </NoRecordsTemplate>                                       
            <Columns>                
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="50" DataField="type" UniqueName="type" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="50" DataField="year2014" UniqueName="year2014" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="50" DataField="year2015" UniqueName="year2015" />
                <telerik:GridBoundColumn HeaderText="" HeaderStyle-Width="50" DataField="vs" UniqueName="vs" />
            </Columns>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Panel>