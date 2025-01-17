<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditBook.aspx.cs" Inherits="LibraryInventory.UI.EditBook" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBoxTitle" runat="server" Placeholder="Title"></asp:TextBox><br />
            <asp:TextBox ID="TextBoxAuthor" runat="server" Placeholder="Author"></asp:TextBox><br />
            <asp:TextBox ID="TextBoxISBN" runat="server" Placeholder="ISBN"></asp:TextBox><br />
            <asp:TextBox ID="TextBoxYear" runat="server" Placeholder="Publication Year"></asp:TextBox><br />
            <asp:TextBox ID="TextBoxQuantity" runat="server" Placeholder="Quantity"></asp:TextBox><br />
            <asp:DropDownList ID="DropDownListCategory" runat="server"></asp:DropDownList><br />
            <asp:Button ID="ButtonSave" runat="server" Text="Save" OnClick="ButtonSave_Click" />
            <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
