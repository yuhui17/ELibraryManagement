using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace ELibraryManagement
{
    public partial class AdminMemberManagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }


        #region Update Status
        protected void Button_Active_Click(object sender, EventArgs e)
        {
            if (checkIfMemberExists())
            {
                updateMemberStatus("active");
            }
            else
            {
                Response.Write("<script>alert('Member does not exist, Please try other ID.');</script>");
            }
        }

        protected void Button_Pending_Click(object sender, EventArgs e)
        {
            if (checkIfMemberExists())
            {
                updateMemberStatus("pending");
            }
            else
            {
                Response.Write("<script>alert('Member does not exist, Please try other ID.');</script>");
            }
        }

        protected void Button_Suspended_Click(object sender, EventArgs e)
        {
            if (checkIfMemberExists())
            {
                updateMemberStatus("suspended");
            }
            else
            {
                Response.Write("<script>alert('Member does not exist, Please try other ID.');</script>");
            }
        }
        #endregion

        protected void Button_Go_Click(object sender, EventArgs e)
        {
            getMemberById();
        }

        protected void Button_DeleteUser_Click(object sender, EventArgs e)
        {
            if (checkIfMemberExists())
            {
                deleteMember();
            }
            else
            {
                Response.Write("<script>alert('Member does not exist, Please try other ID.');</script>");
            }
        }

        void getMemberById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id='" + TextBox_MemberId.Text.Trim() + "'", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBox_Name.Text = dt.Rows[0][0].ToString(); 
                    TextBox_DateOfBirth.Text = dt.Rows[0][1].ToString();
                    TextBox_ContactNo.Text = dt.Rows[0][2].ToString();
                    TextBox_State.Text = dt.Rows[0][3].ToString();
                    TextBox_City.Text = dt.Rows[0][4].ToString();
                    TextBox_Email.Text = dt.Rows[0][5].ToString();
                    TextBox_Postcode.Text = dt.Rows[0][6].ToString();
                    TextBox_FullAddress.Text = dt.Rows[0][7].ToString();
                    TextBox_MemberId.Text = dt.Rows[0][8].ToString();
                    TextBox_Status.Text = dt.Rows[0][10].ToString();
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

        void deleteMember()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("DELETE from member_master_tbl " +
                    "WHERE member_id='" + TextBox_MemberId.Text.Trim() + "'", con);

                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('Member Deleted Successfully!');</script>");

                clearForm();

                GridView1.DataBind(); //update gridview
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        bool checkIfMemberExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id='" + TextBox_MemberId.Text.Trim() + "'", con);

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

        void updateMemberStatus(string status)
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("UPDATE member_master_tbl SET account_status=@account_status " +
                    "WHERE member_id='" + TextBox_MemberId.Text.Trim() + "'", con);

                cmd.Parameters.AddWithValue("@account_status", status);

                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('Member Status Update Successfullly');</script>");

                clearForm();

                GridView1.DataBind(); //update gridview
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        void clearForm()
        {
            TextBox_Name.Text = "";
            TextBox_DateOfBirth.Text = "";
            TextBox_ContactNo.Text = "";
            TextBox_State.Text = "";
            TextBox_City.Text = "";
            TextBox_Email.Text = "";
            TextBox_Postcode.Text = "";
            TextBox_FullAddress.Text = "";
            TextBox_MemberId.Text = "";
            TextBox_Status.Text = "";
        }


    }
}