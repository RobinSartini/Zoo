# PLAN DE TEST

ZooManager - Système de gestion du Zoo Municipal de Lyon

**Version :** 1.0

**Date :** 2026-06-09

**Statut : Validé**

**Méthodologie :** TDD (Red - Green - Refactor)

**Framework :** xUnit 2.9.0 + FluentAssertions 6.12.0 · .NET 8

# 1. Identification

| **Projet**  | Zoo - Système de gestion du Zoo Municipal de Lyon |
| ----------- | ------------------------------------------------- |
| **Version** | 1.0                                               |
| **Auteur**  | Sartini Robin / Nouali Malcom / Martel Nathan     |
| **Date**    | 2026-06-09                                        |
| **Statut**  | **Validé**                                        |

# 2. Périmètre

## 2.1 In Scope

- Classe ZooManager - toutes les méthodes publiques : AddAnimal, GetAnimal, TotalAnimals, TotalCapacityUsed, CalculateDailyRation, CalculateDailyCost, **CalculateMonthlyCost**, GetCriticalAnimals, **GetAnimalsByCategory**, RemoveAnimal
- Classe Animal et ses propriétés
- Énumérations AnimalCategory (Carnivore, Herbivore, Omnivore) et HealthStatus (Healthy, Sick, Critical)
- Exceptions métier : DuplicateAnimalException, ZooCapacityExceededException
- Règles de calcul des rations et des coûts journaliers
- Règles de capacité (50 animaux, animal Critical = 2 places)
- **[BONUS] Calcul du coût mensuel (30 jours)**
- **[BONUS] Filtrage des animaux par catégorie alimentaire**

## 2.2 Out of Scope

- Persistance en base de données
- Interface utilisateur (IHM)
- API REST / exposition HTTP
- Authentification et gestion des utilisateurs
- Tests de performance et de charge
- Tests de sécurité

# 3. Stratégie de test

## **3.1 Methodologie**

La campagne suit strictement le cycle TDD (Test-Driven Development) :

- RED : écriture du test avant toute ligne de code de production - le test doit être rouge (echec).
- GREEN : implémentation du code minimal pour faire passer le test au vert.
- REFACTOR : amélioration du code sans casser les tests existants.

Chaque exigence donne lieu à au minimum un commit « test rouge » suivi d'un commit « code vert ». L'historique Git doit refléter ce cycle de manière traçable.

## 3.2 Types de tests prévus

- Tests unitaires (xUnit + FluentAssertions) : couverture des 15 exigences fonctionnelles.
- Tests nominaux (happy path) : comportement attendu avec des données valides.
- Tests alternatifs : variantes valides (ex : animal Sick, animal Critical).
- Tests d'erreur (sad path) : entrées invalides, doublons, dépassement de capacité.
- Tests paramétrés [Theory] : pour couvrir les trois catégories d'animaux en une seule méthode.

## **3.3 Traçabilité dans le code**

Chaque test est annoté avec \[Trait("Requirement", "REQ-Z-XXX")\] pour permettre le filtrage :

dotnet test --filter "Requirement=REQ-Z-006"

# 4. Critères d'entrée

- Spécifications métier validées (section IV du sujet TP Zoo).
- Squelette de classes fourni : ZooManager, Animal, AnimalCategory, HealthStatus, exceptions.
- Solution .NET 8 initialisée avec deux projets : Zoo.Domain et Zoo.UnitTests.
- Packages NuGet installés : xUnit 2.9.0, FluentAssertions 6.12.0, coverlet.collector.
- Environnement de développement opérationnel (IDE, CLI dotnet).

# 5. Critères de sortie

- 100 % des **17 exigences** (REQ-Z-001 à REQ-Z-015 + REQ-Z-016 et REQ-Z-017) couvertes par au moins un cas de test.
- Les exigences bonus REQ-Z-016 et REQ-Z-017 respectent le cycle TDD (commit rouge + commit vert documentés).
- Tous les tests passent au vert (0 échec, 0 test ignoré).
- Couverture de lignes >= 95 % sur la classe ZooManager.
- Couverture de branches >= 90 % sur ZooManager.
- Aucun bug bloquant ou critique non résolu.
- Historique Git : au moins un commit rouge + un commit vert par exigence.
- Rapport de test (TEST_REPORT.md) rédigé avec les métriques réelles d'exécution.

# 6. Environnement

