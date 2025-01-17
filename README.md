# movie-tracker

ISIMA *Service Web .NET* project.

It is a web application to track which movies have been seen by a user, written in **C# Blazor** with **ASP.Net Core**.

## TODO

### Backend

- [ ] Utilise l'authentification JWT
- [x] Utilise l'entity framework pour la base de données sqlite
- [ ] Controller User:
    - [-] Récupérer la liste des utilisateurs (Id, Pseudo, Role)
    - [ ] Récupérer un utilisateur par son pseudo et son mot de passe (login)
    - [ ] Ajouter un utilisateur (register)
    - [ ] Modifier un utilisateur (Pseudo, Password, Role)
    - [ ] Supprimer un utilisateur
    - [ ] Le mot de passe est hashé et n'est pas renvoyé
- [x] Controller Favorite:
    - [x] Récupérer les favoris d'un utilisateur
    - [x] Ajouter un favori
    - [x] Supprimer un favori
- [ ]Controller Movie:
    - [x] Récupérer les films
    - [ ] Supprimer un film
- [x]Controller OMDB:
    - [x] Rechercher un film par son titre
    - [x] Importer des films depuis l'API OMDB
- [ ] Utilisation de la configuration pour les secrets (Clé d'API OMDB, Secret JWT)
- [x] Utilisation de l'injection de dépendance
- [ ] Utilisation du méchanisme d'authentification pour protéger les routes
- [ ] Configuration du JWT
- [-] 2 Services (JWT et OMDB)
- [ ] Gestion des erreurs (try catch, throw)
- [ ] Réponse de code HTTP approprié 200, 404, 500, ...
- [x] Utilisation de async await pour les appels API, et l'accès BDD

Partie Blazor frontend: 8 points

- [x] Formulaire de login
- [x] Formulaire d'inscription
- [x] Page de liste des films:
    - [x] Les films sont affichés sous forme de carte dans un composant
    - [x] On peux ajouter/retirer un film aux favoris
- [ ] Liste des utilisateurs avec leur rôles
- [x] Page qui affiche les films favoris d'un utilisateur
- [ ] Page admin pour importer des films
- [ ] Possibiliter de se déconnecter
- [ ] Présence de 4 services pour communiquer avec l'API:
    - [ ] AuthService
    - [x] UserService
    - [x] FavoriteService
    - [x] MovieService
- [ ] Lors de l'authentification, le token JWT est stocké dans le local storage
- [ ] Le token JWT est envoyé dans le header de chaque requête API qui le nécessite
- [ ] Les pages sont protégées en fonction des rôles
- [x] Les services sont injectés et appelés dans les composants
- [x] Utilisation de async await pour les appels API

