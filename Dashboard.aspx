<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="exam.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">



       <style>
           .btn{
               border-radius:0px;
           }
        .content-wrapper>.content {
    padding: 5px;
}
        .fixheader{
            background-color: #000;
    border-bottom: 0;
    box-shadow: inset 0 1px 0 #dee2e6, inset 0 -1px 0 #dee2e6;
    position: -webkit-sticky;
    position: sticky;
    top: 0;
    z-index: 10; 
    color:#fff;
        text-align: center;
        }
        .p-td{
            padding:5px;
        }
        .form-group {
    margin-bottom: 0px;
    padding:2px 5px;
        }
        .card-body.p-0 .table tbody>tr>td:last-of-type, .card-body.p-0 .table tbody>tr>th:last-of-type, .card-body.p-0 .table tfoot>tr>td:last-of-type, .card-body.p-0 .table tfoot>tr>th:last-of-type, .card-body.p-0 .table thead>tr>td:last-of-type, .card-body.p-0 .table thead>tr>th:last-of-type {
     padding-right: 0; 
}
        .cardarchive{
             overflow:hidden;
         }
        .location-header {
    width: 300px; /* Adjust the width as needed */
    text-align: center;
    padding: 5px; /* Adjust the padding to reduce white space */
    margin: 0; /* Remove any margin */
}

 
content-wrapper {
    overflow: hidden;
}
.bg-gradient-success {
    background: #68b187!important;
    color: #fff;
}
.bg-gradient-danger{
     background: #d54d4d!important;
     border:#d54d4d!important;
    color: #fff;
}
.bg-gradient-primary{
     background: #236a8d!important;
    color: #fff;
    border:#236A8d!important;
   /*  background-color: #236A8d!important;color: #FFF;border:#236A8d!important;*/
}
.district 
{
   background: #20282D!important;
   border:#20282D!important;
}
.district:hover
{
    background: #32383b!important;
    border:#32383b!important;
}
.bg-gradient-primary.btn:hover
{
        background: #125B7F!important;
        border:#125B7F!important;
}
.district.btn:hover
{
        background: #32383b!important;
         border:#32383b!important;
}
.bg-gradient-warning
{
     background: #91C8E4!important;
      border:#91C8E4!important;
      
}
.bg-gradient-warning.btn:hover
{
     background: #7AB0CC!important;
     border:#7AB0CC!important; 
}

 .bg-gradient-warning.btn:not(:disabled):not(.disabled):active { 
   background: #91C8E4!important;
      border:#91C8E4!important; 
           }
   .bg-gradient-primary.btn:not(:disabled):not(.disabled):active{
  background: #125B7F!important;
        border:#125B7F!important;
           }



    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                             <asp:Timer ID="Timer1" runat="server" Interval="60000" Enabled="true" OnTick="Timer1_Tick1">
                        </asp:Timer>
                              <div class="content-wrapper"> 
    <section class="content">
      <div class="container-fluid">
        
        <div class="row">
          <div class="col-12">
            <div class="card">
               

                </div>
                  <div class="card">
                 <div class="col-md-12 card-body" style="padding:2px">
                   <div class="panel-body">
                         <div class="panel panel-primary">
                        <div class="panel-heading navbar-inverse">
                            <%--   Live Counter :--%>
                            <asp:Label ID="lblonlinecounter" runat="server"></asp:Label>
                            <div style="float: right">
                                <asp:Label ID="lbltotalonline" runat="server"></asp:Label></div>
                        </div>
                    </div>
                           </div>
                      </div>



                       <div class="col-md-12 row">
                           <div class="col-md-2" ></div>
                                <div class="col-md-2" style="text-align:center; font-weight:600">
                                    Total<br /> <a id="totalbooth" href="javascript:void(0);" tabindex="0" runat="server"
                                        class='btn btn-block bg-gradient-primary btn-flat'></a>
                                    <%-- </div>--%>
                                </div>
                                 
                                <div class="col-md-2" style="text-align:center; font-weight:600">
                                    <%--<img src="images/camrun.gif" />--%>
                                   Online<br /> <a id="runningbooth" href="javascript:void(0);" tabindex="0" runat="server"
                                        class='btn btn-block bg-gradient-success btn-flat' style="height: 36px;"></a>
                                </div>
                              <div class="col-md-2" style="text-align:center; font-weight:600">
                                 Connected-Once   <br/>
                    <a id="connectedonce" href="javascript:void(0);" tabindex="0" runat="server" class='btn btn-block bg-gradient-warning btn-flat'></a>
                    </div>
                                <div class="col-md-2" style="text-align:center; font-weight:600">
                                    <%-- <img src="images/camstop.gif" />--%>
                                    Offline<br /> <a id="stopbooth" href="javascript:void(0);" tabindex="0" runat="server"
                                        class='btn btn-block bg-gradient-danger btn-flat' ></a>
                                </div> 
                           <div class="col-md-2" ></div>
                            </div> 
                        <br />
                      
                         <div class="col-12 row" style="padding:5px">
                       <asp:ListView ID="listview1" runat="server" OnItemDataBound="listview1_ItemDataBound">
                                        <ItemTemplate>
                                            
                                            <div class="col-md-3 btn btn-default" style="padding:10px; padding-bottom:0px";>
                                                <div class="row">
                                                    <%if (useridentifer == "1" || useridentifer == "2")
                                                        {%>
                                                    <div class="col-md-12">
                                                        <a class='btn btn-block bg-gradient-primary btn-flat district' style="padding:2px;" data-toggle="modal" data-target="#myModal" title='<%#Eval("DATA") %>'
                                                            onclick="loadac('<%#exam.Common.Encode("010$"+ Eval("DATA").ToString())%>')"
                                                            >
                                                        
                                                            <%#Eval("DATA").ToString().Length > 20 ? Eval("DATA").ToString().PadRight(140).Substring(0, 20).TrimEnd() + ".." : Eval("DATA")%>
                                                        </a>
                                                       
                                                    </div>
                                                    <div class="col-md-12"  style="padding:10px">
                                                          <a title='<%#Eval("DATA") %>'>
                                                                <%if (useridentifer == "1")
                                                                    {%>
                                                            <span class='btn bg-gradient-primary' style="padding:2px 25px" onclick="LoadData('<%#exam.Common.Encode("007$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');"> <%#Eval("TotalBooth")%></span> 


                                                              <span class='btn bg-gradient-success' style="padding:2px 25px;height:28px;" 
      onclick='handleButtonClick_1("<%# exam.Common.Encode("000$" + Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString()) %>", "<%# Eval("district").ToString() %>", "<%# Eval("Live") %>");'>
    <%# Eval("Live") %>
</span>
                                                            <span class='btn bg-gradient-warning' style="padding:2px 25px" ><%#Eval("connectedonce") %></span>
                                                           <%-- <span class='btn bg-gradient-danger' style="padding: 2px 25px"  onclick="LoadData('<%#exam.Common.Encode("002$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');"><%#Eval("stop") %></span> --%>
                                                                <span class='btn bg-gradient-danger' style="padding:2px 25px" 
      onclick='handleButtonClick_offline("<%# exam.Common.Encode("002$" + Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString()) %>", "<%# Eval("district").ToString() %>", "<%#Eval("stop") %>");'>
    <%#Eval("stop") %>
</span>
                                                              <%}%>
                                                                <%else
                                                                    {%>
                                                                  
                                                            <span class='btn bg-gradient-primary' style="padding:2px 25px" onclick="LoadData('<%#exam.Common.Encode("007$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');"> <%#Eval("TotalBooth")%> </span> 
                                                             
                                                              <span class='btn bg-gradient-success' style="padding:2px 25px;height:28px;" 
      onclick='handleButtonClick("<%# exam.Common.Encode("000$" + Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString()) %>", "<%# Eval("district").ToString() %>", "<%# Eval("Live") %>");'>
    <%# Eval("Live") %>
</span>

                                                              <span class='btn bg-gradient-warning' style="padding:2px 25px"><%#Eval("connectedonce") %></span>
                                                                      <span class='btn bg-gradient-danger' style="padding:2px 25px" 
      onclick='handleButtonClick_district("<%# exam.Common.Encode("002$" + Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString()) %>", "<%# Eval("district").ToString() %>", "<%#Eval("stop") %>");'>
    <%#Eval("stop") %>
</span>
                                                               <%}%>
                                                        </a>
                                                    </div>
                                                    <%}%> 
                                                </div>
                                            </div> 
                                        </ItemTemplate>
                                    </asp:ListView>
                             </div>
                   
                </div>
              </div>
            </div>

          </div>
        </section>

                              </div>
                               </ContentTemplate> 
                    </asp:UpdatePanel>   
   
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.js" ></script>
<script type="text/javascript">

    function loadac(pcname) {

        document.getElementById("iframesetting").src = 'assembly.aspx?pcname=' + pcname;


    }
    function LoadData(loc) {
        // Check the condition for the particular login
      
            window.top.location.href = "Listview.aspx?d=" + loc;
        }
        function LoadData2(loc) {
        // Perform an AJAX request to get the user's login type
        $.ajax({
            url: 'Dashboard.aspx/setusertype', // Replace this with your actual endpoint to get the user's login type
            method: 'GET',
            success: function (userLoginType) {
                // Check if loc is not equal to the current user's login type
                if (loc == userLoginType) { 
                    window.location.href = "Listview.aspx?d=" + loc;
                } else {
                     
                    alert("no such permission")
                }
            },
            error: function () { 
                alert("error");
            }
        });
    }
    
    function loadgrid(loc) {
        window.top.location.href = "Gridview.aspx?d=" + loc;
    }
    function handleButtonClick(encodedData, district, live) {
        debugger
        var string2 = '<%= Session["DistrictDataSet"] != null ? Session["DistrictDataSet"].ToString() : "" %>';
        if (live == 0 && string2 != district ) {
            Swal.fire({
                icon: 'warning',
              //  title: 'Warning',
                text: 'ACCESS NOT ALLOWED.',
            
                customClass: {
                    confirmButton: 'btn btn-danger' // Change the class to set the button color to red
                }
            });

        }
        else if (live == 0) {
            Swal.fire({
                icon: 'warning',
                //title: 'Warning',
                text: 'PRESENTLY THERE ARE NO STREAMING LIVE CAMERAS',
                customClass: {
                    confirmButton: 'btn btn-danger' // Change the class to set the button color to red
                }
            });
        } 

        else {
            // Call your loadgrid1 function with the appropriate parameters
            loadgrid1(encodedData, district);
        }
    }
    function handleButtonClick_1(encodedData, district, live) {
        debugger;
        var string2 = '<%= Session["DistrictDataSet"] != null ? Session["DistrictDataSet"].ToString() : "" %>';
        if (live == 0) {
            Swal.fire({
                icon: 'warning',
                //title: 'Warning',
                text: 'PRESENTLY THERE ARE NO STREAMING LIVE CAMERAS',
                customClass: {
                    confirmButton: 'btn btn-danger' // Change the class to set the button color to red
                }
            });
        } else {
            // Call your loadgrid1 function with the appropriate parameters
            loadgrid(encodedData, district);
        }
    }

    function handleButtonClick_offline(encodedData, district, live) {
        debugger;
        var string2 = '<%= Session["DistrictDataSet"] != null ? Session["DistrictDataSet"].ToString() : "" %>';
           if (live == 0) {
               Swal.fire({
                   icon: 'warning',
                   //title: 'Warning',
                   text: 'PRESENTLY THERE ARE NO OFFLINE CAMERAS',
                   customClass: {
                       confirmButton: 'btn btn-danger' // Change the class to set the button color to red
                   }
               });
           }



           else {
               // Call your loadgrid1 function with the appropriate parameters
               LoadData(encodedData,district);
           }
    }
    function handleButtonClick_district(encodedData, district, live) { 
        var string2 = '<%= Session["DistrictDataSet"] != null ? Session["DistrictDataSet"].ToString() : "" %>';
        if (live != 0 && string2 != district && live == 0) {
               Swal.fire({
                   icon: 'warning',
                   //  title: 'Warning',
                   text: 'ACCESS NOT ALLOWED.',

                   customClass: {
                       confirmButton: 'btn btn-danger' // Change the class to set the button color to red
                   }
               });

           }
           else if (live == 0) {
               Swal.fire({
                   icon: 'warning',
                   //title: 'Warning',
                   text: 'PRESENTLY THERE ARE NO OFFLINE CAMERAS',
                   customClass: {
                       confirmButton: 'btn btn-danger' // Change the class to set the button color to red
                   }
               });
           } 
           else {
               // Call your loadgrid1 function with the appropriate parameters
               LoadData1(encodedData, district);
           }
       }
    function loadgrid1(loc, strdist) {
        debugger;
        var string1 = strdist;
         var string2 = '<%= Session["DistrictDataSet"] != null ? Session["DistrictDataSet"].ToString() : "" %>';
        if (string1.localeCompare(string2) === 0) {
            window.top.location.href = "Gridview.aspx?d=" + loc;
        }
     
    }
    function LoadData1(loc, strdist) {
        var string1 = strdist;
        var string2 = '<%= Session["DistrictDataSet"] != null ? Session["DistrictDataSet"].ToString() : "" %>';
        if (string1.localeCompare(string2) === 0) {
            window.top.location.href = "ListView.aspx?d=" + loc;
        }
    } 
</script>
     <script type="text/javascript"> 
         $('#dashboardmenu').addClass('active');
     </script>
    </asp:Content>
