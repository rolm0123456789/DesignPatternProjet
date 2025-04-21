namespace Projet;

public abstract class Intervention : IIntervention
{
    public string? Nom { get; set; }
    public string? Demande { get; set; }
    public Personne? Demandeur { get; set; }
    public Personne? Responsable { get; set; }
    public DateTime DateDemande { get; set; }
    public DateTime DateRealisation { get; set; }
    public string? Lieu { get; set; }
    public StatutIntervention Statut { get; protected set; }

    public virtual void InterventionAnnuler()
    {
        // Notifier le responsable maintenance si il a pas le droit sinon notifier que c'est annuler le responsable et le demandeur si c'est pas lui qui l'a annuler
        //changer le statut
    }

    public virtual void InterventionSave()
    {
        // Notifier le responsable maintenance si il a pas le droit sinon le responsable qu'il a une nouvelle intervention  
        //changer le statut
    }

    public virtual void InterventionTerminer()
    {
        // Notifier le responsable maintenance et le demandeur 
        //changer le statut
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
    EnAttente,
    EnCours,
    Annulee,
    Terminee,
    Cloturee
}