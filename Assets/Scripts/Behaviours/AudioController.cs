using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

namespace Behaviours
{
    public class AudioController : MonoBehaviour
    {
        public int poolSize = 5;

        private readonly List<PoolItem<AudioSource>> audioSourcePool = new List<PoolItem<AudioSource>>();
        private static readonly object poolLock = new object();

        private void Awake()
        {
            lock (poolLock)
            {
                for (var i = 0; i < poolSize; i++)
                {
                    var audioSource = gameObject.AddComponent<AudioSource>();
                    audioSource.playOnAwake = false;

                    audioSourcePool.Add(new PoolItem<AudioSource>
                    {
                        Item = audioSource
                    });
                }
            }
        }

        public void Play(AudioClip clip)
        {
            lock (poolLock)
            {
                var source = audioSourcePool.FirstOrDefault(s => s.Available);
                if (source == null)
                {
                    Debug.LogWarning("No available audio sources in the pool. Increase pool size or free up pool items.");
                    return;
                }

                source.Available = false;
                source.Item.clip = clip;
                source.Item.Play();
                StartCoroutine(WaitForClipFinish(source));
            }
        }

        private IEnumerator WaitForClipFinish(PoolItem<AudioSource> item)
        {
            yield return new WaitUntil(() => item.Item.isPlaying == false);

            FreeSource(item);
        }

        private void FreeSource(PoolItem<AudioSource> item)
        {
            lock (poolLock)
            {
                item.Available = true;
            }
        }
    }
}