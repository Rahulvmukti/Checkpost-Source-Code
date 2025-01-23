<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="assembly.aspx.cs" Inherits="exam.assembly"
    ViewStateEncryptionMode="Always" %>
 <style> 
     .card-outline {
    border-top: 3px solid #007bff;
    border-bottom:3px solid #007bff;
} 
 </style> 

                <table id="tblboothlist" class="table table-head-fixed text-nowrap card-outline">
    <thead class="f-size-sm f-color-secondary border-default border-b text-left">
        <tr >
            <th class="p-xs" colspan="7"><asp:Literal runat="server" Text="Assembly List" /></th>
        </tr>
    </thead>
    <%if (BoothList != null)
        {
            if (BoothList.Tables.Count > 0)
            {
                if (BoothList.Tables[0].Rows.Count > 0)
                {
                for (int i = 0; i < BoothList.Tables[0].Rows.Count; i++)
                {
    %>
    <tbody>
        <tr class='bg-white'>
            <td class="py-xs px-sm" style="width: 7%;">&nbsp;</td>
            <td class="py-xs px-sm" style="width: 18%;">
                <div class="flex-stretch f-color-black f-size-sm md:f-size-sm flex-col justify-center text-left capitalize">
                    <h2 class="f-size-2xs md:f-size-2xs"><a class='btn btn-primary btnwidth2' style="display:block" data-toggle="modal" data-target="#myModal" title='<%=BoothList.Tables[0].Rows[i]["DATA"].ToString() %>'>
                        <%=BoothList.Tables[0].Rows[i]["DATA"].ToString().Length > 20 ? BoothList.Tables[0].Rows[i]["DATA"].ToString().PadRight(140).Substring(0, 20).TrimEnd() + ".." : BoothList.Tables[0].Rows[i]["DATA"].ToString()%>
                    </a></h2>
                </div>
            </td>
            <td class="py-xs px-sm" style="width: 15%;">
                <div class="flex-stretch bg-warning f-color-white f-size-sm md:f-size-sm flex-col justify-center text-center shadow-md-primary capitalize">
                    <h2 class="f-size-sm md:f-size-sm"><a class='btn whiteText btnwidth2 cursor-pointer' data-toggle="modal" data-target="#myModal" title='<%=BoothList.Tables[0].Rows[i]["DATA"].ToString() %>'>
                         <%--onclick="LoadData('<%=exam.Common.Encode("007$" + BoothList.Tables[0].Rows[i]["district"].ToString() + "_" + BoothList.Tables[0].Rows[i]["ac"].ToString() + "_" + BoothList.Tables[0].Rows[i]["DATA"].ToString())%>');"--%>
                        <%=BoothList.Tables[0].Rows[i]["TotalBooth"].ToString().Length > 11 ? BoothList.Tables[0].Rows[i]["TotalBooth"].ToString().PadRight(140).Substring(0, 11).TrimEnd() + ".." : BoothList.Tables[0].Rows[i]["TotalBooth"].ToString()%>
                    </a></h2>
                </div>
            </td>
               <td class="py-xs px-sm" style="width: 15%;">
                <div class="flex-stretch lastonehour f-color-white f-size-sm md:f-size-sm flex-col justify-center text-center shadow-md-primary capitalize">
                    <h2 class="f-size-sm md:f-size-sm"><a class='whiteText btn btn-primary-light btnwidth2 cursor-pointer' data-toggle="modal" data-target="#myModal" title='<%=BoothList.Tables[0].Rows[i]["DATA"].ToString() %>'
                         ><%--onclick="LoadData('<%=exam.Common.Encode("001$" +BoothList.Tables[0].Rows[i]["district"].ToString() + "_" + BoothList.Tables[0].Rows[i]["ac"].ToString() + "_" + BoothList.Tables[0].Rows[i]["DATA"].ToString())%>');"--%>
                        <%=BoothList.Tables[0].Rows[i]["lastLive"].ToString().Length > 11 ? BoothList.Tables[0].Rows[i]["lastLive"].ToString().PadRight(140).Substring(0, 11).TrimEnd() + ".." : BoothList.Tables[0].Rows[i]["lastLive"].ToString()%>
                    </a></h2>
                </div>
            </td>
            <td class="py-xs px-sm" style="width: 15%;">
                <div class="flex-stretch  bg-success f-color-white f-size-sm md:f-size-sm flex-col justify-center text-center shadow-md-success capitalize">
                    <h2 class="f-size-sm md:f-size-sm"><a class='btn btn-success btnwidth2 cursor-pointer' data-toggle="modal" data-target="#myModal" title='<%=BoothList.Tables[0].Rows[i]["DATA"].ToString() %>'>
                         <%--onclick="LoadData('<%=exam.Common.Encode("000$" +BoothList.Tables[0].Rows[i]["district"].ToString() + "_" + BoothList.Tables[0].Rows[i]["ac"].ToString() + "_" + BoothList.Tables[0].Rows[i]["DATA"].ToString())%>');"--%>
                        <%=BoothList.Tables[0].Rows[i]["Live"].ToString().Length > 11 ? BoothList.Tables[0].Rows[i]["Live"].ToString().PadRight(140).Substring(0, 11).TrimEnd() + ".." : BoothList.Tables[0].Rows[i]["Live"].ToString()%>
                    </a></h2>
                </div>
            </td>
             <td class="py-xs px-sm" style="width: 15%;">
                <div class="cnectonediv flex-stretch f-color-white f-size-sm md:f-size-sm flex-col justify-center text-center shadow-md-secondary capitalize">
                    <h2 class="f-size-sm md:f-size-sm"><a class='whiteText btn btnwidth2 cursor-pointer' data-toggle="modal" data-target="#myModal" title='<%=BoothList.Tables[0].Rows[i]["DATA"].ToString() %>'
                         ><%--onclick="LoadData('<%=exam.Common.Encode("003$" +BoothList.Tables[0].Rows[i]["district"].ToString() + "_" + BoothList.Tables[0].Rows[i]["ac"].ToString() + "_" + BoothList.Tables[0].Rows[i]["DATA"].ToString())%>');"--%>
                        <%=BoothList.Tables[0].Rows[i]["Connectedonce"].ToString().Length > 11 ? BoothList.Tables[0].Rows[i]["Connectedonce"].ToString().PadRight(140).Substring(0, 11).TrimEnd() + ".." : BoothList.Tables[0].Rows[i]["Connectedonce"].ToString()%>
                    </a></h2>
                </div>
            </td>
            <td class="py-xs px-sm" style="width: 15%;">
                <div class="offlinediv flex-stretch f-color-white f-size-sm md:f-size-sm flex-col justify-center text-center shadow-md-warning capitalize">
                    <h2 class="f-size-sm md:f-size-sm"><a class='btn whiteText btnwidth2 cursor-pointer' data-toggle="modal" data-target="#myModal" title='<%=BoothList.Tables[0].Rows[i]["DATA"].ToString() %>'>
                         <%--onclick="LoadData('<%=exam.Common.Encode("002$" +BoothList.Tables[0].Rows[i]["district"].ToString() + "_" + BoothList.Tables[0].Rows[i]["ac"].ToString() + "_" + BoothList.Tables[0].Rows[i]["DATA"].ToString())%>');"--%>
                        <%=BoothList.Tables[0].Rows[i]["stop"].ToString().Length > 11 ? BoothList.Tables[0].Rows[i]["stop"].ToString().PadRight(140).Substring(0, 11).TrimEnd() + ".." : BoothList.Tables[0].Rows[i]["stop"].ToString()%>
                    </a></h2>
                </div>
            </td>
           
         
        </tr>
    </tbody>
    <%}
        }
        else
        {%>
    <tbody>
        <tr class='bg-white'>
            <td colspan="7">No Record found.</td>
        </tr>
    </tbody>
    <%}
        }
        else
        {%>
    <tbody>
        <tr class='bg-white'>
            <td colspan="7">No Record found.</td>
        </tr>
    </tbody>
    <%}
            
        }else
        {%>
    <tbody>
        <tr class='bg-white'>
            <td colspan="7">No Record found.</td>
        </tr>
    </tbody>
    <%} %>
