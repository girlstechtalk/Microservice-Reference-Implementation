
/// <summary>
/// Destimation Implemenetaion Class 
/// Includes this business logic 
/// Author - Shreema Das
/// </summary>
namespace DemoAPI.Implementation
{
    using DemoAPI.Interface;
    using DemoAPI.Model;
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class TripImplementation : ITrip
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ChargeLevel> ChargeLevelAsync(string url, ChargeLevelReq req)
        {
            try
            {
                ///log service call started 
                using (var client = new HttpClient())
                {
                    HttpContent c = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, c);
                    response.EnsureSuccessStatusCode();
                    string responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ChargeLevel>(responseData);
                }
            }
            catch(Exception ex)
            {
                ///Log and throw 
                throw ex;
            }
        }

        public async Task<ChargeStations> ChargingStationsAsync(string url, Trip req)
        {
            try
            {
                ///log service call started 
                using (var client = new HttpClient())
                {
                    HttpContent c = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, c);
                    response.EnsureSuccessStatusCode();
                    string responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ChargeStations>(responseData);
                }
            }
            catch (Exception ex)
            {
                ///log and throw
                throw ex;
            }
        }


        public async Task<DistanceModel> DistanceAsync(string url, Trip req)
        {
            try
            {
                ///log service call started 
                using (var client = new HttpClient())
                {
                    HttpContent c = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(url, c);
                    response.EnsureSuccessStatusCode();
                    string responseData = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<DistanceModel>(responseData);
                }
            }
            catch (Exception ex)
            {
                ///log and trhow
                throw ex;
            }
        }
    }
}
