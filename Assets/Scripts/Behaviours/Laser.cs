using System;
using DG.Tweening;
using UnityEngine;

namespace Behaviours
{
    [RequireComponent(typeof(TrailRenderer))]
    public class Laser : MonoBehaviour
    {
        private Transform target;
        private TrailRenderer trailRenderer;

        private void Awake()
        {
            trailRenderer = GetComponent<TrailRenderer>();
        }

        public void Fire(Transform target, float speed, Action onComplete)
        {
            this.target = target;
            trailRenderer.emitting = true;

            var tweener = gameObject.transform.DOMove(target.position, speed);
            tweener.OnComplete(() => onComplete());
        }
    }
}