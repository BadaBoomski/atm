using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace handleData
{
    class Track
    {
        public string Tag { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Altitude { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Velocity { get; set; }
        public double Course { get; set; }



        public Track convertToTrackData(string rawData)
        {
            Track tempTrack = new Track();

            string[] delimiters = { ";" };
            string[] subStrings = rawData.Split(delimiters, StringSplitOptions.None);

            tempTrack.Tag = subStrings[0];
            tempTrack.X = Int32.Parse(subStrings[1]);
            tempTrack.Y = Int32.Parse(subStrings[2]);
            tempTrack.Altitude = Int32.Parse(subStrings[3]);
            tempTrack.TimeStamp = DateTime.ParseExact(subStrings[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
            tempTrack.Velocity = 0;
            tempTrack.Course = 0;

            return tempTrack;
        }

        public void calculateSpeed(Track previousData)
        {
            if (previousData.Altitude < 1 && previousData.X < 1 && previousData.Y < 1)
            {
                System.Console.WriteLine("No previos data to compare to - Insufficient data to calculate speed");
                return;
            }

            int deltaXAxis = this.X - previousData.X;
            int deltaYAxis = this.Y - previousData.Y;
            int deltaZAxis = this.Altitude - previousData.Altitude;
            double deltaTime = this.TimeStamp.Subtract(previousData.TimeStamp).TotalSeconds;

            double horizontalResult =
                (deltaXAxis * deltaXAxis) + (deltaYAxis * deltaYAxis); //pythagoras sentence for horizontalmovement
            horizontalResult = Math.Sqrt(horizontalResult); //horizontal movement since last ping

            double verticalResult =
                (deltaZAxis * deltaZAxis) +
                (horizontalResult * horizontalResult); //pythagoras sentence for vertical movement
            double distanceTraveled = Math.Sqrt(verticalResult); //total movement since last ping 

            double metersPerSecond =
                distanceTraveled / (deltaTime); //prototype implementations. should be improved

            this.Velocity = metersPerSecond;
        }

        public void compassCourse(Track previousData)
        {
            if (previousData.X < 1 && previousData.Y < 1)
            {
                System.Console.WriteLine("No previos data to compare to - Insufficient data to calculate compass direction");
                return;
            }

            int deltaX = this.X - previousData.X;
            int deltaY = this.Y - previousData.Y;

            double direction = Math.Atan2(deltaY, deltaX); //Udregning af retning i grader

            this.Course = direction; //assigned memeber data
        }
    }
}

