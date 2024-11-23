<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="XMLCRUD._Default" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet" />

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.mzin.js"></script>
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
            // Optional:Reset any other UI elements if needed.
            $(this).val(null).trigger('change');  // This will reset the dropdown to the placeholder text
        });
    </script>
    <style>
        .grid-action-btn {
            margin-right: 10px; 
        }
    </style>
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
        <asp:DropDownList ID="DropDownList1" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="searchable-dropdown" Width="188px">
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
            OnRowCancelingEdit="gvEmployees_RowCancelingEdit"
            BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px"
            CellPadding="3" CellSpacing="2" Width="1174px">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Designation" HeaderText="Designation" />
                <asp:BoundField DataField="Salary" HeaderText="Salary" />

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" ToolTip="Edit" CssClass="grid-action-btn">
                    <i class="fas fa-edit"></i>
                        </asp:LinkButton>

                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');" CssClass="grid-action-btn">
                    <i class="fas fa-trash-alt"></i>
                        </asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" ToolTip="Update">
                    <i class="fas fa-check"></i>
                        </asp:LinkButton>
                        <!-- Cancel Icon -->
                        <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" ToolTip="Cancel">
                    <i class="fas fa-times"></i>
                        </asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />
        </asp:GridView>


    </center>
</asp:Content>
