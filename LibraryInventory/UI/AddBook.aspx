<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddBook.aspx.cs" Inherits="LibraryInventory.UI.AddBook" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red" Text=""></asp:Label>

            <asp:TextBox ID="TextBoxTitle" runat="server" Placeholder="Title"></asp:TextBox>
            <asp:TextBox ID="TextBoxAuthor" runat="server" Placeholder="Author"></asp:TextBox>
            <asp:TextBox ID="TextBoxISBN" runat="server" Placeholder="ISBN"></asp:TextBox>
            <asp:TextBox ID="TextBoxYear" runat="server" Placeholder="Publication Year"></asp:TextBox>
            <asp:TextBox ID="TextBoxQuantity" runat="server" Placeholder="Quantity"></asp:TextBox>
            <asp:DropDownList ID="DropDownListCategory" runat="server"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorCategory" runat="server"
                ControlToValidate="DropDownListCategory"
                InitialValue="0"
                ErrorMessage="Please select a valid category."
                ForeColor="Red">
            </asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
            <asp:Button ID="ButtonSave" runat="server" Text="Save" OnClick="ButtonSave_Click" />

        </div>
    </form>
</body>
</html>
