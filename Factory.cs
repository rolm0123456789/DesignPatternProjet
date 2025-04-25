namespace Projet;

/// <summary>
///     Classe abstraite représentant un créateur dans le pattern Factory Method.
///     Définit une méthode abstraite pour la création d'interventions.
/// </summary>
public abstract class Creator
{
    /// <summary>
    ///     Sujet utilisé pour notifier les observateurs des changements d'état.
    /// </summary>
    protected readonly IInterventionSubject _subject;

    /// <summary>
    ///     Initialise une nouvelle instance de la classe <see cref="Creator" />.
    /// </summary>
    /// <param name="subject">Le sujet utilisé pour les notifications.</param>
    protected Creator(IInterventionSubject subject)
    {
        _subject = subject;
    }

    /// <summary>
    ///     Méthode abstraite pour créer une intervention.
    /// </summary>
    /// <returns>Une instance d'<see cref="Intervention" />.</returns>
    public abstract Intervention FactoryMethod();
}

/// <summary>
///     Créateur concret pour les interventions de maintenance.
/// </summary>
internal class MaintenanceInterventionCreator : Creator
{
    /// <summary>
    ///     Initialise une nouvelle instance de la classe <see cref="MaintenanceInterventionCreator" />.
    /// </summary>
    /// <param name="subject">Le sujet utilisé pour les notifications.</param>
    public MaintenanceInterventionCreator(IInterventionSubject subject) : base(subject)
    {
    }

    /// <summary>
    ///     Crée une intervention de maintenance et notifie les observateurs.
    /// </summary>
    /// <returns>Une instance de <see cref="MaintenanceIntervention" />.</returns>
    public override Intervention FactoryMethod()
    {
        var intervention = new MaintenanceIntervention();
        _subject.Notify(intervention, "Intervention maintenance créée");
        return intervention;
    }
}

/// <summary>
///     Créateur concret pour les interventions d'urgence.
/// </summary>
internal class UrgenceInterventionCreator : Creator
{
    /// <summary>
    ///     Initialise une nouvelle instance de la classe <see cref="UrgenceInterventionCreator" />.
    /// </summary>
    /// <param name="subject">Le sujet utilisé pour les notifications.</param>
    public UrgenceInterventionCreator(IInterventionSubject subject) : base(subject)
    {
    }

    /// <summary>
    ///     Crée une intervention d'urgence et notifie les observateurs.
    /// </summary>
    /// <returns>Une instance de <see cref="UrgenceIntervention" />.</returns>
    public override Intervention FactoryMethod()
    {
        var intervention = new UrgenceIntervention();
        _subject.Notify(intervention, "Intervention urgence créée");
        return intervention;
    }
}

/// <summary>
///     Enumération des types d'interventions possibles.
/// </summary>
public enum TypeIntervention
{
    /// <summary>
    ///     Intervention de maintenance.
    /// </summary>
    Maintenance,

    /// <summary>
    ///     Intervention d'urgence.
    /// </summary>
    Urgence
}

/// <summary>
///     Fabrique pour créer des interventions en fonction de leur type.
/// </summary>
public class InterventionFactory
{
    /// <summary>
    ///     Sujet utilisé pour notifier les observateurs des changements d'état.
    /// </summary>
    private readonly IInterventionSubject _subject;

    /// <summary>
    ///     Initialise une nouvelle instance de la classe <see cref="InterventionFactory" />.
    /// </summary>
    /// <param name="subject">Le sujet utilisé pour les notifications.</param>
    public InterventionFactory(IInterventionSubject subject)
    {
        _subject = subject;
    }

    /// <summary>
    ///     Crée une intervention en fonction du type spécifié.
    /// </summary>
    /// <param name="type">Le type d'intervention à créer.</param>
    /// <returns>Une instance d'<see cref="Intervention" /> correspondant au type spécifié.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Lancé si le type d'intervention est inconnu.</exception>
    public Intervention CreateIntervention(TypeIntervention type)
    {
        Creator creator = type switch
        {
            TypeIntervention.Maintenance => new MaintenanceInterventionCreator(_subject),
            TypeIntervention.Urgence => new UrgenceInterventionCreator(_subject),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        return creator.FactoryMethod();
    }
}