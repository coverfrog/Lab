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

        [Header("Additive")] 
        [SerializeField] private List<SceneField> additiveSceneList;

        private void Start()
        {
            StartCoroutine(SceneLoader.AsyncLoad(additiveSceneList, null, null));

           
        }
    }
}
