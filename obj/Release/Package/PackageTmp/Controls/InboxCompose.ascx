<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InboxCompose.ascx.cs" Inherits="WdS.ElioPlus.Controls.InboxCompose" %>

<div class="inbox-compose form-horizontal">
    <div id="divError" runat="server" visible="false" class="alert alert-danger">
        <strong><asp:Label ID="LblError" runat="server" /></strong><asp:Label ID="LblErrorContent" runat="server" />
    </div>
    <div id="divSuccess" runat="server" visible="false" class="alert alert-success">
        <strong><asp:Label ID="LblSuccess" runat="server" /></strong><asp:Label ID="LblSuccessContent" runat="server" />
    </div>
    <div id="divWarning" runat="server" visible="false" class="alert alert-warning">
        <strong><asp:Label ID="LblWarning" runat="server" /></strong><asp:Label ID="LblWarningContent" runat="server" />
    </div>
    <div id="divInfo" runat="server" visible="false" class="alert alert-info">
        <strong><asp:Label ID="LblInfo" runat="server" /></strong><asp:Label ID="LblInfoContent" runat="server" />
    </div>
    <div class="inbox-header">
        <h3 class="pull-left">
            <asp:Label ID="LblTitle" runat="server" />
        </h3>
    </div>
    <br />
    <div class="inbox-form-group mail-to">
        <asp:Label ID="LblTo" runat="server" />
        <div class="controls controls-to" style="padding-right:0px; margin-left:0px;">
            
            <telerik:RadAutoCompleteBox RenderMode="Lightweight" runat="server" ID="RacbxRecipient" Delimiter=" "
                Width="100%" DataSourceID="AllCompanies" DataTextField="company_name" DataValueField="id"
                Filter="StartsWith" TextSettings-SelectionMode="Single" AllowCustomEntry="true" InputType="Text" DropDownHeight="400"
                DropDownWidth="400" EmptyMessage="Start typing the name of the company that you want to communicate with">
                <DropDownItemTemplate>
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" style="width: 25%; padding-left: 10px;">                            
                                <asp:Image ID="ImgCompanyLogo" AlternateText="company logo" Width="50" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "company_logo")%>' />
                            </td>
                            <td align="left" style="width: 400px; padding-left: 10px;">
                                <%# DataBinder.Eval(Container.DataItem, "company_name")%>
                                <br />
                                <%# DataBinder.Eval(Container.DataItem, "company_type")%>
                            </td>
                        </tr>
                    </table>
                </DropDownItemTemplate>
            </telerik:RadAutoCompleteBox>
            <asp:SqlDataSource ID="AllCompanies" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                ProviderName="System.Data.SqlClient" SelectCommand="SELECT [id], [company_name], [company_type], [company_logo] FROM [Elio_users] WHERE [account_status] = 1 AND [is_public] = 1">
            </asp:SqlDataSource>        
        </div>
    </div>
    <br />
    <div class="inbox-form-group">
        <asp:Label ID="LblSubject" runat="server" />
        <div class="controls" style="border: thin solid #cdcdcd; margin-left:0px;">
            <asp:TextBox ID="TbxSubject" CssClass="form-control" MaxLength="45" placeholder="Type here a subject for your message..." runat="server" />
        </div>
    </div>
    <br />
    <div class="inbox-form-group">
        <asp:Label ID="LblMessage" runat="server" />
        <asp:TextBox ID="TbxMessage" CssClass="inbox-editor inbox-wysihtml5 form-control"
            TextMode="MultiLine" Rows="15" MaxLength="2000" placeholder="Type here the content of your message..." runat="server" />
    </div>
    <div class="inbox-compose-btn">
        <asp:Button ID="BtnSend" CssClass="btn green" OnClick="BtnSendMessage_OnClick" runat="server" />
        <asp:Button ID="BtnCancel" CssClass="btn default" OnClick="BtnDiscard_OnClick" runat="server" />    
    </div>
</div>

<script type="text/javascript">
    
    function test(sender, args) {
        var autoCompleteBox = $find("<%= RacbxRecipient.ClientID %>");
        var textBox = $find("RacbxRecipient");
        if (textBox.get_value() == "") return;
        var entry = new Telerik.Web.UI.AutoCompleteBoxEntry();
        entry.set_text(textBox.get_value());
        autoCompleteBox.get_entries().add(entry);
    }
</script>