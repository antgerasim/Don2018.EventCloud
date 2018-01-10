﻿using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

namespace Don2018.EventCloud.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Base class for custom repositories of the application.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public abstract class EventCloudRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<EventCloudDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected EventCloudRepositoryBase(IDbContextProvider<EventCloudDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        // Add your common methods for all repositories
    }

    /// <summary>
    /// Base class for custom repositories of the application.
    /// This is a shortcut of <see cref="EventCloudRepositoryBase{TEntity,TPrimaryKey}"/> for <see cref="int"/> primary key.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class EventCloudRepositoryBase<TEntity> : EventCloudRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected EventCloudRepositoryBase(IDbContextProvider<EventCloudDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        // Do not add any method here, add to the class above (since this inherits it)!!!
    }
}
