<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewOtherProfile.aspx.cs" Inherits="Life_Planner.Account.ViewOtherProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <html xmlns="http://www.w3.org/1999/xhtml">

    <body>
        <div class="container">

            <div class="well">
                <fieldset class="form-horizontal">
                    <div class="row">
                        
                        <legend class="col-lg-offset-4 col-lg-3">User: <asp:Label ID="uname"  runat="server"></asp:Label>'s Profile</legend>
                    </div>


                    <div class="form-group">
                        <asp:Label ID="fname" CssClass="col-lg-4 control-label" runat="server">First Name</asp:Label>
                        <div class="col-lg-6">
                            <asp:TextBox ID="tbFirstName" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <div class="form-group">
                        <asp:Label ID="lname" runat="server" CssClass="col-lg-4 control-label">Last Name</asp:Label>
                        <div class="col-lg-6">
                            <asp:TextBox ID="tbLname" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>

                        </div>
                    </div>

                    <br />

                    <div class="form-group">
                        <asp:Label ID="email" runat="server" CssClass="col-lg-4 control-label">Email</asp:Label>
                        <div class="col-lg-6">
                            <asp:TextBox ID="tbEmail" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <br />

                    <div class="form-group">
                        <asp:Label ID="birthdate" runat="server" CssClass="col-lg-4 control-label">Birth Date</asp:Label>
                        <div class="col-lg-6">
                            <asp:TextBox ID="tbBirthDate" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <br />

                    <div class="form-group">
                        <asp:Label ID="gender" runat="server" CssClass="col-lg-4 control-label">Gender</asp:Label>

                        <div class="col-lg-6">
                            <asp:TextBox ID="tbGender" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <br />


                    <div class="form-group">

                        <div class="pull-right">

                            <asp:Button ID="back" runat="server" Text="Back" CssClass="btn btn-primary" OnClick="back_Click" Style="margin-left: 20px" />
                            <br />

                        </div>
                    </div>




                </fieldset>
            </div>
        </div>
     



    </body>
    </html>

</asp:Content>
