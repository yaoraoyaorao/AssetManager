using AssetManager.Shared;
using Newtonsoft.Json;
using RestSharp;

namespace AssetManager.WPF.Service.Base
{
    public class HttpRestClient
    {
        private readonly string apiUrl;

        protected readonly RestClient client;

        public HttpRestClient(string apiUrl)
        {

            this.apiUrl = apiUrl;

            client = new RestClient(apiUrl);
        }

        public async Task<ApiResponse> ExecuteAsync(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseRequest.Route, baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);

            if (baseRequest.Parameter != null)
            {
                string body = JsonConvert.SerializeObject(baseRequest.Parameter);
                request.AddJsonBody(body);
            }

            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<ApiResponse>(response.Content);
            else
                return new ApiResponse { Code = 200, Message = response.ErrorMessage };
        }

        public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseRequest.Route, baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);

            if (baseRequest.Parameter != null)
            {
                string body = JsonConvert.SerializeObject(baseRequest.Parameter);
                request.AddJsonBody(body);
            }

            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
            else
                return new ApiResponse<T> { Code = 400, Message = response.ErrorMessage };
        }
    }
}
