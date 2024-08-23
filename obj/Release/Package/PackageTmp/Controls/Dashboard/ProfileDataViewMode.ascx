<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileDataViewMode.ascx.cs" Inherits="WdS.ElioPlus.Controls.Dashboard.ProfileDataViewMode" %>

<asp:Panel ID="PnlData" style="margin-top:20px; padding:10px; margin-left:50px; margin-top:20px;" runat="server">
    <h2><asp:Label ID="LblTitle" runat="server" /></h2>
    <div style="margin-top:30px;"></div>
    <table>
        <tr>
            <td style="padding: 10px; width:105px; vertical-align:middle;">
                <div>
                    <asp:Label ID="LblPersonalImage" runat="server" Text="" />
                </div>
            </td>
            <td style="padding: 10px; width:200px;">
                <div>
                    <asp:Image ID="ImgLogo" runat="server" />
                </div>
            </td>
        </tr>        
        <tr>
            <td style="padding: 10px; width:105px;">
                <div>
                    <asp:Label ID="LblLastNameText" runat="server" Text="" />
                </div>
            </td>
            <td style="padding: 10px; width:90px;">
                <div>
                   <asp:Label ID="LblLastName" runat="server" />
                </div>
            </td>
        </tr>           
        <tr>
            <td style="padding:10px; width:105px;">
                <div>
                    <asp:Label ID="LblFirstNameText" runat="server" Text="" />
                </div>
            </td>
            <td style="padding:10px; width:90px;">
                <div>
                   <asp:Label ID="LblFirstName" runat="server" />
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding:10px; width:105px;">
                <div>
                    <asp:Label ID="LblJobPositionText" runat="server" Text="" />
                </div>
            </td>
            <td style="padding:10px; width:90px;">
                <div>
                    <asp:Label ID="LblJobPosition" runat="server" />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="padding:10px;width:150px;">
                    <asp:Label ID="LblSummaryText" runat="server" Text="" />
                </div>
            </td>
            <td>
                <div style="padding:10px;">
                    <asp:Label ID="LblSummary" runat="server" />
                </div>
            </td>                                       
        </tr>
    </table>
    <table>
        <tr>
            <td style="width:170px;">
                <div style="padding:10px;"></div>
            </td>
            <td style="padding: 10px; width:306px;">
                <div  style="width:306px;">
                    <telerik:RadButton ID="RbtnEdit" OnClick="RbtnEdit_OnClick" runat="server">
                        <ContentTemplate>
                            <span>
                                <asp:Label ID="LblEditText" runat="server" />
                            </span>
                        </ContentTemplate>
                    </telerik:RadButton>  
                    <telerik:RadButton ID="RbtnRegister" OnClick="RbtnRegister_OnClick" runat="server">
                        <ContentTemplate>
                            <span>
                                <asp:Label ID="LblRegisterText" runat="server" />
                            </span>
                        </ContentTemplate>
                    </telerik:RadButton>                                      
                </div>
            </td>
        </tr>
    </table>    
    <div style="margin-bottom:100px;"></div>
</asp:Panel>