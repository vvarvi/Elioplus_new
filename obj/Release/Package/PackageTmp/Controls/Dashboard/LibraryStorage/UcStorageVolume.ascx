<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UcStorageVolume.ascx.cs" Inherits="WdS.ElioPlus.Controls.Dashboard.LibraryStorage.UcStorageVolume" %>

<div id="divDataVolumeTitle" visible="false" runat="server" class="sidebar-category" style="margin-bottom:5px;">
    <asp:Label ID="LblDataVolume" runat="server" />
</div>
<div id="divDataVolumeContent" visible="false" runat="server" class="sidebar-widget">
    <ul class="list-unstyled pl-15 pr-15">
        <li class="mb-20">
            <div class="block clearfix mb-10">
                <span class="pull-left">
                    <asp:Label ID="LblLibraryUsedSpace" runat="server" />
                </span>
                <span id="spanUsedSpace" runat="server" class="pull-right label label-outline label-success"><asp:Label ID="LblUsedSpace" runat="server" />
                    <asp:Image ID="ImgInfo" Visible="false" AlternateText="info" ImageUrl="/images/icons/small/info.png" style="cursor:pointer;" runat="server" />
                </span>
            </div>
            <telerik:RadToolTip ID="RttInfo" TargetControlID="ImgInfo" Animation="Fade" EnableShadow="true" runat="server" Position="TopRight" />
            <div class="progress progress-xs mb-0">
                <div id="divTransitionProgress" role="progressbar" class="progress-bar progress-bar-success" runat="server">
                </div>
            </div>
        </li>
        <li id="liLibraryFreeSpace" class="mb-20" runat="server">
            <div class="block clearfix mb-10">
                <span class="pull-left">
                    <asp:Label ID="LblLibraryFreeSpace" runat="server" />
                </span>
                <span id="spanFreeSpace" runat="server" class="pull-right label label-outline label-purple"><asp:Label ID="LblFreeSpace" runat="server" />
                    <asp:Image ID="ImgFreeInfo" Visible="false" AlternateText="free info" ImageUrl="/images/icons/small/info.png" style="cursor:pointer;" runat="server" />
                </span>
            </div>
            <telerik:RadToolTip ID="RttFreeInfo" TargetControlID="ImgFreeInfo" Animation="Fade" EnableShadow="true" runat="server" Position="TopRight" />
            <div class="progress progress-xs mb-0">
                <div id="divFreeTransitionProgress" runat="server" role="progressbar" class="progress-bar progress-bar-purple">
                </div>
            </div>
        </li>
    </ul>
</div>