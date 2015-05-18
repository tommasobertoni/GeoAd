﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoAdServer.Domain.Contracts;
using System.Collections.Generic;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using GeoAdServer.Postgresql;
using GeoAdServer.Domain.Entities;
using GeoAdServer.Domain.Entities.DTOs;

namespace GeoAdServer.Domain.ContractsImplementations.Test
{
    [TestClass]
    public class LocationsRepository
    {
        [TestMethod]
        public void LocationsRepositoryTest()
        {
            //Container initialization
            string connectionString = "Server=localhost;Port=5012;UserId=postgres;Password=admin;Database=GeoAdDb";

            var container = new WindsorContainer();
            container.Register(
                Component.For<ILocationsRepository>()
                         .ImplementedBy<PostgresqlLocationsRepository>()
                         .DependsOn(Dependency.OnValue("connectionString", connectionString))
                         .Named("pgrslocrepo"));

            //Test
            ILocationsRepository locationRepository = container.Resolve<ILocationsRepository>();

            //Category Test
            string newCategory = "aggghtldon";
            Assert.IsFalse(locationRepository.DeleteCategory(newCategory));
            Assert.IsTrue(locationRepository.InsertCategory(newCategory) != -1);
            Assert.IsTrue(locationRepository.GetCategories().ContainsValue(newCategory));
            Assert.IsTrue(locationRepository.DeleteCategory(newCategory));
            Assert.IsFalse(locationRepository.GetCategories().ContainsValue(newCategory));

            //Location Test
            var enumerator = locationRepository.GetCategories().GetEnumerator();
            enumerator.MoveNext(); int pCatId = enumerator.Current.Key;

            var loc = new Location
            {
                UserId = "-a1",
                PCatId = pCatId,
                Name = "TestLocation",
                Lat = "11",
                Lng = "12",
                Desc = "A description",
                Type = "POI"
            };
            int id = locationRepository.Insert(loc);
            LocationDTO locdto1 = locationRepository.GetById(id);
            loc.Desc = "New description";
            Assert.IsTrue(locationRepository.Update(id, loc));
            Assert.IsFalse(locationRepository.Update(-1, loc));
            LocationDTO locdto2 = locationRepository.GetById(id);
            Assert.AreNotEqual(locdto1, locdto2);
            var locs = new List<LocationDTO>(locationRepository.GetByUserId("-a1"));
            Assert.IsTrue(locs.Count == 1);
            Assert.AreEqual(locs[0], locdto2);
            Assert.IsTrue(locationRepository.DeleteById(id));
            var newLocs = new List<LocationDTO>(locationRepository.GetByUserId("-a1"));
            Assert.IsTrue(newLocs.Count == 0);
            Assert.IsNull(locationRepository.GetById(id));
        }
    }
}