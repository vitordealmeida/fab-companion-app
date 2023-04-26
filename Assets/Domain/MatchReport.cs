using System;
using System.Collections.Generic;

namespace Domain
{
    [Serializable]
    public class MatchReport
    {
        public DateTime MatchDateTime;
        public int finalTotalLife => playerEvents[^1].totalLifeAfterChange;
        public int finalOpponentLife => opponentEvents[^1].totalLifeAfterChange;
        public MatchConfig matchInfo;
        public List<MatchEvent> playerEvents = new();
        public List<MatchEvent> opponentEvents = new();

        public MatchReport(MatchConfig matchInfo)
        {
            this.matchInfo = matchInfo;
            MatchDateTime = DateTime.Now;
        }

        public MatchEvent AddPlayerEvent(int lifeChange)
        {
            var matchEvent = new MatchEvent
            {
                eventTime = (DateTime.Now - MatchDateTime).TotalSeconds,
                lifeChange = lifeChange,
                totalLifeAfterChange = playerEvents.Count > 0 ? playerEvents[^1].totalLifeAfterChange + lifeChange : matchInfo.player.lifePoints + lifeChange
            };
            playerEvents.Add(matchEvent);
            return matchEvent;
        }

        public MatchEvent AddOpponentEvent(int lifeChange)
        {
            var matchEvent = new MatchEvent
            {
                eventTime = (DateTime.Now - MatchDateTime).TotalSeconds,
                lifeChange = lifeChange,
                totalLifeAfterChange = opponentEvents.Count > 0 ? opponentEvents[^1].totalLifeAfterChange + lifeChange : matchInfo.opponent.lifePoints + lifeChange
            };
            opponentEvents.Add(matchEvent);
            return matchEvent;
        }
    }

    [Serializable]
    public class MatchEvent
    {
        public double eventTime;
        public int lifeChange;
        public int totalLifeAfterChange;
    }
}