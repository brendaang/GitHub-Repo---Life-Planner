<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateThread.aspx.cs" Inherits="Life_Planner.Account.CreateThread" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section id="header">
        <h2>Create a Discussion Thread</h2>
    </section>
    <br />
    <section id="createThreadForm">
        <div class="form-group">
            <asp:Label ID="lblThreadName" runat="server" CssClass="col-md-2 control-label" Text="Discussion Thread Name:"></asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtThreadName" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtThreadName" CssClass="text-danger" ErrorMessage="The 'Thread Name' field is required." />
                <br />
            </div>
        </div>
        <br />
        <br />
        <br />
        <div class="form-group">
            <asp:Label ID="lblThreadDesc" runat="server" CssClass="col-md-2 control-label" Text="Discussion Thread Description:"></asp:Label>
         
                <asp:TextBox ID="txtThreadDesc" runat="server" Rows="3" TextMode="MultiLine" CssClass="form-control" Width="500px" onkeypress="return this.value.length <= 255"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtThreadDesc" CssClass="text-danger" ErrorMessage="The 'Thread Description' field is required." />
                <br />
                <br />
           
        </div>
        <br />
        <br />
        <div class="form-group">
            <asp:Label ID="lblAuthor" runat="server" CssClass="col-md-2 control-label" Text="Author:"></asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtAuthor" CssClass="form-control" ReadOnly="True" />
                <br />
            </div>
        </div>
        <br />
        <br />
        <div class="form-group">
            <asp:Label ID="lblDate" runat="server" CssClass="col-md-2 control-label" Text="Date:"></asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtDate" CssClass="form-control" ReadOnly="True" />
                <br />
            </div>
        </div>
        <br />
        <div class="form-group">
            <asp:Label ID="lbl" runat="server" CssClass="col-md-2 control-label" Text=""></asp:Label>
            <div class="col-md-10">
                <asp:Button ID="btnCreateThread" runat="server" CssClass="form-control" Text="Create Discussion Thread" Width="200px" OnClick="btnCreateThread_Click" />
            </div>
        </div>
        <br />
        <br />
        <br />
        <br />
    </section>
</asp:Content>
