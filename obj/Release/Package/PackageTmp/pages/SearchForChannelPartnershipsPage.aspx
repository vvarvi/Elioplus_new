<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="SearchForChannelPartnershipsPage.aspx.cs" Inherits="WdS.ElioPlus.pages.SearchForChannelPartnershipsPage" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Find all software, cloud and hardware IT partners and browse their technologies and partnerships to help you with licensing, implementation, support and training services." />
    <meta name="keywords" content="IT partnerships, cloud partnerships, software partnerships, hardware partnerships" />

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
                            </div>
                            <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Search for more than <span class='text-blue'>
                                <asp:Label ID="LblTechnoCount" runat="server" />
                            </span>
                                <br class='hidden lg:block'>
                                Partnerships & Technologies at Elioplus</h1>
                            <p class="text-base lg:text-body text-center max-w-full">Are you looking for an IT provider (like reseller, VAR, consultant etc.) that sells a specific technology? Browse our platform's IT providers listed by their specific partnerships and expertise in the list below or by using the search form.</p>
                        </div>
                    </div>
                    <div class="flex justify-center">
                        <div class="flex gap-10px w-full lg:w-[760px] max-w-full pt-5px pr-5px pb-5px pl-15px bg-white shadow-card rounded-40px overflow-hidden">
                            <input class="grow pl-15px rounded-30px outline-none" placeholder="Type to search for a specific partnership, expertise, vendor technology">
                            <button class="btn large font-bold text-xs xl:text-sm bg-blue text-white" type="submit">
                                <span>SEARCH</span><svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_40_8306)"><path d="M19.8102 18.9119L14.6466 13.8308C15.9988 12.3616 16.8296 10.4187 16.8296 8.28068C16.8289 3.7071 13.0618 0 8.41447 0C3.76713 0 0 3.7071 0 8.28068C0 12.8543 3.76713 16.5614 8.41447 16.5614C10.4224 16.5614 12.2641 15.8668 13.7107 14.7122L18.8944 19.8134C19.147 20.0622 19.5571 20.0622 19.8096 19.8134C20.0628 19.5646 20.0628 19.1607 19.8102 18.9119ZM8.41447 15.2873C4.48231 15.2873 1.29468 12.1504 1.29468 8.28068C1.29468 4.41101 4.48231 1.27403 8.41447 1.27403C12.3467 1.27403 15.5343 4.41101 15.5343 8.28068C15.5343 12.1504 12.3467 15.2873 8.41447 15.2873Z" fill="white" /></g><defs><clipPath id="clip0_40_8306"><rect width="20" height="20" fill="white" /></clipPath>
                                </defs></svg>

                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section>
            <div class="container">
                <div class="flex gap-30px lg:gap-50px flex-col w-full" id="tabTechnology">
                    <div class="flex flex-col gap-15px w-full">
                        <h3 class="text-xl lg:text-2xl font-bold font-inter text-center lg:text-left">Vendor Technologies:</h3>
                    </div>
                    <div class="grid-cols-1 lg:grid-cols-letters gap-30px lg:gap-50px grid w-full">
                        <div class="flex flex-col gap-15px items-center justify-start border-x-2 border-gray py-5px">
                            <a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techA">A</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techB">B</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techC">C</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techD">D</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techE">E</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techF">F</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techG">G</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techH">H</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techI">I</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techJ">J</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techK">K</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techL">L</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techM">M</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techN">N</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techO">O</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techP">P</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techQ">Q</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techR">R</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techS">S</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techT">T</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techU">U</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techV">V</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techW">W</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techX">X</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techY">Y</a><a class="go-to-expand text-sm lg:text-base w-[35px] h-[35px] font-medium font-inter border border-iconGray bg-white rounded-full text-textGray flex items-center justify-center text-center" href="#techZ">Z</a>
                        </div>
                        <div class="gap-30px lg:gap-40px flex flex-col w-full">
                            <details class="text-sm lg:text-base" id="techA" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">A</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechA" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techB" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">B</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechB" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techC" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">C</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechC" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techD" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">D</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechD" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techE" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">E</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechE" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techF" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">F</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechF" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techG" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">G</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechG" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techH" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">H</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechH" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techI" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">I</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechI" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techJ" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">J</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechJ" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techK" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">K</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechK" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techL" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">L</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechL" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techM" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">M</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechM" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techN" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">N</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechN" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techO" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">O</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechO" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techP" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">P</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechP" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techQ" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">Q</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechQ" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techR" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">R</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechR" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techS" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">S</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechS" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techT" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">T</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechT" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techU" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">U</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechU" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techV" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">V</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechV" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techW" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">W</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechW" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techX" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">X</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechX" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techY" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">Y</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechY" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                            <details class="text-sm lg:text-base" id="techZ" open>
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px  flex gap-7px font-bold relative bg-white shadow-card relative rounded-4px cursor-pointer">
                                    <h4 class="font-bold font-inter text-lg">Technologies starting with&nbsp;<span class="text-blue">Z</span>:</h4>
                                </summary>
                                <div class="mt-20px w-full flex gap-15px flex-wrap">
                                    <asp:Repeater ID="RptTechZ" runat="server">
                                        <ItemTemplate>
                                            <a id="aLink" runat="server" class="hover:text-blue flex items-center gap-5px bg-white shadow-card rounded-4px py-10px px-15px text-textGray transition-all duration-200" href='<%# Eval("link") %>'>
                                                <span class="font-bold text-sm"><%# Eval("description") %></span>
                                                <img src="/assets_out/images/global/next.svg" alt="next icon" loading="lazy">
                                            </a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </details>
                            <div class="h-[2px] divider"></div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
