namespace GameAudio
{
    public class GameplayState : IGameState
    {
        public void Menu(IGameContext context)
        {
            
        }

        public void Gameplay(IGameContext context)
        {
            context.SetState(new GameplayState());
        }

        public void GameplayDanger(IGameContext context)
        {
           
        }

        public void Pause(IGameContext context)
        {
           
        }
    }
}