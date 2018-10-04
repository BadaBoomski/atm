using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransponderReceiverUser
{
    public class TrackData
    {
        public string Tag { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int Altitude { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Velocity { get; set; }
        public double CompassCourse { get; set; }

        public TrackData(string rawData)
        {
            string[] delimiters = { ";" };
            string[] subStrings = rawData.Split(delimiters, StringSplitOptions.None);

            Tag = subStrings[0];
            XCoordinate = Int32.Parse(subStrings[1]);
            YCoordinate = Int32.Parse(subStrings[2]);
            Altitude = Int32.Parse(subStrings[3]);
            TimeStamp = DateTime.ParseExact(subStrings[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
            Velocity = 0;
            CompassCourse = 0;
        }

        public void Update(TrackData newData)
        {
            var deltaX = newData.XCoordinate - XCoordinate;
            var deltaY = newData.YCoordinate - YCoordinate;
            Velocity = Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
            CompassCourse = Math.Atan2(deltaY, deltaX) * 180 / Math.PI;
            XCoordinate = newData.XCoordinate;
            YCoordinate = newData.YCoordinate;
            Altitude = newData.Altitude;
            TimeStamp = newData.TimeStamp;
        }
    }
}
