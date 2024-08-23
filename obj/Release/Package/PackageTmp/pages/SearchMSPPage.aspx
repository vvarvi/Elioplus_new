<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="SearchMSPPage.aspx.cs" Inherits="WdS.ElioPlus.pages.SearchMSPPage" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Browse thousands of different partner programs based on product category designed especially for Managed Service Providers – MSPs." />
    <meta name="keywords" content="managed service providers partner programs, MSPs partner programs" />
</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <main class="transition-all duration-200">
        <section class="bg-gray">
            <div class="container">
                <div class="gap-40px lg:gap-60px flex flex-col justify-center items-center w-full">
                    <div class="gap-20px lg:gap-30px flex flex-col justify-center items-center w-full">
                        <div class="text-center gap-5px lg:gap-10px flex flex-col items-center w-full">
                            <div class="text-xs lg:text-base flex gap-5px items-center justify-center text-textGray font-medium">
                                <a id="aBCrHome" runat="server" class="transition-all duration-200 hover:text-blue" />
                                <a id="aBCrCategory" runat="server" class="transition-all duration-200 hover:text-blue" />
                            </div>
                            <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Managed Service Providers (MSPs) partner programs</h1>
                            <p class="text-base lg:text-body text-center max-w-full">A managed service provider (MSP) is an IT company that manages remotely the infrastructure and systems for a customer. In order to reduce costs and not to hire any IT personnel many small and mid-sized companies choose to hire an MSP company based on a subscription model to manage all their IT needs.</p>
                        </div>
                        <div class="flex justify-center">
                            <div class="flex gap-10px w-full lg:w-[500px] max-w-full pt-5px pr-5px pb-5px pl-15px bg-white shadow-card rounded-40px overflow-hidden">
                                <input class="grow pl-15px rounded-30px outline-none" placeholder="Type to search for a product category">
                                <button class="btn large font-bold text-xs xl:text-sm bg-blue text-white" type="submit">
                                    <span>SEARCH</span><svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_40_8306)"><path d="M19.8102 18.9119L14.6466 13.8308C15.9988 12.3616 16.8296 10.4187 16.8296 8.28068C16.8289 3.7071 13.0618 0 8.41447 0C3.76713 0 0 3.7071 0 8.28068C0 12.8543 3.76713 16.5614 8.41447 16.5614C10.4224 16.5614 12.2641 15.8668 13.7107 14.7122L18.8944 19.8134C19.147 20.0622 19.5571 20.0622 19.8096 19.8134C20.0628 19.5646 20.0628 19.1607 19.8102 18.9119ZM8.41447 15.2873C4.48231 15.2873 1.29468 12.1504 1.29468 8.28068C1.29468 4.41101 4.48231 1.27403 8.41447 1.27403C12.3467 1.27403 15.5343 4.41101 15.5343 8.28068C15.5343 12.1504 12.3467 15.2873 8.41447 15.2873Z" fill="white" /></g><defs><clipPath id="clip0_40_8306"><rect width="20" height="20" fill="white" /></clipPath>
                                    </defs></svg>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section>
            <div class="container">
                <div class="flex flex-col gap-50px w-full">
                    <div class="flex flex-col gap-15px w-full">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter text-center lg:text-left">Partner Programs</h3>
                        <p class="text-base lg:text-body text-center lg:text-left max-w-full">Find the best SaaS and software partner programs based on the product category and designed especially for MSPs.</p>
                    </div>
                    <div class="gap-30px lg:gap-40px flex flex-col w-full">
                        <div class="flex w-full bg-white shadow-card rounded-10px overflow-hidden">
                            <img src="/assets_out/images/search/vendor-type-categories/marketing.png" alt="Partner Program category Sales &amp; Marketing">
                            <div class="gap-10px lg:gap-15px flex flex-col p-25px justify-center">
                                <h4 class="font-bold font-inter text-2xl">Sales &amp; Marketing</h4>
                                <div class="h-[2px] divider"></div>
                                <div class="w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptSalesMarketing" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" href='<%# Eval("link") %>' class="hover:text-blue flex items-center gap-5px py-10px px-15px text-textGray transition-all duration-200 border border-gray rounded-4px">
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg"></a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <div class="flex w-full bg-white shadow-card rounded-10px overflow-hidden">
                            <img src="/assets_out/images/search/vendor-type-categories/customer-management.png" alt="Partner Program category Customer Management">
                            <div class="gap-10px lg:gap-15px flex flex-col p-25px justify-center">
                                <h4 class="font-bold font-inter text-2xl">Customer Management</h4>
                                <div class="h-[2px] divider"></div>
                                <div class="w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptCustomerManagement" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" href='<%# Eval("link") %>' class="hover:text-blue flex items-center gap-5px py-10px px-15px text-textGray transition-all duration-200 border border-gray rounded-4px">
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg"></a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <div class="flex w-full bg-white shadow-card rounded-10px overflow-hidden">
                            <img src="/assets_out/images/search/vendor-type-categories/project-management.png" alt="Partner Program category Project Management">
                            <div class="gap-10px lg:gap-15px flex flex-col p-25px justify-center">
                                <h4 class="font-bold font-inter text-2xl">Project Management</h4>
                                <div class="h-[2px] divider"></div>
                                <div class="w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptProjectManagement" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" href='<%# Eval("link") %>' class="hover:text-blue flex items-center gap-5px py-10px px-15px text-textGray transition-all duration-200 border border-gray rounded-4px">
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg"></a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <div class="flex w-full bg-white shadow-card rounded-10px overflow-hidden">
                            <img src="/assets_out/images/search/vendor-type-categories/operations-workflow.png" alt="Partner Program category Operations &amp; Workflow">
                            <div class="gap-10px lg:gap-15px flex flex-col p-25px justify-center">
                                <h4 class="font-bold font-inter text-2xl">Operations &amp; Workflow</h4>
                                <div class="h-[2px] divider"></div>
                                <div class="w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptOperationsWorkflow" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" href='<%# Eval("link") %>' class="hover:text-blue flex items-center gap-5px py-10px px-15px text-textGray transition-all duration-200 border border-gray rounded-4px">
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg"></a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <div class="flex w-full bg-white shadow-card rounded-10px overflow-hidden">
                            <img src="/assets_out/images/search/vendor-type-categories/tracking.png" alt="Partner Program category Tracking &amp; Measurement">
                            <div class="gap-10px lg:gap-15px flex flex-col p-25px justify-center">
                                <h4 class="font-bold font-inter text-2xl">Tracking &amp; Measurement</h4>
                                <div class="h-[2px] divider"></div>
                                <div class="w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTrackingMeasurement" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" href='<%# Eval("link") %>' class="hover:text-blue flex items-center gap-5px py-10px px-15px text-textGray transition-all duration-200 border border-gray rounded-4px">
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg"></a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <div class="flex w-full bg-white shadow-card rounded-10px overflow-hidden">
                            <img src="/assets_out/images/search/vendor-type-categories/accounting.png" alt="Partner Program category Accounting &amp; Financials">
                            <div class="gap-10px lg:gap-15px flex flex-col p-25px justify-center">
                                <h4 class="font-bold font-inter text-2xl">Accounting &amp; Financials</h4>
                                <div class="h-[2px] divider"></div>
                                <div class="w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptAccountingFinancials" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" href='<%# Eval("link") %>' class="hover:text-blue flex items-center gap-5px py-10px px-15px text-textGray transition-all duration-200 border border-gray rounded-4px">
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg"></a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <div class="flex w-full bg-white shadow-card rounded-10px overflow-hidden">
                            <img src="/assets_out/images/search/vendor-type-categories/hr.png" alt="Partner Program category HR">
                            <div class="gap-10px lg:gap-15px flex flex-col p-25px justify-center">
                                <h4 class="font-bold font-inter text-2xl">HR</h4>
                                <div class="h-[2px] divider"></div>
                                <div class="w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptHR" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" href='<%# Eval("link") %>' class="hover:text-blue flex items-center gap-5px py-10px px-15px text-textGray transition-all duration-200 border border-gray rounded-4px">
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg"></a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <div class="flex w-full bg-white shadow-card rounded-10px overflow-hidden">
                            <img src="/assets_out/images/search/vendor-type-categories/web-development.png" alt="Partner Program category Web Mobile Software Development">
                            <div class="gap-10px lg:gap-15px flex flex-col p-25px justify-center">
                                <h4 class="font-bold font-inter text-2xl">Web Mobile Software Development</h4>
                                <div class="h-[2px] divider"></div>
                                <div class="w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptWebMobileSoftwareDevelopment" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" href='<%# Eval("link") %>' class="hover:text-blue flex items-center gap-5px py-10px px-15px text-textGray transition-all duration-200 border border-gray rounded-4px">
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg"></a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <div class="flex w-full bg-white shadow-card rounded-10px overflow-hidden">
                            <img src="/assets_out/images/search/vendor-type-categories/accounting.png" alt="Partner Program category Accounting &amp; Financials">
                            <div class="gap-10px lg:gap-15px flex flex-col p-25px justify-center">
                                <h4 class="font-bold font-inter text-2xl">IT &amp; Infrastructure</h4>
                                <div class="h-[2px] divider"></div>
                                <div class="w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptItInfrastructure" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" href='<%# Eval("link") %>' class="hover:text-blue flex items-center gap-5px py-10px px-15px text-textGray transition-all duration-200 border border-gray rounded-4px">
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg"></a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <div class="flex w-full bg-white shadow-card rounded-10px overflow-hidden">
                            <img src="/assets_out/images/search/vendor-type-categories/hr.png" alt="Partner Program category HR">
                            <div class="gap-10px lg:gap-15px flex flex-col p-25px justify-center">
                                <h4 class="font-bold font-inter text-2xl">Business Utilities</h4>
                                <div class="h-[2px] divider"></div>
                                <div class="w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptBusinessUtilities" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" href='<%# Eval("link") %>' class="hover:text-blue flex items-center gap-5px py-10px px-15px text-textGray transition-all duration-200 border border-gray rounded-4px">
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg"></a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <div class="flex w-full bg-white shadow-card rounded-10px overflow-hidden">
                            <img src="/assets_out/images/search/vendor-type-categories/web-development.png" alt="Partner Program category Web Mobile Software Development">
                            <div class="gap-10px lg:gap-15px flex flex-col p-25px justify-center">
                                <h4 class="font-bold font-inter text-2xl">Data Security &amp; GRC</h4>
                                <div class="h-[2px] divider"></div>
                                <div class="w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptDataSecurityGRC" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" href='<%# Eval("link") %>' class="hover:text-blue flex items-center gap-5px py-10px px-15px text-textGray transition-all duration-200 border border-gray rounded-4px">
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg"></a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <div class="flex w-full bg-white shadow-card rounded-10px overflow-hidden">
                            <img src="/assets_out/images/search/vendor-type-categories/web-development.png" alt="Partner Program category Web Mobile Software Development">
                            <div class="gap-10px lg:gap-15px flex flex-col p-25px justify-center">
                                <h4 class="font-bold font-inter text-2xl">Design &amp; Multimedia</h4>
                                <div class="h-[2px] divider"></div>
                                <div class="w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptDesignMultimedia" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" href='<%# Eval("link") %>' class="hover:text-blue flex items-center gap-5px py-10px px-15px text-textGray transition-all duration-200 border border-gray rounded-4px">
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg"></a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <div class="flex w-full bg-white shadow-card rounded-10px overflow-hidden">
                            <img src="/assets_out/images/search/vendor-type-categories/web-development.png" alt="Partner Program category Web Mobile Software Development">
                            <div class="gap-10px lg:gap-15px flex flex-col p-25px justify-center">
                                <h4 class="font-bold font-inter text-2xl">Miscellanious</h4>
                                <div class="h-[2px] divider"></div>
                                <div class="w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptMiscellanious" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" href='<%# Eval("link") %>' class="hover:text-blue flex items-center gap-5px py-10px px-15px text-textGray transition-all duration-200 border border-gray rounded-4px">
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg"></a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <div class="flex w-full bg-white shadow-card rounded-10px overflow-hidden">
                            <img src="/assets_out/images/search/vendor-type-categories/web-development.png" alt="Partner Program category Web Mobile Software Development">
                            <div class="gap-10px lg:gap-15px flex flex-col p-25px justify-center">
                                <h4 class="font-bold font-inter text-2xl">Unified Communications</h4>
                                <div class="h-[2px] divider"></div>
                                <div class="w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptUnifiedCommunications" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" href='<%# Eval("link") %>' class="hover:text-blue flex items-center gap-5px py-10px px-15px text-textGray transition-all duration-200 border border-gray rounded-4px">
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg"></a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <div class="flex w-full bg-white shadow-card rounded-10px overflow-hidden">
                            <img src="/assets_out/images/search/vendor-type-categories/web-development.png" alt="Partner Program category Web Mobile Software Development">
                            <div class="gap-10px lg:gap-15px flex flex-col p-25px justify-center">
                                <h4 class="font-bold font-inter text-2xl">CAD &amp; PLM</h4>
                                <div class="h-[2px] divider"></div>
                                <div class="w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptCADPLM" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" href='<%# Eval("link") %>' class="hover:text-blue flex items-center gap-5px py-10px px-15px text-textGray transition-all duration-200 border border-gray rounded-4px">
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg"></a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
