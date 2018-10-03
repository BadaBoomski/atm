using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransponderReceiverUser
{
    public class Warnings : IWarnings
    {
        public List<Track>isInList { get; set; }
        private bool PlanesInOurList(Track planes)
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

        private bool PlanesAreTooDamnClose(Track data)
        {
            lock (isInList)
            {
                foreach (var item in isInList.ToList())
                {
                    if (data.Tag != item.Tag)
                    {
                        if (Math.Abs((data.XCoordinate - item.XCoordinate)) < 5000 &&
                            Math.Abs((data.YCoordinate - item.YCoordinate)) < 5000 &&
                            Math.Abs((data.Altitude - item.Altitude)) < 300)
                        {
                            Console.WriteLine("WARNING!! {0} and {1} are TOO DAMN CLOSE!!", data.Tag, item.Tag);
                        }
                    }
                }
            }

        }
    }
}
