using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ActionCommandGame.Repository
{
    public class ActionButtonUsersDbContext : DbContext
    {
        public ActionButtonUsersDbContext(DbContextOptions<ActionButtonUsersDbContext> options) : base(options)
        {

        }
    }
}
