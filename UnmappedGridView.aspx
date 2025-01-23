<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true"
    CodeBehind="UnmappedGridView.aspx.cs" Inherits="exam.UnmappedGridView" %>

<%@ Register TagPrefix="ucplayer" TagName="player" Src="~/Module/player.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <%--<script src="<%=ResolveUrl("~/js/flv.min.js") %>" type="text/javascript"></script> --%>
    <style>
        .form-group {
    margin-bottom: 0px;
    padding:2px 5px;
        }
       .form-control
       {
           color: #fff;
    background-color: #000;
       }
       .card-header
       {
           padding:5px;
       }
       .btn-default {
    background-color: #3d3e3e;
    border-color: #746e6e;
    color: #f1eaea;
    padding:1px 10px;
}
       .navbar{
           padding:0px;
       }
       .text-justify
       {
           color:lightgreen;
       }
    </style>
    <script src="https://code.jquery.com/jquery-3.6.0.js" type="text/javascript"></script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
    <div class="content-wrapper" style="background-color:#454d55">
        <section class="content">
      <div class="container-fluid">
        
        <div class="row">
          <div class="col-12" style="padding:2px">
            <div class="card" style="background-color:#454d55">
                 <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                <div class="card-header row"> 
                                         <div class="form-group">
                                                    <asp:TextBox ID="txtCameraID" runat="server" placeholder="Enter Camera Id" CssClass="form-control" />
                                                </div>
                                               <div class="form-group">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                            <asp:ListItem Text="Select Status" Value="" Width="100px" />
                                                            <asp:ListItem Text="Online" Value="RUNNING" Selected="True" />
                                                            <asp:ListItem Text="Offline" Value="STOPPED" />
                                                            <asp:ListItem Text="Both" Value="BOTH" />
                                                        </asp:DropDownList>
                                                    </div>
                                                    
                                           <div class="form-group" style="display:none">
                                                    <label class="black">
                                                        Grid</label>
                                                    <asp:DropDownList ID="ddlgrid" runat="server" CssClass="datalist border pr-md pl-xs py-xs" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlgrid_SelectedIndexChanged">
                                                        <asp:ListItem Value="3x2">3x2</asp:ListItem>
                                                        <asp:ListItem Value="4x3" Selected="True">4x3</asp:ListItem>
                                                        <%--<asp:ListItem Value="6x4">6x4</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>
                                            
                                            <div class="form-group" style="display:none">
                                                    <label class="black">
                                                        Timer</label>
                                                    <asp:DropDownList ID="ddlTimer" runat="server" CssClass="datalist border pr-md pl-xs py-xs" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlTimer_SelectedIndexChanged">
                                                        <asp:ListItem Value="15000">15 Seconds</asp:ListItem>
                                                        <asp:ListItem Value="30000">30 Seconds</asp:ListItem>
                                                        <asp:ListItem Value="60000">60 Seconds</asp:ListItem>
                                                        <asp:ListItem Value="90000">90 Seconds</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                          
                                                  <div class="form-group">
                                                        <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-block btn-primary"
                                                            OnClick="btnsearch_Click" />
                                                   </div>
                      <div class="form-group"><div class="text-justify ">
                                Total Online: <asp:Label Text="" ID="lblTotalOnlineCount" runat="server" />
                            </div>
                            </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlgrid" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlTimer" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                           
                <asp:Timer ID="Timer1" runat="server" Interval="90000" OnTick="Timer1_Tick" Enabled="false">

    </asp:Timer>
                 
                  <div class="col-12 row" style="padding:0px">
                     
                    <asp:ListView ID="listview1" runat="server">
                                        <ItemTemplate> 
                                              <div class="col-<%=12/gridcolumns %> pb-xs" style="padding:2px">
                                                 <div class="card" style="margin-bottom:1px" >
                                                    <ucplayer:player ID="playeritem" runat="server" playerURL='<%#Bind("servername") %>'
                                                    playerid='<%# string.Concat(Container.DataItemIndex.ToString(), "_", Eval("streamname"))%>'
                                                    flag='<%#Bind("statusFlag") %>' sid='<%#Bind("id") %>' /> 
                                                       <div class="px-thin" style="position:relative;background:#000; font-size:12px">
                                                             <div class="col-12 md:col-12 col12">
                                                               <p class="data word-break-all" style="margin-bottom:0px">
                                       
                                        <span style="color: #07f5b3;"title='<%# Eval("streamname") %>'><%#(Eval("streamname").ToString().Length >18 ? Eval("streamname").ToString().Substring(0,17) + "...": Eval("streamname").ToString()) %></span>
                                        </p>
                                                      </div>  
                                                                    </div>
                                                      </div> 
                                            </div>  
                                        </ItemTemplate> 
                                        <EmptyDataTemplate>
                                            <div align="center">
                                                <h4>
                                                    <label class="text-center text-danger">
                                                        No Data Found</label>
                                                </h4>
                                            </div>
                                        </EmptyDataTemplate>
                                    </asp:ListView>
                </div>
                <nav class="navbar navbar-default navbar-fixed-bottom" role="navigation">
                                <div class="mrfive">
                                    <div class="text-center">
                                        <div class="page py-xs">
                                            <asp:LinkButton ID="prev" runat="server" Text="First" OnClick="prev_Click" CssClass="btn btn-default btncustom"></asp:LinkButton>
                                            <asp:Repeater ID="rptPages" runat="server" OnItemDataBound="rptPages_ItemDataBound">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btn_page_camera" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%#Eval("Value") %>'
                                                        Enabled='<%#Eval("Enabled") %>' OnClick="Page_Changed"  CssClass="btn btn-default btncustom" />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:LinkButton ID="next" runat="server" Text="Last" OnClick="next_Click"  CssClass="btn btn-default btncustom"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </nav>
                </div>
              </div>
            </div>
          </div>
            </section>
        </div>
      </ContentTemplate>
        <Triggers>      
            <asp:PostBackTrigger ControlID="Timer1" />
                            <asp:PostBackTrigger ControlID="prev" />
                            <asp:PostBackTrigger ControlID="next" />
                            <asp:PostBackTrigger ControlID="rptPages" />
            <asp:PostBackTrigger ControlID="btnsearch" />
                                </Triggers>
                    </asp:UpdatePanel>
                <script type="text/javascript">
                    var lastfocusedElementId = "";
                    function pageLoad() {
                        //$('[data-toggle="popover"]').popover();

                        $(document).click(function (e) {
                            //$('[data-toggle="popover"]').each(function () {
                            //    if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
                            //        $(this).popover('hide');
                            //        lastfocusedElementId = "";
                            //    }
                            //});
                        });

                        $("#btntogglefullscreen").click(function () {

                            var el = parent.document.getElementById("fldiv");


                            if (document.fullscreenEnabled ||
                                document.webkitFullscreenEnabled ||
                                document.mozFullScreenEnabled ||
                                document.msFullscreenEnabled) {

                                if (el.requestFullScreen) {
                                    el.requestFullScreen();
                                } else if (el.mozRequestFullScreen) {
                                    el.mozRequestFullScreen();
                                } else if (el.webkitRequestFullScreen) {
                                    el.webkitRequestFullScreen(Element.ALLOW_KEYBOARD_INPUT);
                                }
                                $('#btntogglefullscreen').find('i').toggleClass('glyphicon-resize-small glyphicon-resize-small');
                            } else {
                                if (document.cancelFullScreen) {
                                    document.cancelFullScreen();
                                } else if (document.mozCancelFullScreen) {
                                    document.mozCancelFullScreen();
                                } else if (document.webkitCancelFullScreen) {
                                    document.webkitCancelFullScreen();
                                }
                                $('#btntogglefullscreen').find('i').toggleClass('glyphicon-resize-full glyphicon-resize-full');
                            }
                        });
                    }
                </script>
       <script type="text/javascript"> 
           $('#viewmenu').addClass('active');
           $('#UnmappedGridViewmenu').addClass('active');
       </script>
        
</asp:Content>















