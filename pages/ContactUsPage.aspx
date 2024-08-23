<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="ContactUsPage.aspx.cs" Inherits="WdS.ElioPlus.pages.ContactUsPage" %>

<%@ Register Src="~/Controls/AlertControls/MessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Get in touch with our sales or support team if you have a related question." />
    <meta name="keywords" content="elioplus contact us, support form" />

    <script type="text/javascript" src="https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit" async defer></script>
    <script type="text/javascript">
        var onloadCallback = function () {

            grecaptcha.render('dvCaptcha', {
                'sitekey': '<%=ReCaptcha_Key %>',
                'callback': function (response) {
                    $.ajax({
                        type: "POST",
                        url: "ContactUs.aspx/VerifyCaptcha",
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
    <asp:UpdatePanel runat="server" ID="UpdatePanel2">
        <ContentTemplate>
            <main class="transition-all duration-200">
                <section class="bg-gray opener">
                    <div class="container">
                        <div class="grid-cols-1 lg:grid-cols-2 grid gap-30px">
                            <div class="row-start-2 lg:row-start-1 gap-20px lg:gap-30px justify-center flex flex-col">
                                <div class="text-center lg:text-left gap-10px lg:gap-15px flex flex-col">
                                    <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Contact Us</h1>
                                    <h2 class="text-2xl lg:text-3xl font-bold font-inter text-blue">We are here to help our customers.</h2>
                                </div>
                                <p class="text-base lg:text-body text-center lg:text-left">We appreciate your interest in ElioPlus. You can contact us by filling the form below or through our social. If you need immediate assistance you can send us a message through our chat portal.</p>
                            </div>
                            <div class="justify-center flex">
                                <img src="/assets_out/images/contact-us/opener.svg" alt="Contact Elioplus and get help and support from our team">
                            </div>
                        </div>
                    </div>
                    <div class="mt-30px wave">
                        <img src="/assets_out/images/global/wave.svg" alt="Elioplus decorative wave">
                    </div>
                </section>
                <section>
                    <div class="container">
                        <div class="grid-cols-1 lg:grid-cols-2 gap-50px grid">
                            <div class="gap-30px lg:gap-50px flex flex-col justify-center">
                                <div class="text-center lg:text-left flex flex-col gap-10px">
                                    <h3 class="text-2xl lg:text-3xl font-bold font-inter">Let's start a conversation</h3>
                                    <p class="w-full text-base lg:text-body">Please fill out the form below and someone from our team will get back to you as soon as possible.</p>
                                </div>
                                <div class="w-full p-20px lg:p-40px flex flex-col bg-white shadow-card rounded-10px justify-center items-center" method="POST">
                                    <div class="w-full flex flex-col gap-30px pb-30px">
                                        <div class="input-field">
                                            <label class="text-sm font-semibold" for="email">Email address:</label>
                                            <asp:TextBox ID="TbxEmail" runat="server" MaxLength="45" type="email" placeholder="Enter your business email address" />
                                        </div>
                                        <div class="input-field">
                                            <label class="text-sm font-semibold" for="name">Name:</label>
                                            <asp:TextBox ID="TbxName" runat="server" MaxLength="45" type="text" placeholder="Enter your full name" />
                                        </div>
                                        <div class="input-field">
                                            <label class="text-sm font-semibold" for="phone">Phone: <span class="text-textGray">(optional)</span></label>
                                            <asp:TextBox ID="TbxPhone" runat="server" MaxLength="15" type="text" placeholder="Enter your phone number" />
                                        </div>
                                        <div class="input-field">
                                            <label class="text-sm font-semibold" for="subject">Subject:</label>
                                            <asp:TextBox ID="TbxSubject" runat="server" MaxLength="45" type="text" placeholder="Enter the topic of the discussion" />
                                        </div>
                                        <div class="input-field">
                                            <label class="text-sm font-semibold" for="message">Message:</label>
                                            <asp:TextBox ID="TbxMessage" runat="server" MaxLength="2000" TextMode="MultiLine" Rows="3" placeholder="Enter your message" />
                                        </div>
                                        <div class="w-full flex flex-col gap-20px">
                                            <asp:Button ID="BtnSend" OnClick="BtnSend_OnClick" Text="SEND MESSAGE" class="btn large font-bold text-sm bg-blue text-white w-full justify-center" type="submit" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group" style="display: inline-block; float: left; margin-left: 15px;">
                                        <div id="dvCaptcha"></div>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtCaptcha" runat="server" Style="display: none" />
                                            <asp:RequiredFieldValidator ID="rfvCaptcha" ErrorMessage="Captcha validation is required." ControlToValidate="txtCaptcha" runat="server" ForeColor="Red" Display="Dynamic" />
                                        </div>
                                    </div>
                                    <div class="w-full flex flex-col">
                                        <controls:MessageControl ID="UcMessageAlert" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="flex items-end">
                                <div class="flex flex-col gap-30px w-full">
                                    <div class="flex flex-col gap-15px w-full">
                                        <h3 class="text-2xl lg:text-2.5xl font-bold font-inter">Additional contact options</h3>
                                        <div class="h-[2px] divider"></div>
                                    </div>
                                    <div class="gap-30px lg:gap-50px flex flex-col w-full">
                                        <div class="gap-20px lg:gap-25px flex flex-col w-full">
                                            <div class="gap-5px lg:gap-5px flex flex-col w-full">
                                                <strong class="text-base lg:text-lg">Email:</strong>
                                                <h4 class="text-base lg:text-lg text-textGray font-bold">info@elioplus.com</h4>
                                            </div>
                                            <div class="w-full flex gap-15px">
                                                <a class="btn copy font-bold text-sm text-blue" href="#" data-email="info@elioplus.com"><span>COPY EMAIL</span></a><a class="btn font-bold text-sm bg-blue text-white" href="mailto:info@elioplus.com"><span>SEND EMAIL</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                                </defs></svg>
                                                </a>
                                            </div>
                                        </div>
                                        <div class="gap-20px lg:gap-25px flex flex-col w-full">
                                            <div class="gap-5px lg:gap-5px flex flex-col w-full">
                                                <strong class="text-base lg:text-lg">US office:</strong>
                                                <h4 class="text-base lg:text-lg text-textGray"><strong>Address:</strong> 108 West 13th Street, Suite 105 Wilmington, Delaware 19801</h4>
                                            </div>
                                            <div class="w-full flex gap-15px">
                                                <a class="btn font-bold text-sm text-blue" href="https://www.google.com/maps/place/108+W+13th+St+%23105,+Wilmington,+DE+19801,+%CE%97%CE%BD%CF%89%CE%BC%CE%AD%CE%BD%CE%B5%CF%82+%CE%A0%CE%BF%CE%BB%CE%B9%CF%84%CE%B5%CE%AF%CE%B5%CF%82/@39.7487052,-75.5502282,17z/data=!3m1!4b1!4m5!3m4!1s0x89c6fd3f043a96d3:0xc22ce469047034d3!8m2!3d39.7487011!4d-75.5476533?entry=ttu" target="_blank"><span>GET DIRECTIONS</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                                </defs></svg>
                                                </a>
                                            </div>
                                        </div>
                                        <div class="gap-20px lg:gap-25px flex flex-col w-full">
                                            <div class="gap-5px lg:gap-5px flex flex-col w-full">
                                                <strong class="text-base lg:text-lg">Europe office:</strong>
                                                <h4 class="text-base lg:text-lg text-textGray"><strong>Address:</strong> 33 Saronikou St , 163 45, Ilioupoli, Athens, Greece</h4>
                                            </div>
                                            <div class="w-full flex gap-15px">
                                                <a class="btn font-bold text-sm text-blue" href="https://www.google.com/maps/place/Elioplus/@37.9458828,23.7512186,17z/data=!3m1!4b1!4m6!3m5!1s0x14a1bd6aa46d8f73:0x9f6fbac797a48673!8m2!3d37.9458786!4d23.7537935!16s%2Fg%2F11f308dv68" target="_blank"><span>GET DIRECTIONS</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                                </defs></svg>
                                                </a>
                                            </div>
                                        </div>
                                        <div class="gap-10px lg:gap-15px flex flex-col w-full">
                                            <strong class="text-base lg:text-lg">Social:</strong>
                                            <div class="flex flex-row gap-15px items-center">
                                                <a class="socialLink rounded-full flex items-center justify-center" href="https://www.facebook.com/elioplus">
                                                    <svg width="12" height="13" viewBox="0 0 12 13" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                        <mask id="mask0_75_24467" style="mask-type: luminance" maskUnits="userSpaceOnUse" x="0" y="0" width="12" height="13">
                                                            <path d="M11.6928 0.773926H0.154297V12.3811H11.6928V0.773926Z" fill="white" />
                                                        </mask>
                                                        <g mask="url(#mask0_75_24467)">
                                                            <path d="M6.8171 12.3811V7.08689H8.58291L8.84784 5.02305H6.8171V3.70558C6.8171 3.10824 6.98133 2.70115 7.83381 2.70115L8.91932 2.70071V0.854729C8.73159 0.830189 8.08721 0.773926 7.33721 0.773926C5.7711 0.773926 4.69891 1.73556 4.69891 3.50118V5.02305H2.92773V7.08689H4.69891V12.3811H6.8171Z" fill="#39B4EF" />
                                                        </g>
                                                    </svg>
                                                </a><a class="socialLink rounded-full flex items-center justify-center" href="https://www.linkedin.com/company/elio/">
                                                    <svg width="15" height="16" viewBox="0 0 15 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                        <path d="M13.3608 13.7186V9.4994C13.3608 7.4258 12.9144 5.8418 10.4952 5.8418C9.32882 5.8418 8.55122 6.4754 8.23442 7.0802H8.20562V6.029H5.91602V13.7186H8.30642V9.9026C8.30642 8.8946 8.49362 7.9298 9.73202 7.9298C10.956 7.9298 10.9704 9.0674 10.9704 9.9602V13.7042H13.3608V13.7186Z" fill="#39B4EF" />
                                                        <path d="M2.02832 6.0293H4.41872V13.7189H2.02832V6.0293Z" fill="#39B4EF" />
                                                        <path d="M3.22322 2.19873C2.46002 2.19873 1.84082 2.81793 1.84082 3.58113C1.84082 4.34433 2.46002 4.97793 3.22322 4.97793C3.98642 4.97793 4.60562 4.34433 4.60562 3.58113C4.60562 2.81793 3.98642 2.19873 3.22322 2.19873V2.19873Z" fill="#39B4EF" />
                                                    </svg>
                                                </a><a class="socialLink rounded-full flex items-center justify-center" href="https://twitter.com/elioplus">
                                                    <svg width="15" height="16" viewBox="0 0 15 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                                                        <g clip-path="url(#clip0_75_24495)">
                                                            <path d="M14.8004 3.49399C14.2649 3.72889 13.6943 3.88459 13.0994 3.96019C13.7114 3.59479 14.1785 3.02059 14.3981 2.32849C13.8275 2.66869 13.1975 2.90899 12.5261 3.04309C11.9843 2.46619 11.2121 2.10889 10.3697 2.10889C8.73529 2.10889 7.41949 3.43549 7.41949 5.06179C7.41949 5.29579 7.43929 5.52079 7.48789 5.73499C5.03359 5.61529 2.86189 4.43899 1.40299 2.64709C1.14829 3.08899 0.998891 3.59479 0.998891 4.13929C0.998891 5.16169 1.52539 6.06799 2.31019 6.59269C1.83589 6.58369 1.37059 6.44599 0.976391 6.22909C0.976391 6.23809 0.976391 6.24979 0.976391 6.26149C0.976391 7.69609 1.99969 8.88769 3.34159 9.16219C3.10129 9.22789 2.83939 9.25939 2.56759 9.25939C2.37859 9.25939 2.18779 9.24859 2.00869 9.20899C2.39119 10.3781 3.47659 11.2376 4.76719 11.2655C3.76279 12.0512 2.48749 12.5246 1.10689 12.5246C0.864791 12.5246 0.632591 12.5138 0.400391 12.4841C1.70809 13.3274 3.25789 13.8089 4.92919 13.8089C10.3616 13.8089 13.3316 9.30889 13.3316 5.40829C13.3316 5.27779 13.3271 5.15179 13.3208 5.02669C13.9067 4.61089 14.399 4.09159 14.8004 3.49399Z" fill="#39B4EF" />
                                                        </g>
                                                        <defs>
                                                            <clipPath id="clip0_75_24495">
                                                                <rect width="14.4" height="14.4" fill="white" transform="translate(0.400391 0.758789)" />
                                                            </clipPath>
                                                        </defs>
                                                    </svg>
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <section class="bg-white">
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
                                        <a id="aFaq" runat="server" href="/faq" class="btn large font-bold text-sm xl:text-base text-blue"><span>VIEW FREQUENTLY ASKED QUESTIONS</span><svg width="9" height="10" viewBox="0 0 9 10" fill="none" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0_75_23830)"><path d="M2.28634 0.643855L1.98948 0.938663C1.89677 1.03144 1.8457 1.15488 1.8457 1.28681C1.8457 1.41867 1.89677 1.54225 1.98948 1.63503L5.35258 4.99799L1.98575 8.36482C1.89304 8.45745 1.84204 8.58104 1.84204 8.7129C1.84204 8.84475 1.89304 8.96841 1.98575 9.06112L2.28078 9.356C2.47263 9.548 2.78515 9.548 2.977 9.356L7.00003 5.34738C7.09267 5.25475 7.15794 5.13131 7.15794 4.99828V4.99674C7.15794 4.86482 7.09259 4.74138 7.00003 4.64874L2.9879 0.643855C2.89527 0.551074 2.7681 0.500147 2.63624 0.5C2.50431 0.5 2.3789 0.551074 2.28634 0.643855Z" fill="#39B4EF" /></g><defs><clipPath id="clip0_75_23830"><rect width="9" height="9" fill="white" transform="matrix(0 -1 1 0 0 9.5)" /></clipPath>
                                        </defs></svg>
                                        </a>
                                    </div>
                                </div>
                                <div class="flex items-center">
                                    <img class="max-w-[350px] lg:max-w-full" src="/assets_out/images/contact-us/faq.svg" alt="Visit the Elioplus FAQ to get answers for your questions" loading="lazy">
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </main>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
