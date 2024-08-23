<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ComLogin.ascx.cs" Inherits="WdS.ElioPlus.Controls.Community.ComLogin" %>

<div class="container container-main clearfix">
        <div id="login-container" class="container">
            <div id="login-form-container" class="container">
                <div class="container-heading">
                    <h1 class="page-title" style="margin-left: 34%">Login</h1>
                </div>
                <div class="login" id="theme-my-login">
                    <div class="login-rtbx">                    
                        <telerik:RadTextBox ID="RtbxUsername" CssClass="login-rtbx" runat="server" EmptyMessage="Username" Width="100%">                                             
                        </telerik:RadTextBox>
                    </div>
                    <div class="login-rtbx">                 
                        <telerik:RadTextBox ID="RtbxPassword" CssClass="" runat="server" EmptyMessage="Password" Width="100%">                                             
                        </telerik:RadTextBox>
                    </div>                    
                    <div class="btnreg">
                        <asp:Button ID="BtnLogin" runat="server" Text="Login" CssClass="btn btn-primary" />
                    </div>                                                     
                </div>               
            </div>
            <p id="password-find" class="tiny dark-grey fright"><a href="" title="Lost Password">Forgot password?</a></p>
            <p class="tiny dark-grey">
                Dont have an account? <a href="" title="Login">Register here</a>
            </p>         
        </div>
    </div>
