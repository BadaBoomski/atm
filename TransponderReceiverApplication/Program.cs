﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TransponderReceiver;
using TransponderReceiverUser;

namespace TransponderReceiverApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            // Using the real transponder data receiver
            var receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            // Dependency injection with the real TDR
            var system = new TransponderReceiverUser.TransponderReceiverClient(receiver);

            //create a program wide version of warnings
            Warnings warnings = new Warnings();

            // Let the real TDR execute in the background
            while (true)
            {
                Thread.Sleep(1000);
            }

        }
    }
}
