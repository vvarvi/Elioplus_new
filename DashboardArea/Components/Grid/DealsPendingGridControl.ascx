﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DealsPendingGridControl.ascx.cs" Inherits="WdS.ElioPlus.Dashboard.Components.Grid.DealsPendingGridControl" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<div class="card-body">
    <!--begin: Search Form-->
    <!--begin::Search Form-->
    <div class="mb-7">
        <div class="row align-items-center">
            <div class="col-lg-9 col-xl-8">
                <div class="row align-items-center">
                    <div class="col-md-6 my-2 my-md-0">
                        <div class="input-icon">
                            <asp:TextBox ID="TbxPending" placeholder="Name/Email" CssClass="form-control" runat="server" />

                            <span>
                                <i class="flaticon2-search-1 text-muted"></i>
                            </span>
                        </div>
                    </div>
                    <div class="col-md-6 my-2 my-md-0">
                        <div class="d-flex align-items-center">
                            <asp:Button ID="BtnSearchPending" OnClick="BtnSearchPending_Click" runat="server" CssClass="btn btn-light-primary px-6 font-weight-bold" Text="Search" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-xl-4 mt-5 mt-lg-0">
            </div>
        </div>
    </div>
    <!--end::Search Form-->
    <!--end: Search Form-->
    <!--begin: Datatable-->
    <table class="table table-separate table-head-custom table-checkable" id="kt_datatable">
        <thead>
            <tr>
                <th style="width: 200px;">
                    <asp:Label ID="LblConName" runat="server" Text="Partner Name" />
                </th>
                <th id="tdCountry" runat="server" style="width: 240px;">
                    <asp:Label ID="LblCountry" runat="server" Text="Partner Location" />
                </th>
                <th style="width: 100px;">
                    <asp:Label ID="LblClient" runat="server" Text="Client" />
                </th>
                <th id="tdClientEmail" runat="server" style="width: 180px;">
                    <asp:Label ID="LblClientEmail" runat="server" Text="Client Email" />
                </th>
                <th id="tdClientWebsite" runat="server" style="">
                    <asp:Label ID="LblClientWebsite" runat="server" Text="Client Website" />
                </th>
                <th style="width: 100px;">
                    <asp:Label ID="LblAdded" runat="server" Text="Added" />
                </th>
                <th id="tdIsActive" runat="server" style="display: none;">
                    <asp:Label ID="LblIsActive" runat="server" Text="Deal" />
                </th>
                <th id="tdResultStstus" runat="server" style="display: none;">
                    <asp:Label ID="LblResultStstus" runat="server" Text="Result Ststus" />
                </th>
                <th id="tdActions" runat="server" style="width: 50px;">
                    <asp:Label ID="LblActions" Text="Actions" runat="server" />
                </th>
            </tr>
        </thead>
        <asp:Repeater ID="RdgDealsPending" OnItemDataBound="RdgDealsPending_OnItemDataBound" OnLoad="RdgDealsPending_OnNeedDataSource" runat="server">
            <ItemTemplate>
                <tbody>
                    <tr>
                        <td style="">
                            <a id="aCompanyName" runat="server" class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                <div class="symbol symbol-50 symbol-light mr-2" style="float: left;">
                                    <span class="symbol-label">
                                        <asp:Image ID="ImgLogo" runat="server" class="h-50 align-self-center" alt="" />
                                    </span>
                                </div>
                                <asp:Label ID="LblCompanyNameContent" Text='<%# DataBinder.Eval(Container.DataItem, "partner_name")%>' runat="server" />
                            </a>
                            <div id="divNotification" runat="server" visible="false" class="text-right" style="display: none;">
                                <span id="spanNotificationMsg" class="label label-lg label-light-danger label-inline" title="New unread message" runat="server">
                                    <asp:Label ID="LblNotificationMsg" Text="new!" runat="server" />
                                </span>
                                <asp:HiddenField ID="HdnIsNew" Value='<%# DataBinder.Eval(Container.DataItem, "is_new")%>' runat="server" />
                            </div>
                        </td>
                        <td id="tdbCountry" runat="server" style="">
                            <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                <asp:Label ID="LblCountry" Text='<%# DataBinder.Eval(Container.DataItem, "partner_location")%>' runat="server" />
                            </span>
                        </td>
                        <td style="">
                            <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                <asp:Label ID="LblClientName" Text='<%# DataBinder.Eval(Container.DataItem, "company_name")%>' runat="server" />
                            </span>
                            <div id="divClientNotification" runat="server" visible="false" class="media-right">
                                <span id="spanClientNotificationMsg" class="label label-lg label-light-danger label-inline" title="New open deal" runat="server">
                                    <asp:Label ID="LblClientNotificationMsg" Text="new!" runat="server" />
                                </span>
                            </div>
                        </td>
                        <td id="tdbClientEmail" runat="server" style="">
                            <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                <asp:Label ID="LblClientEmail" Text='<%# DataBinder.Eval(Container.DataItem, "email")%>' runat="server" />
                            </span>
                        </td>
                        <td id="tdbWebsite" runat="server" style="">
                            <a id="aWebsite" style="text-decoration: underline !important;" runat="server">
                                <asp:Label ID="LblWebsite" Text='<%# DataBinder.Eval(Container.DataItem, "website")%>' runat="server" />
                            </a>
                        </td>
                        <td style="">
                            <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                <asp:Label ID="Label2" Text='<%# DataBinder.Eval(Container.DataItem, "sysdate")%>' runat="server" />
                            </span>
                        </td>
                        <td id="tdbActiveStatus" runat="server" class="text-right" style="display: none;">
                            <span id="spanActiveStatus" runat="server" class="label label-lg label-light-primary label-inline">
                                <asp:Label ID="LblActiveStatus" Text='<%# DataBinder.Eval(Container.DataItem, "is_active")%>' runat="server" />
                            </span>
                        </td>
                        <td id="tdbResultStatus" runat="server" class="text-right" style="display: none;">
                            <span id="spanResultStatus" runat="server" class="label label-lg label-light-primary label-inline">
                                <asp:Label ID="LblResultStatus" Text='<%# DataBinder.Eval(Container.DataItem, "deal_result")%>' runat="server" />
                            </span>
                        </td>
                        <td id="tdbActions" runat="server" style="width: 100px;">
                            <a id="aEdit" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm mx-3">
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
    <controls:MessageControl ID="UcConnectionsMessageAlert" runat="server" />
</div>
