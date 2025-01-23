<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="TrailRunRpt.aspx.cs" Inherits="exam.TrailRunRpt" %>
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
   <%--<asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>--%> 
    <div class="content-wrapper"> 
    <section class="content">
      <div class="container-fluid">
            <div class="row">
          <div class="col-12">
            <div class="card">
                  <div class="card-header row">  
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
                       <div class="select">
                                                <asp:DropDownList ID="ddlStatus" runat="server"
                                                    AutoPostBack="True" Width="150px"
                                                    OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"
                                                   CssClass="form-control">
                                                    <asp:listitem Value="" Text="All"></asp:listitem>
                                                    <asp:listitem Value="Connected" Text="Connected"></asp:listitem>
                                                    <asp:listitem Value="Not Connected" Text="Not Connected"></asp:listitem>
                                                </asp:DropDownList>
                                            </div>
                                            </div> 
                         <div class="form-group">
                       <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-block btn-primary"
                                                OnClick="btnsearch_Click" />
                      </div>
                      </div>


                <div class="card-body table-responsive p-0" style="max-height: 500px; overflow:auto">
                 <table  id="datatbl" class="table table-head-fixed">
                       <thead>
                                                <tr>
                                                     <th class="p-thin text-left">District</th>
                                                    <th class="p-thin text-left">Assembly</th>
                                                    <th class="p-thin text-left">PS No</th>
                                                    <th class="p-thin text-left">Vehical No</th> 
                                                    <th class="p-thin text-left">Driver Name / Contact No</th> 
                                                    <th class="p-thin text-left">Camera No</th>
                                                    <th class="p-thin text-left">Status</th> 
                                                </tr>
                                                    </thead>
                     <tbody>
                                              <% if (dsReport.Tables[0].Rows.Count > 0)%>
                                                    <% {%>
                                                         <%for (int i = 0; i < dsReport.Tables[0].Rows.Count; i++)
                                                    { 
                                                          %>
                                                                
                                                   <tr>
                                                    <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["District"].ToString() %></td>
                                                    <td class="p-thin text-left"><%=(dsReport.Tables[0].Rows[i]["acname"]).ToString()%></td> 
                                                 <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["PSNum"].ToString() %></td>
                                                <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["location"].ToString() %></td>
                                                   <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["operatorName"].ToString()%></td>
                                                    <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["streamname"].ToString() %></td>
                                                     <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["Trial"].ToString() %></td>
                                                </tr>
                                                <%} %>
                          <%} %>
                                                    </tbody>
                     </table>
                      </div>
                </div>
              </div>

            </div>
          </div>
          </section>
          </div> 
                           <%--  </ContentTemplate> 
                    </asp:UpdatePanel> --%>
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
    <script >
        var $j = jQuery.noConflict();
        //$j(".fromdate, .todate").datepicker({
        //    dateFormat: 'dd/mm/yy',
        //    showOn: "both",
        //    buttonImage: "images/calender.png",
        //    //buttonImageOnly: true,
        //    buttonText: "Select date",
        //    //minDate: "06/06/2022",
        //    //maxDate: "26/06/2022"
        //});
        //Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function () {
        //    $j("#loaderdiv").show();
        //});

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            //var date = $(".fromdate").val();
            //var newdate = date.split("/").join("-");
            //$j(".fromdate, .todate").datepicker({
            //    dateFormat: 'dd/mm/yy',
            //    showOn: "both",
            //    buttonImage: "images/calender.png",
            //    //buttonImageOnly: true,
            //    buttonText: "Select date",
            //    //minDate: "06/06/2022",
            //    //maxDate: "26/06/2022"
            //});
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
                        title: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_excel_title"].ToString()%> - Trial Run Report',
                        messageBottom: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_excel_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                        , exportOptions: {
                            columns: ':not(.d-hidden)',
                        }
                    },
                    {
                        extend: 'pdfHtml5',
                        orientation: 'landscape',
                        pageSize: 'LEGAL',
                        title: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_pdf_title"].ToString()%> - Trial Run REPORT',
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
            //var date = $(".fromdate").val();
            //var newdate = date.split("/").join("-");
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
                        title: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_excel_title"].ToString()%> - Trial Run REPORT',
                        messageBottom: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_excel_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                        , exportOptions: {
                            columns: ':not(.d-hidden)',
                        }
                    },
                    {
                        extend: 'pdfHtml5',
                        orientation: 'landscape',
                        pageSize: 'LEGAL', 
                        title: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_pdf_title"].ToString()%> - Trial Run REPORT',
                        messageBottom: '<%=ConfigurationManager.AppSettings["CameraStatusReport_2_pdf_footer"].ToString()%> , Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
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
        $('#TrialRunRpt').addClass('active');
    </script>
</asp:Content>
