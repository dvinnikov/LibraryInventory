<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddBook.aspx.cs" Inherits="LibraryInventory.UI.AddBook" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Book</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Add Book</h1>
            
            <asp:Label ID="ErrorMessage" runat="server" ForeColor="Red"></asp:Label>
            <br />
            
            <asp:TextBox ID="TextBoxTitle" runat="server" Placeholder="Title"></asp:TextBox><br />
            <asp:TextBox ID="TextBoxAuthor" runat="server" Placeholder="Author"></asp:TextBox><br />
            <asp:TextBox ID="TextBoxISBN" runat="server" Placeholder="ISBN"></asp:TextBox><br />
            <asp:TextBox ID="TextBoxYear" runat="server" Placeholder="Publication Year"></asp:TextBox><br />
            <asp:TextBox ID="TextBoxQuantity" runat="server" Placeholder="Quantity"></asp:TextBox><br />
            
            <asp:DropDownList ID="DropDownListCategory" runat="server"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorCategory" runat="server" 
                ControlToValidate="DropDownListCategory" InitialValue="0" 
                ErrorMessage="Please select a valid category." ForeColor="Red"></asp:RequiredFieldValidator>
            <br /><br />

            <asp:Button ID="ButtonSave" runat="server" Text="Save" OnClick="ButtonSave_Click" /><br /><br />
            <asp:Button ID="BackToMenuButton" runat="server" Text="Back to Main Menu" PostBackUrl="Home.aspx" />
        </div>
    </form>
</body>
</html>
