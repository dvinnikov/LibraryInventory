<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="LibraryInventory.UI.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridViewBooks" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewBooks_RowCommand">
    <Columns>
        <asp:BoundField DataField="Title" HeaderText="Title" />
        <asp:BoundField DataField="Author" HeaderText="Author" />
        <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
        <asp:BoundField DataField="PublicationYear" HeaderText="Publication Year" />
        <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="EditButton" runat="server" CommandName="Edit" CommandArgument='<%# Eval("Id") %>' Text="Edit" />
                <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" CommandArgument='<%# Eval("Id") %>' Text="Delete" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:Button ID="AddButton" runat="server" Text="Add Book" PostBackUrl="AddBook.aspx" />

        </div>
    </form>
</body>
</html>
