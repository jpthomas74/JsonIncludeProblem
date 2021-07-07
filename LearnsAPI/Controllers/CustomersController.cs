using LearnData;
using LearnsAPI.Databontext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CustomersController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpPost]
        [Route("getincluded")]
        public async Task<IEnumerable<Customer>> GetSelectedValues()
        {
            var data = await _appDbContext.Customers.Include(ca=> ca.CustomerAddresses).ToListAsync();
            return data;

        }

        [HttpPost]
        [Route("getexcluded")]
        public async Task<IEnumerable<Customer>> GetSelectedValuesNotInclude()
        {
            var data = await _appDbContext.Customers.ToListAsync();
            return data;

        }

    }
}
