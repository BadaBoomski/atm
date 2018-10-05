using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Remoting.Services;
using TransponderReceiver;

namespace TransponderReceiverUser
{
    public class TransponderReceiverClient
    {

        private Warnings warnings;
        private ITransponderReceiver receiver;

        // Using constructor injection for dependency/ies
        public TransponderReceiverClient(ITransponderReceiver receiver)
        {
            warnings = new Warnings();
            // This will store the real or the fake transponder data receiver
            this.receiver = receiver;

            // Attach to the event of the real or the fake TDR
            this.receiver.TransponderDataReady += ReceiverOnTransponderDataReady;
        }

        private void ReceiverOnTransponderDataReady(object sender, RawTransponderDataEventArgs e)
        {
            Console.Clear();
            // Just display data
            foreach (var data in e.TransponderData)
            {
                TrackData trackData = new TrackData(data);
                warnings.ProcessTrackData(trackData);
                // warnings.PlanesAreTooDamnClose();
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- Planes --- {0}", DateTime.Now.ToString("T"));
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (Plane plane in warnings.planeList)
            {
                Console.WriteLine(plane);
                warnings.PlanesAreTooDamnClose(plane);
            }

            if (warnings.planesInDanger.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n--- THESE NIGGAS TOO CLOSE ---");
                var prints = warnings.planesInDanger.Select(pair => string.Format("{0} is too close to {1} at {2}", pair.Item1.Data.Tag, pair.Item2.Data.Tag, DateTime.Now.ToString("T")));
                foreach (var print in prints)
                {
                    Console.WriteLine(print);
                }
                Console.ResetColor();
            }
        }
    }
}