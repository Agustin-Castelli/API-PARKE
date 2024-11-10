using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ProductCreateRequest
    {
        public string? Code { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImage { get; set; }
    }
}
