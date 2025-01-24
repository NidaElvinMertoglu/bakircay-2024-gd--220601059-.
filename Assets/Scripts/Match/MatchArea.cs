using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Match
{
    public class MatchArea : MonoBehaviour
    {
        public GameObject currentObject;

        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _leftObjectPlacement;
        [SerializeField] private Transform _rightObjectPlacement;

        [SerializeField] private ScoreManager scoreManager; // Reference to the ScoreManager

        private readonly string objectTag = "Moveable";
        private Coroutine matchCoroutine;

        private readonly int _lidOpenHash = Animator.StringToHash("LidOpen");
        private readonly int _lidCloseHash = Animator.StringToHash("LidClose");

        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody == null || other.attachedRigidbody.CompareTag(objectTag) == false)
                return;

            if (other.gameObject == currentObject)
                return;

            if (currentObject == null)
            {
                SetCurrentObject(other);
            }
            else
            {
                if (ChechMatch(other))
                    return;

                other.attachedRigidbody.AddForce(Vector3.up * 15 + Vector3.forward * 15f, ForceMode.Impulse);
            }
        }

        private bool ChechMatch(Collider other)
        {
            if (matchCoroutine != null)
            {
                return false;
            }

            var currentItem = currentObject.GetComponent<Item>();
            var otherItem = other.attachedRigidbody.gameObject.GetComponent<Item>();
            if (!currentItem.IsMatching(otherItem))
                return false;

            other.attachedRigidbody.isKinematic = true;
            matchCoroutine = StartCoroutine(MatchCoroutine(currentItem, otherItem)); // Pass the current and other item to the coroutine
            return true;
        }

        private IEnumerator MatchCoroutine(Item currentItem, Item otherItem)
        {
            float lidOpenDuration = 0.5f;
            float lidCloseDuration = 0.5f;
            float objectMovementDuration = 1f;

            yield return null;

            currentItem.SetCollidersActive(false);
            otherItem.SetCollidersActive(false);

            // iki objeyi de yerine yerleþtir
            DOTween.Kill(currentItem.transform, true);
            DOTween.Kill(otherItem.transform, true);

            otherItem.transform.DOMove(_rightObjectPlacement.position, lidOpenDuration);
            otherItem.transform.DORotate(_rightObjectPlacement.rotation.eulerAngles, lidOpenDuration);

            // kapak açýlma animasyonunu baþlat
            _animator.SetTrigger(_lidOpenHash);
            yield return new WaitForSeconds(lidOpenDuration);

            // objeleri merkeze al, aþaðýya doðru kaydýr
            Vector3 targetPos = (currentItem.transform.position + otherItem.transform.position) / 2f;

            currentItem.transform.DOMove(targetPos, objectMovementDuration);
            otherItem.transform.DOMove(targetPos, objectMovementDuration);

            yield return new WaitForSeconds(objectMovementDuration);

            targetPos = targetPos + Vector3.down * 2f;
            currentItem.transform.DOMove(targetPos, objectMovementDuration);
            otherItem.transform.DOMove(targetPos, objectMovementDuration);

            yield return new WaitForSeconds(objectMovementDuration);

            // kapama animasyonunu baþlat
            _animator.SetTrigger(_lidCloseHash);
            yield return new WaitForSeconds(lidCloseDuration);

            // objeleri yok et  
            if (currentItem != null && otherItem != null)
            {
                currentItem.gameObject.SetActive(false);
                otherItem.gameObject.SetActive(false);

                GameEvents.OnItemMatched?.Invoke(currentItem.itemData);
            }

            // Update the score after the match, using itemScore from ItemData
            int scoreForMatch = currentItem.itemData.itemScore + otherItem.itemData.itemScore; // Add the score from both items
            scoreManager.AddScore(scoreForMatch); // Update score

            currentObject = null;
            matchCoroutine = null;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.attachedRigidbody == null || other.attachedRigidbody.CompareTag(objectTag) == false)
                return;
            if (other.attachedRigidbody.gameObject == currentObject)
            {
                DOTween.Kill(currentObject.transform);
                currentObject = null;
            }
        }

        private void SetCurrentObject(Collider other)
        {
            other.attachedRigidbody.isKinematic = true;
            currentObject = other.attachedRigidbody.gameObject;

            var tweenDuration = 1f;

            currentObject.transform
                .DORotate(_leftObjectPlacement.rotation.eulerAngles, tweenDuration);
            currentObject.transform.DOMove(_leftObjectPlacement.position, tweenDuration);
        }
    }
}
