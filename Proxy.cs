namespace Projet;

public class InterventionProxy : IIntervention
{
    private readonly IIntervention _realIntervention;
    private readonly Personne _utilisateur;

    public InterventionProxy(IIntervention realIntervention, Personne utilisateur)
    {
        _realIntervention = realIntervention;
        _utilisateur = utilisateur;
    }

    public void InterventionSave()
    {
        if (EstAutorise())
        {
            _realIntervention.InterventionSave();
        }
        else
        {
            Console.WriteLine("Accès refusé : utilisateur non autorisé à sauvegarder l'intervention.");
        }
    }

    public void InterventionAnnuler()
    {
        if (EstAutorise())
        {
            _realIntervention.InterventionAnnuler();
        }
        else
        {
            Console.WriteLine("Accès refusé : utilisateur non autorisé à annuler l'intervention.");
        }
    }

    public void InterventionTerminer()
    {
        if (EstAutorise())
        {
            _realIntervention.InterventionTerminer();
        }
        else
        {
            Console.WriteLine("Accès refusé : utilisateur non autorisé à terminer l'intervention.");
        }
    }

    public void AssignerTechnicien(Personne technicien)
    {
        if (EstAutorise())
        {
            _realIntervention.AssignerTechnicien(technicien);
        }
        else
        {
            Console.WriteLine("Accès refusé : utilisateur non autorisé à assigner un technicien.");
        }
    }


    private bool EstAutorise()
    {
        // Exemple simple : vérifier un rôle
        return _utilisateur.Role == Role.ResponsableMaintenance;
    }
}
