using System;
using Microsoft.EntityFrameworkCore;


namespace dummyApi.Models
{
    public class TodoContext : DbContext
    {
        //This is how you create an instance
        public TodoContext(DbContextOptions<TodoContext> options) : base(options){}

        public DbSet<TodoItem> TodoItems {get;set;}
    }   
}