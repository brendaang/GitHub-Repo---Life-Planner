<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Editing-Posts.aspx.cs" Inherits="Life_Planner.Account.Editing_Posts" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section>
        <h2>Edit Post</h2>
    </section>
    <br />
    <br />
    <div class="form-group">
        <asp:Label ID="lblUsername" runat="server" CssClass="col-md-2 control-label" Text="User Name: "></asp:Label>
        <div class="col-md-10">
            <asp:Label ID="labelUsername" runat="server" CssClass="control-label"></asp:Label>
        </div>
    </div>
    <br />
    <br />
    <div class="form-group">
        <asp:Label ID="lblPostID" runat="server" CssClass="col-md-2 control-label" Text="PostID: "></asp:Label>
        <div class="col-md-10">
            <asp:Label ID="labelPostID" runat="server" CssClass="control-label"></asp:Label>
        </div>
    </div>
    <br />
    <br />
    <div class="form-group">
        <asp:Label ID="lblDatePosted" runat="server" CssClass="col-md-2 control-label" Text="Date Posted: "></asp:Label>
        <div class="col-md-10">
            <asp:Label ID="labelDatePosted" runat="server" CssClass="control-label"></asp:Label>
        </div>
    </div>
    <br />
    <br />
    <div class="form-group">
        <asp:Label ID="lblPostContent" runat="server" CssClass="col-md-2 control-label" Text="Post Content: "></asp:Label>
        <div class="col-md-10">
            <asp:Textbox ID="txtEditor" runat="server" Width="900px" Height="200" TextMode="MultiLine" /><br />
        </div>
    </div>
    <br />
    <br />
    <div class="form-group">
        <asp:Label ID="lbl" runat="server" CssClass="col-md-2 control-label"></asp:Label>
        <div class="col-md-10">
            <br />
            <asp:Button ID="btnEditPost" runat="server" CssClass="btn btn-primary" Text="Edit Post" OnClick="btnEditPost_Click" Width="87px" />
            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancel_Click" Width="87px" />
            <br />
        </div>
    </div>
</asp:Content>

