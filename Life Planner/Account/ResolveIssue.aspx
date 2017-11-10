<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResolveIssue.aspx.cs" Inherits="Life_Planner.Account.ResolveIssue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function ConfirmDelete() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to delete this post?")) {
                confirm_value.value = "Yes";
            }
            else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script type="text/javascript">
        function ConfirmIgnore() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to ignore this post?")) {
                confirm_value.value = "Yes";
            }
            else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <section>
        <h2>Resolve Issue</h2>
    </section>
    <section>
        <div class="form-group">
            <asp:Label ID="lblReportReason" runat="server" CssClass="control-label" Text="Report Reason: "></asp:Label>
            <br />
            <asp:Label ID="labelReportReason" runat="server"></asp:Label>
            <br />
            <br />
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <div>
                        <itemtemplate>
                            <asp:LinkButton ID="authorLinkButton" runat="server" CausesValidation="false" OnClick="authorLinkButton_Click" CssClass="btn btn-link" ForeColor="White" ></asp:LinkButton>
                        </itemtemplate>
                    </div>
                <div class="panel-footer">
                    <h4>
                        <asp:Label ID="lblPostText" runat="server"></asp:Label>
                    </h4>
                    <asp:Label ID="lblPostID" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="panel-body">
                    <p class="small">
                        <asp:Label ID="lblDatePosted" runat="server"></asp:Label>
                    </p>
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" CommandName="delete" OnClick="btnDelete_Click" OnClientClick="ConfirmDelete()" />
                    <asp:Button ID="btnIgnore" runat="server" Text="Ignore" CssClass="btn btn-warning" CommandName="ignore" OnClick="btnIgnore_Click" OnClientClick="ConfirmIgnore()"/>
                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-primary" CommandName="back" OnClick="btnBack_Click" />
                </div>
            </div>
    </section>
</asp:Content>
