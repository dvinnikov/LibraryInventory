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
    public partial class CategoryList : System.Web.UI.Page
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
            var repository = new Repository<Category>(new LibraryContext());
            GridViewCategories.DataSource = repository.GetAll();
            GridViewCategories.DataBind();
        }

        protected void GridViewCategories_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                Response.Redirect($"EditCategory.aspx?id={e.CommandArgument}");
            }
            else if (e.CommandName == "Delete")
            {
                int categoryId = Convert.ToInt32(e.CommandArgument);
                var repository = new Repository<Category>(new LibraryContext());
                repository.Delete(categoryId);
                BindCategories();
            }
        }
    }
}