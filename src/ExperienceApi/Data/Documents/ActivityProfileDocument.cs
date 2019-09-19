﻿using System;

namespace Doctrina.ExperienceApi.Data.Documents
{
    public class ActivityProfileDocument : Document
    {
        public Iri ActivityId { get; set; }

        public string ProfileId { get; set; }

        public Guid? Registration { get; set; }
    }
}
