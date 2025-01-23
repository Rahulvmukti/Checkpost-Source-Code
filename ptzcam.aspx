<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true"
    CodeBehind="ptzcam.aspx.cs" Inherits="exam.ptzcam" %>

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
        .ptzbtn{
            background:#64BDD3;
            border-radius:7px
        }
        .ptzbtn:hover{
            background:#267588;
        }
        .ptzbtn:active {
         background:#39B54A
        }
        .right {
            font-size: 8px;
            text-align: center;
            cursor: pointer;
            outline: none;
            color: #fff;
            background-color: #00A0E3;
            border: none;
            border-radius: 15px;
            display: inline-block;
            width: 4em;
            height: 4em;
            margin-left: 1.5em;

        }

            .right:hover {
                background-color: #B3B3B3;
            }

            .right:active {
                background-color: #B3B3B3;
                transform: translateY(4px);
            }

            .right:after {
                content: '';
                display: inline-block;
                margin-top: 1.10em;
                margin-left: -0.6em;
                width: 1.4em;
                height: 1.4em;
                border-top: 0.5em solid #fff;
                border-right: 0.5em solid #fff;
                -moz-transform: rotate(45deg);
                -webkit-transform: rotate(45deg);
                transform: rotate(45deg);
            }

        .left {
            font-size: 8px;
            text-align: center;
            cursor: pointer;
            outline: none;
            color: #fff;
            background-color: #00A0E3;
            border: none;
            border-radius: 15px;
            display: inline-block;
            width: 4em;
            height: 4em;
            margin-left: 1.5em;
        }

            .left:hover {
                background-color: #B3B3B3;
            }

            .left:active {
                background-color: #B3B3B3;
                transform: translateY(4px);
            }

            .left:after {
                content: '';
                display: inline-block;
                margin-top: 1.10em;
                margin-left: 0.6em;
                width: 1.4em;
                height: 1.4em;
                border-top: 0.5em solid #fff;
                border-right: 0.5em solid #fff;
                -moz-transform: rotate(-135deg);
                -webkit-transform: rotate(-135deg);
                transform: rotate(-135deg);
            }


        .up {
            font-size: 8px;
            text-align: center;
            cursor: pointer;
            outline: none;
            color: #fff;
            background-color: #00A0E3;
            border: none;
            border-radius: 15px;
            display: inline-block;
            width: 4em;
            height: 4em;
            margin-left: 1.5em;
        }

            .up:hover {
                background-color: #B3B3B3;
            }

            .up:active {
                background-color: #B3B3B3;
                transform: translateY(4px);
            }

            .up:after {
                content: '';
               /* display: inline-block;*/
                margin-top: 1.4em;
                width: 1.4em;
                height: 1.4em;
                border-top: 0.5em solid #fff;
                border-right: 0.5em solid #fff;
                -moz-transform: rotate(-45deg);
                -webkit-transform: rotate(-45deg);
                transform: rotate(-45deg);
            }

        .bottom {
            font-size: 8px;
            text-align: center;
            cursor: pointer;
            outline: none;
            color: #fff;
            background-color: #00A0E3;
            border: none;
            border-radius: 15px;
            display: inline-block;
            width: 4em;
            height: 4em;
            margin-left: 1.5em;
        }

            .bottom:hover {
                background-color: #B3B3B3;
            }

            .bottom:active {
                background-color: #B3B3B3;
                transform: translateY(4px);
            }

            .bottom:after {
                content: '';
                display: inline-block;
                margin-top: 1.0em;
                width: 1.4em;
                height: 1.4em;
                border-top: 0.5em solid #fff;
                border-right: 0.5em solid #fff;
                -moz-transform: rotate(135deg);
                -webkit-transform: rotate(135deg);
                transform: rotate(135deg);
            }

        .zoomcss {
            font-size: 20px;
            background-color: #00A0E3;
            color: White;
            border-radius: 2px;
            padding: 9px 26px 12px 26px;
        }

        .card {
            /*box-shadow: 0 4px 8px 0 rgba(0,0,0,1.0);*/
            transition: 0.3s;
            width: 95%;
            border-radius: 0px;
            border: 0.1rem solid #000;
        }

            .card:hover {
                background-color: cornsilk;
                box-shadow: 0 8px 16px 0 rgba(0,0,0,1.0);
            }

        .heading {
            font-size: 1.5rem;
            font-weight: 900;
            color: #0d6efd;
            letter-spacing: .025em;
            margin-bottom: 0.6rem;
            margin-top: 0.6rem;
            line-height: 1.2;
        }

        .sub-heading {
            font-size: 1.45rem;
            color: #6c757d;
            line-height: 1.2;
            margin-bottom: 0.3rem;
        }

        .data {
            font-size: 1.25rem;
            line-height: 1.2;
            margin-bottom: 0.5rem;
        }

        .container {
            padding: 2px 16px;
        }
        .ptzcanvas {
    width: 100%;
    height: auto; /* This will maintain the aspect ratio of the canvas */
}
#ContentPlaceHolder1_arrup {
    float: left;
    margin-right: 10px; /* Adjust the margin as needed */
}



    </style>


    <link href="css/ptzctrl.css" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveUrl("~/js/nodeplayer/NodePlayer.min.js") %>"></script>
    <%
        exam.Common cm1 = new exam.Common();
        if (cm1.isMobile())
        { %>
    <%--<script src="<%=ResolveUrl("~/js/jwp6/jwplayer.js") %>" type="text/javascript"></script>--%>
    <%--<script src="<%=ResolveUrl("~/js/jwplayer.8.24.0.js") %>" type="text/javascript"></script>--%>
   <%-- <script type="text/javascript"> 
        jwplayer.key = 'UeOwyRhgpc5Og22j+4YM7uzrcbTwxTV9r+XadsoRGr8=';
    </script>--%>
    <%}
        else
        { %>
    <%--<script src="<%=ResolveUrl("~/js/jwp7/jwplayer.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        jwplayer.key = '+Amzd6SXh2+L9OeORFc8SMH9bZZtyha7IZ/DPg==';
    </script>--%>
    <script src="https://cdn.jwplayer.com/libraries/9pu77ByB.js"></script>
    <%-- <script src="<%=ResolveUrl("~/js/jwp5/jwplayer.js") %>" type="text/javascript"></script>--%>
    <%}%>
   <%-- <script src="<%=ResolveUrl("~/js/screenfull.js") %>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/js/jwp8/jwplayer.js") %>" type="text/javascript"></script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<div class="bg-white">
        &nbsp;Center List - Single Booth view</div>--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
  
                
                     <div class="content-wrapper">
        <section class="content">
      <div class="container-fluid">
        
        <div class="row" >
          <div class="col-12" >
            <div class="card" style="background-color:#454d55;width:100%;">
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                <div class="card-header row"> 
                    
                                         <div class="form-group">
                                                <asp:DropDownList ID="ddlDistrict" runat="server"  CssClass="form-control"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                      
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlPC" runat="server" AutoPostBack="true" CssClass="form-control"
                                                    OnSelectedIndexChanged="ddlbooth_SelectedIndexChanged" >
                                                </asp:DropDownList>
                                            </div>
                                           <div class="form-group">
                                            <asp:DropDownList ID="VehicleDropDownList"
                                                runat="server" 
                                                AutoPostBack="true"
                                                CssClass="form-control"
                                                OnSelectedIndexChanged="VehicleDropDownList_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                     <div class="form-group">
                   <asp:TextBox ID="strm_txtBox" runat="server"  CssClass="form-control" placeholder="Camera ID"
                                                  Width="200px">
                                                </asp:TextBox>
                         </div>
                                     <div class="form-group">
                  <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="btn btn-block btn-primary"
                                                OnClick="btnsearch_Click"/>
                          </div>
                          <%--      <%
                                    exam.Common cm = new exam.Common();
                                    if (!cm.isMobile())
                                    {
                                %>
                                <div class="f-right d-hidden">
                                    <label>
                                        Not able to View?
                                    </label>
                                    <a href="<%=ResolveUrl("~/Help/index.html") %>" target="_blank">Click Here for System
                            Test</a>
                                </div>
                                <%} %>--%>
                            </div>
                  
              </ContentTemplate> 
            </asp:UpdatePanel>
      
 
          <div class="row">
              <div class="col-md-12">
            <div class="card card-primary">
              <div class="card-body row">
               
                <div class="col-9 lg:col-9"><%--embed-responsive  embed-responsive-16by9--%>
                    <div class="col-12-pb-xs">
                            <div id="divptz" class="card">
                                <canvas id="<%= ptzstreamname %>" class="ptzcanvas w-full"><%--<%= ptzstreamname %>--%>
                                </canvas>

                            </div> 
                        </div>

                 <div class="px-thin" style="position:relative; font-size:12px">
                            <div class="px-thin" style="text-align:center">
                                <div id="dist_ass"></div>
                                <div class="flex-row flex-wrap">

                                        <p class="word-break-all"><b>Vehical No.:-</b>
                                        <span id="Vehicle_No">/</span>  
                                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                        <b>Driver Name/No:-</b>
                                               <span id="Driver">/</span> 
                                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                       <b>Camera Id:-</b>  
                                     <span id="strnm"></span></p>
                                </div>
                            </div> 
                     </div>
                        <div id="playercontrol">
                            <div align="left">
 
                            </div>
                        </div>
                    </div>

                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                  <div class="col-md-12"> 
                  <div style="display: table;">
    <div style="display: table-row;">
        <div style="display: table-cell;"></div> <!-- Empty cell -->
        <div style="display: table-cell;">
            <asp:LinkButton ID="arrup" runat="server" ToolTip="Move Up" OnClick="btnup_Click">
                <img src="images/up.png" name="Image20" border="0" height="40" width="40" class="ptzbtn"
                    id="up" alt="up">
            </asp:LinkButton>
            <asp:HiddenField ID="hf_up" runat="server" />
        </div>
        <div style="display: table-cell;"></div> <!-- Empty cell -->
    </div>

    <div style="display: table-row;">
        <div style="display: table-cell;">
            <asp:LinkButton ID="arrleft" runat="server" ToolTip="Move Left" OnClick="btnleft_Click">
                <img src="images/left.png" name="Image22" border="0" height="40" width="40" class="ptzbtn"
                    id="left" alt="left">
            </asp:LinkButton>
            <asp:HiddenField ID="hf_left" runat="server" />
        </div>
        <div style="display: table-cell;"></div> <!-- Empty cell -->
        <div style="display: table-cell;">
            <asp:LinkButton ID="arrright" runat="server" ToolTip="Move Right" OnClick="btnright_Click">
                <img src="images/right.png" name="Image24" border="0" height="40" width="40" class="ptzbtn"
                    id="right" alt="right">
            </asp:LinkButton>
            <asp:HiddenField ID="hf_right" runat="server" />
        </div>
    </div>

    <div style="display: table-row;">
        <div style="display: table-cell;"></div> <!-- Empty cell -->
        <div style="display: table-cell;"></div> <!-- Empty cell -->
        <div style="display: table-cell;"></div> <!-- Empty cell -->
    </div>

    <div style="display: table-row;">
        <div style="display: table-cell;"></div> <!-- Empty cell -->
        <div style="display: table-cell;">
            <asp:LinkButton ID="arrdown" runat="server" ToolTip="Move Down" OnClick="btndown_Click">
                <img src="images/down.png" name="Image26" border="0" height="40" width="40" class="ptzbtn"
                    id="down" alt="down">
            </asp:LinkButton>
            <asp:HiddenField ID="hf_down" runat="server" />
        </div>
        <div style="display: table-cell;"></div> <!-- Empty cell -->
    </div>
                       <div style="display: table-row;">
                             <div style="display: table-cell;"></div>
                       </div>
                      <div style="display: table-row;">
       
        <div style="display: table-cell;">
             <asp:LinkButton ID="arrzoomin" runat="server" ToolTip="Zoom In" OnClick="btnzoomin_Click">
                                    <div class="zin">
                                         
                                        <img id="img_zin" width="50" height="50" alt="zoomin" src="images/Zoomin.png" class="ptzbtn"/></div>
                                </asp:LinkButton>
                                <asp:HiddenField ID="hf_near" runat="server" />
        </div>
                           <div style="display: table-cell;"></div> <!-- Empty cell -->
        <div style="display: table-cell;">
             <asp:LinkButton ID="arrzoomout" runat="server" ToolTip="Zoom Out" OnClick="btnzoomout_Click"> 
                                    <img id="img_zout" alt="zoomout" class="zout ptzbtn" width="50" height="50" src="images/ZoomOut.png" />
                                </asp:LinkButton>
                                <asp:HiddenField ID="hf_far" runat="server" />
        </div> <!-- Empty cell -->
    </div>
