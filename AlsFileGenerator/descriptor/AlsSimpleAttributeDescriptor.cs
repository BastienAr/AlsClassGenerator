using System;

namespace AlsFileGenerator.descriptor
{
  [Serializable()]
  public class AlsSimpleAttributeDescriptor : AlsAttributeDescriptor
  {
    public bool IsXmlAttribute { get; set; }
  }
}
