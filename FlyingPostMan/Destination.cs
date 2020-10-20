using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingPostMan
{
     public  class Destination:Station
    {
        public Time startTime { get; set; }
        public Time endTime { get; set; }

        public Destination(string name, int x, int y) : base(name, x, y)
        {

        }

        //public Destination(Station station) : base()
        //{
        //    this.X = station.X;
        //    this.Y = station.Y;
        //    this.Name = station.Name;
        //    this.distanceToLastStation = station.distanceToLastStation;
        //}
    }
}
