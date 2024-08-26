using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GitHubActivity.Utilities
{
    public static class Utility
    {
        public static List<string> ParseInput(string input)
        {
            var commandArgs = new List<string>();

            // Regex to match arguments, including those inside quotes
            var regex = new Regex(@"[\""].+?[\""]|[^ ]+");
            var matches = regex.Matches(input);

            foreach (Match match in matches)
            {
                // Remove surrounding quotes if any
                string value = match.Value.Trim('"');
                commandArgs.Add(value);
            }

            return commandArgs;
        }
    }
}
