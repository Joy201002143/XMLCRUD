<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="XMLCRUD._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <center>

        <asp:Label ID="Label2" runat="server" Text="CRUD using Employee XML"></asp:Label>
        <br />
        <br />
        <br />

    <asp:TextBox ID="txtID" runat="server" Placeholder="ID"></asp:TextBox>
        <br />
    <br />
    <asp:TextBox ID="txtName" runat="server" Placeholder="Name"></asp:TextBox>
        <br />
    <br />
    <asp:TextBox ID="txtDepartment" runat="server" Placeholder="Department"></asp:TextBox>
    <br />
    <br />
    <br />
    <br />
    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" Width="105px" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" />
    <br />
        <br />
        <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="View All" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="You can also select by DropDown"></asp:Label>
        <br />
    <br />
        <asp:DropDownList ID="DropDownList1" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
    <br />
    <br />
    <asp:TextBox ID="txtSearch" runat="server" Placeholder="Search by Name"></asp:TextBox>
&nbsp;&nbsp;
    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
    <br />
    <br />
    <asp:GridView ID="gvEmployees" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
        </center>
</asp:Content>
