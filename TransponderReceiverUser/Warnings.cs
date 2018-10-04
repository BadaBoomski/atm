using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
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

        // sort planes only in airspace.
        public bool PlanesInOurList(Track planes)
        {
            var existsInList = false;
            lock (isInList)
            {
                for (int i = 0; i < isInList.Count; i++)
                {
                    if (isInList[i].Tag == planes.Tag)
                    {
                        existsInList = true;
                        isInList[i].XCoordinate = planes.XCoordinate;
                        isInList[i].YCoordinate = planes.YCoordinate;
                        isInList[i].Altitude = planes.Altitude;
                        isInList[i].TimeStamp = planes.TimeStamp;
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
