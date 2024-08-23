<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="AboutUsPage.aspx.cs" Inherits="WdS.ElioPlus.pages.AboutUsPage" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Meet the team behind Elioplus and read our story." />
    <meta name="keywords" content="elioplus team, elioplus about us" />

</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <main class="transition-all duration-200">
        <section class="bg-gray opener">
            <div class="container">
                <div class="grid-cols-1 lg:grid-cols-2 grid gap-30px">
                    <div class="row-start-2 lg:row-start-1 gap-20px lg:gap-30px flex flex-col">
                        <div class="text-center lg:text-left gap-10px lg:gap-15px flex flex-col">
                            <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">About ElioPlus</h1>
                            <h2 class="text-2xl lg:text-3xl font-bold font-inter text-blue">Who we are and what we do.</h2>
                        </div>
                        <p class="text-base lg:text-body text-center lg:text-left">Elioplus is a B2B business development platform that enables software, SaaS and cloud vendors to recruit and manage their channel partners with our solutions. We use over 15 main criteria in order to match quickly, effectively and with accuracy vendors with channel partners and help them establish valuable partnerships and expand their network.</p>
                    </div>
                    <img src="/assets_out/images/aboutUs/aboutOpener.svg" alt="About Elioplus the B2B network for channel partners and software vendors">
                </div>
            </div>
            <div class="mt-30px wave">
                <img src="/assets_out/images/global/wave.svg" alt="Elioplus decorative wave"></div>
        </section>
        <section>
            <div class="container">
                <div class="gap-80px lg:gap-160px flex flex-col">
                    <div class="grid-cols-1 lg:grid-cols-2 text-img-row grid gap-30px">
                        <div class="row-start-2 lg:row-start-1 gap-20px lg:gap-30px flex flex-col">
                            <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Overview</h3>
                            <p class="w-full text-base lg:text-body text-center lg:text-left">Since our beginning with have worked with both startup companies to help them kick off their partner program and attract their first partners up to enterprise level organizations that rely on our platform to further increase their network around the world.
                                <br>
                                <br>
                                We provide channel managers with tools to identify and partner with the best candidate companies and seamlessly optimize their network under the same roof. Partner management as it should be: <strong class='text-blue'>Effortless and affordable</strong>.</p>
                        </div>
                        <div class="flex items-center">
                            <img src="/assets_out/images/aboutUs/aboutOverview.svg" alt="Overview of Elioplus company" loading="lazy"></div>
                    </div>
                    <div class="grid-cols-1 lg:grid-cols-2 text-img-row grid gap-30px">
                        <div class="flex items-center">
                            <img src="/assets_out/images/aboutUs/aboutOurVision.svg" alt="The vision of an effective and effortless partner management tool" loading="lazy"></div>
                        <div class="gap-20px lg:gap-30px flex flex-col">
                            <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Our Vision</h3>
                            <p class="w-full text-base lg:text-body text-center lg:text-left">We envisioned a simple yet effective partner management tool accessible to small and medium businesses ready to be used without complex processes nor breaking the bank.
                                <br>
                                <br>
                                Channel managers can save time by connecting their favorite applications and move data around seamlessly while having insights about the performance and activity of their partners.
                                <br>
                                <br>
                                Now you can recruit, onboard, manage and train your channel partners from a single solution.</p>
                        </div>
                    </div>
                    <div class="flex flex-col gap-40px">
                        <div class="flex flex-col gap-10px text-center items-center">
                            <h3 class="text-2xl lg:text-3xl font-bold font-inter">Why choose ElioPlus?</h3>
                            <p class="w-full text-base lg:text-body">We provide channel partners with the same tools to manage all the tech partnerships from a single dashboard without signing up or logging in to multiple partner portals and wasting valuable time and resources in the process. As a channel company you can manage all your sales data, reporting, commissions, training and communications from a single point and connect all your team members in the same account for better visibility and performance.</p>
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
                                <span>CREATE YOUR ACCOUNT FOR FREE</span></a>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-white">
            <div class="container">
                <div class="gap-50px lg:gap-80px flex flex-col">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">A roadmap to success</h3>
                        <p class="w-full text-base lg:text-body">Our journey began in 2015, since then we have worked with hundreds of companies, small and large, to help them on their indirect sales channel. We are constantly expanding by focusing on development and growth of the company & our clients</p>
                    </div>
                    <div class="timeline flex-col lg:flex-row text-left lg:text-center gap-20px lg:gap-0 pl-40px lg:pl-0 flex justify-between w-full relative">
                        <div class="timeline-item flex flex-col gap-0 lg:gap-15px w-full lg:w-[150px] items-start lg:items-center relative z-1 pl-40px lg:pl-0">
                            <h4 class="text-sm lg:text-base font-semibold">Elioplus Incorporation</h4>
                            <div class="w-[30px] h-[30px] rounded-full hidden lg:block bg-blue"></div>
                            <h5 class="text-sm lg:text-base text-textGray">Aug. 2015</h5>
                        </div>
                        <div class="timeline-item flex flex-col gap-0 lg:gap-15px w-full lg:w-[150px] items-start lg:items-center relative z-1 pl-40px lg:pl-0">
                            <h4 class="text-sm lg:text-base font-semibold">Launched Partner Recruitment</h4>
                            <div class="w-[30px] h-[30px] rounded-full hidden lg:block bg-blue"></div>
                            <h5 class="text-sm lg:text-base text-textGray">Mar. 2016</h5>
                        </div>
                        <div class="timeline-item flex flex-col gap-0 lg:gap-15px w-full lg:w-[150px] items-start lg:items-center relative z-1 pl-40px lg:pl-0">
                            <h4 class="text-sm lg:text-base font-semibold">Reached 100+ Clients</h4>
                            <div class="w-[30px] h-[30px] rounded-full hidden lg:block bg-blue"></div>
                            <h5 class="text-sm lg:text-base text-textGray">Apr. 2018</h5>
                        </div>
                        <div class="timeline-item flex flex-col gap-0 lg:gap-15px w-full lg:w-[150px] items-start lg:items-center relative z-1 pl-40px lg:pl-0">
                            <h4 class="text-sm lg:text-base font-semibold">Launched PRM Software</h4>
                            <div class="w-[30px] h-[30px] rounded-full hidden lg:block bg-blue"></div>
                            <h5 class="text-sm lg:text-base text-textGray">May. 2019</h5>
                        </div>
                        <div class="timeline-item flex flex-col gap-0 lg:gap-15px w-full lg:w-[150px] items-start lg:items-center relative z-1 pl-40px lg:pl-0">
                            <h4 class="text-sm lg:text-base font-semibold">Launched ITX Marketplace</h4>
                            <div class="w-[30px] h-[30px] rounded-full hidden lg:block bg-blue"></div>
                            <h5 class="text-sm lg:text-base text-textGray">Jun. 2022</h5>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-blue bg-particles bg-cover">
            <div class="container">
                <div class="gap-30px lg:gap-50px flex flex-col">
                    <div class="flex flex-col gap-10px text-center items-center text-white">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Leadership Team</h3>
                        <p class="w-[780px] max-w-full text-base lg:text-body">Meet the team behind the ElioPlus who collectively responsible for the success of the company.</p>
                    </div>
                    <div class="flex-col lg:flex-row gap-30px lg:gap-50px flex text-center items-center justify-center">
                        <div class="w-[250px] max-w-full py-15px lg:py-30px bg-white rounded-10px shadow-card flex flex-col gap-15px items-center">
                            <div class="rounded-full bg-lightBlue2 flex items-center justify-center">
                                <img class="rounded-full border-2 border-blue" src="/assets_out/images/aboutUs/elias.png" alt="Ilias Ndreu - CEO &amp; Business Development at Elioplus"></div>
                            <div class="flex flex-col gap-5px">
                                <h4 class="text-base lg:text-[20px] font-bold">Ilias Ndreu</h4>
                                <p class="text-sm text-textGray">CEO Business Development</p>
                            </div>
                            <div class="flex flex-row gap-20px items-center">
                                <a class="socialLink rounded-full flex items-center justify-center" href="https://www.linkedin.com/in/iliasndreu" target="_blank">
                                    <svg width="15" height="16" viewBox="0 0 15 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                                        <path d="M13.3608 13.7186V9.4994C13.3608 7.4258 12.9144 5.8418 10.4952 5.8418C9.32882 5.8418 8.55122 6.4754 8.23442 7.0802H8.20562V6.029H5.91602V13.7186H8.30642V9.9026C8.30642 8.8946 8.49362 7.9298 9.73202 7.9298C10.956 7.9298 10.9704 9.0674 10.9704 9.9602V13.7042H13.3608V13.7186Z" fill="#39B4EF" />
                                        <path d="M2.02832 6.0293H4.41872V13.7189H2.02832V6.0293Z" fill="#39B4EF" />
                                        <path d="M3.22322 2.19873C2.46002 2.19873 1.84082 2.81793 1.84082 3.58113C1.84082 4.34433 2.46002 4.97793 3.22322 4.97793C3.98642 4.97793 4.60562 4.34433 4.60562 3.58113C4.60562 2.81793 3.98642 2.19873 3.22322 2.19873V2.19873Z" fill="#39B4EF" />
                                    </svg>
                                </a>
                            </div>
                        </div>
                        <div class="w-[250px] max-w-full py-15px lg:py-30px bg-white rounded-10px shadow-card flex flex-col gap-15px items-center">
                            <div class="rounded-full bg-lightBlue2 flex items-center justify-center">
                                <img class="rounded-full border-2 border-blue" src="/assets_out/images/aboutUs/vaggelis.png" alt="Vagelis Varvitsiotis - CTO &amp; Lead Developer at Elioplus"></div>
                            <div class="flex flex-col gap-5px">
                                <h4 class="text-base lg:text-[20px] font-bold">Vagelis Varvitsiotis</h4>
                                <p class="text-sm text-textGray">CTO & Lead Developer at Elioplus</p>
                            </div>
                            <div class="flex flex-row gap-20px items-center">
                                <a class="socialLink rounded-full flex items-center justify-center" href="https://www.linkedin.com/in/evagelos-varvitsiotis-27b70152/" target="_blank">
                                    <svg width="15" height="16" viewBox="0 0 15 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                                        <path d="M13.3608 13.7186V9.4994C13.3608 7.4258 12.9144 5.8418 10.4952 5.8418C9.32882 5.8418 8.55122 6.4754 8.23442 7.0802H8.20562V6.029H5.91602V13.7186H8.30642V9.9026C8.30642 8.8946 8.49362 7.9298 9.73202 7.9298C10.956 7.9298 10.9704 9.0674 10.9704 9.9602V13.7042H13.3608V13.7186Z" fill="#39B4EF" />
                                        <path d="M2.02832 6.0293H4.41872V13.7189H2.02832V6.0293Z" fill="#39B4EF" />
                                        <path d="M3.22322 2.19873C2.46002 2.19873 1.84082 2.81793 1.84082 3.58113C1.84082 4.34433 2.46002 4.97793 3.22322 4.97793C3.98642 4.97793 4.60562 4.34433 4.60562 3.58113C4.60562 2.81793 3.98642 2.19873 3.22322 2.19873V2.19873Z" fill="#39B4EF" />
                                    </svg>
                                </a>
                            </div>
                        </div>
                        <div class="w-[250px] max-w-full py-15px lg:py-30px bg-white rounded-10px shadow-card flex flex-col gap-15px items-center">
                            <div class="rounded-full bg-lightBlue2 flex items-center justify-center">
                                <img class="rounded-full border-2 border-blue" src="/assets_out/images/aboutUs/chris.png" alt="Christos Mantzikos - Marketing Community and Sales at Elioplus"></div>
                            <div class="flex flex-col gap-5px">
                                <h4 class="text-base lg:text-[20px] font-bold">Christos Mantzikos</h4>
                                <p class="text-sm text-textGray">Marketing Community and Sales</p>
                            </div>
                            <div class="flex flex-row gap-20px items-center">
                                <a class="socialLink rounded-full flex items-center justify-center" href="https://www.linkedin.com/in/chrismantzikos" target="_blank">
                                    <svg width="15" height="16" viewBox="0 0 15 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                                        <path d="M13.3608 13.7186V9.4994C13.3608 7.4258 12.9144 5.8418 10.4952 5.8418C9.32882 5.8418 8.55122 6.4754 8.23442 7.0802H8.20562V6.029H5.91602V13.7186H8.30642V9.9026C8.30642 8.8946 8.49362 7.9298 9.73202 7.9298C10.956 7.9298 10.9704 9.0674 10.9704 9.9602V13.7042H13.3608V13.7186Z" fill="#39B4EF" />
                                        <path d="M2.02832 6.0293H4.41872V13.7189H2.02832V6.0293Z" fill="#39B4EF" />
                                        <path d="M3.22322 2.19873C2.46002 2.19873 1.84082 2.81793 1.84082 3.58113C1.84082 4.34433 2.46002 4.97793 3.22322 4.97793C3.98642 4.97793 4.60562 4.34433 4.60562 3.58113C4.60562 2.81793 3.98642 2.19873 3.22322 2.19873V2.19873Z" fill="#39B4EF" />
                                    </svg>
                                </a><a class="socialLink rounded-full flex items-center justify-center" href="https://twitter.com/Xristos13magic" target="_blank">
                                    <svg width="15" height="16" viewBox="0 0 15 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                                        <g clip-path="url(#clip0_75_24495)">
                                            <path d="M14.8004 3.49399C14.2649 3.72889 13.6943 3.88459 13.0994 3.96019C13.7114 3.59479 14.1785 3.02059 14.3981 2.32849C13.8275 2.66869 13.1975 2.90899 12.5261 3.04309C11.9843 2.46619 11.2121 2.10889 10.3697 2.10889C8.73529 2.10889 7.41949 3.43549 7.41949 5.06179C7.41949 5.29579 7.43929 5.52079 7.48789 5.73499C5.03359 5.61529 2.86189 4.43899 1.40299 2.64709C1.14829 3.08899 0.998891 3.59479 0.998891 4.13929C0.998891 5.16169 1.52539 6.06799 2.31019 6.59269C1.83589 6.58369 1.37059 6.44599 0.976391 6.22909C0.976391 6.23809 0.976391 6.24979 0.976391 6.26149C0.976391 7.69609 1.99969 8.88769 3.34159 9.16219C3.10129 9.22789 2.83939 9.25939 2.56759 9.25939C2.37859 9.25939 2.18779 9.24859 2.00869 9.20899C2.39119 10.3781 3.47659 11.2376 4.76719 11.2655C3.76279 12.0512 2.48749 12.5246 1.10689 12.5246C0.864791 12.5246 0.632591 12.5138 0.400391 12.4841C1.70809 13.3274 3.25789 13.8089 4.92919 13.8089C10.3616 13.8089 13.3316 9.30889 13.3316 5.40829C13.3316 5.27779 13.3271 5.15179 13.3208 5.02669C13.9067 4.61089 14.399 4.09159 14.8004 3.49399Z" fill="#39B4EF" />
                                        </g>
                                        <defs>
                                            <clipPath id="clip0_75_24495">
                                                <rect width="14.4" height="14.4" fill="white" transform="translate(0.400391 0.758789)" />
                                            </clipPath>
                                        </defs>
                                    </svg>
                                </a>
                            </div>
                        </div>
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
                            <a id="aPartnerRecruitment" runat="server" class="w-[380px] max-w-full bg-white shadow-card overflow-hidden rounded-10px">
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
                            <a id="aPRMSoftware" runat="server" class="w-[380px] max-w-full bg-white shadow-card overflow-hidden rounded-10px">
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
                            <a id="aIntentSignals" runat="server" class="w-[380px] max-w-full bg-white shadow-card overflow-hidden rounded-10px">
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
                            <a id="aReferralSoftware" runat="server" class="w-[380px] max-w-full bg-white shadow-card overflow-hidden rounded-10px">
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
                            <a id="aMAMarketplace" runat="server" class="w-[380px] max-w-full bg-white shadow-card overflow-hidden rounded-10px">
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
