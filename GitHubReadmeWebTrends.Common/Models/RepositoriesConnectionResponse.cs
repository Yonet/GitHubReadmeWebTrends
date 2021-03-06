﻿using System.Collections.Generic;
using System.Linq;

namespace GitHubReadmeWebTrends.Common
{
    public class RepositoriesConnectionResponse
    {
        public RepositoriesConnectionResponse(User_RepositoriesConnectionResponse user)
        {
            var repositoryList = new List<Repository>();

            foreach (var repository in user.Repositories.RepositoryList)
            {
                if (repository.Owner == user.Login && !repository.IsFork && repository.DefaultBranch != null && !repositoryList.Any(x => x.Name == repository.Name && x.Owner == user.Login))
                    repositoryList.Add(new Repository(repository.Id, user.Login, repository.Name, repository.DefaultBranch, repository.IsFork));
            }

            RepositoryList = repositoryList;
            PageInfo = user.Repositories.PageInfo;
        }

        public IReadOnlyList<Repository> RepositoryList { get; }
        public PageInfo PageInfo { get; }
    }

    public class User_RepositoriesConnectionResponse
    {
        public User_RepositoriesConnectionResponse(string login, Repositories_RepositoriesConnectionResponse repositories) =>
            (Login, Repositories) = (login, repositories);

        public Repositories_RepositoriesConnectionResponse Repositories { get; }
        public string Login { get; }
    }

    public class Repositories_RepositoriesConnectionResponse
    {
        public Repositories_RepositoriesConnectionResponse(IEnumerable<Repository_RepositoriesConnectionResponse> nodes, PageInfo pageInfo) =>
            (RepositoryList, PageInfo) = (nodes.ToList(), pageInfo);

        public List<Repository_RepositoriesConnectionResponse> RepositoryList { get; }

        public PageInfo PageInfo { get; }
    }

    public class Repository_RepositoriesConnectionResponse
    {
        public Repository_RepositoriesConnectionResponse(string id, string name, bool isFork, Owner owner, DefaultBranchModel? defaultBranchRef) =>
            (Id, Name, IsFork, Owner, DefaultBranch) = (id, name, isFork, owner.Login, defaultBranchRef);

        public string Id { get; }
        public string Name { get; }
        public bool IsFork { get; }
        public string Owner { get; }
        public DefaultBranchModel? DefaultBranch { get; }
    }

    public class Owner_RepositoriesConnectionResponse
    {
        public Owner_RepositoriesConnectionResponse(string login) => Login = login;

        public string Login { get; }
    }
}
