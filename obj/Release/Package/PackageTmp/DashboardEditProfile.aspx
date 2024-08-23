<%@ Page Title="" Language="C#" MasterPageFile="~/DashboardMaster.Master" AutoEventWireup="true" CodeBehind="DashboardEditProfile.aspx.cs" Inherits="WdS.ElioPlus.DashboardEditProfile" %>

<%@ Register Src="/Controls/Payment/Stripe_Packets_Ctrl.ascx" TagName="UcStripe" TagPrefix="controls" %>
<%@ Register Src="~/Controls/Dashboard/AlertControls/DAMessageControl.ascx" TagName="MessageControl" TagPrefix="controls" %>

<asp:Content ID="DashEditHead" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="DashEditMain" ContentPlaceHolderID="MainContent" runat="server">

    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">

            function isNumberOnly(evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;

                return true;
            }
        </script>

        <script type="text/javascript">
            function OpenConfDeleteUserPopUp() {
                $('#divConfirmDeletion').modal('show');
            }
        </script>
        <script type="text/javascript">
            function CloseConfDeleteUserPopUp() {
                $('#divConfirmDeletion').modal('hide');
            }
        </script>

        <script type="text/javascript">

            function CloseAddMoreEmailsPopUp() {
                $('#AddMoreEmailsModal').modal('hide');
            }

        </script>

        <script id="UpdateAPIs" type="text/javascript">
            function UpdateAPIs() {
                var x1 = document.getElementById("CbxAPIBusServ").checked;
                var x2 = document.getElementById("CbxAPIMedEnter").checked;
                var x3 = document.getElementById("CbxAPIRetEcom").checked;
                var x4 = document.getElementById("CbxAPIGeol").checked;
                var x5 = document.getElementById("CbxAPISoc").checked;
                var x6 = document.getElementById("CbxAPIHeal").checked;

                if (x1 == true) {
                    document.getElementById('<%= HdnAPIBusServ.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnAPIBusServ.ClientID%>').value = "0";
                }

                if (x2 == true) {
                    document.getElementById('<%= HdnAPIMedEnter.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnAPIMedEnter.ClientID%>').value = "0";
                }

                if (x3 == true) {
                    document.getElementById('<%= HdnAPIRetEcom.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnAPIRetEcom.ClientID%>').value = "0";
                }

                if (x4 == true) {
                    document.getElementById('<%= HdnAPIGeol.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnAPIGeol.ClientID%>').value = "0";
                }

                if (x5 == true) {
                    document.getElementById('<%= HdnAPISoc.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnAPISoc.ClientID%>').value = "0";
                }

                if (x6 == true) {
                    document.getElementById('<%= HdnAPIHeal.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnAPIHeal.ClientID%>').value = "0";
                }
            }
        </script>
        <script id="SetAPIs" type="text/javascript">
            function SetAPIs() {
                var x1 = document.getElementById('<%= HdnAPIBusServ.ClientID%>').value;
                var x2 = document.getElementById('<%= HdnAPIMedEnter.ClientID%>').value;
                var x3 = document.getElementById('<%= HdnAPIRetEcom.ClientID%>').value;
                var x4 = document.getElementById('<%= HdnAPIGeol.ClientID%>').value;
                var x5 = document.getElementById('<%= HdnAPISoc.ClientID%>').value;
                var x6 = document.getElementById('<%= HdnAPIHeal.ClientID%>').value;

                if (x1 == "1") {
                    document.getElementById("CbxAPIBusServ").checked = true;
                }
                else {
                    document.getElementById("CbxAPIBusServ").checked = false;
                }

                if (x2 == "1") {
                    document.getElementById("CbxAPIMedEnter").checked = true;
                }
                else {
                    document.getElementById("CbxAPIMedEnter").checked = false;
                }

                if (x3 == "1") {
                    document.getElementById("CbxAPIRetEcom").checked = true;
                }
                else {
                    document.getElementById("CbxAPIRetEcom").checked = false;
                }

                if (x4 == "1") {
                    document.getElementById("CbxAPIGeol").checked = true;
                }
                else {
                    document.getElementById("CbxAPIGeol").checked = false;
                }

                if (x5 == "1") {
                    document.getElementById("CbxAPISoc").checked = true;
                }
                else {
                    document.getElementById("CbxAPISoc").checked = false;
                }

                if (x6 == "1") {
                    document.getElementById("CbxAPIHeal").checked = true;
                }
                else {
                    document.getElementById("CbxAPIHeal").checked = false;
                }
            }
        </script>
        <script id="UpdateMarkets" type="text/javascript">
            function UpdateMarkets() {
                var x1 = document.getElementById("CbxMarkConsum").checked;
                var x2 = document.getElementById("CbxMarkSOHO").checked;
                var x3 = document.getElementById("CbxMarkSmallMid").checked;
                var x4 = document.getElementById("CbxMarkEnter").checked;

                if (x1 == true) {
                    document.getElementById('<%= HdnMarkConsum.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnMarkConsum.ClientID%>').value = "0";
                }

                if (x2 == true) {
                    document.getElementById('<%= HdnMarkSOHO.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnMarkSOHO.ClientID%>').value = "0";
                }

                if (x3 == true) {
                    document.getElementById('<%= HdnMarkSmallMid.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnMarkSmallMid.ClientID%>').value = "0";
                }

                if (x4 == true) {
                    document.getElementById('<%= HdnMarkEnter.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnMarkEnter.ClientID%>').value = "0";
                }
            }
        </script>
        <script id="SetMarkets" type="text/javascript">
            function SetMarkets() {
                var x1 = document.getElementById('<%= HdnMarkConsum.ClientID%>').value;
                var x2 = document.getElementById('<%= HdnMarkSOHO.ClientID%>').value;
                var x3 = document.getElementById('<%= HdnMarkSmallMid.ClientID%>').value;
                var x4 = document.getElementById('<%= HdnMarkEnter.ClientID%>').value;


                if (x1 == "1") {
                    document.getElementById("CbxMarkConsum").checked = true;
                }
                else {
                    document.getElementById("CbxMarkConsum").checked = false;
                }

                if (x2 == "1") {
                    document.getElementById("CbxMarkSOHO").checked = true;
                }
                else {
                    document.getElementById("CbxMarkSOHO").checked = false;
                }

                if (x3 == "1") {
                    document.getElementById("CbxMarkSmallMid").checked = true;
                }
                else {
                    document.getElementById("CbxMarkSmallMid").checked = false;
                }

                if (x4 == "1") {
                    document.getElementById("CbxMarkEnter").checked = true;
                }
                else {
                    document.getElementById("CbxMarkEnter").checked = false;
                }
            }
        </script>
        <script id="UpdateIndustries" type="text/javascript">
            function UpdateIndustries() {
                var x1 = document.getElementById("CbxIndAdvMark").checked;
                var x2 = document.getElementById("CbxIndCommun").checked;
                var x3 = document.getElementById("CbxIndConsWeb").checked;
                var x4 = document.getElementById("CbxIndDigMed").checked;
                var x5 = document.getElementById("CbxIndEcom").checked;
                var x6 = document.getElementById("CbxIndEduc").checked;
                var x7 = document.getElementById("CbxIndEnter").checked;
                var x8 = document.getElementById("CbxIndEntGam").checked;
                var x9 = document.getElementById("CbxIndHard").checked;
                var x10 = document.getElementById("CbxIndMob").checked;
                var x11 = document.getElementById("CbxIndNetHos").checked;
                var x12 = document.getElementById("CbxIndSocMed").checked;
                var x13 = document.getElementById("CbxIndSoft").checked;

                var x103 = document.getElementById("CbxInd14").checked;
                var x104 = document.getElementById("CbxInd15").checked;
                var x105 = document.getElementById("CbxInd16").checked;
                var x106 = document.getElementById("CbxInd17").checked;
                var x107 = document.getElementById("CbxInd18").checked;
                var x108 = document.getElementById("CbxInd19").checked;
                var x109 = document.getElementById("CbxInd20").checked;
                var x110 = document.getElementById("CbxInd21").checked;
                var x111 = document.getElementById("CbxInd22").checked;
                var x112 = document.getElementById("CbxInd23").checked;
                var x113 = document.getElementById("CbxInd24").checked;
                var x114 = document.getElementById("CbxInd25").checked;
                var x115 = document.getElementById("CbxInd26").checked;

                var x116 = document.getElementById("CbxInd27").checked;
                var x117 = document.getElementById("CbxInd28").checked;
                var x118 = document.getElementById("CbxInd29").checked;
                var x119 = document.getElementById("CbxInd30").checked;
                var x120 = document.getElementById("CbxInd31").checked;
                var x121 = document.getElementById("CbxInd32").checked;
                var x122 = document.getElementById("CbxInd33").checked;
                var x123 = document.getElementById("CbxInd34").checked;
                var x124 = document.getElementById("CbxInd35").checked;
                var x125 = document.getElementById("CbxInd36").checked;
                var x126 = document.getElementById("CbxInd37").checked;
                var x127 = document.getElementById("CbxInd38").checked;
                var x128 = document.getElementById("CbxInd39").checked;

                var x129 = document.getElementById("CbxInd40").checked;
                var x130 = document.getElementById("CbxInd41").checked;
                var x131 = document.getElementById("CbxInd42").checked;

                if (x1 == true) {
                    document.getElementById('<%= HdnIndAdvMark.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnIndAdvMark.ClientID%>').value = "0";
                }

                if (x2 == true) {
                    document.getElementById('<%= HdnIndCommun.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnIndCommun.ClientID%>').value = "0";
                }

                if (x3 == true) {
                    document.getElementById('<%= HdnIndConsWeb.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnIndConsWeb.ClientID%>').value = "0";
                }

                if (x4 == true) {
                    document.getElementById('<%= HdnIndDigMed.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnIndDigMed.ClientID%>').value = "0";
                }

                if (x5 == true) {
                    document.getElementById('<%= HdnIndEcom.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnIndEcom.ClientID%>').value = "0";
                }

                if (x6 == true) {
                    document.getElementById('<%= HdnIndEduc.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnIndEduc.ClientID%>').value = "0";
                }

                if (x7 == true) {
                    document.getElementById('<%= HdnIndEnter.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnIndEnter.ClientID%>').value = "0";
                }

                if (x8 == true) {
                    document.getElementById('<%= HdnIndEntGam.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnIndEntGam.ClientID%>').value = "0";
                }

                if (x9 == true) {
                    document.getElementById('<%= HdnIndHard.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnIndHard.ClientID%>').value = "0";
                }

                if (x10 == true) {
                    document.getElementById('<%= HdnIndMob.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnIndMob.ClientID%>').value = "0";
                }

                if (x11 == true) {
                    document.getElementById('<%= HdnIndNetHos.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnIndNetHos.ClientID%>').value = "0";
                }

                if (x12 == true) {
                    document.getElementById('<%= HdnIndSocMed.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnIndSocMed.ClientID%>').value = "0";
                }

                if (x13 == true) {
                    document.getElementById('<%= HdnIndSoft.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnIndSoft.ClientID%>').value = "0";
                }

                if (x103 == true) {
                    document.getElementById('<%= HdnInd14.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd14.ClientID%>').value = "0";
                }

                if (x104 == true) {
                    document.getElementById('<%= HdnInd15.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd15.ClientID%>').value = "0";
                }

                if (x105 == true) {
                    document.getElementById('<%= HdnInd16.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd16.ClientID%>').value = "0";
                }

                if (x106 == true) {
                    document.getElementById('<%= HdnInd17.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd17.ClientID%>').value = "0";
                }

                if (x107 == true) {
                    document.getElementById('<%= HdnInd18.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd18.ClientID%>').value = "0";
                }

                if (x108 == true) {
                    document.getElementById('<%= HdnInd19.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd19.ClientID%>').value = "0";
                }

                if (x109 == true) {
                    document.getElementById('<%= HdnInd20.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd20.ClientID%>').value = "0";
                }

                if (x110 == true) {
                    document.getElementById('<%= HdnInd21.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd21.ClientID%>').value = "0";
                }

                if (x111 == true) {
                    document.getElementById('<%= HdnInd22.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd22.ClientID%>').value = "0";
                }

                if (x112 == true) {
                    document.getElementById('<%= HdnInd23.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd23.ClientID%>').value = "0";
                }

                if (x113 == true) {
                    document.getElementById('<%= HdnInd24.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd24.ClientID%>').value = "0";
                }

                if (x114 == true) {
                    document.getElementById('<%= HdnInd25.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd25.ClientID%>').value = "0";
                }

                if (x115 == true) {
                    document.getElementById('<%= HdnInd26.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd26.ClientID%>').value = "0";
                }

                if (x116 == true) {
                    document.getElementById('<%= HdnInd27.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd27.ClientID%>').value = "0";
                }

                if (x117 == true) {
                    document.getElementById('<%= HdnInd28.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd28.ClientID%>').value = "0";
                }

                if (x118 == true) {
                    document.getElementById('<%= HdnInd29.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd29.ClientID%>').value = "0";
                }

                if (x119 == true) {
                    document.getElementById('<%= HdnInd30.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd30.ClientID%>').value = "0";
                }

                if (x120 == true) {
                    document.getElementById('<%= HdnInd31.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd31.ClientID%>').value = "0";
                }

                if (x121 == true) {
                    document.getElementById('<%= HdnInd32.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd32.ClientID%>').value = "0";
                }

                if (x122 == true) {
                    document.getElementById('<%= HdnInd33.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd33.ClientID%>').value = "0";
                }

                if (x123 == true) {
                    document.getElementById('<%= HdnInd34.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd34.ClientID%>').value = "0";
                }

                if (x124 == true) {
                    document.getElementById('<%= HdnInd35.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd35.ClientID%>').value = "0";
                }

                if (x125 == true) {
                    document.getElementById('<%= HdnInd36.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd36.ClientID%>').value = "0";
                }

                if (x126 == true) {
                    document.getElementById('<%= HdnInd37.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd37.ClientID%>').value = "0";
                }

                if (x127 == true) {
                    document.getElementById('<%= HdnInd38.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd38.ClientID%>').value = "0";
                }

                if (x128 == true) {
                    document.getElementById('<%= HdnInd39.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd39.ClientID%>').value = "0";
                }

                if (x129 == true) {
                    document.getElementById('<%= HdnInd40.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd40.ClientID%>').value = "0";
                }

                if (x130 == true) {
                    document.getElementById('<%= HdnInd41.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd41.ClientID%>').value = "0";
                }

                if (x131 == true) {
                    document.getElementById('<%= HdnInd42.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnInd42.ClientID%>').value = "0";
                }
            }
        </script>
        <script id="SetIndustries" type="text/javascript">
            function SetIndustries() {
                var x1 = document.getElementById('<%= HdnIndAdvMark.ClientID%>').value;
                var x2 = document.getElementById('<%= HdnIndCommun.ClientID%>').value;
                var x3 = document.getElementById('<%= HdnIndConsWeb.ClientID%>').value;
                var x4 = document.getElementById('<%= HdnIndDigMed.ClientID%>').value;
                var x5 = document.getElementById('<%= HdnIndEcom.ClientID%>').value;
                var x6 = document.getElementById('<%= HdnIndEduc.ClientID%>').value;
                var x7 = document.getElementById('<%= HdnIndEnter.ClientID%>').value;
                var x8 = document.getElementById('<%= HdnIndEntGam.ClientID%>').value;
                var x9 = document.getElementById('<%= HdnIndHard.ClientID%>').value;
                var x10 = document.getElementById('<%= HdnIndMob.ClientID%>').value;
                var x11 = document.getElementById('<%= HdnIndNetHos.ClientID%>').value;
                var x12 = document.getElementById('<%= HdnIndSocMed.ClientID%>').value;
                var x13 = document.getElementById('<%= HdnIndSoft.ClientID%>').value;

                var x103 = document.getElementById('<%= HdnInd14.ClientID%>').value;
                var x104 = document.getElementById('<%= HdnInd15.ClientID%>').value;
                var x105 = document.getElementById('<%= HdnInd16.ClientID%>').value;
                var x106 = document.getElementById('<%= HdnInd17.ClientID%>').value;
                var x107 = document.getElementById('<%= HdnInd18.ClientID%>').value;
                var x108 = document.getElementById('<%= HdnInd19.ClientID%>').value;
                var x109 = document.getElementById('<%= HdnInd20.ClientID%>').value;
                var x110 = document.getElementById('<%= HdnInd21.ClientID%>').value;
                var x111 = document.getElementById('<%= HdnInd22.ClientID%>').value;
                var x112 = document.getElementById('<%= HdnInd23.ClientID%>').value;
                var x113 = document.getElementById('<%= HdnInd24.ClientID%>').value;
                var x114 = document.getElementById('<%= HdnInd25.ClientID%>').value;
                var x115 = document.getElementById('<%= HdnInd26.ClientID%>').value;

                var x116 = document.getElementById('<%= HdnInd27.ClientID%>').value;
                var x117 = document.getElementById('<%= HdnInd28.ClientID%>').value;
                var x118 = document.getElementById('<%= HdnInd29.ClientID%>').value;
                var x119 = document.getElementById('<%= HdnInd30.ClientID%>').value;
                var x120 = document.getElementById('<%= HdnInd31.ClientID%>').value;
                var x121 = document.getElementById('<%= HdnInd32.ClientID%>').value;
                var x122 = document.getElementById('<%= HdnInd33.ClientID%>').value;
                var x123 = document.getElementById('<%= HdnInd34.ClientID%>').value;
                var x124 = document.getElementById('<%= HdnInd35.ClientID%>').value;
                var x125 = document.getElementById('<%= HdnInd36.ClientID%>').value;
                var x126 = document.getElementById('<%= HdnInd37.ClientID%>').value;
                var x127 = document.getElementById('<%= HdnInd38.ClientID%>').value;
                var x128 = document.getElementById('<%= HdnInd39.ClientID%>').value;

                var x129 = document.getElementById('<%= HdnInd40.ClientID%>').value;
                var x130 = document.getElementById('<%= HdnInd41.ClientID%>').value;
                var x131 = document.getElementById('<%= HdnInd42.ClientID%>').value;

                if (x1 == "1") {
                    document.getElementById("CbxIndAdvMark").checked = true;
                }
                else {
                    document.getElementById("CbxIndAdvMark").checked = false;
                }

                if (x2 == "1") {
                    document.getElementById("CbxIndCommun").checked = true;
                }
                else {
                    document.getElementById("CbxIndCommun").checked = false;
                }

                if (x3 == "1") {
                    document.getElementById("CbxIndConsWeb").checked = true;
                }
                else {
                    document.getElementById("CbxIndConsWeb").checked = false;
                }

                if (x4 == "1") {
                    document.getElementById("CbxIndDigMed").checked = true;
                }
                else {
                    document.getElementById("CbxIndDigMed").checked = false;
                }

                if (x5 == "1") {
                    document.getElementById("CbxIndEcom").checked = true;
                }
                else {
                    document.getElementById("CbxIndEcom").checked = false;
                }

                if (x6 == "1") {
                    document.getElementById("CbxIndEduc").checked = true;
                }
                else {
                    document.getElementById("CbxIndEduc").checked = false;
                }

                if (x7 == "1") {
                    document.getElementById("CbxIndEnter").checked = true;
                }
                else {
                    document.getElementById("CbxIndEnter").checked = false;
                }

                if (x8 == "1") {
                    document.getElementById("CbxIndEntGam").checked = true;
                }
                else {
                    document.getElementById("CbxIndEntGam").checked = false;
                }

                if (x9 == "1") {
                    document.getElementById("CbxIndHard").checked = true;
                }
                else {
                    document.getElementById("CbxIndHard").checked = false;
                }

                if (x10 == "1") {
                    document.getElementById("CbxIndMob").checked = true;
                }
                else {
                    document.getElementById("CbxIndMob").checked = false;
                }

                if (x11 == "1") {
                    document.getElementById("CbxIndNetHos").checked = true;
                }
                else {
                    document.getElementById("CbxIndNetHos").checked = false;
                }

                if (x12 == "1") {
                    document.getElementById("CbxIndSocMed").checked = true;
                }
                else {
                    document.getElementById("CbxIndSocMed").checked = false;
                }

                if (x13 == "1") {
                    document.getElementById("CbxIndSoft").checked = true;
                }
                else {
                    document.getElementById("CbxIndSoft").checked = false;
                }

                if (x103 == "1") {
                    document.getElementById("CbxInd14").checked = true;
                }
                else {
                    document.getElementById("CbxInd14").checked = false;
                }

                if (x104 == "1") {
                    document.getElementById("CbxInd15").checked = true;
                }
                else {
                    document.getElementById("CbxInd15").checked = false;
                }

                if (x105 == "1") {
                    document.getElementById("CbxInd16").checked = true;
                }
                else {
                    document.getElementById("CbxInd16").checked = false;
                }

                if (x106 == "1") {
                    document.getElementById("CbxInd17").checked = true;
                }
                else {
                    document.getElementById("CbxInd17").checked = false;
                }

                if (x107 == "1") {
                    document.getElementById("CbxInd18").checked = true;
                }
                else {
                    document.getElementById("CbxInd18").checked = false;
                }

                if (x108 == "1") {
                    document.getElementById("CbxInd19").checked = true;
                }
                else {
                    document.getElementById("CbxInd19").checked = false;
                }

                if (x109 == "1") {
                    document.getElementById("CbxInd20").checked = true;
                }
                else {
                    document.getElementById("CbxInd20").checked = false;
                }

                if (x110 == "1") {
                    document.getElementById("CbxInd21").checked = true;
                }
                else {
                    document.getElementById("CbxInd21").checked = false;
                }

                if (x111 == "1") {
                    document.getElementById("CbxInd22").checked = true;
                }
                else {
                    document.getElementById("CbxInd22").checked = false;
                }

                if (x112 == "1") {
                    document.getElementById("CbxInd23").checked = true;
                }
                else {
                    document.getElementById("CbxInd23").checked = false;
                }

                if (x113 == "1") {
                    document.getElementById("CbxInd24").checked = true;
                }
                else {
                    document.getElementById("CbxInd24").checked = false;
                }

                if (x114 == "1") {
                    document.getElementById("CbxInd25").checked = true;
                }
                else {
                    document.getElementById("CbxInd25").checked = false;
                }

                if (x115 == "1") {
                    document.getElementById("CbxInd26").checked = true;
                }
                else {
                    document.getElementById("CbxInd26").checked = false;
                }

                if (x116 == "1") {
                    document.getElementById("CbxInd27").checked = true;
                }
                else {
                    document.getElementById("CbxInd27").checked = false;
                }

                if (x117 == "1") {
                    document.getElementById("CbxInd28").checked = true;
                }
                else {
                    document.getElementById("CbxInd28").checked = false;
                }

                if (x118 == "1") {
                    document.getElementById("CbxInd29").checked = true;
                }
                else {
                    document.getElementById("CbxInd29").checked = false;
                }

                if (x119 == "1") {
                    document.getElementById("CbxInd30").checked = true;
                }
                else {
                    document.getElementById("CbxInd30").checked = false;
                }

                if (x120 == "1") {
                    document.getElementById("CbxInd31").checked = true;
                }
                else {
                    document.getElementById("CbxInd31").checked = false;
                }

                if (x121 == "1") {
                    document.getElementById("CbxInd32").checked = true;
                }
                else {
                    document.getElementById("CbxInd32").checked = false;
                }

                if (x122 == "1") {
                    document.getElementById("CbxInd33").checked = true;
                }
                else {
                    document.getElementById("CbxInd33").checked = false;
                }

                if (x123 == "1") {
                    document.getElementById("CbxInd34").checked = true;
                }
                else {
                    document.getElementById("CbxInd34").checked = false;
                }

                if (x124 == "1") {
                    document.getElementById("CbxInd35").checked = true;
                }
                else {
                    document.getElementById("CbxInd35").checked = false;
                }

                if (x125 == "1") {
                    document.getElementById("CbxInd36").checked = true;
                }
                else {
                    document.getElementById("CbxInd36").checked = false;
                }

                if (x126 == "1") {
                    document.getElementById("CbxInd37").checked = true;
                }
                else {
                    document.getElementById("CbxInd37").checked = false;
                }

                if (x127 == "1") {
                    document.getElementById("CbxInd38").checked = true;
                }
                else {
                    document.getElementById("CbxInd38").checked = false;
                }

                if (x128 == "1") {
                    document.getElementById("CbxInd39").checked = true;
                }
                else {
                    document.getElementById("CbxInd39").checked = false;
                }

                if (x129 == "1") {
                    document.getElementById("CbxInd40").checked = true;
                }
                else {
                    document.getElementById("CbxInd40").checked = false;
                }

                if (x130 == "1") {
                    document.getElementById("CbxInd41").checked = true;
                }
                else {
                    document.getElementById("CbxInd41").checked = false;
                }

                if (x131 == "1") {
                    document.getElementById("CbxInd42").checked = true;
                }
                else {
                    document.getElementById("CbxInd42").checked = false;
                }
            }
        </script>
        <script id="UpdatePrograms" type="text/javascript">
            function UpdatePrograms() {
                var x1 = document.getElementById("CbxProgWhiteL").checked;
                var x2 = document.getElementById("CbxProgResel").checked;
                var x3 = document.getElementById("CbxProgVAR").checked;
                var x4 = document.getElementById("CbxProgDistr").checked;
                var x5 = document.getElementById("CbxProgAPIprg").checked;
                var x6 = document.getElementById("CbxProgSysInteg").checked;
                var x7 = document.getElementById("CbxProgServProv").checked;

                if (x1 == true) {
                    document.getElementById('<%= HdnProgWhiteL.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnProgWhiteL.ClientID%>').value = "0";
                }

                if (x2 == true) {
                    document.getElementById('<%= HdnProgResel.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnProgResel.ClientID%>').value = "0";
                }

                if (x3 == true) {
                    document.getElementById('<%= HdnProgVAR.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnProgVAR.ClientID%>').value = "0";
                }

                if (x4 == true) {
                    document.getElementById('<%= HdnProgDistr.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnProgDistr.ClientID%>').value = "0";
                }

                if (x5 == true) {
                    document.getElementById('<%= HdnProgAPIprg.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnProgAPIprg.ClientID%>').value = "0";
                }

                if (x6 == true) {
                    document.getElementById('<%= HdnProgSysInteg.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnProgSysInteg.ClientID%>').value = "0";
                }

                if (x7 == true) {
                    document.getElementById('<%= HdnProgServProv.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= HdnProgServProv.ClientID%>').value = "0";
                }
            }
        </script>
        <script id="SetPrograms" type="text/javascript">
            function SetPrograms() {
                var x1 = document.getElementById('<%= HdnProgWhiteL.ClientID%>').value;
                var x2 = document.getElementById('<%= HdnProgResel.ClientID%>').value;
                var x3 = document.getElementById('<%= HdnProgVAR.ClientID%>').value;
                var x4 = document.getElementById('<%= HdnProgDistr.ClientID%>').value;
                var x5 = document.getElementById('<%= HdnProgAPIprg.ClientID%>').value;
                var x6 = document.getElementById('<%= HdnProgSysInteg.ClientID%>').value;
                var x7 = document.getElementById('<%= HdnProgServProv.ClientID%>').value;

                if (x1 == "1") {
                    document.getElementById("CbxProgWhiteL").checked = true;
                }
                else {
                    document.getElementById("CbxProgWhiteL").checked = false;
                }

                if (x2 == "1") {
                    document.getElementById("CbxProgResel").checked = true;
                }
                else {
                    document.getElementById("CbxProgResel").checked = false;
                }

                if (x3 == "1") {
                    document.getElementById("CbxProgVAR").checked = true;
                }
                else {
                    document.getElementById("CbxProgVAR").checked = false;
                }

                if (x4 == "1") {
                    document.getElementById("CbxProgDistr").checked = true;
                }
                else {
                    document.getElementById("CbxProgDistr").checked = false;
                }

                if (x5 == "1") {
                    document.getElementById("CbxProgAPIprg").checked = true;
                }
                else {
                    document.getElementById("CbxProgAPIprg").checked = false;
                }

                if (x6 == "1") {
                    document.getElementById("CbxProgSysInteg").checked = true;
                }
                else {
                    document.getElementById("CbxProgSysInteg").checked = false;
                }

                if (x7 == "1") {
                    document.getElementById("CbxProgServProv").checked = true;
                }
                else {
                    document.getElementById("CbxProgServProv").checked = false;
                }
            }
        </script>
        <script id="UpdateVerticals" type="text/javascript">
            function UpdateVerticals() {
                var x1_1 = document.getElementById("Cbx1_1").checked;
                var x1_2 = document.getElementById("Cbx1_2").checked;
                var x1_3 = document.getElementById("Cbx1_3").checked;
                var x1_4 = document.getElementById("Cbx1_4").checked;
                var x1_5 = document.getElementById("Cbx1_5").checked;
                var x1_6 = document.getElementById("Cbx1_6").checked;
                var x1_7 = document.getElementById("Cbx1_7").checked;
                var x1_8 = document.getElementById("Cbx1_8").checked;
                var x1_9 = document.getElementById("Cbx1_9").checked;
                var x1_10 = document.getElementById("Cbx1_10").checked;
                var x1_11 = document.getElementById("Cbx1_11").checked;
                var x1_12 = document.getElementById("Cbx1_12").checked;
                var x1_13 = document.getElementById("Cbx1_13").checked;
                var x1_14 = document.getElementById("Cbx1_14").checked;
                var x1_15 = document.getElementById("Cbx1_15").checked;
                var x1_16 = document.getElementById("Cbx1_16").checked;
                var x1_17 = document.getElementById("Cbx1_17").checked;
                var x1_18 = document.getElementById("Cbx1_18").checked;

                var x2_1 = document.getElementById("Cbx2_1").checked;
                var x2_2 = document.getElementById("Cbx2_2").checked;
                var x2_3 = document.getElementById("Cbx2_3").checked;
                var x2_4 = document.getElementById("Cbx2_4").checked;
                var x2_5 = document.getElementById("Cbx2_5").checked;
                var x2_6 = document.getElementById("Cbx2_6").checked;

                var x3_1 = document.getElementById("Cbx3_1").checked;
                var x3_2 = document.getElementById("Cbx3_2").checked;
                var x3_3 = document.getElementById("Cbx3_3").checked;

                var x4_1 = document.getElementById("Cbx4_1").checked;
                var x4_2 = document.getElementById("Cbx4_2").checked;
                var x4_3 = document.getElementById("Cbx4_3").checked;
                var x4_4 = document.getElementById("Cbx4_4").checked;
                var x4_5 = document.getElementById("Cbx4_5").checked;
                var x4_6 = document.getElementById("Cbx4_6").checked;
                var x4_7 = document.getElementById("Cbx4_7").checked;
                var x4_8 = document.getElementById("Cbx4_8").checked;
                var x4_9 = document.getElementById("Cbx4_9").checked;
                var x4_10 = document.getElementById("Cbx4_10").checked;
                var x4_11 = document.getElementById("Cbx4_11").checked;
                var x4_12 = document.getElementById("Cbx4_12").checked;
                var x4_13 = document.getElementById("Cbx4_13").checked;
                var x4_14 = document.getElementById("Cbx4_14").checked;
                var x4_15 = document.getElementById("Cbx4_15").checked;
                var x4_16 = document.getElementById("Cbx4_16").checked;
                var x4_17 = document.getElementById("Cbx4_17").checked;
                var x4_18 = document.getElementById("Cbx4_18").checked;
                var x4_19 = document.getElementById("Cbx4_19").checked;
                var x4_20 = document.getElementById("Cbx4_20").checked;
                var x4_21 = document.getElementById("Cbx4_21").checked;
                var x4_22 = document.getElementById("Cbx4_22").checked;

                var x5_1 = document.getElementById("Cbx5_1").checked;
                var x5_2 = document.getElementById("Cbx5_2").checked;
                var x5_3 = document.getElementById("Cbx5_3").checked;
                var x5_4 = document.getElementById("Cbx5_4").checked;
                var x5_5 = document.getElementById("Cbx5_5").checked;

                var x6_1 = document.getElementById("Cbx6_1").checked;
                var x6_2 = document.getElementById("Cbx6_2").checked;
                var x6_3 = document.getElementById("Cbx6_3").checked;
                var x6_4 = document.getElementById("Cbx6_4").checked;
                var x6_5 = document.getElementById("Cbx6_5").checked;

                var x7_1 = document.getElementById("Cbx7_1").checked;
                var x7_2 = document.getElementById("Cbx7_2").checked;
                var x7_3 = document.getElementById("Cbx7_3").checked;
                var x7_4 = document.getElementById("Cbx7_4").checked;
                var x7_5 = document.getElementById("Cbx7_5").checked;
                var x7_6 = document.getElementById("Cbx7_6").checked;
                var x7_7 = document.getElementById("Cbx7_7").checked;

                var x8_1 = document.getElementById("Cbx8_1").checked;
                var x8_2 = document.getElementById("Cbx8_2").checked;
                var x8_3 = document.getElementById("Cbx8_3").checked;
                var x8_4 = document.getElementById("Cbx8_4").checked;
                var x8_5 = document.getElementById("Cbx8_5").checked;
                var x8_6 = document.getElementById("Cbx8_6").checked;
                var x8_7 = document.getElementById("Cbx8_7").checked;
                var x8_8 = document.getElementById("Cbx8_8").checked;
                var x8_9 = document.getElementById("Cbx8_9").checked;

                var x9_1 = document.getElementById("Cbx9_1").checked;
                var x9_2 = document.getElementById("Cbx9_2").checked;
                var x9_3 = document.getElementById("Cbx9_3").checked;
                var x9_4 = document.getElementById("Cbx9_4").checked;
                var x9_5 = document.getElementById("Cbx9_5").checked;
                var x9_6 = document.getElementById("Cbx9_6").checked;
                var x9_7 = document.getElementById("Cbx9_7").checked;
                var x9_8 = document.getElementById("Cbx9_8").checked;
                var x9_9 = document.getElementById("Cbx9_9").checked;
                var x9_10 = document.getElementById("Cbx9_10").checked;
                var x9_11 = document.getElementById("Cbx9_11").checked;
                var x9_12 = document.getElementById("Cbx9_12").checked;
                var x9_13 = document.getElementById("Cbx9_13").checked;

                var x10_1 = document.getElementById("Cbx10_1").checked;
                var x10_2 = document.getElementById("Cbx10_2").checked;
                var x10_3 = document.getElementById("Cbx10_3").checked;
                var x10_4 = document.getElementById("Cbx10_4").checked;
                var x10_5 = document.getElementById("Cbx10_5").checked;
                var x10_6 = document.getElementById("Cbx10_6").checked;
                var x10_7 = document.getElementById("Cbx10_7").checked;
                var x10_8 = document.getElementById("Cbx10_8").checked;
                var x10_9 = document.getElementById("Cbx10_9").checked;

                var x11_1 = document.getElementById("Cbx11_1").checked;
                var x11_2 = document.getElementById("Cbx11_2").checked;
                var x11_3 = document.getElementById("Cbx11_3").checked;
                var x11_4 = document.getElementById("Cbx11_4").checked;
                var x11_5 = document.getElementById("Cbx11_5").checked;
                var x11_6 = document.getElementById("Cbx11_6").checked;
                var x11_7 = document.getElementById("Cbx11_7").checked;
                var x11_8 = document.getElementById("Cbx11_8").checked;
                var x11_9 = document.getElementById("Cbx11_9").checked;
                var x11_10 = document.getElementById("Cbx11_10").checked;
                var x11_11 = document.getElementById("Cbx11_11").checked;
                var x11_12 = document.getElementById("Cbx11_12").checked;
                var x11_13 = document.getElementById("Cbx11_13").checked;
                var x11_14 = document.getElementById("Cbx11_14").checked;
                var x11_15 = document.getElementById("Cbx11_15").checked;
                var x11_16 = document.getElementById("Cbx11_16").checked;

                var x12_1 = document.getElementById("Cbx12_1").checked;
                var x12_2 = document.getElementById("Cbx12_2").checked;
                var x12_3 = document.getElementById("Cbx12_3").checked;
                var x12_4 = document.getElementById("Cbx12_4").checked;

                var x13_1 = document.getElementById("Cbx13_1").checked;
                var x13_2 = document.getElementById("Cbx13_2").checked;
                var x13_3 = document.getElementById("Cbx13_3").checked;

                var x14_1 = document.getElementById("Cbx14_1").checked;
                var x14_2 = document.getElementById("Cbx14_2").checked;
                var x14_3 = document.getElementById("Cbx14_3").checked;
                var x14_4 = document.getElementById("Cbx14_4").checked;
                var x14_5 = document.getElementById("Cbx14_5").checked;
                var x14_6 = document.getElementById("Cbx14_6").checked;
                var x14_7 = document.getElementById("Cbx14_7").checked;
                var x14_8 = document.getElementById("Cbx14_8").checked;
                var x14_9 = document.getElementById("Cbx14_9").checked;
                var x14_10 = document.getElementById("Cbx14_10").checked;
                var x14_11 = document.getElementById("Cbx14_11").checked;
                var x14_12 = document.getElementById("Cbx14_12").checked;

                var x15_1 = document.getElementById("Cbx15_1").checked;
                var x15_2 = document.getElementById("Cbx15_2").checked;
                var x15_3 = document.getElementById("Cbx15_3").checked;
                var x15_4 = document.getElementById("Cbx15_4").checked;
                var x15_5 = document.getElementById("Cbx15_5").checked;
                var x15_6 = document.getElementById("Cbx15_6").checked;
                var x15_7 = document.getElementById("Cbx15_7").checked;
                var x15_8 = document.getElementById("Cbx15_8").checked;

                var x16_1 = document.getElementById("Cbx16_1").checked;
                var x16_2 = document.getElementById("Cbx16_2").checked;
                var x16_3 = document.getElementById("Cbx16_3").checked;
                var x16_4 = document.getElementById("Cbx16_4").checked;
                var x16_5 = document.getElementById("Cbx16_5").checked;
                var x16_6 = document.getElementById("Cbx16_6").checked;
                var x16_7 = document.getElementById("Cbx16_7").checked;
                var x16_8 = document.getElementById("Cbx16_8").checked;
                var x16_9 = document.getElementById("Cbx16_9").checked;

                if (x1_1 == true) {
                    document.getElementById('<%= Hdn1_1.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn1_1.ClientID%>').value = "0";
                }

                if (x1_2 == true) {
                    document.getElementById('<%= Hdn1_2.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn1_2.ClientID%>').value = "0";
                }

                if (x1_3 == true) {
                    document.getElementById('<%= Hdn1_3.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn1_3.ClientID%>').value = "0";
                }

                if (x1_4 == true) {
                    document.getElementById('<%= Hdn1_4.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn1_4.ClientID%>').value = "0";
                }

                if (x1_5 == true) {
                    document.getElementById('<%= Hdn1_5.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn1_5.ClientID%>').value = "0";
                }

                if (x1_6 == true) {
                    document.getElementById('<%= Hdn1_6.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn1_6.ClientID%>').value = "0";
                }

                if (x1_7 == true) {
                    document.getElementById('<%= Hdn1_7.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn1_7.ClientID%>').value = "0";
                }

                if (x1_8 == true) {
                    document.getElementById('<%= Hdn1_8.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn1_8.ClientID%>').value = "0";
                }

                if (x1_9 == true) {
                    document.getElementById('<%= Hdn1_9.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn1_9.ClientID%>').value = "0";
                }

                if (x1_10 == true) {
                    document.getElementById('<%= Hdn1_10.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn1_10.ClientID%>').value = "0";
                }

                if (x1_11 == true) {
                    document.getElementById('<%= Hdn1_11.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn1_11.ClientID%>').value = "0";
                }

                if (x1_12 == true) {
                    document.getElementById('<%= Hdn1_12.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn1_12.ClientID%>').value = "0";
                }

                if (x1_13 == true) {
                    document.getElementById('<%= Hdn1_13.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn1_13.ClientID%>').value = "0";
                }

                if (x1_14 == true) {
                    document.getElementById('<%= Hdn1_14.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn1_14.ClientID%>').value = "0";
                }

                if (x1_15 == true) {
                    document.getElementById('<%= Hdn1_15.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn1_15.ClientID%>').value = "0";
                }

                if (x1_16 == true) {
                    document.getElementById('<%= Hdn1_16.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn1_16.ClientID%>').value = "0";
                }

                if (x1_17 == true) {
                    document.getElementById('<%= Hdn1_17.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn1_17.ClientID%>').value = "0";
                }

                if (x1_18 == true) {
                    document.getElementById('<%= Hdn1_18.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn1_18.ClientID%>').value = "0";
                }

                if (x2_1 == true) {
                    document.getElementById('<%= Hdn2_1.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn2_1.ClientID%>').value = "0";
                }

                if (x2_2 == true) {
                    document.getElementById('<%= Hdn2_2.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn2_2.ClientID%>').value = "0";
                }

                if (x2_3 == true) {
                    document.getElementById('<%= Hdn2_3.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn2_3.ClientID%>').value = "0";
                }

                if (x2_4 == true) {
                    document.getElementById('<%= Hdn2_4.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn2_4.ClientID%>').value = "0";
                }

                if (x2_5 == true) {
                    document.getElementById('<%= Hdn2_5.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn2_5.ClientID%>').value = "0";
                }

                if (x2_6 == true) {
                    document.getElementById('<%= Hdn2_6.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn2_6.ClientID%>').value = "0";
                }

                if (x3_1 == true) {
                    document.getElementById('<%= Hdn3_1.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn3_1.ClientID%>').value = "0";
                }

                if (x3_2 == true) {
                    document.getElementById('<%= Hdn3_2.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn3_2.ClientID%>').value = "0";
                }

                if (x3_3 == true) {
                    document.getElementById('<%= Hdn3_3.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn3_3.ClientID%>').value = "0";
                }

                if (x4_1 == true) {
                    document.getElementById('<%= Hdn4_1.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_1.ClientID%>').value = "0";
                }

                if (x4_2 == true) {
                    document.getElementById('<%= Hdn4_2.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_2.ClientID%>').value = "0";
                }

                if (x4_3 == true) {
                    document.getElementById('<%= Hdn4_3.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_3.ClientID%>').value = "0";
                }

                if (x4_4 == true) {
                    document.getElementById('<%= Hdn4_4.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_4.ClientID%>').value = "0";
                }

                if (x4_5 == true) {
                    document.getElementById('<%= Hdn4_5.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_5.ClientID%>').value = "0";
                }

                if (x4_6 == true) {
                    document.getElementById('<%= Hdn4_6.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_6.ClientID%>').value = "0";
                }

                if (x4_7 == true) {
                    document.getElementById('<%= Hdn4_7.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_7.ClientID%>').value = "0";
                }

                if (x4_8 == true) {
                    document.getElementById('<%= Hdn4_8.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_8.ClientID%>').value = "0";
                }

                if (x4_9 == true) {
                    document.getElementById('<%= Hdn4_9.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_9.ClientID%>').value = "0";
                }

                if (x4_10 == true) {
                    document.getElementById('<%= Hdn4_10.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_10.ClientID%>').value = "0";
                }

                if (x4_11 == true) {
                    document.getElementById('<%= Hdn4_11.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_11.ClientID%>').value = "0";
                }

                if (x4_12 == true) {
                    document.getElementById('<%= Hdn4_12.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_12.ClientID%>').value = "0";
                }

                if (x4_13 == true) {
                    document.getElementById('<%= Hdn4_13.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_13.ClientID%>').value = "0";
                }

                if (x4_14 == true) {
                    document.getElementById('<%= Hdn4_14.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_14.ClientID%>').value = "0";
                }

                if (x4_15 == true) {
                    document.getElementById('<%= Hdn4_15.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_15.ClientID%>').value = "0";
                }

                if (x4_16 == true) {
                    document.getElementById('<%= Hdn4_16.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_16.ClientID%>').value = "0";
                }

                if (x4_17 == true) {
                    document.getElementById('<%= Hdn4_17.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_17.ClientID%>').value = "0";
                }

                if (x4_18 == true) {
                    document.getElementById('<%= Hdn4_18.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_18.ClientID%>').value = "0";
                }

                if (x4_19 == true) {
                    document.getElementById('<%= Hdn4_19.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_19.ClientID%>').value = "0";
                }

                if (x4_20 == true) {
                    document.getElementById('<%= Hdn4_20.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_20.ClientID%>').value = "0";
                }

                if (x4_21 == true) {
                    document.getElementById('<%= Hdn4_21.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_21.ClientID%>').value = "0";
                }

                if (x4_22 == true) {
                    document.getElementById('<%= Hdn4_22.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn4_22.ClientID%>').value = "0";
                }

                if (x5_1 == true) {
                    document.getElementById('<%= Hdn5_1.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn5_1.ClientID%>').value = "0";
                }

                if (x5_2 == true) {
                    document.getElementById('<%= Hdn5_2.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn5_2.ClientID%>').value = "0";
                }

                if (x5_3 == true) {
                    document.getElementById('<%= Hdn5_3.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn5_3.ClientID%>').value = "0";
                }

                if (x5_4 == true) {
                    document.getElementById('<%= Hdn5_4.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn5_4.ClientID%>').value = "0";
                }

                if (x5_5 == true) {
                    document.getElementById('<%= Hdn5_5.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn5_5.ClientID%>').value = "0";
                }

                if (x6_1 == true) {
                    document.getElementById('<%= Hdn6_1.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn6_1.ClientID%>').value = "0";
                }

                if (x6_2 == true) {
                    document.getElementById('<%= Hdn6_2.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn6_2.ClientID%>').value = "0";
                }

                if (x6_3 == true) {
                    document.getElementById('<%= Hdn6_3.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn6_3.ClientID%>').value = "0";
                }

                if (x6_4 == true) {
                    document.getElementById('<%= Hdn6_4.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn6_4.ClientID%>').value = "0";
                }

                if (x6_5 == true) {
                    document.getElementById('<%= Hdn6_5.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn6_5.ClientID%>').value = "0";
                }

                if (x7_1 == true) {
                    document.getElementById('<%= Hdn7_1.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn7_1.ClientID%>').value = "0";
                }

                if (x7_2 == true) {
                    document.getElementById('<%= Hdn7_2.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn7_2.ClientID%>').value = "0";
                }

                if (x7_3 == true) {
                    document.getElementById('<%= Hdn7_3.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn7_3.ClientID%>').value = "0";
                }

                if (x7_4 == true) {
                    document.getElementById('<%= Hdn7_4.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn7_4.ClientID%>').value = "0";
                }

                if (x7_5 == true) {
                    document.getElementById('<%= Hdn7_5.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn7_5.ClientID%>').value = "0";
                }

                if (x7_6 == true) {
                    document.getElementById('<%= Hdn7_6.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn7_6.ClientID%>').value = "0";
                }

                if (x7_7 == true) {
                    document.getElementById('<%= Hdn7_7.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn7_7.ClientID%>').value = "0";
                }

                if (x8_1 == true) {
                    document.getElementById('<%= Hdn8_1.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn8_1.ClientID%>').value = "0";
                }

                if (x8_2 == true) {
                    document.getElementById('<%= Hdn8_2.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn8_2.ClientID%>').value = "0";
                }

                if (x8_3 == true) {
                    document.getElementById('<%= Hdn8_3.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn8_3.ClientID%>').value = "0";
                }

                if (x8_4 == true) {
                    document.getElementById('<%= Hdn8_4.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn8_4.ClientID%>').value = "0";
                }

                if (x8_5 == true) {
                    document.getElementById('<%= Hdn8_5.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn8_5.ClientID%>').value = "0";
                }

                if (x8_6 == true) {
                    document.getElementById('<%= Hdn8_6.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn8_6.ClientID%>').value = "0";
                }

                if (x8_7 == true) {
                    document.getElementById('<%= Hdn8_7.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn8_7.ClientID%>').value = "0";
                }

                if (x8_8 == true) {
                    document.getElementById('<%= Hdn8_8.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn8_8.ClientID%>').value = "0";
                }

                if (x8_9 == true) {
                    document.getElementById('<%= Hdn8_9.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn8_9.ClientID%>').value = "0";
                }

                if (x9_1 == true) {
                    document.getElementById('<%= Hdn9_1.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn9_1.ClientID%>').value = "0";
                }

                if (x9_2 == true) {
                    document.getElementById('<%= Hdn9_2.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn9_2.ClientID%>').value = "0";
                }

                if (x9_3 == true) {
                    document.getElementById('<%= Hdn9_3.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn9_3.ClientID%>').value = "0";
                }

                if (x9_4 == true) {
                    document.getElementById('<%= Hdn9_4.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn9_4.ClientID%>').value = "0";
                }

                if (x9_5 == true) {
                    document.getElementById('<%= Hdn9_5.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn9_5.ClientID%>').value = "0";
                }

                if (x9_6 == true) {
                    document.getElementById('<%= Hdn9_6.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn9_6.ClientID%>').value = "0";
                }

                if (x9_7 == true) {
                    document.getElementById('<%= Hdn9_7.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn9_7.ClientID%>').value = "0";
                }

                if (x9_8 == true) {
                    document.getElementById('<%= Hdn9_8.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn9_8.ClientID%>').value = "0";
                }

                if (x9_9 == true) {
                    document.getElementById('<%= Hdn9_9.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn9_9.ClientID%>').value = "0";
                }

                if (x9_10 == true) {
                    document.getElementById('<%= Hdn9_10.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn9_10.ClientID%>').value = "0";
                }

                if (x9_11 == true) {
                    document.getElementById('<%= Hdn9_11.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn9_11.ClientID%>').value = "0";
                }

                if (x9_12 == true) {
                    document.getElementById('<%= Hdn9_12.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn9_12.ClientID%>').value = "0";
                }

                if (x9_13 == true) {
                    document.getElementById('<%= Hdn9_13.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn9_13.ClientID%>').value = "0";
                }

                if (x10_1 == true) {
                    document.getElementById('<%= Hdn10_1.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn10_1.ClientID%>').value = "0";
                }

                if (x10_2 == true) {
                    document.getElementById('<%= Hdn10_2.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn10_2.ClientID%>').value = "0";
                }

                if (x10_3 == true) {
                    document.getElementById('<%= Hdn10_3.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn10_3.ClientID%>').value = "0";
                }

                if (x10_4 == true) {
                    document.getElementById('<%= Hdn10_4.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn10_4.ClientID%>').value = "0";
                }

                if (x10_5 == true) {
                    document.getElementById('<%= Hdn10_5.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn10_5.ClientID%>').value = "0";
                }

                if (x10_6 == true) {
                    document.getElementById('<%= Hdn10_6.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn10_6.ClientID%>').value = "0";
                }

                if (x10_7 == true) {
                    document.getElementById('<%= Hdn10_7.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn10_7.ClientID%>').value = "0";
                }

                if (x10_8 == true) {
                    document.getElementById('<%= Hdn10_8.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn10_8.ClientID%>').value = "0";
                }

                if (x10_9 == true) {
                    document.getElementById('<%= Hdn10_9.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn10_9.ClientID%>').value = "0";
                }

                if (x11_1 == true) {
                    document.getElementById('<%= Hdn11_1.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn11_1.ClientID%>').value = "0";
                }

                if (x11_2 == true) {
                    document.getElementById('<%= Hdn11_2.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn11_2.ClientID%>').value = "0";
                }

                if (x11_3 == true) {
                    document.getElementById('<%= Hdn11_3.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn11_3.ClientID%>').value = "0";
                }

                if (x11_4 == true) {
                    document.getElementById('<%= Hdn11_4.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn11_4.ClientID%>').value = "0";
                }

                if (x11_5 == true) {
                    document.getElementById('<%= Hdn11_5.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn11_5.ClientID%>').value = "0";
                }

                if (x11_6 == true) {
                    document.getElementById('<%= Hdn11_6.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn11_6.ClientID%>').value = "0";
                }

                if (x11_7 == true) {
                    document.getElementById('<%= Hdn11_7.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn11_7.ClientID%>').value = "0";
                }

                if (x11_8 == true) {
                    document.getElementById('<%= Hdn11_8.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn11_8.ClientID%>').value = "0";
                }

                if (x11_9 == true) {
                    document.getElementById('<%= Hdn11_9.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn11_9.ClientID%>').value = "0";
                }

                if (x11_10 == true) {
                    document.getElementById('<%= Hdn11_10.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn11_10.ClientID%>').value = "0";
                }

                if (x11_11 == true) {
                    document.getElementById('<%= Hdn11_11.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn11_11.ClientID%>').value = "0";
                }

                if (x11_12 == true) {
                    document.getElementById('<%= Hdn11_12.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn11_12.ClientID%>').value = "0";
                }

                if (x11_13 == true) {
                    document.getElementById('<%= Hdn11_13.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn11_13.ClientID%>').value = "0";
                }

                if (x11_14 == true) {
                    document.getElementById('<%= Hdn11_14.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn11_14.ClientID%>').value = "0";
                }

                if (x11_15 == true) {
                    document.getElementById('<%= Hdn11_15.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn11_15.ClientID%>').value = "0";
                }

                if (x11_16 == true) {
                    document.getElementById('<%= Hdn11_16.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn11_16.ClientID%>').value = "0";
                }

                if (x12_1 == true) {
                    document.getElementById('<%= Hdn12_1.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn12_1.ClientID%>').value = "0";
                }

                if (x12_2 == true) {
                    document.getElementById('<%= Hdn12_2.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn12_2.ClientID%>').value = "0";
                }

                if (x12_3 == true) {
                    document.getElementById('<%= Hdn12_3.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn12_3.ClientID%>').value = "0";
                }

                if (x12_4 == true) {
                    document.getElementById('<%= Hdn12_4.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn12_4.ClientID%>').value = "0";
                }

                if (x13_1 == true) {
                    document.getElementById('<%= Hdn13_1.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn13_1.ClientID%>').value = "0";
                }

                if (x13_2 == true) {
                    document.getElementById('<%= Hdn13_2.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn13_2.ClientID%>').value = "0";
                }

                if (x13_3 == true) {
                    document.getElementById('<%= Hdn13_3.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn13_3.ClientID%>').value = "0";
                }

                if (x14_1 == true) {
                    document.getElementById('<%= Hdn14_1.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn14_1.ClientID%>').value = "0";
                }

                if (x14_2 == true) {
                    document.getElementById('<%= Hdn14_2.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn14_2.ClientID%>').value = "0";
                }

                if (x14_3 == true) {
                    document.getElementById('<%= Hdn14_3.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn14_3.ClientID%>').value = "0";
                }

                if (x14_4 == true) {
                    document.getElementById('<%= Hdn14_4.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn14_4.ClientID%>').value = "0";
                }

                if (x14_5 == true) {
                    document.getElementById('<%= Hdn14_5.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn14_5.ClientID%>').value = "0";
                }

                if (x14_6 == true) {
                    document.getElementById('<%= Hdn14_6.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn14_6.ClientID%>').value = "0";
                }

                if (x14_7 == true) {
                    document.getElementById('<%= Hdn14_7.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn14_7.ClientID%>').value = "0";
                }

                if (x14_8 == true) {
                    document.getElementById('<%= Hdn14_8.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn14_8.ClientID%>').value = "0";
                }

                if (x14_9 == true) {
                    document.getElementById('<%= Hdn14_9.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn14_9.ClientID%>').value = "0";
                }

                if (x14_10 == true) {
                    document.getElementById('<%= Hdn14_10.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn14_10.ClientID%>').value = "0";
                }

                if (x14_11 == true) {
                    document.getElementById('<%= Hdn14_11.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn14_11.ClientID%>').value = "0";
                }

                if (x14_12 == true) {
                    document.getElementById('<%= Hdn14_12.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn14_12.ClientID%>').value = "0";
                }

                if (x15_1 == true) {
                    document.getElementById('<%= Hdn15_1.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn15_1.ClientID%>').value = "0";
                }

                if (x15_2 == true) {
                    document.getElementById('<%= Hdn15_2.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn15_2.ClientID%>').value = "0";
                }

                if (x15_3 == true) {
                    document.getElementById('<%= Hdn15_3.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn15_3.ClientID%>').value = "0";
                }

                if (x15_4 == true) {
                    document.getElementById('<%= Hdn15_4.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn15_4.ClientID%>').value = "0";
                }

                if (x15_5 == true) {
                    document.getElementById('<%= Hdn15_5.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn15_5.ClientID%>').value = "0";
                }

                if (x15_6 == true) {
                    document.getElementById('<%= Hdn15_6.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn15_6.ClientID%>').value = "0";
                }

                if (x15_7 == true) {
                    document.getElementById('<%= Hdn15_7.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn15_7.ClientID%>').value = "0";
                }

                if (x15_8 == true) {
                    document.getElementById('<%= Hdn15_8.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn15_8.ClientID%>').value = "0";
                }

                if (x16_1 == true) {
                    document.getElementById('<%= Hdn16_1.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn16_1.ClientID%>').value = "0";
                }

                if (x16_2 == true) {
                    document.getElementById('<%= Hdn16_2.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn16_2.ClientID%>').value = "0";
                }

                if (x16_3 == true) {
                    document.getElementById('<%= Hdn16_3.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn16_3.ClientID%>').value = "0";
                }

                if (x16_4 == true) {
                    document.getElementById('<%= Hdn16_4.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn16_4.ClientID%>').value = "0";
                }

                if (x16_5 == true) {
                    document.getElementById('<%= Hdn16_5.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn16_5.ClientID%>').value = "0";
                }

                if (x16_6 == true) {
                    document.getElementById('<%= Hdn16_6.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn16_6.ClientID%>').value = "0";
                }

                if (x16_7 == true) {
                    document.getElementById('<%= Hdn16_7.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn16_7.ClientID%>').value = "0";
                }

                if (x16_8 == true) {
                    document.getElementById('<%= Hdn16_8.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn16_8.ClientID%>').value = "0";
                }

                if (x16_9 == true) {
                    document.getElementById('<%= Hdn16_9.ClientID%>').value = "1";
                }
                else {
                    document.getElementById('<%= Hdn16_9.ClientID%>').value = "0";
                }
            }
        </script>
        <script id="SetVerticals" type="text/javascript">
            function SetVerticals() {
                var x1 = document.getElementById('<%= Hdn1_1.ClientID%>').value;
                var x2 = document.getElementById('<%= Hdn1_2.ClientID%>').value;
                var x3 = document.getElementById('<%= Hdn1_3.ClientID%>').value;
                var x4 = document.getElementById('<%= Hdn1_4.ClientID%>').value;
                var x5 = document.getElementById('<%= Hdn1_5.ClientID%>').value;
                var x6 = document.getElementById('<%= Hdn1_6.ClientID%>').value;
                var x7 = document.getElementById('<%= Hdn1_7.ClientID%>').value;
                var x8 = document.getElementById('<%= Hdn1_8.ClientID%>').value;
                var x9 = document.getElementById('<%= Hdn1_9.ClientID%>').value;
                var x10 = document.getElementById('<%= Hdn1_10.ClientID%>').value;
                var x11 = document.getElementById('<%= Hdn1_11.ClientID%>').value;
                var x12 = document.getElementById('<%= Hdn1_12.ClientID%>').value;
                var x13 = document.getElementById('<%= Hdn1_13.ClientID%>').value;
                var x14 = document.getElementById('<%= Hdn1_14.ClientID%>').value;
                var x15 = document.getElementById('<%= Hdn1_15.ClientID%>').value;
                var x16 = document.getElementById('<%= Hdn1_16.ClientID%>').value;
                var x17 = document.getElementById('<%= Hdn1_17.ClientID%>').value;
                var x18 = document.getElementById('<%= Hdn1_18.ClientID%>').value;

                var x19 = document.getElementById('<%= Hdn2_1.ClientID%>').value;
                var x20 = document.getElementById('<%= Hdn2_2.ClientID%>').value;
                var x21 = document.getElementById('<%= Hdn2_3.ClientID%>').value;
                var x22 = document.getElementById('<%= Hdn2_4.ClientID%>').value;
                var x23 = document.getElementById('<%= Hdn2_5.ClientID%>').value;
                var x24 = document.getElementById('<%= Hdn2_6.ClientID%>').value;

                var x25 = document.getElementById('<%= Hdn3_1.ClientID%>').value;
                var x26 = document.getElementById('<%= Hdn3_2.ClientID%>').value;
                var x27 = document.getElementById('<%= Hdn3_3.ClientID%>').value;

                var x28 = document.getElementById('<%= Hdn4_1.ClientID%>').value;
                var x29 = document.getElementById('<%= Hdn4_2.ClientID%>').value;
                var x30 = document.getElementById('<%= Hdn4_3.ClientID%>').value;
                var x31 = document.getElementById('<%= Hdn4_4.ClientID%>').value;
                var x32 = document.getElementById('<%= Hdn4_5.ClientID%>').value;
                var x33 = document.getElementById('<%= Hdn4_6.ClientID%>').value;
                var x34 = document.getElementById('<%= Hdn4_7.ClientID%>').value;
                var x35 = document.getElementById('<%= Hdn4_8.ClientID%>').value;
                var x36 = document.getElementById('<%= Hdn4_9.ClientID%>').value;
                var x37 = document.getElementById('<%= Hdn4_10.ClientID%>').value;
                var x38 = document.getElementById('<%= Hdn4_11.ClientID%>').value;
                var x39 = document.getElementById('<%= Hdn4_12.ClientID%>').value;
                var x40 = document.getElementById('<%= Hdn4_13.ClientID%>').value;
                var x41 = document.getElementById('<%= Hdn4_14.ClientID%>').value;
                var x42 = document.getElementById('<%= Hdn4_15.ClientID%>').value;
                var x43 = document.getElementById('<%= Hdn4_16.ClientID%>').value;
                var x44 = document.getElementById('<%= Hdn4_17.ClientID%>').value;
                var x45 = document.getElementById('<%= Hdn4_18.ClientID%>').value;
                var x128 = document.getElementById('<%= Hdn4_19.ClientID%>').value;
                var x129 = document.getElementById('<%= Hdn4_20.ClientID%>').value;
                var x141 = document.getElementById('<%= Hdn4_21.ClientID%>').value;
                var x147 = document.getElementById('<%= Hdn4_22.ClientID%>').value;

                var x46 = document.getElementById('<%= Hdn5_1.ClientID%>').value;
                var x47 = document.getElementById('<%= Hdn5_2.ClientID%>').value;
                var x48 = document.getElementById('<%= Hdn5_3.ClientID%>').value;
                var x49 = document.getElementById('<%= Hdn5_4.ClientID%>').value;
                var x50 = document.getElementById('<%= Hdn5_5.ClientID%>').value;

                var x51 = document.getElementById('<%= Hdn6_1.ClientID%>').value;
                var x52 = document.getElementById('<%= Hdn6_2.ClientID%>').value;
                var x53 = document.getElementById('<%= Hdn6_3.ClientID%>').value;
                var x54 = document.getElementById('<%= Hdn6_4.ClientID%>').value;
                var x55 = document.getElementById('<%= Hdn6_5.ClientID%>').value;

                var x56 = document.getElementById('<%= Hdn7_1.ClientID%>').value;
                var x57 = document.getElementById('<%= Hdn7_2.ClientID%>').value;
                var x58 = document.getElementById('<%= Hdn7_3.ClientID%>').value;
                var x59 = document.getElementById('<%= Hdn7_4.ClientID%>').value;
                var x60 = document.getElementById('<%= Hdn7_5.ClientID%>').value;
                var x61 = document.getElementById('<%= Hdn7_6.ClientID%>').value;
                var x62 = document.getElementById('<%= Hdn7_7.ClientID%>').value;

                var x63 = document.getElementById('<%= Hdn8_1.ClientID%>').value;
                var x64 = document.getElementById('<%= Hdn8_2.ClientID%>').value;
                var x65 = document.getElementById('<%= Hdn8_3.ClientID%>').value;
                var x66 = document.getElementById('<%= Hdn8_4.ClientID%>').value;
                var x67 = document.getElementById('<%= Hdn8_5.ClientID%>').value;
                var x68 = document.getElementById('<%= Hdn8_6.ClientID%>').value;
                var x69 = document.getElementById('<%= Hdn8_7.ClientID%>').value;
                var x70 = document.getElementById('<%= Hdn8_8.ClientID%>').value;
                var x71 = document.getElementById('<%= Hdn8_9.ClientID%>').value;

                var x72 = document.getElementById('<%= Hdn9_1.ClientID%>').value;
                var x73 = document.getElementById('<%= Hdn9_2.ClientID%>').value;
                var x74 = document.getElementById('<%= Hdn9_3.ClientID%>').value;
                var x75 = document.getElementById('<%= Hdn9_4.ClientID%>').value;
                var x76 = document.getElementById('<%= Hdn9_5.ClientID%>').value;
                var x77 = document.getElementById('<%= Hdn9_6.ClientID%>').value;
                var x78 = document.getElementById('<%= Hdn9_7.ClientID%>').value;
                var x79 = document.getElementById('<%= Hdn9_8.ClientID%>').value;
                var x80 = document.getElementById('<%= Hdn9_9.ClientID%>').value;
                var x81 = document.getElementById('<%= Hdn9_10.ClientID%>').value;
                var x82 = document.getElementById('<%= Hdn9_11.ClientID%>').value;
                var x83 = document.getElementById('<%= Hdn9_12.ClientID%>').value;
                var x143 = document.getElementById('<%= Hdn9_13.ClientID%>').value;

                var x84 = document.getElementById('<%= Hdn10_1.ClientID%>').value;
                var x85 = document.getElementById('<%= Hdn10_2.ClientID%>').value;
                var x86 = document.getElementById('<%= Hdn10_3.ClientID%>').value;
                var x87 = document.getElementById('<%= Hdn10_4.ClientID%>').value;
                var x88 = document.getElementById('<%= Hdn10_5.ClientID%>').value;
                var x89 = document.getElementById('<%= Hdn10_6.ClientID%>').value;
                var x90 = document.getElementById('<%= Hdn10_7.ClientID%>').value;
                var x91 = document.getElementById('<%= Hdn10_8.ClientID%>').value;
                var x92 = document.getElementById('<%= Hdn10_9.ClientID%>').value;

                var x93 = document.getElementById('<%= Hdn11_1.ClientID%>').value;
                var x94 = document.getElementById('<%= Hdn11_2.ClientID%>').value;
                var x95 = document.getElementById('<%= Hdn11_3.ClientID%>').value;
                var x96 = document.getElementById('<%= Hdn11_4.ClientID%>').value;
                var x97 = document.getElementById('<%= Hdn11_5.ClientID%>').value;
                var x98 = document.getElementById('<%= Hdn11_6.ClientID%>').value;
                var x99 = document.getElementById('<%= Hdn11_7.ClientID%>').value;
                var x100 = document.getElementById('<%= Hdn11_8.ClientID%>').value;
                var x101 = document.getElementById('<%= Hdn11_9.ClientID%>').value;
                var x102 = document.getElementById('<%= Hdn11_10.ClientID%>').value;
                var x103 = document.getElementById('<%= Hdn11_11.ClientID%>').value;
                var x104 = document.getElementById('<%= Hdn11_12.ClientID%>').value;
                var x105 = document.getElementById('<%= Hdn11_13.ClientID%>').value;
                var x141 = document.getElementById('<%= Hdn11_14.ClientID%>').value;
                var x142 = document.getElementById('<%= Hdn11_15.ClientID%>').value;
                var x144 = document.getElementById('<%= Hdn11_16.ClientID%>').value;

                var x106 = document.getElementById('<%= Hdn12_1.ClientID%>').value;
                var x107 = document.getElementById('<%= Hdn12_2.ClientID%>').value;
                var x108 = document.getElementById('<%= Hdn12_3.ClientID%>').value;
                var x130 = document.getElementById('<%= Hdn12_4.ClientID%>').value;

                var x109 = document.getElementById('<%= Hdn13_1.ClientID%>').value;
                var x110 = document.getElementById('<%= Hdn13_2.ClientID%>').value;
                var x111 = document.getElementById('<%= Hdn13_3.ClientID%>').value;

                var x112 = document.getElementById('<%= Hdn14_1.ClientID%>').value;
                var x113 = document.getElementById('<%= Hdn14_2.ClientID%>').value;
                var x114 = document.getElementById('<%= Hdn14_3.ClientID%>').value;
                var x115 = document.getElementById('<%= Hdn14_4.ClientID%>').value;
                var x116 = document.getElementById('<%= Hdn14_5.ClientID%>').value;
                var x117 = document.getElementById('<%= Hdn14_6.ClientID%>').value;
                var x118 = document.getElementById('<%= Hdn14_7.ClientID%>').value;
                var x119 = document.getElementById('<%= Hdn14_8.ClientID%>').value;
                var x120 = document.getElementById('<%= Hdn14_9.ClientID%>').value;
                var x131 = document.getElementById('<%= Hdn14_10.ClientID%>').value;
                var x145 = document.getElementById('<%= Hdn14_11.ClientID%>').value;
                var x146 = document.getElementById('<%= Hdn14_12.ClientID%>').value;

                var x121 = document.getElementById('<%= Hdn15_1.ClientID%>').value;
                var x122 = document.getElementById('<%= Hdn15_2.ClientID%>').value;
                var x123 = document.getElementById('<%= Hdn15_3.ClientID%>').value;
                var x124 = document.getElementById('<%= Hdn15_4.ClientID%>').value;
                var x125 = document.getElementById('<%= Hdn15_5.ClientID%>').value;
                var x126 = document.getElementById('<%= Hdn15_6.ClientID%>').value;
                var x127 = document.getElementById('<%= Hdn15_7.ClientID%>').value;
                var x141 = document.getElementById('<%= Hdn15_8.ClientID%>').value;

                var x132 = document.getElementById('<%= Hdn16_1.ClientID%>').value;
                var x133 = document.getElementById('<%= Hdn16_2.ClientID%>').value;
                var x134 = document.getElementById('<%= Hdn16_3.ClientID%>').value;
                var x135 = document.getElementById('<%= Hdn16_4.ClientID%>').value;
                var x136 = document.getElementById('<%= Hdn16_5.ClientID%>').value;
                var x137 = document.getElementById('<%= Hdn16_6.ClientID%>').value;
                var x138 = document.getElementById('<%= Hdn16_7.ClientID%>').value;
                var x139 = document.getElementById('<%= Hdn16_8.ClientID%>').value;
                var x140 = document.getElementById('<%= Hdn16_9.ClientID%>').value;

                if (x1 == "1") {
                    document.getElementById("Cbx1_1").checked = true;
                }
                else {
                    document.getElementById("Cbx1_1").checked = false;
                }

                if (x2 == "1") {
                    document.getElementById("Cbx1_2").checked = true;
                }
                else {
                    document.getElementById("Cbx1_2").checked = false;
                }

                if (x3 == "1") {
                    document.getElementById("Cbx1_3").checked = true;
                }
                else {
                    document.getElementById("Cbx1_3").checked = false;
                }

                if (x4 == "1") {
                    document.getElementById("Cbx1_4").checked = true;
                }
                else {
                    document.getElementById("Cbx1_4").checked = false;
                }

                if (x5 == "1") {
                    document.getElementById("Cbx1_5").checked = true;
                }
                else {
                    document.getElementById("Cbx1_5").checked = false;
                }

                if (x6 == "1") {
                    document.getElementById("Cbx1_6").checked = true;
                }
                else {
                    document.getElementById("Cbx1_6").checked = false;
                }

                if (x7 == "1") {
                    document.getElementById("Cbx1_7").checked = true;
                }
                else {
                    document.getElementById("Cbx1_7").checked = false;
                }

                if (x8 == "1") {
                    document.getElementById("Cbx1_8").checked = true;
                }
                else {
                    document.getElementById("Cbx1_8").checked = false;
                }

                if (x9 == "1") {
                    document.getElementById("Cbx1_9").checked = true;
                }
                else {
                    document.getElementById("Cbx1_9").checked = false;
                }

                if (x10 == "1") {
                    document.getElementById("Cbx1_10").checked = true;
                }
                else {
                    document.getElementById("Cbx1_10").checked = false;
                }

                if (x11 == "1") {
                    document.getElementById("Cbx1_11").checked = true;
                }
                else {
                    document.getElementById("Cbx1_11").checked = false;
                }

                if (x12 == "1") {
                    document.getElementById("Cbx1_12").checked = true;
                }
                else {
                    document.getElementById("Cbx1_12").checked = false;
                }

                if (x13 == "1") {
                    document.getElementById("Cbx1_13").checked = true;
                }
                else {
                    document.getElementById("Cbx1_13").checked = false;
                }

                if (x14 == "1") {
                    document.getElementById("Cbx1_14").checked = true;
                }
                else {
                    document.getElementById("Cbx1_14").checked = false;
                }

                if (x15 == "1") {
                    document.getElementById("Cbx1_15").checked = true;
                }
                else {
                    document.getElementById("Cbx1_15").checked = false;
                }

                if (x16 == "1") {
                    document.getElementById("Cbx1_16").checked = true;
                }
                else {
                    document.getElementById("Cbx1_16").checked = false;
                }

                if (x17 == "1") {
                    document.getElementById("Cbx1_17").checked = true;
                }
                else {
                    document.getElementById("Cbx1_17").checked = false;
                }

                if (x18 == "1") {
                    document.getElementById("Cbx1_18").checked = true;
                }
                else {
                    document.getElementById("Cbx1_18").checked = false;
                }


                if (x19 == "1") {
                    document.getElementById("Cbx2_1").checked = true;
                }
                else {
                    document.getElementById("Cbx2_1").checked = false;
                }

                if (x20 == "1") {
                    document.getElementById("Cbx2_2").checked = true;
                }
                else {
                    document.getElementById("Cbx2_2").checked = false;
                }

                if (x21 == "1") {
                    document.getElementById("Cbx2_3").checked = true;
                }
                else {
                    document.getElementById("Cbx2_3").checked = false;
                }

                if (x22 == "1") {
                    document.getElementById("Cbx2_4").checked = true;
                }
                else {
                    document.getElementById("Cbx2_4").checked = false;
                }

                if (x23 == "1") {
                    document.getElementById("Cbx2_5").checked = true;
                }
                else {
                    document.getElementById("Cbx2_5").checked = false;
                }

                if (x24 == "1") {
                    document.getElementById("Cbx2_6").checked = true;
                }
                else {
                    document.getElementById("Cbx2_6").checked = false;
                }


                if (x25 == "1") {
                    document.getElementById("Cbx3_1").checked = true;
                }
                else {
                    document.getElementById("Cbx3_1").checked = false;
                }

                if (x26 == "1") {
                    document.getElementById("Cbx3_2").checked = true;
                }
                else {
                    document.getElementById("Cbx3_2").checked = false;
                }

                if (x27 == "1") {
                    document.getElementById("Cbx3_3").checked = true;
                }
                else {
                    document.getElementById("Cbx3_3").checked = false;
                }

                if (x28 == "1") {
                    document.getElementById("Cbx4_1").checked = true;
                }
                else {
                    document.getElementById("Cbx4_1").checked = false;
                }

                if (x29 == "1") {
                    document.getElementById("Cbx4_2").checked = true;
                }
                else {
                    document.getElementById("Cbx4_2").checked = false;
                }

                if (x30 == "1") {
                    document.getElementById("Cbx4_3").checked = true;
                }
                else {
                    document.getElementById("Cbx4_3").checked = false;
                }

                if (x31 == "1") {
                    document.getElementById("Cbx4_4").checked = true;
                }
                else {
                    document.getElementById("Cbx4_4").checked = false;
                }

                if (x32 == "1") {
                    document.getElementById("Cbx4_5").checked = true;
                }
                else {
                    document.getElementById("Cbx4_5").checked = false;
                }

                if (x33 == "1") {
                    document.getElementById("Cbx4_6").checked = true;
                }
                else {
                    document.getElementById("Cbx4_6").checked = false;
                }

                if (x34 == "1") {
                    document.getElementById("Cbx4_7").checked = true;
                }
                else {
                    document.getElementById("Cbx4_7").checked = false;
                }

                if (x35 == "1") {
                    document.getElementById("Cbx4_8").checked = true;
                }
                else {
                    document.getElementById("Cbx4_8").checked = false;
                }

                if (x36 == "1") {
                    document.getElementById("Cbx4_9").checked = true;
                }
                else {
                    document.getElementById("Cbx4_9").checked = false;
                }

                if (x37 == "1") {
                    document.getElementById("Cbx4_10").checked = true;
                }
                else {
                    document.getElementById("Cbx4_10").checked = false;
                }

                if (x38 == "1") {
                    document.getElementById("Cbx4_11").checked = true;
                }
                else {
                    document.getElementById("Cbx4_11").checked = false;
                }

                if (x39 == "1") {
                    document.getElementById("Cbx4_12").checked = true;
                }
                else {
                    document.getElementById("Cbx4_12").checked = false;
                }

                if (x40 == "1") {
                    document.getElementById("Cbx4_13").checked = true;
                }
                else {
                    document.getElementById("Cbx4_13").checked = false;
                }

                if (x41 == "1") {
                    document.getElementById("Cbx4_14").checked = true;
                }
                else {
                    document.getElementById("Cbx4_14").checked = false;
                }

                if (x42 == "1") {
                    document.getElementById("Cbx4_15").checked = true;
                }
                else {
                    document.getElementById("Cbx4_15").checked = false;
                }

                if (x43 == "1") {
                    document.getElementById("Cbx4_16").checked = true;
                }
                else {
                    document.getElementById("Cbx4_16").checked = false;
                }

                if (x44 == "1") {
                    document.getElementById("Cbx4_17").checked = true;
                }
                else {
                    document.getElementById("Cbx4_17").checked = false;
                }

                if (x45 == "1") {
                    document.getElementById("Cbx4_18").checked = true;
                }
                else {
                    document.getElementById("Cbx4_18").checked = false;
                }

                if (x128 == "1") {
                    document.getElementById("Cbx4_19").checked = true;
                }
                else {
                    document.getElementById("Cbx4_19").checked = false;
                }

                if (x129 == "1") {
                    document.getElementById("Cbx4_20").checked = true;
                }
                else {
                    document.getElementById("Cbx4_20").checked = false;
                }

                if (x141 == "1") {
                    document.getElementById("Cbx4_21").checked = true;
                }
                else {
                    document.getElementById("Cbx4_21").checked = false;
                }

                if (x147 == "1") {
                    document.getElementById("Cbx4_22").checked = true;
                }
                else {
                    document.getElementById("Cbx4_22").checked = false;
                }

                if (x46 == "1") {
                    document.getElementById("Cbx5_1").checked = true;
                }
                else {
                    document.getElementById("Cbx5_1").checked = false;
                }

                if (x47 == "1") {
                    document.getElementById("Cbx5_2").checked = true;
                }
                else {
                    document.getElementById("Cbx5_2").checked = false;
                }

                if (x48 == "1") {
                    document.getElementById("Cbx5_3").checked = true;
                }
                else {
                    document.getElementById("Cbx5_3").checked = false;
                }

                if (x49 == "1") {
                    document.getElementById("Cbx5_4").checked = true;
                }
                else {
                    document.getElementById("Cbx5_4").checked = false;
                }

                if (x50 == "1") {
                    document.getElementById("Cbx5_5").checked = true;
                }
                else {
                    document.getElementById("Cbx5_5").checked = false;
                }

                if (x51 == "1") {
                    document.getElementById("Cbx6_1").checked = true;
                }
                else {
                    document.getElementById("Cbx6_1").checked = false;
                }

                if (x52 == "1") {
                    document.getElementById("Cbx6_2").checked = true;
                }
                else {
                    document.getElementById("Cbx6_2").checked = false;
                }

                if (x53 == "1") {
                    document.getElementById("Cbx6_3").checked = true;
                }
                else {
                    document.getElementById("Cbx6_3").checked = false;
                }

                if (x54 == "1") {
                    document.getElementById("Cbx6_4").checked = true;
                }
                else {
                    document.getElementById("Cbx6_4").checked = false;
                }

                if (x55 == "1") {
                    document.getElementById("Cbx6_5").checked = true;
                }
                else {
                    document.getElementById("Cbx6_5").checked = false;
                }

                if (x56 == "1") {
                    document.getElementById("Cbx7_1").checked = true;
                }
                else {
                    document.getElementById("Cbx7_1").checked = false;
                }

                if (x57 == "1") {
                    document.getElementById("Cbx7_2").checked = true;
                }
                else {
                    document.getElementById("Cbx7_2").checked = false;
                }

                if (x58 == "1") {
                    document.getElementById("Cbx7_3").checked = true;
                }
                else {
                    document.getElementById("Cbx7_3").checked = false;
                }

                if (x59 == "1") {
                    document.getElementById("Cbx7_4").checked = true;
                }
                else {
                    document.getElementById("Cbx7_4").checked = false;
                }

                if (x60 == "1") {
                    document.getElementById("Cbx7_5").checked = true;
                }
                else {
                    document.getElementById("Cbx7_5").checked = false;
                }

                if (x61 == "1") {
                    document.getElementById("Cbx7_6").checked = true;
                }
                else {
                    document.getElementById("Cbx7_6").checked = false;
                }

                if (x62 == "1") {
                    document.getElementById("Cbx7_7").checked = true;
                }
                else {
                    document.getElementById("Cbx7_7").checked = false;
                }

                if (x63 == "1") {
                    document.getElementById("Cbx8_1").checked = true;
                }
                else {
                    document.getElementById("Cbx8_1").checked = false;
                }

                if (x64 == "1") {
                    document.getElementById("Cbx8_2").checked = true;
                }
                else {
                    document.getElementById("Cbx8_2").checked = false;
                }

                if (x65 == "1") {
                    document.getElementById("Cbx8_3").checked = true;
                }
                else {
                    document.getElementById("Cbx8_3").checked = false;
                }

                if (x66 == "1") {
                    document.getElementById("Cbx8_4").checked = true;
                }
                else {
                    document.getElementById("Cbx8_4").checked = false;
                }

                if (x67 == "1") {
                    document.getElementById("Cbx8_5").checked = true;
                }
                else {
                    document.getElementById("Cbx8_5").checked = false;
                }

                if (x68 == "1") {
                    document.getElementById("Cbx8_6").checked = true;
                }
                else {
                    document.getElementById("Cbx8_6").checked = false;
                }

                if (x69 == "1") {
                    document.getElementById("Cbx8_7").checked = true;
                }
                else {
                    document.getElementById("Cbx8_7").checked = false;
                }

                if (x70 == "1") {
                    document.getElementById("Cbx8_8").checked = true;
                }
                else {
                    document.getElementById("Cbx8_8").checked = false;
                }

                if (x71 == "1") {
                    document.getElementById("Cbx8_9").checked = true;
                }
                else {
                    document.getElementById("Cbx8_9").checked = false;
                }

                if (x72 == "1") {
                    document.getElementById("Cbx9_1").checked = true;
                }
                else {
                    document.getElementById("Cbx9_1").checked = false;
                }

                if (x73 == "1") {
                    document.getElementById("Cbx9_2").checked = true;
                }
                else {
                    document.getElementById("Cbx9_2").checked = false;
                }

                if (x74 == "1") {
                    document.getElementById("Cbx9_3").checked = true;
                }
                else {
                    document.getElementById("Cbx9_3").checked = false;
                }

                if (x75 == "1") {
                    document.getElementById("Cbx9_4").checked = true;
                }
                else {
                    document.getElementById("Cbx9_4").checked = false;
                }

                if (x76 == "1") {
                    document.getElementById("Cbx9_5").checked = true;
                }
                else {
                    document.getElementById("Cbx9_5").checked = false;
                }

                if (x77 == "1") {
                    document.getElementById("Cbx9_6").checked = true;
                }
                else {
                    document.getElementById("Cbx9_6").checked = false;
                }

                if (x78 == "1") {
                    document.getElementById("Cbx9_7").checked = true;
                }
                else {
                    document.getElementById("Cbx9_7").checked = false;
                }

                if (x79 == "1") {
                    document.getElementById("Cbx9_8").checked = true;
                }
                else {
                    document.getElementById("Cbx9_8").checked = false;
                }

                if (x80 == "1") {
                    document.getElementById("Cbx9_9").checked = true;
                }
                else {
                    document.getElementById("Cbx9_9").checked = false;
                }

                if (x81 == "1") {
                    document.getElementById("Cbx9_10").checked = true;
                }
                else {
                    document.getElementById("Cbx9_10").checked = false;
                }

                if (x82 == "1") {
                    document.getElementById("Cbx9_11").checked = true;
                }
                else {
                    document.getElementById("Cbx9_11").checked = false;
                }

                if (x83 == "1") {
                    document.getElementById("Cbx9_12").checked = true;
                }
                else {
                    document.getElementById("Cbx9_12").checked = false;
                }

                if (x143 == "1") {
                    document.getElementById("Cbx9_13").checked = true;
                }
                else {
                    document.getElementById("Cbx9_13").checked = false;
                }

                if (x84 == "1") {
                    document.getElementById("Cbx10_1").checked = true;
                }
                else {
                    document.getElementById("Cbx10_1").checked = false;
                }

                if (x85 == "1") {
                    document.getElementById("Cbx10_2").checked = true;
                }
                else {
                    document.getElementById("Cbx10_2").checked = false;
                }

                if (x86 == "1") {
                    document.getElementById("Cbx10_3").checked = true;
                }
                else {
                    document.getElementById("Cbx10_3").checked = false;
                }

                if (x87 == "1") {
                    document.getElementById("Cbx10_4").checked = true;
                }
                else {
                    document.getElementById("Cbx10_4").checked = false;
                }

                if (x88 == "1") {
                    document.getElementById("Cbx10_5").checked = true;
                }
                else {
                    document.getElementById("Cbx10_5").checked = false;
                }

                if (x89 == "1") {
                    document.getElementById("Cbx10_6").checked = true;
                }
                else {
                    document.getElementById("Cbx10_6").checked = false;
                }

                if (x90 == "1") {
                    document.getElementById("Cbx10_7").checked = true;
                }
                else {
                    document.getElementById("Cbx10_7").checked = false;
                }

                if (x91 == "1") {
                    document.getElementById("Cbx10_8").checked = true;
                }
                else {
                    document.getElementById("Cbx10_8").checked = false;
                }

                if (x92 == "1") {
                    document.getElementById("Cbx10_9").checked = true;
                }
                else {
                    document.getElementById("Cbx10_9").checked = false;
                }

                if (x93 == "1") {
                    document.getElementById("Cbx11_1").checked = true;
                }
                else {
                    document.getElementById("Cbx11_1").checked = false;
                }

                if (x94 == "1") {
                    document.getElementById("Cbx11_2").checked = true;
                }
                else {
                    document.getElementById("Cbx11_2").checked = false;
                }

                if (x95 == "1") {
                    document.getElementById("Cbx11_3").checked = true;
                }
                else {
                    document.getElementById("Cbx11_3").checked = false;
                }

                if (x96 == "1") {
                    document.getElementById("Cbx11_4").checked = true;
                }
                else {
                    document.getElementById("Cbx11_4").checked = false;
                }

                if (x97 == "1") {
                    document.getElementById("Cbx11_5").checked = true;
                }
                else {
                    document.getElementById("Cbx11_5").checked = false;
                }

                if (x98 == "1") {
                    document.getElementById("Cbx11_6").checked = true;
                }
                else {
                    document.getElementById("Cbx11_6").checked = false;
                }

                if (x99 == "1") {
                    document.getElementById("Cbx11_7").checked = true;
                }
                else {
                    document.getElementById("Cbx11_7").checked = false;
                }

                if (x100 == "1") {
                    document.getElementById("Cbx11_8").checked = true;
                }
                else {
                    document.getElementById("Cbx11_8").checked = false;
                }

                if (x101 == "1") {
                    document.getElementById("Cbx11_9").checked = true;
                }
                else {
                    document.getElementById("Cbx11_9").checked = false;
                }

                if (x102 == "1") {
                    document.getElementById("Cbx11_10").checked = true;
                }
                else {
                    document.getElementById("Cbx11_10").checked = false;
                }

                if (x103 == "1") {
                    document.getElementById("Cbx11_11").checked = true;
                }
                else {
                    document.getElementById("Cbx11_11").checked = false;
                }

                if (x104 == "1") {
                    document.getElementById("Cbx11_12").checked = true;
                }
                else {
                    document.getElementById("Cbx11_12").checked = false;
                }

                if (x105 == "1") {
                    document.getElementById("Cbx11_13").checked = true;
                }
                else {
                    document.getElementById("Cbx11_13").checked = false;
                }

                if (x141 == "1") {
                    document.getElementById("Cbx11_14").checked = true;
                }
                else {
                    document.getElementById("Cbx11_14").checked = false;
                }

                if (x142 == "1") {
                    document.getElementById("Cbx11_15").checked = true;
                }
                else {
                    document.getElementById("Cbx11_15").checked = false;
                }

                if (x144 == "1") {
                    document.getElementById("Cbx11_16").checked = true;
                }
                else {
                    document.getElementById("Cbx11_16").checked = false;
                }

                if (x106 == "1") {
                    document.getElementById("Cbx12_1").checked = true;
                }
                else {
                    document.getElementById("Cbx12_1").checked = false;
                }

                if (x107 == "1") {
                    document.getElementById("Cbx12_2").checked = true;
                }
                else {
                    document.getElementById("Cbx12_2").checked = false;
                }

                if (x108 == "1") {
                    document.getElementById("Cbx12_3").checked = true;
                }
                else {
                    document.getElementById("Cbx12_3").checked = false;
                }

                if (x130 == "1") {
                    document.getElementById("Cbx12_4").checked = true;
                }
                else {
                    document.getElementById("Cbx12_4").checked = false;
                }

                if (x109 == "1") {
                    document.getElementById("Cbx13_1").checked = true;
                }
                else {
                    document.getElementById("Cbx13_1").checked = false;
                }

                if (x110 == "1") {
                    document.getElementById("Cbx13_2").checked = true;
                }
                else {
                    document.getElementById("Cbx13_2").checked = false;
                }

                if (x111 == "1") {
                    document.getElementById("Cbx13_3").checked = true;
                }
                else {
                    document.getElementById("Cbx13_3").checked = false;
                }

                if (x112 == "1") {
                    document.getElementById("Cbx14_1").checked = true;
                }
                else {
                    document.getElementById("Cbx14_1").checked = false;
                }

                if (x113 == "1") {
                    document.getElementById("Cbx14_2").checked = true;
                }
                else {
                    document.getElementById("Cbx14_2").checked = false;
                }

                if (x114 == "1") {
                    document.getElementById("Cbx14_3").checked = true;
                }
                else {
                    document.getElementById("Cbx14_3").checked = false;
                }

                if (x115 == "1") {
                    document.getElementById("Cbx14_4").checked = true;
                }
                else {
                    document.getElementById("Cbx14_4").checked = false;
                }

                if (x116 == "1") {
                    document.getElementById("Cbx14_5").checked = true;
                }
                else {
                    document.getElementById("Cbx14_5").checked = false;
                }

                if (x117 == "1") {
                    document.getElementById("Cbx14_6").checked = true;
                }
                else {
                    document.getElementById("Cbx14_6").checked = false;
                }

                if (x118 == "1") {
                    document.getElementById("Cbx14_7").checked = true;
                }
                else {
                    document.getElementById("Cbx14_7").checked = false;
                }

                if (x119 == "1") {
                    document.getElementById("Cbx14_8").checked = true;
                }
                else {
                    document.getElementById("Cbx14_8").checked = false;
                }

                if (x120 == "1") {
                    document.getElementById("Cbx14_9").checked = true;
                }
                else {
                    document.getElementById("Cbx14_9").checked = false;
                }

                if (x131 == "1") {
                    document.getElementById("Cbx14_10").checked = true;
                }
                else {
                    document.getElementById("Cbx14_10").checked = false;
                }

                if (x145 == "1") {
                    document.getElementById("Cbx14_11").checked = true;
                }
                else {
                    document.getElementById("Cbx14_11").checked = false;
                }

                if (x146 == "1") {
                    document.getElementById("Cbx14_12").checked = true;
                }
                else {
                    document.getElementById("Cbx14_12").checked = false;
                }

                if (x121 == "1") {
                    document.getElementById("Cbx15_1").checked = true;
                }
                else {
                    document.getElementById("Cbx15_1").checked = false;
                }

                if (x122 == "1") {
                    document.getElementById("Cbx15_2").checked = true;
                }
                else {
                    document.getElementById("Cbx15_2").checked = false;
                }

                if (x123 == "1") {
                    document.getElementById("Cbx15_3").checked = true;
                }
                else {
                    document.getElementById("Cbx15_3").checked = false;
                }

                if (x124 == "1") {
                    document.getElementById("Cbx15_4").checked = true;
                }
                else {
                    document.getElementById("Cbx15_4").checked = false;
                }

                if (x125 == "1") {
                    document.getElementById("Cbx15_5").checked = true;
                }
                else {
                    document.getElementById("Cbx15_5").checked = false;
                }

                if (x126 == "1") {
                    document.getElementById("Cbx15_6").checked = true;
                }
                else {
                    document.getElementById("Cbx15_6").checked = false;
                }

                if (x127 == "1") {
                    document.getElementById("Cbx15_7").checked = true;
                }
                else {
                    document.getElementById("Cbx15_7").checked = false;
                }

                if (x141 == "1") {
                    document.getElementById("Cbx15_8").checked = true;
                }
                else {
                    document.getElementById("Cbx15_8").checked = false;
                }

                if (x132 == "1") {
                    document.getElementById("Cbx16_1").checked = true;
                }
                else {
                    document.getElementById("Cbx16_1").checked = false;
                }

                if (x133 == "1") {
                    document.getElementById("Cbx16_2").checked = true;
                }
                else {
                    document.getElementById("Cbx16_2").checked = false;
                }

                if (x134 == "1") {
                    document.getElementById("Cbx16_3").checked = true;
                }
                else {
                    document.getElementById("Cbx16_3").checked = false;
                }

                if (x135 == "1") {
                    document.getElementById("Cbx16_4").checked = true;
                }
                else {
                    document.getElementById("Cbx16_4").checked = false;
                }

                if (x136 == "1") {
                    document.getElementById("Cbx16_5").checked = true;
                }
                else {
                    document.getElementById("Cbx16_5").checked = false;
                }

                if (x137 == "1") {
                    document.getElementById("Cbx16_6").checked = true;
                }
                else {
                    document.getElementById("Cbx16_6").checked = false;
                }

                if (x138 == "1") {
                    document.getElementById("Cbx16_7").checked = true;
                }
                else {
                    document.getElementById("Cbx16_7").checked = false;
                }

                if (x139 == "1") {
                    document.getElementById("Cbx16_8").checked = true;
                }
                else {
                    document.getElementById("Cbx16_8").checked = false;
                }

                if (x140 == "1") {
                    document.getElementById("Cbx16_9").checked = true;
                }
                else {
                    document.getElementById("Cbx16_9").checked = false;
                }
            }
        </script>
    </telerik:RadScriptBlock>

    <!-- BEGIN PAGE BAR -->
    <div class="page-bar">
        <ul class="page-breadcrumb">
            <li>
                <span>
                    <asp:Label ID="LblDashboard" runat="server" /></span>
                <i class="fa fa-circle"></i>
            </li>
            <li>
                <span>
                    <asp:Label ID="LblDashPage" runat="server" /></span>
            </li>
        </ul>
        <div class="page-toolbar" id="divPgToolbar" runat="server">
            <div class="clearfix">
                <asp:Label ID="LblPricingPlan" runat="server" />
                <span style="margin-left: 10px;">
                    <a id="aBtnGoPremium" runat="server" href="#PaymentModal" role="button" data-toggle="modal" class="btn btn-circle green-light btn-md" visible="false">
                        <asp:Label ID="LblBtnGoPremium" runat="server" />
                    </a>
                    <a id="aBtnGoFull" runat="server" class="btn btn-circle green-light btn-md" visible="false">
                        <asp:Label ID="LblGoFull" runat="server" />
                    </a>
                </span>
                <br />
                <asp:Label ID="LblRenewalHead" runat="server" Visible="false" /><asp:Label ID="LblRenewal" Visible="false" runat="server" />
            </div>
        </div>
    </div>
    <!-- END PAGE BAR -->
    <!-- BEGIN PAGE TITLE-->
    <h3 class="page-title">
        <asp:Label ID="LblElioplusDashboard" runat="server" />
        <small>
            <asp:Label ID="LblDashSubTitle" runat="server" /></small>
    </h3>
    <!-- END PAGE TITLE-->
    <div class="profile">
        <div class="tabbable-line tabbable-full-width">
            <ul class="nav nav-tabs">
                <li style="display: none;">
                    <a href="#tab_1_1" data-toggle="tab">
                        <asp:Label ID="LblAccountOverview" runat="server" /></a>
                </li>
                <li id="liEditAccount" runat="server" class="active">
                    <a href="#tab_1_3" data-toggle="tab">
                        <asp:Label ID="LblEditAccount" ForeColor="#9A12B3" Font-Bold="true" runat="server" /></a>
                </li>
                <li id="liEditBillingAccount" runat="server">
                    <a href="#tab_1_4" data-toggle="tab">
                        <asp:Label ID="LblEditBillingAccount" ForeColor="#9A12B3" Font-Bold="true" runat="server" /></a>
                </li>
                <li id="liAccountSettings" runat="server">
                    <a href="#tab_1_5" data-toggle="tab">
                        <asp:Label ID="LblAccountSettings" ForeColor="#9A12B3" Font-Bold="true" runat="server" /></a>
                </li>
            </ul>
            <div class="tab-content">
                <!--tab_1_1-->
                <div class="tab-pane" id="tab_1_1" style="display: none;">
                    <div class="row">
                        <div class="col-md-3">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel7">
                                <ContentTemplate>
                                    <ul class="list-unstyled profile-nav">
                                        <li id="liLogo" visible="false" runat="server">
                                            <asp:Label ID="LblLogoHeader" runat="server" ForeColor="#9A12B3" Font-Bold="true" />
                                            <asp:Image ID="ImgCompanyLogo" class="img-responsive pic-bordered" AlternateText="Company logo" runat="server" />
                                        </li>
                                        <li style="margin-top: 10px; overflow: hidden; text-overflow: ellipsis;">
                                            <asp:Label ID="LblUserHeader" runat="server" ForeColor="#9A12B3" Font-Bold="true" />
                                            <br />
                                            <asp:Label ID="LblUsername" runat="server" />
                                        </li>
                                        <li style="margin-top: 10px; overflow: hidden; text-overflow: ellipsis;">
                                            <asp:Label ID="LblEmailHeader" runat="server" ForeColor="#9A12B3" Font-Bold="true" />
                                            <br />
                                            <asp:Label ID="LblEmail" runat="server" />
                                        </li>
                                        <li id="liWebsite" runat="server" visible="false" style="margin-top: 10px; overflow: hidden; text-overflow: ellipsis;">
                                            <asp:Label ID="LblWebsiteHeader" runat="server" ForeColor="#9A12B3" Font-Bold="true" />
                                            <br />
                                            <asp:HyperLink ID="HpLnkWebSite" Style="text-decoration: none; border-left: solid 0px #169ef4; padding: 0px;" Target="_blank" runat="server" />
                                        </li>
                                        <li id="liOfficialEmail" runat="server" visible="false" style="margin-top: 10px; overflow: hidden; text-overflow: ellipsis;">
                                            <asp:Label ID="LblOffEmailHeader" runat="server" ForeColor="#9A12B3" Font-Bold="true" />
                                            <br />
                                            <asp:Label ID="LblOfficialEmail" runat="server" />
                                        </li>
                                        <li id="liAddress" runat="server" visible="false" style="margin-top: 10px; overflow: hidden; text-overflow: ellipsis;">
                                            <asp:Label ID="LblAddressHeader" runat="server" ForeColor="#9A12B3" Font-Bold="true" />
                                            <br />
                                            <asp:Label ID="LblAddress" runat="server" />
                                        </li>
                                        <li id="liPhone" runat="server" visible="false" style="margin-top: 10px; margin-bottom: 50px; overflow: hidden; text-overflow: ellipsis;">
                                            <asp:Label ID="LblPhoneHeader" runat="server" ForeColor="#9A12B3" Font-Bold="true" />
                                            <br />
                                            <asp:Label ID="LblPhone" runat="server" />
                                        </li>
                                    </ul>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="divRightPanelInfo" visible="false" runat="server" class="col-md-9">
                            <div class="row">
                                <asp:UpdatePanel runat="server" ID="UpdatePanel8">
                                    <ContentTemplate>
                                        <div class="col-md-12 profile-info">
                                            <h1 class="font-green sbold uppercase">
                                                <asp:Label ID="LblCompanyName" runat="server" /></h1>
                                            <br />
                                            <asp:Label ID="LblOverviewHeader" runat="server" ForeColor="#9A12B3" Font-Bold="true" />
                                            <p style="text-align: justify">
                                                <asp:Label ID="LblOverview" runat="server" />
                                            </p>
                                            <br />
                                            <asp:Label ID="LblDescriptionHeader" runat="server" ForeColor="#9A12B3" Font-Bold="true" />
                                            <p style="text-align: justify">
                                                <asp:Label ID="LblDescription" runat="server" />
                                            </p>
                                            <ul class="list-inline">
                                                <li id="liCountry" runat="server" visible="false">
                                                    <i class="fa fa-globe"></i>
                                                    <asp:Label ID="LblCountry" runat="server" />
                                                </li>
                                                <li>
                                                    <i class="fa fa-sign-in"></i>
                                                    <asp:Label ID="LblRegDate" runat="server" />
                                                </li>
                                                <li id="liType" runat="server" visible="false">
                                                    <i class="fa fa-briefcase"></i>
                                                    <asp:Label ID="LblType" runat="server" />
                                                </li>
                                                <li>
                                                    <i class="fa fa-star"></i>
                                                    <asp:Label ID="LblBillingType" runat="server" />
                                                </li>
                                            </ul>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <!--end row-->
                            <div class="tabbable-line tabbable-custom-profile">
                                <ul class="nav nav-tabs">
                                    <li class="active">
                                        <a href="#tab_1_11" data-toggle="tab">
                                            <asp:Label ID="LblCompanyCharacteristics" ForeColor="#9A12B3" Font-Bold="true" runat="server" /></a>
                                    </li>
                                    <li id="liSubcategories" runat="server">
                                        <a href="#tab_1_22" data-toggle="tab">
                                            <asp:Label ID="LblSubcategories" ForeColor="#9A12B3" Font-Bold="true" runat="server" /></a>
                                    </li>
                                </ul>
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tab_1_11">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel9">
                                            <ContentTemplate>
                                                <div class="portlet-body">
                                                    <table class="table table-striped table-bordered table-advance table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <i class="fa fa-tasks" style="margin-right: 10px;"></i>
                                                                    <asp:Label ID="LblCharacteristic" ForeColor="#9A12B3" Font-Bold="true" runat="server" />
                                                                </th>
                                                                <th class="hidden-xs">
                                                                    <i class="fa fa-bars" style="margin-right: 10px;"></i>
                                                                    <asp:Label ID="LblValue" ForeColor="#9A12B3" Font-Bold="true" runat="server" />
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr id="rowIndustry" runat="server" visible="false">
                                                                <td>
                                                                    <asp:Label ID="LblIndustry" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LblIndustryValue" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr id="rowProgram" runat="server" visible="false">
                                                                <td>
                                                                    <asp:Label ID="LblProgram" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LblProgramValue" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr id="rowMarket" runat="server" visible="false">
                                                                <td>
                                                                    <asp:Label ID="LblMarket" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LblMarketValue" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr id="rowAPI" runat="server" visible="false">
                                                                <td>
                                                                    <asp:Label ID="LblAPI" runat="server" />
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="LblAPIValue" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <!--tab-pane-->
                                    <div class="tab-pane" id="tab_1_22">
                                        <div class="tab-pane active" id="tab_1_1_1">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                                                <ContentTemplate>
                                                    <div class="portlet-body">
                                                        <table class="table table-striped table-bordered table-advance table-hover">
                                                            <thead>
                                                                <tr>
                                                                    <th>
                                                                        <i class="fa fa-tasks" style="margin-right: 10px;"></i>
                                                                        <asp:Label ID="LblSubcategoryGroup" ForeColor="#9A12B3" Font-Bold="true" runat="server" />
                                                                    </th>
                                                                    <th class="hidden-xs">
                                                                        <i class="fa fa-bars" style="margin-right: 10px;"></i>
                                                                        <asp:Label ID="LblSubcategoryItem" ForeColor="#9A12B3" Font-Bold="true" runat="server" />
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr id="rowSalMark" runat="server" visible="false">
                                                                    <td>
                                                                        <asp:Label ID="LblSalMark" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblSalMarkValue" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr id="rowCustMan" runat="server" visible="false">
                                                                    <td>
                                                                        <asp:Label ID="LblCustMan" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblCustManValue" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr id="rowProjMan" runat="server" visible="false">
                                                                    <td>
                                                                        <asp:Label ID="LblProjMan" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblProjManValue" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr id="rowOperWork" runat="server" visible="false">
                                                                    <td>
                                                                        <asp:Label ID="LblOperWork" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblOperWorkValue" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr id="rowTracKMeaus" runat="server" visible="false">
                                                                    <td>
                                                                        <asp:Label ID="LblTracKMeaus" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblTracKMeausValue" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr id="rowAccFin" runat="server" visible="false">
                                                                    <td>
                                                                        <asp:Label ID="LblAccFin" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblAccFinValue" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr id="rowHR" runat="server" visible="false">
                                                                    <td>
                                                                        <asp:Label ID="LblHR" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblHRValue" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr id="rowWMSD" runat="server" visible="false">
                                                                    <td>
                                                                        <asp:Label ID="LblWMSD" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblWMSDValue" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr id="rowITInfr" runat="server" visible="false">
                                                                    <td>
                                                                        <asp:Label ID="LblITInfr" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblITInfrValue" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr id="rowBusUtil" runat="server" visible="false">
                                                                    <td>
                                                                        <asp:Label ID="LblBusUtil" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblBusUtilValue" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr id="rowSecBack" runat="server" visible="false">
                                                                    <td>
                                                                        <asp:Label ID="LblSecBack" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblSecBackValue" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr id="rowDesMult" runat="server" visible="false">
                                                                    <td>
                                                                        <asp:Label ID="LblDesMult" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblDesMultValue" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr id="rowMisc" runat="server" visible="false">
                                                                    <td>
                                                                        <asp:Label ID="LblMisc" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblMiscValue" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr id="rowUnCom" runat="server" visible="false">
                                                                    <td>
                                                                        <asp:Label ID="LblUnCom" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblUnComValue" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr id="rowCadPlm" runat="server" visible="false">
                                                                    <td>
                                                                        <asp:Label ID="LblCadPlm" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblCadPlmValue" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr id="rowHrdware" runat="server" visible="false">
                                                                    <td>
                                                                        <asp:Label ID="LblHrdware" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LblHrdwareValue" runat="server" />
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <!--tab-pane-->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--tab_1_3-->
                <div class="tab-pane active" id="tab_1_3">
                    <div class="row profile-account">
                        <div class="col-md-3">
                            <ul class="ver-inline-menu tabbable margin-bottom-10">
                                <li class="active">
                                    <a data-toggle="tab" href="#tab_1-1">
                                        <i class="fa fa-keyboard-o"></i>
                                        <asp:Label ID="LblCompanyGeneralInfo" runat="server" />
                                    </a>
                                    <span class="after"></span>
                                </li>
                                <li id="liPersonalInfo" visible="false" runat="server">
                                    <a data-toggle="tab" href="#tab_7-7">
                                        <i class="fa fa-pencil-square-o"></i>
                                        <asp:Label ID="LblPersonalInfo" runat="server" />
                                    </a>
                                    <span class="after"></span>
                                </li>
                                <li id="liCompanyBusinessInfo" visible="false" runat="server">
                                    <a data-toggle="tab" href="#tab_4-4">
                                        <i class="fa fa-pencil-square-o"></i>
                                        <asp:Label ID="LblCompanyBusinessInfo" runat="server" />
                                    </a>
                                    <span class="after"></span>
                                </li>
                                <li id="liCompanySubcategories" visible="false" runat="server">
                                    <a data-toggle="tab" href="#tab_5-5">
                                        <i class="fa fa-database"></i>
                                        <asp:Label ID="LblCompanySubcategories" runat="server" />
                                    </a>
                                    <span class="after"></span>
                                </li>
                                <li id="liCompanyProducts" visible="false" runat="server">
                                    <a data-toggle="tab" href="#tab_11-11">
                                        <i class="fa fa-database"></i>
                                        <asp:Label ID="LblCompanyProducts" runat="server" />
                                    </a>
                                    <span class="after"></span>
                                </li>
                                <li>
                                    <a data-toggle="tab" href="#tab_3-3">
                                        <i class="fa fa-lock"></i>
                                        <asp:Label ID="LblCompanyPassword" runat="server" />
                                    </a>
                                </li>
                            </ul>
                        </div>
                        <div class="col-md-9">
                            <div class="tab-content">
                                <div id="tab_1-1" class="tab-pane active">
                                    <div role="form">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                            <ContentTemplate>
                                                <div id="divCompanyName" visible="false" runat="server" class="form-group">
                                                    <asp:Label ID="LblCompany" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxCompanyName" CssClass="form-control" MaxLength="100" runat="server" />
                                                    <div id="divCompanyNameError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblCompanyNameError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="LblCompanyUsername" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxCompanyUsername" CssClass="form-control" MaxLength="45" runat="server" />
                                                    <div id="divCompanyUsernameError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblCompanyUsernameError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="LblCompanyEmail" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxCompanyEmail" CssClass="form-control" MaxLength="95" runat="server" />
                                                    <div id="divCompanyEmailError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblCompanyEmailError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="divCompanyOfficialEmail" visible="false" runat="server" class="form-group">
                                                    <asp:Label ID="LblCompanyOfficialEmail" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxCompanyOfficialEmail" CssClass="form-control" MaxLength="95" runat="server" />
                                                    <div id="divCompanyOfficialEmailError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblCompanyOfficialEmailError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="divPatrnerEmail" runat="server" class="form-group">
                                                    <asp:Label ID="LblPartnerEmail" Text="More Emails" CssClass="control-label" runat="server" />
                                                    <a id="aAddMoreEmails" href="#AddMoreEmailsModal" role="button" data-toggle="modal" runat="server">
                                                        <asp:Image ID="ImgBtnAddEmail" ImageUrl="~/images/icons/add_btn_6.png" ToolTip="Add Email" runat="server" />
                                                    </a>
                                                    <telerik:RadGrid ID="RdgEmails" Style="position: relative;" AllowPaging="true" AllowSorting="false" HeaderStyle-BackColor="#39b4ef" PagerStyle-Position="Bottom" HeaderStyle-ForeColor="#ffffff" PageSize="5" Width="400" CssClass="rgdd" OnItemDataBound="RdgEmails_OnItemDataBound" OnNeedDataSource="RdgEmails_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                                        <MasterTableView>
                                                            <NoRecordsTemplate>
                                                                <div class="emptyGridHolder">
                                                                    <asp:Literal ID="LtlNoDataFound" runat="server" />
                                                                </div>
                                                            </NoRecordsTemplate>
                                                            <Columns>
                                                                <telerik:GridBoundColumn HeaderStyle-Width="20" Display="false" DataField="id" UniqueName="id" />
                                                                <telerik:GridBoundColumn HeaderStyle-Width="80" Display="false" DataField="email" UniqueName="email" />
                                                                <telerik:GridTemplateColumn HeaderStyle-Width="300" HeaderText="Email">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LblMoreEmail" runat="server" />
                                                                        <telerik:RadTextBox ID="RtbxEmail" Width="280" MaxLength="95" Visible="false" runat="server" />
                                                                        <asp:Panel ID="divPartnerEmailError" runat="server" Visible="false" class="form-group has-error" Style="margin-bottom: 30px;">
                                                                            <asp:Label ID="LblPartnerEmailError" CssClass="control-label col-md-12" runat="server" />
                                                                        </asp:Panel>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn HeaderStyle-Width="80" HeaderText="Actions" DataField="actions" UniqueName="actions">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImgBtnEdit" OnClick="ImgBtnEdit_OnClick" ToolTip="Edit Email" ImageUrl="~/Images/edit.png" runat="server" />
                                                                        <asp:ImageButton ID="ImgBtnCancel" Visible="false" OnClick="ImgBtnCancel_OnClick" ToolTip="Cancel Edit Email" ImageUrl="~/Images/cancel.png" runat="server" />
                                                                        <asp:ImageButton ID="ImgBtnSave" ImageUrl="~/Images/save.png" Visible="false" OnClick="ImgBtnSave_OnClick" ToolTip="Save Email" runat="server" />
                                                                        <asp:ImageButton ID="ImgBtnDelete" ImageUrl="~/Images/icons/small/delete.png" OnClick="ImgBtnDelete_OnClick" ToolTip="Delete Email" runat="server" />
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                </div>
                                                <div id="divCompanyWebsite" visible="false" runat="server" class="form-group">
                                                    <asp:Label ID="LblCompanyWebsite" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxCompanyWebsite" CssClass="form-control" MaxLength="145" runat="server" />
                                                    <div id="divCompanyWebsiteError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblCompanyWebsiteError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="divCompanyCountry" visible="false" runat="server" class="form-group">
                                                    <asp:Label ID="LblCompanyCountry" CssClass="control-label" runat="server" />
                                                    <asp:DropDownList ID="DdlCountries" Width="100%" runat="server">
                                                    </asp:DropDownList>
                                                    <div id="divCompanyCountryError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblCompanyCountryError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="divCompanyAddress" visible="false" runat="server" class="form-group">
                                                    <asp:Label ID="LblCompanyAddress" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxCompanyAddress" CssClass="form-control" MaxLength="350" runat="server" />
                                                    <div id="divCompanyAddressError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblCompanyAddressError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="divCompanyPhone" visible="false" runat="server" class="form-group">
                                                    <asp:Label ID="LblCompanyPhone" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxCompanyPhone" CssClass="form-control" onkeypress="return event.charCode >= 48 && event.charCode <= 57" MaxLength="18" runat="server" />
                                                    <div id="divCompanyPhoneError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblCompanyPhoneError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="divCompanyProductDemo" visible="false" runat="server" class="form-group">
                                                    <asp:Label ID="LblCompanyProductDemo" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxCompanyProductDemo" placeholder="https://company-name/product-demo-link" CssClass="form-control" MaxLength="350" runat="server" />
                                                    <div id="divCompanyProductDemoError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblCompanyProductDemoError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="divMashape" visible="false" runat="server" class="form-group">
                                                    <asp:Label ID="LblMashape" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxMashape" CssClass="form-control" MaxLength="145" runat="server" />
                                                    <div id="divMashapeError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblMashapeError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="divUploadCompanyLogo" runat="server" class="form-group">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                                                        <ContentTemplate>
                                                            <asp:Panel ID="PnlEditLogo" Visible="false" runat="server">
                                                                <asp:Label ID="LblLogoUpdateHeader" ForeColor="#9A12B3" Font-Bold="true" runat="server" />
                                                                <br />
                                                                <div class="form-group" style="margin-top: 10px;">
                                                                    <div class="fileinput fileinput-new" data-provides="fileinput">
                                                                        <div class="fileinput-new thumbnail" style="width: 200px; height: 150px;">
                                                                            <asp:Image ID="ImgPhotoBckgr" ImageUrl="https://www.placehold.it/200x150/EFEFEF/AAAAAA&amp;text=no+image" AlternateText="Update logo" runat="server" />
                                                                        </div>
                                                                        <div class="fileinput-preview fileinput-exists thumbnail" style="max-width: 200px; max-height: 150px;"></div>
                                                                        <div>
                                                                            <span class="btn default btn-file">
                                                                                <span class="fileinput-new">
                                                                                    <asp:Label ID="LblSelectImg" runat="server" /></span>
                                                                                <span class="fileinput-exists">
                                                                                    <asp:Label ID="LblChangeImg" runat="server" /></span>
                                                                                <input type="file" enableviewstate="true" name="ImgCompanyLogo" id="CompanyLogo" runat="server" accept=".png, .jpg, .jpeg" />
                                                                            </span>
                                                                            <a class="btn default fileinput-exists" data-dismiss="fileinput">
                                                                                <asp:Label ID="LblRemoveImg" runat="server" /></a>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div id="divLogoSuccess" runat="server" visible="false" class="alert alert-success">
                                                                    <strong>
                                                                        <asp:Label ID="LblLogoSuccess" runat="server" /></strong><asp:Label ID="LblLogoSuccessContent" runat="server" />
                                                                </div>
                                                                <div id="divLogoFailure" runat="server" visible="false" class="alert alert-danger">
                                                                    <strong>
                                                                        <asp:Label ID="LblLogoFailure" runat="server" /></strong><asp:Label ID="LblLogoFailureContent" runat="server" />
                                                                </div>
                                                                <div class="margin-top-10">
                                                                    <asp:Button ID="BtnSubmitLogo" OnClick="BtnSubmitLogo_OnClick" CssClass="btn green" runat="server" />
                                                                </div>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="BtnSubmitLogo" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div id="divCompanyOverview" visible="false" runat="server" class="form-group">
                                                    <asp:Label ID="LblCompanyOverview" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxCompanyOverview" TextMode="MultiLine" Rows="10" MaxLength="2000" CssClass="form-control" runat="server" />
                                                    <div id="divCompanyOverviewError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblCompanyOverviewError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="divCompanyDescription" visible="false" runat="server" class="form-group">
                                                    <asp:Label ID="LblCompanyDescription" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxCompanyDescription" TextMode="MultiLine" Rows="10" MaxLength="2000" CssClass="form-control" runat="server" />
                                                    <div id="divCompanyDescriptionError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblCompanyDescriptionError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="divGeneralSuccess" runat="server" visible="false" class="alert alert-success">
                                                    <strong>
                                                        <asp:Label ID="LblGeneralSuccess" runat="server" /></strong><asp:Label ID="LblGeneralSuccessContent" runat="server" />
                                                </div>
                                                <div id="divGeneralFailure" runat="server" visible="false" class="alert alert-danger">
                                                    <strong>
                                                        <asp:Label ID="LblGeneralFailure" runat="server" /></strong><asp:Label ID="LblGeneralFailureContent" runat="server" />
                                                </div>
                                                <div class="margin-top-10">
                                                    <asp:Button ID="BtnSaveGeneral" OnClick="BtnSaveGeneral_OnClick" CssClass="btn green" runat="server" />
                                                    <asp:Button ID="BtnCancelGeneral" OnClick="BtnCancelGeneral_OnClick" CssClass="btn default" runat="server" />
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div id="tab_3-3" class="tab-pane">
                                    <div role="form">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                                <div class="form-group">
                                                    <asp:Label ID="LblCurPasw" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxCurPasw" CssClass="form-control" type="password" MaxLength="40" runat="server" />
                                                    <div id="divCurPaswError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblCurPaswError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="LblNewPasw" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxNewPasw" CssClass="form-control" type="password" MaxLength="40" runat="server" />
                                                    <div id="divNewPaswError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblNewPaswError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="LblRetNewPasw" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxRetNewPasw" CssClass="form-control" type="password" MaxLength="40" runat="server" />
                                                    <div id="divRetNewPaswError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblRetNewPaswError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="divPaswSuccess" runat="server" visible="false" class="alert alert-success">
                                                    <strong>
                                                        <asp:Label ID="LblPaswSuccess" runat="server" /></strong><asp:Label ID="LblPaswSuccessContent" runat="server" />
                                                </div>
                                                <div id="divPaswFailure" runat="server" visible="false" class="alert alert-danger">
                                                    <strong>
                                                        <asp:Label ID="LblPaswFailure" runat="server" /></strong><asp:Label ID="LblPaswFailureContent" runat="server" />
                                                </div>
                                                <div class="margin-top-10">
                                                    <asp:Button ID="BtnSavePasw" OnClick="BtnChangePassword_OnClick" CssClass="btn green" runat="server" />
                                                    <asp:Button ID="BtnCancelPasw" OnClick="BtnCancelChangePassword_OnClick" CssClass="btn default" runat="server" />
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div id="tab_4-4" class="tab-pane">
                                    <div role="form">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                            <ContentTemplate>
                                                <div id="divIndustriesSelection" runat="server" visible="false" class="portlet light bordered">
                                                    <div class="portlet-title">
                                                        <div class="caption font-green-haze">
                                                            <i class="icon-settings font-green-haze"></i>
                                                            <span class="caption-subject bold uppercase">
                                                                <asp:Label ID="LblIndustriesTitle" runat="server" /></span>
                                                        </div>
                                                        <div class="actions">
                                                            <a class="btn btn-circle btn-icon-only btn-default fullscreen" data-original-title="" title=""></a>
                                                        </div>
                                                    </div>
                                                    <div class="portlet-body form">
                                                        <div role="form" class="form-horizontal">
                                                            <div class="form-body">
                                                                <div class="form-group form-md-line-input">
                                                                    <div class="col-md-12">
                                                                        <div class="md-checkbox-list">
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxIndAdvMark" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxIndAdvMark">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblIndAdvMark" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnIndAdvMark" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxIndCommun" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxIndCommun">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblIndCommun" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnIndCommun" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxIndConsWeb" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxIndConsWeb">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblIndConsWeb" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnIndConsWeb" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxIndDigMed" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxIndDigMed">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblIndDigMed" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnIndDigMed" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxIndEcom" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxIndEcom">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblIndEcom" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnIndEcom" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxIndEduc" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxIndEduc">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblIndEduc" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnIndEduc" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxIndEnter" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxIndEnter">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblIndEnter" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnIndEnter" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxIndEntGam" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxIndEntGam">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblIndEntGam" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnIndEntGam" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxIndHard" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxIndHard">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblIndHard" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnIndHard" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxIndMob" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxIndMob">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblIndMob" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnIndMob" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxIndNetHos" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxIndNetHos">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblIndNetHos" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnIndNetHos" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxIndSocMed" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxIndSocMed">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblIndSocMed" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnIndSocMed" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxIndSoft" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxIndSoft">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblIndSoft" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnIndSoft" runat="server" />
                                                                            </div>

                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd14" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd14">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd14" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd14" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd15" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd15">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd15" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd15" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd16" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd16">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd16" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd16" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd17" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd17">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd17" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd17" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd18" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd18">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd18" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd18" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd19" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd19">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd19" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd19" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd20" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd20">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd20" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd20" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd21" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd21">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd21" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd21" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd22" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd22">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd22" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd22" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd23" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd23">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd23" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd23" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd24" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd24">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd24" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd24" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd25" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd25">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd25" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd25" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd26" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd26">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd26" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd26" runat="server" />
                                                                            </div>

                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd27" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd27">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd27" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd27" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd28" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd28">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd28" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd28" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd29" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd29">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd29" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd29" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd30" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd30">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd30" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd30" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd31" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd31">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd31" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd31" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd32" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd32">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd32" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd32" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd33" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd33">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd33" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd33" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd34" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd34">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd34" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd34" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd35" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd35">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd35" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd35" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd36" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd36">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd36" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd36" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd37" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd37">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd37" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd37" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd38" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd38">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd38" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd38" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd39" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd39">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd39" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd39" runat="server" />
                                                                            </div>

                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd40" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd40">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd40" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd40" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd41" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd41">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd41" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd41" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxInd42" onclick="UpdateIndustries()" class="md-check" />
                                                                                <label for="CbxInd42">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblInd42" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnInd42" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="divProgramsSelection" runat="server" visible="false" class="portlet light bordered">
                                                    <div class="portlet-title">
                                                        <div class="caption font-green-haze">
                                                            <i class="icon-settings font-green-haze"></i>
                                                            <span class="caption-subject bold uppercase">
                                                                <asp:Label ID="LblProgramsTitle" runat="server" /></span>
                                                        </div>
                                                        <div class="actions">
                                                            <a class="btn btn-circle btn-icon-only btn-default fullscreen" data-original-title="" title=""></a>
                                                        </div>
                                                    </div>
                                                    <div class="portlet-body form">
                                                        <div role="form" class="form-horizontal">
                                                            <div class="form-body">
                                                                <div class="form-group form-md-line-input">
                                                                    <div class="col-md-12">
                                                                        <div class="md-checkbox-list">
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxProgWhiteL" onclick="UpdatePrograms()" class="md-check" />
                                                                                <label for="CbxProgWhiteL">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblProgWhiteL" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnProgWhiteL" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxProgResel" onclick="UpdatePrograms()" class="md-check" />
                                                                                <label for="CbxProgResel">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblProgResel" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnProgResel" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxProgVAR" onclick="UpdatePrograms()" class="md-check" />
                                                                                <label for="CbxProgVAR">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblProgVAR" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnProgVAR" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxProgDistr" onclick="UpdatePrograms()" class="md-check" />
                                                                                <label for="CbxProgDistr">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblProgDistr" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnProgDistr" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxProgAPIprg" onclick="UpdatePrograms()" class="md-check" />
                                                                                <label for="CbxProgAPIprg">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblProgAPIprg" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnProgAPIprg" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxProgSysInteg" onclick="UpdatePrograms()" class="md-check" />
                                                                                <label for="CbxProgSysInteg">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblProgSysInteg" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnProgSysInteg" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxProgServProv" onclick="UpdatePrograms()" class="md-check" />
                                                                                <label for="CbxProgServProv">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblProgServProv" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnProgServProv" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="divMarketsSelection" runat="server" visible="false" class="portlet light bordered">
                                                    <div class="portlet-title">
                                                        <div class="caption font-green-haze">
                                                            <i class="icon-settings font-green-haze"></i>
                                                            <span class="caption-subject bold uppercase">
                                                                <asp:Label ID="LblMarketsTitle" runat="server" /></span>
                                                        </div>
                                                        <div class="actions">
                                                            <a class="btn btn-circle btn-icon-only btn-default fullscreen" data-original-title="" title=""></a>
                                                        </div>
                                                    </div>
                                                    <div class="portlet-body form">
                                                        <div role="form" class="form-horizontal">
                                                            <div class="form-body">
                                                                <div class="form-group form-md-line-input">
                                                                    <div class="col-md-12">
                                                                        <div class="md-checkbox-list">
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxMarkConsum" onclick="UpdateMarkets()" class="md-check" />
                                                                                <label for="CbxMarkConsum">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblMarkConsum" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnMarkConsum" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxMarkSOHO" onclick="UpdateMarkets()" class="md-check" />
                                                                                <label for="CbxMarkSOHO">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblMarkSOHO" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnMarkSOHO" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxMarkSmallMid" onclick="UpdateMarkets()" class="md-check" />
                                                                                <label for="CbxMarkSmallMid">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblMarkSmallMid" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnMarkSmallMid" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxMarkEnter" onclick="UpdateMarkets()" class="md-check" />
                                                                                <label for="CbxMarkEnter">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblMarkEnter" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnMarkEnter" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="divAPIsSelection" runat="server" visible="false" class="portlet light bordered">
                                                    <div class="portlet-title">
                                                        <div class="caption font-green-haze">
                                                            <i class="icon-settings font-green-haze"></i>
                                                            <span class="caption-subject bold uppercase">
                                                                <asp:Label ID="LblAPIsTitle" runat="server" /></span>
                                                        </div>
                                                        <div class="actions">
                                                            <a class="btn btn-circle btn-icon-only btn-default fullscreen" data-original-title="" title=""></a>
                                                        </div>
                                                    </div>
                                                    <div class="portlet-body form">
                                                        <div role="form" class="form-horizontal">
                                                            <div class="form-body">
                                                                <div class="form-group form-md-line-input">
                                                                    <div class="col-md-12">
                                                                        <div class="md-checkbox-list">
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxAPIBusServ" onclick="UpdateAPIs()" class="md-check" />
                                                                                <label for="CbxAPIBusServ">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblAPIBusServ" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnAPIBusServ" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxAPIMedEnter" onclick="UpdateAPIs()" class="md-check" />
                                                                                <label for="CbxAPIMedEnter">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblAPIMedEnter" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnAPIMedEnter" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxAPIRetEcom" onclick="UpdateAPIs()" class="md-check" />
                                                                                <label for="CbxAPIRetEcom">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblAPIRetEcom" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnAPIRetEcom" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxAPIGeol" onclick="UpdateAPIs()" class="md-check" />
                                                                                <label for="CbxAPIGeol">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblAPIGeol" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnAPIGeol" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxAPISoc" onclick="UpdateAPIs()" class="md-check" />
                                                                                <label for="CbxAPISoc">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblAPISoc" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnAPISoc" runat="server" />
                                                                            </div>
                                                                            <div class="md-checkbox col-md-4">
                                                                                <input type="checkbox" id="CbxAPIHeal" onclick="UpdateAPIs()" class="md-check" />
                                                                                <label for="CbxAPIHeal">
                                                                                    <span></span>
                                                                                    <span class="check"></span>
                                                                                    <span class="box"></span>
                                                                                    <span style="margin-left: 30px; margin-top: -10px;">
                                                                                        <asp:Label ID="LblAPIHeal" runat="server" /></span>
                                                                                </label>
                                                                                <asp:HiddenField ID="HdnAPIHeal" runat="server" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--end profile-settings-->
                                                <div id="divBusinessSuccess" runat="server" visible="false" class="alert alert-success" style="margin-top: 30px;">
                                                    <strong>
                                                        <asp:Label ID="LblBusinessSuccess" runat="server" /></strong><asp:Label ID="LblBusinessSuccessContent" runat="server" />
                                                </div>
                                                <div id="divBusinessFailure" runat="server" visible="false" class="alert alert-danger" style="margin-top: 30px;">
                                                    <strong>
                                                        <asp:Label ID="LblBusinessFailure" runat="server" /></strong><asp:Label ID="LblBusinessFailureContent" runat="server" />
                                                </div>
                                                <div class="margin-top-10">
                                                    <asp:Button ID="BtnSaveBusinessSettings" OnClick="BtnSaveBusinessSettings_OnClick" CssClass="btn green" runat="server" />
                                                    <asp:Button ID="BtnCanceBusinessSettings" OnClick="BtnCanceBusinessSettings_OnClick" CssClass="btn grey-salsa btn-outline" runat="server" />
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div id="tab_5-5" class="tab-pane">
                                    <div class="portlet box purple">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-qrcode"></i>
                                                <asp:Label ID="LblIndustryVerticalsEdit" runat="server" />
                                            </div>
                                            <div class="tools">
                                                <a class="collapse"></a>
                                                <a class="reload"></a>
                                                <a class="remove"></a>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="panel-group accordion scrollable" id="accordion2">
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_1">
                                                                <asp:Label ID="LblVertSalMark" runat="server" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_1" class="panel-collapse in">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="md-checkbox-list">
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx1_1" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx1_1">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl1_1" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn1_1" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx1_2" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx1_2">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl1_2" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn1_2" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx1_3" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx1_3">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl1_3" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn1_3" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx1_4" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx1_4">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl1_4" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn1_4" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx1_5" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx1_5">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl1_5" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn1_5" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx1_6" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx1_6">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl1_6" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn1_6" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx1_7" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx1_7">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl1_7" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn1_7" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx1_8" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx1_8">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl1_8" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn1_8" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx1_9" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx1_9">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl1_9" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn1_9" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx1_10" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx1_10">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl1_10" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn1_10" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx1_11" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx1_11">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl1_11" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn1_11" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx1_12" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx1_12">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl1_12" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn1_12" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx1_13" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx1_13">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl1_13" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn1_13" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx1_14" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx1_14">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl1_14" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn1_14" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx1_15" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx1_15">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl1_15" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn1_15" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx1_16" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx1_16">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl1_16" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn1_16" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx1_17" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx1_17">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl1_17" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn1_17" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx1_18" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx1_18">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl1_18" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn1_18" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_2">
                                                                <asp:Label ID="LblVertCustMan" runat="server" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_2" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="md-checkbox-list">
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx2_1" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx2_1">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl2_1" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn2_1" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx2_2" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx2_2">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl2_2" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn2_2" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx2_3" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx2_3">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl2_3" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn2_3" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx2_4" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx2_4">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl2_4" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn2_4" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx2_5" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx2_5">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl2_5" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn2_5" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx2_6" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx2_6">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl2_6" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn2_6" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_3">
                                                                <asp:Label ID="LblVertProjMan" runat="server" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_3" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="md-checkbox-list">
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx3_1" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx3_1">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl3_1" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn3_1" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx3_2" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx3_2">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl3_2" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn3_2" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx3_3" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx3_3">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl3_3" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn3_3" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_4">
                                                                <asp:Label ID="LblVertOperWork" runat="server" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_4" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="md-checkbox-list">
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_1" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_1">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_1" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_1" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_2" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_2">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_2" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_2" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_3" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_3">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_3" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_3" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_4" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_4">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_4" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_4" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_5" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_5">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_5" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_5" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_6" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_6">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_6" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_6" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_7" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_7">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_7" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_7" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_8" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_8">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_8" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_8" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_9" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_9">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_9" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_9" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_10" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_10">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_10" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_10" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_11" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_11">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_11" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_11" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_12" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_12">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_12" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_12" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_13" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_13">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_13" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_13" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_14" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_14">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_14" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_14" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_15" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_15">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_15" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_15" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_16" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_16">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_16" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_16" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_17" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_17">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_17" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_17" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_18" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_18">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_18" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_18" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_19" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_19">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_19" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_19" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_20" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_20">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_20" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_20" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_21" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_21">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_21" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_21" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx4_22" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx4_22">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl4_22" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn4_22" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_5">
                                                                <asp:Label ID="LblVertTrackMeaus" runat="server" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_5" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="md-checkbox-list">
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx5_1" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx5_1">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl5_1" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn5_1" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx5_2" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx5_2">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl5_2" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn5_2" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx5_3" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx5_3">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl5_3" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn5_3" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx5_4" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx5_4">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl5_4" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn5_4" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx5_5" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx5_5">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl5_5" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn5_5" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_6">
                                                                <asp:Label ID="LblVertAccFin" runat="server" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_6" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="md-checkbox-list">
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx6_1" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx6_1">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl6_1" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn6_1" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx6_2" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx6_2">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl6_2" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn6_2" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx6_3" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx6_3">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl6_3" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn6_3" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx6_4" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx6_4">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl6_4" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn6_4" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx6_5" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx6_5">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl6_5" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn6_5" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_7">
                                                                <asp:Label ID="LblVertHR" runat="server" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_7" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="md-checkbox-list">
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx7_1" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx7_1">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl7_1" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn7_1" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx7_2" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx7_2">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl7_2" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn7_2" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx7_3" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx7_3">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl7_3" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn7_3" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx7_4" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx7_4">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl7_4" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn7_4" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx7_5" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx7_5">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl7_5" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn7_5" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx7_6" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx7_6">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl7_6" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn7_6" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx7_7" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx7_7">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl7_7" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn7_7" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_8">
                                                                <asp:Label ID="LblVertWMSD" runat="server" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_8" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="md-checkbox-list">
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx8_1" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx8_1">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl8_1" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn8_1" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx8_2" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx8_2">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl8_2" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn8_2" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx8_3" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx8_3">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl8_3" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn8_3" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx8_4" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx8_4">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl8_4" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn8_4" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx8_5" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx8_5">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl8_5" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn8_5" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx8_6" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx8_6">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl8_6" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn8_6" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx8_7" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx8_7">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl8_7" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn8_7" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx8_8" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx8_8">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl8_8" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn8_8" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx8_9" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx8_9">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl8_9" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn8_9" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_9">
                                                                <asp:Label ID="LblVertITInfr" runat="server" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_9" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="md-checkbox-list">
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx9_1" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx9_1">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl9_1" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn9_1" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx9_2" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx9_2">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl9_2" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn9_2" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx9_3" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx9_3">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl9_3" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn9_3" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx9_4" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx9_4">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl9_4" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn9_4" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx9_5" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx9_5">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl9_5" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn9_5" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx9_6" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx9_6">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl9_6" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn9_6" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx9_7" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx9_7">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl9_7" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn9_7" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx9_8" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx9_8">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl9_8" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn9_8" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx9_9" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx9_9">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl9_9" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn9_9" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx9_10" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx9_10">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl9_10" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn9_10" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx9_11" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx9_11">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl9_11" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn9_11" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx9_12" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx9_12">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl9_12" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn9_12" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx9_13" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx9_13">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl9_13" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn9_13" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_10">
                                                                <asp:Label ID="LblVertBussUtil" runat="server" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_10" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="md-checkbox-list">
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx10_1" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx10_1">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl10_1" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn10_1" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx10_2" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx10_2">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl10_2" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn10_2" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx10_3" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx10_3">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl10_3" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn10_3" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx10_4" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx10_4">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl10_4" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn10_4" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx10_5" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx10_5">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl10_5" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn10_5" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx10_6" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx10_6">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl10_6" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn10_6" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx10_7" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx10_7">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl10_7" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn10_7" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx10_8" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx10_8">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl10_8" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn10_8" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx10_9" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx10_9">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl10_9" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn10_9" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_11">
                                                                <asp:Label ID="LblVertSecBack" runat="server" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_11" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="md-checkbox-list">
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx11_1" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx11_1">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl11_1" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn11_1" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx11_2" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx11_2">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl11_2" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn11_2" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx11_3" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx11_3">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl11_3" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn11_3" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx11_4" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx11_4">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl11_4" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn11_4" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx11_5" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx11_5">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl11_5" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn11_5" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx11_6" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx11_6">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl11_6" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn11_6" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx11_7" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx11_7">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl11_7" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn11_7" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx11_8" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx11_8">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl11_8" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn11_8" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx11_9" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx11_9">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl11_9" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn11_9" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx11_10" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx11_10">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl11_10" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn11_10" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx11_11" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx11_11">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl11_11" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn11_11" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx11_12" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx11_12">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl11_12" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn11_12" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx11_13" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx11_13">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl11_13" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn11_13" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx11_14" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx11_14">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl11_14" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn11_14" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx11_15" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx11_15">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl11_15" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn11_15" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx11_16" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx11_16">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl11_16" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn11_16" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_12">
                                                                <asp:Label ID="LblVertDesMult" runat="server" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_12" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="md-checkbox-list">
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx12_1" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx12_1">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl12_1" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn12_1" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx12_2" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx12_2">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl12_2" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn12_2" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx12_3" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx12_3">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl12_3" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn12_3" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx12_4" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx12_4">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl12_4" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn12_4" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_13">
                                                                <asp:Label ID="LblVertMisc" runat="server" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_13" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="md-checkbox-list">
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx13_1" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx13_1">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl13_1" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn13_1" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx13_2" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx13_2">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl13_2" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn13_2" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx13_3" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx13_3">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl13_3" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn13_3" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_14">
                                                                <asp:Label ID="LblVertUnCom" runat="server" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_14" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="md-checkbox-list">
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx14_1" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx14_1">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl14_1" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn14_1" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx14_2" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx14_2">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl14_2" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn14_2" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx14_3" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx14_3">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl14_3" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn14_3" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx14_4" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx14_4">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl14_4" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn14_4" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx14_5" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx14_5">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl14_5" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn14_5" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx14_6" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx14_6">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl14_6" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn14_6" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx14_7" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx14_7">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl14_7" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn14_7" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx14_8" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx14_8">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl14_8" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn14_8" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx14_9" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx14_9">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl14_9" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn14_9" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx14_10" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx14_10">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl14_10" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn14_10" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx14_11" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx14_11">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl14_11" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn14_11" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx14_12" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx14_12">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl14_12" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn14_12" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_15">
                                                                <asp:Label ID="LblVertCadPlm" runat="server" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_15" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="md-checkbox-list">
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx15_1" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx15_1">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl15_1" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn15_1" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx15_2" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx15_2">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl15_2" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn15_2" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx15_3" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx15_3">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl15_3" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn15_3" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx15_4" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx15_4">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl15_4" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn15_4" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx15_5" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx15_5">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl15_5" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn15_5" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx15_6" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx15_6">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl15_6" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn15_6" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx15_7" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx15_7">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl15_7" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn15_7" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx15_8" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx15_8">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl15_8" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn15_8" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="panel panel-default">
                                                    <div class="panel-heading">
                                                        <h4 class="panel-title">
                                                            <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion2" href="#collapse_2_16">
                                                                <asp:Label ID="LblVertHrdware" runat="server" /></a>
                                                        </h4>
                                                    </div>
                                                    <div id="collapse_2_16" class="panel-collapse collapse">
                                                        <div class="panel-body">
                                                            <div class="col-md-12">
                                                                <div class="md-checkbox-list">
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx16_1" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx16_1">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl16_1" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn16_1" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx16_2" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx16_2">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl16_2" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn16_2" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx16_3" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx16_3">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl16_3" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn16_3" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx16_4" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx16_4">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl16_4" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn16_4" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx16_5" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx16_5">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl16_5" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn16_5" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx16_6" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx16_6">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl16_6" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn16_6" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx16_7" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx16_7">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl16_7" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn16_7" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx16_8" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx16_8">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl16_8" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn16_8" runat="server" />
                                                                    </div>
                                                                    <div class="md-checkbox col-md-4">
                                                                        <input type="checkbox" id="Cbx16_9" onclick="UpdateVerticals()" class="md-check" />
                                                                        <label for="Cbx16_9">
                                                                            <span></span>
                                                                            <span class="check"></span>
                                                                            <span class="box"></span>
                                                                            <span style="margin-left: 30px; margin-top: -10px;">
                                                                                <asp:Label ID="Lbl16_9" runat="server" /></span>
                                                                        </label>
                                                                        <asp:HiddenField ID="Hdn16_9" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                                <ContentTemplate>
                                                    <div class="margin-top-10">
                                                        <asp:Button ID="BtnSaveVerticals" OnClick="BtnSubmitVerticals_OnClick" CssClass="btn green" runat="server" />
                                                        <asp:Button ID="BtnCancelVerticals" OnClick="BtnCancelSubmitVerticals_OnClick" CssClass="btn grey-salsa btn-outline" runat="server" />
                                                    </div>
                                                    <div id="divVerticalsSuccess" runat="server" visible="false" class="alert alert-success" style="margin-top: 30px;">
                                                        <strong>
                                                            <asp:Label ID="LblVerticalSuccess" runat="server" /></strong><asp:Label ID="LblVerticalSuccessContent" runat="server" />
                                                    </div>
                                                    <div id="divVerticalFailure" runat="server" visible="false" class="alert alert-danger" style="margin-top: 30px;">
                                                        <strong>
                                                            <asp:Label ID="LblVerticalFailure" runat="server" /></strong><asp:Label ID="LblVerticalFailureContent" runat="server" />
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div id="tab_11-11" class="tab-pane">
                                    <div class="portlet box purple">
                                        <div class="portlet-title">
                                            <div class="caption">
                                                <i class="fa fa-qrcode"></i>
                                                <asp:Label ID="LblCompanyProductsSelection" Text="Industry Products Selection" runat="server" />
                                            </div>
                                            <div class="tools">
                                                <a class="collapse"></a>
                                                <a class="reload"></a>
                                                <a class="remove"></a>
                                            </div>
                                        </div>
                                        <div class="portlet-body">
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel21">
                                                <ContentTemplate>
                                                    <div class="row">
                                                        <div class="col-md-5">
                                                            <telerik:RadAutoCompleteBox RenderMode="Lightweight" runat="server" ID="RcbxIntegrations" Delimiter=" "
                                                                Width="345" DataSourceID="AllIntegrations" AllowCustomEntry="true" DataTextField="description" DataValueField="id"
                                                                Filter="StartsWith" TextSettings-SelectionMode="Single" InputType="Text" DropDownHeight="400"
                                                                DropDownWidth="400" EmptyMessage="Start typing the name of the integrations you use">
                                                                <DropDownItemTemplate>
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td align="left" style="width: 400px; padding-left: 10px;">
                                                                                <%# DataBinder.Eval(Container.DataItem, "description")%>                                                               
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DropDownItemTemplate>
                                                            </telerik:RadAutoCompleteBox>
                                                            <asp:SqlDataSource ID="AllIntegrations" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                                ProviderName="System.Data.SqlClient" SelectCommand="SELECT [id], [description] FROM [Elio_registration_integrations] WHERE [is_public] = 1 order by description"></asp:SqlDataSource>
                                                            <telerik:RadAutoCompleteBox RenderMode="Lightweight" runat="server" ID="RcbxProducts" Delimiter=" "
                                                                Width="345" DataSourceID="AllProducts" AllowCustomEntry="true" DataTextField="description" DataValueField="id"
                                                                Filter="StartsWith" TextSettings-SelectionMode="Single" InputType="Text" DropDownHeight="400"
                                                                DropDownWidth="400" EmptyMessage="Start typing the name of the products you use">
                                                                <DropDownItemTemplate>
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td align="left" style="width: 400px; padding-left: 10px;">
                                                                                <%# DataBinder.Eval(Container.DataItem, "description")%>                                                               
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </DropDownItemTemplate>
                                                            </telerik:RadAutoCompleteBox>
                                                            <asp:SqlDataSource ID="AllProducts" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                                ProviderName="System.Data.SqlClient" SelectCommand="SELECT [id], [description] FROM [Elio_registration_products] WHERE [is_public] = 1 order by description"></asp:SqlDataSource>
                                                        </div>
                                                        <div class="col-md-1" style="padding-top: 5px;">
                                                            <asp:ImageButton ID="ImgBtnAdd" OnClick="ImgBtnAdd_Click" runat="server" ImageUrl="~/images/icons/add_btn_1.png" />
                                                        </div>
                                                        <div class="col-md-6" style="padding-top: 5px;">
                                                            <asp:CheckBoxList ID="CbxUserProductsIntegrationsList" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="margin-top-10" style="padding: 15px;">
                                                            <asp:Button ID="BtnSubmitProducts" OnClick="BtnSubmitProducts_OnClick" CssClass="btn green" runat="server" />
                                                            <asp:Button ID="BtnCancelSubmitProducts" OnClick="BtnCancelSubmitProducts_OnClick" CssClass="btn grey-salsa btn-outline" runat="server" />
                                                        </div>
                                                        <div id="divProductsSuccess" runat="server" visible="false" class="alert alert-success" style="margin: 15px;">
                                                            <strong>
                                                                <asp:Label ID="LblProductsSuccess" runat="server" /></strong><asp:Label ID="LblProductsSuccessContent" runat="server" />
                                                        </div>
                                                        <div id="divProductsFailure" runat="server" visible="false" class="alert alert-danger" style="margin: 15px;">
                                                            <strong>
                                                                <asp:Label ID="LblProductsFailure" runat="server" /></strong><asp:Label ID="LblProductsFailureContent" runat="server" />
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div id="tab_7-7" class="tab-pane">
                                    <div role="form">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel16">
                                            <ContentTemplate>
                                                <div class="form-group">
                                                    <asp:Label ID="LblPersonLastName" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxPersonLastName" CssClass="form-control" MaxLength="100" runat="server" />
                                                    <div id="divPersonLastNameError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblPersonLastNameError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="LblPersonFirstName" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxPersonFirstName" CssClass="form-control" MaxLength="100" runat="server" />
                                                    <div id="divPersonFirstNameError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblPersonFirstNameError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="LblPersonPhone" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxPersonPhone" CssClass="form-control" MaxLength="50" runat="server" />
                                                    <div id="divPersonPhoneError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblPersonPhoneError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="LblPersonLocation" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxPersonLocation" CssClass="form-control" MaxLength="150" runat="server" />
                                                    <div id="divPersonLocationError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblPersonLocationError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="LblPersonTimeZone" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxPersonTimeZone" CssClass="form-control" MaxLength="50" runat="server" />
                                                    <div id="divPersonTimeZoneError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblPersonTimeZoneError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="LblPersonTitle" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxPersonTitle" CssClass="form-control" MaxLength="350" runat="server" />
                                                    <div id="divPersonTitleError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblPersonTitleError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="LblPersonRole" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxPersonRole" CssClass="form-control" MaxLength="50" runat="server" />
                                                    <div id="divPersonRoleError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblPersonRoleError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="LblPersonSeniority" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxPersonSeniority" CssClass="form-control" MaxLength="350" runat="server" />
                                                    <div id="divPersonSeniorityError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblPersonSeniorityError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="LblPersonTwitterHandle" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxPersonTwitterHandle" CssClass="form-control" MaxLength="150" runat="server" />
                                                    <div id="divPersonTwitterHandleError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblPersonTwitterHandleError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="LblPersonAboutMeHandle" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxPersonAboutMeHandle" CssClass="form-control" MaxLength="150" runat="server" />
                                                    <div id="divPersonAboutMeHandleError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblPersonAboutMeHandleError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="divPersonAvatar" runat="server" class="form-group">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel17">
                                                        <ContentTemplate>
                                                            <asp:Panel ID="Panel1" Visible="true" runat="server">
                                                                <asp:Label ID="LblPersonAvatarHeader" ForeColor="#9A12B3" Font-Bold="true" runat="server" />
                                                                <br />
                                                                <div class="form-group" style="margin-top: 10px;">
                                                                    <div class="fileinput fileinput-new" data-provides="fileinput">
                                                                        <div class="fileinput-new thumbnail" style="width: 200px; height: 150px;">
                                                                            <asp:Image ID="ImgPersonAvatarBckgrd" ImageUrl="https://www.placehold.it/200x150/EFEFEF/AAAAAA&amp;text=no+image" AlternateText="Update logo" runat="server" />
                                                                        </div>
                                                                        <div class="fileinput-preview fileinput-exists thumbnail" style="max-width: 200px; max-height: 150px;"></div>
                                                                        <div>
                                                                            <span class="btn default btn-file">
                                                                                <span class="fileinput-new">
                                                                                    <asp:Label ID="Label24" Text="Select image" runat="server" /></span>
                                                                                <span class="fileinput-exists">
                                                                                    <asp:Label ID="Label25" Text="Change" runat="server" /></span>
                                                                                <input type="file" enableviewstate="true" name="ImgPersonAvatarLogo" id="avatarInput" title="Select image" runat="server" accept=".png, .jpg, .jpeg" />
                                                                            </span>
                                                                            <a class="btn default fileinput-exists" data-dismiss="fileinput">
                                                                                <asp:Label ID="LblPersonAvatarUpload" Text="Remove" runat="server" /></a>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div id="divPersonAvatarSuccess" runat="server" visible="false" class="alert alert-success">
                                                                    <strong>
                                                                        <asp:Label ID="LblPersonAvatarSuccess" runat="server" /></strong><asp:Label ID="LblPersonAvatarSuccessMsg" runat="server" />
                                                                </div>
                                                                <div id="divPersonAvatarError" runat="server" visible="false" class="alert alert-danger">
                                                                    <strong>
                                                                        <asp:Label ID="LblPersonAvatarError" runat="server" /></strong><asp:Label ID="LblPersonAvatarErrorMsg" runat="server" />
                                                                </div>
                                                                <div class="margin-top-10">
                                                                    <asp:Button ID="BtnAvatarUpload" OnClick="BtnAvatarUpload_Click" CssClass="btn green" runat="server" />
                                                                </div>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="BtnAvatarUpload" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="LblPersonBio" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxPersonBio" TextMode="MultiLine" Rows="10" MaxLength="2000" CssClass="form-control" runat="server" />
                                                    <div id="divPersonBioError" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblPersonBioError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="divPersonSuccessMessage" runat="server" visible="false" class="alert alert-success">
                                                    <strong>
                                                        <asp:Label ID="LblPersonSuccessTitle" runat="server" /></strong><asp:Label ID="LblPersonSuccessMsg" runat="server" />
                                                </div>
                                                <div id="divPersonWarningMessage" runat="server" visible="false" class="alert alert-danger">
                                                    <strong>
                                                        <asp:Label ID="LblPersonWarningTitle" runat="server" /></strong><asp:Label ID="LblPersonWarningMsg" runat="server" />
                                                </div>
                                                <div class="margiv-top-10">
                                                    <asp:Button ID="BtnPersonSaveGeneral" OnClick="BtnPersonSaveGeneral_Click" CssClass="btn green" runat="server" />
                                                    <asp:Button ID="BtnPersonCancelGeneral" OnClick="BtnPersonCancelGeneral_Click" CssClass="btn default" runat="server" />
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--end col-md-9-->
                    </div>
                </div>
                <!--tab_1_4-->
                <div class="tab-pane" id="tab_1_4">
                    <div class="row profile-account">
                        <div class="col-md-3">
                            <ul class="ver-inline-menu tabbable margin-bottom-10">
                                <li class="active">
                                    <a data-toggle="tab" href="#tab_2-2">
                                        <i class="fa fa-keyboard-o"></i>
                                        <asp:Label ID="LblElioBillingDetails" runat="server" />
                                    </a>
                                    <span class="after"></span>
                                </li>
                                <li id="li1" runat="server">
                                    <a data-toggle="tab" href="#tab_6-6">
                                        <i class="fa fa-pencil-square-o"></i>
                                        <asp:Label ID="LblStripeCreditCardDetails" runat="server" />
                                    </a>
                                    <span class="after"></span>
                                </li>
                            </ul>
                        </div>
                        <div class="col-md-9">
                            <div class="tab-content">
                                <div id="tab_2-2" class="tab-pane active">
                                    <div role="form">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel12">
                                            <ContentTemplate>
                                                <div class="form-group">
                                                    <asp:Label ID="LblBillingCompanyName" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxBillingCompanyName" CssClass="form-control" ReadOnly="true" MaxLength="45" runat="server" />
                                                    <div id="divBillingCompanyName" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblBillingCompanyNameError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="LblBillingCompanyAddress" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxBillingCompanyAddress" CssClass="form-control" MaxLength="45" runat="server" />
                                                    <div id="divBillingCompanyAddress" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblBillingCompanyAddressError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="LblBillingCompanyPostCode" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxBillingCompanyPostCode" CssClass="form-control" MaxLength="10" runat="server" />
                                                    <div id="divBillingCompanyPostCode" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblBillingCompanyPostCodeError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="LblBillingCompanyVatNumber" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxBillingCompanyVatNumber" CssClass="form-control" MaxLength="45" runat="server" />
                                                    <div id="divBillingCompanyVatNumber" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblBillingCompanyVatNumberError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>

                                                <%--<div class="form-group">
                                                    <asp:Label ID="LblCompanyPostCode" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxCompanyPostCode" CssClass="form-control" MaxLength="10" runat="server" />
                                                    <div id="divCompanyPostCode" runat="server" visible="false" class="form-group has-error" style="margin-bottom:30px;">
                                                        <asp:Label ID="LblCompanyPostCodeError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>                                            
                                                </div>
                                                <div id="div7" runat="server" class="form-group">
                                                    <asp:Label ID="LblUserVatNumber" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxUserVatNumber" CssClass="form-control" MaxLength="20" runat="server" />
                                                    <div id="divUserVatNumber" runat="server" visible="false" class="form-group has-error" style="margin-bottom:30px;">
                                                        <asp:Label ID="LblUserVatNumberError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>                                            
                                                </div>                                                
                                                <div id="div10" runat="server" class="form-group">
                                                    <asp:Label ID="LblUserIdNumber" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxUserIdNumber" CssClass="form-control" MaxLength="20" runat="server" />
                                                    <div id="divUserIdNumber" runat="server" visible="false" class="form-group has-error" style="margin-bottom:30px;">
                                                        <asp:Label ID="LblUserIdNumberError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>                                            
                                                </div>   
                                                <div id="div16" runat="server" class="form-group">
                                                    <asp:Label ID="LblUserBillingEmail" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxUserBillingEmail" CssClass="form-control" MaxLength="100" runat="server" />
                                                    <div id="divUserBillingEmailFailure" runat="server" visible="false" class="form-group has-error" style="margin-bottom:30px;">
                                                        <asp:Label ID="LblUserBillingEmailError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>                                            
                                                </div>                                            
                                                
                                                
                                                <div id="div1" runat="server" class="form-group">
                                                    <asp:Label ID="LblBillingAddress" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxBillingAddress" CssClass="form-control" MaxLength="45" runat="server" />
                                                    <div id="divBillingAddress" runat="server" visible="false" class="form-group has-error" style="margin-bottom:30px;">
                                                        <asp:Label ID="LblBillingAddressError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>                                            
                                                </div>--%>
                                                <div role="form">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel13">
                                                        <ContentTemplate>
                                                            <div class="margin-top-10">
                                                                <asp:Button ID="BtnSaveBillingDetails" OnClick="BtnSaveBillingDetails_OnClick" CssClass="btn green" runat="server" />
                                                            </div>
                                                            <div id="divSaveSuccess" runat="server" visible="false" class="alert alert-success" style="margin-top: 30px;">
                                                                <strong>
                                                                    <asp:Label ID="Label1" Text="Done! " runat="server" /></strong><asp:Label ID="LblSaveBillingSuccess" runat="server" />
                                                            </div>
                                                            <div id="divSaveFailure" runat="server" visible="false" class="alert alert-danger" style="margin-top: 30px;">
                                                                <strong>
                                                                    <asp:Label ID="LblSaveBillingfailureError" Text="Error! " runat="server" /></strong><asp:Label ID="LblSaveBillingfailure" runat="server" />
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div id="tab_6-6" class="tab-pane">
                                    <div role="form">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel14">
                                            <ContentTemplate>
                                                <controls:MessageControl ID="UcMessageControl" Visible="false" runat="server" />
                                                <div id="div2" visible="false" runat="server" class="form-group">
                                                    <asp:Label ID="LblFullName" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxFullName" CssClass="form-control" MaxLength="45" runat="server" />
                                                    <div id="divFullName" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblFullNameError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="div31" visible="false" runat="server" class="form-group">
                                                    <asp:Label ID="LblAddress1" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxAddress1" CssClass="form-control" MaxLength="45" runat="server" />
                                                    <div id="divAddress1" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblAddress1Error" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="div41" visible="false" runat="server" class="form-group">
                                                    <asp:Label ID="LblAddress2" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxAddress2" CssClass="form-control" MaxLength="45" runat="server" />
                                                    <div id="divAddress2" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblAddress2Error" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="div8" visible="false" runat="server" class="form-group">
                                                    <asp:Label ID="LblOrigin" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxOrigin" CssClass="form-control" MaxLength="20" runat="server" />
                                                    <div id="divOrigin" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblOriginError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="div11" visible="false" runat="server" class="form-group">
                                                    <asp:Label ID="LblCardType" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxCardType" ReadOnly="true" CssClass="form-control" MaxLength="20" runat="server" />
                                                    <div id="divCardType" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblCardTypeError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="div5" runat="server" class="form-group">
                                                    <asp:Label ID="LblCCNumber" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxCCNumber" CssClass="form-control" MaxLength="20" runat="server" />
                                                    <div id="divCCNumber" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblCCNumberError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="div4" runat="server" class="form-group">
                                                    <asp:Label ID="LblCvcNumber" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxCvcNumber" CssClass="form-control" MaxLength="3" runat="server" />
                                                    <div id="divCvcNumber" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblCvcNumberError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="div15" runat="server" class="form-group">
                                                    <asp:Label ID="LblExpMonth" CssClass="control-label" runat="server" />
                                                    <%--<asp:TextBox ID="TbxExpMonth" CssClass="form-control" MaxLength="2" placeholder="MM" onkeypress="return isNumberOnly(event);" runat="server" />--%>
                                                    <asp:DropDownList ID="DrpExpMonth" CssClass="form-control" placeholder="MM" data-placement="top" data-trigger="manual" runat="server">
                                                        <asp:ListItem Text="MM" Selected="True" Value="0" />
                                                        <asp:ListItem Text="01" Value="1" />
                                                        <asp:ListItem Text="02" Value="2" />
                                                        <asp:ListItem Text="03" Value="3" />
                                                        <asp:ListItem Text="04" Value="4" />
                                                        <asp:ListItem Text="05" Value="5" />
                                                        <asp:ListItem Text="06" Value="6" />
                                                        <asp:ListItem Text="07" Value="7" />
                                                        <asp:ListItem Text="08" Value="8" />
                                                        <asp:ListItem Text="09" Value="9" />
                                                        <asp:ListItem Text="10" Value="10" />
                                                        <asp:ListItem Text="11" Value="11" />
                                                        <asp:ListItem Text="12" Value="12" />
                                                    </asp:DropDownList>
                                                    <div id="divExpMonth" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblExpMonthError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="div18" runat="server" class="form-group">
                                                    <asp:Label ID="LblExpYear" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxExpYear" CssClass="form-control" MaxLength="2" placeholder="YY" onkeypress="return isNumberOnly(event);" runat="server" />
                                                    <div id="divExpYear" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblExpYearError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div id="div20" visible="false" runat="server" class="form-group">
                                                    <asp:Label ID="LblZipCode" CssClass="control-label" runat="server" />
                                                    <asp:TextBox ID="TbxZipCode" CssClass="form-control" MaxLength="45" runat="server" />
                                                    <div id="divZipCode" runat="server" visible="false" class="form-group has-error" style="margin-bottom: 30px;">
                                                        <asp:Label ID="LblZipCodeError" CssClass="control-label col-md-12" runat="server" />
                                                    </div>
                                                </div>
                                                <div role="form">
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel15">
                                                        <ContentTemplate>
                                                            <div class="margin-top-10">
                                                                <asp:Button ID="BtnSaveCreditCardDetails" Visible="false" OnClick="BtnSaveCreditCardDetails_OnClick" CssClass="btn green" runat="server" />

                                                                <asp:Button ID="BtnAddNewCard" OnClick="BtnAddNewCard_OnClick" CssClass="btn green" runat="server" />
                                                                <asp:Button ID="BtnCancelAddNewCard" OnClick="BtnCancelAddNewCard_OnClick" CssClass="btn purple" runat="server" />
                                                            </div>
                                                            <div id="divSaveCreditCardSuccess" runat="server" visible="false" class="alert alert-success" style="margin-top: 30px;">
                                                                <strong>
                                                                    <asp:Label ID="LblSaveCreditCardSuccessDone" Text="Done! " runat="server" /></strong><asp:Label ID="LblSaveCreditCardSuccess" runat="server" />
                                                            </div>
                                                            <div id="divSaveCreditCardFailure" runat="server" visible="false" class="alert alert-danger" style="margin-top: 30px;">
                                                                <strong>
                                                                    <asp:Label ID="LblSaveCreditCardFailureError" Text="Error! " runat="server" /></strong><asp:Label ID="LblSaveCreditCardFailure" runat="server" />
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--end col-md-9-->
                    </div>
                </div>
                <!--tab_1_5-->
                <div class="tab-pane" id="tab_1_5">
                    <div class="row profile-account">
                        <div class="col-md-3">
                            <ul class="ver-inline-menu tabbable margin-bottom-10">
                                <li class="active">
                                    <a data-toggle="tab" href="#tab_8-8">
                                        <i class="fa fa-keyboard-o"></i>
                                        <asp:Label ID="LblDeleteAccount" runat="server" />
                                    </a>
                                    <span class="after"></span>
                                </li>
                                <li id="liDownloadCompanyData" runat="server">
                                    <a data-toggle="tab" href="#tab_9-9">
                                        <i class="fa fa-pencil-square-o"></i>
                                        <asp:Label ID="LblDownloadCompanyData" runat="server" />
                                    </a>
                                    <span class="after"></span>
                                </li>
                                <li id="liEmailNotificationSettings" runat="server">
                                    <a data-toggle="tab" href="#tab_10-10">
                                        <i class="fa fa-pencil-square-o"></i>
                                        <asp:Label ID="LblEmailNotificationSettings" runat="server" />
                                    </a>
                                    <span class="after"></span>
                                </li>
                            </ul>
                        </div>
                        <div class="col-md-9">
                            <div class="tab-content">
                                <div id="tab_8-8" class="tab-pane active">
                                    <div role="form">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel18">
                                            <ContentTemplate>
                                                <div class="form-group">
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div id="divCompleteRegistrationNotifications" class="note note-info" runat="server">
                                                                <div class="margin-10">
                                                                    <asp:Button ID="BtnDeleteAccountData" OnClick="BtnDeleteAccountData_Click" CssClass="btn red" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <controls:MessageControl ID="UcMessageAlert" Visible="false" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div id="tab_9-9" class="tab-pane">
                                    <div role="form">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel20">
                                            <ContentTemplate>
                                                <controls:MessageControl ID="MessageControlDownloadData" Visible="false" runat="server" />
                                                <div id="div6" runat="server" class="form-group">
                                                    <div class="col-md-12">
                                                        <div id="div9" class="note note-success" runat="server">
                                                            <div class="margin-10">
                                                                <asp:Button ID="BtnExport" OnClick="BtnExport_Click" Text="Download my account data" CssClass="btn red" runat="server" />
                                                                <asp:ImageButton ID="ImgBtnExport" Visible="false" Width="50" Height="50" OnClick="ImgBtnExport_Click" ToolTip="Export my account data to .csv file" ImageUrl="~/images/icons/csv_export_3.png" runat="server" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div id="tab_10-10" class="tab-pane">
                                    <div role="form">

                                        <asp:Panel ID="PnlEmailNotifications" CssClass="multy-panel-css" Style="min-height: 300px;" runat="server">
                                            <div class="profile-settings-row-notif">
                                                <div class="profile-settings-row-small">
                                                    <h2>
                                                        <asp:Label ID="LblENotifications" Visible="false" runat="server" Text="Email Notifications" />
                                                    </h2>
                                                </div>
                                                <div class="form-group">
                                                    <div class="prof-set-chkbx">
                                                        <div class="dash-bar-row" style="width: 565px;">
                                                            <asp:CheckBoxList ID="CbxEmailNotifications" CssClass="com-cbx" TextAlign="left" runat="server">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="prof-set-chkbx">
                                                        <div class="col-md-12">
                                                            <asp:Label ID="Lbl1" CssClass="control-label col-md-9" runat="server" Text="Email me when someone adds a review" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox1" value="1" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                            <asp:Label ID="Lbl2" CssClass="control-label col-md-9" runat="server" Text="Email me when i have new inbox email" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox2" value="2" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                            <asp:Label ID="Lbl3" CssClass="control-label col-md-9" runat="server" Text="Email me when i have new lead" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox3" value="4" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                            <asp:Label ID="Lbl4" CssClass="control-label col-md-9" runat="server" Text="Email me for completing my registration" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox4" value="5" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                            <asp:Label ID="Lbl5" CssClass="control-label col-md-9" runat="server" Text="Email me when a company adds me as his client" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox5" value="6" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                            <asp:Label ID="Lbl6" CssClass="control-label col-md-9" runat="server" Text="Email me when someone invites me to team" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox6" value="19" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                            <asp:Label ID="Lbl7" CssClass="control-label col-md-9" runat="server" Text="Email me when someone invites me to his collaboration tool" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox7" value="24" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                            <asp:Label ID="Lbl8" CssClass="control-label col-md-9" runat="server" Text="Email me when someone accept my invitation to my collaboration tool" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox8" value="26" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                            <asp:Label ID="Lbl9" CssClass="control-label col-md-9" runat="server" Text="Email me to complete my registration in order to accept collaboration invitation" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox9" value="27" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                            <asp:Label ID="Lbl10" CssClass="control-label col-md-9" runat="server" Text="Email me when someone request a demo" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox10" value="30" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                            <asp:Label ID="Lbl11" CssClass="control-label col-md-9" runat="server" Text="Email me when my collaboration vendor upload a file to Onboarding Library" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox11" value="31" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                            <asp:Label ID="Lbl12" CssClass="control-label col-md-9" runat="server" Text="Email me when my collaboration partner add new deal registration" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox12" value="32" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                            <asp:Label ID="Lbl13" CssClass="control-label col-md-9" runat="server" Text="Email me when my collaboration partner accept/reject new deal registration" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox13" value="33" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                            <asp:Label ID="Lbl14" CssClass="control-label col-md-9" runat="server" Text="Email me when my collaboration partner win/lost new deal registration" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox14" value="34" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                            <asp:Label ID="Lbl15" CssClass="control-label col-md-9" runat="server" Text="Email me when my collaboration vendor add a new lead distribution" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox15" value="35" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                            <asp:Label ID="Lbl16" CssClass="control-label col-md-9" runat="server" Text="Email me when my collaboration partner win/lost new lead distribution" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox16" value="36" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                            <asp:Label ID="Lbl17" CssClass="control-label col-md-9" runat="server" Text="Email me when my collaboration partner add new partner to partner deal" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox17" value="37" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                            <asp:Label ID="Lbl19" CssClass="control-label col-md-9" runat="server" Text="Email me when someone reject/deny invitation to my PRM account" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox19" value="39" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                            <asp:Label ID="Lbl20" CssClass="control-label col-md-9" runat="server" Text="Email me when a partner Open/Close deal that has provided to me" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox20" value="40" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                            <asp:Label ID="Lbl21" CssClass="control-label col-md-9" runat="server" Text="Email me when someone register from partner portal to my PRM account" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox21" value="41" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                            <asp:Label ID="Lbl22" CssClass="control-label col-md-9" runat="server" Text="Email me when my collaboration vendor deletes me from his PRM account" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox22" value="42" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                            <asp:Label ID="Lbl23" CssClass="control-label col-md-9" runat="server" Text="Email me when my collaboration vendor upload a file to Collaboration Library" />
                                                            <div class="col-md-3">
                                                                <input id="Checkbox23" value="43" runat="server" type="checkbox" class="make-switch" data-on-color="success" data-off-color="danger">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel22">
                                                    <ContentTemplate>
                                                        <div class="prof-set-btn">

                                                            <telerik:RadButton ID="RbtnSaveNotifications" OnClick="RbtnSaveNotifications_Click" CssClass="btn green" Style="margin-top: 20px;" runat="server">
                                                                <ContentTemplate>
                                                                    <span>
                                                                        <asp:Label ID="LblSaveNotificationsText" runat="server" />
                                                                    </span>
                                                                </ContentTemplate>
                                                            </telerik:RadButton>

                                                        </div>
                                                        <controls:MessageControl ID="MessageControlEmailNotifications" Visible="false" runat="server" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>

                                        </asp:Panel>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--end col-md-9-->
                    </div>
                </div>
                <!--end tab-pane-->
            </div>
        </div>
    </div>

    <!-- Payment form (modal view) -->
    <div id="PaymentModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel11">
            <ContentTemplate>
                <controls:UcStripe ID="UcStripe" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <!-- Add emails form (modal view) -->
    <div id="AddMoreEmailsModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" ID="UpdatePanel10" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-dialog" id="wndMoreEmails" style="width: 300px;">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div style="height: 180px;">
                                <div id="divInfo" runat="server" class="alert alert-info" style="text-align: center;">
                                    <strong>
                                        <asp:Label ID="LblMessage" Text="Add more emails for your partners" runat="server" /></strong>
                                </div>
                                <form class="form-horizontal col-sm-12">
                                    <asp:Panel ID="PnlMoreEmails" runat="server">
                                        <div class="form-group" style="margin-top: 0px; padding: 10px;">
                                            <div style="margin-bottom: 10px;"></div>
                                            <asp:TextBox ID="TbxMoreEmails" CssClass="form-control" Width="250" placeholder="Email" data-placement="top" data-trigger="manual" runat="server" />
                                        </div>
                                    </asp:Panel>
                                </form>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="BtnCancel" OnClick="BtnCancel_OnClick" Text="Cancel" CssClass="btn btn-info" aria-hidden="true" runat="server" />
                            &nbsp;
                            <asp:Button ID="BtnAddEmail" OnClick="BtnAddEmail_OnClick" Text="Add" CssClass="btn btn-success" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <div id="divWarningMsg" runat="server" visible="false" class="alert alert-danger">
                                <strong>
                                    <asp:Label ID="LblWarningMsg" runat="server" />
                                </strong>
                                <asp:Label ID="LblWarningMsgContent" runat="server" />
                            </div>
                            <div id="divSuccessMsg" runat="server" visible="false" class="alert alert-success">
                                <strong>
                                    <asp:Label ID="LblSuccessMsg" runat="server" />
                                </strong>
                                <asp:Label ID="LblSuccessMsgContent" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <!-- Delete form (modal view) -->
    <div id="divConfirmDeletion" class="modal fade" tabindex="-1" data-width="300">
        <asp:UpdatePanel runat="server" ID="UpdatePanel19">
            <ContentTemplate>
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                            <h4 class="modal-title">
                                <asp:Label ID="LblConfTitle" CssClass="control-label" runat="server" /></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:Label ID="LblConfMsg" CssClass="control-label" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="BtnBack" OnClick="BtnBack_Click" CssClass="btn dark btn-outline" runat="server" />
                            <asp:Button ID="BtnDeleteConfirm" OnClick="BtnDeleteConfirm_Click" CssClass="btn red-sunglo" runat="server" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
