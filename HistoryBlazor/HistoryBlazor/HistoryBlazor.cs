using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace HistoryBlazor;

internal sealed class HistoryBlazor(IJSRuntime jsRuntime) : IAsyncDisposable, ISyncHistoryBlazor
{
    public async ValueTask DisposeAsync()
    {
        var module = await moduleTask;
        await module.DisposeAsync();
    }
    private readonly Task<IJSObjectReference> moduleTask =
        jsRuntime.InvokeAsync<IJSObjectReference>(
            "import",
            "./_content/HistoryBlazor/HistoryBlazor.js").AsTask();
    public async Task CheckJavaScriptModuleAsync()
    {
        _ = await moduleTask;
    }
    private IJSInProcessObjectReference? syncModule = null;
    public ISyncHistoryBlazor Sync
    {
        get
        {
            if (this.syncModule is not null)
                return this;

            if (!moduleTask.IsCompleted)
            {
                throw new JSException(
                    "HistoryBlazor JavaScript module has not been imported yet. " +
                    "So you can't use it synchronously at present.");
            }
            if (!moduleTask.IsCompletedSuccessfully)
            {
                if (moduleTask.Exception is null)
                    throw new JSException(
                        "Failed to import HistoryBlazor JavaScript module.");
                else
                    throw new JSException(
                        "Failed to import HistoryBlazor JavaScript module.",
                        moduleTask.Exception);
            }
            if (moduleTask.Result is not IJSInProcessObjectReference syncModule)
            {
                throw new JSException(
                    "HistoryBlazor JavaScript module is not executed in process. " +
                    "So you can't use it synchronously.");
            }

            this.syncModule = syncModule;
            return this;
        }
    }

    public async Task<int> GetLengthAsync(
        CancellationToken cancellationToken = default)
    {
        var module = await moduleTask;
        return await module.InvokeAsync<int>("getLength", cancellationToken);
    }

    public int GetLength()
    {
        // https://github.com/dotnet/aspnetcore/issues/58712
#pragma warning disable IL2026
        return syncModule!.Invoke<int>("getLength");
#pragma warning restore IL2026
    }

    public async Task<ScrollRestoration> GetScrollRestorationAsync(
        CancellationToken cancellationToken = default)
    {
        var module = await moduleTask;
        var result = await module.InvokeAsync<string>("getScrollRestoration", cancellationToken);
        return new ScrollRestoration(result);
    }
    public ScrollRestoration GetScrollRestoration()
    {
        // https://github.com/dotnet/aspnetcore/issues/58712
#pragma warning disable IL2026
        var result = syncModule!.Invoke<string>("getScrollRestoration");
#pragma warning restore IL2026
        return new ScrollRestoration(result);
    }

    public async Task SetScrollRestorationAsync(
        ScrollRestoration value,
        CancellationToken cancellationToken = default)
    {
        var module = await moduleTask;
        await module.InvokeVoidAsync("setScrollRestoration", cancellationToken, value.Value);
    }
    public void SetScrollRestoration(ScrollRestoration value)
    {
        syncModule!.InvokeVoid("setScrollRestoration", value.Value);
    }

    [RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed.")]
    public async Task<T> GetStateAsync<
        [DynamicallyAccessedMembers(
        DynamicallyAccessedMemberTypes.PublicConstructors | 
        DynamicallyAccessedMemberTypes.PublicFields | 
        DynamicallyAccessedMemberTypes.PublicProperties)] T>(
        CancellationToken cancellationToken = default)
    {
        var module = await moduleTask;
        return await module.InvokeAsync<T>("getState", cancellationToken);
    }

    [RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed.")]
    public T GetState<
        [DynamicallyAccessedMembers(
        DynamicallyAccessedMemberTypes.PublicConstructors |
        DynamicallyAccessedMemberTypes.PublicFields |
        DynamicallyAccessedMemberTypes.PublicProperties)] T>()
    {
        return syncModule!.Invoke<T>("getState");
    }

    public async Task BackAsync(
        CancellationToken cancellationToken = default)
    {
        var module = await moduleTask;
        await module.InvokeVoidAsync("back", cancellationToken);
    }
    public void Back()
    {
        syncModule!.InvokeVoid("back");
    }

    public async Task ForwardAsync(
        CancellationToken cancellationToken = default)
    {
        var module = await moduleTask;
        await module.InvokeVoidAsync("forward", cancellationToken);
    }
    public void Forward()
    {
        syncModule!.InvokeVoid("forward");
    }

    public async Task GoAsync(
        int delta = 0,
        CancellationToken cancellationToken = default)
    {
        var module = await moduleTask;
        await module.InvokeVoidAsync("go", cancellationToken, delta);
    }
    public void Go(int delta = 0)
    {
        syncModule!.InvokeVoid("go", delta);
    }

    public async Task PushStateAsync<T>(
        T data, string? url = null,
        CancellationToken cancellationToken = default)
    {
        var module = await moduleTask;
        await module.InvokeVoidAsync("pushState", cancellationToken, data, url);
    }
    public void PushState<T>(T data, string? url = null)
    {
        syncModule!.InvokeVoid("pushState", data, url);
    }

    public async Task PushStateWithCurrentStateAsync(
        string? url = null,
        CancellationToken cancellationToken = default)
    {
        var module = await moduleTask;
        await module.InvokeVoidAsync("pushStateWithCurrentState", cancellationToken, url);
    }
    public void PushStateWithCurrentState(string? url = null)
    {
        syncModule!.InvokeVoid("pushStateWithCurrentState", url);
    }

    public async Task ReplaceStateAsync<T>(
        T data, string? url = null,
        CancellationToken cancellationToken = default)
    {
        var module = await moduleTask;
        await module.InvokeVoidAsync("replaceState", cancellationToken, data, url);
    }
    public void ReplaceState<T>(T data, string? url = null)
    {
        syncModule!.InvokeVoid("replaceState", data, url);
    }

    public async Task ReplaceStateWithCurrentStateAsync(
        string? url = null,
        CancellationToken cancellationToken = default)
    {
        var module = await moduleTask;
        await module.InvokeVoidAsync("replaceStateWithCurrentState", cancellationToken, url);
    }
    public void ReplaceStateWithCurrentState(string? url = null)
    {
        syncModule!.InvokeVoid("replaceStateWithCurrentState", url);
    }
}
