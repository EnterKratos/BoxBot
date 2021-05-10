using UnityEngine;
using UnityEngine.SceneManagement;

namespace Behaviours
{
    public class LoadSceneOnAwake : MonoBehaviour
    {
        public Scene scene;
        public LoadSceneMode mode;

        private void Awake()
        {
            SceneManager.LoadScene((int)scene, mode);
        }
    }
}