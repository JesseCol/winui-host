# What is this?

This is a basic example of how a WinUI3 app can host a ContentIsland containing a system visual.

Specifically, have a look at [MainWindow.xaml.cs](./WinUIAppWithIsland/MainWindow.xaml.cs) to see how the sample attaches
a System Visual to a WinUI3 scene by using a ChildSiteLink.

Caveats:
* Pointer and keyboard input won't flow to the child island.
* The child island content will always be z-order above all the WinUI3 content.
* No support for attaching UIA tree of child island to WinUI3 content.

Other notes:
* Disconnecting the child ContentIsland from its hosting ChildSiteLink isn't currently supported.  The sample shows how to
remove the child content from the scene by calling childSiteLink.Dispose().