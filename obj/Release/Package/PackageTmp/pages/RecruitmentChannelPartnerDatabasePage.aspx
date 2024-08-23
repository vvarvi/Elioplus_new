<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="RecruitmentChannelPartnerDatabasePage.aspx.cs" Inherits="WdS.ElioPlus.pages.RecruitmentChannelPartnerDatabasePage" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Search the biggest database of channel partners like Managed Services Providers, resellers, VARs, consultants etc. globally. Get access to full company details and employees." />
    <meta name="keywords" content="channel partner database, resellers database, managed services providers database" />

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
        <header class="bg-blue text-white shadow-lg z-10 sticky top-[63px] lg:top-[72px] xl:top-[92px] left-0 w-screen py-0 lg:py-15px">
            <div class="container">
                <div class="flex justify-between">
                    <div class="py-5px px-5px pr-[15px] hidden lg:flex items-center gap-5px bg-blueDark rounded-30px">
                        <img src="/assets_out/images/global/product-circles.svg" alt="Referral Program software product" width="30" height="30">
                        <div class="text-sm font-inter">Product:&nbsp;<strong>Partner Recruitment Database</strong></div>
                    </div>
                    <nav class="flex gap-10px xl:gap-15px relative items-center w-full lg:w-fit" id="productNavigation">
                        <div class="w-full lg:w-fit overflow-x-auto">
                            <ul class="text-sm flex gap-15px flex-wrap w-max py-15px lg:py-0">
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#overview">Overview</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#features">Features</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#solutions">Solutions</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#howItWorks">How it works</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#pricing">Pricing</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#clients">Clients</a></li>
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
                            <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Partner Recruitment Database</h1>
                            <h2 class="text-2xl lg:text-3xl font-bold font-inter text-blue">Search and recruit new channel partners</h2>
                        </div>
                        <p class="text-base lg:text-body text-center lg:text-left">
                            Partner Recruiting has never been easier. Create your prospect lists of channel partners based on your needs. Use our advanced search filters, get matched with companies that fit your criteria and access contact details to grow your network.
                        </p>
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
                            <a id="aGetStartedFreeB" runat="server" class="btn large font-bold text-sm xl:text-base bg-blue text-white">
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
                        <img class="max-w-[350px] lg:max-w-full" src="/assets_out/images/partner-recruitment/Partner-Recruitment-homepage.png" alt="Partner Recruitment Database Software">
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
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Features</h3>
                        <p class="w-full text-base lg:text-body">
                            Get the most accurate B2B contact data and connect with partners that can help you increase your indirect sales. Explore the powerful features of Elioplus Partner Recruitment Database.
                        </p>
                    </div>
                    <div class="gap-80px lg:gap-120px flex flex-col">
                        <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                            <div class="flex justify-center items-center">
                                <img class="max-w-[350px] lg:max-w-full" src="/assets_out/images/partner-recruitment/database/advanced-search.png" alt="Advanced Search with Elioplus PR database software" loading="lazy">
                            </div>
                            <div class="gap-15px lg:gap-25px flex flex-col justify-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Advanced Search</h3>
                                <p class="w-full text-base lg:text-xl text-center lg:text-left">
                                    Search our database of channel partners by using those criteria that apply to your requirements. Combine filters such as the type of solution that you are providing (CRM, ERP, BI, LMS, Cyber security and more than 100+ categories), Countries that you target or Worldide, Cities or even States, Company Size, Products that channel partners already offer (like SAP partners, Salesforce Partners, Microsoft 365 partners, Azure partners, Google Workspace partners, Oracle partners and 1,000+ vendors and products) and more.
                                </p>
                            </div>
                        </div>
                        <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                            <div class="row-start-2 lg:row-start-1 gap-15px lg:gap-25px flex flex-col justify-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Complete Data Information</h3>
                                <p class="w-full text-base lg:text-xl text-center lg:text-left">
                                    Get contact details from each company of the search results and get access to more than 30 attributes about the company and the contact person. We bring you the information of the appropriate person to get in touch like First and Last Name, Business email address, Linkedin profile and more. Your point of contact will be the responsible one to take the final decision of adding your product or not on their solutions offerings. Depending on the type of your solution, you can have access to roles like CEO, Managing Director, VP os Sales, Channel Manager, CTO, Vendor Manager, HR director and more.
                                </p>
                            </div>
                            <div class="flex justify-center items-center">
                                <img class="max-w-[350px] lg:max-w-full" src="/assets_out/images/partner-recruitment/database/accurate-valid-data.png" alt="Complete Data Insights with Elioplus PR database software" loading="lazy">
                            </div>
                        </div>
                        <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                            <div class="flex justify-center items-center">
                                <img class="max-w-[350px] lg:max-w-full" src="/assets_out/images/partner-recruitment/database/approach-channel-partners.png" alt="Contact Details with Elioplus PR database software" loading="lazy">
                            </div>
                            <div class="gap-15px lg:gap-25px flex flex-col justify-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Approach your channel partners</h3>
                                <p class="w-full text-base lg:text-xl text-center lg:text-left">
                                    By providing more than 30 contact details of the company and the appropriate person gives you the opportunity to get in touch with them through personalized outreaches. Once you become a premium member we can help you create personalized email templates that have great open and response rates. You can download a CSV file with all the information of the company and the appropriate person that you can upload on your CRM or on the Marketing Automation solution that you use.
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-gray" id="solutions">
            <div class="container">
                <div class="gap-50px lg:gap-80px flex flex-col">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Partner Recruitment Solutions</h3>
                        <p class="w-full text-base lg:text-body">
                            We provide alternative Partner Recruitment solutions based on your needs. Get started with Partner Recruitment <strong>Full Service</strong> or <strong>Automation</strong>.
                        </p>
                    </div>
                    <div class="grid-cols-1 md:grid-cols-2 grid lg:flex lg:flex-row lg:justify-center items-center gap-30px lg:gap-50px w-full">
                        <div class="w-[400px] max-w-full mx-auto bg-white shadow-card overflow-hidden rounded-10px">
                            <div class="flex items-center justify-center">
                                <img src="/assets_out/images/partner-recruitment/full-service-channel-marketing.png" alt="Full service solution with with Elioplus PR software" loading="lazy">
                            </div>
                            <div class="gap-15px lg:gap-25px py-10px lg:py-30px px-20px lg:px-25px text-center bg-white flex flex-col">
                                <h3 class="text-lg lg:text-2xl font-bold font-inter">Full Service</h3>
                                <p class="text-sm lg:text-base">Let our team do the outreach based on your lists and get introductions and meeting with prospect partners. We use the data that has been generated in your account plus additional contacts based on your subscription plan.</p>
                                <a id="aContactusAuto" runat="server" class="btn large font-bold bg-white text-xs xl:text-sm justify-center w-full text-blue" href="/contact-us"><span>CONTACT OUR SALES TEAM</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                        <div class="w-[400px] max-w-full mx-auto bg-white shadow-card overflow-hidden rounded-10px">
                            <div class="flex items-center justify-center">
                                <img src="/assets_out/images/partner-recruitment/partner-recruitment-automate.png" alt="Automation service solution with with Elioplus PR software" loading="lazy">
                            </div>
                            <div class="gap-15px lg:gap-25px py-10px lg:py-30px px-20px lg:px-25px text-center bg-white flex flex-col">
                                <h3 class="text-lg lg:text-2xl font-bold font-inter">Automation</h3>
                                <p class="text-sm lg:text-base">Hustle-free way to generate your ideal prospect lists automatically for partner recruiting. Based on your criteria, our system detects & delivers accurate and valid data which is verified by our team to create your potential partners lists.</p>
                                <a id="aContactusData" runat="server" href="/channel-partner-recruitment-automation" class="btn large font-bold bg-white text-xs xl:text-sm justify-center w-full text-blue"><span>VIEW PRODUCT</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section id="howItWorks">
            <div class="container">
                <div class="gap-50px lg:gap-80px flex flex-col">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">How it works</h3>
                        <p class="w-full text-base lg:text-body">
                            Elioplus has more than 100,000 channel partners looking for new solutions to provide. Create your company's profile, subscribe to one of our paid plans and unlock our search filters to bring inside your dashboard the number of channel partners that your package provides. For custom plans with bigger access and more contact details you can get in touch with us.
                        </p>
                    </div>
                    <div class="gap-80px lg:gap-120px flex flex-col">
                        <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                            <div class="flex justify-center items-center">
                                <img class="shadow-card rounded-10px" src="/assets_out/images/partner-recruitment/create-your-company-profile.png" alt="referral software dashboard with Elioplus Partner Recruiting PR software" loading="lazy">
                            </div>
                            <div class="gap-15px lg:gap-25px flex flex-col justify-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Create your company profile</h3>
                                <p class="w-full text-base lg:text-xl text-center lg:text-left">
                                    Sign up and create a free profile for your partner program adding all your partner program details. You can also add a demo video and a pdf file for your prospect partners to easily evaluate your offering.
                                </p>
                            </div>
                        </div>
                        <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                            <div class="row-start-2 lg:row-start-1 gap-15px lg:gap-25px flex flex-col justify-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Search our database</h3>
                                <p class="w-full text-base lg:text-xl text-center lg:text-left">
                                    Select the filters that you would like to apply in order to bring you the channel partners that you need. Use criteria such as Product Category, Countries that you target or worldwide. You can even search by cities or states, Type of Partnership, Technology category and many more. From all the search results, select those channel partners that you would like to unlock their details according to the plan that you subscribed to. Every plan gives you access to a specific number of channel partners each month and all their contact details.
                                </p>
                            </div>
                            <div class="flex justify-center items-center">
                                <img class="shadow-card rounded-10px" src="/assets_out/images/partner-recruitment/database/advanced-search.png" alt="Select your partnership criteria with Elioplus Partner Recruiting PR software" loading="lazy">
                            </div>
                        </div>
                        <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                            <div class="flex justify-center items-center">
                                <img class="shadow-card rounded-10px" src="/assets_out/images/partner-recruitment/database/approach-channel-partners.png" alt="Get matched with the best opportunities with Elioplus Partner Recruiting PR software" loading="lazy">
                            </div>
                            <div class="gap-15px lg:gap-25px flex flex-col justify-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Get in touch through personalized emails or calls</h3>
                                <p class="w-full text-base lg:text-xl text-center lg:text-left">
                                    Download a CSV file with all the data of your selected channel partners like company data and full information of the appropriate person. Create personalized emails or take phone calls, Invite other colleagues on your team to join your company's account in order to help you with the outreaches and get replies from your potential partners asking for calls, demos etc. Sign more and more resellers agreements every month through our high conversion rates.
                                </p>
                            </div>
                        </div>
                        <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                            <div class="row-start-2 lg:row-start-1 gap-15px lg:gap-25px flex flex-col justify-center">
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Manage your prospect partners</h3>
                                <p class="w-full text-base lg:text-xl text-center lg:text-left">
                                    Elioplus enables you to manage the negotiations with your potential partners in a pipeline, both those from our platform as well as any other source you may add an opportunity from. You can add tasks, notes and reminders, move the opportunities around in order to have a clear image and follow up with your partners for better efficiency.
                                </p>
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
                                <p class="w-full text-base lg:text-xl text-center lg:text-left">
                                    After you have successfully engaged into new partnerships via our platform you can seamlessly add your new partners to our free PRM software to manage, collaborate and incentivize them. You can optionally select to use our partner portal software to unify your partner recruitment and management needs.
                                </p>
                                <a class="btn font-bold text-xs xl:text-sm mt-10px text-blue" href="/contact-us"><span>READ MORE</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
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
                            <h2 class="text-2.5xl lg:text-5xl font-bold font-inter">Partner Recruitment Pricing</h2>
                            <p class="text-base lg:text-body text-center w-[900px] max-w-full">
                                Get connected and reach out to channel partners to your target markets or worldwide, to help you grow your partner network and increase your indirect sales.
                            </p>
                        </div>
                    </div>
                    <div class="grid-cols-1 sm:grid-cols-2 grid lg:flex lg:flex-row lg:justify-between items-center gap-30px lg:gap-0 w-full">
                        <div class="w-[310px] max-w-full mx-auto bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">Start-Up</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">For startups and small companies that are looking to establish a partner network.</p>
                            </div>
                            <div class="flex flex-col gap-10px w-full">
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">$238</h4>
                                <p class="w-full text-sm lg:text-sm text-center lg:text-left italic text-textGray">Per month, you can cancel anytime.</p>
                            </div>
                            <a id="aStartupData" runat="server" class="btn large font-bold text-sm bg-white border-blackBlue text-blackBlue w-full justify-center"><span>GET STARTED NOW</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                            <a id="aChekoutStartupData" runat="server" onserverclick="aChekoutStartupData_ServerClick" visible="false" class="btn large font-bold text-sm bg-white border-blackBlue text-blackBlue w-full justify-center"><span>GET STARTED NOW</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
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
                            <a id="aGrowthData" runat="server" class="btn large font-bold text-sm bg-blue text-white w-full justify-center"><span>GET STARTED NOW</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
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
                            <a id="aEnterpriseContact" runat="server" class="btn large font-bold text-sm bg-white border-blackBlue text-blackBlue w-full justify-center"><span>CONTACT US</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
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
