namespace GameAudio
{
    public class PauseState : IGameState
    {
        public void Menu(IGameContext context)
        {
        }

        public void Gameplay(IGameContext context)
        {
        }

        public void GameplayDanger(IGameContext context)
        {
        }

        public void Pause(IGameContext context)
        {
            context.SetState(new PauseState());
        }
    }
}