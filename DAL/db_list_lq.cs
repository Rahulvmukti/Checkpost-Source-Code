using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;

namespace exam.DAL
{
    public class db_list_lq
    {
        dataclsDataContext dbcls = new dataclsDataContext();
        public string district = ConfigurationManager.AppSettings["district"].ToString();
        public string allKeyword = ConfigurationManager.AppSettings["AllSelectKeword"].ToString() + " ";

        public streamlist Getstreaminfo(int sid)
        {
            return dbcls.streamlists.SingleOrDefault(x => x.id == sid);
        }
        public int? getUserIdentifier(int uid)
        {
            return dbcls.users.FirstOrDefault(x => x.id == uid)?.identifier;
        }
        public IQueryable<dbData> GetBoothList(string usertype, bool isgrid, int st_id, int start, int end, string search_location, string search_ac_ps, string status = "")
        {
            //"select * from " + table_prefix + "
            //booth b inner join " + table_prefix +
            //"streamlist s on b.streamid=s.id  where b.isdisplay='True' and
            //s.IsEnable=1 and s.stateid=" + st_id + " Order By b.schoolname,b.location asc,len(s.streamname) asc;
            string querywherestr = string.Empty;
            string query = string.Empty;

            string dist = string.Empty;
            string dist_pc = string.Empty;
            string dist_sch = string.Empty;
            string dist_psbooth = string.Empty;
            string dist_loc = string.Empty;
            string strzone = string.Empty;
            string[] utype = { };
            string tblname = string.Empty;
            //  orderby b.schoolname, b.location, s.streamname.Length
            var data = from b in dbcls.booths
                       join s in dbcls.streamlists on b.streamid equals s.id
                       join oi in dbcls.operator_infos on b.operatorid equals oi.id into datainfo
                       from oi in datainfo.DefaultIfEmpty()
                       where b.isdisplay == true && s.IsEnable == true && b.boothstateid == st_id
                       select new { b, s, oi };

            //select new dbData()
            //{
            //    district = b.district,
            //    location = b.location,
            //    PSNum = b.PSNum,
            //    rn = b.id,
            //    schoolname = b.schoolname,
            //    streamname = s.streamname
            //};
            if (usertype == "live")
            {
                data = data.Where(x => x.s.status == "RUNNING");
            }
            else if (usertype.StartsWith("dst"))
            {
                dist = usertype.Split('_')[1];
                if (dist != allKeyword + district)
                {
                    data = data.Where(x => x.b.district == dist);
                }
            }
            else if (usertype.StartsWith("pc"))
            {
                dist = usertype.Split('_')[1];
                dist_pc = usertype.Split('_')[2];
                if (dist == allKeyword + district)
                {
                    data = data.Where(x => x.b.accode == dist_pc);
                }
                else
                {
                    data = data.Where(x => x.b.district == dist && x.b.accode == dist_pc);
                }
            }
            else if (usertype.StartsWith("loc"))
            {

                dist = usertype.Split('_')[1];
                dist_pc = usertype.Split('_')[2];
                dist_sch = usertype.Split('_')[3];
                dist_loc = usertype.Split('_')[4];
                data = data.Where(x => x.b.accode == dist_pc && x.b.acname == dist_sch && x.b.PSNum == dist_loc);

            }
            else if (usertype.StartsWith("sch"))
            {

                dist = usertype.Split('_')[1];
                dist_pc = usertype.Split('_')[2];
                dist_sch = usertype.Split('_')[3];
                if (dist == allKeyword + district)
                {
                    data = data.Where(x => x.b.accode == dist_pc && x.b.acname == dist_sch);
                }
                else
                {
                    data = data.Where(x => x.b.district == dist && x.b.accode == dist_pc && x.b.acname == dist_sch);
                }
            }
            else if (usertype.StartsWith("psbooth"))
            {

                dist = usertype.Split('_')[1];
                dist_pc = usertype.Split('_')[2];
                dist_sch = usertype.Split('_')[3];
                dist_psbooth = usertype.Split('_')[4];
                data = data.Where(x => x.b.accode == dist_pc && x.b.acname == dist_sch && x.b.PSNum == dist_psbooth);

            }
            if (isgrid)
            {
                data = data.Where(x => x.s.status == "RUNNING" && x.s.statusFlag > 2);
            }
            if (!string.IsNullOrEmpty(search_location))
            {
                data = data.Where(x => x.b.location.Contains(search_location.Trim()));
            }
            if (!string.IsNullOrEmpty(search_ac_ps) && search_ac_ps.Contains('/'))
            {
                try
                {
                    string[] arr_search = search_ac_ps.Split('/');
                    data = data.Where(x => x.b.acname.Contains(arr_search[0]) && x.b.PSNum.Contains(arr_search[1]));
                    // data = data.Where(x => x.b.pc.StartsWith(arr_search[0]) && x.b.PSNum.StartsWith(arr_search[1]));
                }
                catch (Exception ex)
                {
                }
            }
            //Check Status Online/Offline/Both
            if (!string.IsNullOrEmpty(status))
            {
                if (status != "BOTH")
                    data = data.Where(x => x.s.status == status);
            }
            int i = start;
            //var final_query = data.OrderBy(x => x.b.schoolname).ThenBy(x => x.b.location).ThenBy(x => x.s.streamname.Length).Select(u => new dbData()
            var final_query = data.Select(u => new dbData()
            {
                id = u.b.id,
                district = u.b.district,
                location = u.b.location,
                PSNum = u.b.PSNum,
                rn = i,
                acname = u.b.acname,
                streamname = u.s.streamname,
                strid = u.b.bkpstreamid == null ? "0" : u.b.bkpstreamid,
                servername = u.s.servername,
                status = u.s.status,
                islive = u.s.isLive,
                prourl = u.s.prourl,
                deviceid = u.s.deviceid,
                accode = u.b.accode,
                sFlag = u.s.statusFlag,
                lastseen = u.s.lastseen,
                operator_name = u.oi.operatorName,
                operator_no = u.oi.operatorNumber,
                operator_alt_no = u.oi.operatorAltNumber
            });
            final_query = FilterDataByAccess(final_query, true, true);
            return final_query;
        }

