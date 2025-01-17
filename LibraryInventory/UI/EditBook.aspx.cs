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
    public partial class EditBook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int bookId;
                if (int.TryParse(Request.QueryString["id"], out bookId))
                {
                    BindCategories();
                    LoadBook(bookId);
                }
                else
                {
                    ErrorMessage.Text = "Invalid book ID.";
                }
            }
        }

        private void BindCategories()
        {
            var categoryRepository = new Repository<Category>(new LibraryContext());
            DropDownListCategory.DataSource = categoryRepository.GetAll();
            DropDownListCategory.DataTextField = "Name";
            DropDownListCategory.DataValueField = "Id";
            DropDownListCategory.DataBind();
        }

        private void LoadBook(int bookId)
        {
            var bookRepository = new Repository<Book>(new LibraryContext());
            var book = bookRepository.GetById(bookId);

            if (book != null)
            {
                TextBoxTitle.Text = book.Title;
                TextBoxAuthor.Text = book.Author;
                TextBoxISBN.Text = book.ISBN;
                TextBoxYear.Text = book.PublicationYear.ToString();
                TextBoxQuantity.Text = book.Quantity.ToString();
                DropDownListCategory.SelectedValue = book.CategoryId.ToString();
            }
            else
            {
                ErrorMessage.Text = "Book not found.";
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            int bookId;
            if (!int.TryParse(Request.QueryString["id"], out bookId))
            {
                ErrorMessage.Text = "Invalid book ID.";
                return;
            }

            try
            {
                var bookRepository = new Repository<Book>(new LibraryContext());
                var book = bookRepository.GetById(bookId);

                if (book != null)
                {
                    book.Title = TextBoxTitle.Text;
                    book.Author = TextBoxAuthor.Text;
                    book.ISBN = TextBoxISBN.Text;
                    book.PublicationYear = int.Parse(TextBoxYear.Text);
                    book.Quantity = int.Parse(TextBoxQuantity.Text);
                    book.CategoryId = int.Parse(DropDownListCategory.SelectedValue);

                    bookRepository.Update(book);
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    ErrorMessage.Text = "Book not found.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = $"An error occurred: {ex.Message}";
            }
        }

    }
}