namespace Projet;

/// <summary>
///     Classe abstraite représentant une intervention générique.
///     Contient les propriétés et méthodes communes à toutes les interventions.
/// </summary>
public abstract class Intervention : IIntervention
{
    /// <summary>
    ///     Identifiant unique de l'intervention.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    ///     Nom de l'intervention.
    /// </summary>
    public string? Nom { get; set; }

    /// <summary>
    ///     Description ou demande associée à l'intervention.
    /// </summary>
    public string? Demande { get; set; }

    /// <summary>
    ///     Personne ayant demandé l'intervention.
    /// </summary>
    public Personne? Demandeur { get; set; }

    /// <summary>
    ///     Technicien ou responsable assigné à l'intervention.
    /// </summary>
    public Personne? Responsable { get; set; }

    /// <summary>
    ///     Date à laquelle la demande d'intervention a été effectuée.
    /// </summary>
    public DateTime DateDemande { get; set; }

    /// <summary>
    ///     Date prévue ou effective de réalisation de l'intervention.
    /// </summary>
    public DateTime DateRealisation { get; set; }

    /// <summary>
    ///     Lieu où l'intervention doit être réalisée.
    /// </summary>
    public string? Lieu { get; set; }

    /// <summary>
    ///     Statut actuel de l'intervention.
    /// </summary>
    public StatutIntervention Statut { get; protected set; } = StatutIntervention.Cree;

    /// <summary>
    ///     Annule l'intervention en modifiant son statut.
    /// </summary>
    public virtual void InterventionAnnuler()
    {
        Statut = StatutIntervention.Annulee;
    }

    /// <summary>
    ///     Sauvegarde l'intervention en modifiant son statut.
    /// </summary>
    public virtual void InterventionSave()
    {
        Statut = StatutIntervention.Valide;
    }

    /// <summary>
    ///     Termine l'intervention en modifiant son statut.
    /// </summary>
    public virtual void InterventionTerminer()
    {
        Statut = StatutIntervention.Terminee;
    }

    /// <summary>
    ///     Assigne un technicien à l'intervention.
    /// </summary>
    /// <param name="technicien">Le technicien à assigner.</param>
    public void AssignerTechnicien(Personne technicien)
    {
        Responsable = technicien;
    }

    /// <summary>
    ///     Retourne une chaîne de caractères représentant l'intervention.
    /// </summary>
    /// <returns>Une chaîne contenant les informations principales de l'intervention.</returns>
    public override string ToString()
    {
        var baseInfo =
            $"Id: {Id}, Nom: {Nom}, Demandeur: {Demandeur?.Nom}, Responsable: {Responsable?.Nom}, Date Demande: {DateDemande}, Lieu: {Lieu}, Statut: {Statut}";
        return string.IsNullOrEmpty(this.CheminPieceJointe)
            ? baseInfo
            : $"{baseInfo}, Chemin pièce jointe: {this.CheminPieceJointe}";
    }
}

/// <summary>
///     Interface définissant les actions possibles sur une intervention.
/// </summary>
public interface IIntervention
{
    /// <summary>
    ///     Termine l'intervention.
    /// </summary>
    void InterventionTerminer();

    /// <summary>
    ///     Sauvegarde l'intervention.
    /// </summary>
    void InterventionSave();

    /// <summary>
    ///     Annule l'intervention.
    /// </summary>
    void InterventionAnnuler();

    /// <summary>
    ///     Assigne un technicien à l'intervention.
    /// </summary>
    /// <param name="technicien">Le technicien à assigner.</param>
    void AssignerTechnicien(Personne technicien);
}

/// <summary>
///     Classe représentant une intervention de maintenance.
/// </summary>
public class MaintenanceIntervention : Intervention
{
    /// <summary>
    ///     Annule l'intervention de maintenance.
    /// </summary>
    public override void InterventionAnnuler()
    {
    }

    /// <summary>
    ///     Sauvegarde l'intervention de maintenance.
    /// </summary>
    public override void InterventionSave()
    {
    }

    /// <summary>
    ///     Termine l'intervention de maintenance et modifie son statut à "Clôturée".
    /// </summary>
    public override void InterventionTerminer()
    {
        base.InterventionTerminer();
        Statut = StatutIntervention.Cloturee;
    }
}

/// <summary>
///     Classe représentant une intervention d'urgence.
/// </summary>
public class UrgenceIntervention : Intervention, IIntervention
{
    /// <summary>
    ///     Date limite pour réaliser l'intervention d'urgence.
    /// </summary>
    public DateTime DateLimite { get; set; }

    /// <summary>
    ///     Annule l'intervention d'urgence.
    /// </summary>
    public override void InterventionAnnuler()
    {
    }

    /// <summary>
    ///     Sauvegarde l'intervention d'urgence.
    /// </summary>
    public override void InterventionSave()
    {
    }

    /// <summary>
    ///     Termine l'intervention d'urgence et notifie le responsable maintenance.
    /// </summary>
    public override void InterventionTerminer()
    {
        base.InterventionTerminer();
        Statut = StatutIntervention.Terminee;
        // Notifier le responsable maintenance
    }
}

/// <summary>
///     Enumération des différents statuts possibles pour une intervention.
/// </summary>
public enum StatutIntervention
{
    /// <summary>
    ///     Intervention créée mais non encore validée.
    /// </summary>
    Cree,

    /// <summary>
    ///     Intervention validée.
    /// </summary>
    Valide,

    /// <summary>
    ///     Intervention en attente de traitement.
    /// </summary>
    EnAttente,

    /// <summary>
    ///     Intervention en cours de réalisation.
    /// </summary>
    EnCours,

    /// <summary>
    ///     Intervention annulée.
    /// </summary>
    Annulee,

    /// <summary>
    ///     Intervention terminée.
    /// </summary>
    Terminee,

    /// <summary>
    ///     Intervention clôturée.
    /// </summary>
    Cloturee
}