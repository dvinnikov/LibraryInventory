<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCategory.aspx.cs" Inherits="LibraryInventory.UI.AddCategory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Category</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Add Category</h1>
            
            <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red"></asp:Label>
            <br />
            
            <asp:TextBox ID="TextBoxName" runat="server" Placeholder="Category Name"></asp:TextBox><br />
            <asp:TextBox ID="TextBoxDescription" runat="server" Placeholder="Description"></asp:TextBox><br />
            
            <asp:Button ID="ButtonSave" runat="server" Text="Save" OnClick="ButtonSave_Click" /><br /><br />
            <asp:Button ID="BackToMenuButton" runat="server" Text="Back to Main Menu" PostBackUrl="Home.aspx" />
            <asp:Button ID="CategoryList" runat="server" Text="Category List" PostBackUrl="CategoryList.aspx" />
        </div>
    </form>
</body>
</html>
