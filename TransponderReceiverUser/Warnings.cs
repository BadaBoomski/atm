using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace TransponderReceiverUser
{
    public class Warnings : IWarnings
    {
        public List<Track> isInList { get; set; }
        public List<Track> isInConflict;

        public Warnings()
        {
            isInList = new List<Track>();
        }

        public bool checkY(Track plane)
        {
            if (10000 < plane.YCoordinate && plane.YCoordinate > 90000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkZ(Track plane)
        {
            if (500 < plane.Altitude && plane.Altitude > 20000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkX(Track plane)
        {
            if (10000 < plane.XCoordinate && plane.XCoordinate > 90000)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void addPlane(Track plane)
        {
            if (checkX(plane) && checkY(plane) && checkZ(plane) )
            {
                isInList.Add(plane);
            }
        }

        public void removePlaneIfOutOfAirspace(Track plane)
        {

        }

        // sort planes only in airspace.
        public bool PlanesInOurList(Track plane)
        {
            var existsInList = false;
            lock (isInList)
            {
                for (int i = 0; i < isInList.Count; i++)
                {
                    if (isInList[i].Tag == plane.Tag)
                    {
                        existsInList = true;
                        isInList[i] = plane;
                      
                        break;
                    }
                }
                return existsInList;
            }
        }

        public bool PlanesAreTooDamnClose(Track planes)
        {
            lock (isInConflict)
            {
                isInConflict.Clear();

                lock (isInList)
                {
                    foreach (var item in isInList.ToList())
                        if (planes.Tag != item.Tag)
                        {
                            //foreach (var item in isInList.ToList().Where(item => item.Tag != data.Tag))
                            {
                                if (Math.Abs((planes.XCoordinate - item.XCoordinate)) < 100000 &&
                                    Math.Abs((planes.YCoordinate - item.YCoordinate)) < 100000 &&
                                    Math.Abs((planes.Altitude - item.Altitude)) < 200000)
                                {
                                    isInConflict.Add(item);
                                    Console.WriteLine("WARNING!! {0} and {1} are TOO DAMN CLOSE!!", planes.Tag,
                                        item.Tag);
                                }
                            }
                        }
                }
            }

            return isInConflict.Count >= 1;
        }
    }
  


}




