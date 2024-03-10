using UnityEngine;

namespace _Script.Actors
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerDataSO _so_playerData;

        private void Awake()
        {
            Vector3 position = transform.position;
            _so_playerData.PlayerCoord = new Vector3Int((int)position.x, (int)position.y, (int)position.z);
        }
    }
}
