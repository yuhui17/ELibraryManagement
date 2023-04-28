<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdminMemberManagement.aspx.cs" Inherits="ELibraryManagement.AdminMemberManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <script type="text/javascript">
        $(document).ready(function () {
            $('.table').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable();
        });
            </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid">
        <div class="row">
            <div class="col-md-5">

                <div class="card">
                    <div class="card-body">

                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Member Details</h4>
                                    <img src="images/user_login_icon.png" width="100px" />
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label>Member ID</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox class="form-control" ID="TextBox_MemberId" runat="server" placeholder="ID"></asp:TextBox>
                                        <asp:Button class="btn btn-primary" ID="Button_Go" runat="server" Text="Go" OnClick="Button_Go_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-5">
                                <label>Full Name</label>
                                <div class="input-group">
                                    <asp:TextBox class="form-control" ID="TextBox_Name" runat="server" placeholder="Full Name" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <label>Account Status</label>
                                <div class="input-group">
                                    <asp:TextBox class="form-control" ID="TextBox_Status" runat="server" placeholder="Account Status" ReadOnly="True"></asp:TextBox>
                                    <asp:LinkButton class="btn btn-success mr-1" ID="Button_Active" runat="server" OnClick="Button_Active_Click"><i class="fa-solid fa-circle-check"></i></asp:LinkButton>
                                    <asp:LinkButton class="btn btn-warning mr-1" ID="Button_Pending" runat="server" OnClick="Button_Pending_Click"><i class="fa-solid fa-circle-pause"></i></asp:LinkButton>
                                    <asp:LinkButton class="btn btn-danger mr-1" ID="Button_Suspended" runat="server" OnClick="Button_Suspended_Click"><i class="fa-solid fa-circle-xmark"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label>Date of Birth</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox class="form-control" ID="TextBox_DateOfBirth" runat="server" placeholder="dd-mm-yyyy" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <label>Contact Number</label>
                                <div class="input-group">
                                    <asp:TextBox class="form-control" ID="TextBox_ContactNo" runat="server" placeholder="Contact Number" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-5">
                                <label>Email ID</label>
                                <div class="input-group">
                                    <asp:TextBox class="form-control" ID="TextBox_Email" runat="server" placeholder="Email" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <label>State</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox class="form-control" ID="TextBox_State" runat="server" placeholder="State" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <label>City</label>
                                <div class="input-group">
                                    <asp:TextBox class="form-control" ID="TextBox_City" runat="server" placeholder="City" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <label>Postcode</label>
                                <div class="input-group">
                                    <asp:TextBox class="form-control" ID="TextBox_Postcode" runat="server" placeholder="Postcode" ReadOnly="True"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <label>Full Address</label>
                                <div class="form-group">
                                    <div class="input-group">
                                        <asp:TextBox class="form-control" ID="TextBox_FullAddress" runat="server" placeholder="Your Address" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                                <div class="col-md-8 mx-auto">
                                    <asp:Button class="btn btn-danger btn-block" ID="Button_DeleteUser" runat="server" Text="Delete User Permenantly" OnClick="Button_DeleteUser_Click" />
                                </div>
                        </div>

                    </div>

                </div>

                <a href="HomePage.aspx"><< Back to Home</a>
            </div>

            <div class="col-md-7">

                <div class="card">
                    <div class="card-body">

                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Member List</h4>
                                </center>
                            </div>
                        </div>

                        <div class="row">
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:elibraryDBConnectionString %>" SelectCommand="SELECT * FROM [member_master_tbl]"></asp:SqlDataSource>
                            <div class="col">
                                <asp:GridView class="table table-striped table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="member_id" DataSourceID="SqlDataSource1">
                                    <Columns>
                                        <asp:BoundField DataField="member_id" HeaderText="ID" ReadOnly="True" SortExpression="member_id" />
                                        <asp:BoundField DataField="full_name" HeaderText="Name" SortExpression="full_name" />
                                        <asp:BoundField DataField="dob" HeaderText="Date of Birth" SortExpression="dob" />
                                        <asp:BoundField DataField="contact_no" HeaderText="Contact No" SortExpression="contact_no" />
                                        <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                                        <asp:BoundField DataField="state" HeaderText="State" SortExpression="state" />
                                        <asp:BoundField DataField="city" HeaderText="City" SortExpression="city" />
                                        <asp:BoundField DataField="account_status" HeaderText="Status" SortExpression="account_status" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
