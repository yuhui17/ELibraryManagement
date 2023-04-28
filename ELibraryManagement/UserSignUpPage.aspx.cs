﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ELibraryManagement
{
    public partial class UserSignUpPage : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //sign up button click
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkMemberExists()) 
            {
                Response.Write("<script>alert('Member Already Exist With This Member ID, Try Other ID')</script>");
            }
            else
            {
                signUpNewUser();
            }
        }

        bool checkMemberExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id='"+TextBox_UserID.Text.Trim()+"'", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if(dt.Rows.Count >= 1)
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

        void signUpNewUser()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO member_master_tbl" +
                    "(full_name, dob, contact_no, email, state, city, postcode, full_address, member_id, password, account_status)" +
                    " VALUES (@full_name, @dob, @contact_no, @email, @state, @city, @postcode, @full_address, @member_id, @password, @account_status)", con);

                cmd.Parameters.AddWithValue("@full_name", TextBox_FullName.Text.Trim());
                cmd.Parameters.AddWithValue("@dob", TextBox_DateOfBirth.Text.Trim());
                cmd.Parameters.AddWithValue("@contact_no", TextBox_ContactNumber.Text.Trim());
                cmd.Parameters.AddWithValue("@email", TextBox_Email.Text.Trim());
                cmd.Parameters.AddWithValue("@state", DropDownList_State.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@city", TextBox_City.Text.Trim());
                cmd.Parameters.AddWithValue("@postcode", TextBox_Postcode.Text.Trim());
                cmd.Parameters.AddWithValue("@full_address", TextBox_FullAdress.Text.Trim());
                cmd.Parameters.AddWithValue("@member_id", TextBox_UserID.Text.Trim());
                cmd.Parameters.AddWithValue("@password", TextBox_Password.Text.Trim());
                cmd.Parameters.AddWithValue("@account_status", "pending");

                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("<script>alert('Sign Up Successful. Go to User Login to Login!');</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

    }
}