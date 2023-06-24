/// <summary>
/// Helper class to data Mapper
/// </summary>
namespace DemoAPI.Mapper
{

    using DemoAPI.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Use to map the response , validations, Helper class
    /// Contains Error Code and Descriptions
    /// </summary>
    public static class Mapper
    {
        private static readonly string ErrorChargeLevelAPI = "1111";
        private static readonly string ErrorChargingStationAPI = "8888";
        private static readonly string ErrorDistanceAPI = "7777";
        private static readonly string ErrorinServiceCall = "6666";
        private static readonly string ErrorDesc = "Technical Exception. Mock service response contains Error, please validate the service endpoint or requets data.";
        private static readonly string ErrorTechnical = "9999";
        private static readonly string ErrorTechnicalDesc = "Unable to reach the destination with the current fuel level";

        /// <summary>
        /// Request builder for service call
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static ChargeLevelReq ChargeLevelReqBuilder(DestinationReq req)
        {
            return new ChargeLevelReq { vin = req.VIN };
        }
        /// <summary>
        /// Trip Details Builder from generic request
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static Trip TripReqBuilder(DestinationReq req)
        {
            return new Trip { destination = req.destination, source = req.source };
        }
        /// <summary>
        /// Final APi response object builder
        /// </summary>
        /// <param name="charge"></param>
        /// <param name="distance"></param>
        /// <param name="chargeStation"></param>
        /// <returns></returns>
        public static dynamic DestinationDetailsBuilder(ChargeLevel charge, DistanceModel distance, ChargeStations chargeStation)
        {
            var results = IfResponseContainsError(charge, distance, chargeStation);
            if (results != null)
                return (ErrorResponse)results;

            var destinations =  new DestinationDetails
            {
                ChargingStations = ChargingStations(charge.currentChargeLevel, chargeStation, distance.Distance),
                CurrentChargeLevel = Convert.ToInt16(charge.currentChargeLevel),
                destination = distance.destination,
                Distance = distance.Distance,
               // Errors = Errors(charge, distance, chargeStation),
                IsChargingRequired = IfChargeRequired(charge, distance),
                source = distance.source,
                TransactionId = GenerateTransactionId(0, Int16.MaxValue),
                VIN = charge.VIN
            };
            if(destinations.IsChargingRequired && destinations.ChargingStations == null)
            {
                destinations.Errors= GetErrors(charge, distance, chargeStation, true);
            }
            return destinations;
        }
        /// <summary>
        /// If service response contains any error. 
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="dist"></param>
        /// <param name="stations"></param>
        /// <returns></returns>
        private static ErrorResponse IfResponseContainsError(ChargeLevel ch, DistanceModel dist, ChargeStations stations)
        {
            if (ch == null || dist == null || (stations == null || (stations != null && stations.chargingStations == null)))
                return new ErrorResponse
                {
                    TransactionId = GenerateTransactionId(0, Int16.MaxValue),
                    Errors = new List<ErrorModel>()
                {
                    new ErrorModel (){ Id=ErrorinServiceCall, ErrorDetails=ErrorDesc} },
                };
            return null;
        }
        /// <summary>
        /// Get list of Charge Stations
        /// Contains Algorithm to choose the Stations
        /// </summary>
        /// <param name="chargeStation"></param>
        /// <returns></returns>
        private static List<ChargeStation> ChargingStations(int currentcharge, ChargeStations stations, int dist)
        {
            var list = stations.chargingStations.OrderBy(a => a.Distance).ToList();
            int currposition = 0;
            int initialcharge = currentcharge;
            Dictionary<int, List<ChargeStation>> dic = new Dictionary<int, List<ChargeStation>>();
            bool ifCompleted = false;
            for (int i = 0; i < list.Count; i++)
            {
                int j = i + 1;
                if (currentcharge > list[i].Distance && j < list.Count && currentcharge < list[j].Distance)
                {
                    list[i].IsVisited = true;
                    currposition = list[i].Distance;
                    currentcharge = currentcharge + list.Where(a => a.IsVisited == true).Select(a => a.Limit).Sum() - list[i].Distance;
                }
                if (currentcharge > (dist - currposition))
                {
                    ifCompleted = true;
                    break;
                }
            }
            if (ifCompleted)
                return list.Where(a => a.IsVisited == true).ToList();
            return null;
        }


        /// <summary>
        /// Check if other service response contains any Error
        /// </summary>
        /// <param name="charge"></param>
        /// <param name="distance"></param>
        /// <param name="chargeStation"></param>
        /// <returns></returns>
        private static List<ErrorModel> GetErrors(ChargeLevel charge, DistanceModel distance, ChargeStations chargeStation, bool isTechnicalError =false)
        {
            List<ErrorModel> errors = new List<ErrorModel>();
            if (!string.IsNullOrEmpty(charge.ErrorDetails))
            {
                errors.Add(new ErrorModel { Id = ErrorChargeLevelAPI, ErrorDetails = charge.ErrorDetails });
            }
            if (!string.IsNullOrEmpty(distance.ErrorDetails))
            {
                errors.Add(new ErrorModel { Id = ErrorDistanceAPI, ErrorDetails = distance.ErrorDetails });
            }
            if (!string.IsNullOrEmpty(chargeStation.ErrorDetails))
            {
                errors.Add(new ErrorModel { Id = ErrorChargingStationAPI, ErrorDetails = chargeStation.ErrorDetails });
            }
            if (isTechnicalError)
            {
                errors.Add(new ErrorModel { Id = ErrorTechnical, ErrorDetails = ErrorTechnicalDesc });
            }
            if (errors.Count > 0)
                return errors;
            return null;
        }
        /// <summary>
        /// Return true if charging required
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="dis"></param>
        /// <returns></returns>
        private static bool IfChargeRequired(ChargeLevel ch, DistanceModel dis)
        {
            if (ch.currentChargeLevel == 0 || ch.currentChargeLevel <= dis.Distance)
                return true;
            return false;
        }
        /// <summary>
        /// TransactionId Generator
        /// </summary>

        private static readonly Random randomId = new Random();
        private static int GenerateTransactionId(int min, int max)
        {
            lock (randomId) // synchronize
            {
                return randomId.Next(min, max);
            }
        }
    }
}
