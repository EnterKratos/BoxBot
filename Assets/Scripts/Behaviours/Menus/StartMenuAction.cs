using System.Collections;

namespace Behaviours.Menus
{
    public class StartMenuAction : MenuAction
    {
        private bool loadLevel;
        private IEnumerator coroutine;

        private void Awake()
        {
            var save = SaveGameHelpers.Load();

            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            var scene = save?.CurrentLevel ?? Scene.Level1;

            coroutine = SceneHelpers.LoadSceneInBackground(scene, () => loadLevel);
            StartCoroutine(coroutine);
        }

        public override void Perform()
        {
            loadLevel = true;
        }
    }
}