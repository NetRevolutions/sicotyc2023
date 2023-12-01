﻿using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class LookupCodeGroupRepository : RepositoryBase<LookupCodeGroup>, ILookupCodeGroupRepository
    {
        public LookupCodeGroupRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {            
        }

        public IEnumerable<LookupCodeGroup> GetAllLookupCodeGroups(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(x => x.Name)
            .ToList();

        public LookupCodeGroup GetLookupCodeGroup(Guid lookupCodeGroupId, bool trackChanges) => 
            FindByCondition(l => l.Id.Equals(lookupCodeGroupId), trackChanges)            
            .SingleOrDefault();

        public void CreateLookupCodeGroup(LookupCodeGroup lookupCodeGroup) => Create(lookupCodeGroup);
                
        public IEnumerable<LookupCodeGroup> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
            FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToList();

        public void DeleteLookupCodeGroup(LookupCodeGroup lookupCodeGroup)
        {
            Delete(lookupCodeGroup);
        }
    }
}
