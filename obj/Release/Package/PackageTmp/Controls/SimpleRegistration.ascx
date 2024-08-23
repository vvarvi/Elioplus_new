<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SimpleRegistration.ascx.cs" Inherits="WdS.ElioPlus.Controls.SimpleRegistration" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript" src="https://platform.linkedin.com/in.js">
        api_key: 77pas50dsobxxz
        authorize: false
        onLoad: onLinkedInLoad                 
    </script>
    <script type="text/javascript">
        function onLinkedInLoad() {
            IN.Event.on(IN, "auth", OnLinkedInAuth);

            $('a[id*=li_ui_li_gen_]').css({ marginBottom: '20px' }).html('<img src="/images/Sign-In-Small-Active.png" height="31" width="200" border="0" />');
        }
        function OnLinkedInAuth() {
            IN.API.Profile("me").fields(["id", "firstName", "lastName", "headline", "pictureUrl",
            "emailAddress", "summary", "publicProfileUrl"]).result(ShowProfileData);
        }
        function ShowProfileData(profiles) {
            var member = profiles.values[0];
            var id = member.id;
            var firstName = member.firstName;
            var lastName = member.lastName;
            var headline = member.headline;
            var photo = member.pictureUrl;
            var emailAddress = member.emailAddress;
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

            var clickButton = document.getElementById("<%= Button1.ClientID %>");
            clickButton.click();
        }                                                  
    </script>
    <script>
        function Load(sender, args) {
            pw = sender;
            pw.focus();
            pw.blur();
        }
    </script>
</telerik:RadScriptBlock>
<controls:MessageControl ID="UcMessageControl" Visible="false" runat="server" />
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
                    <asp:Button ID="Button1" runat="server" />                        
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
                                    <telerik:RadTextBox ID="RtbxUsername" Width="320" MaxLength="15" Skin="MetroTouch" runat="server" ToolTip="fdsf"/>
                                </div>  
                            </td>
                            <%--<td>
                                <asp:Image ID="ImageUsername" ImageUrl="~/Images/icons/small/info.png" CssClass="info-pointer" runat="server" />
                                <telerik:RadToolTip ID="RadToolTipUsername" TargetControlID="ImageUsername" Width="300" Position="TopRight" Animation="Fade" runat="server" />
                            </td>--%>
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
                                    <telerik:RadTextBox ID="RtbxPassword" Width="320" TextMode="Password" Skin="MetroTouch" MaxLength="15" runat="server">
                                        <ClientEvents OnLoad="Load" />
                                    </telerik:RadTextBox>                                                                           
                                    <asp:LinkButton ID="LnkBtnCreatePassword" Visible="false" runat="server" />
                                </div>
                            </td>
                            <%--<td>
                                <asp:Image ID="ImagePassword" ImageUrl="~/Images/icons/small/info.png" CssClass="info-pointer" runat="server" />
                                <telerik:RadToolTip ID="RadToolTipPassword" TargetControlID="ImagePassword" Width="300" Position="TopRight" Animation="Fade" runat="server" />
                            </td>--%>
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
                                    <telerik:RadTextBox ID="RtbxRetypePassword" Width="320" TextMode="Password" MaxLength="15" Skin="MetroTouch" runat="server">
                                        <ClientEvents OnLoad="Load" />
                                    </telerik:RadTextBox>
                                </div>
                            </td>
                            <%--<td>
                                <asp:Image ID="ImageRetypePassword" ImageUrl="~/Images/icons/small/info.png" CssClass="info-pointer" runat="server" />
                                <telerik:RadToolTip ID="RadToolTipRetypePassword" TargetControlID="ImageRetypePassword" Width="300" Position="TopRight" Animation="Fade" runat="server" />
                            </td>--%>
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
                                <div class="login-rtbx">
                                    <span id="span3" class="emailImgInactive com-loginImg"></span>
                                    <telerik:RadTextBox ID="RtbxEmail" MaxLength="100" Width="320" Skin="MetroTouch" runat="server" />
                                </div>
                            </td>
                            <%-- <td>
                                <asp:Image ID="ImageEmail" ImageUrl="~/Images/icons/small/info.png" CssClass="info-pointer" runat="server" />
                                <telerik:RadToolTip ID="RadToolTipEmail" TargetControlID="ImageEmail" Width="300" Position="TopRight" Animation="Fade" runat="server" />
                            </td>--%>
                        </tr>
                        <tr>
                            <td>
                                <div style="height:15px; float:left; padding:5px;">
                                    <asp:Label ID="LblEmailError" CssClass="error" runat="server" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div>
                                    <telerik:RadButton ID="RbtnSave" style="width:346px;" runat="server">
                                        <ContentTemplate>
                                            <span>
                                                <asp:Label ID="LblSaveText" runat="server" />
                                            </span>
                                        </ContentTemplate>
                                    </telerik:RadButton>                                                                           
                                    <telerik:RadButton ID="RbtnCancel" Visible="false" runat="server">
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
                                    <asp:LinkButton ID="LnkBtnRegister" runat="server" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
    </div>
</div>