using FXZServer;
using FXZServer.Configurations;
using FXZServer.Store;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace JCSoft.FXZ.Server.Sample.Pages
{
    public class IndexModel : PageModel
    {
        private IOptions<FXZOptions> _options;
        private IStoreProvider _storeProvider;
        public IndexModel(IOptions<FXZOptions> options,
            IStoreProviderFactory storeProviderFactory)
        {
            _options = options;
            _storeProvider = storeProviderFactory.CreateStoreProvider();
        }
        public void OnGet()
        {
            Options = _options.Value;
            ClientRequest = _storeProvider.Get<ClientRequest>("apiname");
        }

        public FXZOptions Options { get; set; }

        public ClientRequest ClientRequest { get; set; }
    }
}
