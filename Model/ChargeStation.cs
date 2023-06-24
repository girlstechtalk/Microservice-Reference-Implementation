using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DemoAPI.Model
{
    public class ChargeStation
    {
        public string Name { get; set; }
        public int Distance { get; set; }
        public int Limit { get; set; }
        [JsonIgnore]
        public bool IsVisited { get; set; }

    }
    public class ChargeStations : Trip, Error
    {
        public List<ChargeStation> chargingStations { get; set; }
        public string ErrorDetails { get; set; }
    }

}
