namespace Projet;

class Program
{
    static void Main()
    {
        GestionnaireInterventions gestionnaire = new GestionnaireInterventions();
        
        IIntervention intervention = gestionnaire.CreerIntervention(new MaintenanceFactory());
        intervention = new SuiviGPSDecorator(intervention);
        
        IInterventionProxy proxy = new InterventionProxy(intervention, true);
        proxy.Executer();
    }
}