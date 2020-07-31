﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace VerifyGitHubReadmeLinks
{
    [Headers("User-Agent: " + nameof(GitHubApiService), "Accept-Encoding: gzip", "Accept: application/json")]
    interface IGitHubApiClient
    {
        [Get("/repos/MicrosoftDocs/cloud-developer-advocates/contents/advocates")]
        public Task<List<RepositoryFile>> GetAllAdvocateFiles();

        [Get("/repos/{owner}/{repo}/readme")]
        public Task<RepositoryFile> GetReadme([AliasAs("owner")] string gitHubUserName, [AliasAs("repo")] string repositoryName);
    }
}