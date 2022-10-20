using SystemOfExtras;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralBlackJack : MonoBehaviour
{
    [SerializeField] private Presentations presentations;
    private TeaTime _presentationEnemy, _presentationPlayer, _preparingGame;

    private void Start()
    {
        _presentationEnemy = this.tt().Pause().Add(() =>
        {
            presentations.StartPresentationEnemy();
        }).Loop(handler =>
        {
            if (presentations.FinishEnemyPresentation)
            {
                handler.Break();
            }
        }).Add(() =>
        {
            _presentationPlayer.Play();
        });

        _presentationPlayer = this.tt().Pause().Add(() =>
        {
            presentations.StartPresentationPlayer();
        }).Loop(handler =>
        {
            if (presentations.FinishPlayerPresentation)
            {
                handler.Break();
            }
        }).Add(() =>
        {
            _preparingGame.Play();
        }).Pause();

        _preparingGame = this.tt().Pause().Add(() =>
        {
            presentations.StartPreparingGame();
        }).Loop(handle =>
        {
            if (presentations.FinishPresentation)
            {
                handle.Break();
            }
        }).Add(() =>
        {
            
        }).Pause();

        _presentationEnemy.Play();
        ServiceLocator.Instance.GetService<ILoadScene>().Open(() => { });
    }

    public void GoToCityBuilding()
    {
        ServiceLocator.Instance.GetService<ILoadScene>().Close(() =>
        {
            SceneManager.LoadScene(3);
        });
    }
}