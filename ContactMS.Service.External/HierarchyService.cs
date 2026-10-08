using ContactMS.DTOs.Hierarchy;
using ContactMS.Framework.Extensions;
using ContactMS.Service.External.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContactMS.Service.External
{
    public class HierarchyService :IHierarchyService
    {
        private readonly IConfiguration _config;
        private readonly int _hierarchyRequestTimeoutSeconds = 40;
        private readonly IHttpClientFactory _httpClientFactory;
        public HierarchyService(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }
        private Dictionary<string, string> GetAPIAuthenticationHeader(string token)
        {
            Dictionary<string, string> headers = new()
            {
                { "Ocp-Apim-Subscription-Key", _config.GetSection("HierarchyAPI:AzureToken").Value },
                { "Bearer", token }
            };
            return headers;
        }
        private HttpClient GetHttpClient(int timeout = 25)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            return client;
        }



        public async Task<ActionStatus<HierarchyDTO>> GetHierarchyDetails(string token, long id)
        {
            try
            {
                HttpClient client = GetHttpClient();
                Dictionary<string, string> headers = GetAPIAuthenticationHeader(token);
                HttpResponseMessage response = await client.APIGetAsync(_config.GetSection("HierarchyAPI:URL").Value + URLStack.HierarchyDetails + "=" + id, headers, _hierarchyRequestTimeoutSeconds);
                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    ActionStatus<HierarchyDTO>? result = JsonSerializer.Deserialize<ActionStatus<HierarchyDTO>>(resultJson, new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return new ActionStatus<HierarchyDTO>(result.IsSuccess, result.Result);
                }
                else
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(resultJson))
                    {
                        ActionStatus<HierarchyDTO>? result = JsonSerializer.Deserialize<ActionStatus<HierarchyDTO>>(resultJson, new JsonSerializerOptions()
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        return result;
                    }
                    return new ActionStatus<HierarchyDTO>(new ResponseVM("DEFAULT"));
                }
            }
            catch (Exception ex)
            {
                return new ActionStatus<HierarchyDTO>("Exception", ex);
            }
        }

        public async Task<ActionStatus<IHierarchyResponse>> CreateHierarchy(string token, HierarchyCreateRequestDTO request)
        {
            try
            {
                HttpClient client = GetHttpClient();
                Dictionary<string, string> headers = GetAPIAuthenticationHeader(token);
                string data = JsonSerializer.Serialize(request);
                HttpResponseMessage response = await client.APIPostAsync(_config.GetSection("HierarchyAPI:URL").Value + URLStack.CreateHierarchy, request, headers, _hierarchyRequestTimeoutSeconds);
                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    ActionStatus<HierarchyResponse>? result = JsonSerializer.Deserialize<ActionStatus<HierarchyResponse>>(resultJson, new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return new ActionStatus<IHierarchyResponse>(result.IsSuccess,result.Result);
                }
                else
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    IResponse? result = JsonSerializer.Deserialize<Response>(resultJson, new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return new ActionStatus<IHierarchyResponse>(new ResponseVM(result.ResponseCode, result.ResponseMessage));
                }
            }
            catch (Exception ex)
            {
                return new ActionStatus<IHierarchyResponse>("Exception", ex);
            }
        }



    }
}
