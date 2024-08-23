<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SignUpControl.ascx.cs" Inherits="WdS.ElioPlus.Controls.Common.SignUpControl" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript" src="https://platform.linkedin.com/in.js">
            api_key: 77pas50dsobxxz;
            authorize: false;
            onLoad: onLinkedInLoad;
    </script>
    <script type="text/javascript">

        function Load(sender, args) {
            pw = sender;
            pw.focus();
            pw.blur();
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
            //alert(JSON.stringify(member));

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

        function RbtnSave_OnClientClicked(sender, args) {
            var isError = false;

            var RbtnSave = $find('<%=RbtnSave.ClientID %>');

            var RtbxUsername = $find('<%=RtbxUsername.ClientID %>');
            var RtbxPassword = $find('<%=RtbxPassword.ClientID %>');
            var RtbxRetypePassword = $find('<%=RtbxRetypePassword.ClientID %>');
            var RtbxEmail = $find('<%=RtbxEmail.ClientID %>');

            var LblUsernameError = document.getElementById('<%=LblUsernameError.ClientID %>');
            var LblPasswordError = document.getElementById('<%=LblPasswordError.ClientID %>');
            var LblRetypePasswordError = document.getElementById('<%=LblRetypePasswordError.ClientID %>');
            var LblEmailError = document.getElementById('<%=LblEmailError.ClientID %>');

            var EmailChecker = /\S+@\S+\.\S+/;

            LblUsernameError.innerHTML = '';
            LblPasswordError.innerHTML = '';
            LblRetypePasswordError.innerHTML = '';
            LblEmailError.innerHTML = '';

            //check username
            if (RtbxUsername.get_value() == '') {
                LblUsernameError.innerHTML = 'Please enter username';
                isError = true;
            }
            //check username length
            else if (document.getElementById('<%=RtbxUsername.ClientID %>').value.length < 8) {
                LblUsernameError.innerHTML = 'Username must be at least 8 characters';
                isError = true;
            }
            //check password
            else if (RtbxPassword.get_value() == '') {
                LblPasswordError.innerHTML = 'Please enter password';
                isError = true;
            }
            //check password length
            else if (document.getElementById('<%=RtbxPassword.ClientID %>').value.length < 8) {
                LblPasswordError.innerHTML = 'Password must be at least 8 characters';
                isError = true;
            }
            //check retype password
            else if (RtbxRetypePassword.get_value() == '') {
                LblRetypePasswordError.innerHTML = 'Please retype password';
                isError = true;
            }
            //check retype password length
            else if (document.getElementById('<%=RtbxRetypePassword.ClientID %>').value.length < 8) {
                LblRetypePasswordError.innerHTML = 'Retype Password must be at least 8 characters';
                isError = true;
            }
            //check password and retype password
            else if (RtbxPassword.get_value() != RtbxRetypePassword.get_value()) {
                LblRetypePasswordError.innerHTML = 'Please retype correct your password';
                isError = true;
            }
            //check email
            else if (RtbxEmail.get_value() == '') {
                LblEmailError.innerHTML = 'Please enter email';
                isError = true;
            }
            //check email validation
            else if (!EmailChecker.test(RtbxEmail.get_value())) {
                LblEmailError.innerHTML = 'Please enter a valid email address';
                isError = true;
            }

            if (isError) {
                RbtnSave.set_autoPostBack(false);
            }
            else {
                RbtnSave.set_autoPostBack(true);
            }
        }
    </script>
</telerik:RadScriptBlock>

<controls:MessageControl ID="UcMessageAlert" runat="server" />
<div class="container container-main clearfix">
    <div id="login-container" class="container">        
        <asp:Panel ID="PnlSignUp" DefaultButton="RbtnSave" runat="server">            
            <div id="login-form-container" class="container">                
                <div class="container-heading">
                    <h1 class="page-title">
                        <asp:Label ID="LblHeader" runat="server" />
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
                <div class="login" id="theme-my-login">
                    <table>
                        <tr>
                            <td>
                                <div class="login-rtbx">
                                    <span id="span4" class="loginImgInactive com-loginImg"></span>
                                    <telerik:RadTextBox ID="RtbxUsername" Width="320" MaxLength="100" Skin="MetroTouch" runat="server" />
                                </div>  
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="height:15px; float:left; padding:5px;">
                                    <asp:Label ID="LblUsernameError" CssClass="error" runat="server" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="login-rtbx">
                                    <span id="span1" class="pwdImgInactive com-loginImg"></span>
                                    <telerik:RadTextBox ID="RtbxPassword" Width="320" TextMode="Password" Skin="MetroTouch" MaxLength="40" runat="server">
                                        <ClientEvents OnLoad="Load" />
                                    </telerik:RadTextBox>                                                                           
                                    <asp:LinkButton ID="LnkBtnCreatePassword" Visible="false" runat="server" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="height:15px; float:left; padding:5px;">
                                    <asp:Label ID="LblPasswordError" CssClass="error" runat="server" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="login-rtbx">
                                    <span id="span2" class="pwdImgInactive com-loginImg"></span>
                                    <telerik:RadTextBox ID="RtbxRetypePassword" Width="320" TextMode="Password" MaxLength="40" Skin="MetroTouch" runat="server">
                                        <ClientEvents OnLoad="Load" />
                                    </telerik:RadTextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="height:15px; float:left; padding:5px;">
                                    <asp:Label ID="LblRetypePasswordError" CssClass="error" runat="server" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divEmail" class="login-rtbx" runat="server">
                                    <span id="span3" class="emailImgInactive com-loginImg"></span>
                                    <telerik:RadTextBox ID="RtbxEmail" Width="320" Skin="MetroTouch" MaxLength="100" runat="server" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divEmailError" style="height:15px; float:left; padding:5px;" runat="server">
                                    <asp:Label ID="LblEmailError" CssClass="error" runat="server" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <telerik:RadButton ID="RbtnSave" style="width:346px;" OnClick="RbtnSave_OnClick" OnClientClicked="RbtnSave_OnClientClicked" runat="server">
                                        <ContentTemplate>
                                            <span>
                                                <asp:Label ID="LblSaveText" runat="server" />
                                            </span>
                                        </ContentTemplate>
                                    </telerik:RadButton>                                                                           
                                    <telerik:RadButton ID="RbtnCancel" Visible="false" OnClick="RbtnCancel_OnClick" runat="server">
                                        <ContentTemplate>
                                            <span>
                                                <asp:Label ID="LblCancelText" runat="server" />
                                            </span>
                                        </ContentTemplate>
                                    </telerik:RadButton>  
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="padding:20px; text-align:center;">
                                    <asp:Label ID="LblAccountText" runat="server" />
                                    <asp:LinkButton ID="LnkBtnRegister" OnClick="LnkBtnRegister_OnClick" runat="server" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
    </div>
</div>
<div id="loader" style="display:none;">
    <div id="loadermsg">
    </div>
</div>