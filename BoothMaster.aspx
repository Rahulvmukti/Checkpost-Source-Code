<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="BoothMaster.aspx.cs" Inherits="exam.BoothMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
    <style>
        .content-wrapper > .content {
            padding: 5px;
        }

        .fixheader {
            background-color: #000;
            border-bottom: 0;
            box-shadow: inset 0 1px 0 #dee2e6, inset 0 -1px 0 #dee2e6;
            position: -webkit-sticky;
            position: sticky;
            top: 0;
            z-index: 10;
            color: #fff;
        }

        .p-td {
            padding: 5px;
        }

        .form-group {
            margin-bottom: 0px;
            padding: 2px 5px;
        }

        .card-body.p-0 .table tbody > tr > td:last-of-type, .card-body.p-0 .table tbody > tr > th:last-of-type, .card-body.p-0 .table tfoot > tr > td:last-of-type, .card-body.p-0 .table tfoot > tr > th:last-of-type, .card-body.p-0 .table thead > tr > td:last-of-type, .card-body.p-0 .table thead > tr > th:last-of-type {
            padding-right: 0;
        }

         

        .paging {
        }

            .paging a {
                background-color: #add8e6;
                padding: 5px 7px;
                text-decoration: none;
                border: 1px solid #00C157;
            }

                .paging a:hover {
                    background-color: #add8e6;
                    color: #00C157;
                    border: 1px solid #00C157;
                }

            .paging span {
                background-color: #add8e6;
                padding: 5px 7px;
                color: #00C157;
                border: 1px solid #00C157;
            }

        tr.paging {
            background: none !important;
        }

            tr.paging tr {
                background: none !important;
            }

            tr.paging td {
                border: none;
            }
            .swal-btn-margin {
    margin-right: 10px; /* Adjust the margin as per your preference */
}
          
/* Autocomplete Dropdown Styles for AdminLTE 3 Theme */
/* Autocomplete Dropdown Styles */
.ui-autocomplete {
    max-height: 200px;
    overflow-y: auto;
    z-index: 99999;
    background-color: #ffffff;
    border: 1px solid #ced4da;
    border-radius: 5px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    position: absolute;
    width: auto; 
    top: 100%; /* Position the dropdown above the textbox */
    left: 0;
}
/* For positioning above modal popup */


.ui-menu-item {
    padding: 5px 10px;
    cursor: pointer;
    transition: background-color 0.3s;
    font-family: inherit;
}

.ui-menu-item:hover {
    background-color: #f8f9fa;
}

.ui-menu-item a {
    color: #007bff;
    text-decoration: none;
}

.ui-menu-item a:hover {
    text-decoration: underline;
}

