<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewOwnPlan.aspx.cs" Inherits="Life_Planner.Account.ViewOwnPlan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Life Planner.</h2>
    <h3>View Own Plan.</h3>
            <div class="container">

        <!-- Alert placeholder, alter attributes in CodeBehind -->
        <div id="Div1" runat="server" visible="false">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <asp:Literal runat="server" ID="Literal1" />
        </div>

        <div class="well">
            <fieldset class="form-horizontal">
                <div class="row">
                    <legend class="col-lg-offset-4 col-lg-5">Plan:</legend>
                 </div>

                <div class="form-group">
                    <asp:Label ID="lbl_priName" CssClass="col-lg-4 control-label" runat="server">Primary School:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_priName" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lbl_secName" CssClass="col-lg-4 control-label" runat="server">Secondary School:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_secName" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lbl_jcName" CssClass="col-lg-4 control-label" runat="server">Junior College:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_jcName" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lbl_polyName" CssClass="col-lg-4 control-label" runat="server">Polytechnic:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_polyName" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lbl_polyCourse" CssClass="col-lg-4 control-label" runat="server">Polytechnic Course:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_polyCourse" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lbl_uniName" CssClass="col-lg-4 control-label" runat="server">University:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_uniName" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lbl_uniCourse" CssClass="col-lg-4 control-label" runat="server">University Course:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_uniCourse" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-lg-4 col-lg-offset-4">
                        <asp:Button ID="btn_editPlan" runat="server" OnClick="btn_editPlan_Click" Text="Edit Plan" class="btn btn-primary" Width="100px"/>
                          <asp:Button ID="btn_deletePlan" runat="server" OnClick="btn_deletePlan_Click" Text="Delete Plan" class="btn btn-danger" Width="100px" OnClientClick="return confirm('Do you want to delete this plan? ');"/>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>
