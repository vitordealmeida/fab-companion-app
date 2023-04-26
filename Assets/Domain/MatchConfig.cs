using System;

namespace Domain
{
    [Serializable]
    public struct MatchConfig
    {
        [NonSerialized]
        public HeroConfig player;
        [NonSerialized]
        public HeroConfig opponent;
        public TimeSpan MatchDuration;
    }
}