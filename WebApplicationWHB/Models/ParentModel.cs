using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationWHB.Models
{
    public class ParentModel
    {
       

            public lastCity lastCity { get; set; }
            public startCity startCity { get; set; }
            public OrderModel OrderModel{ get; set; }
            public busDate busDate { get; set; }
            public busTime busTime { get; set; }
            public ankbu Ankbu { get; set; }
            public Manager Manager { get; set; }
        

    }
}