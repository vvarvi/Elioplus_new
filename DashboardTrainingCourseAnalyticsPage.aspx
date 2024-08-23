<%@ Page Title="" Language="C#" MasterPageFile="~/MasterDashboard.Master" AutoEventWireup="true" CodeBehind="DashboardTrainingCourseAnalyticsPage.aspx.cs" Inherits="WdS.ElioPlus.DashboardTrainingCourseAnalyticsPage" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="DashHomeHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashHomeMain" ContentPlaceHolderID="MainContent" runat="server">

    <!--begin::Entry-->
    <div class="d-flex flex-column-fluid">
        <!--begin::Container-->
        <div class="container">
            <asp:UpdatePanel runat="server" ID="UpdatePanelContent">
                <ContentTemplate>
                    <div id="fade-class" class="card card-custom">
                        <div class="card-header">
                            <h3 class="card-title">
                                <asp:Label ID="LblCourse" runat="server" />
                            </h3>
                            <div class="card-toolbar">
                                <div class="d-flex align-items-center">
                                    <!--begin::Actions-->
                                    <a id="aLibrary" runat="server" class="btn btn-light-primary font-weight-bold btn-sm">Back to Analytics</a>
                                    <!--end::Actions-->                                    
                                </div>
                            </div>
                        </div>
                        <div class="card-body p-0">
                            <div class="row>">
                                <div class="col-lg-12">
                                    <div class="form-group mt-20">
                                        <telerik:RadGrid ID="RdgElioUsers" Style="margin: auto; position: relative;" AllowPaging="true" AllowSorting="true" HeaderStyle-BackColor="#39b4ef" PagerStyle-Position="TopAndBottom" HeaderStyle-ForeColor="#ffffff" PageSize="50" Width="100%" CssClass="rgdd"
                                            OnDetailTableDataBind="RdgElioUsers_DetailTableDataBind"
                                            OnItemDataBound="RdgElioUsers_OnItemDataBound"
                                            OnNeedDataSource="RdgElioUsers_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                            <MasterTableView Width="100%" DataKeyNames="id" Name="Parent" AllowMultiColumnSorting="true">
                                                <NoRecordsTemplate>
                                                    <div class="emptyGridHolder">
                                                        <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                    </div>
                                                </NoRecordsTemplate>
                                                <DetailTables>
                                                    <telerik:GridTableView PageSize="100" DataKeyNames="id" Name="CompanyItems" Width="100%">
                                                        <Columns>
                                                            <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                            <telerik:GridBoundColumn Display="false" DataField="chapter_id" UniqueName="chapter_id" />
                                                            <telerik:GridBoundColumn HeaderText="Chapter Title" DataField="chapter_title" UniqueName="chapter_title" />
                                                            <%--<telerik:GridBoundColumn HeaderText="Chapter File Name" DataField="chapter_file_name" UniqueName="chapter_file_name" />--%>
                                                            <telerik:GridTemplateColumn HeaderText="Chapter File Name" DataField="chapter_file_name" UniqueName="chapter_file_name">
                                                                <ItemTemplate>
                                                                    <a id="aChapterFilePath" runat="server" target="_blank" href='<%# DataBinder.Eval(Container.DataItem, "chapter_file_path")%>' class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                        <span class="text-dark-75 font-weight-bolder d-block font-size-lg">
                                                                            <asp:Label ID="LblChapterFileName" Text='<%# DataBinder.Eval(Container.DataItem, "chapter_file_name")%>' runat="server" />
                                                                        </span>
                                                                    </a>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <%--<telerik:GridBoundColumn Display="false" DataField="chapter_link" UniqueName="chapter_link" />--%>
                                                            <telerik:GridTemplateColumn HeaderText="Chapter Link" DataField="chapter_link" UniqueName="chapter_link">
                                                                <ItemTemplate>
                                                                    <a id="aChapterLink" runat="server" target="_blank" href='<%# DataBinder.Eval(Container.DataItem, "chapter_link")%>' class="text-dark-75 font-weight-bolder text-hover-primary font-size-lg">
                                                                        <asp:Label ID="LblChapterLink" Text="Chapter link" runat="server" Style="cursor: pointer; font-style: italic;" />
                                                                    </a>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn HeaderText="Published" DataField="sysdate" UniqueName="sysdate" />
                                                            <telerik:GridBoundColumn HeaderText="Is Viewed" DataField="is_viewed" UniqueName="is_viewed" />
                                                            <telerik:GridBoundColumn HeaderText="Date Viewed" DataField="date_viewed" UniqueName="date_viewed" />
                                                        </Columns>
                                                    </telerik:GridTableView>
                                                </DetailTables>
                                                <Columns>
                                                    <telerik:GridBoundColumn Display="false" HeaderText="Id" DataField="id" UniqueName="id" />
                                                    <%--<telerik:GridBoundColumn Display="false" HeaderText="Course Image" DataField="overview_image_path" UniqueName="overview_image_path" />--%>
                                                    <telerik:GridTemplateColumn HeaderText="Course Image" DataField="overview_image_path" UniqueName="overview_image_path">
                                                        <ItemTemplate>
                                                            <div class="symbol symbol-30 symbol-light">
                                                                <span class="symbol-label">
                                                                    <asp:Image ID="ImgPreview" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "overview_image_path")%>' runat="server" class="h-50 align-self-center" alt="" />
                                                                </span>
                                                            </div>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn HeaderText="Course" DataField="course_description" UniqueName="course_description" />
                                                    <telerik:GridBoundColumn HeaderText="Published" DataField="sysdate" UniqueName="sysdate" />
                                                    <telerik:GridBoundColumn Display="false" DataField="category_id" UniqueName="category_id" />
                                                    <telerik:GridBoundColumn HeaderText="Category" DataField="category_description" UniqueName="category_description" />
                                                    <telerik:GridBoundColumn HeaderText="Views" DataField="view_count" UniqueName="view_count" />
                                                    <telerik:GridTemplateColumn HeaderText="Actions">
                                                        <ItemTemplate>
                                                            <span class="symbol-label p-1">
                                                                <a id="aGear" runat="server" title="Permissions" class="navi-link">
                                                                    <span class="navi-icon">
                                                                        <i class="flaticon2-gear"></i>
                                                                    </span>
                                                                </a>
                                                            </span>
                                                            <span class="symbol-label p-1">
                                                                <a id="aEdit" runat="server" title="Edit course" class="navi-link">
                                                                    <span class="navi-icon">
                                                                        <i class="flaticon-edit"></i>
                                                                    </span>
                                                                </a>
                                                            </span>
                                                            <span class="symbol-label p-1">
                                                                <a id="aDeleteCourse" runat="server" title="Delete course" class="navi-link">
                                                                    <span class="navi-icon">
                                                                        <i class="flaticon-delete"></i>
                                                                    </span>
                                                                </a>
                                                            </span>
                                                            <span class="symbol-label p-1">
                                                                <a id="aViewPartners" onserverclick="aViewPartners_ServerClick" runat="server" title="View Course Analytics" class="navi-link">
                                                                    <span class="navi-icon">
                                                                        <i class="flaticon-graph"></i>
                                                                    </span>
                                                                </a>
                                                            </span>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </div>
                                </div>
                            </div>
                            <div class="row>">
                                <div class="col-lg-12">
                                    <div style="min-height: 50px; text-align: center; margin-top: 20px;">
                                        <controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />
                                        <controls:MessageControl ID="UcStripeMessageAlert" Visible="false" runat="server" />
                                        <controls:MessageControl ID="UcMessageAlertWarning" Visible="false" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!--end::Container-->
    </div>
    <!--end::Entry-->

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <script type="text/javascript">

            function OpenConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('show');
            }

            function CloseConfirmationPopUp() {
                $('#PopUpMessageAlert').modal('hide');
            }

        </script>

    </telerik:RadScriptBlock>

</asp:Content>