</table>
<%--<asp:ListView ID="listview1" runat="server">
    <itemtemplate>
        <div class="col-md-12">
            <div class="btn btn-default" style="width: 100%">
                <h4 title='<%#Eval("DATA") %>' class=" pull-left">
                    <%#Eval("DATA").ToString().Length > 15 ? Eval("DATA").ToString().PadRight(140).Substring(0, 15).TrimEnd() + "..." : Eval("DATA")%>
                </h4>
                <span class="pull-right">
                    <%if (strcode == "007")
                        { %>
                    <span class='btn btn-primary btnwidth' onclick="LoadData('<%#exam.Common.Encode("007$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');">
                        <%#Eval("TotalBooth")%></span>
                    <%}
                        else if (strcode == "000")
                        {%>
                    <span class='btn btn-success btnwidth' onclick="LoadData('<%#exam.Common.Encode("000$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');">
                        <%#Eval("Live") %></span>
                    <%}
                        else if (strcode == "001")
                        {%>
                    <span class='btn btn-info btnwidth' onclick="LoadData('<%#exam.Common.Encode("001$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');">
                        <%#Eval("lastLive") %></span>
                    <%}
                        else if (strcode == "003")
                        {%>
                    <span class='btn btn-warning btnwidth' onclick="LoadData('<%#exam.Common.Encode("003$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');">
                        <%#Eval("Connectedonce") %></span>
                    <%}
                        else if (strcode == "002")
                        {%>
                    <span class='btn btn-danger btnwidth' onclick="LoadData('<%#exam.Common.Encode("002$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');">
                        <%#Eval("stop") %></span>
                    <%}
                        else
                        {%>
                    <span class='btn btn-primary btnwidth' onclick="LoadData('<%#exam.Common.Encode("007$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');">
                        <%#Eval("TotalBooth")%></span>
                    <span class='btn btn-warning btnwidth' onclick="LoadData('<%#exam.Common.Encode("000$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');">
                        <%#Eval("Connectedonce") %></span>
                    <span class='btn btn-success btnwidth' onclick="LoadData('<%#exam.Common.Encode("000$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');">
                        <%#Eval("Live") %></span>
                    <span class='btn btn-info btnwidth' onclick="LoadData('<%#exam.Common.Encode("000$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');">
                        <%#Eval("lastLive") %></span> <span class='btn btn-danger btnwidth' onclick="LoadData('<%#exam.Common.Encode("002$" +Eval("district").ToString() + "_" + Eval("ac").ToString() + "_" + Eval("DATA").ToString())%>');">

                            <%#Eval("stop") %></span>
                    <%} %>
                </span>
            </div>
        </div>
    </itemtemplate>
</asp:ListView>--%>
<script type="text/javascript">
    function LoadData(loc) {
        window.top.location.href = "Listview.aspx?d=" + loc;
    }
</script>

