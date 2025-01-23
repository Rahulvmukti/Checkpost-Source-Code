using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using exam.DAL;
using System.Data;
using System.Configuration;
using exam.BAL;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace exam
{
    /// <summary>
    /// Summary description for eledata
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]

    public class eledata : System.Web.Services.WebService
    {
        db_data _boothlist = new db_data();
        int st_id = Convert.ToInt32(ConfigurationManager.AppSettings["stateid"].ToString());
        int percentage_var = Convert.ToInt32(ConfigurationManager.AppSettings["per"].ToString());
        [WebMethod]
        public List<string> GetBoothLocation(string locationtext, string district, string pcname, string acname, int f)
        {
            try
            {
                db_data _db = new db_data();
                string usertype_loc;

                if (district == "ALL District")
                {
                    //  DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
                    usertype_loc = "admin";
                }
                else
                {
                    if (acname == "ALL " + ConfigurationManager.AppSettings["assemblyname"].ToString())
                    {
                        usertype_loc = "pc_" + district + "_" + pcname;
                    }
                    else
                    {
                        usertype_loc = "sch_" + district + "_" + pcname + "_" + acname;
                    }
                }

                if (f == 1)

                    return _db.Getbooth(locationtext, "ac/ps#" + usertype_loc);
                else
                    return _db.GetLocation(locationtext, usertype_loc);

            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }
        [WebMethod]
        public List<resultcls> getRandomData(int percentage)
        {
            dataclsDataContext dbcls = new dataclsDataContext();

            try
            {
                var all = from b in dbcls.booths
                          join s in dbcls.streamlists on b.streamid equals s.id
                          where b.isdisplay == true && s.IsEnable == true && s.stateID == st_id
                          select new { b, s };
                Random rnd = new Random();
                List<resultcls> data = all.GroupBy(n => n.b.district).Select(group => new resultcls()
                {
                    Metric = group.Key,
                    totalcount = group.Count(),
                    livecount = group.Where(x => x.s.statusFlag == 3).Count(),
                    offlinecount = group.Where(x => x.s.statusFlag == 0).Count(),
                    othercount = ((group.Count() * (rnd.Next(percentage - percentage_var, percentage))) / 100) - group.Where(x => x.s.statusFlag == 3).Count()


                }).ToList();
                //ubdata = group.Where(x => x.s.statusFlag == 0 || x.s.statusFlag == 2).Select(b => b.s.id).Take(((group.Count() * percentage) / 100) - group.Where(x => x.s.statusFlag == 3).Count()).ToList()
                //  var subdata = all.Where(x => x.b.district == "ARIYALUR").OrderBy(c => Guid.NewGuid());
                //return JsonConvert.SerializeObject(data.ToList());
                return data;
            }
            catch (Exception ex)
            {
                return new List<resultcls>();
            }
        }
        [WebMethod]
        public List<string> getCount(string district, string pc, string acname, string booth)
        {
            List<string> result = new List<string>();
            try
            {
                db_data _booth = new db_data();
                DataSet ds = new DataSet();
                db_list_lq c1 = new db_list_lq();
                string usertype;
                string utypeall = "admin_all";
                usertype = "admin";
                //usertype= string.IsNullOrEmpty(district)?"admin":string.IsNullOrEmpty(pc)?"dst_" + district :string.IsNullOrEmpty(acname)?"pc_" + district +"_"+pc :string.IsNullOrEmpty(booth)?"sch_" + district+"_"+pc+"_"+acname:"psbooth_" + district+"_"+pc+"_"+acname+"_"+booth;
                if (utypeall.Contains("_all"))
                {
                    if (district == "0")
                    {
                        //  DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
                        usertype = utypeall.Replace("_all", "");
                    }
                    else
                    {
                        if (pc == "0")
                        {
                            usertype = "dst_" + district;
                        }
                        else
                        {
                            if (acname == "0")
                            {
                                usertype = "pc_" + district + "_" + pc;
                            }
                            else
                            {
                                if (booth == "0")
                                {
                                    usertype = "sch_" + district + "_" + pc + "_" + acname;
                                }
                                else
                                {
                                    usertype = "psbooth_" + district + "_" + pc + "_" + acname + "_" + booth;
                                }
                            }
                        }
                    }
                }
                else if (utypeall.StartsWith("pc"))
                {
                    usertype = "pc_" + district + "_" + acname;
                }
                else if (utypeall.StartsWith("zn"))
                {
                    if (district == "0")
                    {
                        // DataSet ds = _booth.GetUserData(Page.User.Identity.Name);
                        usertype = utypeall;
                    }
                    else
                    {
                        if (pc == "0")
                        {
                            usertype = "dst_" + district;
                        }
                        else
                        {
                            usertype = "pc_" + district + "_" + pc;
                        }
                    }
                }
                else
                {
                    if (pc == "0")
                    {
                        usertype = "dst_" + district;
                    }
                    else
                    {
                        if (acname == "0")
                        {
                            usertype = "pc_" + district + "_" + pc;
                        }
                        else
                        {
                            if (booth == "0")
                            {
                                usertype = "sch_" + district + "_" + pc + "_" + acname;
                            }
                            else
                            {
                                usertype = "psbooth_" + district + "_" + pc + "_" + acname + "_" + booth;
                            }
                        }
                    }
                    //else
                    //{
                    //    usertype = "dd_" + ddlDistrict.SelectedItem.Text + "_" + acname + "_" + ddllocation.SelectedItem.Text + "_" + ddlDate.SelectedItem.Text;
                    //}
                }
                IQueryable<dbData> alldata = c1.GetBoothList(usertype, false, Convert.ToInt32(ConfigurationManager.AppSettings["stateid"]), 1, 300, "", "");
                int total = alldata.Count();
                int live = alldata.Where(x => x.lastseen >= DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minute"]) * -1)).Count();
                int offline = total - live;
                // DataTableResponse res = new DataTableResponse { draw = draw, recordsTotal = alldata.Count(), recordsFiltered = alldata.Count(), data = alldata.Skip(start).Take(length).ToList() };
                // res.draw = dtp.draw;
                result.Add(total.ToString());
                result.Add(live.ToString());
                result.Add(offline.ToString());
                return result;
                // return j.ToString();
            }
            catch (Exception ex)
            {
                return new List<string>();
            }

        }
        [WebMethod]
        public string getdata(int start, int length, int draw, string district, string pc, string acname, string booth)
        {
            db_data _booth = new db_data();
            DataSet ds = new DataSet();
            db_list_lq c1 = new db_list_lq();
            string usertype;
            string utypeall = "admin_all";
            usertype = "admin";
            //usertype= string.IsNullOrEmpty(district)?"admin":string.IsNullOrEmpty(pc)?"dst_" + district :string.IsNullOrEmpty(acname)?"pc_" + district +"_"+pc :string.IsNullOrEmpty(booth)?"sch_" + district+"_"+pc+"_"+acname:"psbooth_" + district+"_"+pc+"_"+acname+"_"+booth;
            if (utypeall.Contains("_all"))
            {
                if (district == "0")
                {
                    //  DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
                    usertype = utypeall.Replace("_all", "");
                }
                else
                {
                    if (pc == "0")
                    {
                        usertype = "dst_" + district;
                    }
                    else
                    {
                        if (acname == "0")
                        {
                            usertype = "pc_" + district + "_" + pc;
                        }
                        else
                        {
                            if (booth == "0")
                            {
                                usertype = "sch_" + district + "_" + pc + "_" + acname;
                            }
                            else
                            {
                                usertype = "psbooth_" + district + "_" + pc + "_" + acname + "_" + booth;
                            }
                        }
                    }
                }
            }
            else if (utypeall.StartsWith("pc"))
            {
                usertype = "pc_" + district + "_" + acname;
            }
            else if (utypeall.StartsWith("zn"))
            {
                if (district == "0")
                {
                    // DataSet ds = _booth.GetUserData(Page.User.Identity.Name);
                    usertype = utypeall;
                }
                else
                {
                    if (pc == "0")
                    {
                        usertype = "dst_" + district;
                    }
                    else
                    {
                        usertype = "pc_" + district + "_" + pc;
                    }
                }
            }
            else
            {
                if (pc == "0")
                {
                    usertype = "dst_" + district;
                }
                else
                {
                    if (acname == "0")
                    {
                        usertype = "pc_" + district + "_" + pc;
                    }
                    else
                    {
                        if (booth == "0")
                        {
                            usertype = "sch_" + district + "_" + pc + "_" + acname;
                        }
                        else
                        {
                            usertype = "psbooth_" + district + "_" + pc + "_" + acname + "_" + booth;
                        }
                    }
                }
                //else
                //{
                //    usertype = "dd_" + ddlDistrict.SelectedItem.Text + "_" + acname + "_" + ddllocation.SelectedItem.Text + "_" + ddlDate.SelectedItem.Text;
                //}
            }
            IQueryable<dbData> alldata = c1.GetBoothList(usertype, false, Convert.ToInt32(ConfigurationManager.AppSettings["stateid"]), 1, 300, "", "");

            DataTableResponse res = new DataTableResponse { draw = draw, recordsTotal = alldata.Count(), recordsFiltered = alldata.Count(), data = alldata.Skip(start).Take(length).ToList() };
            // res.draw = dtp.draw;
            return JsonConvert.SerializeObject(res);
            // return j.ToString();

        }
        string constr = ConfigurationManager.ConnectionStrings["connectionstr"].ConnectionString;
        string table_prefix = ConfigurationManager.AppSettings["tb_prefix"].ToString();
        [WebMethod]
        public List<ListItem> GetDistrict()
        {

            string query = "SELECT distinct(b.district) FROM " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id WHERE Isdisplay=1 ORDER BY b.District ASC;";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    List<ListItem> booth_dist = new List<ListItem>();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            booth_dist.Add(new ListItem
                            {
                                Value = sdr["district"].ToString(),
                                Text = sdr["district"].ToString()
                            });
                        }
                    }
                    con.Close();
                    return booth_dist;
                }
            }
        }

        [WebMethod]
        public List<ListItem> GetPC(string district)
        {

            string query = "SELECT distinct(pc) FROM " + table_prefix + "booth where district=N'" + district + "'";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    List<ListItem> booth_dist = new List<ListItem>();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            booth_dist.Add(new ListItem
                            {
                                Value = sdr["pc"].ToString(),
                                Text = sdr["pc"].ToString()
                            });
                        }
                    }
                    con.Close();
                    return booth_dist;
                }
            }
        }

        [WebMethod]
        public List<ListItem> GetAC(string district, string pc)
        {

            string query = "SELECT distinct(schoolname) FROM " + table_prefix + "booth where district=N'" + district + "' and pc=N'" + pc + "'";


            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    List<ListItem> booth_dist = new List<ListItem>();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            booth_dist.Add(new ListItem
                            {
                                Value = sdr["schoolname"].ToString(),
                                Text = sdr["schoolname"].ToString()
                            });
                        }
                    }
                    con.Close();
                    return booth_dist;
                }
            }
        }

        [WebMethod]
        public List<ListItem> GetBooth(string district, string pc, string acname)
        {

            string query = "SELECT distinct(psnum) FROM " + table_prefix + "booth where  district=N'" + district + "' and pc=N'" + pc + "' and schoolname=N'" + acname + "'";

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    List<ListItem> booth_dist = new List<ListItem>();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            booth_dist.Add(new ListItem
                            {
                                Value = sdr["psnum"].ToString(),
                                Text = sdr["psnum"].ToString()
                            });
                        }
                    }
                    con.Close();
                    return booth_dist;
                }
            }
        }
        [WebMethod]
        public void SwapCamera(int id1, int id2)
        {
            _boothlist.SwapCameraIDs(id1, id2);
        }
        [WebMethod]
        public string[] GETStreamName(string did)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = _boothlist.GetDeviceID(did);
                return ds.Tables[0].Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
            }
            catch (Exception ex)
            {
                return new string[0];
            }
        }
        [WebMethod]
        public string[] GETInvStreamName(string did)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = _boothlist.GETInvStreamName(did);
                return ds.Tables[0].Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
            }
            catch (Exception ex)
            {
                return new string[0];
            }
        }
    }



    public class DataTableParameter
    {
        public int draw { get; set; }
        public int length { get; set; }
        public int start { get; set; }
        public List<columm> columns { get; set; }
    }
    public struct columm
    {
        public string data;
        public string name;
    }


    public struct dtData
    {
        public long rn;
        public string district;
        public string schoolname;
        public string PSNum;
        public string location;
        public string streamname;
    }

    // So my web service returns a serialize version of this
    public struct DataTableResponse
    {
        public int draw;
        public int recordsTotal;
        public int recordsFiltered;
        public List<dbData> data;
    }
    public struct dashboard_data
    {
        public string district;
        public string name;
        public int total;
        public int online;
        public int offline;
    }
    public class resultcls
    {
        public string Metric { get; set; }
        public int totalcount { get; set; }
        public int livecount { get; set; }
        public int offlinecount { get; set; }
        public int othercount { get; set; }

    }
}
