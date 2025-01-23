<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="headerAdmin.ascx.cs"
    Inherits="exam.Module.headerAdmin" %>
<header>
    <nav class="navbar navbar-default navbar-inverse" role="navigation">
        <asp:LoginView ID="LoginView2" runat="server">
            <AnonymousTemplate>
                <div class="container">
                    <nav class="navbar navbar-default navbar-inverse navbar-fixed-top" role="navigation">
                        <div class="container">
                            <h3 id="title" class="white text-center" runat="server">
                                <%=ConfigurationManager.AppSettings["header"]%></h3>
                        </div>
                    </nav>
                </div>
            </AnonymousTemplate>
            <LoggedInTemplate>
                <div class="container">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#navbarCollapse">
                            <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span><span
                                class="icon-bar"></span><span class="icon-bar"></span>
                        </button>
                       
                    </div>
                    <div class="collapse navbar-collapse" id="navbarCollapse">
                        <ul class="nav navbar-nav navbar-right">
                            <li><a href="javascript:void(0);"><span class="glyphicon glyphicon-user"></span>&nbsp;Welcome
                                :
                                <%=Page.User.Identity.Name %></a> </li>
                          
                           <li><a href="boothupload.aspx"><span class="glyphicon glyphicon-th"></span>&nbsp;Booth Upload</a> </li>
                           <li><a href="operator.aspx"><span class="glyphicon glyphicon-th"></span>&nbsp;Operator Upload</a> </li>
                           <%if (useridentifier == 2)
                             { %>
                           
                           <%}
                             else if (useridentifier == 3)
                             { %>
                              <li><a href="upload.aspx"><span class="glyphicon glyphicon-th"></span>&nbsp;Master</a> </li>
                             <li><a href="streamcount.aspx"><span class="glyphicon glyphicon-th"></span>&nbsp;Live Count</a> </li>                           
                             <li><a href="manual.aspx"><span class="glyphicon glyphicon-th"></span>&nbsp;GO GREEN</a> </li>
                             <li><a href="users.aspx"><span class="glyphicon glyphicon-th"></span>&nbsp;ADD User</a> </li>
                             
                              <li><a href="managers.aspx"><span class="glyphicon glyphicon-th"></span>&nbsp;Manager</a> </li>
                           <%} %>
                            <li><a href="Logout.aspx"><span class="glyphicon glyphicon-off"></span>&nbsp;Logout</a></li>
                        </ul>
                    </div>
                </div>
            </LoggedInTemplate>
        </asp:LoginView>
    </nav>
</header>
