namespace Projet;

public class GestionnaireInterventions
{
    private InterventionSujet _sujet = new InterventionSujet();
    
    public GestionnaireInterventions()
    {
        _sujet.Attacher(new ConsoleLogger());
    }

    public IIntervention CreerIntervention(TypeIntervention type)
    {
        var intervention = InterventionFactory.CreateIntervention(type);
        _sujet.Notifier("Nouvelle intervention créée.");
        return intervention;
    }
    public void AssignerTechnicien()
    {

    }

    public void Sauvegarder()
    {

    }
}
