using GitHubActivity.Utilities;
using System.Net.Http.Json;
using System;
using System.Web;
using GitHubActivity.Models;
using System.Text.Json;
using GitHubActivity.Enums;

PrintWelcomeMessage();

while (true)
{
    ConsoleMessage.PrintCommandMessage("Enter Command : ");
    string input = Console.ReadLine() ?? string.Empty;

    if (string.IsNullOrEmpty(input))
    {
        RestartCommandMode();
        continue;
    }

    List<string> commands = Utility.ParseInput(input);

    if (commands.Count != 2 && !commands[0].Equals("exit"))
    {
        RestartCommandMode();
        continue;
    }

    if (commands[0].Equals("exit"))
    {
        break;
    }

    HttpClient client = new HttpClient();
    client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

    var command = commands[0];
    var username = commands[1];
    if (!command.Equals("github-activity"))
    {
        RestartCommandMode();
        continue;
    }

    if (string.IsNullOrEmpty(username))
    {
        ConsoleMessage.PrintErrorMessage("User name not entered! Type github-activity <username> to get started.");
        continue;
    }

    var events = await GetUserActivities(client, username);
    Dictionary<string, int> table = new();
    List<ActivityInfo> infoList = new List<ActivityInfo>();
    if (events.Count != 0) {
       var groups = events.GroupBy(x=>x.Type);
        foreach (var group in groups)
        {
            var info = new ActivityInfo();
            info.Type = group.Key;
            info.RepoNames = group.Select(x=>x.Repo.Name).Distinct().ToList();
            info.Count = info.RepoNames.Count;
            infoList.Add(info);
        }

        Console.WriteLine("\nOutput : \n");

        foreach (var item in infoList) {
            switch (item.Type)
            {
                case "CreateEvent":
                    Console.WriteLine($"Created {item.Count} new repositories called {ReturnNamesInStringForm(item.RepoNames)}");
                    break;

                case "PushEvent":
                    Console.WriteLine($"Pushed {item.Count} new changes in repositories {ReturnNamesInStringForm(item.RepoNames)}");
                    break;

                case "PullRequestEvent":
                    Console.WriteLine($"Opened {item.Count} new pull requestes in repositories {ReturnNamesInStringForm(item.RepoNames)}");
                    break;

                case "IssueCommentEvent":
                    Console.WriteLine($"Added {item.Count} new comments in repositories {ReturnNamesInStringForm(item.RepoNames)}");
                    break;

                case "IssuesEvent":
                    Console.WriteLine($"Opened {item.Count} new issues in repositories {ReturnNamesInStringForm(item.RepoNames)}");
                    break; 

                case "WatchEvent":
                    Console.WriteLine($"Starred {item.Count} {ReturnNamesInStringForm(item.RepoNames)}");
                    break;

                default:
                    break;
            }
        }
    }

    else
    {
        ConsoleMessage.PrintErrorMessage("No meaning - full activities found for this user. Try with some other user");
        continue;
    }

}

static string ReturnNamesInStringForm(List<string> repoNames)
{
    if(repoNames.Count == 0)
    {
        return string.Empty;
    }

    if (repoNames.Count == 1)
    {
        return repoNames.FirstOrDefault() ?? string.Empty;
    }

    if (repoNames.Count > 1)
    {
        string names = string.Empty;

        for(int i = 0; i < repoNames.Count - 1; i++)
        {
            names += repoNames[i] + ", ";
        }

        names += "& " + repoNames[repoNames.Count - 1];
        return names;
    }

    return string.Empty;
}

static void PrintWelcomeMessage()
{
    ConsoleMessage.PrintInfoMessage("Hello, Welcome to Github Activity console");

    ConsoleMessage.PrintInfoMessage("Type github-activity <username> to get started.");
}

static void RestartCommandMode()
{
    ConsoleMessage.PrintErrorMessage("Wrong command! Type github-activity <username> to get started.");
}

static async Task<List<Activity>> GetUserActivities(HttpClient client, string username)
{
    try
    {
        var encodedUsername = HttpUtility.UrlEncode(username);

        var url = $"https://api.github.com/users/{encodedUsername}/events";
        var httpResponse = await client.GetAsync(url);
        httpResponse.EnsureSuccessStatusCode();

        var content = await httpResponse.Content.ReadAsStringAsync();
        var activities = JsonSerializer.Deserialize<List<Activity>>(content);

        return activities ?? new();
    }
    catch (Exception ex)
    {
        ConsoleMessage.PrintErrorMessage("There is problem in fetching your git hub activities.");
        throw;
    }
}