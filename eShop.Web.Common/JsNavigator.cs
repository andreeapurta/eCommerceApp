using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace eShop.Web.Common
{
    public class JsNavigator
    {
        private IJSRuntime JSRuntime;

        public JsNavigator(IJSRuntime jSRuntime)
        {
            this.JSRuntime = jSRuntime;
        }

        public async Task NavigateToAsync(string url)
        {
            await JSRuntime.InvokeVoidAsync("navigateTo", url);
        }
    }
}