<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiAccountsFullRegistration.ascx.cs" Inherits="WdS.ElioPlus.Controls.MultiAccountsFullRegistration" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            function isNumberOnly(evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;

                return true;
            }
        </script>
</telerik:RadScriptBlock>

<asp:Panel ID="PnlRegister" CssClass="box" runat="server">                      
    <telerik:RadPanelBar ID="RpbRegistrationSteps" ExpandMode="SingleExpandedItem" Width="100%" Height="100%" runat="server">
        <Items>            
            <telerik:RadPanelItem Expanded="True" runat="server" Selected="true">
                <Items>
                    <telerik:RadPanelItem>
                        <ItemTemplate>                            
                            <table class="qsf-fb">
                                <tr class="step2-row">
                                    <td class="step2-td1">
                                        <div>
                                            <asp:Label ID="LblUsername" runat="server" Text="Username" />
                                        </div>
                                    </td>
                                    <td class="step2-td2" style="width:350px;">
                                        <div class="step2-td2">
                                            <telerik:RadTextBox ID="RtbxUsername" MaxLength="100" Width="100%" runat="server" />
                                            <div class="pers-info-row-error">
                                                <asp:Label ID="LblUsernameError" runat="server" />
                                            </div>
                                        </div>
                                        <div class="pers-info-row-error">
                                            <asp:Label ID="Label13" Text="*" runat="server" /> 
                                            <asp:Image ID="ImgUsernameInfo" AlternateText="username" ImageUrl="~/Images/icons/small/info.png" CssClass="info-pointer" runat="server" />
                                            <telerik:RadToolTip ID="RadToolTip23" TargetControlID="ImgUsernameInfo" Width="250" Position="TopRight" Animation="Fade" Text="Enter your company username" runat="server" />
                                        </div>
                                    </td>
                                    <td style="padding: 10px;">
                                        <div>
                                            <asp:ImageButton ID="LnkBtnGenerateUsername" OnClick="LnkBtnGenerateUsername_OnClick" ImageUrl="~/images/settings.png" runat="server" />
                                            <telerik:RadToolTip ID="RttGenerateUsername" TargetControlID="LnkBtnGenerateUsername" Position="TopRight" Animation="Fade" Text="Generate Username" runat="server" />
                                        </div>
                                    </td>                                      
                                </tr>
                                <tr class="step2-row">
                                    <td class="step2-td1">
                                        <div>
                                            <asp:Label ID="LblPassword" runat="server" Text="Password" />                                           
                                        </div>
                                    </td>
                                    <td class="step2-td2" style="width:350px;">
                                        <div class="step2-td2">
                                            <telerik:RadTextBox ID="RtbxPassword" MaxLength="45" Width="100%" runat="server" />                                                
                                            <div class="pers-info-row-error">
                                                <asp:Label ID="LblPasswordError" runat="server" />
                                            </div>
                                        </div>
                                        <div class="pers-info-row-error">
                                            <asp:Label ID="Label31" Text="*" runat="server" /> 
                                            <asp:Image ID="ImgPasswordInfo" AlternateText="password" ImageUrl="~/Images/icons/small/info.png" CssClass="info-pointer" runat="server" />
                                            <telerik:RadToolTip ID="RadToolTip24" TargetControlID="ImgPasswordInfo" Width="250" Position="TopRight" Animation="Fade" Text="Enter your company password" runat="server" />
                                        </div>
                                    </td> 
                                    <td style="padding: 10px;">
                                        <div>
                                            <asp:ImageButton ID="LnkBtnGeneratePassword" OnClick="LnkBtnGeneratePassword_OnClick" ImageUrl="~/images/settings.png" runat="server" />
                                            <telerik:RadToolTip ID="RttGeneratePassword" TargetControlID="LnkBtnGeneratePassword" Position="TopRight" Animation="Fade" Text="Generate Password" runat="server" />
                                        </div>
                                    </td>                                       
                                </tr>
                                <tr class="step2-row">
                                    <td class="step2-td1">
                                        <div>
                                            <asp:Label ID="Label3" runat="server" Text="Company Name" />                                           
                                        </div>
                                    </td>
                                    <td class="step2-td2" style="width:350px;">
                                        <div class="step2-td2">
                                            <telerik:RadTextBox ID="RtbxCompanyName" MaxLength="40" Width="100%" runat="server" />                                                
                                            <div class="pers-info-row-error">
                                                <asp:Label ID="LblCompanyNameError" runat="server" />
                                            </div>
                                        </div>
                                        <div class="pers-info-row-error">
                                            <asp:Label ID="Label39" Text="*" runat="server" /> 
                                            <asp:Image ID="ImgCompanyNameInfo" AlternateText="company name" ImageUrl="~/Images/icons/small/info.png" CssClass="info-pointer" runat="server" />
                                            <telerik:RadToolTip ID="RttMessage" TargetControlID="ImgCompanyNameInfo" Width="250" Position="TopRight" Animation="Fade" Text="Enter your company name or if you are a Developer your full name" runat="server" />
                                        </div>
                                    </td>                                        
                                </tr>
                                <tr class="step2-row">
                                    <td class="step2-td1">
                                        <div>                 
                                            <asp:Label ID="LblCompanyEmail" runat="server" Text="Company Email" />
                                        </div>
                                    </td>
                                    <td class="step2-td2" style="width:350px;">
                                        <div class="step2-td2">                                   
                                            <telerik:RadTextBox ID="RtbxCompanyEmail" MaxLength="100" Width="100%" runat="server" />
                                            <div class="pers-info-row-error">
                                                <asp:Label ID="LblCompanyEmailError" runat="server" />
                                            </div>  
                                        </div>
                                        <div class="pers-info-row-error">
                                            <asp:Label ID="Label9" Text="*" runat="server" />
                                            <asp:Image ID="ImgCompanyEmailInfo" AlternateText="company email" ImageUrl="~/Images/icons/small/info.png" CssClass="info-pointer" runat="server" />
                                            <telerik:RadToolTip ID="RadToolTip22" TargetControlID="ImgCompanyEmailInfo" Width="250" Position="TopRight" Animation="Fade" Text="Use company email, it will not be visible to other users or visitors. Your leads will be forwarded here." runat="server" />                                                       
                                        </div>
                                    </td>                                        
                                </tr>
                                <tr class="step2-row">
                                    <td class="step2-td1">
                                        <div>
                                            <asp:Label ID="Label2" runat="server" Text="Official Email" />
                                        </div>
                                    </td>
                                    <td class="step2-td2" style="width:350px;">
                                        <div class="step2-td2">                                   
                                            <telerik:RadTextBox ID="RtbxOfficialEmail" MaxLength="100" Width="100%" runat="server" />
                                            <div class="pers-info-row-error">
                                                <asp:Label ID="LblOfficialEmailError" runat="server" />
                                            </div>  
                                        </div>
                                        <div class="pers-info-row-error">
                                            <asp:Image ID="ImgOfficialEmailInfo" AlternateText="official email" ImageUrl="~/Images/icons/small/info.png" CssClass="info-pointer-left" runat="server" />
                                            <telerik:RadToolTip ID="RadToolTip1" TargetControlID="ImgOfficialEmailInfo" Width="250" Position="TopRight" Animation="Fade" Text="Use official email, it will not be visible to other users or visitors. Your leads will be forwarded here. If it's the same as your registration email just leave it empty." runat="server" />                                                       
                                        </div>
                                    </td>                                        
                                </tr>                                  
                                <tr class="step2-row">
                                    <td class="step2-td1">
                                        <div>                 
                                            <asp:Label ID="Label4" runat="server" Text="Website" />
                                        </div>
                                    </td>
                                    <td class="step2-td2" style="width:350px;">
                                        <div class="step2-td2">                                   
                                            <telerik:RadTextBox ID="RtbxWebSite" EmptyMessage="http:// or https://" MaxLength="100" Width="100%" runat="server" />                                                
                                            <div class="pers-info-row-error">
                                                <asp:Label ID="LblWeSiteError" runat="server" />
                                            </div>         
                                        </div>
                                        <div class="pers-info-row-error">
                                            <asp:Label ID="Label41" Text="*" runat="server" />
                                            <asp:Image ID="ImgWebSiteInfo" AlternateText="website" ImageUrl="~/Images/icons/small/info.png" CssClass="info-pointer" runat="server" />
                                            <telerik:RadToolTip ID="RadToolTip2" TargetControlID="ImgWebSiteInfo" Width="250" Position="TopRight" Animation="Fade" Text="Your company's website. If you are a Developer, you can enter your blog, Github etc." runat="server" />
                                        </div>   
                                    </td>                                        
                                </tr>
                                <tr class="step2-row">
                                    <td class="step2-td1">
                                        <div>
                                            <asp:Label ID="Label5" runat="server" Text="Country" />
                                        </div>
                                    </td>
                                    <td class="step2-td2" style="width:350px;">
                                        <div class="step2-td2">                                                
                                            <telerik:RadComboBox AutoPostBack="true" OnTextChanged="RcbxCountries_OnTextChanged" ID="RcbxCountries" Width="100%" Height="210" runat="server" />                                                
                                            <div class="pers-info-row-error">
                                                <asp:Label ID="LblCountryError" runat="server" />
                                            </div>
                                        </div>
                                        <div class="pers-info-row-error">
                                            <asp:Label ID="Label40" Text="*" runat="server" />
                                            <asp:Image ID="ImgCountriesInfo" AlternateText="countries" ImageUrl="~/Images/icons/small/info.png" CssClass="info-pointer" runat="server" />
                                            <telerik:RadToolTip ID="RadToolTip3" TargetControlID="ImgCountriesInfo" Width="250" Position="TopRight" Animation="Fade" Text="Enter an initial letter to jump directly to your country." runat="server" />
                                        </div>
                                    </td>                                        
                                </tr>
                                <tr class="step2-row">
                                    <td class="step2-td1">
                                        <div>                                  
                                            <asp:Label ID="Label6" runat="server" Text="Address" />
                                        </div> 
                                    </td>
                                    <td class="step2-td2" style="width:350px;">
                                        <div class="step2-td2">                                 
                                            <telerik:RadTextBox ID="RtbxAddress" MaxLength="100" Width="100%" runat="server" />                                               
                                            <div class="pers-info-row-error">
                                                <asp:Label ID="LblAddressError" runat="server" />
                                            </div>
                                        </div>
                                        <div class="pers-info-row-error">
                                            <asp:Label ID="Label43" Text="*" runat="server" />
                                            <asp:Image ID="ImgAddressInfo" AlternateText="address" ImageUrl="~/Images/icons/small/info.png" CssClass="info-pointer" runat="server" />
                                            <telerik:RadToolTip ID="RadToolTip4" TargetControlID="ImgAddressInfo" Width="250" Position="TopRight" Animation="Fade" Text="Enter your office address. If you are a Developer you can add your city or 'Global' for example." runat="server" />                                                
                                        </div>
                                    </td>                                        
                                </tr>
                                <tr class="step2-row hidden">
                                    <td class="step2-td1">
                                        <div>    
                                            <asp:Label ID="Label7" runat="server" Text="Phone" />
                                        </div>
                                    </td>
                                    <td class="step2-td2 hidden" style="width:350px;">
                                        <div class="step2-td2">
                                            <telerik:RadTextBox ID="RtbxPhoneNumberPrefix" Width="45" ReadOnly="false" runat="server" />
                                            <telerik:RadTextBox ID="RtbxPhone" onkeypress="return isNumberOnly(event);" MaxLength="15" Width="214" runat="server" />                                                
                                            <div class="pers-info-row-error">
                                                <asp:Label ID="LblPhoneError" runat="server" />
                                            </div>
                                        </div>
                                        <div class="pers-info-row-error">
                                            <asp:Image ID="ImgPhoneInfo" AlternateText="phone" ImageUrl="~/Images/icons/small/info.png" CssClass="info-pointer-left" runat="server" />
                                            <telerik:RadToolTip ID="RadToolTip5" TargetControlID="ImgPhoneInfo" Width="250" Position="TopRight" Animation="Fade" Text="Don't leave  spaces or use characters like dots, parenthesis etc." runat="server" />
                                        </div>
                                    </td>                                       
                                </tr>
                                <tr class="step2-row">
                                    <td class="step2-td1">
                                        <div>                 
                                            <asp:Label ID="Label14" runat="server" Text="Logo" />
                                        </div>
                                    </td>
                                    <td class="step2-td2" style="width:350px;">
                                        <div class="step2-td2" style="width:76%;">
                                            <telerik:RadAsyncUpload Width="100%" OnFileUploaded="Logo_OnFileUploaded" ID="Logo" MaxFileSize="100000" AllowedFileExtensions="png,jpg,jpeg,gif" MaxFileInputsCount="1" runat="server" />
                                            <div class="pers-info-row-error">
                                                <asp:Label ID="LblUploadError" runat="server" />
                                            </div>
                                        </div>
                                        <div class="pers-info-row-error">
                                            <asp:Label ID="Label1" Text="*" runat="server" />
                                            <asp:Image ID="ImgLogoInfo" AlternateText="logo" ImageUrl="~/Images/icons/small/info.png" CssClass="info-pointer" runat="server" />                                                
                                            <telerik:RadToolTip ID="RadToolTip6" TargetControlID="ImgLogoInfo" Width="350" Position="TopRight" Animation="Fade" Text="Please use an image no more than 100 Kb and of type: png, jpg, jpeg, gif. Ideal dimensions 400 X 300 pixels." runat="server" />                                                
                                        </div>
                                    </td>                                        
                                </tr>
                                <tr class="step2-row">
                                    <td>
                                        <div class="pers-info-row-error" style="padding-bottom:30px;">
                                            <asp:Label ID="Label42" Text="* Required Fields" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                                <tr class="step2-row">
                                    <td></td>
                                    <td>
                                        <div class="pers-info-row-cont">
                                            <telerik:RadButton ID="RbtnClearStep2" style="background-color:#cccccc; color:#454545;" OnClick="RbtnClearStep2_OnClick" runat="server">
                                                <ContentTemplate>
                                                    <span>
                                                        <asp:Label ID="LblLoginButtonText" Text="Clear" runat="server" />
                                                    </span>
                                                </ContentTemplate>
                                            </telerik:RadButton>
                                            <telerik:RadButton ID="RbtnStep2" OnClick="BtnNext_OnClick" runat="server">
                                                <ContentTemplate>
                                                    <span>
                                                        <asp:Label ID="LblLoginButtonText" Text="Next" runat="server" />
                                                    </span>
                                                </ContentTemplate>
                                            </telerik:RadButton>                                            
                                        </div>  
                                    </td>
                                </tr>                                
                            </table>
                        </ItemTemplate>
                    </telerik:RadPanelItem>
                </Items>
            </telerik:RadPanelItem>
            <telerik:RadPanelItem Visible="false" runat="server" Selected="true">
                <Items>
                    <telerik:RadPanelItem runat="server">
                        <ItemTemplate>
                            <div class="qsf-fb-0" style="text-align:left">    
                                <asp:Panel ID="PnlIndustry" runat="server">                            
                                    <div class="reg-header">
                                        <asp:Label ID="Label7" runat="server" Text="Select Industry" />
                                        <asp:Label ID="Label43" CssClass="pers-info-row-success" Text="  (* You have to select at least one Industry category)" runat="server" />
                                        <asp:Image ID="ImgIndustryInfo" AlternateText="industry" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                        <telerik:RadToolTip ID="RttMessage" TargetControlID="ImgIndustryInfo" Position="TopRight" Animation="Fade" Text="Choose all that apply" runat="server" />
                                    </div>
                                    <div class="dash-bar-row-1">
                                        <asp:CheckBoxList ID="cb1" TextAlign="left" runat="server">                            
                                        </asp:CheckBoxList>
                                    </div>
                                    <div class="dash-bar-row-1">    
                                        <asp:CheckBoxList ID="cb2" TextAlign="left" runat="server">                            
                                        </asp:CheckBoxList> 
                                    </div>
                                    <div class="dash-bar-row-1"> 
                                        <asp:CheckBoxList ID="cb3" TextAlign="left" runat="server">                            
                                        </asp:CheckBoxList> 
                                    </div>
                                    <div class="row row-fix">
                                        <asp:Label ID="LblIndustryError" CssClass="pers-info-row-error" runat="server" />
                                    </div>                                    
                                </asp:Panel>
                                <asp:Panel ID="PnlPartner" runat="server">
                                    <div class="reg-header">
                                        <asp:Label ID="Label19" runat="server" Text="Partner Program" />
                                        <asp:Label ID="Label46" CssClass="pers-info-row-success" Text="  (* You have to select at least one Partner Program category)" runat="server" />
                                        <asp:Image ID="ImgPartnerInfo" AlternateText="partner" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                        <telerik:RadToolTip ID="RadToolTip6" TargetControlID="ImgPartnerInfo" Width="300" Position="TopRight" Animation="Fade" Text="Select the partner programs that you offer (for vendors) or if you are a reseller the type of programs that you are interested in. Choose all that apply" runat="server" />
                                    </div>
                                    <div class="dash-bar-row-2" style="margin-left:3%;">
                                        <asp:CheckBoxList ID="cb4" TextAlign="left" runat="server">                            
                                        </asp:CheckBoxList> 
                                    </div>
                                    <div class="dash-bar-row-2" style="margin-left:3%;">
                                        <asp:CheckBoxList ID="cb5" TextAlign="left" runat="server">                            
                                        </asp:CheckBoxList>
                                    </div>
                                    <div class="dash-bar-row-2" style="margin-left:3%;">
                                        <asp:CheckBoxList ID="cb6" TextAlign="left" runat="server">                            
                                        </asp:CheckBoxList>
                                    </div>
                                    <div class="row row-fix">
                                        <asp:Label ID="LblPartnerError" CssClass="pers-info-row-error" runat="server" />
                                    </div>                                    
                                </asp:Panel>
                                <asp:Panel ID="PnlMarket" runat="server">
                                    <div class="reg-header">
                                        <asp:Label ID="Label25" runat="server" Text="Market Specialisation" />
                                        <asp:Label ID="Label47" CssClass="pers-info-row-success" Text="  (* You have to select at least one Market Specialisation category)" runat="server" />
                                        <asp:Image ID="ImgMarketInfo" AlternateText="market" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                        <telerik:RadToolTip ID="RadToolTip7" TargetControlID="ImgMarketInfo" Width="300" Position="TopRight" Animation="Fade" Text="What is your target market." runat="server" />
                                    </div>
                                    <div class="dash-bar-row-3" style="margin-left:3%;">    
                                        <asp:CheckBoxList ID="cb7" TextAlign="left" runat="server">                                                        
                                        </asp:CheckBoxList>
                                    </div>
                                    <div class="dash-bar-row-3" style="margin-left:3%;width:300px;">   
                                        <asp:CheckBoxList ID="cb8" TextAlign="left" runat="server">                                                        
                                        </asp:CheckBoxList>
                                    </div>
                                    <div></div>
                                    <div class="row row-fix" style="width:500px;margin-left:3%;">
                                        <asp:Label ID="LblMarketError" CssClass="pers-info-row-error" runat="server" />
                                    </div>                                                                      
                                </asp:Panel>                                
                                <asp:Panel ID="PnlApi" runat="server">
                                    <div class="reg-header">
                                        <asp:Label ID="Label28" runat="server" Text="API Category" />
                                        <asp:Label ID="Label48" Visible="false" CssClass="pers-info-row-success" Text="  (* You have to select at least one API category)" runat="server" />
                                    </div>
                                    <div class="dash-bar-row-4" style="margin-left:3%;">  
                                        <asp:CheckBoxList ID="cb9" TextAlign="left" runat="server">                            
                                        </asp:CheckBoxList>
                                    </div>
                                    <div class="dash-bar-row-4" style="margin-left:3%;">   
                                        <asp:CheckBoxList ID="cb10" TextAlign="left" runat="server">                            
                                        </asp:CheckBoxList> 
                                    </div>
                                    <div class="dash-bar-row-4" style="margin-left:3%;">   
                                        <asp:CheckBoxList ID="cb11" TextAlign="left" runat="server">                            
                                        </asp:CheckBoxList>
                                    </div>
                                    <div class="row row-fix">
                                        <asp:Label ID="LblApiError" CssClass="pers-info-row-error" runat="server" />
                                    </div>                                    
                                </asp:Panel>
                                <asp:Panel ID="PnlMashape" Visible="false" runat="server">
                                    <div class="dash-bar-row" style="padding:10px; font-size:13px; font-weight:500; width:100%; margin-top:20px;">
                                        <asp:Label ID="LblMashape" Text="Mashape API page: http://mashape.com/" runat="server" />
                                        <telerik:RadTextBox ID="RtbxMashape" Width="150" EmptyMessage="username" runat="server" />
                                        <asp:Image ID="Imgmashape" AlternateText="mashape" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                        <telerik:RadToolTip ID="RadToolTip9" TargetControlID="Imgmashape" Position="TopRight" Animation="Fade" Text="Enter your Mashape username" runat="server" />
                                    </div>
                                    <div class="dash-bar-row" style="padding:10px; font-size:13px; font-weight:500; margin-bottom:20px;width:100%;">
                                        <asp:Label ID="LblMashapeInfo" CssClass="bold" Text="Don't have Mashape account? Create a free account. Mention Elio to get 50% discount on your premium plans " runat="server" />
                                        <asp:HyperLink ID="HpLnkMashape" CssClass="bold" NavigateUrl="https://www.mashape.com/?utm_source=myelio&utm_medium=website&utm_campaign=partners" Text="here" Target="_blank" runat="server" />
                                    </div>
                                </asp:Panel>
                                <div style="margin-bottom:3                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             0px;"></div>
                                <div class="row row-fix"> 
                                    <telerik:RadButton ID="RbtnClearStep3" style="background-color:#cccccc; color:#454545;" OnClick="RbtnClearStep3_OnClick" runat="server">
                                        <ContentTemplate>
                                            <span>
                                                <asp:Label ID="LblLoginButtonText" Text="Clear" runat="server" />
                                            </span>
                                        </ContentTemplate>
                                    </telerik:RadButton>
                                    <telerik:RadButton ID="RbtnStep3" OnClick="BtnNext_OnClick" runat="server">
                                        <ContentTemplate>
                                            <span>
                                                <asp:Label ID="LblLoginButtonText" Text="Next" runat="server" />
                                            </span>
                                        </ContentTemplate>
                                    </telerik:RadButton>
                                </div>                                                                
                            </div>
                        </ItemTemplate>
                    </telerik:RadPanelItem>
                </Items>
            </telerik:RadPanelItem>
            <telerik:RadPanelItem Visible="false" runat="server">
                <Items>
                    <telerik:RadPanelItem runat="server">
                        <ItemTemplate>
                            <div class="qsf-fb-0" style="text-align:left">    
                                <asp:Panel ID="PnlSubIndustry" runat="server">                            
                                    <div class="reg-header">
                                        <asp:Label ID="LabelSub7" runat="server" Text="Sales & Marketing" />
                                        <asp:Image ID="ImgSubIndustryInfo" AlternateText="subindustry" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                        <telerik:RadToolTip ID="RttSubMessage" TargetControlID="ImgSubIndustryInfo" Position="TopRight" Animation="Fade" Text="Choose all that apply" runat="server" />
                                    </div>
                                    <div style="display: inline-block; width:100%;">
                                        <div class="dash-bar-row-5" style="margin-left:3%;">   
                                            <asp:CheckBoxList ID="cbSub1" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="dash-bar-row-5" style="margin-left:3%;">    
                                            <asp:CheckBoxList ID="cbSub2" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                        <div class="dash-bar-row-5" style="margin-left:3%;"> 
                                            <asp:CheckBoxList ID="cbSub3" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                    </div>
                                    <div class="reg-header">
                                        <asp:Label ID="Label15" runat="server" Text="Customer Management" />
                                        <asp:Image ID="Image1" AlternateText="Customer Management" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                        <telerik:RadToolTip ID="RadToolTip10" TargetControlID="Image1" Position="TopRight" Animation="Fade" Text="Choose all that apply" runat="server" />
                                    </div>
                                    <div style="display: inline-block; width:100%;">
                                        <div class="dash-bar-row-6" style="margin-left:3%;">   
                                            <asp:CheckBoxList ID="cbSub4" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="dash-bar-row-6" style="margin-left:3%;">    
                                            <asp:CheckBoxList ID="cbSub5" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                        <div class="dash-bar-row-6" style="margin-left:3%;"> 
                                            <asp:CheckBoxList ID="cbSub6" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                    </div>
                                    <div class="reg-header">
                                        <asp:Label ID="Label17" runat="server" Text="Project Management" />
                                        <asp:Image ID="Image2" AlternateText="Project Management" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                        <telerik:RadToolTip ID="RadToolTip11" TargetControlID="Image2" Position="TopRight" Animation="Fade" Text="Choose all that apply" runat="server" />
                                    </div>
                                    <div style="display: inline-block; width:100%;">
                                        <div class="dash-bar-row-7" style="margin-left:3%;">   
                                            <asp:CheckBoxList ID="cbSub7" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="dash-bar-row-7" style="margin-left:3%;">    
                                            <asp:CheckBoxList ID="cbSub8" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                        <div class="dash-bar-row-7" style="margin-left:3%;"> 
                                            <asp:CheckBoxList ID="cbSub9" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                    </div>
                                    <div class="reg-header">
                                        <asp:Label ID="Label18" runat="server" Text="Operations & Workflow" />
                                        <asp:Image ID="Image3" AlternateText="Operations & Workflow" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                        <telerik:RadToolTip ID="RadToolTip12" TargetControlID="Image3" Position="TopRight" Animation="Fade" Text="Choose all that apply" runat="server" />
                                    </div>
                                    <div style="display: inline-block; width:100%;">
                                        <div class="dash-bar-row-8" style="margin-left:1%; min-width:232px;">   
                                            <asp:CheckBoxList ID="cbSub10" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="dash-bar-row-8" style="margin-left:1%; min-width:232px;">    
                                            <asp:CheckBoxList ID="cbSub11" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                        <div class="dash-bar-row-8" style="margin-left:1%; min-width:232px;"> 
                                            <asp:CheckBoxList ID="cbSub12" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                    </div>
                                    <div class="reg-header">
                                        <asp:Label ID="Label20" runat="server" Text="Tracking & Measurement" />
                                        <asp:Image ID="Image4" AlternateText="Tracking & Measurement" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                        <telerik:RadToolTip ID="RadToolTip13" TargetControlID="Image4" Position="TopRight" Animation="Fade" Text="Choose all that apply" runat="server" />
                                    </div>
                                    <div style="display: inline-block; width:100%;">
                                        <div class="dash-bar-row-9" style="margin-left:3%;">   
                                            <asp:CheckBoxList ID="cbSub13" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="dash-bar-row-9" style="margin-left:3%;">    
                                            <asp:CheckBoxList ID="cbSub14" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                        <div class="dash-bar-row-9" style="margin-left:3%;"> 
                                            <asp:CheckBoxList ID="cbSub15" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                    </div>                                    
                                    <div class="reg-header">
                                        <asp:Label ID="Label21" runat="server" Text="Accounting & Financials" />
                                        <asp:Image ID="Image5" AlternateText="Accounting & Financials" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                        <telerik:RadToolTip ID="RadToolTip14" TargetControlID="Image5" Position="TopRight" Animation="Fade" Text="Choose all that apply" runat="server" />
                                    </div>
                                    <div style="display: inline-block; width:100%;">
                                        <div class="dash-bar-row-10" style="margin-left:3%;">   
                                            <asp:CheckBoxList ID="cbSub16" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="dash-bar-row-10" style="margin-left:3%;">    
                                            <asp:CheckBoxList ID="cbSub17" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                        <div class="dash-bar-row-10" style="margin-left:3%;"> 
                                            <asp:CheckBoxList ID="cbSub18" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                    </div>                                    
                                    <div class="reg-header">
                                        <asp:Label ID="Label22" runat="server" Text="HR" />
                                        <asp:Image ID="Image6" AlternateText="HR" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                        <telerik:RadToolTip ID="RadToolTip15" TargetControlID="Image6" Position="TopRight" Animation="Fade" Text="Choose all that apply" runat="server" />
                                    </div>
                                    <div style="display: inline-block; width:100%;">
                                        <div class="dash-bar-row-11" style="margin-left:1%; min-width:230px;">   
                                            <asp:CheckBoxList ID="cbSub19" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="dash-bar-row-11" style="margin-left:1%; min-width:230px;">    
                                            <asp:CheckBoxList ID="cbSub20" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                        <div class="dash-bar-row-11" style="margin-left:1%; min-width:230px;"> 
                                            <asp:CheckBoxList ID="cbSub21" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                    </div>                                    
                                    <div class="reg-header">
                                        <asp:Label ID="Label23" runat="server" Text="Web Mobile Software Development" />
                                        <asp:Image ID="Image7" AlternateText="Web Mobile Software Development" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                        <telerik:RadToolTip ID="RadToolTip16" TargetControlID="Image7" Position="TopRight" Animation="Fade" Text="Choose all that apply" runat="server" />
                                    </div>
                                    <div style="display: inline-block; width:100%;">
                                        <div class="dash-bar-row-12" style="margin-left:3%;">   
                                            <asp:CheckBoxList ID="cbSub22" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="dash-bar-row-12" style="margin-left:3%;">    
                                            <asp:CheckBoxList ID="cbSub23" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                        <div class="dash-bar-row-12" style="margin-left:3%;"> 
                                            <asp:CheckBoxList ID="cbSub24" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                    </div>                                    
                                    <div class="reg-header">
                                        <asp:Label ID="Label24" runat="server" Text="IT & Infrastructure" />
                                        <asp:Image ID="Image8" AlternateText="IT & Infrastructure" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                        <telerik:RadToolTip ID="RadToolTip17" TargetControlID="Image8" Position="TopRight" Animation="Fade" Text="Choose all that apply" runat="server" />
                                    </div>
                                    <div style="display: inline-block; width:100%;">
                                        <div class="dash-bar-row-13" style="margin-left:3%;">   
                                            <asp:CheckBoxList ID="cbSub25" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="dash-bar-row-13" style="margin-left:3%;">    
                                            <asp:CheckBoxList ID="cbSub26" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                        <div class="dash-bar-row-13" style="margin-left:3%;"> 
                                            <asp:CheckBoxList ID="cbSub27" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                    </div>                                    
                                    <div class="reg-header">
                                        <asp:Label ID="Label26" runat="server" Text="Business Utilities" />
                                        <asp:Image ID="Image9" AlternateText="Business Utilities" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                        <telerik:RadToolTip ID="RadToolTip18" TargetControlID="Image9" Position="TopRight" Animation="Fade" Text="Choose all that apply" runat="server" />
                                    </div>
                                    <div style="display: inline-block; width:100%;">
                                        <div class="dash-bar-row-14" style="margin-left:3%;">   
                                            <asp:CheckBoxList ID="cbSub28" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="dash-bar-row-14" style="margin-left:3%;">    
                                            <asp:CheckBoxList ID="cbSub29" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                        <div class="dash-bar-row-14" style="margin-left:3%;"> 
                                            <asp:CheckBoxList ID="cbSub30" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                    </div>                                    
                                    <div class="reg-header">
                                        <asp:Label ID="Label27" runat="server" Text="Data Security & GRC" />
                                        <asp:Image ID="Image10" AlternateText="Data Security & GRC" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                        <telerik:RadToolTip ID="RadToolTip19" TargetControlID="Image10" Position="TopRight" Animation="Fade" Text="Choose all that apply" runat="server" />
                                    </div>
                                    <div style="display: inline-block; width:100%;">
                                        <div class="dash-bar-row-15" style="margin-left:3%;">   
                                            <asp:CheckBoxList ID="cbSub31" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="dash-bar-row-15" style="margin-left:3%;">    
                                            <asp:CheckBoxList ID="cbSub32" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                        <div class="dash-bar-row-15" style="margin-left:3%;"> 
                                            <asp:CheckBoxList ID="cbSub33" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                    </div>                                    
                                    <div class="reg-header">
                                        <asp:Label ID="Label29" runat="server" Text="Design & Multimedia" />
                                        <asp:Image ID="Image11" AlternateText="Design & Multimedia" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                        <telerik:RadToolTip ID="RadToolTip20" TargetControlID="Image11" Position="TopRight" Animation="Fade" Text="Choose all that apply" runat="server" />
                                    </div>
                                    <div style="display: inline-block; width:100%;">
                                        <div class="dash-bar-row-16" style="margin-left:3%;">   
                                            <asp:CheckBoxList ID="cbSub34" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="dash-bar-row-16" style="margin-left:3%;">    
                                            <asp:CheckBoxList ID="cbSub35" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                        <div class="dash-bar-row-16" style="margin-left:3%;"> 
                                            <asp:CheckBoxList ID="cbSub36" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                    </div>                                                                        
                                    <div class="reg-header">
                                        <asp:Label ID="Label8" runat="server" Text="Miscellaneous" />
                                        <asp:Image ID="Image12" AlternateText="Miscellaneous" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                        <telerik:RadToolTip ID="RadToolTip21" TargetControlID="Image12" Position="TopRight" Animation="Fade" Text="Choose all that apply" runat="server" />
                                    </div>
                                    <div style="display: inline-block; width:100%;">
                                        <div class="dash-bar-row-3" style="margin-left:3%;">   
                                            <asp:CheckBoxList ID="cbSub37" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="dash-bar-row-3" style="margin-left:3%;">    
                                            <asp:CheckBoxList ID="cbSub38" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                    </div>
                                    <div class="reg-header">
                                        <asp:Label ID="Label16" runat="server" Text="Unified Communications" />
                                        <asp:Image ID="Image13" AlternateText="Unified Communications" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                        <telerik:RadToolTip ID="RadToolTip25" TargetControlID="Image11" Position="TopRight" Animation="Fade" Text="Choose all that apply" runat="server" />
                                    </div>
                                    <div style="display: inline-block; width:100%;">
                                        <div class="dash-bar-row-16" style="margin-left:3%;">   
                                            <asp:CheckBoxList ID="cbSub39" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="dash-bar-row-16" style="margin-left:3%;">    
                                            <asp:CheckBoxList ID="cbSub40" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                        <div class="dash-bar-row-16" style="margin-left:3%;"> 
                                            <asp:CheckBoxList ID="cbSub41" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                    </div>
                                    <div class="reg-header">
                                        <asp:Label ID="Label30" runat="server" Text="CAD & PLM" />
                                        <asp:Image ID="Image14" AlternateText="CAD & PLM" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                        <telerik:RadToolTip ID="RadToolTip26" TargetControlID="Image11" Position="TopRight" Animation="Fade" Text="Choose all that apply" runat="server" />
                                    </div>
                                    <div style="display: inline-block; width:100%;">
                                        <div class="dash-bar-row-16" style="margin-left:3%;">   
                                            <asp:CheckBoxList ID="cbSub42" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="dash-bar-row-16" style="margin-left:3%;">    
                                            <asp:CheckBoxList ID="cbSub43" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                        <div class="dash-bar-row-16" style="margin-left:3%;"> 
                                            <asp:CheckBoxList ID="cbSub44" TextAlign="left" runat="server">                            
                                            </asp:CheckBoxList> 
                                        </div>
                                    </div>
                                    <div class="row row-fix">
                                        <asp:Label ID="LblSubIndustryError" CssClass="pers-info-row-error" runat="server" />
                                    </div>
                                </asp:Panel>
                                <div class="row row-fix"> 
                                    <telerik:RadButton ID="RbtnClearStepSub3" style="background-color:#cccccc; color:#454545;" OnClick="RbtnClearStepSub3_OnClick" runat="server">
                                        <ContentTemplate>
                                            <span>
                                                <asp:Label ID="LblSubLoginButtonText" Text="Clear" runat="server" />
                                            </span>
                                        </ContentTemplate>
                                    </telerik:RadButton>
                                    <telerik:RadButton ID="RbtnStepSub3" OnClick="BtnNext_OnClick" runat="server">
                                        <ContentTemplate>
                                            <span>
                                                <asp:Label ID="LblSubLoginButtonText" Text="Next" runat="server" />
                                            </span>
                                        </ContentTemplate>
                                    </telerik:RadButton>                                    
                                </div>                                                                
                            </div>
                        </ItemTemplate>
                    </telerik:RadPanelItem>
                </Items>
            </telerik:RadPanelItem>            
            <telerik:RadPanelItem Visible="false" runat="server">
                <Items>
                    <telerik:RadPanelItem>
                        <ItemTemplate>
                            <div class="qsf-fb-0" style="text-align:left">
                                <div class="reg-header">
                                    <asp:Label ID="Label37" runat="server" />
                                    <asp:Label ID="Label45" Visible="false" CssClass="pers-info-row-error" runat="server" Text=" *" />
                                    <asp:Image ID="ImgOverviewInfo" Visible="false" AlternateText="overview" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                    <telerik:RadToolTip ID="RttMessage" Visible="false" TargetControlID="ImgOverviewInfo" Width="300" Position="TopRight" Animation="Fade" Text="A brief description of your company and products. If you are a Developer you can add your past and current projects etc." runat="server" />
                                </div>
                                <div class="row row-fix">
                                    <telerik:RadTextBox ID="RtbxOverView" runat="server" Width="98%" MaxLength="2000" TextMode="MultiLine" Rows="5" Height="100" />
                                </div>
                                <div class="row row-fix">
                                    <asp:Label ID="LblOverViewError" CssClass="pers-info-row-error" runat="server" />
                                </div>
                                <div class="reg-header">
                                    <asp:Label ID="Label11" runat="server" />
                                    <asp:Label ID="Label49" Visible="false" CssClass="pers-info-row-error" runat="server" Text=" *" />
                                    <asp:Image ID="ImgDescriptionInfo" Visible="false" AlternateText="description" ImageUrl="~/Images/icons/small/info.png" CssClass="pointer" runat="server" />
                                    <telerik:RadToolTip ID="RadToolTip8" Visible="false" TargetControlID="ImgDescriptionInfo" Width="300" Position="TopRight" Animation="Fade" Text="For vendors: add your partner program description and structure. For resellers: add some details on type of programs and products that you are interested on reselling and your company's experience. For developers: you can add in details what kind of products you'd like to build and in what industry." runat="server" />
                                </div>
                                <div class="row row-fix">
                                    <telerik:RadTextBox ID="RtbxDescription" Width="98%" runat="server" MaxLength="3000" TextMode="MultiLine" Rows="5" Height="200" />
                                </div>
                                <div class="row row-fix">
                                    <asp:Label ID="LblDescriptionError" CssClass="pers-info-row-error" runat="server" />
                                </div>
                                <div class="row row-fix">
                                    <asp:Label ID="Label50" CssClass="pers-info-row-error" runat="server" Text=" * Required Fields" />
                                </div>
                                <div class="row row-fix">
                                    <asp:CheckBox runat="server" ID="CbxTerms" />
                                    <asp:Label ID="LblTerms" runat="server" Text="I agree to the Terms and Conditions" />
                                </div>
                                <div class="row row-fix">
                                    <asp:Label ID="LblTermsError" CssClass="pers-info-row-error" runat="server" />
                                </div>
                                <div class="row row-fix">
                                    <telerik:RadButton ID="RbtnClearStep4" style="background-color:#cccccc; color:#454545;" OnClick="RbtnClearStep4_OnClick" runat="server">
                                        <ContentTemplate>
                                            <span>
                                                <asp:Label ID="LblLoginButtonText" Text="Clear" runat="server" />
                                            </span>
                                        </ContentTemplate>
                                    </telerik:RadButton>                                    
                                    <telerik:RadButton ID="RbtnStep4" OnClick="BtnNext_OnClick" runat="server">
                                        <ContentTemplate>
                                            <span>
                                                <asp:Label ID="LblLoginButtonText" Text="Create Account" runat="server" />
                                            </span>
                                        </ContentTemplate>
                                    </telerik:RadButton>                                               
                                </div>                         
                            </div>
                        </ItemTemplate>
                    </telerik:RadPanelItem>
                </Items>
            </telerik:RadPanelItem>
            <telerik:RadPanelItem Visible="false" runat="server">
                <Items>
                    <telerik:RadPanelItem>
                        <ItemTemplate>
                            <div class="qsf-fb-0" style="text-align:left">
                                <div class="" style="padding:20px; font-size:18px;">
                                    <asp:Image ID="ImgSuccess" AlternateText="success" ImageUrl="~/Images/thank-page-check.png" runat="server" />
                                    <asp:Label ID="Label370" Text="You have added successfully a company registration which is your client." runat="server" /><br /><br />
                                    <asp:Label ID="Label10" Text="You can edit or delete it from your dashboard any time you want it." runat="server" /><br /><br />
                                    <asp:Label ID="Label12" Text="You must login as this specific company and add it's criteria, in order to get matched with possible partners." runat="server" />
                                    <div class="col-md-12" style="margin-bottom:50px; margin-top:30px; text-align:center;">
                                        <a id="aCloseAccount" runat="server" class="btn btn-circle purple btn-lg">
                                            <asp:Label ID="LblCloseAccount" Text="Close Registration Panel" runat="server" />
                                        </a>
                                    </div>
                                </div>                               
                            </div>
                        </ItemTemplate>
                    </telerik:RadPanelItem>
                </Items>
            </telerik:RadPanelItem>
        </Items>
        <CollapseAnimation Duration="0" Type="None" />
        <ExpandAnimation Duration="0" Type="None" />
    </telerik:RadPanelBar>
</asp:Panel>