<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreatePlan.aspx.cs" Inherits="Life_Planner.Account.CreatePlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.4/js/bootstrap-select.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.4/css/bootstrap-select.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.4/css/bootstrap-select.css" />

    <script type="text/javascript">

        $(document).ready(function () {

            $(".selectpicker").selectpicker();

        });

    </script>

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("[id$=txtCreatePlanDOB]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'http://www.bangorparksandrec.com/info/images/button_calendar.png',
                dateFormat: 'yy/mm/dd',
                changeMonth: true,
                changeYear: true,
                yearRange: '-100y:c+nn',
                maxDate: '-1d'
            });
        });
    </script>

    <%--<script src ="https://gist.github.com/eddiemoore/7131781#file-nric-validation-js" type="text/javascript"></script>--%>

    <asp:Panel ID="Panel1" runat="server" Height="997px" >
        <section id="createPlan" >
            <h2></>Create Plan</h2>

            <h5>Step 1: Please enter your child's details</h5>


            <div id="alert_placeholder" runat="server" visible="false">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                <asp:Literal runat="server" ID="alertText" />
            </div>
            <hr />
            <br />

            <div>
                <div class="form-group">
                    <asp:Label ID="lblCreatePlanfName" runat="server" CssClass="col-md-2 control-label" Text="First Name:"></asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="txtCreatePlanfName" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCreatePlanfName" CssClass="text-danger" ErrorMessage="The 'First Name' field is required." />
                        <br />
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblCreatePlanlName" runat="server" CssClass="col-md-2 control-label" Text="Last Name:"></asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="txtCreatePlanlName" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCreatePlanlName" CssClass="text-danger" ErrorMessage="The 'Last Name' field is required." />
                        <br />
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblCreatePlanNRIC" runat="server" CssClass="col-md-2 control-label" Text="NRIC:"></asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="txtCreatePlanNRIC" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCreatePlanNRIC" CssClass="text-danger" ErrorMessage="The 'NRIC' field is required." Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="regexNRIC" runat="server" ControlToValidate="txtCreatePlanNRIC" CssClass="text-danger" Display="Dynamic" Text="The NRIC is invalid." ValidationExpression="^[STFGstfg][0-9][0-9][0-9][0-9][0-9][0-9][0-9][a-zA-Z]$" />
                        <br />
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblCreatePlanEmail" runat="server" CssClass="col-md-2 control-label" Text="Email:"></asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="txtCreatePlanEmail" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCreatePlanEmail" CssClass="text-danger" ErrorMessage="The 'Email' field is required." Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="regemail" runat="server" ControlToValidate="txtcreateplanemail" CssClass="text-danger" Display="Dynamic" Text="The Email is invalid." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                        <br />
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblCreatePlanGender" runat="server" CssClass="col-md-2 control-label" Text="Gender:"></asp:Label>
                    <div class="col-md-10">
                        <asp:RadioButtonList ID="radioCreatePlanGender" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem class="radio-inline" Value="1" Text="Male" Selected="True"></asp:ListItem>
                            <asp:ListItem class="radio-inline" Value="0" Text="Female"></asp:ListItem>
                        </asp:RadioButtonList>
                        <br />
                        <br />
                    </div>
                </div>
                <br />

                <div class="form-group">
                    <br />
                    <asp:Label ID="lblCreatePlanDOB" runat="server" CssClass="col-md-2 control-label" Text="Date Of Birth:"></asp:Label>
                    <div class="col-md-9">
                        <table class="nav-justified">
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" ID="txtCreatePlanDOB" CssClass="form-control"></asp:TextBox></td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCreatePlanDOB" CssClass="text-danger" Display="Dynamic" ErrorMessage="The 'Date Of Birth' field is required." />
                        <asp:RegularExpressionValidator ID="regdob" runat="server" ControlToValidate="txtCreatePlanDOB" CssClass="text-danger" Display="Dynamic" Text="The DOB is invalid." ValidationExpression="^(19[5-9][0-9]|20[0-4][0-9]|2050)[-/](0?[1-9]|1[0-2])[-/](0?[1-9]|[12][0-9]|3[01])$" />
                        <br />
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblCreatePlanEdLv" runat="server" CssClass="col-md-2 control-label" Text="Child's Current Education Level:"></asp:Label>
                    <div class="col-md-10">
                    </div>
                </div>

                

                <div class="form-group">
                    <div class="dropdown">
                        <div class="col-md-10">
                            <asp:DropDownList ID="ddlCreatePlanChildCurrentEdLevel" runat="server" Width="250px" CssClass="selectpicker">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>


                <div class="form-group">
                    <div class="col-lg-10 col-lg-offset-4">
                        <br />
                        <asp:Button ID="btnClearChild" CssClass="btn btn-default" Text="Clear" runat="server" OnClick="clearBtn_Click" CausesValidation="false" />
                        &nbsp;
                        <asp:Button ID="btnSubmitChild" CssClass="btn btn-primary" Text="Submit" runat="server" OnClick="submitFeedback_Click" />
                    </div>
                </div>
            </div>

        </section>
    </asp:Panel>


</asp:Content>
