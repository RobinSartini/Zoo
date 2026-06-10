# Rapport d'exécution des tests — ZooManager

## 1. Identification

| Champ | Valeur |
|---|---|
| **Projet** | Zoo - Système de gestion du Zoo Municipal de Lyon |
| **Version testée** | 1.0 |
| **Auteur** | Sartini Robin / Nouali Malcom / Martel Nathan |
| **Date d'exécution** | 2026-06-10 |
| **Durée totale d'exécution** | 1,35 secondes |

---

## 2. Résumé exécutif

| Indicateur | Valeur |
|---|---|
| **Nombre total de tests** | 35 |
| **Tests réussis** | 35 (100 %) |
| **Tests échoués** | 0 |
| **Tests ignorés** | 0 |
| **Couverture de lignes** | 97,87 % |
| **Couverture de branches** | 92,85 % |
| **Verdict global** | ✅ SUCCÈS |

---

## 3. Résultats détaillés par exigence

| Exigence | Cas de test | Résultat | Notes |
|---|---|---|---|
| REQ-Z-001 | TC-001 `AddAnimal_CarnivoreHealthy_ReturnsId` | ✅ Réussi | RAS |
| REQ-Z-001 | TC-002 `AddAnimal_HerbivoreHealthy_ReturnsId` | ✅ Réussi | RAS |
| REQ-Z-002 | TC-003 `GetAnimal_ExistingAnimal_ReturnsAnimal` | ✅ Réussi | RAS |
| REQ-Z-003 | TC-004 `GetAnimal_NonExistingAnimal_ReturnsNull` | ✅ Réussi | RAS |
| REQ-Z-004 | TC-005 `TotalAnimals_EmptyZoo_ReturnsZero` | ✅ Réussi | RAS |
| REQ-Z-004 | TC-006 `TotalAnimals_AfterTwoAdditions_ReturnsTwo` | ✅ Réussi | RAS |
| REQ-Z-005 | TC-007 `AddAnimal_DuplicateId_ThrowsDuplicateAnimalException` | ✅ Réussi | Message d'exception vérifié |
| REQ-Z-006 | TC-008 `AddAnimal_FiftyFirstAnimal_ThrowsZooCapacityExceededException` | ✅ Réussi | RAS |
| REQ-Z-006 | TC-009 `AddAnimal_FiftiethAnimal_Succeeds` | ✅ Réussi | Limite incluse validée |
| REQ-Z-007 | TC-010 `TotalCapacityUsed_CriticalAnimal_ConsumesTwoSpaces` | ✅ Réussi | RAS |
| REQ-Z-007 | TC-011 `TotalCapacityUsed_OneHealthyAndOneCritical_ConsumesThreeSpaces` | ✅ Réussi | RAS |
| REQ-Z-007 | TC-012 `TotalCapacityUsed_TwoHealthyAnimals_ConsumesTwoSpaces` | ✅ Réussi | RAS |
| REQ-Z-008 | TC-013 `CalculateDailyRation_CarnivoreHealthy_ReturnsFiveKg` | ✅ Réussi | RAS |
| REQ-Z-008 | TC-014 `CalculateDailyRation_HerbivoreHealthy_ReturnsTenKg` | ✅ Réussi | RAS |
| REQ-Z-008 | TC-015 `CalculateDailyRation_OmnivoreHealthy_ReturnsSevenKg` | ✅ Réussi | RAS |
| REQ-Z-009 | TC-016 `CalculateDailyRation_CarnivoreSick_ReturnsThreePointFiveKg` | ✅ Réussi | −30 % validé (5 → 3,5 kg) |
| REQ-Z-009 | TC-017 `CalculateDailyRation_HerbivoreSick_ReturnsSevenKg` | ✅ Réussi | −30 % validé (10 → 7 kg) |
| REQ-Z-009 | TC-018 `CalculateDailyRation_OmnivoreSick_ReturnsFourPointNineKg` | ✅ Réussi | −30 % validé (7 → 4,9 kg) |
| REQ-Z-010 | TC-019 `CalculateDailyCost_OneCarnivoreHealthy_ReturnsTwentyFiveEuros` | ✅ Réussi | RAS |
| REQ-Z-010 | TC-020 `CalculateDailyCost_MultipleAnimals_ReturnsSumOfCosts` | ✅ Réussi | 25 + 8 + 15 = 48 € |
| REQ-Z-010 | TC-023 `CalculateDailyCost_EmptyZoo_ReturnsZero` | ✅ Réussi | RAS |
| REQ-Z-011 | TC-021 `CalculateDailyCost_SickAnimal_IncludesVetFee` | ✅ Réussi | 25 + 20 = 45 € |
| REQ-Z-012 | TC-022 `CalculateDailyCost_CriticalAnimal_IncludesVetFee` | ✅ Réussi | 8 + 50 = 58 € |
| REQ-Z-013 | TC-024 `GetCriticalAnimals_WithCriticalAnimals_ReturnsThem` | ✅ Réussi | RAS |
| REQ-Z-013 | TC-025 `GetCriticalAnimals_NoCriticalAnimals_ReturnsEmptyList` | ✅ Réussi | Liste vide (non null) |
| REQ-Z-014 | TC-026 `RemoveAnimal_ExistingAnimal_ReturnsTrueAndDecrementsTotal` | ✅ Réussi | RAS |
| REQ-Z-014 | TC-028 `GetAnimal_AfterRemoval_ReturnsNull` | ✅ Réussi | RAS |
| REQ-Z-015 | TC-027 `RemoveAnimal_NonExistingAnimal_ReturnsFalse` | ✅ Réussi | RAS |
| REQ-Z-016 ★ | TC-029 `CalculateMonthlyCost_OneCarnivoreHealthy_ReturnsSevenHundredFiftyEuros` | ✅ Réussi | 25 × 30 = 750 € |
| REQ-Z-016 ★ | TC-030 `CalculateMonthlyCost_MixedZoo_ReturnsSumTimesThirty` | ✅ Réussi | 48 × 30 = 1 440 € |
| REQ-Z-016 ★ | TC-031 `CalculateMonthlyCost_WithSickAnimal_ReturnsExpectedCost` | ✅ Réussi | 45 × 30 = 1 350 € |
| REQ-Z-016 ★ | TC-032 `CalculateMonthlyCost_EmptyZoo_ReturnsZero` | ✅ Réussi | RAS |
| REQ-Z-017 ★ | TC-033 `GetAnimalsByCategory_Carnivores_ReturnsTwoElements` | ✅ Réussi | RAS |
| REQ-Z-017 ★ | TC-034 `GetAnimalsByCategory_Herbivores_ReturnsOneElement` | ✅ Réussi | RAS |
| REQ-Z-017 ★ | TC-035 `GetAnimalsByCategory_NoAnimalsFound_ReturnsEmptyList` | ✅ Réussi | Liste vide (non null) |