.ui-state-active {
            font-size: 14px;
            font-weight: 100;
            background: #bcbec2 !important;
            border: none !important;
            margin: 0 !important;
            padding: 3px 1em 3px 0.4em !important;
            width: 100%;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-12">
                                <div class="card">
                                    <div class="card-header row">
                                          
                                        <input type="hidden" value="" id="hiddenField" runat="server" />
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="true"
                                                Width="170px" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" />
                                        </div>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlAssembly" runat="server" AutoPostBack="true" CssClass="form-control"
                                                Width="170px" OnSelectedIndexChanged="ddlAssembly_SelectedIndexChanged" />
                                        </div>
                                        <div class="form-group" style="display: none;">
                                            <asp:DropDownList ID="ddlCameraType" runat="server" CssClass="form-control ddlbooth" AutoPostBack="true"
                                                Width="180px" OnSelectedIndexChanged="ddlCameraType_SelectedIndexChanged">
                                                <asp:ListItem Text="Select Camera Type" Value="" />
                                                <asp:ListItem Text="InDoor" Value="Inside" />
                                                <asp:ListItem Text="OutDoor" Value="Outside" />
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
                                        <div class="form-group">
                                            <asp:Button ID="Add_Booth" runat="server" Text="ADD BOOTH" CssClass="btn btn-block btn-primary"
                                                data-backdrop="static" OnClientClick="OpenPopupAdd();" OnClick="btnaddbooth_Click" />
                                        </div>
                                        <div class="form-group">
                                            <button class="btn btn-block btn-primary" onclick="window.open('BoothExcelFomat.xlsx', '_blank')">
                                                Booth Excel Format
                                            </button>
                                        </div>


                                        <div class="form-group">
                                            <asp:FileUpload ID="FileUploadbooth" runat="server" CssClass="border p-thin" Style="width: 200px; padding: 0; font-size: 15px;" />
                                            <asp:Button ID="btnupload" runat="server" Text="Upload" OnClientClick="javascript: return checkValidate();" OnClick="btnupload_Click" />
                                        </div>
                                        <%if (Session["userType"].ToString().ToLower() == "master_admin")
                                            { %>
                                        <div class="form-group" style="display:none">
                                            <asp:Button ID="btnBulkRemove" runat="server" Text="Assembly Remove" CssClass="btn btn-block btn-primary"
                                                OnClientClick="bulkdelete()" />
                                        </div>
                                        <%} %>
                                    </div>
                                    <div class="card-body table-responsive p-0" style="max-height: 500px; overflow: auto">
                                        <div class="panel-body">

                                           

                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-head-fixed text-nowrap"
                                                    OnRowCommand="GridView1_RowCommand" AlternatingRowStyle-CssClass="bg-light" AllowPaging="true"
                                                    OnPageIndexChanging="GridView1_PageIndexChanging"
                                                    PagerStyle-CssClass="paging">
                                                    <EmptyDataTemplate>
                                                        <div align="center">
                                                            <h4>
                                                                <label class="text-center text-danger">
                                                                    No Data Found</label>
                                                            </h4>
                                                        </div>
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="BoothID" HeaderStyle-CssClass="fixheader" Visible="false">
                                                            <ItemTemplate>
                                                                <%--<asp:HiddenField ID="lblid" runat="server" Value='<%# Eval("id")%>' />--%>
                                                                <asp:Label Text='<%# Eval("id")%>' ID="lblid" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BoothStreamID" Visible="false" ItemStyle-Width="5%" HeaderStyle-CssClass="fixheader">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStreamId" runat="server" Text='<%# Eval("streamid")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BoothoperatorID" Visible="false" ItemStyle-Width="5%" HeaderStyle-CssClass="fixheader">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOperatorId" runat="server" Text='<%# Eval("operatorid")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BoothoperatorID" Visible="false" ItemStyle-Width="5%" HeaderStyle-CssClass="fixheader">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblaccode" runat="server" Text='<%# Eval("accode")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sr.No." HeaderStyle-CssClass="fixheader">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="<%$appSettings:district%>" ItemStyle-Width="7%" Visible="true" HeaderStyle-CssClass="fixheader">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldistrict" runat="server" Text='<%# Eval("district")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--  <asp:TemplateField HeaderText="<%$appSettings:pcname%>" ItemStyle-Width="7%" Visible="true" ItemStyle-CssClass="f-size-xxs p-thin  text-left" HeaderStyle-CssClass="f-size-xxs f-color-secondary border-default border-b sr-header p-xs text-left">
                                <ItemTemplate>
                                    <asp:Label ID="lblpc" runat="server" Text='<%# Eval("accode")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="<%$appSettings:assemblyname%>" Visible="True" HeaderStyle-CssClass="fixheader">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblschoolname" runat="server" Text='<%# Eval("acname")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="<%$appSettings:psnum%>" HeaderStyle-CssClass="fixheader">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblps" runat="server" Text='<%# Eval("psnum")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="<%$appSettings:location%>" HeaderStyle-CssClass="fixheader">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbllocation" runat="server" Text='<%# Eval("location")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Operator name" HeaderStyle-CssClass="fixheader">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldisp" runat="server" Text='<%# Eval("operatorname")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Operator no." HeaderStyle-CssClass="fixheader">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldispmob" runat="server" Text='<%# Eval("operatornumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Designation" Visible="false" HeaderStyle-CssClass="fixheader">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldispDesignation" runat="server" Text='<%# Eval("Designation")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Camera No" HeaderStyle-CssClass="fixheader">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstreamnames" runat="server" Text='<%# Eval("streamname")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Camera Location Type" Visible="false" HeaderStyle-CssClass="fixheader">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcamloctype" runat="server" Text='<%# Eval("CameraLocationType")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" Visible="True" HeaderStyle-CssClass="fixheader">
                                                            <ItemTemplate>
                                                                <%#Eval("status").ToString() == "RUNNING" ? "Online" : Eval("status").ToString() == "STOPPED" ? "Offline" : "" %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="fixheader">
                                                            <ItemTemplate>
                                                                <asp:Button runat="server" CssClass="btn btn-block btn-primary"
                                                                    Style="padding: 6px 12px; font-size: 15px" Text="Edit" ID="btnMapping" CommandName="Submit" OnClientClick="OpenPopupAdd()" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="fixheader">
                                                            <%-- <ItemTemplate>
                                    <asp:Button runat="server" CssClass="cursor-pointer bg-primary f-color-white f-weight-semibold hover:shadow-md transition-all px-2xs py-thin"
                                        Style="padding: 6px 2px; font-size: 15px" Text="Delete" ID="btnDelete" CommandName="Delete" OnClientClick="return swal('Are you sure you want to delete this Booth?', {buttons: {cancel: true,confirm: true,},});" />
                                </ItemTemplate>--%>
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdID" Value='<%# Eval("id") %>' runat="server" />
                                                                <asp:Button ID="btnSweetAlert" runat="server"  OnClientClick='<%# "deletebyID(" + Eval("id").ToString() + ")" %>'  Text="Delete" CssClass="btn btn-block btn-danger" Style="padding: 6px 12px; font-size: 15px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                          
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
              <Triggers>
                               
                                <%--<asp:AsyncPostBackTrigger ControlID="btnupload" EventName="Click" />--%>
                                <%--<asp:AsyncPostBackTrigger ControlID="btnupload" EventName="Click"/>--%>
                            <asp:AsyncPostBackTrigger ControlID="btnsearch" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
           <%--         <asp:AsyncPostBackTrigger ControlID="ddlDistrict" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlAssembly" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlCameraType" EventName="SelectedIndexChanged" />--%>
            
                 
                            </Triggers>
                </asp:UpdatePanel>
            </div>

            <div class="modal wow fadeInUp animated" id="PopupAddCamera" style="z-index:1035">
                <div class="modal-dialog">
                    <div class="modal-content bg-secondary" style="overflow-x: auto;height: 500px";>
                     
                        <div class="modal-header" style="padding: 0px 10px">
                    
                            <asp:Label ID="lblmsg" runat="server" ClientIDMode="Static" Style="color: #dc7c3c;">
                                </asp:Label>
                               <asp:Label ID="lblTitle" runat="server" Text="Add Booth" class="modal-title" ClientIDMode="AutoID"></asp:Label>
                             
                             <asp:HiddenField ID="lblboothid11" runat="server" Value="0"  EnableViewState="true"/>
                            <button type="button" onclick="ClosePopupAdd();" class="btnclose close cursor-pointer" id="btncancel" data-dismiss="modal" aria-hidden="true">
                                &times;
                            </button>
                        </div>
                        <div class="modal-body cardarchive" id="videoContainer">
                            <div>
                                <asp:HiddenField ID="lblboothid1" runat="server" Value="0" />
                                <asp:Label ID="lblStreamId" runat="server" Visible="false">
                                </asp:Label>
                                <asp:Label ID="lblaccode" runat="server" Visible="false">
                                </asp:Label>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
    <ContentTemplate>
          

        <asp:HiddenField ID="lblboothid" runat="server" Value="0" />
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label style="padding: 4px; font-size: 15px;" class="required" id="lbldistrict">District</label>
                                        <asp:DropDownList ID="drpAddDistrict" runat="server" CssClass="form-control ddlDistrict" AutoPostBack="true"
                                            OnSelectedIndexChanged="drpAddDistrict_SelectedIndexChanged" Style="font-size: 15px;"
                                            ClientIDMode="Static" ValidationGroup="GrpSave">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="District" runat="server" ControlToValidate="drpAddDistrict"
                                            ErrorMessage="Select District!" ForeColor="Red" Style="font-size: 12px;" ValidationGroup="GrpSave" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">

                                        <label style="padding: 4px; font-size: 15px;" class="required" id="lblAss">Assembly</label>

                                        <asp:DropDownList ID="drpAddAcname" runat="server" AutoPostBack="true" CssClass="ddlPC form-control"
                                            Style="padding: 4px; font-size: 15px; width: 100%" ClientIDMode="Static"
                                            OnSelectedIndexChanged="drpAddAcname_SelectedIndexChanged" ValidationGroup="GrpSave">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpAddAcname"
                                            ErrorMessage="Select Assembly!" ForeColor="Red" Style="font-size: 12px;" ValidationGroup="GrpSave" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label style="padding: 4px; font-size: 15px;" class="required">PS No</label>

                                        <asp:TextBox runat="server" ID="txtAddPSNum" ClientIDMode="Static"
                                            Style="font-size: 15px; border: 1px solid #ccc; padding: 3px" ValidationGroup="GrpSave" Width="100%" CssClass="form-control">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddPSNum"
                                            ErrorMessage="Enter PS No!" ForeColor="Red" Style="font-size: 12px;" ValidationGroup="GrpSave" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddPSNum"   
                                                ErrorMessage="Enter PSNum" ForeColor="Red" Style="font-size: 12px;" ></asp:RequiredFieldValidator> --%>
                                        <asp:Label runat="server" ID="Label5" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label style="padding: 4px; font-size: 15px;" class="required">Check Post Name</label>

                                        <asp:TextBox runat="server" ID="txtAddLocation" ClientIDMode="Static"
                                            Style="font-size: 15px; border: 1px solid #ccc; padding: 3px" ValidationGroup="GrpSave" Width="100%" CssClass="form-control">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddLocation"
                                            ErrorMessage="Enter Check Post Name !" ForeColor="Red" Style="font-size: 12px;" ValidationGroup="GrpSave" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddLocation"   
                                                ErrorMessage="Enter Check Post Name " ForeColor="Red" Style="font-size: 12px;" ></asp:RequiredFieldValidator>--%>
                                        <asp:Label runat="server" ID="Label2" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label style="padding: 4px; font-size: 15px;" class="required">Camera ID</label>


                                        <asp:TextBox runat="server" ID="txtAddCameraId" ClientIDMode="Static" Width="100%"
                                            Style="font-size: 15px; border: 1px solid #ccc; padding: 3px" onkeyup="autocompletecameradid()" ValidationGroup="GrpSave" CssClass="form-control">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAddCameraId"
                                            ErrorMessage="Enter a valid CameraID!" ForeColor="Red" Style="font-size: 12px;" ValidationGroup="GrpSave" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAddCameraId"   
                                                ErrorMessage="Enter CameraId" ForeColor="Red" Style="font-size: 12px;" ></asp:RequiredFieldValidator> --%>
                                        <%--<asp:Label runat="server" ID="Label3" Visible="false"></asp:Label>--%>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label style="padding: 4px; font-size: 15px;" class="required">Type</label>

                                        <asp:DropDownList ID="drpaddLocationType" runat="server" AutoPostBack="true" CssClass="form-control"
                                            Style="padding: 4px; font-size: 15px; width: 100%" ClientIDMode="Static"
                                            OnSelectedIndexChanged="drpaddLocationType_SelectedIndexChanged">
                                            <asp:ListItem Value="Inside" Text="Indoor"></asp:ListItem>
                                            <asp:ListItem Value="Outside" Text="Outdoor"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label style="padding: 4px; font-size: 15px;">Operator Name</label>

                                        <asp:TextBox runat="server" ID="txtAddOperatorName" ClientIDMode="Static"
                                            Style="font-size: 15px; border: 1px solid #ccc; padding: 3px" ValidationGroup="GrpSave" Width="100%" CssClass="form-control">
                                        </asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAddOperatorName"   
                                                ErrorMessage="Enter Operator Name!" ForeColor="Red" Style="font-size: 12px;" ValidationGroup="GrpSave" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                        <asp:Label runat="server" ID="Label4" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label style="padding: 4px; font-size: 15px;">Operator Number</label>

                                        <asp:TextBox runat="server" ID="txtAddMobileNumber" ClientIDMode="Static"
                                            Style="font-size: 15px; border: 1px solid #ccc; padding: 3px" ValidationGroup="GrpSave" Width="100%" CssClass="form-control">
                                        </asp:TextBox>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                            ControlToValidate="txtAddMobileNumber" ErrorMessage="Enter Valid Mobile No" ForeColor="Red" Style="font-size: 12px;"
                                            ValidationExpression="[0-9]{10}" ValidationGroup="GrpSave"></asp:RegularExpressionValidator>
                                        <asp:Label runat="server" ID="Label8" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label style="padding: 4px; font-size: 15px;">Operator Designation</label>

                                    <asp:TextBox runat="server" ID="txtoperatordesignation" ClientIDMode="Static"
                                        Style="font-size: 15px; border: 1px solid #ccc; padding: 3px" ValidationGroup="GrpSave" Width="100%" CssClass="form-control">
                                    </asp:TextBox>
                                    <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtoperatordesignation"   
                                                ErrorMessage="Enter Designation!" ForeColor="Red" Style="font-size: 12px;" ValidationGroup="GrpSave" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                    <asp:Label runat="server" ID="Label6" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group" style="display: none;">
                                <label style="padding: 4px; font-size: 15px;">IsActive</label>
                            </div>
                            <div class="form-group" style="display: none;">
                                <asp:CheckBox ID="chlIsActive" runat="server" Checked="true" />
                            </div>
                          
        
                           
        </ContentTemplate>
                                </asp:UpdatePanel>
                        </div>
                         <div class="modal-footer" style="text-align: center">

                                <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="cursor-pointer bg-primary f-color-white f-size-2xs f-weight-semibold hover:shadow-md transition-all px-2xs py-thin" OnClick="btnAdd_Click" ValidationGroup="GrpSave" />
                                <asp:Button ID="btnCloseAdd" Text="Cancel" runat="server" OnClientClick="ClosePopupAdd()" class="btnclose cursor-pointer bg-gray f-color-default f-size-2xs f-weight-semibold hover:shadow-md transition-all px-2xs py-thin" data-dismiss="modal" aria-hidden="true" />
                            </div>
                    </div>
                </div>
                </div>
        </section>
    </div>
    <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script src="https://code.jquery.com/ui/1.13.1/jquery-ui.js"></script>
    <link media="screen" rel="stylesheet" href="https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.css" />
    <script type="text/javascript" src="https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.js"> </script>
    <script>
        var $j = jQuery.noConflict();

        function OpenconfirmPopup() {
            $j("#dialog").show();
        }
        function OpenPopupAdd() {
          
            $j("#lblTitle").val("");
            $j("#PopupAddCamera").show();
        }
        function ClosePopupAdd() {
            $j("#txtAddCameraId").val('');
            $j("#txtAddPSNum").val('');
            $j("#txtAddLocation").val('');
            $j("#txtAddOperatorName").val('');
            $j("#txtAddMobileNumber").val('');
            $j("#txtoperatordesignation").val('');
            $j("#chlIsActive").val('');
            $j("#lblboothid").val('0');
            //$j("#lblboothid").text('0');
            $j("#drpAddDistrict").val("");
            $j("#drpAddAcname").val("");
            $j("#lblmsg").val("");
            $j("#PopupAddCamera").hide();
            $j("#lblTitle").val("");
            return false;
        }

        function deletebyID(id) {
            swal({
                title: 'Confirm!',
                text: "Are you sure you want to delete this record?",
                type: 'warning',
                showCancelButton: true,
                confirmButtonClass: 'btn btn-success swal-btn-margin',
                cancelButtonClass: 'btn btn-danger swal-btn-margin',
                buttonsStyling: false
            }).then(function (result) {
                if (result) {
                    $.ajax({
                        type: "POST",
                        url: "BoothMaster.aspx/DeleteClick",
                        data: "{id:" + id + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
                            if (r.d == "Deleted") {
                                swal("Success!", "Record deleted successfully.", "success").then(function () {
                                    window.location.href = "BoothMaster.aspx";
                                    //var obj = $("#GridView1").ejGrid("instance");
                                    //obj.model.dataSource.push(data);
                                    //obj.refreshContent();
                                });
                            }
                            else {
                                swal("Error!", "Error occured while deleting record.", "success");
                            }
                        }
                    });
                }
            },
                function (dismiss) {
                    if (dismiss == 'cancel') {
                        swal('Cancelled', 'No record Deleted', 'error');
                    }
                });
            return false;
        }

        function bulkdelete() {
            var district_Name = $j("#<%=ddlDistrict.ClientID %> :selected").text();
            var Ac_name = $j("#<%=ddlAssembly.ClientID %> :selected").text();
            if (Ac_name == "ALL Assembly" || $j("#<%=ddlAssembly.ClientID %>").val() == "") {
                swal("Error!", "Please select Assembly!!", "error");
            }
            else {

                swal({
                    title: 'Confirm!',
                    text: "Are you sure you want to delete all Booth?",
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonClass: 'btn btn-success swal-btn-margin',
                    cancelButtonClass: 'btn btn-danger swal-btn-margin',
                    buttonsStyling: false
                }).then(function (result) {
                    if (result) {
                        $.ajax({
                            type: "POST",
                            url: "BoothMaster.aspx/BulkDelete",
                            data: "{ 'district':'" + district_Name + "','acname':'" + Ac_name + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (r) {
                                if (r.d == "Deleted") {
                                    swal("Success!", "District: " + district_Name + "| Acname:" + Ac_name + "  booth deleted successfully.", "success").then(function () {
                                        window.location.href = "BoothMaster.aspx";
                                    });
                                }
                                else {
                                    swal("Error!", "Error occured while deleting record.", "error");
                                }
                            }
                        });
                    }
                },
                    function (dismiss) {
                        if (dismiss == 'cancel') {
                            swal('Cancelled', 'No record Deleted', 'error');
                        }
                    });
                return false;
            }
        }

        function Confirm(msg, ID, ID1) {
            swal({
                title: 'Confirm!',
                text: "" + msg + "",
                type: 'warning',
                showCancelButton: true,
                confirmButtonClass: 'btn btn-success swal-btn-margin',
                cancelButtonClass: 'btn btn-danger swal-btn-margin',
                buttonsStyling: false
            }).then(function (result) {
                if (result) {
                    $.ajax({
                        type: "POST",
                        url: "BoothMaster.aspx/swapCamera",
                        data: "{id1:" + ID + ",id2:" + ID1 + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
                            if (r.d == "Swap") {
                                swal("Success!", "Swap successfully.", "success").then(function () {
                                    window.location.href = "BoothMaster.aspx";
                                });
                            }
                            else {
                                swal("Error!", "Error occured while deleting record.", "error");
                            }
                        }
                    });
                }
            },
                function (dismiss) {
                    if (dismiss == 'cancel') {
                        swal('Cancelled', 'No change', 'error');
                    }
                });
            return false;
        }

        function autocompletecameradid() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "eledata.asmx/GETStreamName",
                data: '{"did":"' + $j("#txtAddCameraId").val() + '"}',
                dataType: "json",
                success: function (data) {
                    $j("#txtAddCameraId").autocomplete({
                        source: data.d,

                        select: function (event, ui) {
                            $j("#txtAddCameraId").val(ui.item.value);
                        }
                    });
                },
                error: (error) => {
                    console.log(JSON.stringify(error));
                }
            });

        }

        function checkValidate() {
            var district_Name = $j("#<%=ddlDistrict.ClientID %> :selected").text();
            var Ac_name = $j("#<%=ddlAssembly.ClientID %> :selected").text();
            if (Ac_name == "ALL Assembly" || $j("#<%=ddlAssembly.ClientID %>").val() == "") {
                swal("Error!", "Please select Assembly!!", "error");
                return false;
            }
            $("#loaderdiv .loader").css({ "left": ($(window).width() / 2) });
            $("#loaderdiv .loader").css({ "top": ($(window).height() / 2) - 70 });
            $("#loaderdiv").show();

        };
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btncancel').on('click', function () {
                $('#lblTitle').text(''); // Clear the text of the label
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btncancel').on('click', function () {
                $('#btnCloseAdd').text(''); // Clear the text of the label
            });
        });
    </script>
    
</asp:Content>
