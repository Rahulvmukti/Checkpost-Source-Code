<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArchiveView.aspx.cs" Inherits="exam.ArchiveView" MasterPageFile="~/Admin.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css" />
                <link rel="stylesheet" href="https://cdn.datatables.net/buttons/2.1.0/css/buttons.dataTables.min.css" />
       <style>
           .form-group {
    margin-bottom: 0px;
    padding:2px 5px;
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
       button.dt-button
        {
            padding: 3px 15px;
        }
        .ui-datepicker-trigger{
            display:none!important;
        }
        #ui-datepicker-div
        {
            z-index:999!important;
        }
           ul {
               color:white
           }
               video::-webkit-media-controls {  /*Works only on Chrome-based browsers */
        display: flex;
    }
    </style>
    <script src="https://code.jquery.com/jquery-3.6.0.js" type="text/javascript"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
   
                              <script src="<%=ResolveUrl("~/js/flv.min.js") %>" type="text/javascript"></script> 
   <%-- <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>--%>
                      <div class="content-wrapper"> 
                           
    <section class="content">
      <div class="container-fluid">
            <div class="row">
          <div class="col-12" style="padding:3px">
            <div class="card" >
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                      <ContentTemplate>
                  <div class="card-header row"> 
                 <div class="form-group"> 
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="true"
                                                    Width="150px" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" /> 
                        </div>
                    <div class="form-group"> 
                         <asp:DropDownList ID="ddlAssembly" runat="server" AutoPostBack="true" CssClass="form-control"
                                                    Width="150px" OnSelectedIndexChanged="ddlAssembly_SelectedIndexChanged" />
                          </div>
                    <div class="form-group">
                   <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="true" CssClass="form-control"
                                                    Width="150px" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" /> 
                          </div>
                    <div class="form-group">
                   <div id='datepicker' class="row" data-date="" data-link-field="dtp_input2" >
                    <asp:TextBox ID="FromDt" runat="server" CssClass="form-control fromdate" size="16" autocomplete="off" Width="80%" Text=''/>
                          </div></div>
                         <div class="form-group">
                              <asp:TextBox ID="txtDID" runat="server" CssClass="form-control"  placeholder="Camera ID" Width="80%"></asp:TextBox>
                          </div>  
                    <div class="form-group">
                <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-block btn-primary"
                                               OnClick="btnsearch_Click"/> 
                          </div>  
              </div><div style="display:none">
            <asp:TextBox ID="lblservername" runat="server" ForeColor="White" ></asp:TextBox>
                                                <asp:TextBox ID="lblcameraid" runat="server"  ForeColor="White" ></asp:TextBox>
        </div>
                  </ContentTemplate>
                    </asp:UpdatePanel>
                </div> 
              <div class="row">
          <div class="col-md-3">
            <!-- Widget: user widget style 2 -->
            <div class="card card-widget widget-user-2">
              <!-- Add the bg color to the header using any of the bg-* classes -->
              <div class="widget-user-header bg-warning"> 
                <h5 class="widget-user-desc"></h5>
              </div>
              <div class="card-footer p-0" style="height:400px; overflow:auto">
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                <ul class="nav flex-column">
    <% if (dsReport != null) { %>
        <% int leftPos = 0; %>
        <% for (int i = 0; i < dsReport.Rows.Count; i++) { %>
            <% string flvname = dsReport.Rows[i]["a_Text"].ToString(); %>
            <% string filename = flvname.Substring(11, flvname.Length - 15).Replace("-", ":"); %>
            <li class="nav-item">
                <a href="#" class="nav-link" onclick="loadplayer('<%= dsReport.Rows[i]["a_Text"].ToString() %>');">
                    <%= filename %>
                </a>
            </li>
        <% } %>
    <% }else{ %>
                     <li class="nav-item">
                <a href="#" class="nav-link">
                   No Record Found
                </a>
            </li>
                  <% } %>   
</ul>
 </ContentTemplate>
                    <Triggers>   
                         <asp:AsyncPostBackTrigger ControlID="ddlDistrict" EventName="SelectedIndexChanged" />
                         <asp:AsyncPostBackTrigger ControlID="ddlAssembly" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlLocation" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnsearch" EventName="Click" />
                     </Triggers>
                 </asp:UpdatePanel> 
              </div>
            </div>
            <!-- /.widget-user -->
          </div> 
<div class="col-md-7">
     <div class="card">
            <video id="videoElement" muted autoplay preload="auto" controls style="width:100%; height:400px; padding:10px">
            </div>
              <div class="row col-12 md:col-12 col12 pb-xs" align="center" style="padding-left:10px">  
                    <button style="font-size:13px; display:none" id="zoomInButton" ><i class="material-icons">zoom_in</i></button> 
                         <button style="font-size:13px; display:none" id="ResetzoomButton"><i class="material-icons">center_focus_strong</i></button> 
                         <button style="font-size:13px; display:none" id="zoomOutButton"><i class="material-icons">zoom_out</i></button> 
                               <button style="font-size:24px" id="rewindBtn"><i class="material-icons">skip_previous</i></button>&nbsp&nbsp&nbsp
                                <button style="font-size:24px" id="forwordBtn"><i class="material-icons">skip_next</i></button>&nbsp&nbsp&nbsp
                                <button style="font-size:24px" id="downloadBtn"><i class="material-icons">file_download</i></button>
                                          </div>
          </div>
    </div>

                   </div> 
           </section>
          </div> 
     <script src="js/flv.min.js"></script> 
  <link href="http://vjs.zencdn.net/c/video-js.css" rel="stylesheet"> 
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet"> 
     <script src="https://code.jquery.com/ui/1.11.1/jquery-ui.min.js"></script>
    <script>
        var $j = jQuery.noConflict(); 
        $j(".fromdate, .todate").datepicker({
            dateFormat: 'yy-mm-dd',
            showOn: "both",
            buttonImage: "images/calender.png",
            //buttonImageOnly: true,
            buttonText: "Select date",
            minDate: "2023-04-01",
            //maxDate: "26/06/2022"
        });
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            var date = $(".fromdate").val();
            var newdate = date.split("/").join("-");
            $j(".fromdate, .todate").datepicker({
                dateFormat: 'yy-mm-dd',
                showOn: "both",
                buttonImage: "images/calender.png",
                //buttonImageOnly: true,
                buttonText: "Select date",
                minDate: "2023-04-01",
                //maxDate: "26/06/2022"
            });
        });
        function OpenPopupAdd() { 
            $j("#myModal").show();
            flvurl = "";
        }
        function ClosePopupAdd() {
            $j("#myModal").hide();
        }
        var flvurl = "";
        function loadplayer(name) { 
            flvurl = "";
            if (flvjs.isSupported()) {
                var videoElement = document.getElementById('videoElement');
                var serverName = $('#<%= lblservername.ClientID %>').val();
                 var cameraid = $('#<%= lblcameraid.ClientID %>').val();
                  flvurl = 'https://' + serverName + '/live-record/' + cameraid + '/' + name;
                 var flvPlayer = flvjs.createPlayer({
                     type: 'flv',
                     // url: 'https://punjab106.vmukti.com/live-record/ANYK-803582-AAAAA/2023-03-07-13-07-03.flv'
                     url: flvurl
                     //url: 'https://goa3.vmukti.com/live-record/ANYK-800791-AAAAA/2023-04-01-20-51-23.flv'
                 });
                 flvPlayer.attachMediaElement(videoElement);
                 flvPlayer.load();
                 flvPlayer.play();
                 videoElement.addEventListener('play', function () {
                     console.log('Video is playing');
                 });
                 videoElement.addEventListener('pause', function () {
                     console.log('Video is paused');
                 });
                 videoElement.addEventListener('timeupdate', function () {
                     console.log('Current time: ' + videoElement.currentTime);
                 });
                 var skipBackwardButton = document.getElementById("rewindBtn");
                 skipBackwardButton.addEventListener('click', function () {
                     event.preventDefault();
                     videoElement.currentTime -= 10;
                 });
                 var skipForwardButton = document.getElementById("forwordBtn");
                 skipForwardButton.addEventListener('click', function () {
                     event.preventDefault();
                     videoElement.currentTime += 10;
                 });
                

                var downloadButton = document.getElementById("downloadBtn");
                downloadButton.addEventListener('click', function (event) {
                   event.preventDefault(); 

                    var url = flvurl; 
                    window.open(url);  
                });

                var ResetzoomButton = document.getElementById("ResetzoomButton");
                var zoomInButton = document.getElementById("zoomInButton");
                var zoomOutButton = document.getElementById("zoomOutButton");
                var videoElement = document.getElementById('videoElement');
                var zoomLevel = 1;

                ResetzoomButton.addEventListener('click', function () {
                    event.preventDefault();
                    zoomLevel = 1;
                    applyZoom();
                });

                zoomInButton.addEventListener('click', function () {
                    event.preventDefault();
                    zoomLevel += 0.1;
                    applyZoom();
                });

                zoomOutButton.addEventListener('click', function () {
                    event.preventDefault();
                    if (zoomLevel > 0.1) {
                        if (zoomLevel != 1)
                            zoomLevel -= 0.1;
                        applyZoom();
                    }
                });

                function applyZoom() {
                    videoElement.style.transform = 'scale(' + zoomLevel + ')';
                }
                   
             }
         }
    </script>

    <script type="text/javascript"> 
        $('#viewmenu').addClass('active');
        $('#Archiveviewmenu').addClass('active');
    </script>

	
	
</asp:Content>
