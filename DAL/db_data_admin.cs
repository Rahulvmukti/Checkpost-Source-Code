using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace exam.DAL
{
    public class db_data_admin
    {
 
        public string connstr = Common.DecodeConnectionstring(ConfigurationManager.ConnectionStrings["connectionstr"].ToString());
        public int minute = Convert.ToInt32(ConfigurationManager.AppSettings["minute"]);
        public string currentphase = ConfigurationManager.AppSettings["stateid"].ToString();
        public string uploadphase = ConfigurationManager.AppSettings["uploadstateid"].ToString();
        public string stcode = ConfigurationManager.AppSettings["stcode"].ToString();
        public string table_prefix = ConfigurationManager.AppSettings["tb_prefix"].ToString();
        string allKeyword = ConfigurationManager.AppSettings["AllSelectKeword"].ToString() + " "; 
        string districtname = ConfigurationManager.AppSettings["district"].ToString(); 


        public DataSet GetUser(string username, string password)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "SELECT * FROM " + table_prefix + "adminlogin WHERE admin_user=@username and admin_pwd=@password and IsEnable=1";
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
         

        public DataSet GetBoothList()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string tblname = string.Empty;
                if (currentphase != uploadphase)
                {
                    tblname = "p" + uploadphase;
                    query = "SELECT Distinct(b.id),b.district,b.acname,b.location,b.isdisplay,STUFF((SELECT ', ' + s.streamname FROM streamlist" + tblname + " s WHERE ( b.id = s.schoolid ) FOR XML PATH ('')),1,2,'') as streamnames From booth" + tblname + " b,streamlist" + tblname + " s1 where b.id = s1.schoolid and s1.stateID='" + uploadphase + "'";
                }
                else
                {
                    query = "SELECT Distinct(b.id),b.district,b.acname,b.location,b.isdisplay,STUFF((SELECT ', ' + s.streamname FROM streamlist s WHERE ( b.streamid = s.id ) FOR XML PATH ('')),1,2,'') as streamnames From " + table_prefix + "booth b," + table_prefix + "streamlist s where b.streamid = s.id and b.boothstateid='" + ConfigurationManager.AppSettings["stateid"].ToString() + "'";
                }
             
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("Get" + table_prefix + "boothList()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }

        
        public DataSet GetStreamDetailsByDID(string DID)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                SqlCommand SQLcommand = new SqlCommand("GetStreamDetailsByDID", SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.AddWithValue("@DID", DID);
                SqlDataAdapter adp = new SqlDataAdapter(SQLcommand);
                DataSet ds = new DataSet();
                SQLconn.Open();
                adp.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Common.Log("GetStreamDetailsByDID()--> " + ex.Message);
                return new DataSet();
            }
            finally
            {
                SQLconn.Close();
            }
        }

        public DataSet GetStreamlist()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string tblname = string.Empty;
                if (currentphase != uploadphase)
                {
                    tblname = "p" + uploadphase;
                    query = "SELECT * from streamlist" + tblname + " where stateID='" + uploadphase + "'";
                }
                else
                {
                    query = "SELECT * from " + table_prefix + "streamlist where stateID='" + ConfigurationManager.AppSettings["stateid"].ToString() + "'";
                }
                SqlCommand command = new SqlCommand(query, conn);

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetStreamlist()--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }

        public DataSet getuserlist(string username, string status, string dstsch)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                if (status == "loaduserslist")
                {
                    query = "select * from " + table_prefix + "users where usercode != 'admin' and usercode=@usercode";
                }
                else if (status == "chkuserslist")
                {
                    query = "select id,username from " + table_prefix + "users where username=@username";
                }

                SqlCommand command = new SqlCommand(query, conn);
                if (username != "")
                {
                    command.Parameters.AddWithValue("@username", username);
                }
                else
                {
                    command.Parameters.AddWithValue("@usercode", dstsch);
                }
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("getuserlist() - " + status + "--> " + ex.Message);
            }
            finally
            {

            }
            return ds;
        }

        public bool updateuserlist(int userid, string username, string password, int identifier)
        {
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;

                query = "update " + table_prefix + "users set username=@username,password=@password,identifier=@identifier where id=@id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@identifier", identifier);
                command.Parameters.AddWithValue("@id", userid);

                conn.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Common.Log("updateuserlist()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return true;
        }

        public bool adduser(string username, string password, string usercode, int stateid, int user_identifier)
        {
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                query = "insert into " + table_prefix + "users (username,password,usercode,isDvr,stateID,identifier) values (@username,@password,@usercode,@isDvr,@stateID,@identifier)";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@usercode", usercode);
                command.Parameters.AddWithValue("@isDvr", 0);
                command.Parameters.AddWithValue("@stateID", stateid);
                command.Parameters.AddWithValue("@identifier", user_identifier);

                conn.Open();
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Common.Log("adduser()--> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return true;
        }
        public int addAssemblyIfNotExist(string district, string acname, int st_id)
        {
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                   query = " IF NOT EXISTS (SELECT * FROM " + table_prefix + "district WHERE district=@district and acname=@acnum and stateid=@stateid) " +
                " BEGIN " +
                "     insert into " + table_prefix + "district (district,acname,isenable,stateid) output INSERTED.id values (@district,@acnum,'true',@stateid) " +
                " END  " +
                " ELSE SELECT 0";
                // "IF NOT EXISTS (SELECT * FROM district WHERE district = @district and acname=@acnum)   BEGIN insert into district (district,schoolname,isenable) output INSERTED.id values (@district,@acnum,@isenable) END";
               SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@district", district.ToUpper());
                command.Parameters.AddWithValue("@acnum", acname);
                command.Parameters.AddWithValue("@stateid", st_id);
                // command.Parameters.AddWithValue("@isenable", Boolean.TrueString);
                conn.Open();
                int id = Convert.ToInt32(command.ExecuteScalar());
                return id;

            }
            catch (Exception ex)
            {
                Common.Log("adduser()--> " + ex.Message);
                return 0;
            }
            finally
            {
                conn.Close();
            }

        }
        
        public DataSet GetDistrictList(string user_type, int st_id)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                if (user_type == "admin")
                {
                    query = "SELECT distinct(district), district as SelValue  FROM " + table_prefix + "district where stateid=@stateid ORDER BY District ASC;";
                }
                else
                {
                    if (user_type.Contains("dst_"))
                    {
                        user_type = user_type.Replace("dst_", "");
                    }
                    query = "SELECT distinct(district),district as SelValue  FROM " + table_prefix + "district where district=N'" + user_type + "' and stateid=@stateid ORDER BY District ASC;";
                }
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@stateid", st_id);
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

        public DataSet GetPCList(string district)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                 query = "SELECT distinct(b.accode),b.accode,b.acname,b.accode SelValue  FROM " + table_prefix + "district b where b.district=@pc and stateid=" + currentphase + " ORDER BY b.accode ASC;";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@pc", district);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetPCList()--> " + ex.Message);
            }
            finally
            {
            }
            return ds;
        }

        public DataSet GetACList(string district, string pcname)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                if (string.IsNullOrEmpty(pcname))
                {
                    query = "SELECT distinct(b.acname),b.acname SelValue FROM " + table_prefix + "district b where b.district=@district and stateid="+currentphase+ " ORDER BY b.acname ASC;";
                }
                else
                {
                    query = "SELECT distinct(b.acname),b.acname SelValue FROM " + table_prefix + "district b where b.district=@district and b.accode=@pc and stateid=" + currentphase + " ORDER BY b.acname ASC;";
                }
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@district", district);
                if (!string.IsNullOrEmpty(pcname))
                {
                    command.Parameters.AddWithValue("@pc", pcname);
                }
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetACList()--> " + ex.Message);
            }
            finally
            {
            }
            return ds;
        }
        
        public DataSet LiveCounter(string usertype)
        {
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string result = string.Empty;
                string query = string.Empty;
                query = "select b.district,count(b.streamid) as lastseen from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid = s.id where b.isdisplay='True' and s.IsEnable=1 and b.boothstateid=" + currentphase + " and lastseen >= DateAdd(minute,-" + ConfigurationManager.AppSettings["minute"] + ",CAST(SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30') as DATETIME)) group by b.district";
                DateTime chklastseen = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minute"]) * -1);
                string chklastseenString = chklastseen.ToString("yyyy-MM-dd HH:mm:ss");
                query = "select MAX(b.district) as district,b.district as DATA,Cast(COUNT(b.acname) as nvarchar(MAX)) as ac, COUNT(CASE WHEN lastseen >= '" + chklastseenString + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live', (count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseenString + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth,COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce' from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and b.boothstateid=" + currentphase + " group by district";
                if (usertype.Contains("dst_"))
                {
                    query = "select b.acname,count(b.streamid) as lastseen from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid = s.id where b.isdisplay='True' and s.IsEnable=1 and b.boothstateid=" + currentphase + " and b.district=N'" + usertype.Split('_')[1] + "' and lastseen >= DateAdd(minute,-" + ConfigurationManager.AppSettings["minute"] + ",CAST(SWITCHOFFSET(SYSDATETIMEOFFSET(), '+05:30') as DATETIME)) group by b.acname";
                }
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter adp = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                conn.Open();
                adp.Fill(ds);

                return ds;
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
        public DataSet allcountlive(string usertype)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                //sDate = dDate.ToString("MM/dd/yyyy hh:mm:ss tt");
                //     DateTime utcdate = DateTime.ParseExact(DateTime.Now.ToString("M/dd/yyyy h:mm:ss tt"), "M/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                //        var istdate = TimeZoneInfo.ConvertTimeFromUtc(utcdate, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                DateTime chklastseendt = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minute"]) * -1);
                DateTime chklastseendefaultdt = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minutedefault"]) * -1);
                string chklastseen = chklastseendt.ToString("MM/dd/yyyy HH:mm:ss");

                string chklastseendefault = chklastseendefaultdt.ToString("MM/dd/yyyy HH:mm:ss");
                //if (ConfigurationManager.AppSettings["envirnment"].ToString().ToLower() == "live")
                //{
                //     chklastseen = TimeZoneInfo.ConvertTimeFromUtc(DateTime.ParseExact(chklastseendt.ToString("M/dd/yyyy h:mm:ss tt"), "M/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("MM/dd/yyyy HH:mm:ss");

                //     chklastseendefault = TimeZoneInfo.ConvertTimeFromUtc(DateTime.ParseExact(chklastseendefaultdt.ToString("M/dd/yyyy h:mm:ss tt"), "M/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).ToString("MM/dd/yyyy HH:mm:ss");
                //}




                //DateTime utcdate = DateTime.ParseExact(DateTime.Now.ToString("M/dd/yyyy h:mm:ss tt"), "M/dd/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                //var istdate = TimeZoneInfo.ConvertTimeFromUtc(utcdate, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                //        string chklastseenString = chklastseen.ToString("yyyy-MM-dd HH:mm:ss");
                /* add "- b.acname" in data */
                //  query = "select MAX(b.district) as district,b.accode+'-'+b.acname as DATA,Cast(COUNT(b.schoolname) as nvarchar(MAX)) as ac,SUBSTRING(b.accode, 0, CHARINDEX('-',b.accode)) as odr, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live', (count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and b.boothstateid=" + currentphase + " group by b.accode,schoolname order by len(SUBSTRING(b.accode, 0, CHARINDEX('-',b.accode))),odr";
                query = "select  COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce',MAX(b.district) as district,b.district as DATA,Cast(COUNT(b.acname) as nvarchar(MAX)) as ac, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live',COUNT(CASE WHEN lastseen >= '" + chklastseendefault + "' OR statusFlag=2 then 1 ELSE NULL END) as 'lastLive', (count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and ISNULL(b.isdelete,'')=0 and b.boothstateid=" + currentphase + " group by district";

                if (usertype.Contains("dst_"))
                {
                    /* add "- b.acname" in data */
                    query = "select  COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce',MAX(b.district) as district,b.accode+'-'+b.acname as DATA,Cast(COUNT(b.acname) as nvarchar(MAX)) as ac,SUBSTRING(b.accode, 0, CHARINDEX('-',b.accode)) as odr, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live',COUNT(CASE WHEN lastseen >= '" + chklastseendefault + "' OR statusFlag=2 then 1 ELSE NULL END) as 'lastLive', (count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and ISNULL(b.isdelete,'')=0 and b.boothstateid=" + currentphase + " and b.district=N'" + usertype.Split('_')[1] + "' group by b.accode,b.acname order by len(SUBSTRING(b.accode, 0, CHARINDEX('-',b.accode))),odr";
                    if (usertype.Split('_')[1] == allKeyword + districtname)
                    {
                        query = "select  COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce',MAX(b.district) as district,b.accode+'-'+b.acname as DATA,Cast(COUNT(b.acname) as nvarchar(MAX)) as ac,SUBSTRING(b.accode, 0, CHARINDEX('-',b.accode)) as odr, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live',COUNT(CASE WHEN lastseen >= '" + chklastseendefault + "' OR statusFlag=2 then 1 ELSE NULL END) as 'lastLive', (count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and ISNULL(b.isdelete,'')=0 and b.boothstateid=" + currentphase + " group by b.accode,b.acname order by cast(accode as int)";
                    }
                }
                if (usertype.Contains("pc_"))
                {
                    query = "select  COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce',MAX(b.district) as district,MAX(b.accode) as ac,b.acname as DATA, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live',COUNT(CASE WHEN lastseen >= '" + chklastseendefault + "' OR statusFlag=2 then 1 ELSE NULL END) as 'lastLive', (count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and ISNULL(b.isdelete,'')=0 and b.boothstateid=" + currentphase + " and b.district=N'" + usertype.Split('_')[1] + "' and b.accode=N'" + usertype.Split('_')[2] + "' group by b.acname";
                    if (usertype.Split('_')[1] == allKeyword + districtname)
                    {
                        query = "select COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce', MAX(b.district) as district,MAX(b.accode) as ac,b.acname as DATA, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live',COUNT(CASE WHEN lastseen >= '" + chklastseendefault + "' OR statusFlag=2 then 1 ELSE NULL END) as 'lastLive',(count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and ISNULL(b.isdelete,'')=0 and b.boothstateid=" + currentphase + " and b.accode=N'" + usertype.Split('_')[2] + "' group by b.acname";
                    }

                }
                if (usertype.Contains("pc1_"))
                {
                    query = "select  COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce',MAX(b.district) as district,MAX(b.accode) as ac,b.acname as DATA, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live', COUNT(CASE WHEN lastseen >= '" + chklastseendefault + "' OR statusFlag=2 then 1 ELSE NULL END) as 'lastLive',(count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and ISNULL(b.isdelete,'')=0 and b.boothstateid=" + currentphase + " and b.accode=N'" + usertype.Split('_')[2] + "' group by b.acname";
                    if (usertype.Split('_')[1] == allKeyword + districtname)
                    {
                        query = "select COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce', MAX(b.district) as district,MAX(b.accode) as ac,b.acname as DATA, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live',COUNT(CASE WHEN lastseen >= '" + chklastseendefault + "' OR statusFlag=2 then 1 ELSE NULL END) as 'lastLive', (count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and ISNULL(b.isdelete,'')=0 and b.boothstateid=" + currentphase + " and b.accode=N'" + usertype.Split('_')[2] + "' group by b.acname";
                    }
                }
                if (usertype.Contains("sch1_"))
                {
                    query = "select COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce', MAX(b.district) as district,MAX(b.accode) as ac,b.acname as DATA, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live',COUNT(CASE WHEN lastseen >= '" + chklastseendefault + "' OR statusFlag=2 then 1 ELSE NULL END) as 'lastLive', (count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and ISNULL(b.isdelete,'')=0 and b.boothstateid=" + currentphase + " and b.accode=N'" + usertype.Split('_')[2] + "' and b.acname=N'" + usertype.Split('_')[3] + "' group by b.acname";
                }
                if (usertype.Contains("sch_"))
                {
                    if (usertype.Split('_')[1] == allKeyword + districtname)
                    {
                        query = "select  COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce',MAX(b.district) as district,MAX(b.accode) as ac,b.acname as DATA, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live', COUNT(CASE WHEN lastseen >= '" + chklastseendefault + "' OR statusFlag=2 then 1 ELSE NULL END) as 'lastLive',(count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and ISNULL(b.isdelete,'')=0 and b.boothstateid=" + currentphase + " and b.accode=N'" + usertype.Split('_')[2] + "' and b.acname=N'" + usertype.Split('_')[3] + "' group by b.acname";

                    }
                    else
                    {
                        query = "select  COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce', MAX(b.district) as district,MAX(b.accode) as ac,b.acname as DATA, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live', COUNT(CASE WHEN lastseen >= '" + chklastseendefault + "' OR statusFlag=2 then 1 ELSE NULL END) as 'lastLive',COUNT(CASE WHEN lastseen < '" + chklastseen + "' then 1 ELSE NULL END) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and ISNULL(b.isdelete,'')=0 and b.boothstateid=" + currentphase + " and b.district=N'" + usertype.Split('_')[1] + "' and b.accode=N'" + usertype.Split('_')[2] + "' and b.acname=N'" + usertype.Split('_')[3] + "' group by b.acname";

                    }
                }
                if (usertype.Contains("psbooth_"))
                {
                    query = "select  COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce',MAX(b.district) as district,MAX(b.accode) as ac,MAX(b.acname) as ac2,b.location as DATA, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live',COUNT(CASE WHEN lastseen >= '" + chklastseendefault + "' OR statusFlag=2 then 1 ELSE NULL END) as 'lastLive', (count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and ISNULL(b.isdelete,'')=0 and b.boothstateid=" + currentphase + " and b.district=N'" + usertype.Split('_')[1] + "' and b.accode=N'" + usertype.Split('_')[2] + "' and b.acname=N'" + usertype.Split('_')[3] + "' and b.psnum=N'" + usertype.Split('_')[4] + "' group by b.location";
                    if (usertype.Split('_')[1] == allKeyword + districtname)
                    {
                        query = "select  COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce',MAX(b.district) as district,MAX(b.accode) as ac,MAX(b.acname) as ac2,b.location as DATA, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live',COUNT(CASE WHEN lastseen >= '" + chklastseendefault + "' OR statusFlag=2 then 1 ELSE NULL END) as 'lastLive', (count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and ISNULL(b.isdelete,'')=0 and b.boothstateid=" + currentphase + " and b.accode=N'" + usertype.Split('_')[2] + "' and b.acname=N'" + usertype.Split('_')[3] + "' and b.psnum=N'" + usertype.Split('_')[4] + "' group by b.location";

                    }
                }
                if (usertype == allKeyword + districtname)
                {
                    query = "select  COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce', b.district as DATA, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live', COUNT(CASE WHEN lastseen >= '" + chklastseendefault + "' OR statusFlag=2 then 1 ELSE NULL END) as 'lastLive',(count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and ISNULL(b.isdelete,'')=0 and b.boothstateid=" + currentphase + " group by b.district";
                }
                if (usertype.StartsWith("SELDistrict_"))
                {
                    query = "select  COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce',b.district as DATA, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live',COUNT(CASE WHEN lastseen >= '" + chklastseendefault + "' OR statusFlag=2 then 1 ELSE NULL END) as 'lastLive', (count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and ISNULL(b.isdelete,'')=0 and b.boothstateid=" + currentphase + " and b.district=N'" + usertype.Split('_')[1] + "' group by b.district";
                }
                if (usertype == "ALL AC Name")
                {
                    query = "select  COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce',MAX(b.district) as district,MAX(b.accode) as pc, b.acname as DATA, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live',COUNT(CASE WHEN lastseen >= '" + chklastseendefault + "' OR statusFlag=2 then 1 ELSE NULL END) as 'lastLive', (count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and ISNULL(b.isdelete,'')=0 and b.boothstateid=" + currentphase + " group by b.acname";

                }
                if (usertype.StartsWith("ACFROMDISTRICT_"))
                {
                    query = "select  COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce', MAX(b.district) as district,MAX(b.accode) as pc, b.acname as DATA, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live',COUNT(CASE WHEN lastseen >= '" + chklastseendefault + "' OR statusFlag=2 then 1 ELSE NULL END) as 'lastLive', (count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and ISNULL(b.isdelete,'')=0 and b.boothstateid=" + currentphase + " and b.district=N'" + usertype.Split('_')[1] + "' group by b.acname";
                    if (usertype.Split('_')[1] == allKeyword + districtname)
                    {
                        query = "select  COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce', MAX(b.district) as district,MAX(b.accode) as pc, b.acname as DATA, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live',COUNT(CASE WHEN lastseen >= '" + chklastseendefault + "' OR statusFlag=2 then 1 ELSE NULL END) as 'lastLive', (count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and ISNULL(b.isdelete,'')=0 and b.boothstateid=" + currentphase + " group by b.acname";
                    }


                }
                if (usertype.StartsWith("ALLBOOTH_"))
                {
                    query = "select  COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce',district,accode,acname,psnum,location,streamname,status,islive from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and ISNULL(b.isdelete,'')=0 and b.boothstateid=" + currentphase + "";

                }
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                conn.Open();
                da.Fill(ds);
                ds = FilterDataByAccess(ds, true, false);
            }
            catch (Exception ex)
            {
                Common.Log("allcountlive()==>> " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        public DataSet allcountlivenew(string usertype, int UserID, string cameraType = "", int pink = -1)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                DateTime chklastseendt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minute"]) * -1);
                DateTime chklastseendefaultdt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minutedefault"]) * -1);

                string query = string.Empty;
                query = "GetAllLiveCount";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@lastseen", chklastseendt);
                command.Parameters.AddWithValue("@lastseendefault", chklastseendefaultdt);
                command.Parameters.AddWithValue("@UserID", UserID);
                command.Parameters.AddWithValue("@cameratype", cameraType);
                command.Parameters.AddWithValue("@Ispink", pink);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                //ds = FilterDataByAccess(ds, true, false);
            }
            catch (Exception ex)
            {
                Common.Log("allcountlivenew()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }

        
        public DataSet allcountlive_assembly(string usertype)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                DateTime chklastseen = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minute"]) * -1);
                DateTime chklastseendefault = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minutedefault"]) * -1);
                /* add "- b.acname" in data */
                //  query = "select MAX(b.district) as district,b.accode+'-'+b.acname as DATA,Cast(COUNT(b.acname) as nvarchar(MAX)) as ac,SUBSTRING(b.accode, 0, CHARINDEX('-',b.accode)) as odr, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live', (count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and b.boothstateid=" + currentphase + " group by b.accode,acname order by len(SUBSTRING(b.accode, 0, CHARINDEX('-',b.accode))),odr";
                query = "select COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce',MAX(b.district) as district,b.district as DATA,Cast(COUNT(b.acname) as nvarchar(MAX)) as ac,SUBSTRING(b.accode, 0, CHARINDEX('-',b.accode)) as odr, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live',COUNT(CASE WHEN lastseen >= '" + chklastseendefault + "' OR statusFlag=2 then 1 ELSE NULL END) as 'lastLive', (count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and ISNULL(b.isdelete,'')=0 and b.boothstateid=" + currentphase + " group by district,pc order by len(SUBSTRING(b.accode, 0, CHARINDEX('-',b.accode))),odr";


                if (usertype.Contains("pc_"))
                {
                    query = "select COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce', MAX(b.district) as district,MAX(b.accode) as ac,b.acname as DATA, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live',COUNT(CASE WHEN lastseen >= '" + chklastseendefault + "' OR statusFlag=2 then 1 ELSE NULL END) as 'lastLive', (count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and ISNULL(b.isdelete,'')=0 and b.boothstateid=" + currentphase + " and b.district=N'" + usertype.Split('_')[1] + "' and b.accode=N'" + usertype.Split('_')[2] + "' group by b.acname";
                    if (usertype.Split('_')[1] == allKeyword + districtname)
                    {
                        query = "select COUNT(CASE s.isLive WHEN 'True' then 1 ELSE NULL END) as 'connectedonce',MAX(b.district) as district,MAX(b.accode) as ac,b.acname as DATA, COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END) as 'Live',COUNT(CASE WHEN lastseen >= '" + chklastseendefault + "' OR statusFlag=2 then 1 ELSE NULL END) as 'lastLive',(count(DISTINCT b.id)-COUNT(CASE WHEN lastseen >= '" + chklastseen + "' OR statusFlag=2 then 1 ELSE NULL END)) as 'stop', count(DISTINCT b.id) as TotalBooth from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid=s.id where b.isdisplay='True' and s.IsEnable=1 and ISNULL(b.isdelete,'')=0 and b.boothstateid=" + currentphase + " and b.district=N'" + usertype.Split('_')[2] + "' group by b.acname";
                    }

                }
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                conn.Open();
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
        public DataSet allcountlive_assemblynew(string district, int UserID, string cameraType = "", int pink = -1)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                DateTime chklastseendt = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minute"]) * -1);
                DateTime chklastseendefaultdt = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minutedefault"]) * -1);

                string query = string.Empty;
                query = "GetAllLiveCountDetails";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@lastseen", chklastseendt);
                command.Parameters.AddWithValue("@lastseendefault", chklastseendefaultdt);
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@UserID", UserID);
                command.Parameters.AddWithValue("@cameratype", cameraType);
                command.Parameters.AddWithValue("@Ispink", pink);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
             //   ds = FilterDataByAccess(ds, false, true);
            }
            catch (Exception ex)
            {
                Common.Log("allcountlive_assemblynew()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
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

        public DataSet allcountliveondashboard(int UserID)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                DateTime chklastseendt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minute"]) * -1); 

                string query = string.Empty;
                query = "GetDashbordAllLiveCount";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@lastseen", chklastseendt); 
                command.Parameters.AddWithValue("@UserID", UserID); 
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds); 
            }
            catch (Exception ex)
            {
                Common.Log("allcountlivenew()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
       
        public DataSet allcountlive_assemblyneworderby(string district, int UserID, string orderby, string cameraType = "", int pink = -1)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                DateTime chklastseendt = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minute"]) * -1);
                DateTime chklastseendefaultdt = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minutedefault"]) * -1);

                string query = string.Empty;
                query = "GetAllLiveCountDetailsorderby";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@lastseen", chklastseendt);
                command.Parameters.AddWithValue("@lastseendefault", chklastseendefaultdt);
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@UserID", UserID);
                command.Parameters.AddWithValue("@cameratype", cameraType);
                command.Parameters.AddWithValue("@Ispink", pink);
                command.Parameters.AddWithValue("@orderby", orderby);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                //   ds = FilterDataByAccess(ds, false, true);
            }
            catch (Exception ex)
            {
                Common.Log("allcountlive_assemblynew()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }


      ///all installationcountbyzone
        public DataSet alllivecountinstallation()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                DateTime chklastseendt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minute"]) * -1);
                DateTime chklastseendefaultdt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minutedefault"]) * -1);

                string query = string.Empty;
                query = "GetInstallationSummaryByLocation";
                SqlCommand command = new SqlCommand(query, conn);

                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                //ds = FilterDataByAccess(ds, true, false);
            }
            catch (Exception ex)
            {
                Common.Log("allcountlivenew()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        //installtioncountbydistrict
        public DataSet alllivecountinstallationdistict(string zone)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                DateTime chklastseendt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minute"]) * -1);
                DateTime chklastseendefaultdt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minutedefault"]) * -1);

                string query = string.Empty;
                query = "GetInstallationSummaryBydistrict";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@zone", zone);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                //ds = FilterDataByAccess(ds, true, false);
            }
            catch (Exception ex)
            {
                Common.Log("allcountlivenew()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet alllivecountinstallationdistictandzone(string zone, string district, string flag)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                DateTime chklastseendt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minute"]) * -1);
                DateTime chklastseendefaultdt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minutedefault"]) * -1);

                string query = string.Empty;
                query = "GetInstallationSummaryBydistrict";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@zone", zone);
                command.Parameters.AddWithValue("@district", district);
                command.Parameters.AddWithValue("@flag", flag);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                //ds = FilterDataByAccess(ds, true, false);
            }
            catch (Exception ex)
            {
                Common.Log("allcountlivenew()--> " + ex.Message);
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
    }
}
        
