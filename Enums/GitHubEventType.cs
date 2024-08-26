using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubActivity.Enums
{
    public enum GitHubEventType
    {
        PushEvent,
        PullRequestEvent,
        IssueCommentEvent,
        IssuesEvent,
        CreateEvent,
        WatchEvent
    }
}
