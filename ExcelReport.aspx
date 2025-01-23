<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ExcelReport.aspx.cs" Inherits="exam.ExcelReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css" />
                <link rel="stylesheet" href="https://cdn.datatables.net/buttons/2.1.0/css/buttons.dataTables.min.css" />
    <style>
        .form-group {
            margin-bottom: 0px;
            padding: 0px 4px;
        }

        button.dt-button {
            padding: 3px 15px;
        }

        .dataTables_wrapper .dataTables_filter input {
            border-radius: 4px !important;
            padding: 0px !important;
        }

        div.dt-buttons {
            padding: 0px 10px !important;
        }

        .ui-datepicker-trigger {
            display: none !important;
        }

        #ui-datepicker-div {
            z-index: 999 !important;
        }

        table.dataTable thead th, table.dataTable thead td {
            padding: 5px 25px !important;
        }
        .status-completed {
    background-color: #dff0d8; /* Light green */
    color: #3c763d; /* Darker green */
    padding: 10px;
    border-radius:-26px;
}

.status-pending {
    background-color: #f2dede; /* Light red */
    color: #a94442; /* Darker red */
    padding: 10px;
    border-radius: -26px;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="content-wrapper">
                <section class="content">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-12">
                                <div class="card">
                                    <div class="card-header row">
                                        <div class="form-group" style="display:none;">
                                            <div id='datepicker' class="row" data-date="" data-link-field="dtp_input2">
                                                <asp:TextBox ID="FromDt" runat="server" CssClass="form-control fromdate" size="16" autocomplete="off" Width="80%" Text='' />
                                            </div>
                                        </div>
                                        <div class="form-group" style="display:none;">
                                           <div id='datepicker2' class="row" data-date="" data-link-field="dtp_input2">
                                                <asp:TextBox ID="ToDt" runat="server" CssClass="todate form-control" size="16" autocomplete="off" Width="80%"
                                                    value="" />
                                            </div>
                                        </div>
                                          <div class="form-group">
                                            <asp:DropDownList ID="ddlzone" runat="server" CssClass="form-control" AutoPostBack="true"
                                                Width="170px" OnSelectedIndexChanged="ddlzone_SelectedIndexChanged" />
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

                                        <div class="form-group" style="display:none;">
                                            <asp:DropDownList ID="ddlislive" runat="server"
                                                AutoPostBack="True" Width="120px"
                                                OnSelectedIndexChanged="ddlislive_SelectedIndexChanged"
                                                CssClass="form-control">
                                                <asp:ListItem Value="-1" Text="Select IsLive" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="YES"></asp:ListItem>
                                                <asp:ListItem Value="0" Text="NO"></asp:ListItem>

                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group" style="display:none;">
                                            <asp:DropDownList ID="ddlcamara" runat="server" Width="120px"
                                                OnSelectedIndexChanged="ddlcamara_SelectedIndexChanged"
                                                AutoPostBack="True" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group" >
                                            <asp:DropDownList ID="ddlStatus" runat="server"
                                                AutoPostBack="True" Width="120px"
                                                OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"
                                                CssClass="form-control">
                                                <asp:ListItem Value="" Text="Select Status" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="Installed" Text="Installed"></asp:ListItem>
                                                <asp:ListItem Value="UnInstalled" Text="UnInstalled"></asp:ListItem>

                                            </asp:DropDownList>
                                        </div>

                                        <div class="form-group">
                                            <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-block btn-primary"
                                                OnClick="btnsearch_Click" />
                                        </div>

                                    </div>

                                    <div class="card-body table-responsive p-0" style="max-height: 500px; overflow: auto">
                                        <table id="datatbl" class="table table-head-fixed">
                                            <thead>
                                                <tr>
                                                       <th class="p-thin text-left tdist">Zone</th>
                                                    <th class="p-thin text-left tdist">District</th>
                                                    <th class="p-thin text-left tacname ">Assembly</th>
                                        
                                                    <th class="p-thin text-left tlocation">VehicleNumber</th>
                                                    <th class="p-thin text-left toperator">Contact No/Operator Name</th>
                                                    <th class="p-thin text-left tdid">Camera Id</th>

                                                  <th class="p-thin text-center">Status</th>
                                                    <th class="p-thin text-center">Installation Date</th>
                                                   <%-- <th class="p-thin text-center">IsLive</th>--%>
                                                

                                                </tr>
                                            </thead>
                                            <tbody>
                                                <% if (dsReport.Tables[0].Rows.Count > 0)%>
                                                <% {%>
                                                <%  for (int i = 0; i < dsReport.Tables[0].Rows.Count; i++)
                                                    {%>
                                                <tr>
                                                     <td class="p-thin text-left tdist"><%=dsReport.Tables[0].Rows[i]["Zone"].ToString() %></td>
                                                    <td class="p-thin text-left tdist"><%=dsReport.Tables[0].Rows[i]["District"].ToString() %></td>
                                                    <td class="p-thin text-left tacname"><%=dsReport.Tables[0].Rows[i]["acname"].ToString() %></td>

                                               
                                                    <td class="p-thin text-left tpsno"><%=dsReport.Tables[0].Rows[i]["vehicleno"].ToString() %></td>
                                                    <td class="p-thin text-left tlocation"><%=dsReport.Tables[0].Rows[i]["drivername"].ToString()+"/"+dsReport.Tables[0].Rows[i]["driverno"].ToString() %></td>

                                                    <td class="p-thin text-left tdid"><%=dsReport.Tables[0].Rows[i]["Camera"].ToString() %></td>

                                                     <td class="p-thin text-center <%= dsReport.Tables[0].Rows[i]["status"].ToString() == "installed" ? "status-completed" : "status-pending" %>">
        <%= dsReport.Tables[0].Rows[i]["status"].ToString() %>
    </td>
                                                <td class="p-thin text-center tonline"><%= Convert.ToDateTime(dsReport.Tables[0].Rows[i]["ReportFromDate"]).ToString("yyyy-MM-dd") %></td>

                                                    <%  }%>
                                                    <% }%>
                                                    <% else
                                                    {%>
                                                    <%for (int i = 0; i < dsReport.Tables[0].Rows.Count; i++)
                                                        {
                                                    %>
                                                    <tr>
                                                        <td class="p-thin text-left tdist"><%=dsReport.Tables[0].Rows[i]["Zone"].ToString() %></td>
                                                        <td class="p-thin text-left tdist"><%=dsReport.Tables[0].Rows[i]["District"].ToString() %></td>
                                                        <td class="p-thin text-left tacname"><%=dsReport.Tables[0].Rows[i]["acname"].ToString() %></td>
                                          <td class="p-thin text-left tpsno"><%=dsReport.Tables[0].Rows[i]["vehicleno"].ToString() %></td>
                                                        <td class="p-thin text-left tlocation"><%=dsReport.Tables[0].Rows[i]["drivername"].ToString() %></td>
                                                        <td class="p-thin text-left toperator"><%=dsReport.Tables[0].Rows[i]["driverno"].ToString() %></td>
                                                        <td class="p-thin text-left tdid"><%=dsReport.Tables[0].Rows[i]["Camera"].ToString() %></td>
                                                        <%-- <td class="p-thin text-left tdate"><%=dsReport.Tables[0].Rows[i]["Dt"].ToString() %></td>--%>
                                                            <td class="p-thin text-center <%= dsReport.Tables[0].Rows[i]["status"].ToString() == "installed" ? "status-completed" : "status-pending" %>">
        <%= dsReport.Tables[0].Rows[i]["status"].ToString() %>  
                                                    </tr> 
                                                    <%  }
                                                        }%>
                                            </tbody>
                                        </table> 
                                    </div> 
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlDistrict" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlAssembly" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlcamara" EventName="SelectedIndexChanged" /> 
            <asp:AsyncPostBackTrigger ControlID="btnsearch" EventName="Click" /> 
        </Triggers>
    </asp:UpdatePanel>

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
             buttonText: "Select date",
             minDate: "26/10/2023",
             maxDate: new Date()
         });
         Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function () {
             $j("#loaderdiv").show();
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
                 minDate: "26/10/2023",
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
                         title: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_excel_title"].ToString()%> - INSTALLATION  REPORT' + '-' + newdate,
                        messageBottom: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_excel_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                        , exportOptions: {
                            columns: ':not(.d-hidden)',
                        }
                    },
                    {
                        extend: 'pdfHtml5',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        title: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_pdf_title"].ToString()%> - INSTALLATION REPORT' + '-' + newdate,
                      messageBottom: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_pdf_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
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
                    //'copy', 'csv', 'excel', 'pdf', 'print'
                    {
                        extend: 'excelHtml5',
                        orientation: 'landscape',
                        pageSize: 'LEGAL', 
                        title: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_excel_title"].ToString()%> - INSTALLATION REPORT' + '-' + newdate,
                        messageBottom: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_excel_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                        , exportOptions: {
                            columns: ':not(.d-hidden)',
                        }
                    },
                    {
                        extend: 'pdfHtml5',
                        orientation: 'landscape',
                        pageSize: 'LEGAL', 
                        title: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_pdf_title"].ToString()%> - INSTALLATION REPORT' + '-' + newdate,
                        messageBottom: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_pdf_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                        , exportOptions: {
                            columns: ':not(.d-hidden)',
                        }
                    }
                ]
            });
        });

    </script>
</asp:Content>
