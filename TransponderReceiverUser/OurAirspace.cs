using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransponderReceiverUser
{
    class OurAirspace : IOurAirspace
    {
        public int SouthWestCornerX { get; set; }
        public int SoutWestCornerY { get; set; }
        public int NorthEastCornerX { get; set; }
        public int NorthEastCornerY { get; set; }
        public int LowerAltitude { get; set; }
        public int UpperAltitude { get; set; }

        public OurAirspace()
        {
            SouthWestCornerX = 10000;
            SoutWestCornerY = 10000;
            NorthEastCornerX = 90000;
            NorthEastCornerY = 90000;
            LowerAltitude = 500;
            UpperAltitude = 20000;
        }

        public bool isAirplaneInOurAirspace(Track track)
        {
            if (track.XCoordinate >= SouthWestCornerX && track.XCoordinate <= NorthEastCornerX &&
                track.YCoordinate >= SoutWestCornerY && track.YCoordinate <= NorthEastCornerY &&
                track.Altitude >= LowerAltitude && track.Altitude <= UpperAltitude)
            {
                return true;
            }
            return false;
        }

        public void cleanUp(Track track) // remove tracks from output. Secondary.
        {
        }
    }
}
