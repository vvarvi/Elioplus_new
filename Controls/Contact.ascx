<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Contact.ascx.cs" Inherits="WdS.ElioPlus.Controls.Contact" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
       
        function isNumberOnly(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        function RbtnSend_OnClientClicked(sender, args) {
            var isError = false;

            var RbtnSend = $find('<%=RbtnSend.ClientID %>');

            var RtbxCompanyName = $find('<%=RtbxCompanyName.ClientID %>');
            var RtbxCompanyEmail = $find('<%=RtbxCompanyEmail.ClientID %>');
            var RtbxCompanyPhone = $find('<%=RtbxCompanyPhone.ClientID %>');
            var RtbxSubject = $find('<%=RtbxSubject.ClientID %>');
            var RtbxMessage = $find('<%=RtbxMessage.ClientID %>');

            var LblnameError = document.getElementById('<%=LblnameError.ClientID %>');
            var LblEmailError = document.getElementById('<%=LblEmailError.ClientID %>');
            var LblPhoneError = document.getElementById('<%=LblPhoneError.ClientID %>');
            var LblSubjectError = document.getElementById('<%=LblSubjectError.ClientID %>');
            var LblMessageError = document.getElementById('<%=LblMessageError.ClientID %>');

            var EmailChecker = /\S+@\S+\.\S+/;

            LblnameError.innerHTML = '';
            LblEmailError.innerHTML = '';
            LblPhoneError.innerHTML = '';
            LblSubjectError.innerHTML = '';
            LblMessageError.innerHTML = '';

            //check name
            if (RtbxCompanyName.get_value() == '') {
                LblnameError.innerHTML = 'Please fill your name';
                isError = true;
            }
            else if (RtbxCompanyEmail != null) {
                //check email
                if (RtbxCompanyEmail.get_value() == '') {
                    LblEmailError.innerHTML = 'Please fill your email address';
                    isError = true;
                }
                //check email validation
                else if (!EmailChecker.test(RtbxCompanyEmail.get_value())) {
                    LblEmailError.innerHTML = 'Please enter a valid email address';
                    isError = true;
                }
            }
            //check phone
            else if (RtbxCompanyPhone.get_value() == '') {
                LblPhoneError.innerHTML = 'Please fill your phone';
                isError = true;
            }
            //check subject
            else if (RtbxSubject.get_value() == '') {
                LblSubjectError.innerHTML = 'Please fill your message subject';
                isError = true;
            }
            //check message
            else if (RtbxMessage.get_value() == '') {
                LblMessageError.innerHTML = 'Please fill your message';
                isError = true;
            }

            if (isError) {
                RbtnSend.set_autoPostBack(false);
            }
            else {
                RbtnSend.set_autoPostBack(true);
            }
        }

        function RbtnReset_OnClientClicked(sender, args) {

            var LblnameError = document.getElementById('<%=LblnameError.ClientID %>');
            var LblEmailError = document.getElementById('<%=LblEmailError.ClientID %>');
            var LblPhoneError = document.getElementById('<%=LblPhoneError.ClientID %>');
            var LblSubjectError = document.getElementById('<%=LblSubjectError.ClientID %>');
            var LblMessageError = document.getElementById('<%=LblMessageError.ClientID %>');

            if ('<%= Session["User"] %>' == '') {

                document.getElementById('<%=RtbxCompanyName.ClientID %>').value = '';

                if (document.getElementById('<%=RtbxCompanyEmail.ClientID %>') != null) {
                    document.getElementById('<%=RtbxCompanyEmail.ClientID %>').value = '';
                }

                document.getElementById('<%=RtbxCompanyPhone.ClientID %>').value = '';
            }
           
            LblnameError.innerHTML = '';
            LblEmailError.innerHTML = '';
            LblPhoneError.innerHTML = '';
            LblSubjectError.innerHTML = '';
            LblMessageError.innerHTML = '';

            document.getElementById('<%=RtbxSubject.ClientID %>').value = '';
            document.getElementById('<%=RtbxMessage.ClientID %>').value = '';
        }
    </script>
</telerik:RadScriptBlock>

<asp:Panel ID="PnlContactInfo" DefaultButton="RbtnSend" runat="server">
    <asp:Table runat="server" ID="tblContact">
        <telerik:GridTableRow>
            <telerik:GridTableCell Width="300px">
                <div>
                    <asp:Table runat="server" ID="tblInfo">
                        <telerik:GridTableRow>
                            <telerik:GridTableCell Height="30">
                                <asp:Label ID="LblName" runat="server" Font-Size="Large" />
                            </telerik:GridTableCell>
                        </telerik:GridTableRow>
                        <telerik:GridTableRow>
                            <telerik:GridTableCell Height="30">
                                <telerik:RadTextBox ID="RtbxCompanyName" MaxLength="50" runat="server" Width="270" Height="30" /> 
                            </telerik:GridTableCell>
                        </telerik:GridTableRow>
                        <telerik:GridTableRow>
                            <telerik:GridTableCell Height="30">
                                <asp:Label ID="LblnameError" CssClass="error" runat="server" />
                            </telerik:GridTableCell>
                        </telerik:GridTableRow>
                        <telerik:GridTableRow>
                            <telerik:GridTableCell Height="30">
                                <asp:Label ID="LblEmail" runat="server" Font-Size="Large" />
                            </telerik:GridTableCell>
                        </telerik:GridTableRow>
                        <telerik:GridTableRow>
                            <telerik:GridTableCell Height="30">
                                <telerik:RadTextBox ID="RtbxCompanyEmail" MaxLength="50" Width="270" Height="30" runat="server" /> 
                                <telerik:RadComboBox ID="RcbxCompanyEmail" Visible="false" Width="270" runat="server" />
                            </telerik:GridTableCell>
                        </telerik:GridTableRow>
                        <telerik:GridTableRow>
                            <telerik:GridTableCell Height="30">
                                <asp:Label ID="LblEmailError" CssClass="error" runat="server" />
                            </telerik:GridTableCell>
                        </telerik:GridTableRow>
                        <telerik:GridTableRow>
                            <telerik:GridTableCell Height="30">
                                <asp:Label ID="LblPhone" runat="server" Font-Size="Large" />
                            </telerik:GridTableCell>
                        </telerik:GridTableRow>
                        <telerik:GridTableRow>
                            <telerik:GridTableCell Height="30">
                                <telerik:RadTextBox ID="RtbxCompanyPhone" onkeypress="return isNumberOnly(event);" MaxLength="15" runat="server" Width="270" Height="30" />                                                    
                            </telerik:GridTableCell>
                        </telerik:GridTableRow>
                        <telerik:GridTableRow>
                            <telerik:GridTableCell Height="30">
                                <asp:Label ID="LblPhoneError" CssClass="error" runat="server" />
                            </telerik:GridTableCell>
                        </telerik:GridTableRow>
                    </asp:Table>
                </div>                                
            </telerik:GridTableCell>
            <telerik:GridTableCell Width="320px">                               
                    <asp:Table ID="Table1" runat="server">
                        <telerik:GridTableRow>
                            <telerik:GridTableCell Height="30">
                                <asp:Label ID="LblSubject" runat="server" Font-Size="Large" />
                            </telerik:GridTableCell>
                        </telerik:GridTableRow>
                        <telerik:GridTableRow>
                            <telerik:GridTableCell Height="30">
                                <telerik:RadTextBox ID="RtbxSubject" MaxLength="100" runat="server" Width="320" Height="30" />                                                    
                            </telerik:GridTableCell>
                        </telerik:GridTableRow>
                        <telerik:GridTableRow>
                            <telerik:GridTableCell Height="30">
                                <asp:Label ID="LblSubjectError" CssClass="error" runat="server" />
                            </telerik:GridTableCell>
                        </telerik:GridTableRow>
                        <telerik:GridTableRow>
                            <telerik:GridTableCell Height="30">
                                <asp:Label ID="LblMessage" runat="server" Font-Size="Large" />
                                <asp:Image ID="ImgInfo" AlternateText="info" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                <telerik:RadToolTip ID="RttMessage" TargetControlID="ImgInfo" Width="300" Position="TopRight" Animation="Fade" runat="server" />
                            </telerik:GridTableCell>
                        </telerik:GridTableRow>
                        <telerik:GridTableRow>
                            <telerik:GridTableCell Height="130">
                                <telerik:RadTextBox ID="RtbxMessage" runat="server" MaxLength="500" Height="130" TextMode="MultiLine" Rows="9" Width="320" />                                                                                                  
                            </telerik:GridTableCell>
                        </telerik:GridTableRow>
                        <telerik:GridTableRow>
                            <telerik:GridTableCell Height="30px">
                                <asp:Label ID="LblMessageError" CssClass="error" runat="server" />
                            </telerik:GridTableCell>
                        </telerik:GridTableRow>
                        <telerik:GridTableRow>
                            <telerik:GridTableCell HorizontalAlign="Right" Height="100px">
                                <telerik:RadButton ID="RbtnReset" AutoPostBack="false" OnClick="RbtnReset_OnClick" OnClientClicked="RbtnReset_OnClientClicked" style="margin-right:10px;" runat="server">
                                    <ContentTemplate>
                                        <span>                                                       
                                            <asp:Label ID="LblResetText" runat="server" />                                                  
                                        </span>
                                    </ContentTemplate>
                                </telerik:RadButton>
                                <telerik:RadButton ID="RbtnSend" OnClick="RbtnSend_OnClick" OnClientClicked="RbtnSend_OnClientClicked" runat="server">
                                    <ContentTemplate>
                                        <span>                                                     
                                            <asp:Label ID="LblSendText" runat="server" />
                                        </span>                                                        
                                    </ContentTemplate>
                                </telerik:RadButton>
                            </telerik:GridTableCell>
                        </telerik:GridTableRow>
                    </asp:Table>                                
            </telerik:GridTableCell>
        </telerik:GridTableRow>
    </asp:Table>
</asp:Panel>
<controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />