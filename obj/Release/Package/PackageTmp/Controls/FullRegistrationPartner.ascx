<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FullRegistrationPartner.ascx.cs" Inherits="WdS.ElioPlus.Controls.FullRegistrationPartner" %>

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
            <telerik:RadPanelItem Expanded="True" Selected="true" BackColor="#6ba3c8" BorderColor="#6ba3c8" ForeColor="White" runat="server">
                <Items>
                    <telerik:RadPanelItem>
                        <ItemTemplate>
                            <div class="form-body col-md-12" style="color: #6ba3c8 !important;">
                                <div class="form-group col-md-6">
                                    <div class="col-md-12">
                                        <h3>
                                            <asp:Label ID="Lbl10" runat="server" /></h3>
                                    </div>
                                    <div class="col-md-12">
                                        <h5>
                                            <asp:Label ID="Label9" runat="server" /></h5>
                                    </div>
                                </div>
                                <div class="form-group col-md-6">
                                    <div class="col-md-12">
                                        <h3>
                                            <asp:Label ID="Label2" runat="server" /></h3>
                                    </div>
                                    <div class="col-md-12">
                                        <h5>
                                            <asp:Label ID="Label3" runat="server" /></h5>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 skin skin-flat form-body form-group input-group-large icheck-list" style="padding-left: 38%; color: #6ba3c8 !important;">
                                <div class="col-md-6" style="font-size: 20px">
                                    <asp:RadioButtonList ID="RdBtnUserType" AutoPostBack="true" OnSelectedIndexChanged="RdBtnUserType_OnSelectedIndexChanged" CssClass="icheck" data-radio="iradio_square-red" RepeatDirection="Vertical" runat="server">
                                        <asp:ListItem Value="1" Text="I am a Vendor" />
                                        <asp:ListItem Value="2" Text="I am a Channel Partner" />
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="col-md-12" style="text-align: center; margin-top: -15px; margin-bottom: 20px; color: #6ba3c8 !important;">
                                <strong>
                                    <asp:Label ID="LblTypeInfo" runat="server" Font-Size="14px" ForeColor="#6ba3c8" Text="Please select your company type from the options above!" />
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
                                <div id="divLogoInfo" runat="server" class="col-md-8 alert alert-success" style="margin-left: 17%; margin-right: 17%; font-size: 16px; margin-top: 50px; margin-bottom: 0px; color: #fff !important; background-color: #6ba3c8 !important;">
                                    <strong>
                                        <asp:Label ID="LblLogoInfo" runat="server" Text="Info! " /></strong><asp:Label ID="LblLogoInfoContent" Text="Logo is required(*), must be of type '.png, jpg', maximum size accepted is '100 kb' and ideal dimensions are '300x100 px'. Image is uploaded by clicking 'Next'." runat="server" />
                                </div>
                                <div class="col-md-12" style="text-align: center; margin-left: 17%; margin-right: 17%;">
                                    <asp:UpdatePanel runat="server" ID="LogoUpdatePanel">
                                        <ContentTemplate>
                                            <asp:Panel ID="PnlEditLogo" CssClass="col-md-4" runat="server">
                                                <asp:Label ID="LblLogoUpdateHeader" ForeColor="#9A12B3" Font-Bold="true" runat="server" />
                                                <br />
                                                <div class="form-group" style="margin-top: 10px;">
                                                    <div class="fileinput fileinput-new" data-provides="fileinput">
                                                        <div class="fileinput-new thumbnail" style="width: 300px; height: 100px;">
                                                            <asp:Image ID="ImgPhotoBckgr" ImageUrl="/images/no_logo_company.png" AlternateText="Update logo" runat="server" />
                                                        </div>
                                                        <div class="fileinput-preview fileinput-exists thumbnail hidden" style="max-width: 300px; max-height: 100px;"></div>
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
                                                <div style="margin-top: 32px; max-width: 300px; max-height: 100px; display: flex;">
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
            <telerik:RadPanelItem BackColor="#72ad2d" ForeColor="White" Visible="false" runat="server" Selected="true">
                <Items>
                    <telerik:RadPanelItem runat="server">
                        <ItemTemplate>
                            <div class="skin skin-flat form" style="text-align: left">
                                <asp:Panel ID="PnlIndustry" CssClass="form-body form-group" runat="server">
                                    <div class="portlet light bordered" style="padding-bottom: 0px; margin-bottom: 0px;">
                                        <div class="portlet-title" style="padding: 10px; background: bisque; border-bottom: 1px solid #72AD2D;">
                                            <asp:Label ID="Label7" Font-Size="20px" runat="server" Text="Select Industry" />
                                            <asp:Label ID="Label43" Font-Size="18px" CssClass="pers-info-row-success" Text="  (* You have to select at least one Industry category)" runat="server" />
                                        </div>
                                        <div class="portlet-body" style="padding-top: 0px;">
                                            <div class="icheck-list input-group dash-bar-row-1">
                                                <asp:CheckBoxList ID="cb1" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="icheck-list input-group dash-bar-row-1">
                                                <asp:CheckBoxList ID="cb2" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="icheck-list input-group dash-bar-row-1">
                                                <asp:CheckBoxList ID="cb3" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="row row-fix" style="width: 400px; margin-left: 33%">
                                                <asp:Label ID="LblIndustryError" CssClass="pers-info-row-error" Font-Size="18px" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PnlPartner" CssClass="form-body form-group" runat="server">
                                    <div class="portlet light bordered" style="padding-bottom: 0px; margin-bottom: 0px;">
                                        <div class="portlet-title" style="padding: 10px; background: bisque; border-bottom: 1px solid #72AD2D;">
                                            <asp:Label ID="Label19" Font-Size="20px" runat="server" Text="Partner Program" />
                                            <asp:Label ID="Label46" Font-Size="18px" CssClass="pers-info-row-success" Text="  (* You have to select at least one Partner Program category)" runat="server" />
                                        </div>
                                        <div class="portlet-body" style="padding-top: 0px;">
                                            <div class="icheck-list input-group dash-bar-row-2">
                                                <asp:CheckBoxList ID="cb4" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="icheck-list input-group dash-bar-row-2">
                                                <asp:CheckBoxList ID="cb5" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="icheck-list input-group dash-bar-row-2">
                                                <asp:CheckBoxList ID="cb6" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="row row-fix" style="width: 400px; margin-left: 33%">
                                                <asp:Label ID="LblPartnerError" CssClass="pers-info-row-error" Font-Size="18px" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PnlMarket" CssClass="form-body form-group" runat="server">
                                    <div class="portlet light bordered" style="padding-bottom: 0px; margin-bottom: 0px;">
                                        <div class="portlet-title" style="padding: 10px; background: bisque; border-bottom: 1px solid #72AD2D;">
                                            <asp:Label ID="Label25" Font-Size="20px" runat="server" Text="Market Specialisation" />
                                            <asp:Label ID="Label47" Font-Size="18px" CssClass="pers-info-row-success" Text="  (* You have to select at least one Market Specialisation category)" runat="server" />
                                        </div>
                                        <div class="portlet-body" style="padding-top: 0px;">
                                            <div class="icheck-list input-group dash-bar-row-3">
                                                <asp:CheckBoxList ID="cb7" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="icheck-list input-group dash-bar-row-3" style="width: 300px;">
                                                <asp:CheckBoxList ID="cb8" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                </asp:CheckBoxList>
                                            </div>
                                            <div></div>
                                            <div class="row row-fix" style="width: 400px; margin-left: 33%">
                                                <asp:Label ID="LblMarketError" CssClass="pers-info-row-error" Font-Size="18px" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PnlApi" CssClass="form-body form-group" runat="server">
                                    <div class="portlet light bordered" style="padding-bottom: 0px; margin-bottom: 0px;">
                                        <div class="portlet-title" style="padding: 10px; background: bisque; border-bottom: 1px solid #72AD2D;">
                                            <asp:Label ID="Label28" Font-Size="20px" runat="server" Text="API Category" />
                                            <asp:Label ID="Label48" Font-Size="16px" Visible="false" CssClass="pers-info-row-success" Text="  (* You have to select at least one API category)" runat="server" />
                                        </div>
                                        <div class="portlet-body" style="padding-top: 0px;">
                                            <div class="icheck-list input-group dash-bar-row-4">
                                                <asp:CheckBoxList ID="cb9" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="icheck-list input-group dash-bar-row-4">
                                                <asp:CheckBoxList ID="cb10" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="icheck-list input-group dash-bar-row-4">
                                                <asp:CheckBoxList ID="cb11" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                </asp:CheckBoxList>
                                            </div>
                                            <div class="row row-fix" style="width: 400px; margin-left: 33%">
                                                <asp:Label ID="LblApiError" CssClass="pers-info-row-error" Font-Size="18px" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="form-body col-md-12" style="text-align: center; margin-bottom: 50px;">
                                    <asp:Button ID="BtnClearStep2" Font-Size="18px" OnClick="BtnClearStep2_OnClick" data-loading-text="Clearing..." class="demo-loading-btn btn grey-cascade" Width="150px" runat="server" Text="Clear" />
                                    <asp:Button ID="BtnBackStep2" Font-Size="18px" OnClick="BtnBack_OnClick" data-loading-text="Back..." class="demo-loading-btn btn blue-hoki" Width="150px" runat="server" Text="Back" />
                                    <asp:Button ID="BtnStep2" Font-Size="18px" OnClick="BtnNext_OnClick" data-loading-text="Next..." class="demo-loading-btn btn green-meadow" Width="150px" runat="server" Text="Next" />
                                </div>
                            </div>
                        </ItemTemplate>
                    </telerik:RadPanelItem>
                </Items>
            </telerik:RadPanelItem>
            <telerik:RadPanelItem BackColor="#72ad2d" ForeColor="White" Visible="false" runat="server">
                <Items>
                    <telerik:RadPanelItem runat="server">
                        <ItemTemplate>
                            <div class="skin skin-flat form" style="text-align: left">
                                <asp:Panel ID="PnlSubIndustry" CssClass="form-body form-group" runat="server">
                                    <div class="portlet light bordered">
                                        <div class="portlet-title">
                                            <div class="caption col-md-12" style="padding: 20px; font-size: 20px; background: linen; border-bottom: 1px solid #72AD2D;">
                                                <asp:Label ID="Label1" runat="server" Text="Industry verticals - choose all that apply (maximum 15)" />
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="panel-group accordion scrollable" id="accordion2">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title" style="font-size: 18px; background: bisque; border-bottom: 1px solid #72AD2D;">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_1">
                                                                <asp:Label ID="LabelSub7" runat="server" Text="Sales & Marketing" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_1" class="panel-collapse in">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="input-group" style="display: inline-block; width: 100%;">
                                                                    <div class="icheck-list dash-bar-row-5">
                                                                        <asp:CheckBoxList ID="cbSub1" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-5">
                                                                        <asp:CheckBoxList ID="cbSub2" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-5">
                                                                        <asp:CheckBoxList ID="cbSub3" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title" style="font-size: 18px; background: bisque; border-bottom: 1px solid #72AD2D;">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_2">
                                                                <asp:Label ID="Label15" runat="server" Text="Customer Management" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_2" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="input-group" style="display: inline-block; width: 100%;">
                                                                    <div class="icheck-list dash-bar-row-6">
                                                                        <asp:CheckBoxList ID="cbSub4" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-6">
                                                                        <asp:CheckBoxList ID="cbSub5" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-6">
                                                                        <asp:CheckBoxList ID="cbSub6" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title" style="font-size: 18px; background: bisque; border-bottom: 1px solid #72AD2D;">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_3">
                                                                <asp:Label ID="Label17" runat="server" Text="Project Management" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_3" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="input-group" style="display: inline-block; width: 100%;">
                                                                    <div class="icheck-list dash-bar-row-7">
                                                                        <asp:CheckBoxList ID="cbSub7" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-7">
                                                                        <asp:CheckBoxList ID="cbSub8" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-7">
                                                                        <asp:CheckBoxList ID="cbSub9" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title" style="font-size: 18px; background: bisque; border-bottom: 1px solid #72AD2D;">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_4">
                                                                <asp:Label ID="Label18" runat="server" Text="Operations & Workflow" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_4" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="input-group" style="display: inline-block; width: 100%;">
                                                                    <div class="icheck-list dash-bar-row-8">
                                                                        <asp:CheckBoxList ID="cbSub10" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-8">
                                                                        <asp:CheckBoxList ID="cbSub11" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-8">
                                                                        <asp:CheckBoxList ID="cbSub12" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title" style="font-size: 18px; background: bisque; border-bottom: 1px solid #72AD2D;">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_5">
                                                                <asp:Label ID="Label20" runat="server" Text="Tracking & Measurement" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_5" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="input-group" style="display: inline-block; width: 100%;">
                                                                    <div class="icheck-list dash-bar-row-9">
                                                                        <asp:CheckBoxList ID="cbSub13" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-9">
                                                                        <asp:CheckBoxList ID="cbSub14" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-9">
                                                                        <asp:CheckBoxList ID="cbSub15" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title" style="font-size: 18px; background: bisque; border-bottom: 1px solid #72AD2D;">
                                                            <a class="accordion-toggle" aria-expanded="false" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_6">
                                                                <asp:Label ID="Label21" runat="server" Text="Accounting & Financials" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_6" class="panel-collapse collapse" aria-expanded="false">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="input-group" style="display: inline-block; width: 100%;">
                                                                    <div class="icheck-list dash-bar-row-10">
                                                                        <asp:CheckBoxList ID="cbSub16" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-10">
                                                                        <asp:CheckBoxList ID="cbSub17" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-10">
                                                                        <asp:CheckBoxList ID="cbSub18" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title" style="font-size: 18px; background: bisque; border-bottom: 1px solid #72AD2D;">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_7">
                                                                <asp:Label ID="Label22" runat="server" Text="HR" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_7" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="input-group" style="display: inline-block; width: 100%;">
                                                                    <div class="icheck-list dash-bar-row-11">
                                                                        <asp:CheckBoxList ID="cbSub19" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-11">
                                                                        <asp:CheckBoxList ID="cbSub20" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-11">
                                                                        <asp:CheckBoxList ID="cbSub21" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title" style="font-size: 18px; background: bisque; border-bottom: 1px solid #72AD2D;">
                                                            <a class="accordion-toggle" aria-expanded="false" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_8">
                                                                <asp:Label ID="Label23" runat="server" Text="Web Mobile Software Development" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_8" class="panel-collapse collapse" aria-expanded="false">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="input-group" style="display: inline-block; width: 100%;">
                                                                    <div class="icheck-list dash-bar-row-12">
                                                                        <asp:CheckBoxList ID="cbSub22" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-12">
                                                                        <asp:CheckBoxList ID="cbSub23" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-12">
                                                                        <asp:CheckBoxList ID="cbSub24" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title" style="font-size: 18px; background: bisque; border-bottom: 1px solid #72AD2D;">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_9">
                                                                <asp:Label ID="Label24" runat="server" Text="IT & Infrastructure" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_9" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="input-group" style="display: inline-block; width: 100%;">
                                                                    <div class="icheck-list dash-bar-row-13">
                                                                        <asp:CheckBoxList ID="cbSub25" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-13">
                                                                        <asp:CheckBoxList ID="cbSub26" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-13">
                                                                        <asp:CheckBoxList ID="cbSub27" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title" style="font-size: 18px; background: bisque; border-bottom: 1px solid #72AD2D;">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_10">
                                                                <asp:Label ID="Label4" runat="server" Text="Business Utilities" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_10" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="input-group" style="display: inline-block; width: 100%;">
                                                                    <div class="icheck-list dash-bar-row-14">
                                                                        <asp:CheckBoxList ID="cbSub28" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-14">
                                                                        <asp:CheckBoxList ID="cbSub29" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-14">
                                                                        <asp:CheckBoxList ID="cbSub30" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title" style="font-size: 18px; background: bisque; border-bottom: 1px solid #72AD2D;">
                                                            <a class="accordion-toggle" aria-expanded="false" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_11">
                                                                <asp:Label ID="Label27" runat="server" Text="Data Security & GRC" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_11" class="panel-collapse collapse" aria-expanded="false">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="input-group" style="display: inline-block; width: 100%; padding: 1px !important;">
                                                                    <div class="icheck-list dash-bar-row-15">
                                                                        <asp:CheckBoxList ID="cbSub31" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-15">
                                                                        <asp:CheckBoxList ID="cbSub32" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-15">
                                                                        <asp:CheckBoxList ID="cbSub33" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title" style="font-size: 18px; background: bisque; border-bottom: 1px solid #72AD2D;">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_12">
                                                                <asp:Label ID="Label29" runat="server" Text="Design & Multimedia" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_12" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="input-group" style="display: inline-block; width: 100%;">
                                                                    <div class="icheck-list dash-bar-row-16">
                                                                        <asp:CheckBoxList ID="cbSub34" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-16">
                                                                        <asp:CheckBoxList ID="cbSub35" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-16">
                                                                        <asp:CheckBoxList ID="cbSub36" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title" style="font-size: 18px; background: bisque; border-bottom: 1px solid #72AD2D;">
                                                            <a class="accordion-toggle" aria-expanded="false" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_13">
                                                                <asp:Label ID="Label8" runat="server" Text="Miscellaneous" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_13" class="panel-collapse collapse" aria-expanded="false">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="input-group" style="display: inline-block; width: 100%;">
                                                                    <div class="icheck-list dash-bar-row-3">
                                                                        <asp:CheckBoxList ID="cbSub37" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-3">
                                                                        <asp:CheckBoxList ID="cbSub38" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title" style="font-size: 18px; background: bisque; border-bottom: 1px solid #72AD2D;">
                                                            <a class="accordion-toggle" aria-expanded="false" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_14">
                                                                <asp:Label ID="Label5" runat="server" Text="Unified Communications" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_14" class="panel-collapse collapse" aria-expanded="false">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="input-group" style="display: inline-block; width: 100%;">
                                                                    <div class="icheck-list dash-bar-row-16">
                                                                        <asp:CheckBoxList ID="cbSub39" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-16">
                                                                        <asp:CheckBoxList ID="cbSub40" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-16">
                                                                        <asp:CheckBoxList ID="cbSub41" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title" style="font-size: 18px; background: bisque; border-bottom: 1px solid #72AD2D;">
                                                            <a class="accordion-toggle" aria-expanded="false" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_15">
                                                                <asp:Label ID="Label6" runat="server" Text="CAD & PLM" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_15" class="panel-collapse collapse" aria-expanded="false">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="input-group" style="display: inline-block; width: 100%;">
                                                                    <div class="icheck-list dash-bar-row-16">
                                                                        <asp:CheckBoxList ID="cbSub42" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-16">
                                                                        <asp:CheckBoxList ID="cbSub43" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-16">
                                                                        <asp:CheckBoxList ID="cbSub44" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title" style="font-size: 18px; background: bisque; border-bottom: 1px solid #72AD2D;">
                                                            <a class="accordion-toggle" aria-expanded="false" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_16">
                                                                <asp:Label ID="Label10" runat="server" Text="Hardware" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_16" class="panel-collapse collapse" aria-expanded="false">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="input-group" style="display: inline-block; width: 100%;">
                                                                    <div class="icheck-list dash-bar-row-16">
                                                                        <asp:CheckBoxList ID="cbSub45" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-16">
                                                                        <asp:CheckBoxList ID="cbSub46" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                    <div class="icheck-list dash-bar-row-16">
                                                                        <asp:CheckBoxList ID="cbSub47" CssClass="icheck" data-checkbox="icheckbox_flat-red" runat="server">
                                                                        </asp:CheckBoxList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="row row-fix" style="text-align: center; margin-bottom: 35px; margin-top: 20px;">
                                    <asp:Label ID="LblSubIndustryError" Font-Size="18px" CssClass="pers-info-row-error" runat="server" />
                                </div>
                                <div class="form-body col-md-12" style="text-align: center; margin-bottom: 50px;">
                                    <asp:Button ID="BtnClearStep2" Font-Size="18px" OnClick="BtnClearStep3_OnClick" data-loading-text="Clearing..." class="demo-loading-btn btn grey-cascade" Width="150px" runat="server" Text="Clear" />
                                    <asp:Button ID="BtnBackStep2" Font-Size="18px" OnClick="BtnBack_OnClick" data-loading-text="Back..." class="demo-loading-btn btn blue-hoki" Width="150px" runat="server" Text="Back" />
                                    <asp:Button ID="BtnStep2" Font-Size="18px" OnClick="BtnNext_OnClick" data-loading-text="Next..." class="demo-loading-btn btn green-meadow" Width="150px" runat="server" Text="Next" />
                                </div>
                            </div>
                        </ItemTemplate>
                    </telerik:RadPanelItem>
                </Items>
            </telerik:RadPanelItem>
            <telerik:RadPanelItem BackColor="#72ad2d" Visible="false" runat="server">
                <Items>
                    <telerik:RadPanelItem>
                        <ItemTemplate>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="portlet light form-fit bordered">
                                        <div class="portlet-title">
                                            <div class="caption" style="font-family: Calibri">
                                                <i class=" icon-layers font-green"></i>
                                                <span class="caption-subject font-green bold">
                                                    <asp:Label Font-Size="20px" ID="Label37" runat="server" /></span>
                                            </div>
                                        </div>
                                        <div class="portlet-body form">
                                            <form class="form-horizontal form-bordered">
                                                <div>
                                                    <div>
                                                        <div class="col-md-12" style="margin-bottom: 50px;">
                                                            <asp:TextBox ID="TbxOverview" Font-Size="16px" MaxLength="2500" placeholder="Minimum text required 400 characters, including spaces" CssClass="form-control" TextMode="MultiLine" Rows="10" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <div class="col-md-12 caption caption-subject font-green bold" style="margin-bottom: 20px; margin-left: 15px;">
                                                                <asp:Label Font-Size="18px" ID="Label11" runat="server" />
                                                            </div>
                                                            <asp:TextBox ID="TbxDescription" Font-Size="16px" MaxLength="2500" placeholder="Minimum text required 400 characters, including spaces" CssClass="form-control" TextMode="MultiLine" Rows="10" runat="server"></asp:TextBox>
                                                            <div class="col-md-12 row row-fix" style="text-align: center">
                                                                <asp:Label ID="LblDescriptionError" Font-Size="18px" CssClass="pers-info-row-error" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </form>
                                        </div>
                                        <div class="col-md-12 row row-fix" style="text-align: center">
                                            <asp:Label ID="LblOverViewError" Font-Size="18px" CssClass="pers-info-row-error" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12" style="margin-bottom: 50px; text-align: center;">
                                    <strong>
                                        <asp:Label ID="LblTerms" runat="server" ForeColor="SeaGreen" Font-Size="16px" Text="By clicking Register you agree and accept the Terms and Conditions of our platform!" /></strong>
                                </div>
                            </div>
                            <div class="form-body col-md-12" style="text-align: center; margin-bottom: 50px;">
                                <asp:Button ID="BtnCancel" OnClick="BtnCancel_OnClick" Font-Size="18px" data-loading-text="Cancel..." class="demo-loading-btn btn red-sunglo" Width="150px" runat="server" Text="Cancel" />
                                <asp:Button ID="BtnClearStep4" OnClick="BtnClearStep4_OnClick" Font-Size="18px" data-loading-text="Clearing..." class="demo-loading-btn btn grey-cascade" Width="150px" runat="server" Text="Clear" />
                                <asp:Button ID="BtnBackStep4" OnClick="BtnBack_OnClick" Font-Size="18px" data-loading-text="Back..." class="demo-loading-btn btn blue-hoki" Width="150px" runat="server" Text="Back" />
                                <a id="BtnStep4" href="#MdlSaveData" class="demo-loading-btn btn green-meadow" style="width: 150px; font-size: 18px;" data-toggle="modal">Register</a>
                            </div>
                            <div class="modal fade bs-modal-sm draggable-modal" id="MdlSaveData" tabindex="-1" role="dialog" aria-hidden="true">
                                <div class="modal-dialog modal-sm">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                                            <strong>
                                                <h4 class="modal-title">Save information</h4>
                                            </strong>
                                        </div>
                                        <div class="modal-body" style="font-size: 16px">Save the information you submitted and continue?</div>
                                        <div class="modal-footer" style="text-align: center;">
                                            <asp:Button ID="BtnSaveNo" data-dismiss="modal" Font-Size="18px" class="demo-loading-btn btn grey-cascade" Width="100px" runat="server" Text="No" />
                                            <asp:Button ID="BtnSaveYes" OnClick="BtnNext_OnClick" Font-Size="18px" class="demo-loading-btn btn green-meadow" Width="100px" runat="server" Text="Yes" />
                                        </div>
                                    </div>
                                    <!-- /.modal-content -->
                                </div>
                                <!-- /.modal-dialog -->
                            </div>
                            <!-- /.modal -->
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
