using System;

namespace Domain
{
    [Serializable]
    public struct MatchConfig
    {
        public HeroConfig player;
        public HeroConfig opponent;
        public TimeSpan MatchDuration;
    }
}