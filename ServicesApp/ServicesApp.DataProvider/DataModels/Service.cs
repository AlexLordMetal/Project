﻿using ServicesApp.DataProvider.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.DataProvider.DataModels
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }

        public virtual ServiceCategory Category { get; set; }

        public virtual List<ServiceProviderService> ServiceProviderServices { get; set; }
    }
}