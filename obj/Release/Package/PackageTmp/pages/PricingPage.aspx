<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="PricingPage.aspx.cs" Inherits="WdS.ElioPlus.pages.PricingPage" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="View our pricing plans for partner management and recruitment. Join for free on our basic plan, upgrade or downgrade anytime. Completely free for channel partners." />
    <meta name="keywords" content="elioplus pricing, partner recruitment pricing, partner management pricing, free PRM software, vendor management free" />

</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <main class="transition-all duration-200">
        <section class="bg-gray opener">
            <div class="container">
                <div class="grid-cols-1 lg:grid-cols-2 grid gap-30px">
                    <div class="row-start-2 lg:row-start-1 gap-20px lg:gap-30px flex flex-col justify-center">
                        <div class="text-center lg:text-left gap-10px lg:gap-15px flex flex-col">
                            <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Pricing</h1>
                            <h2 class="text-2xl lg:text-3xl font-bold font-inter text-blue">The cost of Elioplus products.</h2>
                        </div>
                        <p class="text-base lg:text-body text-center lg:text-left">All our products for software, cloud and IT vendors include a free package. All our features are completely free for channel partners without limitations.</p>
                        <div class="flex items-center gap-10px xl:gap-15px flex-col lg:flex-row">
                            <a class="btn large font-bold text-sm xl:text-base bg-blue text-white" href="#productsPricing"><span>PRODUCTS PRICING</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                        </div>
                    </div>
                    <img src="/assets_out/images/pricing/opener.svg" alt="Pricing &amp; costs of Elioplus products and services">
                </div>
            </div>
            <div class="mt-30px wave">
                <img src="/assets_out/images/global/wave.svg" alt="Elioplus decorative wave"></div>
        </section>
        <section>
            <div class="container">
                <div class="gap-50px lg:gap-80px flex flex-col">
                    <div class="flex flex-col gap-10px text-center items-center w-full">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Products & Services Pricing</h3>
                        <p class="w-full text-base lg:text-body">We provide 3 main products which include a free pricing plan.
                            <br class='hidden lg:block'>
                            Click on a product below to view the available packages for each one.</p>
                    </div>
                    <div class="gap-30px lg:gap-60px flex w-full flex-col" id="productsPricing">
                        <div class="gap-30px lg:gap-0 flex w-full justify-between flex-wrap">
                            <div class="w-[380px] max-w-full bg-white shadow-card overflow-hidden rounded-10px">
                                <div class="h-[250px] bg-gray flex items-center justify-center">
                                    <img src="/assets_out/images/products/partner-recruitment.svg" alt="Pricing for Partner Recruitment Automate / Database" loading="lazy"></div>
                                <div class="gap-15px py-20px lg:py-30px px-20px lg:px-25px bg-white flex flex-col text-center">
                                    <h3 class="text-base lg:text-2xl font-bold font-inter">Partner Recruitment Automate / Database</h3>
                                    <ul class="text-sm lg:text-base font-regular lg:font-semibold flex flex-col justify-center items-center text-center text-textGray gap-5px">
                                        <li>3 Pricing Plans</li>
                                        <li>Free package included</li>
                                        <li>Starting from $298/month</li>
                                    </ul>
                                    <a class="btn font-bold text-sm text-blue w-full justify-center" href="/channel-partner-recruitment"><span>PRODUCT DETAILS</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                    </defs></svg>
                                    </a><a class="btn font-bold text-sm bg-blue w-full justify-center text-white" href="/partner-recruitment-pricing"><span>VIEW PRICING</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                    </defs></svg>
                                    </a>
                                </div>
                            </div>
                            <div class="w-[380px] max-w-full bg-white shadow-card overflow-hidden rounded-10px">
                                <div class="h-[250px] bg-gray flex items-center justify-center">
                                    <img src="/assets_out/images/products/partner-relationship-manager.svg" alt="Pricing for Partner Relationship Management (PRM)" loading="lazy"></div>
                                <div class="gap-15px py-20px lg:py-30px px-20px lg:px-25px bg-white flex flex-col text-center">
                                    <h3 class="text-base lg:text-2xl font-bold font-inter">Partner Relationship Management (PRM)</h3>
                                    <ul class="text-sm lg:text-base font-regular lg:font-semibold flex flex-col justify-center items-center text-center text-textGray gap-5px">
                                        <li>3 Pricing Plans</li>
                                        <li>Free package included</li>
                                        <li>Starting from $298/month</li>
                                    </ul>
                                    <a class="btn font-bold text-sm text-blue w-full justify-center" href="/prm-software"><span>PRODUCT DETAILS</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                    </defs></svg>
                                    </a><a class="btn font-bold text-sm bg-blue w-full justify-center text-white" href="/prm-software-pricing"><span>VIEW PRICING</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                    </defs></svg>
                                    </a>
                                </div>
                            </div>
                            <div class="w-[380px] max-w-full bg-white shadow-card overflow-hidden rounded-10px">
                                <div class="h-[250px] bg-gray flex items-center justify-center">
                                    <img src="/assets_out/images/products/intent-signals.svg" alt="Pricing for Intent Signals &amp; Lead Generation" loading="lazy"></div>
                                <div class="gap-15px py-20px lg:py-30px px-20px lg:px-25px bg-white flex flex-col text-center">
                                    <h3 class="text-base lg:text-2xl font-bold font-inter">Intent Signals &amp; Lead Generation</h3>
                                    <ul class="text-sm lg:text-base font-regular lg:font-semibold flex flex-col justify-center items-center text-center text-textGray gap-5px">
                                        <li>Dynamic Pricing</li>
                                        <li>Free leads for local IT companies</li>
                                        <li>Unlimited access to leads/month</li>
                                    </ul>
                                    <a class="btn font-bold text-sm text-blue w-full justify-center" href="/intent-signals"><span>PRODUCT DETAILS</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                    </defs></svg>
                                    </a><a class="btn font-bold text-sm bg-blue w-full justify-center text-white" href="/intent-signals-pricing"><span>VIEW PRICING</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                    </defs></svg>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-white">
            <div class="container">
                <div class="gap-50px lg:gap-80px flex flex-col">
                    <div class="flex flex-col gap-30px text-center items-center w-full">
                        <div class="flex flex-col gap-10px text-center items-center w-full">
                            <h3 class="text-2xl lg:text-3xl font-bold font-inter">For Channel Partners</h3>
                            <p class="w-full text-base lg:text-body">All our features and benefits are completely free for channel partners without limitations.</p>
                        </div>
                        <div class="flex items-center gap-10px xl:gap-15px flex-col lg:flex-row justify-center">
                            <a class="btn large font-bold text-sm xl:text-base" href="/channel-partners-features"><span>VIEW FEATURES IN DETAIL</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a><a id="aFreeSignUp" runat="server" class="btn large font-bold text-sm xl:text-base bg-blue text-white" href="/free-sign-up"><span>GET STARTED NOW</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                        </div>
                    </div>
                    <div class="grid-cols-1 lg:grid-cols-2 text-img-row grid gap-50px">
                        <div class="flex items-center justify-center">
                            <img src="/assets_out/images/pricing/channel-partners.svg" alt="Features for Channel Partners" loading="lazy"></div>
                        <div class="gap-20px lg:gap-30px flex flex-col justify-center">
                            <h3 class="text-2xl lg:text-2.5xl font-bold font-inter text-center lg:text-left">Features:</h3>
                            <div class="flex flex-col gap-20px w-full">
                                <div class="flex gap-10px items-start">
                                    <div class="pt-5px lg:pt-10px">
                                        <div class="w-[15px] h-[15px] bg-blue rounded-full"></div>
                                    </div>
                                    <div class="flex flex-col gap-5px">
                                        <h4 class="text-base lg:text-body font-semibold">Sign up for free</h4>
                                        <h5 class="text-sm lg:text-base text-textGray">Create your company's profile and get started (under 30 seconds).</h5>
                                    </div>
                                </div>
                                <div class="flex gap-10px items-start">
                                    <div class="pt-5px lg:pt-10px">
                                        <div class="w-[15px] h-[15px] bg-blue rounded-full"></div>
                                    </div>
                                    <div class="flex flex-col gap-5px">
                                        <h4 class="text-base lg:text-body font-semibold">Partner with new vendors</h4>
                                        <h5 class="text-sm lg:text-base text-textGray">We analyze and provide you the best vendors recommendations.</h5>
                                    </div>
                                </div>
                                <div class="flex gap-10px items-start">
                                    <div class="pt-5px lg:pt-10px">
                                        <div class="w-[15px] h-[15px] bg-blue rounded-full"></div>
                                    </div>
                                    <div class="flex flex-col gap-5px">
                                        <h4 class="text-base lg:text-body font-semibold">Manage your vendors</h4>
                                        <h5 class="text-sm lg:text-base text-textGray">Collaborate in real time with your vendors using our built in tools.</h5>
                                    </div>
                                </div>
                                <div class="flex gap-10px items-start">
                                    <div class="pt-5px lg:pt-10px">
                                        <div class="w-[15px] h-[15px] bg-blue rounded-full"></div>
                                    </div>
                                    <div class="flex flex-col gap-5px">
                                        <h4 class="text-base lg:text-body font-semibold">Receive sales incentives</h4>
                                        <h5 class="text-sm lg:text-base text-textGray">Receive documentation, sales and marketing material from your vendors.</h5>
                                    </div>
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
                        <div class="flex items-center">
                            <img class="max-w-[350px] lg:max-w-full" src="/assets_out/images/contact-us/faq.svg" alt="Visit the Elioplus FAQ to get answers for your questions" loading="lazy"></div>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
