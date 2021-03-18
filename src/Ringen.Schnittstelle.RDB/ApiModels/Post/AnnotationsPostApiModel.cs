﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Ringen.Schnittstelle.RDB.ApiModels.Post
{
    public class AnnotationsPostApiModel
    {
        [JsonProperty("points")]
        public RoundValuePostApiModel Points { get; set; }

        [JsonProperty("duration")]
        public RoundValuePostApiModel Duration { get; set; }
    }
}
