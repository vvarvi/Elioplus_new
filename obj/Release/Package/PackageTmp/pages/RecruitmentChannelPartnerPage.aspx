<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="RecruitmentChannelPartnerPage.aspx.cs" Inherits="WdS.ElioPlus.pages.RecruitmentChannelPartnerPage" %>

<asp:Content ID="HomeHeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Find and recruit channel partners like resellers, VARs, MSPs and consultants based on their location or industry." />
    <meta name="keywords" content="channel partner recruitment, SaaS channel partners" />

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

<asp:Content ID="HomeMainContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="transition-all duration-200">
        <header class="bg-blue text-white shadow-lg z-10 sticky top-[63px] lg:top-[72px] xl:top-[92px] left-0 w-screen py-0 lg:py-15px">
            <div class="container">
                <div class="flex justify-between">
                    <div class="py-5px px-5px pr-[15px] hidden lg:flex items-center gap-5px bg-blueDark rounded-30px">
                        <img src="/assets_out/images/global/product-circles.svg" alt="Referral Program software product" width="30" height="30">
                        <div class="text-sm font-inter">Product:&nbsp;<strong>Partner Recruitment</strong></div>
                    </div>
                    <nav class="flex gap-10px xl:gap-15px relative items-center w-full lg:w-fit" id="productNavigation">
                        <div class="w-full lg:w-fit overflow-x-auto">
                            <ul class="text-sm flex gap-15px flex-wrap w-max py-15px lg:py-0">
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#overview">Overview</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#services">Services</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#howItWorks">How it works</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#channelMarketing">Channel Marketing</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#pricing">Pricing</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#clients">Our Clients</a></li>
                            </ul>
                        </div>
                        <a id="aGetStartedFree" runat="server" class="btn hidden lg:flex font-bold text-sm bg-yellow text-white w-fit"><span>GET STARTED</span></a>
                    </nav>
                </div>
            </div>
        </header>
        <section class="bg-gradient-product opener" id="overview">
            <div class="container">
                <div class="grid-cols-1 lg:grid-cols-2 grid gap-30px">
                    <div class="row-start-2 lg:row-start-1 gap-20px lg:gap-30px flex flex-col justify-center">
                        <div class="text-center lg:text-left gap-10px lg:gap-15px flex flex-col">
                            <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Channel Partner Recruitment</h1>
                            <h2 class="text-2xl lg:text-3xl font-bold font-inter text-blue">Engage and Onboard new Channel Partners</h2>
                        </div>
                        <p class="text-base lg:text-body text-center lg:text-left">Get connected and reach out to channel partners (like resellers, Value Added Resellers, Managed Service Providers - MSPs,  system integrators, consultants etc.) to your target markets or worldwide, to help you grow your partner network and increase your indirect sales.</p>
                        <div class="flex items-center gap-10px xl:gap-15px flex-col lg:flex-row">
                            <a target="_blank" class="btn large font-bold text-sm xl:text-base" href="https://calendly.com/elioplus"><span>
                                <asp:Label ID="Label7" Text="Schedule demo" runat="server" />
                            </span>
                                <svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <g clip-path="url(#clip0_75_23830)">
                                        <path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" />
                                    </g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                    </defs></svg>
                            </a>
                            <a id="aGetStartedFreeB" runat="server" class="btn large font-bold text-sm xl:text-base bg-blue text-white" href="/free-sign-up">
                                <svg width="18" height="19" viewBox="0 0 18 19" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <path d="M8.95734 5.12856C10.2416 5.12856 11.2826 4.09242 11.2826 2.81428C11.2826 1.53614 10.2416 0.5 8.95734 0.5C7.67314 0.5 6.63208 1.53614 6.63208 2.81428C6.63208 4.09242 7.67314 5.12856 8.95734 5.12856Z" fill="#39B4EF" />
                                    <path d="M8.95734 11.8141C10.2416 11.8141 11.2826 10.778 11.2826 9.49983C11.2826 8.22169 10.2416 7.18555 8.95734 7.18555C7.67314 7.18555 6.63208 8.22169 6.63208 9.49983C6.63208 10.778 7.67314 11.8141 8.95734 11.8141Z" fill="#39B4EF" />
                                    <path d="M8.95734 18.5001C10.2416 18.5001 11.2826 17.464 11.2826 16.1859C11.2826 14.9077 10.2416 13.8716 8.95734 13.8716C7.67314 13.8716 6.63208 14.9077 6.63208 16.1859C6.63208 17.464 7.67314 18.5001 8.95734 18.5001Z" fill="#39B4EF" />
                                    <path d="M2.75666 11.8141C4.04087 11.8141 5.08192 10.778 5.08192 9.49983C5.08192 8.22169 4.04087 7.18555 2.75666 7.18555C1.47245 7.18555 0.431396 8.22169 0.431396 9.49983C0.431396 10.778 1.47245 11.8141 2.75666 11.8141Z" fill="#39B4EF" />
                                    <path d="M15.6746 11.8141C16.9589 11.8141 17.9999 10.778 17.9999 9.49983C17.9999 8.22169 16.9589 7.18555 15.6746 7.18555C14.3904 7.18555 13.3494 8.22169 13.3494 9.49983C13.3494 10.778 14.3904 11.8141 15.6746 11.8141Z" fill="#39B4EF" />
                                </svg>
                                <span>GET STARTED FOR FREE</span></a>
                        </div>
                    </div>
                    <div class="flex justify-center items-center">
                        <img class="max-w-[350px] lg:max-w-full" src="/assets_out/images/partner-recruitment/Partner-Recruitment-homepage.png" alt="Partner Recruitment Software">
                    </div>
                </div>
            </div>
            <div class="mt-30px wave">
                <img src="/assets_out/images/global/wave.svg" alt="Elioplus decorative wave">
            </div>
        </section>
        <section id="services">
            <div class="container">
                <div class="gap-50px lg:gap-80px flex flex-col">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Partner Recruitment Services</h3>
                        <p class="w-full text-base lg:text-body">
                            Elioplus offers 2 main services to get access to our B2B database to identify, connect with, and close more customers.
                Get started for free with Partner Recruitment <strong>Database</strong> or <strong>Automation</strong> and get insights & data based on your criteria.
                        </p>
                    </div>
                    <div class="grid-cols-1 md:grid-cols-2 grid lg:flex lg:flex-row lg:justify-center items-center gap-30px lg:gap-50px w-full">
                        <div class="w-[400px] max-w-full mx-auto bg-white shadow-card overflow-hidden rounded-10px">
                            <div class="flex items-center justify-center">
                                <img src="/assets_out/images/partner-recruitment/partner-recruitment-database.png" alt="Partner Recruitment - Database">
                            </div>
                            <div class="gap-15px lg:gap-25px py-10px lg:py-30px px-20px lg:px-25px text-center bg-white flex flex-col">
                                <h3 class="text-lg lg:text-2xl font-bold font-inter">Partner Recruitment - Database</h3>
                                <div class="flex flex-col gap-10px w-full">
                                    <div class="flex items-start gap-5px text-left w-full">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="check icon" loading="lazy">
                                        </div>
                                        <div class="text-[15px]">Advance search criteria and accurate data to source local partners.</div>
                                    </div>
                                    <div class="flex items-start gap-5px text-left w-full">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="check icon" loading="lazy">
                                        </div>
                                        <div class="text-[15px]">Locate companies based on their expertise, partnerships and location</div>
                                    </div>
                                    <div class="flex items-start gap-5px text-left w-full">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="check icon" loading="lazy">
                                        </div>
                                        <div class="text-[15px]">Use company type filters to discover specific segments, like Managed Service Providers</div>
                                    </div>
                                    <div class="flex items-start gap-5px text-left w-full">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="check icon" loading="lazy">
                                        </div>
                                        <div class="text-[15px]">View complete insights, like website, industry, revenues and many more attributions</div>
                                    </div>
                                    <div class="flex items-start gap-5px text-left w-full">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="check icon" loading="lazy">
                                        </div>
                                        <div class="text-[15px]">Get C-level contact details</div>
                                    </div>
                                </div>
                                <a class="btn large font-bold bg-white text-xs xl:text-sm justify-center w-full text-blue" href="/channel-partner-recruitment-database"><span>VIEW PRODUCT</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                        <div class="w-[400px] max-w-full mx-auto bg-white shadow-card overflow-hidden rounded-10px">
                            <div class="flex items-center justify-center">
                                <img src="/assets_out/images/partner-recruitment/partner-recruitment-automate.png" alt="Partner Recruitment - automation">
                            </div>
                            <div class="gap-15px lg:gap-25px py-10px lg:py-30px px-20px lg:px-25px text-center bg-white flex flex-col">
                                <h3 class="text-lg lg:text-2xl font-bold font-inter">Partner Recruitment - Automation</h3>
                                <div class="flex flex-col gap-10px w-full">
                                    <div class="flex items-start gap-5px text-left w-full">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="check icon" loading="lazy">
                                        </div>
                                        <div class="text-[15px]">Set your search criteria and our system will deliver prospect partners that 100% fit your needs.</div>
                                    </div>
                                    <div class="flex items-start gap-5px text-left w-full">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="check icon" loading="lazy">
                                        </div>
                                        <div class="text-[15px]">All data are human verified by our team to gurantee accuracy and validity.</div>
                                    </div>
                                    <div class="flex items-start gap-5px text-left w-full">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="check icon" loading="lazy">
                                        </div>
                                        <div class="text-[15px]">Your prospect lists will be completed automatically.</div>
                                    </div>
                                    <div class="flex items-start gap-5px text-left w-full">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="check icon" loading="lazy">
                                        </div>
                                        <div class="text-[15px]">View all company insights and C-level contact details.</div>
                                    </div>
                                </div>
                                <a class="btn large font-bold bg-white text-xs xl:text-sm justify-center w-full text-blue" href="/channel-partner-recruitment-automation"><span>VIEW PRODUCT</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Full-Service Partner Recruitment</h3>
                        <p class="w-full text-base lg:text-body">Let our team do the outreach based on your lists and get introductions and meeting with prospect partners. We use the data that has been generated in your account plus additional contacts based on your subscription plan.</p>
                        <div class="flex items-center justify-center mt-20px">
                            <a class="btn large font-bold bg-white text-sm xl:text-base" href="/contact-us"><span>CONTACT OUR SALES TEAM</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-gray" id="howItWorks">
            <div class="container">
                <div class="gap-50px lg:gap-80px flex flex-col">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">How it works</h3>
                        <p class="w-full text-base lg:text-body">Elioplus PRM provides features that can increase your channel sales. Avoid a ton of features that make your life difficult. Through Elioplus PRM you are focusing on your channel partner's productivity and needs through collaboration and incentivization.</p>
                    </div>
                    <div class="gap-80px lg:gap-120px flex flex-col">
                        <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                            <div class="flex justify-center items-center">
                                <img class="shadow-card rounded-10px" src="/assets_out/images/partner-recruitment/Create-your-company-profile.png" alt="referral software dashboard with Elioplus Partner Recruiting PR software" loading="lazy">
                            </div>
                            <div class="gap-15px lg:gap-25px flex flex-col justify-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Create your company profile</h3>
                                <p class="w-full text-base lg:text-xl text-center lg:text-left">Sign up and create a free profile for your partner program adding all your partner program details. You can also add a demo video and a pdf file for your prospect partners to easily evaluate your offering. Your partners can rate and review your partner program to help you rank higher on relevant searches attract new channel partners.</p>
                            </div>
                        </div>
                        <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                            <div class="row-start-2 lg:row-start-1 gap-15px lg:gap-25px flex flex-col justify-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Select your partnership criteria</h3>
                                <p class="w-full text-base lg:text-xl text-center lg:text-left">Select the product categories you want to recruit new partners, your locations (our platform enables global coverage), type of partnership, industry and from 12 more optional criteria that are available. If you have specialized needs then you can inform your account manager and we’ll apply them, nothing is too extreme for us.</p>
                            </div>
                            <div class="flex justify-center items-center">
                                <img class="shadow-card rounded-10px" src="/assets_out/images/partner-recruitment/Select-your-partnership-criteria.png" alt="Select your partnership criteria with Elioplus Partner Recruiting PR software" loading="lazy">
                            </div>
                        </div>
                        <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                            <div class="flex justify-center items-center">
                                <img class="shadow-card rounded-10px" src="/assets_out/images/partner-recruitment/get-matched-opportunities.png" alt="Get matched with the best opportunities with Elioplus Partner Recruiting PR software" loading="lazy">
                            </div>
                            <div class="gap-15px lg:gap-25px flex flex-col justify-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Get matched with the best opportunities</h3>
                                <p class="w-full text-base lg:text-xl text-center lg:text-left">Once you have completed the registration we provide you with a number of channel partners that exact match your selected criteria. We start providing you the best matches directly in your dashboard along with contact details from each company and more than 30 attributes about the company and the contact person. You can add each potential partner to our pipeline tool or export your data in a CSV file.</p>
                            </div>
                        </div>
                        <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                            <div class="row-start-2 lg:row-start-1 gap-15px lg:gap-25px flex flex-col justify-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Manage your prospect partners</h3>
                                <p class="w-full text-base lg:text-xl text-center lg:text-left">Elioplus enables you to manage the negotiations with your potential partners in a pipeline, both those from our platform as well as you can add an opportunity from any other source. You can add tasks, notes and reminders, move the opportunities around in order to have a clear image and follow up with your partners for better efficiency.</p>
                            </div>
                            <div class="flex justify-center items-center">
                                <img class="shadow-card rounded-10px" src="/assets_out/images/partner-recruitment/manage-prospect-partners.png" alt="Manage your prospect partners with Elioplus Partner Recruiting PR software" loading="lazy">
                            </div>
                        </div>
                        <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                            <div class="flex justify-center items-center">
                                <img class="shadow-card rounded-10px" src="/assets_out/images/partner-recruitment/PRM-Integration.png" alt="PRM integration with Elioplus Partner Recruiting PR software" loading="lazy">
                            </div>
                            <div class="gap-15px lg:gap-25px flex flex-col justify-center items-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">PRM Integration</h3>
                                <p class="w-full text-base lg:text-xl text-center lg:text-left">After you have successfully engaged into new partnerships via our platform you can seamlessly add your new partners to our free PRM software to manage, collaborate and incentivize them. You can optionally select to use our partner portal software to unify your partner recruitment and management needs.</p>
                                <a class="btn font-bold text-xs xl:text-sm mt-10px text-blue" href="/prm-software/features"><span>READ MORE</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-white" id="channelMarketing">
            <div class="container">
                <div class="grid-cols-1 lg:grid-cols-7-5 text-img-row grid gap-30px">
                    <div class="row-start-2 lg:row-start-1 gap-20px lg:gap-30px flex flex-col justify-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Full-service channel marketing</h3>
                        <p class="w-full text-base lg:text-lg text-center lg:text-left">If you have limited resources or time to reach out to potential partners we offer the option to hire a dedicated account manager from our company to do all the necessary follow ups to your matches that you receive through Elioplus.</p>
                        <p class="w-full text-base lg:text-lg text-center lg:text-left">So, we are doing all the hard work of follow ups through emails to all your matches and we provide you only qualified partnership opportunities like channel partner sign ups, demo requests, call requests, more information requests in order to convert them as channel partners of your solutions.</p>
                    </div>
                    <div class="flex items-center justify-center pl-30px">
                        <img src="/assets_out/images/partner-recruitment/full-service-channel-marketing.png">
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-gray" id="pricing">
            <div class="container">
                <div class="gap-40px lg:gap-60px flex flex-col justify-center items-center w-full">
                    <div class="gap-20px lg:gap-30px flex flex-col justify-center items-center w-full">
                        <div class="text-center gap-5px lg:gap-10px flex flex-col items-center w-full">
                            <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Partner Recruitment Pricing</h1>
                            <p class="text-base lg:text-body text-center w-[900px] max-w-full">Get connected and reach out to channel partners to your target markets or worldwide, to help you grow your partner network and increase your indirect sales.</p>
                        </div>
                    </div>
                    <div class="w-full lg:w-[600px] flex gap-0 items-center text-center" id="tabs"><a class="active tab-item w-2/4 text-base lg:text-lg font-inter font-medium text-textGray" href="#database">DATABASE</a><a class="w-2/4 tab-item text-base lg:text-lg font-inter font-medium text-textGray" href="#automate">AUTOMATION</a></div>
                    <div class="tab-content grid-cols-1 sm:grid-cols-2 grid lg:flex lg:flex-row lg:justify-between items-center gap-30px lg:gap-20px w-full" id="database">
                        <div class="w-[310px] max-w-full mx-auto bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">Start-Up</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">For startups and small companies that are looking to establish a partner network.</p>
                            </div>
                            <div class="flex flex-col gap-10px w-full">
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">$238</h4>
                                <p class="w-full text-sm lg:text-sm text-center lg:text-left italic text-textGray">Per month, you can cancel anytime.</p>
                            </div>
                            <a id="aStartupData" runat="server" class="btn large font-bold text-sm bg-white border-blackBlue text-blackBlue w-full justify-center"><span>
                                <asp:Label ID="LblStartupData" runat="server" Text="GET STARTED NOW" />
                            </span>
                                <svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <g clip-path="url(#clip0_75_23830)">
                                        <path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" />
                                    </g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                    </defs></svg>
                            </a>
                            <a id="aChekoutStartupData" onserverclick="aChekoutStartupData_ServerClick" visible="false" runat="server" class="btn large font-bold text-sm bg-white border-blackBlue text-blackBlue w-full justify-center"><span>
                                <asp:Label ID="LblCkStartupData" runat="server" Text="UPGRADE NOW" />
                            </span>
                                <svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <g clip-path="url(#clip0_75_23830)">
                                        <path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" />
                                    </g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                    </defs></svg>
                            </a>
                            <div class="flex flex-col gap-15px w-full">
                                <h5 class="text-base font-inter font-semibold">Features:</h5>
                                <div class="flex flex-col gap-5px w-full">
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Create a company profile" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Create a company profile</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Featured Profile" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Featured Profile</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for 100 Channel Partner Profiles" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">100 Channel Partner Profiles</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Advanced Searches" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Advanced Searches</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="w-[310px] max-w-full mx-auto bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left text-blue">Growth</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">For companies with a mature partner program that are looking to expand their partner network.</p>
                            </div>
                            <div class="flex flex-col gap-10px w-full">
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">$398</h4>
                                <p class="w-full text-sm lg:text-sm text-center lg:text-left italic text-textGray">Per month, you can cancel anytime.</p>
                            </div>
                            <a id="aGrowthData" runat="server" class="btn large font-bold text-sm bg-blue text-white w-full justify-center"><span>
                                <asp:Label ID="LblGrowthData" runat="server" Text="GET STARTED NOW" /></span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                            </a>
                            <a id="aChekoutGrowthData" onserverclick="aChekoutGrowthData_ServerClick" runat="server" visible="false" class="btn large font-bold text-sm bg-blue text-white w-full justify-center"><span>
                                <asp:Label ID="LblCkGrowthData" runat="server" Text="UPGRADE NOW" /></span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                            </a>
                            <div class="flex flex-col gap-15px w-full">
                                <h5 class="text-base font-inter font-semibold">Features:</h5>
                                <div class="flex flex-col gap-5px w-full">
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Create a company profile" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Create a company profile</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Featured Profile" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Featured Profile</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for 200 Channel Partner Profiles" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">200 Channel Partner Profiles</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for All Search Criteria" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">All Search Criteria</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="w-[310px] max-w-full mx-auto bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">Enterprise</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">For companies that are looking to grow their indirect sales worldwide.</p>
                            </div>
                            <div class="flex flex-col gap-10px w-full">
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">$900+</h4>
                                <p class="w-full text-sm lg:text-sm text-center lg:text-left italic text-textGray">Custom pricing, starting from.</p>
                            </div>
                            <a id="aEnterpriseData" runat="server" class="btn large font-bold text-sm bg-white border-blackBlue text-blackBlue w-full justify-center"><span>CONTACT US</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                            <div class="flex flex-col gap-15px w-full">
                                <h5 class="text-base font-inter font-semibold">Features:</h5>
                                <div class="flex flex-col gap-5px w-full">
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Create a company profile" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Create a company profile</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Featured Profile" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Featured Profile</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Custom Matches" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Custom Matches</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Custom Search Criteria" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Custom Search Criteria</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-content grid-cols-1 sm:grid-cols-2 grid hidden lg:flex lg:flex-row lg:justify-between items-center gap-30px lg:gap-20px w-full" id="automate">
                        <div class="w-[310px] max-w-full mx-auto bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">Free</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">Get started with a free company profile to get familiar with our solution.</p>
                            </div>
                            <div class="flex flex-col gap-10px w-full">
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">$0</h4>
                                <p class="w-full text-sm lg:text-sm text-center lg:text-left italic text-textGray">Upgrade to a premium plan anytime.</p>
                            </div>
                            <a id="aFreeAuto" runat="server" class="btn large font-bold text-sm bg-white border-blackBlue text-blackBlue w-full justify-center"><span>START FOR FREE</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                            <div class="flex flex-col gap-15px w-full">
                                <h5 class="text-base font-inter font-semibold">Features:</h5>
                                <div class="flex flex-col gap-5px w-full">
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Create a company profile" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Create a company profile</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/close.svg" alt="Not included feature for Featured Profile" loading="lazy" />
                                        </div>
                                        <div class="text-[15px] text-textGray">Featured Profile</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/close.svg" alt="Not included feature for Matches" loading="lazy" />
                                        </div>
                                        <div class="text-[15px] text-textGray">Matches</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/close.svg" alt="Not included feature for Direct Messages to Channel Partners" loading="lazy" />
                                        </div>
                                        <div class="text-[15px] text-textGray">Direct Messages</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/close.svg" alt="Not included feature for Advanced Searches" loading="lazy" />
                                        </div>
                                        <div class="text-[15px] text-textGray">Advanced Searches</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="w-[310px] max-w-full mx-auto bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">Start-Up</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">For startups and small companies that are looking to establish a partner network.</p>
                            </div>
                            <div class="flex flex-col gap-10px w-full">
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">$298</h4>
                                <p class="w-full text-sm lg:text-sm text-center lg:text-left italic text-textGray">Per month, you can cancel anytime.</p>
                            </div>
                            <a id="aStartupAuto" runat="server" class="btn large font-bold text-sm bg-white border-blackBlue text-blackBlue w-full justify-center"><span>
                                <asp:Label ID="LblStartupAuto" runat="server" Text="GET STARTED NOW" />
                            </span>
                                <svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <g clip-path="url(#clip0_75_23830)">
                                        <path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" />
                                    </g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                    </defs></svg>
                            </a>
                            <a id="aChekoutStartupAuto" runat="server" onserverclick="aChekoutStartupAuto_ServerClick" visible="false" class="btn large font-bold text-sm bg-blue text-white w-full justify-center"><span>
                                <asp:Label ID="LblCkStartupAuto" runat="server" Text="UPGRADE NOW" />
                            </span>
                                <svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <g clip-path="url(#clip0_75_23830)">
                                        <path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" />
                                    </g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                    </defs></svg>
                            </a>
                            <div class="flex flex-col gap-15px w-full">
                                <h5 class="text-base font-inter font-semibold">Features:</h5>
                                <div class="flex flex-col gap-5px w-full">
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Create a company profile" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Create a company profile</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Featured Profile" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Featured Profile</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for 70 Matches" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">70 Matches</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Advanced Searches" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Advanced Searches</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="w-[310px] max-w-full mx-auto bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left text-blue">Growth</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">For companies with a mature partner program that are looking to expand their partner network.</p>
                            </div>
                            <div class="flex flex-col gap-10px w-full">
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">$498</h4>
                                <p class="w-full text-sm lg:text-sm text-center lg:text-left italic text-textGray">Per month, you can cancel anytime.</p>
                            </div>
                            <a id="aGrowthAuto" runat="server" class="btn large font-bold text-sm bg-blue text-white w-full justify-center"><span>
                                <asp:Label ID="LblGrowthAuto" runat="server" Text="GET STARTED NOW" />
                            </span>
                                <svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <g clip-path="url(#clip0_75_23830)">
                                        <path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" />
                                    </g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                    </defs></svg>
                            </a>
                            <a id="aChekoutGrowthAuto" runat="server" onserverclick="aChekoutGrowthAuto_ServerClick" visible="false" class="btn large font-bold text-sm bg-blue text-white w-full justify-center"><span>
                                <asp:Label ID="LblCkGrowthAuto" runat="server" Text="UPGRADE NOW" />
                            </span>
                                <svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <g clip-path="url(#clip0_75_23830)">
                                        <path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" />
                                    </g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                    </defs></svg>
                            </a>
                            <div class="flex flex-col gap-15px w-full">
                                <h5 class="text-base font-inter font-semibold">Features:</h5>
                                <div class="flex flex-col gap-5px w-full">
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Create a company profile" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Create a company profile</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Featured Profile" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Featured Profile</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for 150 Matches" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">150 Matches</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Advanced Searches" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Advanced Searches</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="w-[310px] max-w-full mx-auto bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">Enterprise</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">For companies that are looking to grow their indirect sales worldwide.</p>
                            </div>
                            <div class="flex flex-col gap-10px w-full">
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">$1200+</h4>
                                <p class="w-full text-sm lg:text-sm text-center lg:text-left italic text-textGray">Custom pricing, starting from.</p>
                            </div>
                            <a id="aEnterpriseAuto" runat="server" class="btn large font-bold text-sm bg-white border-blackBlue text-blackBlue w-full justify-center"><span>CONTACT US</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                            <div class="flex flex-col gap-15px w-full">
                                <h5 class="text-base font-inter font-semibold">Features:</h5>
                                <div class="flex flex-col gap-5px w-full">
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Create a company profile" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Create a company profile</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Featured Profile" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Featured Profile</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Custom Matches" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Custom Matches</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Advanced Searches" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Advanced Searches</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-white" id="clients">
            <div class="container">
                <div class="flex flex-col gap-40px">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Our Clients</h3>
                        <p class="w-full text-base lg:text-body">Leading IT companies trust Elioplus and use Partner Recruitment to grow their sales pipeline.</p>
                    </div>
                    <div class="m-carousel flex sm:grid sm:grid-cols-3 lg:flex gap-30px justify-center" data-items="1">
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <img src="/assets_out/images/intent-signals/acronis.png" alt="acronis logo" loading="lazy">
                            </div>
                        </div>
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <img src="/assets_out/images/intent-signals/cloudit.png" alt="cloudit logo" loading="lazy">
                            </div>
                        </div>
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <img src="/assets_out/images/intent-signals/nttdata.png" alt="nttdata logo" loading="lazy">
                            </div>
                        </div>
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <img src="/assets_out/images/intent-signals/massivegrid.png" alt="massivegrid logo" loading="lazy">
                            </div>
                        </div>
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <img src="/assets_out/images/intent-signals/marketman.png" alt="marketman logo" loading="lazy">
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
