<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" 
    CodeBehind="GridView.aspx.cs" Inherits="exam.GridView" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
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
       .loader {
    display: none;
}
      .video-canvas {
    width: 100%; /* or any specific width */
    height: auto; /* maintain aspect ratio, or set a specific height */
    aspect-ratio: 16 / 9;
     background-color: black;
}
     .ptzcard {
        width: calc(100% );  
        
    }
    </style> 
    <script type="text/javascript" >
        $(document).ready(function () { 
            $('#sidebarmini1').prop('checked', false);
            if ($('body').hasClass('sidebar-mini')) {
                $('body').removeClass('sidebar-mini')
            }
            else {
                $('body').addClass('sidebar-mini')
            } 
        });
    </script>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
    <div class="content-wrapper" style="background-color:#454d55">
        <section class="content">
      <div class="container-fluid">
        
        <div class="row" style="margin-right:-22px">
          <div class="col-12" style="padding:2px">
            <div class="card" style="background-color:#454d55"> 
			 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
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
                    <div class="form-group" >
                  <asp:DropDownList ID="ddlgrid" runat="server" CssClass="form-control" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlgrid_SelectedIndexChanged">
                                                <asp:ListItem Value="3x2">3x2</asp:ListItem> 
                                               <%-- <asp:ListItem Value="1x2">1x2</asp:ListItem>--%>
                                               <%-- <asp:ListItem Value="2x1">2x1</asp:ListItem>--%>
                                               <%-- <asp:ListItem Value="2x2">2x2</asp:ListItem>
                                                <asp:ListItem Value="3x3">3x3</asp:ListItem>--%>
                                               <%-- <asp:ListItem Value="4x3">4x3</asp:ListItem> 
                                                 <asp:ListItem Value="4x4">4x4</asp:ListItem> --%>
                                             <%--   <asp:ListItem Value="6x4">6x4</asp:ListItem> --%>
                                            </asp:DropDownList>
                          </div> 
                   <div class="form-group">
                  <asp:DropDownList ID="ddlTimer" runat="server" CssClass="form-control" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlTimer_SelectedIndexChanged">
                                                 <asp:ListItem Value="0">NONE</asp:ListItem>
                                                <asp:ListItem Value="45000">45 Seconds</asp:ListItem> 
                                                <asp:ListItem Value="90000">90 Seconds</asp:ListItem>
                                                <asp:ListItem Value="120000">120 Seconds</asp:ListItem>
                                                <asp:ListItem Value="150000">150 Seconds</asp:ListItem>
                                                 
                                                
                                            </asp:DropDownList>
                          </div>
                       <div class="form-group">
                   <asp:TextBox ID="strm_txtBox" runat="server"  CssClass="form-control"
                                                   Width="150px">
                                                </asp:TextBox>
                          </div>
                     <div class="form-group">
                  <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-block btn-primary"
                                                OnClick="btnsearch_Click" />
                          </div>
                    <div class="form-group" id="dashboardDiv" runat="server">
                                                    <asp:Button ID="Dashboard" runat="server" Text="Back To Dashbord" Visible="false" CssClass="btn btn-block btn-primary"
                                                        OnClick="Dashboard_Click" />

                                                </div>
                    
              </div> 
  </ContentTemplate>			                    
								<Triggers>
                                    <asp:PostBackTrigger ControlID="ddlDistrict" />
                                    <asp:PostBackTrigger ControlID="ddlAssembly" />
                                    <asp:PostBackTrigger ControlID="ddlgrid" />
                                    <asp:PostBackTrigger ControlID="ddlTimer" />
                                      <asp:PostBackTrigger ControlID="btnsearch" />
                                </Triggers>
                                </asp:UpdatePanel>
                 <asp:Timer ID="Timer1" runat="server" Interval="45000" OnTick="Timer1_Tick" Enabled="true">
    </asp:Timer>
                 
                  <div class="col-12 row" style="padding:0px">
           <asp:ListView ID="listview1" runat="server" EnablePartialRendering="true">
    <ItemTemplate>
        <div class="col-<%=12/gridcolumns %> pb-xs" style="padding:2px">
            <div class="card" style="margin-bottom:1px">
                 <div id='<%# "div_" + Eval("streamname") + "_" + Container.DataItemIndex %>' class="ptzcard" data-servername='<%# Eval("servername") %>' style="position:relative">
                  <video id='<%# "canvas_" + Eval("streamname") + "_" + Container.DataItemIndex %>' class="video-canvas" muted controls preload="auto" style="width:100%"></video>
  <span onclick="fullscreen(<%# Container.DataItemIndex %>)" class="full-screen"><i class="fas fa-expand"></i></span>
                </div>
                <div class="px-thin" style="position: relative; background: #0d6c99; font-size: 12px">
                    <div class="col-12 md:col-12 col12">
                        <p class="data word-break-all" style="margin-bottom: 0px; text-align: center;">
                            <span style="color: #FFF;"><%#(Eval("district").ToString()) %>/</span>
                            <span style="color: #FFF;"><%#(Eval("acname").ToString()) %>/</span> 
                            <span style="color: #FFF;"><%#(Eval("location").ToString()) %></span><br />
                            <span style="color: #FFF;" title='<%# Eval("streamname") %>'><%#(Eval("streamname").ToString().Length > 18 ? Eval("streamname").ToString().Substring(0, 17) + "..." : Eval("streamname").ToString()) %></span>
                            <span style="color: #FFF;"><%# Eval("OperatorName").ToString() + " / " + Eval("OperatorNumber").ToString() %></span>
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
                    No Data Found
                </label>
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
    <asp:AsyncPostBackTrigger ControlID="listview1" EventName="PagePropertiesChanged" />
