using LibraryInventory.Data;
using LibraryInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibraryInventory.UI
{
    public partial class AddBook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategories();
            }
        }

        private void BindCategories()
        {
            var categoryRepository = new Repository<Category>(new LibraryContext());
            DropDownListCategory.DataSource = categoryRepository.GetAll();
            DropDownListCategory.DataTextField = "Name"; // Displayed name
            DropDownListCategory.DataValueField = "Id"; // Value used for CategoryId
            DropDownListCategory.DataBind();

            // Optionally, add a default "Select Category" item
            DropDownListCategory.Items.Insert(0, new ListItem("--Select Category--", "0"));
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (DropDownListCategory.SelectedValue == "0")
            {
                // Show an error message or handle invalid selection
                ErrorMessage.Text = "Please select a valid category.";
                return;
            }

            var repository = new Repository<Book>(new LibraryContext());
            var book = new Book
            {
                Title = TextBoxTitle.Text,
                Author = TextBoxAuthor.Text,
                ISBN = TextBoxISBN.Text,
                PublicationYear = int.Parse(TextBoxYear.Text),
                Quantity = int.Parse(TextBoxQuantity.Text),
                CategoryId = int.Parse(DropDownListCategory.SelectedValue)
            };
            repository.Add(book);
            Response.Redirect("Home.aspx");
        }


    }
}