<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PartnersGridControl.ascx.cs" Inherits="WdS.ElioPlus.Dashboard.Components.Grid.PartnersGridControl" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Collaboration/CreateNewInvitationToConnectionsMessage.ascx" TagName="UcCreateNewInvitationToConnectionsMessage" TagPrefix="controls" %>

<div class="card-body">
    <asp:UpdatePanel runat="server" ID="UpdatePanelContent" UpdateMode="Conditional">
        <ContentTemplate>
            
            <!--begin::Search Form-->
            <div class="mb-7">
                <div id="divSearchAreaConfirmed" runat="server" class="row align-items-center">
                    <div class="col-lg-10 col-xl-8">
                        <div class="row align-items-center">                            
                            <div class="col-md-6 my-2 my-md-0">
                                <div class="d-flex align-items-center">                                    
                                    <asp:DropDownList ID="DdlCountriesConfirmed" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6 my-2 my-md-0">
                                <div class="d-flex align-items-center">
                                    <asp:TextBox ID="RtbxCompanyNameEmailConfirmed" CssClass="form-control" placeholder="Name/Email" runat="server" />

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2 col-xl-4 mt-5 mt-lg-0">
                        <asp:Button ID="BtnSearchConfirmed" OnClick="BtnSearchConfirmed_Click" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="Search" />

                    </div>
                </div>
            </div>
            <!--end: Search Form-->
            <!--begin: Datatable-->
            <table class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                <thead>
                    <tr>
                        <th style="width: 220px;">
                            <asp:Label ID="LblConName" runat="server" Text="Partner Name" />
                        </th>
                        <th id="tdCountry" runat="server" style="width: 110px;">
                            <asp:Label ID="LblCountry" runat="server" Text="Country" />
                        </th>
                        <th style="width: 180px;">
                            <asp:Label ID="LblAdded" runat="server" Text="Registration Date" />
                        </th>
                        <th id="tdIsActive" runat="server" style="width: 130px;">
                            <asp:Label ID="LblTierStatus" runat="server" Text="Tier Status" />
                        </th>
                        <th id="tdStatus" runat="server" style="width: 75px;">
                            <asp:Label ID="LblStatus" runat="server" Text="Status" />
                        </th>
                        <th id="tdActions" runat="server" style="width: 50px;">
                            <asp:Label ID="LblActions" Text="Actions" runat="server" />
                        </th>
                    </tr>
                </thead>
                <asp:Repeater ID="RdgSendInvitationsConfirmed" OnItemDataBound="RdgSendInvitationsConfirmed_OnItemDataBound" OnLoad="RdgSendInvitationsConfirmed_OnNeedDataSource" runat="server">
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td style="">
                                    <a id="aCompanyName" runat="server" class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                        <div class="symbol symbol-50 symbol-light mr-2" style="float: left; padding-right: 15px;">
                                            <span class="symbol-label">
                                                <asp:Image ID="ImgCompanyLogo" runat="server" class="h-50 align-self-center" alt="" />
                                            </span>
                                        </div>
                                        <asp:Label ID="LblCompanyName" Text='<%# DataBinder.Eval(Container.DataItem, "company_name")%>' runat="server" />
                                    </a>
                                    <div id="divNotification" runat="server" visible="false" class="text-right" style="display: none;">
                                        <span id="spanNotificationMsg" class="label label-lg label-light-danger label-inline" title="New unread message" runat="server">
                                            <asp:Label ID="LblNotificationMsg" Text="new!" runat="server" />
                                        </span>
                                    </div>
                                </td>
                                <td style="">
                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                        <asp:Label ID="Label1" Text='<%# DataBinder.Eval(Container.DataItem, "country")%>' runat="server" />
                                    </span>
                                </td>
                                <td style="">
                                    <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                        <asp:Label ID="Label2" Text='<%# DataBinder.Eval(Container.DataItem, "sysdate")%>' runat="server" />
                                    </span>
                                </td>
                                <td style="">
                                    <a id="aTierStatus" runat="server">
                                        <asp:Label ID="LblTierStatus" Text='<%# DataBinder.Eval(Container.DataItem, "tier_status")%>' runat="server" />
                                    </a>
                                    <div id="divEdit" runat="server">
                                        <asp:Label ID="Label3" runat="server" />
                                        <a id="aEditTierStatus" runat="server" onserverclick="aEditTierStatus_OnClick">
                                            <i class="fa fa-edit"></i>
                                        </a>
                                        <asp:ImageButton ID="ImgBtnEditTierStatus" Visible="false" ImageUrl="~../../../Images/edit.png" runat="server" />
                                    </div>
                                    <div id="divSave" visible="false" runat="server">
                                        <telerik:RadComboBox ID="RcbxTierStatus" Width="85" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Value="0" Text="Select" runat="server" />
                                                <telerik:RadComboBoxItem Value="1" Text="Silver" runat="server" />
                                                <telerik:RadComboBoxItem Value="2" Text="Gold" runat="server" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <a id="aSaveTierStatus" runat="server" onserverclick="aSaveTierStatus_OnClick">
                                            <i class="fa fa-save"></i>
                                        </a>
                                        <a id="aCancelTierStatus" runat="server" onserverclick="aCancelTierStatus_OnClick">
                                            <i class="fa fa-remove"></i>
                                        </a>
                                        <asp:ImageButton ID="ImgBtnTierSave" Visible="false" ImageUrl="~../../../Images/save.png" runat="server" />
                                    </div>
                                </td>

                                <td class="text-right" style="">
                                    <span id="spanInvStatus" runat="server" class="label label-lg label-light-primary label-inline">
                                        <asp:Label ID="LblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "invitation_status")%>' runat="server" />
                                    </span>
                                </td>
                                <td style="width: 100px;">
                                    <a id="aCollaborationRoomConfirmed" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm mx-3">
                                        <span class="svg-icon svg-icon-md svg-icon-primary">
                                            <!--begin::Svg Icon | path:assets/media/svg/icons/Communication/Write.svg-->
                                            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="24px" height="24px" viewBox="0 0 24 24" version="1.1">
                                                <g stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                                                    <rect x="0" y="0" width="24" height="24" />
                                                    <path d="M12.2674799,18.2323597 L12.0084872,5.45852451 C12.0004303,5.06114792 12.1504154,4.6768183 12.4255037,4.38993949 L15.0030167,1.70195304 L17.5910752,4.40093695 C17.8599071,4.6812911 18.0095067,5.05499603 18.0083938,5.44341307 L17.9718262,18.2062508 C17.9694575,19.0329966 17.2985816,19.701953 16.4718324,19.701953 L13.7671717,19.701953 C12.9505952,19.701953 12.2840328,19.0487684 12.2674799,18.2323597 Z" fill="#000000" fill-rule="nonzero" transform="translate(14.701953, 10.701953) rotate(-135.000000) translate(-14.701953, -10.701953)" />
                                                    <path d="M12.9,2 C13.4522847,2 13.9,2.44771525 13.9,3 C13.9,3.55228475 13.4522847,4 12.9,4 L6,4 C4.8954305,4 4,4.8954305 4,6 L4,18 C4,19.1045695 4.8954305,20 6,20 L18,20 C19.1045695,20 20,19.1045695 20,18 L20,13 C20,12.4477153 20.4477153,12 21,12 C21.5522847,12 22,12.4477153 22,13 L22,18 C22,20.209139 20.209139,22 18,22 L6,22 C3.790861,22 2,20.209139 2,18 L2,6 C2,3.790861 3.790861,2 6,2 L12.9,2 Z" fill="#000000" fill-rule="nonzero" opacity="0.3" />
                                                </g>
                                            </svg>
                                            <!--end::Svg Icon-->
                                        </span>
                                    </a>
                                </td>
                            </tr>
                        </tbody>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <!--end: Datatable-->
            <controls:MessageControl ID="UcMessageAlert" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

