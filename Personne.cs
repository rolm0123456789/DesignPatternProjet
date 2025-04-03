namespace Projet;

public class Personne
{
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public Role? Role { get; set; }
}

enum Role
{
    Admin,
    Manager,
    Technicien,
    Visiteur
}