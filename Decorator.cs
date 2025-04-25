using System.Runtime.CompilerServices;

namespace Projet;

/// <summary>
/// Classe abstraite représentant un décorateur pour les interventions.
/// Permet d'ajouter dynamiquement des fonctionnalités à une intervention.
/// </summary>
public abstract class InterventionDecorator : IIntervention
{
    /// <summary>
    /// Référence à l'intervention décorée.
    /// </summary>
    protected IIntervention _intervention;

    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="InterventionDecorator"/>.
    /// </summary>
    /// <param name="intervention">L'intervention à décorer.</param>
    public InterventionDecorator(IIntervention intervention) => _intervention = intervention;

    /// <summary>
    /// Assigne un technicien à l'intervention décorée.
    /// </summary>
    /// <param name="technicien">Le technicien à assigner.</param>
    public void AssignerTechnicien(Personne technicien) => _intervention.AssignerTechnicien(technicien);

    /// <summary>
    /// Annule l'intervention décorée.
    /// </summary>
    public void InterventionAnnuler() => _intervention.InterventionAnnuler();

    /// <summary>
    /// Sauvegarde l'intervention décorée.
    /// </summary>
    public void InterventionSave() => _intervention.InterventionSave();

    /// <summary>
    /// Termine l'intervention décorée.
    /// </summary>
    public virtual void InterventionTerminer() => _intervention.InterventionTerminer();
}

/// <summary>
/// Décorateur ajoutant un suivi GPS à une intervention.
/// </summary>
public class SuiviGPSDecorator : InterventionDecorator
{
    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="SuiviGPSDecorator"/>.
    /// </summary>
    /// <param name="intervention">L'intervention à décorer.</param>
    public SuiviGPSDecorator(IIntervention intervention) : base(intervention) { }

    /// <summary>
    /// Coordonnées GPS associées à l'intervention.
    /// </summary>
    public (decimal, decimal) CoordonneeGps { get; set; } = (0, 0);

    /// <summary>
    /// Retourne une chaîne de caractères représentant l'intervention avec les coordonnées GPS.
    /// </summary>
    /// <returns>Une chaîne contenant les informations de l'intervention et les coordonnées GPS.</returns>
    public override string ToString()
    {
        return $"{_intervention} - Coordonnée GPS : {CoordonneeGps.Item1}, {CoordonneeGps.Item2}";
    }
}

/// <summary>
/// Décorateur ajoutant une pièce jointe à une intervention.
/// </summary>
public class PiecesJointesDecorator : InterventionDecorator
{
    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="PiecesJointesDecorator"/>.
    /// </summary>
    /// <param name="intervention">L'intervention à décorer.</param>
    public PiecesJointesDecorator(IIntervention intervention) : base(intervention) { }

    /// <summary>
    /// Chemin de la pièce jointe associée à l'intervention.
    /// </summary>
    public string CheminPieceJointe { get; set; } = string.Empty;

    /// <summary>
    /// Retourne une chaîne de caractères représentant l'intervention avec la pièce jointe.
    /// </summary>
    /// <returns>Une chaîne contenant les informations de l'intervention et le chemin de la pièce jointe.</returns>
    public override string ToString()
    {
        return $"{_intervention} - Pièce jointe : {CheminPieceJointe}";
    }
}

/// <summary>
/// Classe d'extension pour ajouter des pièces jointes aux interventions.
/// </summary>
public static class InterventionPiecesJointeExts
{
    /// <summary>
    /// Table associative pour stocker les données des pièces jointes des interventions.
    /// </summary>
    private static ConditionalWeakTable<Intervention, PieceJointeData> _extensionData = new ConditionalWeakTable<Intervention, PieceJointeData>();

    /// <summary>
    /// Classe interne pour représenter les données des pièces jointes.
    /// </summary>
    private class PieceJointeData
    {
        /// <summary>
        /// Chemin de la pièce jointe associée à l'intervention.
        /// </summary>
        public string? cheminPieceJointe { get; set; }
    }

    /// <summary>
    /// Obtient les données des pièces jointes pour une intervention donnée.
    /// </summary>
    /// <param name="intervention">L'intervention pour laquelle récupérer les données.</param>
    /// <returns>Les données des pièces jointes associées.</returns>
    private static PieceJointeData GetPieceJointeData(Intervention intervention)
    {
        return _extensionData.GetValue(intervention, _ => new PieceJointeData());
    }

    /// <summary>
    /// Propriété d'extension pour gérer le chemin de la pièce jointe d'une intervention.
    /// </summary>
    /// <param name="intervention">L'intervention cible.</param>
    extension(Intervention intervention)
    {
        public string? CheminPieceJointe
        {
            get => GetPieceJointeData(intervention).cheminPieceJointe;
            set => GetPieceJointeData(intervention).cheminPieceJointe = value;
        }
    }
}