        public IQueryable<dbData> GetBoothListNew(string district = "", string assembly = "", int st_id = 0)
        {
            //"select * from " + table_prefix + "
            //booth b inner join " + table_prefix +
            //"streamlist s on b.streamid=s.id  where b.isdisplay='True' and
            //s.IsEnable=1 and s.stateid=" + st_id + " Order By b.schoolname,b.location asc,len(s.streamname) asc;
            string querywherestr = string.Empty;
            string query = string.Empty;

            string dist = string.Empty;
            string dist_pc = string.Empty;
            string dist_sch = string.Empty;
            string dist_psbooth = string.Empty;
            string dist_loc = string.Empty;
            string strzone = string.Empty;
            string[] utype = { };
            string tblname = string.Empty;
            //  orderby b.schoolname, b.location, s.streamname.Length
            var data = from b in dbcls.booths
                       join s in dbcls.streamlists on b.streamid equals s.id
                       join oi in dbcls.operator_infos on b.operatorid equals oi.id into datainfo
                       from oi in datainfo.DefaultIfEmpty()
                       where b.isdisplay == true && s.IsEnable == true && b.boothstateid == st_id
                       select new { b, s, oi };

            data = data.Where(x => x.b.district == district || district == "");
            data = data.Where(x => x.b.acname == assembly || assembly == "");
            
            //var final_query = data.OrderBy(x => x.b.schoolname).ThenBy(x => x.b.location).ThenBy(x => x.s.streamname.Length).Select(u => new dbData()
            var final_query = data.Select(u => new dbData()
            {
                id = u.b.id,
                district = u.b.district,
                location = u.b.location,
                PSNum = u.b.PSNum,
                //rn = i,
                acname = u.b.acname,
                streamname = u.s.streamname,
                strid = u.b.bkpstreamid == null ? "0" : u.b.bkpstreamid,
                servername = u.s.servername,
                status = u.s.status,
                islive = u.s.isLive,
                prourl = u.s.prourl,
                deviceid = u.s.deviceid,
                accode = u.b.accode,
                sFlag = u.s.statusFlag,
                lastseen = u.s.lastseen,
                operator_name = u.oi.operatorName,
                operator_no = u.oi.operatorNumber,
                operator_alt_no = u.oi.operatorAltNumber
            });
            final_query = FilterDataByAccess(final_query, true, true);
            return final_query;
        }


