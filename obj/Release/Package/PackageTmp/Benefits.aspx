<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Benefits.aspx.cs" Inherits="WdS.ElioPlus.Benefits" %>

<asp:Content ID="HowItWorksHead" ContentPlaceHolderID="HeadContent" runat="server">    
    <meta name="description" content="Discover great IT partnerships on Elioplus. We match the best software, SaaS and hardware vendors with resellers and API developers globally."/>
    <meta name="keywords" content="value added resellers, software distributors, SaaS vendors, SaaS resellers" /> 
</asp:Content>

<asp:Content ID="HowItWorksMain" ContentPlaceHolderID="MainContent" runat="server">    
    <div class="body_wrap">       
        <!-- header -->
        <div class="header header_thin" style="background-image: url(images/temp/slider_1_2.jpg)">
            <div class="header_title">
                <h1><asp:Label ID="Lbl2" runat="server" ForeColor="GrayText" /></h1>
            </div>
        </div>
        <!--/ header -->       
        <!-- middle -->
        <div id="middle" class="full_width">
            <div class="container clearfix">
                <!-- content -->
                <div class="content">
                    <div class="entry">                       
                        <div id="divVendors" class="divider_space_thin" runat="server">
                            <div class="how-holder">
                               <div class="how-content txt-color">
                                    <asp:PlaceHolder ID="PhBenefitsContent" runat="server" />                                    
                               </div>
                            </div>
                        </div>                        
                    </div>
                </div>
                <!--/ content -->
            </div>
        </div>
        <!--/ middle -->   
        <!-- popular brands -->
	    <div class="middle_row row_light_gray brand_list">
            <asp:PlaceHolder ID="PhFeatureCompanies" runat="server" />
	    </div>     
    </div>
</asp:Content>

