namespace Projet;

public interface IObservateur
{
    void MettreAJour(string message);
}

public class ConsoleLogger : IObservateur
{
    public void MettreAJour(string message) => Console.WriteLine("LOG: " + message);
}

public class InterventionSujet
{
    private List<IObservateur> _observateurs = new List<IObservateur>();
    public void Attacher(IObservateur observateur) => _observateurs.Add(observateur);
    public void Notifier(string message)
    {
        foreach (var observateur in _observateurs)
            observateur.MettreAJour(message);
    }

    public void Detacher(IObservateur observateur) => _observateurs.Remove(observateur);
}
