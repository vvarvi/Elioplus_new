<%@ Page Title="" Language="C#" MasterPageFile="~/CollaborationToolMaster.Master" AutoEventWireup="true" CodeBehind="DashboardCollaborationLibrary.aspx.cs" Inherits="WdS.ElioPlus.DashboardCollaborationLibrary" %>
<%@ MasterType VirtualPath="~/CollaborationToolMaster.Master" %>

<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl"
    TagPrefix="controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxManager ID="masterRadAjaxManager" runat="server" RestoreOriginalRenderDelegate="false">
    </telerik:RadAjaxManager>    
    <!-- BEGIN PAGE TITLE-->
    <h3 class="page-title">
        <asp:Label ID="LblElioplusDashboard" runat="server" />
        <small>
            <asp:Label ID="LblDashSubTitle" runat="server" /></small>
    </h3>
    <!-- END PAGE TITLE-->
    <asp:UpdatePanel runat="server" ID="UpdatePanel7" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="main-container">
            <!-- BEGIN CONTENT -->
                <div class="page-container">
                    <div class="page-content container-fluid">
                        <div class="row">
                            <div class="col-lg-12">
                                <h3 class="widget-heading" style="padding-bottom:50px;padding-left:40%;padding-top:0">
                                    <asp:Label ID="LblFileLibrary" Text="Library Files" runat="server" /> 
                                </h3>
                                <div class="widget-body">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanelContent" UpdateMode="Conditional">
                                        <ContentTemplate>                                    
                                    <div role="tabpanel">
                                        <div id="divTabs" class="nav nav-tabs mb-15" style="padding-left:10%;padding-bottom:50px" runat="server">
                                            <ul class="nav nav-sidebar-content" style="float:left">
                                                <li id="liProductUpdates" class="active" runat="server">
                                                    <a id="aTabProductUpdates" class="" onserverclick="aProductUpdates_OnClick" runat="server"><asp:Label ID="LblProductUpdates" Text="Product Updates" runat="server" /></a>
                                                </li>
                                                <li id="liMarketingMaterial" runat="server">
                                                    <a id="aTabMarketingMaterial" onserverclick="aTabMarketingMaterial_OnClick" runat="server"><asp:Label ID="LblMarketingMaterial" Text="Marketing Material" runat="server" /></a>
                                                </li>
                                                <li id="liNewCampaign" class="" runat="server">
                                                    <a id="aTabNewCampaign" onserverclick="aTabNewCampaign_OnClick" runat="server"><asp:Label ID="LblNewCampaign" Text="New Campaign" runat="server" /></a>
                                                </li>
                                                <li id="liNewsLetter" runat="server">
                                                    <a id="aTabNewsLetter" onserverclick="aTabNewsLetter_OnClick" runat="server"><asp:Label ID="LblbNewsLetter" Text="NewLetter Issue" runat="server" /></a>
                                                </li>
                                                <li id="liBanner" runat="server">
                                                    <a id="aTabBanner" onserverclick="aTabBanner_OnClick" runat="server"><asp:Label ID="LblBanner" Text="Banners" runat="server" /></a>
                                                </li>
                                                <li id="liDocumentationPdf" runat="server">
                                                    <a id="aTabDocumentationPdf" onserverclick="aTabDocumentationPdf_OnClick" runat="server"><asp:Label ID="LblDocumentationPdf" Text="Documentation(Pdf)" runat="server" /></a>
                                                </li>
                                            </ul>
                                            <div class="tab-content" style="float:left">
                                                <!--tab_1_1-->
                                                <div role="tabpanel" aria-labelledby="tab_1_1-tab" class="tab-pane fade active in" id="tab_1_1" runat="server">
                                                    <div class="col-md-12"> 
                                                        <div class="form-group">
                                                            <telerik:RadGrid ID="RdgProductUpdates" ShowHeader="false" OnNeedDataSource="RdgProductUpdates_OnNeedDataSource" 
                                                                OnItemDataBound="RdgProductUpdates_OnItemDataBound" CssClass="" Width="500px"
                                                                HeaderStyle-CssClass="display-none" BorderStyle="None" AllowPaging="true" PageSize="8" AutoGenerateColumns="false" 
                                                                runat="server" PagerStyle-Position="Bottom" MasterTableView-CssClass="" 
                                                                ItemStyle-CssClass="" AlternatingItemStyle-CssClass="">
                                                                <MasterTableView>
                                                                    <NoRecordsTemplate>
                                                                        <div class="emptyGridHolder">
                                                                            <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                                        </div>
                                                                    </NoRecordsTemplate>
                                                                    <Columns>                            
                                                                        <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="user_id" UniqueName="user_id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_type_id" UniqueName="file_type_id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="category_id" UniqueName="category_id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_title" UniqueName="file_title" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_name" UniqueName="file_name" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_path" UniqueName="file_path" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_type" UniqueName="file_type" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_format_extensions" UniqueName="file_format_extensions" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="date_created" UniqueName="date_created" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="is_public" UniqueName="is_public" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="is_new" UniqueName="is_new" />
                                                                        <telerik:GridTemplateColumn>
                                                                            <ItemTemplate>
                                                                                    <div class="tab-pane fade active in" style="background: whitesmoke; width:100%;border-radius:5px!important">
                                                                                        <div class="widget-news margin-bottom-10">
                                                                                            <div class="widget-news-left-elem" style="width:auto;padding-left:10px;">
                                                                                                <asp:HyperLink id="HpLnkFile" target="_blank" runat="server">
                                                                                                    <asp:Image ID="ImgFile" CssClass="" Width="40" Height="40" ImageUrl="~/images/no_logo.jpg" runat="server" />
                                                                                                </asp:HyperLink>
                                                                                            </div>
                                                                                            <div class="widget-news-right-body">                                                                                                
                                                                                                <h3 class="widget-news-right-body-title" style="margin-top:5px">
                                                                                                    <asp:Label ID="LblFileName" runat="server" />
                                                                                                    <asp:ImageButton ID="ImgFileNameInfo" Visible="false" ImageUrl="~/images/icons/small/info.png" runat="server" />
                                                                                                    <telerik:RadToolTip ID="RttFileNameInfo" TargetControlID="ImgFileNameInfo" Position="TopRight" Animation="Fade" runat="server" />
                                                                                                    <span class="label label-default" style="background:none">
                                                                                                        <a id="aDelete" onserverclick="aDelete_OnClick" visible="false" class="" runat="server">
                                                                                                            <i class="">
                                                                                                                <img src="/images/icons/small/remove-from-database.png" width="20" height="20" alt="delete" />
                                                                                                                <asp:Label ID="LblDeleteFile" Text="" runat="server" />
                                                                                                            </i>
                                                                                                        </a>                                            
                                                                                                        <telerik:RadToolTip ID="RttDeleteFile" TargetControlID="aDelete" Position="BottomRight" Animation="Fade" Text="Delete File" runat="server" />
                                                                                                    </span>
                                                                                                </h3>
                                                                                                <div style="line-height:initial; font-family:sans-serif; font-size:13px">Upload date: <span><asp:Label ID="LblUploadDate" Text="" runat="server" /></span></div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>                                       
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                            <controls:messagecontrol id="UcMessageAlert" visible="false" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--tab_1_2-->
                                                <div role="tabpanel" aria-labelledby="tab_1_2-tab" class="tab-pane fade" id="tab_1_2" runat="server">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <telerik:RadGrid ID="RdgMarketingMaterial" ShowHeader="false" OnNeedDataSource="RdgMarketingMaterial_OnNeedDataSource" 
                                                                OnItemDataBound="RdgMarketingMaterial_OnItemDataBound" CssClass="" Width="500px"
                                                                HeaderStyle-CssClass="display-none" BorderStyle="None" AllowPaging="true" PageSize="8" AutoGenerateColumns="false" 
                                                                runat="server" PagerStyle-Position="Bottom" MasterTableView-CssClass="" 
                                                                ItemStyle-CssClass="" AlternatingItemStyle-CssClass="">
                                                                <MasterTableView>
                                                                    <NoRecordsTemplate>
                                                                        <div class="emptyGridHolder">
                                                                            <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                                        </div>
                                                                    </NoRecordsTemplate>
                                                                    <Columns>                            
                                                                        <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="user_id" UniqueName="user_id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_type_id" UniqueName="file_type_id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="category_id" UniqueName="category_id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_title" UniqueName="file_title" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_name" UniqueName="file_name" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_path" UniqueName="file_path" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_type" UniqueName="file_type" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_format_extensions" UniqueName="file_format_extensions" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="date_created" UniqueName="date_created" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="is_public" UniqueName="is_public" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="is_new" UniqueName="is_new" />
                                                                        <telerik:GridTemplateColumn>
                                                                            <ItemTemplate>
                                                                                <div class="tab-pane fade active in" style="background: whitesmoke; width:100%;border-radius:5px!important">
                                                                                        <div class="widget-news margin-bottom-10">
                                                                                            <div class="widget-news-left-elem" style="width:auto;padding-left:10px;">
                                                                                                <asp:HyperLink id="HpLnkFile" target="_blank" runat="server">
                                                                                                    <asp:Image ID="ImgFile" CssClass="" Width="40" Height="40" ImageUrl="~/images/no_logo.jpg" runat="server" />
                                                                                                </asp:HyperLink>
                                                                                            </div>
                                                                                            <div class="widget-news-right-body">                                                                                                
                                                                                                <h3 class="widget-news-right-body-title" style="margin-top:5px">
                                                                                                    <asp:Label ID="LblFileName" runat="server" />
                                                                                                    <asp:ImageButton ID="ImgFileNameInfo" Visible="false" ImageUrl="~/images/icons/small/info.png" runat="server" />
                                                                                                    <telerik:RadToolTip ID="RttFileNameInfo" TargetControlID="ImgFileNameInfo" Position="TopRight" Animation="Fade" runat="server" />
                                                                                                    <span class="label label-default" style="background:none">
                                                                                                        <a id="aDelete" onserverclick="aDelete_OnClick" visible="false" class="" runat="server">
                                                                                                            <i class="">
                                                                                                                <img src="/images/icons/small/remove-from-database.png" width="20" height="20" alt="delete" />
                                                                                                                <asp:Label ID="LblDeleteFile" Text="" runat="server" />
                                                                                                            </i>
                                                                                                        </a>                                            
                                                                                                        <telerik:RadToolTip ID="RttDeleteFile" TargetControlID="aDelete" Position="BottomRight" Animation="Fade" Text="Delete File" runat="server" />
                                                                                                    </span>
                                                                                                </h3>
                                                                                                <div style="line-height:initial; font-family:sans-serif; font-size:13px">Upload date: <span><asp:Label ID="LblUploadDate" Text="" runat="server" /></span></div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>                                                                    
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                            <controls:messagecontrol id="MessageVideoAlert" visible="false" runat="server" />
                                                        </div>
                                                    </div>                                    
                                                </div>
                                                <!--tab_1_3-->
                                                <div role="tabpanel" aria-labelledby="tab_1_3-tab" class="tab-pane fade" id="tab_1_3" runat="server">
                                                    <div class="col-md-12"> 
                                                        <div class="form-group">
                                                            <telerik:RadGrid ID="RdgNewCampaign" ShowHeader="false" OnNeedDataSource="RdgNewCampaign_OnNeedDataSource" 
                                                                OnItemDataBound="RdgNewCampaign_OnItemDataBound" CssClass="" Width="500px"
                                                                HeaderStyle-CssClass="display-none" BorderStyle="None" AllowPaging="true" PageSize="8" 
                                                                AutoGenerateColumns="false" runat="server" PagerStyle-Position="Bottom" 
                                                                MasterTableView-CssClass="" 
                                                                ItemStyle-CssClass="" AlternatingItemStyle-CssClass="">
                                                                <MasterTableView>
                                                                    <NoRecordsTemplate>
                                                                        <div class="emptyGridHolder">
                                                                            <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                                        </div>
                                                                    </NoRecordsTemplate>
                                                                    <Columns>                            
                                                                        <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="user_id" UniqueName="user_id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_type_id" UniqueName="file_type_id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="category_id" UniqueName="category_id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_title" UniqueName="file_title" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_name" UniqueName="file_name" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_path" UniqueName="file_path" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_type" UniqueName="file_type" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_format_extensions" UniqueName="file_format_extensions" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="date_created" UniqueName="date_created" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="is_public" UniqueName="is_public" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="is_new" UniqueName="is_new" />
                                                                        <telerik:GridTemplateColumn>
                                                                            <ItemTemplate>
                                                                                <div class="tab-pane fade active in" style="background: whitesmoke; width:100%;border-radius:5px!important">
                                                                                        <div class="widget-news margin-bottom-10">
                                                                                            <div class="widget-news-left-elem" style="width:auto;padding-left:10px;">
                                                                                                <asp:HyperLink id="HpLnkFile" target="_blank" runat="server">
                                                                                                    <asp:Image ID="ImgFile" CssClass="" Width="40" Height="40" ImageUrl="~/images/no_logo.jpg" runat="server" />
                                                                                                </asp:HyperLink>
                                                                                            </div>
                                                                                            <div class="widget-news-right-body">                                                                                                
                                                                                                <h3 class="widget-news-right-body-title" style="margin-top:5px">
                                                                                                    <asp:Label ID="LblFileName" runat="server" />
                                                                                                    <asp:ImageButton ID="ImgFileNameInfo" Visible="false" ImageUrl="~/images/icons/small/info.png" runat="server" />
                                                                                                    <telerik:RadToolTip ID="RttFileNameInfo" TargetControlID="ImgFileNameInfo" Position="TopRight" Animation="Fade" runat="server" />
                                                                                                    <span class="label label-default" style="background:none">
                                                                                                        <a id="aDelete" onserverclick="aDelete_OnClick" visible="false" class="" runat="server">
                                                                                                            <i class="">
                                                                                                                <img src="/images/icons/small/remove-from-database.png" width="20" height="20" alt="delete" />
                                                                                                                <asp:Label ID="LblDeleteFile" Text="" runat="server" />
                                                                                                            </i>
                                                                                                        </a>                                            
                                                                                                        <telerik:RadToolTip ID="RttDeleteFile" TargetControlID="aDelete" Position="BottomRight" Animation="Fade" Text="Delete File" runat="server" />
                                                                                                    </span>
                                                                                                </h3>
                                                                                                <div style="line-height:initial; font-family:sans-serif; font-size:13px">Upload date: <span><asp:Label ID="LblUploadDate" Text="" runat="server" /></span></div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>                                                                  
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                            <controls:messagecontrol id="Messagecontrol1" visible="false" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--tab_1_4-->
                                                <div role="tabpanel" aria-labelledby="tab_1_4-tab" class="tab-pane fade" id="tab_1_4" runat="server">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <telerik:RadGrid ID="RdgNewsLetter" ShowHeader="false" OnNeedDataSource="RdgNewsLetter_OnNeedDataSource" 
                                                                OnItemDataBound="RdgNewsLetter_OnItemDataBound" CssClass="" HeaderStyle-CssClass="display-none" 
                                                                BorderStyle="None" AllowPaging="true" PageSize="8" AutoGenerateColumns="false" runat="server" Width="500px"
                                                                PagerStyle-Position="Bottom" MasterTableView-CssClass="" ItemStyle-CssClass="" AlternatingItemStyle-CssClass="">
                                                                <MasterTableView>
                                                                    <NoRecordsTemplate>
                                                                        <div class="emptyGridHolder">
                                                                            <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                                        </div>
                                                                    </NoRecordsTemplate>
                                                                    <Columns>                            
                                                                        <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="user_id" UniqueName="user_id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_type_id" UniqueName="file_type_id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="category_id" UniqueName="category_id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_title" UniqueName="file_title" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_name" UniqueName="file_name" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_path" UniqueName="file_path" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_type" UniqueName="file_type" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_format_extensions" UniqueName="file_format_extensions" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="date_created" UniqueName="date_created" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="is_public" UniqueName="is_public" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="is_new" UniqueName="is_new" />
                                                                        <telerik:GridTemplateColumn>
                                                                            <ItemTemplate>
                                                                                <div class="tab-pane fade active in" style="background: whitesmoke; width:100%;border-radius:5px!important">
                                                                                        <div class="widget-news margin-bottom-10">
                                                                                            <div class="widget-news-left-elem" style="width:auto;padding-left:10px;">
                                                                                                <asp:HyperLink id="HpLnkFile" target="_blank" runat="server">
                                                                                                    <asp:Image ID="ImgFile" CssClass="" Width="40" Height="40" ImageUrl="~/images/no_logo.jpg" runat="server" />
                                                                                                </asp:HyperLink>
                                                                                            </div>
                                                                                            <div class="widget-news-right-body">                                                                                                
                                                                                                <h3 class="widget-news-right-body-title" style="margin-top:5px">
                                                                                                    <asp:Label ID="LblFileName" runat="server" />
                                                                                                    <asp:ImageButton ID="ImgFileNameInfo" Visible="false" ImageUrl="~/images/icons/small/info.png" runat="server" />
                                                                                                    <telerik:RadToolTip ID="RttFileNameInfo" TargetControlID="ImgFileNameInfo" Position="TopRight" Animation="Fade" runat="server" />
                                                                                                    <span class="label label-default" style="background:none">
                                                                                                        <a id="aDelete" onserverclick="aDelete_OnClick" visible="false" class="" runat="server">
                                                                                                            <i class="">
                                                                                                                <img src="/images/icons/small/remove-from-database.png" width="20" height="20" alt="delete" />
                                                                                                                <asp:Label ID="LblDeleteFile" Text="" runat="server" />
                                                                                                            </i>
                                                                                                        </a>                                            
                                                                                                        <telerik:RadToolTip ID="RttDeleteFile" TargetControlID="aDelete" Position="BottomRight" Animation="Fade" Text="Delete File" runat="server" />
                                                                                                    </span>
                                                                                                </h3>
                                                                                                <div style="line-height:initial; font-family:sans-serif; font-size:13px">Upload date: <span><asp:Label ID="LblUploadDate" Text="" runat="server" /></span></div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>                                                                    
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                            <controls:messagecontrol id="Messagecontrol2" visible="false" runat="server" />
                                                        </div>
                                                    </div>                                    
                                                </div>
                                                <!--tab_1_5-->
                                                <div role="tabpanel" aria-labelledby="tab_1_5-tab" class="tab-pane fade" id="tab_1_5" runat="server">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <telerik:RadGrid ID="RdgBanner" ShowHeader="false" OnNeedDataSource="RdgBanner_OnNeedDataSource" Width="500px"
                                                                OnItemDataBound="RdgBanner_OnItemDataBound" CssClass="" HeaderStyle-CssClass="display-none" BorderStyle="None" 
                                                                AllowPaging="true" PageSize="8" AutoGenerateColumns="false" runat="server" PagerStyle-Position="Bottom" 
                                                                MasterTableView-CssClass="" ItemStyle-CssClass="" AlternatingItemStyle-CssClass="">
                                                                <MasterTableView>
                                                                    <NoRecordsTemplate>
                                                                        <div class="emptyGridHolder">
                                                                            <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                                        </div>
                                                                    </NoRecordsTemplate>
                                                                    <Columns>                            
                                                                        <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="user_id" UniqueName="user_id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_type_id" UniqueName="file_type_id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="category_id" UniqueName="category_id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_title" UniqueName="file_title" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_name" UniqueName="file_name" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_path" UniqueName="file_path" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_type" UniqueName="file_type" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_format_extensions" UniqueName="file_format_extensions" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="date_created" UniqueName="date_created" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="is_public" UniqueName="is_public" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="is_new" UniqueName="is_new" />
                                                                        <telerik:GridTemplateColumn>
                                                                            <ItemTemplate>
                                                                                <div class="tab-pane fade active in" style="background: whitesmoke; width:100%;border-radius:5px!important">
                                                                                        <div class="widget-news margin-bottom-10">
                                                                                            <div class="widget-news-left-elem" style="width:auto;padding-left:10px;">
                                                                                                <asp:HyperLink id="HpLnkFile" target="_blank" runat="server">
                                                                                                    <asp:Image ID="ImgFile" CssClass="" Width="40" Height="40" ImageUrl="~/images/no_logo.jpg" runat="server" />
                                                                                                </asp:HyperLink>
                                                                                            </div>
                                                                                            <div class="widget-news-right-body">                                                                                                
                                                                                                <h3 class="widget-news-right-body-title" style="margin-top:5px">
                                                                                                    <asp:Label ID="LblFileName" runat="server" />
                                                                                                    <asp:ImageButton ID="ImgFileNameInfo" Visible="false" ImageUrl="~/images/icons/small/info.png" runat="server" />
                                                                                                    <telerik:RadToolTip ID="RttFileNameInfo" TargetControlID="ImgFileNameInfo" Position="TopRight" Animation="Fade" runat="server" />
                                                                                                    <span class="label label-default" style="background:none">
                                                                                                        <a id="aDelete" onserverclick="aDelete_OnClick" visible="false" class="" runat="server">
                                                                                                            <i class="">
                                                                                                                <img src="/images/icons/small/remove-from-database.png" width="20" height="20" alt="delete" />
                                                                                                                <asp:Label ID="LblDeleteFile" Text="" runat="server" />
                                                                                                            </i>
                                                                                                        </a>                                            
                                                                                                        <telerik:RadToolTip ID="RttDeleteFile" TargetControlID="aDelete" Position="BottomRight" Animation="Fade" Text="Delete File" runat="server" />
                                                                                                    </span>
                                                                                                </h3>
                                                                                                <div style="line-height:initial; font-family:sans-serif; font-size:13px">Upload date: <span><asp:Label ID="LblUploadDate" Text="" runat="server" /></span></div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>                                                                    
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                            <controls:messagecontrol id="Messagecontrol3" visible="false" runat="server" />
                                                        </div>
                                                    </div>                                    
                                                </div>
                                                <!--tab_1_6-->
                                                <div role="tabpanel" aria-labelledby="tab_1_6-tab" class="tab-pane fade" id="tab_1_6" runat="server">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <telerik:RadGrid ID="RdgDocumentationPdf" ShowHeader="false" OnNeedDataSource="RdgDocumentationPdf_OnNeedDataSource" 
                                                                OnItemDataBound="RdgDocumentationPdf_OnItemDataBound" CssClass="" HeaderStyle-CssClass="display-none" 
                                                                BorderStyle="None" AllowPaging="true" PageSize="8" AutoGenerateColumns="false" runat="server" Width="500px"
                                                                PagerStyle-Position="Bottom" MasterTableView-CssClass="" ItemStyle-CssClass="" AlternatingItemStyle-CssClass="">
                                                                <MasterTableView>
                                                                    <NoRecordsTemplate>
                                                                        <div class="emptyGridHolder">
                                                                            <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                                        </div>
                                                                    </NoRecordsTemplate>
                                                                    <Columns>                            
                                                                        <telerik:GridBoundColumn Display="false" DataField="id" UniqueName="id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="user_id" UniqueName="user_id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_type_id" UniqueName="file_type_id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="category_id" UniqueName="category_id" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_title" UniqueName="file_title" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_name" UniqueName="file_name" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_path" UniqueName="file_path" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_type" UniqueName="file_type" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="file_format_extensions" UniqueName="file_format_extensions" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="date_created" UniqueName="date_created" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="is_public" UniqueName="is_public" />
                                                                        <telerik:GridBoundColumn Display="false" DataField="is_new" UniqueName="is_new" />
                                                                        <telerik:GridTemplateColumn>
                                                                            <ItemTemplate>
                                                                                <div class="tab-pane fade active in" style="background: whitesmoke; width:100%;border-radius:5px!important">
                                                                                        <div class="widget-news margin-bottom-10">
                                                                                            <div class="widget-news-left-elem" style="width:auto;padding-left:10px;">
                                                                                                <asp:HyperLink id="HpLnkFile" target="_blank" runat="server">
                                                                                                    <asp:Image ID="ImgFile" CssClass="" Width="40" Height="40" ImageUrl="~/images/no_logo.jpg" runat="server" />
                                                                                                </asp:HyperLink>
                                                                                            </div>
                                                                                            <div class="widget-news-right-body">                                                                                                
                                                                                                <h3 class="widget-news-right-body-title" style="margin-top:5px">
                                                                                                    <asp:Label ID="LblFileName" runat="server" />
                                                                                                    <asp:ImageButton ID="ImgFileNameInfo" Visible="false" ImageUrl="~/images/icons/small/info.png" runat="server" />
                                                                                                    <telerik:RadToolTip ID="RttFileNameInfo" TargetControlID="ImgFileNameInfo" Position="TopRight" Animation="Fade" runat="server" />
                                                                                                    <span class="label label-default" style="background:none">
                                                                                                        <a id="aDelete" onserverclick="aDelete_OnClick" visible="false" class="" runat="server">
                                                                                                            <i class="">
                                                                                                                <img src="/images/icons/small/remove-from-database.png" width="20" height="20" alt="delete" />
                                                                                                                <asp:Label ID="LblDeleteFile" Text="" runat="server" />
                                                                                                            </i>
                                                                                                        </a>                                            
                                                                                                        <telerik:RadToolTip ID="RttDeleteFile" TargetControlID="aDelete" Position="BottomRight" Animation="Fade" Text="Delete File" runat="server" />
                                                                                                    </span>
                                                                                                </h3>
                                                                                                <div style="line-height:initial; font-family:sans-serif; font-size:13px">Upload date: <span><asp:Label ID="LblUploadDate" Text="" runat="server" /></span></div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>                                                                    
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                            <controls:messagecontrol id="Messagecontrol4" visible="false" runat="server" />
                                                        </div>
                                                    </div>                                    
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="width:100%;">
                                    
                                    </div>
                                    
                                    <controls:MessageControl ID="UcMessageAlertLibraryControl" Visible="false" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>                        
                        </div>                        
                        <div class="row">
                            <div class="col-lg-12" style="padding-left:10%">
                                <h4 class="widget-heading">
                                    File Upload
                                </h4>
                                <div class="widget-body">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                                        <ContentTemplate>
                                            <div class="form-group">
                                                <div class="col-md-6">
                                                    <table>
                                                        <tr style="">
                                                            <td style="padding: 10px; margin: 15px;">
                                                                <asp:Label ID="LblFileTitle" Text="Title" runat="server" />
                                                            </td>
                                                            <td style="padding: 10px; margin: 15px;">
                                                                <asp:TextBox ID="TbxFileTitle" Width="320" CssClass="form-control todo-taskbody-taskdesc"
                                                                    placeholder="File title" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 10px; margin: 15px;">
                                                                <asp:Label ID="LblFileCategory" Text="Category" runat="server" />
                                                            </td>
                                                            <td style="padding: 10px; margin: 15px;">
                                                                <%--<controls:UcLibraryCategories ID="UcLibraryCategories" runat="server" />--%>
                                                                <telerik:RadDropDownList ID="Ddlcategory" Skin="Silk" Width="320" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 10px; margin: 15px;">
                                                            </td>
                                                            <td style="padding: 10px; float: right;">
                                                                <input type="file" name="uploadFile" id="inputFile" data-buttonname="btn-black btn-outline"
                                                                    data-iconname="ion-image mr-5" class="filestyle" accept=".pdf, .csv, .xls, .xlsx, .doc, .png, .jpg, .jpeg, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, text/plain, text/html"
                                                                    runat="server" />                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 10px; margin: 15px;">
                                                            </td>
                                                            <td style="padding: 10px; margin: 15px;">
                                                                <asp:Button ID="BtnUploadFile" OnClick="BtnUploadFile_OnCick" CssClass="btn btn-black"
                                                                    Text="Upload" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 10px; margin: 15px;">
                                                            </td>
                                                            <td style="padding: 10px; margin: 15px;">
                                                                <controls:MessageControl ID="UploadMessageAlert" Visible="false" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="BtnUploadFile" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>   
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Pop Up Invitation form Message (modal view) -->
    <div id="PopUpMessageAlert" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel5">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-black no-border">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                            <h4 class="modal-title"><asp:Label ID="LblFileUploadTitle" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="LblFileUploadfMsg" CssClass="control-label" runat="server" />                                       
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

    <!-- Confirmation Delete Partner form (modal view) -->
    <div id="divConfirm" class="modal fade" tabindex="-1" data-width="300">
        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
            <ContentTemplate>
                <div role="document" class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-black no-border">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                            <h4 class="modal-title"><asp:Label ID="LblConfTitle" Text="Confirmation" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="LblConfMsg" Text="Are you sure you want to delete this file from your library?" CssClass="control-label" runat="server" />
                                    </div>                                    
                                </div>                            
                            </div>                    
                        </div>
                        <div id="divFailure" runat="server" visible="false" class="col-md-12 alert alert-danger" style="text-align:center">
                            <strong>
                                <asp:Label ID="LblFailure" Text="Error! " runat="server" />
                            </strong>
                            <asp:Label ID="LblFailureMsg" runat="server" />
                        </div>
                        <div id="divSuccess" runat="server" visible="false" class="col-md-12 alert alert-success" style="text-align:center;">
                            <strong>
                                <asp:Label ID="LblSuccess" Text="Done! " runat="server" />
                            </strong>
                            <asp:Label ID="LblSuccessMsg" runat="server" />
                        </div> 
                        <div class="modal-footer">                            
                            <button type="button" data-dismiss="modal" class="btn btn-danger">Close</button>
                            <asp:Button ID="BtnDeleteFile" OnClick="BtnDeleteFile_OnClick" Text="Delete" CssClass="btn red-sunglo" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <style>
            .RadGrid_MetroTouch .rgAltRow
            {
                background-color:transparent !important;
            }
            .col-lg-3
            {
                width:16% !important;
            }
        </style>
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
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
