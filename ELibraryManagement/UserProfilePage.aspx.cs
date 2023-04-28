using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace ELibraryManagement
{
    public partial class UserProfilePage : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["username"].ToString() == "" || Session["username"].ToString() == null) 
                {
                    Response.Write("<script>alert('Session Expired Please Login Again!');</script>");
                    Response.Redirect("UserLoginPage.aspx");
                }
                else
                {
                    getUserBookData();

                    if (!Page.IsPostBack)
                    {
                        getUserPersonalData();
                    }
                }

            }catch (Exception ex)
            {
                Response.Write("<script>alert('Session Expired Please Login Again!');</script>");
                Response.Redirect("UserLoginPage.aspx");
            }
        }

        protected void Button_Update_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["username"].ToString() == "" || Session["username"].ToString() == null)
                {
                    Response.Write("<script>alert('Session Expired Please Login Again!');</script>");
                    Response.Redirect("UserLoginPage.aspx");
                }
                else
                {
                    updateUserPersonalData();
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Session Expired Please Login Again!');</script>");
                Response.Redirect("UserLoginPage.aspx");
            }
        }

        void getUserBookData()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM book_issue_tbl WHERE member_id='" + Session["username"].ToString() + "'", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void getUserPersonalData()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id='" + Session["username"].ToString() + "'", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBox_Name.Text = dt.Rows[0]["full_name"].ToString();
                    TextBox_DateOfBirth.Text = dt.Rows[0]["dob"].ToString();
                    TextBox_ContactNumber.Text = dt.Rows[0]["contact_no"].ToString();
                    TextBox_Email.Text = dt.Rows[0]["email"].ToString();
                    TextBox_PostCode.Text = dt.Rows[0]["postcode"].ToString();
                    TextBox_FullAddress.Text = dt.Rows[0]["full_address"].ToString();
                    TextBox_UserId.Text = dt.Rows[0]["member_id"].ToString();
                    TextBox_OldPassword.Text = dt.Rows[0]["password"].ToString();
                    Label_Status.Text = dt.Rows[0]["account_status"].ToString();

                    DropDownList_City.SelectedValue = dt.Rows[0]["state"].ToString();
                    DropDownList_State.SelectedValue = dt.Rows[0]["city"].ToString();

                    if (dt.Rows[0]["account_status"].ToString().Trim() == "active")
                    {
                        Label_Status.ForeColor = Color.LightGreen;
                    }
                    else if(dt.Rows[0]["account_status"].ToString().Trim() == "pending")
                    {
                        Label_Status.ForeColor = Color.Yellow;

                    }
                    else if (dt.Rows[0]["account_status"].ToString().Trim() == "suspended")
                    {
                        Label_Status.ForeColor = Color.Red;
                    }
                    else
                    {
                        Label_Status.ForeColor = Color.Black;
                    }
                }
                else
                {
                    Response.Write("<script>alert('Invalid Member ID!');</script>");
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //color any row that overdue
                //cell[5] is due date column
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DateTime dt = Convert.ToDateTime(e.Row.Cells[5].Text);
                    DateTime today = DateTime.Today;

                    if (today > dt)
                    {
                        e.Row.BackColor = System.Drawing.Color.PaleVioletRed;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void updateUserPersonalData()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("UPDATE member_master_tbl SET full_name=@full_name, dob=@dob, contact_no=@contact_no, state=@state, city=@city, email=@email, postcode=@postcode, full_address=@full_address, member_id=@member_id, password=@password " +
                    "WHERE member_id='" + Session["username"].ToString() + "'", con);

                cmd.Parameters.AddWithValue("@full_name", TextBox_Name.Text);
                cmd.Parameters.AddWithValue("@dob", TextBox_DateOfBirth.Text);
                cmd.Parameters.AddWithValue("@contact_no", TextBox_ContactNumber.Text);
                cmd.Parameters.AddWithValue("@state", DropDownList_State.SelectedItem.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@city", DropDownList_City.SelectedItem.Value.ToString().Trim());
                cmd.Parameters.AddWithValue("@email", TextBox_Email.Text);
                cmd.Parameters.AddWithValue("@postcode", TextBox_PostCode.Text);
                cmd.Parameters.AddWithValue("@full_address", TextBox_FullAddress.Text);
                cmd.Parameters.AddWithValue("@member_id", TextBox_UserId.Text);

                if(TextBox_NewPassword.Text != "" || TextBox_NewPassword.Text != null)
                {
                    cmd.Parameters.AddWithValue("@password", TextBox_NewPassword.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@password", TextBox_OldPassword.Text);
                }

                int result = cmd.ExecuteNonQuery();
                con.Close();

                if(result > 0)
                {

                    Response.Write("<script>alert('Member Personal Details Update Successfullly');</script>");
                    getUserBookData();
                    getUserPersonalData();
                }
                else
                {

                    Response.Write("<script>alert('Member Personal Details Update Failed!');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
}