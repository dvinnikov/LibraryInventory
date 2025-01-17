using LibraryInventory.Data;
using LibraryInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace LibraryInventory.UI
{
    public partial class DeleteBook : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int bookId = int.Parse(Request.QueryString["id"]);
                LoadBook(bookId);
            }
        }

        private void LoadBook(int bookId)
        {
            var repository = new Repository<Book>(new LibraryContext());
            var book = repository.GetById(bookId);
            LabelBookDetails.Text = $"Title: {book.Title}, Author: {book.Author}, ISBN: {book.ISBN}";
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            int bookId = int.Parse(Request.QueryString["id"]);
            var repository = new Repository<Book>(new LibraryContext());
            repository.Delete(bookId);
            Response.Redirect("Home.aspx");
        }
    }
}