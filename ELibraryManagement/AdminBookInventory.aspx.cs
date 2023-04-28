using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ELibraryManagement
{
    public partial class AdminBookInventory : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        static string global_filepath;
        static int global_acutal_stock, global_current_stock, global_issued_book;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillAuthorPublisherValues();
            }

            GridView1.DataBind();
        }

        protected void Button_Add_Click(object sender, EventArgs e)
        {
            if (checkIfBookExists())
            {
                Response.Write("<script>alert('Book Already Exists!!!');</script>");
            }
            else
            {
                addNewBook();
            }
        }

        protected void Button_Update_Click(object sender, EventArgs e)
        {
            if (checkIfBookExists())
            {
                updateBookById();
            }
            else
            {
                Response.Write("<script>alert('Book Does not Exists!!!');</script>");
            }
        }

        protected void Button_Delete_Click(object sender, EventArgs e)
        {
            if (checkIfBookExists())
            {
                deleteBook();
            }
            else
            {
                Response.Write("<script>alert('Book Does not Exists!!!');</script>");
            }
        }

        protected void Button_Go_Click(object sender, EventArgs e)
        {
            if (checkIfBookExists())
            {
                getBookById();
            }
            else
            {
                Response.Write("<script>alert('Book Does not Exists!!!');</script>");
            }
        }

        void addNewBook()
        {
            try
            {
                string genres = "";

                foreach (int i in ListBox_Genre.GetSelectedIndices())
                {
                    genres = genres + ListBox_Genre.Items[i] + ",";
                }
                //genres = a,b,c,
                genres = genres.Remove(genres.Length - 1); //to remove last ,

                string filepath = "~/book_inventory/example_book.jpg";
                string filename = Path.GetFileName(FileUpload_Image.PostedFile.FileName);
                FileUpload_Image.SaveAs(Server.MapPath("book_inventory/" + filename));
                filepath = "~/book_inventory/" + filename;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO book_master_tbl" +
                    "(book_id,book_name,genre,author_name,publisher_name,publish_date,language,edition,book_cost,no_of_pages,book_description,actual_stock,current_stock,book_img_link)" +
                    " VALUES (@book_id,@book_name,@genre,@author_name,@publisher_name,@publish_date,@language,@edition,@book_cost,@no_of_pages,@book_description,@actual_stock,@current_stock,@book_img_link)", con);

                cmd.Parameters.AddWithValue("@book_id", TextBox_BookId.Text.Trim());
                cmd.Parameters.AddWithValue("@book_name", TextBox_BookName.Text.Trim());
                cmd.Parameters.AddWithValue("@genre", genres);
                cmd.Parameters.AddWithValue("@author_name", DropDownList_AuthorName.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@publisher_name", DropDownList_PublisherName.Text.Trim());
                cmd.Parameters.AddWithValue("@publish_date", TextBox_PublishDate.Text.Trim());
                cmd.Parameters.AddWithValue("@language", DropDownList_Language.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@edition", TextBox_Edition.Text.Trim());
                cmd.Parameters.AddWithValue("@book_cost", TextBox_BookCost.Text.Trim());
                cmd.Parameters.AddWithValue("@no_of_pages", TextBox_Pages.Text.Trim());
                cmd.Parameters.AddWithValue("@book_description", TextBox_BookDescription.Text.Trim());
                cmd.Parameters.AddWithValue("@actual_stock", TextBox_ActualStock.Text.Trim());
                cmd.Parameters.AddWithValue("@current_stock", TextBox_ActualStock.Text.Trim());
                cmd.Parameters.AddWithValue("@book_img_link", filepath);

                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('Book Added Successfully!!!');</script>");

                GridView1.DataBind();
                clearForm();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void fillAuthorPublisherValues()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                //get author dropdownlist
                SqlCommand cmd = new SqlCommand("SELECT author_name from author_master_tbl;", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DropDownList_AuthorName.DataSource = dt;
                DropDownList_AuthorName.DataValueField = "author_name";
                DropDownList_AuthorName.DataBind();

                //get publisher dropdownlist
                cmd = new SqlCommand("SELECT publisher_name from publisher_master_tbl;", con);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                DropDownList_PublisherName.DataSource = dt;
                DropDownList_PublisherName.DataValueField = "publisher_name";
                DropDownList_PublisherName.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        bool checkIfBookExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM book_master_tbl WHERE book_id='" + TextBox_BookId.Text.Trim() + "' OR book_name='" + TextBox_BookName.Text.Trim() + "';", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

                return false;
            }
        }

        void getBookById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM book_master_tbl WHERE book_id='" + TextBox_BookId.Text.Trim() + "'", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBox_BookName.Text = dt.Rows[0]["book_name"].ToString().Trim();
                    TextBox_PublishDate.Text = dt.Rows[0]["publish_date"].ToString().Trim();
                    TextBox_BookCost.Text = dt.Rows[0]["book_cost"].ToString().Trim();
                    TextBox_Pages.Text = dt.Rows[0]["no_of_pages"].ToString().Trim();
                    TextBox_BookDescription.Text = dt.Rows[0]["book_description"].ToString().Trim();
                    TextBox_ActualStock.Text = dt.Rows[0]["actual_stock"].ToString().Trim();
                    TextBox_CurrentStock.Text = dt.Rows[0]["current_stock"].ToString().Trim();
                    TextBox_IssuedBook.Text = "" + (Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString().Trim()) - Convert.ToInt32(dt.Rows[0]["current_stock"].ToString().Trim()));

                    DropDownList_Language.SelectedValue = dt.Rows[0]["language"].ToString().Trim();
                    DropDownList_AuthorName.SelectedValue = dt.Rows[0]["author_name"].ToString().Trim();
                    DropDownList_PublisherName.SelectedValue = dt.Rows[0]["publisher_name"].ToString().Trim();

                    //split the , and mark the listbox item true if match
                    ListBox_Genre.ClearSelection();
                    string[] genre = dt.Rows[0]["genre"].ToString().Trim().Split(',');
                    for(int i=0; i<genre.Length; i++)
                    {
                        for(int j=0; j < ListBox_Genre.Items.Count; j++)
                        {
                            if (ListBox_Genre.Items[j].ToString() == genre[i])
                            {
                                ListBox_Genre.Items[j].Selected = true;
                            }
                        }
                    }

                    global_acutal_stock = Convert.ToInt32(dt.Rows[0]["actual_stock"].ToString().Trim());
                    global_current_stock = Convert.ToInt32(dt.Rows[0]["current_stock"].ToString().Trim());
                    global_issued_book = (global_acutal_stock- global_current_stock);
                    global_filepath = dt.Rows[0]["book_img_link"].ToString().Trim(); ;

                }
                else
                {
                    Response.Write("<script>alert('Invalid Book ID!');</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void updateBookById()
        {
            try
            {
                int actual_stock = Convert.ToInt32(TextBox_ActualStock.Text.Trim());
                int current_stock = Convert.ToInt32(TextBox_CurrentStock.Text.Trim());

                if (global_acutal_stock == actual_stock)
                {
                    //do nothing, since the book stock is not changing
                }
                else
                {
                    if (actual_stock < global_issued_book)
                    {
                        Response.Write("<script>alert('Actual Stock Value Cannot Less than the Issued Book');</script>");
                        return;
                    }
                    else
                    {
                        current_stock = actual_stock - global_issued_book;
                        TextBox_CurrentStock.Text = "" + current_stock;   
                    }
                }

                //add all selected genre together example: "genre1,genre2,genre3"
                string genres = "";
                foreach(int i in ListBox_Genre.GetSelectedIndices())
                {
                    genres = genres + ListBox_Genre.Items[i] + ",";
                }
                genres = genres.Remove(genres.Length - 1);

                string filepath = "~/book_inventory/book1.jpg";
                string filename = Path.GetFileName(FileUpload_Image.PostedFile.FileName);
                if (filename == "" || filename == null)
                {
                    filepath = global_filepath;
                }
                else
                {
                    FileUpload_Image.SaveAs(Server.MapPath("book_inventory/" + filename));
                    filepath = "~/book_inventory/" + filename;
                }

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("UPDATE book_master_tbl SET book_name=@book_name,genre=@genre,author_name=@author_name,publisher_name=@publisher_name,publish_date=@publish_date,language=@language,edition=@edition,book_cost=@book_cost,no_of_pages=@no_of_pages,book_description=@book_description,actual_stock=@actual_stock,current_stock=@current_stock,book_img_link=@book_img_link " +
                    "WHERE book_id='" + TextBox_BookId.Text.Trim() + "'", con);

                cmd.Parameters.AddWithValue("book_name", TextBox_BookName.Text);
                cmd.Parameters.AddWithValue("genre", genres);
                cmd.Parameters.AddWithValue("@author_name", DropDownList_AuthorName.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@publisher_name", DropDownList_PublisherName.Text.Trim());
                cmd.Parameters.AddWithValue("@publish_date", TextBox_PublishDate.Text.Trim());
                cmd.Parameters.AddWithValue("@language", DropDownList_Language.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@edition", TextBox_Edition.Text.Trim());
                cmd.Parameters.AddWithValue("@book_cost", TextBox_BookCost.Text.Trim());
                cmd.Parameters.AddWithValue("@no_of_pages", TextBox_Pages.Text.Trim());
                cmd.Parameters.AddWithValue("@book_description", TextBox_BookDescription.Text.Trim());
                cmd.Parameters.AddWithValue("@actual_stock", TextBox_ActualStock.Text.Trim());
                cmd.Parameters.AddWithValue("@current_stock", current_stock.ToString());
                cmd.Parameters.AddWithValue("book_img_link", filepath);

                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('Book Updated Successfullly');</script>");

                GridView1.DataBind(); //update gridview
                clearForm();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void deleteBook()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("DELETE from book_master_tbl " +
                    "WHERE book_id='" + TextBox_BookId.Text.Trim() + "'", con);

                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('Book Deleted Successfully!');</script>");

                GridView1.DataBind(); //update gridview
                clearForm();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void clearForm()
        {
            TextBox_BookId.Text = "";
            TextBox_BookName.Text = "";
            TextBox_Edition.Text = "";
            TextBox_BookCost.Text = "";
            TextBox_Pages.Text = "";
            TextBox_ActualStock.Text = "0";
            TextBox_CurrentStock.Text = "0";
            TextBox_IssuedBook.Text = "0";
            TextBox_BookDescription.Text = "";
            DropDownList_Language.SelectedIndex = -1;
            DropDownList_AuthorName.SelectedIndex = -1;
            DropDownList_PublisherName.SelectedIndex = -1;
            ListBox_Genre.SelectedIndex = -1;
        }
    }
}