<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LibraryViewSendControl.ascx.cs" Inherits="WdS.ElioPlus.Controls.Collaboration.Library.LibraryViewSendControl" %>

<%@ Register Src="~/Controls/AlertControls/MessageAlertControl.ascx" TagName="MessageAlertControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/AlertControls/NotificationControl.ascx" TagName="NotificationControl" TagPrefix="controls" %>

<!--begin::Entry-->
<div class="d-flex flex-column-fluid" style="min-width:800px !important;">
    <!--begin::Container-->
    <div class="container">
        <!--begin::Dashboard-->
        <asp:UpdatePanel runat="server" ID="UpdatePanelContent" UpdateMode="Conditional">
            <ContentTemplate>
                <!--begin::Todo Files-->
                <div class="d-flex flex-row">                    
                    <!--begin::List-->
                    <div class="flex-row-fluid d-flex flex-column lg-12">
                        <div class="d-flex flex-column flex-grow-1" style="">
                            <!--begin::Head-->
                            <div class="card card-custom gutter-b">
                                <!--begin::Body-->
                                <div class="card-body d-flex align-items-center justify-content-between flex-wrap py-3" style="padding: 2rem 1.75rem !important;">
                                    <!--begin::Info-->
                                    <div class="d-flex align-items-center mr-2 py-2">
                                        <!--begin::Navigation-->
                                        <div class="d-flex mr-0">
                                            <!--begin::Navi-->
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                                                <ContentTemplate>
                                                    <div id="divVendorsUploadFilesArea" runat="server" class="navi navi-hover navi-active navi-link-rounded navi-bold d-flex flex-row">
                                                        <!--begin::Item-->
                                                        <div class="navi-item mr-4 mt-4">
                                                            <asp:Label ID="LblCategoryTitle" Text="Select category: " runat="server" />
                                                        </div>
                                                        <!--end::Item--> 
                                                        <!--begin::Item-->
                                                        <div class="navi-item mr-4">
                                                            <asp:DropDownList ID="Ddlcategory" AutoPostBack="true" OnSelectedIndexChanged="Ddlcategory_SelectedIndexChanged" Width="230" CssClass="form-control" runat="server" />
                                                        </div>
                                                        <!--end::Item-->                                                       
                                                        <!--begin::Item-->
                                                        <div class="navi-item mr-2">
                                                            <a id="BtnSelectFiles" role="button" visible="false" style="width: 210px;" runat="server" title="Select files" class="btn btn-light-primary px-6 font-weight-bold">Select
                                                            </a>
                                                        </div>
                                                        <!--end::Item-->
                                                    </div>
                                                </ContentTemplate>                                                
                                            </asp:UpdatePanel>
                                            <!--end::Navi-->
                                        </div>
                                        <!--end::Navigation-->
                                    </div>
                                    <!--end::Info-->
                                </div>
                                <!--end::Body-->
                            </div>
                            <!--end::Head-->
                            <controls:MessageAlertControl ID="UcMessageAlert" runat="server" />

                            <div class="tab-content">
                                <div class="tab-pane fade" id="tab_0" runat="server" visible="false" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                    <controls:MessageControl ID="UcNoPartnersMessageControl" runat="server" />
                                    <div class="col-xl-12">
                                        <div class="row">
                                            <div id="divInvitationToPartners" runat="server">
                                                <a id="aInvitationToPartners" runat="server" class="btn btn-primary" style="color: #fff;">
                                                    <i class="ion-plus-round mr-5"></i>
                                                    <asp:Label ID="LblInvitationToPartners" Text="Invite your technology partners" runat="server" />
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade show active" id="tab_1_1" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                    <asp:Repeater ID="Rdg1" OnLoad="Rdg1_Load" OnItemDataBound="Rdg1_ItemDataBound" runat="server">
                                        <ItemTemplate>
                                            <!--begin::Row-->
                                            <div class="row">
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div1" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif1" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete1" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete1" onserverclick="aDelete1_OnClick" visible="false" role="button" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm">
                                                                    </a>                                                                    
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server">
                                                                    <img id="img1" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type1")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->                                                                
                                                                <a id="a1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl1" Text='<%# DataBinder.Eval(Container.DataItem, "file_title1")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx1" AutoPostBack="true" OnCheckedChanged="Cbx1_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId1" Value='<%# Eval("id1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId1" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId1" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId1" Value='<%# Eval("file_type_id1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName1" Value='<%# Eval("file_name1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath1" Value='<%# Eval("file_path1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew1" Value='<%# Eval("is_new1") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div2" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif2" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete2" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete2" onserverclick="aDelete2_OnClick" visible="false" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile2" onserverclick="HpLnkFile2_ServerClick" role="button" runat="server">
                                                                    <img id="img2" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type2")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a2" runat="server" onserverclick="HpLnkFile2_ServerClick" role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl2" Text='<%# DataBinder.Eval(Container.DataItem, "file_title2")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx2" AutoPostBack="true" OnCheckedChanged="Cbx2_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId2" Value='<%# Eval("id2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId2" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId2" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId2" Value='<%# Eval("file_type_id2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName2" Value='<%# Eval("file_name2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath2" Value='<%# Eval("file_path2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew2" Value='<%# Eval("is_new2") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div3" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif3" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete3" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete3" onserverclick="aDelete3_OnClick" visible="false" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile3" onserverclick="HpLnkFile3_ServerClick" role="button" runat="server">
                                                                    <img id="img3" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type3")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a3" runat="server" onserverclick="HpLnkFile3_ServerClick" role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl3" Text='<%# DataBinder.Eval(Container.DataItem, "file_title3")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx3" AutoPostBack="true" OnCheckedChanged="Cbx3_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId3" Value='<%# Eval("id3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId3" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId3" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId3" Value='<%# Eval("file_type_id3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName3" Value='<%# Eval("file_name3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath3" Value='<%# Eval("file_path3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew3" Value='<%# Eval("is_new3") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div4" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif4" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete4" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete4" onserverclick="aDelete4_OnClick" visible="false" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile4" onserverclick="HpLnkFile4_ServerClick" role="button" runat="server">
                                                                    <asp:Image ID="img4" runat="server" AlternateText="" class="max-h-65px" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "file_type4")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a4" runat="server" onserverclick="HpLnkFile4_ServerClick" role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl4" Text='<%# DataBinder.Eval(Container.DataItem, "file_title4")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx4" AutoPostBack="true" OnCheckedChanged="Cbx4_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId4" Value='<%# Eval("id4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId4" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId4" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId4" Value='<%# Eval("file_type_id4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName4" Value='<%# Eval("file_name4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath4" Value='<%# Eval("file_path4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew4" Value='<%# Eval("is_new4") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                            </div>
                                            <!--end::Row-->
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <controls:MessageControl ID="UcRgd1" runat="server" />
                                </div>
                                <div class="tab-pane fade" id="tab_1_2" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                    <asp:Repeater ID="Rdg2" OnLoad="Rdg2_Load" OnItemDataBound="Rdg2_ItemDataBound" runat="server">
                                        <ItemTemplate>
                                            <!--begin::Row-->
                                            <div class="row">
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div1" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif1" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete1" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete1" onserverclick="aDelete1_OnClick" visible="false" role="button" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server">
                                                                    <img id="img1" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type1")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl1" Text='<%# DataBinder.Eval(Container.DataItem, "file_title1")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx1" AutoPostBack="true" OnCheckedChanged="Cbx1_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId1" Value='<%# Eval("id1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId1" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId1" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId1" Value='<%# Eval("file_type_id1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName1" Value='<%# Eval("file_name1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath1" Value='<%# Eval("file_path1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew1" Value='<%# Eval("is_new1") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div2" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif2" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete2" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete2" onserverclick="aDelete2_OnClick" visible="false" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile2" onserverclick="HpLnkFile2_ServerClick" role="button" runat="server">
                                                                    <img id="img2" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type2")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a2" runat="server" onserverclick="HpLnkFile2_ServerClick" role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl2" Text='<%# DataBinder.Eval(Container.DataItem, "file_title2")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx2" AutoPostBack="true" OnCheckedChanged="Cbx2_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId2" Value='<%# Eval("id2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId2" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId2" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId2" Value='<%# Eval("file_type_id2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName2" Value='<%# Eval("file_name2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath2" Value='<%# Eval("file_path2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew2" Value='<%# Eval("is_new2") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div3" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif3" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete3" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete3" onserverclick="aDelete3_OnClick" visible="false" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile3" onserverclick="HpLnkFile3_ServerClick" role="button" runat="server">
                                                                    <img id="img3" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type3")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a3" runat="server" onserverclick="HpLnkFile3_ServerClick" role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl3" Text='<%# DataBinder.Eval(Container.DataItem, "file_title3")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx3" AutoPostBack="true" OnCheckedChanged="Cbx3_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId3" Value='<%# Eval("id3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId3" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId3" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId3" Value='<%# Eval("file_type_id3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName3" Value='<%# Eval("file_name3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath3" Value='<%# Eval("file_path3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew3" Value='<%# Eval("is_new3") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div4" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif4" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete4" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete4" onserverclick="aDelete4_OnClick" visible="false" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile4" onserverclick="HpLnkFile4_ServerClick" role="button" runat="server">
                                                                    <asp:Image ID="img4" runat="server" AlternateText="" class="max-h-65px" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "file_type4")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a4" runat="server" onserverclick="HpLnkFile4_ServerClick" role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl4" Text='<%# DataBinder.Eval(Container.DataItem, "file_title4")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx4" AutoPostBack="true" OnCheckedChanged="Cbx4_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId4" Value='<%# Eval("id4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId4" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId4" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId4" Value='<%# Eval("file_type_id4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName4" Value='<%# Eval("file_name4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath4" Value='<%# Eval("file_path4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew4" Value='<%# Eval("is_new4") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                            </div>
                                            <!--end::Row-->
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <controls:MessageControl ID="UcRgd2" runat="server" />
                                </div>
                                <div class="tab-pane fade" id="tab_1_3" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                    <asp:Repeater ID="Rdg3" OnLoad="Rdg3_Load" OnItemDataBound="Rdg3_ItemDataBound" runat="server">
                                        <ItemTemplate>
                                            <!--begin::Row-->
                                            <div class="row">
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div1" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif1" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete1" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete1" onserverclick="aDelete1_OnClick" visible="false" role="button" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server">
                                                                    <img id="img1" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type1")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl1" Text='<%# DataBinder.Eval(Container.DataItem, "file_title1")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx1" AutoPostBack="true" OnCheckedChanged="Cbx1_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId1" Value='<%# Eval("id1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId1" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId1" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId1" Value='<%# Eval("file_type_id1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName1" Value='<%# Eval("file_name1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath1" Value='<%# Eval("file_path1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew1" Value='<%# Eval("is_new1") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div2" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif2" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete2" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete2" onserverclick="aDelete2_OnClick" visible="false" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile2" onserverclick="HpLnkFile2_ServerClick" role="button" runat="server">
                                                                    <img id="img2" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type2")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a2" runat="server" onserverclick="HpLnkFile2_ServerClick" role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl2" Text='<%# DataBinder.Eval(Container.DataItem, "file_title2")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx2" AutoPostBack="true" OnCheckedChanged="Cbx2_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId2" Value='<%# Eval("id2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId2" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId2" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId2" Value='<%# Eval("file_type_id2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName2" Value='<%# Eval("file_name2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath2" Value='<%# Eval("file_path2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew2" Value='<%# Eval("is_new2") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div3" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif3" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete3" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete3" onserverclick="aDelete3_OnClick" visible="false" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile3" onserverclick="HpLnkFile3_ServerClick" role="button" runat="server">
                                                                    <img id="img3" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type3")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a3" runat="server" onserverclick="HpLnkFile3_ServerClick" role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl3" Text='<%# DataBinder.Eval(Container.DataItem, "file_title3")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx3" AutoPostBack="true" OnCheckedChanged="Cbx3_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId3" Value='<%# Eval("id3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId3" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId3" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId3" Value='<%# Eval("file_type_id3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName3" Value='<%# Eval("file_name3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath3" Value='<%# Eval("file_path3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew3" Value='<%# Eval("is_new3") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div4" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif4" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete4" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete4" onserverclick="aDelete4_OnClick" visible="false" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile4" onserverclick="HpLnkFile4_ServerClick" role="button" runat="server">
                                                                    <asp:Image ID="img4" runat="server" AlternateText="" class="max-h-65px" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "file_type4")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a4" runat="server" onserverclick="HpLnkFile4_ServerClick" role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl4" Text='<%# DataBinder.Eval(Container.DataItem, "file_title4")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx4" AutoPostBack="true" OnCheckedChanged="Cbx4_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId4" Value='<%# Eval("id4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId4" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId4" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId4" Value='<%# Eval("file_type_id4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName4" Value='<%# Eval("file_name4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath4" Value='<%# Eval("file_path4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew4" Value='<%# Eval("is_new4") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                            </div>
                                            <!--end::Row-->
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <controls:MessageControl ID="UcRgd3" runat="server" />
                                </div>
                                <div class="tab-pane fade" id="tab_1_4" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                    <asp:Repeater ID="Rdg4" OnLoad="Rdg4_Load" OnItemDataBound="Rdg4_ItemDataBound" runat="server">
                                        <ItemTemplate>
                                            <!--begin::Row-->
                                            <div class="row">
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div1" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif1" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete1" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete1" onserverclick="aDelete1_OnClick" visible="false" role="button" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server">
                                                                    <img id="img1" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type1")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl1" Text='<%# DataBinder.Eval(Container.DataItem, "file_title1")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx1" AutoPostBack="true" OnCheckedChanged="Cbx1_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId1" Value='<%# Eval("id1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId1" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId1" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId1" Value='<%# Eval("file_type_id1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName1" Value='<%# Eval("file_name1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath1" Value='<%# Eval("file_path1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew1" Value='<%# Eval("is_new1") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div2" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif2" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete2" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete2" onserverclick="aDelete2_OnClick" visible="false" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile2" onserverclick="HpLnkFile2_ServerClick" role="button" runat="server">
                                                                    <img id="img2" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type2")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a2" runat="server" onserverclick="HpLnkFile2_ServerClick" role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl2" Text='<%# DataBinder.Eval(Container.DataItem, "file_title2")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx2" AutoPostBack="true" OnCheckedChanged="Cbx2_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId2" Value='<%# Eval("id2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId2" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId2" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId2" Value='<%# Eval("file_type_id2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName2" Value='<%# Eval("file_name2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath2" Value='<%# Eval("file_path2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew2" Value='<%# Eval("is_new2") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div3" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif3" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete3" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete3" onserverclick="aDelete3_OnClick" visible="false" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile3" onserverclick="HpLnkFile3_ServerClick" role="button" runat="server">
                                                                    <img id="img3" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type3")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a3" runat="server" onserverclick="HpLnkFile3_ServerClick" role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl3" Text='<%# DataBinder.Eval(Container.DataItem, "file_title3")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx3" AutoPostBack="true" OnCheckedChanged="Cbx3_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId3" Value='<%# Eval("id3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId3" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId3" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId3" Value='<%# Eval("file_type_id3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName3" Value='<%# Eval("file_name3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath3" Value='<%# Eval("file_path3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew3" Value='<%# Eval("is_new3") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div4" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif4" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete4" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete4" onserverclick="aDelete4_OnClick" visible="false" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile4" onserverclick="HpLnkFile4_ServerClick" role="button" runat="server">
                                                                    <asp:Image ID="img4" runat="server" AlternateText="" class="max-h-65px" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "file_type4")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a4" runat="server" onserverclick="HpLnkFile4_ServerClick" role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl4" Text='<%# DataBinder.Eval(Container.DataItem, "file_title4")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx4" AutoPostBack="true" OnCheckedChanged="Cbx4_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId4" Value='<%# Eval("id4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId4" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId4" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId4" Value='<%# Eval("file_type_id4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName4" Value='<%# Eval("file_name4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath4" Value='<%# Eval("file_path4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew4" Value='<%# Eval("is_new4") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                            </div>
                                            <!--end::Row-->
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <controls:MessageControl ID="UcRgd4" runat="server" />
                                </div>
                                <div class="tab-pane fade" id="tab_1_5" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                    <asp:Repeater ID="Rdg5" OnLoad="Rdg5_Load" OnItemDataBound="Rdg5_ItemDataBound" runat="server">
                                        <ItemTemplate>
                                            <!--begin::Row-->
                                            <div class="row">
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div1" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif1" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete1" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete1" onserverclick="aDelete1_OnClick" visible="false" role="button" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server">
                                                                    <img id="img1" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type1")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl1" Text='<%# DataBinder.Eval(Container.DataItem, "file_title1")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx1" AutoPostBack="true" OnCheckedChanged="Cbx1_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId1" Value='<%# Eval("id1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId1" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId1" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId1" Value='<%# Eval("file_type_id1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName1" Value='<%# Eval("file_name1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath1" Value='<%# Eval("file_path1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew1" Value='<%# Eval("is_new1") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div2" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif2" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete2" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete2" onserverclick="aDelete2_OnClick" visible="false" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile2" onserverclick="HpLnkFile2_ServerClick" role="button" runat="server">
                                                                    <img id="img2" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type2")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a2" runat="server" onserverclick="HpLnkFile2_ServerClick" role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl2" Text='<%# DataBinder.Eval(Container.DataItem, "file_title2")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx2" AutoPostBack="true" OnCheckedChanged="Cbx2_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId2" Value='<%# Eval("id2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId2" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId2" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId2" Value='<%# Eval("file_type_id2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName2" Value='<%# Eval("file_name2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath2" Value='<%# Eval("file_path2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew2" Value='<%# Eval("is_new2") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div3" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif3" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete3" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete3" onserverclick="aDelete3_OnClick" visible="false" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile3" onserverclick="HpLnkFile3_ServerClick" role="button" runat="server">
                                                                    <img id="img3" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type3")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a3" runat="server" onserverclick="HpLnkFile3_ServerClick" role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl3" Text='<%# DataBinder.Eval(Container.DataItem, "file_title3")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx3" AutoPostBack="true" OnCheckedChanged="Cbx3_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId3" Value='<%# Eval("id3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId3" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId3" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId3" Value='<%# Eval("file_type_id3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName3" Value='<%# Eval("file_name3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath3" Value='<%# Eval("file_path3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew3" Value='<%# Eval("is_new3") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div4" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif4" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete4" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete4" onserverclick="aDelete4_OnClick" visible="false" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile4" onserverclick="HpLnkFile4_ServerClick" role="button" runat="server">
                                                                    <asp:Image ID="img4" runat="server" AlternateText="" class="max-h-65px" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "file_type4")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a4" runat="server" onserverclick="HpLnkFile4_ServerClick" role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl4" Text='<%# DataBinder.Eval(Container.DataItem, "file_title4")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx4" AutoPostBack="true" OnCheckedChanged="Cbx4_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId4" Value='<%# Eval("id4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId4" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId4" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId4" Value='<%# Eval("file_type_id4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName4" Value='<%# Eval("file_name4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath4" Value='<%# Eval("file_path4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew4" Value='<%# Eval("is_new4") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                            </div>
                                            <!--end::Row-->
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <controls:MessageControl ID="UcRgd5" runat="server" />
                                </div>
                                <div class="tab-pane fade" id="tab_1_6" runat="server" role="tabpanel" aria-labelledby="kt_tab_pane_1_3">
                                    <asp:Repeater ID="Rdg6" OnLoad="Rdg6_Load" OnItemDataBound="Rdg6_ItemDataBound" runat="server">
                                        <ItemTemplate>
                                            <!--begin::Row-->
                                            <div class="row">
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div1" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif1" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete1" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete1" onserverclick="aDelete1_OnClick" visible="false" role="button" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server">
                                                                    <img id="img1" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type1")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a1" onserverclick="HpLnkFile1_ServerClick" role="button" runat="server" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl1" Text='<%# DataBinder.Eval(Container.DataItem, "file_title1")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx1" AutoPostBack="true" OnCheckedChanged="Cbx1_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId1" Value='<%# Eval("id1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId1" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId1" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId1" Value='<%# Eval("file_type_id1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName1" Value='<%# Eval("file_name1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath1" Value='<%# Eval("file_path1") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew1" Value='<%# Eval("is_new1") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div2" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif2" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete2" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete2" onserverclick="aDelete2_OnClick" visible="false" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile2" onserverclick="HpLnkFile2_ServerClick" role="button" runat="server">
                                                                    <img id="img2" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type2")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a2" runat="server" onserverclick="HpLnkFile2_ServerClick" role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl2" Text='<%# DataBinder.Eval(Container.DataItem, "file_title2")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx2" AutoPostBack="true" OnCheckedChanged="Cbx2_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId2" Value='<%# Eval("id2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId2" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId2" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId2" Value='<%# Eval("file_type_id2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName2" Value='<%# Eval("file_name2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath2" Value='<%# Eval("file_path2") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew2" Value='<%# Eval("is_new2") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div3" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif3" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete3" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete3" onserverclick="aDelete3_OnClick" visible="false" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile3" onserverclick="HpLnkFile3_ServerClick" role="button" runat="server">
                                                                    <img id="img3" runat="server" alt="" class="max-h-65px" src='<%# DataBinder.Eval(Container.DataItem, "file_type3")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a3" runat="server" onserverclick="HpLnkFile3_ServerClick" role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl3" Text='<%# DataBinder.Eval(Container.DataItem, "file_title3")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx3" AutoPostBack="true" OnCheckedChanged="Cbx3_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId3" Value='<%# Eval("id3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId3" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId3" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId3" Value='<%# Eval("file_type_id3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName3" Value='<%# Eval("file_name3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath3" Value='<%# Eval("file_path3") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew3" Value='<%# Eval("is_new3") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                                <!--begin::Col-->
                                                <div class="col-xl-3 col-lg-6 col-md-6 col-sm-6">
                                                    <!--begin::Card-->
                                                    <div id="div4" runat="server" class="card card-custom gutter-b card-stretch">
                                                        <div class="card-header border-0">
                                                            <h3 class="card-title"></h3>
                                                            <div class="card-toolbar">
                                                                <controls:NotificationControl ID="UcNotif4" MessageTypes="Error" Text="new" IsBold="false" HasRightMargin="true" IsLightColor="false" runat="server" />
                                                                <div id="divAdelete4" runat="server" visible="false" class="dropdown dropdown-inline" data-toggle="tooltip" title="Delete file" data-placement="right">
                                                                    <a id="aDelete4" onserverclick="aDelete4_OnClick" visible="false" runat="server" class="btn btn-icon btn-light btn-hover-primary btn-sm"></a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="card-body">
                                                            <div class="d-flex flex-column align-items-center">
                                                                <!--begin: Icon-->
                                                                <a id="HpLnkFile4" onserverclick="HpLnkFile4_ServerClick" role="button" runat="server">
                                                                    <asp:Image ID="img4" runat="server" AlternateText="" class="max-h-65px" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "file_type4")%>' />
                                                                </a>
                                                                <!--end: Icon-->
                                                                <!--begin: Tite-->
                                                                <a id="a4" runat="server" onserverclick="HpLnkFile4_ServerClick" role="button" class="text-dark-75 font-weight-bold mt-15 font-size-lg">
                                                                    <asp:Label ID="Lbl4" Text='<%# DataBinder.Eval(Container.DataItem, "file_title4")%>' runat="server" />
                                                                </a>
                                                                <asp:CheckBox ID="Cbx4" AutoPostBack="true" OnCheckedChanged="Cbx4_CheckedChanged" runat="server" />
                                                                <asp:HiddenField ID="HdnId4" Value='<%# Eval("id4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnCategoryId4" Value='<%# Eval("category_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnUserId4" Value='<%# Eval("user_id") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileTypeId4" Value='<%# Eval("file_type_id4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFileName4" Value='<%# Eval("file_name4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnFilePath4" Value='<%# Eval("file_path4") %>' runat="server" />
                                                                <asp:HiddenField ID="HdnIsNew4" Value='<%# Eval("is_new4") %>' runat="server" />
                                                                <!--end: Tite-->
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!--end:: Card-->
                                                </div>
                                                <!--end::Col-->
                                            </div>
                                            <!--end::Row-->
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <controls:MessageControl ID="UcRgd6" runat="server" />
                                </div>                                
                            </div>

                        </div>
                    </div>
                    <!--end::List-->
                </div>
                <!--end::Todo Files-->
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
                            <asp:Label ID="LblFileUploadTitle" runat="server" />
                        </h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <i aria-hidden="true" class="ki ki-close"></i>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <controls:MessageControl ID="UploadMessageAlert" runat="server" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-light-primary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
