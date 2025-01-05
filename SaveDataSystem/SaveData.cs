using Microsoft.Xna.Framework; // Use MonoGame's Vector2 if applicable
using System.Collections.Generic;

namespace NEITGameEngine.SaveDataSystem
{
    

    public class SaveData
    {
        public int PlayerScore { get; set; }
        public Vector2 PlayerPosition { get; set; }
        public List<Vector2> EnemyPositions { get; set; }
    }
}
