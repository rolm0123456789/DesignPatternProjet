namespace Projet;

/// <summary>
///     Interface représentant un observateur dans le pattern Observer.
///     Les observateurs implémentent cette interface pour réagir aux notifications.
/// </summary>
public interface IObserver
{
    /// <summary>
    ///     Méthode appelée lorsqu'un changement d'état est notifié par le sujet.
    /// </summary>
    /// <param name="notifier">Le sujet qui notifie les observateurs.</param>
    void Update(IInterventionSubject notifier);
}

/// <summary>
///     Observateur qui affiche les notifications dans la console.
/// </summary>
public class ConsoleLogger : IObserver
{
    /// <summary>
    ///     Réagit à une notification en affichant un message dans la console.
    /// </summary>
    /// <param name="notifier">Le sujet qui notifie les observateurs.</param>
    public void Update(IInterventionSubject notifier)
    {
        Console.WriteLine($"[ConsoleLogger] Changement d'état : {notifier.Intervention?.Nom} - Message : {notifier.Message}");
    }
}

/// <summary>
///     Observateur qui envoie des notifications par email pour les interventions d'urgence.
/// </summary>
public class EmailNotificationObserver : IObserver
{
    /// <summary>
    ///     Liste des destinataires des notifications par email.
    /// </summary>
    public List<string> Destinataires = new();

    /// <summary>
    ///     Réagit à une notification en envoyant un email si l'intervention est une urgence.
    /// </summary>
    /// <param name="notifier">Le sujet qui notifie les observateurs.</param>
    public void Update(IInterventionSubject notifier)
    {
        if (notifier.Intervention is UrgenceIntervention intervention)
            Console.WriteLine($"[Email] À : {intervention.Demandeur} | Sujet : {intervention.Nom} | Message : {notifier.Message}");
    }
}

/// <summary>
///     Sujet dans le pattern Observer, responsable de notifier les observateurs des changements.
/// </summary>
public class InterventionNotifier : IInterventionSubject
{
    /// <summary>
    ///     Liste des observateurs attachés au sujet.
    /// </summary>
    private readonly List<IObserver> _observers = new();

    /// <summary>
    ///     Intervention associée à la notification en cours.
    /// </summary>
    public Intervention? Intervention { get; private set; }

    /// <summary>
    ///     Message associé à la notification en cours.
    /// </summary>
    public string? Message { get; private set; }

    /// <summary>
    ///     Attache un observateur au sujet.
    /// </summary>
    /// <param name="observer">L'observateur à attacher.</param>
    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    /// <summary>
    ///     Détache un observateur du sujet.
    /// </summary>
    /// <param name="observer">L'observateur à détacher.</param>
    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    /// <summary>
    ///     Notifie tous les observateurs d'un changement d'état.
    /// </summary>
    /// <param name="intervention">L'intervention associée à la notification.</param>
    /// <param name="message">Le message de la notification.</param>
    public void Notify(Intervention intervention, string message)
    {
        Intervention = intervention;
        Message = message;

        foreach (var observer in _observers) observer.Update(this);
    }
}

/// <summary>
///     Interface représentant un sujet dans le pattern Observer.
///     Les sujets implémentent cette interface pour gérer les observateurs et les notifications.
/// </summary>
public interface IInterventionSubject
{
    /// <summary>
    ///     Obtient l'intervention associée à la notification en cours.
    /// </summary>
    Intervention? Intervention { get; }

    /// <summary>
    ///     Obtient le message associé à la notification en cours.
    /// </summary>
    string? Message { get; }

    /// <summary>
    ///     Attache un observateur au sujet.
    /// </summary>
    /// <param name="observer">L'observateur à attacher.</param>
    void Attach(IObserver observer);

    /// <summary>
    ///     Détache un observateur du sujet.
    /// </summary>
    /// <param name="observer">L'observateur à détacher.</param>
    void Detach(IObserver observer);

    /// <summary>
    ///     Notifie tous les observateurs d'un changement d'état.
    /// </summary>
    /// <param name="intervention">L'intervention associée à la notification.</param>
    /// <param name="message">Le message de la notification.</param>
    void Notify(Intervention intervention, string message);
}