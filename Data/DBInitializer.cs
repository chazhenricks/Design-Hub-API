using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using designhubAPI.Models;
using System.Threading.Tasks;

namespace designhubAPI.data
{
    //Seeds databse with intial data for testing
    public static class DbInitializer
    {
        //runs on startup
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new designhubAPIContext(serviceProvider.GetRequiredService<DbContextOptions<designhubAPIContext>>()))
            {
                //Look for Projects
                if(context.Projects.Any())
                {
                    return; //this means that the table exists and doesnt need to run
                }
                //***********************************
                //Create new instances of Projects
                //***********************************
                var projects = new Projects[]
                {
                    new Projects{
                       Name = "Walmart Campaign",
                       DateCreated = DateTime.Now
                    },
                    new Projects{
                        Name = "LP Campaign",
                        DateCreated = new DateTime(2016, 03, 28)
                    },
                    new Projects{
                        Name = "Exxon Campaign",
                        DateCreated = new DateTime(2015, 03, 28)
                    }
                };
                // Adds each new Project into the context
                foreach (Projects p in projects)
                {
                    context.Projects.Add(p);
                }
                // Saves the customers to the database
                context.SaveChanges();


                //***********************************
                //Create new instances of FileGroup
                //***********************************

                var fileGroups = new FileGroup[]
                {
                    new FileGroup{
                        DateCreated = new DateTime(2017, 03, 28),
                        Name = "Walmart CEO Photo",
                    },
                    new FileGroup{
                        DateCreated = new DateTime(2017, 03, 28),
                        Name = "Walmart Speech",
                    },
                    new FileGroup{
                        DateCreated = new DateTime(2017, 03, 28),
                        Name = "LP CEO Photo",
                    },
                    new FileGroup{
                        DateCreated = new DateTime(2017, 03, 28),
                        Name = "LP CEO Speech",
                    },
                    new FileGroup{
                        DateCreated = new DateTime(2017, 03, 28),
                        Name = "Exxon CEO Photo",
                    },
                    new FileGroup{
                        DateCreated = new DateTime(2017, 03, 28),
                        Name = "Exxon Pitch Proposal",
                    }
                };

                // Adds each new FileGroup into the context
                foreach (FileGroup fg in fileGroups)
                {
                    context.FileGroup.Add(fg);
                }
                // Saves the customers to the database
                context.SaveChanges();

                //***********************************
                //Create new instances of Files
                //***********************************
                
                var files = new File[]
                {
                    new File{
                        DateCreated = new DateTime(2017, 03, 28),
                        FilePath = "Path/To/File",
                        FileGroupID = fileGroups.Single(fg => fg.Name == "Walmart CEO Photo").FileGroupID
                    },
                    new File{
                        DateCreated = new DateTime(2017, 03, 29),
                        FilePath = "Path/To/File",
                        FileGroupID = fileGroups.Single(fg => fg.Name == "Walmart CEO Photo").FileGroupID
                    },
                    new File{
                        DateCreated = new DateTime(2017, 03, 30),
                        FilePath = "Path/To/File",
                        FileGroupID = fileGroups.Single(fg => fg.Name == "Walmart CEO Photo").FileGroupID
                    },
                    new File{
                        DateCreated = new DateTime(2017, 03, 28),
                        FilePath = "Path/To/File",
                        FileGroupID = fileGroups.Single(fg => fg.Name == "Walmart Speech").FileGroupID
                    },
                    new File{
                        DateCreated = new DateTime(2017, 03, 29),
                        FilePath = "Path/To/File",
                        FileGroupID = fileGroups.Single(fg => fg.Name == "Walmart Speech").FileGroupID
                    },
                    new File{
                        DateCreated = new DateTime(2017, 03, 30),
                        FilePath = "Path/To/File",
                        FileGroupID = fileGroups.Single(fg => fg.Name == "Walmart Speech").FileGroupID
                    },
                    new File{
                        DateCreated = new DateTime(2017, 03, 28),
                        FilePath = "Path/To/File",
                        FileGroupID = fileGroups.Single(fg => fg.Name == "LP CEO Photo").FileGroupID
                    },
                    new File{
                        DateCreated = new DateTime(2017, 03, 29),
                        FilePath = "Path/To/File",
                        FileGroupID = fileGroups.Single(fg => fg.Name == "LP CEO Photo").FileGroupID
                    },
                    new File{
                        DateCreated = new DateTime(2017, 03, 30),
                        FilePath = "Path/To/File",
                        FileGroupID = fileGroups.Single(fg => fg.Name == "LP CEO Photo").FileGroupID
                    },
                    new File{
                        DateCreated = new DateTime(2017, 03, 28),
                        FilePath = "Path/To/File",
                        FileGroupID = fileGroups.Single(fg => fg.Name == "LP CEO Speech").FileGroupID
                    },
                    new File{
                        DateCreated = new DateTime(2017, 03, 29),
                        FilePath = "Path/To/File",
                        FileGroupID = fileGroups.Single(fg => fg.Name == "LP CEO Speech").FileGroupID
                    },
                    new File{
                        DateCreated = new DateTime(2017, 03, 30),
                        FilePath = "Path/To/File",
                        FileGroupID = fileGroups.Single(fg => fg.Name == "LP CEO Speech").FileGroupID
                    },
                    new File{
                        DateCreated = new DateTime(2017, 03, 28),
                        FilePath = "Path/To/File",
                        FileGroupID = fileGroups.Single(fg => fg.Name == "Exxon CEO Photo").FileGroupID
                    },
                    new File{
                        DateCreated = new DateTime(2017, 03, 29),
                        FilePath = "Path/To/File",
                        FileGroupID = fileGroups.Single(fg => fg.Name == "Exxon CEO Photo").FileGroupID
                    },
                    new File{
                        DateCreated = new DateTime(2017, 03, 30),
                        FilePath = "Path/To/File",
                        FileGroupID = fileGroups.Single(fg => fg.Name == "Exxon CEO Photo").FileGroupID
                    },
                    new File{
                        DateCreated = new DateTime(2017, 03, 28),
                        FilePath = "Path/To/File",
                        FileGroupID = fileGroups.Single(fg => fg.Name == "Exxon Pitch Proposal").FileGroupID
                    },
                    new File{
                        DateCreated = new DateTime(2017, 03, 29),
                        FilePath = "Path/To/File",
                        FileGroupID = fileGroups.Single(fg => fg.Name == "Exxon Pitch Proposal").FileGroupID
                    },
                    new File{
                        DateCreated = new DateTime(2017, 03, 30),
                        FilePath = "Path/To/File",
                        FileGroupID = fileGroups.Single(fg => fg.Name == "Exxon Pitch Proposal").FileGroupID
                    }
                };
                // Adds each new File into the context
                foreach(File f in files)
                {
                    context.File.Add(f);
                }
                // Saves the customers to the database
                context.SaveChanges();

                //***********************************
                //Create new instances of Comments
                //***********************************

                var comments = new Comment[]{
                    new Comment{
                        Message = "Change one thing",
                        DateCreated = new DateTime(2017, 03, 30),
                        FileID = files.Single(f => f.FileID == 1).FileID    
                    },
                    new Comment{
                        Message = "Great!",
                        DateCreated = new DateTime(2017, 03, 31),
                        FileID = files.Single(f => f.FileID == 16).FileID    
                    }
                };

                foreach(Comment c in comments)
                {
                    context.Comment.Add(c);
                }
                context.SaveChanges();



                //***********************************
                //Create new instances of ProjectFileGroups
                //***********************************

                var projectFileGroups = new ProjectFileGroup[]{
                    new ProjectFileGroup{
                        ProjectsID = 1,
                        FileGroupID = 1
                    },
                     new ProjectFileGroup{
                        ProjectsID = 1,
                        FileGroupID = 2
                    },
                     new ProjectFileGroup{
                        ProjectsID = 2,
                        FileGroupID = 3
                    },
                     new ProjectFileGroup{
                        ProjectsID = 2,
                        FileGroupID = 4
                    },
                     new ProjectFileGroup{
                        ProjectsID = 3,
                        FileGroupID = 5
                    },
                     new ProjectFileGroup{
                        ProjectsID = 3,
                        FileGroupID = 6
                    }
                };

                foreach(ProjectFileGroup pfg in projectFileGroups)
                {
                    context.ProjectFileGroup.Add(pfg);
                }

                context.SaveChanges();

            }//End Using
        }//End Intialize method
    } //End class
}//End Namespace 