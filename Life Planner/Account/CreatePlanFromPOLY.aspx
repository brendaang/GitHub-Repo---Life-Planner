<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreatePlanFromPOLY.aspx.cs" Inherits="Life_Planner.Account.CreatePlanFromJCPOLY1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" Height="831px">

            <section id="createPlan">
                <h2></>Create Plan</h2>

                <h5>Step 2: Selecting a Polytechnic</h5>


                <div id="alert_placeholder" runat="server" visible="false">
                </div>
                <hr />
                <div>

                    <%--////2--%>
                    <div class="form-group">
                        <div class="col-md-10">
                            <asp:Label ID="lblITEJCPOLYSchFilterByLoc" runat="server" CssClass="col-md-3 control-label" Text="Filter by Location:"></asp:Label>
                            <br />
                            <br />
                            <table class="nav-justified" style="width: 79%">
                                <tr>
                                    <td style="width: 88px">
                                        <asp:Button ID="btnPOLYNorth" CssClass="btn btn-default" Text="North" runat="server" OnClick="btn_POLYNorth" CausesValidation="false"/>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnPOLYSouth" CssClass="btn btn-default" Text="South" runat="server" OnClick="btn_POLYSouth" CausesValidation="false"/>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnPOLYEast" CssClass="btn btn-default" Text="East" runat="server" OnClick="btn_POLYEast" CausesValidation="false"/>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnPOLYWest" CssClass="btn btn-default" Text="West" runat="server" OnClick="btn_POLYWest" CausesValidation="false"/>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnPOLYNone" CssClass="btn btn-default" Text="None" runat="server" OnClick="btn_POLYNone" CausesValidation="false" />
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>

                    <div class="form-group">
                        <div style="width: 100%; height: 250px; overflow: auto">
                            <br />
                            <br />
                            <asp:GridView ID="POLYTable" runat="server" AutoGenerateColumns="False" BackColor="White" CssClass="table table-striped table-hover" EnableTheming="False" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="1166px" DataKeyNames="school_name" OnSelectedIndexChanged="POLYGridView_SelectedIndexChanging" AutoGenerateSelectButton="True">
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
                                    <asp:BoundField DataField="school_name" HeaderText="ITE/JC/POLY Name" InsertVisible="False" ReadOnly="True" SortExpression="school_name" Visible="true" />
                                    <asp:BoundField DataField="zone_code" HeaderText="Area" SortExpression="zone_code" />
                                    <asp:BoundField DataField="dgp_code" HeaderText="Location" SortExpression="dgp_code" />
                                    <asp:BoundField DataField="url_address" HeaderText="URL" SortExpression="url_address" />
                                </Columns>
                            </asp:GridView>
                            <br />
                        </div>
                    </div>

                </div>

                
                <br />
                <br />

                
                <%--******--%>
                <div>
                    <div class="form-group">
                        <div style="width: 100%; height: 400px; overflow: auto">
                            <asp:GridView ID="PolyCoursesTable" runat="server" AutoGenerateColumns="False" BackColor="White" CssClass="table table-striped table-hover" EnableTheming="False" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="1166px" DataKeyNames="course_name" OnSelectedIndexChanged="POLYCourseGridView_SelectedIndexChanging" AutoGenerateSelectButton="True" Visible="False">
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
                                    <asp:BoundField DataField="course_name" HeaderText="Course Name" InsertVisible="False" ReadOnly="True" SortExpression="course_name" Visible="true" />
                                    <asp:BoundField DataField="school" HeaderText="Faculty" SortExpression="school" />
                                    <asp:BoundField DataField="gceo_cut_off" HeaderText="Cut-off Point" SortExpression="gceo_cut_off" />
                                </Columns>
                            </asp:GridView>
                            <br />
                        </div>
                    </div>


                </div>

                <div class="form-group">
                    <div class="col-lg-10 col-lg-offset-10">
                        <br />
                        <asp:Button ID="btnPOLYCont2" CssClass="btn btn-default" Text="Continue" runat="server" OnClick="btnPOLYContinuePlanning2" CausesValidation="false" Width="93px" Visible="False" />
                        &nbsp;
                        <asp:Button ID="btnPOLYSubmit2" CssClass="btn btn-primary" Text="End" runat="server" OnClick="btnPOLYSubmitPlan2" Width="93px" Visible="False" />
                    </div>
                </div>


            </section>
        </asp:Panel>
</asp:Content>
