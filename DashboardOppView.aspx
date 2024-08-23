<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="DashboardOppView.aspx.cs" Inherits="WdS.ElioPlus.DashboardOppView" %>

<asp:Content ID="DashEditHead" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/assets/layouts/layout/css/layout.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function RapPage_OnRequestStart(sender, args) {
            $('#loader').show();
        }

        function endsWith(s) {
            return this.length >= s.length && this.substr(this.length - s.length) == s;
        }

        function RapPage_OnResponseEnd(sender, args) {
            $('#loader').hide();
        }

        function isNumberOnly(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="DashEditMain" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadAjaxPanel ID="RapPage" ClientEvents-OnRequestStart="RapPage_OnRequestStart" ClientEvents-OnResponseEnd="RapPage_OnResponseEnd" runat="server" RestoreOriginalRenderDelegate="false">
    </telerik:RadAjaxPanel>
    <style type="text/css">        
        .itemClass {           
            margin-bottom:5px;
            border-width:3px !important;           
            border-radius:5px !important;
            border-style:outset !important;
            line-height:30px !important;
        }
        .itemClass.rlbSelected {           
            color:sienna !important;
        }        
        .nameClass {
            float: right;
            width:100%;
            margin-left:10px;
            font-weight:400;
            border-bottom:1px solid #454545;
        }        
        .imgClass{
            float:left;            
            margin-right:0px;
            margin-top:10px;
        }        
        .settClass{
            float:right;
            width:20px;
            padding-top:5px;
            font-weight: 400;
        }        
        .lineClass{
            width:100%;
        }        
        .noteClass{
            float:right;
            width:50%;
            border-bottom:1px solid #454545;
            border-top:1px solid #454545;
            font-weight: 400;
        }        
        .taskClass{
            float:left;
            width:50%;
            border-bottom:1px solid #454545;
            border-top:1px solid #454545;
            font-weight: 400;
        }
        .stClass{
            float:left;
            width:100%;
            margin-right:0px;
            margin-top:10px;
        }
        .saveClass{
            float:right;
            width:20px;
            padding-top:5px;
            font-weight: 400;
            margin-top:-33px;
        }  
        .cClass{
            float:right;
            width:60%;
            border-bottom:1px solid #454545;                
            font-weight: 400;
        }
        .emailClass{
            float:left;
            width:80%;
            margin-left:50px;
        }
    </style> 
    <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Conditional" ChildrenAsTriggers="true" >
        <ContentTemplate>
            <!-- BEGIN PAGE BAR -->
            <div class="page-bar">
                <ul class="page-breadcrumb">
                    <li><span><asp:Label ID="LblDashboard" runat="server" /></span> <i class="fa fa-circle"></i>
                    </li>
                    <li><span><asp:Label ID="LblDashPage" runat="server" /></span> </li>
                </ul>
            </div>
            <!-- END PAGE BAR -->
            <!-- BEGIN PAGE TITLE-->
            <h3 class="page-title">
                <asp:Label ID="LblElioplusDashboard" runat="server" />
                <small><asp:Label ID="LblDashSubTitle" runat="server" /></small>
            </h3>
            <!-- END PAGE TITLE-->
            <div id="page-content-wrapper">
                <div style="padding:10px;">
                    <a id="aAddOpportunity" runat="server" class="btn btn-circle green-meadow btn-md">
                        <asp:Label ID="Label3" Text="Add New Opportunity" runat="server" />
                    </a>
                    <div style="float:right;">
                        <asp:DropDownList ID="DdlOpportKinds" runat="server">
                            <asp:ListItem Value="0" Text="All opportunities" Selected="True" />
                            <asp:ListItem Value="1" Text="Opportunities with open tasks" />                                                
                        </asp:DropDownList>
                        <Telerik:RadTextBox ID="TbxSearch" style="margin-bottom:3px; vertical-align:middle; font-size:14px;" EmptyMessage="Organization Name" Height="23" runat="server"></Telerik:RadTextBox>
                        <asp:Button ID="BtnSearch" OnClick="BtntSort_OnClick" CssClass="btn btn-circle btn-sm btn-primary" runat="server" Text="Search" />
                    </div>
                </div> 
                <div class="row" style="margin-bottom:50px">
                    <div class="col-md-12">
                        <div class="col-md-3 col-sm-3 col-xs-6">                        
                            <div class="portlet light portlet-fit bordered" style="border: 0px solid #e7ecf1 !important;">
                                <div class="portlet-title" style="padding:0px;">                            
                                    <div class="portlet-body">
                                        <div class="mt-element-step">
                                            <div class="row step-thin">
                                                <%--f2878d--%>
                                                <div class="col-md-12 mt-step-col" style="background-color:#bddcdf">
                                                    <div class="mt-step-number first bg-white font-grey-cascade">
                                                        <asp:Label ID="LblAllContacts" runat="server" />
                                                    </div>
                                                    <div class="mt-step-title uppercase font-grey-cascade" style="font-size: 18px; margin-top:10px;">
                                                        <a id="EditStatusOne" style="float:left;" onserverclick="EditStatusOne_OnClick" runat="server">
                                                            <i id="iEditStatusOne" class="fa fa-edit" title="Change opportunity status description" runat="server"></i>                                                        
                                                        </a>
                                                        <a id="DeleteStatusOne" style="float:left;" onserverclick="DeleteStatusOne_OnCick" visible="false" runat="server">
                                                            <i id="iDeleteStatusOne" class="fa fa-times" title="Delete custom and use default" runat="server"></i>
                                                        </a> 
                                                        <div style="float:left; margin-left:2px;">
                                                            <asp:Label ID="LblStatusOne" Text="Contact" ForeColor="White" runat="server" />
                                                        </div>
                                                        <div style="float:left; margin-left:2px;">
                                                            <asp:TextBox ID="TbxStatusOne" Visible="false" Width="95" runat="server" />
                                                        </div>                                                   
                                                        <div style="float:right;">
                                                            <asp:Label ID="Label6" Text=">>" ForeColor="White" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="mt-step-content font-grey-cascade">
                                                        <%--View-Edit your Contacts list--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row step-thin form-group" style="margin-top:10px">
                                                <asp:DropDownList ID="DdlContactSort" runat="server">
                                                    <asp:ListItem Value="0" Text="Order by date" Selected="True" />
                                                    <asp:ListItem Value="1" Text="Order by title" />
                                                    <asp:ListItem Value="2" Text="Order by notes" />
                                                    <asp:ListItem Value="3" Text="Order by tasks" />
                                                </asp:DropDownList>
                                                <asp:Button ID="BtnContactSort" OnClick="BtntContactSort_OnClick" CssClass="btn btn-circle btn-sm btn-primary" runat="server" Text="Sort" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="caption" style="width:100%;">                                        
                                        <%--<div class="classname" style="max-width:240px">
                                            <asp:DropDownList AutoPostBack="true" Visible="false" ID="DdlContactCounts" OnTextChanged="DdlContactCounts_OnTextChanged" runat="server"> f5bec1                                                  
                                            </asp:DropDownList>--%>
                                            <telerik:RadGrid ID="RdgContacts" AllowPaging="true" Width="240" PageSize="10" CssClass="rgdd" AlternatingItemStyle-BackColor="#bddcdf" ItemStyle-BackColor="#bddcdf" SelectedItemStyle-BackColor="YellowGreen" ShowHeader="false" PagerStyle-Position="Bottom" HeaderStyle-ForeColor="#ffffff" OnNeedDataSource="RdgContacts_OnNeedDataSource" OnItemDataBound="RdgContacts_OnItemDataBound" AutoGenerateColumns="false" runat="server">
                                                <MasterTableView>
                                                    <NoRecordsTemplate>
                                                        <div class="emptyGridHolder">
                                                            <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                        </div>
                                                    </NoRecordsTemplate>
                                                    <Columns> 
                                                        <telerik:GridBoundColumn Display="false" DataField="opportId" UniqueName="opportId" />
                                                        <telerik:GridBoundColumn Display="false" DataField="opportOrganName" UniqueName="opportOrganName" />
                                                        <telerik:GridBoundColumn Display="false" DataField="opportNotes" UniqueName="opportNotes" /> 
                                                        <telerik:GridBoundColumn Display="false" DataField="opportTasks" UniqueName="opportTasks" />
                                                        <telerik:GridBoundColumn Display="false" DataField="opportDate" UniqueName="opportDate" />
                                                        <telerik:GridBoundColumn Display="false" DataField="organizationUserId" UniqueName="organizationUserId" />
                                                        <telerik:GridBoundColumn Display="false" DataField="companyLogo" UniqueName="companyLogo" />
                                                        <telerik:GridTemplateColumn DataField="actions" UniqueName="actions">
                                                            <ItemTemplate>                                                                                                                                                               
                                                                <div style="float:right">  
                                                                    <li id="liUserOptions" runat="server" class="dropdown dropdown-user">
                                                                        <a class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                                                                            <asp:Image ID="ImgUserPhoto" CssClass="img-circle" ImageUrl="~/images/icons/settings.png" runat="server" />  
                                                                            <span class="username username-hide-on-mobile"></span>
                                                                        </a>
                                                                        <ul class="dropdown-menu dropdown-menu-default" style="background-color:#bddcdf !important; min-width:120px !important; text-align: center;">
                                                                            <li>
                                                                                <a id="aDelete" onserverclick="BtnConfirm_OnClick" runat="server">
                                                                                    <i id="iDelete" class="" runat="server"></i>
                                                                                    <asp:Label ID="LblDelete" Text="delete" runat="server" />
                                                                                    <asp:Image ID="ImgBtnDelete" Visible="false" ImageUrl="/images/icons/small/delete.png" runat="server" />
                                                                                </a> 
                                                                            </li>
                                                                            <li>
                                                                                <a id="aMoveRight" onserverclick="BtnConfirm_OnClick" runat="server">
                                                                                    <i id="iMoveRight" class="" runat="server"></i>
                                                                                    <asp:Label ID="Label1" Text="move" runat="server" />
                                                                                    <asp:Image ID="ImgBtnMoveRight" Visible="false" ImageUrl="/images/icons/small/move_right_1.png" runat="server" />
                                                                                </a>
                                                                            </li>
                                                                            <li>
                                                                                <a id="aEditOpportunity" runat="server">
                                                                                    <asp:Label ID="LblEditOpportunity" Text="view/edit" CssClass="" runat="server" />
                                                                                    <asp:Image ID="ImgEditOpportunity" Visible="false" CssClass="settClass" ImageUrl="/images/settings.png" runat="server" />
                                                                                </a> 
                                                                            </li>                                        
                                                                        </ul>
                                                                    </li>                                            
                                                                    
                                                                </div>
                                                                <asp:Image ID="Img1" CssClass="imgClass" Width="35" Height="35" style="border-radius:20px !important;" ImageUrl="/images/sidebar_inline_toggler_icon_grey.jpg" runat="server" />
                                                                <asp:Label ID="LblOpportunityId" Visible="false" runat="server" />
                                                                <asp:Label ID="LblCompanyname" CssClass="nameClass" runat="server" />
                                                                <asp:HyperLink ID="HpNotes" CssClass="noteClass" runat="server" />
                                                                <asp:HyperLink ID="HpTasks" CssClass="taskClass" runat="server" />
                                                                                                                                 
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>                                             
                                            
                                        <%--</div> --%>                              
                                    </div>                           
                                </div>
                            </div>                        
                        </div>
                        <div class="col-md-3 col-sm-3 col-xs-6">
                            <div class="portlet light portlet-fit bordered" style="border: 0px solid #e7ecf1 !important;">
                                <div class="portlet-title" style="padding:0px;">                            
                                    <div class="portlet-body">
                                        <div class="mt-element-step">
                                            <div class="row step-thin">
                                                <%--f2cf8d--%>
                                                <div class="col-md-12 mt-step-col" style="background-color:#a4d3d7">
                                                    <div class="mt-step-number first bg-white font-grey-cascade">
                                                        <asp:Label ID="LblAllMeeting" runat="server" />
                                                    </div>
                                                    <div class="mt-step-title uppercase font-grey-cascade" style="font-size: 18px; margin-top:10px;">
                                                        <a id="EditStatusTwo" style="float:left;" onserverclick="EditStatusTwo_OnClick" runat="server">
                                                            <i id="iEditStatusTwo" class="fa fa-edit" title="Change opportunity status description" runat="server"></i>                                                        
                                                        </a>
                                                        <a id="DeleteStatusTwo" style="float:left;" onserverclick="DeleteStatusTwo_OnCick" visible="false" runat="server">
                                                            <i id="iDeleteStatusTwo" class="fa fa-times" title="Delete custom and use default" runat="server"></i>
                                                        </a> 
                                                        <div style="float:left; margin-left:2px;">
                                                            <asp:Label ID="LblStatusTwo" Text="Meeting" ForeColor="White" runat="server" />
                                                        </div>
                                                        <div style="float:left; margin-left:2px;">
                                                            <asp:TextBox ID="TbxStatusTwo" Visible="false" Width="95" runat="server" />
                                                        </div>
                                                     
                                                        <div style="float:right;">
                                                            <asp:Label ID="Label7" Text=">>" ForeColor="White" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="mt-step-content font-grey-cascade">
                                                        <%--View-Edit your Meeting-Demo list--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row step-thin form-group" style="margin-top:10px">
                                                <asp:DropDownList ID="DdlMeetingSort" runat="server">
                                                    <asp:ListItem Value="0" Text="Order by date" Selected="True" />
                                                    <asp:ListItem Value="1" Text="Order by title" />
                                                    <asp:ListItem Value="2" Text="Order by notes" />
                                                    <asp:ListItem Value="3" Text="Order by tasks" />
                                                </asp:DropDownList>
                                                <asp:Button ID="BtnMeetingSort" OnClick="BtntMeetingSort_OnClick" CssClass="btn btn-circle btn-sm btn-primary" runat="server" Text="Sort" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="caption" style="width:100%;">
                                        <%--<div class="classname" style="max-width:240px">                                            
                                            <asp:DropDownList AutoPostBack="true" Visible="false" ID="DdlMeetingCount" OnTextChanged="DdlMeetingCount_OnTextChanged" runat="server"> f4e2bf                                                   
                                            </asp:DropDownList>--%>
                                            <telerik:RadGrid ID="RdgMeeting" AllowPaging="true" PageSize="10" Width="240" CssClass="rgdd" AlternatingItemStyle-BackColor="#a4d3d7" ItemStyle-BackColor="#a4d3d7" ShowHeader="false" PagerStyle-Position="Bottom" HeaderStyle-ForeColor="#ffffff" OnNeedDataSource="RdgMeeting_OnNeedDataSource" OnItemDataBound="RdgMeeting_OnItemDataBound" AutoGenerateColumns="false" runat="server">
                                                <MasterTableView>
                                                    <NoRecordsTemplate>
                                                        <div class="emptyGridHolder">
                                                            <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                        </div>
                                                    </NoRecordsTemplate>
                                                    <Columns> 
                                                        <telerik:GridBoundColumn Display="false" DataField="opportId" UniqueName="opportId" />
                                                        <telerik:GridBoundColumn Display="false" DataField="opportOrganName" UniqueName="opportOrganName" />
                                                        <telerik:GridBoundColumn Display="false" DataField="opportNotes" UniqueName="opportNotes" /> 
                                                        <telerik:GridBoundColumn Display="false" DataField="opportTasks" UniqueName="opportTasks" />                                                            
                                                        <telerik:GridBoundColumn Display="false" DataField="opportDate" UniqueName="opportDate" />
                                                        <telerik:GridBoundColumn Display="false" DataField="organizationUserId" UniqueName="organizationUserId" />
                                                        <telerik:GridBoundColumn Display="false" DataField="companyLogo" UniqueName="companyLogo" />
                                                        <telerik:GridTemplateColumn DataField="actions" UniqueName="actions">
                                                            <ItemTemplate>                                                                
                                                                <div style="float:right">
                                                                    <li id="liUserOptions" runat="server" class="dropdown dropdown-user">
                                                                        <a class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                                                                            <asp:Image ID="ImgUserPhoto" CssClass="img-circle" ImageUrl="~/images/icons/settings.png" runat="server" />  
                                                                            <span class="username username-hide-on-mobile"></span>                                                                            
                                                                        </a>
                                                                        <ul class="dropdown-menu dropdown-menu-default" style="background-color:#a4d3d7 !important; min-width:120px !important; text-align: center;">
                                                                            <li>
                                                                                <a id="aMoveLeft" onserverclick="BtnConfirm_OnClick" runat="server">
                                                                                    <i id="iMoveLeft" class="" runat="server"></i>
                                                                                    <asp:Label ID="LblDelete" Text="move left" runat="server" />
                                                                                    <asp:Image ID="ImgBtnMoveLeft" Visible="false" ImageUrl="/images/icons/small/move_left_1.png" runat="server" />
                                                                                </a>
                                                                            </li>
                                                                            <li>
                                                                                <a id="aDelete" onserverclick="BtnConfirm_OnClick" runat="server">
                                                                                    <i id="iDelete" class="" runat="server"></i>
                                                                                    <asp:Label ID="Label2" Text="delete" runat="server" />
                                                                                    <asp:Image ID="ImgBtnDelete" Visible="false" ImageUrl="/images/icons/small/delete.png" runat="server" />
                                                                                </a>
                                                                            </li>
                                                                            <li>
                                                                                <a id="aMoveRight" onserverclick="BtnConfirm_OnClick" runat="server">
                                                                                    <i id="iMoveRight" class="" runat="server"></i>
                                                                                    <asp:Label ID="Label4" Text="move right" runat="server" />
                                                                                    <asp:Image ID="ImgBtnMoveRight" Visible="false" ImageUrl="/images/icons/small/move_right_1.png" runat="server" />
                                                                                </a> 
                                                                            </li> 
                                                                            <li>
                                                                                <a id="aEditOpportunity" runat="server">
                                                                                    <asp:Label ID="LblEditOpportunity" Text="view/edit" CssClass="" runat="server" />
                                                                                    <asp:Image ID="ImgEditOpportunity" Visible="false" CssClass="" ImageUrl="/images/settings.png" runat="server" />
                                                                                </a>
                                                                            </li>                                       
                                                                        </ul>
                                                                    </li>
                                                                </div>
                                                                <asp:Image ID="Img1" CssClass="imgClass" Width="35" Height="35" style="border-radius:20px !important;" ImageUrl="/images/sidebar_inline_toggler_icon_grey.jpg" runat="server" />
                                                                <asp:Label ID="LblOpportunityId" Visible="false" runat="server" />
                                                                <asp:Label ID="LblCompanyname" CssClass="nameClass" runat="server" />
                                                                <asp:HyperLink ID="HpNotes" CssClass="noteClass" runat="server" />
                                                                <asp:HyperLink ID="HpTasks" CssClass="taskClass" runat="server" />                                                                
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                            
                                        <%--</div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-xs-6">
                            <div class="portlet light portlet-fit bordered" style="border: 0px solid #e7ecf1 !important;">
                                <div class="portlet-title" style="padding:0px;">                            
                                    <div class="portlet-body">
                                        <div class="mt-element-step">
                                            <div class="row step-thin">
                                                <%--c7e47a--%>
                                                <div class="col-md-12 mt-step-col" style="background-color:#86cdd3">
                                                    <div class="mt-step-number first bg-white font-grey-cascade">
                                                        <asp:Label ID="LblAllProposal" runat="server" />
                                                    </div>
                                                    <div class="mt-step-title uppercase font-grey-cascade" style="font-size:18px; margin-top:10px;">
                                                        <a id="EditStatusThree" style="float:left;" onserverclick="EditStatusThree_OnClick" runat="server">
                                                            <i id="iEditStatusThree" class="fa fa-edit" title="Change opportunity status description" runat="server"></i>                                                        
                                                        </a> 
                                                        <a id="DeleteStatusThree" style="float:left;" onserverclick="DeleteStatusThree_OnCick" visible="false" runat="server">
                                                            <i id="iDeleteStatusThree" class="fa fa-times" title="Delete custom and use default" runat="server"></i>
                                                        </a> 
                                                        <div style="float:left; margin-left:2px;">
                                                            <asp:Label ID="LblStatusThree" Text="Proposal" ForeColor="White" runat="server" />
                                                        </div>
                                                        <div style="float:left; margin-left:2px;">
                                                            <asp:TextBox ID="TbxStatusThree" Visible="false" Width="95" runat="server" />
                                                        </div>
                                                        <div style="float:right;">
                                                            <asp:Label ID="Label8" Text=">>" ForeColor="White" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="mt-step-content font-grey-cascade">
                                                        <%--View-Edit your Proposal list--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row step-thin form-group" style="margin-top:10px">
                                                <asp:DropDownList ID="DdlProposalSort" runat="server">
                                                    <asp:ListItem Value="0" Text="Order by date" Selected="True" />
                                                    <asp:ListItem Value="1" Text="Order by title" />
                                                    <asp:ListItem Value="2" Text="Order by notes" />
                                                    <asp:ListItem Value="3" Text="Order by tasks" />
                                                </asp:DropDownList>
                                                <asp:Button ID="BtnProposalSort" OnClick="BtntProposalSort_OnClick" CssClass="btn btn-circle btn-sm btn-primary" runat="server" Text="Sort" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="caption" style="width:100%;">
                                        <%--<div class="classname" style="max-width:240px">
                                            <asp:DropDownList AutoPostBack="true" Visible="false" ID="DdlProposalCount" OnTextChanged="DdlProposalCount_OnTextChanged" runat="server">d7e7ad                                                   
                                            </asp:DropDownList>--%>
                                            <telerik:RadGrid ID="RdgProposal" AllowPaging="true" Width="240" PageSize="10" CssClass="rgdd" AlternatingItemStyle-BackColor="#86cdd3" ItemStyle-BackColor="#86cdd3" ShowHeader="false" PagerStyle-Position="Bottom" HeaderStyle-ForeColor="#ffffff" OnNeedDataSource="RdgProposal_OnNeedDataSource" OnItemDataBound="RdgProposal_OnItemDataBound" AutoGenerateColumns="false" runat="server">
                                                <MasterTableView>
                                                    <NoRecordsTemplate>
                                                        <div class="emptyGridHolder">
                                                            <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                        </div>
                                                    </NoRecordsTemplate>
                                                    <Columns> 
                                                        <telerik:GridBoundColumn Display="false" DataField="opportId" UniqueName="opportId" />
                                                        <telerik:GridBoundColumn Display="false" DataField="opportOrganName" UniqueName="opportOrganName" />
                                                        <telerik:GridBoundColumn Display="false" DataField="opportNotes" UniqueName="opportNotes" /> 
                                                        <telerik:GridBoundColumn Display="false" DataField="opportTasks" UniqueName="opportTasks" />                                                            
                                                        <telerik:GridBoundColumn Display="false" DataField="opportDate" UniqueName="opportDate" />
                                                        <telerik:GridBoundColumn Display="false" DataField="organizationUserId" UniqueName="organizationUserId" />
                                                        <telerik:GridBoundColumn Display="false" DataField="companyLogo" UniqueName="companyLogo" />
                                                        <telerik:GridTemplateColumn DataField="actions" UniqueName="actions">
                                                            <ItemTemplate>
                                                                <div style="float:right">
                                                                    <li id="liUserOptions" runat="server" class="dropdown dropdown-user">
                                                                        <a class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                                                                            <asp:Image ID="ImgUserPhoto" CssClass="img-circle" ImageUrl="~/images/icons/settings.png" runat="server" />                                
                                                                            <span class="username username-hide-on-mobile"></span>                                                                            
                                                                        </a>
                                                                        <ul class="dropdown-menu dropdown-menu-default" style="background-color:#86cdd3 !important; min-width:120px !important; text-align: center;">
                                                                            <li>
                                                                                <a id="aMoveLeft" onserverclick="BtnConfirm_OnClick" runat="server">
                                                                                    <i id="iMoveLeft" class="" runat="server"></i>
                                                                                    <asp:Label ID="LblDelete" Text="move left" runat="server" />
                                                                                    <asp:Image ID="ImgBtnMoveLeft" Visible="false" ImageUrl="/images/icons/small/move_left_1.png" runat="server" />
                                                                                </a>                                                                               
                                                                            </li>
                                                                            <li>
                                                                                <a id="aDelete" onserverclick="BtnConfirm_OnClick" runat="server">
                                                                                    <i id="iDelete" class="" runat="server"></i>
                                                                                    <asp:Label ID="Label10" Text="delete" runat="server" />
                                                                                    <asp:Image ID="ImgBtnDelete" Visible="false" ImageUrl="/images/icons/small/delete.png" runat="server" />
                                                                                </a>
                                                                            </li>
                                                                            <li>
                                                                                <a id="aMoveRight" onserverclick="BtnConfirm_OnClick" runat="server">
                                                                                    <i id="iMoveRight" class="" runat="server"></i>
                                                                                    <asp:Label ID="Label11" Text="move right" runat="server" />
                                                                                    <asp:Image ID="ImgBtnMoveRight" Visible="false" ImageUrl="/images/icons/small/move_right_1.png" runat="server" />
                                                                                </a>
                                                                            </li>                                                                             
                                                                            <li>
                                                                                <a id="aEditOpportunity" runat="server">
                                                                                    <asp:Label ID="LblEditOpportunity" Text="view/edit" CssClass="" runat="server" />
                                                                                    <asp:Image ID="ImgEditOpportunity" Visible="false" CssClass="" ImageUrl="/images/settings.png" runat="server" />
                                                                                </a>
                                                                            </li>                                       
                                                                        </ul>
                                                                    </li>
                                                                </div>
                                                                <asp:Image ID="Img1" CssClass="imgClass" Width="35" Height="35" style="border-radius:20px !important;" ImageUrl="/images/sidebar_inline_toggler_icon_grey.jpg" runat="server" />
                                                                <asp:Label ID="LblOpportunityId" Visible="false" runat="server" />
                                                                <asp:Label ID="LblCompanyname" CssClass="nameClass" runat="server" />
                                                                <asp:HyperLink ID="HpNotes" CssClass="noteClass" runat="server" />
                                                                <asp:HyperLink ID="HpTasks" CssClass="taskClass" runat="server" />
                                                                
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                            
                                        <%--</div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-xs-6">                        
                            <div class="portlet light portlet-fit bordered" style="border: 0px solid #e7ecf1 !important;">
                                <div class="portlet-title" style="padding:0px;">                            
                                    <div class="portlet-body">
                                        <div class="mt-element-step">
                                            <div class="row step-thin">
                                                <%--f3df94--%>
                                                <div class="col-md-12 mt-step-col" style="background-color:#32c5d2">
                                                    <div class="mt-step-number first bg-white font-grey-cascade">
                                                        <asp:Label ID="LblAllClosed" runat="server" />
                                                    </div>
                                                    <div class="mt-step-title uppercase font-grey-cascade" style="font-size: 18px; margin-top:10px;">
                                                        <a id="EditStatusFour" style="float:left;" onserverclick="EditStatusFour_OnClick" runat="server">
                                                            <i id="iEditStatusFour" class="fa fa-edit" title="Change opportunity status description" runat="server"></i>                                                        
                                                        </a> 
                                                        <a id="DeleteStatusFour" style="float:left;" onserverclick="DeleteStatusFour_OnCick" visible="false" runat="server">
                                                            <i id="iDeleteStatusFour" class="fa fa-times" title="Delete custom and use default" runat="server"></i>
                                                        </a> 
                                                        <div style="float:left; margin-left:2px;">
                                                            <asp:Label ID="LblStatusFour" Text="Closed" ForeColor="White" runat="server" />
                                                        </div>
                                                        <div style="float:left; margin-left:2px;">
                                                            <asp:TextBox ID="TbxStatusFour" Visible="false" Width="100" runat="server" />
                                                        </div>
                                                        <div style="float:right;">
                                                            <asp:Label ID="Label9" Text=">>" ForeColor="White" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="mt-step-content font-grey-cascade">
                                                        <%--View-Edit your Closed list--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row step-thin form-group" style="margin-top:10px">
                                                <asp:DropDownList ID="DdlClosedSort" runat="server">
                                                    <asp:ListItem Value="0" Text="Order by date" Selected="True" />
                                                    <asp:ListItem Value="1" Text="Order by title" />
                                                    <asp:ListItem Value="2" Text="Order by notes" />
                                                    <asp:ListItem Value="3" Text="Order by tasks" />
                                                </asp:DropDownList>
                                                <asp:Button ID="BtnClosedSort" OnClick="BtntClosedSort_OnClick" CssClass="btn btn-circle btn-sm btn-primary" runat="server" Text="Sort" />
                                            </div>
                                            <div class="row step-thin form-group" style="margin-top:10px">
                                                <asp:Label ID="LblClosedSubStatusText" runat="server" />
                                                <asp:DropDownList AutoPostBack="true" ID="DdlClosedSubStatusSort" OnSelectedIndexChanged="DdlClosedSubStatusSort_OnSelectedIndexChanged" runat="server" />
                                                <asp:Button ID="BtnClosedSubStatusSort" Visible="false" OnClick="BtnClosedSubStatusSort_OnClick" CssClass="btn btn-circle btn-sm btn-primary" runat="server" Text="Sort" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="caption" style="width:100%;">
                                        <%--<div class="classname" style="max-width:240px">     
                                            <asp:DropDownList AutoPostBack="true" Visible="false" ID="DdlClosedCount" OnTextChanged="DdlClosedCount_OnTextChanged" runat="server">f5eccb                                                   
                                            </asp:DropDownList>--%>
                                            <telerik:RadGrid ID="RdgClosed" AllowPaging="true" PageSize="7" Width="240" CssClass="rgdd" AlternatingItemStyle-BackColor="#32c5d2" ItemStyle-BackColor="#32c5d2" ShowHeader="false" PagerStyle-Position="Bottom" HeaderStyle-ForeColor="#ffffff" OnNeedDataSource="RdgClosed_OnNeedDataSource" OnItemDataBound="RdgClosed_OnItemDataBound" AutoGenerateColumns="false" runat="server">
                                                <MasterTableView>
                                                    <NoRecordsTemplate>
                                                        <div class="emptyGridHolder">
                                                            <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                        </div>
                                                    </NoRecordsTemplate>
                                                    <Columns> 
                                                        <telerik:GridBoundColumn Display="false" DataField="opportId" UniqueName="opportId" />
                                                        <telerik:GridBoundColumn Display="false" DataField="opportOrganName" UniqueName="opportOrganName" />
                                                        <telerik:GridBoundColumn Display="false" DataField="opportNotes" UniqueName="opportNotes" /> 
                                                        <telerik:GridBoundColumn Display="false" DataField="opportTasks" UniqueName="opportTasks" />                                                            
                                                        <telerik:GridBoundColumn Display="false" DataField="opportDate" UniqueName="opportDate" />
                                                        <telerik:GridBoundColumn Display="false" DataField="organizationUserId" UniqueName="organizationUserId" />
                                                        <telerik:GridBoundColumn Display="false" DataField="companyLogo" UniqueName="companyLogo" />
                                                        <telerik:GridTemplateColumn DataField="actions" UniqueName="actions">
                                                            <ItemTemplate>
                                                                <div style="float:right">
                                                                    <li id="liUserOptions" runat="server" class="dropdown dropdown-user">
                                                                        <a class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                                                                            <asp:Image ID="ImgUserPhoto" CssClass="img-circle" ImageUrl="~/images/icons/settings.png" runat="server" />                                
                                                                            <span class="username username-hide-on-mobile"></span>
                                                                            
                                                                        </a>
                                                                        <ul class="dropdown-menu dropdown-menu-default" style="background-color:#32c5d2 !important; min-width:120px !important; text-align: center;">
                                                                            <li>
                                                                                <a id="aMoveLeft" onserverclick="BtnConfirm_OnClick" runat="server">
                                                                                    <i id="iMoveLeft" class="" runat="server"></i>
                                                                                    <asp:Label ID="Label5" Text="move left" CssClass="" runat="server" />
                                                                                    <asp:Image ID="ImgBtnMoveLeft" Visible="false" ImageUrl="/images/icons/small/move_left_1.png" runat="server" />
                                                                                </a>                                                                             
                                                                            </li>
                                                                            <li>
                                                                                <a id="aDelete" onserverclick="BtnConfirm_OnClick" runat="server">
                                                                                    <i id="iDelete" class="" runat="server"></i>
                                                                                    <asp:Label ID="Label12" Text="delete" CssClass="" runat="server" />
                                                                                    <asp:Image ID="ImgBtnDelete" Visible="false" ImageUrl="/images/icons/small/delete.png" runat="server" />
                                                                                </a>
                                                                            </li>
                                                                            <li>
                                                                                <a id="aMoveRight" visible="false" runat="server">
                                                                                    <i id="iMoveRight" class="" runat="server"></i>
                                                                                    <asp:Label ID="Label11" Text="move right" runat="server" />
                                                                                    <asp:Image ID="ImgBtnMoveRight" Visible="false" ImageUrl="/images/icons/small/move_right_1.png" runat="server" />
                                                                                </a>
                                                                            </li> 
                                                                            <li>
                                                                                <a id="aEditOpportunity" runat="server">
                                                                                    <asp:Label ID="LblEditOpportunity" Text="view/edit" CssClass="" runat="server" />
                                                                                    <asp:Image ID="ImgEditOpportunity" Visible="false" CssClass="" ImageUrl="/images/settings.png" runat="server" />
                                                                                </a>
                                                                            </li>                                                                                                           
                                                                        </ul>
                                                                    </li>
                                                                </div>
                                                                <asp:Image ID="Img1" CssClass="imgClass" Width="35" Height="35" style="border-radius:20px !important;" ImageUrl="/images/sidebar_inline_toggler_icon_grey.jpg" runat="server" />
                                                                <asp:Label ID="LblOpportunityId" Visible="false" runat="server" />
                                                                <asp:Label ID="LblCompanyname" CssClass="nameClass" runat="server" />
                                                                <asp:HyperLink ID="HpNotes" CssClass="noteClass" runat="server" />
                                                                <asp:HyperLink ID="HpTasks" CssClass="taskClass" runat="server" />
                                                                <asp:Label ID="LblSubStatus" runat="server" />
                                                                <telerik:RadComboBox ID="RcbxSubStatus" runat="server" />
                                                                <asp:ImageButton ID="ImgBtnSaveSub" OnClick="ImgBtnSaveSub_OnClick" runat="server" />
                                                                
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                            
                                        <%--</div>--%>
                                    </div>
                                </div>
                            </div>                        
                        </div>
                    </div>
                </div>
            </div>
            <div id="loader" style="display:none;">
                <div id="loadermsg">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="divConfirm" class="modal fade" tabindex="-1" data-width="300">
        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                            <h4 class="modal-title"><asp:Label ID="LblConfTitle" Text="Confirmation" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="LblConfMsg" CssClass="control-label" runat="server" />
                                        <asp:TextBox ID="TbxOpportConfId" CssClass="hidden" runat="server" />
                                        <asp:TextBox ID="TbxOpportAction" CssClass="hidden" runat="server" />                                        
                                    </div>                                    
                                </div>                            
                            </div>                    
                        </div>
                        <div class="modal-footer">                            
                            <button type="button" data-dismiss="modal" class="btn dark btn-outline">Back</button>
                            <asp:Button ID="BtnBack" Text="Back" Visible="false" OnClick="BtnBack_OnClick" CssClass="btn dark btn-outline" runat="server" />
                            <asp:Button ID="BtnSave" OnClick="BtnSave_OnClick" CssClass="btn red-sunglo" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script type="text/javascript">
        function OpenConfPopUp() {
            $('#divConfirm').modal('show');
        }
    </script>
    <script type="text/javascript">
        function CloseConfPopUp() {
            $('#divConfirm').modal('hide');
        }
    </script>
</asp:Content>