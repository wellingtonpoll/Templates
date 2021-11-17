namespace OrleansTemplate.Server;

using System.Runtime.InteropServices;

internal static class HostExtensions
{
    public static void LogApplicationStarted(this IHost host)
    {
        var hostEnvironment = host.Services.GetRequiredService<IHostEnvironment>();
        var logger = host.Services.GetRequiredService<ILogger<Program>>();
        logger.ApplicationStarted(
            hostEnvironment.ApplicationName,
            hostEnvironment.EnvironmentName,
            RuntimeInformation.FrameworkDescription,
            RuntimeInformation.OSDescription);
    }

    public static void LogApplicationStopped(this IHost host)
    {
        var hostEnvironment = host.Services.GetRequiredService<IHostEnvironment>();
        var logger = host.Services.GetRequiredService<ILogger<Program>>();
        logger.ApplicationStopped(
            hostEnvironment.ApplicationName,
            hostEnvironment.EnvironmentName,
            RuntimeInformation.FrameworkDescription,
            RuntimeInformation.OSDescription);
    }

    public static void LogApplicationTerminatedUnexpectedly(this IHost host, Exception exception)
    {
        if (host == null)
        {
            LogToConsole(exception);
        }
        else
        {
            try
            {
                var hostEnvironment = host.Services.GetRequiredService<IHostEnvironment>();
                var logger = host.Services.GetRequiredService<ILogger<Program>>();
                logger.ApplicationTerminatedUnexpectedly(
                    exception,
                    hostEnvironment.ApplicationName,
                    hostEnvironment.EnvironmentName,
                    RuntimeInformation.FrameworkDescription,
                    RuntimeInformation.OSDescription);
            }
            catch
            {
                LogToConsole(exception);
            }
        }

        static void LogToConsole(Exception exception)
        {
            Console.WriteLine($"{AssemblyInformation.Current.Product} terminated unexpectedly.");
            Console.WriteLine(exception.ToString());
        }
    }
}
