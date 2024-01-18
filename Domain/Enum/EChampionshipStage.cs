using System.ComponentModel;

namespace Domain.Enum
{
    public enum EChampionshipStage
    {
        [Description("Quartas de Final")]
        QuarterFinals = 0,

        [Description("Semi Final")]
        SemiFinals = 1,

        [Description("Disputa de 3º Lugar")]
        ThirdPlacePlayoff = 2,

        [Description("Final")]
        Final = 3,
    }
}
