﻿<%@ Page Title="Life Planner" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Life_Planner.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
<!--  jQuery -->
<script type="text/javascript" src="https://code.jquery.com/jquery-1.11.3.min.js"></script>

<!-- Bootstrap Date-Picker Plugin -->
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css"/>


    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("[id$=tb_datepicker]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: 'http://jqueryui.com/demos/datepicker/images/calendar.gif',
                dateFormat: 'yy/mm/dd',
                changeMonth: true,
                changeYear: true,
                yearRange: '-100y:c+nn',
                maxDate: '-1d'
            });
        });
    </script>

    <h2><%: Title %>.</h2>
    <h3>Register a new account.</h3>

    <div class="container">

        <!-- Alert placeholder, alter attributes in CodeBehind -->
        <div id="alert_placeholder" runat="server" visible="false">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <asp:Literal runat="server" ID="alertText" />
        </div>

        <div class="well">
            <fieldset class="form-horizontal">
                <div class="row">
                     <legend class="col-lg-offset-4 col-lg-3">Register a new account</legend>
                </div>
                <div class="form-group">
                    <asp:Label ID="lbl_username" CssClass="col-lg-4 control-label" runat="server">Username:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_username" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:RequiredFieldValidator ID="rfv_username" runat="server" ErrorMessage="Username required" ControlToValidate="tb_username" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_fName" CssClass="col-lg-4 control-label" runat="server">First Name:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_fName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:RequiredFieldValidator ID="rfv_fName" runat="server" ErrorMessage="First Name Required" ControlToValidate="tb_fName" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_lName" CssClass="col-lg-4 control-label" runat="server">Last Name:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_lName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:RequiredFieldValidator ID="rfv_lName" runat="server" ErrorMessage="Last Name Required" ControlToValidate="tb_lName" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_email" CssClass="col-lg-4 control-label" runat="server">Email Address:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_email" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:RequiredFieldValidator ID="rfv_email" runat="server" ErrorMessage="Email Address Required" ControlToValidate="tb_email" ForeColor="Red" Display="Dynamic">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rev_validEmail" runat="server" ErrorMessage="Not a valid email" ControlToValidate="tb_email" ForeColor="Red" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                    </div>
                </div>

                <div class="form-group">
                    <%--<label>Date Of Birth</label>--%>
                     <asp:Label ID="lbl_dob" CssClass="col-lg-4 control-label" runat="server">Date Of Birth:</asp:Label>
                    <div class="col-lg-6">
                       <%--<input type="text" id="tb_datepicker" class="form-control">--%>
                         <asp:TextBox ID="tb_datepicker" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_password" CssClass="col-lg-4 control-label" runat="server">Password:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_password" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:RequiredFieldValidator ID="rfv_password" runat="server" ErrorMessage="Password Required" ControlToValidate="tb_password" ForeColor="Red">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cv_passwords" runat="server" ErrorMessage="Passwords do not match" ControlToValidate="tb_password" ControlToCompare="tb_rePassword" Type="String" Operator="Equal" ForeColor="Red" Display="Dynamic">*</asp:CompareValidator>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_rePassword" CssClass="col-lg-4 control-label" runat="server">Re-enter Password:</asp:Label>
                    <div class="col-lg-6">
                        <asp:Panel ID="pan_password" runat="server" DefaultButton="btn_submit">
                            <asp:TextBox ID="tb_rePassword" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                        </asp:Panel>
                    </div>
                    <div class="col-lg-1">
                        <asp:RequiredFieldValidator ID="rfv_rePassword" runat="server" ErrorMessage="Re-enter password" ControlToValidate="tb_rePassword" ForeColor="Red" Display="Dynamic">*</asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-lg-offset-3">
                        <asp:ValidationSummary ID="vs_all" ForeColor="Red" runat="server" />
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-lg-4 col-lg-offset-4">
                        <asp:Button ID="btn_cancel" CssClass="btn btn-default" Text="Cancel" runat="server" OnClick="btn_cancel_Click" CausesValidation="false" />
                        <asp:Button ID="btn_submit" CssClass="btn btn-primary" Text="Submit" runat="server" OnClick="btn_submit_Click" />
                    </div>
                </div>

            </fieldset>
        </div>
    </div>
</asp:Content>
