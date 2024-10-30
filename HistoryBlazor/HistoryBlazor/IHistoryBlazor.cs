
namespace HistoryBlazor;

public interface IHistoryBlazor
{
    ISyncHistoryBlazor Sync { get; }
    Task BackAsync(CancellationToken cancellationToken = default);
    Task CheckJavaScriptModuleAsync();
    ValueTask DisposeAsync();
    Task ForwardAsync(CancellationToken cancellationToken = default);
    Task<int> GetLengthAsync(CancellationToken cancellationToken = default);
    Task<ScrollRestoration> GetScrollRestorationAsync(CancellationToken cancellationToken = default);
    Task GetStateAsync(CancellationToken cancellationToken = default);
    Task GoAsync(int delta = 0, CancellationToken cancellationToken = default);
    Task PushStateAsync<T>(T data, Uri url, CancellationToken cancellationToken = default);
    Task ReplaceStateAsync<T>(T data, Uri url, CancellationToken cancellationToken = default);
    Task PushStateAsync<T>(T data, string? url = null, CancellationToken cancellationToken = default);
    Task ReplaceStateAsync<T>(T data, string? url = null, CancellationToken cancellationToken = default);
    Task ReplaceStateWithCurrentStateAsync(string url, CancellationToken cancellationToken = default);
    Task SetScrollRestorationAsync(ScrollRestoration value, CancellationToken cancellationToken = default);
}