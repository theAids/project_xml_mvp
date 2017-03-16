<%@ Page Title="Export" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ExportPanel.aspx.cs" Inherits="Project_XML.Views.ExportPanel" %>

<%@ Register TagPrefix="uc" TagName="UserMenu" Src="./UserControls/UserMenu.ascx" %>

<asp:Content ID="ExportPanel_Content" ContentPlaceHolderID="Master_Page" runat="server">

    <uc:UserMenu runat="server" ID="UserMenu1" />

    <asp:HiddenField runat="server" ID="accountSelected" />

    <div class="col-sm-9 col-sm-offset-3 col-md-10 col-md-offset-2 main">
        <h1 class="page-header">Export XML</h1>
        <div class="row">

            <div class="col-sm-6 col-md-7">

                <!-- aeoi report panel -->
                <div class="panel panel-default db-panel">
                    <div class="panel-heading"><span class="glyphicon glyphicon-folder-close icon"></span>AEOI Report</div>
                    <div class="panel-body">
                        <p>AEOI ID: <b>1000234</b></p>
                        <p>FI Name: <b>SGV & Co.</b></p>
                        <ul class="nav nav-tabs nav-justified">
                            <li role="presentation" class="active"><a href="#" id="new-data-link">New Data</a></li>
                            <li role="presentation"><a href="#" id="corr-data-link">Correction Data</a></li>
                            <li role="presentation"><a href="#" id="del-data-link">Deletion Data</a></li>
                        </ul>

                        <!-- New Data Panel -->
                        <div class="dataPanel new-data">
                            <div class="row">
                                <div class="col-sm-6">
                                    <!-- Contact field -->
                                    <div class="form-group">
                                        <label class="control-label" for="newContact">Contact:</label>
                                        <asp:TextBox runat="server" ID="newContact" CssClass="form-control" />
                                    </div>
                                    <!-- Return Year Field -->
                                    <div class="form-group required">
                                        <label class="control-label" for="newYear">Return Year:</label>
                                        <asp:DropDownList runat="server" ID="newYear" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <!-- Attention Note Field -->
                                    <div class="form-group">
                                        <label class="control-label" for="newAttentionNote">Attention Note:</label>
                                        <asp:TextBox runat="server" ID="newAttentionNote" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <!-- Account List field -->
                                    <div class="panel panel-default accounts-table required">
                                        <div class="panel-heading"><b>Accounts</b></div>
                                        <div class="table-responsive">
                                            <asp:Repeater runat="server" ID="accountsList">
                                                <HeaderTemplate>
                                                    <table class="table table-striped">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <input type="checkbox" id="selectAllAccountsBox" class="form-control chkbox" /></th>
                                                                <th>Account Number</th>
                                                                <th>Account Holder</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="accountCheckBox">
                                                            <input type="checkbox" name="accountCheckGroup" class="form-control chkbox" value='<%# Eval("AcctNumber") %>' /></td>
                                                        <td class="newAcctNumber"><%# Eval("AcctNumber") %></td>
                                                        <td class="newAcctHolder"><%# Eval("AcctHolder") %></td>
                                                        <td class="newAcctHolderId" style="display: none;"><%# Eval("AcctHolderId") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
                                            </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                                <div class="btn-export-panel">
                                    <asp:LinkButton runat="server" ID="newDataBtn" class="btn btn-export btn-md" OnClick="CreateNewData" OnClientClick="getCheckedAccounts()">Export XML<span class="glyphicon glyphicon-download-alt"></span></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <!-- /New Data Panel -->

                        <!-- Correction Data Panel -->
                        <div class="dataPanel correction-data">
                            <div class="row">
                                <div class="col-sm-6">
                                    <!-- Contact field -->
                                    <div class="form-group">
                                        <label class="control-label" for="corrContact">Contact:</label>
                                        <asp:TextBox runat="server" ID="corrContact" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <!-- Message Ref Id -->
                                    <div class="form-group required">
                                        <label class="control-label" for="corrMessageRefId">Message Reference Id:</label>
                                        <asp:DropDownList runat="server" ID="corrMessageRefId" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-sm-6">
                                    <!-- Attention note field -->
                                    <div class="form-group">
                                        <label class="control-label" for="corrAttentionNote">Attention Note:</label>
                                        <asp:TextBox runat="server" ID="corrAttentionNote" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <!-- File Serial Number -->
                                    <div class="form-group  required">
                                        <label class="control-label" for="corrFSN">File Serial Number:</label>
                                        <asp:TextBox runat="server" ID="corrFSN" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <!-- Doc Ref table -->
                                    <div class="panel panel-default accounts-table required">
                                        <div class="panel-heading"><b>Accounts</b></div>
                                        <div class="table-responsive">
                                            <asp:Repeater runat="server" ID="corrDocRefList">
                                                <HeaderTemplate>
                                                    <table class="table table-striped">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <input type="checkbox" id="selectAllDocsBox_corr" class="form-control chkbox" /></th>
                                                                <th>Doc Ref ID</th>
                                                                <th>Account Number</th>
                                                                <th>Account Holder</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="corrDocCheckBox">
                                                            <input type="checkbox" name="corrDocCheckGroup" class="form-control chkbox" /></td>
                                                        <td class="corrDocRefId">00001234</td>
                                                        <td class="corrDocAcctNumber">3099984</td>
                                                        <td class="corrDocAcctHolder">Adrian Perez</td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
                                            </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                                <div class="btn-export-panel">
                                    <asp:LinkButton runat="server" ID="LinkButton1" class="btn btn-export btn-md">Export XML<span class="glyphicon glyphicon-download-alt"></span></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <!-- /Correction Data Panel -->

                        <!-- Delete Data Panel -->
                        <div class="dataPanel delete-data">
                            <div class="row">
                                <div class="col-sm-6">
                                    <!-- Contact field -->
                                    <div class="form-group">
                                        <label class="control-label" for="delContact">Contact:</label>
                                        <asp:TextBox runat="server" ID="delContact" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <!-- Message Ref Id -->
                                    <div class="form-group required">
                                        <label class="control-label" for="delMessageRefId">Message Reference Id:</label>
                                        <asp:DropDownList runat="server" ID="delMessageRefId" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-sm-6">
                                    <!-- Attention note field -->
                                    <div class="form-group">
                                        <label class="control-label" for="delAttentionNote">Attention Note:</label>
                                        <asp:TextBox runat="server" ID="delAttentionNote" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <!-- File Serial Number -->
                                    <div class="form-group required">
                                        <label class="control-label" for="delFSN">File Serial Number:</label>
                                        <asp:TextBox runat="server" ID="delFSN" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <!-- Doc Ref table -->
                                    <div class="panel panel-default accounts-table required">
                                        <div class="panel-heading"><b>Accounts</b></div>
                                        <div class="table-responsive">
                                            <asp:Repeater runat="server" ID="delDocRefList">
                                                <HeaderTemplate>
                                                    <table class="table table-striped">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    <input type="checkbox" id="selectAllDocBox_del" class="form-control chkbox" /></th>
                                                                <th>Doc Ref ID</th>
                                                                <th>Account Number</th>
                                                                <th>Account Holder</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="delDocCheckBox">
                                                            <input type="checkbox" name="delDocCheckGroup" class="form-control chkbox" /></td>
                                                        <td class="delDocRefId">00001234</td>
                                                        <td class="delDocAcctNumber">3099984</td>
                                                        <td class="delDocAcctHolder">Adrian Perez</td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </tbody>
                                            </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                                <div class="btn-export-panel">
                                    <asp:LinkButton runat="server" ID="LinkButton2" class="btn btn-export btn-md">Export XML<span class="glyphicon glyphicon-download-alt"></span></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <!-- /Delete Data Panel -->
                    </div>
                </div>
            </div>
            <!-- / aeoi report panel -->

            <!-- Upload file panel -->
            <div class="col-sm-4 col-md-5">
                <div class="panel panel-default upload-panel" style ="overflow: auto">
                    <div class="panel-heading">
                        <span class="glyphicon glyphicon-inbox icon"></span>Upload File
                    </div>
                    <div class="panel-body upload-panel-body">
                        <asp:Panel runat="server" ID="UploadPanel" Visible="false">
                            <asp:Label runat="server" ID="UploadIcon">
                            </asp:Label>
                            <asp:Literal runat="server" ID="uploadID" />
                        </asp:Panel>
                        
                        <label class="control-label">Upload New Data</label>
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="filestyle" data-icon="false" />
                        <div class="btn-upload-panel">
                            <asp:LinkButton runat="server" ID="uploadXML" OnClick="UploadNewFile" class="btn btn-import btn-md" enctype="multipart/form-data">Upload<span class="glyphicon glyphicon glyphicon-upload"></span></asp:LinkButton>
                        </div>

                        <br />

                        <label class="control-label">Upload Corrected Data</label>
                        <asp:FileUpload ID="FileUpload2" runat="server" CssClass="filestyle" data-icon="false" />
                        <div class="btn-upload-panel">
                            <asp:LinkButton runat="server" ID="LinkButton3" OnClick="UploadNewFile" class="btn btn-import btn-md" enctype="multipart/form-data">Upload<span class="glyphicon glyphicon glyphicon-upload"></span></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <!--/ Upload file panel -->

            <!-- Action Log -->
            <div class="col-sm-4 col-md-5">
                <div class="panel panel-default log-panel" style ="overflow: auto">
                    <div class="panel-heading">
                        <span class="glyphicon glyphicon-log-in icon"></span>Export Log
                    <asp:LinkButton runat="server" ID="clearBtn" OnClick="ClearLog" OnClientClick="showClearProgress()" CssClass="pull-right">Clear</asp:LinkButton>
                    </div>
                    <div class="panel-body log-panel-body">
                        <asp:UpdatePanel runat="server" ID="logUpdatePanel">
                            <ContentTemplate>
                                <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="logUpdatePanel" DynamicLayout="true"></asp:UpdateProgress>
                                <asp:Literal runat="server" ID="logPanel" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="clearBtn" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function pageLoad() {
            $('.log-panel-body').removeClass('clear-log').css('opacity', '');
            $('#exportPanel').addClass('active');

            $('#exportPanel').siblings().each(function () {
                $(this).removeClass('active');
            });

            //$('#newDataFile').fileinput();
        }

        //new data panel
        $('#new-data-link').click(function () {
            if (!$(this).parent().hasClass('active')) {
                $(this).parent().addClass('active');
            }

            $(this).parent().siblings().each(function () {
                $(this).removeClass('active');
            });


            $('.correction-data').css('display', 'none');
            $('.delete-data').css('display', 'none');
            $('.new-data').css('display', 'block');
        });

        //correction data panel
        $('#corr-data-link').click(function () {
            if (!$(this).parent().hasClass('active')) {
                $(this).parent().addClass('active');
            }

            $(this).parent().siblings().each(function () {
                $(this).removeClass('active');
            });

            $('.delete-data').css('display', 'none');
            $('.new-data').css('display', 'none');
            $('.correction-data').css('display', 'block');
        });

        //delete data panel
        $('#del-data-link').click(function () {
            if (!$(this).parent().hasClass('active')) {
                $(this).parent().addClass('active');
            }

            $(this).parent().siblings().each(function () {
                $(this).removeClass('active');
            });

            $('.new-data').css('display', 'none');
            $('.correction-data').css('display', 'none');
            $('.delete-data').css('display', 'block');
        });




        //select all accounts checkbox
        $('#selectAllAccountsBox').click(function () {
            $('input[name=accountCheckGroup]').toggleCheckBoxes(this.checked);
        });

        //add selected accounts to list
        var accounts;

        function getCheckedAccounts() {
            accounts = new Array();

            $('input[name=accountCheckGroup]').each(addSelected);
            $('#<%= accountSelected.ClientID%>').val(accounts);

            alert(accounts)

        }

        function addSelected() {
            if (this.checked)
                accounts.push(this.value + ':' + $(this).parent().siblings('.newAcctHolderId').text());
        }

        //toggle all checkboxes function
        $.fn.toggleCheckBoxes = function (checked) {
            if (checked) {
                this.prop('checked', true);
            }
            else {
                this.prop('checked', false);
            }
        };

        //clear log async progress status
        function showClearProgress() {
            $('.log-panel-body').addClass('clear-log').css('opacity', 0.5);
        }
    </script>
</asp:Content>
