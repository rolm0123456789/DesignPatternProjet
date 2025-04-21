namespace Projet;

public class GestionnaireInterventions
{
    private readonly IInterventionSubject _sujet;
    private readonly InterventionFactory _factory;

    public GestionnaireInterventions(IInterventionSubject sujet, InterventionFactory factory)
    {
        _sujet = sujet;
        _factory = factory;

        // Attache ici si ce n'est pas déjà fait globalement
        _sujet.Attach(new ConsoleLogger());
    }

    public IIntervention CreerIntervention(TypeIntervention type)
    {
        var intervention = _factory.CreateIntervention(type);
        _sujet.Notify((Intervention)intervention, "Nouvelle intervention créée.");
        return intervention;
    }

    public void AssignerTechnicien()
    {
        // TODO
    }

    public void Sauvegarder()
    {
        // TODO
    }
}
