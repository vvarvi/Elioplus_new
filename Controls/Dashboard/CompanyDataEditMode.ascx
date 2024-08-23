<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompanyDataEditMode.ascx.cs" Inherits="WdS.ElioPlus.Controls.Dashboard.CompanyDataEditMode" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Panel ID="PnlData" style="margin-top:20px; padding:10px; margin-left:50px; margin-top:20px;" runat="server">
    <h2><asp:Label ID="LblTitle" runat="server" /></h2>
    <div style="margin-top:30px;"></div> 
    <controls:MessageControl ID="UcNewPasswordMessageAlert" Visible="false" runat="server" />
    <table>
        <tr>
            <td style="padding: 10px; width:150px;">
                <div>
                    <asp:Label ID="LblUserBillingType" runat="server" />
                </div>
            </td>
            <td style="padding: 10px; width:90px;">
                <div>
                    <asp:Label ID="RtbxUserBillingType" runat="server" />
                </div>
            </td>
        </tr> 
        <tr>
            <td style="padding: 10px; width:150px;">
                <div>
                    <asp:Label ID="LblUsername" runat="server" />                                            
                </div>
            </td>
            <td style="padding: 10px; width:90px;">
                <div>
                    <telerik:RadTextBox ID="RtbxUsername" Width="306" CssClass="tbx" MaxLength="15" runat="server" />
                    <div style="">
                        <asp:Label ID="LblUsernameError" CssClass="error" runat="server" />
                    </div>
                </div>
            </td>
        </tr>
    </table>
        <asp:Panel ID="PnlNewPassword" Visible="false" runat="server">
            <table>
                <tr>
                    <td style="padding:10px; width:150px;">
                        <div>
                            <asp:Label ID="Label5" runat="server" />
                        </div>
                    </td>
                    <td style="padding: 10px; width:310px;">
                        <div>
                            <telerik:RadTextBox ID="RtbxPassword" Width="306" CssClass="tbx" MaxLength="15" style="height:34px;" TextMode="Password" runat="server" />
                            <div style="">
                                <asp:Label ID="LblPasswordError" CssClass="error" runat="server" />
                            </div>
                        </div>                
                    </td>
                </tr>
                <tr>
                    <td style="padding:10px; width:120px;">
                        <div>
                            <asp:Label ID="Label3" runat="server" />                                            
                        </div>
                    </td>
                    <td style="padding:10px; width:90px;">
                        <div>
                            <telerik:RadTextBox ID="RtbxNewPassword" Width="306" TextMode="Password" MaxLength="15" CssClass="tbx" runat="server" />
                            <div style="">
                                <asp:Label ID="LblNewPasswordError" CssClass="error" runat="server" />
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="padding:10px; width:150px;">
                        <div>
                            <asp:Label ID="Label6" runat="server" />                                            
                        </div>
                    </td>
                    <td style="padding:10px; width:200px;">
                        <div>
                            <telerik:RadTextBox ID="RtbxRetypePassword" Width="306" TextMode="Password" MaxLength="15" CssClass="tbx" runat="server" />
                            <div style="">
                                <asp:Label ID="LblRetypePasswordError" CssClass="error" runat="server" />
                            </div>
                        </div>                           
                    </td>
                </tr> 
            </table>
        </asp:Panel>        
        <table>
            <tr>
                <td style="padding:10px; width:120px;">                
                </td>
                <td>
                    <div style="padding:10px;">
                        <telerik:RadButton ID="RbtnChangePassword" OnClick="RbtnChangePassword_OnClick" runat="server">
                            <ContentTemplate>
                                <span>
                                    <asp:Label ID="LblChangePasswordText" runat="server" />
                                </span>
                            </ContentTemplate>
                        </telerik:RadButton> 
                        <telerik:RadButton ID="RbtnCancelChangePassword" Visible="false" OnClick="RbtnCancelChangePassword_OnClick" runat="server">
                            <ContentTemplate>
                                <span>
                                    <asp:Label ID="LblCancelChangePasswordText" runat="server" />
                                </span>
                            </ContentTemplate>
                        </telerik:RadButton> 
                    </div>
                </td>
            </tr>      
            <tr>
                <td style="padding:10px; width:150px;">
                    <div>
                        <asp:Label ID="Label7" runat="server" />
                    </div>
                </td>
                <td style="padding:10px; width:90px;">
                    <div>
                        <telerik:RadTextBox ID="RtbxEmail" Width="306" CssClass="tbx" runat="server" />
                        <div style="">
                            <asp:Label ID="LblEmailError" CssClass="error" runat="server" />
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <asp:Panel ID="PnlRegisteredUserInfo" runat="server">
            <table>
                <tr>
                    <td style="padding:10px; width:90px;">
                        <div>
                            <asp:Label ID="Label4" runat="server" />
                        </div>
                    </td>
                    <td style="padding:10px; width:90px;">
                        <div>
                            <telerik:RadTextBox ID="RtbxOfficialEmail" Width="306" CssClass="tbx" runat="server" />
                            <div style="">
                                <asp:Label ID="LblOfficialEmailError" CssClass="error" runat="server" />
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="padding:10px;width:150px;">
                            <asp:Label ID="Label8" runat="server" />                                           
                        </div>
                    </td>
                    <td>
                        <div style="padding:10px;">
                            <telerik:RadTextBox ID="RtbxCompanyName" MaxLength="40" Width="306" CssClass="tbx"  runat="server" />
                            <div style="">
                                <asp:Label ID="LblCompanyNameError" CssClass="error" runat="server" />
                            </div>
                        </div>
                    </td>                                       
                </tr>                                    
                <tr>
                    <td>
                        <div style="padding:10px;">                 
                            <asp:Label ID="Label9" runat="server" />
                        </div>
                    </td>
                    <td>
                        <div style="padding:10px;">                                   
                            <telerik:RadTextBox ID="RtbxWebSite" MaxLength="40" Width="306" CssClass="tbx" runat="server" />  
                            <div style="">
                                <asp:Label ID="LblWeSiteError" CssClass="error" runat="server" />
                            </div>         
                        </div>   
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="padding:10px;">
                            <asp:Label ID="Label10" runat="server"/>
                        </div>
                    </td>
                    <td>
                        <div style="padding:10px;">                                                
                            <telerik:RadComboBox AutoPostBack="true" OnTextChanged="RcbxCountries_OnTextChanged" ID="RcbxCountries" Width="306" Height="210" runat="server" />
                            <div style="">
                                <asp:Label ID="LblCountryError" CssClass="error" runat="server" />
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="padding:10px;">                                  
                            <asp:Label ID="Label11" runat="server"/>
                        </div> 
                    </td>
                    <td>
                        <div style="padding:10px;">                                 
                            <telerik:RadTextBox ID="RtbxAddress" MaxLength="100" Width="306" CssClass="tbx" runat="server" />
                            <div style="">
                                <asp:Label ID="LblAddressError" CssClass="error" runat="server" />
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="padding:10px;">    
                            <asp:Label ID="Label12" runat="server" />
                        </div>
                    </td>
                    <td>
                        <div style="padding:10px;">
                            <telerik:RadTextBox ID="RtbxPhoneNumberPrefix" Width="55" ReadOnly="true" CssClass="tbx" runat="server" />
                            <telerik:RadTextBox ID="RtbxPhone" onkeypress="return isNumberOnly(event);" MaxLength="15" Width="246" CssClass="tbx" runat="server" />
                            <div style="">
                                <asp:Label ID="LblPhoneError" CssClass="error" runat="server" />
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="padding:10px;">    
                           <asp:Label ID="LblmashapeText" runat="server" />
                        </div>
                    </td>
                    <td>
                        <div style="padding:10px;">
                            <asp:Label ID="LblmashapeLink" Text="https://www.mashape.com/" runat="server" />
                            <telerik:RadTextBox ID="RtbxMashapeUsername" MaxLength="50" Width="130"  EmptyMessage="username" CssClass="tbx" runat="server" />
                            <div style="">
                                <asp:Label ID="Label15" CssClass="error" runat="server" />
                            </div>
                        </div>
                    </td>
                </tr>            
                <tr>
                    <td>
                        <div style="padding:10px;">                 
                            <asp:Label ID="Label14" runat="server" Text="Logo" />
                        </div>
                    </td>
                    <td>
                        <div style="padding:10px;">
                            <telerik:RadAsyncUpload MaxFileSize="100000" OnFileUploaded="Logo_OnFileUploaded" ID="Logo" AllowedFileExtensions="png,jpg,jpeg,gif" MaxFileInputsCount="1" runat="server" />
                            <div style="">
                                <asp:Label ID="LblUploadError" CssClass="error" runat="server" />
                            </div>
                        </div>
                    </td>
                </tr>            
                <tr>
                    <td  style="width:170px;">
                        <div style="padding:10px;">
                            <asp:Label ID="LblOverView" runat="server" />
                        </div>
                    </td>
                    <td>
                        <div style="padding:10px; text-align:justify;">
                            <telerik:RadTextBox ID="RtbxOverView" runat="server" TextMode="MultiLine" Rows="5" MaxLength="3000" Width="783px" Height="200" />
                            <div style="">
                                <asp:Label ID="LblOverViewError" CssClass="error" runat="server" />
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="padding:10px;">
                            <asp:Label ID="LblDescription" runat="server" />
                        </div>
                    </td>
                    <td>
                        <div style="padding:10px; text-align:justify;">
                            <telerik:RadTextBox ID="RtbxDescription" MaxLength="3000" TextMode="MultiLine" Rows="5" runat="server" Width="783px" Height="200" />
                            <div style="">
                                <asp:Label ID="LblDescriptionError" CssClass="error" runat="server" />
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table>
            <tr>
                <td style="width:170px;">
                    <div style="padding:10px;"></div>
                </td>
                <td style="padding: 10px; width:306px;">
                    <div style="width:306px;">
                        <telerik:RadButton ID="RbtnSave" OnClick="RbtnSave_OnClick" runat="server">
                            <ContentTemplate>
                                <span>
                                    <asp:Label ID="LblSaveText" runat="server" />
                                </span>
                            </ContentTemplate>
                        </telerik:RadButton>   
                        <telerik:RadButton ID="RbtnCancel" OnClick="RbtnCancel_OnClick" runat="server">
                            <ContentTemplate>
                                <span>
                                    <asp:Label ID="LblCancelText" runat="server" />
                                </span>
                            </ContentTemplate>
                        </telerik:RadButton>
                    </div>
                </td>
            </tr>
        </table>
        <controls:MessageControl ID="UcPasswordMessageAlert" Visible="false" runat="server" />
    <div style="margin-top:40px;"></div>
        
    <asp:PlaceHolder ID="PhCompanyCategoriesData" runat="server" />

    <div style="margin-bottom:100px;"></div>
</asp:Panel>