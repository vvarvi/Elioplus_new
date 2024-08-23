<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginControl.ascx.cs" Inherits="WdS.ElioPlus.Controls.Common.LoginControl" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript" src="https://platform.linkedin.com/in.js">
            api_key: 77pas50dsobxxz
            authorize: true
            onLoad: onLinkedInLoad
    </script>
    <script type="text/javascript">
        function Load(sender, args) {
            pw = sender;
            pw.focus();
            pw.blur();
        }

        function RapPage_OnRequestStart(sender, args) {
            $('#loader').show();
        }
        function endsWith(s) {
            return this.length >= s.length && this.substr(this.length - s.length) == s;
        }
        function RapPage_OnResponseEnd(sender, args) {
            $('#loader').hide();
        }    

        function onLinkedInLoad() {
            IN.Event.on(IN, "auth", OnLinkedInAuth);

            $('a[id*=li_ui_li_gen_]').css({ marginBottom: '20px' }).html('<img src="/images/Sign-In-Small-Active.png" height="31" width="200" border="0" />');
        }

        function OnLinkedInAuth() {
            IN.API.Profile("me").fields(["id", "firstName", "lastName", "headline", "pictureUrl",
                            "emailAddress", "summary", "publicProfileUrl"]).result(ShowProfileData);
        }

        function ShowProfileData(profiles) {
            alert(JSON.stringify(member));

            var member = profiles.values[0];
            var id = member.id;
            var firstName = member.firstName;
            var lastName = member.lastName;
            var headline = member.headline;
            var photo = member.pictureUrl;
            var emailAddress = member.emailAddress
            var summary = member.summary;
            var publicProfileUrl = member.publicProfileUrl;


            document.getElementById('LinkId').value = id;
            document.getElementById('LinkFirstName').value = firstName;
            document.getElementById('LinkLastName').value = lastName;
            document.getElementById('LinkHeadline').value = headline;
            document.getElementById('LinkPicture').value = photo;
            document.getElementById('LinkEmail').value = emailAddress;
            document.getElementById('LinkSummary').value = summary;
            document.getElementById('LinkProfileUrl').value = publicProfileUrl;

            var clickButton = document.getElementById("<%= LinkedinRetrieve.ClientID %>");
            clickButton.click();
        }

        function RbtnLogin_OnClientClicked(sender, args) {

            var isUsernameError = false;
            var isPasswordError = false;

            var RbtnLogin = $find('<%=BtnSubmit.ClientID %>');

            var RtbxUsername = $find('<%=RtbxUsernameLogin.ClientID %>');
            var RtbxPassword = $find('<%=RtbxPasswordLogin.ClientID %>');
            var LblUsernameError = document.getElementById('<%=LblUsernameLoginError.ClientID %>');
            var LblPasswordError = document.getElementById('<%=LblPasswordLoginError.ClientID %>');
            LblUsernameError.innerHTML = '';
            LblPasswordError.innerHTML = '';

            //check username
            if (RtbxUsername.get_value() == '') {
                LblUsernameError.innerHTML = 'Enter username';
                isUsernameError = true;
            }
            else {               

                //check password
                if (RtbxPassword.get_value() == '') {
                    LblPasswordError.innerHTML = 'Enter password';
                    isPasswordError = true;
                }
            }

            if (isUsernameError || isPasswordError) {
                RbtnLogin.set_autoPostBack(false);

            }
            else {
                RbtnLogin.set_autoPostBack(true);
            }
        }  
    </script>
</telerik:RadScriptBlock>

    <controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />
    <div id="login-container" class="container">
        <div id="login-form-container" class="container" style="">                
            <div class="container-heading">
                <h1 class="page-title">
                    <asp:Label ID="LblTopicsHeader" runat="server" />
                </h1>
            </div>
            <div style="padding-top:40px; margin-left:30px;">                        
                <asp:hiddenfield id="LinkId" ClientIDMode="Static" runat="server"/>
                <asp:hiddenfield id="LinkFirstName" ClientIDMode="Static" runat="server"/>
                <asp:hiddenfield id="LinkLastName" ClientIDMode="Static" runat="server"/>
                <asp:hiddenfield id="LinkHeadline" ClientIDMode="Static" runat="server"/>
                <asp:hiddenfield id="LinkPicture" ClientIDMode="Static" runat="server"/>
                <asp:hiddenfield id="LinkEmail" ClientIDMode="Static" runat="server"/>
                <asp:hiddenfield id="LinkSummary" ClientIDMode="Static" runat="server"/>
                <asp:hiddenfield id="LinkProfileUrl" ClientIDMode="Static" runat="server"/>
                <script type="in/Login"></script>
                <asp:Button ID="LinkedinRetrieve" runat="server" OnClick="LinkedinRetrieve_OnClick" />
            </div>
            <div class="OrLabel">
                <asp:Label ID="Label1" runat="server" Text="Or" Font-Bold="true" />
            </div>
            <asp:Panel ID="PnlLogin" DefaultButton="BtnSubmit" runat="server">
                <div class="login" id="theme-my-login">
                    <table>
                        <tr>
                            <td>
                                <div class="lgn-rtbx">
                                    <span id="span4" class="loginImgInactive com-loginImg"></span>
                                    <telerik:RadTextBox ID="RtbxUsernameLogin" Width="320" Skin="MetroTouch" MaxLength="100" runat="server" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="height:15px;padding:5px;">
                                    <asp:Label ID="LblUsernameLoginError" CssClass="error" runat="server" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="lgn-rtbx">
                                    <span id="span1" class="pwdImgInactive com-loginImg"></span>
                                    <telerik:RadTextBox ID="RtbxPasswordLogin" TextMode="Password" Width="320" Skin="MetroTouch" MaxLength="40" style="height:34px;" runat="server">
                                        <ClientEvents OnLoad="Load" />
                                    </telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="height:15px; float:left; padding:5px;">
                                    <asp:Label ID="LblPasswordLoginError" CssClass="error" runat="server" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div> 
                                    <div style="float:left;margin-bottom:5px;">
                                        <asp:CheckBox ID="CbxRememberMe" runat="server" />
                                        <label id="LblRemember" for="CbxRememberMe" class="cbxremember" style="color:#777777; width:120px;line-height:10px; margin-left:5px;">
                                            <asp:Label ID="LblRememberMe" runat="server" />
                                        </label>
                                    </div>
                                    <div class="" style="float:right; margin-top:-20px;">
                                        <asp:LinkButton ID="LnkBtnForgotPassword" OnClick="LnkBtnForgotPassword_OnClick" runat="server" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>                                                                 
                                    <telerik:RadButton ID="BtnCancelLogin" Visible="false" OnClick="BtnCancelLogin_OnClick" runat="server">
                                        <ContentTemplate>
                                            <span>
                                                <asp:Label ID="LblBtnCancelLoginText" runat="server" />
                                            </span>
                                        </ContentTemplate>
                                    </telerik:RadButton>
                                    <telerik:RadButton ID="BtnSubmit" style="width:346px;" OnClick="BtnSubmit_OnClick" OnClientClicked="RbtnLogin_OnClientClicked" runat="server">
                                        <ContentTemplate>
                                            <span>
                                                <asp:Label ID="LblBtnSubmitText" runat="server" />
                                            </span>
                                        </ContentTemplate>
                                    </telerik:RadButton>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="padding:10px; text-align:center;">
                                    <asp:LinkButton ID="LnkBtnRegister" OnClick="LnkBtnRegister_OnClick" runat="server" />
                                </div> 
                            </td>
                        </tr>
                    </table>
                </div>               
            </asp:Panel>
        </div>
    </div>
    <div id="loader" style="display:none;">
        <div id="loadermsg">
        </div>
    </div>