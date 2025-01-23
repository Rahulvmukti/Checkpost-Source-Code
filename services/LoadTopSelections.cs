using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using exam.DAL;
namespace exam.services
{
    public class LoadTopSelections
    {
        db_data _boothlist = new db_data();
        public string pcname = ConfigurationManager.AppSettings["pcname"].ToString();
        public string assemblyname = ConfigurationManager.AppSettings["assemblyname"].ToString();
        public string district = ConfigurationManager.AppSettings["district"].ToString();
        public string allKeyword = ConfigurationManager.AppSettings["AllSelectKeword"].ToString() + " ";
        public void LoadDistrict(string usertype, string utypeall, int stateid, out DataSet ds_district)
        {
            try
            {
                DataSet ds_dist;
                if (utypeall.StartsWith("eci"))
                {
                    ds_dist = _boothlist.GetDistrictListECI(usertype, stateid);
                }
                else
                {
                    ds_dist = _boothlist.GetDistrictList(usertype, stateid);
                }
                //if (usertype.StartsWith("dst") == false)
                //{
                //    DataRow dr = ds_dist.Tables[0].NewRow();
                //    dr[0] = allKeyword + district;
                //    dr[1] = "0";
                //    ds_dist.Tables[0].Rows.InsertAt(dr, 0);
                //}
                ds_district = ds_dist;

            }
            catch (Exception ex)
            {
                ds_district = null;
                Common.Log("LoadDistrict_list() -- >  " + ex.Message);
            }
        }
        

      

    }
}