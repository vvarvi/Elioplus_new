<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="PartnerPortalLocatorPage.aspx.cs" Inherits="WdS.ElioPlus.pages.PartnerPortalLocatorPage" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Browse thousands of partner portals or create your own to manage and automate your partner network." />
    <meta name="keywords" content="partner portals, browse partner portals" />
</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <main class="transition-all duration-200">
        <section class="bg-gray">
            <div class="container">
                <div class="gap-40px lg:gap-60px flex flex-col justify-center items-center w-full">
                    <div class="gap-20px lg:gap-30px flex flex-col justify-center items-center w-full">
                        <div class="text-center gap-5px lg:gap-10px flex flex-col items-center w-full">
                            <h2 class="text-base lg:text-lg font-semibold text-blue">HOME/ PARTNER PORTALS</h2>
                            <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Search & Browse Partner Portals</h1>
                            <p class="text-base lg:text-body text-center max-w-full">
                                Search for an IT partner portal, log in or sign and start collaborating.
                  <br class='hidden lg:block'>
                                Unify all your tech partnerships in a single place and automate your deals and reporting.
                            </p>
                        </div>
                        <div class="flex justify-center">
                            <div class="flex gap-10px w-full lg:w-[750px] max-w-full pt-5px pr-5px pb-5px pl-15px bg-white shadow-card rounded-40px overflow-hidden">
                                <asp:TextBox ID="tbxCompany" runat="server" class="grow pl-15px rounded-30px outline-none" placeholder="Type a company name  to search for an available partner portal at Elioplus" />
                                <asp:Button ID="btnSearch" runat="server" Text="SEARCH" class="btn large font-bold text-xs xl:text-sm bg-blue text-white" type="submit" OnClick="BtnSearch_OnClick" />
                            </div>
                        </div>
                    </div>

                    <div class="w-full flex flex-col gap-30px">
                        <controls:MessageControl ID="UcMessageAlert" runat="server" />
                    </div>

                    <div class="w-full lg:w-[1010px] max-w-full flex flex-col gap-50px" id="navBtns" visible="false" runat="server">
                        <div class="float-left md-padding-1">
                            <asp:Button ID="lbtnPrev" runat="server" CssClass="btn btn-cta-secondary input-xsmall" Text="previous" OnClick="lbtnPrev_Click" />
                        </div>
                        <div class="float-right md-padding-1">
                            <asp:Button ID="lbtnNext" runat="server" CssClass="btn btn-cta-secondary small" Width="80px" Text="next" OnClick="lbtnNext_Click" />
                        </div>
                    </div>

                    <div class="w-full lg:w-[1010px] max-w-full flex flex-col gap-50px">
                        <asp:Repeater ID="Partners" runat="server">
                            <ItemTemplate>
                                <div>
                                    <asp:HiddenField ID="HdnCompanyID" Value='<%# Eval("Id") %>' runat="server" />
                                </div>
                                <div class="flex flex-col w-full bg-white shadow-card rounded-10px overflow-hidden">
                                    <div class="flex w-full flex-col lg:flex-row border-gray border-b-2">
                                        <div class="p-20px lg:p-30px border-gray border-b-2 lg:border-b-0 flex items-center justify-center">
                                            <img id="ImgCompanyLogo" runat="server" src='<%# Eval("CompanyLogo") %>' alt="logo for company profile Loqate a GBG solution" loading="lazy" />
                                        </div>
                                        <div class="flex flex-col grow py-30px px-25px gap-10px border-gray border-l-2">
                                            <div class="flex flex-col-reverse lg:flex-row gap-20px w-full items-center">
                                                <h3 class="text-xl lg:text-2xl font-inter font-bold">'<%# Eval("CompanyName") %>'
                                                </h3>
                                                <div class="flex gap-7px items-center px-10px py-5px bg-gray rounded-30px">
                                                    <img src="/assets_out/images/company-item/rating.svg" alt="Featured icon" loading="lazy">
                                                    <h4 class="text-xs lg:text-sm font-bold text-yellow">Featured Company</h4>
                                                </div>
                                            </div>
                                            <div class="flex gap-20px w-full items-center justify-center lg:justify-start">                                                
                                                <div class="country bg-white overflow-hidden rounded-30px p-5px flex gap-5px items-center w-fit">
                                                    <img class="w-[15px] lg:w-[20px]" src="/assets_out/images/flags/US.svg" alt="" loading="lazy">
                                                    <h4 class="text-xs lg:text-sm font-semibold font-bold">'<%# Eval("Country") %>'
                                                    </h4>
                                                </div>
                                                <div class="flex gap-7px items-center">
                                                    <img src="/assets_out/images/company-item/location.svg" alt="location icon" loading="lazy">
                                                    <h4 class="text-xs lg:text-sm font-semibold">'<%# Eval("City") %>' | '<%# Eval("Address") %>'
                                                    </h4>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="p-25px gap-15px lg:gap-20px border-gray border-b-2 flex flex-col w-full">
                                        <div class="gap-10px lg:gap-15px flex flex-col w-full">
                                            <div class="px-15px py-20px lg:py-10px gap-15px flex-col lg:flex-row w-full bg-gray rounded-10px flex items-center">
                                                <div class="flex gap-7px items-center">
                                                    <img src="/assets_out/images/company-item/categories.svg" alt="categories icon" loading="lazy">
                                                    <h4 class="text-xs lg:text-sm font-semibold">Categories:</h4>
                                                </div>
                                                <div class="hidden lg:flex grow gap-10px flex-wrap justify-center lg:justify-start items-group"><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Advertising/Marketing</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Email Marketing</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">SEO & SEM</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Engagement Tools</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">CRM</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Advertising/Marketing</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Email Marketing</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">SEO & SEM</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Engagement Tools</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">CRM</a><a class="moreTrigger text-xs lg:text-xs py-5px px-10px bg-hawkesBlue rounded-20px text-blue" href="#"><span class="number">+5</span><span class="labelText">more</span></a></div>
                                            </div>
                                            <div class="px-15px py-20px lg:py-10px gap-15px flex-col lg:flex-row w-full bg-gray rounded-10px flex items-center">
                                                <div class="flex gap-7px items-center">
                                                    <img src="/assets_out/images/company-item/partner-programs.svg" alt="partner programs icon" loading="lazy">
                                                    <h4 class="text-xs lg:text-sm font-semibold">Partner Programs:</h4>
                                                </div>
                                                <div class="hidden lg:flex grow gap-10px flex-wrap justify-center lg:justify-start items-group">
                                                    <a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Reseller</a>
                                                    <a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Distributor</a>
                                                    <a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Managed Service Providers (MSPs)</a>
                                                    <a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Reseller</a>
                                                    <a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Distributor</a>
                                                    <a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Managed Service Providers (MSPs)</a>
                                                    <a class="moreTrigger text-xs lg:text-xs py-5px px-10px bg-hawkesBlue rounded-20px text-blue" href="#"><span class="number">+2</span><span class="labelText">more</span></a>
                                                </div>
                                            </div>
                                        </div>
                                        <p class="text-sm lg:text-base text-center lg:text-left text-textGray">
                                            '<%# Eval("Description") %>'
                                        </p>
                                    </div>
                                    <div class="py-15px px-25px flex-col lg:flex-row gap-10px justify-start lg:justify-end  flex">
                                        <a class="btn large font-bold text-xs xl:text-sm bg-white text-blue justify-center w-full lg:w-fit" href='<%# Eval("Profile") %>'><span>VIEW COMPANY PROFILE</span></a>
                                        <a class="btn large font-bold text-xs xl:text-sm bg-yellow text-white justify-center w-full lg:w-fit" href='<%# Eval("WebSite") %>'><span>VIEW WEBSITE</span></a>
                                        <a class="btn large font-bold text-xs xl:text-sm bg-blue text-white justify-center w-full lg:w-fit" href='<%# Eval("PortalLoginUrl") %>'><span>VIEW PARTNER PORTAL</span></a>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <div class="w-full lg:w-[1010px] max-w-full flex flex-col gap-50px" id="navBtnsBottom" visible="false" runat="server">
                        <div class="float-left md-padding-1">
                            <asp:Button ID="lbtnPrevBottom" runat="server" CssClass="btn btn-cta-secondary input-xsmall" Text="previous" OnClick="lbtnPrev_Click" />
                        </div>
                        <div class="float-right md-padding-1">
                            <asp:Button ID="lbtnNextBottom" runat="server" CssClass="btn btn-cta-secondary small" Width="80px" Text="next" OnClick="lbtnNext_Click" />
                        </div>
                    </div>

                </div>
            </div>
        </section>
        <section>
            <div class="container">
                <div class="gap-50px lg:gap-80px flex flex-col">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Partner Portal</h3>
                        <p class="w-full text-base lg:text-body">
                            Find out what’s a partner portal and why your business should start using one in order to increase productivity and sales.
                It is reported that a partner portal can increase partner productivity and sales up to <strong>15%</strong>. That’s why the Elioplus PRM solution integrates the Partner Portal feature by default.
                        </p>
                        <div class="flex justify-center mt-20px">
                            <a class="btn large font-bold text-sm xl:text-base bg-white" href="/prm-software/partner-portal"><span>READ MORE ABOUT OUR PARTNER PORTAL</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                        </div>
                    </div>
                    <div class="gap-80px lg:gap-120px flex flex-col">
                        <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                            <div class="flex justify-center items-center">
                                <img src="/assets_out/images/partner-portal/what-is-a-partner-portal.svg">
                            </div>
                            <div class="gap-15px lg:gap-25px flex flex-col justify-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">What is a partner portal?</h3>
                                <p class="w-full text-base lg:text-xl text-center lg:text-left">
                                    A partner portal is a software tool used by companies with a partner network to manage, optimize and automate their partnerships. A partner portal or PRM tool can be customized to fit specific needs based on the structure of a partner program or the benefits a vendor is offering.
                    Typically, some of the features included in every partner portal include a resources page or content management system, deal registration and lead distribution, partner directory and locator and a collaboration tool.
                                </p>
                            </div>
                        </div>
                        <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                            <div class="row-start-2 lg:row-start-1 gap-15px lg:gap-25px flex flex-col justify-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Why is a partner portal important?</h3>
                                <p class="w-full text-base lg:text-xl text-center lg:text-left">A partner portal can help channel managers to automate many of their channel tasks when it comes to reporting, approving deal registrations, providing marketing content and other material to their partners and give incentives to their network. Also, it helps a channel company to have a single source for all their tech partnerships and reporting needs and thus reducing their workload and increase their productivity.</p>
                            </div>
                            <div class="flex justify-center items-center">
                                <img src="/assets_out/images/partner-portal/why-is-partner-portal-important.svg">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
