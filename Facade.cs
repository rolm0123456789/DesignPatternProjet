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

    public IIntervention CreerIntervention()
    {
        TypeIntervention typeInterventionInput;
        bool inputValide;
        do
        {
            Console.WriteLine("Entrez le type d'intervention (Maintenance/Urgence) :");
            string? input = Console.ReadLine();

            inputValide = Enum.TryParse(input, true, out typeInterventionInput)
                          && Enum.IsDefined(typeof(TypeIntervention), typeInterventionInput);

            if (!inputValide)
            {
                Console.WriteLine("Type invalide. Veuillez entrer 'Maintenance' ou 'Urgence'.");
            }

        } while (!inputValide);

        var intervention = _factory.CreateIntervention(typeInterventionInput);
        _sujet.Notify(intervention, "Nouvelle intervention créée par l'utilisateur.");
        return intervention;
    }

    public IIntervention CreerIntervention(TypeIntervention typeInterventionInput, string nom, Personne demandeur, Personne responsable)
    {
        var intervention = _factory.CreateIntervention(typeInterventionInput);
        intervention.Nom = nom;
        intervention.Demandeur = demandeur;
        intervention.Responsable = responsable;
        intervention.DateDemande = DateTime.Now;
        _sujet.Notify(intervention, "Nouvelle intervention créée par le systeme.");
        return intervention;
    }

    public void AssignerTechnicien(Intervention intervention, Personne technicien)
    {
        intervention.Responsable = technicien;
        _sujet.Notify(intervention, "Technicien assigné à l'intervention.");
    }

    public void Sauvegarder(Intervention intervention)
    {
        intervention.InterventionSave();
        _sujet.Notify(intervention, "Intervention sauvegardée.");
    }

    public void AfficherInterventions(IEnumerable<IIntervention> interventions)
    {
        foreach (var intervention in interventions)
        {
            Console.WriteLine(intervention);
        }
    }
}
