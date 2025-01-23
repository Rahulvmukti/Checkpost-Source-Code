using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace exam.BAL
{
    public class DistrictBAL
    {
        DAL.DistrictDAL districtdal = new DAL.DistrictDAL();
        public DataSet GetDistrict(int stateid)
        {
            DataSet dsDevices = districtdal.GetDistrict(stateid);
            return dsDevices;
        }
        public string ValidateExcelData(string DT_NAME, string AC_NAME, string AC_NO)
        {
            try
            {
                string cameraid = string.Empty;
                //if (DT_NAME.Contains(" "))
                //{
                //    return "Error: DT_NAME contains space.";
                //}
                //else
                //{
                    string DistMessage = districtdal.IsDistrictExist(DT_NAME, AC_NAME, AC_NO);
                    if (DistMessage.Contains("Error"))
                    {
                        return DistMessage;
                    }
                //}
                return "Success";

            }
            catch (Exception ex)
            {
                Log.logmessage("ValidateExcelData()", ex.Message.ToString());
                return "Fail"; //fail
            }
        }
        public bool deleteDistrict( int id)
        {
            try
            {
               return  districtdal.deleteDistrict( id);
                
            }
            catch (Exception ex)
            {
                Log.logmessage("deleteDistrict()", ex.Message.ToString());
                return false; //fail
            }
        }
        public DataSet getUniqueDistricts()
        {
            try
            {
                return districtdal.getUniqueDistricts();

            }
            catch (Exception ex)
            {
                Log.logmessage("getUniqueDistricts()", ex.Message.ToString());
                return null; //fail
            }
        }
        public string addDistrict(string DT_NAME, string AC_NAME, string AC_NO, int id)
        {
            try
            {
                string cameraid = string.Empty;
                string result = "Success";
                if (id <= 0)
                {
                     result = ValidateExcelData(DT_NAME, AC_NAME, AC_NO);
                }
                if (result.Contains("Error"))
                {
                    return result;
                }
                else
                {
                    //upload to DB
                    districtdal.addDistrict(DT_NAME, AC_NAME, AC_NO, id);

                    return "Success";
                }
            }
            catch (Exception ex)
            {
                Log.logmessage("addDistrict()", ex.Message.ToString());
                return "Fail"; //fail
            }
        }
    }
}