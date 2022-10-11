namespace GameAudio
{
    public class GameplayDangerState : IGameState
    {
        public void Menu(IGameContext context)
        {
            
        }

        public void Gameplay(IGameContext context)
        {
            
        }

        public void GameplayDanger(IGameContext context)
        {
            context.SetState(new GameplayDangerState());
        }

        public void Pause(IGameContext context)
        {
           
        }
    }
}