<!-- Confirmation Delete Partner form (modal view) -->
<div id="divConfirm" class="modal fade" tabindex="-1" data-width="300">
    <asp:UpdatePanel runat="server" ID="UpdatePanel4">
        <ContentTemplate>
            <div role="document" class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">
                            <asp:Label ID="LblConfTitle" Text="Confirmation" runat="server" />
                        </h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <i aria-hidden="true" class="ki ki-close"></i>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <controls:MessageControl ID="UcConfirmationMessageControl" runat="server" />
                        </div>
                        <div class="row" style="margin-top: 25px;">
                            <div id="divSuccess" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-success fade show" role="alert">
                                <div class="alert-icon">
                                    <i class="flaticon-warning"></i>
                                </div>
                                <div class="alert-text">
                                    <strong>
                                        <asp:Label ID="LblSuccess" Text="Done!" runat="server" />
                                    </strong>
                                    <asp:Label ID="LblSuccessMsg" runat="server" />
                                </div>
                                <div class="alert-close">
                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                        <span aria-hidden="true">
                                            <i class="ki ki-close"></i>
                                        </span>
                                    </button>
                                </div>
                            </div>
                            <div id="divFailure" runat="server" visible="false" style="width: 100%;" class="alert alert-custom alert-light-danger fade show" role="alert">
                                <div class="alert-icon">
                                    <i class="flaticon-questions-circular-button"></i>
                                </div>
                                <div class="alert-text">
                                    <strong>
                                        <asp:Label ID="LblFailure" Text="Error!" runat="server" />
                                    </strong>
                                    <asp:Label ID="LblFailureMsg" runat="server" />
                                </div>
                                <div class="alert-close">
                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                        <span aria-hidden="true">
                                            <i class="ki ki-close"></i>
                                        </span>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" data-dismiss="modal" class="btn btn-light-primary">Close</button>
                        <asp:Button ID="BtnDeleteFile" OnClick="BtnDeleteFile_OnClick" Text="Delete" CssClass="btn btn-primary" runat="server" />
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

        function OpenConfPopUp() {
            $('#divConfirm').modal('show');
        }

        function CloseConfPopUp() {
            $('#divConfirm').modal('hide');
        }
        function OpenConfPopUpVideo() {
            $('#divVideoConfirm').modal('show');
        }

        function CloseConfPopUpVideo() {
            $('#divVideoConfirm').modal('hide');
        }

        function OpenLibraryPopUp() {
            $('#PopUpLibrary').modal('show');
        }

        function CloseLibraryPopUp() {
            $('#PopUpLibrary').modal('hide');
        }

    </script>

</telerik:RadScriptBlock>