        public IQueryable<dbData> FilterDataByAccess(IQueryable<dbData> ds, bool filterbyDistrict, bool filterbyAssembly)
        {
            if (HttpContext.Current.Session["userType"].ToString().ToLower() != "master_admin" && HttpContext.Current.Session["userType"].ToString().ToLower() != "admin")
            {
                DataTable dtAccess = (DataTable)HttpContext.Current.Session["userAssemblyAccess"];
                var districtlist = string.Join(",", dtAccess.AsEnumerable().Select(r => r.Field<string>("district")).ToArray());
                var assemblylist = string.Join(",", dtAccess.AsEnumerable().Select(r => r.Field<string>("acname")).ToArray());

                if (filterbyDistrict)
                {
                    ds = ds.Where(x => districtlist.IndexOf(x.district) >= 0);
                }
                if (filterbyAssembly)
                {
                    ds = ds.Where(x => assemblylist.IndexOf(x.acname) >= 0);
                }

                return ds;
            }
            else
            {
                return ds;
            }


        }
        //public IEnumerable<dashboard_count_data> GetBoothListCount(string usertype, bool isgrid, int st_id, int start, int end)
        //{
        //    //"select * from " + table_prefix + "
        //    //booth b inner join " + table_prefix +
        //    //"streamlist s on b.streamid=s.id  where b.isdisplay='True' and
        //    //s.IsEnable=1 and s.stateid=" + st_id + " Order By b.schoolname,b.location asc,len(s.streamname) asc;
        //    string querywherestr = string.Empty;
        //    string query = string.Empty;

        //    string dist = string.Empty;
        //    string dist_pc = string.Empty;
        //    string dist_sch = string.Empty;
        //    string dist_psbooth = string.Empty;
        //    string dist_loc = string.Empty;
        //    string strzone = string.Empty;
        //    string[] utype = { };
        //    string tblname = string.Empty;
        //    //  orderby b.schoolname, b.location, s.streamname.Length
        //    var data = from b in dbcls.booths
        //               join s in dbcls.streamlists on b.streamid equals s.id
        //               where b.isdisplay == true && s.IsEnable == true && b.boothstateid == st_id
        //               select new { b, s };

        //    //select new dbData()
        //    //{
        //    //    district = b.district,
        //    //    location = b.location,
        //    //    PSNum = b.PSNum,
        //    //    rn = b.id,
        //    //    schoolname = b.schoolname,
        //    //    streamname = s.streamname
        //    //};
        //    if (usertype == "live")
        //    {
        //        data = data.Where(x => x.s.status == "RUNNING");
        //    }
        //    else if (usertype.StartsWith("dst"))
        //    {
        //        dist = usertype.Split('_')[1];
        //        data = data.Where(x => x.b.district == dist);
        //    }
        //    else if (usertype.StartsWith("pc"))
        //    {
        //        dist = usertype.Split('_')[1];
        //        dist_pc = usertype.Split('_')[2];
        //        data = data.Where(x => x.b.district == dist && x.b.accode == dist_pc);
        //    }
        //    else if (usertype.StartsWith("loc"))
        //    {

        //        dist = usertype.Split('_')[1];
        //        dist_pc = usertype.Split('_')[2];
        //        dist_sch = usertype.Split('_')[3];
        //        dist_loc = usertype.Split('_')[4];
        //        data = data.Where(x => x.b.district == dist && x.b.accode == dist_pc && x.b.acname == dist_sch && x.b.PSNum == dist_loc);

        //    }
        //    else if (usertype.StartsWith("sch"))
        //    {

        //        dist = usertype.Split('_')[1];
        //        dist_pc = usertype.Split('_')[2];
        //        dist_sch = usertype.Split('_')[3];
        //        data = data.Where(x => x.b.district == dist && x.b.accode == dist_pc && x.b.acname == dist_sch);

        //    }
        //    else if (usertype.StartsWith("psbooth"))
        //    {

        //        dist = usertype.Split('_')[1];
        //        dist_pc = usertype.Split('_')[2];
        //        dist_sch = usertype.Split('_')[3];
        //        dist_psbooth = usertype.Split('_')[4];
        //        data = data.Where(x => x.b.district == dist && x.b.accode == dist_pc && x.b.acname == dist_sch && x.b.PSNum == dist_psbooth);

        //    }
        //    if (isgrid)
        //    {
        //        data = data.Where(x => x.s.status == "RUNNING");
        //    }
        //    int i = start;
        //    DateTime chklastseen = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minute"]) * -1);
        //    var countingdata = data.GroupBy(x => x.b.accode).AsEnumerable().Select(group => new dashboard_count_data()
        //    {
        //        DATA = group.Key,
        //        pc_code = group.Key.Split('-')[0],
        //        TotalBooth = group.Count(),
        //        Live = group.Where(x => x.s.lastseen >= chklastseen).Count(),
        //        stop = group.Where(x => x.s.lastseen < chklastseen).Count(),

