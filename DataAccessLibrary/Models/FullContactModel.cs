using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
	public class FullContactModel
	{
        public BasicPeopleModel BasicInfo { get; set; } = new BasicPeopleModel();

		public List<AddressesModel> Address { get; set; } = new List<AddressesModel>();

	
    }
}
