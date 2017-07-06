using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G4S.Business.Services
{
    internal class TranslationService
    {
        private readonly ISecurityService _securityService;

        public TranslationService(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        public string GetTranslation(string group, string keyword,  params string[] parameters)
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
