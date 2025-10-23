# 🏆 PremierInsight – Premier League Dashboard (Blazor App)

## 📘 Overview
**PremierInsight** is a C# Blazor web application that integrates **two third-party REST APIs** to display live and dynamic Premier League data.

The project demonstrates:
- Consuming external web APIs in C#
- Building modern Blazor UIs
- Handling JSON data
- Unit testing API endpoints

---

## ⚽ Features
### 🧩 API Integration
1. **Fantasy Premier League API**
   - Displays live player statistics (name, team, goals, assists, points, etc.)
   - Includes player position mapping using enums and lists.
   - Supports top-player filtering and dynamic table updates.

     <img width="1891" height="875" alt="Screenshot 2025-10-23 161624" src="https://github.com/user-attachments/assets/091a4301-f781-4efe-acb9-7ba9452315c5" />
     <img width="1897" height="864" alt="Screenshot 2025-10-23 161712" src="https://github.com/user-attachments/assets/ef06c1b7-1d11-493a-ad64-22c917d08607" />


2. **Football-Data.org API**
   - Displays current Premier League standings.
   - Lists upcoming fixtures.
   - Uses authentication header with API key.
  
   <img width="1905" height="855" alt="Screenshot 2025-10-23 161801" src="https://github.com/user-attachments/assets/2a26aef1-3885-4a40-ace5-5da63038b8b2" />
   <img width="1895" height="863" alt="Screenshot 2025-10-23 161844" src="https://github.com/user-attachments/assets/c9bcb3cf-8c82-412c-ab40-e8c172f9774a" />

---

### 🎨 UI & UX
- Responsive **tab-based layout**:
  - **Players Tab:** Displays player data from the Fantasy Premier League API.
  - **League Table Tab:** Displays live standings from Football-Data.org.
  - **Fixtures Tab:** Shows upcoming matches.
- **Search bar** to filter players by name or position.
- Styled with **Bootstrap 5** + **custom CSS** for a dark, professional look.
- Fade-in animations and interactive hover effects.

---

## 🧠 C# Concepts Demonstrated
- **Classes and Objects** for structured data (`Player`, `TeamStanding`, `Fixture`)
- **Enums** for player positions (Goalkeeper, Defender, Midfielder, Forward)
- **Asynchronous API calls** using `HttpClient`
- **Lists & LINQ** for sorting and filtering data
- **Interfaces** (e.g., `IStatsProvider`)
- **Testing** with `NUnit` (API response validation)

---

## 🧪 Unit Testing
The **PremierInsight.Tests** project includes `ApiTests.cs` which validates:
- API connections
- Data retrieval success
- Non-empty player, team, and fixture lists

<img width="1906" height="1014" alt="Screenshot 2025-10-23 155441" src="https://github.com/user-attachments/assets/2fe5e8bf-2d43-4384-aad9-ed07aec16af5" />


Testing uses:
- `NUnit`
- `Microsoft.NET.Test.Sdk`
- `NUnit3TestAdapter`

---

References 

