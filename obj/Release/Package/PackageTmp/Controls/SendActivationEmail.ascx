<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SendActivationEmail.ascx.cs" Inherits="WdS.ElioPlus.Controls.SendActivationEmail" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">

        function RbtnSend_OnClientClicked(sender, args) {

            var isEmailError = false;

            var RbtnSend = $find('<%=RbtnSend.ClientID %>');

            //check email
            var RtbxCompanyEmail = $find('<%=RtbxCompanyEmail.ClientID %>');
            var LblEmailError = document.getElementById('<%=LblEmailError.ClientID %>');

            if (RtbxCompanyEmail.get_value() == '') {
                LblEmailError.innerHTML = 'Please fill your email address';
                isEmailError = true;
            }

            if (isEmailError) {
                RbtnSend.set_autoPostBack(false);

            }
            else {
                RbtnSend.set_autoPostBack(true);
            }
        }

        function RbtnReset_OnClientClicked(sender, args) {

            var RbtnReset = $find('<%=RbtnReset.ClientID %>');
            var RtbxCompanyEmail = document.getElementById('<%=RtbxCompanyEmail.ClientID %>')
            var LblEmailError = document.getElementById('<%=LblEmailError.ClientID %>');

            RtbxCompanyEmail.value = "";
            LblEmailError.innerHTML = '';

            RbtnReset.set_autoPostBack(false);
        }
    </script>
</telerik:RadScriptBlock>

<asp:Table runat="server" ID="tblContact">
    <telerik:GridTableRow>
        <telerik:GridTableCell Width="305px">
            <div>
                <asp:Table runat="server" ID="tblInfo">                   
                    <telerik:GridTableRow>
                        <telerik:GridTableCell Height="30">
                            <asp:Label ID="LblEmail" runat="server" Font-Size="Large"></asp:Label>
                            <asp:Image ID="ImgEmailInfo" AlternateText="email" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                            <telerik:RadToolTip ID="RttMessage" TargetControlID="ImgEmailInfo" Width="300" Position="TopRight" Animation="Fade" runat="server" />
                        </telerik:GridTableCell>
                    </telerik:GridTableRow>
                    <telerik:GridTableRow>
                        <telerik:GridTableCell Height="30">
                            <telerik:RadTextBox ID="RtbxCompanyEmail" MaxLength="50" placeholder="" Width="305" Height="30" runat="server" />
                        </telerik:GridTableCell>
                    </telerik:GridTableRow>
                    <telerik:GridTableRow>
                        <telerik:GridTableCell Height="30">
                            <asp:Label ID="LblEmailError" CssClass="error" runat="server" />
                        </telerik:GridTableCell>
                    </telerik:GridTableRow>
                    <telerik:GridTableRow>
                        <telerik:GridTableCell Height="30">
                            <telerik:RadButton ID="RbtnReset" OnClick="RbtnReset_OnClick" OnClientClicked="RbtnReset_OnClientClicked" runat="server">
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
            </div>                                
        </telerik:GridTableCell>        
    </telerik:GridTableRow>
</asp:Table>
<controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />