<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WdS.ElioPlus.Default" %>

<asp:Content ID="HomeHeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Recruit new channel partners & manage your partner network in an all-in-one solution. Attract new resellers, consultants, VARs and MSPs to help you scale your indirect sales." />
    <meta name="keywords" content="channel partners, software resellers, VARs, partner program, partner recruitment, partner management, vendor management, Partner Relationship Management" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"> 
    </script>
    <script>
        function recordData(data) {
            console.log(data.ip, data.country);
        }
    </script>
    <script src="https://ipinfo.io/json?callback=recordData"></script>

</asp:Content>

<asp:Content ID="HomeMainContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <main class="transition-all duration-200">
                <section class="bg-gray opener">
                    <div class="container">
                        <div class="grid-cols-1 lg:grid-cols-2 grid gap-30px">
                            <div class="row-start-2 lg:row-start-1 gap-20px lg:gap-30px flex flex-col">
                                <div class="text-center lg:text-left gap-10px lg:gap-15px flex flex-col">
                                    <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">
                                        <asp:Label ID="Label1" runat="server" />
                                    </h1>
                                    <h2 class="text-2xl lg:text-3xl font-bold font-inter text-blue">
                                        <asp:Label ID="Label2" runat="server" /></h2>
                                </div>
                                <p class="text-base lg:text-body text-center lg:text-left">
                                    <asp:Label ID="Label3" runat="server" />
                                </p>
                                <div class="flex items-center gap-10px xl:gap-15px flex-col lg:flex-row">
                                    <a id="aBrowseCompanies" runat="server" class="btn large font-bold text-sm xl:text-base"><span>
                                        <asp:Label ID="Label4" runat="server" /></span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                        </defs></svg>
                                    </a><a id="aSignUp" runat="server" class="btn large font-bold text-sm xl:text-base bg-blue text-white">
                                        <svg width="18" height="19" viewBox="0 0 18 19" fill="none" xmlns="http://www.w3.org/2000/svg">
                                            <path d="M8.95734 5.12856C10.2416 5.12856 11.2826 4.09242 11.2826 2.81428C11.2826 1.53614 10.2416 0.5 8.95734 0.5C7.67314 0.5 6.63208 1.53614 6.63208 2.81428C6.63208 4.09242 7.67314 5.12856 8.95734 5.12856Z" fill="#39B4EF" />
                                            <path d="M8.95734 11.8141C10.2416 11.8141 11.2826 10.778 11.2826 9.49983C11.2826 8.22169 10.2416 7.18555 8.95734 7.18555C7.67314 7.18555 6.63208 8.22169 6.63208 9.49983C6.63208 10.778 7.67314 11.8141 8.95734 11.8141Z" fill="#39B4EF" />
                                            <path d="M8.95734 18.5001C10.2416 18.5001 11.2826 17.464 11.2826 16.1859C11.2826 14.9077 10.2416 13.8716 8.95734 13.8716C7.67314 13.8716 6.63208 14.9077 6.63208 16.1859C6.63208 17.464 7.67314 18.5001 8.95734 18.5001Z" fill="#39B4EF" />
                                            <path d="M2.75666 11.8141C4.04087 11.8141 5.08192 10.778 5.08192 9.49983C5.08192 8.22169 4.04087 7.18555 2.75666 7.18555C1.47245 7.18555 0.431396 8.22169 0.431396 9.49983C0.431396 10.778 1.47245 11.8141 2.75666 11.8141Z" fill="#39B4EF" />
                                            <path d="M15.6746 11.8141C16.9589 11.8141 17.9999 10.778 17.9999 9.49983C17.9999 8.22169 16.9589 7.18555 15.6746 7.18555C14.3904 7.18555 13.3494 8.22169 13.3494 9.49983C13.3494 10.778 14.3904 11.8141 15.6746 11.8141Z" fill="#39B4EF" />
                                        </svg>
                                        <span>
                                            <asp:Label ID="Label5" runat="server" /></span></a>
                                </div>
                            </div>
                            <img src="/assets_out/images/homepage/map.svg" alt="Connecting cloud &amp; software Vendors with Channel Partners">
                        </div>
                    </div>
                    <div class="mt-30px wave">
                        <img src="/assets_out/images/global/wave.svg" alt="Elioplus decorative wave">
                    </div>
                </section>
                <section>
                    <div class="container">
                        <div class="gap-50px lg:gap-80px flex flex-col">
                            <div class="flex flex-col gap-10px text-center items-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter">
                                    <asp:Label ID="Label6" runat="server" /></h3>
                                <p class="w-[770px] max-w-full text-base lg:text-body">
                                    <asp:Label ID="Label8" runat="server" />
                                </p>
                            </div>
                            <div class="gap-30px lg:gap-60px flex w-full flex-col">
                                <div class="gap-30px lg:gap-0 flex w-full justify-between flex-wrap">
                                    <a class="w-[380px] max-w-full bg-white shadow-card overflow-hidden rounded-10px" href="/channel-partner-recruitment">
                                        <div class="h-[250px] bg-gray flex items-center justify-center">
                                            <img src="/assets_out/images/products/partner-recruitment-homepage-small.png">
                                        </div>
                                        <div class="gap-10px lg:gap-15px py-20px lg:py-30px px-20px lg:px-25px bg-white flex flex-col">
                                            <h3 class="text-lg lg:text-2xl font-bold font-inter">
                                                <asp:Label ID="Label12" runat="server" /></h3>
                                            <p class="text-sm lg:text-base">
                                                <asp:Label ID="Label13" runat="server" />
                                            </p>
                                            <div class="btn font-bold text-sm text-blue">
                                                <span>
                                                    <asp:Label ID="Label14" runat="server" /></span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                                    </defs></svg>

                                            </div>
                                        </div>
                                    </a><a class="w-[380px] max-w-full bg-white shadow-card overflow-hidden rounded-10px" href="/prm-software">
                                        <div class="h-[250px] bg-gray flex items-center justify-center">
                                            <img src="/assets_out/images/products/PRM-homepage-small.png">
                                        </div>
                                        <div class="gap-10px lg:gap-15px py-20px lg:py-30px px-20px lg:px-25px bg-white flex flex-col">
                                            <h3 class="text-lg lg:text-2xl font-bold font-inter">
                                                <asp:Label ID="Label15" runat="server" /></h3>
                                            <p class="text-sm lg:text-base">
                                                <asp:Label ID="Label16" runat="server" />
                                            </p>
                                            <div class="btn font-bold text-sm text-blue">
                                                <span>
                                                    <asp:Label ID="Label17" runat="server" /></span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                                    </defs></svg>

                                            </div>
                                        </div>
                                    </a><a class="w-[380px] max-w-full bg-white shadow-card overflow-hidden rounded-10px" href="/intent-signals">
                                        <div class="h-[250px] bg-gray flex items-center justify-center">
                                            <img src="/assets_out/images/products/Lead-generation-homepage-small.png">
                                        </div>
                                        <div class="gap-10px lg:gap-15px py-20px lg:py-30px px-20px lg:px-25px bg-white flex flex-col">
                                            <h3 class="text-lg lg:text-2xl font-bold font-inter">
                                                <asp:Label ID="Label18" runat="server" /></h3>
                                            <p class="text-sm lg:text-base">
                                                <asp:Label ID="Label19" runat="server" />
                                            </p>
                                            <div class="btn font-bold text-sm text-blue">
                                                <span>
                                                    <asp:Label ID="Label20" runat="server" /></span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                                    </defs></svg>

                                            </div>
                                        </div>
                                    </a>
                                </div>
                                <div class="gap-30px lg:gap-50px flex w-full justify-center flex-wrap">
                                    <a class="w-[380px] max-w-full bg-white shadow-card overflow-hidden rounded-10px" href="/referral-software">
                                        <div class="h-[250px] bg-gray flex items-center justify-center">
                                            <img src="/assets_out/images/products/referral-homepage-small.png">
                                        </div>
                                        <div class="gap-10px lg:gap-15px py-20px lg:py-30px px-20px lg:px-25px bg-white flex flex-col">
                                            <h3 class="text-lg lg:text-2xl font-bold font-inter">
                                                <asp:Label ID="Label21" runat="server" /></h3>
                                            <p class="text-sm lg:text-base">
                                                <asp:Label ID="Label22" runat="server" />
                                            </p>
                                            <div class="btn font-bold text-sm text-blue">
                                                <span>
                                                    <asp:Label ID="Label23" runat="server" /></span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                                    </defs></svg>

                                            </div>
                                        </div>
                                    </a><a class="w-[380px] max-w-full bg-white shadow-card overflow-hidden rounded-10px" href="/mergers-acquisition-marketplace">
                                        <div class="h-[250px] bg-gray flex items-center justify-center">
                                            <img src="/assets_out/images/products/ma-marketplace-homepage-small.png">
                                        </div>
                                        <div class="gap-10px lg:gap-15px py-20px lg:py-30px px-20px lg:px-25px bg-white flex flex-col">
                                            <h3 class="text-lg lg:text-2xl font-bold font-inter">
                                                <asp:Label ID="Label24" runat="server" /></h3>
                                            <p class="text-sm lg:text-base">
                                                <asp:Label ID="Label25" runat="server" />
                                            </p>
                                            <div class="btn font-bold text-sm text-blue">
                                                <span>
                                                    <asp:Label ID="Label26" runat="server" /></span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                                    </defs></svg>

                                            </div>
                                        </div>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <section class="bg-white">
                    <div class="container">
                        <div class="flex flex-col gap-40px">
                            <div class="flex flex-col gap-10px text-center items-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter">
                                    <asp:Label ID="Label27" runat="server" /></h3>
                                <p class="w-[855px] max-w-full text-base lg:text-body">
                                    <asp:Label ID="Label28" runat="server" /><asp:Label ID="Label29" runat="server" />
                                </p>
                            </div>
                            <div class="m-carousel flex gap-30px justify-center" data-items="1">
                                <div class="flex items-center justify-center h-full">
                                    <div class="w-max">
                                        <a id="aSap" runat="server">
                                            <img src="/assets_out/images/homepage/sap.png">
                                        </a>
                                    </div>
                                </div>
                                <div class="hidden mdl:flex items-center">
                                    <div class="w-[3px]  separator"></div>
                                </div>
                                <div class="flex items-center justify-center h-full">
                                    <div class="w-max">
                                        <a id="aFreshworks" runat="server">
                                            <img src="/assets_out/images/homepage/freshworks.png">
                                        </a>
                                    </div>
                                </div>
                                <div class="hidden mdl:flex items-center">
                                    <div class="w-[3px]  separator"></div>
                                </div>
                                <div class="flex items-center justify-center">
                                    <div class="w-max">
                                        <a id="aDeloitte" runat="server">
                                            <img src="/assets_out/images/homepage/Deloitte.png">
                                        </a>
                                    </div>
                                </div>
                                <div class="hidden mdl:flex items-center">
                                    <div class="w-[3px]  separator"></div>
                                </div>
                                <div class="flex items-center justify-center h-full">
                                    <div class="w-max">
                                        <a id="aIRI" runat="server">
                                            <img src="/assets_out/images/homepage/iri.png">
                                        </a>
                                    </div>
                                </div>
                                <div class="hidden mdl:flex items-center">
                                    <div class="w-[3px]  separator"></div>
                                </div>
                                <div class="flex items-center justify-center h-full">
                                    <div class="w-max">
                                        <a id="aTotara" runat="server">
                                            <img src="/assets_out/images/homepage/totaralms.png">
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="flex justify-center items-center flex-col lg:flex-row gap-10px xl:gap-20px">
                                <a id="aViewAllCompanies" runat="server" class="btn large font-bold text-sm xl:text-base"><span>
                                    <asp:Label ID="Label31" runat="server" /></span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                    </defs></svg>
                                </a><a id="aHomeAddAccount" runat="server" class="btn large font-bold text-sm xl:text-base bg-blue text-white">
                                    <svg width="18" height="19" viewBox="0 0 18 19" fill="none" xmlns="http://www.w3.org/2000/svg">
                                        <path d="M8.95734 5.12856C10.2416 5.12856 11.2826 4.09242 11.2826 2.81428C11.2826 1.53614 10.2416 0.5 8.95734 0.5C7.67314 0.5 6.63208 1.53614 6.63208 2.81428C6.63208 4.09242 7.67314 5.12856 8.95734 5.12856Z" fill="#39B4EF" />
                                        <path d="M8.95734 11.8141C10.2416 11.8141 11.2826 10.778 11.2826 9.49983C11.2826 8.22169 10.2416 7.18555 8.95734 7.18555C7.67314 7.18555 6.63208 8.22169 6.63208 9.49983C6.63208 10.778 7.67314 11.8141 8.95734 11.8141Z" fill="#39B4EF" />
                                        <path d="M8.95734 18.5001C10.2416 18.5001 11.2826 17.464 11.2826 16.1859C11.2826 14.9077 10.2416 13.8716 8.95734 13.8716C7.67314 13.8716 6.63208 14.9077 6.63208 16.1859C6.63208 17.464 7.67314 18.5001 8.95734 18.5001Z" fill="#39B4EF" />
                                        <path d="M2.75666 11.8141C4.04087 11.8141 5.08192 10.778 5.08192 9.49983C5.08192 8.22169 4.04087 7.18555 2.75666 7.18555C1.47245 7.18555 0.431396 8.22169 0.431396 9.49983C0.431396 10.778 1.47245 11.8141 2.75666 11.8141Z" fill="#39B4EF" />
                                        <path d="M15.6746 11.8141C16.9589 11.8141 17.9999 10.778 17.9999 9.49983C17.9999 8.22169 16.9589 7.18555 15.6746 7.18555C14.3904 7.18555 13.3494 8.22169 13.3494 9.49983C13.3494 10.778 14.3904 11.8141 15.6746 11.8141Z" fill="#39B4EF" />
                                    </svg>
                                    <span>
                                        <asp:Label ID="Label32" runat="server" /></span></a>
                            </div>
                        </div>
                    </div>
                </section>
                <section class="bg-blue bg-particles bg-cover">
                    <div class="container">
                        <div class="gap-30px lg:gap-50px flex flex-col">
                            <div class="flex flex-col gap-10px text-center items-center text-white">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter">
                                    <asp:Label ID="Label33" runat="server" /></h3>
                                <p class="w-[780px] max-w-full text-base lg:text-body">
                                    <asp:Label ID="Label34" runat="server" />
                                </p>
                            </div>
                            <div class="flex-col lg:flex-row gap-30px lg:gap-50px flex text-center items-center justify-center">
                                <div class="w-[250px] max-w-full py-15px lg:py-30px bg-white rounded-10px shadow-card flex flex-col gap-15px items-center">
                                    <div class="w-[60px] h-[60px] lg:w-[85px] lg:h-[85px] p-10px rounded-full bg-lightBlue2 flex items-center justify-center">
                                        <svg width="54" height="53" viewBox="0 0 54 53" fill="none" xmlns="http://www.w3.org/2000/svg">
                                            <path fill-rule="evenodd" clip-rule="evenodd" d="M24.3914 21.8417C24.0208 21.8417 23.7186 21.5405 23.7186 21.1689C23.7186 20.7983 24.0208 20.4971 24.3914 20.4971C24.762 20.4971 25.0643 20.7983 25.0643 21.1689C25.0643 21.5405 24.763 21.8417 24.3914 21.8417ZM19.1753 26.5693C18.8047 26.5693 18.5024 26.267 18.5024 25.8964C18.5024 25.5259 18.8036 25.2236 19.1753 25.2236C19.5458 25.2236 19.8481 25.5259 19.8481 25.8964C19.8471 26.267 19.5458 26.5693 19.1753 26.5693ZM29.6086 23.6326C29.9792 23.6326 30.2814 23.9338 30.2814 24.3044C30.2814 24.676 29.9802 24.9772 29.6086 24.9772C29.238 24.9772 28.9357 24.676 28.9357 24.3044C28.9357 23.9338 29.238 23.6326 29.6086 23.6326ZM34.8258 18.7342C35.1964 18.7342 35.4986 19.0354 35.4986 19.4071C35.4986 19.7776 35.1964 20.0789 34.8258 20.0789C34.4552 20.0789 34.1529 19.7776 34.1529 19.4071C34.1529 19.0354 34.4542 18.7342 34.8258 18.7342ZM27.6936 24.3044C27.6936 25.3613 28.5527 26.2194 29.6086 26.2194C30.6645 26.2194 31.5236 25.3613 31.5236 24.3044C31.5236 24.0249 31.4615 23.7589 31.3528 23.5197L33.9314 21.0985C34.1985 21.2403 34.5028 21.3211 34.8258 21.3211C35.8816 21.3211 36.7408 20.4629 36.7408 19.4071C36.7408 18.3512 35.8816 17.492 34.8258 17.492C33.7699 17.492 32.9107 18.3512 32.9107 19.4071C32.9107 19.6865 32.9729 19.9526 33.0815 20.1927L30.503 22.6129C30.2359 22.4711 29.9316 22.3904 29.6086 22.3904C29.1397 22.3904 28.7111 22.5601 28.3778 22.8407L26.264 21.5695C26.292 21.4411 26.3064 21.3066 26.3064 21.1689C26.3064 20.113 25.4473 19.2549 24.3914 19.2549C23.3355 19.2549 22.4764 20.113 22.4764 21.1689C22.4764 21.438 22.5323 21.6927 22.6327 21.9246L20.0986 24.2205C19.8243 24.0684 19.5096 23.9814 19.1742 23.9814C18.1184 23.9814 17.2592 24.8406 17.2592 25.8964C17.2592 26.9523 18.1184 27.8115 19.1742 27.8115C20.2301 27.8115 21.0893 26.9523 21.0893 25.8964C21.0893 25.6283 21.0334 25.3727 20.9329 25.1408L23.466 22.8448C23.7403 22.997 24.055 23.0839 24.3904 23.0839C24.8593 23.0839 25.2879 22.9142 25.6212 22.6336L27.735 23.9048C27.7091 24.0342 27.6936 24.1677 27.6936 24.3044ZM35.834 34.2657H33.8155V24.6998H35.834V34.2657ZM36.4562 23.4576C36.7988 23.4576 37.0772 23.735 37.0772 24.0787V34.8868C37.0772 35.2294 36.7988 35.5079 36.4562 35.5079H33.1954C32.8517 35.5079 32.5743 35.2294 32.5743 34.8868V24.0787C32.5743 23.735 32.8528 23.4576 33.1954 23.4576H36.4562ZM30.6179 34.2657H28.5993V28.8353H30.6179V34.2657ZM31.239 27.5931C31.5816 27.5931 31.8601 27.8715 31.8601 28.2142V34.8868C31.8601 35.2294 31.5816 35.5079 31.239 35.5079H27.9782C27.6356 35.5079 27.3571 35.2294 27.3571 34.8868V28.2142C27.3571 27.8715 27.6356 27.5931 27.9782 27.5931H31.239ZM25.4007 34.2657H23.3821V26.5351H25.4007V34.2657ZM26.0218 25.293C26.3654 25.293 26.6429 25.5714 26.6429 25.914V34.8868C26.6429 35.2294 26.3644 35.5079 26.0218 35.5079H22.761C22.4184 35.5079 22.1399 35.2294 22.1399 34.8868V25.914C22.1399 25.5714 22.4184 25.293 22.761 25.293H26.0218ZM20.1835 34.2657H18.1649V30.1716H20.1835V34.2657ZM20.8046 28.9295H17.5438C17.2012 28.9295 16.9228 29.2079 16.9228 29.5506V34.8868C16.9228 35.2294 17.2012 35.5079 17.5438 35.5079H20.8046C21.1472 35.5079 21.4257 35.2294 21.4257 34.8868V29.5506C21.4257 29.2079 21.1483 28.9295 20.8046 28.9295ZM49.8966 48.6937C49.4432 47.6482 48.5727 46.8325 47.5096 46.4412C48.0799 45.9143 48.437 45.1597 48.437 44.3243C48.437 42.7333 47.1421 41.4383 45.551 41.4383C43.96 41.4383 42.665 42.7333 42.665 44.3243C42.665 45.1597 43.0232 45.9143 43.5925 46.4412C42.5284 46.8325 41.6588 47.6482 41.2055 48.6947C40.3473 47.6751 39.8794 46.3988 39.8794 45.051C39.8794 43.5365 40.4695 42.1122 41.5408 41.0408C43.7519 38.8297 47.3501 38.8297 49.5612 41.0408C51.6543 43.1339 51.7651 46.4681 49.8966 48.6937ZM45.551 45.9681C46.4578 45.9681 47.1949 45.2301 47.1949 44.3243C47.1949 43.4175 46.4578 42.6805 45.551 42.6805C44.6442 42.6805 43.9072 43.4175 43.9072 44.3243C43.9072 45.2301 44.6442 45.9681 45.551 45.9681ZM42.1971 49.6253C44.1857 51.0849 46.9174 51.0849 48.9049 49.6253C48.583 48.3531 47.4267 47.4298 46.0862 47.4298H45.0159C43.6753 47.4298 42.5191 48.3521 42.1971 49.6253ZM15.9714 37.5285C22.053 43.61 31.948 43.61 38.0286 37.5285C44.1101 31.447 44.1101 21.5529 38.0286 15.4714C35.0825 12.5253 31.1665 10.9032 27 10.9032C22.8345 10.9032 18.9175 12.5253 15.9714 15.4714C9.8899 21.5529 9.8899 31.447 15.9714 37.5285ZM12.7946 48.6947C12.3412 47.6482 11.4716 46.8325 10.4075 46.4412C10.9779 45.9143 11.335 45.1597 11.335 44.3243C11.335 42.7333 10.04 41.4383 8.44897 41.4383C6.85793 41.4383 5.56295 42.7333 5.56295 44.3243C5.56295 45.1597 5.92111 45.9143 6.49045 46.4412C5.42734 46.8325 4.55678 47.6482 4.10338 48.6937C2.23492 46.4681 2.34568 43.1339 4.43877 41.0408C6.64986 38.8297 10.2481 38.8297 12.4592 41.0408C13.5305 42.1122 14.1206 43.5365 14.1206 45.051C14.1206 46.3988 13.6517 47.6751 12.7946 48.6947ZM8.44897 45.9681C7.5432 45.9681 6.80514 45.2301 6.80514 44.3243C6.80514 43.4175 7.5432 42.6805 8.44897 42.6805C9.35576 42.6805 10.0928 43.4175 10.0928 44.3243C10.0928 45.2301 9.35473 45.9681 8.44897 45.9681ZM5.09506 49.6253C7.08256 51.0849 9.81537 51.0849 11.8029 49.6253C11.4809 48.3531 10.3247 47.4298 8.98414 47.4298H7.91379C6.57326 47.4298 5.41699 48.3531 5.09506 49.6253ZM5.09506 12.5233C5.41699 11.25 6.57326 10.3277 7.91379 10.3277H8.98414C10.3247 10.3277 11.4809 11.25 11.8029 12.5233C10.836 13.2355 9.67252 13.6205 8.44897 13.6205C7.22541 13.6205 6.06189 13.2355 5.09506 12.5233ZM8.44897 8.86606C7.5432 8.86606 6.80514 8.12799 6.80514 7.22223C6.80514 6.31543 7.5432 5.5784 8.44897 5.5784C9.35576 5.5784 10.0928 6.31543 10.0928 7.22223C10.0928 8.12799 9.35473 8.86606 8.44897 8.86606ZM2.77734 7.94891C2.77734 6.43344 3.36738 5.0101 4.43877 3.93872C5.51016 2.86733 6.9335 2.27729 8.44897 2.27729C9.9634 2.27729 11.3878 2.86733 12.4592 3.93872C13.5305 5.0101 14.1206 6.43344 14.1206 7.94891C14.1206 9.29668 13.6527 10.572 12.7946 11.5916C12.3412 10.5461 11.4716 9.73041 10.4075 9.33913C10.9768 8.8112 11.335 8.0576 11.335 7.22223C11.335 5.63016 10.04 4.33622 8.44897 4.33622C6.85793 4.33622 5.56295 5.63016 5.56295 7.22223C5.56295 8.0576 5.92111 8.8112 6.49045 9.33913C5.42631 9.73041 4.55678 10.5461 4.10338 11.5916C3.24523 10.572 2.77734 9.29668 2.77734 7.94891ZM41.5408 3.93872C42.6122 2.86733 44.0366 2.27729 45.551 2.27729C47.0655 2.27729 48.4898 2.86733 49.5612 3.93872C50.6326 5.0101 51.2227 6.43344 51.2227 7.94891C51.2227 9.29668 50.7548 10.572 49.8966 11.5916C49.4432 10.5461 48.5737 9.73041 47.5096 9.33913C48.0799 8.8112 48.437 8.0576 48.437 7.22223C48.437 5.63016 47.1421 4.33622 45.551 4.33622C43.96 4.33622 42.665 5.63016 42.665 7.22223C42.665 8.0576 43.0221 8.8112 43.5925 9.33913C42.5284 9.73041 41.6588 10.5461 41.2055 11.5916C40.3473 10.572 39.8794 9.29668 39.8794 7.94891C39.8794 6.43344 40.4695 5.0101 41.5408 3.93872ZM45.551 8.86606C46.4578 8.86606 47.1949 8.12799 47.1949 7.22223C47.1949 6.31543 46.4578 5.5784 45.551 5.5784C44.6442 5.5784 43.9072 6.31543 43.9072 7.22223C43.9072 8.12799 44.6442 8.86606 45.551 8.86606ZM42.1971 12.5233C42.5191 11.25 43.6753 10.3277 45.0159 10.3277H46.0862C47.4267 10.3277 48.584 11.25 48.9049 12.5233C47.9381 13.2355 46.7746 13.6205 45.551 13.6205C44.3275 13.6205 43.164 13.2355 42.1971 12.5233ZM41.1226 39.7437L39.337 37.9591C45.3274 31.5153 45.3274 21.4846 39.337 15.0408L41.1216 13.2562C42.3628 14.2955 43.9124 14.8627 45.551 14.8627C47.3978 14.8627 49.1337 14.1433 50.4401 12.8369C51.7454 11.5316 52.4648 9.79563 52.4648 7.94891C52.4648 6.10219 51.7454 4.36624 50.4401 3.05987C47.7445 0.364321 43.3586 0.364321 40.663 3.05987C39.3577 4.36624 38.6383 6.10219 38.6383 7.94891C38.6383 9.58653 39.2045 11.1372 40.2438 12.3783L38.4592 14.163C35.3351 11.2552 31.2907 9.66106 27 9.66106C22.7103 9.66106 18.6649 11.2552 15.5408 14.163L13.7562 12.3783C14.7955 11.1372 15.3617 9.58653 15.3617 7.94891C15.3617 6.10219 14.6423 4.36624 13.337 3.05987C10.6414 0.364321 6.25547 0.364321 3.55992 3.05987C2.25459 4.36624 1.53516 6.10219 1.53516 7.94891C1.53516 9.79563 2.25459 11.5316 3.55992 12.8369C4.86629 14.1433 6.60225 14.8627 8.44897 14.8627C10.0866 14.8627 11.6372 14.2955 12.8784 13.2562L14.663 15.0408C11.7563 18.1649 10.1611 22.2092 10.1611 26.4999C10.1611 30.7907 11.7563 34.835 14.663 37.9591L12.8784 39.7437C10.1653 37.4767 6.10848 37.6155 3.56096 40.163C0.86541 42.8585 0.86541 47.2445 3.56096 49.94C4.90873 51.2878 6.67885 51.9617 8.45 51.9617C10.2201 51.9617 11.9913 51.2878 13.339 49.94C15.8866 47.3925 16.0253 43.3357 13.7572 40.6226L15.5429 38.8369C18.667 41.7447 22.7113 43.3388 27.0021 43.3388C31.2928 43.3388 35.3371 41.7447 38.4613 38.8369L40.2459 40.6226C37.9789 43.3357 38.1165 47.3925 40.6641 49.94C42.0118 51.2878 43.783 51.9617 45.5531 51.9617C47.3232 51.9617 49.0933 51.2878 50.4421 49.94C53.1377 47.2445 53.1377 42.8585 50.4421 40.163C47.8926 37.6155 43.8358 37.4767 41.1226 39.7437Z" fill="#39B4EF" />
                                        </svg>

                                    </div>
                                    <div class="flex flex-col">
                                        <h4 class="text-base lg:text-body font-bold">
                                            <asp:Label ID="LblTotalPublicVendors" runat="server" />
                                        </h4>
                                        <p class="text-sm">
                                            <a id="aPartnerPortal" runat="server">
                                                <asp:Label ID="Label35" runat="server" />
                                            </a>
                                        </p>
                                    </div>
                                </div>
                                <div class="w-[250px] max-w-full py-15px lg:py-30px bg-white rounded-10px shadow-card flex flex-col gap-15px items-center">
                                    <div class="w-[60px] h-[60px] lg:w-[85px] lg:h-[85px] p-10px rounded-full bg-lightBlue2 flex items-center justify-center">
                                        <svg width="54" height="53" viewBox="0 0 54 53" fill="none" xmlns="http://www.w3.org/2000/svg">
                                            <g clip-path="url(#clip0_349_611)">
                                                <path d="M27 14.9063C28.4741 14.9063 29.9151 14.4691 31.1407 13.6502C32.3664 12.8312 33.3217 11.6672 33.8858 10.3053C34.4499 8.94343 34.5975 7.44486 34.3099 5.9991C34.0223 4.55333 33.3125 3.22531 32.2702 2.18297C31.2278 1.14063 29.8998 0.430794 28.454 0.143213C27.0083 -0.144367 25.5097 0.00322933 24.1478 0.567339C22.7859 1.13145 21.6219 2.08673 20.803 3.31239C19.984 4.53805 19.5469 5.97904 19.5469 7.45313C19.5491 9.42915 20.335 11.3236 21.7323 12.7209C23.1295 14.1181 25.024 14.9041 27 14.9063ZM27 1.65625C28.1465 1.65625 29.2673 1.99623 30.2206 2.6332C31.1739 3.27017 31.9169 4.17552 32.3556 5.23476C32.7944 6.294 32.9092 7.45956 32.6855 8.58404C32.4618 9.70853 31.9097 10.7414 31.099 11.5521C30.2883 12.3628 29.2554 12.9149 28.1309 13.1386C27.0064 13.3623 25.8409 13.2475 24.7816 12.8087C23.7224 12.37 22.817 11.627 22.1801 10.6737C21.5431 9.72041 21.2031 8.59964 21.2031 7.45313C21.2049 5.91624 21.8162 4.4428 22.9029 3.35606C23.9897 2.26931 25.4631 1.65801 27 1.65625Z" fill="#39B4EF" />
                                                <path d="M25.4481 9.97068C25.6034 10.1259 25.814 10.2131 26.0336 10.2131C26.2532 10.2131 26.4638 10.1259 26.6191 9.97068L30.4839 6.10665C30.563 6.03026 30.6261 5.93888 30.6695 5.83784C30.7129 5.73681 30.7358 5.62814 30.7367 5.51818C30.7377 5.40822 30.7167 5.29918 30.6751 5.1974C30.6334 5.09563 30.572 5.00317 30.4942 4.92541C30.4164 4.84766 30.324 4.78617 30.2222 4.74453C30.1204 4.70289 30.0114 4.68194 29.9014 4.68289C29.7915 4.68385 29.6828 4.70669 29.5818 4.75009C29.4807 4.7935 29.3894 4.85658 29.313 4.93568L26.0336 8.21423L24.6871 6.86769C24.5309 6.71684 24.3217 6.63337 24.1046 6.63526C23.8874 6.63715 23.6797 6.72424 23.5262 6.87778C23.3726 7.03132 23.2855 7.23903 23.2837 7.45616C23.2818 7.67329 23.3652 7.88248 23.5161 8.03866L25.4481 9.97068Z" fill="#39B4EF" />
                                                <path d="M50.3233 17.5223C50.3045 17.415 50.2648 17.3126 50.2063 17.2208C50.1479 17.1289 50.0719 17.0495 49.9828 16.9871C49.8937 16.9246 49.7931 16.8803 49.6868 16.8568C49.5806 16.8332 49.4707 16.8309 49.3635 16.8498L45.2875 17.5678C45.1802 17.5866 45.0777 17.6264 44.9858 17.6849C44.8939 17.7433 44.8144 17.8194 44.752 17.9086C44.6895 17.9978 44.6453 18.0985 44.6218 18.2049C44.5983 18.3112 44.596 18.4212 44.615 18.5284L44.7923 19.5313L42.4735 19.9362L33.1281 18.5069C31.7596 18.3008 30.3606 18.4782 29.0869 19.0195L27 19.9089L24.9189 19.022C23.6432 18.4783 22.241 18.3014 20.8702 18.511L11.5207 19.9395L9.20855 19.5321L9.38494 18.5284C9.40401 18.4212 9.40173 18.3112 9.37823 18.2049C9.35474 18.0985 9.31049 17.9978 9.24803 17.9086C9.18556 17.8194 9.10611 17.7433 9.01422 17.6849C8.92232 17.6264 8.8198 17.5866 8.71251 17.5678L4.64062 16.8498C4.53337 16.8308 4.42343 16.833 4.31707 16.8565C4.21071 16.88 4.11003 16.9243 4.02079 16.9867C3.93156 17.0492 3.85553 17.1287 3.79705 17.2205C3.73857 17.3124 3.69879 17.415 3.67999 17.5223L0.512413 35.4619C0.493352 35.5692 0.495633 35.6791 0.519128 35.7855C0.542623 35.8918 0.586869 35.9925 0.649332 36.0818C0.711796 36.171 0.791249 36.247 0.883142 36.3055C0.975035 36.364 1.07756 36.4038 1.18485 36.4226L5.26171 37.1405C5.36889 37.1595 5.47876 37.1571 5.58502 37.1336C5.69128 37.11 5.79186 37.0657 5.881 37.0033C5.97015 36.9408 6.0461 36.8614 6.10453 36.7696C6.16296 36.6778 6.20271 36.5753 6.22151 36.4681L6.29687 36.0234L6.9809 36.1443C7.61137 37.533 8.5286 38.7725 9.67231 39.7814C9.62847 40.5292 9.84135 41.2695 10.2757 41.8797C10.71 42.4899 11.3398 42.9335 12.0606 43.137C11.6732 43.7804 11.5173 44.537 11.6189 45.2812C11.7205 46.0254 12.0734 46.7125 12.619 47.2286C13.1647 47.7447 13.8704 48.0588 14.6191 48.1187C15.3678 48.1787 16.1145 47.981 16.7354 47.5584C16.8177 48.1082 17.0387 48.6281 17.3774 49.0691C17.7162 49.51 18.1616 49.8574 18.6717 50.0787C19.1818 50.3 19.7398 50.3877 20.2932 50.3338C20.8466 50.2798 21.3772 50.0859 21.835 49.7703C21.9125 50.2775 22.108 50.7594 22.4058 51.1772C22.7036 51.5949 23.0954 51.9369 23.5496 52.1756C24.0038 52.4142 24.5077 52.5429 25.0207 52.5512C25.5337 52.5594 26.0415 52.4471 26.5031 52.2232V52.2281L26.8344 52.4484C27.1971 52.6886 27.6037 52.855 28.0308 52.938C28.4578 53.021 28.8971 53.0191 29.3234 52.9324C29.7498 52.8456 30.1548 52.6758 30.5155 52.4324C30.8762 52.1891 31.1854 51.8771 31.4255 51.5143C31.788 50.9623 31.9735 50.3129 31.9571 49.6527C32.2258 49.7258 32.5027 49.7642 32.7811 49.767C33.2187 49.7683 33.6522 49.6828 34.0565 49.5153C34.4608 49.3479 34.8279 49.1018 35.1364 48.7915C35.4449 48.4811 35.6887 48.1126 35.8537 47.7073C36.0188 47.302 36.1017 46.868 36.0978 46.4304C36.3614 46.4998 36.6326 46.5359 36.9052 46.5381C37.3422 46.5381 37.7749 46.4516 38.1784 46.2837C38.5819 46.1157 38.9481 45.8696 39.256 45.5594C39.5639 45.2493 39.8074 44.8813 39.9725 44.4766C40.1375 44.0719 40.2209 43.6386 40.2177 43.2016C40.7225 43.3331 41.2514 43.3425 41.7606 43.229C42.2698 43.1155 42.7447 42.8823 43.1458 42.5487C43.5469 42.2152 43.8628 41.7909 44.0674 41.3109C44.2719 40.831 44.3591 40.3092 44.3219 39.7889C45.4667 38.7783 46.384 37.5364 47.0133 36.1451L47.7031 36.0275L47.781 36.4681C47.7998 36.5754 47.8395 36.6779 47.898 36.7698C47.9565 36.8617 48.0325 36.9412 48.1218 37.0036C48.211 37.0661 48.3117 37.1103 48.418 37.1338C48.5244 37.1573 48.6343 37.1596 48.7416 37.1405L52.8176 36.4226C52.9249 36.4038 53.0274 36.364 53.1193 36.3055C53.2112 36.247 53.2907 36.171 53.3531 36.0818C53.4156 35.9925 53.4599 35.8918 53.4833 35.7855C53.5068 35.6791 53.5091 35.5692 53.4901 35.4619L50.3233 17.5223ZM4.73419 35.3667L2.28791 34.9344L5.16316 18.6203L7.61027 19.0518L4.73419 35.3667ZM11.7045 41.017C11.4223 40.6806 11.2852 40.2459 11.3234 39.8084C11.3616 39.3709 11.572 38.9666 11.9082 38.6842L15.0808 36.0234C15.4173 35.7409 15.8522 35.6037 16.2898 35.6419C16.7275 35.6801 17.132 35.8906 17.4144 36.2271C17.6969 36.5636 17.8341 36.9985 17.7959 37.4361C17.7577 37.8738 17.5472 38.2783 17.2107 38.5608L14.0382 41.2232C13.8714 41.3629 13.6788 41.4684 13.4712 41.5337C13.2637 41.599 13.0453 41.6227 12.8286 41.6035C12.6119 41.5844 12.4011 41.5227 12.2082 41.4221C12.0153 41.3215 11.8442 41.1838 11.7045 41.017ZM15.9627 46.0926C15.7961 46.2324 15.6035 46.3381 15.396 46.4035C15.1885 46.4689 14.9702 46.4929 14.7534 46.4739C14.5367 46.4549 14.3258 46.3935 14.1328 46.293C13.9398 46.1926 13.7685 46.0551 13.6287 45.8884C13.4888 45.7218 13.3832 45.5292 13.3177 45.3217C13.2523 45.1142 13.2284 44.8959 13.2473 44.6791C13.2663 44.4624 13.3278 44.2515 13.4282 44.0585C13.5287 43.8655 13.6661 43.6942 13.8328 43.5544L17.6422 40.3603C17.9805 40.0955 18.4084 39.9725 18.8356 40.0173C19.2628 40.0621 19.656 40.2711 19.932 40.6002C20.2081 40.9293 20.3454 41.3528 20.3152 41.7813C20.2849 42.2098 20.0893 42.6098 19.7696 42.8968L15.9627 46.0926ZM21.0599 48.302C20.8932 48.4418 20.7006 48.5474 20.4932 48.6128C20.2857 48.6782 20.0674 48.702 19.8507 48.6831C19.634 48.6641 19.4231 48.6026 19.2302 48.5021C19.0373 48.4016 18.866 48.2641 18.7262 48.0975C18.5864 47.9308 18.4808 47.7382 18.4154 47.5308C18.35 47.3233 18.3262 47.105 18.3452 46.8883C18.3641 46.6716 18.4256 46.4607 18.5261 46.2678C18.6266 46.0749 18.7641 45.9036 18.9307 45.7638L21.4689 43.6347C21.8061 43.358 22.2388 43.2254 22.6731 43.2657C23.1074 43.3061 23.5082 43.5161 23.7886 43.8503C24.0689 44.1844 24.2062 44.6156 24.1705 45.0503C24.1348 45.485 23.9291 45.8881 23.5981 46.1721L21.0599 48.302ZM26.1561 50.5156C25.8191 50.794 25.3857 50.9278 24.9503 50.888C24.515 50.8481 24.1131 50.6378 23.8322 50.3028C23.5514 49.9678 23.4143 49.5354 23.451 49.0998C23.4876 48.6642 23.695 48.2607 24.0279 47.9774L26.5652 45.8483C26.7318 45.7084 26.9243 45.6027 27.1318 45.5372C27.3392 45.4718 27.5575 45.4478 27.7743 45.4667C27.991 45.4857 28.2018 45.5471 28.3948 45.6475C28.5877 45.7479 28.759 45.8854 28.8989 46.052C29.0387 46.2186 29.1443 46.4112 29.2097 46.6187C29.2751 46.8262 29.299 47.0445 29.28 47.2612C29.261 47.4779 29.1995 47.6887 29.099 47.8817C28.9985 48.0746 28.861 48.2459 28.6943 48.3856L26.1561 50.5156ZM30.0425 50.6034C29.8231 50.938 29.4888 51.1807 29.1027 51.286C28.7167 51.3912 28.3055 51.3517 27.9465 51.1748L29.7593 49.6543C29.9308 49.511 30.0873 49.3506 30.2264 49.1757C30.3048 49.4114 30.3289 49.6617 30.2971 49.9081C30.2653 50.1544 30.1782 50.3904 30.0425 50.5984V50.6034ZM42.4139 40.9093C42.2938 41.0908 42.1392 41.2467 41.9589 41.3684C41.7786 41.4901 41.576 41.575 41.3629 41.6184C41.1497 41.6618 40.9301 41.6627 40.7165 41.6212C40.503 41.5797 40.2997 41.4965 40.1183 41.3764L33.2109 36.8076C33.1204 36.742 33.0175 36.6954 32.9084 36.6706C32.7994 36.6459 32.6865 36.6434 32.5764 36.6635C32.4664 36.6835 32.3616 36.7257 32.2683 36.7873C32.175 36.849 32.0952 36.9289 32.0337 37.0223C31.9721 37.1156 31.9301 37.2205 31.9101 37.3305C31.8902 37.4405 31.8927 37.5535 31.9176 37.6625C31.9425 37.7715 31.9892 37.8744 32.0549 37.9648C32.1207 38.0553 32.204 38.1316 32.3 38.1889L37.8269 41.8468C38.193 42.0896 38.4477 42.4679 38.5349 42.8985C38.6221 43.329 38.5347 43.7766 38.2919 44.1427C38.0491 44.5089 37.6708 44.7636 37.2402 44.8508C36.8096 44.938 36.362 44.8505 35.9959 44.6077L30.4698 40.9507C30.3777 40.8891 30.2741 40.8468 30.1651 40.8265C29.977 40.7911 29.7825 40.8219 29.6146 40.9136C29.4466 41.0054 29.3157 41.1525 29.244 41.3299C29.1722 41.5073 29.1642 41.7041 29.2211 41.8868C29.2781 42.0695 29.3966 42.2268 29.5564 42.3321L33.697 45.0756C34.0609 45.3193 34.3135 45.6971 34.3995 46.1265C34.4856 46.5559 34.398 47.0019 34.1561 47.367C33.9141 47.732 33.5374 47.9863 33.1084 48.0743C32.6794 48.1623 32.233 48.0769 31.8669 47.8366L30.9228 47.2122C30.9456 46.6046 30.7993 46.0026 30.5001 45.4732C30.2009 44.9439 29.7606 44.508 29.2283 44.2142C28.6959 43.9204 28.0925 43.7802 27.4851 43.8091C26.8778 43.8381 26.2904 44.0352 25.7884 44.3783C25.7242 43.9478 25.5745 43.5344 25.3481 43.1626C25.1217 42.7908 24.8231 42.4681 24.4701 42.2134C24.117 41.9588 23.7166 41.7773 23.2923 41.6798C22.8681 41.5823 22.4286 41.5706 21.9998 41.6455C22.0143 40.8617 21.7446 40.0991 21.2406 39.4986C20.7366 38.8981 20.0323 38.5002 19.2579 38.3786C19.5002 37.6948 19.5107 36.9503 19.2877 36.26C19.0647 35.5697 18.6206 34.972 18.024 34.5593C17.4275 34.1466 16.7116 33.9417 15.987 33.9765C15.2624 34.0112 14.5694 34.2835 14.015 34.7514L10.8424 37.4138C10.6206 37.6048 10.4257 37.8249 10.2628 38.068C9.43397 37.2136 8.78069 36.205 8.33985 35.0992C8.28764 34.9677 8.20261 34.8518 8.09286 34.7625C7.98312 34.6732 7.85233 34.6135 7.71296 34.5891L6.58588 34.3903L8.91871 21.161L11.365 21.5925C11.4541 21.6078 11.5451 21.6078 11.6341 21.5925L21.1203 20.1482C22.1858 19.9848 23.2758 20.1225 24.2672 20.5457L24.8833 20.8083L21.5766 22.2161C20.8337 22.5338 20.2002 23.0624 19.7547 23.7365L16.6385 28.4345C16.278 28.9862 16.15 29.6581 16.2824 30.3038C16.4149 30.9495 16.7971 31.5167 17.3457 31.8819C18.262 32.4857 19.3802 32.7015 20.4553 32.4822C21.5305 32.2628 22.4748 31.6262 23.0813 30.7118L24.5032 28.5703L27.5267 27.9517C27.8526 28.9564 28.4061 29.8722 29.1442 30.6278C29.8822 31.3834 30.7848 31.9582 31.7816 32.3076C32.1349 32.4336 32.4973 32.5324 32.8656 32.6032L41.946 38.6154C42.3121 38.8577 42.567 39.2354 42.6547 39.6655C42.7424 40.0957 42.6558 40.543 42.4139 40.9093ZM46.2854 34.5907C46.1454 34.6151 46.0141 34.6751 45.904 34.7648C45.794 34.8546 45.7088 34.9712 45.6568 35.1033C45.2158 36.2142 44.5601 37.2273 43.7273 38.0846C43.4941 37.7491 43.2 37.4603 42.8602 37.2333L35.8212 32.5767C36.7359 32.3939 37.6072 32.0381 38.3884 31.5283L39.6894 30.6754C39.873 30.5546 40.0011 30.3658 40.0455 30.1505C40.0899 29.9353 40.047 29.7112 39.9262 29.5276C39.8054 29.344 39.6166 29.2159 39.4014 29.1714C39.1861 29.127 38.962 29.1699 38.7784 29.2907L37.4816 30.1437C36.7315 30.6363 35.877 30.9475 34.9859 31.0526C34.0947 31.1577 33.1913 31.0538 32.3472 30.7491C31.5704 30.4784 30.8687 30.028 30.2992 29.4345C29.7297 28.8409 29.3086 28.1212 29.0703 27.3339C29.019 27.1683 28.9759 26.9919 28.9395 26.8171C28.8946 26.6028 28.7667 26.415 28.5838 26.2947C28.4008 26.1744 28.1776 26.1315 27.9631 26.1753L23.8382 27.02C23.6236 27.0639 23.4351 27.1911 23.314 27.3736L21.7083 29.7992C21.3442 30.3474 20.7778 30.7292 20.133 30.8609C19.4881 30.9927 18.8174 30.8637 18.2674 30.5023C18.0839 30.38 17.9559 30.1904 17.9113 29.9745C17.8667 29.7585 17.9089 29.5337 18.0289 29.3487L21.1435 24.6516C21.4106 24.2482 21.7896 23.9316 22.2341 23.7406L29.7378 20.5441C30.7297 20.123 31.819 19.9851 32.8846 20.1458L42.3667 21.595C42.4553 21.6086 42.5455 21.6086 42.6342 21.595L45.0854 21.166L47.4133 34.3953L46.2854 34.5907ZM49.2666 35.365L46.3905 19.051L48.8377 18.6195L51.7129 34.9336L49.2666 35.365Z" fill="#39B4EF" />
                                            </g>
                                            <defs>
                                                <clipPath id="clip0_349_611">
                                                    <rect width="53" height="53" fill="white" transform="translate(0.5)" />
                                                </clipPath>
                                            </defs>
                                        </svg>

                                    </div>
                                    <div class="flex flex-col">
                                        <h4 class="text-base lg:text-body font-bold">
                                            <asp:Label ID="Label38" runat="server" /></h4>
                                        <p class="text-sm">
                                            <asp:Label ID="Label36" runat="server" />
                                        </p>
                                    </div>
                                </div>
                                <div class="w-[250px] max-w-full py-15px lg:py-30px bg-white rounded-10px shadow-card flex flex-col gap-15px items-center">
                                    <div class="w-[60px] h-[60px] lg:w-[85px] lg:h-[85px] p-10px rounded-full bg-lightBlue2 flex items-center justify-center">
                                        <svg width="54" height="53" viewBox="0 0 54 53" fill="none" xmlns="http://www.w3.org/2000/svg">
                                            <g clip-path="url(#clip0_349_4234)">
                                                <path d="M18.9004 12.4448C18.7482 12.9134 18.8981 13.4156 19.2821 13.7244L22.7017 16.473L21.5508 20.7066C21.4216 21.182 21.5956 21.6764 21.9944 21.9664C22.3934 22.2559 22.9171 22.2686 23.3294 21.9988L27.0001 19.5959L30.671 21.9989C30.8689 22.1284 31.0924 22.1928 31.3155 22.1928C31.5573 22.1928 31.7986 22.1171 32.0057 21.9664C32.4048 21.6767 32.5789 21.182 32.4494 20.7066L31.2984 16.4731L34.7178 13.7246C35.1022 13.4159 35.2521 12.9136 35.0997 12.4448C34.9474 11.9758 34.5309 11.6575 34.0387 11.6337L29.6565 11.4201L28.0993 7.31855C27.9244 6.85781 27.4929 6.55999 27 6.55999C26.5072 6.55999 26.0756 6.8577 25.9009 7.31845L24.3435 11.4202L19.9617 11.6337C19.4693 11.6575 19.0527 11.9758 18.9004 12.4448ZM24.7383 13.059C25.2054 13.0362 25.6144 12.739 25.7801 12.302L26.9999 9.0894L28.2197 12.3023C28.3858 12.7393 28.7949 13.0362 29.2615 13.059L32.6938 13.2264L30.0152 15.3792C29.6507 15.6723 29.4946 16.1531 29.6173 16.604L30.5188 19.92L27.6436 18.0378C27.448 17.9099 27.2239 17.8459 26.9998 17.8459C26.7757 17.8459 26.5514 17.91 26.3557 18.0379L23.4807 19.92L24.3822 16.6042C24.505 16.153 24.3488 15.6721 23.9842 15.3792L21.3057 13.2265L24.7383 13.059ZM26.1718 3.86714V0.828125C26.1718 0.370793 26.5426 0 26.9999 0C27.4573 0 27.828 0.370793 27.828 0.828125V3.86714C27.828 4.32447 27.4573 4.69526 26.9999 4.69526C26.5426 4.69526 26.1718 4.32447 26.1718 3.86714ZM31.215 8.77357C30.909 8.43373 30.9364 7.91004 31.2763 7.60405L34.6628 4.55469C35.0027 4.24859 35.5263 4.27602 35.8324 4.61597C36.1384 4.95581 36.111 5.4795 35.7711 5.78549L32.3845 8.83485C32.2262 8.97739 32.0281 9.04758 31.8306 9.04758C31.6042 9.04758 31.3785 8.95524 31.215 8.77357ZM18.1674 4.61597C18.4734 4.27602 18.997 4.24859 19.3371 4.55469L22.7237 7.60405C23.0635 7.91004 23.091 8.43373 22.785 8.77357C22.6214 8.95514 22.3958 9.04758 22.1693 9.04758C21.9719 9.04758 21.7737 8.97739 21.6153 8.83485L18.2287 5.78549C17.8888 5.4795 17.8615 4.95581 18.1674 4.61597ZM37.6241 13.7244L41.0438 16.473L39.8928 20.7066C39.7636 21.182 39.9376 21.6764 40.3365 21.9664C40.7351 22.2559 41.2589 22.2685 41.6714 21.9988L45.3422 19.5959L49.013 21.9989C49.211 22.1284 49.4344 22.1928 49.6575 22.1928C49.8993 22.1928 50.1406 22.1171 50.3478 21.9664C50.7466 21.6769 50.9207 21.1824 50.7914 20.7066L49.6404 16.4731L53.0599 13.7246C53.4441 13.416 53.594 12.9137 53.4416 12.4448C53.2894 11.9758 52.8728 11.6575 52.3807 11.6337L47.9986 11.4201L46.4414 7.31855C46.2666 6.85781 45.835 6.55999 45.3422 6.55999C44.8492 6.55999 44.4177 6.8577 44.2428 7.31845L42.6856 11.4201L38.3038 11.6336C37.8113 11.6574 37.3947 11.9758 37.2425 12.4448C37.0901 12.9136 37.24 13.4159 37.6241 13.7244ZM43.0802 13.059C43.5474 13.0362 43.9565 12.739 44.1223 12.3019L45.3421 9.08929L46.5617 12.3018C46.7276 12.739 47.1367 13.0362 47.6037 13.0589L51.0359 13.2263L48.3577 15.3789C47.993 15.6719 47.8367 16.1528 47.9594 16.6039L48.861 19.9199L45.9857 18.0377C45.7901 17.9098 45.566 17.8458 45.3419 17.8458C45.1176 17.8458 44.8934 17.9099 44.6979 18.0378L41.8228 19.9199L42.7244 16.6041C42.847 16.1531 42.6909 15.6723 42.3263 15.3791L39.6479 13.2263L43.0802 13.059ZM0.939861 13.7244L4.3595 16.473L3.20851 20.7066C3.07932 21.182 3.25333 21.6764 3.65218 21.9664C4.05113 22.2559 4.57481 22.2686 4.98711 21.9988L8.65788 19.5959L12.3288 21.9989C12.5267 22.1284 12.7502 22.1928 12.9732 22.1928C13.2151 22.1928 13.4563 22.1171 13.6635 21.9664C14.0623 21.6769 14.2364 21.1824 14.1072 20.7066L12.9562 16.4731L16.3756 13.7246C16.7596 13.4162 16.9096 12.914 16.7574 12.4442C16.6048 11.9756 16.1882 11.6574 15.6964 11.6336L11.3143 11.4199L9.75712 7.31855C9.58228 6.85781 9.15072 6.55999 8.65788 6.55999C8.16494 6.55999 7.73339 6.8577 7.55855 7.31845L6.00136 11.4201L1.61954 11.6336C1.12712 11.6574 0.710574 11.9758 0.558406 12.4442C0.40572 12.9133 0.555507 13.4158 0.939861 13.7244ZM6.39596 13.059C6.86313 13.0362 7.27222 12.739 7.43806 12.3019L8.65778 9.08929L9.8774 12.3018C10.0433 12.739 10.4524 13.0362 10.9194 13.0589L14.3517 13.2263L11.6734 15.3789C11.3087 15.6719 11.1524 16.1528 11.2752 16.6039L12.1767 19.9199L9.30144 18.0377C9.10579 17.9098 8.88168 17.8458 8.65757 17.8458C8.43336 17.8458 8.20914 17.9099 8.0136 18.0378L5.13856 19.9199L6.04008 16.6041C6.16274 16.1531 6.00664 15.6723 5.64206 15.3791L2.96359 13.2263L6.39596 13.059ZM53.3696 43.9872C52.7869 42.1055 51.5935 40.4159 50.009 39.2294C49.2504 38.6614 48.4243 38.219 47.5529 37.9058C48.7611 36.9576 49.5395 35.4856 49.5395 33.8342C49.5395 30.9807 47.218 28.6592 44.3646 28.6592C41.511 28.6592 39.1895 30.9807 39.1895 33.8342C39.1895 35.4836 39.9661 36.9541 41.1718 37.9025C39.5558 38.4792 38.1242 39.4927 37.0279 40.8582C36.5155 40.1414 35.9244 39.4756 35.2599 38.8747C33.9211 37.6638 32.3498 36.7747 30.6576 36.2491C32.4521 35.0595 33.6384 33.0226 33.6384 30.7131C33.6384 27.0528 30.6603 24.0748 27 24.0748C23.3397 24.0748 20.3617 27.0528 20.3617 30.7131C20.3617 33.0226 21.5479 35.0595 23.3423 36.2491C21.65 36.7745 20.0788 37.6638 18.7399 38.8747C18.0754 39.4756 17.4844 40.1414 16.972 40.8581C15.8758 39.4925 14.4442 38.479 12.8281 37.9021C14.0338 36.9538 14.8104 35.4833 14.8104 33.8339C14.8104 30.9804 12.4889 28.6589 9.63548 28.6589C6.78197 28.6589 4.46043 30.9804 4.46043 33.8339C4.46043 35.4852 5.23886 36.9573 6.44689 37.9055C5.5755 38.2187 4.74944 38.6611 3.99078 39.2291C2.40626 40.4155 1.21283 42.1052 0.630142 43.9868C0.360898 44.8559 0.510064 45.738 1.05011 46.4706C1.59004 47.2031 2.38856 47.6064 3.29846 47.6064H14.7466V49.101C14.7466 51.251 16.4957 53.0001 18.6457 53.0001H35.3541C37.5041 53.0001 39.2533 51.251 39.2534 49.101V47.6068H50.7014C51.6114 47.6068 52.4099 47.2034 52.9497 46.4711C53.4897 45.7384 53.6388 44.8562 53.3696 43.9872ZM44.3646 30.3155C46.3048 30.3155 47.8833 31.894 47.8833 33.8342C47.8833 35.7745 46.3048 37.353 44.3646 37.353C42.4243 37.353 40.8458 35.7745 40.8458 33.8342C40.8458 31.894 42.4243 30.3155 44.3646 30.3155ZM22.0179 30.7131C22.0179 27.966 24.2529 25.7311 27 25.7311C29.7471 25.7311 31.9821 27.966 31.9821 30.7131C31.9821 33.4594 29.7486 35.6936 27.0027 35.6951C27.0018 35.6951 27.0007 35.6951 26.9999 35.6951C26.9991 35.6951 26.9982 35.6951 26.9972 35.6951C24.2514 35.6935 22.0179 33.4593 22.0179 30.7131ZM9.63548 30.3151C11.5757 30.3151 13.1542 31.8936 13.1542 33.8338C13.1542 35.7741 11.5757 37.3526 9.63548 37.3526C7.69519 37.3526 6.11668 35.7741 6.11668 33.8338C6.11668 31.8936 7.69519 30.3151 9.63548 30.3151ZM14.8541 45.9501H3.29857C2.91742 45.9501 2.60957 45.7946 2.38339 45.4878C2.1571 45.1809 2.09965 44.8408 2.21238 44.4767C3.22518 41.2062 6.20839 39.0088 9.63548 39.0088C12.2278 39.0088 14.5893 40.2614 16.0418 42.3817C15.4676 43.4972 15.0654 44.6995 14.8541 45.9501ZM37.5971 49.1009C37.597 50.3376 36.5908 51.3438 35.3541 51.3438H18.6457C17.409 51.3438 16.4028 50.3376 16.4028 49.1009V46.8232C17.0024 41.4209 21.5492 37.3527 26.9972 37.3513C26.9982 37.3513 26.9991 37.3514 27 37.3514C27.001 37.3514 27.0019 37.3513 27.0027 37.3513C32.4506 37.3528 36.9974 41.4209 37.5971 46.8232V49.1009ZM51.6166 45.4883C51.3904 45.795 51.0826 45.9505 50.7014 45.9505H39.1459C38.9345 44.6998 38.5324 43.4975 37.9583 42.3819C39.4108 40.2617 41.7722 39.0092 44.3645 39.0092C47.7915 39.0092 50.7748 41.2067 51.7876 44.4772C51.9003 44.8411 51.8427 45.1813 51.6166 45.4883Z" fill="#39B4EF" />
                                            </g>
                                            <defs>
                                                <clipPath id="clip0_349_4234">
                                                    <rect width="53" height="53" fill="white" transform="translate(0.5)" />
                                                </clipPath>
                                            </defs>
                                        </svg>

                                    </div>
                                    <div class="flex flex-col">
                                        <h4 class="text-base lg:text-body font-bold">
                                            <asp:Label ID="Label39" runat="server" /></h4>
                                        <p class="text-sm">
                                            <asp:Label ID="Label37" runat="server" />
                                        </p>
                                    </div>
                                </div>
                            </div>
                            <div class="flex justify-center items-center gap-10px xl:gap-20px">
                                <a id="aPartnerPortalB" runat="server" class="btn font-bold text-sm bg-white"><span>
                                    <asp:Label ID="Label40" runat="server" /></span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                    </defs></svg>
                                </a>
                            </div>
                        </div>
                    </div>
                </section>
                <section class="bg-white">
                    <div class="container">
                        <div class="gap-30px lg:gap-50px flex flex-col">
                            <div class="flex flex-col gap-10px text-center items-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter">
                                    <asp:Label ID="Label41" runat="server" /></h3>
                                <p class="w-[780px] max-w-full text-base lg:text-body">
                                    <asp:Label ID="Label42" runat="server" />
                                </p>
                            </div>
                            <div class="gap-7px lg:gap-15px countries flex justify-center flex-wrap">
                                <a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/north-america/united-states/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/US.svg" alt="country flag for United States" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label43" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/asia-pacific/australia/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Australia.svg" alt="country flag for Australia" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label44" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/asia-pacific/india/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/India.svg" alt="country flag for India" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label45" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/africa/nigeria/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Nigeria.svg" alt="country flag for Nigeria" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label46" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/africa/south-africa/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/SouthAfrica.svg" alt="country flag for South Africa" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label47" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/asia-pacific/new-zealand/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/NewZealand.svg" alt="country flag for New Zealand" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label48" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/asia-pacific/philippines/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Philippines.svg" alt="country flag for Philippines" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label49" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/asia-pacific/singapore/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Singapore.svg" alt="country flag for Singapore" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label50" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/europe/france/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/France.svg" alt="country flag for France" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label51" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/europe/germany/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Germany.svg" alt="country flag for Germany" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label52" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/europe/italy/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Italy.svg" alt="country flag for Italy" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label53" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/europe/netherlands/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Netherlands.svg" alt="country flag for Netherlands" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label54" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/europe/poland/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Poland.svg" alt="country flag for Poland" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label55" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/europe/spain/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Spain.svg" alt="country flag for Spain" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label56" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/europe/united-kingdom/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/UK.svg" alt="country flag for United Kingdom" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label57" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/middle-east/united-arab-emirates/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/United_Arab_Emirates.svg" alt="country flag for United Arab Emirates" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label58" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/north-america/canada/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Canada.svg" alt="country flag for Canada" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label59" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/middle-east/egypt/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Egypt.svg" alt="country flag for Egypt" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label60" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/north-america/mexico/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Mexico.svg" alt="country flag for Mexico" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label61" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/south-america/argentina/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Argentina.svg" alt="country flag for Argentina" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label62" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/south-america/brazil/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Brazil.svg" alt="country flag for Brazil" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label63" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/south-america/chile/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Chile.svg" alt="country flag for Chile" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label64" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/asia-pacific/china/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/China.svg" alt="country flag for China" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label65" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/asia-pacific/japan/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Japan.svg" alt="country flag for Japan" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label66" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/europe/greece/channel-partners">
                                    <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Greece.svg" alt="country flag for Greece" loading="lazy" width="30" height="31">
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label67" runat="server" /></h5>
                                </a>
                            </div>
                            <div class="flex justify-center items-center gap-10px xl:gap-20px">
                                <a id="aSearchChannelPartners" runat="server" class="btn font-bold text-sm bg-white"><span>
                                    <asp:Label ID="Label68" runat="server" /></span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                    </defs></svg>
                                </a>
                            </div>
                        </div>
                    </div>
                </section>

                <section class="bg-white" style="padding-top:10px;">
                    <div class="container">
                        <div class="gap-30px lg:gap-50px flex flex-col">
                            <div class="flex flex-col gap-10px text-center items-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter">
                                    <asp:Label ID="Label7" Text="Popular MSP Locations" runat="server" /></h3>
                                <p class="w-[780px] max-w-full text-base lg:text-body">
                                    <asp:Label ID="Label9" Text="Browse the most popular Managed Services Providers locations to find an MSP near you." runat="server" />
                                </p>
                            </div>
                            <div class="gap-7px lg:gap-15px countries flex justify-center flex-wrap">
                                <a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href="/asia-pacific/australia/new-south-wales/sydney/channel-partners/managed-service-providers">
                                    
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label10" Text="MSPs in Sydney" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href="/asia-pacific/australia/queensland/brisbane/channel-partners/managed-service-providers">
                                    
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label11" Text="MSPs in Brisbane" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href="/asia-pacific/australia/victoria/melbourne/channel-partners/managed-service-providers">
                                    
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label30" Text="MSPs in Melbourne" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href="/europe/united-kingdom/middlesex/london/channel-partners/managed-service-providers">
                                    
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label71" Text="MSPs in London" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href="/europe/united-kingdom/west-midlands/birmingham/channel-partners/managed-service-providers">
                                    
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label72" Text="MSPs in Birmingham" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href="/europe/united-kingdom/north-west-england/manchester/channel-partners/managed-service-providers">
                                    
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label73" Text="MSPs in Manchester" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href="/north-america/united-states/new-york-state/new-york/channel-partners/managed-service-providers">
                                    
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label74" Text="MSPs in New York" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href="/north-america/united-states/california/los-angeles/channel-partners/managed-service-providers">
                                    
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label75" Text="MSPs in Los Angeles" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href="/north-america/united-states/illinois/chicago/channel-partners/managed-service-providers">
                                    
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label76" Text="MSPs in Chicago" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href="/north-america/united-states/texas/houston/channel-partners/managed-service-providers">
                                    
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label77" Text="MSPs in Houston" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href="/north-america/united-states/arizona/phoenix/channel-partners/managed-service-providers">
                                    
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label78" Text="MSPs in Phoenix" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href="/north-america/united-states/pennsylvania/philadelphia/channel-partners/managed-service-providers">
                                    
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label79" Text="MSPs in Philadelphia" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href="/north-america/united-states/texas/san-antonio/channel-partners/managed-service-providers">
                                    
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label80" Text="MSPs in San Antonio" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href="/north-america/united-states/california/san-diego/channel-partners/managed-service-providers">
                                    
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label81" Text="MSPs in San Diego" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href="/north-america/united-states/texas/dallas/channel-partners/managed-service-providers">
                                    
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label82" Text="MSPs in Dallas" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href="/north-america/united-states/florida/jacksonville/channel-partners/managed-service-providers">
                                    
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label83" Text="MSPs in Jacksonville" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href="/north-america/united-states/texas/austin/channel-partners/managed-service-providers">
                                    
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label84" Text="MSPs in Austin" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href="/north-america/united-states/texas/fort-worth/channel-partners/managed-service-providers">
                                    
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label85" Text="MSPs in Fort Worth" runat="server" /></h5>
                                </a><a class="country bg-white overflow-hidden rounded-30px p-10px flex gap-5px items-center border-gray border-2 w-fit" href="/north-america/united-states/california/san-jose/channel-partners/managed-service-providers">
                                    
                                    <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">
                                        <asp:Label ID="Label86" Text="MSPs in San Jose" runat="server" /></h5>
                                </a>
                            </div>
                        </div>
                    </div>
                </section>

                <section>
                    <div class="container">
                        <div class="gap-25px lg:gap-50px flex flex-col">
                            <div class="flex flex-col gap-10px text-center items-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter">
                                    <asp:Label ID="Label69" runat="server" /></h3>
                                <p class="w-[690px] max-w-full text-base lg:text-body">
                                    <asp:Label ID="Label70" runat="server" />
                                </p>
                            </div>
                            <div class="owl-carousel owl-theme" data-items="2">
                                <div class="p-20px">
                                    <div class="bg-white rounded-10px shadow-card flex flex-col gap-25px py-30px px-25px">
                                        <div class="justify-center lg:justify-start text-center lg:text-left flex gap-15px flex-wrap">
                                            <div class="w-[55px] h-[55px] rounded-full bg-lightBlue2 flex items-center justify-center">
                                                <img src="/assets_out/images/prm/testimonials/IRI_testimonial.png" alt="Len B.profile image">
                                            </div>
                                            <div class="flex flex-col">
                                                <h5 class="text-base lg:text-lg font-semibold font-inter text-blue">
                                                    <asp:Label ID="Label110" runat="server" /></h5>
                                                <p class="text-sm lg:text-base text-textGray">
                                                    <asp:Label ID="Label111" runat="server" />
                                                </p>
                                            </div>
                                        </div>
                                        <div class="divider"></div>
                                        <div class="gap-5px mdl:gap-15px justify-center lg:justify-start text-center lg:text-left flex flex-col">
                                            <h4 class="text-base lg:text-body font-inter font-bold">&quot;Great Combination of Software &amp; Support&quot;</h4>
                                            <p class="text-sm lg:text-base">
                                                <asp:Label ID="Label109" runat="server" />
                                            </p>
                                        </div>
                                        <div class="justify-center lg:justify-end flex">
                                            <a class="btn font-bold text-sm bg-white" href="https://www.capterra.com/p/171144/Elioplus-PRM/reviews/" target="_blank"><span>READ FULL REVIEW</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                            </defs></svg>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                                <div class="p-20px">
                                    <div class="bg-white rounded-10px shadow-card flex flex-col gap-25px py-30px px-25px">
                                        <div class="justify-center lg:justify-start text-center lg:text-left flex gap-15px flex-wrap">
                                            <div class="w-[55px] h-[55px] rounded-full bg-lightBlue2 flex items-center justify-center">
                                                <img src="/assets_out/images/prm/testimonials/chatwork_testimonial.png" alt="Len B.profile image">
                                            </div>
                                            <div class="flex flex-col">
                                                <h5 class="text-base lg:text-lg font-semibold font-inter text-blue">
                                                    <asp:Label ID="Label99" runat="server" /></h5>
                                                <p class="text-sm lg:text-base text-textGray">
                                                    <asp:Label ID="Label98" runat="server" />
                                                </p>
                                                <p class="text-sm lg:text-base text-textGray">
                                                    <br />
                                                    <br />
                                                </p>
                                            </div>
                                        </div>
                                        <div class="divider"></div>
                                        <div class="gap-5px mdl:gap-15px justify-center lg:justify-start text-center lg:text-left flex flex-col">
                                            <h4 class="text-base lg:text-body font-inter font-bold">&quot;Great Combination of Software &amp; Support&quot;</h4>
                                            <p class="text-sm lg:text-base">
                                                <asp:Label ID="Label97" runat="server" />
                                            </p>
                                        </div>
                                        <div class="justify-center lg:justify-end flex">
                                            <a class="btn font-bold text-sm bg-white" href="https://www.capterra.com/p/171144/Elioplus-PRM/reviews/" target="_blank"><span>READ FULL REVIEW</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                            </defs></svg>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                                <div class="p-20px">
                                    <div class="bg-white rounded-10px shadow-card flex flex-col gap-25px py-30px px-25px">
                                        <div class="justify-center lg:justify-start text-center lg:text-left flex gap-15px flex-wrap">
                                            <div class="w-[55px] h-[55px] rounded-full bg-lightBlue2 flex items-center justify-center">
                                                <img src="/assets_out/images/prm/testimonials/evernote_testimonial.png" alt="Len B.profile image">
                                            </div>
                                            <div class="flex flex-col">
                                                <h5 class="text-base lg:text-lg font-semibold font-inter text-blue">
                                                    <asp:Label ID="Label101" runat="server" /></h5>
                                                <p class="text-sm lg:text-base text-textGray">
                                                    <asp:Label ID="Label102" runat="server" />
                                                </p>
                                                <p class="text-sm lg:text-base text-textGray">
                                                    <br />
                                                    <br />
                                                </p>
                                            </div>
                                        </div>
                                        <div class="divider"></div>
                                        <div class="gap-5px mdl:gap-15px justify-center lg:justify-start text-center lg:text-left flex flex-col">
                                            <h4 class="text-base lg:text-body font-inter font-bold">&quot;Great Combination of Software &amp; Support&quot;</h4>
                                            <p class="text-sm lg:text-base">
                                                <asp:Label ID="Label100" runat="server" />
                                            </p>
                                        </div>
                                        <div class="justify-center lg:justify-end flex">
                                            <a class="btn font-bold text-sm bg-white" href="https://www.capterra.com/p/171144/Elioplus-PRM/reviews/" target="_blank"><span>READ FULL REVIEW</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                            </defs></svg>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                                <div class="p-20px">
                                    <div class="bg-white rounded-10px shadow-card flex flex-col gap-25px py-30px px-25px">
                                        <div class="justify-center lg:justify-start text-center lg:text-left flex gap-15px flex-wrap">
                                            <div class="w-[55px] h-[55px] rounded-full bg-lightBlue2 flex items-center justify-center">
                                                <img src="/assets_out/images/prm/testimonials/bobile_testimonial.png" alt="Len B.profile image">
                                            </div>
                                            <div class="flex flex-col">
                                                <h5 class="text-base lg:text-lg font-semibold font-inter text-blue">
                                                    <asp:Label ID="Label104" runat="server" /></h5>
                                                <p class="text-sm lg:text-base text-textGray">
                                                    <asp:Label ID="Label105" runat="server" />
                                                </p>
                                                <p class="text-sm lg:text-base text-textGray">
                                                    <br />
                                                </p>
                                            </div>
                                        </div>
                                        <div class="divider"></div>
                                        <div class="gap-5px mdl:gap-15px justify-center lg:justify-start text-center lg:text-left flex flex-col">
                                            <h4 class="text-base lg:text-body font-inter font-bold">&quot;Great Combination of Software &amp; Support&quot;</h4>
                                            <p class="text-sm lg:text-base">
                                                <asp:Label ID="Label103" runat="server" />
                                            </p>
                                        </div>
                                        <div class="justify-center lg:justify-end flex">
                                            <a class="btn font-bold text-sm bg-white" href="https://www.capterra.com/p/171144/Elioplus-PRM/reviews/" target="_blank"><span>READ FULL REVIEW</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                            </defs></svg>
                                            </a>
                                        </div>
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
