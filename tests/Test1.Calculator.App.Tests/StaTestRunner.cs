using System.Runtime.ExceptionServices;

namespace Test1.Calculator.App.Tests;

internal static class StaTestRunner
{
    private const int JoinTimeoutMilliseconds = 30_000;

    public static void Run(Action action)
    {
        Exception? captured = null;
        var thread = new Thread(() =>
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                captured = ex;
            }
        })
        {
            IsBackground = true,
        };

        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();

        if (!thread.Join(JoinTimeoutMilliseconds))
        {
            throw new TimeoutException(
                $"STA test thread did not complete within {JoinTimeoutMilliseconds / 1000} seconds.");
        }

        if (captured is not null)
        {
            ExceptionDispatchInfo.Capture(captured).Throw();
        }
    }
}