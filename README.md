# 🎬 MovieBot<img width="1073" height="500" alt="Screenshot 2026-06-30 144112" src="https://github.com/user-attachments/assets/13804948-cf92-48c1-b977-b4e6dd862906" />
<img width="1073" height="500" alt="Screenshot 2026-06-30 144112" src="https://github.com/user-attachments/assets/72db5332-5973-4685-ac73-c2f169848cc3" />


https://github.com/user-attachments/assets/fa53a522-0554-4bb7-a4c5-ca609e3f4ffe



A console-based chatbot built in C# that recommends movies by genre, using Google Dialogflow ES for natural language understanding.

## Features
- Understands natural language requests for movie suggestions (comedy, action, romance, horror, thriller)
- Mood-based and random movie suggestions
- Maintains a local movie database in C# and intelligently picks recommendations
- Handles greetings, farewells, and fallback responses for unrecognized input

## Tech Stack
- **C# (.NET 10)** — console application logic
- **Google Dialogflow ES** — natural language intent detection
- **Google.Cloud.Dialogflow.V2** NuGet package — API integration

## How It Works
1. User types a message in the console
2. The message is sent to Dialogflow, which detects the user's intent (e.g. "wants a comedy movie")
3. C# code receives the detected intent and picks a relevant movie from its local database
4. The bot replies with a friendly response and a movie recommendation

## Setup & Run
1. Clone this repository
2. Add your own `credentials.json` (Google Cloud service account key) in the project root — **not included for security reasons**
3. Run:
