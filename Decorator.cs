using System.Runtime.CompilerServices;

namespace Projet;

public abstract class InterventionDecorator : IIntervention
{
    protected IIntervention _intervention;
    public InterventionDecorator(IIntervention intervention) => _intervention = intervention;

    public void InterventionAnnuler() => _intervention.InterventionAnnuler();

    public void InterventionSave() => _intervention.InterventionSave();
    public virtual void InterventionTerminer() => _intervention.InterventionTerminer();
}

public class SuiviGPSDecorator : InterventionDecorator
{
    public SuiviGPSDecorator(IIntervention intervention) : base(intervention) { }
    
    public (decimal, decimal) CoordonneeGps { get; set; } = (0, 0);

    public override string ToString()
    {
        return $"{_intervention} - Coordonnée GPS : {CoordonneeGps.Item1}, {CoordonneeGps.Item2}";
    }
}

public static class InterventionPiecesJointeExts
{
    private static ConditionalWeakTable<Intervention, PieceJointeData> _extensionData = new ConditionalWeakTable<Intervention, PieceJointeData>();
    private class PieceJointeData
    {
        public string? cheminPieceJointe { get; set; }
    }

    private static PieceJointeData GetPieceJointeData(Intervention intervention)
    {
        return _extensionData.GetValue(intervention, _ => new PieceJointeData());
    }

    extension(Intervention intervention)
    {
        public string? CheminPieceJointe
        {
            get => GetPieceJointeData(intervention).cheminPieceJointe;
            set => GetPieceJointeData(intervention).cheminPieceJointe = value;
        }
    }
}