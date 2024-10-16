﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatGPTClone.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected readonly ISender MediatR;

        public ApiControllerBase(ISender mediatR)
        {
            MediatR = mediatR;
        }
    }
}
