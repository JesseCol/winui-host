using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Composition;
using Microsoft.UI.Content;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUIAppWithIsland
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        ChildSiteLink? childSiteLink;
        Windows.UI.Composition.Compositor? systemCompositor;
        Windows.UI.Composition.ContainerVisual? systemContainerVisual;
        Microsoft.UI.Content.ContentIsland? childContentIsland;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            if (childSiteLink != null)
            {
                // We set it up previously, so let's detach.
                childSiteLink.Dispose();
                childSiteLink = null;

                childContentIsland!.Dispose();
                childContentIsland = null;
                return;
            }

            ContainerVisual placementVisual = (ContainerVisual)ElementCompositionPreview.GetElementVisual(this.IslandHostRect);
            childSiteLink = ChildSiteLink.Create(this.Content.XamlRoot.ContentIsland, placementVisual);

            // This is required whem the child is a system Visual:
            childSiteLink.ProcessesKeyboardInput = false;
            childSiteLink.ProcessesPointerInput = false;

            if (systemCompositor == null)
            {
                systemCompositor = new Windows.UI.Composition.Compositor();
            }

            systemContainerVisual = systemCompositor.CreateContainerVisual();

            // Make a nice blue system visual
            var blueBrush = systemCompositor.CreateColorBrush(Windows.UI.Color.FromArgb(150, 10, 10, 200));
            var rectangle = systemCompositor.CreateSpriteVisual();
            rectangle.Brush = blueBrush;
            rectangle.Size = new(200, 200);
            systemContainerVisual.Children.InsertAtTop(rectangle);

            var dq = DispatcherQueue.GetForCurrentThread();
            childContentIsland = ContentIsland.CreateForSystemVisual(dq, systemContainerVisual);

            childSiteLink.Connect(childContentIsland);
        }
    }
}
