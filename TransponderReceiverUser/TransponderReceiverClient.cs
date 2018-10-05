using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Services;
using TransponderReceiver;


namespace TransponderReceiverUser
{
    public class TransponderReceiverClient
    {
        private ITransponderReceiver receiver;

        // Using constructor injection for dependency/ies
        public TransponderReceiverClient(ITransponderReceiver receiver)
        {
            // This will store the real or the fake transponder data receiver
            this.receiver = receiver;

            // Attach to the event of the real or the fake TDR
            this.receiver.TransponderDataReady += ReceiverOnTransponderDataReady;
        }

        private void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            // Just display data
            foreach (var data in e.TransponderData)
            {
                //System.Console.WriteLine($"Transponderdata {data}");
                Track track = new Track();
                var convertedTrackData = track.ConvertToTrackData(data);
               .warnings.addPlane(plane)
                Program.warnings.removePlaneIfOutOfAirspace(convertedTrackData);
                Program.warnings.PlanesAreTooDamnClose(convertedTrackData);
                Console.WriteLine(warnings.PlanesAreTooDamnClose(convertedTrackData));
                
                //warnings.isInList.ForEach(Console.WriteLine);
                
                //handleData.Track testTrack = new handleData.Track();
                //handleData.Track previoustestTrack = new handleData.Track();
                //// Just display data 
                //foreach (var data2 in e.TransponderData)
                //{
                //    testTrack = testTrack.convertToTrackData(data2);
                //    testTrack.calculateSpeed(previoustestTrack);
                //    testTrack.compassCourse(previoustestTrack);

                //    Console.WriteLine($"Tag: {testTrack.Tag.ToString()}");
                //    Console.WriteLine($"X coordinate = {testTrack.X.ToString()}, Y coordinate = {testTrack.Y.ToString()}, Height = {testTrack.Altitude.ToString()}");
                //    Console.WriteLine($"Velocity: {testTrack.Velocity.ToString()}, Compas Course: {testTrack.Course.ToString()}");
                //    Console.WriteLine("");

                //    previoustestTrack = testTrack;
            }
            }
        }
    }

