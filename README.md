# HandleFocus
.Net library to handle focusing of open windows.

Sometime we need to focus running process windows programatically. 

```c#
HandleFocus f = new HandleFocus();
f.FocusByTitle("notepad"); //focusByTitle use to set focus by program title.

```

```c#
HandleFocus f = new HandleFocus();
f.Focus("notepad"); //Simple "focus" use to set focus by process name.
```

