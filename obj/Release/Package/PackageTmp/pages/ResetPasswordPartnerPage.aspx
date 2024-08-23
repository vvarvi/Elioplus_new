<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="ResetPasswordPartnerPage.aspx.cs" Inherits="WdS.ElioPlus.pages.ResetPasswordPartnerPage" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
   <meta name="description" id="metaDescription" runat="server" content="" />
    <meta name="keywords" id="metaKeywords" runat="server" content="" />
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
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Forgot your password?</h3>
                        <p class="text-base lg:text-body w-[572px] max-w-full">Please enter your email address below and you will receive a new password in your email inbox. You can change it any time you want.</p>
                    </div>
                    <div class="flex flex-col gap-25px text-center items-center w-full">
                        <div id="divContact" runat="server" class="w-[460px] max-w-full p-20px lg:p-40px flex flex-col bg-white shadow-card rounded-10px justify-center items-center" method="POST">
                            <div class="w-full flex flex-col gap-30px">
                                <div class="input-field">
                                    <label class="text-sm font-semibold" for="email">Your email address:</label>
                                    <asp:TextBox ID="TbxEmail" type="email" MaxLength="150" placeholder="Enter your account email address" required="true" runat="server" />
                                </div>
                                <div class="w-full flex flex-col gap-20px">
                                    <asp:Button ID="BtnSend" runat="server" Text="RESET MY PASSOWRD" OnClick="BtnSend_OnClick" CssClass="btn large font-bold text-sm bg-blue text-white w-full justify-center" type="submit" />
                                </div>

                                <controls:MessageControl ID="UcMessageControlAlert" runat="server" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
