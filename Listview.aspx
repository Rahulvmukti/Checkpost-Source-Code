<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Listview.aspx.cs" Inherits="exam.Listview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css" />
     <link rel="stylesheet" href="https://cdn.datatables.net/buttons/2.1.0/css/buttons.dataTables.min.css" />
     
    <style>
        .content-wrapper>.content {
    padding: 5px;
}
        .fixheader{
            background-color: #000;
    border-bottom: 0;
    box-shadow: inset 0 1px 0 #dee2e6, inset 0 -1px 0 #dee2e6;
    position: -webkit-sticky;
    position: sticky;
    top: 0;
    z-index: 10; 
    color:#fff;
        }
        .p-td{
            padding:5px;
        }
        .form-group {
    margin-bottom: 0px;
    padding:2px 5px;
        }
        .card-body.p-0 .table tbody>tr>td:last-of-type, .card-body.p-0 .table tbody>tr>th:last-of-type, .card-body.p-0 .table tfoot>tr>td:last-of-type, .card-body.p-0 .table tfoot>tr>th:last-of-type, .card-body.p-0 .table thead>tr>td:last-of-type, .card-body.p-0 .table thead>tr>th:last-of-type {
     padding-right: 0; 
}
        .cardarchive{
             overflow:auto;
         }
        .fixheader
        {
            text-align:center;
        }
       .gridview-center-text td,
.gridview-center-text th {
    text-align: center;
}
    </style>
        <script src="https://code.jquery.com/jquery-3.6.0.js" type="text/javascript"></script>  
    <script src="js/flv.min.js" type="text/javascript"></script>
    <style>
            #videoElementPopup {
                position: relative;
                overflow: auto;
            }

            .vjs-tech {
                position: absolute;
                transition: transform .2s;
                /* Animation */
            }

            .zoom-in-button::before,
            .zoom-out-button::before {
                line-height: 2;
                margin: 0 0.2em;
            }

            .zoom-in-button::before {
                content: "\f00e";
            }

            .zoom-out-button::before {
                content: "\f010";
            }
            .modal-content {
            display: inline-table!important;
        }
            .btn-default input[type="radio"] {
    transform: scale(0.7); /* Adjust the scale factor as needed to make the radio buttons smaller */
    opacity: 0;
}

