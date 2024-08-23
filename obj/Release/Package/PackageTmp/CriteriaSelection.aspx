<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CriteriaSelection.aspx.cs" Inherits="WdS.ElioPlus.CriteriaSelection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" >
    <title></title>
</head>
<body>
    <form id="form1" style="background-color:#FAFAFA" runat="server">
    <telerik:RadScriptManager ID="masterRadScriptManager" runat="server">
    </telerik:RadScriptManager>        
        <div class="col-md-12" style="margin-top:5%; padding-left:10%; padding-right:10%; margin-bottom:1%">
            <div class="note note-info" style="text-align:center;">
                <h1><asp:Label ID="LblAlgTitle" Font-Bold="true" Font-Size="24px" runat="server" /></h1>                
                <asp:Label ID="LblAlgInfo" runat="server" />                
            </div>
        </div>
        <div class="col-md-12" style="padding-left:10%; padding-right:10%;">
            <asp:PlaceHolder ID="PhCriteriaSelection" runat="server" /> 
        </div>
        <div class="col-md-12" style="float:right; margin-bottom:50px; padding-left:10%; padding-right:10%;">
            <a id="aBtnSkip" runat="server" style="float:right;" class="btn btn-circle purple btn-lg">
                <asp:Label ID="LblBtnSkip" runat="server" />
            </a>
        </div>
        <script src="/assets/global/plugins/counterup/jquery.waypoints.min.js" type="text/javascript"></script>
        <script src="/assets/global/plugins/counterup/jquery.counterup.min.js" type="text/javascript"></script> 
    </form>
</body>
</html>
