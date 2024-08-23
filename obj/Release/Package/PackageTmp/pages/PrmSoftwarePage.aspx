<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="PrmSoftwarePage.aspx.cs" Inherits="WdS.ElioPlus.pages.PrmSoftwarePage" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta id="metaDescription" runat="server" name="description" content="" />
    <meta id="metaKeywords" runat="server" name="keywords" content="" />

</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <main class="transition-all duration-200">
        <header class="bg-blue text-white shadow-lg z-10 sticky top-[63px] lg:top-[72px] xl:top-[92px] left-0 w-screen py-0 lg:py-15px">
            <div class="container">
                <div class="flex justify-between">
                    <div class="py-5px px-5px pr-[15px] hidden lg:flex items-center gap-5px bg-blueDark rounded-30px">
                        <img src="/assets_out/images/global/product-circles.svg" alt="Referral Program software product" width="30" height="30">
                        <div class="text-sm font-inter">Product:&nbsp;<strong>PRM Software</strong></div>
                    </div>
                    <nav class="flex gap-10px xl:gap-15px relative items-center w-full lg:w-fit" id="productNavigation">
                        <div class="w-full lg:w-fit overflow-x-auto">
                            <ul class="text-sm flex gap-15px flex-wrap w-max py-15px lg:py-0">
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#overview">
                                    <asp:Literal ID="Literal1" runat="server" /></a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#features">
                                    <asp:Literal ID="Literal3" runat="server" /></a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#integrations">
                                    <asp:Literal ID="Literal4" runat="server" /></a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#pricing">
                                    <asp:Literal ID="Literal6" runat="server" /></a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#clients">
                                    <asp:Literal ID="Literal7" runat="server" /></a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#testimonials">
                                    <asp:Literal ID="Literal8" runat="server" /></a></li>
                            </ul>
                        </div>
                        <a id="aGetStartedFree" runat="server" class="btn hidden lg:flex font-bold text-sm bg-yellow text-white w-fit" href="/prm-free-sign-up"><span>
                            <asp:Label ID="Label06" runat="server" /></span></a>
                    </nav>
                </div>
                <div id="divAllTransArea" runat="server" class="pt-15px">
                    <a id="aTranslateFR" runat="server" href="/fr/prm-software" style="float: left;" class="country bg-white overflow-hidden rounded-30px p-5px flex gap-5px items-center border-gray border-2 w-fit" title="French translation">
                        <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/France.svg" alt="French translation" title="French translation" loading="lazy" width="30" height="31" />
                    </a>
                    <a id="aTranslateES" runat="server" href="/es/prm-software" style="float: left;" class="country bg-white overflow-hidden rounded-30px p-5px flex gap-5px items-center border-gray border-2 w-fit" title="Spanish translation">
                        <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Spain.svg" alt="Spanish translation" title="Spanish translation" loading="lazy" width="30" height="31" />
                    </a>
                    <a id="aTranslateDE" runat="server" href="/de/prm-software" style="float: left;" class="country bg-white overflow-hidden rounded-30px p-5px flex gap-5px items-center border-gray border-2 w-fit" title="German translation">
                        <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Germany.svg" alt="German translation" title="German translation" loading="lazy" width="30" height="31" />
                    </a>
                    <a id="aTranslatePT" runat="server" href="/pt/prm-software" style="float: left;" class="country bg-white overflow-hidden rounded-30px p-5px flex gap-5px items-center border-gray border-2 w-fit" title="Portuguese translation">
                        <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Portugal.svg" alt="Portuguese translation" title="Portuguese translation" loading="lazy" width="30" height="31" />
                    </a>
                    <a id="aTranslateAR" runat="server" href="/ar/prm-software" style="float: left;" class="country bg-white overflow-hidden rounded-30px p-5px flex gap-5px items-center border-gray border-2 w-fit" title="Saudi Arabia translation">
                        <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Saudi_Arabia.svg" alt="Saudi Arabia translation" title="Saudi Arabia translation" loading="lazy" width="30" height="31" />
                    </a>
                    <a id="aTranslateUK" runat="server" href="/prm-software" style="float: left;" class="country bg-white overflow-hidden rounded-30px p-5px flex gap-5px items-center border-gray border-2 w-fit" title="United Kingdom translation">
                        <img id="ImgTransFlag" runat="server" class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/UK.svg" title="United Kingdom translation" alt="United Kingdom translation" loading="lazy" width="30" height="31" />
                    </a>
                </div>
            </div>
        </header>
        <section class="bg-gradient-product opener" id="overview">
            <div class="container">
                <div class="grid-cols-1 lg:grid-cols-2 grid gap-30px">
                    <div class="row-start-2 lg:row-start-1 gap-20px lg:gap-30px flex flex-col justify-center">
                        <div class="text-center lg:text-left gap-10px lg:gap-15px flex flex-col">
                            <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">
                                <asp:Label ID="Label1" runat="server" /></h1>
                            <h2 class="text-2xl lg:text-3xl font-bold font-inter text-blue">
                                <asp:Label ID="Label01" runat="server" /></h2>
                        </div>
                        <p class="text-base lg:text-body text-center lg:text-left">
                            <asp:Label ID="Label2" runat="server" />
                        </p>
                        <div class="flex items-center gap-10px xl:gap-15px flex-col lg:flex-row">
                            <a target="_blank" class="btn large font-bold text-sm xl:text-base" href="https://calendly.com/elioplus"><span>
                                <asp:Label ID="Label7" runat="server" />
                            </span>
                                <svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <g clip-path="url(#clip0_75_23830)">
                                        <path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" />
                                    </g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                    </defs></svg>
                            </a><a id="aGetStartedFreeB" runat="server" class="btn large font-bold text-sm xl:text-base bg-blue text-white" href="/prm-free-sign-up">
                                <svg width="18" height="19" viewBox="0 0 18 19" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <path d="M8.95734 5.12856C10.2416 5.12856 11.2826 4.09242 11.2826 2.81428C11.2826 1.53614 10.2416 0.5 8.95734 0.5C7.67314 0.5 6.63208 1.53614 6.63208 2.81428C6.63208 4.09242 7.67314 5.12856 8.95734 5.12856Z" fill="#39B4EF" />
                                    <path d="M8.95734 11.8141C10.2416 11.8141 11.2826 10.778 11.2826 9.49983C11.2826 8.22169 10.2416 7.18555 8.95734 7.18555C7.67314 7.18555 6.63208 8.22169 6.63208 9.49983C6.63208 10.778 7.67314 11.8141 8.95734 11.8141Z" fill="#39B4EF" />
                                    <path d="M8.95734 18.5001C10.2416 18.5001 11.2826 17.464 11.2826 16.1859C11.2826 14.9077 10.2416 13.8716 8.95734 13.8716C7.67314 13.8716 6.63208 14.9077 6.63208 16.1859C6.63208 17.464 7.67314 18.5001 8.95734 18.5001Z" fill="#39B4EF" />
                                    <path d="M2.75666 11.8141C4.04087 11.8141 5.08192 10.778 5.08192 9.49983C5.08192 8.22169 4.04087 7.18555 2.75666 7.18555C1.47245 7.18555 0.431396 8.22169 0.431396 9.49983C0.431396 10.778 1.47245 11.8141 2.75666 11.8141Z" fill="#39B4EF" />
                                    <path d="M15.6746 11.8141C16.9589 11.8141 17.9999 10.778 17.9999 9.49983C17.9999 8.22169 16.9589 7.18555 15.6746 7.18555C14.3904 7.18555 13.3494 8.22169 13.3494 9.49983C13.3494 10.778 14.3904 11.8141 15.6746 11.8141Z" fill="#39B4EF" />
                                </svg>
                                <span>
                                    <asp:Label ID="Label6" runat="server" /></span></a>
                        </div>
                    </div>
                    <div class="flex justify-center items-center">
                        <img class="shadow-card rounded-10px" src="/assets_out/images/prm/PRM-homepage.png" alt="Partner Relation Management Software (PRM) solution by Elioplus">
                    </div>
                </div>
            </div>
            <div class="mt-30px wave">
                <img src="/assets_out/images/global/wave.svg" alt="Elioplus decorative wave">
            </div>
        </section>
        <section id="features">
            <div class="container">
                <div class="gap-50px lg:gap-80px flex flex-col">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">
                            <asp:Label ID="Label20" runat="server" /></h3>
                        <p class="w-full text-base lg:text-body">
                            <asp:Label ID="LblPrmFeatures1" runat="server" />
                        </p>
                    </div>
                    <div class="gap-80px lg:gap-120px flex flex-col">
                        <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                            <div class="flex justify-center items-center">
                                <img class="shadow-card rounded-10px" src="/assets_out/images/prm/Deal-Registration.png" alt="Deals – Collaborative Sales with Elioplus PRM software" loading="lazy">
                            </div>
                            <div class="gap-15px lg:gap-25px flex flex-col justify-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">
                                    <asp:Label ID="Label9" runat="server" /></h3>
                                <p class="w-full text-base lg:text-xl text-center lg:text-left">
                                    <asp:Label ID="Label10" runat="server" />
                                </p>
                                <div class="flex items-center gap-10px xl:gap-15px flex-col lg:flex-row">
                                    <a id="aDeals" runat="server" class="btn font-bold text-xs xl:text-sm text-blue" href="/prm-software/deal-registration"><span>
                                        <asp:Label ID="Label11" runat="server" />
                                    </span>
                                        <svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg">
                                            <g clip-path="url(#clip0_75_23830)">
                                                <path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" />
                                            </g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                            </defs></svg>
                                    </a>
                                </div>
                            </div>
                        </div>
                        <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                            <div class="row-start-2 lg:row-start-1 gap-15px lg:gap-25px flex flex-col justify-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">
                                    <asp:Label ID="Label12" runat="server" /></h3>
                                <p class="w-full text-base lg:text-xl text-center lg:text-left">
                                    <asp:Label ID="Label13" runat="server" />
                                </p>
                                <div class="flex items-center gap-10px xl:gap-15px flex-col lg:flex-row">
                                    <a id="aLeads" runat="server" class="btn font-bold text-xs xl:text-sm text-blue" href="/prm-software/lead-distribution"><span>
                                        <asp:Label ID="Label14" runat="server" /></span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                        </defs></svg>
                                    </a>
                                </div>
                            </div>
                            <div class="flex justify-center items-center">
                                <img class="shadow-card rounded-10px" src="/assets_out/images/prm/Lead-Distribution.png" alt="Lead Distribution with Elioplus PRM software" loading="lazy">
                            </div>
                        </div>
                        <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                            <div class="flex justify-center items-center">
                                <img class="shadow-card rounded-10px" src="/assets_out/images/prm/CRM-Integrations.png" alt="Integrate your CRM with Elioplus PRM software" loading="lazy">
                            </div>
                            <div class="gap-15px lg:gap-25px flex flex-col justify-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">
                                    <asp:Label ID="Label15" runat="server" /></h3>
                                <p class="w-full text-base lg:text-xl text-center lg:text-left">
                                    <asp:Label ID="Label16" runat="server" />
                                </p>
                                <div class="flex items-center gap-10px xl:gap-15px flex-col lg:flex-row">
                                    <a id="aIntegrations" runat="server" class="btn font-bold text-xs xl:text-sm text-blue" href="/prm-software/crm-integrations"><span>
                                        <asp:Label ID="Label17" runat="server" /></span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                        </defs></svg>
                                    </a>
                                </div>
                            </div>
                        </div>
                        <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                            <div class="row-start-2 lg:row-start-1 gap-15px lg:gap-25px flex flex-col justify-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">
                                    <asp:Label ID="Label18" runat="server" /></h3>
                                <p class="w-full text-base lg:text-xl text-center lg:text-left">
                                    <asp:Label ID="Label19" runat="server" />
                                </p>
                                <div class="flex items-center gap-10px xl:gap-15px flex-col lg:flex-row">
                                    <a id="aContentManagement" runat="server" class="btn font-bold text-xs xl:text-sm text-blue" href="/prm-software/content-management"><span>
                                        <asp:Label ID="Label131" runat="server" /></span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                        </defs></svg>
                                    </a>
                                </div>
                            </div>
                            <div class="flex justify-center items-center">
                                <img class="shadow-card rounded-10px" src="/assets_out/images/prm/Content-Management.png" alt="Content Management with Elioplus PRM software" loading="lazy">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-gray">
            <div class="container">
                <div class="gap-50px lg:gap-80px flex flex-col">
                    <div class="text-center lg:text-left items-center lg:items-start flex flex-col gap-10px">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">
                            <asp:Label ID="Label045" runat="server" /></h3>
                        <p class="w-full text-base lg:text-body">
                            <asp:Label ID="Label0045" runat="server" />
                        </p>
                        <div class="flex items-center gap-10px xl:gap-15px flex-col lg:flex-row mt-10px">
                            <a id="aPrmSoftwareFeatures" runat="server" class="btn large font-bold text-sm xl:text-base bg-blue text-white" href="/prm-software/partner-portal">
                                <svg width="18" height="19" viewBox="0 0 18 19" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <path d="M8.95734 5.12856C10.2416 5.12856 11.2826 4.09242 11.2826 2.81428C11.2826 1.53614 10.2416 0.5 8.95734 0.5C7.67314 0.5 6.63208 1.53614 6.63208 2.81428C6.63208 4.09242 7.67314 5.12856 8.95734 5.12856Z" fill="#39B4EF" />
                                    <path d="M8.95734 11.8141C10.2416 11.8141 11.2826 10.778 11.2826 9.49983C11.2826 8.22169 10.2416 7.18555 8.95734 7.18555C7.67314 7.18555 6.63208 8.22169 6.63208 9.49983C6.63208 10.778 7.67314 11.8141 8.95734 11.8141Z" fill="#39B4EF" />
                                    <path d="M8.95734 18.5001C10.2416 18.5001 11.2826 17.464 11.2826 16.1859C11.2826 14.9077 10.2416 13.8716 8.95734 13.8716C7.67314 13.8716 6.63208 14.9077 6.63208 16.1859C6.63208 17.464 7.67314 18.5001 8.95734 18.5001Z" fill="#39B4EF" />
                                    <path d="M2.75666 11.8141C4.04087 11.8141 5.08192 10.778 5.08192 9.49983C5.08192 8.22169 4.04087 7.18555 2.75666 7.18555C1.47245 7.18555 0.431396 8.22169 0.431396 9.49983C0.431396 10.778 1.47245 11.8141 2.75666 11.8141Z" fill="#39B4EF" />
                                    <path d="M15.6746 11.8141C16.9589 11.8141 17.9999 10.778 17.9999 9.49983C17.9999 8.22169 16.9589 7.18555 15.6746 7.18555C14.3904 7.18555 13.3494 8.22169 13.3494 9.49983C13.3494 10.778 14.3904 11.8141 15.6746 11.8141Z" fill="#39B4EF" />
                                </svg>
                                <span>
                                    <asp:Label ID="Label45" runat="server" /></span></a>
                        </div>
                    </div>
                    <div class="grid-cols-1 md:grid-cols-2 gap-30px lg:gap-50px grid">
                        <div class="w-full flex gap-10px items-start">
                            <div class="pt-5px lg:pt-10px">
                                <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                            </div>
                            <div class="gap-0 lg:gap-5px flex flex-col">
                                <h4 class="text-base lg:text-body font-bold">
                                    <asp:Label ID="Label21" runat="server" /></h4>
                                <h5 class="text-sm lg:text-base">
                                    <asp:Label ID="Label22" runat="server" />
                                </h5>
                                <a class="btn font-bold text-xs xl:text-sm mt-10px text-blue" href="/prm-software/tier-management"><span>READ MORE</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                        <div class="w-full flex gap-10px items-start">
                            <div class="pt-5px lg:pt-10px">
                                <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                            </div>
                            <div class="gap-0 lg:gap-5px flex flex-col">
                                <h4 class="text-base lg:text-body font-bold">
                                    <asp:Label ID="Label29" runat="server" /></h4>
                                <h5 class="text-sm lg:text-base">
                                    <asp:Label ID="Label30" runat="server" />
                                </h5>

                            </div>
                        </div>
                        <div class="w-full flex gap-10px items-start">
                            <div class="pt-5px lg:pt-10px">
                                <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                            </div>
                            <div class="gap-0 lg:gap-5px flex flex-col">
                                <h4 class="text-base lg:text-body font-bold">
                                    <asp:Label ID="Label23" runat="server" /></h4>
                                <h5 class="text-sm lg:text-base">
                                    <asp:Label ID="Label24" runat="server" />
                                </h5>
                                <a class="btn font-bold text-xs xl:text-sm mt-10px text-blue" href="/prm-software/channel-analytics"><span>READ MORE</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                        <div class="w-full flex gap-10px items-start">
                            <div class="pt-5px lg:pt-10px">
                                <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                            </div>
                            <div class="gap-0 lg:gap-5px flex flex-col">
                                <h4 class="text-base lg:text-body font-bold">
                                    <asp:Label ID="Label31" runat="server" /></h4>
                                <h5 class="text-sm lg:text-base">
                                    <asp:Label ID="Label32" runat="server" /></h5>
                                <a class="btn font-bold text-xs xl:text-sm mt-10px text-blue" href="/prm-software/partner-portal"><span>READ MORE</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                        <div class="w-full flex gap-10px items-start">
                            <div class="pt-5px lg:pt-10px">
                                <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                            </div>
                            <div class="gap-0 lg:gap-5px flex flex-col">
                                <h4 class="text-base lg:text-body font-bold">
                                    <asp:Label ID="Label35" runat="server" /></h4>
                                <h5 class="text-sm lg:text-base">
                                    <asp:Label ID="Label36" runat="server" />
                                </h5>
                                <a class="btn font-bold text-xs xl:text-sm mt-10px text-blue" href="/prm-software/partner-activation"><span>READ MORE</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                        <div class="w-full flex gap-10px items-start">
                            <div class="pt-5px lg:pt-10px">
                                <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                            </div>
                            <div class="gap-0 lg:gap-5px flex flex-col">
                                <h4 class="text-base lg:text-body font-bold">
                                    <asp:Label ID="Label33" runat="server" /></h4>
                                <h5 class="text-sm lg:text-base">
                                    <asp:Label ID="Label34" runat="server" /></h5>
                                <a class="btn font-bold text-xs xl:text-sm mt-10px text-blue" href="/prm-software/partner-locator"><span>READ MORE</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                        <div class="w-full flex gap-10px items-start">
                            <div class="pt-5px lg:pt-10px">
                                <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                            </div>
                            <div class="gap-0 lg:gap-5px flex flex-col">
                                <h4 class="text-base lg:text-body font-bold">
                                    <asp:Label ID="Label25" runat="server" /></h4>
                                <h5 class="text-sm lg:text-base">
                                    <asp:Label ID="Label26" runat="server" /></h5>
                                <a class="btn font-bold text-xs xl:text-sm mt-10px text-blue" href="/prm-software/collaboration"><span>READ MORE</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                        <div class="w-full flex gap-10px items-start">
                            <div class="pt-5px lg:pt-10px">
                                <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                            </div>
                            <div class="gap-0 lg:gap-5px flex flex-col">
                                <h4 class="text-base lg:text-body font-bold">
                                    <asp:Label ID="Label27" runat="server" /></h4>
                                <h5 class="text-sm lg:text-base">
                                    <asp:Label ID="Label28" runat="server" /></h5>
                                <a class="btn font-bold text-xs xl:text-sm mt-10px text-blue" href="/prm-software/team-roles"><span>READ MORE</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-white" id="integrations">
            <div class="container">
                <div class="flex flex-col gap-40px">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">
                            <asp:Label ID="Label46" runat="server" /></h3>
                        <p class="w-full text-base lg:text-body">
                            <asp:Label ID="Label133" runat="server" />
                            <br class='hidden lg:block'>
                            <asp:Label ID="Label134" runat="server" />
                        </p>
                    </div>
                    <div class="m-carousel flex gap-50px justify-center" data-items="1">
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <a id="aIntegrationHubspot" runat="server">
                                    <img src="/assets_out/images/referral-software/hubspot.png" alt="hubspot Integration" loading="lazy" />
                                </a>
                            </div>
                        </div>
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <a id="aIntegrationDynamics" runat="server">
                                    <img src="/assets_out/images/referral-software/microsoftcrm.png" alt="microsoftcrm Integration" loading="lazy" />
                                </a>
                            </div>
                        </div>
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <a id="aIntegrationSalesforce" runat="server">
                                    <img src="/assets_out/images/referral-software/salesforce.png" alt="salesforce Integration" loading="lazy" />
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-gray" id="pricing">
            <div class="container">
                <div class="gap-40px lg:gap-60px flex flex-col justify-center items-center w-full">
                    <div class="gap-20px lg:gap-30px flex flex-col justify-center items-center w-full">
                        <div class="text-center gap-5px lg:gap-10px flex flex-col items-center w-full">
                            <h2 class="text-base lg:text-lg font-semibold text-blue">
                                <asp:Label ID="Label52" runat="server" /></h2>
                            <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Partner Relationship Management</h1>
                            <p class="text-base lg:text-body text-center w-[900px] max-w-full">Manage your partner network and incentivize your partner with our partner relationship management software. Get started with the plan that suits your needs.</p>
                        </div>
                    </div>
                    <div class="grid-cols-1 sm:grid-cols-2 grid lg:flex lg:flex-row lg:justify-between items-center gap-30px lg:gap-20px w-full">
                        <div class="w-[310px]  max-w-full mx-auto bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">Free</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">What you need to get started and try the Elioplus PRM software.</p>
                            </div>
                            <div class="flex flex-col gap-10px w-full">
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">$0</h4>
                                <p class="w-full text-sm lg:text-sm text-center lg:text-left italic text-textGray">Upgrade to a premium plan anytime.</p>
                            </div>
                            <a id="aFree" runat="server" class="btn large font-bold text-sm bg-white border-blackBlue text-blackBlue w-full justify-center" href="/login"><span>START FOR FREE</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                            <div class="flex flex-col gap-15px w-full">
                                <h5 class="text-base font-inter font-semibold">Features:</h5>
                                <div class="flex flex-col gap-5px w-full">
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                <path d="M9.65531 3.57862C10.3951 4.04247 10.9139 4.82739 11.0083 5.73851C11.31 5.87948 11.6449 5.96055 11.9999 5.96055C13.296 5.96055 14.3465 4.91006 14.3465 3.61416C14.3465 2.31807 13.296 1.26758 11.9999 1.26758C10.7162 1.26798 9.67488 2.2997 9.65531 3.57862ZM8.11801 8.38321C9.4141 8.38321 10.4646 7.33252 10.4646 6.03663C10.4646 4.74073 9.4139 3.69024 8.11801 3.69024C6.82211 3.69024 5.77102 4.74093 5.77102 6.03683C5.77102 7.33272 6.82211 8.38321 8.11801 8.38321ZM9.11339 8.54315H7.12223C5.46552 8.54315 4.11771 9.89116 4.11771 11.5479V13.9829L4.1239 14.0211L4.29163 14.0736C5.87266 14.5676 7.24622 14.7323 8.37679 14.7323C10.585 14.7323 11.8649 14.1027 11.9438 14.0626L12.1005 13.9833H12.1173V11.5479C12.1179 9.89116 10.7701 8.54315 9.11339 8.54315ZM12.9957 6.12069H11.0199C10.9985 6.91121 10.6611 7.62305 10.1273 8.13502C11.6 8.57291 12.6774 9.93849 12.6774 11.5515V12.3018C14.6282 12.2304 15.7524 11.6775 15.8265 11.6403L15.9832 11.5609H16V9.12501C16 7.4685 14.6522 6.12069 12.9957 6.12069ZM4.0005 5.96095C4.45955 5.96095 4.88666 5.82697 5.24847 5.59874C5.36348 4.84856 5.76563 4.19302 6.3401 3.74655C6.34249 3.70262 6.34669 3.65909 6.34669 3.61476C6.34669 2.31867 5.29599 1.26818 4.0005 1.26818C2.70421 1.26818 1.65391 2.31867 1.65391 3.61476C1.65391 4.91026 2.70421 5.96095 4.0005 5.96095ZM6.10787 8.13502C5.57674 7.62565 5.24048 6.9176 5.21592 6.13187C5.14264 6.12648 5.07016 6.12069 4.99548 6.12069H3.00452C1.34781 6.12069 0 7.4685 0 9.12501V11.5605L0.00618994 11.598L0.173917 11.6509C1.44226 12.0469 2.57422 12.2294 3.55742 12.2869V11.5515C3.55782 9.93849 4.63487 8.57331 6.10787 8.13502Z" fill="#39B4EF" />
                                            </svg>

                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label56" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                <g clip-path="url(#clip0_130_13596)">
                                                    <path d="M8 4C12.4183 4 16 3.10457 16 2C16 0.895431 12.4183 0 8 0C3.58172 0 0 0.895431 0 2C0 3.10457 3.58172 4 8 4Z" fill="#39B4EF" />
                                                    <path d="M8 5.33325C3.582 5.33325 0 4.43792 0 3.33325V5.99992C0 7.10459 3.582 7.99992 8 7.99992C12.418 7.99992 16 7.10459 16 5.99992V3.33325C16 4.43792 12.418 5.33325 8 5.33325Z" fill="#39B4EF" />
                                                    <path d="M8 9.33325C3.582 9.33325 0 8.43792 0 7.33325V9.99992C0 11.1046 3.582 11.9999 8 11.9999C12.418 11.9999 16 11.1046 16 9.99992V7.33325C16 8.43792 12.418 9.33325 8 9.33325Z" fill="#39B4EF" />
                                                    <path d="M8 13.3333C3.582 13.3333 0 12.4379 0 11.3333V13.9999C0 15.1046 3.582 15.9999 8 15.9999C12.418 15.9999 16 15.1046 16 13.9999V11.3333C16 12.4379 12.418 13.3333 8 13.3333Z" fill="#39B4EF" />
                                                </g>
                                                <defs>
                                                    <clipPath id="clip0_130_13596">
                                                        <rect width="16" height="16" fill="white" />
                                                    </clipPath>
                                                </defs>
                                            </svg>

                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label57" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Onboarding" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label58" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Deal registration" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label59" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Lead Distribution" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label60" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Collaboration" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label61" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Partner directory" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label62" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Partner to partner" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label63" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/close.svg" alt="Not included feature for Partner recruitment" loading="lazy" />
                                        </div>
                                        <div class="text-[15px] text-textGray">
                                            <asp:Label ID="Label075" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/close.svg" alt="Not included feature for Customization" loading="lazy" />
                                        </div>
                                        <div class="text-[15px] text-textGray">
                                            <asp:Label ID="Label076" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="w-[310px]  max-w-full mx-auto bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">Start-Up</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">For small companies and start-ups that need a PRM solution.</p>
                            </div>
                            <div class="flex flex-col gap-10px w-full">
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">$298</h4>
                                <p class="w-full text-sm lg:text-sm text-center lg:text-left italic text-textGray">Per month, you can cancel anytime.</p>
                            </div>
                            <a id="aStartup" runat="server" class="btn large font-bold text-sm bg-white border-blackBlue text-blackBlue w-full justify-center" href="/login"><span>GET STARTED NOW</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                            <a id="aChekoutStartup" onserverclick="aChekoutStartup_ServerClick" runat="server" visible="false" class="btn large font-bold text-sm bg-blue text-white w-full justify-center"><span>
                                <asp:Label ID="LblCkStartup" runat="server" Text="UPGRADE NOW" /></span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                            </a>
                            <div class="flex flex-col gap-15px w-full">
                                <h5 class="text-base font-inter font-semibold">Features:</h5>
                                <div class="flex flex-col gap-5px w-full">
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                <path d="M9.65531 3.57862C10.3951 4.04247 10.9139 4.82739 11.0083 5.73851C11.31 5.87948 11.6449 5.96055 11.9999 5.96055C13.296 5.96055 14.3465 4.91006 14.3465 3.61416C14.3465 2.31807 13.296 1.26758 11.9999 1.26758C10.7162 1.26798 9.67488 2.2997 9.65531 3.57862ZM8.11801 8.38321C9.4141 8.38321 10.4646 7.33252 10.4646 6.03663C10.4646 4.74073 9.4139 3.69024 8.11801 3.69024C6.82211 3.69024 5.77102 4.74093 5.77102 6.03683C5.77102 7.33272 6.82211 8.38321 8.11801 8.38321ZM9.11339 8.54315H7.12223C5.46552 8.54315 4.11771 9.89116 4.11771 11.5479V13.9829L4.1239 14.0211L4.29163 14.0736C5.87266 14.5676 7.24622 14.7323 8.37679 14.7323C10.585 14.7323 11.8649 14.1027 11.9438 14.0626L12.1005 13.9833H12.1173V11.5479C12.1179 9.89116 10.7701 8.54315 9.11339 8.54315ZM12.9957 6.12069H11.0199C10.9985 6.91121 10.6611 7.62305 10.1273 8.13502C11.6 8.57291 12.6774 9.93849 12.6774 11.5515V12.3018C14.6282 12.2304 15.7524 11.6775 15.8265 11.6403L15.9832 11.5609H16V9.12501C16 7.4685 14.6522 6.12069 12.9957 6.12069ZM4.0005 5.96095C4.45955 5.96095 4.88666 5.82697 5.24847 5.59874C5.36348 4.84856 5.76563 4.19302 6.3401 3.74655C6.34249 3.70262 6.34669 3.65909 6.34669 3.61476C6.34669 2.31867 5.29599 1.26818 4.0005 1.26818C2.70421 1.26818 1.65391 2.31867 1.65391 3.61476C1.65391 4.91026 2.70421 5.96095 4.0005 5.96095ZM6.10787 8.13502C5.57674 7.62565 5.24048 6.9176 5.21592 6.13187C5.14264 6.12648 5.07016 6.12069 4.99548 6.12069H3.00452C1.34781 6.12069 0 7.4685 0 9.12501V11.5605L0.00618994 11.598L0.173917 11.6509C1.44226 12.0469 2.57422 12.2294 3.55742 12.2869V11.5515C3.55782 9.93849 4.63487 8.57331 6.10787 8.13502Z" fill="#39B4EF" />
                                            </svg>

                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label67" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                <g clip-path="url(#clip0_130_13596)">
                                                    <path d="M8 4C12.4183 4 16 3.10457 16 2C16 0.895431 12.4183 0 8 0C3.58172 0 0 0.895431 0 2C0 3.10457 3.58172 4 8 4Z" fill="#39B4EF" />
                                                    <path d="M8 5.33325C3.582 5.33325 0 4.43792 0 3.33325V5.99992C0 7.10459 3.582 7.99992 8 7.99992C12.418 7.99992 16 7.10459 16 5.99992V3.33325C16 4.43792 12.418 5.33325 8 5.33325Z" fill="#39B4EF" />
                                                    <path d="M8 9.33325C3.582 9.33325 0 8.43792 0 7.33325V9.99992C0 11.1046 3.582 11.9999 8 11.9999C12.418 11.9999 16 11.1046 16 9.99992V7.33325C16 8.43792 12.418 9.33325 8 9.33325Z" fill="#39B4EF" />
                                                    <path d="M8 13.3333C3.582 13.3333 0 12.4379 0 11.3333V13.9999C0 15.1046 3.582 15.9999 8 15.9999C12.418 15.9999 16 15.1046 16 13.9999V11.3333C16 12.4379 12.418 13.3333 8 13.3333Z" fill="#39B4EF" />
                                                </g>
                                                <defs>
                                                    <clipPath id="clip0_130_13596">
                                                        <rect width="16" height="16" fill="white" />
                                                    </clipPath>
                                                </defs>
                                            </svg>

                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label68" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Onboarding" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label69" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Deal registration" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label70" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Lead Distribution" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label71" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Collaboration" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label72" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Partner directory" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label73" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Partner to partner" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label74" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Partner recruitment" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label75" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Customization" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label76" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="w-[310px]  max-w-full mx-auto bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left text-blue">Growth</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">For medium and large companies that scale and grow fast.</p>
                            </div>
                            <div class="flex flex-col gap-10px w-full">
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">$498</h4>
                                <p class="w-full text-sm lg:text-sm text-center lg:text-left italic text-textGray">Per month, you can cancel anytime.</p>
                            </div>
                            <a id="aGrowth" runat="server" class="btn large font-bold text-sm bg-blue text-white w-full justify-center" href="/login"><span>GET STARTED NOW</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                            <a id="aChekoutGrowth" onserverclick="aChekoutGrowth_ServerClick" runat="server" visible="false" class="btn large font-bold text-sm bg-blue text-white w-full justify-center"><span>
                                <asp:Label ID="LblCkGrowth" runat="server" Text="UPGRADE NOW" /></span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                            </a>
                            <div class="flex flex-col gap-15px w-full">
                                <h5 class="text-base font-inter font-semibold">Features:</h5>
                                <div class="flex flex-col gap-5px w-full">
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                <path d="M9.65531 3.57862C10.3951 4.04247 10.9139 4.82739 11.0083 5.73851C11.31 5.87948 11.6449 5.96055 11.9999 5.96055C13.296 5.96055 14.3465 4.91006 14.3465 3.61416C14.3465 2.31807 13.296 1.26758 11.9999 1.26758C10.7162 1.26798 9.67488 2.2997 9.65531 3.57862ZM8.11801 8.38321C9.4141 8.38321 10.4646 7.33252 10.4646 6.03663C10.4646 4.74073 9.4139 3.69024 8.11801 3.69024C6.82211 3.69024 5.77102 4.74093 5.77102 6.03683C5.77102 7.33272 6.82211 8.38321 8.11801 8.38321ZM9.11339 8.54315H7.12223C5.46552 8.54315 4.11771 9.89116 4.11771 11.5479V13.9829L4.1239 14.0211L4.29163 14.0736C5.87266 14.5676 7.24622 14.7323 8.37679 14.7323C10.585 14.7323 11.8649 14.1027 11.9438 14.0626L12.1005 13.9833H12.1173V11.5479C12.1179 9.89116 10.7701 8.54315 9.11339 8.54315ZM12.9957 6.12069H11.0199C10.9985 6.91121 10.6611 7.62305 10.1273 8.13502C11.6 8.57291 12.6774 9.93849 12.6774 11.5515V12.3018C14.6282 12.2304 15.7524 11.6775 15.8265 11.6403L15.9832 11.5609H16V9.12501C16 7.4685 14.6522 6.12069 12.9957 6.12069ZM4.0005 5.96095C4.45955 5.96095 4.88666 5.82697 5.24847 5.59874C5.36348 4.84856 5.76563 4.19302 6.3401 3.74655C6.34249 3.70262 6.34669 3.65909 6.34669 3.61476C6.34669 2.31867 5.29599 1.26818 4.0005 1.26818C2.70421 1.26818 1.65391 2.31867 1.65391 3.61476C1.65391 4.91026 2.70421 5.96095 4.0005 5.96095ZM6.10787 8.13502C5.57674 7.62565 5.24048 6.9176 5.21592 6.13187C5.14264 6.12648 5.07016 6.12069 4.99548 6.12069H3.00452C1.34781 6.12069 0 7.4685 0 9.12501V11.5605L0.00618994 11.598L0.173917 11.6509C1.44226 12.0469 2.57422 12.2294 3.55742 12.2869V11.5515C3.55782 9.93849 4.63487 8.57331 6.10787 8.13502Z" fill="#39B4EF" />
                                            </svg>

                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label81" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                <g clip-path="url(#clip0_130_13596)">
                                                    <path d="M8 4C12.4183 4 16 3.10457 16 2C16 0.895431 12.4183 0 8 0C3.58172 0 0 0.895431 0 2C0 3.10457 3.58172 4 8 4Z" fill="#39B4EF" />
                                                    <path d="M8 5.33325C3.582 5.33325 0 4.43792 0 3.33325V5.99992C0 7.10459 3.582 7.99992 8 7.99992C12.418 7.99992 16 7.10459 16 5.99992V3.33325C16 4.43792 12.418 5.33325 8 5.33325Z" fill="#39B4EF" />
                                                    <path d="M8 9.33325C3.582 9.33325 0 8.43792 0 7.33325V9.99992C0 11.1046 3.582 11.9999 8 11.9999C12.418 11.9999 16 11.1046 16 9.99992V7.33325C16 8.43792 12.418 9.33325 8 9.33325Z" fill="#39B4EF" />
                                                    <path d="M8 13.3333C3.582 13.3333 0 12.4379 0 11.3333V13.9999C0 15.1046 3.582 15.9999 8 15.9999C12.418 15.9999 16 15.1046 16 13.9999V11.3333C16 12.4379 12.418 13.3333 8 13.3333Z" fill="#39B4EF" />
                                                </g>
                                                <defs>
                                                    <clipPath id="clip0_130_13596">
                                                        <rect width="16" height="16" fill="white" />
                                                    </clipPath>
                                                </defs>
                                            </svg>

                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label82" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Onboarding" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label83" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Deal registration" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label84" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Lead Distribution" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label85" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Collaboration" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label86" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Partner directory" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label87" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Partner to partner" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label88" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Partner recruitment" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label89" runat="server" />
                                        </div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Customization" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">
                                            <asp:Label ID="Label90" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="w-[310px]  max-w-full mx-auto bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">Enterprise</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">For big corporations with extensive requirements that need a PRM.</p>
                            </div>
                            <div class="flex flex-col gap-10px w-full">
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">$1200+</h4>
                                <p class="w-full text-sm lg:text-sm text-center lg:text-left italic text-textGray">Custom pricing, starting from.</p>
                            </div>
                            <a id="aContactUs" runat="server" class="btn large font-bold text-sm bg-white border-blackBlue text-blackBlue w-full justify-center" href="/contact-us"><span>CONTACT US</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                            <div class="flex flex-col gap-15px w-full">
                                <h5 class="text-base font-inter font-semibold">Features:</h5>
                                <div class="flex flex-col gap-5px w-full">
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                <path d="M9.65531 3.57862C10.3951 4.04247 10.9139 4.82739 11.0083 5.73851C11.31 5.87948 11.6449 5.96055 11.9999 5.96055C13.296 5.96055 14.3465 4.91006 14.3465 3.61416C14.3465 2.31807 13.296 1.26758 11.9999 1.26758C10.7162 1.26798 9.67488 2.2997 9.65531 3.57862ZM8.11801 8.38321C9.4141 8.38321 10.4646 7.33252 10.4646 6.03663C10.4646 4.74073 9.4139 3.69024 8.11801 3.69024C6.82211 3.69024 5.77102 4.74093 5.77102 6.03683C5.77102 7.33272 6.82211 8.38321 8.11801 8.38321ZM9.11339 8.54315H7.12223C5.46552 8.54315 4.11771 9.89116 4.11771 11.5479V13.9829L4.1239 14.0211L4.29163 14.0736C5.87266 14.5676 7.24622 14.7323 8.37679 14.7323C10.585 14.7323 11.8649 14.1027 11.9438 14.0626L12.1005 13.9833H12.1173V11.5479C12.1179 9.89116 10.7701 8.54315 9.11339 8.54315ZM12.9957 6.12069H11.0199C10.9985 6.91121 10.6611 7.62305 10.1273 8.13502C11.6 8.57291 12.6774 9.93849 12.6774 11.5515V12.3018C14.6282 12.2304 15.7524 11.6775 15.8265 11.6403L15.9832 11.5609H16V9.12501C16 7.4685 14.6522 6.12069 12.9957 6.12069ZM4.0005 5.96095C4.45955 5.96095 4.88666 5.82697 5.24847 5.59874C5.36348 4.84856 5.76563 4.19302 6.3401 3.74655C6.34249 3.70262 6.34669 3.65909 6.34669 3.61476C6.34669 2.31867 5.29599 1.26818 4.0005 1.26818C2.70421 1.26818 1.65391 2.31867 1.65391 3.61476C1.65391 4.91026 2.70421 5.96095 4.0005 5.96095ZM6.10787 8.13502C5.57674 7.62565 5.24048 6.9176 5.21592 6.13187C5.14264 6.12648 5.07016 6.12069 4.99548 6.12069H3.00452C1.34781 6.12069 0 7.4685 0 9.12501V11.5605L0.00618994 11.598L0.173917 11.6509C1.44226 12.0469 2.57422 12.2294 3.55742 12.2869V11.5515C3.55782 9.93849 4.63487 8.57331 6.10787 8.13502Z" fill="#39B4EF" />
                                            </svg>

                                        </div>
                                        <div class="text-[15px]">Custom partners management</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                <g clip-path="url(#clip0_130_13596)">
                                                    <path d="M8 4C12.4183 4 16 3.10457 16 2C16 0.895431 12.4183 0 8 0C3.58172 0 0 0.895431 0 2C0 3.10457 3.58172 4 8 4Z" fill="#39B4EF" />
                                                    <path d="M8 5.33325C3.582 5.33325 0 4.43792 0 3.33325V5.99992C0 7.10459 3.582 7.99992 8 7.99992C12.418 7.99992 16 7.10459 16 5.99992V3.33325C16 4.43792 12.418 5.33325 8 5.33325Z" fill="#39B4EF" />
                                                    <path d="M8 9.33325C3.582 9.33325 0 8.43792 0 7.33325V9.99992C0 11.1046 3.582 11.9999 8 11.9999C12.418 11.9999 16 11.1046 16 9.99992V7.33325C16 8.43792 12.418 9.33325 8 9.33325Z" fill="#39B4EF" />
                                                    <path d="M8 13.3333C3.582 13.3333 0 12.4379 0 11.3333V13.9999C0 15.1046 3.582 15.9999 8 15.9999C12.418 15.9999 16 15.1046 16 13.9999V11.3333C16 12.4379 12.418 13.3333 8 13.3333Z" fill="#39B4EF" />
                                                </g>
                                                <defs>
                                                    <clipPath id="clip0_130_13596">
                                                        <rect width="16" height="16" fill="white" />
                                                    </clipPath>
                                                </defs>
                                            </svg>

                                        </div>
                                        <div class="text-[15px]">Custom library storage</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Onboarding" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Onboarding</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Deal registration" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Deal registration</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Lead Distribution" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Lead Distribution</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Collaboration" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Collaboration</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Partner directory" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Partner directory</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Partner to partner" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Partner to partner</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Partner recruitment" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Partner recruitment</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Customization" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Customization</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--<p class="text-sm lg:text-base text-center w-[970px] max-w-full text-textGray italic">
                        <asp:Label ID="Label92" runat="server" />
                        <br class='hidden lg:block'>
                        <asp:Label ID="Label092" runat="server" />
                    </p>--%>
                </div>
            </div>
        </section>
        <section class="bg-white" id="clients">
            <div class="container">
                <div class="flex flex-col gap-40px">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">
                            <asp:Label ID="Label94" runat="server" /></h3>
                        <p class="w-full text-base lg:text-body">
                            View some of our clients that have trusted Elioplus to launch their partner portal.
                        </p>
                    </div>
                    <div class="m-carousel flex sm:grid sm:grid-cols-3 lg:flex gap-30px justify-center" data-items="1">
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <a id="aClient5" runat="server">
                                    <img src="/assets_out/images/prm/Randstad.png" alt="sap logo" loading="lazy" />
                                </a>
                            </div>
                        </div>
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <a id="aClient2" runat="server">
                                    <img src="/assets_out/images/prm/FireCompass.png" alt="iri logo" loading="lazy" />
                                </a>
                            </div>
                        </div>
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <a id="aClient1" runat="server">
                                    <img src="/assets_out/images/prm/DeepCrawl.png" alt="repsly logo" loading="lazy" />
                                </a>
                            </div>
                        </div>
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <a id="aClient6" runat="server">
                                    <img src="/assets_out/images/prm/Claritytel.png" alt="totara logo" loading="lazy" />
                                </a>
                            </div>
                        </div>
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <a id="aClient4" runat="server">
                                    <img src="/assets_out/images/prm/BluedogSecurity.png" alt="contentverse logo" loading="lazy" />
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-gray" id="testimonials">
            <div class="container">
                <div class="gap-50px lg:gap-80px flex flex-col">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">
                            <asp:Label ID="Label95" runat="server" /></h3>
                        <p class="w-[760px] max-w-full text-base lg:text-body">
                            <asp:Label ID="Label96" runat="server" />
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
        <section>
            <div class="container">
                <div class="gap-80px lg:gap-160px flex flex-col">
                    <div class="grid-cols-1 lg:grid-cols-2 text-img-row grid gap-30px">
                        <div class="row-start-2 lg:row-start-1 gap-20px lg:gap-30px flex flex-col justify-center">
                            <div class="gap-5px lg:gap-10px flex flex-col">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Do you have questions?</h3>
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left text-blue">We have the answers.</h4>
                            </div>
                            <p class="w-full text-base lg:text-body text-center lg:text-left">You can visit our help center with frequently asked questions about ElioPlus and our products & services.</p>
                            <div class="flex justify-center lg:justify-start">
                                <a class="btn large font-bold text-sm xl:text-base text-blue" href="/faq"><span>VIEW FREQUENTLY ASKED QUESTIONS</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                        <div class="flex items-center justify-center">
                            <img class="max-w-[350px] lg:max-w-full" src="/assets_out/images/contact-us/faq.svg" alt="Visit the Elioplus FAQ to get answers for your questions" loading="lazy">
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
