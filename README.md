# GitHub Activity Console Application

Project Task URL: https://roadmap.sh/projects/github-activity

.NET 8.0 Console app solution for the GitHub Activity [challenge](https://roadmap.sh/projects/github-user-activity) from [roadmap.sh](https://roadmap.sh/).

GitHub Activity Console Application is a simple tool designed to help you fetch and display GitHub activities for any user. This application interacts with the GitHub API to retrieve and categorize recent events related to a user's repositories.

## Features

- **Fetch GitHub Activity**: Retrieve recent GitHub activities for a specified user.
- **Activity Categorization**: Display activities categorized by type:
  - Repository creation
  - Push events
  - Pull requests
  - Issue comments
  - Issues opened
  - Stars added
- **User-Friendly Interface**: Clear prompts and error messages guide the user through the process.

## Installation

To run this application, follow these steps:

1. Clone this repository:
    ```bash
    git clone https://github.com/damonsahil26/GitHubActivity.git
    ```

2. Navigate to the project directory:
    ```bash
    cd GitHubActivity
    ```

3. Restore dependencies:
    ```bash
    dotnet restore
    ```

4. Build the project:
    ```bash
    dotnet build
    ```

5. Run the application:
    ```bash
    dotnet run
    ```

## Usage

After running the application, you will be greeted with a welcome message. You can then start entering commands.

### Available Commands

- **github-activity [username]**: Fetches and displays recent GitHub activities for the specified username.
- **exit**: Exits the application.

### Example Usage

```bash
Enter Command: github-activity damonsahil26
Created 2 new repositories called Hello-World & Spoon-Knife
Pushed 3 new changes in repositories Hello-World & Spoon-Knife
Opened 1 new pull request in repository Spoon-Knife
Added 4 new comments in repositories Hello-World & Spoon-Knife
Starred Hello-World & Spoon-Knife

Enter Command: exit
