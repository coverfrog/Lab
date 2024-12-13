using System;
using System.Collections.Generic;
using UnityEngine;

namespace ParkGolf
{
    [Serializable]
    public class ScoreGroup
    {
        [SerializeField] private List<Score> scores = new List<Score>();

        private Dictionary<ScoreName, Score> _scoreDict;

        public void Init()
        {
            // todo : score list => dict

            #region < Score List To Dict >

            if (scores != null)
            {
                _scoreDict = new Dictionary<ScoreName, Score>();
                
                var missingCount = 0;
                
                foreach (var score in scores)
                {
                    if (score == null)
                    {
                        missingCount++;
                        continue;
                    }

                    if (_scoreDict.ContainsKey(score))
                    {
                        missingCount++;
                        continue;
                    }

                    _scoreDict.Add(score, score);
                }
                
                if (missingCount <= 0)
                {
                    return;
                }

#if UNITY_EDITOR
                Debug.Log($"score to dict [ missing count : {missingCount} ]");
#endif
            }

            else
            {
#if UNITY_EDITOR
                Debug.Log("score list is null");
#endif
            }
            
            #endregion
        }
    }
}
