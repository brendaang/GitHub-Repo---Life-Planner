<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResolveFeedback.aspx.cs" Inherits="Life_Planner.ResolveFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Feedback Details</h2>
    <hr />
    <table class="nav-justified">
        <tr>
            <td>
                <div class="form-group">
                    <asp:Label ID="lblFbkTime" runat="server" CssClass="col-md-2 control-label">Feedback Date and Time :</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="fbkDatetime" CssClass="form-control" />
                        <br />
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="form-group">
                    <asp:Label runat="server" CssClass="col-md-2 control-label">Feedback Issue :</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="txtfeedbackIssue" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtfeedbackIssue" />
                        <br />
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="form-group">
                    <asp:Label ID="lblFbkcontent" runat="server" CssClass="col-md-2 control-label">Content :</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox ID="fbkContent" runat="server" Rows="5" TextMode="MultiLine" CssClass="form-control" Width="500px"></asp:TextBox>
                        <br />
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="form-group">
                    <asp:Label ID="lblNotes" runat="server" CssClass="col-md-2 control-label">Notes :</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtAddnotes" runat="server" Rows="5" TextMode="MultiLine" CssClass="form-control" Width="500px" onkeyup="textCounter(this,'counter',10000);"></asp:TextBox>
                        <table class="nav-justified">
                            <tr>
                                <td style="width: 316px" class="modal-sm">&nbsp;</td>
                                <td>
                                    <input disabled maxlength="3" size="3" value="10000" id="counter" style="background-color: #ffffff; text-align: right; border: 1px #ffffff">
                                    character remaining</td>
                                <script>
                                    function textCounter(field, field2, maxlimit) {
                                        var countfield = document.getElementById(field2);
                                        if (field.value.length > maxlimit) {
                                            field.value = field.value.substring(0, maxlimit);
                                            return false;
                                        } else {
                                            countfield.value = maxlimit - field.value.length;
                                        }
                                    }
                                </script>
                            </tr>
                        </table>
                        <br />
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="form-group">
                    <asp:Label ID="lblResolvedBy" runat="server" CssClass="col-md-2 control-label">Resolved By :</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="resolvedBy" CssClass="form-control" ReadOnly="True" />
                        <br />
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="form-group">
                    <asp:Label ID="lblDate" runat="server" CssClass="col-md-2 control-label">Resolved On :</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="resolvedOn" CssClass="form-control" ReadOnly="True" />
                        <br />
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="form-group">
                    <asp:Label ID="lblDropdown" runat="server" CssClass="col-md-2 control-label">Feedback Status :</asp:Label>
                    <div class="col-md-10">
                        <asp:DropDownList ID="DropDownListStatus" runat="server">
                            <asp:ListItem Value="Open">Open</asp:ListItem>
                            <asp:ListItem Value="Pending">Pending</asp:ListItem>
                            <asp:ListItem Value="Resolved">Resolved</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </td>
        </tr>
    </table>


</asp:Content>
