namespace Projet;

public class Personne
{
    public required string Nom { get; set; }
    public required string Prenom { get; set; }
    public Role Role { get; set; }
}

public enum Role
{
    ResponsableMaintenance,
    Manager,
    Technicien,
    Visiteur
}