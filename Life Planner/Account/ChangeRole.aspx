<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangeRole.aspx.cs" Inherits="Life_Planner.Account.ChangeRole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Life Planner.</h2>
    <h3>Admin Setting.</h3>
            <div class="container">

        <!-- Alert placeholder, alter attributes in CodeBehind -->
        <div id="Div1" runat="server" visible="false">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <asp:Literal runat="server" ID="Literal1" />
        </div>

        <div class="well">
            <fieldset class="form-horizontal">
                <div class="row">
                    <legend class="col-lg-offset-4 col-lg-5">Admin Setting:</legend>
                 </div>

                <div class="form-group">
                    <asp:Label ID="lbl_accId" CssClass="col-lg-4 control-label" runat="server">Account ID:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_accId" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lbl_fname" CssClass="col-lg-4 control-label" runat="server">First Name:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_fname" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lbl_lname" CssClass="col-lg-4 control-label" runat="server">Last Name:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_lname" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lbl_role" CssClass="col-lg-4 control-label" runat="server">Role:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_role" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-lg-4 col-lg-offset-4">
                        <asp:Button ID="btn_changeRole" runat="server" OnClick="btn_ChangeRole_Click" Text="Change Role" class="btn btn-primary" Width="150px" OnClientClick="return confirm('Do you want to change role of user? ');"/>
                        <asp:Label ID="lbl_changeRole" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
