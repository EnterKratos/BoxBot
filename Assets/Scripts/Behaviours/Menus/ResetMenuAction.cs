using UnityEngine.SceneManagement;

namespace Behaviours.Menus
{
    public class ResetMenuAction : MenuAction
    {
        public override void Perform()
        {
            SaveGameHelpers.ResetSave();
            SceneManager.LoadScene((int)Scene.MainMenu);
        }
    }
}