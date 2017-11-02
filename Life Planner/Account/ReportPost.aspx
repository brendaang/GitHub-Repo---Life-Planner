<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportPost.aspx.cs" Inherits="Life_Planner.Account.ReportPost" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Report Form</h3>
    <br />
    <h4>Reasons for Report</h4>
    <br />
    <div class="form-group">

        <asp:RadioButtonList ID="RadioButtonList1" runat="server">
            <asp:ListItem>Post was irrelevant to Topic</asp:ListItem>
            <asp:ListItem>Post contained Vulgarities</asp:ListItem>
            <asp:ListItem>Post contained Advertising Elements</asp:ListItem>
            <asp:ListItem>Others</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Label ID="lblSpecifyReasons" runat="server" CssClass="col-md-2 control-label" Text="Please Specify Your Reasons: "></asp:Label>
        <br />
        <div>
            <asp:TextBox ID="txtSpecifyReasons" runat="server" Rows="3" TextMode="MultiLine" CssClass="form-control" Width="500px" onkeypress="return this.value.length <= 100" Enabled="false" AutoPostBack="True"></asp:TextBox>
            <br />
            <br />
        </div>
    </div>
    <div>
        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" Width="87px" />
        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancel_Click" Width="87px" />
    </div>
</asp:Content>
