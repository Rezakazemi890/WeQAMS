using Atlassian.Jira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAMS.Module.BusinessObjects.Tools
{
    public class JiraTool
    {
        private Jira jiraConn = null;
        public JiraTool(string url, string username, string password)
        {
            jiraConn = Jira.CreateRestClient(url, username, password);
            
        }

        public IJiraUserService GetUsers()
        {
            return jiraConn.Users;
        }

        public IProjectService GetProjects()
        {
            return jiraConn.Projects;
        }

        public Issue CreateIssue(string project, string parrentIssueKey)
        {
            return jiraConn.CreateIssue(project, parrentIssueKey);
        }

    }
}
