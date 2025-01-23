using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace exam.DAL
{
    public class training_data
    {
        public string connstr = Common.DecodeConnectionstring(ConfigurationManager.ConnectionStrings["connectionstr"].ToString());
        public int minute = Convert.ToInt32(ConfigurationManager.AppSettings["minute"]);
        public string ph_stateid = ConfigurationManager.AppSettings["ph_stateid"].ToString();
        public string table_prefix = ConfigurationManager.AppSettings["tb_prefix"].ToString();
        public DataSet GetUser(string username, string password)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "SELECT * FROM "+table_prefix+"users WHERE username=@username and password=@password and IsEnable=1 and isDVR=0";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetUser()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        public DataSet GetUserData(string username)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "SELECT * FROM " + table_prefix + "users WHERE username=@username and IsEnable=1 and isDVR=0";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@username", username);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetUserData()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }
        public DataSet GetBoothList(string usertype, bool isgrid)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string dist_sch = string.Empty;
                string[] utype = { };
                string tblname = string.Empty;
                if (!isgrid)
                {
                    if (usertype == "admin")
                    {
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.id = s.schoolid where b.isdisplay='True' and s.IsEnable=1 and s.stateID='" + ph_stateid + "' Order By b.acname,b.location asc";
                    }
                    else if (usertype == "live")
                    {
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.id = s.schoolid where b.isdisplay='True' and s.status='RUNNING' and s.IsEnable=1 and s.stateID='" + ph_stateid + "' Order By b.acname,b.location asc";
                    }

                    else if (usertype.StartsWith("dst"))
                    {
                        if (usertype.Contains("?"))
                        {
                            utype = usertype.Split('?');
                            if (utype[1] != ph_stateid)
                            {
                                tblname = "p" + utype[1];
                            }
                            dist = utype[0].Split('_')[1];
                           query = "select * from booth" + tblname + " b inner join streamlist" + tblname + " s on b.id = s.schoolid where b.isdisplay='True' and b.district=@dst and s.IsEnable=1 and s.stateID='" + utype[1] + "' Order By b.acname,b.location asc";
                        }
                        else
                        {
                            dist = usertype.Split('_')[1];
                            query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.id = s.schoolid where b.isdisplay='True' and b.district=@dst and s.IsEnable=1 and s.stateID='" + ph_stateid + "' Order By b.acname,b.location asc";
                        }
                    }
                    else if (usertype.StartsWith("sch"))
                    {
                        //dist_sch = Convert.ToInt32(usertype.Split('_')[2]);
                        //query = "select * from booth b inner join streamlist s on b.id = s.schoolid where b.isdisplay='True' and s.IsEnable=1 and b.district=@dst and b.id = @dist_sch and s.stateID='" + ph_stateid + "' Order By s.status,LEN(s.streamname),s.id";
                        if (usertype.Contains("?"))
                        {
                            utype = usertype.Split('?');
                            if (utype[1] != ph_stateid)
                            {
                                tblname = "p" + utype[1];
                            }
                            dist = utype[0].Split('_')[1];
                            dist_sch = utype[0].Split('_')[2];
                            query = "select * from booth" + tblname + " b inner join streamlist" + tblname + " s on b.id = s.schoolid where b.isdisplay='True' and s.IsEnable=1 and b.district=@dst and b.acname = @dist_sch and s.stateID='" + utype[1] + "' Order By b.acname,b.location asc";
                        }
                        else
                        {
                            dist = usertype.Split('_')[1];
                            dist_sch = usertype.Split('_')[2];
                            query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.id = s.schoolid where b.isdisplay='True' and s.IsEnable=1 and b.district=@dst and b.acname = @dist_sch and s.stateID='" + ph_stateid + "' Order By b.acname,b.location asc";
                        }
                    }
                }
                else
                {
                    if (usertype == "admin")
                    {
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.id = s.schoolid where b.isdisplay='True' and s.status='RUNNING' and s.IsEnable=1 and s.stateID='" + ph_stateid + "' Order By s.lastseen desc";
                    }
                    else if (usertype == "live")
                    {
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.id = s.schoolid where b.isdisplay='True' and s.status='RUNNING' and s.IsEnable=1 and s.stateID='" + ph_stateid + "' Order By s.lastseen desc";
                    }
                    else if (usertype.StartsWith("dst"))
                    {
                        dist = usertype.Split('_')[1];
                        query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.id = s.schoolid where b.isdisplay='True' and b.district=@dst and s.status='RUNNING' and s.IsEnable=1 and s.stateID='" + ph_stateid + "' Order By s.lastseen desc";
                    }
                    else if (usertype.StartsWith("sch"))
                    {
                        dist = usertype.Split('_')[1];
                        // dist_sch = Convert.ToInt32(usertype.Split('_')[2]);
                        dist_sch = usertype.Split('_')[2];
                        //query = "select * from booth b inner join streamlist s on b.id = s.schoolid where b.isdisplay='True' and s.status='RUNNING' and s.IsEnable=1 and b.district=@dst and b.id = @dist_sch and s.stateID='" + ph_stateid + "' Order By s.status,LEN(s.streamname),s.id";
                           query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.id = s.schoolid where b.isdisplay='True' and s.status='RUNNING' and s.IsEnable=1 and b.district=@dst and b.acname = @dist_sch and s.stateID='" + ph_stateid + "' Order By s.lastseen desc";
                      }
                }
                SqlCommand command = new SqlCommand(query, conn);
                if (!string.IsNullOrEmpty(dist))
                {
                    command.Parameters.AddWithValue("@dst", dist);
                }
                if (!string.IsNullOrEmpty(dist) && !string.IsNullOrEmpty(dist_sch))
                {
                    command.Parameters.AddWithValue("@dist_sch", dist_sch);
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
        public DataSet GetDistrictList(string usertype)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                string tblname = string.Empty;
                string[] utype = { };
                if (usertype == "admin" || usertype == "live")
                {
                    query = "SELECT distinct(b.district) FROM " + table_prefix + "Booth b inner join " + table_prefix + "streamlist s on b.id = s.schoolid WHERE Isdisplay=1 and s.stateID='" + ph_stateid + "' ORDER BY b.District ASC;";
                }
                else if (usertype.Contains("?"))
                {
                    utype = usertype.Split('?');
                    if (utype[1] != ph_stateid)
                    {
                        tblname = "p" + utype[1];
                    }
                    if (utype[0] == "admin" || utype[0] == "live")
                    {
                        query = "SELECT distinct(b.district) FROM Booth" + tblname + " b inner join " + table_prefix + "streamlist" + tblname + " s on b.id = s.schoolid WHERE Isdisplay=1 and s.stateID='" + utype[1] + "' ORDER BY b.District ASC;";
                    }
                    else
                    {
                        dist = utype[0].Split('_')[1];
                        query = "SELECT distinct(b.district) FROM Booth" + tblname + " b inner join streamlist" + tblname + " s on b.id = s.schoolid WHERE b.Isdisplay=1 and b.district ='" + dist + "' and s.stateID='" + utype[1] + "' ORDER BY b.District ASC;";
                        //query = "SELECT distinct([acname]) FROM [Booth] WHERE Isdisplay=1 and district ='" + dist + "'  ORDER BY [acname] ASC;";
                    }
                }
                else
                {
                    dist = usertype.Split('_')[1];
                    query = "SELECT distinct(b.district) FROM " + table_prefix + "Booth b inner join " + table_prefix + "streamlist s on b.id = s.schoolid WHERE b.Isdisplay=1 and b.district = N'" + dist + "' and s.stateID='" + ph_stateid + "' ORDER BY b.District ASC;";
                    //query = "SELECT distinct([acname]) FROM [Booth] WHERE Isdisplay=1 and district ='" + dist + "'  ORDER BY [acname] ASC;";
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

        public DataSet GetPhaseList(string usertype)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string dist = string.Empty;
                query = "SELECT * from state where isdisplay=1";
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

        public DataSet GetBoothFromDistrict(string District, string usertype)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string utype = string.Empty;
                string tblname = string.Empty;
                if (usertype.Contains("?"))
                {
                    utype = usertype.Split('?')[1];
                    if (utype != ph_stateid)
                    {
                        tblname = "p" + utype;
                    }
                     //query = "SELECT distinct(b.acname),b.id FROM Booth" + tblname + " b inner join streamlist" + tblname + " s on b.id = s.schoolid WHERE b.Isdisplay=1 and b.district ='" + District + "' and s.stateID='" + utype + "' and b.id in (select min(id) from booth" + tblname + " where isdisplay=1 group by schoolname);";
                    query = "SELECT distinct(b.acname) FROM Booth" + tblname + " b WHERE b.Isdisplay=1 and b.district = N'" + District + "';";
                }
                else
                {
                    // query = "SELECT distinct(b.acname),b.id FROM Booth b inner join streamlist s on b.id = s.schoolid WHERE b.Isdisplay=1 and b.district ='" + District + "' and s.stateID='" + ph_stateid + "' and b.id in (select min(id) from booth where isdisplay=1 group by schoolname);";
                    query = "SELECT distinct(b.acname) FROM " + table_prefix + "Booth b WHERE b.Isdisplay=1 and b.district = N'" + District + "'";
                }
                if (usertype.StartsWith("sch"))
                {
                    int schoolid = Convert.ToInt32(usertype.Split('_')[2].ToString());
                    query = "SELECT distinct(b.schoolname),b.id FROM " + table_prefix + "Booth b inner join " + table_prefix + "streamlist s on b.id = s.schoolid WHERE b.Isdisplay=1 and b.district = N'" + District + "'  and b.id = " + schoolid + " and s.stateID='" + ph_stateid + "' ORDER BY b.schoolname ASC;";
                }

                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetBoothFromDistrict()--> " + ex.Message);
            }
            finally
            {
            }
            return ds;
        }
        public DataSet GetBoothFromDistrict(string District)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "SELECT distinct(b.acname),b.id FROM " + table_prefix + "Booth b inner join " + table_prefix + "streamlist s on b.id = s.schoolid WHERE b.Isdisplay=1 and b.district ='" + District + "' and s.stateID='" + ph_stateid + "' ORDER BY b.schoolname ASC;";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetBoothFromDistrict(1param)--> " + ex.Message);
            }
            finally
            {
            }
            return ds;
        }
        public string LiveCounter(string usertype)
        {
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string result = string.Empty;
                string query = string.Empty;
                string utype = usertype.Split('_')[1];

                query = "select COUNT(s.id) from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.id = s.schoolid where b.district=N'" + utype + "' and b.isdisplay='True' and s.IsEnable=1 and s.stateID='" + ph_stateid + "' and lastseen >= DateAdd(minute,-" + minute + ",CAST(SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30') as DATETIME))";

                SqlCommand command = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = command.ExecuteReader();
                if (dr.Read())
                {
                    result = dr.GetValue(0).ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conn.Close();
            }

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

        public int getliveconn()
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = query = "select minute from liveconn";
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
                SqlDataAdapter adp = new SqlDataAdapter(SQLcommand);
                DataTable dt = new DataTable();

                SQLconn.Open();
                adp.Fill(dt);
                SQLconn.Close();

                return Convert.ToInt32(dt.Rows[0]["minute"]);
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
    }
}