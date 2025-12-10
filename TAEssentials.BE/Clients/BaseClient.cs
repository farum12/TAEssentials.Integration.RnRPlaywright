using System.Net;

namespace TAEssentials.BE.Clients
{
    public abstract class BaseClient
    {
        protected RestClient _restClient;

        protected BaseClient()
        {
            // Let's apply initialization of configuration here
        }

        protected abstract  Task Authorize();

        protected async Task<RestResponse<T>> GetAsync<T>(RestRequest request)
        {
            var response = await _restClient.ExecuteGetAsync<T>(request);
            return response;
        }

        protected async Task<RestResponse> GetAsync(RestRequest request)
        {
            var response = await _restClient.ExecuteGetAsync(request);
            return response;
        }

        protected async Task<RestResponse<T>> PostAsync<T>(RestRequest request)
        {
            var response = await _restClient.ExecutePostAsync<T>(request);
            return response;
        }

        protected async Task<RestResponse> PostAsync(RestRequest request)
        {
            var response = await _restClient.ExecutePostAsync(request);
            return response;
        }

        protected async Task<RestResponse<T>> PutAsync<T>(RestRequest request)
        {
            var response = await _restClient.ExecutePutAsync<T>(request);
            return response;
        }

        protected async Task<RestResponse> PutAsync(RestRequest request)
        {
            var response = await _restClient.ExecutePutAsync(request);
            return response;
        }

        protected async Task<RestResponse<T>> DeleteAsync<T>(RestRequest request)
        {
            var response = await _restClient.ExecuteDeleteAsync<T>(request);
            return response;
        }

        protected async Task<RestResponse> DeleteAsync(RestRequest request)
        {
            var response = await _restClient.ExecuteDeleteAsync(request);
            return response;
        }

        // PATCH
        protected async Task<RestResponse<T>> PatchAsync<T>(RestRequest request)
        {
            var response = await _restClient.ExecutePatchAsync<T>(request);
            return response;
        }

        protected async Task<RestResponse> PatchAsync(RestRequest request)
        {
            var response = await _restClient.ExecutePatchAsync(request);
            return response;
        }
    }
}