<!-- Invitation form (modal view) -->
<div id="SendInvitationModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
    aria-hidden="true">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div role="document" class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-black no-border">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                        <h4 class="modal-title">
                            <asp:Label ID="LblInvitationSendTitle" Text="Invitation Send" CssClass="control-label" runat="server" /></h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="LblSuccessfullSendfMsg" CssClass="control-label" runat="server" />
                                </div>
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
<!-- Invitation form (modal view) -->
<div id="CreateInvitationModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
    aria-hidden="true">
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <controls:UcCreateNewInvitationToConnectionsMessage ID="UcCreateNewInvitationToConnectionsMessage"
                runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<!-- Confirmation Delete Partner form (modal view) -->
<div id="divConfirm" class="modal fade" tabindex="-1" data-width="300">
    <asp:UpdatePanel runat="server" ID="UpdatePanel4">
        <ContentTemplate>
            <div role="document" class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-black no-border">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                        <h4 class="modal-title">
                            <asp:Label ID="LblConfTitle" Text="Confirmation" CssClass="control-label" runat="server" /></h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="LblConfMsg" Text="Are you sure you want to delete this partner from your list?" CssClass="control-label" runat="server" />
                                    <asp:HiddenField ID="HdnVendorResellerCollaborationId" Value="0" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divFailure" runat="server" visible="false" class="col-md-12 alert alert-danger" style="text-align: center">
                        <strong>
                            <asp:Label ID="LblFailure" Text="Error! " runat="server" />
                        </strong>
                        <asp:Label ID="LblFailureMsg" runat="server" />
                    </div>
                    <div id="divSuccess" runat="server" visible="false" class="col-md-12 alert alert-success" style="text-align: center;">
                        <strong>
                            <asp:Label ID="LblSuccess" Text="Done! " runat="server" />
                        </strong>
                        <asp:Label ID="LblSuccessMsg" runat="server" />
                    </div>
                    <div class="modal-footer">
                        <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                        <asp:Button ID="BtnDelete" OnClick="BtnDelete_OnClick" Text="Delete" CssClass="btn red-sunglo" runat="server" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

