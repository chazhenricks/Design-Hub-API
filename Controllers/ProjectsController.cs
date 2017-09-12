using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using designhubAPI.data;
using designhubAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace designhubAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        //****************************** 
        //Context Setup
        //******************************* 
        private designhubAPIContext _context;

        public ProjectsController(designhubAPIContext ctx)
        {
            _context = ctx;
        }
        
        //****************************** 
        //Helper Functions
        //******************************* 
        // Checks if the computer already exists in the database
        private bool ProjectExists(int projectsID)
        {
          return _context.Projects.Count(e => e.ProjectsID == projectsID) > 0;
        }




        //****************************** 
        //GET REQUESTS 
        //******************************* 

        //GET ALL Projects
        // GET /api/projects
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> projects = from project in _context.Projects select project;

            if (projects == null)
            {
                return NotFound();
            }

            return Ok(projects);

        }

        //GET SINGLE COMMENT
        // GET api/projects/5
        [HttpGet("{id}", Name="GetSingleProject")]
        public IActionResult Get([FromRoute]int id)
        {
            //checks to see if model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //check if there is a projectID that matches our request
            try
            {
                Projects project = _context.Projects.Single(m => m.ProjectsID == id);
                
                if (project == null)
                {
                    return NotFound();
                }
                
                //if match found, return it
                return Ok(project);
            }

            //if no file found send 404
            catch (System.InvalidOperationException ex)
            {
                return NotFound(ex);
            }
        }

        //****************************** 
        //POST REQUESTS
        //******************************* 

        //POST NEW PROJECT
        // POST api/projects
        [HttpPost]
        public IActionResult Post([FromBody]Projects newProject)
        {
            //checks to make sure everything to create a new entry is present
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //adds project to local context of DB
            _context.Projects.Add(newProject);
            
            //attempts to save the changes
            try
            {
                _context.SaveChanges();
            }
            //throws an exception if there is an error
            catch (DbUpdateException)
            {
                if (ProjectExists(newProject.ProjectsID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            //returns a GET for the row you just created
            return CreatedAtRoute("GetSingleProject", new { id = newProject.ProjectsID }, newProject);
        }


        //EDIT PROJECT 
        // PUT api/projects/5

        //REQUIRES FULL PROJECT OBJECT, YA DINGUS
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Projects modifiedProject)
        {
            //checks if full project object is present
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if the id youre trying to change doesnt match the modified project, throw exception
            if (id != modifiedProject.ProjectsID)
            {
                return BadRequest();
            }

            //Edit the entry in the context
            _context.Entry(modifiedProject).State = EntityState.Modified;

            //attempt to save changes
            try
            {
                _context.SaveChanges();
            }
            //throw exception if it messes up
            catch (DbUpdateConcurrencyException)
            {
                //if you cant find the project let em know
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                //if comment is there but still error
                else
                {
                    //¯\_(ツ)_/¯
                    throw;
                }
            }

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        //****************************** 
        //DELETE REQUESTS
        //******************************* 
        
        // DELETE api/projects/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Projects project = _context.Projects.Single(m => m.ProjectsID == id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            _context.SaveChanges();

            return Ok(project);
        }
    }
}
