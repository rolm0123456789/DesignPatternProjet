using System.Runtime.CompilerServices;

namespace Projet;
public abstract class InterventionDecorator : IIntervention
{
    protected IIntervention _intervention;
    public InterventionDecorator(IIntervention intervention) => _intervention = intervention;
    public virtual void Executer() => _intervention.Executer();
}

public class SuiviGPSDecorator : InterventionDecorator
{
    public SuiviGPSDecorator(IIntervention intervention) : base(intervention) { }
    public override void Executer()
    {
        base.Executer();
        Console.WriteLine("Ajout du suivi GPS.");
    }
}

//public class PiecesJointesDecorator : InterventionDecorator
//{
//    public PiecesJointesDecorator(IIntervention intervention) : base(intervention) {}
//    public override void Executer()
//    {
//        base.Executer();
//        Console.WriteLine("Ajout de pièces jointes.");
//    }
//}

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

    public static void SetCheminPieceJointe(this Intervention intervention, string chemin)
    {
        GetPieceJointeData(intervention).cheminPieceJointe = chemin;
    }
    public static string? GetCheminPieceJointe(this Intervention intervention)
    {
        return GetPieceJointeData(intervention).cheminPieceJointe;
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
        public string CheminPieceJointe
        {
            get => GetPieceJointeData(intervention).cheminPieceJointe;
            set => GetPieceJointeData(intervention).cheminPieceJointe = chemin;
        }
    }
}