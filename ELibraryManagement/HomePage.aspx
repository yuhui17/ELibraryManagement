<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="ELibraryManagement.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section>
        <img class="img-fluid" src="images/homepage_background.png" />
    </section>

    <section>
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <center>
                        <h2>Our Features</h2>
                        <p>Our 3 Primary Features - </p>
                    </center>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    <center>
                        <img width="150px" height="150px" src="images/homepage_digital_book_inventory.jpg" />
                        <h4>Digital Book Inventory</h4>
                        <p class="text-justify">Track your digital book collection with our inventory function! Keep a record of all your ebooks, audiobooks, and digital magazines in one convenient place on our webpage.</p>
                    </center>
                </div>
                <div class="col-md-4">
                    <center>
                        <img width="150px" height="150px" src="images/homepage_search_book.jpg" />
                        <h4>Search Book</h4>
                        <p class="text-justify">Find your next favorite book with our Search Book function! Simply enter keywords or author names and browse through our extensive collection of books to discover your next great read.</p>
                    </center>
                </div>
                <div class="col-md-4">
                    <center>
                        <img width="150px" height="150px" src="images/homepage_defaulter_list.jpg" />
                        <h4>Defaulter List</h4>
                        <p class="text-justify">Stay on top of late returns with our Defaulter List! Our webpage keeps track of overdue books and displays them in a user-friendly list, making it easy to follow up with borrowers and ensure that your collection stays up-to-date.</p>
                    </center>
                </div>
            </div>
        </div>
    </section>

    <section>
        <img class="img-fluid" src="images/homepage_background_2.png" />
    </section>

    <section>
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <center>
                        <h2>Our Process</h2>
                        <p>We have a Simple 3 Step Process - </p>
                    </center>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    <center>
                        <img width="150px" height="150px" src="images/homepage_sign_up.png" />
                        <h4>Sign Up</h4>
                        <p class="text-justify">Join our digital library community today by signing up for our webpage library system! Get access to thousands of ebooks, audiobooks, and digital magazines, all from the comfort of your own device.</p>
                    </center>
                </div>
                <div class="col-md-4">
                    <center>
                        <img width="150px" height="150px" src="images/homepage_search_book.jpg" />
                        <h4>Search Book</h4>
                        <p class="text-justify">Find your next favorite book with our Search Book function! Simply enter keywords or author names and browse through our extensive collection of books to discover your next great read.</p>
                    </center>
                </div>
                <div class="col-md-4">
                    <center>
                        <img width="150px" height="150px" src="images/homepage_visit_us.png" />
                        <h4>Visit Us</h4>
                        <p class="text-justify">Discover the magic of the library in person by visiting us! Our webpage library system provides information on our location, hours, and events so you can plan your next visit and explore all that we have to offer.</p>
                    </center>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
