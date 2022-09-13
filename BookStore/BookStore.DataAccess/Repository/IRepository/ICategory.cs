﻿using BookStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository.IRepository
{
    public interface ICategory:IRepository<Category>
    {
        void Update(Category obj);
        void Save();
    }
}