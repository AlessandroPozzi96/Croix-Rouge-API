# Croix-Rouge-API
- Projet développé sous ASP .NET CORE en C#.
- Cette API pour but de réaliser des opérations diverses sur notre base de données de manière sécurisée tout en garantissant son intégrité.

## Prérequis
- .NET CORE 2.1.500 [téléchargement](https://dotnet.microsoft.com/download)
- Recommandation IDE : Visual Studio Code [téléchargement](https://code.visualstudio.com/Download)

## Installation
- Clonez le répertoire Git (Soit en téléchargement le fichier .zip du respository soit en utilisant la commande `git clone --branche nomTag urlRepository` )
- Copier le fichier appSettings.json à la racine du projet api
- Positionnnez vous à la racine du projet api
- Tapez la commande `dotnet restore`
- Tapez la commande `dotnet run` pour lancer l'API localement

## Sécurité
- BCrypt est utilisé pour le hashing des mots de passe (lien GitHub : [url](https://github.com/BcryptNet/bcrypt.net))
- Le facteur de coût est à 12