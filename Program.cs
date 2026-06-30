using System;
using System.Collections.Generic;
using System.Linq;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf.WellKnownTypes;
using Grpc.Auth;
using Google.Apis.Auth.OAuth2;

class Program
{
    static Dictionary<string, List<string>> movieDatabase = new Dictionary<string, List<string>>()
    {
        { "comedy", new List<string> { "3 Idiots", "Hera Pheri", "Andaz Apna Apna", "Golmaal", "Welcome" } },
        { "action", new List<string> { "War", "Pathaan", "Baaghi", "Tiger Zinda Hai", "Dhoom 3" } },
        { "romance", new List<string> { "Yeh Jawaani Hai Deewani", "Jab We Met", "Kabir Singh", "Tamasha", "Rockstar" } },
        { "horror", new List<string> { "Stree", "Bhoot Police", "Tumbbad", "1920", "Raaz" } },
        { "thriller", new List<string> { "Andhadhun", "Drishyam", "Kahaani", "Talvar", "Badla" } }
    };

    static Random rng = new Random();

    static void Main(string[] args)
    {
        string credentialsPath = "credentials.json";
        string projectId = "enduring-aria-501008-v6"; 

       
        var credential = GoogleCredential.FromFile(credentialsPath);
        var sessionsClient = new SessionsClientBuilder
        {
            ChannelCredentials = credential.ToChannelCredentials()
        }.Build();

        string sessionId = Guid.NewGuid().ToString();
        SessionName session = new SessionName(projectId, sessionId);

        Console.WriteLine("=== MovieBot ===");
        Console.WriteLine("Type 'exit' to quit.\n");

        while (true)
        {
            Console.Write("You: ");
            string userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput)) continue;
            if (userInput.ToLower() == "exit") break;

            QueryInput queryInput = new QueryInput
            {
                Text = new TextInput
                {
                    Text = userInput,
                    LanguageCode = "en"
                }
            };

            DetectIntentResponse response = sessionsClient.DetectIntent(session, queryInput);
            string detectedIntent = response.QueryResult.Intent.DisplayName;
            string botReply = response.QueryResult.FulfillmentText;

           
            string genre = ExtractGenre(detectedIntent);

            if (genre != null && movieDatabase.ContainsKey(genre))
            {
                List<string> movies = movieDatabase[genre];
                string pick = movies[rng.Next(movies.Count)];
                Console.WriteLine($"Bot: {botReply} I'd recommend: \"{pick}\"\n");
            }
            else if (detectedIntent == "AskRandomMovie")
            {
                var allGenres = movieDatabase.Keys.ToList();
                string randomGenre = allGenres[rng.Next(allGenres.Count)];
                var movies = movieDatabase[randomGenre];
                string pick = movies[rng.Next(movies.Count)];
                Console.WriteLine($"Bot: {botReply} How about \"{pick}\" ({randomGenre})?\n");
            }
            else
            {
                Console.WriteLine($"Bot: {botReply}\n");
            }
        }

        Console.WriteLine("MovieBot session ended.");
    }

    static string ExtractGenre(string intentName)
    {
        if (intentName == null) return null;
        intentName = intentName.ToLower();

        if (intentName.Contains("comedy")) return "comedy";
        if (intentName.Contains("action")) return "action";
        if (intentName.Contains("romance")) return "romance";
        if (intentName.Contains("horror")) return "horror";
        if (intentName.Contains("thriller")) return "thriller";

        return null;
    }
}
