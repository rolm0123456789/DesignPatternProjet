namespace Projet;

class Program
{
    static void Main()
    {
        var responsable = new Personne
        {
            Nom = "Dupont",
            Prenom = "Jean",
            Role = Role.ResponsableMaintenance
        };
        var technicien = new Personne
        {
            Nom = "Martin",
            Prenom = "Pierre",
            Role = Role.Technicien
        };
        var demandeur = new Personne
        {
            Nom = "Durand",
            Prenom = "Marie",
            Role = Role.Visiteur
        };


        TypeIntervention typeInterventionInput;
        bool inputValide;
        do
        {
            Console.WriteLine("Entrez le type d'intervention (Maintenance/Urgence) :");
            string? input = Console.ReadLine();

            inputValide = Enum.TryParse(input, true, out typeInterventionInput)
                          && Enum.IsDefined(typeof(TypeIntervention), typeInterventionInput);

            if (!inputValide)
            {
                Console.WriteLine("Type invalide. Veuillez entrer 'Maintenance' ou 'Urgence'.");
            }

        } while (!inputValide);

        Intervention itv = InterventionFactory.CreateIntervention(typeInterventionInput);
        Console.WriteLine($"Intervention créée: {itv.GetType().Name}");
        itv.CheminPieceJointe = "chemin/vers/piecejointe.pdf";
    }
}