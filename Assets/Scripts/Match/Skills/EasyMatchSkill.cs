using Match.View;
using UnityEngine;
using DG.Tweening;
using System.Linq;

namespace Match
{
    public class EasyMatchSkill : MonoBehaviour
    {
        [SerializeField] private Transform _leftObjectPlacement;
        [SerializeField] private Transform _rightObjectPlacement;

        public void UseEasyMatchSkill()
        {
            // Bulunabilir öðeleri al
            var movableItems = GameObject.FindGameObjectsWithTag("Moveable");

            if (movableItems.Length == 0)
            {
                Debug.LogWarning("No Moveable items found.");
                return;
            }

            // Rastgele bir öðe seç ve eþleþen bir öðe bul
            var firstItem = movableItems[Random.Range(0, movableItems.Length)];
            var matchID = firstItem.GetComponent<Item>().matchID;
            var secondItem = movableItems
                .Where(item => item.GetComponent<Item>().matchID == matchID && item != firstItem)
                .FirstOrDefault();

            if (secondItem != null)
            {
                // Nesneleri yerleþtir
                PlaceItemAtPosition(firstItem, _leftObjectPlacement);
                PlaceItemAtPosition(secondItem, _rightObjectPlacement);
            }
        }

        private void PlaceItemAtPosition(GameObject item, Transform position)
        {
            item.transform.position = position.position;
            item.transform.rotation = position.rotation;
            item.SetActive(true);

            // Hareket animasyonu
            float tweenDuration = 1f;
            item.transform.DOMove(position.position, tweenDuration);
            item.transform.DORotate(position.rotation.eulerAngles, tweenDuration);
        }
    }
}
