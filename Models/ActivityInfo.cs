using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubActivity.Models
{
    public class ActivityInfo
    {
        public string Type { get; set; } = string.Empty;

        public int Count { get; set; }

        public List<string> RepoNames { get; set; } = new();
    }
}
