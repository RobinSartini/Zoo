# 1. Ajouter le package (une seule fois)
dotnet add Zoo.UniTests package coverlet.collector

# 2. Installer ReportGenerator (une seule fois)
dotnet tool install --global dotnet-reportgenerator-globaltool

# 3. Lancer les tests avec collecte de couverture
dotnet test --collect:"XPlat Code Coverage"

# 4. Récuperer le chemin du fichier de couverture (syntaxe PowerShell)

à la fin de la commande précédente (dans Pièces jointes) :

Exemple :  C:\Users\natha\RiderProjects\Zoo\Zoo.UniTests\TestResults\38b7737a-40f5-433e-8d19-4bb579525e4b\coverage.cobertura.xml

# 3. Coller le chemin trouvé à la place de VOTRE_CHEMIN dans la commande suivante pour générer le rapport HTML :
reportgenerator -reports:"VOTRE_CHEMIN" -targetdir:".\coverage-report" -reporttypes:Html

# 5. Ouvrir le rapport
Start-Process ".\coverage-report\index.html"