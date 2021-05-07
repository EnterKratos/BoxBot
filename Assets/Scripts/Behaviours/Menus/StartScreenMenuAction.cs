namespace Behaviours.Menus
{
    public class StartScreenMenuAction : MenuAction
    {
        public MenuTransitioner transitioner;
        public Menu targetMenu;

        public override void Perform()
        {
            transitioner.TransitionTo(targetMenu);
        }
    }
}