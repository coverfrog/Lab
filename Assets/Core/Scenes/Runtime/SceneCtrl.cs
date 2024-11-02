using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cf.Scenes
{
    public class SceneCtrl : MonoBehaviour
    {
        [Header("Text")]
        [SerializeField] private string codeName;
        [SerializeField] [TextArea] private string description;

        [Header("Load With")] 
        [SerializeField] private List<SceneField> loadWithSceneList;

        private void Start()
        {
            SceneLoader.AsyncLoad(loadWithSceneList, null, null);
        }
    }
}
