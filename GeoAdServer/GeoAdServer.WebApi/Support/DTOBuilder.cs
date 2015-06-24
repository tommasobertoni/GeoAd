﻿using GeoAdServer.Domain.Entities;
using GeoAdServer.Domain.Entities.DTOs;
using GeoAdServer.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoAdServer.WebApi.Support
{
    public static class DTOBuilder
    {
        public static OfferingDTO BuildDTO(this Offering o, int id)
        {
            return new OfferingDTO
            {
                Id = id,
                Name = o.Desc,
                LocationId = o.LocationId,
                Desc = o.Desc,
                ExpDateMillis = o.ExpDateMillis
            };
        }

        public static LocationDTO BuildDTO(this LocationApiModel locApi, int id, string type)
        {
            return new LocationDTO
            {
                Id = id,
                PCat = locApi.PCat,
                SCat = locApi.SCat,
                Name = locApi.Name,
                Lat = locApi.Lat,
                Lng = locApi.Lat,
                Desc = locApi.Desc,
                Type = type
            };
        }
    }
}