        //    }).OrderBy(x => x.pc_code.Length);
        //    //var final_query = data.OrderBy(x => x.b.schoolname).OrderBy(x => x.b.location).OrderBy(x => x.s.streamname.Length).Select(u => new dbData()
        //    //{
        //    //    district = u.b.district,
        //    //    location = u.b.location,
        //    //    PSNum = u.b.PSNum,
        //    //    rn = i,
        //    //    schoolname = u.b.schoolname,
        //    //    streamname = u.s.streamname,
        //    //    servername = u.s.servername,
        //    //    status = u.s.status,
        //    //    deviceid = u.s.deviceid,
        //    //    pc = u.b.pc,
        //    //    lastseen = u.s.lastseen
        //    //});
        //    return countingdata;
        //}

        public IQueryable<dbData> GetCount(string usertype, bool isgrid, int st_id, int start, int end)
        {
            //"select * from " + table_prefix + "
            //booth b inner join " + table_prefix +
            //"streamlist s on b.streamid=s.id  where b.isdisplay='True' and
            //s.IsEnable=1 and s.stateid=" + st_id + " Order By b.schoolname,b.location asc,len(s.streamname) asc;
            string querywherestr = string.Empty;
            string query = string.Empty;

            string dist = string.Empty;
            string dist_pc = string.Empty;
            string dist_sch = string.Empty;
            string dist_psbooth = string.Empty;
            string dist_loc = string.Empty;
            string strzone = string.Empty;
            string[] utype = { };
            string tblname = string.Empty;
            //  orderby b.schoolname, b.location, s.streamname.Length
            var data = from b in dbcls.booths
                       join s in dbcls.streamlists on b.streamid equals s.id
                       where b.isdisplay == true && s.IsEnable == true && b.boothstateid == st_id
                       select new { b, s };

            //select new dbData()
            //{
            //    district = b.district,
            //    location = b.location,
            //    PSNum = b.PSNum,
            //    rn = b.id,
            //    schoolname = b.schoolname,
            //    streamname = s.streamname
            //};
            if (usertype == "live")
            {
                data = data.Where(x => x.s.status == "RUNNING");
            }
            else if (usertype.StartsWith("dst"))
            {
                dist = usertype.Split('_')[1];
                data = data.Where(x => x.b.district == dist);
            }
            else if (usertype.StartsWith("pc"))
            {
                dist = usertype.Split('_')[1];
                dist_pc = usertype.Split('_')[2];
                data = data.Where(x => x.b.district == dist && x.b.accode == dist_pc);
            }
            else if (usertype.StartsWith("loc"))
            {

                dist = usertype.Split('_')[1];
                dist_pc = usertype.Split('_')[2];
                dist_sch = usertype.Split('_')[3];
                dist_loc = usertype.Split('_')[4];
                data = data.Where(x => x.b.district == dist && x.b.accode == dist_pc && x.b.acname == dist_sch && x.b.PSNum == dist_loc);

            }
            else if (usertype.StartsWith("sch"))
            {

                dist = usertype.Split('_')[1];
                dist_pc = usertype.Split('_')[2];
                dist_sch = usertype.Split('_')[3];
                data = data.Where(x => x.b.district == dist && x.b.accode == dist_pc && x.b.acname == dist_sch);

            }
            else if (usertype.StartsWith("psbooth"))
            {

                dist = usertype.Split('_')[1];
                dist_pc = usertype.Split('_')[2];
                dist_sch = usertype.Split('_')[3];
                dist_psbooth = usertype.Split('_')[4];
                data = data.Where(x => x.b.district == dist && x.b.accode == dist_pc && x.b.acname == dist_sch && x.b.PSNum == dist_psbooth);

            }
            if (isgrid)
            {
                data = data.Where(x => x.s.status == "RUNNING");
            }
            int i = start;
            var final_query = data.OrderBy(x => x.b.acname).OrderBy(x => x.b.location).OrderBy(x => x.s.streamname.Length).Select(u => new dbData()
            {
                district = u.b.district,
                location = u.b.location,
                PSNum = u.b.PSNum,
                rn = i,
                acname = u.b.acname,
                streamname = u.s.streamname,
                servername = u.s.servername,
                islive = u.s.isLive,
                status = u.s.status,
                prourl = u.s.prourl,
                deviceid = u.s.deviceid,
                accode = u.b.accode,
                lastseen = u.s.lastseen
            });
            return final_query;
        }
        public DataSet GetBoothList_1(string usertype, bool isgrid, int st_id, int start, int end, string search_location, string search_ac_ps, string status = "")
        {
            var data = from b in dbcls.booths
                       join s in dbcls.streamlists on b.streamid equals s.id
                       join oi in dbcls.operator_infos on b.operatorid equals oi.id into datainfo
                       from oi in datainfo.DefaultIfEmpty()
                       where b.isdisplay == true && s.IsEnable == true && b.boothstateid == st_id
                       select new
                       {
                           b,
                           s,
                           operatorName = oi != null ? oi.operatorName : null,
                           operatorNumber = oi != null ? oi.operatorNumber : null,
                           statusFlag = s.statusFlag,
                         
                       };

            // Your existing filtering logic goes here

            var filteredData = data.Select(u => new dbData
            {
                id = u.b.id,
                district = u.b.district,
                location = u.b.location,
                PSNum = u.b.PSNum,
                rn = u.b.id, // Assuming id is unique for each record
                acname = u.b.acname,
                streamname = u.s.streamname,
                strid = u.b.bkpstreamid == null ? "0" : u.b.bkpstreamid,
                servername = u.s.servername,
                status = u.s.status,
                islive = u.s.isLive,
                prourl = u.s.prourl,
                deviceid = u.s.deviceid,
                accode = u.b.accode,
                sFlag = u.statusFlag,
                lastseen = u.s.lastseen,
                operatorName = u.operatorName,
                operatorNumber = u.operatorNumber,
                statusFlag = u.statusFlag,
                bkpstreamid = u.b.bkpstreamid == null ? "0" : u.b.bkpstreamid,
                streamnamebkp = u.s.streamnamebkp,
                cameralocationtype = u.b.cameralocationtype,
                isdelete=u.b.isdelete

            }).ToList();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("id", typeof(int));
            dataTable.Columns.Add("district", typeof(string));
            dataTable.Columns.Add("location", typeof(string));
            dataTable.Columns.Add("PSNum", typeof(string));
            dataTable.Columns.Add("rn", typeof(int));
            dataTable.Columns.Add("acname", typeof(string));
            dataTable.Columns.Add("streamname", typeof(string));
            dataTable.Columns.Add("strid", typeof(string));
            dataTable.Columns.Add("servername", typeof(string));
            dataTable.Columns.Add("status", typeof(string));
            dataTable.Columns.Add("islive", typeof(bool));
            dataTable.Columns.Add("prourl", typeof(string));
            dataTable.Columns.Add("deviceid", typeof(string));
            dataTable.Columns.Add("accode", typeof(string));
            dataTable.Columns.Add("sFlag", typeof(int));
            dataTable.Columns.Add("lastseen", typeof(DateTime));
            dataTable.Columns.Add("operatorName", typeof(string));
            dataTable.Columns.Add("operatorNumber", typeof(string));
            dataTable.Columns.Add("statusFlag", typeof(int));
            dataTable.Columns.Add("bkpstreamid", typeof(string));
            dataTable.Columns.Add("streamnamebkp", typeof(string));
            dataTable.Columns.Add("cameralocationtype", typeof(string));
            dataTable.Columns.Add("isdelete", typeof(bool));
            foreach (var item in filteredData)
            {
                dataTable.Rows.Add(item.id, item.district, item.location, item.PSNum, item.rn, item.acname, item.streamname, item.strid, item.servername, item.status, item.islive, item.prourl, item.deviceid, item.accode, item.sFlag, item.lastseen, item.operatorName, item.operatorNumber, item.statusFlag, item.bkpstreamid,item.streamnamebkp,item.cameralocationtype,item.isdelete);
            }

            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dataTable); 
            return dataSet;
        }
    }
    [Serializable()]
    public class dbData
    {
        public int id { get; set; }
        public long rn { get; set; }
        public string district { get; set; }
        public string acname { get; set; }
        public string PSNum { get; set; }
        public string location { get; set; }
        public string streamname { get; set; }
        public string servername { get; set; }
        public string status { get; set; }
        public bool? islive { get; set; }
        public string prourl { get; set; }
        public int? sFlag { get; set; }
        public string deviceid { get; set; }
        public string accode { get; set; }
        public string strid { get; set; }
        public DateTime? lastseen { get; set; }
        public string operator_name { get; set; }

        public string operatorName { get; set; }
        public decimal? operator_no { get; set; }
        public decimal? operatorNumber { get; set; }
        public decimal? operator_alt_no { get; set; }

        public int? statusFlag { get; set; }
        public string bkpstreamid { get; set; }
        public string streamnamebkp { get; set; }
        
        public string cameralocationtype { get; set; }


        public bool? isdelete { get; set; }
    }
}