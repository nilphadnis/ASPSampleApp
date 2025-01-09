<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SampleApp.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/bootstrap-datepicker.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap-datepicker.min.js"></script>
    <script src="Scripts/jquery.mask.min.js"></script>
    <br />
    <br />
    <div class="row">
        <div class="col-lg-3"></div>
        <div class="col-lg-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4>New Registration</h4>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Full Name <span class="required">*</span></label>
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ID="fullNameReqValidator" runat="server"
                                    ControlToValidate="txtFullName" ValidationGroup="submit"
                                    ErrorMessage="Full name is required" CssClass="error"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Email <span class="required">*</span></label>
                                <a href="#" id="checkavailability" style="font-size: 12px;">Click here to check email availability</a>
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ID="emailRequiredValidator" runat="server"
                                    ControlToValidate="txtEmail" ValidationGroup="submit"
                                    ErrorMessage="Email is required" CssClass="error"></asp:RequiredFieldValidator>

                                <asp:RegularExpressionValidator ID="rgValidatorEmail" runat="server"
                                    ValidationGroup="submit" ErrorMessage="Invalid email address" ControlToValidate="txtEmail"
                                    ValidationExpression="^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+.)+[a-z]{2,5}$" CssClass="error">
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>DOB <span class="required">*</span></label>
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                    <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ID="DOBRequiredValidator" runat="server"
                                    ControlToValidate="txtDOB" ValidationGroup="submit"
                                    ErrorMessage="DOB is required" CssClass="error"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Phone <span class="required">*</span></label>
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-phone"></i></span>
                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" MaxLength="12" placeholder="###-###-####"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ID="phoneRequiredValidator" runat="server"
                                    ControlToValidate="txtPhone" ValidationGroup="submit"
                                    ErrorMessage="Phone is required" CssClass="error"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Gender</label>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <label class="radio-inline">
                                            <asp:RadioButton ID="rdbMale" runat="server" GroupName="gender" Checked="true" Style="float: left;" />
                                            Male
                                        </label>
                                    </div>
                                    <div class="col-lg-6">
                                        <label class="radio-inline">
                                            <asp:RadioButton ID="rdbFemale" runat="server" GroupName="gender" Style="float: left;" />
                                            Female
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>State <span class="required">*</span></label>
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-list"></i></span>
                                    <asp:DropDownList ID="ddlStates" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <asp:RequiredFieldValidator ID="ddlStatesRequired" runat="server"
                                    ControlToValidate="ddlStates" ValidationGroup="submit"
                                    ErrorMessage="State is required" CssClass="error"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-footer text-center">
                    <asp:Button ID="btnRegister" runat="server" CssClass="btn btn-md btn-primary" Text="Register" ValidationGroup="submit" OnClick="btnRegister_Click" />
                    <button type="reset" class="btn btn-md btn-danger btn-reset">Reset</button>
                </div>
            </div>
        </div>
        <div class="col-lg-3"></div>
    </div>
    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="myModalLabel">Message</h5>
                    <button type="button" class="btn btn-sm btn-close" data-bs-dismiss="modal" aria-label="Close" style="float: right; margin-top: -25px;">X</button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            // Initialize the datepicker
            $('#<%=txtDOB.ClientID %>').datepicker({
                format: 'mm/dd/yyyy',  // Set the date format
                startView: 'days',          // Start the view at the month level
                minViewMode: 'days',        // Allow only months and years  
                autoclose: true,        // Close datepicker when a date is selected
                todayHighlight: true    // Highlight today's date
            });
            $('.btn-close').click(function () {
                $('#myModal').modal('hide');
            });
            $('#<%=txtDOB.ClientID %>').keydown(function (e) {
                e.preventDefault(); // Prevent the keystroke
            });
            $('#<%=txtPhone.ClientID %>').mask('000-000-0000');
            $('.btn-reset').click(function () {
                document.forms[0].reset();
            });
            $('#checkavailability').click(function () {
                let emailId = $('#<%=txtEmail.ClientID%>').val();
                if (emailId && emailId != '') {
                    $.ajax({
                        url: "Register.aspx/CheckEmailAddressAvailability",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: '{ email:"' + emailId + '"}',
                        cache: false,
                        success: function (data) {
                            $('#<%=lblMessage.ClientID%>').text(data.d);
                             $('#myModal').modal('show');
                        },
                        error: function (response) {                           
                            console.log(response);
                        }
                    })
                }
            });
        });
    </script>
</asp:Content>
