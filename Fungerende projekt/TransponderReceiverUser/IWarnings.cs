using System.Collections.Generic;

namespace TransponderReceiverUser
{
    public interface IWarnings
    {
        List<Plane> planeList { get; set; }
    }
}