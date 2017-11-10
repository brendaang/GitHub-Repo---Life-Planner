<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SetAdmin.aspx.cs" Inherits="Life_Planner.Account.SetAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<h2><%: Title %>.</h2>--%>
    <h2> Life Planner.</h2>
    <h3>Admin Setting.</h3>
        <div class="container">

        <!-- Alert placeholder, alter attributes in CodeBehind -->
        <div id="alert_placeholder" runat="server" visible="false">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <asp:Literal runat="server" ID="alertText" />
        </div>

        <div class="well">
            <fieldset class="form-horizontal">
                <div class="row">
                    <legend class="col-lg-offset-4 col-lg-5">Admin Setting:</legend>
                </div>

                <div class="form-group">
                    <asp:Label ID="lbl_SBUserOrId" CssClass="col-lg-4 control-label" runat="server">Username:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_SBUsername" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-lg-1">
                        <asp:RequiredFieldValidator ID="rfv_SBUsername" runat="server" ErrorMessage="Username required" ControlToValidate="tb_SBUsername" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-lg-offset-4">
                        <asp:ValidationSummary ID="vs_all" runat="server" ForeColor="Red" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-lg-4 col-lg-offset-4">
                        <asp:Button ID="btn_Change" runat="server" OnClick="btn_Change_Click" Text="Search" class="btn btn-primary" />
                        <asp:Label ID="lb_EndInfo" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>    

</asp:Content>
