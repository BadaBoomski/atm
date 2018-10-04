using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransponderReceiverUser
{
    public class OurAirspace : IOurAirspace, IWarnings
    {
        public int SouthWestCornerX { get; set; }
        public int SoutWestCornerY { get; set; }
        public int NorthEastCornerX { get; set; }
        public int NorthEastCornerY { get; set; }
        public int LowerAltitude { get; set; }
        public int UpperAltitude { get; set; }

        public List<Track> isInList { get; set;}

        public OurAirspace()
        {
            SouthWestCornerX = 10000;
            SoutWestCornerY = 10000;
            NorthEastCornerX = 90000;
            NorthEastCornerY = 90000;
            LowerAltitude = 500;
            UpperAltitude = 20000;
        }

        // The class will check wether the planes are inside the airspace or not
        public bool isAirplaneInOurAirspace(Track track)
        {
            if (track.XCoordinate >= SouthWestCornerX && track.XCoordinate <= NorthEastCornerX &&
                track.YCoordinate >= SoutWestCornerY && track.YCoordinate <= NorthEastCornerY &&
                track.Altitude >= LowerAltitude && track.Altitude <= UpperAltitude)
            {
                Console.WriteLine("Added plane to list.."); // Just for debugging..
                isInList.Add(track);
                return true;
            }

            Console.WriteLine("Removed plane from list.."); // Just for debugging..
            isInList.Remove(track);
            return false;
        }

        // Trying to create a method, that removes/cleans the output (secondary!)
        public void cleanUp(Track track)
        {
        }
    }
}
