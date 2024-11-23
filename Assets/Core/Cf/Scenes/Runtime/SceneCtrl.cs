using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Cf.Scenes
{
    public sealed class SceneCtrl : MonoBehaviour
    {
        [Title("Text")]
        [SerializeField] private string codeName;
        [SerializeField] [TextArea] private string description;

        [Title("Additive")] 
        [SerializeField] private List<SceneField> additiveSceneList;
    }
}
