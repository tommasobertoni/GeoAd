﻿using GeoAdServer.DataAccess;
using GeoAdServer.Domain.Contracts;
using GeoAdServer.Domain.Entities;
using GeoAdServer.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace GeoAdServer.WebApi.Support
{
    public static class Utils
    {
        public static string GetUserId(this HttpRequestContext context)
        {
            return context.Principal.Identity.GetUserId();
        }

        public static string GetUserId(this IIdentity identity)
        {
            return identity.Name.GetHashCode().ToString();
        }

        public static bool IsLocationOwner(this int id, string userId)
        {
            using (var repos = DataRepos.Instance)
            {
                return id.IsLocationOwner(userId, repos.Locations);
            }
        }

        public static bool IsLocationOwner(this int id, string userId, ILocationsRepository repoloc)
        {
            var owner = repoloc.GetOwnerId(id);
            if (owner == null) return false;

            return owner == userId;
        }

        public static bool IsAdmin(this IIdentity identity)
        {
            return identity.Name.CompareTo("admin@geoad.com") == 0;
        }

        public static Location CreateLocationFromModel(this LocationApiModel model, string userId, ILocationsRepository repository, IIdentity identity)
        {
            int pCatId;
            int? sCatId = null;

            if ((pCatId = repository.GetCategoryId(model.PCat).Value) == -1) pCatId = repository.InsertCategory(model.PCat, null);
            if (model.SCat != null && (sCatId = repository.GetCategoryId(model.SCat).Value) == -1) repository.InsertCategory(model.SCat, pCatId);

            var typeId = identity.IsAdmin() ? 1 : 0;

            return new Location
            {
                UserId = userId,
                PCatId = pCatId,
                SCatId = sCatId,
                Name = model.Name,
                Lat = model.Lat,
                Lng = model.Lng,
                Desc = model.Desc,
                Type = Types.Values[typeId]
            };
        }
    }
}