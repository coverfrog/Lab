using System.IO;
using UnityEngine;

namespace Golf
{
    public static class PythonQgisConst
    {
        private static readonly string[] ModuleNamesArr = new string[]
        {
            "sd", 
            "sd"
        };

        public static readonly string GetModuleNames = string.Join(" ", ModuleNamesArr);

        public static readonly string EnvPath = Path.Combine(Directory.GetParent(Application.dataPath)?.FullName ?? string.Empty, "Qgis Python Env");
    }

    public class PythonQgis : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            PythonUtil.InitPythonEnv(PythonQgisConst.EnvPath);
            // PythonUtil.SetPythonModules(PythonQgisConst.GetModuleNames);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}