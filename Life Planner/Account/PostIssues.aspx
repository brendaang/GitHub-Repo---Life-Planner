<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PostIssues.aspx.cs" Inherits="Life_Planner.Account.PostIssues" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section>
        <h2>Unresolved Post Issues</h2>
    </section>
    <section>
        <div class="form-group">
            <asp:GridView ID="issuesGridView" runat="server" AutoGenerateColumns="False" BackColor="White" CssClass="table table-striped table-hover" EnableTheming="False" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="1166px" DataKeyNames="reportID" OnRowCommand="getIssues">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
                <Columns>
                    <asp:BoundField DataField="reportID" HeaderText="Report ID" InsertVisible="False" ReadOnly="True" SortExpression="reportID" Visible="true" />
                    <asp:BoundField DataField="postID" HeaderText="Post ID" SortExpression="postID" />
                    <%--<asp:BoundField DataField="reporter" HeaderText="Reporter" SortExpression="reporter" />--%>
                    <asp:TemplateField HeaderText="Reporter">
                        <ItemTemplate>
                            <asp:LinkButton ID="reporterLinkButton" runat="server" CausesValidation="false" OnClick="getUser" Text='<%#Eval("reporter")%>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Author">
                        <ItemTemplate>
                            <asp:LinkButton ID="authorLinkButton" runat="server" CausesValidation="false" OnClick="getUser" Text='<%#Eval("author")%>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="dateTimeReported" HeaderText="Date Reported" SortExpression="dateTimeReported" />
                    <%--<asp:BoundField DataField="resolved" HeaderText="Resolved" SortExpression="resolved" />--%>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="viewLinkButton" runat="server" CausesValidation="true" CommandName="viewIssues" CommandArgument="<%# Container.DataItemIndex %>" Text="View">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </section>
</asp:Content>
