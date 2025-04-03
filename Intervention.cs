namespace Projet;

public class Intervention
{
    public string Nom { get; set; }
    public string Demande { get; set; }
    public Personne Demandeur { get; set; }
    public Personne Responsable { get; set; }
    public DateTime DateDemande { get; set; }
    public DateTime DateRealisation { get; set; }
    public string Lieu { get; set; }
    public TimeSpan Pointage { get; set; }
}

public interface IIntervention
{
    void Executer();
}

public class MaintenanceIntervention : IIntervention
{
    public void Executer() => Console.WriteLine("Intervention de maintenance en cours...");
}

public class UrgenceIntervention : IIntervention
{
    public void Executer() => Console.WriteLine("Intervention d'urgence en cours...");
}