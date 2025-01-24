using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match
{
    public class MixSkill : MonoBehaviour
    {
        private ItemSpawner _itemSpawner;

        public void Initialize(ItemSpawner itemSpawner)
        {
            _itemSpawner = itemSpawner;
        }

        public void UseMixSkill()
        {
            if (_itemSpawner == null)
            {
                Debug.LogError("ItemSpawner is not initialized.");
                return;
            }

            if (_itemSpawner.spawnedObjects.Count == 0)
            {
                Debug.LogWarning("No items to shuffle.");
                return;
            }
            Debug.Log($"SpawnedObjects Count: {_itemSpawner.spawnedObjects.Count}");

            ShuffleItems();

        }

        private void ShuffleItems()
        {
            // Karýþtýrma iþlemi
            foreach (var item in _itemSpawner.spawnedObjects)
            {
                Vector3 randomPosition = GetRandomPosition();
                item.position = randomPosition;
            }
        }

        private Vector3 GetRandomPosition()
        {
            // Karýþtýrýlacak nesne için rastgele pozisyon belirleme
            float xPos = Random.Range(-_itemSpawner.spawnArea.x / 2, _itemSpawner.spawnArea.x / 2);
            float yPos = Random.Range(-_itemSpawner.spawnArea.y / 2, _itemSpawner.spawnArea.y / 2);
            float zPos = Random.Range(-_itemSpawner.spawnArea.z / 2, _itemSpawner.spawnArea.z / 2);

            return new Vector3(xPos, yPos, zPos) + _itemSpawner.transform.position;
        }
    }
}


