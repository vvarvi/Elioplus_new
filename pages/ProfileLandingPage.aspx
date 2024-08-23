<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="ProfileLandingPage.aspx.cs" Inherits="WdS.ElioPlus.pages.ProfileLandingPage" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" id="metaDescription" runat="server" />
    <meta name="keywords" id="metaKeywords" runat="server" />
</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Conditional">
        <ContentTemplate>
            <main class="transition-all duration-200">
                <section class="bg-gray">
                    <div class="container">
                        <div class="px-30px lg:px-80px py-30px lg:px-40px gap-30px lg:gap-50px bg-white shadow-card flex flex-col rounded-20px">
                            <div class="flex w-full flex-col lg:flex-row items-center gap-20px">
                                <div class="w-[80px] h-[80px] lg:w-[150px] lg:h-[150px] border-blue border-2 p-5px flex items-center justify-center rounded-full">
                                    <img id="ImgCompanyLogo" runat="server" class="w-full" src="/assets_out/images/company-profile/cupix.png" alt="cupix" width="150" height="150" />

                                </div>
                                <div class="flex flex-col grow gap-10px">
                                    <div class="flex flex-col-reverse lg:flex-row gap-20px w-full items-center">
                                        <h1 class="text-2xl lg:text-3xl font-inter font-bold">
                                            <asp:Label ID="LblCompanyName" runat="server" />
                                        </h1>
                                        <div id="divFeaturedProfile" runat="server" visible="false" class="flex gap-7px items-center px-10px py-5px bg-gray rounded-30px">
                                            <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                <g clip-path="url(#clip0_457_22)">
                                                    <path d="M15.4609 6.23701C15.3627 5.93327 15.0933 5.71755 14.7746 5.68882L10.4452 5.29571L8.7332 1.28869C8.60697 0.995023 8.31949 0.804932 8.00008 0.804932C7.68066 0.804932 7.39318 0.995023 7.26695 1.28937L5.55498 5.29571L1.22489 5.68882C0.906732 5.71824 0.638017 5.93327 0.539252 6.23701C0.440487 6.54074 0.531699 6.87389 0.772375 7.08389L4.0449 9.95392L3.07991 14.2047C3.00929 14.5173 3.1306 14.8403 3.38993 15.0278C3.52933 15.1285 3.69241 15.1798 3.85687 15.1798C3.99866 15.1798 4.13931 15.1415 4.26554 15.066L8.00008 12.834L11.7332 15.066C12.0064 15.2304 12.3508 15.2154 12.6095 15.0278C12.869 14.8398 12.9902 14.5166 12.9196 14.2047L11.9546 9.95392L15.2271 7.08447C15.4678 6.87389 15.5597 6.54132 15.4609 6.23701Z" fill="#FFB618" />
                                                </g>
                                                <defs>
                                                    <clipPath id="clip0_457_22">
                                                        <rect width="15" height="15" fill="white" transform="translate(0.5 0.483032)" />
                                                    </clipPath>
                                                </defs>
                                            </svg>

                                            <h4 class="text-xs lg:text-sm font-bold text-yellow">Featured Company</h4>
                                        </div>
                                    </div>
                                    <div class="flex gap-20px w-full items-center flex-col lg:flex-row">
                                        <div class="flex gap-7px items-center flex-col lg:flex-row justify-center">
                                            <div class="flex gap-7px items-center">
                                                <svg width="18" height="19" viewBox="0 0 18 19" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                    <path d="M8.95734 5.12856C10.2416 5.12856 11.2826 4.09242 11.2826 2.81428C11.2826 1.53614 10.2416 0.5 8.95734 0.5C7.67314 0.5 6.63208 1.53614 6.63208 2.81428C6.63208 4.09242 7.67314 5.12856 8.95734 5.12856Z" fill="#39B4EF" />
                                                    <path d="M8.95734 11.8141C10.2416 11.8141 11.2826 10.778 11.2826 9.49983C11.2826 8.22169 10.2416 7.18555 8.95734 7.18555C7.67314 7.18555 6.63208 8.22169 6.63208 9.49983C6.63208 10.778 7.67314 11.8141 8.95734 11.8141Z" fill="#39B4EF" />
                                                    <path d="M8.95734 18.5001C10.2416 18.5001 11.2826 17.464 11.2826 16.1859C11.2826 14.9077 10.2416 13.8716 8.95734 13.8716C7.67314 13.8716 6.63208 14.9077 6.63208 16.1859C6.63208 17.464 7.67314 18.5001 8.95734 18.5001Z" fill="#39B4EF" />
                                                    <path d="M2.75666 11.8141C4.04087 11.8141 5.08192 10.778 5.08192 9.49983C5.08192 8.22169 4.04087 7.18555 2.75666 7.18555C1.47245 7.18555 0.431396 8.22169 0.431396 9.49983C0.431396 10.778 1.47245 11.8141 2.75666 11.8141Z" fill="#39B4EF" />
                                                    <path d="M15.6746 11.8141C16.9589 11.8141 17.9999 10.778 17.9999 9.49983C17.9999 8.22169 16.9589 7.18555 15.6746 7.18555C14.3904 7.18555 13.3494 8.22169 13.3494 9.49983C13.3494 10.778 14.3904 11.8141 15.6746 11.8141Z" fill="#39B4EF" />
                                                </svg>

                                                <h4 class="text-xs lg:text-sm font-semibold">Company Type:</h4>
                                            </div>
                                            <strong class="text-xs lg:text-sm font-bold">
                                                <asp:Label ID="LblCompanyType" runat="server" />
                                            </strong>
                                        </div>
                                        <div class="country bg-gradient-product overflow-hidden rounded-30px p-5px flex gap-5px items-center w-fit">
                                            <%--<img class="w-[15px] lg:w-[20px]" src="/assets_out/images/flags/US.svg">--%>
                                            <h4 class="text-xs lg:text-sm font-semibold font-bold">Country: <strong>
                                                <asp:Label ID="LblCompanyCountryText" runat="server" /></strong></h4>
                                        </div>
                                        <div id="divCity" runat="server" class="country bg-gradient-case overflow-hidden rounded-30px p-5px flex gap-5px items-center w-fit">
                                            <%--<img class="w-[15px] lg:w-[20px]" src="/assets_out/images/flags/US.svg">--%>
                                            <h4 class="text-xs lg:text-sm font-semibold font-bold">City: <strong>
                                                <asp:Label ID="LblCompanyCityText" runat="server" /></strong></h4>
                                        </div>
                                    </div>
                                    <div class="flex-row gap-10px justify-start lg:justify-start  flex">
                                        <a id="aWebsite" visible="false" runat="server" class="btn font-bold text-xs xl:text-sm bg-blue text-white justify-center w-full lg:w-fit">
                                            <span>WEBSITE</span>
                                        </a>

                                        <a id="aContact" visible="false" runat="server" class="btn font-bold text-xs xl:text-sm bg-blue text-white justify-center w-full lg:w-fit">
                                            <span>GET A QUOTE</span>
                                        </a>
                                    </div>
                                </div>
                            </div>

                            <div class="gap-15px lg:gap-25px flex flex-col justify-center w-full">
                                <div class="divider"></div>
                                <h2 class="text-2xl lg:text-2.5xl font-bold font-inter text-left">Overview:</h2>
                                <p class="w-full text-base lg:text-xl text-left">
                                    <asp:Label ID="LblOverview" runat="server" />
                                </p>
                            </div>

                            <div id="divDescriptionArea" runat="server" class="gap-15px lg:gap-25px flex flex-col justify-center w-full">
                                <div class="divider"></div>
                                <h2 class="text-2xl lg:text-2.5xl font-bold font-inter text-left">Partner Program Description:</h2>
                                <p class="w-full text-base lg:text-xl text-left">
                                    <asp:Label ID="LblDescription" runat="server" />
                                </p>
                            </div>

                            <div id="divChannelPartnersProducts" runat="server" class="gap-15px lg:gap-25px flex flex-col justify-center w-full">
                                <div class="divider"></div>
                                <h2 class="text-2xl lg:text-2.5xl font-bold font-inter text-left">
                                    <asp:Label ID="LblChannelPartnersProductsTitle" Text="Products/Vendors" runat="server" />
                                </h2>
                                <div class="flex-col lg:flex-row gap-10px lg:gap-50px flex justify-start w-full">
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhProductsContent1" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhProductsContent2" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhProductsContent3" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div id="divVendorsIntegrations" runat="server" class="gap-15px lg:gap-25px flex flex-col justify-center w-full">
                                <div class="divider"></div>
                                <h2 class="text-2xl lg:text-2.5xl font-bold font-inter text-left">Integrations</h2>
                                <div class="flex-col lg:flex-row gap-10px lg:gap-50px flex justify-start w-full">
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhVendorsIntegrations1" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhVendorsIntegrations2" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhVendorsIntegrations3" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div id="divCategories" runat="server" class="gap-15px lg:gap-25px flex flex-col justify-center w-full">
                                <div class="divider"></div>
                                <h2 class="text-2xl lg:text-2.5xl font-bold font-inter text-left">Categories:</h2>
                                <div class="flex-col lg:flex-row gap-10px lg:gap-50px flex justify-start w-full">
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhVerticalsContent1" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhVerticalsContent2" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhVerticalsContent3" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div id="divCityCategory" visible="false" runat="server" class="gap-15px lg:gap-25px flex flex-col justify-center w-full">
                                <div class="divider"></div>
                                <h2 class="text-2xl lg:text-2.5xl font-bold font-inter text-left">
                                    <asp:Label ID="LblSimilarCityCompanyTitle" runat="server" />
                                </h2>
                                <div class="flex-col lg:flex-row gap-10px lg:gap-50px flex justify-start w-full">
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhVerticalsCityContent1" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhVerticalsCityContent2" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhVerticalsCityContent3" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div id="divCityProduct" visible="false" runat="server" class="gap-15px lg:gap-25px flex flex-col justify-center w-full">
                                <div class="divider"></div>
                                <h2 class="text-2xl lg:text-2.5xl font-bold font-inter text-left">
                                    <asp:Label ID="LblCityProductTitle" runat="server" />
                                </h2>
                                <div class="flex-col lg:flex-row gap-10px lg:gap-50px flex justify-start w-full">
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhProductsCityContent1" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhProductsCityContent2" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhProductsCityContent3" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div id="divCityVerticalsTrans" visible="false" runat="server" class="gap-15px lg:gap-25px flex flex-col justify-center w-full">
                                <div class="divider"></div>
                                <h2 class="text-2xl lg:text-2.5xl font-bold font-inter text-left">
                                    <asp:Label ID="LblSimilarCityCompanyTitleTrans" runat="server" />
                                </h2>
                                <div class="flex-col lg:flex-row gap-10px lg:gap-50px flex justify-start w-full">
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhVerticalsCityContentTrans1" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhVerticalsCityContentTrans2" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhVerticalsCityContentTrans3" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div id="divCityProductTrans" visible="false" runat="server" class="gap-15px lg:gap-25px flex flex-col justify-center w-full">
                                <div class="divider"></div>
                                <h2 class="text-2xl lg:text-2.5xl font-bold font-inter text-left">
                                    <asp:Label ID="LblCityProductTransTitle" runat="server" />
                                </h2>
                                <div class="flex-col lg:flex-row gap-10px lg:gap-50px flex justify-start w-full">
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhProductsCityContentTrans1" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhProductsCityContentTrans2" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhProductsCityContentTrans3" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div id="divCountryVerticalsTrans" visible="false" runat="server" class="gap-15px lg:gap-25px flex flex-col justify-center w-full">
                                <div class="divider"></div>
                                <h2 class="text-2xl lg:text-2.5xl font-bold font-inter text-left">
                                    <asp:Label ID="LblSimilarCountryCompanyTitleTrans" runat="server" />
                                </h2>
                                <div class="flex-col lg:flex-row gap-10px lg:gap-50px flex justify-start w-full">
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhVerticalsCountryContentTrans1" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhVerticalsCountryContentTrans2" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhVerticalsCountryContentTrans3" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div id="divCountryProductTrans" visible="false" runat="server" class="gap-15px lg:gap-25px flex flex-col justify-center w-full">
                                <div class="divider"></div>
                                <h2 class="text-2xl lg:text-2.5xl font-bold font-inter text-left">
                                    <asp:Label ID="LblCountryProductTransTitle" runat="server" />
                                </h2>
                                <div class="flex-col lg:flex-row gap-10px lg:gap-50px flex justify-start w-full">
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhProductsCountryContentTrans1" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhProductsCountryContentTrans2" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhProductsCountryContentTrans3" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div id="divIndustries" runat="server" class="gap-15px lg:gap-25px flex flex-col justify-center w-full">
                                <div class="divider"></div>
                                <h2 class="text-2xl lg:text-2.5xl font-bold font-inter text-left">Industries:</h2>
                                <div class="flex-col lg:flex-row gap-10px lg:gap-50px flex justify-start w-full">
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhIndustries1" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhIndustries2" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhIndustries3" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div id="divMarkets" runat="server" class="gap-15px lg:gap-25px flex flex-col justify-center w-full">
                                <div class="divider"></div>
                                <h2 class="text-2xl lg:text-2.5xl font-bold font-inter text-left">Market Specialization:</h2>
                                <div class="flex-col lg:flex-row gap-10px lg:gap-50px flex justify-start w-full">
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhMarkets1" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhMarkets2" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhMarkets3" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div id="divVendorsPartnerPrograms" runat="server" class="gap-15px lg:gap-25px flex flex-col justify-center w-full">
                                <div class="divider"></div>
                                <h2 class="text-2xl lg:text-2.5xl font-bold font-inter text-left">Partner Program available for:</h2>
                                <div class="flex-col lg:flex-row gap-10px lg:gap-50px flex justify-start w-full">
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhPartPrV1" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhPartPrV2" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhPartPrV3" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div id="divResellersPartnerPrograms" runat="server" class="gap-15px lg:gap-25px flex flex-col justify-center w-full">
                                <div class="divider"></div>
                                <h2 class="text-2xl lg:text-2.5xl font-bold font-inter text-left">Partner Program available for:</h2>
                                <div class="flex-col lg:flex-row gap-10px lg:gap-50px flex justify-start w-full">
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhPartPrR1" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhPartPrR2" runat="server" />
                                    </div>
                                    <div class="flex flex-col gap-20px">
                                        <asp:PlaceHolder ID="PhPartPrR3" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div class="gap-15px lg:gap-25px flex flex-col justify-center w-full">
                                <div class="divider"></div>
                                <h2 class="text-2xl lg:text-2.5xl font-bold font-inter text-left">Contact Details:</h2>
                                <div class="flex-col lg:flex-row gap-10px lg:gap-50px flex justify-start w-full">
                                    <div class="w-fit flex gap-10px items-center">
                                        <div class="w-[15px] h-[15px]">
                                            <img src="/assets_out/images/company-profile/location.svg">
                                        </div>
                                        <h4 class="text-base lg:text-lg font-semibold">
                                            <asp:Label ID="LblAddress" runat="server" />
                                        </h4>
                                    </div>
                                    <div class="w-fit flex gap-10px items-center">
                                        <div class="w-[15px] h-[15px]">
                                            <img src="/assets_out/images/company-profile/phone.svg">
                                        </div>
                                        <h4 class="text-base lg:text-lg font-semibold">
                                            <asp:Label ID="LblPhone" runat="server" />
                                        </h4>
                                    </div>
                                    <a id="aEmailTo" runat="server" visible="false" class="w-fit flex gap-10px items-center">
                                        <div class="w-[15px] h-[15px]">
                                            <img src="/assets_out/images/company-profile/mail.svg">
                                        </div>
                                        <h4 class="text-base lg:text-lg font-semibold">
                                            <asp:Label ID="LblEmail" Text="info@cupix.com" runat="server" />
                                        </h4>
                                    </a>
                                </div>
                            </div>

                            <div class="divider"></div>
                            <div class="flex-row gap-10px justify-start lg:justify-start  flex">
                                <a id="aClaimProfile" runat="server" visible="false" class="btn large font-bold text-xs xl:text-sm bg-blue text-white justify-center w-full lg:w-fit">
                                    <span>CLAIM PROFILE</span></a>
                            </div>
                        </div>
                    </div>
                </section>
                <section>
                    <div class="container">
                        <div id="divSimilarResults" runat="server" visible="false" class="grid-cols-1 lg:grid-cols-company gap-30px lg:gap-50px grid">
                            <div class="gap-40px flex flex-col">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">
                                    <asp:Label ID="LblSimilarCompanies" runat="server" />
                                </h3>
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
                                                                <h4 class="text-xs lg:text-sm font-semibold">Products:</h4>
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
                            </div>
                            <div>
                                <div class="relative h-full">
                                    <aside class="sidebar-container pb-0 lg:pb-80px flex flex-col gap-50px">
                                        <div id="divAcronisAds" runat="server" visible="false" class="flex flex-col gap-15px w-full justify-center items-center">
                                            <a id="aAcronisPromo" runat="server" href="https://promo.acronis.com/Migration-Promo.html?utm_source=elioplus&utm_id=elioplus_digital" target="_blank">
                                                <img class="shadow-card rounded-10px w-fit" src="/assets_out/images/banners/acronis-banner.png" alt="Acronis sponsored banner" loading="lazy">
                                            </a>
                                            <div class="flex flex-col gap-7px">
                                                <div class="flex gap-7px items-center justify-center">
                                                    <img src="/assets_out/images/global/info-dark.svg" alt="info icon" loading="lazy" />
                                                    <h4 class="text-base font-bold">Advertisement</h4>
                                                </div>
                                                <p class="text-textGray text-sm text-center">
                                                    <asp:Label ID="LblAdv" runat="server" />
                                                </p>
                                                <a class="text-center text-blue font-bold text-base" href="/contact-us">Contact us!</a>
                                            </div>
                                        </div>
                                        <div id="divAcronisAds2" runat="server" visible="false" class="flex flex-col gap-15px w-full justify-center items-center">
                                            <a id="aAcronisPromo2" runat="server" href="https://www.acronis.com/en-eu/products/cyber-protect/trial/" target="_blank">
                                                <img class="shadow-card rounded-10px w-fit" src="/images/LandingPages/Ads/acronis2-ads.png" alt="Acronis sponsored banner" loading="lazy">
                                            </a>
                                            <div class="flex flex-col gap-7px">
                                                <div class="flex gap-7px items-center justify-center">
                                                    <img src="/assets_out/images/global/info-dark.svg" alt="info icon" loading="lazy" />
                                                    <h4 class="text-base font-bold">Advertisement</h4>
                                                </div>
                                                <p class="text-textGray text-sm text-center">
                                                    <asp:Label ID="LblAdv2" runat="server" />
                                                </p>
                                                <a class="text-center text-blue font-bold text-base" href="/contact-us">Contact us!</a>
                                            </div>
                                        </div>

                                        <div id="divRelatedCompanies" runat="server" visible="false" class="py-25px px-20px flex flex-col gap-15px bg-white shadow-card w-full rounded-10px">
                                            <h4 class="text-base font-bold">
                                                <asp:Label ID="LblFooterRelatedCompanies" Text="Related Categories:" runat="server" /></h4>
                                            <div class="divider"></div>
                                            <div class="flex flex-col gap-5px w-full">
                                                <a id="aFooterCompany1" runat="server" class="text-sm font-semibold underline text-blue">
                                                    <asp:Label ID="LblFooterCompany1" Text="Affiliate Marketing" runat="server" /></a>
                                                <a id="aFooterCompany2" runat="server" class="text-sm font-semibold underline text-blue">
                                                    <asp:Label ID="LblFooterCompany2" Text="Campaign Management" runat="server" /></a>
                                                <a id="aFooterCompany3" runat="server" class="text-sm font-semibold underline text-blue">
                                                    <asp:Label ID="LblFooterCompany3" Text="Content Marketing" runat="server" /></a>
                                                <a id="aFooterCompany4" runat="server" class="text-sm font-semibold underline text-blue">
                                                    <asp:Label ID="LblFooterCompany4" Text="Sales Intelligence" runat="server" /></a>
                                                <a id="aFooterCompany5" runat="server" class="text-sm font-semibold underline text-blue">
                                                    <asp:Label ID="LblFooterCompany5" Text="Sales Process Management" runat="server" /></a>
                                                <a id="aFooterCompany6" runat="server" class="text-sm font-semibold underline text-blue">
                                                    <asp:Label ID="LblFooterCompany6" Text="SEO & SEM" runat="server" /></a>
                                            </div>
                                        </div>
                                    </aside>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </main>

            <!-- Form Modal -->
            <div id="PopUpRFPsForm" class="modal fixed p-25px bg-white shadow-card z-50 hidden">
                <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                    <ContentTemplate>
                        <div class="flex flex-col w-full gap-30px">
                            <h4 class="text-base lg:text-xl font-bold font-inter text-center lg:text-left">
                                <asp:Label ID="LblMessageTitle" Text="Request a quote form" runat="server" />
                            </h4>
                            <form class="flex flex-col gap-15px w-full" method="POST">
                                <div id="divStepOne" runat="server" class="">
                                    <div class="grid-cols-1 lg:grid-cols-2 gap-30px lg:gap-50px grid">
                                        <div class="input-field">
                                            <label class="text-sm font-semibold">Product/Technology:</label>
                                            <asp:TextBox ID="TbxProduct" runat="server" MaxLength="100" name="firstname" type="text" placeholder="Product/Technology" />
                                        </div>
                                        <div class="input-field">
                                            <label class="text-sm font-semibold">Number of units:</label>
                                            <asp:TextBox ID="TbxNumberUnits" runat="server" MaxLength="20" name="lastname" type="text" placeholder="Number of units" />
                                        </div>
                                    </div>
                                    <div class="input-field">
                                        <label class="text-sm font-semibold">Message:</label>
                                        <asp:TextBox ID="TbxMessage" runat="server" MaxLength="2000" name="message" type="text" TextMode="MultiLine" Rows="5" placeholder="Enter your message" />
                                    </div>
                                </div>
                                <div id="divStepTwo" visible="false" runat="server" class="">
                                    <div class="grid-cols-1 lg:grid-cols-2 gap-30px lg:gap-50px grid">
                                        <div class="input-field">
                                            <label class="text-sm font-semibold">First Name:</label>
                                            <asp:TextBox ID="TbxFirstName" runat="server" MaxLength="100" name="companyname" type="text" placeholder="First Name" />
                                        </div>
                                        <div class="input-field">
                                            <label class="text-sm font-semibold">Last Name:</label>
                                            <asp:TextBox ID="TbxLastName" runat="server" MaxLength="100" name="companyname" type="text" placeholder="Last Name" />
                                        </div>
                                    </div>
                                    <div class="grid-cols-1 lg:grid-cols-2 gap-30px lg:gap-50px grid">
                                        <div class="input-field">
                                            <label class="text-sm font-semibold">Company Name:</label>
                                            <asp:TextBox ID="TbxBusinessName" runat="server" MaxLength="15" name="phone" type="number" placeholder="Company Name" />
                                        </div>
                                        <div class="input-field">
                                            <label class="text-sm font-semibold">Company Email:</label>
                                            <asp:TextBox ID="TbxCompanyEmail" runat="server" MaxLength="45" name="email" type="text" placeholder="Company Email" />
                                        </div>
                                    </div>
                                    <div class="grid-cols-1 lg:grid-cols-2 gap-30px lg:gap-50px grid">
                                        <div class="input-field">
                                            <label class="text-sm font-semibold">Country:</label>
                                            <asp:DropDownList ID="DdlCountries" runat="server" />
                                        </div>
                                        <div class="input-field">
                                            <label class="text-sm font-semibold">Phone Number:</label>
                                            <asp:TextBox ID="TbxPhoneNumber" runat="server" MaxLength="20" name="email" type="text" placeholder="Phone Number" />
                                        </div>
                                    </div>
                                    <div class="input-field">
                                        <label class="text-sm font-semibold">City:</label>
                                        <asp:TextBox ID="TbxCity" runat="server" MaxLength="20" name="email" type="text" placeholder="City" />
                                    </div>
                                </div>
                                <asp:HiddenField ID="HdnLeadId" Value="0" runat="server" />
                                <div class="w-full flex justify-end">
                                    <asp:Button ID="BtnBack" OnClick="BtnBack_Click" Text="BACK" Visible="false" class="btn large font-bold text-sm bg-blue text-white justify-center" type="submit" runat="server" />
                                    <asp:Button ID="BtnProceed" OnClick="BtnProceed_Click" Text="NEXT" class="btn large font-bold text-sm bg-blue text-white justify-center" type="submit" runat="server" />
                                </div>
                                <div class="w-full flex flex-col">
                                    <div id="divDemoWarningMsg" runat="server" visible="false" class="card bg-red text-white gap-15px">
                                        <div class="flex items-center gap-5px">
                                            <img src="/assets_out/images/alerts/error-white.svg" alt="Quick Actions Icon" title="Click to toggle the quick actions menu" width="20" height="20">
                                            <h3 class="font-bold text-base text-white">
                                                <asp:Label ID="LblDemoWarningMsg" runat="server" />
                                            </h3>
                                        </div>
                                        <p class="text-sm text-white">
                                            <asp:Label ID="LblDemoWarningMsgContent" runat="server" />
                                        </p>
                                    </div>

                                    <div id="divDemoSuccessMsg" runat="server" visible="false" class="card bg-green text-white gap-15px">
                                        <div class="flex items-center gap-5px">
                                            <img src="/assets_out/images/alerts/success-white.svg" alt="Quick Actions Icon" title="Click to toggle the quick actions menu" width="20" height="20">
                                            <h3 class="font-bold text-base text-white">
                                                <asp:Label ID="LblDemoSuccessMsg" runat="server" />
                                            </h3>
                                        </div>
                                        <p class="text-sm text-white">
                                            <asp:Label ID="LblDemoSuccessMsgContent" runat="server" />
                                        </p>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <!-- Claim Profile form (modal view) -->
            <div id="claimProfileModal" class="modal fixed p-25px bg-white shadow-card z-50 hidden">
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>
                        <div class="flex flex-col w-full gap-30px">
                            <h4 class="text-base lg:text-xl font-bold font-inter text-center lg:text-left">
                                <asp:Label ID="LblClaimMessageHeader" Text="Request a quote form" runat="server" />
                            </h4>
                            <form class="flex flex-col gap-15px w-full" method="POST">
                                <div class="input-field">
                                    <label class="text-sm font-semibold">Enter Your E-mail:</label>
                                    <asp:TextBox ID="TbxClaimMessageEmail" MaxLength="100" placeholder="Your E-mail" runat="server" />
                                </div>
                                <div class="w-full flex justify-end">
                                    <asp:Button ID="BtnCancelClaimMsg" OnClick="BtnCancelClaimMsg_OnClick" Text="Cancel" class="btn large font-bold text-sm bg-blue text-white justify-center" type="submit" runat="server" />
                                    <asp:Button ID="BtnSendClaim" OnClick="BtnSendClaim_OnClick" Text="NEXT" class="btn large font-bold text-sm bg-blue text-white justify-center" type="submit" runat="server" />
                                </div>
                                <div class="w-full flex flex-col">
                                    <div id="divClaimWarningMsg" runat="server" visible="false" class="card bg-red text-white gap-15px">
                                        <div class="flex items-center gap-5px">
                                            <img src="/assets_out/images/alerts/error-white.svg" alt="Quick Actions Icon" title="Click to toggle the quick actions menu" width="20" height="20">
                                            <h3 class="font-bold text-base text-white">
                                                <asp:Label ID="LblClaimWarningMsg" runat="server" />
                                            </h3>
                                        </div>
                                        <p class="text-sm text-white">
                                            <asp:Label ID="LblClaimWarningMsgContent" runat="server" />
                                        </p>
                                    </div>

                                    <div id="divClaimSuccessMsg" runat="server" visible="false" class="card bg-green text-white gap-15px">
                                        <div class="flex items-center gap-5px">
                                            <img src="/assets_out/images/alerts/success-white.svg" alt="Quick Actions Icon" title="Click to toggle the quick actions menu" width="20" height="20">
                                            <h3 class="font-bold text-base text-white">
                                                <asp:Label ID="LblClaimSuccessMsg" runat="server" />
                                            </h3>
                                        </div>
                                        <p class="text-sm text-white">
                                            <asp:Label ID="LblClaimSuccessMsgContent" runat="server" />
                                        </p>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

                <script type="text/javascript">

                    function OpenRFPsPopUp() {
                        $('#PopUpRFPsForm').modal('show');
                    }

                    function CloseRFPsPopUp() {
                        $('#PopUpRFPsForm').modal('hide');
                    }

                    function OpenClaimProfilePopUp() {
                        $('#claimProfileModal').modal('show');
                    }
                    function CloseClaimProfilePopUp() {
                        $('#claimProfileModal').modal('hide');
                    }
                </script>

            </telerik:RadScriptBlock>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
