<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="ClaimProfilePage.aspx.cs" Inherits="WdS.ElioPlus.pages.ClaimProfilePage" %>

<%@ Register Src="~/Controls/AlertControls/MessageAlertControl.ascx" TagName="MessageAlertControl" TagPrefix="controls" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" id="metaDescription" runat="server" content="Share your requirements and get quotations from Managed Service Providers, IT companies and agencies near you to compare prices." />
    <meta name="keywords" id="metaKeywords" runat="server" content="get quote, quotations, Elioplus quotations" />
</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <main class="transition-all duration-200">
                <section>
                    <div class="container">
                        <div class="flex w-full justify-center">
                            <div class="w-full md:w-[730px] flex flex-col gap-30px">
                                <div class="text-center gap-5px lg:gap-10px flex flex-col items-center w-full">
                                    <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Claim Profile Message Form</h1>
                                </div>
                                <div class="w-full p-20px lg:p-40px flex flex-col bg-white shadow-card rounded-10px justify-center items-center" method="POST">
                                    <div class="w-full flex flex-col gap-30px">
                                        <div class="w-full flex flex-col gap-30px step-content active">
                                            <h3 class="text-base lg:text-lg font-bold font-inter">
                                                <asp:Label ID="LblTitleStep1F" runat="server" />
                                            </h3>
                                            <div class="grid-cols-1 lg:grid-cols-2 gap-30px lg:gap-50px grid">
                                                <div class="input-field">
                                                    <label class="text-sm font-semibold">Enter Your E-mail:</label>
                                                    <asp:TextBox ID="TbxClaimMessageEmail" MaxLength="100" placeholder="Your E-mail" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="flex w-full flex-col gap-30px step-content active">
                                            <div class="w-full flex justify-end gap-20px">
                                                <asp:Button ID="BtnCancelClaimMsg" OnClick="BtnCancelClaimMsg_OnClick" Text="Cancel" class="btn large font-bold text-sm bg-blue text-white justify-center" type="submit" runat="server" />
                                                <asp:Button ID="BtnSendClaim" OnClick="BtnSendClaim_OnClick" Text="Send it" class="btn large font-bold text-sm bg-blue text-white justify-center" type="submit" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="w-full flex flex-col gap-30px pt-10px">
                                        <div id="divClaimWarningMsg" runat="server" visible="false" class="card bg-red text-white gap-15px">
                                            <div class="flex items-center gap-5px">
                                                <img src="/assets_out/images/alerts/error-white.svg" alt="Quick Actions Icon" title="Click to toggle the quick actions menu" width="20" height="20">
                                                <h3 class="font-bold text-base text-white">
                                                    <asp:Label ID="LblClaimWarningMsg" Text="Error!" runat="server" />
                                                </h3>
                                            </div>
                                            <p class="text-sm text-white">
                                                <asp:Label ID="LblClaimWarningMsgContent" runat="server" />
                                            </p>
                                        </div>

                                        <div id="divClaimSuccessMsg" runat="server" visible="false" class="card bg-green text-white gap-15px">
                                            <div class="flex items-center gap-5px">
                                                <img src="/assets_out/images/alerts/success-white.svg" alt="Quick Actions Icon" title="Click to toggle the quick actions menu" width="20" height="20">
                                                <h3 class="font-bold text-base text-white">
                                                    <asp:Label ID="LblClaimSuccessMsg" Text="Success!" runat="server" />
                                                </h3>
                                            </div>
                                            <p class="text-sm text-white">
                                                <asp:Label ID="LblClaimSuccessMsgContent" runat="server" />
                                            </p>
                                        </div>
                                    </div>
                                </div>
                                <div class="w-full p-25px gap-15px lg:gap-20px bg-white shadow-card overflow-hidden rounded-10px flex items-start flex-wrap">
                                    <img class="mt-5px" src="/assets_out/images/search/channel-partners.svg" alt="Search and browse Sass Channel Partners" loading="lazy">
                                    <div class="gap-10px lg:gap-15px bg-white flex flex-col w-min grow">
                                        <div class="flex flex-col gap-5px">
                                            <h3 class="text-base lg:text-lg font-bold font-inter">Sass Channel Partners</h3>
                                            <p class="text-sm lg:text-base">Explore channel companies that sell your products or services and belong to your indirect sales force while acting independent.</p>
                                        </div>
                                        <a class="btn font-bold text-xs xl:text-sm justify-center text-blue" href="/search/channel-partners"><span>VIEW CHANNEL PARTNERS</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                        </defs></svg>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </main>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
