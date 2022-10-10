namespace SystemOfExtras
{
    public interface IServiceOfMissions
    {
        bool IsActiveMission(IdMissions idMissions);
        void AddMission(IdMissions idMissions);
        void MissionCompleted(IdMissions idMissions);
    }
}