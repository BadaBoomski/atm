using System.Collections.Generic;
using NSubstitute;
using NSubstitute.Core;
using System.Linq;
using System.Text;
using NSubstitute.Core.Arguments;
using NUnit.Framework;
using TransponderReceiver;
using NUnit.Framework.Internal;
using TransponderReceiverApplication;

namespace TransponderReceiverUser.Test.Unit
{
    [TestFixture]
    [Author("SWTgruppe20")]
    class TestTrackAndWarnings
    {
        private Warnings _iwarnings;

        [SetUp]
        public void SetUp()
        {
            _iwarnings = new Warnings();
        }

        [TestCase("ATR423;39045;12932;14000;20151006213456789", true)]
        [TestCase("ATR423;10000;10000;500;20151006213456789", true)]        // equals lower boundary
        [TestCase("ATR423;89999;89999;19999;20151006213456789", true)]      // just below upper boundary
        [TestCase("ATR423;9999;9999;499;20151006213456789", false)]         // all outside lower boundary
        [TestCase("ATR423;90000;90000;20000;20151006213456789", false)]     // all outside upper boundary
        [TestCase("ATR423;45000;9999;14000;20151006213456789", false)]      // YCoor below boundary
        [TestCase("ATR423;9999;45000;14000;20151006213456789", false)]      // XCoor below boundary
        [TestCase("ATR423;45000;45000;200;20151006213456789", false)]       // Altitude below boundary
        [TestCase("ATR423;0;12932;14000;20151006213456789", false)]         // XCoor = 0
        [TestCase("ATR423;39045;0;14000;20151006213456789", false)]         // YCoor = 0
        [TestCase("ATR423;39045;12932;0;20151006213456789", false)]         // Altitude = 0
        [TestCase("ATR423;39045;12932;-5;20151006213456789", false)]        // Altitude is negative

        public void insideAirspace_ReturnsBoolean(string data, bool resultat) // works!
        {
            TrackData _data = new TrackData(data);
            Assert.That(_iwarnings.insideAirspace(_data), Is.EqualTo(resultat));
        }


    }
}
