namespace Projet;


public abstract class InterventionFactory : Intervention
{
    public abstract IIntervention CreerIntervention();
}

public class MaintenanceFactory : InterventionFactory
{
    public override IIntervention CreerIntervention() => new MaintenanceIntervention();
}

public class UrgenceFactory : InterventionFactory
{
    public override IIntervention CreerIntervention() => new UrgenceIntervention();
}