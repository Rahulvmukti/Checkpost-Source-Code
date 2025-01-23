<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="exam.did.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DID</title>
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid">
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
                        <asp:BoundField DataField="schoolname" HeaderText="schoolname" />
                        <asp:BoundField DataField="psnum" HeaderText="PS No." />
                        <asp:BoundField DataField="location" HeaderText="location" />
                        <asp:BoundField DataField="status" HeaderText="status" />
                        <asp:BoundField DataField="deviceid" HeaderText="DID" />
                        <asp:BoundField DataField="streamname" HeaderText="streamname" />
                        <asp:BoundField DataField="islive" HeaderText="IS Live" />
                        <asp:BoundField DataField="lastseen" HeaderText="Lastseen" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
