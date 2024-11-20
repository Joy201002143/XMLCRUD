<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="XMLCRUD._Default" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/js/select2.min.js"></script>
    <!-- Select2 library -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/css/select2.min.css" rel="stylesheet" />
    <!-- Select2 CSS -->

    <script type="text/javascript">
        $(document).ready(function () {
            // Initialize Select2 on the DropDownList
            $(".searchable-dropdown").select2({
                placeholder: "Select a Designation",
                allowClear: true
            });

        });
        $(".searchable-dropdown").on("select2:unselect", function (e) {
            // Optional: Reset any other UI elements if needed
            $(this).val(null).trigger('change');  // This will reset the dropdown to the placeholder text
        });
    </script>
    <center>

        <asp:Label ID="Label2" runat="server" Text="CRUD using Employee XML"></asp:Label>

        <asp:TextBox ID="txtID" runat="server" Placeholder="ID" Style="display: none;"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox ID="txtName" runat="server" Placeholder="Name"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox ID="txtDesignation" runat="server" Placeholder="Designation"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox ID="txtSalary" runat="server" Placeholder="Salary"></asp:TextBox>


        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" Width="105px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="View All" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="You can also select by DropDown"></asp:Label>
        <br />
        <br />                                                                                                                      
        <asp:DropDownList ID="DropDownList1" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="searchable-dropdown" Width="188px" >
        </asp:DropDownList>
        <br />
        <br />
        <asp:TextBox ID="txtDel" runat="server" Placeholder="Inter ID to Delete"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" />
        <br />
        <br />
   <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" 
    DataKeyNames="Id" 
    OnRowEditing="gvEmployees_RowEditing" 
    OnRowDeleting="gvEmployees_RowDeleting" 
    OnRowUpdating="gvEmployees_RowUpdating" 
    OnRowCancelingEdit="gvEmployees_RowCancelingEdit">
    <Columns>
        <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="Designation" HeaderText="Designation" />
        <asp:BoundField DataField="Salary" HeaderText="Salary" />

        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
    </Columns>
</asp:GridView>

    </center>
</asp:Content>
