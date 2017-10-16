<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Posts.aspx.cs" Inherits="Life_Planner.Account.Posts" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function ConfirmReport() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to report this post?")) {
                confirm_value.value = "Yes";
            }
            else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <section>
        <h2>Discussion Posts</h2>
        <h3>Topic: 
            <asp:Label ID="lblTopic" runat="server"></asp:Label>
        </h3>
        <br />
        <div class="panel panel-success">
            <div class="panel-heading">
                Thread Description
            </div>
            <div class="panel-body">
                <h4>
                    <asp:Label ID="lblThreadDesc" runat="server"></asp:Label>
                </h4>
            </div>
        </div>
    </section>

    <section>
        <asp:Label runat="server" ID="pageNum" Visible="false"></asp:Label>
        <div class="form-group">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <asp:Panel runat="server">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <!-- Some Title text -->
                                <%--<asp:Label ID="lblAcctName" runat="server" Text='<%# Eval("acctName") %>'></asp:Label>--%>
                                <%--<%# Eval("postID") %>--%>
                                <div class="col-md-11">
                                    <itemtemplate>
                                        <asp:LinkButton ID="authorLinkButton" runat="server" CausesValidation="false" OnClick="getAuthor" Text='<%#Eval("userName")%>' CssClass="btn btn-link" ForeColor="White" ></asp:LinkButton>
                                    </itemtemplate>
                                    <%--<asp:Label ID="userTitle" runat="server" Text='<%#Eval("acctTitle") %>'></asp:Label>--%>
                                </div>
                                <asp:Button ID="btnReport" runat="server" Text="Report" style="background-color: black" CssClass="btn" CommandName="report" OnClick="btnReport_Click" OnClientClick="ConfirmReport()" />
                            </div>
                            <div class="panel-body">
                                <!-- Your content-->
                                <p class="small">
                                    <asp:Label ID="postID" runat="server" Text='<%#Eval("postID") %>' Visible="false"></asp:Label>
                                </p>
                                <h4><%# Eval("postText") %></h4>
                                <p class="small"><%# Eval("datePosted") %></p>

                            </div>
                            <div class="panel-footer col-md-0">
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-warning" CommandName="edit" OnClick="btnEdit_Click" Width="87px" />
                                <asp:Button ID="btnQuote" runat="server" Text="Quote" CssClass="btn btn-success" CommandName="quote" OnClick="btnQuote_Click" Width="87px" />
                                <div class="col-md-10">
                                    <asp:Button ID="btnLike" runat="server" Text="Like ↑" CssClass="btn btn-primary" CommandName="like" OnClick="btnLikeOnClick" />
                                    <asp:Button ID="btnDislike" runat="server" Text="Dislike ↓" CssClass="btn btn-danger" CommandName="dislike" OnClick="btnDislikeOnClick" />
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Repeater ID="rptPages" runat="server" OnItemCommand="rptPages_ItemCommand">
                <HeaderTemplate>
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr class="text">
                            <td><b>Page:</b>&nbsp;</td>
                            <td>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="btnPage"
                        CommandName="Page"
                        CommandArgument="<%#
                         Container.DataItem %>"
                        CssClass="text"
                        runat="server"><%# Container.DataItem %>
                    </asp:LinkButton>&nbsp;
                </ItemTemplate>
                <FooterTemplate>
                    </td>
      </tr>
      </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </section>

    <section>
        <br />
        <asp:Label ID="lblEmptyThread" runat="server"></asp:Label>
    </section>
    <section id="postToThread">
        <h2>
            <asp:Label ID="lblPostOrReply" runat="server"></asp:Label>
        </h2>
        <br />
        <div class="form-group">

            <asp:Label ID="lblPostOrReplyContent" runat="server" CssClass="col-md-2 control-label"></asp:Label>
            <div class="col-md-10">
                 <asp:Textbox ID="txtEditor" runat="server" Width="900px" Height="200" TextMode="MultiLine" /><br />
                <asp:Label ID="lblWarningMsg" runat="server" CssClass="text-danger" Text=" "></asp:Label>
                <br />
                <br />
            </div>
        </div>
        <br />
        <div class="form-group">
            <asp:Label ID="lbl" runat="server" CssClass="col-md-2 control-label" Text=""></asp:Label>
            <div style="float: left">
                <asp:Button ID="btnPost" runat="server" CssClass="btn btn-primary" OnClick="btnPost_Click" Text="Post" />
                <br />
            </div>
            <div>
                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
        <br />
    </section>


</asp:Content>
