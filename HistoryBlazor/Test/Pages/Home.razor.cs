using System.Text.Json;

namespace Test.Pages;

public partial class Home
{
    private async Task BackAsync()
    {
        await this.History.BackAsync();
    }
    private void BackSync()
    {
        this.History.Sync.Back();
    }
    private async Task ForwardAsync()
    {
        await this.History.ForwardAsync();
    }
    private void ForwardSync()
    {
        this.History.Sync.Forward();
    }

    private string getOutput = "";
    private async Task GetLengthAsync()
    {
        this.getOutput = (await this.History.GetLengthAsync()).ToString();
    }
    private void GetLengthSync()
    {
        this.getOutput = (this.History.Sync.GetLength()).ToString();
    }
    private async Task GetScrollRestorationAsync()
    {
        this.getOutput = (await this.History.GetScrollRestorationAsync()).Value;
    }
    private void GetScrollRestorationSync()
    {
        this.getOutput = (this.History.Sync.GetScrollRestoration()).Value;
    }
    private async Task GetStateAsync()
    {
        var json = await this.History.GetStateAsync<JsonElement>();
        this.getOutput = json.GetRawText();
    }
    private void GetStateSync()
    {
        var json = this.History.Sync.GetState<JsonElement>();
        this.getOutput = json.GetRawText();
    }

    private int deltaInput = 0;
    private async Task GoAsync()
    {
        await this.History.GoAsync(this.deltaInput);
    }
    private void GoSync()
    {
        this.History.Sync.Go(this.deltaInput);
    }

    private string scrollRestorationInput = "auto";
    private async Task SetScrollRestorationAsync()
    {
        await this.History.SetScrollRestorationAsync(new(this.scrollRestorationInput));
    }
    private void SetScrollRestorationSync()
    {
        this.History.Sync.SetScrollRestoration(new(this.scrollRestorationInput));
    }

    private string stateInput = "{ \"hello\": \"world\"}";
    private string urlInput = "/";
    private async Task PushStateAsync()
    {
        await this.History.PushStateAsync(JsonSerializer.Deserialize<JsonElement>(this.stateInput), this.urlInput);
    }
    private void PushStateSync()
    {
        this.History.Sync.PushState(JsonSerializer.Deserialize<JsonElement>(this.stateInput), this.urlInput);
    }
    private async Task ReplaceStateAsync()
    {
        await this.History.ReplaceStateAsync(JsonSerializer.Deserialize<JsonElement>(this.stateInput), this.urlInput);
    }
    private void ReplaceStateSync()
    {
        this.History.Sync.ReplaceState(JsonSerializer.Deserialize<JsonElement>(this.stateInput), this.urlInput);
    }
    private async Task PushStateWithCurrentStateAsync()
    {
        await this.History.PushStateWithCurrentStateAsync(this.urlInput);
    }
    private void PushStateWithCurrentStateSync()
    {
        this.History.Sync.PushStateWithCurrentState(this.urlInput);
    }
    private async Task ReplaceStateWithCurrentStateAsync()
    {
        await this.History.ReplaceStateWithCurrentStateAsync(this.urlInput);
    }
    private void ReplaceStateWithCurrentStateSync()
    {
        this.History.Sync.ReplaceStateWithCurrentState(this.urlInput);
    }
}