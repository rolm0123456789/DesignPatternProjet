namespace Projet;

/// <summary>
/// Représente une personne avec un nom, un prénom et un rôle spécifique.
/// </summary>
public class Personne
{
    /// <summary>
    /// Obtient ou définit le nom de la personne.
    /// </summary>
    public required string Nom { get; set; }

    /// <summary>
    /// Obtient ou définit le prénom de la personne.
    /// </summary>
    public required string Prenom { get; set; }

    /// <summary>
    /// Obtient ou définit le rôle de la personne.
    /// </summary>
    public Role Role { get; set; }

    /// <summary>
    /// Retourne une chaîne de caractères représentant la personne.
    /// </summary>
    /// <returns>Une chaîne au format "Nom Prénom - Rôle".</returns>
    public override string ToString()
    {
        return $"{Nom} {Prenom} - {Role}";
    }
}

/// <summary>
/// Enumération des rôles possibles pour une personne.
/// </summary>
public enum Role
{
    /// <summary>
    /// Responsable de la maintenance.
    /// </summary>
    ResponsableMaintenance,

    /// <summary>
    /// Manager de l'équipe.
    /// </summary>
    Manager,

    /// <summary>
    /// Technicien effectuant les interventions.
    /// </summary>
    Technicien,

    /// <summary>
    /// Visiteur sans rôle spécifique.
    /// </summary>
    Visiteur
}