</Triggers>
        
                 </asp:UpdatePanel> 
     <script type="text/javascript"> 
         $('#viewmenu').addClass('active');
         $('#gridviewmenu').addClass('active');
     </script>   
      <script src="js/flv.min.js" type="text/javascript"></script>
        <script type="text/javascript"> 
            $(document).ready(function () { 
                function applyCustomAspectRatio() {
                    $('.video-canvas').each(function () {
                        var videoId = this.id;
                        var streamName = videoId.split('_')[1]; // Adjust based on your ID format
                        if (streamName.startsWith("SSAM")) {
                            this.style.aspectRatio = "12 / 9";
                        } else { 
                            this.style.aspectRatio = "16 / 9";
                        }
                    });
                } 
                var observer = new MutationObserver(function (mutations) {
                    mutations.forEach(function (mutation) {
                        applyCustomAspectRatio(); // Re-apply the aspect ratio rules
                    });
                }); 
                var config = { childList: true, subtree: true };
                 
                var target = document.getElementById('ContentPlaceHolder1'); // Adjust this ID to your container's ID
                 
                if (target) {
                    observer.observe(target, config);
                }  
            });
        </script>

        <script type="text/javascript"> 
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                initializeVideoPlayers();
            });
             
            initializeVideoPlayers();
        </script> 
         <script type="text/javascript"> 
             var videoElements = document.querySelectorAll('.video-canvas');
             videoElements.forEach(function (video, index) {
                 var streamname = video.id.split("_")[1];
                 var servername = $(video).closest('.ptzcard').data('servername'); 
                 LoadNodePlayer(streamname, servername, video);
             });
              
             function initializeVideoPlayers() {
                 var videoElements = document.querySelectorAll('.video-canvas');
                 videoElements.forEach(function (video) {
                     var streamname = video.id.split("_")[1];
                     var servername = $(video).closest('.ptzcard').data('servername'); 
                     video.style.width = '100%';  
                     video.style.height = 'auto';   
                     LoadNodePlayer(streamname, servername, video);
                 });
             }

             function LoadNodePlayer(streamname, servername, video) { 
                 const aspectRatio = streamname.startsWith("SSAM") ? "12:9" : "16:9"; 
                 const customURL = "wss://" + servername + ":443/live-record/" + streamname + ".flv";
                 var flvPlayer = flvjs.createPlayer({
                     type: 'flv',
                     isLive: true,
                     url: customURL,
                     hasAudio: true,
                     hasVideo: true,
                     enableWorker: true,
                     lazyLoadMaxDuration: 1,
                     lazyLoadMaxBW: 500,
                     lazyLoadRecoverDuration: 1,
                     autoplay: true,
                     aspectRatio: aspectRatio, // Use dynamic aspect ratio
                 });

                 flvPlayer.attachMediaElement(video);
                 flvPlayer.load();
                 flvPlayer.play();
             }
             //function adjustAspectRatio(streamname, videoElementId) {
             //    const videoContainer = document.querySelector(`#${videoElementId}`).parentNode;

             //    if (streamname.startsWith("SSAM")) {
             //        videoContainer.style.aspectRatio = "12 / 9";
             //    } else {
             //        videoContainer.style.aspectRatio = "16 / 9";
             //    }
             //}
             function fullscreen(index) {
                 document.getElementsByTagName('video')[index].webkitEnterFullscreen();
             }
         </script> 
</asp:Content>