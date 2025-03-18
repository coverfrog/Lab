using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Golf
{
    public static class PythonConst
    {
        private static readonly string EnvDir = Path.Combine(Directory.GetParent(Application.dataPath)?.FullName ?? string.Empty, "Python312");

        public static readonly string PythonFileDir = Path.Combine(EnvDir, "Scripts");
    }

    public enum PythonRunType
    {
        Init,
        Code,
    }
 
    public static class PythonUtil
    {
        #region :: Run

        private static bool RunProcess(string fileName, string arguments, out string output, out string error)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = fileName,
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
                
                output = string.IsNullOrEmpty(output) ? "null" : output;
                error = string.IsNullOrEmpty(error) ? "null" : error;
                
                Debug.LogError($"[Py]\n<Output>\n{output}\n<Error>\n{error}");
                
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
        
        private static bool RunPython(string fileName, string arguments, out string output, out string error)
        {
            var scriptPath = Path.Combine(PythonConst.PythonFileDir, $"{Path.GetFileNameWithoutExtension(fileName)}.py");

            return RunProcess(scriptPath, arguments , out output, out error);
        }

        #endregion
    }
}