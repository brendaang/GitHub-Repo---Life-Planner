<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewOwnPlan.aspx.cs" Inherits="Life_Planner.Account.ViewOwnPlan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Life Planner.</h2>
    <h3>View Own Plan.</h3>
            <div class="container">

        <!-- Alert placeholder, alter attributes in CodeBehind -->
        <div id="alert_placeholder" runat="server" visible="false">
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
            <asp:Literal runat="server" ID="alertText" />
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
                    <div class="col-lg-4 col-lg-offset-4">
                        <asp:Button ID="btn_editPri" runat="server" Text="Edit Primary" class="btn btn-primary" Width="100px" OnClick="btn_editPri_Click" Visible="false"/>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lbl_secName" CssClass="col-lg-4 control-label" runat="server">Secondary School:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_secName" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-lg-4 col-lg-offset-4">
                        <asp:Button ID="btn_editSec" runat="server" Text="Edit Secondary" class="btn btn-primary" Width="150px" OnClick="btn_editSec_Click" Visible="false"/>
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
                    <div class="col-lg-4 col-lg-offset-4">
                       <asp:Button ID="btn_editTertiary" runat="server" Text="Edit Tertiary" class="btn btn-primary" Width="100px" OnClick="btn_editTertiary_Click" Visible="false"/>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lbl_shortestTime" CssClass="col-lg-4 control-label" runat="server">Shortest Time:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_shortestTime" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lbl_longestTime" CssClass="col-lg-4 control-label" runat="server">Longest Time:</asp:Label>
                    <div class="col-lg-6">
                        <asp:TextBox ID="tb_longestTime" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-lg-4 col-lg-offset-4">
                        <asp:Button ID="btn_editPlan" runat="server" OnClick="btn_editPlan_Click" Text="Edit Plan" class="btn btn-primary" Width="100px"/>
                        <asp:Button ID="btn_doneEdit" runat="server" OnClick="btn_doneEdit_Click" Text="Done" class="btn btn-primary" Width="100px" Visible="false"/>
                        <asp:Button ID="btn_deletePlan" runat="server" OnClick="btn_deletePlan_Click" Text="Delete Plan" class="btn btn-danger" Width="100px" OnClientClick="return confirm('Do you want to delete this plan? ');"/>
                    </div>
                </div>
            </fieldset>
        </div>
</asp:Content>
