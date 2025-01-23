using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace exam.DAL
{
    public class db_data
    {
        public string connstr = Common.DecodeConnectionstring(ConfigurationManager.ConnectionStrings["connectionstr"].ToString());
        public int minute = Convert.ToInt32(ConfigurationManager.AppSettings["minute"]);
        public string stcode = ConfigurationManager.AppSettings["stcode"].ToString();
        public string table_prefix = ConfigurationManager.AppSettings["tb_prefix"].ToString();
        public string currentphase = ConfigurationManager.AppSettings["stateid"].ToString();
        public DateTime start_hour = Convert.ToDateTime(ConfigurationManager.AppSettings["starthour"].ToString());
        public DateTime end_hour = Convert.ToDateTime(ConfigurationManager.AppSettings["endhour"].ToString());
       


        public DataSet GetUser(string username, string password)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                using (SqlCommand command = new SqlCommand("GetUser", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                Common.Log("GetUser()--> " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ds;
        }



        public DataSet GetUserData(string username)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                using (SqlCommand command = new SqlCommand("GetUserData", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", username);

                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                Common.Log("GetUserData()--> " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ds;
        }


        public DataSet GetMapBoothList(string usertype, bool isgrid, int st_id)
        {
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string dist_sch = string.Empty;
                string[] utype = { };
                dist = usertype.Split('_')[1];
                dist = dist.ToUpper() == "ALL DISTRICT" ? "" : dist;
                if (usertype.StartsWith("sch"))
                {
                    dist_sch = usertype.Split('_')[2];
                }
                query = "GetMapDetails";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@dst", dist);
                command.Parameters.AddWithValue("@acname", dist_sch);
                command.Parameters.AddWithValue("@userid", st_id);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds1 = FilterDataByAccess(ds, true, true);
                ds1.Tables[0].TableName = "Table";
                ds1.Tables.Add(ds.Tables[1].Copy());
            }
            catch (Exception ex)
            {
                Common.Log("GetBoothList()--> " + ex.Message);
            }
            finally
            {

            }
            return ds1;
        }


        public DataSet GetMapBoothListNew_Grid(int userid, string district, string assembly, int statusflag, string status, int isPink = -1, int isARO = -1, string booth = "", string streamname = "", string orderBy = "", int PageNumber = 1, int pageitemcount = 6)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetBoothListNew1", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@assembly", assembly);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@isPink", isPink);
                command.Parameters.AddWithValue("@isARO", isARO);
                command.Parameters.AddWithValue("@booth", booth);
                command.Parameters.AddWithValue("@streamname", streamname);
                command.Parameters.AddWithValue("@psnum", streamname);
                command.Parameters.AddWithValue("@location", streamname);
                command.Parameters.AddWithValue("@statusFlag", statusflag);
                command.Parameters.AddWithValue("@UserID", userid);
                command.Parameters.AddWithValue("@pageIndex", PageNumber);
                command.Parameters.AddWithValue("@pageSize", pageitemcount);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                //ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetMapBoothListNew()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }

        public DataSet GetOnlineMapBoothListNew(string district, string assembly, string status, int isPink = -1, int isARO = -1, string booth = "", string streamname = "", string orderBy = "")
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetOnlineBoothListNew", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@assembly", assembly);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@isPink", isPink);
                command.Parameters.AddWithValue("@isARO", isARO);
                command.Parameters.AddWithValue("@booth", booth);
                command.Parameters.AddWithValue("@streamname", streamname);
                command.Parameters.AddWithValue("@psnum", streamname);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetMapBoothListNew()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }

        public DataSet GetBoothList(string usertype, bool isgrid, int st_id)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string dist_sch = string.Empty;
                string dist_loc = string.Empty;
                string strzone = string.Empty;
                string[] utype = { };
                string tblname = string.Empty;
                if (!isgrid)
                {
                    if (usertype == "Master_Admin")
                    {
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id  where b.isdisplay='True' and s.IsEnable=1 and b.boothstateid=" + st_id + " Order By b.acname,b.location asc,len(s.streamname) asc; select * from " + table_prefix + "static_count;";
                    }
                    else if (usertype == "live")
                    {
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id  where b.isdisplay='True' and s.status='RUNNING' and s.IsEnable=1 and b.boothstateid=" + st_id + " Order By b.acname,b.location asc,len(s.streamname) asc";
                    }
                    else if (usertype.StartsWith("zn"))
                    {
                        strzone = usertype.Split('_')[1];
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id  where b.isdisplay='True' and b.district in (select zoneDistrict from " + table_prefix + "zoneinfo where zoneName =@zn) and s.IsEnable=1 and b.boothstateid='" + st_id + "' Order By b.acname,b.location asc,len(s.streamname) asc";
                    }
                    else if (usertype.StartsWith("dst"))
                    {
                        if (usertype.Contains("?"))
                        {
                            utype = usertype.Split('?');
                            if (utype[1] != st_id.ToString())
                            {
                                tblname = "p" + utype[1];
                            }
                            dist = utype[0].Split('_')[1];
                            query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id  where b.isdisplay='True' and b.district=@dst and s.IsEnable=1 and b.boothstateid='" + utype[1] + "' Order By b.acname,b.location asc,len(s.streamname) asc";
                        }
                        else
                        {
                            dist = usertype.Split('_')[1];
                            query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id  where b.isdisplay='True' and b.district=@dst and s.IsEnable=1 Order By b.acname,b.location asc,len(s.streamname) asc";
                        }
                    }
                    else if (usertype.StartsWith("sch"))
                    {
                        //dist_sch = Convert.ToInt32(usertype.Split('_')[2]);
                        //query = "select * from booth b inner join streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and b.district=@dst and b.id = @dist_sch and b.boothstateid='" + st_id + "' Order By s.status,LEN(s.streamname),s.id";
                        if (usertype.Contains("?"))
                        {
                            utype = usertype.Split('?');
                            if (utype[1] != st_id.ToString())
                            {
                                tblname = "p" + utype[1];
                            }
                            dist = utype[0].Split('_')[1];
                            dist_sch = utype[0].Split('_')[2];
                            query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist" + tblname + " s on b.streamid=s.id  where b.isdisplay='True' and s.IsEnable=1 and b.district=@dst and b.acname = @dist_sch and b.boothstateid='" + utype[1] + "' Order By b.acname,b.location asc,len(s.streamname) asc";
                        }
                        else
                        {
                            dist = usertype.Split('_')[1];
                            dist_sch = usertype.Split('_')[2];
                            query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id  where b.isdisplay='True' and s.IsEnable=1 and b.district=@dst and b.acname = @dist_sch Order By b.acname,b.location asc,len(s.streamname) asc";
                        }
                    }

                    else if (usertype.StartsWith("loc"))
                    {

                        dist = usertype.Split('_')[1];
                        dist_sch = usertype.Split('_')[2];
                        dist_loc = usertype.Split('_')[3];
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id  where b.isdisplay='True' and s.IsEnable=1 and b.district=@dst and b.acname = @dist_sch and b.psnum=@dist_loc Order By b.acname,b.location asc,len(s.streamname) asc";

                    }

                }
                else
                {
                    if (usertype == "Master_Admin")
                    {
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.status='RUNNING' and s.IsEnable=1 and b.boothstateid=" + st_id + " Order By s.lastseen desc";
                    }
                    else if (usertype == "live")
                    {
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.status='RUNNING' and s.IsEnable=1 and b.boothstateid=" + st_id + " Order By s.lastseen desc";
                    }
                    else if (usertype.StartsWith("zn"))
                    {
                        strzone = usertype.Split('_')[1];
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.status='RUNNING' and b.district in (select zoneDistrict from " + table_prefix + "zoneinfo where zoneName =@zn) and s.IsEnable=1 and b.boothstateid='" + st_id + "' Order By s.hasalarm desc";
                    }
                    else if (usertype.StartsWith("dst"))
                    {
                        dist = usertype.Split('_')[1];
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.status='RUNNING' and b.boothstateid=" + st_id + " and b.district=@dst and s.IsEnable=1 Order By s.hasalarm desc";
                    }
                    else if (usertype.StartsWith("sch"))
                    {
                        dist = usertype.Split('_')[1];
                        // dist_sch = Convert.ToInt32(usertype.Split('_')[2]);
                        dist_sch = usertype.Split('_')[2];
                        //query = "select * from booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.status='RUNNING' and s.IsEnable=1 and b.district=@dst and b.id = @dist_sch and b.boothstateid='" + st_id + "' Order By s.status,LEN(s.streamname),s.id";
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.status='RUNNING' and s.IsEnable=1 and b.district=@dst and b.acname = @dist_sch and b.boothstateid=" + st_id + " Order By s.lastseen desc";
                    }
                }
                SqlCommand command = new SqlCommand(query, conn);
                if (!string.IsNullOrEmpty(dist))
                {
                    command.Parameters.AddWithValue("@dst", dist);
                }
                if (!string.IsNullOrEmpty(strzone))
                {
                    command.Parameters.AddWithValue("@zn", strzone);
                }
                if (!string.IsNullOrEmpty(dist) && !string.IsNullOrEmpty(dist_sch))
                {
                    command.Parameters.AddWithValue("@dist_sch", dist_sch);
                }
                if (!string.IsNullOrEmpty(dist) && !string.IsNullOrEmpty(dist_sch) && !string.IsNullOrEmpty(dist_loc))
                {
                    command.Parameters.AddWithValue("@dist_loc", dist_loc);
                }

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetBoothList()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        public List<string> GetLocation(string location, string usertype)
        {
            try
            {
                List<string> locationres = new List<string>();
                using (SqlConnection con = new SqlConnection(connstr))
                {
                    return locationres;
                }
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        public List<string> Getbooth(string searchtxt, string usertype)
        {
            try
            {
                List<string> locationres = new List<string>();

                return locationres;
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }



        public DataSet GetDistrictList(string usertype, int st_id)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string tblname = string.Empty;
                string[] utype = { };
                query = "SELECT distinct(b.district)district,b.district as SelValue FROM " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id WHERE Isdisplay=1 and b.boothstateid=" + st_id + " ORDER BY b.District ASC;";

                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, false);
            }
            catch (Exception ex)
            {
                Common.Log("GetDistrictList()--> " + ex.Message);
            }
            finally
            {
            }
            return ds;
        }


        public DataSet Getdistrictid(int id)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string tblname = string.Empty;
                string[] utype = { };
                query = @"
                SELECT *
                FROM district
                WHERE id IN (
                    SELECT CAST(value AS INT)
                    FROM STRING_SPLIT((SELECT AssemblyAccesIds FROM users WHERE usercode = 'District_Level' AND id = @UserId), ',')
                )";


                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@UserId", id);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                //  ds = FilterDataByAccess(ds, true, false);
            }
            catch (Exception ex)
            {
                Common.Log("GetDistrictList()--> " + ex.Message);
            }
            finally
            {
            }
            return ds;
        }
        public DataSet GetDistrictListECI(string usertype, int st_id)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string tblname = string.Empty;
                string[] utype = { };
                if (usertype == "Master_Admin" || usertype == "live")
                {
                    query = "SELECT distinct(b.district)district,b.district as SelValue  FROM " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id WHERE Isdisplay=1 and b.boothstateid='" + st_id + "' and s.selected=1 ORDER BY b.District ASC;";
                }
                else if (usertype.Contains("?"))
                {
                    utype = usertype.Split('?');
                    if (utype[1] != st_id.ToString())
                    {
                        tblname = "p" + utype[1];
                    }
                    if (utype[0] == "admin" || utype[0] == "live")
                    {
                        query = "SELECT distinct(b.district)district,b.district as SelValue FROM " + table_prefix + "booth" + tblname + " b inner join " + table_prefix + "streamlist" + tblname + " s on b.streamid=s.id WHERE Isdisplay=1 and b.boothstateid='" + utype[1] + "' ORDER BY b.District ASC;";
                    }

                    else
                    {
                        dist = utype[0].Split('_')[1];
                        query = "SELECT distinct(b.district)district,b.district as SelValue FROM Booth" + tblname + " b inner join streamlist" + tblname + " s on b.streamid=s.id WHERE b.Isdisplay=1 and b.district =N'" + dist + "' and b.boothstateid='" + utype[1] + "' ORDER BY b.District ASC;";
                        //query = "SELECT distinct([acname]) FROM [Booth] WHERE Isdisplay=1 and district ='" + dist + "'  ORDER BY [schoolname] ASC;";
                    }

                }
                else if (usertype.StartsWith("zn"))
                {
                    dist = usertype.Split('_')[1];
                    query = "SELECT distinct(b.district)district,b.district as SelValue FROM " + table_prefix + "booth b inner join streamlist s on b.streamid=s.id WHERE b.Isdisplay=1 and b.district  in (select zoneDistrict from " + table_prefix + "zoneinfo where zoneName ='" + dist + "')  and b.boothstateid='" + st_id + "' ORDER BY b.District ASC;";

                }
                else
                {
                    dist = usertype.Split('_')[1];
                    query = "SELECT distinct(b.district)district,b.district as SelValue FROM " + table_prefix + "booth b inner join streamlist s on b.streamid=s.id WHERE b.Isdisplay=1 and b.district = N'" + dist + "' and b.boothstateid='" + st_id + "' and s.selected=1 ORDER BY b.District ASC;";
                    //query = "SELECT distinct([acname]) FROM [Booth] WHERE Isdisplay=1 and district ='" + dist + "'  ORDER BY [schoolname] ASC;";
                }


                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetDistrictList()--> " + ex.Message);
            }
            finally
            {
            }
            return ds;
        }
        public void UpdateStatus(string streamname)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = query = "UPDATE " + table_prefix + "streamlist SET hasalarm='False' WHERE streamname = @streamname;";
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
                SQLcommand.Parameters.Add(new SqlParameter("@streamname", streamname));
                SQLconn.Open();
                SQLcommand.ExecuteNonQuery();
                SQLconn.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                SQLconn.Close();
            }
        }

        public void updateloginstatus(bool status, string username)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = query = "UPDATE " + table_prefix + "users set islogin = @islogin, logincount=logincount-1 where username = @username";
                //  string query = query = "UPDATE " + table_prefix + "users set islogin = @islogin where username = @username";
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
                SQLcommand.Parameters.Add(new SqlParameter("@islogin", status));
                SQLcommand.Parameters.Add(new SqlParameter("@username", username));
                SQLconn.Open();
                SQLcommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                SQLconn.Close();
            }
        }

        public bool updatelogincount(string username)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = query = "UPDATE " + table_prefix + "users set logincount = logincount+1 where username = @username";
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);

                SQLcommand.Parameters.Add(new SqlParameter("@username", username));
                SQLconn.Open();
                SQLcommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                SQLconn.Close();
            }
        }



        public DataSet getUnMappedCamera()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                using (SqlCommand command = new SqlCommand("GetUnMappedCamera", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                Common.Log("getCameraMatchData()--> " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return ds;
        }


        public DataSet GetAllDistrictByStateId(int stateid)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string tblname = string.Empty; 
                query = "getdistrictName"; 
                SqlCommand command = new SqlCommand(query, conn);
                 command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetDistrictList()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetAllAssemblyByDistrict(int stateid, string district)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "SELECT accode,acname  from " + table_prefix + "district WHERE stateid=" + stateid + " AND district='" + district + "' ORDER BY District ASC;";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, false, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetAllAssembly()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }



       
      
     
        public DataSet FilterDataByAccess(DataSet ds, bool filterbyDistrict, bool filterbyAssembly)
        {
            if (ds != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dtAccess = (DataTable)HttpContext.Current.Session["userAssemblyAccess"];
                    var districtlist = string.Join(",", dtAccess.AsEnumerable().Select(r => r.Field<string>("district")).ToArray());
                    var assemblylist = string.Join(",", dtAccess.AsEnumerable().Select(r => r.Field<string>("acname")).ToArray());
                    DataTable dt = null;
                    if (filterbyDistrict)
                    {
                        var a = ds.Tables[0].AsEnumerable().Where(x => districtlist.Contains(x.Field<string>("district")));
                        if (a != null && a.Count() > 0)
                            dt = ds.Tables[0].AsEnumerable().Where(x => districtlist.Contains(x.Field<string>("district"))).CopyToDataTable();
                    }
                    if (filterbyAssembly)
                    {
                        dt = ds.Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            var b = dt.AsEnumerable().Where(x => assemblylist.Contains(x.Field<string>("acname")));
                            if (b != null && b.Count() > 0)
                                dt = dt.AsEnumerable().Where(x => assemblylist.Contains(x.Field<string>("acname"))).CopyToDataTable();
                        }
                    }
                    DataSet returnds = new DataSet();
                    if (dt != null)
                        returnds.Tables.Add(dt.Copy());
                    return returnds;
                }
            }
            return ds;
        }

          
      
        public DataSet GetDashboardList(string district, string accode, int UserID)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);

            DateTime chklastseendt = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minute"]) * -1);

            try
            {
                string query = string.Empty;
                query = "GetDashboardList";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@lastseen", chklastseendt);
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@accode", accode);
                command.Parameters.AddWithValue("@UserID", UserID);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, false);
            }
            catch (Exception ex)
            {
                Common.Log("GetDashboardList()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetIndoorOutDoorForGraph(string district, string accode, int UserID)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);

            DateTime chklastseendt = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minute"]) * -1);

            try
            {
                string query = string.Empty;
                query = "GetIndoorOutDoorForGraph";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@lastseen", chklastseendt);
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@accode", accode);
                command.Parameters.AddWithValue("@UserID", UserID);
                command.CommandTimeout = 240;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                //ds = FilterDataByAccess(ds, true, false);
            }
            catch (Exception ex)
            {
                Common.Log("GetDashboardList()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }


        public DataSet GetDashboardDetailListByAssembly(string acname, string status, string CameraType = "", int IsPink = -1)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetDashboardDetailListByAcName";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@acname", acname);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@CameraType", CameraType);
                command.Parameters.AddWithValue("@IsPink", IsPink);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetDashboardDetailList()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }


        public DataSet GetPTZViewData(string vehicleNo)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetPTZViewdtels";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@vehicalno", vehicleNo);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetDashboardDetailList()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetPTZViewDatawithcamera(string streamid)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetPTZViewdtelscamera";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@streamid", streamid);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetDashboardDetailList()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetShiftWizeVehicleByAcCode(string accode)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetLocationbyac";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@location", accode);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetShiftWizeVehicleByAcCode()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetCameraOfflineList(string FromDt, string ToDt, string district, string accode, string vehicleNo, string shift)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetCameraOfflineList5";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@accode", accode);
                command.Parameters.AddWithValue("@psnum", vehicleNo);
                command.Parameters.AddWithValue("@FromDt", FromDt);
                command.Parameters.AddWithValue("@ToDt", ToDt);
                command.CommandTimeout = 240;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                //ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetShiftWizeVehicleByAcCode()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }


        public DataSet GetCameraStatusReport(string FromDt, string ToDt, string district, string accode, string vehicleNo)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "[dbo].[GetCameraStatusReport]";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@accode", accode);
                command.Parameters.AddWithValue("@psnum", vehicleNo);
                command.Parameters.AddWithValue("@FromDt", FromDt);
                command.Parameters.AddWithValue("@ToDt", ToDt);
                command.CommandTimeout = 240;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
              //  ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetShiftWizeVehicleByAcCode()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetUserLoginHistoryReport(string dt)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetUserLoginHistoryReport";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@dt", dt);
                command.CommandTimeout = 240;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetUserLoginHistoryReport()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetExcelReport(string FromDt, string ToDt, string district, string accode, string vehicleNo, int islive, string status)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "[dbo].[GetExeclBoothReport]";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@accode", accode);
                command.Parameters.AddWithValue("@psnum", vehicleNo);
                command.Parameters.AddWithValue("@FromDt", FromDt);
                command.Parameters.AddWithValue("@ToDt", ToDt);
                command.Parameters.AddWithValue("@Status", "");
                command.CommandTimeout = 240;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetShiftWizeVehicleByAcCode()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetUnmappedCameraList(string CameraID, string status)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                SqlCommand command = new SqlCommand("GetUnmappedCameraList", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CameraID", CameraID);
                command.Parameters.AddWithValue("@Status", status);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetUserData()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet SaveLoginUserHistory(int userid, string ipaddress)
        {
            DataSet ds = new DataSet();
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                SqlCommand SQLcommand = new SqlCommand("SaveLoginUserHistory", SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.Add(new SqlParameter("@UserID", userid));
                SQLcommand.Parameters.Add(new SqlParameter("@IPAddress", ipaddress));
                SQLconn.Open();
                SqlDataAdapter da = new SqlDataAdapter(SQLcommand);
                da.Fill(ds);
                //SQLcommand.ExecuteNonQuery();
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
            finally
            {
                SQLconn.Close();
            }
        }
        public DataSet UpdateUserSessionActivityByUserId(int userid, string sessionid)
        {
            DataSet ds = new DataSet();
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                SqlCommand SQLcommand = new SqlCommand("UpdateUserSessionActivityByUserId", SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.Add(new SqlParameter("@UserID", userid));
                SQLcommand.Parameters.Add(new SqlParameter("@SessionId", sessionid));
                SQLconn.Open();
                SQLcommand.ExecuteNonQuery();
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
            finally
            {
                SQLconn.Close();
            }
        }
        public DataSet getuserbysessionid(int userid, string sessionid)
        {
            DataSet ds = new DataSet();
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                SqlCommand SQLcommand = new SqlCommand("GetActiveUserSessionByUserId", SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.Add(new SqlParameter("@UserID", userid));
                SQLcommand.Parameters.Add(new SqlParameter("@SessionId", sessionid));
                SQLconn.Open();
                SqlDataAdapter da = new SqlDataAdapter(SQLcommand);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                return ds;
            }
            finally
            {
                SQLconn.Close();
            }
        }
        public bool deleteuserbysessionid(int userid, string sessionid)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                SqlCommand SQLcommand = new SqlCommand("DeleteUserSessionActivityByUserId", SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.Add(new SqlParameter("@UserID", userid));
                SQLcommand.Parameters.Add(new SqlParameter("@SessionId", sessionid));
                SQLconn.Open();
                SQLcommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                SQLconn.Close();
            }
        }

        public DataSet SwapCameraIDs(int id1, int id2)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("swapCameraIDs", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id1", id1);
                command.Parameters.AddWithValue("@id2", id2);
                conn.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Common.Log("SwapCameraIDs()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        public DataSet AddUnmapToMapCamera(int id, int stremid, string username)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = "UnMapToMapCamera";
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.AddWithValue("@id", id);
                SQLcommand.Parameters.AddWithValue("@StremlistID", stremid);
                SQLcommand.Parameters.AddWithValue("@UserName", username);
                SQLcommand.Parameters.AddWithValue("@IPAddress", System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());
                SqlDataAdapter adp = new SqlDataAdapter(SQLcommand);
                DataSet ds = new DataSet();
                SQLconn.Open();
                adp.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Common.Log("AddUnmapToMapCamera()--> " + ex.Message);
                return new DataSet();
            }
            finally
            {
                SQLconn.Close();
            }
        }
        
        
        public DataSet GetassemblywiseLocation(string district, string assembly)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "GetAssemblyWiseLocation";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@assembly", assembly);
                command.Parameters.AddWithValue("@flag", "Location");
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetassemblywiseLocation()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        
        public DataSet GetOperatorName(int stateid, string district)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                query = "select * from operator_info WHERE stateid=" + stateid + " AND district='" + district + "'";

                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetOperatorName()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetTrailRunRpt(string district, string assembly, string status)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetBoothListTrailRunRpt", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@assembly", assembly);
                command.Parameters.AddWithValue("@status", status);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetTrailRunRpt()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
         
       

        public int SaveStreamList(string deviceid, string servername, string prourl, string userName, string PageName)
        {
            DataSet ds = new DataSet();
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                SqlCommand SQLcommand = new SqlCommand("SaveStreamList", SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.Add(new SqlParameter("@deviceid", deviceid));
                SQLcommand.Parameters.Add(new SqlParameter("@servername", servername));
                SQLcommand.Parameters.Add(new SqlParameter("@prourl", prourl));
                SQLcommand.Parameters.Add(new SqlParameter("@UserName", userName));
                SQLcommand.Parameters.Add(new SqlParameter("@PageName", PageName));
                SQLconn.Open();
                SqlDataAdapter sqa = new SqlDataAdapter(SQLcommand);
                sqa.Fill(ds);
                int.TryParse(ds.Tables[0].Rows[0]["ID"].ToString(), out int ID);
                return ID;
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                SQLconn.Close();
            }
        }
        //Booth Master Function
        public DataSet GetMapBoothListNew_Master(string district, string assembly, string Search, string cameratype)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetBoothList", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@acname", assembly);
                command.Parameters.AddWithValue("@search", Search);
                command.Parameters.AddWithValue("@cameratype", cameratype);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetMapBoothListNew()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        public DataSet GetDeviceID(string DID)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetStreamNameListAutoComplete", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@search", DID);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetDeviceID()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        public DataSet GETInvStreamName(string DID)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetInvStreamListAutoComplete", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@search", DID);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GETInvStreamName()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        public DataSet GetBoothListMaster(int id)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetBoothListByID", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetMapBoothListNew()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        public DataSet SaveBooth(int id, int streamid, string OpName, string OpMobNo, string OpDesignation, string district, string accode, string acname, string PSNum, string location, string cameralocationtype, string userName)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = "SaveBoothByID";
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.AddWithValue("@id", id);
                SQLcommand.Parameters.AddWithValue("@streamid", streamid);
                SQLcommand.Parameters.AddWithValue("@OperatorName", OpName);
                SQLcommand.Parameters.AddWithValue("@OperatorNumber", OpMobNo);
                SQLcommand.Parameters.AddWithValue("@OperatorDesignation", OpDesignation);
                SQLcommand.Parameters.AddWithValue("@district", district);
                SQLcommand.Parameters.AddWithValue("@accode", accode);
                SQLcommand.Parameters.AddWithValue("@acname", acname);
                SQLcommand.Parameters.AddWithValue("@PSNum", PSNum);
                SQLcommand.Parameters.AddWithValue("@location", location);
                SQLcommand.Parameters.AddWithValue("@cameralocationtype", cameralocationtype);
                SQLcommand.Parameters.AddWithValue("@UserName", userName);
                SQLcommand.Parameters.AddWithValue("@IPAddress", System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());
                SqlDataAdapter adp = new SqlDataAdapter(SQLcommand);
                DataSet ds = new DataSet();
                SQLconn.Open();
                adp.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Common.Log("SaveBooth()--> " + ex.Message);
                return new DataSet();
            }
            finally
            {
                SQLconn.Close();
            }
        }
        public bool DeleteBoothListMaster(int id, string UserName)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string tblname = string.Empty;
                SqlCommand comm = new SqlCommand("DeleteBoothByID", SQLconn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@id", id);
                comm.Parameters.AddWithValue("@UserName", UserName);
                comm.Parameters.AddWithValue("@IPAddress", System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());
                SQLconn.Open();
                comm.ExecuteNonQuery();
                SQLconn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Common.Log("deletebooth()--> " + ex.Message);
                return false;
            }
            finally
            {
                SQLconn.Close();
            }
        }

      

        public DataSet SaveBulkStreamList(DataTable dt, string username, string updatedfrom)
        {
            DataSet ds = new DataSet();
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                SqlCommand SQLcommand = new SqlCommand("BulkInsertCamera", SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.AddWithValue("@tblData", dt);
                SQLcommand.Parameters.AddWithValue("@UserName", username);
                SQLcommand.Parameters.AddWithValue("@UpdatedFrom", updatedfrom);
                SQLconn.Open();
                SqlDataAdapter sqa = new SqlDataAdapter(SQLcommand);
                sqa.Fill(ds);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                SQLconn.Close();
            }
            return ds;
        }
        public DataSet getDataSet(string Action, int id)
        {
            SqlConnection sqlConnection = new SqlConnection(connstr);
            string CommandText2 = "GetLatestBoothHistoryByID";
            SqlCommand sqlCommand = new SqlCommand(CommandText2, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BoothId", id);
            sqlCommand.Parameters.AddWithValue("@Action", Action);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;
            DataSet dataSet = new DataSet();
            try
            {
                sqlDataAdapter.Fill(dataSet, "header");
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                sqlConnection.Close();
                return null;
            }
            return dataSet;
        }

        public DataSet GetMapBoothListView(int userid, string district, string assembly, string LoactionType, string status, int isPink = -1, int isARO = -1, string booth = "", string streamname = "", int PageNumber = 1, int pageitemcount = 100)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                SqlCommand command = new SqlCommand("GetMapBoothListViewNew1", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@assembly", assembly);
                command.Parameters.AddWithValue("@LocationType", LoactionType);
                command.Parameters.AddWithValue("@status", status);
                command.Parameters.AddWithValue("@booth", booth);
                command.Parameters.AddWithValue("@streamname", streamname);
                command.Parameters.AddWithValue("@UserID", userid);
                command.Parameters.AddWithValue("@pageIndex", PageNumber);
                command.Parameters.AddWithValue("@pageSize", pageitemcount);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetMapBoothListNew()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        public DataSet GetAlllocationbyAcCode(string accode)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "SELECT location FROM  booth WHERE Accode ='" + accode + "'";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetAllVehicleDetail()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
         
        public DataSet getLocationWise(string district, string acname, string Location)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "getlocationwisedeviceid";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@acname", acname);
                command.Parameters.AddWithValue("@location", Location);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

       
     
        public DataSet getservername(string DID)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {

                SqlCommand command = new SqlCommand("getservarname1", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Camearaid", DID);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetRecordingSize()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        
         //GetZone
        public DataSet GetZone()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string tblname = string.Empty;

                query = "SP_VehicleInstallation";

                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@flag", "GETZONE");
                SqlDataAdapter da = new SqlDataAdapter(command);

                da.Fill(ds);

            }
            catch (Exception ex)
            {
                Common.Log("GetDistrictList()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        //Getdistrict by zone
        public DataSet GetAllistictbyzone(string zone)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "SP_VehicleInstallation";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@flag", "GETDistrict");
                command.Parameters.AddWithValue("@Zone", zone);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                Common.Log("GetAllAssembly()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        //Getacname by zone,distict
        public DataSet GetAllacnamebyzoneandistrict(string zone, string distict)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "SP_VehicleInstallation";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@flag", "GETacname");
                command.Parameters.AddWithValue("@Zone", zone);
                command.Parameters.AddWithValue("@District", distict);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                Common.Log("GetAllAssembly()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        
         public DataSet GetExcelReport1(string zone, string FromDt, string ToDt, string district, string accode, string vehicleNo, int islive, string status)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "[dbo].[GetExeclBoothReport_install]";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@zone", zone);
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@accode", accode);

                command.Parameters.AddWithValue("@FromDt", FromDt);
                command.Parameters.AddWithValue("@ToDt", ToDt);
                //command.Parameters.AddWithValue("@islive", islive);
                command.Parameters.AddWithValue("@Status", status);
                command.CommandTimeout = 240;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetShiftWizeVehicleByAcCode()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

     
        public DataSet GetAllDistrictByuserid(int userid)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string tblname = string.Empty;
                query = "getdistrictNamebyuserid";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@userid", userid);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds); 
            }
            catch (Exception ex)
            {
                Common.Log("GetAllDistrictByuserid()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet GetAllacnameByuserid(string district,int userid)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string tblname = string.Empty;
                query = "getacnamebyuserid";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@userid", userid);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetAllDistrictByuserid()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
    
         public DataSet GetCameraOfflineList11(string FromDt, string ToDt, string district, string accode, string vehicleNo, string shift)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "GetCameraOfflineList11";
                SqlCommand command = new SqlCommand(query, conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@accode", accode);
                command.Parameters.AddWithValue("@vehicle", vehicleNo);
                command.Parameters.AddWithValue("@FromDt", FromDt);
                command.Parameters.AddWithValue("@ToDt", ToDt);
                command.CommandTimeout = 240;
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                //ds = FilterDataByAccess(ds, true, true);
            }
            catch (Exception ex)
            {
                Common.Log("GetShiftWizeVehicleByAcCode()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

    }
}
                    