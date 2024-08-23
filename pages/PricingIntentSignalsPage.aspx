<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="PricingIntentSignalsPage.aspx.cs" Inherits="WdS.ElioPlus.pages.PricingIntentSignalsPage" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="View the Intent Signals pricing plans and the cost for RFQs and high intent leads. Get access to potential customers near you." />
    <meta name="keywords" content="lead generation pricing, cost for RFQs, cost for high intent leads" />

</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <main class="transition-all duration-200">
        <section>
            <div class="container">
                <div class="gap-40px lg:gap-60px flex flex-col justify-center items-center w-full">
                    <div class="gap-20px lg:gap-30px flex flex-col justify-center items-center w-full">
                        <div class="text-center gap-5px lg:gap-10px flex flex-col items-center w-full">
                            <h2 class="text-base lg:text-lg font-semibold text-blue">INTENT SIGNALS PRICING</h2>
                            <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Lead Generation & RFQs</h1>
                            <p class="text-base lg:text-body text-center w-[900px] max-w-full">Grow your sales pipeline with leads and RFQs from thousands of local businesses near you. Contact us today and get a quote from our sales team.</p>
                        </div>
                        <div class="flex justify-center">
                            <a class="btn large font-bold text-sm xl:text-base bg-white" href="/intent-signals"><span>VIEW THE INTENT SIGNALS PRODUCT</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                        </div>
                    </div>
                    <div class="grid-cols-1 md:grid-cols-2 mdl:grid-cols-2 grid lg:flex lg:flex-row lg:justify-center items-center gap-30px lg:gap-50px w-full">
                        <div class="w-[360px] max-w-full mx-auto lg:mx-0 bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">Free</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">Limited to small local IT companies, we offer a free plan to get access to a number of leads every month.</p>
                            </div>
                            <div class="flex flex-col gap-10px w-full">
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">$0</h4>
                                <p class="w-full text-sm lg:text-sm text-center lg:text-left italic text-textGray">Upgrade to a plan anytime.</p>
                            </div>
                            <a class="btn large font-bold text-sm bg-white border-blackBlue text-blackBlue w-full justify-center" href="/contact-us"><span>CONTACT US NOW</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
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
                                            <img src="/assets_out/images/pricing/close.svg" alt="Not included feature for Access RFQs" loading="lazy" />
                                        </div>
                                        <div class="text-[15px] text-textGray">Access RFQs</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/close.svg" alt="Not included feature for High Intent Leads" loading="lazy" />
                                        </div>
                                        <div class="text-[15px] text-textGray">High Intent Leads</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/close.svg" alt="Not included feature for Customer Requests" loading="lazy" />
                                        </div>
                                        <div class="text-[15px] text-textGray">Customer Requests</div>
                                    </div>
                                    <div class="flex items-center gap-5px">

                                        <div class="text-[15px] text-textGray"></div>
                                    </div>
                                    <div class="flex items-center gap-5px">

                                        <div class="text-[15px] text-textGray">
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="w-[360px] max-w-full mx-auto lg:mx-0 bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left text-blue">Premium</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">The pricing is based on the country or regions that you are interested to gain access and is charged on a monthly basis.</p>
                            </div>
                            <div class="flex flex-col gap-10px w-full">
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Custom Pricing</h4>
                                <p class="w-full text-sm lg:text-sm text-center lg:text-left italic text-textGray">Reach out to our sales team for details</p>
                            </div>
                            <a class="btn large font-bold text-sm bg-blue text-white w-full justify-center" href="/contact-us"><span>CONTACT SALES</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                            <div class="flex flex-col gap-15px w-full">
                                <h5 class="text-base font-inter font-semibold">Features:</h5>
                                <div class="flex flex-col gap-5px w-full">
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Get a Featured company profile" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Get a Featured company profile</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Access RFQs" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Access RFQs</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Unlimited High Intent Leads" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Unlimited High Intent Leads</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Exclusive Customer Requests" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Exclusive Customer Requests</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Worlwide or Local Targeting" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Worlwide or Local Targeting</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/check.svg" alt="Included feature for Enreach Your Leads (Coming Soon)" loading="lazy" />
                                        </div>
                                        <div class="text-[15px]">Enreach Your Leads (Coming Soon)</div>
                                    </div>
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
                    <div class="grid-cols-1 lg:grid-cols-2 text-img-row grid gap-50px">
                        <div class="flex items-center justify-center">
                            <img class="max-w-[350px] lg:max-w-full" src="/assets_out/images/pricing/intent-cta.svg" alt="Join more than 120k companies using Elioplus for increasing their Intent Signals and RFQ data" loading="lazy">
                        </div>
                        <div class="gap-20px lg:gap-30px flex flex-col justify-center">
                            <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">More than <span class='text-blue'>120,000</span> companies are using Elioplus.</h3>
                            <p class="w-full text-base lg:text-body text-center lg:text-left">What are you waiting for? Get started now and win new customers, expand your services or upsell your current clients and easily grow your network.</p>
                            <div class="flex justify-center lg:justify-start gap-15px">
                                <a class="btn large font-bold text-sm xl:text-base" href="/intent-signals"><span>INTENT SIGNALS DETAILS</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a><a class="btn large font-bold text-sm xl:text-base bg-blue text-white" href="/contact-us"><span>CONTACT SALES</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section>
            <div class="container">
                <div class="gap-30px lg:gap-50px flex flex-col">
                    <div class="flex flex-col gap-10px">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Frequently Asked Questions</h3>
                        <p class="w-full text-base lg:text-body text-left">Common questions about the Elioplus Intent Signals solution.</p>
                    </div>
                    <div class="grid-cols-1 lg:grid-cols-2 gap-30px grid">
                        <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                            <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                <div class="text-blue">Q.</div>
                                <div>What is the pricing?</div>
                            </summary>
                            <p class="px-10px lg:px-20px py-25px">
                                The pricing is based on the country or regions that you are interested to gain access and is charged on a monthly basis.
                            </p>
                        </details>
                        <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                            <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                <div class="text-blue">Q.</div>
                                <div>Is there a limit on the number of leads?</div>
                            </summary>
                            <p class="px-10px lg:px-20px py-25px">
                                No. You get unlimited access to the leads and quotations that are uploaded each month on our platform.
                            </p>
                        </details>
                        <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                            <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                <div class="text-blue">Q.</div>
                                <div>What regions do you cover?</div>
                            </summary>
                            <p class="px-10px lg:px-20px py-25px">
                                We get quotation requests and leads from every country on the planet so no matter where your company is based you can try Intent Signals.
                            </p>
                        </details>
                        <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                            <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                <div class="text-blue">Q.</div>
                                <div>Is there a trial option?</div>
                            </summary>
                            <p class="px-10px lg:px-20px py-25px">
                                While there is no trial option, we do offer a plan for local IT companies to get access to a number of leads for free per month. Please contact our team for more information.
                            </p>
                        </details>
                        <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                            <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                <div class="text-blue">Q.</div>
                                <div>What type of data do you provide?</div>
                            </summary>
                            <p class="px-10px lg:px-20px py-25px">
                                On the quotation requests you will be sending your quote directly to the appropriate person and for the intent data you get all the necessary company details to get in touch with a specific lead.
                            </p>
                        </details>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
