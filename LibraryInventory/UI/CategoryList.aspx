<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoryList.aspx.cs" Inherits="LibraryInventory.UI.CategoryList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridViewCategories" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewCategories_RowCommand">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" CommandArgument='<%# Eval("Id") %>' Text="Edit" />
                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" CommandArgument='<%# Eval("Id") %>' Text="Delete" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Button ID="AddButton" runat="server" Text="Add Category" PostBackUrl="AddCategory.aspx" />
        </div>
    </form>
</body>
</html>
