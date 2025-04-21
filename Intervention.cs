namespace Projet;

public abstract class Intervention : IIntervention
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Nom { get; set; }
    public string? Demande { get; set; }
    public Personne? Demandeur { get; set; }
    public Personne? Responsable { get; set; }
    public DateTime DateDemande { get; set; }
    public DateTime DateRealisation { get; set; }
    public string? Lieu { get; set; }
    public StatutIntervention Statut { get; protected set; } = StatutIntervention.Cree;

    public virtual void InterventionAnnuler()
    {
        Statut = StatutIntervention.Annulee;
    }

    public virtual void InterventionSave()
    {
        Statut = StatutIntervention.Valide;
    }

    public virtual void InterventionTerminer()
    {
        Statut = StatutIntervention.Terminee;
    }

    public override string ToString()
    {
        var baseInfo = $"Id: {Id}, Nom: {Nom}, Demandeur: {Demandeur?.Nom}, Responsable: {Responsable?.Nom}, Date Demande: {DateDemande}, Lieu: {Lieu}, Statut: {Statut}";
        return string.IsNullOrEmpty(this.CheminPieceJointe)
            ? baseInfo
            : $"{baseInfo}, Chemin pièce jointe: {this.CheminPieceJointe}";
    }
}
public interface IIntervention
{
    public void InterventionTerminer();
    public void InterventionSave();
    public void InterventionAnnuler();
}

public class MaintenanceIntervention : Intervention
{
    public override void InterventionAnnuler()
    {
    }

    public override void InterventionSave()
    {
    }

    public override void InterventionTerminer()
    {
        base.InterventionTerminer();
        Statut = StatutIntervention.Cloturee;
    }
}

public class UrgenceIntervention : Intervention, IIntervention
{
    public DateTime DateLimite { get; set; }

    public override void InterventionAnnuler()
    {
    }

    public override void InterventionSave()
    {

    }

    public override void InterventionTerminer()
    {
        base.InterventionTerminer();
        Statut = StatutIntervention.Terminee;
        // Notifier le responsable maintenance
    }
}

public enum StatutIntervention
{
    Cree,
    Valide,
    EnAttente,
    EnCours,
    Annulee,
    Terminee,
    Cloturee
}