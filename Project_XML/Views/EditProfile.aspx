<%@ Page Title="Edit Profile" Language="C#" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" MasterPageFile="~/Site.Master" Inherits="Project_XML.Views.EditProfile" %>
<%@ Register TagPrefix="uc" TagName="UserMenu" Src="./UserControls/UserMenu.ascx" %>

<asp:Content ID="Edit_Profile_Content" ContentPlaceHolderID="Master_Page" runat="server">
    <uc:UserMenu runat="server" ID="UserMenu1" />

    <div class="col-sm-10 col-sm-offset-3 col-md-10 col-md-offset-2 main">
        <h1 class="page-header">Edit Profile</h1>
        <div class="row">
            <div class="col-sm-7 col-md-8">
                <asp:Panel runat="server" ID="editUserPanel" Visible="false">
                    <asp:Label runat="server" ID="editUserIcon"></asp:Label>
                    <asp:Literal runat="server" ID="editUserMsg" />
                </asp:Panel>
                <div class="modal-body form-horizontal">
                    <!-- username label -->
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="usernameEdit">Username</label>
                        <label class="col-sm-10 username_edit">
                            <asp:Label runat="server" ID="usernameEdit"></asp:Label></label>
                    </div>
                    <!-- / usermame label -->
                    <!-- fname field -->
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="userValidation_edit" ControlToValidate="fnameEdit" ErrorMessage="This field is required." ForeColor="Red" Font-Italic="true" Display="Dynamic" CssClass="col-sm-offset-3" />
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="fnameEdit">Firstname</label>
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" CssClass="form-control" ID="fnameEdit" placeholder="Enter firstname" EnableViewState="false"></asp:TextBox>
                        </div>
                    </div>
                    <!-- / fname field -->
                    <!-- lname field -->
                    <asp:RequiredFieldValidator runat="server" ValidationGroup="userValidation_edit" ControlToValidate="lnameEdit" ErrorMessage="This field is required." ForeColor="Red" Font-Italic="true" Display="Dynamic" CssClass="col-sm-offset-3" />
                    <div class="form-group">
                        <label class="control-label col-sm-2" for="lnameEdit">Lastname</label>
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" CssClass="form-control" ID="lnameEdit" placeholder="Enter lastname" EnableViewState="false"></asp:TextBox>
                        </div>
                    </div>
                    <!-- / lname field -->
                    <!-- Change password panel -->
                    <div class="panel panel-default">
                        <div class="panel-heading">Change Password</div>
                        <div class="panel-body">

                            <!-- old password -->
                            <asp:CustomValidator runat="server" ID="oldpass" ValidationGroup="userValidation_edit" ControlToValidate="oldPwordEdit" ErrorMessage="Password is incorrect." ForeColor="Red" Font-Italic="true" Display="Dynamic" CssClass="col-sm-offset-3" OnServerValidate="Validate_Password" />
                            <div class="form-group">
                                <label class="control-label col-sm-3" for="oldPwordEdit">Old Password</label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="oldPwordEdit" TextMode="Password" placeholder="Enter password" EnableViewState="false"></asp:TextBox>
                                </div>
                            </div>
                            <!-- / old password -->
                            <!-- new password field -->
                            <asp:RegularExpressionValidator runat="server" ValidationGroup="userValidation_edit" Display="Dynamic" Font-Italic="true" CssClass="col-sm-offset-3" ControlToValidate="pwordEdit1" ErrorMessage="Password must be at least 8 characters in length." ValidationExpression="^(?:.{8,}|)$" ForeColor="red" />
                            <div class="form-group">
                                <label class="control-label col-sm-3" for="pwordEdit1">New Password</label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="pwordEdit1" TextMode="Password" placeholder="Enter password" EnableViewState="false"></asp:TextBox>
                                </div>
                            </div>
                            <!-- / new password field -->
                            <!-- confirm password field -->
                            <asp:CompareValidator runat="server" ValidationGroup="userValidation_edit" ControlToValidate="pwordEdit2" ControlToCompare="pwordEdit1" ErrorMessage="Entered password does not match." ForeColor="Red" Font-Italic="true" CssClass="col-sm-offset-3" />
                            <div class="form-group">
                                <label class="control-label col-sm-3" for="pwordEdit2">Confirm Password</label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="pwordEdit2" TextMode="Password" placeholder="Re-enter password" EnableViewState="false"></asp:TextBox>
                                </div>
                            </div>
                            <!-- confirm password field -->
                        </div>
                    </div>
                    <!-- / Change password panel -->
                    <div class="col-sm-12 text-center">
                        <asp:Button runat="server" ID="edit_btn" Text="Update Information" CssClass="btn btn-primary btn-md" ValidationGroup="userValidation_edit" OnClick="Update_Info" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
