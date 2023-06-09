using System;
using UnityEngine;

namespace Domain
{
    [Serializable]
    public class HeroConfig: ScriptableObject
    {
        public string name;
        public int lifePoints;
        public Sprite backgroundImage;
        public Color alternativeColor = Color.white;
    }
}