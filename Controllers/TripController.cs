/// <summary>
/// Controller class to call the appropriate Business switches
/// Author - Shreema Das
/// </summary>


namespace DemoAPI.Controllers
{
    using DemoAPI.Interface;
    using DemoAPI.Mapper;
    using DemoAPI.Model;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Commenting it so that it can performed as an Open API 
    /// </summary>
    // [Microsoft.AspNetCore.Authorization.Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TripController : ControllerBase
    {

        private readonly ILogger<TripController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ITrip _destination;
        public TripController(ILogger<TripController> logger, ITrip destination, IConfiguration con)
        {
            _logger = logger;
            _destination = destination;
            _configuration = con;
        }
        /// <summary>
        /// Demo response to test API endpoint
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {
            return "Hello from API";
        }

        [HttpPost]
        public string ChargeLevel(DestinationReq req)
        {

            //string url = _configuration.GetValue<string>("APIEndpoint:ChargeLevel");
            //_destination.ChargeLevelAsync(url, req);
            string chargingStationsurl = _configuration.GetValue<string>("APIEndpoint:Distance");
            var chargingStations = _destination.DistanceAsync(chargingStationsurl, Mapper.TripReqBuilder(req));
            return "test";
        }
        /// <summary>
        /// destination Details 
        /// Check if the vehicle can reach the destination without charging
        /// Find charging stations - If the destination can not be reached with current charging level.
        /// Appropriate handling to be done if the destination cannot be reached even with charging
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("query")]
        public async Task<dynamic> Destination(DestinationReq req)
        {
            ///Log controler request
            ///Check Request Validation
            if (req == null)
                return new DestinationDetails { destination = req.destination, Errors = new List<ErrorModel>() { new ErrorModel { } } };

            ///Read from Configuration
            string chargeurl = _configuration.GetValue<string>("APIEndpoint:ChargeLevel");
            string distanceurl = _configuration.GetValue<string>("APIEndpoint:Distance");
            string chargingStationsurl = _configuration.GetValue<string>("APIEndpoint:ChargingStations");

            ///Calling the Implementaion Method
            var chargeLevel = _destination.ChargeLevelAsync(chargeurl, Mapper.ChargeLevelReqBuilder(req));
            var distance = _destination.DistanceAsync(distanceurl, Mapper.TripReqBuilder(req));
            var chargingStations = _destination.ChargingStationsAsync(chargingStationsurl, Mapper.TripReqBuilder(req));

            await Task.WhenAll(chargeLevel, distance, chargingStations);
            var results1 = await chargeLevel;
            var results2 = await distance;
            var results3 = await chargingStations;

            ///Combining the response
            return Mapper.DestinationDetailsBuilder(results1, results2, results3);

        }
    }
}
