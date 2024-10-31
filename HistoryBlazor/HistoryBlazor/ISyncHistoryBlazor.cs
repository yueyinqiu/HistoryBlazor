
using System.Diagnostics.CodeAnalysis;

namespace HistoryBlazor;

public interface ISyncHistoryBlazor : IHistoryBlazor
{
    void Back();
    void Forward();
    int GetLength();
    ScrollRestoration GetScrollRestoration();

    [RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed.")]
    T GetState<
        [DynamicallyAccessedMembers(
        DynamicallyAccessedMemberTypes.PublicConstructors |
        DynamicallyAccessedMemberTypes.PublicFields |
        DynamicallyAccessedMemberTypes.PublicProperties)] T>();
    void Go(int delta = 0);
    void PushState<T>(T data, Uri url);
    void ReplaceState<T>(T data, Uri url);
    void PushState<T>(T data, string? url = null);
    void ReplaceState<T>(T data, string? url = null);
    void SetScrollRestoration(ScrollRestoration value);
}