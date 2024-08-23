<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpportunityForm.ascx.cs" Inherits="WdS.ElioPlus.Controls.Modals.OpportunityForm" %>

<telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
    <script type="text/javascript">
       
        function ClosePopUp() {
            $('#Modal').modal('hide');
        }
       
    </script>
</telerik:RadScriptBlock>

    <div class="modal-dialog" id="wndOpportunity" style="">
        <div class="modal-content">
            <div class="modal-header" style="text-align: center;">
                <h3 id="myOpportunityModalLabel">
                    <asp:Label ID="LblOpportunityMessageHeader" Text="Edit Opportunity!" runat="server" />
                </h3>
            </div>
            <div class="modal-body">
                <div style="">
                    <form class="form-horizontal col-sm-12">                        
                        <asp:Panel ID="PnlOpportunityMsg" runat="server">                            
                            <h2><asp:Label ID="Label8" runat="server" /></h2>
                            <div style="float:left; width:100%; display:inline-block;">                        
                                <h3><asp:Label ID="LblName" Text="Name and Occupation" runat="server" /></h3>
                                <hr />
                                <table class="qsf-fb">
                                    <tr class="step2-row">                                    
                                        <td class="step2-td2">
                                            <div style="padding: 5px;">
                                                <telerik:RadTextBox ID="RtbxId" Visible="false" MaxLength="10" Width="20" runat="server" />
                                            </div>
                                        </td>
                                        <td class="step2-td2">
                                            <div style="padding: 5px;">
                                                <telerik:RadTextBox ID="RtbxUserId" Visible="false" MaxLength="20" Width="20" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr class="step2-row">
                                        <td class="step2-td2">
                                            <div style="padding: 5px;">
                                                <telerik:RadTextBox ID="RtxLastname" EmptyMessage="Last name" MaxLength="80" Width="260" runat="server" />
                                            </div>
                                        </td>
                                        <td class="step2-td2">
                                            <div style="padding: 5px;">
                                                <telerik:RadTextBox ID="RtbxFirstname" EmptyMessage="First name" MaxLength="80" Width="260" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr class="step2-row">
                                        <td class="step2-td2">
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblLastnameError" runat="server" />
                                            </div>
                                        </td>
                                        <td class="step2-td2">
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblFirstnameError" runat="server" />
                                            </div>
                                        </td>
                                    </tr>                           
                                    <tr class="step2-row">                                    
                                        <td>
                                            <div style="padding:5px;">
                                                <telerik:RadTextBox ID="RtbxOrganization" EmptyMessage="Organization" MaxLength="100" Width="260" runat="server" />
                                            </div>
                                        </td>
                                        <td class="step2-row">
                                            <div style="padding:5px;">
                                                <telerik:RadTextBox ID="RtbxOccupation" EmptyMessage="Occupation" MaxLength="40" Width="260" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr class="step2-row">
                                        <td class="step2-td2">
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblOrganizationError" runat="server" />
                                            </div>
                                        </td>
                                        <td class="step2-td2">
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblOccupationError" runat="server" />
                                            </div>
                                        </td>
                                    </tr>   
                                </table>
                                <h3><asp:Label ID="Label5" Text="Contact details" runat="server" /></h3>
                                <hr />
                                <table class="qsf-fb">                                
                                    <tr class="step2-row">
                                        <td class="step2-td2">
                                            <div style="padding:5px;">
                                                <telerik:RadTextBox ID="RtbxEmail" EmptyMessage="Email" MaxLength="100" Width="260" runat="server" />
                                            </div>
                                        </td>
                                        <td class="step2-td2">
                                            <div style="padding:5px;">
                                                <telerik:RadTextBox ID="RtbxAddress" EmptyMessage="Address" MaxLength="100" Width="260" runat="server" />
                                            </div>
                                        </td>                                    
                                    </tr>
                                    <tr class="step2-row">
                                        <td class="step2-td2">
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblEmailError" runat="server" />
                                            </div>
                                        </td>
                                        <td class="step2-td2">
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblAddressError" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr class="step2-row">
                                        <td class="step2-td2">
                                            <div style="padding:5px;">
                                                <telerik:RadTextBox ID="RtbxPhone" onkeypress="return isNumberOnly(event);" EmptyMessage="Phone" MaxLength="20" Width="260" runat="server" />
                                            </div>
                                        </td>
                                        <td class="step2-td2">
                                            <div style="padding:5px;">
                                                <telerik:RadTextBox ID="RtbxWebsite" EmptyMessage="Website" MaxLength="50" Width="260" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr class="step2-row">
                                        <td class="step2-td2">
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblPhoneError" runat="server" />
                                            </div>
                                        </td>
                                        <td class="step2-td2">
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblWebsiteError" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr class="step2-row">
                                        <td class="step2-td2">
                                            <div style="padding:5px;">
                                                <telerik:RadTextBox ID="RtbxLinkedin" EmptyMessage="Linkedin" MaxLength="100" Width="260" runat="server" />
                                            </div>
                                        </td>
                                        <td class="step2-td2">
                                            <div style="padding:5px;">
                                                <telerik:RadTextBox ID="RtbxTwitter" EmptyMessage="Twitter" MaxLength="100" Width="260" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr class="step2-row">
                                        <td class="step2-td2">
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblLinkedinError" runat="server" />
                                            </div>
                                        </td>
                                        <td class="step2-td2">
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblTwitterError" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>  
                                <h3><asp:Label ID="Label1" Text="Additional information" runat="server" /></h3>
                                <hr />
                                <table>
                                    <tr class="step2-row">
                                        <td class="step2-td2">
                                            <div style="padding:5px;">
                                                <asp:Label ID="LblStep" Text="Opportunity Step" runat="server" />
                                            </div>
                                        </td>
                                        <td class="step2-td2">
                                            <div style="padding:5px;">
                                                <telerik:RadComboBox ID="RcbxSteps" Width="158" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr class="step2-row">
                                        <td class="step2-td2">
                                            <div style="color:#fd5840; padding:5px;">                                            
                                            </div>
                                        </td>
                                        <td class="step2-td2">
                                            <div style="color:#fd5840; padding:5px;">
                                                <asp:Label ID="LblStepsError" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>                        
                            <div style="margin-top:10px; display:inline-block;"></div>
                            
                            <table class="qsf-fb">
                                <tr>
                                    <td style="width: 150px; padding: 10px;">
                                    </td>
                                    <td>
                                        <div style="padding: 10px;">
                                            
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </form>
                </div>
                <div id="divOpportunityGeneralSuccess" runat="server" visible="false" class="alert alert-success" style="text-align: justify; margin-top:-40px; margin-bottom:-11px;">
                    <strong>
                        <asp:Label ID="LblOpportunityGeneralSuccess" Text="Done" runat="server" />
                    </strong>
                    <asp:Label ID="LblOpportunitySuccess" runat="server" />
                </div>
                <div id="divOpportunityGeneralFailure" runat="server" visible="false" class="alert alert-danger" style="text-align: justify; margin-top:-40px; margin-bottom:-11px;">
                    <strong>
                        <asp:Label ID="LblOpportunityGeneralFailure" Text="Error" runat="server" />
                    </strong>
                    <asp:Label ID="LblOpportunityFailure" runat="server" />
                </div>
            </div>
            <div id="divOpportunityFooter" class="modal-footer" runat="server">
                <asp:Button ID="RbtnSave" OnClick="RbtnSave_OnClick" Width="135" Text="Save" CssClass="btn btn-success" Style="text-align: center;" runat="server" />                   
                <asp:Button ID="RbtnClear" OnClick="RbtnClear_OnClick" Width="135" Text="Clear" CssClass="btn btn-success" Style="text-align: center;" runat="server" />
                <asp:Button ID="RbtnCancelEdit" OnClick="RbtnCancelEdit_OnClick" Visible="false" Text="Cancel Edit" CssClass="btn btn-success" Width="135" Style="text-align: center;" runat="server" />                  
            </div>
        </div>
    </div>
