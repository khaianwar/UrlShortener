using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repository.Model;
using Repository.Service;
using System;
using UrlShortener.Model;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UriController : ControllerBase
    {
        private readonly UriService _uriService;

        public UriController(UriService uriService)
        {
            _uriService = uriService;
        }

        [HttpGet]
        public string Get(string uriName)
        {
            Uri uriResult;
            UriResultModel result = new UriResultModel();
            bool isValidUri = Uri.TryCreate(uriName, UriKind.Absolute, out uriResult);
            result.IsValid = isValidUri;
            if (result.IsValid)
            {
                UriModel model = _uriService.GetByUri(uriResult.AbsoluteUri);
                if (model == null)
                {
                    string tempId = Helper.Generator.GetNextId();
                    while (_uriService.GetByShortUri(tempId) != null)
                        tempId = Helper.Generator.GetNextId();
                    model = _uriService.Create(uriResult.AbsoluteUri, tempId);
                }
                result.Url = model.ShortUri;
            }

            return JsonConvert.SerializeObject(result);
        }
    }
}
