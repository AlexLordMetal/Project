using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.ViewModels
{
    class CurrentDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var date = (DateTime)value;
            if (date >= DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
}
