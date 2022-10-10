namespace GameAudio
{
    public interface IGameState
    {
        void Menu(IGameContext context);
        void Gameplay(IGameContext context);
        void GameplayDanger(IGameContext context);
        void Pause(IGameContext context);
    }
}