using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Behaviours
{
    [RequireComponent(typeof(TrailRenderer))]
    public class Laser : MonoBehaviour
    {
        private Vector3 originalPosition;
        private TrailRenderer trailRenderer;

        private void Awake()
        {
            originalPosition = transform.position;
            trailRenderer = GetComponent<TrailRenderer>();
        }

        public void Fire(Vector3 target, float speed, Action onComplete)
        {
            trailRenderer.emitting = true;

            var tweener = gameObject.transform.DOMove(target, speed);
            tweener.OnComplete(() =>
            {
                StartCoroutine(End());
                onComplete();
            });
        }

        private IEnumerator End()
        {
            trailRenderer.emitting = false;
            yield return new WaitForSeconds(1f);
            transform.position = originalPosition;
        }
    }
}