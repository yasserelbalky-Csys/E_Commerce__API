﻿using DAL.Contracts;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    internal class ApplicationUserRepository : BaseRepository<Brands>, IBrandRepository
    {
        public ApplicationUserRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}