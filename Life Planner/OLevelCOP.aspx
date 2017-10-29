<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OLevelCOP.aspx.cs" Inherits="Life_Planner.OLevelCOP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<p>
		&nbsp;</p>
	<p>
		O Level Points:
		<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
		<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Go" />
	</p>
	<p>
		<asp:Table ID="Table1" runat="server"  BorderColor="Black" BorderWidth="1px" GridLines="Both" BorderStyle="Solid" CellPadding="1" CellSpacing="1" HorizontalAlign="Left">
		</asp:Table>
	</p>
</asp:Content>
