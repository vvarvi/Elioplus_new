<%@ Page Title="" Language="C#" MasterPageFile="~/ElioplusMaster.Master" AutoEventWireup="true" CodeBehind="ChannelPartnerLocatorWithMaster.aspx.cs" Inherits="WdS.ElioPlus.ChannelPartnerLocatorWithMaster" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="PartnerLocatorHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <script src="/js/carousel-slider/jquery-1.12.0.min.js"></script>
    <link rel="stylesheet" href="/assets/css/styles.css" />
</asp:Content>
<asp:Content ID="PartnerLocatorMain" ContentPlaceHolderID="MainContent" runat="server">
    <main role="main" id="mainForm">
        <div class="head-block">
            <div class="container">
                <div class="row">
                    <div class="col-xs-20 col-md-14 col-lg-12">
                        <h1>Elioplus Partner Locator
                        </h1>
                        <div class="selection-block">
                            <div class="form-group has-feedback input-block">
                                <asp:TextBox ID="tbxCompanyOrAddress" CssClass="form-control search-input" placeholder="Search by company name or address..." 
                                     EnableViewState="true" runat="server" />                                
                                <a id="aBtnSearch" onserverclick="aBtnSearch_OnClick" runat="server">
                                    <ul>
                                        <i class="glyphicon glyphicon-search form-control-feedback"></i>
                                    </ul>
                                </a>
                            </div>
                            <div class="dropdown">                                
                                <asp:DropDownList ID="DdlCountries" runat="server" OnSelectedIndexChanged="DdlCountries_SelectedIndexChanged" CssClass="ddl-selection" AutoPostBack="true" EnableViewState="true" />
                            </div>
                        </div>                    
                    </div>
                </div>
            </div>
        </div>
        <div class="content-block">
            <div class="container">
                <div class="row">
                    <div class="col-xs-22">
                        <div class="table-content">
                            <asp:Repeater ID="Partners" runat="server">
                                <ItemTemplate>
                                    <asp:HiddenField ID="HdnPartnerID" runat="server" Value='<%# Eval("Id") %>' Visible="false" />
                                    <div class="item">
                                        <div class="logo-td">                                                                                
                                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Logo") %>' />
                                            <a id="aCompanyLogo" visible="false" href="javascript:;" target="_blank" runat="server" style="">
                                            </a>
                                        </div>
                                        <div class="name-td">
                                            <p class="company-name">                                                
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                <a id="aCompanyName" visible="false" href="javascript:;" target="_blank" runat="server" style="">
                                                </a>
                                            </p>
                                            <p class="address">
                                                <i class="fa fa-map-marker"></i>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                            </p>
                                        </div>
                                        <div class="contact-td">
                                            <p class="site">
                                                <i class="fa fa-globe"></i>
                                                <a href='<%# Eval("Website") %>' target="_blank" runat="server">website</a>
                                            </p>
                                            <p class="phone">
                                                <i class="fa fa-phone"></i>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                                            </p>
                                        </div>
                                        <div class="info-td">
                                            <p class="status">
                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                            </p>
                                            <p class="cert">
                                                
                                            </p>
                                        </div>
                                    </div>                                    
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />
                    </div>
                </div>
            </div>            
        </div>
    </main>
</asp:Content>