</div>


                     </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="arrup" />
                    <asp:AsyncPostBackTrigger ControlID="arrdown" />
                    <asp:AsyncPostBackTrigger ControlID="arrleft" />
                    <asp:AsyncPostBackTrigger ControlID="arrright" />
                    <asp:AsyncPostBackTrigger ControlID="arrzoomin" />
                    <asp:AsyncPostBackTrigger ControlID="arrzoomout" />
                </Triggers>
            </asp:UpdatePanel>

                </div> 
            </div> 
                  </div>  
                </div>
                 
               
                  </div>        
        </div>
                   </div>
              </div>
   </section>
           </div>

     <%-- </ContentTemplate>
                      </asp:UpdatePanel>--%>
    
       <script type="text/javascript">
           debugger;

         

           function hoverin(element) {
               element.setAttribute('src', 'img/ptzimages/zoomin_hover.png');
           }
           function unhoverin(element) {
               element.setAttribute('src', 'img/ptzimages/zoomin.png');
           }

           function hoverout(element) {
               element.setAttribute('src', 'img/ptzimages/zoomout_hover.png');
           }
           function unhoverout(element) {
               element.setAttribute('src', 'img/ptzimages/zoomout.png');
           }
           Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function () {
               var timer = 0, timerInterval, timeStartup, timeEndup, timeStartdown, timeEnddown, timeStartleft, timeEndleft, timeStartright, timeEndright, timeStartnear, timeEndnear, timeStartfar, timeEndfar;
               $('#<%=arrup.ClientID %>').mouseup(function () {
                timeEndup = new Date();
                $('#<%=hf_up.ClientID%>').val(Math.floor(((timeEndup - timeStartup) / 1000) * 100));
            });
            $('#<%=arrup.ClientID %>').mousedown(function () {

                timeStartup = new Date();
            });

            $('#<%=arrdown.ClientID %>').mouseup(function () {
                timeEnddown = new Date();

                $('#<%=hf_down.ClientID%>').val(Math.floor(((timeEnddown - timeStartdown) / 1000) * 100));
            });
            $('#<%=arrdown.ClientID %>').mousedown(function () {
                timeStartdown = new Date();
            });

            $('#<%=arrleft.ClientID %>').mouseup(function () {
                timeEndleft = new Date();

                $('#<%=hf_left.ClientID%>').val(Math.floor(((timeEndleft - timeStartleft) / 1000) * 100));
            });
            $('#<%=arrleft.ClientID %>').mousedown(function () {
                timeStartleft = new Date();
            });

            $('#<%=arrright.ClientID %>').mouseup(function () {
                timeEndright = new Date();

                $('#<%=hf_right.ClientID%>').val(Math.floor(((timeEndright - timeStartright) / 1000) * 100));
            });
            $('#<%=arrright.ClientID %>').mousedown(function () {
                timeStartright = new Date();
            });

            $('#<%=arrzoomin.ClientID %>').mouseup(function () {
                timeEndnear = new Date();

                $('#<%=hf_near.ClientID%>').val(Math.floor(((timeEndnear - timeStartnear) / 1000) * 100));
            });
            $('#<%=arrzoomin.ClientID %>').mousedown(function () {
                timeStartnear = new Date();
            });

            $('#<%=arrzoomout.ClientID %>').mouseup(function () {
                timeEndfar = new Date();

                $('#<%=hf_far.ClientID%>').val(Math.floor(((timeEndfar - timeStartfar) / 1000) * 100));
            });
            $('#<%=arrzoomout.ClientID %>').mousedown(function () {
                timeStartfar = new Date();
            });
        });

        function LoadNodePlayer(ptzstreamname, servername, DriverName, DriverContactNo, VehicleType, Vehicle_No, dist, assembly) {
            //LoadPlayer('<%=ptzstreamname %>', '<%=servername %>', 'ptz', '', '', '');
            console.log('===================>>>>>>>>>' + ptzstreamname)

            $(".ptzcanvas").attr("id", ptzstreamname);
            <%# Eval("servername") %>


               var Driver = document.getElementById("dist_ass");
               Driver.innerHTML = dist + ' / ' + assembly;

               var Driver = document.getElementById("Driver");
               Driver.innerHTML = DriverName + ' / ' + DriverContactNo;

               var vhno = document.getElementById("Vehicle_No");
               vhno.innerHTML = Vehicle_No;


               var stmnm = document.getElementById("strnm");
               stmnm.innerHTML = ptzstreamname;


               var divptz = document.getElementById('divptz');
               divptz.innerHTML = '';


               var canv = document.createElement('canvas');
               canv.style.width = "100%"
               canv.id = ptzstreamname;
               divptz.appendChild(canv);

               var player;
               NodePlayer.load(() => {
                   player = new NodePlayer();
               });

               var interval = null;
               player.setView(ptzstreamname);
               //player.setView('PNJB-000003-PBPTZ');

               var customURL = "wss://" + servername + ":443/live-record/" + ptzstreamname + ".flv";
               //var customURL = "wss://eletestnew.vmukti.com:443/live-record/PNJB-000003-PBPTZ.flv";

               player.start(customURL);

               player.on("error", (e) => {
                   //interval = setInterval(function () { player.start(customURL); }, 1000)
                   console.log("Player Error");
               });

               player.on("start", () => {
                   if (interval) { clearInterval(); console.log("Interval clear"); }
                   console.log("Player Start");
               });


               player.on("stop", () => {
                   console.log("Player Stopped");
                   interval = setInterval(function () { player.start(customURL); }, 1000)
                   console.log("Interval Set");
               });
               console.log("Stop Defined");

               player.on("stats", (stats) => {
                   //console.log("player on stats=", stats);
               })


           }
       </script>
     <script type="text/javascript"> 
         $('#viewmenu').addClass('active');
         $('#PTZviewmenu').addClass('active');
     </script>
    <style>
        .nav-sidebar .nav-link>.right, .nav-sidebar .nav-link>p>.right{
            display:none!important;
        } 
    </style>
</asp:Content>
