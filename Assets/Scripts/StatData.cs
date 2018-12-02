using System;

namespace DefaultNamespace
{
    [Serializable]
    public class StatData
    {
        public StatData(string username, int score)
        {
            this.username = username;
            this.score = score;
        }

        public StatData()
        {
        }

        public string username;
        public int score;
    }
}