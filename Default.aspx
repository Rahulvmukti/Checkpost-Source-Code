<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="exam.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
    <script src="dist/js/adminlte.min.js"></script>   
    <link rel="stylesheet" href="dist/css/adminlte.min.css"  rel="stylesheet" />
   <link rel="stylesheet" href="plugins/fontawesome-free/css/all.min.css"> 
    </asp:Content>  
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server"> 
        <style>
        .card {
    background-color: #F7F7F7!important;
    padding: 0px!important; 
    margin-top: 50px!important;
    -moz-border-radius: 2px; 
    border-radius: 1px!important;
    -moz-box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3)!important; 
    box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3)!important;
        border: 1px solid #000c0edb;
        left:70%;
}
       /* .card-body {
    flex: 1 1 auto!important;
    padding: 30px 10px!important;
}
     */
          .card-outline {
    border-top: 3px solid #007bff!important; 
}
            .form-control {
                font-size: 18px;
                font-weight: 400;
                line-height: 1.5;
                color: #212529;
                background-color: #fff;
                background-clip: padding-box;
                border-radius: 9px;
            }
            .input-group-text {
                font-size: 18px;
                display: block;
                padding:4px 10px;
            }
            .btn {
                padding: 0px 10px;
                font-size: 18px;
                border-radius: 7px;
            }
            .mb-3 {
    margin-bottom: 2rem !important;
}
            /* @media (min-width: 768px) 
            { 
            .login-page{
                 padding:148px 38%!important; 
            }
            
            }*/
              
             
           #toast-container>div {
    position: fixed!important;
    top:55%;
    right:40%;
}
           .toast-message
           {
               font-size:14px!important;
           }
            .no-underline {
                text-decoration: none;
                color: #0d6efd;
            }
             
    </style>


     <div class="hold-transition login-page" style="align-items:baseline">
    <div class="login-box">
  <!-- /.login-logo -->
  <div class="card">
    <div class="card-header text-center">
  <a href="#" class="h3 no-underline"><b>LOGIN</b></a> 
    </div>
    <div class="card-body">
         
        <div class="input-group mb-3">
             <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control required" placeholder="Enter Username"></asp:TextBox> 
                
          <div class="input-group-append">
            <div class="input-group-text">
              <span class="fas fa-user-alt"></span>
            </div>
          </div>
        </div>
        <div class="input-group mb-3">
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control required" TextMode="Password"
                    placeholder="Enter Password"></asp:TextBox> 
          <div class="input-group-append">
            <div class="input-group-text">
              <span class="fas fa-lock"></span>
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-8"> 
          </div>
          <!-- /.col -->
          <div class="col-4">
              <asp:Button ID="btn_Login" runat="server" class="btn btn-primary btn-block toastrDefaultError"
                    Text="Login" OnClientClick="javascript: return checkValidate();" OnClick="btn_Login_Click" style="border-radius:0px;"/>
          </div>
          <!-- /.col -->
        </div>
           <div class="row" style="font-size: 9px;color: #767676;font-style: italic;">
    <b>Unauthorized Access Prohibited </b><span style="color:red;">This website is for authorized government users only. Unauthorized access is strictly prohibited.</span>
</div>
    </div>
    <!-- /.card-body -->
  </div>
  <!-- /.card --> 
        </div>
         </div>  
       <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback">
  <!-- Font Awesome -->
  <link rel="stylesheet" href="plugins/fontawesome-free/css/all.min.css">

  <!-- Toastr -->
  <link rel="stylesheet" href="plugins/toastr/toastr.min.css">
    <script src="plugins/jquery/jquery.min.js"></script>
<!-- Bootstrap 4 -->
<script src="plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="plugins/toastr/toastr.min.js"></script>
    <script type="text/javascript">
        function checkValidate() {
            if ($("#<%= txtUsername.ClientID %>").valid() && $("#<%= txtPassword.ClientID %>").valid()) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
     <link media="screen" rel="stylesheet" href="https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.css" />
    <script type="text/javascript" src="https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.js"> </script>
   <script type="text/javascript">
       // Function to show success popup if the current date and time are before 15-04-2024 09:00 PM
       function showSuccessPopup() {
           // Define the target date and time (15-04-2024 09:00 PM)
           var targetDate = new Date("2024-04-14T21:15:00");
           var currentDate = new Date();
           if (currentDate < targetDate) {
               var popup = swal({
                   title: "INFORMATION!",
                   text: "Pre Scheduled Maintenance Activity is planned on 14/04/2024 from 21:00 to 21:15 PM IST.",
                   icon: "info",
                   timer: null // Set timer to null to prevent auto-close
               });
           }
       }

       // Call the function when the page finishes loading
       window.onload = function () {
           showSuccessPopup();
       };
   </script>
</asp:Content>
