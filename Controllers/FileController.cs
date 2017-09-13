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
    [Route("[controller]")]
    public class FilesController : Controller
    {
        //****************************** 
        //Context Setup
        //******************************* 
        private designhubAPIContext _context;

        public FilesController(designhubAPIContext ctx)
        {
            _context = ctx;
        }
        
        //****************************** 
        //Helper Functions
        //******************************* 
        // Checks if the computer already exists in the database
        private bool FileExists(int fileID)
        {
          return _context.File.Count(e => e.FileID == fileID) > 0;
        }


        //****************************** 
        //GET REQUESTS 
        //******************************* 

        //GET ALL FILES
        // GET /api/files
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> files = from file in _context.File select file;

            if (files == null)
            {
                return NotFound();
            }

            return Ok(files);

        }

        //GET SINGLE FILE
        // GET api/values/5
        [HttpGet("{id}", Name="GetSingleFile")]
        public IActionResult Get([FromRoute]int id)
        {
            //checks to see if model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //check if there is a fileID that matches our request
            try
            {
                File file = _context.File.Single(m => m.FileID == id);
                
                if (file == null)
                {
                    return NotFound();
                }
                
                //if match found, return it
                return Ok(file);
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

        //POST NEW FILE
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]File newFile)
        {
            //checks to make sure everything to create a new entry is present
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //adds file to local context of DB
            _context.File.Add(newFile);
            
            //attempts to save the changes
            try
            {
                _context.SaveChanges();
            }
            //throws an exception if there is an error
            catch (DbUpdateException)
            {
                if (FileExists(newFile.FileID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            //returns a GET for the row you just created
            return CreatedAtRoute("GetSingleFile", new { id = newFile.FileID }, newFile);
        }




        //POST NEW FILEGROUP
        // POST /file
        [HttpPost("addfilegroup")]
        public IActionResult Post([FromBody]FileGroup newFileGroup)
        {
            //checks to make sure everything to create a new entry is present
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //adds file to local context of DB
            _context.FileGroup.Add(newFileGroup);
            
            //attempts to save the changes
            try
            {
                _context.SaveChanges();
            }
            //throws an exception if there is an error
            catch (DbUpdateException)
            {
                throw;   
            }

            //returns a GET for the row you just created
            return Ok(newFileGroup);
        }

        //EDIT FILE 
        // PUT api/values/5

        //REQUIRES FULL FILE OBJECT
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]File modifiedFile)
        {
            //checks if full file object is present
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if the id youre trying to change doesnt match the modified file, throw exception
            if (id != modifiedFile.FileID)
            {
                return BadRequest();
            }

            //Edit the entry in the context
            _context.Entry(modifiedFile).State = EntityState.Modified;

            //attempt to save changes
            try
            {
                _context.SaveChanges();
            }
            //throw exception if it messes up
            catch (DbUpdateConcurrencyException)
            {
                //if you cant find the file let em know
                if (!FileExists(id))
                {
                    return NotFound();
                }
                //if files there but still error
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
        
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            File file = _context.File.Single(m => m.FileID == id);
            if (file == null)
            {
                return NotFound();
            }

            _context.File.Remove(file);
            _context.SaveChanges();

            return Ok(file);
        }
    }
}
