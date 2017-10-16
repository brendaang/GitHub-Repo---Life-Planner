<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Forum.aspx.cs" Inherits="Life_Planner.Account.Forum" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section>
        <h2>Discussion Threads</h2>
    </section>
    <section>
        <div class="form-group">
                <asp:Label ID="lblSearch" runat="server" Text="Search Thread: " CssClass="col-md-2 control-label"></asp:Label>
                <div class="col-md-8">
                    <asp:TextBox ID="tbSearch" runat="server" CssClass="col-md-4 form-control" placeholder="Search"></asp:TextBox>
                    <div>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn btn-primary" />
                        <asp:Button ID="createThread" runat="server" Text="Create New Thread" CssClass="btn btn-primary" OnClick="createThread_Click" style="margin-left:20px" />
                        <br />
                        <br />
                    </div>
                </div>
        </div>

    </section>
    <br />

    <br />
    <section>
        <div class="form-group">

            <asp:GridView ID="threadsGridView" runat="server" AutoGenerateColumns="False" BackColor="White" CssClass="table table-striped table-hover" EnableTheming="False" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="1166px" DataKeyNames="threadID" OnSelectedIndexChanged="threadsGridView_SelectedIndexChanging" OnRowCommand="getPosts">
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
                    <asp:BoundField DataField="threadID" HeaderText="Thread ID" InsertVisible="False" ReadOnly="True" SortExpression="threadID" Visible="true" />
                    <asp:BoundField DataField="threadName" HeaderText="Thread Name" SortExpression="threadName" />
                    <asp:BoundField DataField="threadDesc" HeaderText="Thread Description" SortExpression="threadDesc" />
                    <%-- <asp:BoundField DataField="acctName" HeaderText="Author" SortExpression="acctName"/>--%>
                    <asp:TemplateField HeaderText="Author">
                        <ItemTemplate>
                            <asp:LinkButton ID="authorLinkButton" runat="server" CausesValidation="false" OnClick="getAuthor" Text='<%#Eval("userName")%>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="dateCreated" HeaderText="Date Created" SortExpression="dateCreated" />

                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="viewLinkButton" runat="server" CausesValidation="true" CommandName="viewPosts" CommandArgument="<%# Container.DataItemIndex %>" Text="View">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div>
            <asp:Label ID="lblThreadID" runat="server" Visible="false"></asp:Label>
        </div>
    </section>
</asp:Content>
