using Microsoft.JSInterop;

namespace CS_Emerios_API_Tracker.Helper
{
    public static class JSExtensions
    {
        public static ValueTask<object> SaveFileAs(this IJSRuntime js, string filename, byte[] file)
        {
            return js.InvokeAsync<object>("saveAsFile",
                filename,
                Convert.ToBase64String(file));
        }
    }
}
