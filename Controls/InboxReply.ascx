<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InboxReply.ascx.cs" Inherits="WdS.ElioPlus.Controls.InboxReply" %>

<form class="inbox-compose form-horizontal" id="fileupload" action="#" method="POST" enctype="multipart/form-data">
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
        <h3 class="pull-left"><asp:Label ID="LblTitle" runat="server" /></h3>                        
    </div>
    <br />    
    <div class="inbox-form-group mail-to">        
        <asp:Label ID="LblTo" CssClass="control-label" runat="server" />
        <div class="controls controls-to">            
            <asp:TextBox ID="TbxRecipient" CssClass="form-control" ReadOnly="true" runat="server" />            
        </div>
    </div>
    <div class="inbox-form-group">
        <br />
        <asp:Label ID="LblSubject" CssClass="control-label" runat="server" />
        <div class="controls">
            <asp:TextBox ID="TbxSubject" CssClass="form-control" MaxLength="45" runat="server" />
        </div>
    </div>
    <div class="inbox-form-group">
        <div class="controls-row">
            <br />
            <asp:Label ID="LblMessage" CssClass="control-label" runat="server" />
            <asp:TextBox ID="TbxReplyContent" CssClass="inbox-editor inbox-wysihtml5 form-control" TextMode="MultiLine" Rows="15" MaxLength="2000" runat="server" />
            <br /> 
            <div id="reply_email_content_body">
                <asp:Label ID="LblOriginalMessage" CssClass="control-label" runat="server" />
                <asp:TextBox ID="TbxOriginalMessage" CssClass="form-control" TextMode="MultiLine" ReadOnly="true" Rows="15" runat="server" />                
            </div>
        </div>
    </div>    
    <div class="inbox-compose-btn">
        <asp:Button ID="BtnSend" CssClass="btn green" OnClick="BtnSendMessage_OnClick" runat="server" />
        <asp:Button ID="BtnCancel" CssClass="btn default" OnClick="BtnDiscard_OnClick" runat="server" />        
    </div>
</form>
