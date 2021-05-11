namespace Behaviours.Menus
{
    public class MenuTransitionBackMenuAction : MenuAction
    {
        public MenuTransitioner transitioner;

        public override void Perform()
        {
            transitioner.TransitionBack();
        }
    }
}