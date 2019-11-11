using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APITask.Models;
using System.Web.Http;

namespace APITask.Controllers
{
    public class CountryController : ApiController
    {

        CountryEntities2 context = new CountryEntities2();

        // Get all Countries
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var Country = context.Countries.Select(x => new CountryDTO()
            {
                Country_ID = x.Country_ID,
                CountryName = x.CountryName,
            }).ToList();
      
            return Ok(Country);
        }

        // get by id 
        [HttpGet]
        public IHttpActionResult GetById(int id )
        {
            Country Country = context.Countries.Find(id);
            if (Country == null)
            {
                return NotFound();
            }
                return Ok(Country);
         
        }

        // get by name 
        [HttpGet]
        [Route("api/country/{name:alpha}")]
        public IHttpActionResult GetbyName( string name)
        {
            Country CountryName = context.Countries.Where(x => x.CountryName == name).FirstOrDefault();

            if (CountryName == null)
            {
                return NotFound();
            }
                return Ok(CountryName);
            
        }

        // Post 
        [HttpPost]

        public IHttpActionResult Post(Country CN)
        {
            if (CN == null)
            {
                return BadRequest();
            }
            else
            {
                context.Countries.Add(CN);
                context.SaveChanges();
                return Created("Add to list", context.Countries.ToList());
            }
        }

        // put 

        public IHttpActionResult put(int id , Country CN)
        {
            Country country = context.Countries.Find(id);

            if (country != null)
            {
                country.CountryName = CN.CountryName;
                context.SaveChanges();
                return Ok(country);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        

        }
        // delete

        public IHttpActionResult delete(int id)
        {
            Country country = context.Countries.Find(id);

            if (country == null)
            {
                return NotFound();
            }
            else
            {
                context.Countries.Remove(country);
                context.SaveChanges();
                return Ok(country);
            }

        }


    }
}
