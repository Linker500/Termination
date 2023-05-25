//Credit to Mijyuoon for this class

using System.Diagnostics;

public static class TermInfo {
    public static (int, int) GetSize() =>
        Environment.OSVersion.Platform switch {
            PlatformID.Unix => GetSize_Linux(),
            PlatformID.Win32NT => GetSize_Windows(),
            _ => throw new PlatformNotSupportedException("cannot get terminal size on this platform"),
        };

    private static (int, int) GetSize_Linux() {
        var processInfo = new ProcessStartInfo {
            FileName = "stty",
            Arguments = "size",
            RedirectStandardOutput = true,
        };

        var process = Process.Start(processInfo);
        if (process is null) {
            throw new InvalidOperationException("could not start `stty`");
        }

        process.WaitForExit();
        if (process.ExitCode != 0) {
            throw new InvalidOperationException($"`stty` exited with error {process.ExitCode}");
        }

        var output = process.StandardOutput.ReadLine();
        if (String.IsNullOrEmpty(output)) {
            throw new InvalidOperationException("`stty` returned no output");
        }

        var values = output.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var height = Int32.Parse(values[0]);
        var width = Int32.Parse(values[1]);
        return (width, height);
    }

    private static (int, int) GetSize_Windows() =>
        (Console.WindowWidth, Console.WindowHeight);
}