<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="IntentSignalPage.aspx.cs" Inherits="WdS.ElioPlus.pages.IntentSignalPage" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Get quotation requests and intent data for thousands of products and technologies exclusively for local IT companies around the world." />
    <meta name="keywords" content="lead generation, intent data, quotes for IT companies" />
</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <main class="transition-all duration-200">
        <header class="bg-blue text-white shadow-lg z-10 sticky top-[63px] lg:top-[72px] xl:top-[92px] left-0 w-screen py-0 lg:py-15px">
            <div class="container">
                <div class="flex justify-between">
                    <div class="py-5px px-5px pr-[15px] hidden lg:flex items-center gap-5px bg-blueDark rounded-30px">
                        <img src="/assets_out/images/global/product-circles.svg" alt="Referral Program software product" width="30" height="30">
                        <div class="text-sm font-inter">Product:&nbsp;<strong>Intent Signals</strong></div>
                    </div>
                    <nav class="flex gap-10px xl:gap-15px relative items-center w-full lg:w-fit" id="productNavigation">
                        <div class="w-full lg:w-fit overflow-x-auto">
                            <ul class="text-sm flex gap-15px flex-wrap w-max py-15px lg:py-0">
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#overview">Overview</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#clients">Our Clients</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#features">Features</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#leads">Leads</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#howItWorks">How it works</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#whyIntent">Why Intent</a></li>
                                <li class="flex items-center"><a class="transition-all duration-200 hover:text-yellow flex items-center gap-5px font-bold text-white go-to" href="#pricing">Pricing</a></li>
                            </ul>
                        </div>
                        <a class="btn hidden lg:flex font-bold text-sm bg-yellow text-white w-fit" target="_blank" href="https://calendly.com/elioplus"><span>GET STARTED</span></a>
                    </nav>
                </div>
            </div>
        </header>
        <section class="bg-gradient-product opener" id="overview">
            <div class="container">
                <div class="grid-cols-1 lg:grid-cols-2 grid gap-30px">
                    <div class="row-start-2 lg:row-start-1 gap-20px lg:gap-30px flex flex-col justify-center">
                        <div class="text-center lg:text-left gap-10px lg:gap-15px flex flex-col">
                            <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Intent Signals</h1>
                            <h2 class="text-2xl lg:text-3xl font-bold font-inter text-blue">Lead generation network.</h2>
                        </div>
                        <p class="text-base lg:text-body text-center lg:text-left">
                            Grow your sales pipeline with leads and RFQs from thousands of local businesses near you.
                        </p>
                        <div class="flex items-center gap-10px xl:gap-15px flex-col lg:flex-row">
                            <a target="_blank" class="btn large font-bold text-sm xl:text-base bg-blue text-white" href="https://calendly.com/elioplus">
                                <svg width="18" height="19" viewBox="0 0 18 19" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <path d="M8.95734 5.12856C10.2416 5.12856 11.2826 4.09242 11.2826 2.81428C11.2826 1.53614 10.2416 0.5 8.95734 0.5C7.67314 0.5 6.63208 1.53614 6.63208 2.81428C6.63208 4.09242 7.67314 5.12856 8.95734 5.12856Z" fill="#39B4EF" />
                                    <path d="M8.95734 11.8141C10.2416 11.8141 11.2826 10.778 11.2826 9.49983C11.2826 8.22169 10.2416 7.18555 8.95734 7.18555C7.67314 7.18555 6.63208 8.22169 6.63208 9.49983C6.63208 10.778 7.67314 11.8141 8.95734 11.8141Z" fill="#39B4EF" />
                                    <path d="M8.95734 18.5001C10.2416 18.5001 11.2826 17.464 11.2826 16.1859C11.2826 14.9077 10.2416 13.8716 8.95734 13.8716C7.67314 13.8716 6.63208 14.9077 6.63208 16.1859C6.63208 17.464 7.67314 18.5001 8.95734 18.5001Z" fill="#39B4EF" />
                                    <path d="M2.75666 11.8141C4.04087 11.8141 5.08192 10.778 5.08192 9.49983C5.08192 8.22169 4.04087 7.18555 2.75666 7.18555C1.47245 7.18555 0.431396 8.22169 0.431396 9.49983C0.431396 10.778 1.47245 11.8141 2.75666 11.8141Z" fill="#39B4EF" />
                                    <path d="M15.6746 11.8141C16.9589 11.8141 17.9999 10.778 17.9999 9.49983C17.9999 8.22169 16.9589 7.18555 15.6746 7.18555C14.3904 7.18555 13.3494 8.22169 13.3494 9.49983C13.3494 10.778 14.3904 11.8141 15.6746 11.8141Z" fill="#39B4EF" />
                                </svg>
                                <span>GET A DEMO</span></a>
                        </div>
                    </div>
                    <div class="flex justify-center items-center">
                        <img class="max-w-[250px] lg:max-w-full" src="/assets_out/images/intent-signals/Lead-generation-homepage.png" alt="Intent Signals, Leads &amp; RFQs generation network by Elioplus.">
                    </div>
                </div>
            </div>
            <div class="mt-30px wave">
                <img src="/assets_out/images/global/wave.svg" alt="Elioplus decorative wave">
            </div>
        </section>
        <section id="clients">
            <div class="container">
                <div class="flex flex-col gap-40px">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Our Clients</h3>
                        <p class="w-full text-base lg:text-body">Leading IT companies trust Elioplus and use Intent Signals to grow their sales pipeline.</p>
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
        <section class="bg-white" id="features">
            <div class="container">
                <div class="gap-50px lg:gap-80px flex flex-col">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Identify and engage buyers when they are ready to buy</h3>

                    </div>
                    <div class="grid-cols-1 lg:grid-cols-2 text-img-row grid gap-30px">
                        <div class="flex items-center justify-center">
                            <img class="max-w-[250px] lg:max-w-full" src="/assets_out/images/intent-signals/leads-and-rfqs-feature.png" alt="igh intent leads &amp; RFQs with the Elioplus Intent Signals Solution" loading="lazy">
                        </div>
                        <div class="gap-20px lg:gap-30px flex flex-col justify-center">
                            <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">High intent leads & RFQs</h3>
                            <p class="w-full text-base lg:text-body text-center lg:text-left">Get access to requests from end-customers that are looking for quotations based on your expertise and tech partnerships. Reach out to high intent leads that have expressed their intent to buy products and solutions that fit your needs.</p>
                        </div>
                    </div>
                    <div class="grid-cols-1 lg:grid-cols-2 text-img-row grid gap-30px">
                        <div class="row-start-2 lg:row-start-1 gap-20px lg:gap-30px flex flex-col justify-center">
                            <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Get featured on our network</h3>
                            <p class="w-full text-base lg:text-body text-center lg:text-left">Your company profile will be recommended on the top spot on all relevant landing pages on our network to help you drive high quality traffic to your website. Potential clients can also reach out directly to your company for quotation that is shared exclusively to you.</p>
                        </div>
                        <div class="flex items-center justify-center">
                            <img class="max-w-[250px] lg:max-w-full" src="/assets_out/images/intent-signals/get-featured.png" alt="Get featured on our network with the Elioplus Intent Signals Solution" loading="lazy">
                        </div>
                    </div>
                    <div class="grid-cols-1 lg:grid-cols-2 text-img-row grid gap-30px">
                        <div class="flex items-center justify-center">
                            <img class="max-w-[250px] lg:max-w-full" src="/assets_out/images/intent-signals/approach-channel-partners.png" alt="Start your campaigns with the Elioplus Intent Signals Solution" loading="lazy">
                        </div>
                        <div class="gap-20px lg:gap-30px flex flex-col justify-center">
                            <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Start your campaigns</h3>
                            <p class="w-full text-base lg:text-body text-center lg:text-left">You can filter and export all available data to be used to your outreach campaigns by your team. We provide company insights for every lead along with contact details for your marketing efforts or ad campaigns.</p>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-gray" id="leads">
            <div class="container">
                <div class="gap-30px lg:gap-50px flex flex-col">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Latest Quotes & Leads</h3>
                        <p class="w-full text-base lg:text-body">View a sample of the latest quote requests and leads uploaded on our platform based on the location and product/technologies. This list is being updated multiple times during the day.</p>
                        <div class="flex items-center justify-center mt-20px">
                            <a target="_blank" class="btn large font-bold bg-white text-sm xl:text-base" href="https://medium.com/@elioplus.com/intent-signals-how-it-works-b7701c427910"><span>LEARN MORE ABOUT OUR LEADS</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                        </div>
                    </div>
                    <div class="w-full overflow-x-auto flex flex-col rounded-10px overflow-hidden shadow-card">
                        <table class="w-max sm:w-full bg-white">
                            <thead class="bg-blue text-white">
                                <tr>
                                    <th>Type</th>
                                    <th class="text-left">Country</th>
                                    <th class="text-left">Company</th>
                                    <th class="text-left">Looking for</th>
                                    <th>Company Size</th>
                                </tr>
                            </thead>
                            <asp:Repeater ID="RdgIntentData" OnLoad="RdgIntentData_Load" runat="server">
                                <ItemTemplate>
                                    <tbody>
                                        <tr class="text-sm lg:text-base font-medium mdl:font-bold">
                                            <td><%# DataBinder.Eval(Container.DataItem, "type")%></td>
                                            <td>
                                                <div class="flex w-full justify-start">
                                                    <div class="country bg-gradient-product overflow-hidden rounded-30px p-5px p-15px flex gap-5px items-center border-gray border-2 w-fit">
                                                        <%--<img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/US.svg" alt="country flag for United States" loading="lazy" width="30" height="31" />--%>
                                                        <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold"><%# DataBinder.Eval(Container.DataItem, "country")%></h5>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="flex items-center justify-start gap-5px">
                                                    <img id="ImgCompanyLogo" runat="server" width="60" title="company's logo" loading="lazy" class="hidden mdl:block" alt='<%# DataBinder.Eval(Container.DataItem, "company_name")%>' src='<%# DataBinder.Eval(Container.DataItem, "company_logo")%>' />
                                                    <div><%# DataBinder.Eval(Container.DataItem, "company_name")%></div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="transition-all duration-200 hover:text-blue">
                                                    <asp:Label ID="LblProducts" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "product")%>' />
                                                </div>
                                                <asp:Image ID="ImgInfo" Visible="false" CssClass="w-[15px] mdl:w-[20px]" Style="cursor: pointer;" AlternateText="info icon" ImageUrl="/assets_out/images/global/info.svg" Width="20" Height="20" runat="server" />
                                                <telerik:RadToolTip ID="RttImgInfo" Visible="true" Text="" TargetControlID="ImgInfo" Width="200" Title="Products" AutoCloseDelay="10000" HideEvent="Default" Position="TopRight" Animation="Fade" runat="server" />

                                            </td>
                                            <td>
                                                <div class="text-center"><%# DataBinder.Eval(Container.DataItem, "users_count")%></div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>

                        <a id="aLoadLess" runat="server" visible="false" onserverclick="aLoadLess_ServerClick" class="btn text-sm mdl:text-base table-load bg-gray2 text-center text-blue font-bold py-20px w-full">LOAD LESS INTENT SIGNALS
                        </a>
                        <a id="aLoadMore" runat="server" visible="false" onserverclick="aLoadNext_ServerClick" class="btn text-sm mdl:text-base table-load bg-gray2 text-center text-blue font-bold py-20px w-full">LOAD MORE INTENT SIGNALS
                        </a>
                    </div>

                    <div class="flex flex-col text-center items-center">
                        <controls:MessageControl ID="UcMessageDataAlert" runat="server" />
                    </div>

                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-base sm:text-xl lg:text-2xl font-bold font-inter">
                            <asp:Label ID="Label1" runat="server" /><span class='text-blue'><asp:Label ID="LblLeadsCount" runat="server" /></span><asp:Label ID="Label2" runat="server" /></h3>
                        <div class="flex items-center justify-center mt-20px">
                            <a target="_blank" class="btn large font-bold bg-white text-sm xl:text-base" href="https://calendly.com/elioplus"><span>GET A DEMO</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                            </defs></svg>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-white">
            <div class="container">
                <div class="gap-50px lg:gap-80px flex flex-col">
                    <div class="gap-30px lg:gap-50px flex flex-col">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center">Popular Locations</h3>
                        <div class="gap-7px lg:gap-15px countries flex justify-center flex-wrap">
                            <a id="a4" runat="server" class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/intent-signals/united-states">
                                <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/US.svg" alt="country flag for United States" loading="lazy" width="30" height="31">
                                <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">United States</h5>
                            </a><a id="aLink1" runat="server" class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/intent-signals/australia">
                                <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Australia.svg" alt="country flag for Australia" loading="lazy" width="30" height="31">
                                <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">Australia</h5>
                            </a><a id="a8" runat="server" class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/intent-signals/india">
                                <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/India.svg" alt="country flag for India" loading="lazy" width="30" height="31">
                                <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">India</h5>
                            </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/intent-signals/canada">
                                <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Canada.svg" alt="country flag for Nigeria" loading="lazy" width="30" height="31">
                                <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">Canada</h5>
                            </a><a id="a6" runat="server" class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/intent-signals/south-africa">
                                <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/SouthAfrica.svg" alt="country flag for South Africa" loading="lazy" width="30" height="31">
                                <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">South Africa</h5>
                            </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/intent-signals/hong-kong">
                                <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Hong_Kong_Sar_China.svg" alt="country flag for New Zealand" loading="lazy" width="30" height="31">
                                <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">Hong Kong</h5>
                            </a><a class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/intent-signals/">
                                <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/Singapore.svg" alt="country flag for Singapore" loading="lazy" width="30" height="31">
                                <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">Singapore</h5>
                            </a><a id="a5" runat="server" class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/intent-signals/united-kingdom">
                                <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/UK.svg" alt="country flag for United Kingdom" loading="lazy" width="30" height="31">
                                <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">United Kingdom</h5>
                            </a><a id="a7" runat="server" class="country bg-white overflow-hidden rounded-30px p-5px pr-15px flex gap-5px items-center border-gray border-2 w-fit" href="/intent-signals/united-arab-emirates">
                                <img class="w-[20px] lg:w-[30px]" src="/assets_out/images/flags/United_Arab_Emirates.svg" alt="country flag for United Arab Emirates" loading="lazy" width="30" height="31">
                                <h5 class="text-xs lg:text-base-15 transition-all duration-200 hover:text-blue font-bold">United Arab Emirates</h5>
                            </a>
                        </div>
                    </div>
                    <div class="gap-30px lg:gap-50px flex flex-col">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center">Popular Technologies / Vendors</h3>
                        <div class="flex-col sm:flex-row gap-15px md:gap-25px mdl:gap-50px flex justify-center w-full">
                            <div class="flex-row sm:flex-col justify-center gap-10px flex-wrap flex">
                                <a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/sap">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">SAP</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/cisco">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Cisco</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/ibm">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Ibm</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/sophos">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Sophos</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/zoho">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Zoho</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/symantec">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Symantec</h4>
                                </a>
                            </div>
                            <div class="flex-row sm:flex-col justify-center gap-10px flex-wrap flex">
                                <a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/salesforce">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Salesforce</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/avaya">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Avaya</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/microsoft">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Microsoft</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/oracle">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Oracle</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/cloudflare">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Cloudflare</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/crm">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Crm Software</h4>
                                </a>
                            </div>
                            <div class="flex-row sm:flex-col justify-center gap-10px flex-wrap flex">
                                <a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/mcafee">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">McAfee</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/sage">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Sage</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/fortinet">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Fortinet</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/arista-networks">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Arista Networks</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/crowdstrike">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Crowdstrike</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/erp">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Erp Software</h4>
                                </a>
                            </div>
                            <div class="flex-row sm:flex-col justify-center gap-10px flex-wrap flex">
                                <a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/dell">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Dell</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/pos-software">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Pos Software</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/google">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Google</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/kofax">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Kofax</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/lenovo">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Lenovo</h4>
                                </a>
                            </div>
                            <div class="flex-row sm:flex-col justify-center gap-10px flex-wrap flex">
                                <a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/meraki">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Meraki</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/mimecast">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Mimecast</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/netapp">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Netapp</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/netsuite">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Netsuite</h4>
                                </a><a class="w-max smw-full flex gap-10px items-center" href="/intent-signals/
                                    ">
                                    <div class="hidden sm:block w-[8px] h-[8px]  sm:w-[15px] sm:h-[15px] rounded-full bg-blue"></div>
                                    <h4 class="text-sm sm:text-base lg:text-lg transition-all duration-200 hover:text-blue font-semibold">Ringcentral</h4>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-blue bg-particles bg-cover" id="howItWorks">
            <div class="container">
                <div class="gap-30px lg:gap-50px flex flex-col">
                    <div class="flex flex-col gap-10px text-center items-center text-white">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center">How it works</h3>
                        <p class="w-[780px] max-w-full text-base lg:text-body">Intent Signals is as simple as it can be. Get started in less than a minute</p>
                    </div>
                    <div class="grid-cols-1 md:grid-cols-2 mdl:grid-cols-3 grid lg:flex lg:flex-row gap-30px lg:gap-50px text-center items-center justify-center">
                        <div class="w-[320px] max-w-full mx-auto px-15px lg:px-25px py-15px lg:py-30px bg-white rounded-10px shadow-card flex flex-col gap-15px items-center">
                            <div class="w-[60px] h-[60px] lg:w-[85px] lg:h-[85px] p-10px rounded-full bg-lightBlue2 flex items-center justify-center">
                                <img src="/assets_out/images/intent-signals/join.svg" alt="Join icon" loading="lazy" width="55" height="55">
                            </div>
                            <div class="flex flex-col gap-5px">
                                <h4 class="text-base lg:text-body font-bold">Join</h4>
                                <p class="text-sm">Sign up and add your company’s expertise and technologies.</p>
                            </div>
                        </div>
                        <div class="w-[320px] max-w-full mx-auto px-15px lg:px-25px py-15px lg:py-30px bg-white rounded-10px shadow-card flex flex-col gap-15px items-center">
                            <div class="w-[60px] h-[60px] lg:w-[85px] lg:h-[85px] p-10px rounded-full bg-lightBlue2 flex items-center justify-center">
                                <img src="/assets_out/images/intent-signals/quotes.svg" alt="Quotes icon" loading="lazy" width="55" height="55">
                            </div>
                            <div class="flex flex-col gap-5px">
                                <h4 class="text-base lg:text-body font-bold">Quotes</h4>
                                <p class="text-sm">Receive quotation requests from local businesses for your products.</p>
                            </div>
                        </div>
                        <div class="w-[320px] max-w-full mx-auto px-15px lg:px-25px py-15px lg:py-30px bg-white rounded-10px shadow-card flex flex-col gap-15px items-center">
                            <div class="w-[60px] h-[60px] lg:w-[85px] lg:h-[85px] p-10px rounded-full bg-lightBlue2 flex items-center justify-center">
                                <img src="/assets_out/images/intent-signals/data.svg" alt="Intent Data icon" loading="lazy" width="55" height="55">
                            </div>
                            <div class="flex flex-col gap-5px">
                                <h4 class="text-base lg:text-body font-bold">Intent Data</h4>
                                <p class="text-sm">Get access to intent data for businesses based on your expertise.</p>
                            </div>
                        </div>
                    </div>
                    <div class="flex justify-center items-center gap-10px xl:gap-20px">
                        <a class="btn font-bold text-sm bg-white" href="https://medium.com/@elioplus.com/intent-signals-how-it-works-b7701c427910"><span>LEARN MORE</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                        </defs></svg>
                        </a>
                    </div>
                </div>
            </div>
        </section>
        <section id="whyIntent">
            <div class="container">
                <div class="gap-50px lg:gap-80px flex flex-col">
                    <div class="flex flex-col gap-10px text-center items-center">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Why choose Intent Signals?</h3>
                        <p class="w-full text-base lg:text-body">The Elioplus Intent Signals is different from other lead generation solutions for 3 main reasons:</p>
                    </div>
                    <div class="grid-cols-1 md:grid-cols-2 mdl:grid-cols-3 grid lg:flex lg:flex-row gap-30px lg:gap-10px justify-start lg:justify-between">
                        <div class="w-[380px] mx-auto max-w-full bg-white shadow-card overflow-hidden rounded-10px">
                            <div class="h-[250px] flex items-center justify-center">
                                <img src="/assets_out/images/intent-signals/whyQuotes.svg" alt="Quotes" loading="lazy">
                            </div>
                            <div class="gap-10px lg:gap-15px py-10px lg:py-30px px-20px lg:px-25px text-center bg-white flex flex-col">
                                <h3 class="text-lg lg:text-2xl font-bold font-inter">Quotes</h3>
                                <p class="text-sm lg:text-base">You will be receiving quote requests from local businesses based on your product portfolio and technologies.</p>
                            </div>
                        </div>
                        <div class="w-[380px] mx-auto max-w-full bg-white shadow-card overflow-hidden rounded-10px">
                            <div class="h-[250px] flex items-center justify-center">
                                <img src="/assets_out/images/intent-signals/whyTechnology.svg" alt="Technologies" loading="lazy">
                            </div>
                            <div class="gap-10px lg:gap-15px py-10px lg:py-30px px-20px lg:px-25px text-center bg-white flex flex-col">
                                <h3 class="text-lg lg:text-2xl font-bold font-inter">Technologies</h3>
                                <p class="text-sm lg:text-base">Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.</p>
                            </div>
                        </div>
                        <div class="w-[380px] mx-auto max-w-full bg-white shadow-card overflow-hidden rounded-10px">
                            <div class="h-[250px] flex items-center justify-center">
                                <img src="/assets_out/images/intent-signals/whyLocal.svg" alt="Local companies" loading="lazy">
                            </div>
                            <div class="gap-10px lg:gap-15px py-10px lg:py-30px px-20px lg:px-25px text-center bg-white flex flex-col">
                                <h3 class="text-lg lg:text-2xl font-bold font-inter">Local</h3>
                                <p class="text-sm lg:text-base">We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.</p>
                            </div>
                        </div>
                    </div>
                    <div class="grid-cols-1 lg:grid-cols-7-5 text-img-row grid gap-30px">
                        <div class="row-start-2 lg:row-start-1 gap-20px lg:gap-30px flex flex-col justify-center">
                            <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Where we get our Leads and RFQs from</h3>
                            <p class="w-full text-base lg:text-lg text-center lg:text-left">Our network has more than 20,000 landing pages for more than 5,000 technologies in 120 countries. Our visitors come from every corner in the world in order to find useful content, engage with local IT providers and ask for quotations. In some cases, we also receive multiple RFQs from local businesses via our communication channels. Local companies trust our network to find the best IT providers in their area, get the best prices for the products they are looking for or search for implementation, support and training services.</p>
                        </div>
                        <div class="flex items-center justify-center pl-30px">
                            <img class="max-w-[250px] lg:max-w-full" src="/assets_out/images/intent-signals/get-featured.png" alt="Where we get Leads and RFQs data for Elioplus Intent Signals software solution" loading="lazy">
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="bg-gray" id="pricing">
            <div class="container">
                <div class="gap-40px lg:gap-60px flex flex-col justify-center items-center w-full">
                    <div class="text-center gap-5px lg:gap-10px flex flex-col items-center w-full">
                        <h2 class="text-base lg:text-lg font-semibold text-blue">LEAD GENERATION & RFQs</h2>
                        <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Intent Signals Pricing</h1>
                        <p class="text-base lg:text-body text-center w-[900px] max-w-full">Grow your sales pipeline with leads and RFQs from thousands of local businesses near you. Contact us today and get a quote from our sales team.</p>
                    </div>
                    <div class="grid-cols-1 md:grid-cols-2 mdl:grid-cols-2 grid lg:flex lg:flex-row lg:justify-center items-center gap-30px lg:gap-50px w-full">
                        <div class="w-[360px] max-w-full mx-auto lg:mx-0 bg-white shadow-card rounded-20px p-30px flex flex-col gap-25px">
                            <div class="flex flex-col gap-10px w-full">
                                <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">Free</h3>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left">Limited to small local IT companies, we offer a free plan to get access to a number of leads every month.</p>
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left" />
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left" />
                                <p class="w-full text-sm lg:text-[15px] text-center lg:text-left" />
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
                            <img class="max-w-[250px] lg:max-w-full" src="/assets_out/images/contact-us/faq.svg" alt="Visit the Elioplus FAQ to get answers for your questions" loading="lazy">
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
    <div class="modal fixed p-25px bg-white shadow-card z-50 hidden" id="modal54">
        <div class="flex flex-col w-full gap-10px">
            <h4 class="text-base lg:text-xl font-bold font-inter text-center lg:text-left">Products:</h4>
            <p>3d architecture,3d cad,8k-miles-software-services,accounting,ace-cloud-hosting,acronis,acts,adaptive insights,adapt-software-india-pvt-ltd,adobe,adobe creative cloud,akamai,algorit,alibaba,alibaba cloud,allion-technologies,analytics software,anaplan,ansible,ansys,anton-systems,appdynamics,appian,apple,arcserve,argil-consulting,ariba,aruba,autocad,autodesk,avaya,aws,azure,backup & restore,backup & restore,bamboohr,barco,barracuda-networks,bilytica,bim,bittitan,bluesapling-technologies,bmc,booz allen,brother,browserstack-inc,bt,business process management,ca,cal-business-solutions,callhippo,calliduscloud,canviz,channel-partners,circular-informationssysteme,cisco,citrix,clearcloud,cloud integration (ipaas),cloud management,cloud management,cloud management,cloud management,cloud management,cloud management,cloud management,cloud storage,cloud storage,cloud storage,cloud storage,cloud storage,cloud storage,cloud storage,cloud storage,cloud storage,cloud storage,cloud storage,cloudamize,cloudendure,cloudflare,cloudhealth,cloudmigrator365,cloudtech24,cognigix,cohesity,commscope,commvault,compliance,conga,conquer-technologies,covantex-llc,creative-computing-solutions,crm,crowdstrike,crystal reports,cyber security,daxperience,deflytics,dell,dell emc,design-centric,detewe-communications,digi-power,digital asset management,digital marketing,digitalmax,digitalocean,d-link,documation-software,docusign,domain-plus-international,domo,druva,d-t,dynacons-systems-solutions,dynamics,ecm,ecommerce,eguard-technology-services,elearning,email,emc,envato,epicor,epson,erp,erpnext,eunoia-limited,exceller-consultancy,fast-in-cloud,fiori,firewall,fis-clouds,forcepoint,fore-solutions,fortinet,fraud prevention,frox,f-secure,gbb,general-purpose cad,genesys,gigahertz-computing,global-business-solution,google cloud,google cloud,google meet,google workspace,google workspace,google workspace,google workspace,google workspace,google workspace,governance, risk & compliance (grc),green hr,hana,healthcare,help desk,hexagon,honeywell,hp,hpe,hr administration,huawei,ibm,illustris,iluvatech,imanage,imenu360,imperva,indyzone,infinidat,infinity-pos,infor,informatica,ingram micro,ingram micro,inspark-intelligent-business-solutions,intel,inventory management,iotap,ip-dynamics,isolved,i-t-solutions-india,ivanti,jd edwards,jd edwards,juniper,kisssoft,kofax,konica minolta,kubera-softtech-private-limited,kyndryl,lbt-mind-technology-pvt-ltd,learning management system,learning management system,lenovo,liferay,lightlever-systems,logmein,mailchimp,mait,malwarebytes,management-science-and-innovation,marketing automation,mcafee,mechknowsoft-pvt-ltd,medialine-eurotrade,metron,microsoft,microsoft azure,microsoft azure,mimecast,mitel,mobileiron,modulus-data,mongodb,mylingotrip,ncr,netapp,netgear,netmagic,netsuite,netsystem,netwrix,new-relic,ninth-dimension-it-solutions-pvt-ltd,novation-technologies,nutanix,nvidia,odoo,office-timeline,ofm-communications,oneglobe,oracle,oracle cloud,palo-alto,paradiso-solutions,pcloud,pentaho,petpooja-prayohsa-food-services-pvt-ltd,pipedrive,plm,poly,pos,power-bi,procurement,productivity suites,project management tools,project management tools,ptc,purevpn,qlik,quick heal,red hat,red hat,red hat,red hat,red-hat,ringcentral,risk management,robotic process automation,rsa,rubrik,ruckus,sage,sales intelligence,sales intelligence,sales process management,salesforce,salesforce marketing cloud,salesforce marketing cloud,sap,sap business bydesign,sap business one,sap business one,sap business one,sap business one,sap business one,sap business one,sap business one,sap businessobjects,sap ecc,sap ecc,sap hana,sap hana,sas,seo & sem,servicenow,sharepoint,shd,shipping & tracking,sia,siemens,sify,slick-account-pvt-ltd,smartbear,snowflake,softlayer,softnow-regina-kiluk,solarwinds,sophos,splunk,sti-infotech,sugarcrm,sumo logic,supply chain management,surveys & forms,symantec,tally,tally erp,teamviewer,techinpro,technobind,techsmith,temenos,tesisquare,the-createch-group,timextender,trade-well-it-solutions,trend-micro,twirll-product-of-embedded-business-solution,uipath,unified-communications,veeam,veritas,video conferencing,video conferencing,virtual-it,vmware,voip,vulnerability management,warehouse management,westbahn-management-gmbh,workday,workflow management,yotta,zendesk,zipbooks,zoho,zoho forms,zoom,zscaler,zwsoft</p>
        </div>
    </div>
</asp:Content>
