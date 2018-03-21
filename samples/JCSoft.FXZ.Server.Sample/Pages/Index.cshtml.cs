using FXZServer;
using FXZServer.Configurations;
using FXZServer.Managers;
using FXZServer.Store;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace JCSoft.FXZ.Server.Sample.Pages
{
    public class IndexModel : PageModel
    {
        private IOptions<FXZOptions> _options;
        private IStoreProvider _storeProvider;
        private IApiManager _apiManager;
        public IndexModel(IOptions<FXZOptions> options,
            IStoreProviderFactory storeProviderFactory,
            IApiManager apiManager)
        {
            _options = options;
            _apiManager = apiManager;
            _storeProvider = storeProviderFactory.CreateStoreProvider();
        }
        public void OnGet()
        {
            Options = _options.Value;
            var response = _apiManager.GetAll();
            if (!response.IsError)
            {
                Apis = response.Data;
            }
        }

        public FXZOptions Options { get; set; }

        public ClientRequest ClientRequest { get; set; }

        public ApiCollections Apis { get; set; }
    }
}
