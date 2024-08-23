<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="PricingPrmSoftwarePage.aspx.cs" Inherits="WdS.ElioPlus.pages.PricingPrmSoftwarePage" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="View the pricing plans for the Elioplus PRM software and compare the features and capabilities for each option." />
    <meta name="keywords" content="prm software plans, prm software pricing, prm software plans" />

</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <main class="transition-all duration-200">
        <section>
            <div class="container">
                <div class="gap-40px lg:gap-60px flex flex-col justify-center items-center w-full">
                    <div class="gap-20px lg:gap-30px flex flex-col justify-center items-center w-full">
                        <div class="text-center gap-5px lg:gap-10px flex flex-col items-center w-full">
                            <h2 class="text-base lg:text-lg font-semibold text-blue">PRM SOFTWARE PRICING</h2>
                            <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Partner Relationship Management</h1>
                            <p class="text-base lg:text-body text-center w-[900px] max-w-full">Manage your partner network and incentivize your partner with our partner relationship management software. Get started with the plan that suits your needs.</p>
                        </div>
                        <div class="flex justify-center">
                            <a class="btn large font-bold text-sm xl:text-base bg-white" href="/prm-software"><span>VIEW PRM PRODUCT IN DETAIL</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                        </div>
                    </div>
                    <div class="grid-cols-1 sm:grid-cols-2 grid lg:flex lg:flex-row lg:justify-between items-center gap-30px lg:gap-20px w-full">
                        <div class="w-[310px] max-w-full mx-auto bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">Free</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">What you need to get started and try the Elioplus PRM software.</p>
                            </div>
                            <div class="flex flex-col gap-10px w-full">
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">$0</h4>
                                <p class="w-full text-sm lg:text-sm text-center lg:text-left italic text-textGray">Upgrade to a premium plan anytime.</p>
                            </div>
                            <a id="aGetStartedForFree" runat="server" class="btn large font-bold text-sm bg-white border-blackBlue text-blackBlue w-full justify-center" href="/free-sign-up"><span>START FOR FREE</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                            <div class="flex flex-col gap-15px w-full">
                                <h5 class="text-base font-inter font-semibold">Features:</h5>
                                <div class="flex flex-col gap-5px w-full">
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/partners.svg" alt="manage partners icon" loading="lazy">
                                        </div>
                                        <div class="text-[15px]">Manage up to 5 partners</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/storage.svg" alt="storage icon" loading="lazy">
                                        </div>
                                        <div class="text-[15px]">2 GB library storage</div>
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
                                            <img src="/assets_out/images/pricing/close.svg" alt="Not included feature for Partner recruitment" loading="lazy" />
                                        </div>
                                        <div class="text-[15px] text-textGray">Partner recruitment</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/close.svg" alt="Not included feature for Customization" loading="lazy" />
                                        </div>
                                        <div class="text-[15px] text-textGray">Customization</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="w-[310px] max-w-full mx-auto bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">Start-Up</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">For small companies and start-ups that need a PRM solution.</p>
                            </div>
                            <div class="flex flex-col gap-10px w-full">
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">$298</h4>
                                <p class="w-full text-sm lg:text-sm text-center lg:text-left italic text-textGray">Per month, you can cancel anytime.</p>
                            </div>
                            <a id="aStartup" runat="server" class="btn large font-bold text-sm bg-white border-blackBlue text-blackBlue w-full justify-center" href="/free-sign-up"><span>GET STARTED NOW</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
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
                                            <img src="/assets_out/images/pricing/partners.svg" alt="manage partners icon" loading="lazy">
                                        </div>
                                        <div class="text-[15px]">Manage up to 100 partners</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/storage.svg" alt="storage icon" loading="lazy">
                                        </div>
                                        <div class="text-[15px]">10 GB library storage</div>
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
                        <div class="w-[310px] max-w-full mx-auto bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left text-blue">Growth</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">For medium and large companies that scale and grow fast.</p>
                            </div>
                            <div class="flex flex-col gap-10px w-full">
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">$498</h4>
                                <p class="w-full text-sm lg:text-sm text-center lg:text-left italic text-textGray">Per month, you can cancel anytime.</p>
                            </div>
                            <a id="aGrowth" runat="server" class="btn large font-bold text-sm bg-blue text-white w-full justify-center" href="/free-sign-up"><span>GET STARTED NOW</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
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
                                            <img src="/assets_out/images/pricing/partners.svg" alt="manage partners icon" loading="lazy">
                                        </div>
                                        <div class="text-[15px]">Manage up to 250 partners</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/storage.svg" alt="storage icon" loading="lazy">
                                        </div>
                                        <div class="text-[15px]">20 GB library storage</div>
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
                        <div class="w-[310px] max-w-full mx-auto bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">Enterprise</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">For big corporations with extensive requirements that need a PRM.</p>
                            </div>
                            <div class="flex flex-col gap-10px w-full">
                                <h4 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">$1200+</h4>
                                <p class="w-full text-sm lg:text-sm text-center lg:text-left italic text-textGray">Custom pricing, starting from.</p>
                            </div>
                            <a class="btn large font-bold text-sm bg-white border-blackBlue text-blackBlue w-full justify-center" href="/contact-us"><span>CONTACT US</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                            <div class="flex flex-col gap-15px w-full">
                                <h5 class="text-base font-inter font-semibold">Features:</h5>
                                <div class="flex flex-col gap-5px w-full">
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/partners.svg" alt="manage partners icon" loading="lazy">
                                        </div>
                                        <div class="text-[15px]">Custom partners management</div>
                                    </div>
                                    <div class="flex items-center gap-5px">
                                        <div class="w-[20px] h-[20px] flex items-center justify-center">
                                            <img src="/assets_out/images/pricing/storage.svg" alt="storage icon" loading="lazy">
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
                </div>
            </div>
        </section>
        <section class="bg-white">
            <div class="container">
                <div class="gap-50px lg:gap-80px flex flex-col">
                    <div class="grid-cols-1 lg:grid-cols-2 text-img-row grid gap-50px">
                        <div class="flex items-center justify-center">
                            <img class="max-w-[350px] lg:max-w-full" src="/assets_out/images/pricing/prm-cta.svg" alt="More than 120,000 companies use Elioplus PRM" loading="lazy">
                        </div>
                        <div class="gap-20px lg:gap-30px flex flex-col justify-center">
                            <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">More than <span class='text-blue'>120,000</span> companies are using Elioplus.</h3>
                            <p class="w-full text-base lg:text-body text-center lg:text-left">What are you waiting for? Select the plan that fits your needs and start inviting your channel partners to join your PRM portal.</p>
                            <div class="flex justify-center lg:justify-start gap-15px">
                                <a class="btn large font-bold text-sm xl:text-base" href="/prm-software"><span>ELIOPLUS PRM</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a><a id="aGetStarted" runat="server" class="btn large font-bold text-sm xl:text-base bg-blue text-white" href="/free-sign-up"><span>GET STARTED NOW</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>        
    </main>
</asp:Content>
