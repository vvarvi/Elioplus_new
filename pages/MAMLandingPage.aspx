<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="MAMLandingPage.aspx.cs" Inherits="WdS.ElioPlus.pages.MAMLandingPage" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Find a buyer for your business on the Elioplus M&A marketplace, in collaboration with ITX. Focused exclusively on IT businesses, we can help you to sell your business easy and fast in a network of 50K IT decision makers." />
    <meta name="keywords" content="IT acquisitions, IT mergers, IT M&A marketplace, sell IT business, buy IT business" />

    <script type="text/javascript" src="https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit" async defer></script>
    <script type="text/javascript">
        var onloadCallback = function () {
            grecaptcha.render('dvCaptcha', {
                'sitekey': '<%=ReCaptcha_Key %>',
                'callback': function (response) {
                    $.ajax({
                        type: "POST",
                        url: "MAMLandingPage.aspx/VerifyCaptcha",
                        data: "{response: '" + response + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
                            var captchaResponse = jQuery.parseJSON(r.d);
                            if (captchaResponse.success) {
                                $("[id*=txtCaptcha]").val(captchaResponse.success);
                                $("[id*=rfvCaptcha]").hide();
                            } else {
                                $("[id*=txtCaptcha]").val("");
                                $("[id*=rfvCaptcha]").show();
                                var error = captchaResponse["error-codes"][0];
                                $("[id*=rfvCaptcha]").html("RECaptcha error. " + error);
                            }
                        }
                    });
                }
            });
        };
    </script>

