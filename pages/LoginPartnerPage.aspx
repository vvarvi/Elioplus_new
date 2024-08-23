<%@ Page Title="" Language="C#" MasterPageFile="~/ElioplusEmpty.Master" AutoEventWireup="true" CodeBehind="LoginPartnerPage.aspx.cs" Inherits="WdS.ElioPlus.pages.LoginPartnerPage" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" id="metaDescription" runat="server" content="Login to your Vendor's partner portal. Start working together and collaborate with your Vendor" />
    <meta name="keywords" id="metaKeywords" runat="server" content="Vendor partner portal" />
</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <main class="transition-all duration-200">
        <section class="bg-gray">
            <div class="container">
                <div class="gap-30px lg:gap-50px flex flex-col justify-center items-center">
                    <div class="flex flex-col gap-10px text-center items-center w-full">
                        <a id="aElioplusLogo" runat="server" class="w-[100px] lg:w-[150px] block header-logo">
                            <asp:Image ID="ImgElioplusLogo" runat="server" ImageUrl="/assets_main/images/elioplus_blue.png" />
                        </a>
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">
                            <asp:Label ID="LblTitle" runat="server" />
                        </h3>
                        <p class="text-base lg:text-body">Enter your credentials to login to your ElioPlus account.</p>
                    </div>
                    <div class="flex flex-col gap-25px text-center items-center w-full">
                        <div class="w-[460px] max-w-full p-20px lg:p-40px flex flex-col bg-white shadow-card rounded-10px justify-center items-center" method="POST">
                            <div class="w-full flex flex-col gap-30px">
                                <div class="input-field">
                                    <label class="text-sm font-semibold" for="email">Email:</label>
                                    <asp:TextBox ID="TbxUsername" runat="server" MaxLength="50" type="email" placeholder="Enter your email address or your username" />
                                </div>
                                <div class="input-field">
                                    <label class="text-sm font-semibold" for="password">Password:</label>
                                    <asp:TextBox ID="TbxPassword" runat="server" MaxLength="50" type="password" placeholder="Enter your password" />
                                </div>
                                <div class="flex justify-between">
                                    <div class="input-field flex-row flex-gap-5px items-center">
                                        <input id="CbxRemember" runat="server" name="remember" type="checkbox" />
                                        <label class="text-sm font-semibold text-textGray" for="remember">Remember me</label>
                                    </div>
                                    <a id="aResetPassword" runat="server" class="text-textGray font-semibold text-sm hover:text-blue" href="/reset-password">
                                        <asp:Label ID="LblForgotPassword" runat="server" Text="Forgot your password?" /></a>
                                </div>
                                <div class="w-full flex flex-col gap-20px">
                                    <asp:Button ID="BtnLoginPartner" Text="LOGIN TO MY ACCOUNT" CssClass="btn large font-bold text-sm bg-blue text-white w-full justify-center" type="submit" OnClick="BtnLoginPartner_OnClick" runat="server" />
                                </div>

                                <controls:MessageControl ID="UcMessageControlAlert" runat="server" />

                            </div>
                        </div>
                        <a id="aCreateAccount" runat="server" class="font-semibold text-sm text-blue underline">
                            <asp:Label ID="LblCreateAccount" runat="server" />
                        </a>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
