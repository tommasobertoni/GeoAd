﻿using GeoAdServer.Domain.Entities;
using GeoAdServer.Domain.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoAdServer.Domain.Contracts
{
    public interface IPhotosRepository
    {
        IEnumerable<PhotoDTO> GetByLocationId(int locationId);

        PhotoDTO GetById(int photoId);

        void Insert(Photo photo);

        void Update(int id, Photo photo);

        bool Delete(int photoId);
    }
}
