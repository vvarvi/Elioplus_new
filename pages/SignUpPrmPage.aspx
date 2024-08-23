<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="SignUpPrmPage.aspx.cs" Inherits="WdS.ElioPlus.pages.SignUpPrmPage" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <<meta name="description" content="Sign up free on Elioplus, create a profile of your company as a software or SaaS vendor, reseller or API developer & get matched with new channel partners" />
    <meta name="keywords" content="submit your partner program, choose software products, find new partners, find new channel partners, find software companies, rate and review software products" />

    <script type="text/javascript" src="https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit" async defer></script>
    <script type="text/javascript">
        var onloadCallback = function () {
            grecaptcha.render('dvCaptcha', {
                'sitekey': '<%=ReCaptcha_Key %>',
                'callback': function (response) {
                    $.ajax({
                        type: "POST",
                        url: "SignUpPage.aspx/VerifyCaptcha",
                        data: "{response: '" + response + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
                            var captchaResponse = jQuery.parseJSON(r.d);
                            if (captchaResponse.success) {
                                $("[id*=txtCaptcha]").val(captchaResponse.success);
                                $("[id*=rfvCaptcha]").hide();
                            } else {
                                $("[id*=txtCaptcha]").val("");
                                $("[id*=rfvCaptcha]").show();
                                var error = captchaResponse["error-codes"][0];
                                $("[id*=rfvCaptcha]").html("RECaptcha error. " + error);
                            }
                        }
                    });
                }
            });
        };
    </script>

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
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">Create your account</h3>
                        <p class="text-base lg:text-body w-[546px] max-w-full">
                            <asp:Label ID="LblSignUpTitle" runat="server" />
                        </p>
                    </div>
                    <div class="flex flex-col gap-25px text-center items-center w-full">
                        <div class="w-[460px] max-w-full p-20px lg:p-40px flex flex-col bg-white shadow-card rounded-10px justify-center items-center" method="POST">
                            <div class="w-full flex flex-col gap-30px">
                                <div class="input-field" id="divEmail" runat="server">
                                    <label class="text-sm font-semibold" for="email">Business email:</label>
                                    <asp:TextBox ID="TbxEmail" runat="server" MaxLength="100" type="email" placeholder="Enter your business email address" required="true" />
                                </div>
                                <div class="input-field">
                                    <label class="text-sm font-semibold" for="username">Username:</label>
                                    <asp:TextBox ID="TbxUsername" runat="server" MaxLength="100" type="text" placeholder="Enter your business email address" required="true" />
                                </div>
                                <div class="input-field">
                                    <label class="text-sm font-semibold" for="password">Password:</label>
                                    <asp:TextBox ID="TbxPassword" runat="server" MaxLength="45" type="password" placeholder="Enter your password" required="true" />
                                </div>
                                <div class="input-field">
                                    <label class="text-sm font-semibold" for="repeat_password">Repeat Password:</label>
                                    <asp:TextBox ID="TbxRetypePassword" runat="server" MaxLength="45" type="password" placeholder="Repeat your entered your password" required="true" />
                                </div>
                                <div class="w-full flex flex-col gap-20px">
                                    <asp:Button ID="BtnSave" runat="server" Text="REGISTER MY ACCOUNT" OnClick="BtnSave_OnClick" CssClass="btn large font-bold text-sm bg-blue text-white w-full justify-center" type="submit" />
                                </div>
                                <div class="w-full flex flex-col">
                                    <controls:MessageControl ID="UcMessageControlAlert" runat="server" />
                                </div>
                            </div>
                            <div class="form-group mb-5">
                                <div id="dvCaptcha"></div>
                                <div class="input-group">
                                    <asp:TextBox ID="txtCaptcha" runat="server" Style="display: none" />
                                    <asp:RequiredFieldValidator ID="rfvCaptcha" ErrorMessage="Captcha validation is required." ControlToValidate="txtCaptcha" runat="server" ForeColor="Red" Display="Dynamic" />
                                </div>
                            </div>
                        </div>
                        <a id="aLogin" runat="server" class="font-semibold text-sm text-blue underline" href="/login">You already have an account? Sign in.</a>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