---

## 4. Anomalies détectées

Aucune anomalie résiduelle.

> Remarque : lors du développement TDD, les méthodes `CalculateMonthlyCost()` et `GetAnimalsByCategory()` étaient initialement fournies sous forme de squelettes levant `NotImplementedException`. Ces méthodes ont été implémentées dans le cadre du cycle RED → GREEN prévu par la méthodologie, conformément aux exigences REQ-Z-016 et REQ-Z-017.

---

## 5. Métriques détaillées

### Distribution par type

| Type de test | Nombre |
|---|---|
| Tests nominaux (happy path) | 22 |
| Tests d'erreur / exceptions (sad path) | 5 |
| Tests de cas limites (boundary) | 8 |
| Tests paramétrés `[Theory]` | 0 — chaque variante est un `[Fact]` indépendant |
| **Total** | **35** |

### Performance (exécution `dotnet test` — .NET 9.0)

| Indicateur | Valeur |
|---|---|
| Test le plus rapide | < 1 ms (21 tests) |
| Test le plus lent | 10 ms (`AddAnimal_DuplicateId_ThrowsDuplicateAnimalException`) |
| Durée moyenne par test | ~0,04 ms |
| Durée totale de la suite (hors build) | ~200 ms |
| Durée totale mesurée (build + exécution) | 1,35 secondes |

---

## 6. Analyse de la couverture

> Source : `coverlet` XPlat Code Coverage → `coverage.cobertura.xml`  
> Projet analysé : `Zoo.Domain`

| Indicateur | Valeur |
|---|---|
| **Lignes couvertes** | 46 / 47 |
| **Taux de couverture de lignes** | **97,87 %** |
| **Branches couvertes** | 13 / 14 |
| **Taux de couverture de branches** | **92,85 %** |

### Branche non couverte (1/14)

La seule branche non couverte concerne la méthode `GetAnimal(int id)`, ligne 22 :

```csharp
// ZooManager.cs – ligne 22
var isNull = id <= 0 || !_animals.ContainsKey(id);
```