| **Composant**                | **Version / Détail**                     |
| ---------------------------- | ---------------------------------------- |
| **.NET SDK**                 | 8.0                                      |
| **Runtime**                  | .NET 8.0                                 |
| **Framework de test**        | xUnit 2.9.0                              |
| **Assertions**               | FluentAssertions 6.12.0                  |
| **Couverture de code**       | coverlet.collector (XPlat Code Coverage) |
| **Rapport HTML**             | dotnet-reportgenerator-globaltool        |
| **OS cible**                 | Windows / Linux / macOS (cross-platform) |
| **IDE recommandé**           | Visual Studio / Rider                    |
| **Gestionnaire de packages** | NuGet                                    |
| **CI/CD (optionnel)**        | GitHub Actions / Azure DevOps            |

# 7. Cas de test prévus

Chaque cas de test couvre une exigence métier identifiée. Les tests sont classés par méthode cible, avec indication du type (nominal, alternatif, erreur) et de la priorité.

| **ID**     | **Titre**                                                        | **Données d'entrée**                                       | **Résultat attendu**                                            | **Exigence** |
| ---------- | ---------------------------------------------------------------- | ---------------------------------------------------------- | --------------------------------------------------------------- | ------------ |
| **TC-001** | Ajouter un lion valide retourne son ID                           | Animal {Id=1, Name='Simba', Cat=Carnivore, Status=Healthy} | Retourne 1                                                      | REQ-Z-001    |
| **TC-002** | Ajouter un herbivore valide retourne son ID                      | Animal {Id=2, Name='Dumbo', Cat=Herbivore, Status=Healthy} | Retourne 2                                                      | REQ-Z-001    |
| **TC-003** | Récupérer un animal existant retourne l'animal                   | GetAnimal(1)                                               | Animal { Id=1, Name='Simba' }                                   | REQ-Z-002    |
| **TC-004** | Récupérer un animal inexistant retourne null                     | GetAnimal(99)                                              | null                                                            | REQ-Z-003    |
| **TC-005** | TotalAnimals vaut 0 sur un zoo vide                              | TotalAnimals                                               | 0                                                               | REQ-Z-004    |
| **TC-006** | TotalAnimals vaut 2 après deux ajouts                            | TotalAnimals                                               | 2                                                               | REQ-Z-004    |
| **TC-007** | Ajouter un animal avec ID existant lève DuplicateAnimalException | AddAnimal { Id=1, Name='Nala' }                            | DuplicateAnimalException: 'An animal with id 1 already exists.' | REQ-Z-005    |
| **TC-008** | Ajouter le 51e animal lève ZooCapacityExceededException          | AddAnimal { Id=51 }                                        | ZooCapacityExceededException                                    | REQ-Z-006    |
| **TC-009** | Ajouter le 50e animal réussit (limite incluse)                   | AddAnimal { Id=50 }                                        | Retourne 50                                                     | REQ-Z-006    |
| **TC-010** | Un animal Critical occupe 2 places                               | TotalCapacityUsed                                          | 2                                                               | REQ-Z-007    |
| **TC-011** | 1 Healthy + 1 Critical = 3 places                                | TotalCapacityUsed                                          | 3                                                               | REQ-Z-007    |
| **TC-012** | 2 animaux Healthy occupent 2 places                              | TotalCapacityUsed                                          | 2                                                               | REQ-Z-007    |
| **TC-013** | Ration carnivore Healthy = 5 kg                                  | CalculateDailyRation(1)                                    | 5.0                                                             | REQ-Z-008    |
| **TC-014** | Ration herbivore Healthy = 10 kg                                 | CalculateDailyRation(2)                                    | 10.0                                                            | REQ-Z-008    |
| **TC-015** | Ration omnivore Healthy = 7 kg                                   | CalculateDailyRation(3)                                    | 7.0                                                             | REQ-Z-008    |
| **TC-016** | Ration carnivore Sick = 3.5 kg (-30%)                            | CalculateDailyRation(1)                                    | 3.5                                                             | REQ-Z-009    |
| **TC-017** | Ration herbivore Sick = 7 kg (-30%)                              | CalculateDailyRation(2)                                    | 7.0                                                             | REQ-Z-009    |
| **TC-018** | Ration omnivore Sick = 4.9 kg (-30%)                             | CalculateDailyRation(3)                                    | 4.9                                                             | REQ-Z-009    |
| **TC-019** | Coût total zoo = 1 carnivore Healthy (25€)                       | CalculateDailyCost()                                       | 25.0                                                            | REQ-Z-010    |
| **TC-020** | Coût total = somme de tous les animaux                           | CalculateDailyCost()                                       | 48.0 (25+8+15)                                                  | REQ-Z-010    |
| **TC-021** | Animal Sick ajoute 20€ de frais vétérinaires                     | CalculateDailyCost()                                       | 45.0 (25+20)                                                    | REQ-Z-011    |
| **TC-022** | Animal Critical ajoute 50€ de frais vétérinaires                 | CalculateDailyCost()                                       | 58.0 (8+50)                                                     | REQ-Z-012    |
| **TC-023** | Zoo vide : coût total = 0€                                       | CalculateDailyCost()                                       | 0.0                                                             | REQ-Z-010    |
| **TC-024** | Retourner la liste des animaux Critical                          | GetCriticalAnimals()                                       | Liste de 2 animaux Critical                                     | REQ-Z-013    |
| **TC-025** | Aucun Critical : liste vide                                      | GetCriticalAnimals()                                       | Liste vide (Count=0)                                            | REQ-Z-013    |
| **TC-026** | Retirer un animal existant retourne true                         | RemoveAnimal(1)                                            | true + TotalAnimals=0                                           | REQ-Z-014    |
| **TC-027** | Retirer un animal inexistant retourne false                      | RemoveAnimal(99)                                           | false                                                           | REQ-Z-015    |
| **TC-028** | Après suppression, GetAnimal retourne null                       | GetAnimal(1)                                               | null                                                            | REQ-Z-014    |
| **TC-029** | ★ Coût mensuel = coût journalier × 30 (1 carnivore Healthy)     | CalculateMonthlyCost()                                     | 750.0 (25 × 30)                                                 | REQ-Z-016    |
| **TC-030** | ★ Coût mensuel zoo mixte (Carnivore + Herbivore + Omnivore)      | CalculateMonthlyCost()                                     | 1440.0 (48 × 30)                                                | REQ-Z-016    |
| **TC-031** | ★ Coût mensuel avec animal Sick inclus                           | CalculateMonthlyCost()                                     | 1350.0 (45 × 30)                                                | REQ-Z-016    |
| **TC-032** | ★ Coût mensuel zoo vide = 0€                                     | CalculateMonthlyCost()                                     | 0.0                                                             | REQ-Z-016    |
| **TC-033** | ★ Retourner les carnivores : liste de 2 éléments                 | GetAnimalsByCategory(Carnivore)                            | IReadOnlyList de 2 carnivores                                   | REQ-Z-017    |
| **TC-034** | ★ Retourner les herbivores : liste de 1 élément                  | GetAnimalsByCategory(Herbivore)                            | IReadOnlyList de 1 herbivore                                    | REQ-Z-017    |
| **TC-035** | ★ Retourner les omnivores : liste vide si aucun                  | GetAnimalsByCategory(Omnivore)                             | Liste vide (Count=0)                                            | REQ-Z-017    |

