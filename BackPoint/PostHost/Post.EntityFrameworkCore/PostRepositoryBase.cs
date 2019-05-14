using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using Post.EntityFrameworkCore.EntityFrameworkCore;

namespace Post.EntityFrameworkCore
{
    public abstract class PostRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<PostDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected PostRepositoryBase(IDbContextProvider<PostDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {

        }
    }

    public abstract class PostRepostoryBase<TEntity> : PostRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected PostRepostoryBase(IDbContextProvider<PostDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {

        }
    }
}
