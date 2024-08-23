<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardPartnersCommissionsBillingPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardPartnersCommissionsBillingPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>

    <!--begin::Entry-->
    <div class="d-flex flex-column-fluid">
        <!--begin::Container-->
        <div class="container">
            <!--begin::Dashboard-->
            <asp:UpdatePanel runat="server" ID="UpdatePanelContent" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="divStep0" runat="server" class="card card-custom mb-10">
                        <div class="card-header flex-wrap py-5">
                            <div class="card-title">
                                <h3 class="card-label">
                                    <asp:Label ID="LblAccountSettings" Text="Step 1: Account Settings" runat="server" />
                                </h3>
                            </div>
                            <div class="card-toolbar">
                                <div class="example-preview">
                                    <div class="dropdown dropdown-inline mr-10">
                                        <button type="button" class="btn btn-light-primary btn-icon btn-sm" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            <i class="ki ki-bold-more-hor"></i>
                                        </button>
                                        <div class="dropdown-menu dropdown-menu-sm" style="position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 33px, 0px);" x-placement="bottom-start">
                                            <a id="aCommissionBillingDetails" runat="server" class="dropdown-item">Edit Billing Details</a>
                                            <a id="aCommissionFeesTerms" runat="server" class="dropdown-item">Fees & Terms</a>
                                            <a id="aCommissionPayments" runat="server" class="dropdown-item">Payments</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="form-group row">
                                <div class="col-lg-3"></div>
                                <label class="col-3 col-form-label">Select your payments method</label>
                                <div class="col-3">
                                    <asp:CheckBox ID="CbxMan" CssClass="checkbox-list" runat="server" Text=" Manual" Checked="true" OnCheckedChanged="CbxMan_CheckedChanged" AutoPostBack="true" />
                                    <br />
                                    <asp:CheckBox ID="CbxAut" CssClass="checkbox-list" runat="server" Text=" Automatic" OnCheckedChanged="CbxAut_CheckedChanged" AutoPostBack="true" />

                                    <asp:CheckBoxList ID="Cbxl" Visible="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Cbxl_SelectedIndexChanged" CausesValidation="true" CssClass="checkbox-list">
                                        <asp:ListItem Value="0" Selected="True" Text="Manual" />
                                        <asp:ListItem Value="1" Text="Automatic" />
                                    </asp:CheckBoxList>
                                    <div id="divCbxList" runat="server" visible="false" class="checkbox-list">
                                        <label class="checkbox">
                                            <input id="cbx1" runat="server" type="checkbox" checked="checked">Manual			
                                            <span></span>
                                        </label>
                                        <label class="checkbox">
                                            <input id="cbx2" runat="server" type="checkbox">Automatic
															
                                            <span></span>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-lg-3"></div>
                            </div>
                        </div>
                        <div id="divStep0Footer" runat="server" visible="false" class="card-footer">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div id="div2" runat="server">
                                        <asp:Button ID="Button1" OnClick="BtnClear3_Click" Text="Clear" CssClass="btn btn-light-primary mr-2" runat="server" />
                                        <asp:Button ID="Button2" OnClick="BtnSaveSettings_Click" Text="Save" CssClass="btn btn-success mr-2" runat="server" />
                                    </div>
                                </div>
                                <div class="col-lg-6"></div>
                            </div>
                            <div class="row">
                                <controls:MessageControl ID="MessageControl2" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div id="divStep1" runat="server" class="card card-custom mb-10">
                        <div class="card-header flex-wrap py-5">
                            <div class="card-title">
                                <h3 class="card-label">
                                    <asp:Label ID="LblHeaderInfo1" Text="Step 1: Company Details" runat="server" />
                                </h3>
                                <div id="divAccountStatus" runat="server" class="mr-4 flex-shrink-0 text-center" style="cursor:pointer;">
                                    <i id="iAccountStatus" runat="server" class="icon-2x text-red-50 flaticon2-information"></i>
                                </div>
                                <telerik:RadToolTip ID="RttpAccountStatus" TargetControlID="divAccountStatus" Position="TopRight" ManualClose="false" Animation="Fade" runat="server" />
                            </div>
                            <div id="divToolbarReseller" runat="server" visible="false" class="card-toolbar">
                                <div class="example-preview">
                                    <div class="dropdown dropdown-inline mr-10">
                                        <button type="button" class="btn btn-light-primary btn-icon btn-sm" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            <i class="ki ki-bold-more-hor"></i>
                                        </button>
                                        <div class="dropdown-menu dropdown-menu-sm" style="position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(0px, 33px, 0px);" x-placement="bottom-start">
                                            <a id="aCommissionBillingDetailsReseller" runat="server" class="dropdown-item">Edit Billing Details</a>
                                            <a id="aCommissionFeesTermsReseller" runat="server" class="dropdown-item">Fees & Terms</a>
                                            <a id="aCommissionPaymentsReseller" runat="server" class="dropdown-item">Payments</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div id="divVendorArea" runat="server">
                                <div class="form-group row">
                                    <label class="col-lg-2 col-form-label text-right">First Name:</label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <asp:TextBox ID="TbxFirstName" placeholder="First Name" CssClass="form-control" MaxLength="45" runat="server" />
                                            <div class="input-group-append"><span class="input-group-text"><i class="la la-user"></i></span></div>
                                        </div>
                                        <span class="form-text text-muted"></span>
                                    </div>
                                    <label class="col-lg-2 col-form-label text-right">Last Name:</label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <asp:TextBox ID="TbxLastName" placeholder="Last Name" CssClass="form-control" MaxLength="45" runat="server" />
                                            <div class="input-group-append"><span class="input-group-text"><i class="la la-user"></i></span></div>
                                        </div>
                                        <span class="form-text text-muted"></span>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-lg-2 col-form-label text-right">Company Name:</label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <asp:TextBox ID="TbxOrganiz" placeholder="Company Name" CssClass="form-control" MaxLength="95" runat="server" />
                                            <div class="input-group-append"><span class="input-group-text"><i class="la la-map-marker"></i></span></div>
                                        </div>
                                        <span class="form-text text-muted"></span>
                                    </div>
                                    <label class="col-lg-2 col-form-label text-right">Email:</label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <asp:TextBox ID="TbxEmail" placeholder="Email" CssClass="form-control" MaxLength="95" runat="server" />
                                            <div class="input-group-append"><span class="input-group-text">@</i></span></div>
                                        </div>
                                        <span class="form-text text-muted"></span>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-lg-2 col-form-label text-right">Country:</label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DrpCompanyCountry" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                            <div class="input-group-append"><span class="input-group-text"><i class="la la-user"></i></span></div>
                                        </div>
                                        <span class="form-text text-muted"></span>
                                    </div>
                                    <label class="col-lg-2 col-form-label text-right">City:</label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <asp:TextBox ID="TbxCity" placeholder="City" CssClass="form-control" MaxLength="95" runat="server" />
                                            <div class="input-group-append"><span class="input-group-text"><i class="la la-chain"></i></span></div>
                                        </div>
                                        <span class="form-text text-muted"></span>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-lg-2 col-form-label text-right">State:</label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <asp:TextBox ID="TbxState" placeholder="State" CssClass="form-control" MaxLength="95" runat="server" />
                                            <div class="input-group-append"><span class="input-group-text"><i class="la la-chain"></i></span></div>
                                        </div>
                                        <span class="form-text text-muted"></span>
                                    </div>
                                    <label class="col-lg-2 col-form-label text-right">Phone:</label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <asp:TextBox ID="TbxPhone" placeholder="Phone" CssClass="form-control" MaxLength="95" runat="server" />
                                            <div class="input-group-append"><span class="input-group-text"><i class="la la-chain"></i></span></div>
                                        </div>
                                        <span class="form-text text-muted"></span>
                                    </div>
                                </div>
                            </div>
                            <div id="divResellerArea" runat="server" visible="false">
                                <div class="form-group row">
                                    <div class="col-lg-3"></div>
                                    <label class="col-lg-2 col-form-label text-right">Account Type:</label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DrpAccountType" CssClass="form-control" Enabled="false" runat="server">
                                                <asp:ListItem Value="0" Text="Standard" />
                                                <asp:ListItem Value="1" Text="Express" Selected="True" />
                                                <asp:ListItem Value="2" Text="Custom" />
                                            </asp:DropDownList>
                                            <div class="input-group-append"><span class="input-group-text"><i class="la la-user"></i></span></div>
                                        </div>
                                        <span class="form-text text-muted"></span>
                                    </div>
                                    <div class="col-lg-3"></div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3"></div>
                                    <label class="col-lg-2 col-form-label text-right">Country:</label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DrpAccountCountry" CssClass="form-control" runat="server">
                                            </asp:DropDownList>
                                            <div class="input-group-append"><span class="input-group-text"><i class="la la-user"></i></span></div>
                                        </div>
                                        <span class="form-text text-muted"></span>
                                    </div>
                                    <div class="col-lg-3"></div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3"></div>
                                    <label class="col-lg-2 col-form-label text-right">Business Type:</label>
                                    <div class="col-lg-3">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DrpBusinessType" Width="100%" CssClass="form-control" Enabled="false" runat="server">
                                                <asp:ListItem Value="0" Text="Company" Selected="True" />
                                                <asp:ListItem Value="1" Text="Individual" />
                                            </asp:DropDownList>
                                            <div class="input-group-append"><span class="input-group-text"><i class="la la-user"></i></span></div>
                                        </div>
                                        <span class="form-text text-muted"></span>
                                    </div>
                                    <div class="col-lg-3"></div>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div id="divVendorActions" runat="server">
                                        <asp:Button ID="BtnClear" OnClick="BtnClear_OnClick" Text="Clear" CssClass="btn btn-light-primary mr-2" runat="server" />
                                        <asp:Button ID="BtnSave" OnClick="BtnSave_OnClick" Text="Save" CssClass="btn btn-success mr-2" runat="server" />
                                    </div>
                                    <div id="divResellerActions" runat="server" visible="false">
                                        <asp:Button ID="BtnCancelStripeAccount" OnClick="BtnCancelStripeAccount_Click" Text="Cancel" CssClass="btn btn-light-primary mr-2" runat="server" />
                                        <asp:Button ID="BtnCreateStripeAccount" OnClick="BtnCreateStripeAccount_Click" Text="Create" CssClass="btn btn-success mr-2" runat="server" />
                                        <asp:Button ID="BtnConfigureAccountOnboarding" Visible="false" OnClick="aConfigureAccountOnboarding_ServerClick" Text="Configure" CssClass="btn btn-primary mr-2" runat="server" />
                                    </div>
                                </div>
                                <div class="col-lg-6"></div>
                            </div>
                            <div class="row">
                                <controls:MessageControl ID="UcMessageAlert" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div id="divStep2" runat="server" class="card card-custom mb-10">
                        <div class="card-header flex-wrap py-5">
                            <div class="card-title">
                                <h3 class="card-label">
                                    <asp:Label ID="LblHeaderInfo2" Text="Step 2: Billing Details" runat="server" />
                                </h3>
                                <div id="divPaymentMethods" runat="server" class="mr-4 flex-shrink-0 text-center" style="cursor:pointer;">
                                    <i id="iPaymentMethods" runat="server" class="icon-2x text-dark-50 flaticon2-information"></i>
                                </div>
                                <telerik:RadToolTip ID="RttpPaymentMethods" TargetControlID="divPaymentMethods" Position="TopRight" ManualClose="false" Animation="Fade" runat="server" />
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="col-3" style="margin-bottom: 30px;">
                                <ul class="nav nav-tabs nav-bold nav-tabs-line">
                                    <li class="nav-item">
                                        <a id="aCardData" runat="server" onserverclick="aCardData_ServerClick" class="nav-link active">
                                            <span class="nav-icon"><i class="flaticon-customer"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblCardData" Text="Credit Card" runat="server" />
                                            </span>
                                        </a>
                                    </li>
                                    <li class="nav-item">
                                        <a id="aBankData" runat="server" onserverclick="aBankData_ServerClick" class="nav-link">
                                            <span class="nav-icon"><i class="flaticon-customer"></i></span>
                                            <span class="nav-text">
                                                <asp:Label ID="LblBankData" Text="Bank Account" runat="server" />
                                            </span>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                            <div class="col-3" style="margin-bottom: 30px;"></div>
                            <div class="col-3" style="margin-bottom: 30px;"></div>
                            <div class="col-12">
                                <div class="tab-content">
                                    <div class="tab-pane fade show active" id="tab_1_1" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                        <div class="form-group row">
                                            <div class="col-lg-3"></div>
                                            <label class="col-lg-2 col-form-label text-right">Credit Card Number:</label>
                                            <div class="col-lg-3">
                                                <div class="input-group">
                                                    <asp:TextBox ID="TbxCreditCardNumber" placeholder="xxxx-xxxx-xxxx-xxxx" CssClass="form-control" MaxLength="16" TextMode="Number" runat="server" />
                                                    <div class="input-group-append"><span class="input-group-text"><i class="la la-user"></i></span></div>
                                                </div>
                                                <span class="form-text text-muted"></span>
                                            </div>
                                            <div class="col-lg-3"></div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3"></div>
                                            <label class="col-lg-2 col-form-label text-right">Expiration Month/Year:</label>
                                            <div class="col-lg-3">
                                                <div class="input-group">
                                                    <asp:TextBox ID="TbxCreditCardExpirationDate" CssClass="form-control" MaxLength="2" TextMode="Month" runat="server" />
                                                    <div class="input-group-append"><span class="input-group-text"><i class="la la-user"></i></span></div>
                                                </div>
                                                <span class="form-text text-muted"></span>
                                            </div>
                                            <div class="col-lg-3"></div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3"></div>
                                            <label class="col-lg-2 col-form-label text-right">Card Verification Value:</label>
                                            <div class="col-lg-3">
                                                <div class="input-group">
                                                    <asp:TextBox ID="TbxCreditCardCVC" placeholder="CVC" CssClass="form-control" MaxLength="4" runat="server" />
                                                    <div class="input-group-append"><span class="input-group-text"><i class="la la-user"></i></span></div>
                                                </div>
                                                <span class="form-text text-muted"></span>
                                            </div>
                                            <div class="col-lg-3"></div>
                                        </div>
                                    </div>
                                    <div class="tab-pane fade" id="tab_1_2" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                        <div class="form-group row">
                                            <div class="col-lg-3"></div>
                                            <label class="col-lg-2 col-form-label text-right">Bank Name:</label>
                                            <div class="col-lg-3">
                                                <div class="input-group">
                                                    <asp:TextBox ID="TbxBankName" placeholder="Bank Name" CssClass="form-control" MaxLength="50" runat="server" />
                                                    <div class="input-group-append"><span class="input-group-text"><i class="la la-user"></i></span></div>
                                                </div>
                                                <span class="form-text text-muted"></span>
                                            </div>
                                            <div class="col-lg-3"></div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3"></div>
                                            <label class="col-lg-2 col-form-label text-right">Bank Account Number/IBAN:</label>
                                            <div class="col-lg-3">
                                                <div class="input-group">
                                                    <asp:TextBox ID="TbxBankAccountNumber" placeholder="IBAN" CssClass="form-control" MaxLength="50" runat="server" />
                                                    <div class="input-group-append"><span class="input-group-text"><i class="la la-user"></i></span></div>
                                                </div>
                                                <span class="form-text text-muted"></span>
                                            </div>
                                            <div class="col-lg-3"></div>
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-lg-3"></div>
                                            <label class="col-lg-2 col-form-label text-right">Country:</label>
                                            <div class="col-lg-3">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="DrpBankCountry" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                    <div class="input-group-append"><span class="input-group-text"><i class="la la-user"></i></span></div>
                                                </div>
                                                <span class="form-text text-muted"></span>
                                            </div>
                                            <div class="col-lg-3"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div id="divVendorActions2" runat="server">
                                        <asp:Button ID="BtnClear2" OnClick="BtnClear2_Click" Text="Clear" CssClass="btn btn-light-primary mr-2" runat="server" />
                                        <asp:Button ID="BtnSaveBillingDetails" OnClick="BtnSaveBillingDetails_Click" Text="Save" CssClass="btn btn-success mr-2" runat="server" />
                                    </div>
                                </div>
                                <div class="col-lg-6"></div>
                            </div>
                            <div class="row">
                                <controls:MessageControl ID="UcMessageAlertBilling" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div id="divStep3" runat="server" class="card card-custom mb-10">
                        <div class="card-header flex-wrap py-5">
                            <div class="card-title">
                                <h3 class="card-label">
                                    <asp:Label ID="LblBillingSettings" Text="Step 3: Billing Settings" runat="server" />
                                </h3>
                            </div>
                        </div>
                        <div class="card-body">
                            <asp:Panel ID="PnlNotifications" runat="server">
                                <div class="form-group row">
                                    <div class="col-lg-3"></div>
                                    <label class="col-lg-2 col-form-label text-right">Payment after:</label>
                                    <div class="col-lg-2">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DrpDaysAfter" CssClass="form-control" runat="server">
                                                
                                            </asp:DropDownList>
                                            <div class="input-group-append"><span class="input-group-text"><i class="la la-user"></i></span></div>
                                        </div>
                                        <span class="form-text text-muted"></span>

                                    </div>
                                    <label class="col-lg-1 col-form-label text-right">days</label>
                                    <div class="col-lg-4"></div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3"></div>
                                    <label class="col-lg-2 col-form-label text-right">First notification before:</label>
                                    <div class="col-lg-2">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DrpFirstNot" CssClass="form-control" runat="server">
                                                
                                            </asp:DropDownList>
                                            <div class="input-group-append"><span class="input-group-text"><i class="la la-user"></i></span></div>
                                        </div>
                                        <span class="form-text text-muted"></span>

                                    </div>
                                    <label class="col-lg-1 col-form-label text-right">days</label>
                                    <div class="col-lg-4"></div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-lg-3"></div>
                                    <label class="col-lg-2 col-form-label text-right">Second notification before:</label>
                                    <div class="col-lg-2">
                                        <div class="input-group">
                                            <asp:DropDownList ID="DrpSecondNot" CssClass="form-control" runat="server">
                                                
                                            </asp:DropDownList>
                                            <div class="input-group-append"><span class="input-group-text"><i class="la la-user"></i></span></div>
                                        </div>
                                        <span class="form-text text-muted"></span>

                                    </div>
                                    <label class="col-lg-1 col-form-label text-right">days</label>
                                    <div class="col-lg-4"></div>
                                </div>
                            </asp:Panel>
                            <div class="form-group row">
                                <div class="col-lg-3"></div>
                                <label class="col-lg-2 col-form-label text-right">Disable notifications</label>
                                <div class="col-2">
                                    <div class="input-group">
                                        <asp:CheckBox ID="CbxDisableNotif" Style="margin-top: 10px !important;" CssClass="checkbox-list" runat="server" Text=" Yes" OnCheckedChanged="CbxDisableNotif_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                </div>
                                <div class="col-lg-4"></div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div id="div4" runat="server">
                                        <asp:Button ID="BtnClear3" OnClick="BtnClear3_Click" Text="Clear" CssClass="btn btn-light-primary mr-2" runat="server" />
                                        <asp:Button ID="BtnSaveSettings" OnClick="BtnSaveSettings_Click" Text="Save" CssClass="btn btn-success mr-2" runat="server" />
                                    </div>
                                </div>
                                <div class="col-lg-6"></div>
                            </div>
                            <div class="row">
                                <controls:MessageControl ID="UcMessageControlSettingsAllert" runat="server" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!--end::Dashboard-->
        </div>
        <!--end::Container-->
    </div>
    <!--end::Entry-->

    <!-- Pop Up Invitation form Message (modal view) -->
    <div id="PopUpMessageAlert" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel5">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="LblFileUploadTitle" CssClass="control-label" runat="server" /></h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <i aria-hidden="true" class="ki ki-close"></i>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group" style="width: 100%;">
                                    <controls:MessageControl ID="UploadMessageAlert" Visible="false" runat="server" />
                                    <asp:Label ID="LblFileUploadfMsg" Visible="false" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <script type="text/javascript">

            function OpenConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('show');
            }

            function CloseConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('hide');
            }

            function OpenLeadsServiceModal() {
                $('#LeadsServiceModal').modal('show');
            }

            function CloseLeadsServiceModal() {
                $('#LeadsServiceModal').modal('hide');
            }
        </script>

    </telerik:RadScriptBlock>

</asp:Content>
