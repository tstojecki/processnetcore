using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace ProcessNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var process = new Process()
            {
                StartInfo = GetByPlatform()
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
        }
        
        static ProcessStartInfo GetByPlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var cmdArgs = @"powershell .\shell\powershell\expand.ps1 -source 'data' -target 'data'".Replace("\"", "\\\"");
                return new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c \"{cmdArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };
            }
            else
            {
                var dir = Directory.GetCurrentDirectory();
                return new ProcessStartInfo
                {
                    FileName = "sh",
                    Arguments = $"{string.Concat(dir, "/shell/bash/expand.sh")} data data",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };
            }
        }
    }
}
