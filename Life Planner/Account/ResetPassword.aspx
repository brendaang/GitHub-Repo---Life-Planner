<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="Life_Planner.Account.ResetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <%--<h2><%: Title %>.</h2>--%>
        <h2>Life Planner.</h2>
        <h3>Reset Password.</h3>
        <div class="container">

        <!-- Alert placeholder, alter attributes in CodeBehind -->
        <div id="alert_placeholder" runat="server" visible="false">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <asp:Literal runat="server" ID="alertText" />
        </div>

        <div class="well">
            <fieldset class="form-horizontal">
                <div class="row">
                    <legend class="col-lg-offset-4 col-lg-5">Reset your password:</legend>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_RandomPassword" CssClass="col-lg-4 control-label" runat="server">Random Password Given:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_RandomPassword" TextMode="Password" CssClass="form-control" runat="server" placeholder="Please enter the random password given."></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:RequiredFieldValidator ID="rfv_RandomPassword" runat="server" ErrorMessage="Password required" ControlToValidate="tb_RandomPassword" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lbl_NewPassword" CssClass="col-lg-4 control-label" runat="server">New Password:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_NewPassword" TextMode="Password" CssClass="form-control" runat="server" placeholder="Please enter new password."></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:RequiredFieldValidator ID="rfv_NewPassword" runat="server" ErrorMessage="New password required" ControlToValidate="tb_NewPassword" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lbl_ConfirmPassword" CssClass="col-lg-4 control-label" runat="server">Re-enter New Password:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_ConfirmPassword" TextMode="Password" CssClass="form-control" runat="server" placeholder="Please re-type your new password."></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:RequiredFieldValidator ID="rfv_ConfirmPassword" runat="server" ErrorMessage="Please re-enter new password" ControlToValidate="tb_ConfirmPassword" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-lg-offset-4">
                        <asp:ValidationSummary ID="vs_all" runat="server" ForeColor="Red" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-lg-4 col-lg-offset-4">
                        <asp:Button ID="btn_Change" runat="server" OnClick="btn_Change_Click" Text="Change Password" class="btn btn-primary" width="150px"/>
                        <asp:Label ID="lb_EndInfo" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
