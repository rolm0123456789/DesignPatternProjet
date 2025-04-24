namespace Projet;

public class GestionnaireInterventions
{
    private readonly IInterventionSubject _sujet;
    private readonly InterventionFactory _factory;

    private InterventionProxy? _interventionProxy;

    public GestionnaireInterventions(IInterventionSubject sujet, InterventionFactory factory)
    {
        _sujet = sujet;
        _factory = factory;

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
        if (_interventionProxy != null)
        {
            _interventionProxy.AssignerTechnicien(technicien);
            _sujet.Notify(intervention, "Technicien assigné à l'intervention.");
        }
        else
        {
            Console.WriteLine("Aucun proxy d'intervention initialisé. Impossible de sauvegarder.");
        }
    }

    public void Sauvegarder(Intervention intervention)
    {
        if (_interventionProxy != null)
        {
            _interventionProxy.InterventionSave();
            _sujet.Notify(intervention, "Intervention sauvegardée.");
        }
        else
        {
            Console.WriteLine("Aucun proxy d'intervention initialisé. Impossible de sauvegarder.");
        }
    }

    public void AfficherInterventions(IEnumerable<IIntervention> interventions)
    {
        foreach (var intervention in interventions)
        {
            Console.WriteLine(intervention);
        }
    }

    public void AnnulerIntervention(Intervention intervention)
    {
        if (_interventionProxy != null)
        {
            _interventionProxy.InterventionAnnuler();
            _sujet.Notify(intervention, "Intervention annulée.");
        }
        else
        {
            Console.WriteLine("Aucun proxy d'intervention initialisé. Impossible d'annuler.");
        }
    }

    public void TerminerIntervention(Intervention intervention)
    {
        if (_interventionProxy != null)
        {
            _interventionProxy.InterventionTerminer();
            _sujet.Notify(intervention, "Intervention terminée.");
        }
        else
        {
            Console.WriteLine("Aucun proxy d'intervention initialisé. Impossible de terminer.");
        }
    }
    public void SetProxy(Personne utilisateur, IIntervention realIntervention)
    {
        _interventionProxy = new InterventionProxy(realIntervention, utilisateur);
    }
}
