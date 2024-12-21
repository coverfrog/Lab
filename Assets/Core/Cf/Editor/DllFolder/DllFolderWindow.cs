#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DllFolder
{
    public class DllFolderWindow : EditorWindow
    {
        private const string StartName = "Cf.";
        private const string CopyDirectory = "Cf_Dlls";

        [MenuItem("Cf/Dll/Folder Clear")]
        private static void FolderClear()
        {
            // todo : get root
            var rootPath = Application.dataPath[..^6];
            
            // todo : define path
            var targetPath = Path.Combine(rootPath, CopyDirectory);

            if (!Directory.Exists(targetPath))
            {
                return;
            }
            
            Directory.Delete(targetPath, true);
            Directory.CreateDirectory(targetPath);
        }

        [MenuItem("Cf/Dll/Folder Update")]
        private static void FolderUpdate()
        {
            // todo : get root
            var rootPath = Application.dataPath[..^6];
            
            // todo : define path
            var dllSourceDirectoryPath = Path.Combine(
                rootPath, 
                "Library", 
                "ScriptAssemblies");
            
            // todo : dll target paths
            var dllSourcePaths = Directory.GetFiles(dllSourceDirectoryPath, "*.dll");
            var dllTargetPaths = new List<string>();
            
            foreach (var dllPath in dllSourcePaths)
            {
                var dllName = Path.GetFileNameWithoutExtension(dllPath);

                if (!dllName.StartsWith(StartName))
                {
                    continue;
                }
                
                dllTargetPaths.Add(dllPath);
            }
            
            // todo : define path
            var copyPath = Path.Combine(
                rootPath,
                CopyDirectory);
            
            // todo : check folder
            if (!Directory.Exists(copyPath))
            {
                Directory.CreateDirectory(copyPath);
            }
            
            // todo : copy
            foreach (var dllPath in dllTargetPaths)
            {
                var dllName = Path.GetFileName(dllPath);
                var destination = Path.Combine(copyPath, dllName);

                File.Copy(dllPath, destination, true);
            }
        }

        [MenuItem("Cf/Dll/Folder Open")]
        private static void FolderOpen()
        {
            var rootPath = Application.dataPath[..^6];
            var copyPath = Path.Combine(rootPath, CopyDirectory);

            if (!Directory.Exists(copyPath))
            {
                Directory.CreateDirectory(copyPath);
            }
            
            EditorUtility.RevealInFinder($"{copyPath}\\");
        }
        
        [MenuItem("Cf/Dll/Folder Open With Update", false, 1)]
        private static void FolderOpenWithUpdate()
        {
            FolderUpdate();
            FolderOpen();
        }
    }
}

#endif