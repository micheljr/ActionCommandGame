using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ActionCommandGame.Repository
{
    public class ActionButtonUsersDbContext : IdentityDbContext
    {
        public ActionButtonUsersDbContext(DbContextOptions<ActionButtonUsersDbContext> options) : base(options)
        {
        }
    }
}
