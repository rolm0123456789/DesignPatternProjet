namespace Projet;

/// <summary>
/// Proxy pour gérer les droits d'accès aux interventions.
/// Implémente le pattern Proxy pour vérifier si un utilisateur a les autorisations nécessaires
/// avant d'exécuter des actions sur une intervention réelle.
/// </summary>
public class InterventionProxy : IIntervention
{
    private readonly IIntervention _realIntervention; // Référence à l'intervention réelle.
    private readonly Personne _utilisateur; // Utilisateur effectuant l'action.

    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="InterventionProxy"/>.
    /// </summary>
    /// <param name="realIntervention">L'intervention réelle à protéger.</param>
    /// <param name="utilisateur">L'utilisateur effectuant les actions.</param>
    public InterventionProxy(IIntervention realIntervention, Personne utilisateur)
    {
        _realIntervention = realIntervention;
        _utilisateur = utilisateur;
    }

    /// <summary>
    /// Sauvegarde l'intervention si l'utilisateur est autorisé.
    /// </summary>
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

    /// <summary>
    /// Annule l'intervention si l'utilisateur est autorisé.
    /// </summary>
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

    /// <summary>
    /// Termine l'intervention si l'utilisateur est autorisé.
    /// </summary>
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

    /// <summary>
    /// Assigne un technicien à l'intervention si l'utilisateur est autorisé.
    /// </summary>
    /// <param name="technicien">Le technicien à assigner.</param>
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

    /// <summary>
    /// Vérifie si l'utilisateur a les autorisations nécessaires pour effectuer une action.
    /// </summary>
    /// <returns>True si l'utilisateur est autorisé, sinon False.</returns>
    private bool EstAutorise()
    {
        // Exemple simple : vérifier si l'utilisateur a le rôle de ResponsableMaintenance.
        return _utilisateur.Role == Role.ResponsableMaintenance;
    }
}