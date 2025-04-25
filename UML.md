# Diagrammes UML du Projet

## 1. Pattern Factory Method

```mermaid
classDiagram
    class InterventionFactory {
        +createIntervention() : Intervention
    }
    class MaintenanceFactory {
        +createIntervention() : Intervention
    }
    class UrgenceFactory {
        +createIntervention() : Intervention
    }
    class Intervention {
        +Id : string
        +Nom : string
        +Demande : string
        +Demandeur : Personne
        +Responsable : Personne
        +Demandeur : Personne
        +DateDemande : DateTime
        +DateRealisation : DateTime
        +Lieu : string
        +Statut : StatutIntervention
        +InterventionAnnuler()
        +InterventionSave()
        +InterventionTerminer()
        +AssignerTechnicien()
        +ToString()
    }
    InterventionFactory <|-- MaintenanceFactory
    InterventionFactory <|-- UrgenceFactory
    MaintenanceFactory ..> Intervention
    UrgenceFactory ..> Intervention
```

## 2. Pattern Decorator

```mermaid
classDiagram
    class Intervention {
        +Id : string
        +Nom : string
        +Demande : string
        +Demandeur : Personne
        +Responsable : Personne
        +Demandeur : Personne
        +DateDemande : DateTime
        +DateRealisation : DateTime
        +Lieu : string
        +Statut : StatutIntervention
        +InterventionAnnuler()
        +InterventionSave()
        +InterventionTerminer()
        +AssignerTechnicien()
        +ToString()
    }
    class InterventionDecorator {
        -intervention : Intervention
        +getDescription() : string
    }
    class SuiviGPSDecorator {
        -coordonnees : string
        +ToString() : string
    }
    class PiecesJointesDecorator {
        -pieceJointe : string
        +ToString() : string
    }
    Intervention <|-- InterventionDecorator
    InterventionDecorator <|-- SuiviGPSDecorator
    InterventionDecorator <|-- PiecesJointesDecorator
```

## 3. Pattern Facade

```mermaid
classDiagram
    class GestionnaireInterventions {
        +creerIntervention()
        +assignerTechnicien()
        +sauvegarder()
        +AfficherInterventions()
        +AnnulerIntervention()
    }
    class InterventionFactory
    class Technicien
    class Intervention
    GestionnaireInterventions --> InterventionFactory
    GestionnaireInterventions --> Technicien
    GestionnaireInterventions --> Intervention
```

## 4. Pattern Observer

```mermaid
classDiagram
    class IObserver {
        +Update(notifier : IInterventionSubject)
    }
    class IInterventionSubject {
        +Attach(observer : IObserver)
        +Detach(observer : IObserver)
        +Notify(intervention : Intervention, message : string)
    }
    class InterventionNotifier {
        -observers : List~IObserver~
        +Attach(observer : IObserver)
        +Detach(observer : IObserver)
        +Notify(intervention : Intervention, message : string)
    }
    class ConsoleLogger {
        +Update(notifier : IInterventionSubject)
    }
    class EmailNotificationObserver {
        +Destinataires : List~string~
        +Update(notifier : IInterventionSubject)
    }
    class Intervention {
        +Id : string
        +Nom : string
        +Demande : string
        +Demandeur : Personne
        +Responsable : Personne
        +Demandeur : Personne
        +DateDemande : DateTime
        +DateRealisation : DateTime
        +Lieu : string
        +Statut : StatutIntervention
        +InterventionAnnuler()
        +InterventionSave()
        +InterventionTerminer()
        +AssignerTechnicien()
        +ToString()
    }

    IInterventionSubject <|.. InterventionNotifier
    IObserver <|.. ConsoleLogger
    IObserver <|.. EmailNotificationObserver
    InterventionNotifier --> IObserver
    InterventionNotifier --> Intervention
```

## 5. Pattern Proxy

```mermaid
classDiagram
    class IGestionnaireInterventions {
        +creerIntervention()
        +modifierIntervention()
    }
    class GestionnaireInterventions {
        +creerIntervention()
        +modifierIntervention()
    }
    class ProxyGestionnaire {
        -gestionnaire : GestionnaireInterventions
        -verifierDroits(action : string) : bool
        +creerIntervention()
        +modifierIntervention()
    }
    IGestionnaireInterventions <|.. GestionnaireInterventions
    IGestionnaireInterventions <|.. ProxyGestionnaire
    ProxyGestionnaire --> GestionnaireInterventions
```
