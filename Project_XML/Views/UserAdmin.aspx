<%@ Page Title="User Admin" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="UserAdmin.aspx.cs" Inherits="Project_XML.Views.UserAdmin" %>

<%@ Register TagPrefix="uc" TagName="UserMenu" Src="./UserControls/UserMenu.ascx" %>

<asp:Content runat="server" ID="User_Admin_Content" ContentPlaceHolderID="Master_Page">
    <asp:HiddenField runat="server" ID="currentUser" Visible="false" />

    <uc:UserMenu runat="server" ID="UserMenu1" />

    <div class="col-sm-9 col-sm-offset-3 col-md-10 col-md-offset-2 main">
        <h1 class="page-header">Users</h1>

        <div class="row">
            <div class="col-sm-7 col-md-8">

                <ul class="list-unstyled">
                    <li><a class="btn btn-sm btn-primary addBtn" data-toggle="modal" data-target="#newUserModal" href="#"><span class="glyphicon glyphicon-plus-sign icon"></span>Add User</a></li>
                </ul>


                <!-- User Table -->
                <asp:UpdatePanel runat="server" ID="userTableUpdate" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="table-responsive">
                            <asp:Repeater runat="server" ID="userList">
                                <HeaderTemplate>
                                    <table id="usersTable" class="table table-striped table-bordered">
                                        <thead>
                                            <tr>
                                                <th>Username</th>
                                                <th>Firstname</th>
                                                <th>Lastname</th>
                                                <th>Role</th>
                                                <th class="no-sort"></th>
                                                <th class="no-sort"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td class="username"><%# Eval("username") %></td>
                                        <td class="fname"><%# Eval("firstname") %></td>
                                        <td class="lname"><%# Eval("lastname") %></td>
                                        <td class="role"><%# Eval("role")%></td>
                                        <td><a data-toggle="modal" data-target="#editUserModal" class="editUser_btn" href="#"><span class="glyphicon glyphicon-pencil"></a></td>
                                        <td>
                                            <asp:LinkButton runat="server" OnCommand="RemoveUser" CommandArgument='<%# Eval("userID")%>'><span class='glyphicon glyphicon-trash alert-danger delBtn <%# Eval("username").Equals(currentUser.Value)? "disabled":"" %>'></asp:LinkButton></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>
                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                        <!-- User Table -->
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>


    <!-- Add User Modal --->
    <div class="modal fade" id="newUserModal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content ">
                <div class="modal-header">
                    <h4 class="modal-title">New User</h4>
                </div>
                <asp:UpdatePanel runat="server" ID="addUserUpdatePanel">
                    <ContentTemplate>
                        <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="addUserUpdatePanel" DynamicLayout="true">
                            <ProgressTemplate>
                                <div class="alert alert-info user-status">Adding User...</div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:Panel runat="server" ID="addUserPanel" Visible="false">
                            <asp:Label runat="server" ID="addUsericon">
                            </asp:Label>
                            <asp:Literal runat="server" ID="addUserMsg" />
                        </asp:Panel>
                        <div class="modal-body form-horizontal">
                            <!-- username field -->
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="userValidation" ControlToValidate="usernameAdd" ErrorMessage="This field is required." ForeColor="Red" Font-Italic="true" Display="Dynamic" CssClass="col-sm-offset-3" />
                            <asp:CustomValidator runat="server" ID="usernameValidate" ValidationGroup="uservalidation" ControlToValidate="usernameAdd" ErrorMessage="Username already exists." ForeColor="Red" Font-Italic="true" Display="Dynamic" CssClass="col-sm-offset-3" OnServerValidate="UserExists" />
                            <div class="form-group required">
                                <label class="control-label col-sm-3" for="usernameAdd">Username</label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="usernameAdd" placeholder="Enter username" autofocus="true" EnableViewState="false"></asp:TextBox>
                                </div>
                            </div>
                            <!-- / username field -->
                            <!-- fname field -->
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="userValidation" ControlToValidate="fnameAdd" ErrorMessage="This field is required." ForeColor="Red" Font-Italic="true" Display="Dynamic" CssClass="col-sm-offset-3" />
                            <div class="form-group required">
                                <label class="control-label col-sm-3" for="fnameAdd">Firstname</label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="fnameAdd" placeholder="Enter firstname" EnableViewState="false"></asp:TextBox>
                                </div>
                            </div>
                            <!-- / fname field -->
                            <!-- lname field -->
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="userValidation" ControlToValidate="lnameAdd" ErrorMessage="This field is required." ForeColor="Red" Font-Italic="true" Display="Dynamic" CssClass="col-sm-offset-3" />
                            <div class="form-group required">
                                <label class="control-label col-sm-3" for="lnameAdd">Lastname</label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="lnameAdd" placeholder="Enter lastname" EnableViewState="false"></asp:TextBox>
                                </div>
                            </div>
                            <!-- / lname field -->
                            <!-- password field -->
                            <asp:RegularExpressionValidator runat="server" ValidationGroup="userValidation" Display="Dynamic" CssClass="col-sm-offset-3" ControlToValidate="pwordAdd1" ErrorMessage="Password must be at least 8 characters in length." ValidationExpression="(.+){8,50}" ForeColor="red" />
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="userValidation" ControlToValidate="pwordAdd1" ErrorMessage="This field is required." ForeColor="Red" Font-Italic="true" Display="Dynamic" CssClass="col-sm-offset-3" />
                            <div class="form-group required">
                                <label class="control-label col-sm-3" for="pwordAdd1">Password</label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="pwordAdd1" TextMode="Password" placeholder="Enter password" EnableViewState="false"></asp:TextBox>
                                </div>
                            </div>
                            <!-- / password field -->
                            <!-- confirm password field -->
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="userValidation" ControlToValidate="pwordAdd2" ErrorMessage="This field is required." ForeColor="Red" Font-Italic="true" Display="Dynamic" CssClass="col-sm-offset-3" />
                            <asp:CompareValidator runat="server" ValidationGroup="userValidation" ControlToValidate="pwordAdd2" ControlToCompare="pwordAdd1" ErrorMessage="Entered password does not match." ForeColor="Red" Font-Italic="true" CssClass="col-sm-offset-3" />
                            <div class="form-group required">
                                <label class="control-label col-sm-3" for="pwordAdd2">Confirm Password</label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="pwordAdd2" TextMode="Password" placeholder="Re-enter password" EnableViewState="false"></asp:TextBox>
                                </div>
                            </div>
                            <!-- / confirm password field -->
                            <!-- role select field -->
                            <div class="form-group required">
                                <label class="control-label col-sm-3" for="role">Role</label>
                                <div class="col-sm-9">
                                    <asp:DropDownList runat="server" ID="roleAdd" AutoPostBack="false" CssClass="form-control" EnableViewState="false">
                                        <asp:ListItem Selected="True" Value="2"> User </asp:ListItem>
                                        <asp:ListItem Value="1"> Administrator </asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                            </div>
                            <!-- / role select field -->
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="addUser_btn" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-cancel" data-dismiss="modal">Close</button>
                    <asp:Button runat="server" class="btn btn-primary btn-proceed" ID="addUser_btn" OnClick="AddUser" Text="Add User" ValidationGroup="userValidation" />
                </div>

            </div>
        </div>
    </div>
    <!-- / Add User Modal -->

    <!-- Edit User Modal -->
    <div class="modal fade" id="editUserModal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content ">
                <div class="modal-header">
                    <h4 class="modal-title">Edit User Info</h4>
                </div>

                <asp:UpdatePanel runat="server" ID="editUserUpdatePanel">
                    <ContentTemplate>
                        <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="editUserUpdatePanel" DynamicLayout="true">
                            <ProgressTemplate>
                                <div class="alert alert-info user-status">Editting user info...</div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:Panel runat="server" ID="editUserPanel" Visible="false">
                            <asp:Label runat="server" ID="editUserIcon"></asp:Label>
                            <asp:Literal runat="server" ID="editUserMsg" />
                        </asp:Panel>
                        <div class="modal-body form-horizontal">
                            <!-- username label -->
                            <div class="form-group">
                                <label class="control-label col-sm-3" for="usernameEdit">Username</label>
                                <label class="col-sm-9 username-edit">
                                    <asp:Label runat="server" ID="usernameEdit"></asp:Label></label>
                                <asp:HiddenField ID="usernameVal" runat="server" />
                            </div>
                            <!-- / usermame label -->
                            <!-- fname field -->
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="userValidation_edit" ControlToValidate="fnameEdit" ErrorMessage="This field is required." ForeColor="Red" Font-Italic="true" Display="Dynamic" CssClass="col-sm-offset-3" />
                            <div class="form-group">
                                <label class="control-label col-sm-3" for="fnameEdit">Firstname</label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="fnameEdit" placeholder="Enter firstname" EnableViewState="false"></asp:TextBox>
                                </div>
                            </div>
                            <!-- / fname field -->
                            <!-- lname field -->
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="userValidation_edit" ControlToValidate="lnameEdit" ErrorMessage="This field is required." ForeColor="Red" Font-Italic="true" Display="Dynamic" CssClass="col-sm-offset-3" />
                            <div class="form-group">
                                <label class="control-label col-sm-3" for="lnameEdit">Lastname</label>
                                <div class="col-sm-9">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="lnameEdit" placeholder="Enter lastname" EnableViewState="false"></asp:TextBox>
                                </div>
                            </div>
                            <!-- / lname field -->
                            <!-- role select field -->
                            <div class="form-group">
                                <label class="control-label col-sm-3" for="roleEdit">Role</label>
                                <div class="col-sm-9">
                                    <asp:DropDownList runat="server" C ID="roleEdit" AutoPostBack="false" CssClass="form-control" EnableViewState="false">
                                        <asp:ListItem Value="2"> User </asp:ListItem>
                                        <asp:ListItem Value="1"> Administrator </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <!-- / role select field -->
                            <!-- Change password panel -->
                            <div class="panel panel-default">
                                <div class="panel-heading">Change Password</div>
                                <div class="panel-body">
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
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="editUser_btn" />
                    </Triggers>
                </asp:UpdatePanel>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default btn-cancel" data-dismiss="modal">Close</button>
                    <asp:Button runat="server" class="btn btn-primary btn-proceed" ID="editUser_btn" OnCommand="EditUser" Text="Update Info" ValidationGroup="userValidation_edit" />
                </div>
            </div>
        </div>
    </div>
    <!-- / Edit User Modal -->
    <script type="text/javascript">

        function pageLoad() { //needed for async postbacks
            //bind table at startup
            $(document).ready(function () {
                $('#usersTable').dataTable({
                    'columnDefs': [{
                        'targets': 'no-sort',
                        'width': '20px',
                        'orderable': false
                    }],
                    'retrieve': true
                });

                $('#userAdminPanel').addClass('active');

                $('#userAdminPanel').siblings().each(function () {
                    $(this).removeClass('active');
                });
            });

            /*Re-bind datable for asyncpost
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                table = $('#usersTable').dataTable({
                    'columnDefs': [{
                        'targets': 'no-sort',
                        'width': '20px',
                        'orderable': false
                    }],
                    'retrieve': true
                });
            });*/

            // clear all fields for new user modal
            $('.addBtn').click(function () {

                //clear client-side validator
                for (i = 0; i < Page_Validators.length; i++) {
                    if (Page_Validators[i].validationGroup == "userValidation")
                        ValidatorEnable(Page_Validators[i], false);
                }

                // clear server-side validator
                $('#<%= usernameValidate.ClientID%>').hide();

                // hide add user status
                $('#<%= addUserPanel.ClientID%>').hide();

                $('#newUserModal input:text').each(function () {
                    $(this).val('');
                });
            });

            // Edit function script
            $('.editUser_btn').click(function () {

                //clear validators
                for (i = 0; i < Page_Validators.length; i++) {
                    if (Page_Validators[i].validationGroup == "userValidation") {
                        ValidatorEnable(Page_Validators[i], false);
                    }
                }

                // hide edit user status
                $('#<%= editUserPanel.ClientID%>').hide();

                $('#<%= usernameEdit.ClientID%>').text(($(this).parent().siblings('.username').text()));
                $('#<%= usernameVal.ClientID%>').val($(this).parent().siblings('.username').text());
                $('#<%= fnameEdit.ClientID%>').val($(this).parent().siblings('.fname').text());
                $('#<%= lnameEdit.ClientID%>').val($(this).parent().siblings('.lname').text());

                var role;
                switch ($(this).parent().siblings('.role').text()) {
                    case 'Administrator': role = 1; break;
                    case 'User': role = 2; break;
                }

                $('#<%= roleEdit.ClientID%>').val(role).change();

            });

            $('.delBtn').click(function () {
                if ($(this).hasClass("disabled"))
                    return false;
                else
                    return confirm("Are you sure you want to delete this user?");
            });
        }
    </script>
</asp:Content>
