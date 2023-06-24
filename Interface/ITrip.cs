
/// <summary>
/// Destimation Interface
/// Author - Shreema Das
/// </summary>

namespace DemoAPI.Interface
{
    using DemoAPI.Model;
    using System.Threading.Tasks;
    /// <summary>
    /// Interface for Service Call Implementation
    /// </summary>
    public interface ITrip
    {
        /// <summary>
        /// Method will be implemented further to call Mock Distance API
        /// </summary>
        /// <param name="url"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<DistanceModel> DistanceAsync(string url, Trip req);

        /// <summary>
        /// Method will be implemented further to call Charge level API
        /// </summary>
        /// <param name="url"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ChargeLevel> ChargeLevelAsync(string url, ChargeLevelReq req);

        /// <summary>
        /// Method will be implemented further to call ChargeStation details
        /// </summary>
        /// <param name="url"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ChargeStations> ChargingStationsAsync(string url,Trip req);
    }
}
