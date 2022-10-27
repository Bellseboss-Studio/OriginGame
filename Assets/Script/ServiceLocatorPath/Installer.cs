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
        [SerializeField] private MixerManager mixerManager;
        private void Awake()
        {
            if (FindObjectsOfType<Installer>().Length > 1)
            {
                Destroy(gameObject);
                return;
            }
            ServiceLocator.Instance.RegisterService<IDialogSystem>(dialogSystem);
            ServiceLocator.Instance.RegisterService<ILoadScene>(ladSceneM);
            ServiceLocator.Instance.RegisterService<IServiceOfMissions>(new ServiceOfMissions());
            ServiceLocator.Instance.RegisterService<ISaveData>(new SaveData());
            ServiceLocator.Instance.RegisterService<IGlobalInformation>(new GlobalInformation());
            ServiceLocator.Instance.RegisterService<IRouletteService>(new RouletteAwards());
            ServiceLocator.Instance.RegisterService(mixerManager);
            DontDestroyOnLoad(gameObject);
        }
    }
}