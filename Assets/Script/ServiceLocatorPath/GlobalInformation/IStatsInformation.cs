using Hexagons;

namespace SystemOfExtras.GlobalInformationPath
{
    public interface IStatsInformation
    {
        int GetHealth();
        int GetDamage();
        void CalculateStatsForEnemy(Hexagon hexagon);
        void SetCenter(Hexagon hexagon);
        int GetEnemyHealth();
        int GetEnemyDamage();
    }
}