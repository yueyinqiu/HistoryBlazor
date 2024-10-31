export function getLength()
{
    return history.length;
}

// "auto" | "manual"
export function getScrollRestoration()
{
    return history.scrollRestoration;
}
export function setScrollRestoration(scrollRestoration)
{
    history.scrollRestoration = scrollRestoration;
}

export function getState()
{
    return history.state;
}

export function back()
{
    history.back();
}

export function forward()
{
    history.forward();
}

export function go(delta)
{
    history.go(delta);
}

export function pushState(data, url)
{
    history.pushState(data, "", url);
}

export function replaceState(data, url)
{
    history.replaceState(data, "", url);
}

export function pushStateWithCurrentState(url)
{
    history.pushState(history.state, "", url);
}

export function replaceStateWithCurrentState(url)
{
    history.replaceState(history.state, "", url);
}
