<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="SearchForChannelPartnersByRegionCountryCityPage.aspx.cs" Inherits="WdS.ElioPlus.pages.SearchForChannelPartnersByRegionCountryCityPage" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" id="metaDescription" runat="server" />
    <meta name="keywords" id="metaKeywords" runat="server" />

    <!-- Snitcher analytics code -->
    <script type="text/javascript">
        !function (s, n, i, t, c, h) {
            s.SnitchObject = i; s[i] || (s[i] = function () {
                (s[i].q = s[i].q || []).push(arguments)
            }); s[i].l = +new Date; c = n.createElement(t);
            h = n.getElementsByTagName(t)[0]; c.src = '//snid.snitcher.com/8415152.js';
            h.parentNode.insertBefore(c, h)
        }(window, document, 'snid', 'script');

        snid('verify', '8415152');
    </script>

</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <main class="transition-all duration-200">
        <section class="bg-gray">
            <div class="container">
                <div class="gap-40px lg:gap-60px flex flex-col justify-center items-center w-full">
                    <div class="gap-20px lg:gap-30px flex flex-col justify-center items-center w-full">
                        <div class="text-center gap-5px lg:gap-10px flex flex-col items-center w-full">
                            <div class="text-xs lg:text-base flex gap-5px items-center justify-center text-textGray font-medium">
                                <a id="aBCrHome" runat="server" class="transition-all duration-200 hover:text-blue"></a>
                                <a id="aBCrRegion" runat="server" class="transition-all duration-200 hover:text-blue"></a>
                                <a id="aBCrCountry" runat="server" class="transition-all duration-200 hover:text-blue"></a>
                                <a id="aBCrCity" runat="server" class="transition-all duration-200 hover:text-blue"></a>
                            </div>
                            <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">
                                <asp:Label ID="LblResultsTitle" runat="server" />
                            </h1>
                            <p class="text-base lg:text-body text-center max-w-full">Channel partnerships can be a win-win relationship for both the vendor and the channel partner. The channel company sells products and services on behalf of their vendor and belongs to the indirect sales force and acts as an independent company.</p>
                        </div>
                        <div class="flex justify-center">
                            <div class="flex gap-10px w-full lg:w-[760px] max-w-full pt-5px pr-5px pb-5px pl-15px bg-white shadow-card rounded-40px overflow-hidden" method="POST" action="SearchForChannelPartnersByRegionCountryCityPage.aspx">
                                <input class="grow pl-15px rounded-30px outline-none" placeholder="Type to search for a product category, vendor technology, country, region">
                                <button class="btn large font-bold text-xs xl:text-sm bg-blue text-white" type="submit">
                                    <span>SEARCH</span><svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_40_8306)"><path d="M19.8102 18.9119L14.6466 13.8308C15.9988 12.3616 16.8296 10.4187 16.8296 8.28068C16.8289 3.7071 13.0618 0 8.41447 0C3.76713 0 0 3.7071 0 8.28068C0 12.8543 3.76713 16.5614 8.41447 16.5614C10.4224 16.5614 12.2641 15.8668 13.7107 14.7122L18.8944 19.8134C19.147 20.0622 19.5571 20.0622 19.8096 19.8134C20.0628 19.5646 20.0628 19.1607 19.8102 18.9119ZM8.41447 15.2873C4.48231 15.2873 1.29468 12.1504 1.29468 8.28068C1.29468 4.41101 4.48231 1.27403 8.41447 1.27403C12.3467 1.27403 15.5343 4.41101 15.5343 8.28068C15.5343 12.1504 12.3467 15.2873 8.41447 15.2873Z" fill="white" /></g><defs><clipPath id="clip0_40_8306"><rect width="20" height="20" fill="white" /></clipPath>
                                    </defs></svg>

                                </button>
                            </div>
                        </div>
                    </div>
                    <div id="divCitiesArea" runat="server" class="gap-20px lg:gap-30px flex flex-col text-center items-center">
                        <h3 class="text-xl lg:text-2xl font-bold font-inter">Cities in <span id='currentCountry'>
                            <asp:Label ID="LblCities" runat="server" />
                        </span>:</h3>
                        <div class="gap-7px lg:gap-15px countries flex justify-center flex-wrap">
                            <asp:Repeater ID="RptCities" runat="server">
                                <ItemTemplate>
                                    <a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href='<%# Eval("link") %>' runat="server" id="alink">

                                        <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                            <%# Eval("city") %>
                                        </h5>
                                    </a>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div id="divStatesArea" runat="server" visible="false" class="gap-20px lg:gap-30px flex flex-col text-center items-center">
                        <h3 class="text-xl lg:text-2xl font-bold font-inter">States in country <span id='currentCity'>
                            <asp:Label ID="LblStates" runat="server" />
                        </span>:</h3>
                        <div class="gap-7px lg:gap-15px countries flex justify-center flex-wrap">
                            <asp:Repeater ID="RptStates" runat="server">
                                <ItemTemplate>
                                    <a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href='<%# Eval("link") %>' runat="server" id="alink">

                                        <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                            <%# Eval("state") %>
                                        </h5>
                                    </a>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="gap-20px lg:gap-30px flex flex-col text-center items-center">
                        <h3 class="text-xl lg:text-2xl font-bold font-inter">Countries in region <span id='currentRegion'>
                            <asp:Label ID="LblByCountry" runat="server" />
                        </span>:</h3>
                        <div class="gap-7px lg:gap-15px countries flex justify-center flex-wrap">
                            <asp:Repeater ID="RptCountries" runat="server">
                                <ItemTemplate>
                                    <a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href='<%# Eval("link") %>' runat="server" id="alink">

                                        <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                            <%# Eval("country_name") %>
                                        </h5>
                                    </a>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section>
            <div class="container">
                <div class="w-full gap-50px lg:gap-80px flex flex-col">
                    <div class="w-full gap-20px lg:gap-30px flex flex-col">
                        <div class="flex flex-col gap-15px">
                            <h2 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">
                                <asp:Label ID="LblResultsTitle2" runat="server" />
                            </h2>
                            <p class="text-base lg:text-body text-center lg:text-left max-w-full">
                                <asp:Label ID="LblResultsTitle4" runat="server" />
                            </p>
                        </div>
                        <div class="py-20px px-15px justify-center  lg:justify-between items-center flex-col lg:flex-row gap-15px lg:gap-15px bg-white rounded-15px shadow-card flex w-full">
                            <div class="text-sm lg:text-base px-15px py-10px bg-gray rounded-10px text-blue font-inter w-fit font-bold">
                                <asp:Label ID="LblRegion" runat="server" />
                            </div>
                            <div class="h-[35px] separator"></div>
                            <div class="search-tabs flex items-center gap-15px">
                                <div class="text-sm lg:text-base text-textGray font-inter w-fit">Browse by:</div>
                                <a class="tab-item flex items-center gap-5px active" href="#tabCategory">
                                    <div class="w-[15px] h-[15px] rounded-full bg-textGray circle"></div>
                                    <h4 class="text-sm lg:text-base text-textGray">Product Category</h4>
                                </a><a class="tab-item flex items-center gap-5px" href="#tabTechnology">
                                    <div class="w-[15px] h-[15px] rounded-full bg-textGray circle"></div>
                                    <h4 class="text-sm lg:text-base text-textGray">Vendor Technology</h4>
                                </a>
                            </div>
                            <div class="h-[35px] separator"></div>
                            <form class="flex gap-10px grow pt-5px pr-5px pb-5px pl-15px rounded-40px overflow-hidden border-gray border-2 bg-gray2">
                                <input class="grow pl-15px rounded-30px outline-none bg-transparent" placeholder="Search for a category, vendor technology, country, region">
                                <button class="w-[40px] h-[40px] bg-blue rounded-full flex justify-center items-center" type="submit">
                                    <svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                                        <g clip-path="url(#clip0_40_8306)">
                                            <path d="M19.8102 18.9119L14.6466 13.8308C15.9988 12.3616 16.8296 10.4187 16.8296 8.28068C16.8289 3.7071 13.0618 0 8.41447 0C3.76713 0 0 3.7071 0 8.28068C0 12.8543 3.76713 16.5614 8.41447 16.5614C10.4224 16.5614 12.2641 15.8668 13.7107 14.7122L18.8944 19.8134C19.147 20.0622 19.5571 20.0622 19.8096 19.8134C20.0628 19.5646 20.0628 19.1607 19.8102 18.9119ZM8.41447 15.2873C4.48231 15.2873 1.29468 12.1504 1.29468 8.28068C1.29468 4.41101 4.48231 1.27403 8.41447 1.27403C12.3467 1.27403 15.5343 4.41101 15.5343 8.28068C15.5343 12.1504 12.3467 15.2873 8.41447 15.2873Z" fill="white" />
                                        </g><defs><clipPath id="clip0_40_8306"><rect width="20" height="20" fill="white" /></clipPath>
                                        </defs></svg>

                                </button>
                            </form>
                        </div>
                    </div>
                    <div class="tab-content gap-30px lg:gap-50px flex flex-col w-full" id="tabCategory">
                        <div class="flex flex-col gap-10px w-full">
                            <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">
                                <asp:Label ID="LblResultsTitle5" runat="server" />
                            </h3>
                            <div class="owl-carousel owl-theme" data-items="5">
                                <div class="p-10px lg:p-15px h-full">
                                    <a id="apopBigData" runat="server" class="w-full h-full bg-white shadow-card overflow-hidden rounded-10px case-study flex flex-col">
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
                                    <a id="apopAnalSoft" runat="server" class="w-full h-full bg-white shadow-card overflow-hidden rounded-10px case-study flex flex-col">
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
                                    <a id="apopEcommerce" runat="server" class="w-full h-full bg-white shadow-card overflow-hidden rounded-10px case-study flex flex-col">
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
                                    <a id="apopAccounting" runat="server" class="w-full h-full bg-white shadow-card overflow-hidden rounded-10px case-study flex flex-col">
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
                                    <a id="apopDatabases" runat="server" class="w-full h-full bg-white shadow-card overflow-hidden rounded-10px case-study flex flex-col">
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
                                    <a id="apopCrm" runat="server" class="w-full h-full bg-white shadow-card overflow-hidden rounded-10px case-study flex flex-col">
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
                                    <a id="apopAdServing" runat="server" class="w-full h-full bg-white shadow-card overflow-hidden rounded-10px case-study flex flex-col">
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
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">
                                    <asp:Label ID="LblProductCategories" runat="server" />
                                </h3>
                                <div class="h-[2px] divider"></div>
                            </div>
                            <div class="grid-cols-1 lg:grid-cols-letters gap-30px lg:gap-50px grid w-full">
                                <div class="flex flex-col gap-15px items-center justify-start border-x-2 border-gray py-5px">
                                    <a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catA">A</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catB">B</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catC">C</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catD">D</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catE">E</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catF">F</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catG">G</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catH">H</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catI">I</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catJ">J</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catK">K</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catL">L</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catM">M</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catN">N</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catO">O</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catP">P</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catQ">Q</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catR">R</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catS">S</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catT">T</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catU">U</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catV">V</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catW">W</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catX">X</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catY">Y</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#catZ">Z</a>
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
                    <div class="tab-content flex hidden gap-30px lg:gap-50px flex-col w-full" id="tabTechnology">
                        <div class="flex flex-col gap-15px w-full">
                            <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">
                                <asp:Label ID="LblResultsTitle3" runat="server" />
                            </h3>
                            <div class="h-[2px] divider"></div>
                        </div>
                        <div class="grid-cols-1 lg:grid-cols-letters gap-30px lg:gap-50px grid w-full">
                            <div class="flex flex-col gap-15px items-center justify-start border-x-2 border-gray py-5px">
                                <a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techA">A</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techB">B</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techC">C</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techD">D</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techE">E</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techF">F</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techG">G</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techH">H</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techI">I</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techJ">J</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techK">K</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techL">L</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techM">M</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techN">N</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techO">O</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techP">P</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techQ">Q</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techR">R</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techS">S</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techT">T</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techU">U</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techV">V</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techW">W</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techX">X</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techY">Y</a><a class="text-sm lg:text-base w-[35px] h-[35px] font-medium go-to-expand font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techZ">Z</a>
                            </div>
                            <div class="gap-30px lg:gap-40px flex flex-col w-full">
                                <details class="text-sm lg:text-base" id="techA" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">A</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechA" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techB" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">B</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechB" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techC" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">C</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechC" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techD" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">D</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechD" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techE" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">E</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechE" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techF" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">F</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechF" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techG" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">G</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechG" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techH" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">H</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechH" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techI" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">I</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechI" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techJ" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">J</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechJ" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techK" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">K</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechK" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techL" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">L</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechL" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techM" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">M</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechM" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techN" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">N</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechN" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techO" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">O</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechO" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techP" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">P</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechP" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techQ" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">Q</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechQ" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techR" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">R</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechR" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techS" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">S</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechS" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techT" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">T</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechT" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techU" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">U</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechU" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techV" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">V</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechV" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techW" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">W</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechW" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techX" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">X</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechX" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techY" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">Y</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechY" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                                <details class="text-sm lg:text-base" id="techZ" open>
                                    <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                        <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">Z</span>:</h4>
                                    </summary>
                                    <div class="mt-20px w-full flex gap-15px flex-wrap">
                                        <asp:Repeater ID="RptTechZ" runat="server">
                                            <ItemTemplate>
                                                <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                    <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                    <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                                </a>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </details>
                                <div class="h-[2px] divider"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
    <section class="bg-gray">
        <div class="container">
            <div class="gap-80px lg:gap-120px flex flex-col">
                <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                    <div class="row-start-2 lg:row-start-1 gap-15px lg:gap-25px flex flex-col justify-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">What’s a Channel Partner?</h3>
                        <p class="w-full text-base lg:text-xl text-center lg:text-left">
                            A channel partner is a company or individual in some cases that partners with a manufacturer or vendor to market and sell their products, technologies or services. The channel company although sells products and services on behalf of their partnering vendor and belongs to the indirect sales force, it is and acts as an independent company.
                <br>
                            <br>
                            This means that a specific channel partner may have partnerships with more than one vendor and in fact the average channel partner works with 7-10 different IT vendors at a given period. Due to the explosive evolution in the Software-as-a-Service (SaaS) industry many channel partners produce and promote their own products as well.
                        </p>
                    </div>
                    <div class="flex justify-center items-center">
                        <img src="/assets_out/images/search/what-is-a-channel-partner.svg" alt="What is a channel partner" loading="lazy">
                    </div>
                </div>
                <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                    <div class="flex justify-center items-center">
                        <img src="/assets_out/images/search/build-partnerships.svg" alt="How to build a channel partnership" loading="lazy">
                    </div>
                    <div class="gap-15px lg:gap-25px flex flex-col justify-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">How to build a channel partnership?</h3>
                        <p class="w-full text-base lg:text-xl text-center lg:text-left">
                            Channel partnerships can be a win-win relationship for both the vendor and the channel partner. A vendor should create an ideal channel partner profile before engaging with potential partners. Then there are several criteria on our platform to help you track these companies and reach out to them.
                <br>
                            <br>
                            When reaching out make sure to provide your potential partners a concise message of what are the benefits and requirements partnering with your company. And please keep in mind that the most important part comes afterwards, how to maintain a successful relationship with your partners.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
