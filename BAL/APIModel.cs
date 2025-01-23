using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace exam.BAL
{
    public class APIModel
    {
    }
    public class GPSAPIResponse
    {
        public string status { get; set; }
        public VehicleDetails[] data { get; set; }
        public string message { get; set; }
    }

    public class VehicleDetails
    {
        public int deviceId { get; set; }
        public string name { get; set; }
        public string deviceImei { get; set; }
        public string status { get; set; }
        public DateTime? lastUpdate { get; set; }
        public long posId { get; set; }
        public string phone { get; set; }
        public string type { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public DateTime? deviceFixTime { get; set; }
        public string speed { get; set; }
        public string course { get; set; }
        public string ignition { get; set; }
        public string totalDistance { get; set; }
        public string alarm { get; set; }
    }


    public class CameraRecordingList
    {
        public CameraRecordingModel[] model { get; set; }
    }

    public class CameraRecordingModel
    {
        public string camera_name { get; set; }
        public string recording_date { get; set; }
        public string recording_endtime { get; set; }
        public string recording_name { get; set; }
        public float recording_size { get; set; }
        public string recording_startingtime { get; set; }
        public string recording_url { get; set; }
    }

}