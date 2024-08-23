<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="RequestQuotePage.aspx.cs" Inherits="WdS.ElioPlus.pages.RequestQuotePage" %>

<%@ Register Src="~/Controls/AlertControls/MessageAlertControl.ascx" TagName="MessageAlertControl" TagPrefix="controls" %>
<%@ Register Src="~/Controls/AlertControls/MessageControl2Msgs.ascx" TagName="MessageControl2Msgs" TagPrefix="controls" %>

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
                                    <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Get a Quote</h1>
                                </div>
                                <div class="px-10px lg:px-25px flex items-center justify-between w-full relative" id="quoteSteps">
                                    <a id="aStep1" runat="server" class="stepTab flex flex-col gap-10px items-center justify-center text-center relative z-2 active">
                                        <div class="w-[30px] h-[30px]  rounded-full border-2 border-blue bg-white circle"></div>
                                        <h4 class="text-sm lg:text-base font-semibold text-textGray">
                                            <asp:Label ID="LblTitleStep1" runat="server" />
                                        </h4>
                                    </a><a id="aStep2" runat="server" class="stepTab flex flex-col gap-10px items-center justify-center text-center relative z-2">
                                        <div class="w-[30px] h-[30px]  rounded-full border-2 border-blue bg-white circle"></div>
                                        <h4 class="text-sm lg:text-base font-semibold text-textGray">
                                            <asp:Label ID="LblTitleStep2" runat="server" />
                                        </h4>
                                    </a><a id="aStep3" runat="server" class="stepTab flex flex-col gap-10px items-center justify-center text-center relative z-2">
                                        <div class="w-[30px] h-[30px]  rounded-full border-2 border-blue bg-white circle"></div>
                                        <h4 class="text-sm lg:text-base font-semibold text-textGray">
                                            <asp:Label ID="LblTitleStep3" runat="server" />
                                        </h4>
                                    </a>
                                </div>
                                <div class="w-full p-20px lg:p-40px flex flex-col bg-white shadow-card rounded-10px justify-center items-center" method="POST">

                                    <div class="w-full flex flex-col gap-30px">
                                        <div class="w-full flex flex-col gap-30px step-content active" id="divStepOne" runat="server">
                                            <h3 class="text-base lg:text-lg font-bold font-inter">
                                                <asp:Label ID="LblTitleStep1F" runat="server" />
                                            </h3>
                                            <div class="grid-cols-1 lg:grid-cols-2 gap-30px lg:gap-50px grid">
                                                <div class="input-field">
                                                    <label class="text-sm font-semibold" for="product">
                                                        <asp:Label ID="LblProduct" runat="server" />
                                                    </label>
                                                    <asp:TextBox ID="TbxProduct" runat="server" MaxLength="100" name="product" type="text" placeholder="Enter Product/Technology" />
                                                </div>
                                                <div class="input-field">
                                                    <label class="text-sm font-semibold" for="items">
                                                        <asp:Label ID="LblNumberUnits" runat="server" />
                                                    </label>
                                                    <asp:TextBox ID="TbxNumberUnits" MaxLength="20" runat="server" name="items" type="number" placeholder="Enter No. of Users/Items" />
                                                </div>
                                            </div>
                                            <div class="input-field">
                                                <label class="text-sm font-semibold" for="description">
                                                    <asp:Label ID="LblMessage" runat="server" />
                                                </label>
                                                <asp:TextBox ID="TbxMessage" TextMode="MultiLine" Rows="5" MaxLength="500" runat="server" name="description" placeholder="Enter your message and describe your requirements" />
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
                                                    <label class="text-sm font-semibold" for="email">
                                                        <asp:Label ID="LblCompanyEmail" runat="server" />
                                                    </label>
                                                    <asp:TextBox ID="TbxCompanyEmail" runat="server" MaxLength="100" name="email" type="email" placeholder="Enter your Work Email" />
                                                </div>
                                                <div class="input-field">
                                                    <label class="text-sm font-semibold" for="company">
                                                        <asp:Label ID="LblBusinessName" runat="server" />
                                                    </label>
                                                    <asp:TextBox ID="TbxBusinessName" MaxLength="100" runat="server" name="company" type="text" placeholder="Enter your Company Name" />
                                                </div>
                                            </div>
                                            <div class="grid-cols-1 lg:grid-cols-2 gap-30px lg:gap-50px grid">
                                                <div class="input-field">
                                                    <label class="text-sm font-semibold" for="country">
                                                        <asp:Label ID="LblCountry" runat="server" Text="Country" />
                                                    </label>
                                                    <asp:DropDownList ID="DdlCountries" runat="server" name="country" type="text" placeholder="Select your Country" />
                                                </div>
                                                <div class="input-field">
                                                    <label class="text-sm font-semibold" for="city">
                                                        <asp:Label ID="LblCity" runat="server" />
                                                    </label>
                                                    <asp:TextBox ID="TbxCity" MaxLength="20" runat="server" name="city" type="text" placeholder="Enter your City" />
                                                </div>
                                            </div>
                                            <div class="input-field">
                                                <label class="text-sm font-semibold" for="phone">
                                                    <asp:Label ID="LblPhoneNumber" runat="server" />
                                                </label>
                                                <asp:TextBox ID="TbxPhoneNumber" MaxLength="20" runat="server" name="phone" type="number" placeholder="Enter your Phone Number" />
                                            </div>
                                            <div class="input-field">
                                                <div style="float: left;">
                                                    <asp:CheckBox ID="CbxAgree" Checked="true" runat="server" />
                                                </div>
                                                <div style="float: right; margin-left: 25px; margin-top: -35px; text-align:justify;">
                                                    <asp:Label ID="LblAgree" Text="I agree to the terms & conditions and to receive communications by Elioplus and its partners concerning the submitted request for quotation." runat="server" />
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
                                                <asp:Button ID="BtnProceed" runat="server" OnClick="BtnProceed_Click" Text="Submit" CssClass="nav-btn btn large font-bold text-sm bg-blue text-white justify-center" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="w-full flex flex-col gap-30px pt-10px">
                                        <controls:MessageAlertControl ID="UcMessageAlert" runat="server" />
                                    </div>
                                </div>
                                <div class="text-center gap-5px lg:gap-10px flex flex-col items-center w-full">
                                    <controls:MessageAlertControl ID="UcMessageAlertTop" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </main>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
