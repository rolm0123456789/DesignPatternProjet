namespace Projet;

public class GestionnaireInterventions
{
    private InterventionSujet _sujet = new InterventionSujet();
    
    public GestionnaireInterventions()
    {
        _sujet.Attacher(new ConsoleLogger());
    }

    public IIntervention CreerIntervention(InterventionFactory factory)
    {
        var intervention = factory.CreerIntervention();
        _sujet.Notifier("Nouvelle intervention créée.");
        return intervention;
    }
}
