<%@ Page Title="Life Planner" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Life_Planner._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Life Planning</h1>
        <p class="lead">Plan out your ideal life with simple planning modules and calculate the costs involved!</p>
        <p><a href="Account/Register.aspx" class="btn btn-primary btn-large">Learn more by Registering! &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
            <%--    ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.--%>
            </p>
            <p>
                <a class="btn btn-default" href="<%--http://go.microsoft.com/fwlink/?LinkId=301948--%>">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>More Information</h2>
            <p>
<%--                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.--%>
            </p>
            <p>
                <a class="btn btn-default" href="<%--http://go.microsoft.com/fwlink/?LinkId=301949--%>">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Contact Us</h2>
            <p>
<%--                You can easily find a web hosting company that offers the right mix of features and price for your applications.
     --%>       </p>
            <p>
                <a class="btn btn-default" href="Contact.aspx">Learn more &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
