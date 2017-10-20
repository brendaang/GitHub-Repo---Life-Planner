<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SendFeedback.aspx.cs" Inherits="Life_Planner.Feedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3></>We value your feedback !</h3>
    <b>
        <br>Thank you for taking your time to fill in our feedback form.</br>
        <p>Your feedback is very important to us!</p>
    </b>
    <asp:Label ID="feedbackACK" runat="server" Width="1000px" Style="background-color: #044586; color: #ffffff; font-size: large; text-align: center;"></asp:Label>
    <hr />
    <div class="form-group">
        <asp:Label ID="lblDate" runat="server" CssClass="col-md-2 control-label">Date and Time</asp:Label>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="txtDatetime" CssClass="form-control" ReadOnly="True" />
            <br />
        </div>
    </div>
    <br />

    <div class="form-group">
        <asp:Label ID="lblAuthor" runat="server" CssClass="col-md-2 control-label">Submitted by</asp:Label>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="submittedBy" CssClass="form-control" ReadOnly="True" />
            <br />
        </div>
    </div>
    <br />

    <div class="form-group">
        <asp:Label runat="server" CssClass="col-md-2 control-label">Feedback Issue</asp:Label>
        <div class="col-md-10">
            <asp:TextBox runat="server" ID="txtfeedbackIssue" CssClass="form-control" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtfeedbackIssue"
                CssClass="text-danger" ErrorMessage="The feedback issue field is required." />
        </div>
    </div>
    <div class="form-group">
        <asp:Label ID="lblThreadDesc" runat="server" CssClass="col-md-2 control-label">Feedback Content</asp:Label>
        <div class="col-md-10">
            <asp:TextBox ID="txtFeedbackContent" runat="server" Rows="5" TextMode="MultiLine" CssClass="form-control" Width="500px" onkeyup="textCounter(this,'counter',2500);"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFeedbackContent" CssClass="text-danger" ErrorMessage="The feedback content field is required." />
            <table class="nav-justified">
                <tr>
                    <td style="width: 316px" class="modal-sm">&nbsp;</td>
                    <td>
                        <input disabled maxlength="3" size="3" value="2500" id="counter" style="background-color: #ffffff; text-align: right; border: 1px #ffffff">
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
                    <br />
                </tr>
            </table>
        </div>
    </div>
    <br />
    <div class="form-group">
        <asp:Label ID="lbl" runat="server" CssClass="col-md-2 control-label"></asp:Label>
        <div class="col-md-10">
            <table class="nav-justified" style="width: 79%">
                <tr>
                    <td style="width: 88px">
                        <asp:Button ID="clearBtn" runat="server" CssClass="form-control" Text="Clear Feedback" Width="200px" OnClick="clearBtn_Click" CausesValidation="false" /></td>
                    <td class="modal-sm" style="width: 297px">
                        <asp:Button ID="submitFeedback" runat="server" CssClass="form-control" Text="Submit Feedback" Width="200px" OnClick="submitFeedback_Click" Style="background-color: #044586; color: #ffffff" /></td>
                </tr>
            </table>
            <hr />
        </div>
    </div>
</asp:Content>
