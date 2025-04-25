namespace Projet;

/// <summary>
///     Classe responsable de la gestion des interventions.
///     Fournit des méthodes pour créer, afficher, sauvegarder et gérer les interventions.
/// </summary>
public class GestionnaireInterventions
{
    /// <summary>
    ///     Fabrique utilisée pour créer des interventions.
    /// </summary>
    private readonly InterventionFactory _factory;

    /// <summary>
    ///     Sujet utilisé pour notifier les observateurs des changements d'état.
    /// </summary>
    private readonly IInterventionSubject _sujet;

    /// <summary>
    ///     Proxy pour gérer les actions sur une intervention spécifique.
    /// </summary>
    private InterventionProxy? _interventionProxy;

    /// <summary>
    ///     Initialise une nouvelle instance de la classe <see cref="GestionnaireInterventions" />.
    /// </summary>
    /// <param name="sujet">Le sujet utilisé pour les notifications.</param>
    /// <param name="factory">La fabrique utilisée pour créer des interventions.</param>
    public GestionnaireInterventions(IInterventionSubject sujet, InterventionFactory factory)
    {
        _sujet = sujet;
        _factory = factory;

        // Attache un observateur pour les notifications de console
        _sujet.Attach(new ConsoleLogger());
    }

    /// <summary>
    ///     Crée une intervention en demandant à l'utilisateur de spécifier le type.
    /// </summary>
    /// <returns>Une instance d'<see cref="IIntervention" />.</returns>
    public IIntervention CreerIntervention()
    {
        TypeIntervention typeInterventionInput;
        bool inputValide;
        do
        {
            Console.WriteLine("Entrez le type d'intervention (Maintenance/Urgence) :");
            var input = Console.ReadLine();

            inputValide = Enum.TryParse(input, true, out typeInterventionInput)
                          && Enum.IsDefined(typeof(TypeIntervention), typeInterventionInput);

            if (!inputValide) Console.WriteLine("Type invalide. Veuillez entrer 'Maintenance' ou 'Urgence'.");
        } while (!inputValide);

        var intervention = _factory.CreateIntervention(typeInterventionInput);
        _sujet.Notify(intervention, "Nouvelle intervention créée par l'utilisateur.");
        return intervention;
    }

    /// <summary>
    ///     Crée une intervention avec des paramètres spécifiques.
    /// </summary>
    /// <param name="typeInterventionInput">Le type d'intervention.</param>
    /// <param name="nom">Le nom de l'intervention.</param>
    /// <param name="demandeur">La personne ayant demandé l'intervention.</param>
    /// <param name="responsable">La personne responsable de l'intervention.</param>
    /// <returns>Une instance d'<see cref="IIntervention" />.</returns>
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

    /// <summary>
    ///     Assigne un technicien à une intervention.
    /// </summary>
    /// <param name="intervention">L'intervention à laquelle assigner le technicien.</param>
    /// <param name="technicien">Le technicien à assigner.</param>
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

    /// <summary>
    ///     Sauvegarde une intervention.
    /// </summary>
    /// <param name="intervention">L'intervention à sauvegarder.</param>
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

    /// <summary>
    ///     Affiche une liste d'interventions.
    /// </summary>
    /// <param name="interventions">La liste des interventions à afficher.</param>
    public void AfficherInterventions(IEnumerable<IIntervention> interventions)
    {
        foreach (var intervention in interventions) Console.WriteLine(intervention);
    }

    /// <summary>
    ///     Annule une intervention.
    /// </summary>
    /// <param name="intervention">L'intervention à annuler.</param>
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

    /// <summary>
    ///     Termine une intervention.
    /// </summary>
    /// <param name="intervention">L'intervention à terminer.</param>
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

    /// <summary>
    ///     Initialise un proxy pour une intervention spécifique.
    /// </summary>
    /// <param name="utilisateur">L'utilisateur associé au proxy.</param>
    /// <param name="realIntervention">L'intervention réelle à gérer via le proxy.</param>
    public void SetProxy(Personne utilisateur, IIntervention realIntervention)
    {
        _interventionProxy = new InterventionProxy(realIntervention, utilisateur);
    }
}