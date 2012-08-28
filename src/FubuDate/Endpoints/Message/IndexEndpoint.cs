
using System;
using Raven.Client;

namespace FubuDate.Endpoints.Message
{
    public class IndexEndpoint
    {
        private readonly IDocumentSession _session;

        public IndexEndpoint(IDocumentSession session)
        {
            _session = session;
        }

        public MessageIndexViewModel Get(MessageIndexRequest request)
        {
            return new MessageIndexViewModel{From = request.From};
        }

        public MessageIndexViewModel Post(MessageIndexRequest request)
        {
            var message = new Domain.Message
                              {
                                  Body = request.Message,
                                  To = request.To,
                                  From = request.From,
                                  Sent = DateTime.Now
                              };

            _session.Store(message);

            return new MessageIndexViewModel {IsMessageSent = true, From = request.From};
        }
    }
}