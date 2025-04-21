namespace Projet;

public class Personne
{
    public required string Nom { get; set; }
    public required string Prenom { get; set; }
    public Role Role { get; set; }

    public override string ToString()
    {
        return $"{Nom} {Prenom} - {Role}";
    }
}

public enum Role
{
    ResponsableMaintenance,
    Manager,
    Technicien,
    Visiteur
}