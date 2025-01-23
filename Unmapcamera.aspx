<%@ Page Title="" Language="C#" MasterPageFile="~/MainOld.Master" AutoEventWireup="true"
    CodeBehind="Unmapcamera.aspx.cs" Inherits="exam.Unmapcamera" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.9/js/jquery.dataTables.min.js"></script>
    <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="bg-info">
        <div class="row">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <div class="form-inline">
                        <div style="text-align:right;">
                            <div class="form-group">
                                <asp:Button ID="btnExcelreport" runat="server" Text="Export To Excel" Visible="true"
                                    CssClass="form-control btn btn-secondary" OnClick="btnExcelreport_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="container-fluid">
                        <asp:GridView ID="GridView1" CssClass="table table-striped table-bordered table-hover"
                            runat="server" AutoGenerateColumns="false" AlternatingRowStyle-CssClass="gridalternateRow"
                            OnRowDataBound="GridView1_RowDataBound" Font-Size="11px" PageSize="500">
                            <%--   <PagerSettings Position="TopAndBottom" />
                            <PagerStyle VerticalAlign="Bottom" />--%>
                            <EmptyDataTemplate>
                                <div align="center">
                                    <h4>
                                        <label class="text-center text-danger">
                                            No Data Found</label>
                                    </h4>
                                </div>
                            </EmptyDataTemplate>
                            <AlternatingRowStyle CssClass="gridalternateRow"></AlternatingRowStyle>
                            <Columns>
                                <asp:BoundField DataField="streamname" HeaderText="streamname" />
                                <asp:BoundField DataField="servername" HeaderText="streamname" />
                                <asp:BoundField DataField="status" HeaderText="streamname" />
                            </Columns>
                            <PagerSettings PageButtonCount="500" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= GridView1.ClientID %>').DataTable();
        });
    </script>
</asp:Content>
