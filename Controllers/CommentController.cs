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
    public class CommentController : Controller
    {
        //****************************** 
        //Context Setup
        //******************************* 
        private designhubAPIContext _context;

        public CommentController(designhubAPIContext ctx)
        {
            _context = ctx;
        }
        
        //****************************** 
        //Helper Functions
        //******************************* 
        // Checks if the computer already exists in the database
        private bool CommentExists(int commentID)
        {
          return _context.Comment.Count(e => e.CommentID == commentID) > 0;
        }




        //****************************** 
        //GET REQUESTS 
        //******************************* 

        //GET ALL COMMENTS
        // GET /api/comment
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> comments = from comment in _context.Comment select comment;

            if (comments == null)
            {
                return NotFound();
            }

            return Ok(comments);

        }

        //GET SINGLE COMMENT
        // GET api/comment/5
        [HttpGet("{id}", Name="GetSingleComment")]
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
                Comment comment = _context.Comment.Single(m => m.CommentID == id);
                
                if (comment == null)
                {
                    return NotFound();
                }
                
                //if match found, return it
                return Ok(comment);
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

        //POST NEW COMMENT
        // POST api/comment
        [HttpPost]
        public IActionResult Post([FromBody]Comment newComment)
        {
            //checks to make sure everything to create a new entry is present
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //adds comment to local context of DB
            _context.Comment.Add(newComment);
            
            //attempts to save the changes
            try
            {
                _context.SaveChanges();
            }
            //throws an exception if there is an error
            catch (DbUpdateException)
            {
                if (CommentExists(newComment.CommentID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            //returns a GET for the row you just created
            return CreatedAtRoute("GetSingleComment", new { id = newComment.CommentID }, newComment);
        }


        //EDIT COMMENT 
        // PUT api/comment/5

        //REQUIRES FULL COMMENT OBJECT, YA DINGUS
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Comment modifiedComment)
        {
            //checks if full comment object is present
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if the id youre trying to change doesnt match the modified comment, throw exception
            if (id != modifiedComment.CommentID)
            {
                return BadRequest();
            }

            //Edit the entry in the context
            _context.Entry(modifiedComment).State = EntityState.Modified;

            //attempt to save changes
            try
            {
                _context.SaveChanges();
            }
            //throw exception if it messes up
            catch (DbUpdateConcurrencyException)
            {
                //if you cant find the comment let em know
                if (!CommentExists(id))
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
        
        // DELETE api/comment/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Comment comment = _context.Comment.Single(m => m.CommentID == id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comment.Remove(comment);
            _context.SaveChanges();

            return Ok(comment);
        }
    }
}
