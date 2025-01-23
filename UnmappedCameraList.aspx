<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UnmappedCameraList.aspx.cs" Inherits="exam.UnmappedCameraList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .page td table {
            margin: auto;
            margin-top: 1rem;
        }

        .page span {
            background: #1c3480;
            font-size: 1.1rem;
            padding: 5px 8px;
            color: #fff;
            border: 1px solid transparent;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <section class="flex-auto flex-col">
        <div>
            <%-- class="container"--%>
            <div class="col-12 lg:col-12 pb-sm flex-col">
                <div class="bg-white shadow flex-stretch">
                    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="50000">
                    </asp:Timer>
                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div class="bg-white shadow flex-stretch border-b border-default p-xs">
                                <%-- p-sm--%>
                                <table class="f-size-xxs">
                                    <tr>
                                        <td class="pr-xs">
                                            <asp:TextBox ID="txtCameraID" runat="server" placeholder="Enter Camera Id" CssClass="border p-xs" />
                                            <%--<input type="text" id="txtCameraID" runat="server" placeholder="Enter Camera Id" class="border p-xs txtCameraID" />--%>
                                        </td>
                                        <td class="pr-xs">
                                            <div class="select">
                                                <%-- <label> <asp:Literal ID="Literal2" runat="server" Text="<%$appSettings:assemblyname%>" /></label>--%>
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="datalist border pr-md pl-xs py-xs"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select Status" Value="" Selected="True" Width="100px" />
                                                    <asp:ListItem Text="Online" Value="RUNNING" />
                                                    <asp:ListItem Text="Offline" Value="STOPPED" />
                                                    <asp:ListItem Text="Both" Value="BOTH" />
                                                </asp:DropDownList>
                                            </div>
                                            <td class="pr-xs">
                                                <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="cursor-pointer bg-primary f-color-white f-size-2xs f-weight-semibold hover:shadow-md transition-all px-2xs py-thin"
                                                    OnClick="btnsearch_Click" />
                                            </td>
                                         <td class="pr-xs" style="padding:5px">
                                               <Span class="">Camera Status:<asp:Label runat="server" ID="Label1"></asp:Label></Span>
                                            </td>
                                        <td class="pr-xs bg-primary f-color-white" style="padding:5px">
                                               <Span class="">Total:<asp:Label runat="server" ID="lblTotal"></asp:Label></Span>
                                            </td>
                                         <td class="pr-xs bg-success f-color-white" style="padding:5px">
                                                 <Span class="">Online:<asp:Label runat="server" ID="lblOnline"></asp:Label></Span>
                                            </td>
                                           <td class="pr-xs bg-warning f-color-white" style="padding:5px">
                                               <Span class="">Offline:<asp:Label runat="server" ID="lblOffline"></asp:Label></Span>
                                            </td>
                                          
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlStatus" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <div class="panel panel-primary">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <div class="panel-body">
                                    <div id="scrollDiv" class="pd0" align="center" style="overflow-y: auto; height: calc(108vh - 250px);">
                                        <asp:GridView ID="GridView1" CssClass="stacked table-zebra w-full text-center capitalize border-t border-default f-size-1xs" runat="server" AutoGenerateColumns="false"
                                            AlternatingRowStyle-CssClass="bg-light" PageSize="50" AllowPaging="true" PagerStyle-CssClass="page" OnPageIndexChanging="GridView1_PageIndexChanging"
                                            OnRowCommand="GridView1_RowCommand">
                                            <EmptyDataTemplate>
                                                <div align="center">
                                                    <h4>
                                                        <label class="text-center text-danger">
                                                            No Data Found</label>
                                                    </h4>
                                                </div>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="No" ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-left">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-left" DataField="streamname" HeaderText="Camera ID" />
                                                <asp:BoundField ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-left" DataField="servername" HeaderText="Server Name" />
                                                <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-left">
                                                    <ItemTemplate>
                                                        <%#Eval("status").ToString() == "RUNNING" ? "Online" : Eval("status").ToString() == "STOPPED" ? "Offline" : "" %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Map Camera" ItemStyle-CssClass="p-thin text-left" HeaderStyle-CssClass="f-size-2xs f-color-secondary border-default border-b sr-header p-xs text-left">
                                                    <ItemTemplate>
                                                        <asp:Button runat="server" CssClass="cursor-pointer bg-primary f-color-white f-size-2xs f-weight-semibold hover:shadow-md transition-all px-2xs py-thin"
                                                            Style="padding: 2px" Text="Map Camera" ID="btnMapping" CommandName="Submit" OnClientClick="OpenPopup()" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="hidden fild" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblid" runat="server" Text='<%# Eval("streamname")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="hidden fild" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSID" runat="server" Text='<%# Eval("id")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <%--<nav class="navbar navbar-default navbar-fixed-bottom" role="navigation">
                                    <div class="mrfive">
                                        <div class="page text-center">
                                            <div>
                                                <asp:LinkButton ID="prev" runat="server" Text="«" OnClick="prev_Click" CssClass="btn btn-default btncustom"></asp:LinkButton>
                                                <asp:Repeater ID="rptPages" runat="server" OnItemDataBound="rptPages_ItemDataBound">
                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="btn_page_camera" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%#Eval("Value") %>'
                                                            Enabled='<%#Eval("Enabled") %>' OnClick="Page_Changed" CssClass="btn btn-default btncustom" />

                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:LinkButton ID="next" runat="server" Text="»" OnClick="next_Click" CssClass="btn btn-default btncustom" ></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </nav>--%>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlStatus" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="btnsearch" EventName="Click" />
                                <%--<asp:AsyncPostBackTrigger ControlID="next" EventName="Click" />--%>
                                <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PageIndexChanging" />
                                <%--<asp:AsyncPostBackTrigger ControlID="prev" EventName="Click" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                        <div class="modal wow fadeInUp animated" id="popupMapcamera" tabindex="-1" data-wow-animation-name="fadeInUp"
                            style="visibility: visible; animation-name: fadeInUp; -webkit-animation-name: fadeInUp;">
                            <div class="modal-dialog">
                                <%-- <asp:UpdatePanel ID="upModal" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <div class="d-inline-block" style="width: 90%;">
                                            <h4 class="modal-title" id="H1">Map Camera
                                            </h4>
                                        </div>
                                        <div class="d-inline-block text-right f-right pr-sm" style="width: 10%;">
                                            <button type="button" onclick="ClosePopup()" class="btnclose close cursor-pointer" id="btncancel" data-dismiss="modal" aria-hidden="true">
                                                &times;
                                            </button>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <div class="modal-body">
                                                <div style="text-align: center">
                                                    <asp:Label class="error" ID="lbl_error" runat="server" Style="color: #dc7c3c;">
                                                    </asp:Label>
                                                </div>
                                                <table cellspacing="5" cellpadding="5">
                                                    <tr>
                                                        <td>
                                                            <label style="padding: 4px; font-size: 15px;">Camera ID</label>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:TextBox runat="server" ID="txtCameraID1" AutoPostBack="true" Enabled="false"
                                                                Style="font-size: 15px; border: 1px solid #ccc; padding: 3px">
                                                </asp:TextBox>
                                                            <asp:Label runat="server" ID="lblSID" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label style="padding: 4px; font-size: 15px;">District</label>
                                                        </td>
                                                        <td colspan="2">
                                                            <div class="select">
                                                                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="datalist border pr-md pl-xs py-xs ddlDistrict" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" Style="padding: 4px; font-size: 15px; width: 185px">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label style="padding: 4px; font-size: 15px;">Assembly</label>
                                                        </td>
                                                        <td colspan="2">
                                                            <div class="select">
                                                                <asp:DropDownList ID="ddlAssembly" runat="server" AutoPostBack="true" CssClass="ddlPC datalist border pr-md pl-xs py-xs"
                                                                    Style="padding: 4px; font-size: 15px; width: 185px" OnSelectedIndexChanged="ddlAssembly_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <label style="padding: 4px; font-size: 15px;">Location</label>
                                                        </td>
                                                        <td colspan="2">
                                                            <div class="select">
                                                                <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="true" CssClass="datalist border pr-md pl-xs py-xs"
                                                                    Style="padding: 4px; font-size: 15px; width: 185px" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                            <asp:Label runat="server" ID="lblboothID" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr class="d-hidden">
                                                        <td>
                                                            <label style="padding: 4px; font-size: 15px;">Operator</label>
                                                        </td>
                                                        <td colspan="2">
                                                            <div class="select">
                                                                <asp:DropDownList ID="ddlOpertor" runat="server" AutoPostBack="true" CssClass="datalist border pr-md pl-xs py-xs"
                                                                    Style="padding: 4px; font-size: 15px; width: 185px" OnSelectedIndexChanged="ddlOpertor_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="modal-footer" style="text-align: center">
                                                <asp:Button ID="btnMapCamera" runat="server" Text="Map Camera" CssClass="cursor-pointer bg-primary f-color-white f-size-2xs f-weight-semibold hover:shadow-md transition-all px-2xs py-thin"
                                                    OnClick="btnMapCamera_Click"/>
                                                <asp:Button ID="btnclose" Text="Cancel" runat="server" OnClientClick="ClosePopup()" OnClick="btnclose_Click" class="btnclose cursor-pointer bg-gray f-color-default f-size-2xs f-weight-semibold hover:shadow-md transition-all px-2xs py-thin" data-dismiss="modal" aria-hidden="true" />
                                                <%--<button type="button" onclick="ClosePopup()" class="btnclose cursor-pointer bg-gray f-color-default f-size-2xs f-weight-semibold hover:shadow-md transition-all px-2xs py-thin" id="btnclose" data-dismiss="modal" aria-hidden="true">Cancel</button>--%>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

        </div>
        <script type="text/javascript">
            //jQuery(function ($) {
            //    var xPos, yPos;
            //    //var prm = Sys.WebForms.PageRequestManager.getInstance();
            //    //prm.add_beginRequest(BeginRequestHandler);
            //    //prm.add_endRequest(EndRequestHandler);
            //    //var nodeId;
            //    function BeginRequestHandler(sender, args) {
            //        $("#scrollDiv").hide();
            //        xPos = $get('scrollDiv').scrollLeft;
            //        yPos = $get('scrollDiv').scrollTop;
            //    }
            //    function EndRequestHandler(sender, args) {
            //        $get('scrollDiv').scrollLeft = xPos;
            //        $get('scrollDiv').scrollTop = yPos;
            //        $("#scrollDiv").show();
            //    }
            //});
            function OpenPopup() {
                $("#popupMapcamera").show();
            }
            function ClosePopup() {
                $("#popupMapcamera").hide();
            }
        </script>
    </section>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>--%>
