using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace TransponderReceiverUser
{
    public class Warnings : IWarnings
    {
        public List<Plane> planeList { get; set; }
        public List<Tuple<Plane, Plane>> planesInDanger { get; set; }

        public Warnings()
        {
            planeList = new List<Plane>();
            planesInDanger = new List<Tuple<Plane, Plane>>();
        }

        public bool insideAirspace(TrackData data)
        {
            return data.YCoordinate >= 10000
                   && data.YCoordinate < 90000
                   && data.XCoordinate >= 10000
                   && data.XCoordinate < 90000
                   && data.Altitude >= 500
                   && data.Altitude < 20000;
        }

        public void ProcessTrackData(TrackData trackData)
        {
            if (planeList.Exists(p => p.Data.Tag == trackData.Tag))
            {
                // update track data.
                if (insideAirspace(trackData))
                {
                    planeList.Find(p => p.Data.Tag == trackData.Tag).Data.Update(trackData);
                }
                else
                {
                    planeList.RemoveAll(p => p.Data.Tag == trackData.Tag);
                }
            }
            else if (insideAirspace(trackData))
            {
                planeList.Add(new Plane(trackData));
            }
        }

        public void PlanesAreTooDamnClose(Plane plane)
        {
            planesInDanger = planeList.Where(item => item.isToCloseTo(plane) && item.Data.Tag != plane.Data.Tag).Select(item => new Tuple<Plane, Plane>(item, plane)).ToList();
        }
    }
  


}




