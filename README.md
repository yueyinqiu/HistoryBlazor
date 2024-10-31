# HistoryBlazor

Use window.history in blazor apps. Supports synchronous calls in WebAssembly.

## Notes

1. `ISyncHistoryBlazor` is not registered as a service, use `IHistoryBlazor.Sync` instead.
2. `IHistoryBlazor.Sync` is only available for Blazor WebAssembly.
3. `IHistoryBlazor.Sync` may throw exceptions if the JavaScript module has not been imported, since the modules can only be imported asynchronously.
    - But you don't have to worry about that, since it's not multithreaded.
    - If you do want to ensure it has been imported, use `IHistoryBlazor.CheckJavaScriptModuleAsync`.

## Usage

In `Program.cs`:

```csharp
using HistoryBlazor.Extensions;

builder.Services.AddHistoryBlazor();
```

In your page:

```razor
@page "/"

@using HistoryBlazor

@inject IHistoryBlazor History

<PageTitle>Home</PageTitle>

<div>
    <button @onclick="BackAsync">BackAsync</button>
    <button @onclick="BackSync" style="background-color: azure">BackSync</button>
    <button @onclick="ForwardAsync">ForwardAsync</button>
    <button @onclick="ForwardSync" style="background-color: azure">ForwardSync</button>
</div>

<div>
    <textarea>@getOutput</textarea>
    <button @onclick="GetLengthAsync">GetLengthAsync</button>
    <button @onclick="GetLengthSync" style="background-color: azure">GetLengthSync</button>
    <button @onclick="GetScrollRestorationAsync">GetScrollRestorationAsync</button>
    <button @onclick="GetScrollRestorationSync" style="background-color: azure">GetScrollRestorationSync</button>
    <button @onclick="GetStateAsync">GetStateAsync</button>
    <button @onclick="GetStateSync" style="background-color: azure">GetStateSync</button>
</div>

<div>
    <InputNumber TValue="int" @bind-Value="deltaInput"></InputNumber>
    <button @onclick="GoAsync">GoAsync</button>
    <button @onclick="GoSync" style="background-color: azure">GoSync</button>
</div>

<div>
    <InputText @bind-Value="scrollRestorationInput"></InputText>
    <button @onclick="SetScrollRestorationAsync">SetScrollRestorationAsync</button>
    <button @onclick="SetScrollRestorationSync" style="background-color: azure">SetScrollRestorationSync</button>
</div>

<div>
    <InputText @bind-Value="urlInput"></InputText>
    <InputTextArea @bind-Value="stateInput"></InputTextArea>
    <button @onclick="PushStateAsync">PushStateAsync</button>
    <button @onclick="PushStateSync" style="background-color: azure">PushStateSync</button>
    <button @onclick="ReplaceStateAsync">ReplaceStateAsync</button>
    <button @onclick="ReplaceStateSync" style="background-color: azure">ReplaceStateSync</button>
</div>
```

```csharp
using System.Text.Json;

namespace Test.Pages;

partial class Home
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
        getOutput = (await this.History.GetLengthAsync()).ToString();
    }
    private void GetLengthSync()
    {
        getOutput = (this.History.Sync.GetLength()).ToString();
    }
    private async Task GetScrollRestorationAsync()
    {
        getOutput = (await this.History.GetScrollRestorationAsync()).Value;
    }
    private void GetScrollRestorationSync()
    {
        getOutput = (this.History.Sync.GetScrollRestoration()).Value;
    }
    private async Task GetStateAsync()
    {
        getOutput = (await this.History.GetStateAsync<JsonElement>()).GetRawText();
    }
    private void GetStateSync()
    {
        getOutput = (this.History.Sync.GetState<JsonElement>()).GetRawText();
    }

    private int deltaInput = 0;
    private async Task GoAsync()
    {
        await this.History.GoAsync(deltaInput);
    }
    private void GoSync()
    {
        this.History.Sync.Go(deltaInput);
    }

    private string scrollRestorationInput = "auto";
    private async Task SetScrollRestorationAsync()
    {
        await this.History.SetScrollRestorationAsync(new(scrollRestorationInput));
    }
    private void SetScrollRestorationSync()
    {
        this.History.Sync.SetScrollRestoration(new(scrollRestorationInput));
    }

    private string stateInput = "{ \"hello\": \"world\"}";
    private string urlInput = "/";
    private async Task PushStateAsync()
    {
        await this.History.PushStateAsync(JsonSerializer.Deserialize<JsonElement>(stateInput), urlInput);
    }
    private void PushStateSync()
    {
        this.History.Sync.PushState(JsonSerializer.Deserialize<JsonElement>(stateInput), urlInput);
    }
    private async Task ReplaceStateAsync()
    {
        await this.History.ReplaceStateAsync(JsonSerializer.Deserialize<JsonElement>(stateInput), urlInput);
    }
    private void ReplaceStateSync()
    {
        this.History.Sync.ReplaceState(JsonSerializer.Deserialize<JsonElement>(stateInput), urlInput);
    }
}
```
