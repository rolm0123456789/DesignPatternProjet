namespace Projet;

public interface IObserver
{
    void Update(IInterventionSubject notifier);
}

public class ConsoleLogger : IObserver
{
    public void Update(IInterventionSubject notifier)
    {
        Console.WriteLine($"[ConsoleLogger] Changement d'état : {notifier.Intervention?.Nom} - Message : {notifier.Message}");
    }
}

public class EmailNotificationObserver : IObserver
{
    public List<string> Destinataires = new List<string>();

    public void Update(IInterventionSubject notifier)
    {
        if (notifier.Intervention is UrgenceIntervention intervention)
        {
            Console.WriteLine($"[Email] À : {intervention.Demandeur} | Sujet : {intervention.Nom} | Message : {notifier.Message}");
        }
    }
}

public class InterventionNotifier : IInterventionSubject
{
    public Intervention? Intervention { get; private set; }
    public string? Message { get; private set; }

    private readonly List<IObserver> _observers = new();

    public void Attach(IObserver observer) => _observers.Add(observer);
    public void Detach(IObserver observer) => _observers.Remove(observer);

    public void Notify(Intervention intervention, string message)
    {
        Intervention = intervention;
        Message = message;

        foreach (var observer in _observers)
        {
            observer.Update(this);
        }
    }
}

public interface IInterventionSubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify(Intervention intervention, string message);

    Intervention? Intervention { get; }
    string? Message { get; }
}