La condition `id <= 0` n'est jamais évaluée à `true` dans les tests actuels : tous les identifiants passés à `GetAnimal` sont soit positifs valides, soit positifs inexistants (ex. `GetAnimal(99)`). Un test couvrant un id nul ou négatif permettrait d'atteindre 100 % de branches.

### Méthodes non couvertes

**Aucune.** L'intégralité des méthodes publiques de `ZooManager` est exercée par au moins un cas de test.

---

## 7. Difficultés rencontrées

Le cycle **Red → Green → Refactor** a été respecté pour chacune des 17 exigences. Voici les principaux points notables rencontrés au fil du développement :

- **Tentation de coder avant le test (REQ-Z-007).** La règle « Critical = 2 places » dans `TotalCapacityUsed` est intuitivement évidente. Il a fallu une discipline consciente pour écrire les trois cas de test (TC-010, TC-011, TC-012) et observer le RED avant d'implémenter le `Sum(a => a.Value.Status == HealthStatus.Critical ? 2 : 1)`.

- **Double règle sur le statut Sick (REQ-Z-009 vs REQ-Z-011).** Le statut `Sick` entraîne à la fois une réduction de ration (−30 %) et un surcoût vétérinaire (+20 €). Ces deux règles indépendantes ont chacune nécessité un cycle TDD distinct pour éviter toute confusion dans `CalculateDailyRation` et `CalculateDailyCost`.

- **Limite de capacité stricte (REQ-Z-006).** Écrire TC-009 (50e animal réussit) en plus de TC-008 (51e échoue) s'est avéré indispensable : une implémentation naïve avec `> 50` au lieu de `>= 50` aurait passé TC-008 tout en refusant faussement le 50e animal.

- **Exigences bonus REQ-Z-016 et REQ-Z-017.** Ces deux méthodes étaient livrées avec `throw new NotImplementedException()` dans le squelette. La phase RED a été immédiate et parfaitement lisible. L'implémentation (`CalculateDailyCost() * 30` et `Where(a => a.Category == category)`) a été triviale une fois les tests rédigés.

- **Aucun refactoring profond n'a été nécessaire.** La structure choisie dès le départ — un `Dictionary<int, Animal>` et une classe `AnimalCategory` en type-objet encapsulant ration et coût — s'est révélée suffisamment robuste pour accueillir la totalité des exigences sans modifier l'architecture.

---

## 8. Conclusion et recommandations

### Verdict final

✅ **La campagne de test est un succès complet.** Les 35 cas de test couvrant les 17 exigences fonctionnelles (REQ-Z-001 à REQ-Z-017, dont 2 exigences bonus) passent tous au vert sans exception.

| Critère de sortie (TEST_PLAN.md) | Objectif | Résultat |
|---|---|---|
| 100 % des exigences couvertes (17/17) | 17 | ✅ 17 |
| 0 test échoué | 0 | ✅ 0 |
| 0 test ignoré | 0 | ✅ 0 |
| Couverture de lignes ≥ 95 % | ≥ 95 % | ✅ 97,87 % |
| Couverture de branches ≥ 90 % | ≥ 90 % | ✅ 92,85 % |
| Aucun bug bloquant ou critique non résolu | — | ✅ Aucun |

### Recommandations pour la suite

1. **Couvrir la branche manquante.** Ajouter un test `GetAnimal_NegativeId_ReturnsNull` pour atteindre 100 % de branches et documenter le comportement défensif sur les identifiants invalides.
2. **Passer à `[Theory]` pour les rations et coûts.** Les tests TC-013 à TC-015 et TC-016 à TC-018 sont structurellement identiques avec des paramètres différents ; un refactoring en `[Theory] + [InlineData]` améliorerait la lisibilité et réduirait la duplication.
3. **Tests d'intégration.** La logique métier est entièrement couverte en mémoire. Si une couche de persistance (base de données, fichiers) est ajoutée, des tests d'intégration dédiés seront nécessaires.
4. **Mise en production.** Le code est prêt pour la mise en production. La couverture de 97,87 % est largement suffisante pour la logique métier ciblée.
5. **CI/CD.** Intégrer `dotnet test --collect:"XPlat Code Coverage"` dans une pipeline GitHub Actions ou Azure DevOps pour garantir la non-régression à chaque commit.

---

## 9. Signature

| Rôle | Nom |
|---|---|
| **Auteur du rapport** | Sartini Robin / Nouali Malcom / Martel Nathan |
| **Validé par** | *(À remplir par le formateur — Kake Abdoulaye)* |
