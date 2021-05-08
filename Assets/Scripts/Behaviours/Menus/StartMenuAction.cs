using System.Collections;

namespace Behaviours.Menus
{
    public class StartMenuAction : MenuAction
    {
        public Scene scene;
        private bool loadLevel;
        private IEnumerator coroutine;

        private void Awake()
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            coroutine = SceneHelpers.LoadSceneInBackground(scene, () => loadLevel);
            StartCoroutine(coroutine);
        }

        public override void Perform()
        {
            loadLevel = true;
        }
    }
}