using NexusSite.Models;
using NKNexusAPIWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusSite.Services
{
	public interface IProffessionals
	{
		IEnumerable<Proffesional> SearchProffesionals(string name);
		string ActivateUser(ProfessionalConfiguration_Root pro);
		ProfessionalConfiguration_Root searchForProffesional(int id);
	}
}
