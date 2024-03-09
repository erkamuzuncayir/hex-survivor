using NaughtyAttributes;
using UnityEngine;

namespace _Script.MapGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private MapDisplay _display;
        public int MapWidth;
        public int MapHeight;
        public float NoiseScale;
        public bool AutoUpdate;
        
        [Button()]
        public void GenerateMap()
        {
            float[,] noiseMap = Noise.GenerateNoiseMap(MapWidth, MapHeight, NoiseScale);

            _display.DrawNoiseMap(noiseMap);
            foreach (var VARIABLE in noiseMap)
            {
                Debug.Log(VARIABLE);
            }
        }
    }
}