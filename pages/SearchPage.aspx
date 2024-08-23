<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="SearchPage.aspx.cs" Inherits="WdS.ElioPlus.pages.SearchPage" %>

<asp:Content ID="SearchHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Search for channel partners based on their product & service offerings, location, industry and other criteria. Find partner programs from innovative vendors in more than 100 categories." />
    <meta name="keywords" content="find channel partners, find partner programs, IT vendors" />
</asp:Content>

<asp:Content ID="SearchMain" ContentPlaceHolderID="MainContent" runat="server">
    <main class="transition-all duration-200">
        <section class="bg-gray">
            <div class="container">
                <div class="gap-40px lg:gap-60px flex flex-col justify-center items-center w-full">
                    <div class="gap-20px lg:gap-30px flex flex-col justify-center items-center w-full">
                        <div class="text-center gap-5px lg:gap-10px flex flex-col items-center w-full">
                            <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Search and browse more than <span class='text-blue'>120.000</span>
                                <br>
                                Channel Partners & Partner Programs at Elioplus</h1>
                            <p class="text-base lg:text-body text-center w-full">Select if you are looking for partner programs by vendors or looking for channel partners. Choose between 125 different products, select the market you are interested in and get connected with thousands of IT companies for free.</p>
                        </div>
                    </div>
                    <div class="flex justify-center items-center flex-col gap-15px">
                        <h2 class="text-base lg:text-lg text-blue text-center font-inter font-bold">Unlock advanced search features and grow your opportunities by purchasing one of our premium products.</h2>
                        <div class="p-15px justify-center lg:justify-between items-center flex-col lg:flex-row gap-15px bg-white rounded-40px shadow-card flex w-full" id="searchForm" method="POST" action="SearchPage.aspx">
                            <div class="text-sm lg:text-sm px-20px py-10px w-full lg:w-fit text-center flex gap-5px text-textGray border-gray border-2 font-bold rounded-30px justify-center bg-gray2">
                                <label for="country">Looking for:</label>
                                <asp:DropDownList ID="DdlCategory" CssClass="bg-transparent text-blackBlue" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="text-sm lg:text-sm px-20px py-10px w-full lg:w-fit text-center flex gap-5px text-textGray border-gray border-2 font-bold rounded-30px justify-center bg-gray2">
                                <label for="city">With category:</label>
                                <asp:DropDownList ID="DdlVertical" CssClass="max-w-[130px] bg-transparent text-blackBlue" runat="server">
                                </asp:DropDownList>                                
                            </div>
                            <div class="text-sm lg:text-sm px-20px py-10px w-full lg:w-fit text-center flex gap-5px text-textGray border-gray border-2 font-bold rounded-30px justify-center bg-gray2">
                                <label for="sort">In country:</label>
                                <asp:DropDownList ID="DdlCountry" CssClass="max-w-[130px] bg-transparent text-blackBlue" runat="server">
                                </asp:DropDownList> 
                            </div>
                            <a id="aSearch" runat="server" onserverclick="aSearch_ServerClick" class="btn large font-bold text-xs lg:text-sm bg-blue text-white" type="submit">
                                <asp:Label ID="LblSearch" Text="APPLY FILTERS" runat="server" />
                            </a>
                        </div>
                    </div>
                    <div class="w-full flex items-center gap-5px justify-center">
                        <svg width="19" height="20" viewBox="0 0 19 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <g clip-path="url(#clip0_153_19581)">
                                <path d="M18.9415 5.29789C18.8039 5.02284 18.4691 4.91247 18.1946 5.04892L15.968 6.1622C15.693 6.29973 15.5815 6.63405 15.7191 6.9091C15.8564 7.18305 16.1891 7.2956 16.466 7.15807L18.6925 6.04479C18.9676 5.90726 19.079 5.57294 18.9415 5.29789Z" fill="#707E95" />
                                <path d="M18.6925 11.7286L16.466 10.6153C16.1909 10.4783 15.8572 10.5886 15.7191 10.8643C15.5815 11.1393 15.693 11.4736 15.968 11.6112L18.1946 12.7244C18.4723 12.8624 18.8046 12.7485 18.9415 12.4755C19.079 12.2004 18.9676 11.8661 18.6925 11.7286Z" fill="#707E95" />
                                <path d="M12.8026 9.4806H4.45309V6.62322C4.45309 5.08863 5.7017 3.84005 7.23625 3.84005C8.7708 3.84005 10.0194 5.08867 10.0194 6.62322V7.7365C10.0194 8.04417 10.2684 8.29314 10.5761 8.29314H12.8026C13.1103 8.29314 13.3592 8.04417 13.3592 7.7365V6.62322C13.3592 3.24701 10.6125 0.500244 7.23625 0.500244C3.86004 0.500244 1.11328 3.24701 1.11328 6.62322V9.58314C0.466761 9.8137 0 10.4258 0 11.1505V17.8301C0 18.7509 0.74909 19.5 1.66988 19.5H12.8026C13.7234 19.5 14.4725 18.7509 14.4725 17.8301V11.1505C14.4725 10.2297 13.7234 9.4806 12.8026 9.4806ZM7.79289 14.9444V16.7168C7.79289 17.0245 7.54392 17.2735 7.23625 17.2735C6.92858 17.2735 6.67961 17.0245 6.67961 16.7168V14.9444C6.03309 14.7138 5.56633 14.1018 5.56633 13.3771C5.56633 12.4562 6.31542 11.7071 7.23625 11.7071C8.15708 11.7071 8.90613 12.4562 8.90613 13.3771C8.90613 14.1018 8.43937 14.7138 7.79289 14.9444Z" fill="#707E95" />
                                <path d="M18.4433 8.33008H16.2168C15.9091 8.33008 15.6602 8.57904 15.6602 8.88672C15.6602 9.19439 15.9091 9.44336 16.2168 9.44336H18.4433C18.751 9.44336 19 9.19439 19 8.88672C19 8.57904 18.751 8.33008 18.4433 8.33008Z" fill="#707E95" />
                            </g>
                            <defs>
                                <clipPath id="clip0_153_19581">
                                    <rect width="19" height="19" fill="white" transform="translate(0 0.5)" />
                                </clipPath>
                            </defs>
                        </svg>

                        <p class="text-sm lg:text-base text-textGray">Unlock advanced search features and grow your opportunities by purchasing one of our premium products.</p>
                        <a id="aRegister" runat="server" class="text-sm lg:text-base text-blue underline inline font-bold" href="/free-sign-up">Upgrade Now!</a>
                    </div>
                </div>
            </div>
        </section>
        <section>
            <div class="container">
                <div class="gap-30px lg:gap-50px flex flex-col">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Create Partnerships & Connections</h3>
                        <p class="w-full text-base lg:text-body">Grow your indirect sales channel and connect with channel partners and IT vendors worldwide.</p>
                    </div>
                    <div class="flex flex-col lg:flex-row gap-30px lg:gap-10px justify-start lg:justify-between">
                        <div class="w-[390px] max-w-full bg-white shadow-card overflow-hidden rounded-10px">
                            <div class="pt-15px lg:pt-25px flex items-center justify-center">
                                <img src="/assets_out/images/search/partner-programs.svg" alt="Sass Partner Programs by Vendors at Elioplus" loading="lazy" width="101" height="100">
                            </div>
                            <div class="gap-10px lg:gap-15px py-10px lg:py-30px lg:pt-20px px-20px lg:px-25px text-center bg-white flex flex-col">
                                <div class="w-full flex flex-col gap-5px">
                                    <h3 class="text-base lg:text-lg font-bold font-inter">Sass Partner Programs by Vendors</h3>
                                    <p class="text-sm lg:text-base">Browse programs by vendors and partner with Resellers, Managed Service Providers (MSPs) and System Integrators (SIs).</p>
                                </div>
                                <a class="btn font-bold text-xs xl:text-sm w-full justify-center text-blue" href="/saas-partner-programs"><span>VIEW PARTNER PROGRAMS</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                        <div class="w-[390px] max-w-full bg-white shadow-card overflow-hidden rounded-10px">
                            <div class="pt-15px lg:pt-25px flex items-center justify-center">
                                <img src="/assets_out/images/search/channel-partners.svg" alt="Browse Sass Channel Partners at Elioplus" loading="lazy" width="100" height="100">
                            </div>
                            <div class="gap-10px lg:gap-15px py-10px lg:py-30px lg:pt-20px px-20px lg:px-25px text-center bg-white flex flex-col">
                                <div class="w-full flex flex-col gap-5px">
                                    <h3 class="text-base lg:text-lg font-bold font-inter">Sass Channel Partners</h3>
                                    <p class="text-sm lg:text-base">Explore channel companies that sell your products or services and belong to your indirect sales force while acting independent.</p>
                                </div>
                                <a class="btn font-bold text-xs xl:text-sm w-full justify-center text-blue" href="/search/channel-partners"><span>VIEW CHANNEL PARTNERS</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                        <div class="w-[390px] max-w-full bg-white shadow-card overflow-hidden rounded-10px">
                            <div class="pt-15px lg:pt-25px flex items-center justify-center">
                                <img src="/assets_out/images/search/partnerships.svg" alt="Browse Partnerships &amp; Technologies at Elioplus" loading="lazy" width="135" height="100">
                            </div>
                            <div class="gap-10px lg:gap-15px py-10px lg:py-30px lg:pt-20px px-20px lg:px-25px text-center bg-white flex flex-col">
                                <div class="w-full flex flex-col gap-5px">
                                    <h3 class="text-base lg:text-lg font-bold font-inter">Partnerships & Technologies</h3>
                                    <p class="text-sm lg:text-base">Search for partnerships offered by vendors who promote their product with current applications that their businesses use.</p>
                                </div>
                                <a class="btn font-bold text-xs xl:text-sm w-full justify-center text-blue" href="/partnerships"><span>VIEW PARTNERSHIPS</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                </defs></svg>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-gray">
            <div class="container">
                <div class="flex flex-col gap-40px">
                    <div class="flex items-center justify-center w-full">
                        <img src="/assets_out/images/global/cta.svg" alt="Elioplus connects software and IT vendors with channel partners" loading="lazy">
                    </div>
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">More than <span class='text-blue'>120.000</span> companies are using Elioplus</h3>
                        <p class="w-full lg:w-[854px] max-w-full text-base lg:text-body">What are you waiting for? Add your company for free and find out how you can improve your business with us.</p>
                    </div>
                    <div class="flex justify-center items-center flex-col lg:flex-row gap-10px xl:gap-20px">
                        <a id="aSignUp" runat="server" class="btn large font-bold text-sm xl:text-base bg-blue text-white">
                            <svg width="18" height="19" viewBox="0 0 18 19" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <path d="M8.95734 5.12856C10.2416 5.12856 11.2826 4.09242 11.2826 2.81428C11.2826 1.53614 10.2416 0.5 8.95734 0.5C7.67314 0.5 6.63208 1.53614 6.63208 2.81428C6.63208 4.09242 7.67314 5.12856 8.95734 5.12856Z" fill="#39B4EF" />
                                <path d="M8.95734 11.8141C10.2416 11.8141 11.2826 10.778 11.2826 9.49983C11.2826 8.22169 10.2416 7.18555 8.95734 7.18555C7.67314 7.18555 6.63208 8.22169 6.63208 9.49983C6.63208 10.778 7.67314 11.8141 8.95734 11.8141Z" fill="#39B4EF" />
                                <path d="M8.95734 18.5001C10.2416 18.5001 11.2826 17.464 11.2826 16.1859C11.2826 14.9077 10.2416 13.8716 8.95734 13.8716C7.67314 13.8716 6.63208 14.9077 6.63208 16.1859C6.63208 17.464 7.67314 18.5001 8.95734 18.5001Z" fill="#39B4EF" />
                                <path d="M2.75666 11.8141C4.04087 11.8141 5.08192 10.778 5.08192 9.49983C5.08192 8.22169 4.04087 7.18555 2.75666 7.18555C1.47245 7.18555 0.431396 8.22169 0.431396 9.49983C0.431396 10.778 1.47245 11.8141 2.75666 11.8141Z" fill="#39B4EF" />
                                <path d="M15.6746 11.8141C16.9589 11.8141 17.9999 10.778 17.9999 9.49983C17.9999 8.22169 16.9589 7.18555 15.6746 7.18555C14.3904 7.18555 13.3494 8.22169 13.3494 9.49983C13.3494 10.778 14.3904 11.8141 15.6746 11.8141Z" fill="#39B4EF" />
                            </svg>
                            <span>ADD YOUR COMPANY FOR FREE</span></a>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
