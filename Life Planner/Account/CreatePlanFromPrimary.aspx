<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreatePlanFromPrimary.aspx.cs" Inherits="Life_Planner.Account.CreatePlanFromPrimary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" Height="636px">
        <section id="createPlan">
            <h2></>Create Plan</h2>

            <h5>Step 2: Please filter recommendation of Primary Schools accordingly</h5>


            <div id="alert_placeholder" runat="server" visible="false">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                <asp:Literal runat="server" ID="alertText" />
            </div>
            <hr />

            <div>
                <div class="form-group">
                    <asp:Label ID="lblSelectPriLocation" runat="server" CssClass="col-md-3 control-label" Text="Filtering of Primary Schools:"></asp:Label>
                </div>
            </div>

            <div class="form-group">
                <div class="col-md-10">
                    <br />
                    <table class="nav-justified" style="width: 79%">
                        <tr>
                            <td style="width: 88px">
                                <asp:Button ID="btnPriNorth" CssClass="btn btn-default" Text="North" runat="server" OnClick="clearBtn_Click" CausesValidation="false" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnPriSouth" CssClass="btn btn-default" Text="South" runat="server" OnClick="clearBtn_Click" CausesValidation="false" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnPriEast" CssClass="btn btn-primary" Text="East" runat="server" OnClick="submitFeedback_Click" CausesValidation="false" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnPriWest" CssClass="btn btn-default" Text="West" runat="server" OnClick="clearBtn_Click" CausesValidation="false" />

                            </td>
                        </tr>
                    </table>
                </div>
            </div>


        </section>
    </asp:Panel>

</asp:Content>
