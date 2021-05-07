namespace Behaviours.Menus
{
    public class StartMenuAction : MenuAction
    {
        public Scene scene;
        private bool loadLevel;

        private void Awake()
        {
            StartCoroutine(SceneHelpers.LoadSceneInBackground(scene, () => loadLevel));
        }

        public override void Perform()
        {
            loadLevel = true;
        }
    }
}