# 8. Matrice de traçabilité

Chaque exigence métier est reliée à au moins un cas de test. Cette matrice prouve l'exhaustivité de la couverture.

| **ID Exigence**   | **Description**                                         | **Cas de test**              | **Méthode couverte**    | **Statut prévu** |
| ----------------- | ------------------------------------------------------- | ---------------------------- | ----------------------- | ---------------- |
| **REQ-Z-001**     | Ajouter un animal retourne l'ID assigné                 | TC-001, TC-002               | AddAnimal               | À faire          |
| **REQ-Z-002**     | Récupérer un animal par son ID                          | TC-003                       | GetAnimal               | À faire          |
| **REQ-Z-003**     | Animal inexistant retourne null                         | TC-004                       | GetAnimal               | À faire          |
| **REQ-Z-004**     | Nombre total d'animaux correct                          | TC-005, TC-006               | TotalAnimals            | À faire          |
| **REQ-Z-005**     | ID dupliqué lève DuplicateAnimalException               | TC-007                       | AddAnimal               | À faire          |
| **REQ-Z-006**     | Capacité maximale = 50 animaux                          | TC-008, TC-009               | AddAnimal               | À faire          |
| **REQ-Z-007**     | Animal Critical occupe 2 places                         | TC-010, TC-011, TC-012       | TotalCapacityUsed       | À faire          |
| **REQ-Z-008**     | Ration journalière selon catégorie                      | TC-013, TC-014, TC-015       | CalculateDailyRation    | À faire          |
| **REQ-Z-009**     | Animal Sick : ration réduite de 30%                     | TC-016, TC-017, TC-018       | CalculateDailyRation    | À faire          |
| **REQ-Z-010**     | Coût total journalier du zoo                            | TC-019, TC-020, TC-023       | CalculateDailyCost      | À faire          |
| **REQ-Z-011**     | Animal Sick : +20€ vétérinaires/jour                    | TC-021                       | CalculateDailyCost      | À faire          |
| **REQ-Z-012**     | Animal Critical : +50€ vétérinaires/jour                | TC-022                       | CalculateDailyCost      | À faire          |
| **REQ-Z-013**     | Retourner la liste des animaux Critical                 | TC-024, TC-025               | GetCriticalAnimals      | À faire          |
| **REQ-Z-014**     | Retirer un animal du zoo                                | TC-026, TC-028               | RemoveAnimal            | À faire          |
| **REQ-Z-015**     | Animal inexistant : RemoveAnimal retourne false         | TC-027                       | RemoveAnimal            | À faire          |
| **REQ-Z-016 ★**   | Coût mensuel du zoo (30 jours)                          | TC-029, TC-030, TC-031, TC-032 | CalculateMonthlyCost  | À faire          |
| **REQ-Z-017 ★**   | Retourner les animaux par catégorie alimentaire         | TC-033, TC-034, TC-035       | GetAnimalsByCategory    | À faire          |

