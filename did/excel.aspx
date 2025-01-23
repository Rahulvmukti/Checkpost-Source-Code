<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="excel.aspx.cs" Inherits="exam.did.excel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DID</title>
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid">
    <div class="row">
            <div class="navbar navbar-default label-warning">
                <h2 class="text-center control-label pull-left">
                    Excel Report</h2>
            </div>
            <div class="col-md-4 bg-white">
                
            </div>
             <div class="col-md-4 bg-white">
                <asp:Button ID="btnreportdst" runat="server" Text="Download ALL Booth Report" CssClass="btn btn-primary btnwidthexcel "
                    OnClick="btnreportdst_Click" />
            </div>
             <div class="col-md-4 bg-white">
              
            </div>
        </div>
        <div class="row">
            <div class="table-responsive">
                <asp:GridView ID="Reportview" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="district" HeaderText="district" />
                      
                        
                      
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
