using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
	public class AddressesModel
	{
        public int Id { get; set; }

        public int PersonId { get; set; }
        public string? Street { get; set; }

        public string? City { get; set; }

        public string? PostalCode { get; set; }

        public string?  Country { get; set; }



    }
}
