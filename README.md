# Gestionnaire d'Interventions Techniques

## 📝 Description

Application console permettant de gérer la planification d'interventions techniques en utilisant différents Design Patterns.

## 🚀 Fonctionnalités

- Création de techniciens et d'interventions
- Génération automatique d'interventions types (maintenance, urgence)
- Sauvegarde dans des fichiers simulés ou logs
- Envoi de notifications internes (console/log)
- Gestion des autorisations et des rôles utilisateurs

## 🎨 Design Patterns Utilisés

### 1. Factory Method

- Utilisé pour la création des différents types d'interventions
- Permet de générer des interventions de maintenance et d'urgence
- Chaque intervention contient : date, technicien, durée, lieu

### 2. Decorator

- Implémentation de `SuiviGPSDecorator` et `PiecesJointesDecorator`
- Implémentation de `InterventionPiecesJointeExts`. Utilisation d'une autre façon de faire, cela permet d'ajouter des propriétés à une classe sans la modifier. Le décorateur et l'extension n'ont pas exactement les mêmes fonctionnalités, il faut adapter selon les cas. (ici le décorateur est préférable pour override le `toString`)
- Permet d'enrichir les interventions avec des fonctionnalités supplémentaires
- Maintient la flexibilité du code sans modifier la classe de base

### 3. Facade

- Implémentation de `GestionnaireInterventions`
- Simplifie l'interface du système
- Appelle le proxy pour vérifier les droits
- Méthodes principales :
  - CreerIntervention()
  - AssignerTechnicien()
  - Sauvegarder()

### 4. Observer

- Système de notifications pour les changements d'interventions
- Permet d'informer différents composants (console, logs)
- Maintient un couplage faible entre les composants
- Sujet en injection de dépendance pour n'avoir qu'un sujet sur les notifications

### 5. Proxy

- Gestion des droits d'accès aux fonctionnalités
- Vérifie les autorisations avant l'exécution des actions
- Sécurise l'accès aux fonctionnalités sensibles

## 🛠️ Technologies Utilisées

- C# (.NET)
- Architecture orientée objet
- Design Patterns GOF (Gangs of Four)

## 📦 Installation

1. [Installer .Net 10 preview 3](https://dotnet.microsoft.com/fr-fr/download/dotnet/10.0)
2. Cloner le repository
3. Ouvrir la solution dans Visual Studio Code
4. `dotnet run` pour lancer
5. Installer l'extension Mermaid pour l'UML 

## 🔍 Structure du Projet

- `Factory.cs` : Implémentation des patterns de création
- `Decorator.cs` : Décorateurs pour les interventions
- `Facade.cs` : Façade du système
- `Observer.cs` : Système de notifications
- `Proxy.cs` : Gestion des droits d'accès
- `Intervention.cs` : Classes de base des interventions
- `Program.cs` : Point d'entrée de l'application
