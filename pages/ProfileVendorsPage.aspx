<%@ Page Title="" Language="C#" MasterPageFile="~/pages/Elioplus.Master" AutoEventWireup="true" CodeBehind="ProfileVendorsPage.aspx.cs" Inherits="WdS.ElioPlus.pages.ProfileVendorsPage" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" id="metaDescription" runat="server" />
    <meta name="keywords" id="metaKeywords" runat="server" />
</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <main class="transition-all duration-200">
        <section class="bg-gray">
            <div class="container">
                <div class="px-30px lg:px-80px py-30px lg:px-40px gap-30px lg:gap-50px bg-white shadow-card flex flex-col rounded-20px">
                    <div class="flex w-full flex-col lg:flex-row items-center gap-20px">
                        <div class="w-[80px] h-[80px] lg:w-[150px] lg:h-[150px] border-blue border-2 p-5px flex items-center justify-center rounded-full">
                            <img class="w-full" src="/assets_out/images/company-profile/cupix.png" alt="Cupix Logo" width="150" height="150"></div>
                        <div class="flex flex-col grow gap-10px">
                            <div class="flex flex-col-reverse lg:flex-row gap-20px w-full items-center">
                                <h1 class="text-2xl lg:text-3xl font-inter font-bold">Cupix</h1>
                                <div class="flex gap-7px items-center px-10px py-5px bg-gray rounded-30px">
                                    <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                                        <g clip-path="url(#clip0_457_22)">
                                            <path d="M15.4609 6.23701C15.3627 5.93327 15.0933 5.71755 14.7746 5.68882L10.4452 5.29571L8.7332 1.28869C8.60697 0.995023 8.31949 0.804932 8.00008 0.804932C7.68066 0.804932 7.39318 0.995023 7.26695 1.28937L5.55498 5.29571L1.22489 5.68882C0.906732 5.71824 0.638017 5.93327 0.539252 6.23701C0.440487 6.54074 0.531699 6.87389 0.772375 7.08389L4.0449 9.95392L3.07991 14.2047C3.00929 14.5173 3.1306 14.8403 3.38993 15.0278C3.52933 15.1285 3.69241 15.1798 3.85687 15.1798C3.99866 15.1798 4.13931 15.1415 4.26554 15.066L8.00008 12.834L11.7332 15.066C12.0064 15.2304 12.3508 15.2154 12.6095 15.0278C12.869 14.8398 12.9902 14.5166 12.9196 14.2047L11.9546 9.95392L15.2271 7.08447C15.4678 6.87389 15.5597 6.54132 15.4609 6.23701Z" fill="#FFB618" />
                                        </g>
                                        <defs>
                                            <clipPath id="clip0_457_22">
                                                <rect width="15" height="15" fill="white" transform="translate(0.5 0.483032)" />
                                            </clipPath>
                                        </defs>
                                    </svg>

                                    <h4 class="text-xs lg:text-sm font-bold text-yellow">Featured Company</h4>
                                </div>
                            </div>
                            <div class="flex gap-20px w-full items-center flex-col lg:flex-row">
                                <div class="flex gap-7px items-center">
                                    <svg width="18" height="19" viewBox="0 0 18 19" fill="none" xmlns="http://www.w3.org/2000/svg">
                                        <path d="M8.95734 5.12856C10.2416 5.12856 11.2826 4.09242 11.2826 2.81428C11.2826 1.53614 10.2416 0.5 8.95734 0.5C7.67314 0.5 6.63208 1.53614 6.63208 2.81428C6.63208 4.09242 7.67314 5.12856 8.95734 5.12856Z" fill="#39B4EF" />
                                        <path d="M8.95734 11.8141C10.2416 11.8141 11.2826 10.778 11.2826 9.49983C11.2826 8.22169 10.2416 7.18555 8.95734 7.18555C7.67314 7.18555 6.63208 8.22169 6.63208 9.49983C6.63208 10.778 7.67314 11.8141 8.95734 11.8141Z" fill="#39B4EF" />
                                        <path d="M8.95734 18.5001C10.2416 18.5001 11.2826 17.464 11.2826 16.1859C11.2826 14.9077 10.2416 13.8716 8.95734 13.8716C7.67314 13.8716 6.63208 14.9077 6.63208 16.1859C6.63208 17.464 7.67314 18.5001 8.95734 18.5001Z" fill="#39B4EF" />
                                        <path d="M2.75666 11.8141C4.04087 11.8141 5.08192 10.778 5.08192 9.49983C5.08192 8.22169 4.04087 7.18555 2.75666 7.18555C1.47245 7.18555 0.431396 8.22169 0.431396 9.49983C0.431396 10.778 1.47245 11.8141 2.75666 11.8141Z" fill="#39B4EF" />
                                        <path d="M15.6746 11.8141C16.9589 11.8141 17.9999 10.778 17.9999 9.49983C17.9999 8.22169 16.9589 7.18555 15.6746 7.18555C14.3904 7.18555 13.3494 8.22169 13.3494 9.49983C13.3494 10.778 14.3904 11.8141 15.6746 11.8141Z" fill="#39B4EF" />
                                    </svg>

                                    <h4 class="text-xs lg:text-sm font-semibold">Company Type: <strong>Vendor</strong></h4>
                                </div>
                                <div class="flex gap-7px items-center">
                                    <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                                        <g clip-path="url(#clip0_457_22)">
                                            <path d="M15.4609 6.23701C15.3627 5.93327 15.0933 5.71755 14.7746 5.68882L10.4452 5.29571L8.7332 1.28869C8.60697 0.995023 8.31949 0.804932 8.00008 0.804932C7.68066 0.804932 7.39318 0.995023 7.26695 1.28937L5.55498 5.29571L1.22489 5.68882C0.906732 5.71824 0.638017 5.93327 0.539252 6.23701C0.440487 6.54074 0.531699 6.87389 0.772375 7.08389L4.0449 9.95392L3.07991 14.2047C3.00929 14.5173 3.1306 14.8403 3.38993 15.0278C3.52933 15.1285 3.69241 15.1798 3.85687 15.1798C3.99866 15.1798 4.13931 15.1415 4.26554 15.066L8.00008 12.834L11.7332 15.066C12.0064 15.2304 12.3508 15.2154 12.6095 15.0278C12.869 14.8398 12.9902 14.5166 12.9196 14.2047L11.9546 9.95392L15.2271 7.08447C15.4678 6.87389 15.5597 6.54132 15.4609 6.23701Z" fill="#FFB618" />
                                        </g>
                                        <defs>
                                            <clipPath id="clip0_457_22">
                                                <rect width="15" height="15" fill="white" transform="translate(0.5 0.483032)" />
                                            </clipPath>
                                        </defs>
                                    </svg>

                                    <h4 class="text-xs lg:text-sm font-semibold">Rating: <strong>5.0</strong> / <span class='text-textGray'>5.0</span></h4>
                                </div>
                                <div class="country bg-white overflow-hidden rounded-30px p-5px flex gap-5px items-center w-fit">
                                    <img class="w-[15px] lg:w-[20px]" src="/assets_out/images/flags/US.svg">
                                    <h4 class="text-xs lg:text-sm font-semibold font-bold">Country: <strong>United States</strong></h4>
                                </div>
                            </div>
                            <div class="flex-row gap-10px justify-start lg:justify-start  flex"><a class="btn font-bold text-xs xl:text-sm bg-blue text-white justify-center w-full lg:w-fit" href="../templates/partnerPortals.html"><span>JOIN AS A PARTNER</span></a></div>
                        </div>
                    </div>
                    <div class="divider"></div>
                    <div class="gap-15px lg:gap-25px flex flex-col justify-center w-full">
                        <h2 class="text-2xl lg:text-2.5xl font-bold font-inter text-left">Overview:</h2>
                        <p class="w-full text-base lg:text-xl text-left">
                            3D Digital Twin solution provider mainly targeting AEC industry customers or facilities owner customers.
                <br>
                            <br>
                            We create and deliver 3D Digital Twins based on consumer level 360 cameras.
                <br>
                            <br>
                            We have created an easy-to-deploy 3D Digital Twin platform enabling architecture, construction, and engineering professionals to manage jobsites remotely and to collaborate each other effectively.
                <br>
                            <br>
                            Cupix is the trusted partner of choice for delivering the industry’s most flexible and easiest-to-deploy 3D digital twin platform to builders and owners everywhere.
                <br>
                            <br>
                            Cupix supports the entire AEC ecosystem across the entire built-world lifecycle and serves every stakeholder’s needs including owners, contractors, project managers, BIM/VDC specialists, and property managers.
                        </p>
                    </div>
                    <div class="divider"></div>
                    <div class="gap-15px lg:gap-25px flex flex-col justify-center w-full">
                        <h2 class="text-2xl lg:text-2.5xl font-bold font-inter text-left">Industries:</h2>
                        <div class="flex-col lg:flex-row gap-10px lg:gap-50px flex justify-start w-full">
                            <div class="flex flex-col gap-20px">
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">Software</a>
                                </div>
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">Energy</a>
                                </div>
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">General Industry</a>
                                </div>
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">Oil &amp; Gas</a>
                                </div>
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">Shipping</a>
                                </div>
                            </div>
                            <div class="flex flex-col gap-20px">
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">Aerospace and Defense</a>
                                </div>
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">Engineering &amp; Construction</a>
                                </div>
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">High Tech &amp; Communications</a>
                                </div>
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">Real Estate</a>
                                </div>
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">Telecommunications</a>
                                </div>
                            </div>
                            <div class="flex flex-col gap-20px">
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">Chemicals &amp; Life Sciences</a>
                                </div>
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">Food and Beverage/CPG</a>
                                </div>
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">Manufacturing</a>
                                </div>
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">Retail</a>
                                </div>
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">Utilities</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="divider"></div>
                    <div class="gap-15px lg:gap-25px flex flex-col justify-center w-full">
                        <h2 class="text-2xl lg:text-2.5xl font-bold font-inter text-left">Market Specialization:</h2>
                        <div class="flex-col lg:flex-row gap-10px lg:gap-50px flex justify-start w-full">
                            <div class="flex gap-10px items-center">
                                <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                <h4 class="text-base lg:text-lg font-semibold">Small & mid-sized businesses (B2B)</h4>
                            </div>
                            <div class="flex gap-10px items-center">
                                <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                <h4 class="text-base lg:text-lg font-semibold">Enterprise (B2B</h4>
                            </div>
                        </div>
                    </div>
                    <div class="divider"></div>
                    <div class="gap-15px lg:gap-25px flex flex-col justify-center w-full">
                        <h2 class="text-2xl lg:text-2.5xl font-bold font-inter text-left">Partner Program available for:</h2>
                        <div class="flex-col lg:flex-row gap-10px lg:gap-50px flex justify-start w-full">
                            <div class="flex flex-col gap-20px">
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">White Label</a>
                                </div>
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">Distributor </a>
                                </div>
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">System Integrator</a>
                                </div>
                            </div>
                            <div class="flex flex-col gap-20px">
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">Reseller </a>
                                </div>
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">API Program (Developers)</a>
                                </div>
                            </div>
                            <div class="flex flex-col gap-20px">
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">Value Added Reseller (VAR)</a>
                                </div>
                                <div class="w-full flex gap-10px items-center">
                                    <div class="w-[15px] h-[15px] rounded-full bg-blue"></div>
                                    <a class="text-base lg:text-lg hover:text-blue font-semibold" href="../templates/searchResultsPartnerPrograms.html">Managed Service Provider (MSP)</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="divider"></div>
                    <div class="gap-15px lg:gap-25px flex flex-col justify-center w-full">
                        <h2 class="text-2xl lg:text-2.5xl font-bold font-inter text-left">Contact Details:</h2>
                        <div class="flex-col lg:flex-row gap-10px lg:gap-50px flex justify-start w-full">
                            <a class="w-fit flex gap-10px items-center" href="#">
                                <div class="w-[15px] h-[15px]">
                                    <img src="/assets_out/images/company-profile/location.svg"></div>
                                <h4 class="text-base lg:text-lg font-semibold">805 Veterans Blvd, Suite 305</h4>
                            </a><a class="w-fit flex gap-10px items-center" href="tel:+014083136218">
                                <div class="w-[15px] h-[15px]">
                                    <img src="/assets_out/images/company-profile/phone.svg"></div>
                                <h4 class="text-base lg:text-lg font-semibold">+014083136218</h4>
                            </a><a class="w-fit flex gap-10px items-center" href="mailto:info@cupix.com">
                                <div class="w-[15px] h-[15px]">
                                    <img src="/assets_out/images/company-profile/mail.svg"></div>
                                <h4 class="text-base lg:text-lg font-semibold">info@cupix.com</h4>
                            </a>
                        </div>
                    </div>
                    <div class="divider"></div>
                    <div class="flex-row gap-10px justify-start lg:justify-start  flex"><a class="btn large font-bold text-xs xl:text-sm bg-blue text-white justify-center w-full lg:w-fit" href="../templates/partnerPortals.html"><span>JOIN AS A PARTNER</span></a></div>
                </div>
            </div>
        </section>
        <section>
            <div class="container">
                <div class="grid-cols-1 lg:grid-cols-company gap-30px lg:gap-50px grid">
                    <div class="gap-40px flex flex-col">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Vendors you might also be interest in:</h3>
                        <div class="w-full flex flex-col gap-50px">
                            <div class="flex flex-col w-full bg-white shadow-card rounded-10px overflow-hidden">
                                <div class="flex w-full flex-col lg:flex-row border-gray border-b-2">
                                    <div class="p-20px lg:p-30px border-gray border-b-2 lg:border-b-0 flex items-center justify-center">
                                        <img src="/assets_out/images/company-item/loqate.png" alt="logo for company profile Loqate a GBG solution" loading="lazy"></div>
                                    <div class="flex flex-col grow py-30px px-25px gap-10px border-gray border-l-2">
                                        <div class="flex flex-col-reverse lg:flex-row gap-20px w-full items-center">
                                            <h3 class="text-xl lg:text-2xl font-inter font-bold">Loqate a GBG solution</h3>
                                            <div class="flex gap-7px items-center px-10px py-5px bg-gray rounded-30px">
                                                <img src="/assets_out/images/company-item/rating.svg" alt="Featured icon" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-bold text-yellow">Featured Company</h4>
                                            </div>
                                        </div>
                                        <div class="flex gap-20px w-full items-center justify-center lg:justify-start">
                                            <div class="flex gap-7px items-center">
                                                <img src="/assets_out/images/company-item/rating.svg" alt="Rating icon" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-semibold">5.0</h4>
                                            </div>
                                            <div class="country bg-white overflow-hidden rounded-30px p-5px flex gap-5px items-center w-fit">
                                                <img class="w-[15px] lg:w-[20px]" src="/assets_out/images/flags/US.svg" alt="" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-semibold font-bold">United States</h4>
                                            </div>
                                            <div class="flex gap-7px items-center">
                                                <img src="/assets_out/images/company-item/location.svg" alt="location icon" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-semibold">New York</h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="p-25px gap-15px lg:gap-20px border-gray border-b-2 flex flex-col w-full">
                                    <div class="gap-10px lg:gap-15px flex flex-col w-full">
                                        <div class="px-15px py-20px lg:py-10px gap-15px flex-col lg:flex-row w-full bg-gray rounded-10px flex items-center">
                                            <div class="flex gap-7px items-center">
                                                <img src="/assets_out/images/company-item/categories.svg" alt="categories icon" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-semibold">Categories:</h4>
                                            </div>
                                            <div class="hidden lg:flex grow gap-10px flex-wrap justify-center lg:justify-start items-group"><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Advertising/Marketing</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Email Marketing</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">SEO & SEM</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Engagement Tools</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">CRM</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Advertising/Marketing</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Email Marketing</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">SEO & SEM</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Engagement Tools</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">CRM</a><a class="moreTrigger text-xs lg:text-xs py-5px px-10px bg-hawkesBlue rounded-20px text-blue" href="#"><span class="number">+5</span><span class="labelText">more</span></a></div>
                                        </div>
                                        <div class="px-15px py-20px lg:py-10px gap-15px flex-col lg:flex-row w-full bg-gray rounded-10px flex items-center">
                                            <div class="flex gap-7px items-center">
                                                <img src="/assets_out/images/company-item/partner-programs.svg" alt="partner programs icon" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-semibold">Partner Programs:</h4>
                                            </div>
                                            <div class="hidden lg:flex grow gap-10px flex-wrap justify-center lg:justify-start items-group"><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Reseller</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Distributor</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Managed Service Providers (MSPs)</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Reseller</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Distributor</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Managed Service Providers (MSPs)</a><a class="moreTrigger text-xs lg:text-xs py-5px px-10px bg-hawkesBlue rounded-20px text-blue" href="#"><span class="number">+2</span><span class="labelText">more</span></a></div>
                                        </div>
                                    </div>
                                    <p class="text-sm lg:text-base text-center lg:text-left text-textGray">Loqate, GBG's location intelligence business unit, is a leading developer of Global Location Data Solutions, including Address Verification and Geocode. WHY PARTNER Loqate partners deliver a richer global data product and increase their revenues...</p>
                                </div>
                                <div class="py-15px px-25px flex-col lg:flex-row gap-10px justify-start lg:justify-end  flex"><a class="btn large font-bold text-xs xl:text-sm bg-white text-blue justify-center w-full lg:w-fit" href="../templates/companyProfileVendor.html"><span>VIEW COMPANY PROFILE</span></a><a class="btn large font-bold text-xs xl:text-sm bg-yellow text-white justify-center w-full lg:w-fit" href="#"><span>VIEW WEBSITE</span></a><a class="btn large font-bold text-xs xl:text-sm bg-blue text-white justify-center w-full lg:w-fit" href="#"><span>JOIN AS PARTNER</span></a></div>
                            </div>
                            <div class="flex flex-col w-full bg-white shadow-card rounded-10px overflow-hidden">
                                <div class="flex w-full flex-col lg:flex-row border-gray border-b-2">
                                    <div class="p-20px lg:p-30px border-gray border-b-2 lg:border-b-0 flex items-center justify-center">
                                        <img src="/assets_out/images/company-item/vcc.png" alt="logo for company profile VCC Live" loading="lazy"></div>
                                    <div class="flex flex-col grow py-30px px-25px gap-10px border-gray border-l-2">
                                        <div class="flex flex-col-reverse lg:flex-row gap-20px w-full items-center">
                                            <h3 class="text-xl lg:text-2xl font-inter font-bold">VCC Live</h3>
                                            <div class="flex gap-7px items-center px-10px py-5px bg-gray rounded-30px">
                                                <img src="/assets_out/images/company-item/rating.svg" alt="Featured icon" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-bold text-yellow">Featured Company</h4>
                                            </div>
                                        </div>
                                        <div class="flex gap-20px w-full items-center justify-center lg:justify-start">
                                            <div class="flex gap-7px items-center">
                                                <img src="/assets_out/images/company-item/rating.svg" alt="Rating icon" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-semibold">5.0</h4>
                                            </div>
                                            <div class="country bg-white overflow-hidden rounded-30px p-5px flex gap-5px items-center w-fit">
                                                <img class="w-[15px] lg:w-[20px]" src="/assets_out/images/flags/US.svg" alt="" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-semibold font-bold">United States</h4>
                                            </div>
                                            <div class="flex gap-7px items-center">
                                                <img src="/assets_out/images/company-item/location.svg" alt="location icon" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-semibold">New York</h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="p-25px gap-15px lg:gap-20px border-gray border-b-2 flex flex-col w-full">
                                    <div class="gap-10px lg:gap-15px flex flex-col w-full">
                                        <div class="px-15px py-20px lg:py-10px gap-15px flex-col lg:flex-row w-full bg-gray rounded-10px flex items-center">
                                            <div class="flex gap-7px items-center">
                                                <img src="/assets_out/images/company-item/categories.svg" alt="categories icon" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-semibold">Categories:</h4>
                                            </div>
                                            <div class="hidden lg:flex grow gap-10px flex-wrap justify-center lg:justify-start items-group"><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Advertising/Marketing</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Email Marketing</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">SEO & SEM</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Engagement Tools</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">CRM</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Advertising/Marketing</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Email Marketing</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">SEO & SEM</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Engagement Tools</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">CRM</a><a class="moreTrigger text-xs lg:text-xs py-5px px-10px bg-hawkesBlue rounded-20px text-blue" href="#"><span class="number">+5</span><span class="labelText">more</span></a></div>
                                        </div>
                                        <div class="px-15px py-20px lg:py-10px gap-15px flex-col lg:flex-row w-full bg-gray rounded-10px flex items-center">
                                            <div class="flex gap-7px items-center">
                                                <img src="/assets_out/images/company-item/partner-programs.svg" alt="partner programs icon" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-semibold">Partner Programs:</h4>
                                            </div>
                                            <div class="hidden lg:flex grow gap-10px flex-wrap justify-center lg:justify-start items-group"><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Reseller</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Distributor</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Managed Service Providers (MSPs)</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Reseller</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Distributor</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Managed Service Providers (MSPs)</a><a class="moreTrigger text-xs lg:text-xs py-5px px-10px bg-hawkesBlue rounded-20px text-blue" href="#"><span class="number">+2</span><span class="labelText">more</span></a></div>
                                        </div>
                                    </div>
                                    <p class="text-sm lg:text-base text-center lg:text-left text-textGray">Move your Contact Center to the Cloud with secure and assisted implementation. Talk to our experts now and get a personalized deployment plan. With VCC Live, helpdesk and customer support...</p>
                                </div>
                                <div class="py-15px px-25px flex-col lg:flex-row gap-10px justify-start lg:justify-end  flex"><a class="btn large font-bold text-xs xl:text-sm bg-white text-blue justify-center w-full lg:w-fit" href="../templates/companyProfileVendor.html"><span>VIEW COMPANY PROFILE</span></a><a class="btn large font-bold text-xs xl:text-sm bg-yellow text-white justify-center w-full lg:w-fit" href="#"><span>VIEW WEBSITE</span></a><a class="btn large font-bold text-xs xl:text-sm bg-blue text-white justify-center w-full lg:w-fit" href="#"><span>JOIN AS PARTNER</span></a></div>
                            </div>
                            <div class="flex flex-col w-full bg-white shadow-card rounded-10px overflow-hidden">
                                <div class="flex w-full flex-col lg:flex-row border-gray border-b-2">
                                    <div class="p-20px lg:p-30px border-gray border-b-2 lg:border-b-0 flex items-center justify-center">
                                        <img src="/assets_out/images/company-item/marketman.png" alt="logo for company profile Marketman" loading="lazy"></div>
                                    <div class="flex flex-col grow py-30px px-25px gap-10px border-gray border-l-2">
                                        <div class="flex flex-col-reverse lg:flex-row gap-20px w-full items-center">
                                            <h3 class="text-xl lg:text-2xl font-inter font-bold">Marketman</h3>
                                            <div class="flex gap-7px items-center px-10px py-5px bg-gray rounded-30px">
                                                <img src="/assets_out/images/company-item/rating.svg" alt="Featured icon" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-bold text-yellow">Featured Company</h4>
                                            </div>
                                        </div>
                                        <div class="flex gap-20px w-full items-center justify-center lg:justify-start">
                                            <div class="flex gap-7px items-center">
                                                <img src="/assets_out/images/company-item/rating.svg" alt="Rating icon" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-semibold">5.0</h4>
                                            </div>
                                            <div class="country bg-white overflow-hidden rounded-30px p-5px flex gap-5px items-center w-fit">
                                                <img class="w-[15px] lg:w-[20px]" src="/assets_out/images/flags/US.svg" alt="" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-semibold font-bold">United States</h4>
                                            </div>
                                            <div class="flex gap-7px items-center">
                                                <img src="/assets_out/images/company-item/location.svg" alt="location icon" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-semibold">New York</h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="p-25px gap-15px lg:gap-20px border-gray border-b-2 flex flex-col w-full">
                                    <div class="gap-10px lg:gap-15px flex flex-col w-full">
                                        <div class="px-15px py-20px lg:py-10px gap-15px flex-col lg:flex-row w-full bg-gray rounded-10px flex items-center">
                                            <div class="flex gap-7px items-center">
                                                <img src="/assets_out/images/company-item/categories.svg" alt="categories icon" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-semibold">Categories:</h4>
                                            </div>
                                            <div class="hidden lg:flex grow gap-10px flex-wrap justify-center lg:justify-start items-group"><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Advertising/Marketing</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Email Marketing</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">SEO & SEM</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Engagement Tools</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">CRM</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Advertising/Marketing</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Email Marketing</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">SEO & SEM</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Engagement Tools</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">CRM</a><a class="moreTrigger text-xs lg:text-xs py-5px px-10px bg-hawkesBlue rounded-20px text-blue" href="#"><span class="number">+5</span><span class="labelText">more</span></a></div>
                                        </div>
                                        <div class="px-15px py-20px lg:py-10px gap-15px flex-col lg:flex-row w-full bg-gray rounded-10px flex items-center">
                                            <div class="flex gap-7px items-center">
                                                <img src="/assets_out/images/company-item/partner-programs.svg" alt="partner programs icon" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-semibold">Partner Programs:</h4>
                                            </div>
                                            <div class="hidden lg:flex grow gap-10px flex-wrap justify-center lg:justify-start items-group"><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Reseller</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Distributor</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Managed Service Providers (MSPs)</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Reseller</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Distributor</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Managed Service Providers (MSPs)</a><a class="moreTrigger text-xs lg:text-xs py-5px px-10px bg-hawkesBlue rounded-20px text-blue" href="#"><span class="number">+2</span><span class="labelText">more</span></a></div>
                                        </div>
                                    </div>
                                    <p class="text-sm lg:text-base text-center lg:text-left text-textGray">MarketMan is a cloud-based inventory management and purchasing app focused on streamlining procurement, delivery and accounting. Our all-in-one restaurant management platform offers...</p>
                                </div>
                                <div class="py-15px px-25px flex-col lg:flex-row gap-10px justify-start lg:justify-end  flex"><a class="btn large font-bold text-xs xl:text-sm bg-white text-blue justify-center w-full lg:w-fit" href="../templates/companyProfileVendor.html"><span>VIEW COMPANY PROFILE</span></a><a class="btn large font-bold text-xs xl:text-sm bg-yellow text-white justify-center w-full lg:w-fit" href="#"><span>VIEW WEBSITE</span></a><a class="btn large font-bold text-xs xl:text-sm bg-blue text-white justify-center w-full lg:w-fit" href="#"><span>JOIN AS PARTNER</span></a></div>
                            </div>
                            <div class="flex flex-col w-full bg-white shadow-card rounded-10px overflow-hidden">
                                <div class="flex w-full flex-col lg:flex-row border-gray border-b-2">
                                    <div class="p-20px lg:p-30px border-gray border-b-2 lg:border-b-0 flex items-center justify-center">
                                        <img src="/assets_out/images/company-item/claritytel.png" alt="logo for company profile ClarityTel" loading="lazy"></div>
                                    <div class="flex flex-col grow py-30px px-25px gap-10px border-gray border-l-2">
                                        <div class="flex flex-col-reverse lg:flex-row gap-20px w-full items-center">
                                            <h3 class="text-xl lg:text-2xl font-inter font-bold">ClarityTel</h3>
                                            <div class="flex gap-7px items-center px-10px py-5px bg-gray rounded-30px">
                                                <img src="/assets_out/images/company-item/rating.svg" alt="Featured icon" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-bold text-yellow">Featured Company</h4>
                                            </div>
                                        </div>
                                        <div class="flex gap-20px w-full items-center justify-center lg:justify-start">
                                            <div class="flex gap-7px items-center">
                                                <img src="/assets_out/images/company-item/rating.svg" alt="Rating icon" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-semibold">5.0</h4>
                                            </div>
                                            <div class="country bg-white overflow-hidden rounded-30px p-5px flex gap-5px items-center w-fit">
                                                <img class="w-[15px] lg:w-[20px]" src="/assets_out/images/flags/US.svg" alt="" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-semibold font-bold">United States</h4>
                                            </div>
                                            <div class="flex gap-7px items-center">
                                                <img src="/assets_out/images/company-item/location.svg" alt="location icon" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-semibold">New York</h4>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="p-25px gap-15px lg:gap-20px border-gray border-b-2 flex flex-col w-full">
                                    <div class="gap-10px lg:gap-15px flex flex-col w-full">
                                        <div class="px-15px py-20px lg:py-10px gap-15px flex-col lg:flex-row w-full bg-gray rounded-10px flex items-center">
                                            <div class="flex gap-7px items-center">
                                                <img src="/assets_out/images/company-item/categories.svg" alt="categories icon" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-semibold">Categories:</h4>
                                            </div>
                                            <div class="hidden lg:flex grow gap-10px flex-wrap justify-center lg:justify-start items-group"><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Advertising/Marketing</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Email Marketing</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">SEO & SEM</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Engagement Tools</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">CRM</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Advertising/Marketing</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Email Marketing</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">SEO & SEM</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Engagement Tools</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">CRM</a><a class="moreTrigger text-xs lg:text-xs py-5px px-10px bg-hawkesBlue rounded-20px text-blue" href="#"><span class="number">+5</span><span class="labelText">more</span></a></div>
                                        </div>
                                        <div class="px-15px py-20px lg:py-10px gap-15px flex-col lg:flex-row w-full bg-gray rounded-10px flex items-center">
                                            <div class="flex gap-7px items-center">
                                                <img src="/assets_out/images/company-item/partner-programs.svg" alt="partner programs icon" loading="lazy">
                                                <h4 class="text-xs lg:text-sm font-semibold">Partner Programs:</h4>
                                            </div>
                                            <div class="hidden lg:flex grow gap-10px flex-wrap justify-center lg:justify-start items-group"><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Reseller</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Distributor</a><a class="text-xs lg:text-xs py-5px px-10px bg-blue rounded-20px text-white" href="#">Managed Service Providers (MSPs)</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Reseller</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Distributor</a><a class="text-xs lg:text-xs py-5px px-10px extra hidden bg-blue rounded-20px text-white" href="#">Managed Service Providers (MSPs)</a><a class="moreTrigger text-xs lg:text-xs py-5px px-10px bg-hawkesBlue rounded-20px text-blue" href="#"><span class="number">+2</span><span class="labelText">more</span></a></div>
                                        </div>
                                    </div>
                                    <p class="text-sm lg:text-base text-center lg:text-left text-textGray">ClarityTel is a leading nationwide Hosted IP PBX and SIP Trunk provider. Since 2003, ClarityTel has leveraged its expertise in telephony engineering and software design to deliver enhanced tele...</p>
                                </div>
                                <div class="py-15px px-25px flex-col lg:flex-row gap-10px justify-start lg:justify-end  flex"><a class="btn large font-bold text-xs xl:text-sm bg-white text-blue justify-center w-full lg:w-fit" href="../templates/companyProfileVendor.html"><span>VIEW COMPANY PROFILE</span></a><a class="btn large font-bold text-xs xl:text-sm bg-yellow text-white justify-center w-full lg:w-fit" href="#"><span>VIEW WEBSITE</span></a><a class="btn large font-bold text-xs xl:text-sm bg-blue text-white justify-center w-full lg:w-fit" href="#"><span>JOIN AS PARTNER</span></a></div>
                            </div>
                        </div>
                    </div>
                    <div>
                        <div class="relative h-full">
                            <aside class="sidebar-container sticky top-[100px] pb-0 lg:pb-80px flex flex-col gap-50px">
                                <div class="flex flex-col gap-15px w-full justify-center items-center">
                                    <img class="shadow-card rounded-10px w-fit" src="/assets_out/images/banners/acronis-banner.png" alt="Acronis sponsored banner" loading="lazy">
                                    <div class="flex flex-col gap-7px">
                                        <div class="flex gap-7px items-center justify-center">
                                            <img src="/assets_out/images/global/info-dark.svg" alt="info icon" loading="lazy">
                                            <h4 class="text-base font-bold">Advertisement</h4>
                                        </div>
                                        <p class="text-textGray text-sm text-center">Interested in advertising on our platform? Reach out:</p>
                                        <a class="text-center text-blue font-bold text-base" href="../templates/contactUs.html">Contact us!</a>
                                    </div>
                                </div>
                                <div class="py-25px px-20px flex flex-col gap-15px bg-white shadow-card w-full rounded-10px">
                                    <h4 class="text-base font-bold">Related Categories:</h4>
                                    <div class="divider"></div>
                                    <div class="flex flex-col gap-5px w-full"><a class="text-sm font-semibold underline text-blue" href="../templates/searchResultsPartnerPrograms.html">Affiliate Marketing</a><a class="text-sm font-semibold underline text-blue" href="../templates/searchResultsPartnerPrograms.html">Campaign Management</a><a class="text-sm font-semibold underline text-blue" href="../templates/searchResultsPartnerPrograms.html">Content Marketing</a><a class="text-sm font-semibold underline text-blue" href="../templates/searchResultsPartnerPrograms.html">Sales Intelligence</a><a class="text-sm font-semibold underline text-blue" href="../templates/searchResultsPartnerPrograms.html">Sales Process Management</a><a class="text-sm font-semibold underline text-blue" href="../templates/searchResultsPartnerPrograms.html">SEO & SEM</a></div>
                                </div>
                            </aside>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