# 9. Risques identifiés et mitigations

| **Risque**                                                                                    | **Probabilité** | **Impact** | **Mitigation**                                                                                                     |
| --------------------------------------------------------------------------------------------- | --------------- | ---------- | ------------------------------------------------------------------------------------------------------------------ |
| Mauvaise interprétation de la règle 'Critical = 2 places' (TotalCapacityUsed vs TotalAnimals) | Moyenne         | Élevé      | Relire attentivement la distinction TotalAnimals / TotalCapacityUsed avant d'écrire TC-010 à TC-012.               |
| Confusion entre ration Sick et coût Sick : deux règles distinctes sur le même statut          | Haute           | Élevé      | Séparer clairement CalculateDailyRation (TC-016) et CalculateDailyCost (TC-021) dans deux cycles TDD indépendants. |
| Implémentation anticipée (coder avant le test rouge) - violation du cycle TDD                 | Haute           | Moyen      | Committer le test rouge avant d'écrire le moindre code de production. Vérifier l'historique Git.                   |
| Test faux positif (test vert sans code) : NotImplementedException masquée                     | Faible          | Élevé      | Exécuter dotnet test après chaque test ajouté et vérifier que le statut est bien FAILED avant de coder.            |
| Capacité : oubli que Critical consomme 2 places dans le compteur de capacité                  | Moyenne         | Élevé      | Ajouter TC-008 avec un mix d'animaux incluant des Critical pour valider le refus à 50 places.                      |
| Calcul du coût Critical : ration réduite ET +50€ - double règle à ne pas omettre              | Moyenne         | Moyen      | Créer un cas de test dédié TC-022 avec un animal Critical et vérifier ration + coût séparément.                    |
| Tests non déterministes (dépendance à l'ordre, au temps)                                      | Faible          | Moyen      | Chaque test instancie son propre ZooManager. Pas de state partagé entre tests.                                     |
| ★ REQ-Z-016 : CalculateMonthlyCost dépend de CalculateDailyCost — régression possible si modifié | Moyenne      | Élevé      | Implémenter CalculateMonthlyCost comme CalculateDailyCost() × 30. Tout changement sur CalculateDailyCost doit relancer TC-029 à TC-032. |
| ★ REQ-Z-017 : GetAnimalsByCategory doit retourner liste vide (pas null) si aucun animal trouvé | Haute          | Moyen      | Ajouter TC-035 : cas liste vide pour Omnivore. Vérifier Count=0 et non NullReferenceException.                     |

# 10. Responsabilités

| **Rôle**                  | **Responsable**                           | **Activités**                                                            |
| ------------------------- | ----------------------------------------- | ------------------------------------------------------------------------ |
| **Développeur / Testeur** | Sartini Robin / Nouali Malcom / Martel Nathan | Rédaction du plan, écriture des tests, implémentation TDD, rapport final |
| **Formateur / Valideur**  | Kake Abdoulaye                            | Validation du plan de test, revue de la matrice de traçabilité, notation |

_Ce plan de test doit être validé avant toute ligne de code de production (exigence TDD stricte)._