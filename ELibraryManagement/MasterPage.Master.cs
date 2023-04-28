using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ELibraryManagement
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["role"] == null)
                {
                    //User
                    LinkButton_UserLogin.Visible = true; //User Login
                    LinkButton_SignUp.Visible = true; //User Sign Up
                    LinkButton_LogOut.Visible = false;  //User Log Out
                    LinkButton_HelloUser.Visible = false; //Hello User

                    //Admin
                    LinkButton_AdminLogin.Visible = true;
                    LinkButton_AuthorManagement.Visible = false;
                    LinkButton_BookInventory.Visible = false;
                    LinkButton_BookIssuing.Visible = false;
                    LinkButton_BookIssuing.Visible = false;
                }
                else if (Session["role"].Equals("user"))
                {
                    //User
                    LinkButton_UserLogin.Visible = false; //User Login
                    LinkButton_SignUp.Visible = false; //User Sign Up
                    LinkButton_LogOut.Visible = true;  //User Log Out
                    LinkButton_HelloUser.Visible = true; //Hello User

                    LinkButton_HelloUser.Text = "Hello " + Session["username"].ToString(); //set name tag

                    //Admin
                    LinkButton_AdminLogin.Visible = true;
                    LinkButton_AuthorManagement.Visible = false;
                    LinkButton_BookInventory.Visible = false;
                    LinkButton_BookIssuing.Visible = false;
                    LinkButton_BookIssuing.Visible = false;
                }
                else if(Session["role"].Equals("admin"))
                {
                    //User
                    LinkButton_UserLogin.Visible = false; //User Login
                    LinkButton_SignUp.Visible = false; //User Sign Up
                    LinkButton_LogOut.Visible = true;  //User Log Out
                    LinkButton_HelloUser.Visible = true; //Hello User

                    LinkButton_HelloUser.Text = "Hello Admin, " + Session["username"].ToString(); //set name tag

                    //Admin
                    LinkButton_AdminLogin.Visible = false;
                    LinkButton_AuthorManagement.Visible = true;
                    LinkButton_BookInventory.Visible = true;
                    LinkButton_BookIssuing.Visible = true;
                    LinkButton_BookIssuing.Visible = true;
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminLoginPage.aspx");
        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminAuthorManagement.aspx");
        }

        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminBookInventory.aspx");
        }

        protected void LinkButton9_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminBookIssuing.aspx");
        }

        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminMemberManagement.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewBooks.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserSignUpPage.aspx");
        }

        protected void LinkButton2_Click1(object sender, EventArgs e)
        {
            Response.Redirect("UserLoginPage.aspx");
        }

        protected void LinkButton_LogOut_Click(object sender, EventArgs e)
        {
            Session["username"] = "";
            Session["fullname"] = "";
            Session["role"] = "";
            Session["status"] = "";

            //User
            LinkButton_UserLogin.Visible = true; //User Login
            LinkButton_SignUp.Visible = true; //User Sign Up
            LinkButton_LogOut.Visible = false;  //User Log Out
            LinkButton_HelloUser.Visible = false; //Hello User

            //Admin
            LinkButton_AdminLogin.Visible = true;
            LinkButton_AuthorManagement.Visible = false;
            LinkButton_BookInventory.Visible = false;
            LinkButton_BookIssuing.Visible = false;
            LinkButton_BookIssuing.Visible = false;

            Response.Redirect("HomePage.aspx");
        }

        protected void LinkButton_HelloUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserProfilePage.aspx");
        }
    }
}