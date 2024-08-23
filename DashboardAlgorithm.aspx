<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="DashboardAlgorithm.aspx.cs" Inherits="WdS.ElioPlus.DashboardAlgorithm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-left: 50px; margin-top: 25px; margin-bottom:25px;">
        <asp:Button ID="BtnRunAlgorithm" CssClass="btn-submit" runat="server" OnClick="BtnRunAlgorithm_OnClick" />        
        <asp:Label ID="LblMatchInfo" runat="server" />        
    </div>    
    <div>    
        <telerik:RadGrid ID="RdgConnections" CssClass="RgPosts" ShowHeader="false" ShowFooter="false" Skin="MetroTouch" runat="server" AutoGenerateColumns="false" OnNeedDataSource="RdgConnections_OnNeedDataSource" OnItemDataBound="RdgConnections_OnItemDataBound">
            <MasterTableView>                
                <Columns>                   
                    <telerik:GridTemplateColumn>
                        <ItemTemplate>
                            <table border="1" style="font-size: 13px; background-color:#d3542e; color:White; table-layout: fixed; width:2500px;">                    
                                <tr>
                                    <td style="width: 128px;" align="center">                            
                                        <asp:Label ID="Label3" runat="server" Text="Company"></asp:Label>
                                    </td>
                                    <td style="width: 60px;" align="center">
                                        <asp:Label ID="Label7" runat="server" Text="Type"></asp:Label>
                                    </td>
                                    <td style="width: 500px;" align="center">
                                        <asp:Label ID="Label8" runat="server" Text="Subcategories"></asp:Label>
                                    </td>
                                    <td style="width: 50px;" align="center">
                                        <asp:Label ID="Label2" runat="server" Text="Status"></asp:Label>
                                    </td>
                                    <td>
                                        <table border="1">
                                            <tr>
                                                <td colspan="15" align="center">
                                                    <asp:Label ID="Label9" runat="server" Text="Criteria"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label10" runat="server" Text="Fee"></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label11" runat="server" Text="Revenues"></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label12" runat="server" Text="Support"></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label13" runat="server" Text="CYears"></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label14" runat="server" Text="PYears"></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label15" runat="server" Text="Partners"></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label16" runat="server" Text="Tiers"></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label17" runat="server" Text="Training"></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label18" runat="server" Text="FTraining"></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label19" runat="server" Text="Material"></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label20" runat="server" Text="Certification"></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label21" runat="server" Text="Localization"></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label22" runat="server" Text="MDF"></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label23" runat="server" Text="Portal"></asp:Label>
                                                </td>
                                                <td style="width: 400px;" align="center">
                                                    <asp:Label ID="Label24" runat="server" Text="Country"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>                
                                </tr>
                                <tr>
                                    <td style="width: 128px;" align="center">
                                        <asp:Label ID="LblUserId" runat="server" Visible="false" Text='<%# Eval("id") %>'></asp:Label>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("company_name") %>'></asp:Label>
                                    </td>
                                    <td style="width: 60px;" align="center">
                                        <asp:Label ID="Label25" runat="server" Text='<%# Eval("company_type") %>'></asp:Label>
                                    </td>
                                    <td style="width: 500px;" align="center">
                                        <asp:Label ID="Label26" runat="server" Text='<%# Eval("subcategories") %>'></asp:Label>
                                    </td>
                                    <td style="width: 50px;" align="center">
                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("billing_type") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <table border="1">
                                            <tr>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label27" runat="server" Text='<%# Eval("fee") %>'></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label28" runat="server" Text='<%# Eval("revenues") %>'></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label29" runat="server" Text='<%# Eval("support") %>'></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label30" runat="server" Text='<%# Eval("company_years") %>'></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label31" runat="server" Text='<%# Eval("program_years") %>'></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label32" runat="server" Text='<%# Eval("partners") %>'></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label33" runat="server" Text='<%# Eval("tiers") %>'></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label34" runat="server" Text='<%# Eval("training") %>'></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label35" runat="server" Text='<%# Eval("free_training") %>'></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label36" runat="server" Text='<%# Eval("material") %>'></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label37" runat="server" Text='<%# Eval("certification") %>'></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label38" runat="server" Text='<%# Eval("localization") %>'></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label39" runat="server" Text='<%# Eval("mdf") %>'></asp:Label>
                                                </td>
                                                <td style="width: 75px;" align="center">
                                                    <asp:Label ID="Label40" runat="server" Text='<%# Eval("portal") %>'></asp:Label>
                                                </td>
                                                <td style="width: 400px;" align="center">
                                                    <asp:Label ID="Label41" runat="server" Text='<%# Eval("country") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr> 
                                <tr style="border: thin solid #000000;">
                                    <td colspan="4" align="center">
                                        <asp:Label ID="Label5" runat="server" Text="Matches"></asp:Label>
                                    </td>
                                </tr>                                                                           
                            </table>
                            <telerik:RadGrid ID="RdgMatches" CssClass="RgPosts" ShowHeader="false" ShowFooter="false" Skin="MetroTouch" runat="server" AutoGenerateColumns="false" OnItemDataBound="RdgMatches_OnItemDataBound">
                                <MasterTableView>
                                    <Columns>
                                        <telerik:GridTemplateColumn>
                                            <ItemTemplate>
                                                <table border="1" style="font-size: 13px; background-color:#3fb8af; table-layout:fixed; width:2500px;">
                                                    <tr>                                                    
                                                        <td style="width: 120px;" align="center">
                                                            <asp:Label ID="LblMID" runat="server" Visible="false" Text='<%# Eval("m_id") %>'></asp:Label>
                                                            <asp:Label ID="Label59" runat="server" Text='<%# Eval("m_company_name") %>'></asp:Label>
                                                            <asp:Button ID="BtnAddCon" runat="server" Text="Add Connection" OnClick="BtnAddCon_OnClick" />
                                                        </td>
                                                        <td style="width: 60px;" align="center">
                                                            <asp:Label ID="Label60" runat="server" Text='<%# Eval("m_company_type") %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 500px;" align="center">
                                                            <asp:Label ID="Label61" runat="server" Text='<%# Eval("m_subcategories") %>'></asp:Label>
                                                        </td>
                                                        <td style="width: 50px;" align="center">
                                                            <asp:Label ID="Label42" runat="server" Text='<%# Eval("m_billing_type") %>'></asp:Label>
                                                        </td>
                                                        <td>
                                                            <table border="1">
                                                                <tr>
                                                                    <td style="width: 75px;" align="center">
                                                                        <asp:Label ID="Label62" runat="server" Text='<%# Eval("m_fee") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="width: 75px;" align="center">
                                                                        <asp:Label ID="Label63" runat="server" Text='<%# Eval("m_revenues") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="width: 75px;" align="center">
                                                                        <asp:Label ID="Label64" runat="server" Text='<%# Eval("m_support") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="width: 75px;" align="center">
                                                                        <asp:Label ID="Label65" runat="server" Text='<%# Eval("m_company_years") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="width: 75px;" align="center">
                                                                        <asp:Label ID="Label66" runat="server" Text='<%# Eval("m_program_years") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="width: 75px;" align="center">
                                                                        <asp:Label ID="Label67" runat="server" Text='<%# Eval("m_partners") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="width: 75px;" align="center">
                                                                        <asp:Label ID="Label68" runat="server" Text='<%# Eval("m_tiers") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="width: 75px;" align="center">
                                                                        <asp:Label ID="Label69" runat="server" Text='<%# Eval("m_training") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="width: 75px;" align="center">
                                                                        <asp:Label ID="Label70" runat="server" Text='<%# Eval("m_free_training") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="width: 75px;" align="center">
                                                                        <asp:Label ID="Label71" runat="server" Text='<%# Eval("m_material") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="width: 75px;" align="center">
                                                                        <asp:Label ID="Label72" runat="server" Text='<%# Eval("m_certification") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="width: 75px;" align="center">
                                                                        <asp:Label ID="Label73" runat="server" Text='<%# Eval("m_localization") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="width: 75px;" align="center">
                                                                        <asp:Label ID="Label74" runat="server" Text='<%# Eval("m_mdf") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="width: 75px;" align="center">
                                                                        <asp:Label ID="Label75" runat="server" Text='<%# Eval("m_portal") %>'></asp:Label>
                                                                    </td>
                                                                    <td style="width: 400px;" align="center">
                                                                        <asp:Label ID="Label76" runat="server" Text='<%# Eval("m_country") %>'></asp:Label>
                                                                    </td>                                                                
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <div>
                                <asp:Label ID="LblConErrorMessage" runat="server" Visible="false" />
                            </div>
                            <div style="margin-top:10px;">
                                <asp:Label ID="Label1" runat="server" Text="Connections:"></asp:Label>
                                <asp:Label ID="LblConnections" runat="server"></asp:Label>
                            </div>
                            <div style="margin-bottom:50px;"></div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>                
            </MasterTableView>
        </telerik:RadGrid>       
    </div>
</asp:Content>
