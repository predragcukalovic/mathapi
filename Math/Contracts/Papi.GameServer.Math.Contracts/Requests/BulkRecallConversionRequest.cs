using System;
using System.Collections.Generic;
using System.Text;

namespace Papi.GameServer.Math.Contracts.Requests
{
    public class BulkRecallConversionRequest
    {
        public List<RecallConversionRequest> RecallConversionRequests { get; set; }
    }
}
