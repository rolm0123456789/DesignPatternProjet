namespace Projet;

public abstract class Creator
{
    protected readonly IInterventionSubject _subject;

    protected Creator(IInterventionSubject subject)
    {
        _subject = subject;
    }

    public abstract Intervention FactoryMethod();
}

class MaintenanceInterventionCreator : Creator
{
    public MaintenanceInterventionCreator(IInterventionSubject subject) : base(subject) { }

    public override Intervention FactoryMethod()
    {
        var intervention = new MaintenanceIntervention();
        _subject.Notify(intervention, "Intervention maintenance créée");
        return intervention;
    }
}

class UrgenceInterventionCreator : Creator
{
    public UrgenceInterventionCreator(IInterventionSubject subject) : base(subject) { }

    public override Intervention FactoryMethod()
    {
        var intervention = new UrgenceIntervention();
        _subject.Notify(intervention, "Intervention urgence créée");
        return intervention;
    }
}


public enum TypeIntervention
{
    Maintenance,
    Urgence
}



public class InterventionFactory
{
    private readonly IInterventionSubject _subject;

    public InterventionFactory(IInterventionSubject subject)
    {
        _subject = subject;
    }

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
