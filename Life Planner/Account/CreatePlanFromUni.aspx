<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreatePlanFromUni.aspx.cs" Inherits="Life_Planner.Account.CreatePlanFromUni" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <asp:Panel ID="Panel1" runat="server" Height="831px">

        <section id="createPlan">
            <h2></>Create Plan</h2>

            <h5>Step 2: Select a University</h5>


            <div id="alert_placeholder" runat="server" visible="false">
            </div>
            <hr />

            <div>
                <div class="form-group">
                    <asp:Label ID="lblUniFilterLoc" runat="server" CssClass="col-md-3 control-label" Text="Filter by Location:"></asp:Label>
                </div>
            </div>


            <div>
                <div class="form-group">
                    <div class="col-md-10">
                        <table class="nav-justified" style="width: 79%">
                            <tr>
                                <td style="width: 88px">
                                    <br />
                                    <asp:Button ID="btnUniNorth" CssClass="btn btn-default" Text="North" runat="server" OnClick="btn_UniNorth" CausesValidation="false"/>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnUniSouth" CssClass="btn btn-default" Text="South" runat="server" OnClick="btn_UniSouth" CausesValidation="false"/>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnUniEast" CssClass="btn btn-default" Text="East" runat="server" OnClick="btn_UniEast" CausesValidation="false"/>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnUniWest" CssClass="btn btn-default" Text="West" runat="server" OnClick="btn_UniWest" CausesValidation="false"/>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnUniNone" CssClass="btn btn-default" Text="None" runat="server" OnClick="btn_UniNone" CausesValidation="false" />
                                    <br />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div class="form-group">
                    <div style="width: 100%; height: 244px; overflow: auto">
                        <asp:GridView ID="uniTable" runat="server" AutoGenerateColumns="False" BackColor="White" CssClass="table table-striped table-hover" EnableTheming="False" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="1166px" DataKeyNames="school_name" OnSelectedIndexChanged="uniGridView_SelectedIndexChanging" AutoGenerateSelectButton="True">
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
                                <asp:BoundField DataField="school_name" HeaderText="University Name" InsertVisible="False" ReadOnly="True" SortExpression="school_name" Visible="true" />
                                <asp:BoundField DataField="zone_code" HeaderText="Area" SortExpression="zone_code" />
                                <asp:BoundField DataField="dgp_code" HeaderText="Location" SortExpression="dgp_code" />
                                <asp:BoundField DataField="url_address" HeaderText="URL" SortExpression="url_address" />
                            </Columns>
                        </asp:GridView>
                        <br />
                    </div>
                </div>

            </div>

            <div class="form-group">
                <div class="col-lg-10 col-lg-offset-10">
                    <br />
                    <asp:Button ID="btnUniSubmit" CssClass="btn btn-primary" Text="End" runat="server" OnClick="btnUniSubmitPlan" Width="93px" Visible="False" />
                </div>
            </div>



        </section>
    </asp:Panel>
</asp:Content>
