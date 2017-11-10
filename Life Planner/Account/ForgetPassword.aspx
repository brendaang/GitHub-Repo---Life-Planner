<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="Life_Planner.Account.ForgetPassword" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <script src="https://www.google.com/recaptcha/api.js" ></script>
    <script type="text/javascript">
        var onloadCallback = function () {
            grecaptcha.render('fp', {
                'sitekey': '6LfqzDUUAAAAAJcCq4BMuPDDD9sePZGAlC1fMX89'
            });
        };
    </script>

        <%--<h2><%: Title %>.</h2>--%>
        <h2>Life Planner.</h2>
        <h3>Forget Password.</h3>
       <div class="container">
        <!-- Alert placeholder, alter attributes in CodeBehind -->
        <div id="alert_placeholder" runat="server" visible="false">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <asp:Literal runat="server" ID="alertText" />
        </div>

        <div class="well">
            <fieldset class="form-horizontal">
                <div class="row">
                    <legend class="col-lg-offset-4 col-lg-5">Recover your password:</legend>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_UsernameOrEmail" CssClass="col-lg-4 control-label" runat="server">Username / Email:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_UsernameOrEmail" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <%--<div class="col-lg-1">
                        <asp:RequiredFieldValidator ID="rfv_fName" runat="server" ErrorMessage="First Name Required" ControlToValidate="tb_fName" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </div>--%>
                </div>
                <div class="form-group">
                    <div class="col-lg-offset-4 col-lg-6">
                         <div id="fp"></div>
                        <%--<div class="g-recaptcha" data-sitekey="6LfqzDUUAAAAAJcCq4BMuPDDD9sePZGAlC1fMX89"></div>--%>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-lg-4 col-lg-offset-4">
                        <asp:Button ID="btn_Reset" runat="server" OnClick="btn_Reset_Click" Text="Reset Password" class="btn btn-primary" Width="150px"/>
                    <asp:Label ID="lb_EndInfo" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </div>
            </fieldset>
            <script src="https://www.google.com/recaptcha/api.js?onload=onloadCallback&render=explicit" >
            </script>
        </div>
    </div>
</asp:Content>
