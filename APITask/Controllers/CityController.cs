using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APITask.Models;

namespace APITask.Controllers
{
    
    public class CityController : ApiController
    {
        CountryEntities2 context = new CountryEntities2();

        // get all cities

       [HttpGet]
       public IHttpActionResult Getall()
        {
           
           
                var City = context.Cities.Select(x => new CityDTO()
                {
                    City_ID = x.City_ID,
                    CityName = x.CityName,
                    Location = x.Location,
                }).ToList();

                return Ok(City);
            }

        // get by id 
        [HttpGet]

        public IHttpActionResult getByID(int id)
        {
            City city = context.Cities.Find(id);
            if (city == null)
            {
                return NotFound();

            }
            else
            {
                return Ok(city);
            }
        }
            // get by name 
            [Route("api/city/{name:alpha}")]
            [HttpGet]
            public IHttpActionResult GetByName(string name)
        {
            City city = context.Cities.Where(x => x.CityName == name).FirstOrDefault();
            if (city == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(city);
            }
        }

        // post
        [HttpPost]
        public IHttpActionResult Post(City Cy)
        {
            if (Cy == null)
            {
                return BadRequest();
            }
            else
            {
                context.Cities.Add(Cy);
                context.SaveChanges();
                return Created("add to list", context.Cities.ToList());
            }
        }

        // put
        public IHttpActionResult Put(int id , City Cy) {

            City city = context.Cities.Find(id);
            if(city!= null)
            {
                city.CityName = Cy.CityName;
                city.Location = Cy.Location;
                context.SaveChanges();
                return Ok(city);
            }
            else
            {
               return StatusCode(HttpStatusCode.NoContent); 
            }
        
        }

        //delete
        public IHttpActionResult Delete(int id)
        {
            City city = context.Cities.Find(id);
            if(city == null)
            {
                return NotFound();
            }
            else
            {
                context.Cities.Remove(city);
                context.SaveChanges();
                return Ok(city);
            }
        }

        }
    }