</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <main class="transition-all duration-200">
        <header class="bg-blue text-white shadow-lg z-10 sticky top-[63px] lg:top-[72px] xl:top-[92px] left-0 w-screen py-0 lg:py-15px">
            <div class="container">
                <div class="flex justify-between">
                    <div class="py-5px px-5px pr-[15px] hidden lg:flex items-center gap-5px bg-blueDark rounded-30px">
                        <img src="/assets_out/images/global/product-circles.svg" alt="Referral Program software product" width="30" height="30">
                        <div class="text-sm font-inter">Product:&nbsp;<strong>M&amp;A Marketplace Software</strong></div>
                    </div>
                    <nav class="flex gap-10px xl:gap-15px relative items-center w-full lg:w-fit" id="productNavigation">
                        <div class="w-full lg:w-fit overflow-x-auto">
                            <ul class="text-sm flex gap-15px flex-wrap w-max py-15px lg:py-0">
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#overview">Overview</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#howItWorks">How it works</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#processTimeline">Process Timeline</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#listings">Listings</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#sellerChannels">Seller Channels</a></li>
                            </ul>
                        </div>
                        <a class="btn hidden lg:flex font-bold text-sm bg-yellow text-white w-fit modal-trigger" href="#BookDemoModal"><span>GET STARTED</span></a>
                    </nav>
                </div>
            </div>
        </header>
        <section class="bg-gradient-product opener" id="overview">
            <div class="container">
                <div class="grid-cols-1 lg:grid-cols-2 grid gap-30px">
                    <div class="row-start-2 lg:row-start-1 gap-20px lg:gap-30px flex flex-col justify-center">
                        <div class="text-center lg:text-left gap-10px lg:gap-15px flex flex-col">
                            <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">IT Focused Mergers & Acquisition Marketplace</h1>
                            <h2 class="text-2xl lg:text-3xl font-bold font-inter text-blue">Sell your IT business fast and reliably.</h2>
                        </div>
                        <p class="text-base lg:text-body text-center lg:text-left">In collaboration with ITX, Elioplus helps you to reach out to potential buyers worldwide and sell your IT company to a network of <strong>50,000</strong> IT decision makers worldwide.</p>
                        <div class="flex items-center gap-10px xl:gap-15px flex-col lg:flex-row"><a class="btn large font-bold text-sm xl:text-base bg-blue text-white modal-trigger" href="#BookDemoModal"><span>GET STARTED AND SELL YOUR COMPANY</span></a></div>
                    </div>
                    <div class="flex justify-center items-center">
                        <img class="max-w-full" src="/assets_out/images/ma-marketplace/MA-Marketplace-homepage.png" alt="IT Focused Mergers &amp; Acquisition Marketplace at Elioplus">
                    </div>
                </div>
            </div>
            <div class="mt-30px wave">
                <img src="/assets_out/images/global/wave.svg" alt="Elioplus decorative wave">
            </div>
        </section>
        <section id="howItWorks">
            <div class="container">
                <div class="gap-50px lg:gap-80px flex flex-col">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">How it Works</h3>
                        <p class="w-full text-base lg:text-body">Get to know how Elioplus in collaboration with ITX help you drive sellers to purchase your company.</p>
                    </div>
                    <div class="grid-cols-1 lg:grid-cols-2 text-img-row grid gap-30px">
                        <div class="flex items-center justify-center">
                            <img class="max-w-full mdl:max-w-[350px]" src="/assets_out/images/ma-marketplace/process.svg" alt="Seamless Process to sell your company" loading="lazy">
                        </div>
                        <div class="gap-20px lg:gap-30px flex flex-col justify-center">
                            <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Seamless Process</h3>
                            <p class="w-full text-base lg:text-body text-center lg:text-left">Reach out to express your interest in selling your business and an expert will connect with you to gather all the necessary information. The ITX experts will launch the campaign, reach out to potentials buyers and assist you in the sale process until the deal is closed.</p>
                        </div>
                    </div>
                    <div class="grid-cols-1 lg:grid-cols-2 text-img-row grid gap-30px">
                        <div class="row-start-2 lg:row-start-1 gap-20px lg:gap-30px flex flex-col justify-center">
                            <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Focused on the IT industry</h3>
                            <p class="w-full text-base lg:text-body text-center lg:text-left">The Elioplus M&A marketplace, in collaboration with ITX, works exclusively with companies that operate in the IT industry, MSPs, SaaS businesses, cyber security firms, providers of CRM & ERP software, IT services and many more. ITX understands your business and has done similar deals in the past.</p>
                        </div>
                        <div class="flex items-center justify-center">
                            <img class="max-w-full mdl:max-w-[350px]" src="/assets_out/images/ma-marketplace/focused.svg" alt="Focused on the IT industry to sell your company" loading="lazy">
                        </div>
                    </div>
                    <div class="grid-cols-1 lg:grid-cols-2 text-img-row grid gap-30px">
                        <div class="flex items-center justify-center">
                            <img class="max-w-full mdl:max-w-[350px]" src="/assets_out/images/ma-marketplace/seller-network.svg" alt="A global Network to sell your company" loading="lazy">
                        </div>
                        <div class="gap-20px lg:gap-30px flex flex-col justify-center">
                            <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Seller Network</h3>
                            <p class="w-full text-base lg:text-body text-center lg:text-left">A global network of more than 50,000 IT decision makers are actively looking to buy their next acquisition. An expert will create a showcase listing of your company for the marketplace and an outreach campaign with hand-selected buyers that match your profile.</p>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-white" id="processTimeline">
            <div class="container">
                <div class="flex flex-col gap-40px">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Process Timeline</h3>
                        <p class="w-full max-w-full text-base lg:text-body">
                            A <strong>100 days</strong> plan to help you sell your business seamlessly.
                            <br class='hidden lg:block'>
                            You will be onboarded and supported along the way, from day 1 that you reach out until the day the deal is closed.
                        </p>
                    </div>
                    <div class="flex-row lg:flex-col gap-20px lg:gap-10px flex text-center items-center justify-center">
                        <div class="flex justify-center items-center flex-col lg:flex-row gap-10px xl:gap-20px w-max lg:w-full">
                            <div class="text-base lg:text-body text-textGray w-max">Day 1</div>
                            <div class="h-[220px] lg:h-[15px] w-[10px] lg:w-fit overflow-hidden rounded-10px flex-col lg:flex-row bg-blue grow flex">
                                <div class="w-full lg:w-1/10 h-1/10 lg:h-full bg-iconGray"></div>
                                <div class="w-full lg:w-3/6 h-3/6 lg:h-full bg-blue"></div>
                                <div class="grow bg-blue2"></div>
                                <div class="w-full lg:w-1/10 h-1/10 lg:h-full bg-green"></div>
                            </div>
                            <div class="text-base lg:text-body text-textGray w-max">Day 100+</div>
                        </div>
                        <div class="flex flex-col lg:flex-row lg:justify-between lg:px-80px gap-10px lg:gap-0 text-left">
                            <div class="w-[220px] flex gap-10px items-start">
                                <div class="pt-5px lg:pt-10px">
                                    <div class="w-[15px] h-[15px] rounded-full bg-iconGray"></div>
                                </div>
                                <div class="gap-0 lg:gap-5px flex flex-col">
                                    <h4 class="text-base lg:text-body font-semibold">Day 1-7</h4>
                                    <h5 class="text-sm lg:text-base text-textGray">Reach out to start the onboarding process.</h5>
                                </div>
                            </div>
                            <div class="w-[280px] flex gap-10px items-start">
                                <div class="pt-5px lg:pt-10px">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                </div>
                                <div class="gap-0 lg:gap-5px flex flex-col">
                                    <h4 class="text-base lg:text-body font-semibold">Day 8-59</h4>
                                    <h5 class="text-sm lg:text-base text-textGray">Your listing will be created and the buyers’ outreach will begin.</h5>
                                </div>
                            </div>
                            <div class="w-[280px] flex gap-10px items-start">
                                <div class="pt-5px lg:pt-10px">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue2"></div>
                                </div>
                                <div class="gap-0 lg:gap-5px flex flex-col">
                                    <h4 class="text-base lg:text-body font-semibold">Day 60-99</h4>
                                    <h5 class="text-sm lg:text-base text-textGray">Due diligence and negotiation of purchase agreement.</h5>
                                </div>
                            </div>
                            <div class="w-[220px] flex gap-10px items-start">
                                <div class="pt-5px lg:pt-10px">
                                    <div class="w-[15px] h-[15px] rounded-full bg-green"></div>
                                </div>
                                <div class="gap-0 lg:gap-5px flex flex-col">
                                    <h4 class="text-base lg:text-body font-semibold">Day 100+</h4>
                                    <h5 class="text-sm lg:text-base text-textGray">Close transaction and press releases.</h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section id="listings">
            <div class="container">
                <div class="gap-50px lg:gap-80px flex flex-col w-full">
                    <div class="flex flex-col gap-10px text-center items-center w-full">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Current listings in the marketplace</h3>
                        <p class="w-full max-w-full text-base lg:text-body">Find some companies that are currently showcased and listed in the marketplace.</p>
                    </div>
                    <div class="flex flex-col gap-50px w-full">
                        <div class="flex flex-col w-full bg-white shadow-card rounded-10px overflow-hidden py-25px gap-20px">
                            <div class="px-20px lg:px-40px flex-col lg:flex-row gap-10px justify-start lg:justify-between items-start lg:items-center flex">
                                <h4 class="text-base lg:text-lg font-inter">Company Type: <strong>IT SECURITY</strong></h4>
                                <h5 class="text-base lg:text-lg font-inter">Reference Number: <strong class='text-blue'>EX-816</strong></h5>
                            </div>
                            <div class="divider"></div>
                            <p class="px-20px lg:px-40px py-10px text-base">Headquartered on the West Coast, the Company is an application security software vendor that offers its always-active Application/API Security Posture Monitoring (ASPM) and Observability technology, endlessly monitoring applications and APIs and ensuring that no API/process escapes discovery and diagnosis. The technology provides an on-demand, real-minute architecture model of an application API ecosystem with all its assets, call-dependencies, vulnerabilities, and attack-surface.</p>
                        </div>
                        <div class="flex flex-col w-full bg-white shadow-card rounded-10px overflow-hidden py-25px gap-20px">
                            <div class="px-20px lg:px-40px flex-col lg:flex-row gap-10px justify-start lg:justify-between items-start lg:items-center flex">
                                <h4 class="text-base lg:text-lg font-inter">Company Type: <strong>MANAGED SERVICES PROVIDERS (MSPs)</strong></h4>
                                <h5 class="text-base lg:text-lg font-inter">Reference Number: <strong class='text-blue'>EX-816</strong></h5>
                            </div>
                            <div class="divider"></div>
                            <p class="px-20px lg:px-40px py-10px text-base">The Company was founded in 2017 and has acquired sixteen local MSPs to create a national managed service provider with local market presence in key markets. It offers managed and hosted services, as well as project engineering to over 600 customers across twelve states.</p>
                        </div>
                        <div class="flex flex-col w-full bg-white shadow-card rounded-10px overflow-hidden py-25px gap-20px">
                            <div class="px-20px lg:px-40px flex-col lg:flex-row gap-10px justify-start lg:justify-between items-start lg:items-center flex">
                                <h4 class="text-base lg:text-lg font-inter">Company Type: <strong>MANAGED SERVICES PROVIDERS (MSPs)</strong></h4>
                                <h5 class="text-base lg:text-lg font-inter">Reference Number: <strong class='text-blue'>EX-816</strong></h5>
                            </div>
                            <div class="divider"></div>
                            <p class="px-20px lg:px-40px py-10px text-base">The company is a digital service provider delivering consumable solutions to organizations in pursuit of their digital transformation initiatives. The Company is a hyper-specialized partner in the cloud services ecosystem focused on DevOps automation and cloud enablement, offering unique go-to market solutions in high end consulting services and managed subscription services.</p>
                        </div>
                        <div class="flex flex-col w-full bg-white shadow-card rounded-10px overflow-hidden py-25px gap-20px">
                            <div class="px-20px lg:px-40px flex-col lg:flex-row gap-10px justify-start lg:justify-between items-start lg:items-center flex">
                                <h4 class="text-base lg:text-lg font-inter">Company Type: <strong>IT SERVICES & INTEGRATION</strong></h4>
                                <h5 class="text-base lg:text-lg font-inter">Reference Number: <strong class='text-blue'>EX-816</strong></h5>
                            </div>
                            <div class="divider"></div>
                            <p class="px-20px lg:px-40px py-10px text-base">This Company is a fast-growing security solution provider that has experienced significant revenue growth in each full year of operations since the company’s founding in 2016 and forecasts $17M in 2022. It represents the top security and infrastructure vendors in the industry such as Fortinet, Meraki, Sophos, ESET, Palo Alto Networks, Barracuda and others, and its sustained top-line growth is paired with meaningful margin-enhancing IP that enables the Company to provide best in class service and care with minimal overhead.</p>
                        </div>
                        <div class="flex flex-col w-full bg-white shadow-card rounded-10px overflow-hidden py-25px gap-20px">
                            <div class="px-20px lg:px-40px flex-col lg:flex-row gap-10px justify-start lg:justify-between items-start lg:items-center flex">
                                <h4 class="text-base lg:text-lg font-inter">Company Type: <strong>IT SERVICES & INTEGRATION</strong></h4>
                                <h5 class="text-base lg:text-lg font-inter">Reference Number: <strong class='text-blue'>EX-816</strong></h5>
                            </div>
                            <div class="divider"></div>
                            <p class="px-20px lg:px-40px py-10px text-base">Headquartered in the East Coast with global offices in Europe and India, the Company is an award-winning software design and development company. It has built large-scale cutting-edge eCommerce applications across the globe catering to B2B & B2C customers ranging from Fortune 500 to the world's largest retailers.</p>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-white" id="sellerChannels">
            <div class="container">
                <div class="flex flex-col gap-40px">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Seller Channels</h3>
                        <p class="w-full text-base lg:text-body">Stop waiting and join a network of sales channels consisted by big companies in the IT industry.</p>
                    </div>
                    <div class="m-carousel flex sm:grid sm:grid-cols-3 lg:flex gap-30px justify-center" data-items="1">
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <img src="/assets_out/images/ma-marketplace/vmware.png" alt="vmware Logo" loading="lazy">
                            </div>
                        </div>
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <img src="/assets_out/images/ma-marketplace/cisco.png" alt="cisco Logo" loading="lazy">
                            </div>
                        </div>
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <img src="/assets_out/images/ma-marketplace/ibdm.png" alt="ibm Logo" loading="lazy">
                            </div>
                        </div>
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <img src="/assets_out/images/ma-marketplace/microsoft.png" alt="microsoft Logo" loading="lazy">
                            </div>
                        </div>
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <img src="/assets_out/images/ma-marketplace/oracle.png" alt="oracle Logo" loading="lazy">
                            </div>
                        </div>
                        <div class="flex items-center justify-center">
                            <div class="w-max">
                                <img src="/assets_out/images/ma-marketplace/salesforce.png" alt="salesforce Logo" loading="lazy">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-gray">
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

    <!-- Form Modal -->
    <div id="BookDemoModal" class="modal fixed p-25px bg-white shadow-card z-50 hidden">
        <asp:UpdatePanel runat="server" ID="UpdatePanel6">
            <ContentTemplate>
                <div class="flex flex-col w-full gap-30px">
                    <h4 class="text-base lg:text-xl font-bold font-inter text-center lg:text-left">
                        <asp:Label ID="Label1" Text="Contact Form:" runat="server" />
                    </h4>
                    <form class="flex flex-col gap-15px w-full" method="POST">
                        <div class="grid-cols-1 lg:grid-cols-2 gap-30px lg:gap-50px grid">
                            <div class="input-field">
                                <label class="text-sm font-semibold">First Name:</label>
                                <asp:TextBox ID="TbxFirstName" runat="server" MaxLength="45" name="firstname" type="text" placeholder="Enter your first name" />
                            </div>
                            <div class="input-field">
                                <label class="text-sm font-semibold">Last Name:</label>
                                <asp:TextBox ID="TbxLastName" runat="server" MaxLength="45" name="lastname" type="text" placeholder="Enter your last name" />
                            </div>
                        </div>
                        <div class="grid-cols-1 lg:grid-cols-2 gap-30px lg:gap-50px grid">
                            <div class="input-field">
                                <label class="text-sm font-semibold">Company Name:</label>
                                <asp:TextBox ID="TbxBusinessName" runat="server" MaxLength="45" name="companyname" type="text" placeholder="Enter your company name" />
                            </div>
                            <div class="input-field">
                                <label class="text-sm font-semibold">Working Email Address:</label>
                                <asp:TextBox ID="TbxCompanyEmail" runat="server" MaxLength="45" name="email" type="text" placeholder="Enter your email address" />
                            </div>
                        </div>
                        <div class="input-field">
                            <label class="text-sm font-semibold">Phone Number:</label>
                            <asp:TextBox ID="TbxPhone" runat="server" MaxLength="15" name="phone" type="number" placeholder="Enter your phone number" />
                        </div>
                        <div class="input-field">
                            <label class="text-sm font-semibold">Message:</label>
                            <asp:TextBox ID="TbxMessage" runat="server" MaxLength="2000" name="message" type="text" TextMode="MultiLine" Rows="10" placeholder="Enter your message" />
                        </div>
                        <div class="w-full flex justify-end gap-10px xl:gap-15px">
                            <asp:Button ID="BtnClear" OnClick="BtnClear_Click" Text="CLEAR" class="btn large font-bold text-sm bg-white text-blue justify-center" type="submit" runat="server" />
                            <asp:Button ID="BtnSubmit" OnClick="BtnSubmit_Click" Text="SEND MESSAGE" class="btn large font-bold text-sm bg-blue text-white justify-center" type="submit" runat="server" />
                        </div>
                        <div class="form-group" style="display: inline-block; float: left; margin-left: 15px;">
                            <div id="dvCaptcha"></div>
                            <div class="input-group">
                                <asp:TextBox ID="txtCaptcha" runat="server" Style="display: none" />
                                <asp:RequiredFieldValidator ID="rfvCaptcha" ErrorMessage="Captcha validation is required." ControlToValidate="txtCaptcha" runat="server" ForeColor="Red" Display="Dynamic" />
                            </div>
                        </div>
                        <div class="w-full flex flex-col">
                            <controls:MessageControl ID="UcMessageAlert" runat="server" />
                        </div>
                    </form>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

        <script type="text/javascript">
            function CloseSelfServiceModal() {
                alert('1');
                $('#BookDemoModal').modal('hide');
                alert('2');
            }
        </script>

    </telerik:RadScriptBlock>

</asp:Content>
