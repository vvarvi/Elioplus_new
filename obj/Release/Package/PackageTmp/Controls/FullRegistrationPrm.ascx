<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FullRegistrationPrm.ascx.cs" Inherits="WdS.ElioPlus.Controls.FullRegistrationPrm" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">

        function isNumberOnly(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        function OpenConfirmationPopUp() {
            $('#PopUpMessageAlert').modal('show');
        }

        function CloseConfirmationPopUp() {
            $('#PopUpMessageAlert').modal('hide');
        }

    </script>
</telerik:RadScriptBlock>

<asp:Panel ID="PnlRegister" runat="server">
    <telerik:RadPanelBar ID="RpbRegistrationSteps" OnItemClick="RpbRegistrationSteps_ItemClick" ExpandMode="SingleExpandedItem" Width="100%" runat="server">
        <Items>
            <telerik:RadPanelItem Expanded="True" Selected="true" BackColor="#5995bb" ForeColor="White" runat="server">
                <Items>
                    <telerik:RadPanelItem>
                        <ItemTemplate>
                            <div class="col-md-12 skin skin-flat form-body form-group input-group-large icheck-list" style="padding-left: 38%; margin-top: 50px;">
                                <div class="col-md-6" style="font-size: 20px">
                                    <asp:RadioButtonList ID="RdBtnUserType" AutoPostBack="true" OnSelectedIndexChanged="RdBtnUserType_OnSelectedIndexChanged" CssClass="icheck" data-radio="iradio_square-red" RepeatDirection="Vertical" runat="server">
                                        <asp:ListItem Value="1" Selected="True" Text="I am a Vendor" />
                                        <asp:ListItem Value="2" Text="I am a Channel Partner" />
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="col-md-12" style="text-align: center; margin-top: -15px; margin-bottom: 20px;">
                                <strong>
                                    <a id="aDennyInvitation" visible="false" runat="server" href="#PopUpMessageAlert" data-toggle="modal"
                                        role="button">
                                        <asp:Label ID="LblInvitationToPartners" Text="Denny Invitation" runat="server" />
                                    </a>
                                </strong>
                            </div>
                            <div class="form col-md-6" style="text-align: center; font-size: 16px; margin-left: 24%">
                                <div id="divUserTypeError" runat="server" visible="false" class="alert alert-danger">
                                    <strong>
                                        <asp:Label ID="LblUserTypeError" runat="server" /></strong><asp:Label ID="LblUserTypeErrorContent" runat="server" />
                                </div>
                            </div>
                            <div class="form col-md-12" style="text-align: center;">
                                <div class="col-md-8" style="margin-left: 17%; margin-right: 17%;">
                                    <div class="form-group form-md-line-input form-md-floating-label has-success" style="text-align: left;">
                                        <asp:TextBox ID="TbxCompanyName" MaxLength="100" Font-Size="18px" CssClass="form-control input-lg" runat="server" />
                                        <label for="TbxCompanyName" runat="server">Company name</label>
                                        <span class="help-block" style="font-size: 16px">Your company's official full name (*required)</span>
                                    </div>
                                    <div class="pers-info-row-error" style="text-align: left; font-size: 18px;">
                                        <asp:Label ID="LblCompanyNameError" runat="server" />
                                    </div>
                                </div>                                
                                <div class="col-md-8" style="margin-left: 17%; margin-right: 17%;">
                                    <div class="form-group form-md-line-input form-md-floating-label has-success" style="text-align: left;">
                                        <asp:TextBox ID="TbxWebsite" MaxLength="49" Font-Size="18px" CssClass="form-control input-lg" runat="server" />
                                        <label for="TbxWebsite" runat="server">Company website</label>
                                        <span class="help-block" style="font-size: 16px">Your company's official website starting with http:// or https:// (*required)</span>
                                    </div>
                                    <div class="pers-info-row-error" style="text-align: left; font-size: 18px;">
                                        <asp:Label ID="LblWeSiteError" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-8" style="margin-left: 17%; margin-right: 17%;">
                                    <div class="form-group form-md-line-input form-md-floating-label has-success" style="text-align: left;">
                                        <asp:DropDownList ID="DdlCountries" Font-Size="15px" CssClass="form-control input-lg" runat="server">
                                        </asp:DropDownList>
                                        <label for="DdlCountries" runat="server">Country</label>
                                        <span class="help-block" style="font-size: 16px">Select your country (*required)</span>
                                    </div>
                                    <div class="pers-info-row-error" style="text-align: left; font-size: 18px;">
                                        <asp:Label ID="LblCountryError" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-8" style="margin-left: 17%; margin-right: 17%;">
                                    <div class="form-group form-md-line-input form-md-floating-label has-success" style="text-align: left;">
                                        <asp:TextBox ID="TbxAddress" MaxLength="350" Font-Size="18px" CssClass="form-control input-lg" runat="server" />
                                        <label for="TbxAddress" runat="server">Address</label>
                                        <span class="help-block" style="font-size: 16px">Your company's address (*required)</span>
                                    </div>
                                    <div class="pers-info-row-error" style="text-align: left; font-size: 18px;">
                                        <asp:Label ID="LblAddressError" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-8" style="margin-left: 17%; margin-right: 17%;">
                                    <div class="form-group form-md-line-input form-md-floating-label has-success" style="text-align: left;">
                                        <asp:TextBox ID="TbxState" MaxLength="99" Font-Size="18px" CssClass="form-control input-lg" runat="server" />
                                        <label for="TbxState" runat="server">State</label>
                                        <span class="help-block" style="font-size: 16px">Your company's state</span>
                                    </div>
                                    <div class="pers-info-row-error" style="text-align: left; font-size: 18px;">
                                        <asp:Label ID="LblTbxStateError" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-8" style="margin-left: 17%; margin-right: 17%;">
                                    <div class="form-group form-md-line-input form-md-floating-label has-success" style="text-align: left;">
                                        <asp:TextBox ID="TbxCompanyPhone" MaxLength="20" Font-Size="18px" CssClass="form-control input-lg" runat="server" />
                                        <label id="LblCompanyPhone" for="TbxCompanyPhone" runat="server">Phone number</label>
                                        <span class="help-block" style="font-size: 16px">Your company's phone number</span>
                                    </div>
                                    <div class="pers-info-row-error" style="text-align: left; font-size: 18px;">
                                        <asp:Label ID="LblCompanyPhoneError" runat="server" />
                                    </div>
                                </div>
                                <div id="divProductDemoLink" visible="false" class="col-md-8" style="margin-left: 17%; margin-right: 17%;" runat="server">
                                    <div class="form-group form-md-line-input form-md-floating-label has-success" style="text-align: left;">
                                        <asp:TextBox ID="TbxProductDemoLink" MaxLength="350" Font-Size="18px" CssClass="form-control input-lg" runat="server" />
                                        <label for="TbxProductDemoLink" runat="server">Product Demo Link</label>
                                        <span class="help-block" style="font-size: 16px">Your company's Product Demo Link</span>
                                    </div>
                                    <div class="pers-info-row-error" style="text-align: left; font-size: 18px;">
                                        <asp:Label ID="LblProductDemoLinkError" runat="server" />
                                    </div>
                                </div>
                                <div id="divLogoInfo" runat="server" class="col-md-8 alert alert-success" style="margin-left: 17%; margin-right: 17%; font-size: 16px; margin-top: 50px; margin-bottom: 0px">
                                    <strong>
                                        <asp:Label ID="LblLogoInfo" runat="server" Text="Info! " /></strong><asp:Label ID="LblLogoInfoContent" Text="Logo is required(*), must be of type '.png, jpg', maximum size accepted is '20 kb' and ideal dimensions are '200x100 px'. Image is uploaded by clicking 'Next'." runat="server" />
                                </div>
                                <div class="col-md-12" style="text-align: center; margin-left: 17%; margin-right: 17%;">
                                    <asp:UpdatePanel runat="server" ID="LogoUpdatePanel">
                                        <ContentTemplate>
                                            <asp:Panel ID="PnlEditLogo" CssClass="col-md-4" runat="server">
                                                <asp:Label ID="LblLogoUpdateHeader" ForeColor="#9A12B3" Font-Bold="true" runat="server" />
                                                <br />
                                                <div class="form-group" style="margin-top: 10px;">
                                                    <div class="fileinput fileinput-new" data-provides="fileinput">
                                                        <div class="fileinput-new thumbnail" style="width: 200px;">
                                                            <asp:Image ID="ImgPhotoBckgr" ImageUrl="/images/no_logo_company-1.png" AlternateText="Update logo" runat="server" />
                                                        </div>
                                                        <div class="fileinput-preview fileinput-exists thumbnail hidden" style="max-width: 200px; max-height: 100px;"></div>
                                                        <div>
                                                            <span class="btn default btn-file">
                                                                <span class="fileinput-new hidden">
                                                                    <asp:Label ID="LblSelectImg" runat="server" /></span>
                                                                <span class="fileinput-exists hidden">
                                                                    <asp:Label ID="LblChangeImg" runat="server" /></span>
                                                                <input type="file" enableviewstate="true" name="ImgCompanyLogo" id="CompanyLogo" runat="server" accept=".png, .jpg, .jpeg" />
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="margin-top-10">
                                                    <asp:Button ID="BtnSubmitLogo" Visible="false" CssClass="btn green" runat="server" />
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="PnlViewLogo" CssClass="col-md-4" runat="server">
                                                <div style="margin-top: 32px; max-width: 200px; max-height: 100px; display: flex;">
                                                    <asp:Image ID="ImgViewLogo" Width="100%" AlternateText="No logo yet - upload one!" ImageAlign="Middle" runat="server" />
                                                </div>
                                            </asp:Panel>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="BtnSubmitLogo" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <div id="divLogoFailure" runat="server" visible="false" class="alert alert-danger col-md-12" style="font-size: 16px;">
                                    <strong>
                                        <asp:Label ID="LblLogoFailure" runat="server" /></strong><asp:Label ID="LblLogoFailureContent" runat="server" />
                                </div>
                                <div class="form-body col-md-12" style="text-align: center; margin-bottom: 50px;">
                                    <asp:Button ID="BtnClearStep1" Font-Size="18px" OnClick="BtnClearStep1_OnClick" data-loading-text="Clearing..." class="demo-loading-btn btn grey-cascade" Width="150px" runat="server" Text="Clear" />
                                    <asp:Button ID="BtnStep1" Font-Size="18px" OnClick="BtnNext_OnClick" data-loading-text="Next..." class="demo-loading-btn btn green-meadow" Width="150px" runat="server" Text="Next" />
                                </div>
                            </div>
                        </ItemTemplate>
                    </telerik:RadPanelItem>
                </Items>
            </telerik:RadPanelItem>
            <telerik:RadPanelItem BackColor="#5995bb" ForeColor="White" Visible="false" runat="server">
                <Items>
                    <telerik:RadPanelItem runat="server">
                        <ItemTemplate>
                            <div class="skin skin-flat form" style="text-align: left">
                                <div class="portlet light portlet-fit bordered">
                                    <div class="portlet-body">
                                        <div style="text-align: center;">
                                            <h4>
                                                <asp:Label ID="LblPlanContent" runat="server" Text="You can upgrade/downgrade or cancel at anytime" />
                                            </h4>
                                        </div>
                                        <div class="pricing-content-1">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="price-column-container border-active">
                                                        <div class="price-table-head bg-purple">
                                                            <h2 class="no-margin">
                                                                <asp:Label ID="LblFree" Text="Free" runat="server" /></h2>
                                                        </div>
                                                        <div class="arrow-down border-top-purple"></div>
                                                        <div class="price-table-pricing">
                                                            <h3>
                                                                <span class="price-sign">$</span>
                                                                <asp:Label ID="LblFreePrice" runat="server" />
                                                            </h3>
                                                            <p>per month</p>
                                                        </div>
                                                        <div class="price-table-content">
                                                            <div id="divFreeConnections" runat="server" visible="false" class="row mobile-padding">
                                                                <div class="col-xs-3 text-right mobile-padding">
                                                                    <i class="icon-user"></i>
                                                                </div>
                                                                <div class="col-xs-9 text-left mobile-padding">
                                                                    <asp:Label ID="LblFreeConnections" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div class="row mobile-padding">
                                                                <div class="col-xs-3 text-right mobile-padding">
                                                                    <i class="icon-users"></i>
                                                                </div>
                                                                <div class="col-xs-9 text-left mobile-padding">
                                                                    <asp:Label ID="LblFreeManagePartners" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div id="divFreeMessages" runat="server" visible="false" class="row mobile-padding" style="padding-bottom: 10px !important;">
                                                                <div class="col-xs-3 text-right mobile-padding">
                                                                    <i class="fa fa-envelope-o"></i>
                                                                </div>
                                                                <div class="col-xs-9 text-left mobile-padding">
                                                                    <asp:Label ID="LblFreeMessages" runat="server" />
                                                                </div>
                                                            </div>

                                                            <div class="row mobile-padding">
                                                                <div class="col-xs-3 text-right mobile-padding">
                                                                    <i class="icon-drawer"></i>
                                                                </div>
                                                                <div class="col-xs-9 text-left mobile-padding">
                                                                    <asp:Label ID="LblFreeLibraryStorage" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="arrow-down arrow-grey"></div>
                                                        <div class="price-table-footer">
                                                            <a id="aGoPremium" onserverclick="PaymentFreemiumModal_OnClick" visible="false" runat="server" class="btn btn-cta-primary btn-outline price-button sbold uppercase">
                                                                <asp:Label ID="LblGoPremium" runat="server" />
                                                            </a>
                                                            <asp:Button ID="BtnGoPremium" Visible="false" OnClick="BtnSearchGoPremium_OnClick" CssClass="btn grey-salsa btn-outline price-button sbold uppercase" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="price-column-container border-active">
                                                        <div class="price-table-head bg-blue">
                                                            <h2 class="no-margin">
                                                                <asp:Label ID="LblStartup" Text="Start-Up" runat="server" />
                                                            </h2>
                                                        </div>
                                                        <div class="arrow-down border-top-blue"></div>
                                                        <div class="price-table-pricing">
                                                            <h3>
                                                                <span class="price-sign">$</span>
                                                                <asp:Label ID="LblPremiumStartupPrice" runat="server" />
                                                            </h3>
                                                            <p>per month</p>
                                                        </div>
                                                        <div class="price-table-content">
                                                            <div id="divPremiumStartupConnections" runat="server" visible="false" class="row mobile-padding">
                                                                <div class="col-xs-3 text-right mobile-padding">
                                                                    <i class="icon-user"></i>
                                                                </div>
                                                                <div class="col-xs-9 text-left mobile-padding">
                                                                    <asp:Label ID="LblPremiumStartupConnections" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div class="row mobile-padding">
                                                                <div class="col-xs-3 text-right mobile-padding">
                                                                    <i class="icon-users"></i>
                                                                </div>
                                                                <div class="col-xs-9 text-left mobile-paddinLblInfo3Contentg">
                                                                    <asp:Label ID="LblPremiumStartupManagePartners" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div id="divPremiumStartupMessages" runat="server" visible="false" class="row mobile-padding">
                                                                <div class="col-xs-3 text-right mobile-padding">
                                                                    <i class="fa fa-envelope-o"></i>
                                                                </div>
                                                                <div class="col-xs-9 text-left mobile-padding">
                                                                    <asp:Label ID="LblPremiumStartupMessages" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div id="collapse_2_5" class="panel-collapse collapse" style="font-size: 13px; background-color: #f3f6f9; text-align: justify;">
                                                                <div class="panel-body">
                                                                    <asp:Label ID="LblPremiumStartupMessagesContent" Text="On our premium plan we deliver in total 40 connections. 10 accurate connections from resellers that are on our platform and have shown interest in your solution and 30 from third party data that match your partner profile." runat="server" />
                                                                </div>
                                                            </div>
                                                            <div class="row mobile-padding">
                                                                <div class="col-xs-3 text-right mobile-padding">
                                                                    <i class="icon-drawer"></i>
                                                                </div>
                                                                <div class="col-xs-9 text-left mobile-padding">
                                                                    <asp:Label ID="LblPremiumStartupLibraryStorage" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="arrow-down arrow-grey"></div>
                                                        <div class="price-table-footer">
                                                            <a id="aStartupModal" onserverclick="PaymentStartupModal_OnClick" visible="false" runat="server" class="btn btn-cta-primary btn-outline sbold uppercase price-button">
                                                                <asp:Label ID="LblStartUpGoPremium" runat="server" />
                                                            </a>
                                                            <asp:Button ID="BtnStartUpGoPremium" Visible="false" OnClick="BtnSearchGoPremium_OnClick" CssClass="btn grey-salsa btn-outline sbold uppercase price-button" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="price-column-container border-active">
                                                        <div class="price-table-head bg-red">
                                                            <h2 class="no-margin">
                                                                <asp:Label ID="LblGrowthUsers" Text="Growth" runat="server" />
                                                            </h2>
                                                        </div>
                                                        <div class="arrow-down border-top-red"></div>
                                                        <div class="price-table-pricing">
                                                            <h3>
                                                                <span class="price-sign">$</span>
                                                                <asp:Label ID="LblPremiumGrowthPrice" runat="server" />
                                                            </h3>
                                                            <p>per month</p>
                                                            <div class="price-ribbon">Popular</div>
                                                        </div>
                                                        <div class="price-table-content">
                                                            <div id="divPremiumGrowthConnections" runat="server" visible="false" class="row mobile-padding">
                                                                <div class="col-xs-3 text-right mobile-padding">
                                                                    <i class="icon-user"></i>
                                                                </div>
                                                                <div class="col-xs-9 text-left mobile-padding">
                                                                    <asp:Label ID="LblPremiumGrowthConnections" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div class="row mobile-padding">
                                                                <div class="col-xs-3 text-right mobile-padding">
                                                                    <i class="icon-users"></i>
                                                                </div>
                                                                <div class="col-xs-9 text-left mobile-padding">
                                                                    <asp:Label ID="LblPremiumGrowthManagePartners" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div id="divPremiumGrowthMessages" runat="server" visible="false" class="row mobile-padding">
                                                                <div class="col-xs-3 text-right mobile-padding">
                                                                    <i class="fa fa-envelope-o"></i>
                                                                </div>
                                                                <div class="col-xs-9 text-left mobile-padding">
                                                                    <asp:Label ID="LblPremiumGrowthMessages" runat="server" />
                                                                </div>
                                                            </div>
                                                            <div id="collapse_3_1" class="panel-collapse collapse" style="font-size: 13px; background-color: #f3f6f9; text-align: justify;">
                                                                <div class="panel-body">
                                                                    <asp:Label ID="LblPremiumGrowthManagePartnersContent" Text="On our premium plan we deliver in total 40 connections. 10 accurate connections from resellers that are on our platform and have shown interest in your solution and 30 from third party data that match your partner profile." runat="server" />
                                                                </div>
                                                            </div>
                                                            <div class="row mobile-padding">
                                                                <div class="col-xs-3 text-right mobile-padding">
                                                                    <i class="icon-drawer"></i>
                                                                </div>
                                                                <div class="col-xs-9 text-left mobile-padding">
                                                                    <asp:Label ID="LblPremiumGrowthLibraryStorage" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="arrow-down arrow-grey"></div>
                                                        <div class="price-table-footer">
                                                            <a id="aGrowthPaymentModal" onserverclick="PaymentGrowthModal_OnClick" visible="false" runat="server" class="btn btn-cta-primary btn-outline price-button sbold uppercase">
                                                                <asp:Label ID="LblGrowthGoPremium" runat="server" />
                                                            </a>
                                                            <asp:Button ID="BtnGrowthGoPremium" Visible="false" OnClick="BtnSearchGoPremium_OnClick" CssClass="btn grey-salsa btn-outline price-button sbold uppercase" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
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
<!-- Pop Up Invitation form Message (modal view) -->
<div id="PopUpMessageAlert" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <asp:UpdatePanel runat="server" ID="UpdatePanel5">
        <ContentTemplate>
            <div role="document" class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-black no-border">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                        <h4 class="modal-title">
                            <asp:Label ID="LblMessageTitle" Text="Delete Invitation" CssClass="control-label" runat="server" /></h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="LblInvitationMsg" CssClass="control-label" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divFailure" runat="server" visible="false" class="col-md-12 alert alert-danger" style="text-align: center">
                        <strong>
                            <asp:Label ID="LblFailureError" Text="Error! " runat="server" />
                        </strong>
                        <asp:Label ID="LblFailureMsg" runat="server" />
                    </div>
                    <div id="divSuccess" runat="server" visible="false" class="col-md-12 alert alert-success" style="text-align: center;">
                        <strong>
                            <asp:Label ID="LblSuccessDone" Text="Done! " runat="server" />
                        </strong>
                        <asp:Label ID="LblSuccessMsg" runat="server" />
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="BtnClose" OnClick="BtnClose_OnClick" data-dismiss="modal" class="btn btn-danger" runat="server" Text="No" />
                        <asp:Button ID="BtnDennyInvitation" OnClick="BtnDennyInvitation_OnClick" class="btn btn-green" runat="server" Text="Yes" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

