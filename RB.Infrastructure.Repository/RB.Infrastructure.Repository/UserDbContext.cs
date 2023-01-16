using MediatR;
using Microsoft.EntityFrameworkCore;
using RB.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB.Infrastructure.RB.Infrastructure.Repository
{
    public class UserDbContext:DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }
        public DbSet<UserRegister> Users { get; set; }
        public DbSet<TempUserRegister> TempUsers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<HostedRides> HostedRides { get; set; }
        public DbSet<RequestHandler> Requests { get; set; }
    }
}
