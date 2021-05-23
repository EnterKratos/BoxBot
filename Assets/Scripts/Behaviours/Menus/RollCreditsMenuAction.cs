using UnityEngine.SceneManagement;

namespace Behaviours.Menus
{
    public class RollCreditsMenuAction : MenuAction
    {
        public override void Perform()
        {
            SceneManager.LoadScene((int)Scene.Credits);
        }
    }
}