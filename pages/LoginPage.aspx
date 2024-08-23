﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="WdS.ElioPlus.pages.LoginPage" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Sign in to your account and find great partnership opportunities and notifications from software and SaaS companies right into your advanced dashboard" />
    <meta name="keywords" content="partnership opportunities, login to Elioplus, login on Elioplus, find new leads, find new partners, receive leads" />
</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <main class="transition-all duration-200">
        <section class="bg-gray">
            <div class="container">
                <div class="gap-30px lg:gap-50px flex flex-col justify-center items-center">
                    <div class="flex flex-col gap-10px text-center items-center w-full">
                        <a id="aElioplusLogo" runat="server" class="w-[100px] lg:w-[150px] block header-logo">
                            <svg width="113" height="48" viewBox="0 0 113 48" fill="none" xmlns="http://www.w3.org/2000/svg">
                                <path d="M25.5525 27.5987C25.5525 20.7196 20.8923 16.5504 14.3471 16.5504C6.75464 16.5504 1.57085 23.0647 1.57085 32.1326C1.57085 40.7836 6.59756 46.6725 14.5042 46.6725C21.5206 46.6725 24.034 43.4414 24.034 39.8976V37.2919H23.7722C22.4631 38.5948 20.3163 39.5328 16.2321 39.5328C12.8286 39.5328 10.3152 37.8652 10.3152 34.8426H13.5616C21.573 34.8426 25.5525 33.4355 25.5525 27.5987ZM17.4888 26.8169C17.4888 28.224 16.651 29.0579 12.5144 29.0579H10.1058C10.2105 25.6704 11.8337 23.4816 14.0853 23.4816C16.3892 23.4816 17.4888 24.6281 17.4888 26.8169ZM43.0752 42.1385V39.2201H41.2425C38.8863 39.2201 37.839 38.4906 37.839 36.1454V13.9447C37.839 10.2446 36.1111 8.4206 32.184 8.4206H29.4088V37.1356C29.4088 43.6499 33.1265 46.3598 37.9437 46.3598C40.1429 46.3598 42.1327 45.5781 42.8134 43.702C42.9705 43.233 43.0752 42.7118 43.0752 42.1385ZM55.248 10.7657C55.248 8.21214 53.1535 6.17969 50.5878 6.17969C47.9697 6.17969 45.8229 8.21214 45.8229 10.7657C45.8229 13.3193 47.9697 15.4039 50.5878 15.4039C53.1535 15.4039 55.248 13.3193 55.248 10.7657ZM59.4893 42.2949V39.4807H58.1279C55.7716 39.4807 54.8291 38.7511 54.8291 36.406V23.3774C54.8291 19.6773 53.0488 17.8533 49.1217 17.8533H46.3465V37.4483C46.3465 43.9626 49.8024 46.4119 54.6197 46.4119C57.9184 46.4119 59.4893 44.8485 59.4893 42.2949ZM61.3469 31.872C61.3469 40.6272 66.2689 46.6725 74.071 46.6725C82.3965 46.6725 86.9519 40.6272 86.9519 31.403C86.9519 22.1788 81.7681 16.5504 74.2804 16.5504C66.2689 16.5504 61.3469 23.0126 61.3469 31.872ZM78.3122 32.1847C78.3122 37.3962 76.3749 39.4286 74.4375 39.4286C71.7147 39.4286 69.9868 37.2398 69.9868 31.0382C69.9868 26.4 71.7147 23.8464 73.9662 23.8464C76.4272 23.8464 78.3122 25.5662 78.3122 32.1847Z" fill="#39B4EF" />
                                <path d="M98.178 6.17143C99.8903 6.17143 101.278 4.78991 101.278 3.08572C101.278 1.38152 99.8903 0 98.178 0C96.4657 0 95.0776 1.38152 95.0776 3.08572C95.0776 4.78991 96.4657 6.17143 98.178 6.17143Z" fill="#39B4EF" />
                                <path d="M98.178 15.0857C99.8903 15.0857 101.278 13.7042 101.278 12C101.278 10.2958 99.8903 8.91431 98.178 8.91431C96.4657 8.91431 95.0776 10.2958 95.0776 12C95.0776 13.7042 96.4657 15.0857 98.178 15.0857Z" fill="#39B4EF" />
                                <path d="M98.178 24C99.8903 24 101.278 22.6185 101.278 20.9143C101.278 19.2101 99.8903 17.8286 98.178 17.8286C96.4657 17.8286 95.0776 19.2101 95.0776 20.9143C95.0776 22.6185 96.4657 24 98.178 24Z" fill="#39B4EF" />
                                <path d="M89.9104 15.0857C91.6227 15.0857 93.0108 13.7042 93.0108 12C93.0108 10.2958 91.6227 8.91431 89.9104 8.91431C88.1981 8.91431 86.81 10.2958 86.81 12C86.81 13.7042 88.1981 15.0857 89.9104 15.0857Z" fill="#39B4EF" />
                                <path d="M107.135 15.0857C108.847 15.0857 110.235 13.7042 110.235 12C110.235 10.2958 108.847 8.91431 107.135 8.91431C105.422 8.91431 104.034 10.2958 104.034 12C104.034 13.7042 105.422 15.0857 107.135 15.0857Z" fill="#39B4EF" />
                            </svg>
                        </a>
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Sign in to your account</h3>
                        <p class="text-base lg:text-body">Enter your credentials to login to your ElioPlus account.</p>
                    </div>
                    <div class="flex flex-col gap-25px text-center items-center w-full">
                        <div class="w-[460px] max-w-full p-20px lg:p-40px flex flex-col bg-white shadow-card rounded-10px justify-center items-center" method="POST">
                            <div class="w-full flex flex-col gap-30px">
                                <div class="input-field">
                                    <label class="text-sm font-semibold" for="username">Email:</label>
                                    <asp:TextBox ID="TbxUsername" runat="server" MaxLength="50" type="username" placeholder="Enter your email address or your username" />
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
                                    <asp:Button ID="BtnLogin" Text="LOGIN TO MY ACCOUNT" CssClass="btn large font-bold text-sm bg-blue text-white w-full justify-center" type="submit" OnClick="BtnSubmit_OnClick" runat="server" />
                                </div>

                                <controls:MessageControl ID="UcMessageControlAlert" runat="server" />

                            </div>
                        </div>
                        <a id="aCreateAccount" runat="server" class="font-semibold text-sm text-blue underline" href="/free-sign-up">You don't have an account yet? Sign up for free now.</a>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
