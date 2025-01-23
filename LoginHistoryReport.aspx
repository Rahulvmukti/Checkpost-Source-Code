<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LoginHistoryReport.aspx.cs" Inherits="exam.LoginHistoryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .select:after {
            width: 0.7rem !important;
            height: 0.7rem !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="3000">
    </asp:ScriptManager>
    <div id="loaderdiv" class="table-responsive" align="center" style="height: calc(100vh - 230px); display: none; position: absolute; margin: -15em auto auto -8em; z-index: 1000; top: 50%; left: 50%; width: 30em; height: 18em;">
        <asp:Image ID="ImgProgress1" runat="server" ImageUrl="images/loading.gif" ToolTip="loading..."
            Width="20%" /><br />
        Please wait...
    </div>
    <section class="flex-auto flex-col">
        <div>
            <div class="col-12 lg:col-12 pb-sm flex-col">
                <div class="bg-white shadow flex-stretch">
                    <div class="bg-white shadow flex-stretch border-b border-default p-xs">
                        <table class="f-size-xxs">
                            <tr>
                                <td class="pr-xs">
                                    <div id='datepicker' class=" pull-right col-md-3 input-group date datepicker border p-thin" style="width: 130px" data-date="" data-link-field="dtp_input2">
                                        <asp:TextBox ID="FromDt" runat="server" CssClass="fromdate form-control" size="16" autocomplete="off" Width="80%" Text='' />
                                    </div>
                                </td>
                                <td class="pr-xs">
                                    <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="cursor-pointer bg-primary f-color-white f-size-2xs f-weight-semibold hover:shadow-md transition-all px-2xs py-thin"
                                        OnClick="btnsearch_Click" />
                                </td>
                                <td class="pr-xs d-hidden">
                                    <asp:Button ID="btnExportPDF" runat="server" OnClick="btnExportPDF_Click" Text="PDF Report" CssClass="cursor-pointer bg-primary f-color-white f-size-2xs f-weight-semibold hover:shadow-md transition-all px-2xs py-thin" />
                                    <%--<asp:Button ID="btnExport" runat="server" Text="PDF Report" CssClass="cursor-pointer bg-primary f-color-white f-size-2xs f-weight-semibold hover:shadow-md transition-all px-2xs py-thin"
                                                OnClick="btnExport_Click" />--%>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="row-fluid">
                        <div id="scrollDiv" class="pd0 f-size-xxs" align="center" style="overflow-y: auto;">

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div id="divReport" runat="server">
                                        <div style="overflow-x: scroll; width: calc(100vw - 220px);">
                                            <table border="1" id="datatbl" class="table-striped stacked table-zebra w-full text-center capitalize border-t border-default">
                                                <thead>
                                                    <tr>
                                                        <th class="p-thin text-left">User Name</th>
                                                        <th class="p-thin text-left">User Role</th>
                                                        <th class="p-thin text-left">IPAddress</th>
                                                        <th class="p-thin text-left">Login Date/Time</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <%for (int i = 0; i < dsReport.Tables[0].Rows.Count; i++)
                                                        {
                                                    %>
                                                    <tr>
                                                        <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["username"].ToString() %></td>
                                                        <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["usercode"].ToString() %></td>
                                                        <td class="p-thin text-left"><%=dsReport.Tables[0].Rows[i]["IPAddress"].ToString() %></td>
                                                        <td class="p-thin text-left"><%=Convert.ToDateTime(dsReport.Tables[0].Rows[i]["LoginDateTime"]).ToString("dd/MM/yyyy hh:mm:ss tt") %></td>
                                                    </tr>
                                                    <%} %>
                                                </tbody>
                                            </table>

                                        </div>
                                    </div>

                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnsearch" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <script>
        var $j = jQuery.noConflict();
        $j(".fromdate").datepicker({
            dateFormat: 'dd/mm/yy',
            showOn: "both",
            buttonImage: "images/calender.png",
            //buttonImageOnly: true,
            buttonText: "Select date"
        });
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function () {
            $j("#loaderdiv").show();
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            $j("#loaderdiv").hide();
            $j(".fromdate").datepicker({
                dateFormat: 'dd/mm/yy',
                showOn: "both",
                buttonImage: "images/calender.png",
                //buttonImageOnly: true,
                buttonText: "Select date"
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
                        //orientation: 'landscape',
                        pageSize: 'LEGAL',
                        title: 'GUJRAT By Election 2022 - USER LOGIN HISTORY REPORT',
                        messageBottom: 'This is system generated report on ' + $(".fromdate").val() + ', Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                        //messageBottom: 'This is system genereted report. Printed on 20/02/2022 10:16:21 PM'
                    },
                    {
                        extend: 'pdfHtml5',
                        //orientation: 'landscape',
                        pageSize: 'LEGAL',
                        title: 'GUJRAT By Election 2022 - USER LOGIN HISTORY REPORT',
                        messageBottom: 'This is system generated report on ' + $(".fromdate").val() + ', Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                        //messageBottom: 'This is system genereted report. Printed on 20/02/2022 10:16:49 PM'
                        , customize: function (doc) {
                            doc.content[1].table.widths =
                                Array(doc.content[1].table.body[0].length + 1).join('*').split('');
                        }
                    }
                ]
            });
        });
    </script>
    <script>$j(document).ready(function () {
            $j('#datatbl').DataTable({
                "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
                "pageLength": 10,
                "bSort": false,
                dom: 'Bfrtip',
                buttons: [
                    //'copy', 'csv', 'excel', 'pdf', 'print'
                    {
                        extend: 'excelHtml5',
                        //orientation: 'landscape',
                        pageSize: 'LEGAL',
                        title: 'GUJARAT VIDHAN SABHA ELECTION 2022 - USER LOGIN HISTORY REPORT',
                        messageBottom: 'This is system generated report on ' + $(".fromdate").val() + ', Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                        //messageBottom: 'This is system genereted report. Printed on 20/02/2022 10:16:21 PM'
                    },
                    {
                        extend: 'pdfHtml5',
                        //orientation: 'landscape',
                        pageSize: 'LEGAL',
                        title: 'GUJARAT VIDHAN SABHA ELECTION 2022 - USER LOGIN HISTORY REPORT',
                        messageBottom: 'This is system generated report on ' + $(".fromdate").val() + ', Downloaded on <%=TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("dd/MM/yyyy hh:mm:ss tt")%>'
                        //messageBottom: 'This is system genereted report. Printed on 20/02/2022 10:16:49 PM'
                        , customize: function (doc) {
                            doc.content[1].table.widths =
                                Array(doc.content[1].table.body[0].length + 1).join('*').split('');
                        }
                    }
                ]
            });
});</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
