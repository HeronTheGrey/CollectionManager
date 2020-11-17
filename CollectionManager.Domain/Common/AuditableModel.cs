using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CollectionManager.Domain.Common
{
    public class AuditableModel
    {
        [XmlElement("CreatedBy")]
        public int CreatedById { get; set; }
        [XmlElement("CreatedDate")]
        public DateTime CreatedDateTime { get; set; }
        [XmlIgnore]
        public int? ModifiedById { get; set; }
        [XmlIgnore]
        public DateTime? ModifiedDateTime { get; set; }
    }
}
