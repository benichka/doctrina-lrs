﻿using Doctrina.Domain.Entities.Documents;
using Doctrina.xAPI;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.Application.AgentProfiles.Commands
{
    public class UpdateAgentProfileCommand : IRequest<AgentProfileEntity>
    {
        public Agent Agent { get; set; }
        public string ProfileId { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }

        public static UpdateAgentProfileCommand Create(Agent agent, string profileId, byte[] content, string contentType)
        {
            var cmd = new UpdateAgentProfileCommand()
            {
                Agent = agent,
                ProfileId = profileId,
                Content = content,
                ContentType = contentType
            };

            return cmd;
        }
    }
}
