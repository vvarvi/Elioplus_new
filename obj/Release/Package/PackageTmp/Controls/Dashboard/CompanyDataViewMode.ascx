<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CompanyDataViewMode.ascx.cs" Inherits="WdS.ElioPlus.Controls.Dashboard.CompanyDataViewMode" %>

<asp:Panel ID="PnlData" style="margin-top:20px; padding:10px; margin-left:50px; margin-top:20px;" runat="server">
    <h2><asp:Label ID="LblTitle" runat="server" /></h2>
    <div style="margin-top:30px;"></div>
    <asp:Panel ID="PnlLogo" runat="server">
        <table>
            <tr>
                <td style="padding: 10px; width:150px;">
                    <div style=" margin-top:70px;">
                        <asp:Label ID="Label4" runat="server" />
                    </div>
                </td>
                <td style="padding: 10px; width:200px;">
                    <div>
                        <asp:Image ID="ImgLogo" Width="200" Height="150" runat="server" />
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
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
                    <asp:Label ID="RtbxUsername" runat="server" />
                </div>
            </td>
        </tr>           
        <tr>
            <td style="padding:10px; width:90px;">
                <div>
                    <asp:Label ID="Label7" runat="server" />
                </div>
            </td>
            <td style="padding:10px; width:90px;">
                <div>
                    <asp:Label ID="RtbxEmail" runat="server" />
                </div>
            </td>
        </tr>
    </table>
    <asp:Panel ID="PnlRegisteredUserInfo" Visible="false" runat="server">
        <table>
            <tr>
                <td style="padding:10px; width:90px;">
                    <div>
                        <asp:Label ID="Label3" runat="server" />
                    </div>
                </td>
                <td style="padding:10px; width:90px;">
                    <div>
                        <asp:Label ID="RtbxOfficialEmail" runat="server" />
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
                        <asp:Label ID="RtbxCompanyName" runat="server" />
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
                        <asp:HyperLink ID="HpLnkWebSite" Target="_blank" runat="server" />
                    </div>   
                </td>
            </tr>
            <tr>
                <td>
                    <div style="padding:10px;">
                        <asp:Label ID="Label10" runat="server" />
                    </div>
                </td>
                <td>
                    <div style="padding:10px;">                                                
                        <asp:Label ID="RcbxCountries" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="padding:10px;">                                  
                        <asp:Label ID="Label11" runat="server" />
                    </div> 
                </td>
                <td>
                    <div style="padding:10px;">                                 
                        <asp:Label ID="RtbxAddress" runat="server" />
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
                        <asp:Label ID="RtbxPhoneNumberPrefix" runat="server" />
                        <asp:Label ID="RtbxPhone" runat="server" />
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
                        <asp:HyperLink ID="HpLnk" NavigateUrl="https://www.mashape.com/?utm_source=myelio&utm_medium=website&utm_campaign=partners" Text="Now" CssClass="bold" Target="_blank" runat="server" />
                    </div>
                </td>
            </tr> 
            <tr>
                <td>
                    <div style="padding:10px;">
                        <asp:Label ID="LblOverView" runat="server" />
                    </div>
                </td>
                <td>
                    <div style="padding:10px; text-align:justify;">                        
                        <asp:Label ID="LblOverViewValue" runat="server" Width="783px" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="padding:10px;">
                        <asp:Label ID="LblOverViewError" CssClass="error" runat="server" />
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
                        <asp:Label ID="LblDescriptionValue" runat="server" Width="783px" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="row">
                        <asp:Label ID="LblDescriptionError" CssClass="error" runat="server" />
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
                <div  style="width:306px;">
                    <telerik:RadButton ID="RbtnEdit" OnClick="RbtnEdit_OnClick" runat="server">
                        <ContentTemplate>
                            <span>
                                <div style="float:left; padding:3px 3px 3px 50px;">
                                    <asp:Image ID="ImgAdd" AlternateText="add" ImageUrl="~/images/icons/edit_1.png" runat="server" />
                                    <asp:Label ID="LblEditText" runat="server" />
                                </div>
                            </span>
                        </ContentTemplate>
                    </telerik:RadButton>  
                    <telerik:RadButton ID="RbtnRegister" OnClick="RbtnRegister_OnClick" runat="server">
                        <ContentTemplate>
                            <span>
                                <div style="padding:3px 3px 3px 3px;">
                                    <asp:Label ID="LblRegisterText" runat="server" />
                                </div>
                            </span>
                        </ContentTemplate>
                    </telerik:RadButton>                                      
                </div>
            </td>
        </tr>
    </table>
    <div style="margin-top:40px;"></div>
    
    <asp:PlaceHolder ID="PhCompanyCategoriesData" runat="server" />   
     
    <div style="margin-bottom:30px;"></div>
</asp:Panel>