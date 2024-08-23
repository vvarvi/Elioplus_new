<%@ Page Title="" Language="C#" MasterPageFile="~/Elioplus.Master" AutoEventWireup="true" CodeBehind="FaqPage.aspx.cs" Inherits="WdS.ElioPlus.pages.FaqPage" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">
    <meta name="description" content="Find answers for frequently asked questions from our users, customers and visitors." />
    <meta name="keywords" content="frequently asked questions elioplus" />
</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">
    <main class="transition-all duration-200">
        <section class="bg-gray opener">
            <div class="container">
                <div class="grid-cols-1 lg:grid-cols-2 grid gap-30px">
                    <div class="row-start-2 lg:row-start-1 gap-20px lg:gap-30px justify-center flex flex-col">
                        <div class="text-center lg:text-left gap-10px lg:gap-15px flex flex-col">
                            <h1 class="text-2.5xl lg:text-5xl font-bold font-inter">Frequently Asked Questions</h1>
                            <h2 class="text-2xl lg:text-3xl font-bold font-inter text-blue">We've got answers for you.</h2>
                        </div>
                        <p class="text-base lg:text-body text-center lg:text-left">At ElioPlus it's our priority to provide an excellent customer experience and provide any assistance possible.</p>
                        <div class="bg-white shadow-card flex w-full flex-wrap search-bar rounded-4px overflow-hidden" method="post">
                            <div class="input-field grow">
                                <input class="w-full" id="name" name="name" type="text" placeholder="Search for a question or topic">
                            </div>
                            <buttton class="w-[45px] h-[45px]  bg-blue flex items-center justify-center" type="submit">
                                <svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                                    <g clip-path="url(#clip0_40_8306)">
                                        <path d="M19.8102 18.9119L14.6466 13.8308C15.9988 12.3616 16.8296 10.4187 16.8296 8.28068C16.8289 3.7071 13.0618 0 8.41447 0C3.76713 0 0 3.7071 0 8.28068C0 12.8543 3.76713 16.5614 8.41447 16.5614C10.4224 16.5614 12.2641 15.8668 13.7107 14.7122L18.8944 19.8134C19.147 20.0622 19.5571 20.0622 19.8096 19.8134C20.0628 19.5646 20.0628 19.1607 19.8102 18.9119ZM8.41447 15.2873C4.48231 15.2873 1.29468 12.1504 1.29468 8.28068C1.29468 4.41101 4.48231 1.27403 8.41447 1.27403C12.3467 1.27403 15.5343 4.41101 15.5343 8.28068C15.5343 12.1504 12.3467 15.2873 8.41447 15.2873Z" fill="white" />
                                    </g><defs><clipPath id="clip0_40_8306"><rect width="20" height="20" fill="white" /></clipPath>
                                    </defs>
                                </svg>

                            </buttton>
                        </div>
                    </div>
                    <div class="justify-center flex">
                        <img src="/assets_out/images/faq/faqOpener.svg" alt="Elioplus Frequently Asked Questions and help support center">
                    </div>
                </div>
            </div>
            <div class="mt-30px wave">
                <img src="/assets_out/images/global/wave.svg" alt="Elioplus decorative wave">
            </div>
        </section>
        <section>
            <div class="container">
                <div class="gap-50px lg:gap-80px flex flex-col">
                    <div class="flex flex-col gap-30px">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">General FAQ</h3>
                        <div class="grid-cols-1 lg:grid-cols-2 gap-30px grid">
                            <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                    <div class="text-blue">Q.</div>
                                    <div>Which companies should create a profile as a Vendor?</div>
                                </summary>
                                <p class="px-10px lg:px-20px py-25px">
                                    Software, SaaS and cloud companies that offer a channel partner partner program and would like to increase their partners’ network by getting matched with channel partners that exact fit their needs.
                                </p>
                            </details>
                            <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                    <div class="text-blue">Q.</div>
                                    <div>Which companies should create a profile as a Channel Partner?</div>
                                </summary>
                                <p class="px-10px lg:px-20px py-25px">
                                    IT companies looking to resell new innovative SaaS solutions or cloud services, IT consultants, VARs, distributors, service providers, system integrators and more, all operating in software, SaaS or cloud industry.
                                </p>
                            </details>
                            <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                    <div class="text-blue">Q.</div>
                                    <div>Can I promote my partner programs for free?</div>
                                </summary>
                                <p class="px-10px lg:px-20px py-25px">
                                    Absolutely. You can create a profile page for your company and promote your partner program to our users and visitors for free.
                                </p>
                            </details>
                            <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                    <div class="text-blue">Q.</div>
                                    <div>Is there a way to promote my offering more actively?</div>
                                </summary>
                                <p class="px-10px lg:px-20px py-25px">
                                    Yes. You can choose to become a Premium user in order to get matched with those channel partners that exact fit your needs. Also, you will appear in the 1st page in search results every time someone searches for your industry, product or vertical, location etc.
                                </p>
                            </details>
                            <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                    <div class="text-blue">Q.</div>
                                    <div>Can I upgrade, downgrade or cancel my Premium plan?</div>
                                </summary>
                                <p class="px-10px lg:px-20px py-25px">
                                    Yes, it’s very easy to change your account’s status.
                                </p>
                            </details>
                            <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                    <div class="text-blue">Q.</div>
                                    <div>I am a channel partner. Why should I join ?</div>
                                </summary>
                                <p class="px-10px lg:px-20px py-25px">
                                    Apart from promoting your company, we make it easy to discover new innovative products and get information about the partner program that fit your needs. You can also make settings in order to get notified early and compare between competitive programs so you can take the best decision about which vendor to trust. Also, by using our management solution you can invite all your vendors into your account and collaborate with them. No matter how many vendors you are partnering with, now you can collaborate and receive relevant content, all in one dashboard so you can get the maximum functionality and save time and resources.
                                </p>
                            </details>
                            <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                    <div class="text-blue">Q.</div>
                                    <div>Are the company profiles and partner programs vetted?</div>
                                </summary>
                                <p class="px-10px lg:px-20px py-25px">
                                    We do our best to check and verify the data provided by our users but it their responsibility to give us accurate data. We want to keep the quality of content on our website very high and we are developing tools towards this direction in order to provide accurate and relevant information.
                                </p>
                            </details>
                        </div>
                    </div>
                    <div class="flex flex-col gap-30px">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">FAQ about matching process</h3>
                        <div class="grid-cols-1 lg:grid-cols-2 gap-30px grid">
                            <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                    <div class="text-blue">Q.</div>
                                    <div>What’s the matching process feature and how it works?</div>
                                </summary>
                                <p class="px-10px lg:px-20px py-25px">
                                    Our matching algorithm analyzes all the data that vendors and channel partners are providing during their registration process, like type of software or cloud service, targeted countries, program maturity, localization, commission based model or annual fee etc. By analyzing all the data from more than 30.000 quality companies of our database, the algorithm brings the most accurate matches inside your dashboard.
                                </p>
                            </details>
                            <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                    <div class="text-blue">Q.</div>
                                    <div>What happens when I go premium?</div>
                                </summary>
                                <p class="px-10px lg:px-20px py-25px">
                                    When you pay for our premium package you are getting matched with the partners that fit your needs according to the criteria you have provided to our system. You will find these companies on the “My Matches” tab of your dashboard and the list will be updated day by day with new prospects in order to get in touch with them. Also, when our system provides a match inside your dashboard, it automatically sends one notification email informing your partner about the potential partnership with your company.
                                </p>
                            </details>
                            <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                    <div class="text-blue">Q.</div>
                                    <div>Are the matches quality ones and is the data up-to-date?</div>
                                </summary>
                                <p class="px-10px lg:px-20px py-25px">
                                    We have more than 30.000 quality channel partners on our network in more than 80+ different software and cloud verticals. Also, our network is always up-to-date providing the information of the appropriate person to get in touch that’s why our conversion rate in terms of qualified partnerships is too high.
                                </p>
                            </details>
                            <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                    <div class="text-blue">Q.</div>
                                    <div>How can I see my possible partnership opportunities?</div>
                                </summary>
                                <p class="px-10px lg:px-20px py-25px">
                                    You can see how many potential partnership opportunities we can provide you on the My Matches tab on your dashboard. It shows the number of matches according to the products you had selected at your registration process! Also you can find out here: https://elioplus.com/search-details
                                </p>
                            </details>
                            <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                    <div class="text-blue">Q.</div>
                                    <div>How the matches look like and how often the platform provides them into my dashboard?</div>
                                </summary>
                                <p class="px-10px lg:px-20px py-25px">
                                    When you are a Premium user then our system provides day by day new matches with channel partners that exact fit your needs on the “My Matches” tab of your dashboard. We provide all the information needed in order to get in touch with them like company name, URL, email of the appropriate person to get in touch, LinkedIn profile of the appropriate person and more.
                                </p>
                            </details>
                            <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                    <div class="text-blue">Q.</div>
                                    <div>Why to submit and save the partnership criteria of the matching process?</div>
                                </summary>
                                <p class="px-10px lg:px-20px py-25px">
                                    By submitting the criteria you are giving all the necessary information to our system in order to match you with the right partners. This is a very important step and it’s free. You can always change your criteria and save them to our system.
                                </p>
                            </details>
                            <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                    <div class="text-blue">Q.</div>
                                    <div>When I upload the CSV file with my partners is the data private or public?</div>
                                </summary>
                                <p class="px-10px lg:px-20px py-25px">
                                    The data you provide us with your partners is private and will not be visible on Elioplus. You are uploading the CSV file in order to get matched with new partners and not with those you already have!
                                </p>
                            </details>
                        </div>
                    </div>
                    <div class="flex flex-col gap-30px">
                        <h3 class="text-2xl lg:text-3xl font-bold font-inter">FAQ about our ‘Lead Generation’ product</h3>
                        <div class="grid-cols-1 lg:grid-cols-2 gap-30px grid">
                            <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                    <div class="text-blue">Q.</div>
                                    <div>What’s the pricing?</div>
                                </summary>
                                <p class="px-10px lg:px-20px py-25px">
                                    The pricing is based on the country or regions that you are interested to gain access and is charged on a monthly basis.
                                </p>
                            </details>
                            <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                    <div class="text-blue">Q.</div>
                                    <div>Is there a limit in the number of the leads?</div>
                                </summary>
                                <p class="px-10px lg:px-20px py-25px">
                                    No. You get unlimited access to the leads and quotations that are uploaded each month on our platform.
                                </p>
                            </details>
                            <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                    <div class="text-blue">Q.</div>
                                    <div>What regions do you cover?</div>
                                </summary>
                                <p class="px-10px lg:px-20px py-25px">
                                    We get quotation requests and leads from every country on the planet so no matter where your company is based you can try Intent Signals.
                                </p>
                            </details>
                            <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                    <div class="text-blue">Q.</div>
                                    <div>Is there  a trial option?</div>
                                </summary>
                                <p class="px-10px lg:px-20px py-25px">
                                    While there is no trial option, we do offer a plan for local IT companies to get access to a number of leads for free per month. Please contact our team for more information.
                                </p>
                            </details>
                            <details class="text-sm lg:text-base faq-item bg-white shadow-card relative rounded-4px h-fit">
                                <summary class="px-10px py-15px lg:p-20px pr-30px lg:pr-40px flex gap-7px font-bold relative">
                                    <div class="text-blue">Q.</div>
                                    <div>What type of data do you provide?</div>
                                </summary>
                                <p class="px-10px lg:px-20px py-25px">
                                    On the quotation requests you will be sending your quote directly to the appropriate person and for the intent data you get all the necessary company details to get in touch with a specific lead.
                                </p>
                            </details>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
