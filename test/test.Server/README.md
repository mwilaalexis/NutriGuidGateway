# 📘 **FoodPlan API Gateway – Ocelot Configuration**

Ce projet contient la configuration complète de l’API Gateway du système **FoodPlan**, basée sur **Ocelot**.  
L’objectif est de centraliser toutes les routes des microservices (Auth, Profile, Foods, Ingredients, Plans, Images…) derrière un point d’entrée unique.

---

## 🚀 **Architecture générale**

L’API Gateway redirige les requêtes entrantes vers les microservices suivants :

| Service | Port | Description |
|--------|-------|-------------|
| Auth Service | **7004** | Authentification, tokens |
| User Profile Service | **7004** | Profils utilisateurs, images |
| User Service | **7004** | Gestion des utilisateurs |
| Foods Service | **7152** | CRUD aliments + images |
| Ingredients Service | **7152** | CRUD ingrédients + images |
| Plans Service | **7028** | Génération et gestion des plans alimentaires |
| Gateway | **7059** | Point d’entrée unique |

---

## 🔗 **Routes configurées**

### 🔐 Authentification
```
/api/auth/*
→ localhost:7004
```

### 👤 Profil utilisateur
```
/api/profile/*
→ localhost:7004
```

### 👥 Utilisateurs
```
/api/user/*
→ localhost:7004
```

### 🍽️ Aliments
```
/api/foods/*
→ localhost:7152
```

### 🧂 Ingrédients
```
/api/ingredients/*
→ localhost:7152
```

### 📅 Plans alimentaires
```
/api/plans/*
→ localhost:7028
```

---

## 🖼️ **Routes d’images**

| Type | Upstream | Downstream |
|------|----------|------------|
| Images aliments | `/foods/images/{fileName}` | `/images/foods/{fileName}` |
| Images ingrédients | `/foods/ingredients/{fileName}` | `/images/ingredients/{fileName}` |
| Images profils | `/profile-images/{fileName}` | `/images/userProfiles/{fileName}` |

---

## 🛠️ **Extrait du fichier `ocelot.json`**

```json
{
  "Routes": [
    {
      "DownstreamPathTemplate": "/api/auth/{everything}",
      "DownstreamScheme": "https",
      "DownstreamHostAndPorts": [{ "Host": "localhost", "Port": 7004 }],
      "UpstreamPathTemplate": "/api/auth/{everything}",
      "UpstreamHttpMethod": [ "GET", "POST", "OPTIONS" ]
    }
  ],
  "GlobalConfiguration": {
    "BaseUrl": "https://localhost:7059"
  }
}
```

---

## 🧪 **Tester l’API Gateway**

### 1. Lancer les microservices
Assure-toi que :

- Auth/Profile/User tournent sur **7004**
- Foods/Ingredients tournent sur **7152**
- Plans tourne sur **7028**

### 2. Lancer la Gateway
```
dotnet run
```

La Gateway écoute sur :

```
https://localhost:7059
```

### 3. Tester via Postman ou Swagger
Exemples :

- `GET https://localhost:7059/api/foods`
- `POST https://localhost:7059/api/auth/login`
- `GET https://localhost:7059/foods/images/apple.png`

---

## 📦 **Technologies utilisées**

- .NET 8  
- Ocelot API Gateway  
- ASP.NET Core Web API  
- EF Core (dans les microservices)  
- JWT Authentication  
- Reverse Proxy Routing  

---

## 🎯 **Objectif du projet**

Créer une architecture **scalable**, **modulaire** et **sécurisée** pour NutriGuide, avec :

- séparation claire des responsabilités  
- microservices indépendants  
- un point d’entrée unique  
- gestion centralisée de l’authentification  
- routage propre et maintenable  
