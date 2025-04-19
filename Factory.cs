namespace Projet;

public abstract class Creator
{
    public abstract Intervention FactoryMethod();
}

class MaintenanceInterventionCreator : Creator
{
    public override Intervention FactoryMethod()
    {
        return new MaintenanceIntervention();
    }
}

class UrgenceInterventionCreator : Creator
{
    public override Intervention FactoryMethod()
    {
        return new UrgenceIntervention();
    }
}

public enum TypeIntervention
{
    Maintenance,
    Urgence
}



public class InterventionFactory
{
    public static Intervention CreateIntervention(TypeIntervention type)
    {
        Creator creator;
        switch (type)
        {
            case TypeIntervention.Urgence:
                creator = new UrgenceInterventionCreator();
                break;
            case TypeIntervention.Maintenance:
                creator = new MaintenanceInterventionCreator();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        return creator.FactoryMethod();
    }
}