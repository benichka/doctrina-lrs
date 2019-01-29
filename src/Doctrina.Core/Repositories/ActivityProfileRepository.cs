﻿using Doctrina.Persistence.Entities;
using Doctrina.Persistence.Entities.Documents;
using Doctrina.xAPI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Doctrina.Persistence.Repositories
{
    public class ActivityProfileRepository : IActivityProfileRepository
    {
        private readonly DoctrinaDbContext _context;

        public ActivityProfileRepository(DoctrinaDbContext context)
        {
            this._context = context;
        }

        public ActivityProfileEntity GetProfile(Iri activityId, string profileId, Guid? registragion = null)
        {

            string strActivityId = activityId.ToString();

            return _context.ActivityProfiles.FirstOrDefault(x => x.Activity.Id == strActivityId && x.ProfileId == profileId && x.RegistrationId == registragion);
        }

        public ActivityProfileEntity GetProfile(Iri activityId, string profileId)
        {

            string strActivityId = activityId.ToString();

            return _context.ActivityProfiles.FirstOrDefault(x => x.Activity.Id == strActivityId && x.ProfileId == profileId);
        }


        public IEnumerable<IDocumentEntity> GetProfilesDocuments(Iri activityId, DateTimeOffset? since = null)
        {
            string strActivityId = activityId.ToString();

            var query = _context.ActivityProfiles.Where(x => x.Activity.Id == strActivityId);

            if (since.HasValue)
            {
                var sinceDate = since.Value;
                query.Where(x => x.UpdateDate >= sinceDate);
            }

            query.OrderByDescending(x => x.UpdateDate);

            return query.Select(x=> x.Document);
        }

        /// <summary>
        /// Delete a single state document
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="agent"></param>
        /// <param name="stateId"></param>
        /// <param name="registration"></param>
        public void DeleteAndSaveChanges(ActivityProfileEntity profile)
        {
            _context.ActivityProfiles.Remove(profile);
            _context.SaveChanges();
        }

        public void AddAndSave(ActivityProfileEntity profile)
        {
            _context.ActivityProfiles.Add(profile);
            _context.SaveChanges();
        }

        public void UpdateAndSave(ActivityProfileEntity profile)
        {
            _context.ActivityProfiles.Update(profile);
            _context.SaveChanges();
        }
    }
}