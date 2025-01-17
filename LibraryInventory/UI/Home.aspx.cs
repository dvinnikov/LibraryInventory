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
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBooks();
            }
        }

        private void BindBooks()
        {
            var repository = new Repository<Book>(new LibraryContext());
            GridViewBooks.DataSource = repository.GetAll();
            GridViewBooks.DataBind();
        }

        protected void GridViewBooks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                Response.Redirect($"EditBook.aspx?id={e.CommandArgument}");
            }
            else if (e.CommandName == "Delete")
            {
                Response.Redirect($"DeleteBook.aspx?id={e.CommandArgument}");
            }
        }

    }
}