<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="header.ascx.cs" Inherits="exam.Module.header" %>
<header>
    <nav class="navbar navbar-default navbar-inverse mb" role="navigation">
        <asp:LoginView ID="LoginView2" runat="server">
            <AnonymousTemplate>
                <div class="container-fluid">
                    <nav class="navbar navbar-default navbar-inverse navbar-fixed-top" role="navigation">
                        <div class="container">
                            <h1 id="title" class="text-center" runat="server">
                                <%=ConfigurationManager.AppSettings["header"]%></h1>
                        </div>
                    </nav>
                </div>
            </AnonymousTemplate>
            <LoggedInTemplate>
                <div class="container-fluid">
                    <nav class="navbar navbar-default navbar-inverse navbar-fixed-top" role="navigation">
                        <div class="container">
                            <h1 id="H1" class="text-center" runat="server">
                                <%=ConfigurationManager.AppSettings["header"]%></h1>
                        </div>
                    </nav>
                </div>
                   <div class="container-fluid" style="display:none;">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#navbarCollapse">
                            <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span><span
                                class="icon-bar"></span><span class="icon-bar"></span>
                        </button>
                        <h4 id="title" class="white text-center" runat="server" style="margin-top: 14px">
                          </h4>
                    </div>
                    <div class="collapse navbar-collapse" id="navbarCollapse">
                       <ul class="nav navbar-nav navbar-left">
                            <li><a href="javascript:void(0);"><span class="glyphicon glyphicon-user"></span>&nbsp; <%=Page.User.Identity.Name %></a> </li>
                           <%if (useridentifier == 1)
                               { %>
                              <li ><a href="DashboardTN.aspx"><span class="glyphicon glyphicon-th"></span>&nbsp;Dashboard</a> </li>   
                               <li class="nav-item dropdown">
	                            <a class="nav-link  dropdown-toggle" href="#" data-toggle="dropdown"><span class="glyphicon glyphicon-th-list"></span>&nbsp;View  </a>
	                            <ul class="dropdown-menu fade-up">
		                                            <li><a class="dropdown-item" href="home.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Single View</a> </li>
                                                   <li><a class="dropdown-item" href="listview.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;List View</a> </li>
                                                    <li ><a class="dropdown-item" href="MultiGrid.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Grid View</a> </li>
	                            </ul>
	                            </li>
                            <li ><a href="gridauto.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Auto</a> </li> 
                            
                            
                                     
                            <%}else if (useridentifier == 2) { %>
                             <li ><a href="DashboardTN.aspx"><span class="glyphicon glyphicon-th"></span>&nbsp;Dashboard</a> </li>   
                             <li class="nav-item dropdown">
	                            <a class="nav-link  dropdown-toggle" href="#" data-toggle="dropdown"><span class="glyphicon glyphicon-th-list"></span>&nbsp;View  </a>
	                            <ul class="dropdown-menu fade-up">
		                                            <li><a class="dropdown-item" href="home.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Single View</a> </li>
                                                   <li><a class="dropdown-item" href="listview.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;List View</a> </li>
                                                    <li ><a class="dropdown-item" href="MultiGrid.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Grid View</a> </li>
	                            </ul>
	                        </li>
                            <li ><a href="gridauto.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Auto</a> </li>
                             <%--<li><a href="download.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Download</a> </li>--%>
                             <li class="nav-item dropdown">
	                            <a class="nav-link  dropdown-toggle" href="#" data-toggle="dropdown"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Reports  </a>
	                            <ul class="dropdown-menu fade-up">
		                                            <li><a class="dropdown-item" href="uptimebooth.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Minute Wise</a> </li>
                                                   <li><a class="dropdown-item" href="uptimeHr.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Hour Wise</a> </li>
	                            </ul>
	                        </li>
                                             
                            <%}else if (useridentifier == 3) { %>
                               <li><a class="dropdown-item" href="boothupload.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Booth Upload</a> </li>
                            <li ><a href="DashboardTN.aspx"><span class="glyphicon glyphicon-th"></span>&nbsp;Dashboard</a> </li>   
                             <li class="nav-item dropdown">
	                            <a class="nav-link  dropdown-toggle" href="#" data-toggle="dropdown"><span class="glyphicon glyphicon-th-list"></span>&nbsp;View  </a>
	                            <ul class="dropdown-menu fade-up">
		                                            <li><a class="dropdown-item" href="home.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Single View</a> </li>
                                                   <li><a class="dropdown-item" href="listview.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;List View</a> </li>
                                                    <li ><a class="dropdown-item" href="MultiGrid.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Grid View</a> </li>
	                            </ul>
	                        </li>
                            <li ><a href="gridauto.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Auto</a> </li>
                            <li><a href="download.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Download</a> </li>
                            <li><a href="uptimebooth.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Report</a> </li>
                                    
                            <%}else if (useridentifier == 4) { %>
                            <li class="nav-item dropdown">
	                         <li ><a href="DashboardTN.aspx"><span class="glyphicon glyphicon-th"></span>&nbsp;Dashboard</a> </li>   
                             <li class="nav-item dropdown">
	                            <a class="nav-link  dropdown-toggle" href="#" data-toggle="dropdown"><span class="glyphicon glyphicon-th-list"></span>&nbsp;View  </a>
	                            <ul class="dropdown-menu fade-up">
		                                            <li><a class="dropdown-item" href="home.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Single View</a> </li>
                                                   <li><a class="dropdown-item" href="listview.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;List View</a> </li>
                                                    <li ><a class="dropdown-item" href="MultiGrid.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Grid View</a> </li>
	                            </ul>
	                        </li>
                            <li ><a href="gridauto.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Auto</a> </li>
                            <li><a href="download.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Download</a> </li>
                            
                            <li><a href="uptimebooth.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Report</a> </li>
                            <%}else if (useridentifier == 5) { %>
                            <li class="nav-item dropdown">
	                            <a class="nav-link  dropdown-toggle" href="#" data-toggle="dropdown"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Upload  </a>
	                            <ul class="dropdown-menu fade-up">
		                                            <li><a class="dropdown-item" href="boothupload.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Booth Upload</a> </li>
                                                   <li><a class="dropdown-item" href="UploadDistrict.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;District Upload</a> </li>
                                                  <%--  <li ><a class="dropdown-item" href="uploadcamera.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;CameraUpload</a> </li>--%>
	                            </ul>
	                        </li>
                            <li style="display:none"><a href="users.aspx"><span class="glyphicon glyphicon-th"></span>&nbsp;Users</a> </li>
                             <li ><a href="DashboardTN.aspx"><span class="glyphicon glyphicon-th"></span>&nbsp;Dashboard</a> </li>
                             <li class="nav-item dropdown">
	                            <a class="nav-link  dropdown-toggle" href="#" data-toggle="dropdown"><span class="glyphicon glyphicon-th-list"></span>&nbsp;View  </a>
	                            <ul class="dropdown-menu fade-up">
		                                            <li><a class="dropdown-item" href="home.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Single View</a> </li>
                                                   <li><a class="dropdown-item" href="listview.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;List View</a> </li>
                                                    <li ><a class="dropdown-item" href="MultiGrid.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Grid View</a> </li>
	                            </ul>
	                        </li>
                            <li ><a href="gridauto.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Auto</a> </li>
                          <%--  <li><a href="download.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Download</a> </li>--%>
                            <li class="nav-item dropdown">
	                            <a class="nav-link  dropdown-toggle" href="#" data-toggle="dropdown"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Reports  </a>
	                            <ul class="dropdown-menu fade-up">
		                                            <li><a class="dropdown-item" href="uptimebooth.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Minute Wise</a> </li>
                                                   <li><a class="dropdown-item" href="uptimeHr.aspx"><span class="glyphicon glyphicon-th-list"></span>&nbsp;Hour Wise</a> </li>
	                            </ul>
	                        </li>
                            <li ><a href="#" data-toggle="modal" data-target="#settingmodal"><span class="glyphicon  glyphicon-cog"></span>&nbsp;Settings</a></li>
                            <%} %>
                            <li><a href="Logout.aspx"><span class="glyphicon glyphicon-off"></span>&nbsp;Logout</a></li>
		   <!-- Modal -->
