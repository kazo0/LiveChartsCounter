using LiveChartsCore.SkiaSharpView.WinUI;
using Uno.Extensions.Markup.Generator;

[assembly: GenerateMarkupForAssembly(typeof(CartesianChart))]

namespace LiveChartsCounter;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.DataContext(new BindableMainModel(), (page, vm) => page
            .Background(ThemeResource.Get<Brush>("ApplicationPageBackgroundThemeBrush"))
            .Content(
                new StackPanel()
                    .VerticalAlignment(VerticalAlignment.Center)
                    .Children(
                        new PolarChart()
                            .Height(400)
                            .Width(400)
                            .Series(() => vm.Countable.CountSeries),
                        new TextBox()
                            .Margin(12)
                            .HorizontalAlignment(HorizontalAlignment.Center)
                            .TextAlignment(Microsoft.UI.Xaml.TextAlignment.Center)
                            .PlaceholderText("Step Size")
                            .Text(x => x.Binding(() => vm.Countable.Step).TwoWay()),
                        new TextBlock()
                            .Margin(12)
                            .HorizontalAlignment(HorizontalAlignment.Center)
                            .TextAlignment(Microsoft.UI.Xaml.TextAlignment.Center)
                            .Text(() => vm.Countable.Count, txt => $"Counter: {txt}"),
                        new Button()
                            .Margin(12)
                            .HorizontalAlignment(HorizontalAlignment.Center)
                            .Command(() => vm.IncrementCommand)
#if __WASM__ 
                        .Content("Increment Counter by Step Size ON WASM")
#elif HAS_UNO_SKIA
                        .Content("Increment Counter by Step Size ON SKIA")
#else
                        .Content("Increment Counter by Step Size ON EVERYTHING ELSE")
#endif
                    )
            )
        );
    }
}
