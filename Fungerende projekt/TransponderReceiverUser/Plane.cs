using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace TransponderReceiverUser
{
    public class Plane
    {
        public TrackData Data { get; set; }

        public Plane(TrackData initialData)
        {
            Data = initialData;
        }

        public bool isToCloseTo(Plane plane)
        {
            return Math.Abs(plane.Data.XCoordinate - Data.XCoordinate) < 55000
                   && Math.Abs(plane.Data.YCoordinate - Data.YCoordinate) < 55000
                   && Math.Abs(plane.Data.Altitude - Data.Altitude) < 5300;
        }

        public override string ToString()
        {
            return string.Format("{0}: X: {1}m, Y: {2}m, A: {3}m, {4}m/s, Course: {5}deg", Data.Tag, Data.XCoordinate, Data.YCoordinate, Data.Altitude, Math.Round(Data.Velocity, 2), Math.Round(Data.CompassCourse, 2));
        }
    }
}
