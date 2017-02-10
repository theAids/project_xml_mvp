<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserMenu.ascx.cs" Inherits="Project_XML.Views.UserControls.UserMenu" %>

<nav class="navbar navbar-inverse navbar-fixed-top nav-ey">
    <div class="container-fluid">
        <div class="navbar-header">
            <a class="navbar-brand" href="#"><span>Project HK</span></a>
        </div>
        <ul class="nav navbar-nav navbar-right">
            <li class="dropdown">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-user icon"></span>
                    <asp:Literal runat="server" ID="currentUser"></asp:Literal>
                    <span class="caret"></span></a>
                <ul class="dropdown-menu">
                    <li>
                        <a href="./EditProfile.aspx"><span class="glyphicon glyphicon-pencil icon"></span>Settings </a>
                    </li>
                    <li>
                        <asp:LinkButton runat="server" ID="logout_btn" OnClick="logout_btn_Click"><span class="glyphicon glyphicon-off icon"></span>Logout</asp:LinkButton>
                    </li>
                </ul>
            </li>
        </ul>
    </div>
</nav>

<!-- Side Bar -->
<div class="col-sm-3 col-md-2 sidebar">
    <asp:Image ID="BrandImg" runat="server" ImageUrl="~/Content/images/EY_logo_hr.gif" />
    <ul class="nav navbar-inverse nav-sidebar">
        <asp:PlaceHolder runat="server" ID="sideMenu" />
    </ul>
</div>
<!-- / Side Bar -->
