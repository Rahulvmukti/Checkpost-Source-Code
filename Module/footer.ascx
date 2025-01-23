<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="footer.ascx.cs" Inherits="exam.Module.footer" %>
<footer>
    <nav class="navbar navbar-default navbar-fixed-bottom" role="navigation">
        <div class="container">
            <div class="row">
                <div class="footer-content">
                    <%
                        exam.Common cm = new exam.Common();
                        if (!cm.isMobile())
                        {
                    %>
                    <div align="center">
                        <div class="col-md-4">
                            <a href="#">
                             </a>
                        </div>
                        <div class="col-md-4">
                          
                        </div>
                        <div class="col-md-4">
                            <a href="#">
                              
                            </a>
                        </div>
                    </div>
                    <%
                        }
                        else
                        {
                    %>
                    <div align="center">
                        <div class="pull-right">
                            <ul class="nav nav-pills payments">
                                <li>
                                    <%--<img src="<%=ResolveUrl("~/images/sec.png") %>" class="img-responsive" alt="State Election Commission" />--%></li>
                               
                                <li>
                                    </li>
                            </ul>
                        </div>
                    </div>
                    <%} %>
                    <br />
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </nav>
</footer>
