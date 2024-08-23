<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="MyDataPage.aspx.cs" Inherits="WdS.ElioPlus.pages.MyDataPage" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Manage your company’s and personal data on Elioplus." />
    <meta name="keywords" content="manage data, remove personal data" />
</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <main class="transition-all duration-200">
        <section class="py-0">
            <div class="container">
                <div class="grid-cols-1 lg:grid-cols-legal grid gap-0">
                    <div class="gap-80px lg:gap-80px px-50px py-100px border-x-2 border-gray flex flex-col w-full">
                        <div class="gap-30px lg:gap-30px flex flex-col w-full">
                            <div class="flex flex-col gap-10px w-full">
                                <div class="flex gap-10px text-textGray text-sm items-center">
                                    <div>Effective: <strong class="italic">September 1, 2015</strong></div>
                                    <div class="w-[7px] h-[7px] bg-textGray rounded-full"></div>
                                    <div>Last Updated: <strong class="italic">May 1, 2018</strong></div>
                                </div>
                                <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Manage your Data</h1>
                            </div>
                            <p class="italic text-base lg:text-xl">At Elioplus Inc. we respect your data and you can manage your Elioplus account as you want. Claim your company profile or request to remove your data from our platform.</p>
                        </div>
                        <div class="gap-50px lg:gap-80px flex flex-col w-full">
                            <div class="gap-20px lg:gap-25px flex flex-col w-full" id="claimProfile">
                                <h2 class="text-xl lg:text-2.5xl font-inter font-bold">1. Claim Your Company Profile</h2>
                                <p class="text-base lg:text-body">
                                    Promote your company’s expertise to our visitors, our business community, and interested local businesses with an up-to-date profile on Elioplus. Update your company’s profile to reflect your product portfolio and services and grow your awareness to your local market.
                    <br>
                                    <br>
                                    In order to claim a company profile please browse and visit a company profile and use the “Claim Profile” option on the bottom of the listing.
                                </p>
                                <a class="btn font-bold text-sm text-blue" href="/search-results"><span>SEARCH MY COMPANY PROFILE</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                            <div class="gap-20px lg:gap-25px flex flex-col w-full" id="requestRemoval">
                                <h2 class="text-xl lg:text-2.5xl font-inter font-bold">2. Request Removal/Deletion from Elioplus (Opt-Out of Sale)</h2>
                                <p class="text-base lg:text-body">
                                    Want to remove your existing company profile and individual data from the Elioplus database?
                    <br>
                                    <br>
                                    If you make this choice, all information contained in your company profile, including personal data, will be removed and your record will become unavailable in our products as soon as possible.
                    <br>
                                    <br>
                                    Removing your company profile also serves as an opt-out of the sale of information that was contained in your profile.
                                </p>
                                <a class="btn font-bold text-sm text-blue" href="/contact-us"><span>REQUEST REMOVAL</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                    </div>
                    <div class="gap-80px lg:gap-80px py-100px  flex flex-col w-full">
                        <div class="relative h-full">
                            <aside class="sidebar-container sticky top-[100px] mt-100px">
                                <div class="gap-30px lg:gap-50px flex flex-col w-full">
                                    <div class="gap-20px lg:gap-25px pl-30px flex flex-col w-full">
                                        <h3 class="text-xl lg:text-2.5xl font-inter font-bold">Table of Contents</h3>
                                        <ol class="gap-5px lg:gap-10px flex flex-col toc">
                                            <li><a class="text-base go-to text-textGray" href="#claimProfile">Claim your company profile</a></li>
                                            <li><a class="text-base go-to text-textGray" href="#requestRemoval">Request Removal/Deletion from Elioplus (Opt-Out of Sale)</a></li>
                                        </ol>
                                    </div>
                                    <div class="divider bg-gray h-[2px]"></div>
                                    <div class="gap-20px lg:gap-25px pl-30px flex flex-col w-full">
                                        <h3 class="text-xl lg:text-2.5xl font-inter font-bold">Legal</h3>
                                        <div class="flex flex-col gap-10px w-full">
                                            <a id="aTerms" runat="server" class="text-base lg:font-[17px] text-blue font-semibold underline">Terms & Conditions</a>
                                            <a id="aPrivacy" runat="server" class="text-base lg:font-[17px] text-blue font-semibold underline">Privacy Policy</a>
                                            <a id="aFaq" runat="server" class="text-base lg:font-[17px] text-blue font-semibold underline">Frequently asked questions</a>
                                        </div>
                                    </div>
                                </div>
                            </aside>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
