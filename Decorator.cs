namespace Projet;
public abstract class InterventionDecorator : IIntervention
{
    protected IIntervention _intervention;
    public InterventionDecorator(IIntervention intervention) => _intervention = intervention;
    public virtual void Executer() => _intervention.Executer();
}

public class SuiviGPSDecorator : InterventionDecorator
{
    public SuiviGPSDecorator(IIntervention intervention) : base(intervention) {}
    public override void Executer()
    {
        base.Executer();
        Console.WriteLine("Ajout du suivi GPS.");
    }
}

public class PiecesJointesDecorator : InterventionDecorator
{
    public PiecesJointesDecorator(IIntervention intervention) : base(intervention) {}
    public override void Executer()
    {
        base.Executer();
        Console.WriteLine("Ajout de pièces jointes.");
    }
}