using System;

namespace Domain
{
    public struct MatchConfig
    {
        public HeroConfig player;
        public HeroConfig opponent;
        public TimeSpan MatchDuration;
    }
}