﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfoSystem.App.DataBase.ReposInterfaces;
using InfoSystem.Infrastructure.Entities;
using InfoSystem.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InfoSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EAVController : ControllerBase
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IPropertiesRepository _propertiesRepository;
        private readonly IValuesRepository _valuesRepository;

        public EAVController(IUnitOfWork uof)
        {
            _entityRepository = uof.EntityRepos;
            _propertiesRepository = uof.PropertiesRepos;
            _valuesRepository = uof.ValuesRepos;
        }
        
        [HttpPost("PostEntity")]
        public void PostEntity([FromBody] string receivedEntity)
        {
            var entity = JsonConvert.DeserializeObject<Entity>(receivedEntity);
            _entityRepository.Add(entity);

            foreach (var prop in entity.Properties)
            {
                _valuesRepository.Add(prop.Value);
                _propertiesRepository.Add(prop);
                //_valuesRepository.Add(new Values{Property = entity.Properties.First(x => x == prop)});
            }
        }

        [HttpGet("GetEntities")]
        public IEnumerable<Entity> GetEntities()
        {
            return _entityRepository.Get().ToList();
        }

        [HttpPost("PostProperty")]
        public void PostProperty([FromBody] string receivedProperty)
        {
            var prop = JsonConvert.DeserializeObject<Properties>(receivedProperty);
            _propertiesRepository.Add(prop);
        }

        [HttpGet("GetProperties")]
        public IEnumerable<Properties> GetProperties()
        {
            return _propertiesRepository.Get().ToList();
        }
    }
}