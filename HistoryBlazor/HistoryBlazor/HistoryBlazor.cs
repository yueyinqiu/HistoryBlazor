using Microsoft.JSInterop;

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

    public async Task GetStateAsync(
        CancellationToken cancellationToken = default)
    {
        var module = await moduleTask;
        await module.InvokeVoidAsync("getState", cancellationToken);
    }
    public void GetState()
    {
        syncModule!.InvokeVoid("getState");
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
        syncModule!.InvokeVoid("forward", delta);
    }

    public async Task PushStateAsync<T>(
        T data, string? url = null,
        CancellationToken cancellationToken = default)
    {
        var module = await moduleTask;
        await module.InvokeVoidAsync("pushState", cancellationToken, data, url);
    }
    public async Task PushStateAsync<T>(
        T data, Uri url,
        CancellationToken cancellationToken = default)
    {
        await this.PushStateAsync(data, url.ToString(), cancellationToken);
    }
    public void PushState<T>(T data, string? url = null)
    {
        syncModule!.InvokeVoid("pushState", data, url);
    }
    public void PushState<T>(T data, Uri url)
    {
        this.PushState(data, url.ToString());
    }

    public async Task ReplaceStateAsync<T>(
        T data, string? url = null,
        CancellationToken cancellationToken = default)
    {
        var module = await moduleTask;
        await module.InvokeVoidAsync("replaceState", cancellationToken, data, url);
    }
    public async Task ReplaceStateAsync<T>(
        T data, Uri url,
        CancellationToken cancellationToken = default)
    {
        await this.ReplaceStateAsync(data, url.ToString(), cancellationToken);
    }
    public void ReplaceState<T>(T data, string? url = null)
    {
        syncModule!.InvokeVoid("replaceState", data, url);
    }
    public void ReplaceState<T>(T data, Uri url)
    {
        this.ReplaceState(data, url.ToString());
    }

    public async Task ReplaceStateWithCurrentStateAsync(
        string url,
        CancellationToken cancellationToken = default)
    {
        var module = await moduleTask;
        await module.InvokeVoidAsync("replaceStateWithCurrentState", cancellationToken, url);
    }
    public void ReplaceStateWithCurrentState(string url)
    {
        syncModule!.InvokeVoid("replaceStateWithCurrentState", url);
    }
}