# Projet PRBD 2021 - AppSchool

## Description du projet
- Il s'agit d'une application desktop en C# .net. Le but est de permettre aux professeurs de gérer leurs cours et les étudiants associés. Les étudiants peuvent également s'y inscrire et gérer leurs cours et leurs tests.
- L'application a été codée sous visual studio 2019.
- La solution se trouve dans le dossier prbd-2021-g01.
- La documentation se trouve dans les dossiers Manuel et Diagrammes.

## Notes de livraison
- L'application n'est pas complète.
- Dans EcoleContext, on a ajouté "MultipleActiveResultSets=true" dans les optionsBuilder de la méthode OnConfiguring. On a été contraint de l'ajouter pour que la base de données fonctionne lorsqu'elle n'est pas regénérée via le Context.Database.EnsureDeleted(). La connexion de certaines requêtes semblaient ne pas se fermer correctement.
- L'écran "Courses" n'affiche plus les cours par moments après modifications ou ajout d'un cours sur un autre onglet. Il faut du coup cliquer sur "Clear" pour que l'écran réaffiche la liste de cours.
- Les "delete on cascade" n'ont pas été implémentées. Cela aurait probablement facilité certaines queries mais on s'est rendu compte trop tard.
- Les tests n'ont pas été effectués de manière intensive par manque de temps.
