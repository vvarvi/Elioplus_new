<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="FeaturesForVendorsPage.aspx.cs" Inherits="WdS.ElioPlus.pages.FeaturesForVendorsPage" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="" />
    <meta name="keywords" content="" />

</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <main class="transition-all duration-200">
        <section class="bg-gray opener">
            <div class="container">
                <div class="grid-cols-1 lg:grid-cols-2 grid gap-30px">
                    <div class="row-start-2 lg:row-start-1 gap-20px lg:gap-30px flex flex-col justify-center">
                        <div class="text-center lg:text-left gap-10px lg:gap-15px flex flex-col">
                            <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Features for IT Vendors</h1>
                            <h2 class="text-2xl lg:text-3xl font-bold font-inter text-blue">The benefits of Elioplus vendors.</h2>
                        </div>
                        <p class="text-base lg:text-body text-center lg:text-left">All our products for IT vendors include a free package. On this page you can view all the perks that you have if you create an account with Elioplus as a vendor.</p>
                        <div class="flex items-center gap-10px xl:gap-15px flex-col lg:flex-row">
                            <a class="btn large font-bold text-sm xl:text-base" href="/channel-partners-features"><span>CHANNEL PARTNER FEATURES</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a><a id="aSignUp" runat="server" class="btn large font-bold text-sm xl:text-base bg-blue text-white" href="/free-sign-up"><span>GET STARTED FOR FREE</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                        </div>
                    </div>
                    <div class="flex justify-center">
                        <img class="max-w-[300px] mdl:max-w-full" src="/assets_out/images/features/vendors/opener.svg" alt="The features and benefits of Elioplus for IT Vendors offering partner programs"></div>
                </div>
            </div>
            <div class="mt-30px wave">
                <img src="/assets_out/images/global/wave.svg" alt="Elioplus decorative wave"></div>
        </section>
        <section>
            <div class="container">
                <div class="gap-80px lg:gap-160px flex flex-col">
                    <div class="grid-cols-1 lg:grid-cols-2 text-img-row grid gap-30px">
                        <div class="row-start-2 lg:row-start-1 gap-20px lg:gap-30px flex flex-col justify-center">
                            <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Sign up for Free</h3>
                            <p class="w-full text-base lg:text-body text-center lg:text-left">Sign up (under 30 seconds), create your company's account and provide some information about your partner program structure like partnership types, benefits, support and requirements. You can add unlimited members in your team, like the Channel Manager, the Business Development etc.</p>
                        </div>
                        <div class="flex items-center justify-center">
                            <img class="max-w-[300px] mdl:max-w-full" src="/assets_out/images/features/vendors/signup.svg" alt="Sign up for free for Vendors" loading="lazy"></div>
                    </div>
                    <div class="grid-cols-1 lg:grid-cols-2 text-img-row grid gap-30px">
                        <div class="flex items-center justify-center">
                            <img class="max-w-[300px] mdl:max-w-full" src="/assets_out/images/features/vendors/promote.svg" alt="Promote your partner program as an IT vendor at Elioplus" loading="lazy"></div>
                        <div class="gap-20px lg:gap-30px flex flex-col justify-center">
                            <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Promote your partner program</h3>
                            <p class="w-full text-base lg:text-body text-center lg:text-left">Promote your partner program and attract high quality leads to recruit new business partners and grow your channel network. Put your partner program in front of thousands of channel partners that visit our website.</p>
                        </div>
                    </div>
                    <div class="grid-cols-1 lg:grid-cols-2 text-img-row grid gap-30px">
                        <div class="row-start-2 lg:row-start-1 gap-20px lg:gap-30px flex flex-col justify-center">
                            <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Get matched automatically</h3>
                            <p class="w-full text-base lg:text-body text-center lg:text-left">Based on the data you provide us, we look in 15 main criteria like product category, geographic markets, monetization structure etc. and provide you with channel partner leads that match these criteria. You set it once and we provide partner leads continuously.</p>
                        </div>
                        <div class="flex items-center justify-center">
                            <img class="max-w-[300px] mdl:max-w-full" src="/assets_out/images/features/vendors/automatic-match.svg" alt="Get matched automatically as an sass vendor at Elioplus" loading="lazy"></div>
                    </div>
                    <div class="grid-cols-1 lg:grid-cols-2 text-img-row grid gap-30px">
                        <div class="flex items-center justify-center">
                            <img class="max-w-[300px] mdl:max-w-full" src="/assets_out/images/features/vendors/opportunities.svg" alt="Create a pipeline to manage all your partner leads an IT vendor at Elioplus" loading="lazy"></div>
                        <div class="gap-20px lg:gap-30px flex flex-col justify-center">
                            <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Opportunities pipeline</h3>
                            <p class="w-full text-base lg:text-body text-center lg:text-left">Create a pipeline to manage all your partner leads and create notes and task reminders for every event. You can also add partner leads from your website, events etc. to unify all your leads.</p>
                        </div>
                    </div>
                    <div class="grid-cols-1 lg:grid-cols-2 text-img-row grid gap-30px">
                        <div class="row-start-2 lg:row-start-1 gap-20px lg:gap-30px flex flex-col justify-center">
                            <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Collaborate with your partners</h3>
                            <p class="w-full text-base lg:text-body text-center lg:text-left">Collaborate in real time with your partners to send them product, marketing and sales material. Manage your channel partners, create a content library and many more features to enable your channel network to promote your products.</p>
                        </div>
                        <div class="flex items-center justify-center">
                            <img class="max-w-[300px] mdl:max-w-full" src="/assets_out/images/features/vendors/collaborate.svg" alt="Collaborate with your partners as a software vendor at Elioplus" loading="lazy"></div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-white">
            <div class="container">
                <div class="flex flex-col gap-40px">
                    <div class="flex items-center justify-center w-full">
                        <img class="max-w-[350px] lg:max-w-full" src="/assets_out/images/global/cta.svg" alt="Improve your business by using Elioplus" loading="lazy"></div>
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">More than <span class='text-blue'>120.000</span> companies are using Elioplus</h3>
                        <p class="w-full lg:w-[854px] max-w-full text-base lg:text-body">What are you waiting for? Add your company for free and find out how you can improve your business with us.</p>
                    </div>
                    <div class="flex justify-center items-center flex-col lg:flex-row gap-10px xl:gap-20px">
                        <a id="aGetStarted" runat="server" class="btn large font-bold text-sm xl:text-base bg-blue text-white" href="/free-sign-up">
                            <svg width="18" height="19" viewBox="0 0 18 19" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <path d="M8.95734 5.12856C10.2416 5.12856 11.2826 4.09242 11.2826 2.81428C11.2826 1.53614 10.2416 0.5 8.95734 0.5C7.67314 0.5 6.63208 1.53614 6.63208 2.81428C6.63208 4.09242 7.67314 5.12856 8.95734 5.12856Z" fill="#39B4EF" />
                                <path d="M8.95734 11.8141C10.2416 11.8141 11.2826 10.778 11.2826 9.49983C11.2826 8.22169 10.2416 7.18555 8.95734 7.18555C7.67314 7.18555 6.63208 8.22169 6.63208 9.49983C6.63208 10.778 7.67314 11.8141 8.95734 11.8141Z" fill="#39B4EF" />
                                <path d="M8.95734 18.5001C10.2416 18.5001 11.2826 17.464 11.2826 16.1859C11.2826 14.9077 10.2416 13.8716 8.95734 13.8716C7.67314 13.8716 6.63208 14.9077 6.63208 16.1859C6.63208 17.464 7.67314 18.5001 8.95734 18.5001Z" fill="#39B4EF" />
                                <path d="M2.75666 11.8141C4.04087 11.8141 5.08192 10.778 5.08192 9.49983C5.08192 8.22169 4.04087 7.18555 2.75666 7.18555C1.47245 7.18555 0.431396 8.22169 0.431396 9.49983C0.431396 10.778 1.47245 11.8141 2.75666 11.8141Z" fill="#39B4EF" />
                                <path d="M15.6746 11.8141C16.9589 11.8141 17.9999 10.778 17.9999 9.49983C17.9999 8.22169 16.9589 7.18555 15.6746 7.18555C14.3904 7.18555 13.3494 8.22169 13.3494 9.49983C13.3494 10.778 14.3904 11.8141 15.6746 11.8141Z" fill="#39B4EF" />
                            </svg>
                            <span>CREATE YOUR ACCOUNT FOR FREE</span></a>
                    </div>
                </div>
            </div>
        </section>
        <section>
            <div class="container">
                <div class="gap-50px lg:gap-80px flex flex-col">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Explore our products & services</h3>
                        <p class="w-full text-base lg:text-body">Find out how we are matching IT vendors with channel partners and have created more than <strong>350k</strong> partnerships that expand across <strong>120+</strong> product/service categories and <strong>156</strong> different countries worldwide.</p>
                    </div>
                    <div class="w-full owl-carousel owl-theme" data-items="3">
                        <div class="p-15px lg:p-20px">
                            <a class="w-[380px] max-w-full bg-white shadow-card overflow-hidden rounded-10px" href="/channel-partner-recruitment">
                                <div class="h-[250px] bg-gray flex items-center justify-center">
                                    <img src="/assets_out/images/products/partner-recruitment.svg"></div>
                                <div class="gap-10px lg:gap-15px py-20px lg:py-30px px-20px lg:px-25px bg-white flex flex-col">
                                    <h3 class="text-lg lg:text-2xl font-bold font-inter">Partner Recruitment</h3>
                                    <p class="text-sm lg:text-base">Get matched and recruit the best partners based on location, industry, experience and 13 more criteria.</p>
                                    <div class="btn font-bold text-sm text-blue">
                                        <span>VIEW PRODUCT</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                        </defs></svg>

                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="p-15px lg:p-20px">
                            <a class="w-[380px] max-w-full bg-white shadow-card overflow-hidden rounded-10px" href="/prm-software">
                                <div class="h-[250px] bg-gray flex items-center justify-center">
                                    <img src="/assets_out/images/products/partner-relationship-manager.svg"></div>
                                <div class="gap-10px lg:gap-15px py-20px lg:py-30px px-20px lg:px-25px bg-white flex flex-col">
                                    <h3 class="text-lg lg:text-2xl font-bold font-inter">Partner Management (PRM)</h3>
                                    <p class="text-sm lg:text-base">Manage your partner network and incentivize your partner with our free partner relationship management software.</p>
                                    <div class="btn font-bold text-sm text-blue">
                                        <span>VIEW PRODUCT</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                        </defs></svg>

                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="p-15px lg:p-20px">
                            <a class="w-[380px] max-w-full bg-white shadow-card overflow-hidden rounded-10px" href="/intent-signals">
                                <div class="h-[250px] bg-gray flex items-center justify-center">
                                    <img src="/assets_out/images/products/intent-signals.svg"></div>
                                <div class="gap-10px lg:gap-15px py-20px lg:py-30px px-20px lg:px-25px bg-white flex flex-col">
                                    <h3 class="text-lg lg:text-2xl font-bold font-inter">Lead Generation</h3>
                                    <p class="text-sm lg:text-base">Get access to quotation requests and intent data from local businesses that are on the market.</p>
                                    <div class="btn font-bold text-sm text-blue">
                                        <span>VIEW PRODUCT</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                        </defs></svg>

                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="p-15px lg:p-20px">
                            <a class="w-[380px] max-w-full bg-white shadow-card overflow-hidden rounded-10px" href="/referral-software">
                                <div class="h-[250px] bg-gray flex items-center justify-center">
                                    <img src="/assets_out/images/products/referral-software.svg"></div>
                                <div class="gap-10px lg:gap-15px py-20px lg:py-30px px-20px lg:px-25px bg-white flex flex-col">
                                    <h3 class="text-lg lg:text-2xl font-bold font-inter">Referral Software</h3>
                                    <p class="text-sm lg:text-base">Referral software to scale and automate your referral partner program. Made exclusively for IT companies.</p>
                                    <div class="btn font-bold text-sm text-blue">
                                        <span>VIEW PRODUCT</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                        </defs></svg>

                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="p-15px lg:p-20px">
                            <a class="w-[380px] max-w-full bg-white shadow-card overflow-hidden rounded-10px" href="/mergers-acquisition-marketplace">
                                <div class="h-[250px] bg-gray flex items-center justify-center">
                                    <img src="/assets_out/images/products/ma-marketplace.svg"></div>
                                <div class="gap-10px lg:gap-15px py-20px lg:py-30px px-20px lg:px-25px bg-white flex flex-col">
                                    <h3 class="text-lg lg:text-2xl font-bold font-inter">M&amp;A Marketplace</h3>
                                    <p class="text-sm lg:text-base">Are you looking to sell your IT business? Visit our M&amp;A Marketplace and get started in less than a minute.</p>
                                    <div class="btn font-bold text-sm text-blue">
                                        <span>VIEW PRODUCT</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                        </defs></svg>

                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
