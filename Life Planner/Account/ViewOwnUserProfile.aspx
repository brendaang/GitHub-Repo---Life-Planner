<%@ Page Title="My Pr" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewOwnUserProfile.aspx.cs" Inherits="Life_Planner.Account.ViewOwnUserProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h2><%: Title %>.</h2>
    <h3>My User</h3>

    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 
<html xmlns="http://www.w3.org/1999/xhtml" >

<body>

    <div>
    <asp:Label ID="fname" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="lname" runat="server" Text="Label"></asp:Label><br />
    <asp:Label ID="email" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="birthdate" runat="server" Text="Label"></asp:Label><br />
    <asp:Label ID="gender" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="role" runat="server" Text="Label"></asp:Label>
    </div>
    
</body>
</html>



</asp:Content>
