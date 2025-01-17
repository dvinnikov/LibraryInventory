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
    public partial class AddCategory : System.Web.UI.Page
    {
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxName.Text))
            {
                ErrorMessage.Text = "Category name is required.";
                return;
            }

            try
            {
                var repository = new Repository<Category>(new LibraryContext());
                var category = new Category
                {
                    Name = TextBoxName.Text,
                    Description = TextBoxDescription.Text
                };
                repository.Add(category);
                Response.Redirect("CategoryList.aspx"); // Redirect to the category list page
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = $"An error occurred: {ex.Message}";
            }
        }

    }
}