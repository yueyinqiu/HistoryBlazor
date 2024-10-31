
using System.Diagnostics.CodeAnalysis;

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

    [RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed.")]
    Task<T> GetStateAsync<
        [DynamicallyAccessedMembers(
        DynamicallyAccessedMemberTypes.PublicConstructors |
        DynamicallyAccessedMemberTypes.PublicFields |
        DynamicallyAccessedMemberTypes.PublicProperties)] T>(CancellationToken cancellationToken = default);
    Task GoAsync(int delta = 0, CancellationToken cancellationToken = default);
    Task PushStateAsync<T>(T data, string? url = null, CancellationToken cancellationToken = default);
    Task PushStateWithCurrentStateAsync(string? url = null, CancellationToken cancellationToken = default);
    Task ReplaceStateAsync<T>(T data, string? url = null, CancellationToken cancellationToken = default);
    Task ReplaceStateWithCurrentStateAsync(string? url = null, CancellationToken cancellationToken = default);
    Task SetScrollRestorationAsync(ScrollRestoration value, CancellationToken cancellationToken = default);
}