﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;

namespace DotNetCoreSample.Models
{
    
        public class Response<T>
        {
            public HttpStatusCode Status { get; set; }
            public string Message { get; set; }
            public T Data { get; set; }

        }
    
}
