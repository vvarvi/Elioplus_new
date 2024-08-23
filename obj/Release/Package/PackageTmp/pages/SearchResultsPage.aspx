<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="SearchResultsPage.aspx.cs" Inherits="WdS.ElioPlus.pages.SearchResultsPage" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Search for channel partners based on their product & service offerings, location, industry and other criteria. Find partner programs from innovative vendors in more than 100 categories." />
    <meta name="keywords" content="find channel partners, find partner programs, IT vendors" />
</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <main class="transition-all duration-200">
                <section class="bg-gray">
                    <div class="container">
                        <div class="flex flex-col w-full gap-50px lg:gap-80px">
                            <div class="w-full gap-20px lg:gap-30px flex flex-col">
                                <div class="text-left gap-5px flex flex-col">
                                    <h1 class="text-body lg:text-5xl font-bold font-inter">
                                        <asp:Label ID="LblHeader1" Text="Showing search results for " runat="server" />
                                        <a id="searchResultType" runat="server" class="underline text-blue inline-block">
                                            <asp:Label ID="LblType" Text="Channel Partners" runat="server" />
                                        </a>
                                        <span>
                                            <asp:Label ID="LblHeader2" runat="server" /></span>
                                        <a id="searchResultCategory" runat="server" class="underline text-blue inline-block ml-7px">
                                            <asp:Label ID="LblCategory" Text="Advertising & Marketing" runat="server" />
                                        </a><span>
                                            <asp:Label ID="LblHeader3" runat="server" /></span>
                                        <a class="underline text-blue inline-block ml-7px" id="searchResultCountry" href="#">
                                            <asp:Label ID="LblCountry" runat="server" />
                                        </a>
                                    </h1>
                                </div>
                                <div class="w-full gap-20px lg:gap-30px flex flex-col">
                                    <h2 class="text-2xl lg:text-2xl font-inter text-center lg:text-left">
                                        <div style="float: left;">
                                            <asp:Label ID="LblSuccessMsgContent" runat="server" />
                                        </div>
                                        <div style="float: left; padding-left: 5px;">
                                            <a href="/channel-partner-recruitment" class="text-2xl lg:text-2xl bg-gray rounded-20px text-blue">here</a>
                                        </div>
                                    </h2>
                                </div>
                            </div>
                            <div class="w-full gap-30px lg:gap-50px flex flex-col">
                                <div class="grid-cols-1 lg:grid-cols-search gap-30px lg:gap-50px grid">
                                    <div>
                                        <div class="relative h-full">
                                            <aside class="sidebar-container pb-0 lg:pb-80px flex flex-col gap-50px">
                                                <div class="flex flex-col gap-15px w-full justify-center items-center">
                                                    <div class="px-25px py-30px gap-25px bg-white shadow-card rounded-10px flex flex-col w-full">
                                                        <div class="text-left gap-15px flex flex-col">
                                                            <h2 class="text-base font-bold">Advanced Search</h2>
                                                            <div class="divider"></div>
                                                        </div>
                                                        <div class="text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2">
                                                            <label for="type">I am looking for:</label>
                                                            <asp:DropDownList ID="DdlCategory" AutoPostBack="true" Width="250" OnTextChanged="DdlCategory_OnTextChanged" CssClass="bg-transparent text-blackBlue font-bold" name="type" runat="server">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div id="divSearchIndustry" runat="server" class="flex flex-col gap-15px">
                                                            <div id="divSearchIndustryCss" runat="server" class="text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2">
                                                                <label for="industry">In this industry:</label>
                                                                <asp:DropDownList ID="DdlIndustry" Width="250" CssClass="bg-transparent text-blackBlue font-bold" name="industry" runat="server" />
                                                            </div>
                                                            <div id="divIndustryInfo" runat="server" visible="false" class="w-full rounded-20px text-center flex justify-center items-center bg-lightBlue text-blue font-bold text-sm border border-blue py-5px gap-7px">
                                                                <img class="w-[15px]" src="/assets_out/images/global/lockBlue.svg"><span>
                                                                    <strong>
                                                                        <asp:Label ID="LblIndustryInfo" runat="server" /></strong><asp:Label ID="LblIndustryInfoContent" runat="server" />
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div id="divSearchVertical" runat="server" class="flex flex-col gap-15px">
                                                            <div id="divSearchVerticalCss" runat="server" class="text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2">
                                                                <label for="vertical">In this vertical:</label>
                                                                <asp:DropDownList ID="DdlVertical" Width="250" CssClass="bg-transparent text-blackBlue font-bold" name="vertical" runat="server" />
                                                            </div>
                                                            <div id="divVerticalInfo" runat="server" visible="false" class="w-full rounded-20px text-center flex justify-center items-center bg-lightBlue text-blue font-bold text-sm border border-blue py-5px gap-7px">
                                                                <img class="w-[15px]" src="/assets_out/images/global/lockBlue.svg"><span>
                                                                    <strong>
                                                                        <asp:Label ID="LblVerticalInfo" runat="server" /></strong><asp:Label ID="LblVerticalInfoContent" runat="server" />
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div id="divSearchProgram" runat="server" class="flex flex-col gap-15px">
                                                            <div id="divSearchProgramCss" runat="server" class="text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2">
                                                                <label for="program">With this partner program:</label>
                                                                <asp:DropDownList ID="DdlProgram" Width="250" CssClass="bg-transparent text-blackBlue font-bold" name="program" runat="server" />
                                                            </div>
                                                            <div id="divProgramInfo" runat="server" visible="false" class="w-full rounded-20px text-center flex justify-center items-center bg-lightBlue text-blue font-bold text-sm border border-blue py-5px gap-7px">
                                                                <img class="w-[15px]" src="/assets_out/images/global/lockBlue.svg"><span>
                                                                    <strong>
                                                                        <asp:Label ID="LblProgramInfo" runat="server" /></strong><asp:Label ID="LblProgramInfoContent" runat="server" />
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div id="divSearchMarket" runat="server" class="flex flex-col gap-15px">
                                                            <div id="divSearchMarketCss" runat="server" class="text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2">
                                                                <label for="market">That specialises in:</label>
                                                                <asp:DropDownList ID="DdlMarket" Width="250" CssClass="bg-transparent text-blackBlue font-bold" name="market" runat="server" />
                                                            </div>
                                                            <div id="divMarketInfo" runat="server" visible="false" class="w-full rounded-20px text-center flex justify-center items-center bg-lightBlue text-blue font-bold text-sm border border-blue py-5px gap-7px">
                                                                <img class="w-[15px]" src="/assets_out/images/global/lockBlue.svg"><span>
                                                                    <strong>
                                                                        <asp:Label ID="LblMarketInfo" runat="server" /></strong><asp:Label ID="LblMarketInfoContent" runat="server" />
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div id="divSearchApi" runat="server" class="flex flex-col gap-15px">
                                                            <div id="divSearchApiCss" runat="server" class="text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2">
                                                                <label for="api">With interest in this API:</label>
                                                                <asp:DropDownList ID="DdlApi" Width="250" CssClass="bg-transparent text-blackBlue font-bold" name="api" runat="server" />
                                                            </div>
                                                            <div id="divApiInfo" runat="server" visible="false" class="w-full rounded-20px text-center flex justify-center items-center bg-lightBlue text-blue font-bold text-sm border border-blue py-5px gap-7px">
                                                                <img class="w-[15px]" src="/assets_out/images/global/lockBlue.svg"><span>
                                                                    <strong>
                                                                        <asp:Label ID="LblApiInfo" runat="server" /></strong><asp:Label ID="LblApiInfoContent" runat="server" />
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div id="divSearchCountry" runat="server" class="flex flex-col gap-15px">
                                                            <div id="divSearchCountryCss" runat="server" class="text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2">
                                                                <label for="country">In this country:</label>
                                                                <asp:DropDownList ID="DdlCountry" Width="250" CssClass="bg-transparent text-blackBlue font-bold" name="country" runat="server" />
                                                            </div>
                                                            <div id="divCountryInfo" runat="server" visible="false" class="w-full rounded-20px text-center flex justify-center items-center bg-lightBlue text-blue font-bold text-sm border border-blue py-5px gap-7px">
                                                                <img class="w-[15px]" src="/assets_out/images/global/lockBlue.svg"><span>
                                                                    <strong>
                                                                        <asp:Label ID="LblCountryInfo" runat="server" /></strong><asp:Label ID="LblCountryInfoContent" runat="server" />
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div id="divSearchName" runat="server" class="flex flex-col gap-15px">
                                                            <div id="divSearchNameCss" runat="server" class="text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2">
                                                                <label for="name">With company name:</label>
                                                                <asp:TextBox ID="TbxCompanyName" Width="250" runat="server" CssClass="bg-transparent text-blackBlue font-bold" name="name" placeholder="Type a company name" />
                                                            </div>
                                                            <div id="divNameInfo" runat="server" visible="false" class="w-full rounded-20px text-center flex justify-center items-center bg-lightBlue text-blue font-bold text-sm border border-blue py-5px gap-7px">
                                                                <img class="w-[15px]" src="/assets_out/images/global/lockBlue.svg"><span>
                                                                    <strong>
                                                                        <asp:Label ID="lblNameInfo" runat="server" /></strong><asp:Label ID="LblNameInfoContent" runat="server" />
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <asp:Button ID="BtnSearch" OnClick="BtnSearch_OnClick" runat="server" Text="APPLY FILTERS" CssClass="btn large font-bold text-xs lg:text-sm bg-blue text-white w-full justify-center" type="submit" />
                                                    </div>
                                                    <div id="divSearchGoPremium" runat="server" visible="false" class="flex flex-col gap-7px">
                                                        <div class="flex gap-7px items-center justify-center">
                                                            <img src="/assets_out/images/global/unlock.svg">
                                                        </div>
                                                        <p class="text-textGray text-sm text-center">Unlock advanced search features and grow your opportunities by purchasing one of our premium products.</p>
                                                        <a id="aSearchGoPremium" visible="false" runat="server" class="text-center text-blue font-bold text-base" href="#">
                                                            <asp:Label ID="LblSearchGoPremium" runat="server" />
                                                        </a>
                                                    </div>
                                                </div>
                                            </aside>
                                        </div>
                                    </div>
                                    <div class="gap-40px flex flex-col">

                                        <asp:Repeater ID="RdgResults" OnItemDataBound="RdgResults_ItemDataBound" runat="server">
                                            <ItemTemplate>
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
                                                                    <div id="divMSPs" runat="server" visible="false" class="flex gap-7px items-center flex-col lg:flex-row justify-center">
                                                                        <div class="flex gap-7px items-center">
                                                                            <img src="/assets_out/images/global/circles.svg" alt="company type icon" loading="lazy">
                                                                        </div>
                                                                        <a id="aMSPs" class="text-xs lg:text-xs py-5px px-10px bg-gray rounded-20px text-blue" runat="server">
                                                                            <strong class="text-xs lg:text-sm font-semibold">Managed Service Provider (MSP)</strong>
                                                                        </a>
                                                                    </div>
                                                                    <div class="country bg-gradient-product overflow-hidden rounded-30px p-5px flex gap-5px items-center w-fit">
                                                                        <%--<img class="w-[15px] lg:w-[20px]" src="/assets_out/images/flags/US.svg" alt="flag US" loading="lazy">--%>
                                                                        <h4 class="text-xs lg:text-sm font-semibold font-bold"><%# Eval("Country") %></h4>
                                                                    </div>
                                                                    <div id="divAllCity" runat="server" class="flex gap-7px items-center">
                                                                        <img src="/assets_out/images/company-item/location.svg" alt="location icon" loading="lazy">
                                                                        <h4 class="text-xs lg:text-sm font-semibold">
                                                                            <a id="aAllCityChannelPartners" runat="server">
                                                                                <%# Eval("City") %>
                                                                            </a>
                                                                        </h4>
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
                                                                        <h4 class="text-xs lg:text-sm font-semibold">
                                                                            <asp:Label ID="LblProducts" runat="server" />
                                                                        </h4>
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
                                                            <a id="aWebsite" href='<%# Eval("WebSite") %>' target="_blank" runat="server" visible="false" class="btn large font-bold text-xs xl:text-sm bg-yellow text-white justify-center w-full lg:w-fit">
                                                                <span>VIEW WEBSITE</span></a>
                                                            <a id="aContact" visible="false" runat="server" class="btn large font-bold text-xs xl:text-sm bg-blue text-white justify-center w-full lg:w-fit">
                                                                <span>CONTACT US</span></a>
                                                            <a id="aRFPsForm" visible="false" runat="server" class="btn large font-bold text-xs xl:text-sm bg-blue text-white justify-center w-full lg:w-fit">
                                                                <asp:Label ID="LblRFPsForm" Text="Get a Quote" runat="server" />
                                                            </a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                        <div class="w-full flex flex-col gap-50px">
                                            <controls:MessageControl ID="UcNoResults" runat="server" />
                                        </div>
                                        <div class="flex justify-center gap-15px">
                                            <asp:Button ID="lbtnPrevBottom" runat="server" Style="width: 90px !important; cursor: pointer;" Text="Previous" OnClick="lbtnPrev_Click" CssClass="w-[25px] h-[25px] lg:w-[35px] lg:h-[35px] rounded-full hover:bg-blue hover:text-white bg-white shadow-card flex justify-center text-center items-center" />
                                            <asp:Button ID="lbtnNextBottom" runat="server" Style="width: 90px !important; cursor: pointer;" Text="Next" OnClick="lbtnNext_Click" CssClass="w-[25px] h-[25px] lg:w-[35px] lg:h-[35px] rounded-full hover:bg-blue hover:text-white bg-white shadow-card flex justify-center text-center items-center" />
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
                                        <img src="/assets_out/images/search/partner-programs.svg">
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
                                        <img src="/assets_out/images/search/channel-partners.svg">
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
                                        <img src="/assets_out/images/search/partnerships.svg">
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
