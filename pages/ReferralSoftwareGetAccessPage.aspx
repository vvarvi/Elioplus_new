<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="ReferralSoftwareGetAccessPage.aspx.cs" Inherits="WdS.ElioPlus.pages.ReferralSoftwareGetAccessPage" %>

<%@ Register Src="~/Controls/AlertControls/MessageAlertControl.ascx" TagName="MessageAlertControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/AlertControls/MessageControl2Msgs.ascx" TagName="MessageControl2Msgs" TagPrefix="controls" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Referral software to scale and automate your referral partner program and generate more leads. Manage your referral partners and payouts, easily integrate your CRM." />
    <meta name="keywords" content="referral software, referral partner program management, referral partner program system, referral partner program software" />
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
                                    <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Get Access</h1>
                                    <controls:MessageAlertControl ID="UcMessageAlertTop" runat="server" />
                                </div>
                                <div class="px-10px lg:px-25px flex items-center justify-between w-full relative" id="quoteSteps">
                                    <div id="aStep1" runat="server" class="stepTab flex flex-col gap-10px items-center justify-center text-center relative z-2 active">
                                        <div class="w-[30px] h-[30px]  rounded-full border-2 border-blue bg-white circle"></div>
                                        <h4 class="text-sm lg:text-base font-semibold text-textGray">
                                            <asp:Label ID="LblTitleStep1" runat="server" />
                                        </h4>
                                    </div><div id="aStep2" runat="server" class="stepTab flex flex-col gap-10px items-center justify-center text-center relative z-2">
                                        <div class="w-[30px] h-[30px]  rounded-full border-2 border-blue bg-white circle"></div>
                                        <h4 class="text-sm lg:text-base font-semibold text-textGray">
                                            <asp:Label ID="LblTitleStep2" runat="server" />
                                        </h4>
                                    </div><div id="aStep3" runat="server" class="stepTab flex flex-col gap-10px items-center justify-center text-center relative z-2">
                                        <div class="w-[30px] h-[30px]  rounded-full border-2 border-blue bg-white circle"></div>
                                        <h4 class="text-sm lg:text-base font-semibold text-textGray">
                                            <asp:Label ID="LblTitleStep3" runat="server" />
                                        </h4>
                                    </div>
                                </div>
                                <div class="w-full p-20px lg:p-40px flex flex-col bg-white shadow-card rounded-10px justify-center items-center">

                                    <div class="w-full flex flex-col gap-30px">
                                        <div class="w-full flex flex-col gap-30px step-content active" id="divStepOne" runat="server">
                                            <h3 class="text-base lg:text-lg font-bold font-inter">
                                                <asp:Label ID="LblTitleStep1F" runat="server" />
                                            </h3>
                                            <div class="grid-cols-1 lg:grid-cols-2 gap-30px lg:gap-50px grid">
                                                <div class="input-field">
                                                    <label class="text-sm font-semibold">Enter Your E-mail:</label>
                                                    <asp:TextBox ID="TbxEmailAddressTop" MaxLength="100" placeholder="Enter Your E-mail Address" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="flex hidden w-full flex-col gap-30px step-content" id="divStepTwo" runat="server" visible="false">
                                            <h3 class="text-base lg:text-lg font-bold font-inter">
                                                <asp:Label ID="LblTitleStep2F" runat="server" />
                                            </h3>
                                            <div class="grid-cols-1 lg:grid-cols-2 gap-30px lg:gap-50px grid">
                                                <div class="input-field">
                                                    <label class="text-sm font-semibold" for="fname">
                                                        <asp:Label ID="LblFirstName" runat="server" />
                                                    </label>
                                                    <asp:TextBox ID="TbxFirstName" runat="server" MaxLength="100" name="fname" type="text" placeholder="Enter your First name" />
                                                </div>
                                                <div class="input-field">
                                                    <label class="text-sm font-semibold" for="lname">
                                                        <asp:Label ID="LblLastName" runat="server" />
                                                    </label>
                                                    <asp:TextBox ID="TbxLastName" runat="server" MaxLength="100" name="lname" type="text" placeholder="Enter your Last name" />
                                                </div>
                                            </div>
                                            <div class="grid-cols-1 lg:grid-cols-2 gap-30px lg:gap-50px grid">                                               
                                                <div class="input-field">
                                                    <label class="text-sm font-semibold" for="company">
                                                        <asp:Label ID="LblBusinessName" runat="server" />
                                                    </label>
                                                    <asp:TextBox ID="TbxBusinessName" MaxLength="100" runat="server" name="company" type="text" placeholder="Enter your Company Name" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="flex hidden w-full flex-col gap-30px step-content" id="divStepThree" runat="server" visible="false">
                                            <h3 class="text-base lg:text-lg font-bold font-inter">
                                                <asp:Label ID="LblTitleStep3F" runat="server" />
                                            </h3>
                                            <div class="w-full flex flex-col gap-30px">
                                                <controls:MessageControl2Msgs ID="UcMessageSuccess" runat="server" />
                                            </div>
                                        </div>
                                        <asp:HiddenField ID="HdnLeadId" Value="0" runat="server" />
                                        <div class="flex w-full flex-col gap-30px step-content active">
                                            <div class="w-full flex justify-end gap-20px">
                                                <asp:Button ID="BtnBack" Text="Back" Visible="false" OnClick="BtnBack_Click" CssClass="nav-btn btn large font-bold text-sm justify-center text-blue" runat="server" />
                                                <asp:Button ID="BtnProceed" runat="server" OnClick="BtnProceed_Click" Text="SUBMIT FORM" CssClass="nav-btn btn large font-bold text-sm bg-blue text-white justify-center" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="w-full flex flex-col gap-30px pt-10px">
                                        <controls:MessageAlertControl ID="UcMessageAlert" runat="server" />
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
