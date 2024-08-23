<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsersRegistrationControl.ascx.cs" Inherits="WdS.ElioPlus.Controls.Dashboard.Charts.UsersRegistrationControl" %>

<%@ Register Assembly="Libero.FusionCharts" Namespace="Libero.FusionCharts.Control" TagPrefix="fcl" %>

<div style="padding-left:260px; padding-top:20px;">
    <h2>
        <asp:Label ID="LblUserPer" runat="server" />
        <telerik:RadComboBox ID="RcbxUserReg" runat="server" />
        <telerik:RadButton ID="BtnSubmit" OnClick="BtnSubmit_OnClick" runat="server">
            <ContentTemplate>
                <span>
                    <asp:Label ID="LblBtnSubmitText" runat="server" />
                </span>
            </ContentTemplate>
        </telerik:RadButton>
    </h2>
</div>
<fcl:FChart ID = "FUserRegChart" runat = "server" Width = "950" Height = "300"/>