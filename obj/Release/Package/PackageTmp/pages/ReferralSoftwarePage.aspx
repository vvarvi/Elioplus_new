<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="ReferralSoftwarePage.aspx.cs" Inherits="WdS.ElioPlus.pages.ReferralSoftwarePage" %>

<%@ Register Src="~/Controls/AlertControls/MessageAlertControl.ascx" TagName="MessageAlertControl" TagPrefix="controls" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Referral software to scale and automate your referral partner program and generate more leads. Manage your referral partners and payouts, easily integrate your CRM." />
    <meta name="keywords" content="referral software, referral partner program management, referral partner program system, referral partner program software" />

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
                        <div class="text-sm font-inter">Product:&nbsp;<strong>Referral Software</strong></div>
                    </div>
                    <nav class="flex gap-10px xl:gap-15px relative items-center w-full lg:w-fit" id="productNavigation">
                        <div class="w-full lg:w-fit overflow-x-auto">
                            <ul class="text-sm flex gap-15px flex-wrap w-max py-15px lg:py-0">
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#overview">Overview</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#services">Services</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#tools">Tools</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#features">Features</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#integrations">Integrations</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#pricing">Pricing</a></li>
                            </ul>
                        </div>
                        <a target="_blank" class="btn hidden lg:flex font-bold text-sm bg-yellow text-white w-fit" href="https://calendly.com/elioplus"><span>GET A DEMO</span></a>
                    </nav>
                </div>
            </div>
        </header>
        <section class="bg-gradient-product opener" id="overview">
            <div class="container">
                <div class="grid-cols-1 lg:grid-cols-2 grid gap-30px">
                    <div class="row-start-2 lg:row-start-1 gap-20px lg:gap-30px flex flex-col justify-center">
                        <div class="text-center lg:text-left gap-10px lg:gap-15px flex flex-col">
                            <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Unlock New Referrals with a Referral Program Software.</h1>
                            <h2 class="text-2xl lg:text-3xl font-bold font-inter text-blue">Dedicated to IT & Sass Companies</h2>
                        </div>
                        <p class="text-base lg:text-body text-center lg:text-left">Make it easy for your referral partners and customers to refer new business. Incentivize your referral partners and keep them active through smart notifications.</p>
                        <div class="flex items-center gap-10px xl:gap-15px flex-col lg:flex-row">
                           <%-- <div class="grow pl-15px rounded-30px outline-none"></div>--%>
                            <a id="aSubmitTop" target="_blank" href="/referral-software-get-access" runat="server" class="btn large font-bold text-xs xl:text-sm bg-blue text-white">
                                <span>GET ACCESS</span>
                            </a>
                        </div>
                    </div>
                    <div class="flex justify-center items-center">
                        <img class="shadow-card rounded-10px" src="/assets_out/images/referral-software/referral-homepage.png" alt="Unlock New Referrals with a Referral Program Software">
                    </div>
                </div>
                <div class="flex gap-10px w-full lg:w-[500px] max-w-full pr-5px pb-5px pl-5px">
                    <controls:MessageAlertControl ID="UcMessageAlertEmailTop" runat="server" />
                </div>
            </div>
            <div class="mt-30px wave">
                <img src="/assets_out/images/global/wave.svg" alt="Elioplus decorative wave">
            </div>
        </section>
        <section id="services">
            <div class="container">
                <div class="gap-50px lg:gap-80px flex flex-col">

                    <div class="flex flex-col mdl:flex-row gap-30px mdl:gap-10px justify-start mdl:justify-between items-center">
                        <div class="w-[270px] lg:w-[380px]  max-w-full bg-white shadow-card overflow-hidden rounded-10px">
                            <div class="h-[250px] flex items-center justify-center">
                                <img src="/assets_out/images/referral-software/whosfor.svg" alt="Referral Software Services Who it’s for" loading="lazy">
                            </div>
                            <div class="gap-10px lg:gap-15px py-10px lg:py-30px px-20px lg:px-25px text-center bg-white flex flex-col">
                                <h3 class="text-lg lg:text-2xl font-bold font-inter">Who it’s for</h3>
                                <p class="text-sm lg:text-base">IT companies, software, cloud and SaaS that work with referral partners to drive new business or offer rewards to customers if they make referrals.</p>
                            </div>
                        </div>
                        <div class="w-[270px] lg:w-[380px]  max-w-full bg-white shadow-card overflow-hidden rounded-10px">
                            <div class="h-[250px] flex items-center justify-center">
                                <img src="/assets_out/images/referral-software/rewards.svg" alt="Automate rewards with Elioplus Referral Software Service" loading="lazy">
                            </div>
                            <div class="gap-10px lg:gap-15px py-10px lg:py-30px px-20px lg:px-25px text-center bg-white flex flex-col">
                                <h3 class="text-lg lg:text-2xl font-bold font-inter">Automate rewards</h3>
                                <p class="text-sm lg:text-base">Work directly from your CRM. When you successfully close a lead on your CRM then your referral partners will be automatically rewarded for their leads.</p>
                            </div>
                        </div>
                        <div class="w-[270px] lg:w-[380px]  max-w-full bg-white shadow-card overflow-hidden rounded-10px">
                            <div class="h-[250px] flex items-center justify-center">
                                <img src="/assets_out/images/referral-software/integration.svg" alt="Easy Integration of your CRM with Elioplus Referral Software" loading="lazy">
                            </div>
                            <div class="gap-10px lg:gap-15px py-10px lg:py-30px px-20px lg:px-25px text-center bg-white flex flex-col">
                                <h3 class="text-lg lg:text-2xl font-bold font-inter">Easy Integration</h3>
                                <p class="text-sm lg:text-base">Connect your CRM with our one step integration to transfer all lead submissions directly to your CRM. We connect with Hubspot, Salesforce and all major CRMs.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-gray" id="tools">
            <div class="container">
                <div class="gap-50px lg:gap-80px flex flex-col">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Automate your referral program tasks</h3>
                        <p class="w-full text-base lg:text-body">We provide you with powerful tools to cover all your requirements to create a successful referral program, from management and analytics to automated payouts.</p>
                    </div>
                    <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                        <div class="flex justify-center items-center">
                            <img class="shadow-card rounded-10px" src="/assets_out/images/referral-software/dashboard.png" alt="Campaign Dashboard tool at Elioplus Referral Software" loading="lazy">
                        </div>
                        <div class="gap-15px lg:gap-25px flex flex-col justify-center">
                            <div class="gap-5px lg:gap-10px flex flex-col justify-center">
                                <h4 class="text-sm lg:text-base font-semibold font-inter text-center lg:text-left text-blue">ANALYTICS</h4>
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Campaign Dashboard</h3>
                            </div>
                            <p class="w-full text-base lg:text-body text-center lg:text-left">Keep track of all your new referrals, see your top referral partners and your lead success rate. Get a full view for your referral partner program’s performance and how to optimize it.</p>
                        </div>
                    </div>
                    <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                        <div class="row-start-2 lg:row-start-1 gap-15px lg:gap-25px flex flex-col justify-center">
                            <div class="gap-5px lg:gap-10px flex flex-col justify-center">
                                <h4 class="text-sm lg:text-base font-semibold font-inter text-center lg:text-left text-blue">ACTIVITY</h4>
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">View your referral partners’ activity</h3>
                            </div>
                            <p class="w-full text-base lg:text-body text-center lg:text-left">Your partner directory helps you identify your top performers to boost their activity and help you underperforming referrers. View each referral partner’s performance, activity and payouts.</p>
                        </div>
                        <div class="flex justify-center items-center">
                            <img class="shadow-card rounded-10px" src="/assets_out/images/referral-software/activity.png" alt="View your referral partners’ activity tool at Elioplus Referral Software" loading="lazy">
                        </div>
                    </div>
                    <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                        <div class="flex justify-center items-center">
                            <img class="shadow-card rounded-10px" src="/assets_out/images/referral-software/activation.png" alt="Referral partner activation at Elioplus Referral Software" loading="lazy">
                        </div>
                        <div class="gap-15px lg:gap-25px flex flex-col justify-center">
                            <div class="gap-5px lg:gap-10px flex flex-col justify-center">
                                <h4 class="text-sm lg:text-base font-semibold font-inter text-center lg:text-left text-blue">AUTOMATION</h4>
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Referral partner activation</h3>
                            </div>
                            <p class="w-full text-base lg:text-body text-center lg:text-left">Our system identifies inactivity on your partners’ side and through notifications tries to engage them again. You can also set up your custom notifications.</p>
                        </div>
                    </div>
                    <div class="grid-cols-1 lg:grid-cols-2 text-img-row gap-30px lg:gap-50px grid">
                        <div class="row-start-2 lg:row-start-1 gap-15px lg:gap-25px flex flex-col justify-center">
                            <div class="gap-5px lg:gap-10px flex flex-col justify-center">
                                <h4 class="text-sm lg:text-base font-semibold font-inter text-center lg:text-left text-blue">PAYOUTS</h4>
                                <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Flexible reward structure</h3>
                            </div>
                            <p class="w-full text-base lg:text-body text-center lg:text-left">You can set up your commission structure, e.g. percentage from won leads, a set amount $ per lead, gift card etc. and manage your payouts to your referral partners.</p>
                        </div>
                        <div class="flex justify-center items-center">
                            <img class="shadow-card rounded-10px" src="/assets_out/images/referral-software/dashboard.png" alt="Flexible reward structure at Elioplus Referral Software" loading="lazy">
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-blue bg-cover" id="features">
            <div class="container">
                <div class="gap-30px lg:gap-50px flex flex-col">
                    <div class="flex flex-col gap-10px text-center items-center text-white">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Referral Software Features</h3>
                        <p class="w-full text-base lg:text-body">All the functionality you need to help you automate your referral program and increase your lead flow.</p>
                    </div>
                    <div class="grid-cols-1 sm:grid-cols-2  md:grid-cols-3 gap-30px mdl:gap-30px lg:gap-50px text-center grid">
                        <div class="w-[320px] lg:w-[400px] max-w-full mx-auto py-15px lg:py-30px px-25px lg:px-50px bg-white rounded-10px shadow-card flex flex-col gap-15px items-center">
                            <div class="flex items-center justify-center">
                                <img src="/assets_out/images/referral-software/icons/branded.png" alt="Branded referral software" loading="lazy">
                            </div>
                            <div class="flex flex-col gap-10px">
                                <h4 class="text-base lg:text-lg font-bold">Branded referral software</h4>
                                <p class="text-sm lg:text-base">A branded referral portal to manage, scale and automate your referrals.</p>
                            </div>
                        </div>
                        <div class="w-[320px] lg:w-[400px] max-w-full mx-auto py-15px lg:py-30px px-25px lg:px-50px bg-white rounded-10px shadow-card flex flex-col gap-15px items-center">
                            <div class="flex items-center justify-center">
                                <img src="/assets_out/images/referral-software/icons/marketing-resources.png" alt="Provide marketing resources Elioplus Referral Software feature" loading="lazy">
                            </div>
                            <div class="flex flex-col gap-10px">
                                <h4 class="text-base lg:text-lg font-bold">Provide marketing resources</h4>
                                <p class="text-sm lg:text-base">Your content library to provide marketing, sales and other material for your partners</p>
                            </div>
                        </div>
                        <div class="w-[320px] lg:w-[400px] max-w-full mx-auto py-15px lg:py-30px px-25px lg:px-50px bg-white rounded-10px shadow-card flex flex-col gap-15px items-center">
                            <div class="flex items-center justify-center">
                                <img src="/assets_out/images/referral-software/icons/engage.png" alt="Elioplus Referral Software feature" loading="lazy">
                            </div>
                            <div class="flex flex-col gap-10px">
                                <h4 class="text-base lg:text-lg font-bold">Engage more referral partners</h4>
                                <p class="text-sm lg:text-base">Get access to the Elioplus platform to attract more referral partners and grow your network</p>
                            </div>
                        </div>
                        <div class="w-[320px] lg:w-[400px] max-w-full mx-auto py-15px lg:py-30px px-25px lg:px-50px bg-white rounded-10px shadow-card flex flex-col gap-15px items-center">
                            <div class="flex items-center justify-center">
                                <img src="/assets_out/images/referral-software/icons/guest-referrals.png" alt="Easy for guest referrals with Elioplus Referral Software feature" loading="lazy">
                            </div>
                            <div class="flex flex-col gap-10px">
                                <h4 class="text-base lg:text-lg font-bold">Easy for guest referrals</h4>
                                <p class="text-sm lg:text-base">Provide a quick option for one-time or infrequent referrers to send their lead without signing up</p>
                            </div>
                        </div>
                        <div class="w-[320px] lg:w-[400px] max-w-full mx-auto py-15px lg:py-30px px-25px lg:px-50px bg-white rounded-10px shadow-card flex flex-col gap-15px items-center">
                            <div class="flex items-center justify-center">
                                <img src="/assets_out/images/referral-software/icons/import.png" alt="Import contacts from CRM Elioplus Referral Software feature" loading="lazy">
                            </div>
                            <div class="flex flex-col gap-10px">
                                <h4 class="text-base lg:text-lg font-bold">Import contacts from CRM</h4>
                                <p class="text-sm lg:text-base">Import contacts from your CRM to transfer your partners directly to your referral software</p>
                            </div>
                        </div>
                        <div class="w-[320px] lg:w-[400px] max-w-full mx-auto py-15px lg:py-30px px-25px lg:px-50px bg-white rounded-10px shadow-card flex flex-col gap-15px items-center">
                            <div class="flex items-center justify-center">
                                <img src="/assets_out/images/referral-software/icons/all-in-one.png" alt="All in one platform with Elioplus Referral Software feature" loading="lazy">
                            </div>
                            <div class="flex flex-col gap-10px">
                                <h4 class="text-base lg:text-lg font-bold">All in one platform</h4>
                                <p class="text-sm lg:text-base">If you also have a partner program for channel partners then you can use our PRM system under the same platform</p>
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
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Integrations</h3>
                        <p class="w-full text-base lg:text-body">
                            You can easily connect your CRM with our one step integration to transfer all lead submissions directly to your CRM.
                            <br class='hidden lg:block'>
                            We connect with Hubspot, Salesforce and all major CRMs.
                        </p>
                    </div>
                    <div class="m-carousel flex gap-50px justify-center" data-items="1">
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <img src="/assets_out/images/referral-software/hubspot.png" alt="Hubspot Integration" loading="lazy">
                            </div>
                        </div>
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <img src="/assets_out/images/referral-software/microsoftcrm.png" alt="Microsoftcrm Integration" loading="lazy">
                            </div>
                        </div>
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <img src="/assets_out/images/referral-software/salesforce.png" alt="Salesforce Integration" loading="lazy">
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
                            <h4 class="text-base lg:text-lg font-semibold text-blue">REFERRAL SOFTWARE PRICING</h4>
                            <h3 class="text-2.5xl lg:text-5xl font-bold font-inter">Simple & Transparent Pricing</h3>
                            <p class="text-base lg:text-body text-center w-full">Whether you are a small IT company or a growing enterprise there is a plan that caters to your needs.</p>
                        </div>
                    </div>
                    <div class="grid-cols-1 md:grid-cols-2 mdl:grid-cols-3 grid lg:flex lg:flex-row lg:justify-center items-center gap-30px lg:gap-50px w-full">
                        <div class="w-[310px] max-w-full  mx-auto lg:mx-0 bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">Starter</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">What you need to get started and try the Elioplus Referral software.</p>
                            </div>
                            <div class="flex flex-col gap-10px w-full">
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">$99</h4>
                                <p class="w-full text-sm lg:text-sm text-center lg:text-left italic text-textGray">Per month, you can cancel anytime.</p>
                            </div>
                            <a id="aSignUp" runat="server" class="btn large font-bold text-sm bg-white border-blackBlue text-blackBlue w-full justify-center" href="/free-sign-up"><span>GET STARTED NOW</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
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
                                        <div class="text-[15px]">100 Referral Partners</div>
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
                                        <div class="text-[15px]">2 GB library storage</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Reporting" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Reporting</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for CRM Integration" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">CRM Integration</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/close.svg" alt="Not included feature for Trigger Notifications" loading="lazy" />
                                        </div>
                                        <div class="text-[15px] text-textGray">Trigger Notifications</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/close.svg" alt="Not included feature for Promote your Referral Program" loading="lazy" />
                                        </div>
                                        <div class="text-[15px] text-textGray">Promote your Referral Program</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="w-[310px] max-w-full  mx-auto lg:mx-0 bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left text-blue">Growth</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">For medium sized companies that are constantly growing.</p>
                            </div>
                            <div class="flex flex-col gap-10px w-full">
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">$249</h4>
                                <p class="w-full text-sm lg:text-sm text-center lg:text-left italic text-textGray">Per month, you can cancel anytime.</p>
                            </div>
                            <a id="aSignUpB" runat="server" class="btn large font-bold text-sm bg-blue text-white w-full justify-center" href="/free-sign-up"><span>GET STARTED NOW</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
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
                                        <div class="text-[15px]">1000 Referral Partners</div>
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
                                        <div class="text-[15px]">10GB Content Library</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Reporting" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Reporting</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for CRM Integration" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">CRM Integration</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Trigger Notifications" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Trigger Notifications</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Promote your Referral Program" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Promote your Referral Program</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="w-[310px] max-w-full  mx-auto lg:mx-0 bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">Enterprise</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">The ultimate solution for large & enterprise companies.</p>
                            </div>
                            <div class="flex flex-col gap-10px w-full">
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">$499</h4>
                                <p class="w-full text-sm lg:text-sm text-center lg:text-left italic text-textGray">Per month, you can cancel anytime.</p>
                            </div>
                            <a id="aSignUpC" runat="server" class="btn large font-bold text-sm bg-white border-blackBlue text-blackBlue w-full justify-center" href="/free-sign-up"><span>GET STARTED NOW</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
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
                                        <div class="text-[15px]">Unlimited Referral Partners</div>
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
                                        <div class="text-[15px]">Unlimited Content Library</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Reporting" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Reporting</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for CRM Integration" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">CRM Integration</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Trigger Notifications" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Trigger Notifications</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Promote your Referral Program" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Promote your Referral Program</div>
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
                        <div class="flex items-center justify-center">
                            <img class="max-w-[350px] lg:max-w-full" src="/assets_out/images/contact-us/faq.svg" alt="Visit the Elioplus FAQ to get answers for your questions" loading="lazy">
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
    <div class="modal fixed p-25px bg-white shadow-card z-50 hidden" id="modalAccessForm">
        <div class="flex flex-col w-full gap-30px">
            <h4 class="text-base lg:text-xl font-bold font-inter text-center lg:text-left">Get Early Access:</h4>
            <form class="flex flex-col gap-15px w-full" method="POST">
                <div class="grid-cols-1 lg:grid-cols-2 gap-30px lg:gap-50px grid">
                    <div class="input-field">
                        <label class="text-sm font-semibold" for="fname">First Name:</label>
                        <input id="fname" name="fname" type="text" placeholder="Enter your first name" required>
                    </div>
                    <div class="input-field">
                        <label class="text-sm font-semibold" for="lname">Last Name:</label>
                        <input id="lname" name="lname" type="text" placeholder="Enter your last name" required>
                    </div>
                </div>
                <div class="grid-cols-1 lg:grid-cols-2 gap-30px lg:gap-50px grid">
                    <div class="input-field">
                        <label class="text-sm font-semibold" for="email">Working Email:</label>
                        <input id="email" name="email" type="email" placeholder="Enter your email address">
                    </div>
                    <div class="input-field">
                        <label class="text-sm font-semibold" for="company">Company:</label>
                        <input id="company" name="company" type="text" placeholder="Enter your company name" required>
                    </div>
                </div>
                <div class="grid-cols-1 lg:grid-cols-2 gap-30px lg:gap-50px grid">
                    <div class="input-field">
                        <label class="text-sm font-semibold" for="country">Country:</label>
                        <input id="country" name="country" type="text" placeholder="Select your Country" required>
                    </div>
                    <div class="input-field">
                        <label class="text-sm font-semibold" for="city">City:</label>
                        <input id="city" name="city" type="text" placeholder="Enter your city" required>
                    </div>
                </div>
                <div class="input-field">
                    <label class="text-sm font-semibold" for="phone">Phone Number:</label>
                    <input id="phone" name="phone" type="number" placeholder="Enter your phone number" required>
                </div>
                <div class="w-full flex justify-end gap-20px">
                    <button class="btn large font-bold text-sm bg-blue text-white justify-center" type="submit">
                        <span>GET ACCESS</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                        </defs></svg>

                    </button>
                </div>
            </form>
        </div>
    </div>

</asp:Content>
