using Game.VisorDeDialogosSystem;
using Gameplay;
using SystemOfExtras.GlobalInformationPath;
using SystemOfExtras.SavedData;
using UnityEngine;

namespace SystemOfExtras
{
    public class Installer : MonoBehaviour
    {
        [SerializeField] private DialogSystem dialogSystem;
        [SerializeField] private LoadSceneService ladSceneM;
        [SerializeField] private AudioService audioService;
        [SerializeField] private int cityBuilding, roulette, shop, healthEnemyBase, damageEnemyBase;
        [SerializeField] private float increment;
        private void Awake()
        {
            if (FindObjectsOfType<Installer>().Length > 1)
            {
                Destroy(gameObject);
                return;
            }
            QualitySettings.vSyncCount = 2;
            ServiceLocator.Instance.RegisterService<IDialogSystem>(dialogSystem);
            ServiceLocator.Instance.RegisterService<ILoadScene>(ladSceneM);
            ServiceLocator.Instance.RegisterService<IServiceOfMissions>(new ServiceOfMissions());
            ServiceLocator.Instance.RegisterService<ISaveData>(new SaveData());
            var global = new GlobalInformation(
                cityBuilding,
                roulette,
                shop,
                healthEnemyBase,
                damageEnemyBase,
                increment
            );
            ServiceLocator.Instance.RegisterService<IGlobalInformation>(global);
            ServiceLocator.Instance.RegisterService<IStatsInformation>(global);
            ServiceLocator.Instance.RegisterService<IShopService>(global);
            ServiceLocator.Instance.RegisterService<IRouletteService>(new RouletteAwards());
            ServiceLocator.Instance.RegisterService<IAudioService>(audioService);
            DontDestroyOnLoad(gameObject);
        }
    }
}