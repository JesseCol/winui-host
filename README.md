This is a basic example of how a WinUI3 app can host a ContentIsland containing a system visual.

Caveats:
* Pointer and keyboard input won't flow to the child island.
* The child island content will always be z-order above all the WinUI3 content.
* No support for attaching UIA tree of child island to WinUI3 content.