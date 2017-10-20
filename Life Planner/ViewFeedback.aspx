﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewFeedback.aspx.cs" Inherits="Life_Planner.ViewFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>All Feedbacks</h3>
    <hr />
    <table class="nav-justified">
        <tr>
            <td style="width: 595px;">All (
                <asp:Label ID="countFeedbacks" runat="server"></asp:Label>
                )&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Records per page: 
                <asp:DropDownList ID="NumRecordLoaded" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <section>
                    <div class="form-group">
                        &nbsp;<asp:Label ID="lblSearch" runat="server" Text="Search : " CssClass="col-md-2 control-label"></asp:Label>
                        <div class="col-md-8">
                            <asp:TextBox ID="fbkSearch" runat="server" CssClass="col-md-4 form-control" placeholder="Search feedback"></asp:TextBox>
                            <div>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" />
                                <br />
                                <br />
                            </div>
                        </div>
                    </div>
                </section>
            </td>
        </tr>
    </table>
    <br />
    <section>
        <div class="form-group">
            <asp:GridView ID="feedbackGridView" runat="server" AutoGenerateColumns="False" BackColor="White" CssClass="table table-striped table-hover" EnableTheming="False" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="1166px">
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
                    <asp:BoundField DataField="feedbackID" HeaderText="Feedback ID" InsertVisible="False" SortExpression="feedbackID" ReadOnly="True" Visible="true" />
                    <asp:BoundField DataField="feedbackStatus" HeaderText="Status" SortExpression="feedbackStatus" />
                    <asp:BoundField DataField="feedbackIssue" HeaderText="Issue" SortExpression="feedbackIssue" />
                    <asp:BoundField DataField="feedbackDatetime" HeaderText="Created Date" SortExpression="feedbackDatetime" />
                    <%-- <asp:BoundField DataField="feedbackContent" HeaderText="Content" SortExpression="feedbackContent" />--%>
                    <%--<asp:BoundField DataField="submittedBy" HeaderText="Submitted By" SortExpression="feedbackContent" />--%>
                    <%-- <asp:BoundField DataField="acctName" HeaderText="Author" SortExpression="acctName"/>--%>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="feedbackAction" runat="server" CommandArgument='<%#Eval("feedbackID")%>' Text="View Details">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </section>
</asp:Content>
