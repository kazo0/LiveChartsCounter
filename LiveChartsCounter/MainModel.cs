using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;

namespace LiveChartsCounter;
internal partial record Countable(int Step, int Count)
{
    private int[] _counts { get; set; } = [Count];

    public ISeries[] CountSeries => [
        new PolarLineSeries<int>
        {
            Values = _counts,
            Fill = null,
        }
    ];

    public Countable Increment() => this with
    {
        Count = Count + Step,
        _counts = [.. _counts, Count + Step]
    };
}

internal partial record MainModel
{
    public IState<Countable> Countable => State.Value(this, () => new Countable(1, 0));

    public ValueTask IncrementCommand() =>
        Countable.UpdateAsync(c => c?.Increment(), CancellationToken.None);
}
