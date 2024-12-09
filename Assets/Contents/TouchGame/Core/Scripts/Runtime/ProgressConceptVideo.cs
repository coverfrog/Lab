using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Cf.TouchGame.Template0
{
    public class ProgressConceptVideo : Progress<ProgressType>
    {
        [SerializeField] private RawImage screen;
        [SerializeField] private VideoPlayer player;
        
        protected override ProgressType TypeName => ProgressType.ConceptVideo;

        private void OnEnable()
        {
            Utils.Util.Video.Play(ref screen, ref player);
        }
    }
}
