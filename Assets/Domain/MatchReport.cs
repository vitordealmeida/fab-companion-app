using System;
using System.Collections.Generic;

namespace Domain
{
    [Serializable]
    public class MatchReport
    {
        public DateTime MatchDateTime;
        public int finalPlayerLife;
        public int finalOpponentLife;
        public MatchConfig matchInfo;
        public List<MatchEvent> matchEvents;
    }

    [Serializable]
    public class MatchEvent
    {
        public bool isPlayer;
        public int lifeChange;
    }
}