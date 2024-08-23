<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileDataEditMode.ascx.cs" Inherits="WdS.ElioPlus.Controls.Dashboard.ProfileDataEditMode" %>

<asp:Panel ID="PnlData" style="margin-top:20px; padding:10px; margin-left:50px; margin-top:20px;" runat="server">
    <h2><asp:Label ID="LblTitle" runat="server" /></h2>
    <div style="margin-top:30px;"></div> 
    <table>            
        <tr>
            <td style="padding: 10px; width:150px;">
                <div>
                    <asp:Label ID="LblLastNameText" runat="server" Text="Last Name" />
                </div>
            </td>
            <td style="padding: 10px; width:90px;">
                <div>
                    <telerik:RadTextBox ID="RtbxLastName" MaxLength="40" Width="300" runat="server" />                                                
                    <div class="pers-info-row-error">
                        <asp:Label ID="LblLastNameError" runat="server" />
                    </div>
                </div>
            </td>
        </tr>           
        <tr>
            <td style="padding:10px; width:90px;">
                <div>
                    <asp:Label ID="LblFirstNameText" runat="server" Text="First Name" />
                </div>
            </td>
            <td style="padding:10px; width:90px;">
                <div>
                    <telerik:RadTextBox ID="RtbxFirstName" MaxLength="40" Width="300" runat="server" />
                    <div class="pers-info-row-error">
                        <asp:Label ID="LblFirstNameError" runat="server" />
                    </div> 
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding:10px; width:90px;">
                <div>
                    <asp:Label ID="LblJobPositionText" runat="server" Text="Job Position" />
                </div>
            </td>
            <td style="padding:10px; width:90px;">
                <div>
                    <telerik:RadTextBox ID="RtbxPosition" MaxLength="100" Width="300" runat="server" />                                               
                    <div class="pers-info-row-error">
                        <asp:Label ID="LblPositionError" runat="server" />
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding: 10px; width:150px;">
                <div>
                    <asp:Label ID="LblPersonalImage" runat="server" Text="Personal Image" />
                </div>
            </td>
            <td style="padding: 10px; width:200px;">
                <div>
                    <telerik:RadAsyncUpload Width="246" OnFileUploaded="PersonalImage_OnFileUploaded" ID="PersonalImage" AllowedFileExtensions="png,jpg,jpeg,gif" MaxFileInputsCount="1" runat="server" />                                                                                                
                    <div class="pers-info-row-error">
                        <asp:Label ID="LblPersonalImageUploadError" runat="server" />
                    </div>
                </div>
            </td>
        </tr>    
        <tr>
            <td style="vertical-align:middle;">
                <div style="padding:10px;width:150px;">
                    <asp:Label ID="LblSummaryText" runat="server" Text="Summary" />
                </div>
            </td>
            <td>
                <div style="padding:10px;">
                    <telerik:RadTextBox ID="RtbxSummary" runat="server" MaxLength="2000" TextMode="MultiLine" Rows="5" Width="462px" Height="150px" />                                              
                        <div class="pers-info-row-error">
                            <asp:Label ID="LblSummaryError" runat="server" />
                        </div>
                </div>
            </td>                                       
        </tr>
    </table>
    <table>
        <tr>
            <td style="width:170px;">
                <div style="padding:10px;">                    
                </div>
            </td>
            <td style="padding: 10px;">
                <div style="width:462px;">
                    <telerik:RadButton ID="RbtnSave" OnClick="RbtnSave_OnClick" runat="server">
                        <ContentTemplate>
                            <span>
                                <asp:Label ID="LblSaveText" runat="server" />
                            </span>
                        </ContentTemplate>
                    </telerik:RadButton>
                    <telerik:RadButton ID="RbtnClearFields" OnClick="RbtnClearFields_OnClick" runat="server">
                        <ContentTemplate>
                            <span>
                                <asp:Label ID="LblClearFieldsText" runat="server" />
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
    <div style="margin-bottom:100px;"></div>
</asp:Panel>