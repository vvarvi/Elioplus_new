<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TeamsGridControl.ascx.cs" Inherits="WdS.ElioPlus.Dashboard.Components.Grid.TeamsGridControl" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageAlertControl.ascx" TagName="MessageAlertControl" TagPrefix="controls" %>

<div class="card-body" style="padding-right: 3px; padding-left: 3px;">
    <asp:UpdatePanel runat="server" ID="UpdatePanelContent" UpdateMode="Conditional">
        <ContentTemplate>
            <!--begin: Datatable-->
            <table class="table table-separate table-head-custom table-checkable" id="kt_datatable">
                <asp:Repeater ID="RdgResults" OnItemDataBound="RdgSendInvitationsConfirmed_OnItemDataBound" OnLoad="RdgSendInvitationsConfirmed_OnNeedDataSource" runat="server">
                    <ItemTemplate>
                        <!--begin::Item-->
                        <div class="card card-custom mb-4">
                            <div id="divRibbonOK" runat="server" visible="false" class="card-header ribbon ribbon-top ribbon-ver">
                                <div class="ribbon-target bg-primary" style="top: -2px; right: 20px;">
                                    OK!
                                </div>
                            </div>
                            <div id="divRibbon" runat="server" visible="false" class="card-header card-header-right ribbon ribbon-clip ribbon-left">
                                <div class="ribbon-target" style="top: 12px;">
                                    <span id="spanRibbon" runat="server" class="ribbon-inner bg-primary"></span>
                                    <asp:Label ID="LblPendingInfo" Text="Not confirmed" runat="server" />
                                </div>
                            </div>
                            <div class="card-body">
                                <div id="div" runat="server" class="d-flex align-items-center mb-10">
                                    <!--begin::Symbol-->
                                    <div id="divImg" runat="server" class="symbol symbol-40 symbol-light-success mr-5">
                                        <span class="symbol-label">
                                            <asp:Image ID="img" runat="server" style="max-width:40px;max-height:40px;" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "personal_image")%>' AlternateText="Team sub account personal image" />
                                        </span>
                                    </div>
                                    <div id="divImgNo" runat="server" visible="false" class="symbol symbol-40 symbol-light-success mr-5 symbol-primary">
                                        <span class="symbol-label font-size-h3 font-weight-boldest">NO</span>
                                    </div>
                                    <!--end::Symbol-->
                                    <!--begin::Text-->
                                    <div class="d-flex flex-column flex-grow-1 font-weight-bold">
                                        <a id="aName" runat="server" class="text-dark text-hover-primary mb-1 font-size-lg">
                                            <asp:Label ID="LblSubAccountName" runat="server" />
                                        </a>
                                        <span class="text-muted">
                                            <%# DataBinder.Eval(Container.DataItem, "team_role_name")%>
                                        </span>
                                    </div>
                                    <!--end::Text-->
                                    <!--begin::Dropdown-->
                                    <div class="dropdown dropdown-inline ml-2">
                                        <a href="#" class="btn btn-hover-light-primary btn-sm btn-icon" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            <i class="ki ki-bold-more-hor"></i>
                                        </a>
                                        <div class="dropdown-menu p-0 m-0 dropdown-menu-md dropdown-menu-right">
                                            <!--begin::Navigation-->
                                            <ul class="navi navi-hover">
                                                <li class="navi-header font-weight-bold py-4">
                                                    <span class="font-size-lg">Edit profile</span>
                                                    <i class="flaticon2-information icon-md text-muted" data-toggle="tooltip" data-placement="right" title="Click to learn more..."></i>
                                                </li>
                                                <li class="navi-separator mb-3 opacity-70"></li>
                                                <li id="liResendInvitation" runat="server" visible="false" class="navi-footer py-4">
                                                    <a id="aResendInvitation" runat="server" role="button" onserverclick="ImgBtnSendEmail_OnClick" class="btn btn-clean font-weight-bold btn-sm">
                                                        <i class="flaticon2-gear"></i>
                                                        Re-send invitation
                                                    </a>
                                                </li>
                                                <li class="navi-footer py-4">
                                                    <a id="aSettings" runat="server" class="btn btn-clean font-weight-bold btn-sm">
                                                        <i class="flaticon2-gear"></i>
                                                        Settings
                                                    </a>
                                                </li>
                                            </ul>
                                            <!--end::Navigation-->
                                        </div>
                                    </div>
                                    <!--end::Dropdown-->
                                    <asp:HiddenField ID="HdnId" Value='<%# Eval("id") %>' runat="server" />
                                    <asp:HiddenField ID="HdnConfirmationUrl" Value='<%# Eval("confirmation_url") %>' runat="server" />
                                    <asp:HiddenField ID="HdnEmail" Value='<%# Eval("email") %>' runat="server" />
                                </div>
                            </div>
                        </div>
                        <!--end::Item-->
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <!--end: Datatable-->

            <controls:MessageAlertControl ID="UcMessageAlert" ShowImg="false" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

