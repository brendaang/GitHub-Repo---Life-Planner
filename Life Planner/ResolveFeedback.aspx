<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResolveFeedback.aspx.cs" Inherits="Life_Planner.ResolveFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.4/js/bootstrap-select.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.12.4/css/bootstrap-select.css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $(".selectpicker").selectpicker();
        });
    </script>

    <h2>Feedback Details</h2>
    <h5>Resolve this feedback.</h5>

    <div id="alert_placeholder" runat="server" visible="false">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
        <asp:Literal runat="server" ID="resolveACK" />
    </div>
    <hr />
    <table class="nav-justified">
        <tr>
            <td>
                <div class="form-group">
                    <asp:Label ID="lblFbkTime" runat="server" CssClass="col-md-2 control-label">Feedback Date and Time :</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox runat="server" ID="fbkDatetime" CssClass="form-control" ReadOnly="True" />
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
                        <asp:TextBox runat="server" ID="txtfeedbackIssue" CssClass="form-control" ReadOnly="True" />
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
                        <asp:TextBox ID="fbkContent" runat="server" Rows="4" TextMode="MultiLine" CssClass="form-control" Width="500px" ReadOnly="True"></asp:TextBox>
                        <br />
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="form-group">
                    <asp:Label ID="lblNotes" runat="server" CssClass="col-md-2 control-label">Additional Notes :</asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtAddnotes" runat="server" Rows="8" TextMode="MultiLine" CssClass="form-control" Width="500px" onkeyup="textCounter(this,'counter',4000);"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAddnotes" CssClass="text-danger" ErrorMessage="The 'Additional Notes' field is required." />
                        <table class="nav-justified">
                            <tr>
                                <td style="width: 316px" class="modal-sm">&nbsp;</td>
                                <td>
                                    <input disabled maxlength="3" size="3" value="4000" id="counter" style="background-color: #ffffff; text-align: right; border: 1px #ffffff">
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
                    <asp:Label ID="fbkStatusLabel" runat="server" Text="" Visible="false"></asp:Label>
                    <div class="dropdown">
                        <div class="col-md-10">
                            <asp:DropDownList ID="DropDownListStatus" runat="server" CssClass="selectpicker">
                                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                <asp:ListItem Value="Resolved">Resolved</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <div class="form-group">
        <asp:Label ID="lbl" runat="server" CssClass="col-md-2 control-label"></asp:Label>
        <div class="col-md-10">
            <table class="nav-justified" style="width: 79%">
                <tr>
                    <td style="width: 88px">
                        <asp:Button ID="cancelBtn" runat="server" CssClass="form-control" Text="Cancel" Width="200px" OnClick="cancelBtn_Click" CausesValidation="false" /></td>
                    <td class="modal-sm" style="width: 297px">
                        <asp:Button ID="resolvedBtn" runat="server" CssClass="form-control" Text="Feedback Resolved" Width="200px" OnClick="resolvedBtn_Click" Style="background-color: #4ab0ea; color: #ffffff" />
                    </td>
                </tr>
            </table>
            <hr />
        </div>
    </div>
</asp:Content>