/* Add this CSS to align the labels and circles to the right */
.form-group.text-right .btn-default {
    float: right;
    margin-left: 10px; /* Adjust the margin as needed */
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
         <asp:Timer ID="Timer1" runat="server" Interval="60000" Enabled="true" OnTick="Timer1_Tick1">
                        </asp:Timer>
        <div class="row">
          <div class="col-12">
            <div class="card">
              <div class="card-header row"> 
                  <div class="form-group">
                                            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" 
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" Width="150px">
                                                </asp:DropDownList>
                        </div>
                    <div class="form-group">
                  <asp:DropDownList ID="ddlAssembly" runat="server" AutoPostBack="true" CssClass="form-control"
                                                    OnSelectedIndexChanged="ddlAssembly_SelectedIndexChanged" Width="150px">
                                                </asp:DropDownList>
                          </div>
                    <div class="form-group">
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select Status" Value="" Selected="True" Width="100px" />
                                                    <asp:ListItem Text="Online" Value="RUNNING" />
                                                    <asp:ListItem Text="Offline" Value="STOPPED" />
                                                    <asp:ListItem Text="Both" Value="BOTH" />
                                                </asp:DropDownList>
                          </div>
                    <div class="form-group" style="display:none;">
                    <asp:DropDownList ID="ddllocationType" runat="server" CssClass="form-control"
                                                    AutoPostBack="true">
                                                        <asp:ListItem Text="ALL" Value="" Selected="True" Width="100px" />
                                                        <asp:ListItem Text="Indoor" Value="inside" />
                                                        <asp:ListItem Text="Outdoor" Value="outside" />
                                                   
                                                </asp:DropDownList>
                          </div>
                    <div class="form-group">
                   <asp:TextBox ID="strm_txtBox" runat="server" AutoPostBack="true" CssClass="form-control"
                                                   Width="150px">
                                                </asp:TextBox>
                          </div>
                    <div class="form-group">
                  <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-block btn-primary"
                                                OnClick="btnsearch_Click" />
                          </div>  
           
<div class="form-group text-right" style="position: absolute; top: 0; right: 10px;" id="indicators" visible="true" runat="server">
    <label class="btn btn-default text-center">
        <input type="radio" name="color_option" id="color_option_a1" autocomplete="off" checked="" style="display:none;">
        ONLINE
        <br>
        <span>🟢</span>
    </label>
    <label class="btn btn-default text-center">
        <input type="radio" name="color_option" id="color_option_a4" autocomplete="off" style="display:none;">
        OFFLINE
        <br>
        <span>🔴</span>
    </label>
</div> 
              </div> 
              <div class="card-body table-responsive p-0" style="max-height: 500px; overflow:auto"> 
                   <div class="panel-body"> 
                        <asp:Label ID="lblonlinecounter" runat="server"></asp:Label>
                                    <div id="scrollDiv" class="pd0" align="center" style="overflow-y: auto; height: calc(108vh - 250px);">
                                        <div align="right" style="width: 95%;color:#4198f5">
                                           <%-- <strong><i>You are viewing page
                                <%=PageNumber%>
                                of
                                <%=(int)Math.Ceiling((double)((decimal)totalDatacount / (decimal)pageitemcount))%>
                                            </i></strong>--%>
                                        </div>
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"  CssClass="table table-head-fixed text-nowrap gridview-center-text"
                                            OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" AlternatingRowStyle-CssClass="bg-light">
                                         
                                            <EmptyDataTemplate>
                                                <div align="center">
                                                    <h4>
                                                        <label class="text-center text-danger">
                                                            No Data Found</label>
                                                    </h4>
                                                </div>
                                            </EmptyDataTemplate>
                                            <Columns>
                                            <asp:TemplateField HeaderText="No" HeaderStyle-CssClass="fixheader">
                                                <ItemTemplate >
                                                    <%# Container.DataItemIndex + 1 + (pageitemcount *(this.PageNumber-1))%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:BoundField DataField="district" HeaderText="District"  HeaderStyle-CssClass="fixheader"/>
                                                <asp:BoundField  DataField="acname" HeaderText="Assembly"  HeaderStyle-CssClass="fixheader"/>
                                                <asp:TemplateField HeaderText="Operator Name" HeaderStyle-CssClass="fixheader" Visible="true" >
                                                    <ItemTemplate>
                                                            <%#Eval("operatorName").ToString() + " / " + Eval("operatorNumber")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 
                                                <asp:TemplateField HeaderText="Location" HeaderStyle-CssClass="fixheader" >
                                                    <ItemTemplate>
                                                        <span title='<%# Eval("location") %>'>
                                                            <%#Eval("location").ToString().Length > 30 ? Eval("location").ToString().PadRight(140).Substring(0, 30).TrimEnd() + "..." : Eval("location")%>
                                                        </span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:BoundField  DataField="streamname" HeaderText="Camera ID"  HeaderStyle-CssClass="fixheader"/> 
                                               
                                                <asp:TemplateField HeaderText="Status"  HeaderStyle-CssClass="fixheader">
                                                    <ItemTemplate>
                                                        <%#Eval("status").ToString() == "RUNNING" ? "🟢" : Eval("status").ToString() == "STOPPED" ? "🔴" : "" %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             
                                                <asp:TemplateField  HeaderStyle-CssClass="fixheader" HeaderText="View"  >
                                                    <ItemTemplate>
                                                        <a href="javascript:void(0)" id="lnkplay" runat="server"></a>
                                                        <div align="center" style="vertical-align: top; display: inline">
                                                            <a href="#" id="viewLink" data-backdrop="static" title="View" data-title=" <%#Eval("acname") %>" data-content=" <%#Eval("location") %>"
                                                                style="color: #1499d6" onclick="OpenPopupAdd(); loadplayer1('<%# Eval("streamname") %>','<%# Eval("servername") %>','<%#exam.Common.Encode(Eval("statusFlag").ToString())%>','<%#exam.Common.Encode(Eval("bkpstreamid") == DBNull.Value ? "0" : Eval("bkpstreamid").ToString())%>');return false">
                                                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-eye">
                                                                    <path d="M1 12s4-8 11-8 11 8 11 8-4 8-11 8-11-8-11-8z"></path><circle cx="12" cy="12" r="3"></circle>
                                                                </svg> 
                                                            </a> 
                                                            </div>
                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField  HeaderStyle-CssClass="fixheader">
                                                    <ItemTemplate>
                                                        <%if (Session["userType"].ToString().ToLower() == "master_admin" || Session["userType"].ToString().ToLower() == "vmukti_internal" || Session["userType"].ToString().ToLower() == "district_level")
                                                            {%>
                                                        <div align="center" style="vertical-align: top; display: inline">
                                                            <a href="camerasetting.aspx?did=<%# Eval("streamname") %>&prourl=<%# Eval("prourl") %>"
                                                        title="Camera Settings"
                                                        target="_blank"
                                                         onclick="openCenteredWindow(this.href, 'CameraSettings', 1200, 760); return false;"
                                                        style="color: #1499d6">
                                                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-settings"><circle cx="12" cy="12" r="3"></circle><path d="M19.4 15a1.65 1.65 0 0 0 .33 1.82l.06.06a2 2 0 0 1 0 2.83 2 2 0 0 1-2.83 0l-.06-.06a1.65 1.65 0 0 0-1.82-.33 1.65 1.65 0 0 0-1 1.51V21a2 2 0 0 1-2 2 2 2 0 0 1-2-2v-.09A1.65 1.65 0 0 0 9 19.4a1.65 1.65 0 0 0-1.82.33l-.06.06a2 2 0 0 1-2.83 0 2 2 0 0 1 0-2.83l.06-.06a1.65 1.65 0 0 0 .33-1.82 1.65 1.65 0 0 0-1.51-1H3a2 2 0 0 1-2-2 2 2 0 0 1 2-2h.09A1.65 1.65 0 0 0 4.6 9a1.65 1.65 0 0 0-.33-1.82l-.06-.06a2 2 0 0 1 0-2.83 2 2 0 0 1 2.83 0l.06.06a1.65 1.65 0 0 0 1.82.33H9a1.65 1.65 0 0 0 1-1.51V3a2 2 0 0 1 2-2 2 2 0 0 1 2 2v.09a1.65 1.65 0 0 0 1 1.51 1.65 1.65 0 0 0 1.82-.33l.06-.06a2 2 0 0 1 2.83 0 2 2 0 0 1 0 2.83l-.06.06a1.65 1.65 0 0 0-.33 1.82V9a1.65 1.65 0 0 0 1.51 1H21a2 2 0 0 1 2 2 2 2 0 0 1-2 2h-.09a1.65 1.65 0 0 0-1.51 1z"></path></svg>
                                                            </a>
                                                        </div>
                                                        <%} %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <nav class="navbar navbar-default navbar-fixed-bottom" role="navigation">
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
                                </nav>
              </div> 
            </div> 
          </div>
        </div> 
      </div> 
    </section>  
          <asp:HiddenField ID="hdstreamname" runat="server" />
                <asp:HiddenField ID="hdservername" runat="server" /> 
  </div>
                            </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="ddlDistrict" />
                                            <asp:PostBackTrigger ControlID="ddlAssembly" />
                                            <asp:PostBackTrigger ControlID="ddlStatus" /> 
                                            <asp:PostBackTrigger ControlID="btnsearch" />
                                        </Triggers>
                                    </asp:UpdatePanel>
    <div class="modal wow fadeInUp animated" id="myModal">
    <div class="modal-dialog">
        <div class="modal-content bg-secondary">
            <div class="modal-header" style="padding:0px 10px">
               <button type="button" onclick="ClosePopupAdd();" class="btnclose close cursor-pointer" id="btncancel" data-dismiss="modal" aria-hidden="true">
    &times;
</button>
            </div>
            <div class="modal-body cardarchive" id="videoContainer">
                <video id="videoElementPopup" class="video-js vjs-default-skin" muted autoplay preload="auto" controls style="height:100%; width:100%">
                </video> 
                  <span onclick="fullscreen(0)" class="full-screen"><i class="fas fa-expand"></i></span>
            </div>
            
        </div> 
    </div> 
</div> 
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet"> 
     <script src="https://code.jquery.com/ui/1.11.1/jquery-ui.min.js"></script>
      <script src="https://code.jquery.com/jquery-3.6.0.js" type="text/javascript"></script>  
    <script type="text/javascript" >   
        var $j = jQuery.noConflict(); 
            var $j = jQuery.noConflict();  
        function OpenPopupAdd() {
            $j("#myModal").show(); 
        }
        function ClosePopupAdd() {
            $j("#myModal").hide();
            if (videojsPlayer) {
                videojsPlayer.dispose(); // Dispose the player to free up resources
                videojsPlayer = null;// Set to null to indicate that the player has been disposed
            }
        } 
        function loadplayer1(streamname, servername, flag, bkupstreamid) {
            debugger
            document.getElementById('<%= hdstreamname.ClientID %>').value = streamname;
            document.getElementById('<%= hdservername.ClientID %>').value = servername;
           
                var videoElement = document.getElementById('videoElementPopup');  
                var flvurl = 'wss://' + servername + '/live-record/' + streamname + '.flv';
                var flvPlayer = flvjs.createPlayer({
                    type: 'flv',
                    url: flvurl
                });
                flvPlayer.attachMediaElement(videoElement);
                flvPlayer.load();
                flvPlayer.play(); 
        }
        function fullscreen(index) {
            document.getElementsByTagName('video')[index].webkitEnterFullscreen();
        }
    </script>
     
    <script type="text/javascript">
        function openCenteredWindow(url, title, width, height) {
            const left = (window.innerWidth - width) / 2;
            const top = (window.innerHeight - height) / 2;
            const options = `width=${width},height=${height},left=${left},top=${top}`;
            window.open(url, title, options);
        }
     
    </script> 
       <script type="text/javascript"> 
           $('#viewmenu').addClass('active');
           $('#listviewmenu').addClass('active');
       </script> 
</asp:Content> 