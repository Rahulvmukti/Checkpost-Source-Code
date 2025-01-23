<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="assemblydashboard.aspx.cs" Inherits="exam.assemblydashboard"
    ViewStateEncryptionMode="Always" %>

<div>
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <div class="f-size-sm f-color-secondary border-default border-y text-left p-xs">
            Assembly List
        </div>
        <div>
        <asp:gridview id="grdDashboardDetail" cssclass="stacked table-zebra w-full text-center capitalize border-t border-default" runat="server" autogeneratecolumns="false"
            OnRowCommand="grdDashboardDetail_RowCommand" alternatingrowstyle-cssclass="bg-light" width="100%">
    <%--   <PagerSettings Position="TopAndBottom" />
        <PagerStyle VerticalAlign="Bottom" />--%>
    <EmptyDataTemplate>
        <div align="center">
            <h4>
                <label class="text-center text-danger f-size-2xs">
                    No Data Found</label>
            </h4>
        </div>
    </EmptyDataTemplate>
    <Columns>
        <asp:TemplateField HeaderText="" ItemStyle-CssClass="p-thin text-left f-size-xxs pl-sm" ItemStyle-Width="3%" HeaderStyle-Width="3%" HeaderStyle-CssClass="pl-sm f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-left">
            <ItemTemplate>&nbsp;</ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Assembly" HeaderStyle-Width="35%" ItemStyle-CssClass="p-thin text-left f-size-xxs pl-sm" HeaderStyle-CssClass="pl-sm f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-left">
            <ItemTemplate><%# Eval("DATA") %></ItemTemplate>
        </asp:TemplateField>
        <%--<asp:TemplateField HeaderText="<%$appSettings:assemblyname%>" ItemStyle-CssClass="p-thin" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs">
        <ItemTemplate><%# Eval("Acname") %></ItemTemplate>
    </asp:TemplateField>--%>
        <%--<asp:TemplateField HeaderText="Total Booth" ItemStyle-CssClass="p-thin text-center f-size-xxs" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-center">
            <ItemTemplate>
                <asp:Label ID="Label1" Text='<%# Eval("Total") %>' runat="server" ForeColor="Green" />
            </ItemTemplate>
        </asp:TemplateField>--%>
        <asp:TemplateField HeaderText="Total" HeaderStyle-Width="20%" ItemStyle-CssClass="p-thin text-center f-size-xxs" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-center">
            <ItemTemplate>
                <asp:LinkButton ID="lnkTotalBooth" Text='<%# Eval("TotalBooth") %>' runat="server" CssClass="no-underline" ForeColor="Green" Visible='<%# Eval("TotalBooth").ToString() != "0" %>' CommandName="Total" CommandArgument='<%# Eval("DATA").ToString() + "|" + Eval("CameraType").ToString() %>' />
                <asp:Label ID="lblTotalBooth" Text='<%# Eval("TotalBooth") %>' runat="server" ForeColor="Green" Visible='<%# Eval("TotalBooth").ToString() == "0" %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Online" HeaderStyle-Width="20%" ItemStyle-CssClass="p-thin text-center f-size-xxs" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-center">
            <ItemTemplate>
                <asp:LinkButton ID="lnkLive" text='<%# Eval("Live") %>' runat="server" CssClass="no-underline" ForeColor="Green" Visible='<%# Eval("Live").ToString() != "0" %>' CommandName="Live" CommandArgument='<%# Eval("DATA").ToString() + "|" + Eval("CameraType").ToString() %>' />
                <asp:Label ID="lblLive" Text='<%# Eval("Live") %>' runat="server" ForeColor="Green" Visible='<%# Eval("Live").ToString() == "0" %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Offline" HeaderStyle-Width="20%" ItemStyle-CssClass="p-thin text-center f-size-xxs" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-center">
            <ItemTemplate>
                <asp:LinkButton ID="lnkstop" Text='<%# Eval("stop") %>' runat="server" ForeColor="Red" CssClass="no-underline" Visible='<%# Eval("stop").ToString() != "0" %>' CommandName="stop" CommandArgument='<%# Eval("DATA").ToString() + "|" + Eval("CameraType").ToString() %>' />
                <asp:Label ID="lblstop" Text='<%# Eval("stop") %>' runat="server" ForeColor="Red" Visible='<%# Eval("stop").ToString() == "0" %>' />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:gridview>
            </div>
        <div id="divDashboardDetails" runat="server" class="row-fluid d-inline-block" style="width: 100%!important;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdDashboardDetails" CssClass="stacked table-zebra w-full text-center capitalize border-t border-default" runat="server" AutoGenerateColumns="false"
                        AlternatingRowStyle-CssClass="bg-light" Width="100%">
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
                        <Columns>
                            <%--<asp:TemplateField HeaderText="<%$appSettings:district%>" ItemStyle-CssClass="p-thin f-size-xxs f-color-derfault text-left" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">
                                <ItemTemplate><%# Eval("District") %></ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Sr. No." ItemStyle-CssClass="p-thin f-size-xxs f-color-derfault text-left" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">
                                <ItemTemplate><%# (Container.DataItemIndex + 1) %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$appSettings:assemblyname%>" HeaderStyle-Width="20%" ItemStyle-CssClass="p-thin f-size-xxs f-color-derfault text-left" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">
                                <ItemTemplate><%# Eval("Acname") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location" HeaderStyle-Width="30%" ItemStyle-CssClass="p-thin f-size-xxs f-color-derfault text-left" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">
                                <ItemTemplate><%# Eval("PSNum").ToString() + " - " + Eval("location").ToString() %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Camera ID" HeaderStyle-Width="20%" ItemStyle-CssClass="p-thin f-size-xxs f-color-derfault text-left" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">
                                <ItemTemplate><%# Eval("streamname") %></ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Status" ItemStyle-CssClass="p-thin f-size-xxs f-color-derfault text-left" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">
                                <ItemTemplate><%# Eval("Status").ToString() == "RUNNING" ? "Online" : Eval("Status").ToString() == "STOPPED" ? "Offline" : Eval("Status").ToString() %></ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Operator Name" HeaderStyle-Width="15%" ItemStyle-CssClass="p-thin f-size-xxs f-color-derfault text-left" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">
                                <ItemTemplate><%# Eval("drivername") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact No" HeaderStyle-Width="15%" ItemStyle-CssClass="p-thin f-size-xxs f-color-derfault text-left" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">
                                <ItemTemplate><%# Eval("drivercontactno") %></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="grdDashboardDetail" EventName="RowCommand" />
                    <%--<asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />--%>
                    <%--<asp:AsyncPostBackTrigger ControlID="grdDashboard" EventName="RowCommand" />--%>
                    <%--<asp:AsyncPostBackTrigger ControlID="ddlReasons" EventName="SelectedIndexChanged" />--%>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </form>
