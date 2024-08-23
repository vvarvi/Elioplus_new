<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InboxView.ascx.cs" Inherits="WdS.ElioPlus.Controls.InboxView" %>

<div class="inbox-header inbox-view-header">
    <div class="inbox-header">
        <h3 class="pull-left"><asp:Label ID="LblTitle" runat="server" /></h3>                        
    </div>
    <h1 class="pull-left">        
        <asp:Label ID="LblMessageTitle" runat="server" />
    </h1>    
</div>
<div class="inbox-view-info">
    <div class="row">
        <div class="col-md-10">            
            <asp:Image ID="ImgSenderPhoto" Width="50" CssClass="inbox-author" runat="server" />            
            <span class="sbold"><asp:Label ID="LblSenderName" runat="server" /></span>
            <span>&#60;<asp:Label ID="LblSenderEmail" runat="server" />&#62;</span> to
            <span class="sbold"><asp:Label ID="LblReceiverName" runat="server" /></span> on <asp:Label ID="LblDateMessageReceived" runat="server" />
        </div>
        <div class="col-md-2 inbox-info-btn">
            <div class="btn-group">                
                <a id="aReply" runat="server" class="btn green reply-btn">
                    <i class="fa fa-reply"></i> Reply
                    <i class="fa fa-angle-down"></i>
                </a>
            </div>
        </div>
    </div>
</div>
<div class="inbox-view">
    <p>
       <asp:Label ID="LblMessageContent" runat="server" /> 
    </p>
</div>
<hr />