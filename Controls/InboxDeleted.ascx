<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InboxDeleted.ascx.cs" Inherits="WdS.ElioPlus.Controls.InboxDeleted" %>

<div class="inbox-header">
    <h3 class="pull-left"><asp:Label ID="LblTitle" runat="server" /></h3>                        
</div>
<table class="table table-striped table-advance table-hover">    
    <thead>
        <tr>
            <th colspan="3">                
                <div class="btn-group input-actions">
                    <a class="btn btn-sm blue btn-outline dropdown-toggle sbold" data-toggle="dropdown">
                        <asp:Label ID="LblActions" runat="server" />
                        <i class="fa fa-angle-down"></i>
                    </a>
                    <ul class="dropdown-menu">
                        <li>
                            <a id="aRestore" runat="server" onserverclick="Restore_OnClick">
                                <i class="fa fa-pencil"></i><asp:Label ID="LblRestore" runat="server" />
                            </a>
                        </li>
                        <li class="divider"> </li>
                        <li>
                            <a id="aDelete" runat="server" onserverclick="Delete_OnClick">
                                <i class="fa fa-trash-o"></i><asp:Label ID="LblDelete" runat="server" />
                            </a>
                        </li>
                    </ul>
                </div>
            </th>            
        </tr>
    </thead>
    <tbody>
        <div id="divInfo" runat="server" visible="false" class="alert alert-info">
            <strong><asp:Label ID="LblInfo" runat="server" /></strong><asp:Label ID="LblInfoContent" runat="server" />
        </div>
        <telerik:RadGrid ID="RdgMessages" AllowPaging="true" PageSize="5" PagerStyle-Position="Bottom" PagerStyle-Mode="Slider" OnItemDataBound="RdgMessages_OnItemDataBound" OnNeedDataSource="RdgMessages_OnNeedDataSource" AutoGenerateColumns="false" runat="server" AlternatingItemStyle-CssClass="table table-striped table-advance table-hover" ItemStyle-CssClass="table table-striped table-advance table-hover" CssClass="table table-striped table-advance table-hover" PagerStyle-HorizontalAlign="Center">
            <MasterTableView>                                                
                <Columns>
                    <telerik:GridBoundColumn DataField="id" UniqueName="id" />
                    <telerik:GridTemplateColumn>
                        <ItemTemplate>
                            <table class="table table-striped table-advance table-hover">
                                <tr id="rowMessage" runat="server">
                                    <td class="inbox-small-cells">
                                        <asp:CheckBox ID="CbxMessage" runat="server" CssClass="mail-checkbox" />                                        
                                    </td>
                                    <td class="inbox-small-cells">
                                        <a id="aViewCompany" runat="server">
                                            <i title="View company's profile" class="fa fa-external-link"></i>
                                        </a>
                                    </td>
                                    <td class="view-message hidden-xs" style="width:250px;">
                                        <asp:LinkButton ID="LnkBtnSenderName" ForeColor="black" OnClick="LnkBtnPreview_OnClick" runat="server" />
                                    </td>
                                    <td class="view-message">
                                        <asp:LinkButton ID="LnkBtnMessageSubject" ForeColor="black" OnClick="LnkBtnPreview_OnClick" runat="server" />
                                    </td>                                    
                                    <td class="view-message text-right">
                                        <asp:Label ID="LblDateReceived" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>        
    </tbody>
</table>
