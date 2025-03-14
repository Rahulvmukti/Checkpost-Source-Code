﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="CameraOfflineReport.aspx.cs" Inherits="exam.CameraOfflineReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
                <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css" />
                <link rel="stylesheet" href="https://cdn.datatables.net/buttons/2.1.0/css/buttons.dataTables.min.css" />
                
    <style>
        .form-group {
    margin-bottom: 0px;
    padding: 0px 4px;
}
        button.dt-button
        {
            padding: 3px 15px;
        }
        .dataTables_wrapper .dataTables_filter input { 
    border-radius: 4px!important;
    padding: 0px!important;
}
        div.dt-buttons { 
    padding: 0px 10px!important;
}
        .ui-datepicker-trigger{
            display:none!important;
        }
        #ui-datepicker-div
        {
            z-index:999!important;
        }
        table.dataTable thead th, table.dataTable thead td {
    padding: 5px 25px!important; 
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
<%--    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>--%>
    <div class="content-wrapper"> 
    <section class="content">
      <div class="container-fluid">
            <div class="row">
          <div class="col-12">
            <div class="card">
                  <div class="card-header row"> 
                 <div class="form-group"> 
                      <div id='datepicker' class="row" data-date="" data-link-field="dtp_input2">
                                                <asp:TextBox ID="FromDt" runat="server" CssClass="form-control fromdate" size="16" autocomplete="off" Width="80%" Text=''/>
                                            </div>
                     </div>
                        <div class="form-group" style="display:none">
                              <div id='datepicker2' class="row" data-date="" data-link-field="dtp_input2">
                                                <asp:TextBox ID="ToDt" runat="server" CssClass="todate form-control" size="16" autocomplete="off" Width="80%" Text=''/> 
                                            </div>
                      </div>
                       <div class="form-group" style="display:none">
                           <asp:DropDownList ID="ddlFromTime" runat="server" Width="170px"
                                                    CssClass="form-control">
                                                    <asp:ListItem Text="Select From Time" Value="" />
                                                    <asp:ListItem Text="12:00 AM" Value="00:00:00" />
                                                    <asp:ListItem Text="12:30 AM" Value="00:30:00" />
                                                    <asp:ListItem Text="1:00 AM" Value="01:00:00" />
                                                    <asp:ListItem Text="1:30 AM" Value="01:30:00" />
                                                    <asp:ListItem Text="2:00 AM" Value="02:00:00" />
                                                    <asp:ListItem Text="2:30 AM" Value="02:30:00" />
                                                    <asp:ListItem Text="3:00 AM" Value="03:00:00" />
                                                    <asp:ListItem Text="3:30 AM" Value="03:30:00" />
                                                    <asp:ListItem Text="4:00 AM" Value="04:00:00" />
                                                    <asp:ListItem Text="4:30 AM" Value="04:30:00" />
                                                    <asp:ListItem Text="5:00 AM" Value="05:00:00" />
                                                    <asp:ListItem Text="5:30 AM" Value="05:30:00" />
                                                    <asp:ListItem Text="6:00 AM" Value="06:00:00" />
                                                    <asp:ListItem Text="6:30 AM" Value="06:30:00" />
                                                    <asp:ListItem Text="7:00 AM" Value="07:00:00" />
                                                    <asp:ListItem Text="7:30 AM" Value="07:30:00" />
                                                    <asp:ListItem Text="8:00 AM" Value="08:00:00" />
                                                    <asp:ListItem Text="8:30 AM" Value="08:30:00" />
                                                    <asp:ListItem Text="9:00 AM" Value="09:00:00" />
                                                    <asp:ListItem Text="9:30 AM" Value="09:30:00" />
                                                    <asp:ListItem Text="10:00 AM" Value="10:00:00" />
                                                    <asp:ListItem Text="10:30 AM" Value="10:30:00" />
                                                    <asp:ListItem Text="11:00 AM" Value="11:00:00" />
                                                    <asp:ListItem Text="11:30 AM" Value="11:30:00" />
                                                    <asp:ListItem Text="12:00 PM" Value="12:00:00" />
                                                    <asp:ListItem Text="12:30 PM" Value="12:30:00" />
                                                    <asp:ListItem Text="1:00 PM" Value="13:00:00" />
                                                    <asp:ListItem Text="1:30 PM" Value="13:30:00" />
                                                    <asp:ListItem Text="2:00 PM" Value="14:00:00" />
                                                    <asp:ListItem Text="2:30 PM" Value="14:30:00" />
                                                    <asp:ListItem Text="3:00 PM" Value="15:00:00" />
                                                    <asp:ListItem Text="3:30 PM" Value="15:30:00" />
                                                    <asp:ListItem Text="4:00 PM" Value="16:00:00" />
                                                    <asp:ListItem Text="4:30 PM" Value="16:30:00" />
                                                    <asp:ListItem Text="5:00 PM" Value="17:00:00" />
                                                    <asp:ListItem Text="5:30 PM" Value="17:30:00" />
                                                    <asp:ListItem Text="6:00 PM" Value="18:00:00" />
                                                    <asp:ListItem Text="6:30 PM" Value="18:30:00" />
                                                    <asp:ListItem Text="7:00 PM" Value="19:00:00" />
                                                    <asp:ListItem Text="7:30 PM" Value="19:30:00" />
                                                    <asp:ListItem Text="8:00 PM" Value="20:00:00" />
                                                    <asp:ListItem Text="8:30 PM" Value="20:30:00" />
                                                    <asp:ListItem Text="9:00 PM" Value="21:00:00" />
                                                    <asp:ListItem Text="9:30 PM" Value="21:30:00" />
                                                    <asp:ListItem Text="10:00 PM" Value="22:00:00" />
                                                    <asp:ListItem Text="10:30 PM" Value="22:30:00" />
                                                    <asp:ListItem Text="11:00 PM" Value="23:00:00" />
                                                    <asp:ListItem Text="11:30 PM" Value="23:30:00" />
                                                    <asp:ListItem Text="11:59 PM" Value="23:59:59" />
                                                </asp:DropDownList>
                      </div>
                       <div class="form-group"style="display:none">
                           <asp:DropDownList ID="ddlToTime" runat="server" Width="150px"
                                                    CssClass="form-control">
                                                    <asp:ListItem Text="Select To Time" Value="" />
                                                    <asp:ListItem Text="12:00 AM" Value="00:00:00" />
                                                    <asp:ListItem Text="12:30 AM" Value="00:30:00" />
                                                    <asp:ListItem Text="1:00 AM" Value="01:00:00" />
                                                    <asp:ListItem Text="1:30 AM" Value="01:30:00" />
                                                    <asp:ListItem Text="2:00 AM" Value="02:00:00" />
                                                    <asp:ListItem Text="2:30 AM" Value="02:30:00" />
                                                    <asp:ListItem Text="3:00 AM" Value="03:00:00" />
                                                    <asp:ListItem Text="3:30 AM" Value="03:30:00" />
                                                    <asp:ListItem Text="4:00 AM" Value="04:00:00" />
                                                    <asp:ListItem Text="4:30 AM" Value="04:30:00" />
                                                    <asp:ListItem Text="5:00 AM" Value="05:00:00" />
                                                    <asp:ListItem Text="5:30 AM" Value="05:30:00" />
                                                    <asp:ListItem Text="6:00 AM" Value="06:00:00" />
                                                    <asp:ListItem Text="6:30 AM" Value="06:30:00" />
                                                    <asp:ListItem Text="7:00 AM" Value="07:00:00" />
                                                    <asp:ListItem Text="7:30 AM" Value="07:30:00" />
                                                    <asp:ListItem Text="8:00 AM" Value="08:00:00" />
                                                    <asp:ListItem Text="8:30 AM" Value="08:30:00" />
                                                    <asp:ListItem Text="9:00 AM" Value="09:00:00" />
                                                    <asp:ListItem Text="9:30 AM" Value="09:30:00" />
                                                    <asp:ListItem Text="10:00 AM" Value="10:00:00" />
                                                    <asp:ListItem Text="10:30 AM" Value="10:30:00" />
                                                    <asp:ListItem Text="11:00 AM" Value="11:00:00" />
                                                    <asp:ListItem Text="11:30 AM" Value="11:30:00" />
                                                    <asp:ListItem Text="12:00 PM" Value="12:00:00" />
                                                    <asp:ListItem Text="12:30 PM" Value="12:30:00" />
                                                    <asp:ListItem Text="1:00 PM" Value="13:00:00" />
                                                    <asp:ListItem Text="1:30 PM" Value="13:30:00" />
                                                    <asp:ListItem Text="2:00 PM" Value="14:00:00" />
                                                    <asp:ListItem Text="2:30 PM" Value="14:30:00" />
                                                    <asp:ListItem Text="3:00 PM" Value="15:00:00" />
                                                    <asp:ListItem Text="3:30 PM" Value="15:30:00" />
                                                    <asp:ListItem Text="4:00 PM" Value="16:00:00" />
                                                    <asp:ListItem Text="4:30 PM" Value="16:30:00" />
                                                    <asp:ListItem Text="5:00 PM" Value="17:00:00" />
                                                    <asp:ListItem Text="5:30 PM" Value="17:30:00" />
                                                    <asp:ListItem Text="6:00 PM" Value="18:00:00" />
                                                    <asp:ListItem Text="6:30 PM" Value="18:30:00" />
                                                    <asp:ListItem Text="7:00 PM" Value="19:00:00" />
                                                    <asp:ListItem Text="7:30 PM" Value="19:30:00" />
                                                    <asp:ListItem Text="8:00 PM" Value="20:00:00" />
                                                    <asp:ListItem Text="8:30 PM" Value="20:30:00" />
                                                    <asp:ListItem Text="9:00 PM" Value="21:00:00" />
                                                    <asp:ListItem Text="9:30 PM" Value="21:30:00" />
                                                    <asp:ListItem Text="10:00 PM" Value="22:00:00" />
                                                    <asp:ListItem Text="10:30 PM" Value="22:30:00" />
                                                    <asp:ListItem Text="11:00 PM" Value="23:00:00" />
                                                    <asp:ListItem Text="11:30 PM" Value="23:30:00" />
                                                    <asp:ListItem Text="11:59 PM" Value="23:59:59" />
                                                </asp:DropDownList>
                      </div>
                       <div class="form-group">
                           <asp:DropDownList ID="ddlDistrict" runat="server"
                                                    AutoPostBack="True" Width="150px"
                                                    OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                    CssClass="form-control">
                                                </asp:DropDownList>
                      </div>
                       <div class="form-group">
                           <asp:DropDownList ID="ddlAssembly" runat="server"
                                                    AutoPostBack="True" Width="165px"
                               OnSelectedIndexChanged="ddlAssembly_SelectedIndexChanged"
                                                    CssClass="form-control">
                                                </asp:DropDownList>
                      </div>
                         <div class="form-group">
                       <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-block btn-primary"
                                                OnClick="btnsearch_Click" />
                      </div>
                       <div class="form-group">
                           <asp:Label runat="server" ID="lblmsg" Text="" style="color:#4198f5; font-style:italic"></asp:Label>
                             <asp:Label runat="server" ID="lblmsgfooter" Text="" Visible="false" ></asp:Label>
                           </div>
                           <div class="navbar-nav ml-auto"> 
                           <h5>Camera Downtime Report</h5>
                           </div>
                      </div>


                  <div class="card-body table-responsive p-0" style="max-height: 500px; overflow:auto">
                 <table  id="datatbl" class="table table-head-fixed">
                       <thead>
                                                <tr>
                                                    <th class="p-thin text-left tdist">District</th> 
                                                    <th class="p-thin text-left tacname ">Assembly</th>
                                         <%--                <th class="p-thin text-left tpsno">PS-No</th>--%> 
                                                    <th class="p-thin text-left tlocation">Location</th> 
                                                  <%--  <th class="p-thin text-left toperinfo">Operator Info</th>--%>
                                                    <th class="p-thin text-left tdid">Camera Id</th> 
                                                    <th class="p-thin text-left tstarttime">Start Time</th>
                                                    <th class="p-thin text-left tendtime">End Time</th>
                                                    <th class="p-thin text-center ttime">Time (HH:MM)</th>
                                                    <%--<th class="p-thin text-left tremark">Remarks</th>--%>
                                                </tr>
                                                    </thead>
                     <tbody>
                           <% if (dsReport1.Tables[0].Rows.Count > 0)%>
                                                    <% {%>
                                                          <%  for (int i = 0; i < dsReport1.Tables[0].Rows.Count; i++) {
                                                  // TimeSpan datediff = TimeSpan.FromMinutes(Convert.ToInt32(dsReport1.Tables[0].Rows[i]["DiffMin"]));%>

                         <% 
    int diffMin = Convert.ToInt32(dsReport1.Tables[0].Rows[i]["DiffMin"]);
    int hours = diffMin / 60;
    int minutes = diffMin % 60;
    string formattedTime = hours.ToString() + ":" + minutes.ToString("00");
                           
%>
                                                    <tr >
                                                          <% if (dsReport1.Tables[0].Rows[i]["StopTime"].ToString()==""){%>
                                                         <td class="p-thin text-left tdist"><%=dsReport1.Tables[0].Rows[i]["district"].ToString() %></td> 
                                                         <td class="p-thin text-left tdist"><%=dsReport1.Tables[0].Rows[i]["acname"].ToString() %></td> 
                                                        <%-- <td class="p-thin text-left tdist"><%=dsReport1.Tables[0].Rows[i]["psnum"].ToString() %></td> --%>
                                                     
                                                         <%} %>
                                                          <%  else{%>
                                                        <td class="p-thin text-left tdist"></td> 
                                                        <td class="p-thin text-left tdist"></td> 
                                                         <%} %>
                                                     <td class="p-thin text-left tlocation"><%=dsReport1.Tables[0].Rows[i]["VehicleNo"].ToString() %></td>
                                                          <% if (dsReport1.Tables[0].Rows[i]["StopTime"].ToString()==""){%>
                                                         <td class="p-thin text-left tdid"><%=dsReport1.Tables[0].Rows[i]["streamname"].ToString() %></td>
                                                     
                                                        <%} else{%>
                                                     <td class="p-thin text-left tdid"></td>
                                                         <%} %>
                                                       <td class="p-thin text-left tstarttime"><%=dsReport1.Tables[0].Rows[i]["StartTime"].ToString() %></td>

                                                        <% if (dsReport1.Tables[0].Rows[i]["StopTime"].ToString()==""){%>
                                                          <td class="p-thin text-left tstarttime">TOTAL</td> 
                                                         <%} else{%>

                                                       <td class="p-thin text-left tstarttime"><%=dsReport1.Tables[0].Rows[i]["StopTime"].ToString() %></td>
                                                         <%} %> 
                                             <td class="p-thin text-center tendtime"><%= formattedTime %></td>
                                                     <%--<td  class="p-thin text-center ttime"><%= datediff.TotalHours == 24 && datediff.Minutes == 0 ? "24:00" : datediff.TotalHours.ToString("00") == "24" && datediff.Minutes > 0 ? ("23:" + datediff.Minutes.ToString("00")) : (datediff.Hours.ToString("00") + ":" + datediff.Minutes.ToString("00")) %></td>--%>
                                                   <%-- <td class="p-thin text-left tremark"><%=dsReport.Tables[0].Rows[i]["Reason"].ToString() %></td>--%>
                                                        </tr>
                         <%}%>
                         <tr>
                            
                             <td></td>
                                  <td></td>
                           <%--<td class="p-thin text-right" style="font-weight:bold">Total Downtime:- </td> 
                                 <td id="Td1" class="p-thin text-left" runat="server" style="font-weight:bold">From <%= FromDt.Text %> 09:00:00  </td> 
                       <td id="myTd" runat="server" style="font-weight:bold">To <%= ToDt.Text +" "+ lblmsgfooter.Text %></td>--%> 
                             <td class="p-thin text-right" style="font-weight:bold"> </td> 
                                 <td id="Td1" class="p-thin text-left" runat="server" style="font-weight:bold"> </td> 
                       <td id="myTd" runat="server" style="font-weight:bold"></td>

                                          <% 
    int TotalSum = Convert.ToInt32(dsReport1.Tables[1].Rows[0]["TotalSum"]);
    int hours1 = TotalSum / 60;
    int minutes1 = TotalSum % 60;
    string TotalSum1 = hours1.ToString() + ":" + minutes1.ToString("00"); 
%>
                                   <td class="p-thin text-left tendtime" style="font-weight:bold">Total Downtime:- </td> 
                                   <td class="p-thin text-left tendtime" style="font-weight:bold"><%= TotalSum1 %></td> 
                         </tr>
                         </tbody>
                      <%} %>
                     </table>
                      </div>
                </div>
              </div>

            </div>
          </section>
          </div> 
                       <%--     </ContentTemplate> 
                    </asp:UpdatePanel>--%>
       <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.2.4/jquery.min.js"></script>
                <script src="https://cdn.datatables.net/1.11.3/js/jquery.dataTables.min.js"></script>
                <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.11.3/css/jquery.dataTables.min.css" />
                <script src="https://code.jquery.com/ui/1.11.1/jquery-ui.min.js"></script>
                 <script src="https://cdn.datatables.net/buttons/2.1.0/js/dataTables.buttons.min.js"></script>
                <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
                <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
                <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
                <script src="https://cdn.datatables.net/buttons/2.1.0/js/buttons.html5.min.js"></script>
                <script src="https://cdn.datatables.net/buttons/2.1.0/js/buttons.print.min.js"></script>      
    <script>
        var $j = jQuery.noConflict();
        $j(".fromdate, .todate").datepicker({
            dateFormat: 'dd/mm/yy',
            showOn: "both",
            buttonImage: "images/calender.png",
            //buttonImageOnly: true,
            buttonText: "Select date"
            , minDate: "01/03/2024",
            maxDate: new Date()
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            var date = $(".fromdate").val();
            var newdate = date.split("/").join("-");
            $j(".fromdate, .todate").datepicker({
                dateFormat: 'dd/mm/yy',
                showOn: "both",
                buttonImage: "images/calender.png",
                //buttonImageOnly: true,
                buttonText: "Select date",
                minDate: "01/03/2024",
                maxDate: new Date()
            });
            $j('#datatbl').DataTable().destroy();
            $j('#datatbl').DataTable({
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                "pageLength": 10,
                "bSort": false,
                dom: 'Bfrtip',
                buttons: [
                    //'copy', 'csv', 'excel', 'pdf', 'print'
                    {
                        extend: 'excelHtml5',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        title: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_excel_title"].ToString()%> - CAMERA OFFLINE REPORT' + '-' + newdate,
                        messageBottom: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_excel_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                        , exportOptions: {
                            columns: ':not(.d-hidden)',
                        }
                    },
                    {
                        extend: 'pdfHtml5',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        title: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_pdf_title"].ToString()%> - CAMERA OFFLINE REPORT' + '-' + newdate,
                        messageBottom: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_pdf_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                        , exportOptions: {
                            columns: ':not(.d-hidden)',
                        }
                    }
                ]
            });
        });
    </script>
       <script>
           $j(document).ready(function () {
               var date = $(".fromdate").val();
               var newdate = date.split("/").join("-");
               $j('#datatbl').DataTable({
                   "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                   "pageLength": 10,
                   "bSort": false,
                   dom: 'Bfrtip',
                   buttons: [
                       {
                           extend: 'excelHtml5',
                           orientation: 'landscape',
                           pageSize: 'LEGAL',
                           title: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_excel_title"].ToString()%> - CAMERA OFFLINE REPORT' + '-' + newdate,
                messageBottom: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_excel_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                , exportOptions: {
                    columns: ':not(.d-hidden)',
                }
            },
            {
                extend: 'pdfHtml5',
                orientation: 'landscape',
                pageSize: 'LEGAL',
                title: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_pdf_title"].ToString()%> - CAMERA OFFLINE REPORT' + '-' + newdate,
                    messageBottom: '<%=ConfigurationManager.AppSettings["ConsolidatedCameraOffline_2_pdf_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                , exportOptions: {
                    columns: ':not(.d-hidden)',
                }
            }
        ]
    });
           });

       </script>
    <script type="text/javascript"> 
        $('#Reportmenu').addClass('active');
        $('#offlineReport').addClass('active');
    </script>
</asp:Content>