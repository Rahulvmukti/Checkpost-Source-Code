<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="camerasetting.aspx.cs"
    Inherits="exam.camerasetting" Async="true" ViewStateEncryptionMode="Always" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
   <%-- <link href="assets/font-awesome/css/font-awesome.css" rel="stylesheet" />--%>
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700&display=fallback">
    <link href="plugins/bootstrap-slider/css/bootstrap-slider.min.css" rel="stylesheet" />
    <link href="dist/css/adminlte.min.css" rel="stylesheet" />
    <script type="text/javascript" src="<%=ResolveUrl("~/js/jquery.js") %>"></script>
    <script src="<%=ResolveUrl("~/js/bootstrap.min.js") %>" type="text/javascript"></script>
    <script src="<%=ResolveUrl("~/js/common.js") %>" type="text/javascript"></script>




    <script type="text/javascript">
        window.onload = function () {
            if (parent) {
                var oHead = document.getElementsByTagName("head")[0];
                var arrStyleSheets = parent.document.getElementsByTagName("style");
                for (var i = 0; i < arrStyleSheets.length; i++)
                    oHead.appendChild(arrStyleSheets[i].cloneNode(true));
            }
        }
              
    </script>
   
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptmanager_1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="updatepanelsetting" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <%-- <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick" Enabled="false">
            </asp:Timer>--%>
            <div class="modal-content">
                <%--<div class="modal-header" style="justify-content:center">
                    <h4 class="text-left modal-title" id="myTab">
                        <i class="fa fa-cog"></i>&nbsp;Settings
                    </h4>
                </div>--%>
                <div class="modal-body " id="setting" style="padding:0px 2rem"> 
                  <div class="col-md-12 row">  
                      <div class="card card-primary card-outline" style="padding:0px;margin: 5px 2px;">
                       <div class="card-header" style="padding:5px">
                <h3 class="card-title">
                  <i class="fas fa-bullhorn"></i>
                   Settings
                </h3>
              </div>
                  </div>
                  </div>
                    <div class="col-md-12 row"> 
                        <div class="col-md-6">
             <div class="card card-default" style="margin:0px;padding: 0px;">
               
              <!-- /.card-header -->
              <div class="card-body">
              
                <div class="callout callout-danger">
    <div class="row">
        <div class="col-sm-6">
            <h5 style="margin-top: 8px;">Quality</h5>
        </div>
        <div class="col-sm-6">
            <label class="pull-right drpmtop">
                <select class="form-control" id="quality_level" runat="server">
                    <option value="Normal">Normal</option>
                    <option value="FHD">FHD</option>
                    <option value="HD">HD</option>
                    <option value="3G">3G</option>
                    <option value="2G">2G</option>
                </select>
            </label>
        </div>
    </div>
