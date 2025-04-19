namespace Projet;

public interface IObserver
{
    void Update(ISubject interventionNotifieurSujet);
}

public interface ISubject
{
    void Attach(IObserver observer);

    void Detach(IObserver observer);

    void Notify();
}


public class ConsoleLogger : IObserver
{
    public void Update(ISubject subject)
    {
        if (subject is InterventionNotifierSujet interventionNotifieurSujet)
        {

            Console.WriteLine("ConsoleLogger: Subject's state has changed to: " + interventionNotifieurSujet.Itervention);
        }
    }
}

public class EmailNotifiactionObserver : IObserver
{
    public List<string> Personnes = new List<string>();
    public void Update(ISubject subject)
    {
        if (subject is InterventionNotifierSujet interventionNotifieurSujet)
        {
            if (interventionNotifieurSujet.Itervention != null && interventionNotifieurSujet.Itervention is UrgenceIntervention intervention)
            {
                Console.WriteLine("Envoie d'un email a : " + intervention.Demandeur + "Intervention" + intervention.Nom + " " + interventionNotifieurSujet.Message);
            }
        }
    }
}

public class InterventionNotifierSujet : ISubject
{
    public Intervention? Itervention { get; set; }
    public string? Message { get; set; }
    private List<IObserver> _observers = new List<IObserver>();

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.Update(this);
        }
    }

    public void Notify(Intervention itv, string message)
    {
        Itervention = itv;
        Message = message;
        Notify();
    }
}
