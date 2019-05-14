﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.xAPI
{
    public static class MediaTypes
    {
        public static class Application
        {
            public const string Json = "application/json";
            public const string OctetStream = "application/octet-stream";
        }

        public static class Multipart
        {
            public const string Mixed = "multipart/mixed";
        }
    }
}