using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Golf
{
    public static class PythonUtil
    {
        public static bool GetIsPythonInstalled()
        {
            var isRun = RunPython("--version", out string output, out string error);

            if (!isRun)
            {
                return false;
            }

            var pattern = @"Python ([A-Za-z0-9]+(\.[A-Za-z0-9]+)+)";
            
            // Python x.x.(int) 
            // Python x.x.(string)
            
            Debug.Log($"output : {output}, error : {error}");

            return !string.IsNullOrEmpty(output) && Regex.IsMatch(output, pattern);
            
            // Check Env
        }

        public static bool InitPythonEnv(string envPath)
        {
            return Directory.Exists(envPath) || RunPython($"-m venv \"{envPath}\"", out _, out _);
        }

        public static bool InitPythonModules(string modules)
        {
            return RunPython($"-m pip install {modules}", out _, out _);
        }
        
        private static bool RunPython(string arguments, out string output, out string error)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = false
                };

                using var process = Process.Start(psi);

                if (process == null)
                {
                    output = string.Empty;
                    error = string.Empty;

                    return false;
                }

                process.WaitForExit();

                output = process.StandardOutput.ReadToEnd();
                error = process.StandardError.ReadToEnd();
                
                return true;
            }

            catch
            {
                // ignore
            }

            output = string.Empty;
            error = string.Empty;

            return false;
        }
    }
}