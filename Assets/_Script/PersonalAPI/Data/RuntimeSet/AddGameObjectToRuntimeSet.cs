using UnityEngine;

namespace _Script.PersonalAPI.Data.RuntimeSet
{
    public class AddGameObjectToRuntimeSet : MonoBehaviour
    {
        public GameObjectRuntimeSet GameObjectRuntimeSet;

        private void OnEnable()
        {
            GameObjectRuntimeSet.AddToList(gameObject);
        }

        private void OnDisable()
        {
            GameObjectRuntimeSet.RemoveFromList(gameObject);
        }
    }
}