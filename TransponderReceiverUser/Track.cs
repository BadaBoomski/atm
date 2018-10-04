using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace TransponderReceiverUser
{
    public class Track
    {
        public string Tag { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public int Altitude { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Velocity { get; set; }
        public double CompassCourse { get; set; }

        // Converting the given data from the .dll into useful data.
        public Track ConvertToTrackData(string rawData)
        {
            Track tempTrack = new Track();

            string[] delimiters = { ";" };
            string[] subStrings = rawData.Split(delimiters, StringSplitOptions.None);

            tempTrack.Tag = subStrings[0];
            tempTrack.XCoordinate = Int32.Parse(subStrings[1]);
            tempTrack.YCoordinate = Int32.Parse(subStrings[2]);
            tempTrack.Altitude = Int32.Parse(subStrings[3]);
            tempTrack.TimeStamp = DateTime.ParseExact(subStrings[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
            tempTrack.Velocity = 0;
            tempTrack.CompassCourse = 0;

            return tempTrack;
        }

    }
}
