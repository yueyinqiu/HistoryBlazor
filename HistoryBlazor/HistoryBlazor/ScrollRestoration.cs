namespace HistoryBlazor;

public sealed record ScrollRestoration(string Value)
{
    public override string ToString() => this.Value;
    public ScrollRestoration(bool manual = false) : this(manual ? "manual" : "auto") { }
    public bool IsValueValid
    {
        get
        {
            var lower = this.Value.ToLowerInvariant();
            return lower is "manual" or "auto";
        }
    }
}
