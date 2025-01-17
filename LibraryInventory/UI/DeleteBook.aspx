<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteBook.aspx.cs" Inherits="LibraryInventory.UI.DeleteBook" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="LabelBookDetails" runat="server"></asp:Label>
            <asp:Button ID="ButtonDelete" runat="server" Text="Delete" OnClick="ButtonDelete_Click" />
        </div>
    </form>
</body>
</html>