</div>
                <div class="callout callout-warning">
                    <div class="row">
        <div class="col-sm-6">
            <h5 style="margin-top: 8px;">WDR</h5>
        </div>
        <div class="col-sm-6">
            <label class="pull-right drpmtop">
                            <select class="form-control border-radius" id="wdr" runat="server">
                                <option id="wdroff" value="Off">Off</option>
                                <option id="wdrlow" value="Low">Low</option>
                                <option id="wdrmid" value="Mid">Mid</option>
                                <option id="wdrhigh" value="High">High</option>
                                <option id="wdrhigher" value="Higher">Higher</option>
                            </select>
                        </label>
        </div>
                </div>
                </div>
                <div class="callout callout-success">
                    <div class="row">
        <div class="col-sm-6">
            <h5 style="margin-top: 8px;">IRCut</h5>
        </div>
        <div class="col-sm-6">
              <label class="pull-right drpmtop">
                            <select class="form-control" id="ircut" runat="server">
                                <option id="ircutdaynight" value="Day&Night">Day&Night</option>
                                <option id="ircutcolor" value="Color">Color</option>
                                <option id="ircutblackwhite" value="Black&White">Black&White</option>
                            </select>
                        </label>
        </div>
                </div>
                </div>
                  <div class="callout callout-success">
                    <div class="row">
        <div class="col-sm-6">
            <h5 style="margin-top: 8px;">Flip</h5>
        </div>
        <div class="col-sm-6">
               <label class="pull-right">
                            <div class="onoffswitch">
                                <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="flip_on_off"
                                    runat="server" />
                                <label class="onoffswitch-label" for="<%= flip_on_off.ClientID %>"  style="margin-bottom:0px"> 
                                    <span class="onoffswitch-inner"></span><span class="onoffswitch-switch"></span>
                                </label>
                            </div>
                        </label>
        </div>
                </div>
                </div>
                  <div class="callout callout-warning">
                    <div class="row">
        <div class="col-sm-6">
            <h5 style="margin-top: 8px;">Mirror</h5>
        </div>
        <div class="col-sm-6">
             <label class="pull-right" >
                            <div class="onoffswitch">
                                <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="mirror_on_off"
                                    runat="server" />
                                <label class="onoffswitch-label" for="<%= mirror_on_off.ClientID %>">
                                    <span class="onoffswitch-inner"></span><span class="onoffswitch-switch"></span>
                                </label>
                            </div>
                        </label> 
        </div>
                </div>
                </div>
              </div>
              <!-- /.card-body -->
            </div>
            <!-- /.card -->
          </div>
                     <div class="col-md-6">
            <div class="card card-default" style="margin:0px;padding: 0px;">
             
              <!-- /.card-header -->
              <div class="card-body">
                
                
                  <div class="callout callout-info">
                   <div class="row">
        <div class="col-sm-6">
            <h5 style="margin-top: 8px;">Saturation</h5>
        </div>
        <div class="col-sm-6">
            <div class="pull-right"> 
                            <asp:HiddenField ID="hf_imgsaturation" runat="server" />
                            <label class="badge badge-primary" id="imgsaturation">
                                <%=hf_imgsaturation.Value%></label>
                            <input id="Slider4" class="pull-right" type="range" min="0" max="100" value="<%=hf_imgsaturation.Value %>" />
                        </div>  
        </div>
    </div>
                </div>
                <div class="callout callout-warning">
                    <div class="row">
        <div class="col-sm-6">
            <h5 style="margin-top: 8px;">Hue</h5>
        </div>
        <div class="col-sm-6">
             <div class="pull-right">  
                            <asp:HiddenField ID="hf_imghue" runat="server" />
                            <label class="badge badge-primary" id="imghue">
                                <%=hf_imghue.Value%></label>
                            <input id="Slider1" class="pull-right" type="range" min="0" max="100" value="<%=hf_imghue.Value %>" />
                        </div> 
        </div>
                </div>
                </div>
                <div class="callout callout-success">
                    <div class="row">
        <div class="col-sm-6">
            <h5 style="margin-top: 8px;">Brightness</h5>
        </div>
        <div class="col-sm-6">
           <div class="pull-right"> 
                            <asp:HiddenField ID="hf_imgbright" runat="server" />
                            <label class="badge badge-primary" id="imgbright">
                                <%=hf_imgbright.Value%></label>
                            <input id="Slider2" class="pull-right" type="range" min="0" max="100" value="<%=hf_imgbright.Value %>" />
                        </div>
        </div>
                </div>
                </div>
                    <div class="callout callout-danger">
    <div class="row">
        <div class="col-sm-6">
            <h5 style="margin-top: 8px;">Contrast</h5>
        </div>
        <div class="col-sm-6">
             <div class="pull-right"> 
                            <asp:HiddenField ID="hf_imgcontrast" runat="server" />
                            <label class="badge badge-primary" id="imgcontrast">
                                <%=hf_imgcontrast.Value%></label>
                            <input id="Slider3" class="pull-right" type="range" min="0" max="100" value="<%=hf_imgcontrast.Value %>" />
                        </div>
        </div>
    </div>
</div>
                  <div class="callout callout-info">
                   <div class="row">
        <div class="col-sm-6">
            <h5 style="margin-top: 8px;">Audio</h5>
        </div>
        <div class="col-sm-6">
           <label id="lblaudio" runat="server"></label>   
        </div>
    </div>
                </div>
              </div>
              <!-- /.card-body -->
            </div>
            <!-- /.card -->
          </div>



                    

                    </div>


                    <div class="col-md-12 row">  
                        <div class="card card-default" style="margin-top:0px;padding: 0px;">
                       <div class="card-header" style="text-align:right;padding: 5px 15px;">
                 <asp:Button ID="btnsaveconfig" runat="server" Text="Save" CssClass="button button-primary button-rounded button-small"
                        OnClick="btnsaveconfig_Click"  OnClientClick="closeWindow();" />
                    <asp:Button ID="btnclose" runat="server" Text="Close" CssClass="button button-rounded button-small"
                        OnClick="btnclose_Click" style="display:none"/>
              </div>
                  </div>
                  </div>
          <!-- /.col -->
        </div>
        <!-- /.row -->
        <!-- END ALERTS AND CALLOUTS -->
 
                </div>
                <div class="modal-footer"> 
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnsaveconfig" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
         <script> 
             function closeWindow() {
                 window.close();
             }
         </script>
    <script type="text/javascript">

        $("#btnclose").click(function (e) {

            window.parent.closeModal();
        });

        function pageLoad() {
        }
        function toggleChevron(e) {
            $(e.target)
            .prev('')
            .find("i")
            .toggleClass('fa fa-caret-down fa fa-caret-right');
        }

        $(function () {

            $('#Slider1').change(function () {
                $('#<%=hf_imghue.ClientID%>').val(this.value);
                $('#imghue').html(this.value);
            }); $('#Slider1').change();

            $('#Slider2').change(function () {
                $('#<%=hf_imgbright.ClientID%>').val(this.value);
                $('#imgbright').html(this.value);
            }); $('#Slider2').change();

            $('#Slider3').change(function () {
                $('#<%=hf_imgcontrast.ClientID%>').val(this.value);
                $('#imgcontrast').html(this.value);
            }); $('#Slider3').change();

            $('#Slider4').change(function () {
                $('#<%=hf_imgsaturation.ClientID%>').val(this.value);
                $('#imgsaturation').html(this.value);
            }); $('#Slider4').change();
        });

        function CloseWindow() {
            window.close();
        }
    </script>
        
    </form>
   
</body>
</html>
