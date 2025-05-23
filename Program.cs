﻿namespace Projet;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Point d'entrée principal de l'application.
/// Configure les services, initialise les observateurs et gère les interactions utilisateur.
/// </summary>
class Program
{
    /// <summary>
    /// Méthode principale de l'application.
    /// Configure les services, initialise les observateurs et gère les interactions utilisateur via la console.
    /// </summary>
    static void Main()
    {
        // Configuration des services
        var services = new ServiceCollection();
        services.AddSingleton<IInterventionSubject, InterventionNotifier>();
        services.AddTransient<InterventionFactory>();
        services.AddTransient<GestionnaireInterventions>();

        var provider = services.BuildServiceProvider();

        // Ajout d'observateurs sur InterventionNotifier
        var notifier = provider.GetRequiredService<IInterventionSubject>();
        notifier.Attach(new ConsoleLogger());
        notifier.Attach(new EmailNotificationObserver());

        var gestionnaire = provider.GetRequiredService<GestionnaireInterventions>();

        // Initialisation des listes de données
        List<Personne> personnes = new List<Personne>();
        List<Intervention> interventions = new List<Intervention>();
        List<SuiviGPSDecorator> interventionsAvecCordonne = new List<SuiviGPSDecorator>();

        // Ajout de personnes
        personnes.Add(new Personne
        {
            Nom = "Dupont",
            Prenom = "Jean",
            Role = Role.ResponsableMaintenance
        });

        personnes.Add(new Personne
        {
            Nom = "Martin",
            Prenom = "Pierre",
            Role = Role.Technicien
        });

        personnes.Add(new Personne
        {
            Nom = "Durand",
            Prenom = "Marie",
            Role = Role.Visiteur
        });

        // Création d'interventions initiales
        interventions.Add((Intervention)gestionnaire.CreerIntervention(TypeIntervention.Urgence, "Intervention 1", personnes[0], personnes[1]));
        interventions.Add((Intervention)gestionnaire.CreerIntervention(TypeIntervention.Maintenance, "Intervention 2", personnes[1], personnes[2]));

        // Gestion de la connexion utilisateur
        Personne personneConnecter = null;
        do
        {
            Console.WriteLine("Entrez votre nom :");
            string? nom = Console.ReadLine();
            if (string.IsNullOrEmpty(nom))
            {
                Console.WriteLine("Nom invalide. Veuillez réessayer.");
            }
            else
            {
                personnes.FirstOrDefault(p => p.Nom == nom);
                if (personneConnecter != null)
                {
                    Console.WriteLine($"Bonjour {personneConnecter.Nom} !");
                }
                else
                {
                    Console.WriteLine("Personne non trouvée. Veuillez réessayer.");
                }
            }
        } while (personneConnecter != null);

        // Menu principal
        while (true)
        {
            Console.WriteLine("Que voulez-vous faire ?");
            Console.WriteLine("[1] Afficher les interventions en cours");
            Console.WriteLine("[2] Afficher les personnes en cours");
            Console.WriteLine("[3] Créer une intervention");
            Console.WriteLine("[4] Ajouter une fonctionnalité à une intervention");
            Console.WriteLine("[5] Assigner un technicien à une intervention");
            Console.WriteLine("[6] Sauvegarder une intervention");
            Console.WriteLine("[7] Quitter");

            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            int choix = keyInfo.KeyChar - '0';

            switch (choix)
            {
                case 1:
                    // Affiche les interventions en cours
                    Console.WriteLine("Interventions :");
                    gestionnaire.AfficherInterventions(interventions);
                    Console.WriteLine("Interventions avec coordonnées GPS :");
                    gestionnaire.AfficherInterventions(interventionsAvecCordonne);
                    break;
                case 2:
                    // Affiche les personnes enregistrées
                    Console.WriteLine("Personnes :");
                    foreach (var personne in personnes)
                    {
                        Console.WriteLine(personne);
                    }
                    break;
                case 3:
                    // Création d'une nouvelle intervention
                    Console.WriteLine("Création d'une nouvelle intervention...");
                    interventions.Add((Intervention)gestionnaire.CreerIntervention());
                    break;
                case 4:
                    // Ajout d'une fonctionnalité à une intervention existante
                    Console.WriteLine("Ajout d'une fonctionnalité à une intervention...");
                    Console.WriteLine("Entrez l'ID de l'intervention :");
                    string idIntervention = Console.ReadLine();
                    Intervention interventionToUpdate = interventions.FirstOrDefault(i => i.Id == idIntervention);
                    if (interventionToUpdate != null)
                    {
                        Console.WriteLine("Entrez la fonctionnalité à ajouter :");
                        Console.WriteLine("1. Coordonnee GPS");
                        Console.WriteLine("2. Chemin pieces jointes");
                        ConsoleKeyInfo keyInfoFonctionalite = Console.ReadKey(intercept: true);
                        int choixFonctionalite = keyInfoFonctionalite.KeyChar - '0';
                        switch (choixFonctionalite)
                        {
                            case 1:
                                Console.WriteLine("Entrez la localisation :");
                                string? localisation = Console.ReadLine();
                                if (localisation == null || !localisation.Contains(","))
                                {
                                    Console.WriteLine("Localisation invalide.");
                                    break;
                                }
                                // Créer un décorateur pour ajouter la fonctionnalité de suivi GPS
                                SuiviGPSDecorator itv = new SuiviGPSDecorator(interventionToUpdate);
                                itv.CoordonneeGps = (decimal.Parse(localisation.Split(",")[0]), decimal.Parse(localisation.Split(",")[1]));
                                interventions.Remove(interventionToUpdate);
                                interventionsAvecCordonne.Add(itv);
                                break;
                            case 2:
                                Console.WriteLine("Entrez le chemin des pièces jointes :");
                                string? CheminPieceJointe = Console.ReadLine();
                                interventionToUpdate.CheminPieceJointe = CheminPieceJointe;
                                break;
                            default:
                                Console.WriteLine("Fonctionnalité invalide.");
                                break;
                        }

                    }
                    else
                    {
                        Console.WriteLine("Intervention non trouvée.");
                    }
                    break;
                case 5:
                    // Assignation d'un technicien à une intervention
                    Console.WriteLine("Assignation d'un technicien à une intervention...");
                    Console.WriteLine("Entrez l'ID de l'intervention :");
                    string idInterventionTechnicien = Console.ReadLine();
                    Intervention interventionToAssign = interventions.FirstOrDefault(i => i.Id == idInterventionTechnicien);
                    if (interventionToAssign != null)
                    {
                        gestionnaire.SetProxy(personneConnecter, interventionToAssign);
                        Console.WriteLine("Entrez le nom du technicien :");
                        string? nomTechnicien = Console.ReadLine();
                        Personne technicien = personnes.FirstOrDefault(p => p.Nom == nomTechnicien && p.Role == Role.Technicien);
                        if (technicien != null)
                        {
                            gestionnaire.AssignerTechnicien(interventionToAssign, technicien);
                        }
                        else
                        {
                            Console.WriteLine("Technicien non trouvé.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Intervention non trouvée.");
                    }

                    break;
                case 6:
                    // Sauvegarde d'une intervention
                    Console.WriteLine("Sauvegarde de l'intervention...");
                    Console.WriteLine("Entrez l'ID de l'intervention :");
                    string idInterventionSave = Console.ReadLine();
                    Intervention interventionToSave = interventions.FirstOrDefault(i => i.Id == idInterventionSave);
                    if (interventionToSave != null)
                    {
                        gestionnaire.SetProxy(personneConnecter, interventionToSave);
                        gestionnaire.Sauvegarder(interventionToSave);
                    }
                    else
                    {
                        Console.WriteLine("Intervention non trouvée.");
                    }
                    break;
                case 7:
                    // Quitte l'application
                    Console.WriteLine("Au revoir !");
                    return;
                default:
                    Console.WriteLine("Choix invalide. Veuillez réessayer.");
                    break;
            }
        }
    }
}