using FakeUserDataGeneration.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace FakeUserDataGeneration.Components
{
    public partial class UsersTable
    {
        [Parameter]
        public List<FakeUser> Users { get; set; }

        [Parameter]
        public EventCallback<EventArgs> TriggerCallbackToParent { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }


        [JSInvokable]
        public async Task OnScrollToBottom() => await InvokeAsync(() => TriggerCallbackToParent.InvokeAsync());

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("scrollToBottom", DotNetObjectReference.Create(this));
            }
        }

        private void HandleScroll(WheelEventArgs e)
        {
        }
    }
}
