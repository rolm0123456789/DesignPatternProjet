namespace Projet;

public interface IInterventionProxy
{
    void Executer();
}

public class InterventionProxy : IInterventionProxy
{
    private readonly IIntervention _intervention;
    private readonly bool _estAutorise;

    public InterventionProxy(IIntervention intervention, bool estAutorise)
    {
        _intervention = intervention;
        _estAutorise = estAutorise;
    }

    public void Executer()
    {
        if (_estAutorise)
            _intervention.Executer();
        else
            Console.WriteLine("Accès refusé.");
    }
}