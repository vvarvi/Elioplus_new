<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="SearchForVendorsPage.aspx.cs" Inherits="WdS.ElioPlus.pages.SearchForVendorsPage" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" id="metaDescription" runat="server" />
    <meta name="keywords" id="metaKeywords" runat="server" />
</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <main class="transition-all duration-200">
        <section class="bg-gray">
            <div class="container">
                <div class="gap-40px lg:gap-60px flex flex-col justify-center items-center w-full">
                    <div class="gap-20px lg:gap-30px flex flex-col justify-center items-center w-full">
                        <div class="text-center gap-5px lg:gap-10px flex flex-col items-center w-full">
                            <div class="text-xs lg:text-base flex gap-5px items-center justify-center text-textGray font-medium"><a id="aHome" runat="server" class="transition-all duration-200 hover:text-blue">HOME</a><span>/</span><span>SASS PARTNER PROGRAMS</span></div>
                            <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Search for Partners Programs by Vendors</h1>
                            <p class="text-base lg:text-body text-center max-w-full">
                                Partner with other companies within and outside your industry to help market and sell your product or service.Find out the best channel SaaS partner programs provided by IT and software Vendors.
                            </p>
                        </div>
                        <div class="flex justify-center">
                            <div class="flex gap-10px w-full lg:w-[760px] max-w-full pt-5px pr-5px pb-5px pl-15px bg-white shadow-card rounded-40px overflow-hidden" id="searchPartnerProgramsForm" method="POST" action="SearchForVendorsPage.aspx">
                                <input class="grow pl-15px rounded-30px outline-none" name="partner_program_search" placeholder="Type to search for a product category or a vendor company type">
                                <button class="btn large font-bold text-xs xl:text-sm bg-blue text-white" type="submit">
                                    <span>SEARCH</span><svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_40_8306)"><path d="M19.8102 18.9119L14.6466 13.8308C15.9988 12.3616 16.8296 10.4187 16.8296 8.28068C16.8289 3.7071 13.0618 0 8.41447 0C3.76713 0 0 3.7071 0 8.28068C0 12.8543 3.76713 16.5614 8.41447 16.5614C10.4224 16.5614 12.2641 15.8668 13.7107 14.7122L18.8944 19.8134C19.147 20.0622 19.5571 20.0622 19.8096 19.8134C20.0628 19.5646 20.0628 19.1607 19.8102 18.9119ZM8.41447 15.2873C4.48231 15.2873 1.29468 12.1504 1.29468 8.28068C1.29468 4.41101 4.48231 1.27403 8.41447 1.27403C12.3467 1.27403 15.5343 4.41101 15.5343 8.28068C15.5343 12.1504 12.3467 15.2873 8.41447 15.2873Z" fill="white" /></g><defs><clipPath id="clip0_40_8306"><rect width="20" height="20" fill="white" /></clipPath>
                                    </defs></svg>

                                </button>
                            </div>
                        </div>
                    </div>
                    <div class="flex-col lg:flex-row flex justify-center gap-30px w-full">
                        <div class="w-[390px] p-40px bg-white rounded-20px shadow-card flex flex-col gap-25px">
                            <div class="flex flex-col items-center just-center text-center gap-5px w-full">
                                <h2 class="text-lg font-bold font-inter">White Label Partner Programs</h2>
                                <p class="text-sm">The simplest form that most SaaS vendors use but sometimes some further customization may exist.</p>
                            </div>
                            <a class="btn font-bold text-xs xl:text-sm w-full justify-center text-blue" href="/white-label-partner-programs"><span>VIEW PARTNER PROGRAMS</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                        </div>
                        <div class="w-[390px] p-40px bg-white rounded-20px shadow-card flex flex-col gap-25px">
                            <div class="flex flex-col items-center just-center text-center gap-5px w-full">
                                <h2 class="text-lg font-bold font-inter">MSPs Partner Programs</h2>
                                <p class="text-sm">Usually chosen by small and mid-sized companies to hire companies that offer remote services.</p>
                            </div>
                            <a class="btn font-bold text-xs xl:text-sm w-full justify-center text-blue" href="/msps-partner-programs"><span>VIEW PARTNER PROGRAMS</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                        </div>
                        <div class="w-[390px] p-40px bg-white rounded-20px shadow-card flex flex-col gap-25px">
                            <div class="flex flex-col items-center just-center text-center gap-5px w-full">
                                <h2 class="text-lg font-bold font-inter">SI Partner Programs</h2>
                                <p class="text-sm">Offered mainly by vendors who promote their product with current applications that their businesses use.</p>
                            </div>
                            <a class="btn font-bold text-xs xl:text-sm w-full justify-center text-blue" href="/systems-integrators-partner-programs"><span>VIEW PARTNER PROGRAMS</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
    <section>
        <div class="container">
            <div class="w-full gap-50px lg:gap-80px flex flex-col">
                <div class="w-full gap-20px lg:gap-30px flex flex-col">
                    <div class="flex flex-col gap-15px">
                        <h2 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Partner Programs</h2>
                        <p class="text-base lg:text-body text-center lg:text-left max-w-full">Browse channel partner programs provided by Vendors at ElioPlus platform.</p>
                    </div>
                </div>
                <div class="tab-content gap-30px lg:gap-50px flex flex-col w-full" id="tabCategory">
                    <div class="flex flex-col gap-10px w-full">
                        <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">Popular categories:</h3>
                        <div class="owl-carousel owl-theme" data-items="5">
                            <div class="p-10px lg:p-15px h-full">
                                <a id="apopBigData" runat="server" href="/profile/vendors/big_data" class="w-full h-full bg-white shadow-card overflow-hidden rounded-10px case-study flex flex-col">
                                    <div class="bg-gray flex items-center justify-center">
                                        <img src="/assets_out/images/search/categories/bigData.png" alt="View companies in category Big Data" loading="lazy">
                                    </div>
                                    <div class="gap-5px lg:gap-10px py-15px lg:py-20px px-10px lg:px-15px grow bg-white flex flex-col">
                                        <div class="flex flex-col gap-5px">
                                            <h5 class="text-xs font-inter">POPULAR</h5>
                                            <h3 class="text-base font-bold font-inter text-blue">Big Data</h3>
                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="p-10px lg:p-15px h-full">
                                <a id="apopAnalSoft" runat="server" href="/profile/vendors/analytics_software" class="w-full h-full bg-white shadow-card overflow-hidden rounded-10px case-study flex flex-col">
                                    <div class="bg-gray flex items-center justify-center">
                                        <img src="/assets_out/images/search/categories/analyticsSoftware.png" alt="View companies in category Analytics Software" loading="lazy">
                                    </div>
                                    <div class="gap-5px lg:gap-10px py-15px lg:py-20px px-10px lg:px-15px grow bg-white flex flex-col">
                                        <div class="flex flex-col gap-5px">
                                            <h5 class="text-xs font-inter">POPULAR</h5>
                                            <h3 class="text-base font-bold font-inter text-blue">Analytics Software</h3>
                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="p-10px lg:p-15px h-full">
                                <a id="apopEcommerce" runat="server" href="/profile/vendors/ecommerce" class="w-full h-full bg-white shadow-card overflow-hidden rounded-10px case-study flex flex-col">
                                    <div class="bg-gray flex items-center justify-center">
                                        <img src="/assets_out/images/search/categories/ecommerce.png" alt="View companies in category Ecommerce" loading="lazy">
                                    </div>
                                    <div class="gap-5px lg:gap-10px py-15px lg:py-20px px-10px lg:px-15px grow bg-white flex flex-col">
                                        <div class="flex flex-col gap-5px">
                                            <h5 class="text-xs font-inter">POPULAR</h5>
                                            <h3 class="text-base font-bold font-inter text-blue">Ecommerce</h3>
                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="p-10px lg:p-15px h-full">
                                <a id="apopAccounting" runat="server" href="/profile/vendors/accounting" class="w-full h-full bg-white shadow-card overflow-hidden rounded-10px case-study flex flex-col">
                                    <div class="bg-gray flex items-center justify-center">
                                        <img src="/assets_out/images/search/categories/accounting.png" alt="View companies in category Accounting" loading="lazy">
                                    </div>
                                    <div class="gap-5px lg:gap-10px py-15px lg:py-20px px-10px lg:px-15px grow bg-white flex flex-col">
                                        <div class="flex flex-col gap-5px">
                                            <h5 class="text-xs font-inter">POPULAR</h5>
                                            <h3 class="text-base font-bold font-inter text-blue">Accounting</h3>
                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="p-10px lg:p-15px h-full">
                                <a id="apopDatabases" runat="server" href="/profile/vendors/databases" class="w-full h-full bg-white shadow-card overflow-hidden rounded-10px case-study flex flex-col">
                                    <div class="bg-gray flex items-center justify-center">
                                        <img src="/assets_out/images/search/categories/databases.png" alt="View companies in category Databases" loading="lazy">
                                    </div>
                                    <div class="gap-5px lg:gap-10px py-15px lg:py-20px px-10px lg:px-15px grow bg-white flex flex-col">
                                        <div class="flex flex-col gap-5px">
                                            <h5 class="text-xs font-inter">POPULAR</h5>
                                            <h3 class="text-base font-bold font-inter text-blue">Databases</h3>
                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="p-10px lg:p-15px h-full">
                                <a id="apopCrm" runat="server" href="/profile/vendors/crm" class="w-full h-full bg-white shadow-card overflow-hidden rounded-10px case-study flex flex-col">
                                    <div class="bg-gray flex items-center justify-center">
                                        <img src="/assets_out/images/search/categories/databases.png" alt="View companies in category Crm" loading="lazy">
                                    </div>
                                    <div class="gap-5px lg:gap-10px py-15px lg:py-20px px-10px lg:px-15px grow bg-white flex flex-col">
                                        <div class="flex flex-col gap-5px">
                                            <h5 class="text-xs font-inter">POPULAR</h5>
                                            <h3 class="text-base font-bold font-inter text-blue">CRM</h3>
                                        </div>
                                    </div>
                                </a>
                            </div>
                            <div class="p-10px lg:p-15px h-full">
                                <a id="apopAdServing" runat="server" href="/profile/vendors/ad_serving" class="w-full h-full bg-white shadow-card overflow-hidden rounded-10px case-study flex flex-col">
                                    <div class="bg-gray flex items-center justify-center">
                                        <img src="/assets_out/images/search/categories/databases.png" alt="View companies in category Ad Serving" loading="lazy">
                                    </div>
                                    <div class="gap-5px lg:gap-10px py-15px lg:py-20px px-10px lg:px-15px grow bg-white flex flex-col">
                                        <div class="flex flex-col gap-5px">
                                            <h5 class="text-xs font-inter">POPULAR</h5>
                                            <h3 class="text-base font-bold font-inter text-blue">Ad Serving</h3>
                                        </div>
                                    </div>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="flex flex-col gap-30px w-full">
                        <div class="flex flex-col gap-15px w-full">
                            <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">All product categories:</h3>
                            <div class="h-[2px] divider"></div>
                        </div>
                        <div class="grid-cols-1 lg:grid-cols-letters gap-30px lg:gap-50px grid w-full">
                            <div class="flex flex-col gap-15px items-center justify-start border-x-2 border-gray py-5px">
                                <a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catA">A</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catB">B</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catC">C</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catD">D</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catE">E</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catF">F</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catG">G</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catH">H</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catI">I</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catJ">J</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catK">K</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catL">L</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catM">M</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catN">N</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catO">O</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catP">P</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catQ">Q</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catR">R</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catS">S</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catT">T</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catU">U</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catV">V</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catW">W</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catX">X</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catY">Y</a><a class="go-to text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catZ">Z</a>
                            </div>
                            <div class="gap-30px lg:gap-40px flex flex-col w-full">
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catA">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">A</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptA" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catB">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">B</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptB" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catC">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">C</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptC" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catD">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">D</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptD" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catE">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">E</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptE" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catF">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">F</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptF" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catG">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">G</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptG" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catH">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">H</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptH" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catI">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">I</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptI" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catJ">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">J</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptJ" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catK">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">K</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptK" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catL">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">L</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptL" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catM">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">M</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptM" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catN">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">N</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptN" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catO">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">O</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptO" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catP">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">P</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptP" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catQ">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">Q</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptQ" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catR">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">R</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptR" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catS">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">S</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptS" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catT">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">T</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptT" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catU">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">U</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptU" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catV">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">V</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptV" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catW">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">W</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptW" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catX">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">X</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptX" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catY">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">Y</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptY" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                                <div class="gap-20px lg:gap-25px w-full flex flex-col" id="catZ">
                                    <h4 class="font-bold font-inter text-2xl">Categories starting with&nbsp;<span class="text-blue">Z</span>:</h4>
                                    <div class="w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptZ" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="h-[2px] divider"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
