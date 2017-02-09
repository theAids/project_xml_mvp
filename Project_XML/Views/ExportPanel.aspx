<%@ Page Title="Export" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ExportPanel.aspx.cs" Inherits="Project_XML.Views.ExportPanel" %>

<%@ Register TagPrefix="uc" TagName="UserMenu" Src="./UserControls/UserMenu.ascx" %>

<asp:Content ID="ExportPanel_Content" ContentPlaceHolderID="Master_Page" runat="server">

    <uc:UserMenu runat="server" ID="UserMenu1" />

    <div class="col-sm-9 col-sm-offset-3 col-md-10 col-md-offset-2 main">
        <h1 class="page-header">Export XML</h1>
        <div class="row">

            <div class="col-sm-5 col-md-6">
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
                                        <label class="control-label" for="ContactField1">Contact:</label>
                                        <input type="text" class="form-control" id="ContactField1" />
                                    </div>
                                    <!-- Return Year Field -->
                                    <div class="form-group required">
                                        <label class="control-label" for="YearField1">Return Year:</label>
                                        <select class="form-control" id="YearField1">
                                            <option>2017</option>
                                            <option>2016</option>
                                            <option>2015</option>
                                            <option>2014</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <!-- Attention Note Field -->
                                    <div class="form-group">
                                        <label class="control-label" for="AttentionNoteField">Attention Note:</label>
                                        <textarea rows="2" style="resize: none" class="form-control" id="AttentionNoteText"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <!-- Account List field -->
                                    <div class="panel panel-default accounts-table required">
                                        <div class="panel-heading"><b>Accounts</b></div>
                                        <div class="table-responsive">
                                            <table class="table table-striped">
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox runat="server" ID="selectAllAccountsChk" /></th>
                                                        <th>Account Number</th>
                                                        <th>Account Holder</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <input type="checkbox" />
                                                        </td>
                                                        <td>3099984</td>
                                                        <td>Adrian Perez</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input type="checkbox" /></td>
                                                        <td>3099985</td>
                                                        <td>Stock Holdings Lmt.</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="btn-export-panel">
                                    <asp:LinkButton runat="server" ID="NewDataBtn" class="btn btn-export btn-md">Export XML<span class="glyphicon glyphicon-download-alt"></span></asp:LinkButton>
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
                                        <label class="control-label" for="ContactField2">Contact:</label>
                                        <input type="text" class="form-control" id="ContactField2" />
                                    </div>
                                    <!-- Message Ref ID -->
                                    <div class="form-group required">
                                        <label class="control-label" for="MsgRefId">Message Ref ID:</label>
                                        <select class="form-control" id="MsgRefId">
                                            <option>2017109898720170123125101</option>
                                            <option>2017109898720170123125103</option>
                                            <option>2017109898720170123125104</option>
                                        </select>
                                    </div>
                                </div>

                                <div class="col-sm-6">
                                    <!-- Attention note field -->
                                    <div class="form-group">
                                        <label class="control-label" for="AttentionNoteField">Attention Note:</label>
                                        <textarea rows="2" style="resize: none" class="form-control" id="AttentionNoteText"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <!-- Accounts table -->
                                    <div class="panel panel-default accounts-table required">
                                        <div class="panel-heading"><b>Accounts</b></div>
                                        <div class="table-responsive">
                                            <table class="table table-striped">
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox runat="server" ID="CheckBox1" /></th>
                                                        <th>Doc Ref ID</th>
                                                        <th>Account Number</th>
                                                        <th>Account Holder</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <input type="checkbox" />
                                                        </td>
                                                        <td>00001234</td>
                                                        <td>3099984</td>
                                                        <td>Adrian Perez</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input type="checkbox" /></td>
                                                        <td>00001235</td>
                                                        <td>30999811</td>
                                                        <td>Stock Holdings Lmt.</td>
                                                    </tr>
                                                </tbody>
                                            </table>
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
                                        <label class="control-label" for="ContactField3">Contact:</label>
                                        <input type="text" class="form-control" id="ContactField3" />
                                    </div>
                                    <!-- Message Ref ID -->
                                    <div class="form-group required">
                                        <label class="control-label" for="MesgRefId1">Message Ref ID:</label>
                                        <select class="form-control" id="MsgRefId1">
                                            <option>2017109898720170123125101</option>
                                            <option>2017109898720170123125103</option>
                                            <option>2017109898720170123125104</option>
                                        </select>
                                    </div>
                                </div>

                                <div class="col-sm-6">
                                    <!-- Attention note field -->
                                    <div class="form-group">
                                        <label class="control-label" for="AttentionNoteField">Attention Note:</label>
                                        <textarea rows="2" style="resize: none" class="form-control" id="AttentionNoteText"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <!-- Accounts table -->
                                    <div class="panel panel-default accounts-table required">
                                        <div class="panel-heading"><b>Accounts</b></div>
                                        <div class="table-responsive">
                                            <table class="table table-striped">
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            <asp:CheckBox runat="server" ID="CheckBox2" /></th>
                                                        <th>Doc Ref ID</th>
                                                        <th>Account Number</th>
                                                        <th>Account Holder</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <input type="checkbox" />
                                                        </td>
                                                        <td>00001234</td>
                                                        <td>3099984</td>
                                                        <td>Adrian Perez</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <input type="checkbox" /></td>
                                                        <td>00001235</td>
                                                        <td>30999811</td>
                                                        <td>Stock Holdings Lmt.</td>
                                                    </tr>
                                                </tbody>
                                            </table>
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

            <!-- Action Log -->
            <div class="col-sm-5 col-md-6">
                <div class="panel panel-default log-panel">
                    <div class="panel-heading">
                        <span class="glyphicon glyphicon-log-in icon"></span>Export Log
                    <asp:LinkButton runat="server" ID="clearBtn" OnClick="ClearLog" OnClientClick="clear_logs()" CssClass="pull-right">Clear</asp:LinkButton>
                    </div>
                    <div class="panel-body log-panel-body">
                        <asp:UpdatePanel runat="server" ID="logUpdatePanel">
                            <ContentTemplate>
                                <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="logUpdatePanel" DynamicLayout="true">
                                </asp:UpdateProgress>
                                <asp:Literal runat="server" ID="logPanel" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="clearBtn" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="panel panel-default sub-report">
                    <div class="panel-heading">
                        <span class="glyphicon glyphicon-inbox icon"></span>Submitted Documents
                    </div>
                    <div class="table-responsive doc-table">
                        <table class="table table-striped">
                            <thead>
                                <tr>
                                    <th>Message Ref ID</th>
                                    <th>File Serial Number</th>
                                    <th>Message Type</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>2017109898720170123125101</td>
                                    <td>12345677</td>
                                    <td>New</td>
                                </tr>
                                <tr>
                                    <td>2016109898720170123125101</td>
                                    <td>22323412</td>
                                    <td>Correction</td>
                                </tr>
                                <tr>
                                    <td>2013109898720170123125101</td>
                                    <td><a href="#">Add</a></td>
                                    <td>Correction</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>

                </div>
            <!--/ Action Log -->
        </div>

    </div>

    <script type="text/javascript">
        function pageLoad() {
            $('.log-panel-body').removeClass('clear-log').css('opacity', '');
            $('#exportPanel').addClass('active');

            $('#exportPanel').siblings().each(function () {
                $(this).removeClass('active');
            });
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

        //clear log async progress status
        function clear_logs() {
            $('.log-panel-body').addClass('clear-log').css('opacity', 0.5);
        }
    </script>

</asp:Content>
