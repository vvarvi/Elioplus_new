<%@ Page Title="" Language="C#" MasterPageFile="~/ElioplusMaster.Master" AutoEventWireup="true" CodeBehind="PartnerPortalLocator.aspx.cs" Inherits="WdS.ElioPlus.PartnerPortalLocator" %>

<asp:Content ID="PartnerLocatorHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Browse thousands of partner portals or create your own to manage and automate your partner network." />
    <meta name="keywords" content="partner portals, browse partner portals" />
    <link rel="stylesheet" href="/assets/css/styles.css" />
</asp:Content>
<asp:Content ID="PartnerLocatorMain" ContentPlaceHolderID="MainContent" runat="server">
    <main role="main" id="mainForm">
        <div class="head-block">
            <div class="container">
                <div class="row">
                    <div class="col-xs-20 col-md-14 col-lg-12">
                        <h1>Browse Partner Portals</h1>
                        <p class="after-title">
                            Search for an IT partner portal, log in or sign and start collaborating.<br/>
                            Unify all your tech partnerships in a single place and automate your deals and reporting.
                        </p>                        
                    </div>
                </div>
            </div>
        </div>
        <div class="content-block">
            <div class="container">
                <div class="row text-center s-center margin-bottom-40">
                    <div class="col-md-2"></div>
                    <div class="col-md-8">
                        <div class="input-group">
                            <asp:TextBox ID="tbxCompany" CssClass="form-control" Height="50px" BackColor="White" ForeColor="Black" placeholder="Search by company name..."
                                runat="server" />
                            <div class="input-group-btn">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary nwx-small-btn" Height="50px" Text="SEARCH" OnClick="BtnSearch_OnClick" />                                
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                <div class="row text-center" id="noResultMsg" visible="false" runat="server">
                    <div class="col-md-3"></div>
                    <div class="col-md-6 alert alert-info layout-align-center" role="alert">
                        <b>There are no available results for this name</b>
                    </div>
                    <div class="col-md-3"></div>
                </div>
                <div class="row text-center" id="minCharMsg" visible="false" runat="server">
                    <div class="col-md-3"></div>
                    <div class="col-md-6 alert alert-info layout-align-center" role="alert">
                        <b>Name is too short! Try at least 2 characters</b>
                    </div>
                    <div class="col-md-3"></div>
                </div>
                <div class="row md-margin" id="navBtns" visible="false" runat="server">
                    <div class="float-left md-padding-1">
                        <asp:Button ID="lbtnPrev" runat="server" CssClass="btn btn-cta-secondary input-xsmall" Text="previous" OnClick="lbtnPrev_Click" />
                    </div>
                    <div class="float-right md-padding-1">
                        <asp:Button ID="lbtnNext" runat="server" CssClass="btn btn-cta-secondary small" Width="80px" Text="next" OnClick="lbtnNext_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-22">
                        <div class="table-content">
                            <asp:Repeater ID="Partners" runat="server">
                                <ItemTemplate>
                                    <div>
                                        <asp:HiddenField ID="HdnCompanyID" Value='<%# Eval("Id") %>' runat="server" />
                                    </div>
                                    <div class="item">
                                        <div class="logo-td">
                                            <asp:Image ID="ImgCompanyLogo" runat="server" ImageUrl='<%# Eval("CompanyLogo") %>' />
                                        </div>
                                        <div class="name-td">
                                            <p class="company-name">
                                                <asp:Label ID="LblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>'></asp:Label>
                                            </p>
                                            <p class="address">
                                                <asp:Label ID="LblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                            </p>
                                        </div>
                                        <div class="contact-td">
                                            <p class="phone">
                                                <asp:Label ID="LblCountry" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
                                            </p>
                                            <p class="phone">
                                                <asp:Label ID="LblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                            </p>
                                        </div>
                                        <div class="info-td">
                                            <p class="status">
                                                <asp:Label ID="LblOverview" runat="server" Text='<%# Eval("Overview") %>'></asp:Label>
                                            </p>
                                            <p class="status">                                                
                                                <a class="btn btn-primary small" href='<%# Eval("WebSite") %>'>Visit Partner Portal</a>
                                            </p>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <div class="row md-margin" id="navBtnsBottom" visible="false" runat="server">
                    <div class="float-left md-padding-1">
                        <asp:Button ID="lbtnPrevBottom" runat="server" CssClass="btn btn-cta-secondary input-xsmall" Text="previous" OnClick="lbtnPrev_Click" />
                    </div>
                    <div class="float-right md-padding-1">
                        <asp:Button ID="lbtnNextBottom" runat="server" CssClass="btn btn-cta-secondary small" Width="80px" Text="next" OnClick="lbtnNext_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="content-text-block">
            <div class="container">
                <div class="row">
                    <div class="col-md-4">
                        <p class="text-center input-large bold">1</p>
                        <p class="md-padding-1">Search for a partner portal using the company name</p>
                    </div>
                    <div class="col-md-4">
                        <p class="text-center input-large bold">2</p>
                        <p class="md-padding-1">Use the link to visit the branded partner portal</p>
                    </div>
                    <div class="col-md-4">
                        <p class="text-center input-large bold">3</p>
                        <p class="md-padding-1">Log in or sign up to start collaborating with your tech partners</p>
                    </div>
                </div>
                <div class="row">
                    <h2 class="text-center">What is a partner portal?</h2>
                    <p class="text-justify">
                        A partner portal is a software tool used by companies with a partner network to manage, optimize and automate their partnerships. 
                        A partner portal or PRM tool can be customized to fit specific needs based on the structure of a partner program or the benefits 
                        a vendor is offering. 
                    </p>
                    <p class="text-justify">
                        Typically, some of the features included in every partner portal include a resources page or content management system, 
                        deal registration and lead distribution, partner directory and locator and a collaboration tool. Depending on the PRM provider 
                        other features may be available to help businesses manage their partner recruitment, analytics and integrations with third party 
                        applications like CRMs.
                    </p>
                </div>                
                <div class="row margin-top-40">
                    <h2 class="text-center">Why is a partner portal important?</h2>
                    <p class="text-justify">
                        A partner portal can help channel managers to automate many of their channel tasks when it comes to reporting, 
                        approving deal registrations, providing marketing content and other material to their partners and give incentives to their network. 
                        Also, it helps a channel company to have a single source for all their tech partnerships and reporting needs and thus 
                        reducing their workload and increase their productivity. It is reported that a partner portal can <b>increase partner productivity 
                        and sales up to 15%.</b>
                    </p>                    
                </div>
                <div class="row margin-top-40">
                    <h2 class="text-center">Build your free partner portal</h2>
                    <div class="col-md-8">
                        <p>Create your own partner portal for free.</p>
                        <p>
                            <a id="aGetStarted" class="btn btn-cta btn-cta-primary" runat="server">
                                <asp:Label ID="LblGetStarted" Text="Create your Partner Portal" runat="server" />
                            </a>
                        </p>
                    </div>  
                    <div class="col-md-4">
                        <p>
                            Including features like:
                        </p>
                        <ul>
                            <li>Partner directory</li>
                            <li>Onboarding & content management</li>
                            <li>Deal registration</li>
                            <li>Lead distribution</li>
                            <li>Collaboration</li>
                            <li>Analytics</li>
                            <li>Partner locator</li>
                            <li>Customization and many more</li>
                        </ul>
                    </div>
                </div>
                <div class="row">
                    <p>
                        <b>Questions?</b> If you need to get in touch with someone from our team please reach us <a href="/contact-us">here.</a>
                    </p>
                </div>
            </div>
        </div>
        <div class="s-divider" style="width:100%"></div>
    </main>
    <script src="/js/carousel-slider/jquery-1.12.0.min.js"></script>    
</asp:Content>
