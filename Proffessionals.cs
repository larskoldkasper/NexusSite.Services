using NexusSite.Models;
using NKNexusAPIWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusSite.Services
{
	public class Proffessionals : IProffessionals
	{
		List<Proffesional> proffesionalsList;

		public string ActivateUser(ProfessionalConfiguration_Root pro)
		{
			nkProfessional prof = new nkProfessional(NKNexusAPIWrapper.NexusEnviroment.eTest);
			if (prof != null)
			{
				nkNexusResult result = prof.ProfesionnalSetActive(pro.Id);
				if (result != null && result.httpStatusCode == System.Net.HttpStatusCode.OK)
				{
					return $"Bruger: {pro.PrimaryIdentifier} er aktiveret!";
				}
				if (result != null)
					return $"IKKE Aktiveret. Fejl: {result.httpStatusCode} - {result.httpStatusText}";
			}
			return "** UKENDT FEJL OPSTÅET!. kontakt system administrator. **";
		}

		public ProfessionalConfiguration_Root searchForProffesional(int id)
		{
			nkProfessional prof = new nkProfessional(NKNexusAPIWrapper.NexusEnviroment.eTest);
			return prof.GetProfessionalConfiguration(id);
		}

		public IEnumerable<Proffesional> SearchProffesionals(string name)
		{
			proffesionalsList = new List<Proffesional>();
			nkProfessional prof = new nkProfessional(NKNexusAPIWrapper.NexusEnviroment.eTest);
			List<Professional_Root> vikarliste = prof.GetSubstitutes(name);
			foreach (Professional_Root profroot in vikarliste)
			{
				proffesionalsList.Add(new Proffesional() 
				{ 
					Id = profroot.Id, 
					Enabled = profroot.Active, 
                    FirstName = profroot.FirstName,
                    FullName = profroot.FullName,
                    DepartmentName=profroot.DepartmentName,
                    Name = profroot.PrimaryIdentifier 
				});
                

            }

			return proffesionalsList;
		}
	}
}
