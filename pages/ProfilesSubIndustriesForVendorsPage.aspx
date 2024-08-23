<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="ProfilesSubIndustriesForVendorsPage.aspx.cs" Inherits="WdS.ElioPlus.pages.ProfilesSubIndustriesForVendorsPage" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" id="metaDescription" runat="server" content="" />
    <meta name="keywords" id="metaKeywords" runat="server" content="" />

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
                <div class="flex flex-col w-full gap-50px lg:gap-80px">
                    <div class="w-full gap-20px lg:gap-30px flex flex-col">
                        <div class="text-left gap-5px flex flex-col">
                            <div class="text-xs lg:text-base flex gap-5px items-center text-textGray">
                                <a id="aBCrHome" runat="server" class="hover:text-blue" />
                                <a id="aBCrCategory" runat="server" class="hover:text-blue" />
                            </div>
                            <h1 class="text-body lg:text-5xl font-bold font-inter">
                                <asp:Label ID="LblResultsTitle" runat="server" />
                            </h1>
                        </div>
                        <p class="text-base lg:text-body text-left">
                            <asp:Label ID="LblResultsContent1" runat="server" />
                        </p>
                        <p class="text-base lg:text-body text-left">
                            <asp:Label ID="LblResultsContent2" runat="server" />
                        </p>
                        <p class="text-base lg:text-body text-left">
                            <asp:Label ID="LblResultsContent3" runat="server" />
                        </p>
                        <p class="text-base lg:text-body text-left">
                            <asp:Label ID="LblGetStartedList" runat="server" />
                        </p>
                        <div class="flex items-start gap-10px xl:gap-15px flex-col lg:flex-row ">
                            <a id="aGetStarted" runat="server" class="btn large font-bold text-sm xl:text-base bg-blue text-white" href="/free-sign-up">
                                <svg width="18" height="19" viewBox="0 0 18 19" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <path d="M8.95734 5.12856C10.2416 5.12856 11.2826 4.09242 11.2826 2.81428C11.2826 1.53614 10.2416 0.5 8.95734 0.5C7.67314 0.5 6.63208 1.53614 6.63208 2.81428C6.63208 4.09242 7.67314 5.12856 8.95734 5.12856Z" fill="#39B4EF" />
                                    <path d="M8.95734 11.8141C10.2416 11.8141 11.2826 10.778 11.2826 9.49983C11.2826 8.22169 10.2416 7.18555 8.95734 7.18555C7.67314 7.18555 6.63208 8.22169 6.63208 9.49983C6.63208 10.778 7.67314 11.8141 8.95734 11.8141Z" fill="#39B4EF" />
                                    <path d="M8.95734 18.5001C10.2416 18.5001 11.2826 17.464 11.2826 16.1859C11.2826 14.9077 10.2416 13.8716 8.95734 13.8716C7.67314 13.8716 6.63208 14.9077 6.63208 16.1859C6.63208 17.464 7.67314 18.5001 8.95734 18.5001Z" fill="#39B4EF" />
                                    <path d="M2.75666 11.8141C4.04087 11.8141 5.08192 10.778 5.08192 9.49983C5.08192 8.22169 4.04087 7.18555 2.75666 7.18555C1.47245 7.18555 0.431396 8.22169 0.431396 9.49983C0.431396 10.778 1.47245 11.8141 2.75666 11.8141Z" fill="#39B4EF" />
                                    <path d="M15.6746 11.8141C16.9589 11.8141 17.9999 10.778 17.9999 9.49983C17.9999 8.22169 16.9589 7.18555 15.6746 7.18555C14.3904 7.18555 13.3494 8.22169 13.3494 9.49983C13.3494 10.778 14.3904 11.8141 15.6746 11.8141Z" fill="#39B4EF" />
                                </svg>
                                <span>SIGN UP FOR FREE NOW</span></a>
                        </div>
                    </div>
                    <div class="w-full gap-30px lg:gap-50px flex flex-col">
                        <div class="w-full gap-20px lg:gap-30px flex flex-col">
                            <div class="py-25px px-20px justify-center  lg:justify-between items-center flex-col lg:flex-row gap-15px lg:gap-0 bg-white rounded-10px shadow-card flex w-full" id="searchChannelPartnersForm" method="POST">
                                <div class="text-xs lg:text-sm px-20px py-10px text-textGray bg-gray font-inter font-bold rounded-30px w-fit">Search Filter:</div>
                                <div class="text-sm lg:text-sm px-20px py-10px w-full lg:w-fit text-center flex gap-5px text-textGray border-gray border-2 font-bold rounded-30px justify-center bg-gray2">
                                    <label for="sort">Sort by:</label>
                                    <asp:DropDownList ID="DrpSortList" Width="155" runat="server">
                                        <asp:ListItem Value="0" Text="Default Sort" />
                                        <asp:ListItem Value="1" Text="A-Z" />
                                        <asp:ListItem Value="2" Text="Z-A" />
                                        <asp:ListItem Value="3" Text="Oldest to Newest" />
                                        <asp:ListItem Value="4" Text="Newest to Oldest" />
                                    </asp:DropDownList>
                                </div>
                                <a id="aSearchBy" runat="server" onserverclick="aSearchBy_Click" class="btn large font-bold text-xs lg:text-sm bg-blue text-white" type="submit">
                                    <span>APPLY FILTERS</span>
                                </a>
                            </div>
                        </div>
                        <div class="grid-cols-1 lg:grid-cols-company gap-30px lg:gap-50px grid">
                            <div class="gap-40px flex flex-col">
                                <asp:Repeater ID="RdgResults" OnItemDataBound="RdgResults_ItemDataBound" runat="server">
                                    <ItemTemplate>
                                        <div>
                                            <asp:HiddenField ID="HdnCompanyID" Value='<%# Eval("Id") %>' runat="server" />
                                        </div>
                                        <div class="w-full flex flex-col gap-50px">
                                            <div class="flex flex-col w-full bg-white shadow-card rounded-10px overflow-hidden">
                                                <div class="flex w-full flex-col lg:flex-row border-gray border-b-2">
                                                    <div class="p-20px lg:p-30px border-gray border-b-2 lg:border-b-0 flex items-center justify-center">
                                                        <a id="aCompanyLogo" href='<%# Eval("Profiles") %>' runat="server">
                                                            <img id="ImgCompanyLogo" src='<%# Eval("CompanyLogo") %>' alt='<%# Eval("CompanyName") %>' runat="server" width="100" loading="lazy" />
                                                        </a>
                                                    </div>
                                                    <div class="flex flex-col grow py-30px px-25px gap-10px border-gray border-l-2">
                                                        <div class="flex flex-col-reverse lg:flex-row gap-20px w-full items-center">
                                                            <h3 class="text-xl lg:text-2xl font-inter font-bold">
                                                                <a id="aCompanyName" href='<%# Eval("Profiles") %>' runat="server">
                                                                    <%# Eval("CompanyName") %>
                                                                </a>
                                                            </h3>
                                                            <div id="divFeatured" runat="server" visible="false" class="flex gap-7px items-center px-10px py-5px bg-gray rounded-30px">
                                                                <img src="/assets_out/images/company-item/rating.svg" alt="feaured icon" loading="lazy">
                                                                <h4 class="text-xs lg:text-sm font-bold text-yellow">
                                                                    <asp:Label ID="LblFeature" Text="Featured company" runat="server" />
                                                                </h4>
                                                            </div>
                                                        </div>
                                                        <div class="flex gap-20px w-full items-center justify-center lg:justify-start">
                                                            <div class="country bg-gradient-product overflow-hidden rounded-30px p-5px flex gap-5px items-center w-fit">
                                                                <%--<img class="w-[15px] lg:w-[20px]" src="/assets_out/images/flags/US.svg" alt="flag US" loading="lazy">--%>
                                                                <h4 class="text-xs lg:text-sm font-semibold font-bold"><%# Eval("Country") %></h4>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="p-25px gap-15px lg:gap-20px border-gray border-b-2 flex flex-col w-full">
                                                    <div class="gap-10px lg:gap-15px flex flex-col w-full">
                                                        <div id="divCategoriesArea" runat="server" class="px-15px py-20px lg:py-10px gap-15px flex-col lg:flex-row w-full bg-gray rounded-10px flex items-center">
                                                            <div class="flex gap-7px items-center">
                                                                <img src="/assets_out/images/company-item/categories.svg" alt="categories icon" loading="lazy">
                                                                <h4 class="text-xs lg:text-sm font-semibold">Categories:</h4>
                                                            </div>
                                                            <div class="hidden lg:flex grow gap-10px flex-wrap justify-center lg:justify-start items-group">
                                                                <a id="aVert1" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblVert1" runat="server" />
                                                                </a>
                                                                <a id="aVert2" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblVert2" runat="server" />
                                                                </a>
                                                                <a id="aVert3" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblVert3" runat="server" />
                                                                </a>
                                                                <a id="aVert4" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblVert4" runat="server" />
                                                                </a>
                                                                <a id="aVert5" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblVert5" runat="server" />
                                                                </a>
                                                                <a id="aVert6" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblVert6" runat="server" />
                                                                </a>
                                                                <a id="aVert7" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblVert7" runat="server" />
                                                                </a>
                                                                <a id="aVert8" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblVert8" runat="server" />
                                                                </a>
                                                                <a id="aVert9" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblVert9" runat="server" />
                                                                </a>
                                                                <a id="aVert10" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblVert10" runat="server" />
                                                                </a>
                                                                <a id="aVert11" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblVert11" runat="server" />
                                                                </a>
                                                                <a id="aVert12" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblVert12" runat="server" />
                                                                </a>
                                                                <a id="aVert13" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblVert13" runat="server" />
                                                                </a>
                                                                <a id="aVert14" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblVert14" runat="server" />
                                                                </a>
                                                                <a id="aVert15" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblVert15" runat="server" />
                                                                </a>
                                                                <a id="aVertMore" runat="server" visible="false" class="moreTrigger text-xs lg:text-xs py-5px px-10px bg-hawkesBlue rounded-20px text-blue">
                                                                    <span class="number">
                                                                        <asp:Label ID="LblVertMoreNum" runat="server" />
                                                                    </span><span class="labelText">
                                                                        <asp:Label ID="LblVertMore" runat="server" /></span>
                                                                </a>
                                                            </div>
                                                        </div>
                                                        <div id="divProductsArea" runat="server" class="px-15px py-20px lg:py-10px gap-15px flex-col lg:flex-row w-full bg-gray rounded-10px flex items-center">
                                                            <div class="flex gap-7px items-center">
                                                                <img src="/assets_out/images/company-item/products.svg" alt="products icon" loading="lazy">
                                                                <h4 class="text-xs lg:text-sm font-semibold">Partner Programs:</h4>
                                                            </div>
                                                            <div class="hidden lg:flex grow gap-10px flex-wrap justify-center lg:justify-start items-group">
                                                                <a id="aPartPr1" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblPartPr1" runat="server" />
                                                                </a>
                                                                <a id="aPartPr2" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblPartPr2" runat="server" />
                                                                </a>
                                                                <a id="aPartPr3" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblPartPr3" runat="server" />
                                                                </a>
                                                                <a id="aPartPr4" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblPartPr4" runat="server" />
                                                                </a>
                                                                <a id="aPartPr5" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblPartPr5" runat="server" />
                                                                </a>
                                                                <a id="aPartPr6" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblPartPr6" runat="server" />
                                                                </a>
                                                                <a id="aPartPr7" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblPartPr7" runat="server" />
                                                                </a>
                                                                <a id="aPartPr8" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblPartPr8" runat="server" />
                                                                </a>
                                                                <a id="aPartPr9" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblPartPr9" runat="server" />
                                                                </a>
                                                                <a id="aPartPr10" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblPartPr10" runat="server" />
                                                                </a>
                                                                <a id="aPartPr11" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblPartPr11" runat="server" />
                                                                </a>
                                                                <a id="aPartPr12" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblPartPr12" runat="server" />
                                                                </a>
                                                                <a id="aPartPr13" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblPartPr13" runat="server" />
                                                                </a>
                                                                <a id="aPartPr14" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblPartPr14" runat="server" />
                                                                </a>
                                                                <a id="aPartPr15" runat="server" visible="false" class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white">
                                                                    <asp:Label ID="LblPartPr15" runat="server" />
                                                                </a>
                                                                <a id="aPartPrMore" runat="server" visible="false" class="moreTrigger text-xs lg:text-xs py-5px px-10px bg-hawkesBlue rounded-20px text-blue">
                                                                    <span class="number">
                                                                        <asp:Label ID="LblPartPrMoreNum" runat="server" />
                                                                    </span><span class="labelText">
                                                                        <asp:Label ID="LblPartPrMore" runat="server" />
                                                                    </span>
                                                                </a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <p class="text-sm lg:text-base text-center lg:text-left text-textGray">
                                                        <asp:Label ID="LblOverview" runat="server" />
                                                    </p>
                                                </div>
                                                <div class="py-15px px-25px flex-col lg:flex-row gap-10px justify-start lg:justify-end  flex">
                                                    <a id="aProfile" href='<%# Eval("Profiles") %>' runat="server" class="btn large font-bold text-xs xl:text-sm bg-white text-blue justify-center w-full lg:w-fit">
                                                        <span>VIEW COMPANY PROFILE</span></a>
                                                    <a id="aPartnerPortalLogin" href='<%# Eval("PartnerPortal") %>' runat="server" class="btn large font-bold text-xs xl:text-sm bg-yellow text-white justify-center w-full lg:w-fit">
                                                        <span>Join as a Partner</span></a>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="w-full flex flex-col gap-50px">
                                    <div id="divWarningMsg" runat="server" visible="false" class="card bg-blue text-white gap-15px">
                                        <div class="flex items-center gap-5px">
                                            <img src="/assets_out/images/alerts/info-white.svg" alt="Quick Actions Icon" title="Click to toggle the quick actions menu" width="20" height="20">
                                            <h3 class="font-bold text-base text-white">
                                                <asp:Label ID="LblWarningMsg" runat="server" />
                                            </h3>
                                        </div>
                                        <p class="text-sm text-white">
                                            <asp:Label ID="LblWarningMsgContent" runat="server" />
                                        </p>
                                    </div>
                                </div>
                                <div class="flex justify-center gap-15px">
                                    <asp:Button ID="lbtnPrevBottom" runat="server" Style="width: 90px !important; cursor: pointer;" Text="Previous" OnClick="lbtnPrev_Click" CssClass="w-[25px] h-[25px] lg:w-[35px] lg:h-[35px] rounded-full hover:bg-blue hover:text-white bg-white shadow-card flex justify-center text-center items-center" />
                                    <asp:Button ID="lbtnNextBottom" runat="server" Style="width: 90px !important; cursor: pointer;" Text="Next" OnClick="lbtnNext_Click" CssClass="w-[25px] h-[25px] lg:w-[35px] lg:h-[35px] rounded-full hover:bg-blue hover:text-white bg-white shadow-card flex justify-center text-center items-center" />
                                </div>
                            </div>
                            <div>
                                <div class="relative h-full">
                                    <aside class="sidebar-container sticky top-[100px] pb-0 lg:pb-80px flex flex-col gap-50px">

                                        <div id="divRelatedCompanies" runat="server" visible="false" class="py-25px px-20px flex flex-col gap-15px bg-white shadow-card w-full rounded-10px">
                                            <h4 class="text-base font-bold">
                                                <asp:Label ID="LblFooterRelatedCompanies" runat="server" />
                                            </h4>
                                            <div class="divider"></div>
                                            <div class="flex flex-col gap-5px w-full">
                                                <a id="aFooterCompany1" runat="server" class="text-sm font-semibold underline text-blue">
                                                    <asp:Label ID="LblFooterCompany1" runat="server" />
                                                </a>
                                                <a id="aFooterCompany2" runat="server" class="text-sm font-semibold underline text-blue">
                                                    <asp:Label ID="LblFooterCompany2" runat="server" />
                                                </a>
                                                <a id="aFooterCompany3" runat="server" class="text-sm font-semibold underline text-blue">
                                                    <asp:Label ID="LblFooterCompany3" runat="server" />
                                                </a>
                                                <a id="aFooterCompany4" runat="server" class="text-sm font-semibold underline text-blue">
                                                    <asp:Label ID="LblFooterCompany4" runat="server" />
                                                </a>
                                                <a id="aFooterCompany5" runat="server" class="text-sm font-semibold underline text-blue">
                                                    <asp:Label ID="LblFooterCompany5" runat="server" />
                                                </a>
                                                <a id="aFooterCompany6" runat="server" class="text-sm font-semibold underline text-blue">
                                                    <asp:Label ID="LblFooterCompany6" runat="server" />
                                                </a>
                                                <a id="aFooterCompany7" runat="server" class="text-sm font-semibold underline text-blue">
                                                    <asp:Label ID="LblFooterCompany7" runat="server" />
                                                </a>
                                                <a id="aFooterCompany8" runat="server" class="text-sm font-semibold underline text-blue">
                                                    <asp:Label ID="LblFooterCompany8" runat="server" />
                                                </a>
                                            </div>
                                        </div>
                                    </aside>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section>
            <div class="container">
                <div class="gap-30px lg:gap-50px flex flex-col">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Create Partnerships & Connections</h3>
                        <p class="w-full text-base lg:text-body">Grow your indirect sales channel and connect with channel partners and IT vendors worldwide.</p>
                    </div>
                    <div class="flex flex-col lg:flex-row gap-30px lg:gap-10px justify-start lg:justify-between">
                        <div class="w-[390px] max-w-full bg-white shadow-card overflow-hidden rounded-10px">
                            <div class="pt-15px lg:pt-25px flex items-center justify-center">
                                <img src="/assets_out/images/search/partner-programs.svg" alt="Sass Partner Programs by Vendors at Elioplus" loading="lazy" width="101" height="100">
                            </div>
                            <div class="gap-10px lg:gap-15px py-10px lg:py-30px lg:pt-20px px-20px lg:px-25px text-center bg-white flex flex-col">
                                <div class="w-full flex flex-col gap-5px">
                                    <h3 class="text-base lg:text-lg font-bold font-inter">Sass Partner Programs by Vendors</h3>
                                    <p class="text-sm lg:text-base">Browse programs by vendors and partner with Resellers, Managed Service Providers (MSPs) and System Integrators (SIs).</p>
                                </div>
                                <a class="btn font-bold text-xs xl:text-sm w-full justify-center text-blue" href="/saas-partner-programs"><span>VIEW PARTNER PROGRAMS</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                        <div class="w-[390px] max-w-full bg-white shadow-card overflow-hidden rounded-10px">
                            <div class="pt-15px lg:pt-25px flex items-center justify-center">
                                <img src="/assets_out/images/search/channel-partners.svg" alt="Browse Sass Channel Partners at Elioplus" loading="lazy" width="100" height="100">
                            </div>
                            <div class="gap-10px lg:gap-15px py-10px lg:py-30px lg:pt-20px px-20px lg:px-25px text-center bg-white flex flex-col">
                                <div class="w-full flex flex-col gap-5px">
                                    <h3 class="text-base lg:text-lg font-bold font-inter">Sass Channel Partners</h3>
                                    <p class="text-sm lg:text-base">Explore channel companies that sell your products or services and belong to your indirect sales force while acting independent.</p>
                                </div>
                                <a class="btn font-bold text-xs xl:text-sm w-full justify-center text-blue" href="/search/channel-partners"><span>VIEW CHANNEL PARTNERS</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                        <div class="w-[390px] max-w-full bg-white shadow-card overflow-hidden rounded-10px">
                            <div class="pt-15px lg:pt-25px flex items-center justify-center">
                                <img src="/assets_out/images/search/partnerships.svg" alt="Browse Partnerships &amp; Technologies at Elioplus" loading="lazy" width="135" height="100">
                            </div>
                            <div class="gap-10px lg:gap-15px py-10px lg:py-30px lg:pt-20px px-20px lg:px-25px text-center bg-white flex flex-col">
                                <div class="w-full flex flex-col gap-5px">
                                    <h3 class="text-base lg:text-lg font-bold font-inter">Partnerships & Technologies</h3>
                                    <p class="text-sm lg:text-base">Search for partnerships offered by vendors who promote their product with current applications that their businesses use.</p>
                                </div>
                                <a class="btn font-bold text-xs xl:text-sm w-full justify-center text-blue" href="/partnerships"><span>VIEW PARTNERSHIPS</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