</div>
<%--<asp:ListView ID="listview1" runat="server">
    <itemtemplate>
        <div class="col-md-12">
            <div class="btn btn-default" style="width: 100%">
                <h4 title='<%#Eval("DATA") %>' class=" pull-left">
                    <%#Eval("DATA").ToString().Length > 15 ? Eval("DATA").ToString().PadRight(140).Substring(0, 15).TrimEnd() + "..." : Eval("DATA")%>
                </h4>
                <span class="pull-right">
                    <%if (strcode == "007")
                        { %>
                    <span class='btn btn-primary btnwidth' onclick="LoadData('<%#exam.Common.Encode("007$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');">
                        <%#Eval("TotalBooth")%></span>
                    <%}
                        else if (strcode == "000")
                        {%>
                    <span class='btn btn-success btnwidth' onclick="LoadData('<%#exam.Common.Encode("000$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');">
                        <%#Eval("Live") %></span>
                    <%}
                        else if (strcode == "001")
                        {%>
                    <span class='btn btn-info btnwidth' onclick="LoadData('<%#exam.Common.Encode("001$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');">
                        <%#Eval("lastLive") %></span>
                    <%}
                        else if (strcode == "003")
                        {%>
                    <span class='btn btn-warning btnwidth' onclick="LoadData('<%#exam.Common.Encode("003$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');">
                        <%#Eval("Connectedonce") %></span>
                    <%}
                        else if (strcode == "002")
                        {%>
                    <span class='btn btn-danger btnwidth' onclick="LoadData('<%#exam.Common.Encode("002$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');">
                        <%#Eval("stop") %></span>
                    <%}
                        else
                        {%>
                    <span class='btn btn-primary btnwidth' onclick="LoadData('<%#exam.Common.Encode("007$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');">
                        <%#Eval("TotalBooth")%></span>
                    <span class='btn btn-warning btnwidth' onclick="LoadData('<%#exam.Common.Encode("000$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');">
                        <%#Eval("Connectedonce") %></span>
                    <span class='btn btn-success btnwidth' onclick="LoadData('<%#exam.Common.Encode("000$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');">
                        <%#Eval("Live") %></span>
                    <span class='btn btn-info btnwidth' onclick="LoadData('<%#exam.Common.Encode("000$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');">
                        <%#Eval("lastLive") %></span> <span class='btn btn-danger btnwidth' onclick="LoadData('<%#exam.Common.Encode("002$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');">

                            <%#Eval("stop") %></span>
                    <%} %>
                </span>
            </div>
        </div>
    </itemtemplate>
</asp:ListView>--%>
<script type="text/javascript">
    function LoadData(loc) {
        window.top.location.href = "Listview.aspx?d=" + loc;
    }
    $j(document).on('click', '.ui-dialog-titlebar-close', function () {
        $j("#<%=divDashboardDetails.ClientID%>").hide();
    })
</script>

