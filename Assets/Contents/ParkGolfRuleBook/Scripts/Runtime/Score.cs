using UnityEngine;

namespace ParkGolf
{
    [CreateAssetMenu(menuName = "Park Golf/Score", fileName = "Score_")]
    public class Score : ScriptableObject
    {
        [SerializeField] private ScoreName scoreName;
        [SerializeField] private int scoreValue;
        [SerializeField] [TextArea(10, 20)] private string description;

        public int ScoreValue => scoreValue;

        public static implicit operator int(Score score)
        {
            return score.scoreValue;
        }

        public static implicit operator ScoreName(Score score)
        {
            return score.scoreName;
        }
    }
}
