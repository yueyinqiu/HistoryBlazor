
namespace HistoryBlazor;

public interface ISyncHistoryBlazor : IHistoryBlazor
{
    void Back();
    void Forward();
    int GetLength();
    ScrollRestoration GetScrollRestoration();
    void GetState();
    void Go(int delta = 0);
    void PushState<T>(T data, Uri url);
    void ReplaceState<T>(T data, Uri url);
    void PushState<T>(T data, string? url = null);
    void ReplaceState<T>(T data, string? url = null);
    void ReplaceStateWithCurrentState(string url);
    void SetScrollRestoration(ScrollRestoration value);
}