<div id="settingmodal" class="modal fade" role="dialog">
  <div class="modal-dialog modal-sm">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Choose Player </h4>
                    </div>
			    <div class="modal-body">
        <script>
                         var playerType
                    
                    <%
                    if (Session["PlayerType"] == null || Session["PlayerType"].ToString() == "")
                    { 
                    Session["PlayerType"] = "JWPlayer";
                    %>                
                    <%} %>
                    </script>
                    <div class="col-lg-12 pull-right text_center" style="padding:30px;border: 1px solid #DADEE7;background: #00a0e3;margin-bottom: 10px;color:#FFF" >
                        
                        <input type="radio" id="JWPalyer" name="Playertype" <%if (Session["PlayerType"]!=null && Session["PlayerType"].ToString() == "JWPlayer")
                            { %>  checked="checked" <%} %> value="JWPlayer" /> JW Player
                        <input type="radio" id="RTMPPalyer" name="Playertype" value="RTMPPlayer" <%if (Session["PlayerType"].ToString() == "RTMPPlayer")
                            { %>  checked="checked" <%} %> /> VMukti Player
                        <input type="radio" id="AmbicamPalyer" name="Playertype" value="AmbicamPlayer" <%if (Session["PlayerType"].ToString() == "AmbicamPlayer")
                            { %>  checked="checked" <%} %> /> Ambicam Player

                       
                </div>
                <script>
                   
                     playerType = '<%=Session["PlayerType"]%>'
                   
                    $(document).ready(function () {
                        $('input:radio[name=Playertype]').change(function () {
                            if (this.value == "JWPlayer") {
                              
                                playerType = "JWPlayer"
                                $.ajax("setPlayerType.aspx?Playertype=JWPlayer", {
                                    success: function (data) { }
                                });
                            }
                            else if (this.value == "RTMPPlayer") {
                                playerType = "RTMPPlayer"
                                $.ajax("setPlayerType.aspx?Playertype=RTMPPlayer", {
                                    success: function (data) { }
                                });

                            
                            }
                            else if (this.value == "AmbicamPlayer") {
                                playerType = "AmbicamPlayer"
                                $.ajax("setPlayerType.aspx?Playertype=AmbicamPlayer", {
                                    success: function (data) { }
                                });


                            }
                           
                        });
                    });
                </script>
      </div>

      <div class="modal-footer">
         <button type="button" class="btn btn-default" data-dismiss="modal">Save</button>
      </div>
    </div>
    </div>
  </div>
</div>
                </div>
            
            </LoggedInTemplate>
        </asp:LoginView>
    </nav>
</header>
