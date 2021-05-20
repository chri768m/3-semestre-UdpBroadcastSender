using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpBroadcastSender
{
    class Measurement
    {
        public int Temp { get; set; }
        public int Humi { get; set; }
        public int Id { get; set; }
        public int Stoej { get; set; }

        public Measurement(int temp, int humi, int stoej, int id)
        {
            Temp = temp;
            Humi = humi;
            Stoej = stoej;
            Id = id;
        }

        public override string ToString()
        {
            return Temp + " " + Humi + " " + Stoej;
        }
    }
}
