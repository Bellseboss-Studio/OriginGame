namespace GameAudio
{
    public class MenuState : IGameState
    {
        public void Menu(IGameContext context)
        {
            context.SetState(new MenuState());
        }

        public void Gameplay(IGameContext context)
        {
            
        }

        public void GameplayDanger(IGameContext context)
        {
            
        }

        public void Pause(IGameContext context)
        {
            
        }
    }
}