using UnityEngine;

namespace Golf
{
    public class PythonTest : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            bool isPythonInstalled = PythonUtil.GetIsPythonInstalled();
            UnityEngine.Debug.Log($"Is Python Installed: {isPythonInstalled}");
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
